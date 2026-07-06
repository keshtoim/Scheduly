using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ClosedXML.Excel;

namespace testing
{
    public partial class ImportWorkloadForm : Form
    {
        // ── Модель строки из файла ─────────────────────────────────────────
        private class ImportRow
        {
            public int    LineNumber;
            public string RawClass, RawSubject, RawDifficulty, RawTeacher, RawHours, RawSubgroup;
            public int    ClassId, SubjectParallelId, TeacherId, Hours;
            public int?   Subgroup;           // null = весь класс
            public string Status;             // "ok" | "duplicate" | "error"
            public string StatusText;         // текст для колонки Статус
        }

        private List<ImportRow> _rows = new List<ImportRow>();

        public ImportWorkloadForm() { InitializeComponent(); }

        private void ImportWorkloadForm_Load(object sender, EventArgs e)
        {
            // Настраиваем столбцы предпросмотра
            previewGrid.AutoGenerateColumns = false;
            previewGrid.CellFormatting     += previewGrid_CellFormatting;

            AddCol("colRow",      "#",        45);
            AddCol("colClass",    "Класс",    62);
            AddCol("colSubject",  "Предмет", 185);
            AddCol("colDiff",     "Сложн.",   60);
            AddCol("colTeacher",  "Учитель", 195);
            AddCol("colHours",    "Часов",    55);
            AddCol("colSubgroup", "Подгр.",   65);
            var colStatus = new DataGridViewTextBoxColumn
            {
                Name = "colStatus", HeaderText = "Статус",
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            previewGrid.Columns.Add(colStatus);

            UpdateButtons();
        }

        private void AddCol(string name, string header, int width)
        {
            previewGrid.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = name, HeaderText = header, Width = width, ReadOnly = true
            });
        }

        // ── Открыть файл ──────────────────────────────────────────────────
        private void buttonOpen_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog
            {
                Title  = "Выберите файл нагрузки",
                Filter = "Excel файл (*.xlsx)|*.xlsx"
            })
            {
                if (ofd.ShowDialog() != DialogResult.OK) return;
                labelPath.Text = ofd.FileName;
                try
                {
                    ParseFile(ofd.FileName);
                    BuildPreview();
                    UpdateButtons();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка чтения файла:\n" + ex.Message,
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        // ── Парсинг и валидация ───────────────────────────────────────────
        private void ParseFile(string path)
        {
            _rows.Clear();
            using (var wb = new XLWorkbook(path))
            {
                var ws = wb.Worksheet(1);
                int lastRow = ws.LastRowUsed()?.RowNumber() ?? 1;
                for (int r = 2; r <= lastRow; r++)
                {
                    string cls  = ws.Cell(r, 1).GetValue<string>().Trim();
                    string subj = ws.Cell(r, 2).GetValue<string>().Trim();
                    string diff = ws.Cell(r, 3).GetValue<string>().Trim();
                    string tchr = ws.Cell(r, 4).GetValue<string>().Trim();
                    string hrs  = ws.Cell(r, 5).GetValue<string>().Trim();
                    string sg   = ws.Cell(r, 6).GetValue<string>().Trim();

                    if (string.IsNullOrEmpty(cls) && string.IsNullOrEmpty(subj)) continue;

                    var row = new ImportRow
                    {
                        LineNumber     = r,
                        RawClass       = cls,
                        RawSubject     = subj,
                        RawDifficulty  = diff,
                        RawTeacher     = tchr,
                        RawHours       = hrs,
                        RawSubgroup    = sg
                    };
                    Validate(row);
                    _rows.Add(row);
                }
            }
        }

        private void Validate(ImportRow row)
        {
            // 1. Класс
            if (!TryParseClass(row.RawClass, out int classId, out int grade))
            {
                Fail(row, $"Класс «{row.RawClass}» не найден в БД"); return;
            }
            row.ClassId = classId;

            // 2. Сложность
            if (!decimal.TryParse(row.RawDifficulty.Replace('.', ','),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.CurrentCulture,
                out decimal diff) || diff <= 0)
            {
                Fail(row, $"Некорректная сложность «{row.RawDifficulty}»"); return;
            }

            // 3. Предмет → SubjectByParallel
            if (!TryFindSubjectParallel(row.RawSubject, grade, diff, out int subjectParallelId))
            {
                Fail(row, $"Предмет «{row.RawSubject}» для {grade} кл. (сл.{diff}) не найден"); return;
            }
            row.SubjectParallelId = subjectParallelId;

            // 4. Учитель
            if (!TryFindTeacher(row.RawTeacher, out int teacherId))
            {
                Fail(row, $"Учитель «{row.RawTeacher}» не найден в БД"); return;
            }
            row.TeacherId = teacherId;

            // 5. Часы
            if (!int.TryParse(row.RawHours, out int hours) || hours <= 0 || hours > 99)
            {
                Fail(row, $"Некорректные часы «{row.RawHours}»"); return;
            }
            row.Hours = hours;

            // 6. Подгруппа
            string sgTrim = row.RawSubgroup.Trim();
            if (string.IsNullOrEmpty(sgTrim) || sgTrim == "-")
                row.Subgroup = null;
            else if (sgTrim == "1") row.Subgroup = 1;
            else if (sgTrim == "2") row.Subgroup = 2;
            else { Fail(row, $"Подгруппа: допустимы 1, 2 или пусто (получено «{sgTrim}»)"); return; }

            // 7. Дубль
            try
            {
                object sgParam = row.Subgroup.HasValue ? (object)row.Subgroup.Value : DBNull.Value;
                bool dup = DbHelper.Exists(
                    "SELECT COUNT(*) FROM Workload " +
                    "WHERE ID_класса = @cl AND ID_предмета_параллели = @sp AND Подгруппа IS @sg",
                    p => {
                        p.AddWithValue("@cl", classId);
                        p.AddWithValue("@sp", subjectParallelId);
                        p.AddWithValue("@sg", sgParam);
                    });
                if (dup)
                { row.Status = "duplicate"; row.StatusText = "⊘ Уже существует (пропустим)"; return; }
            }
            catch { /* не критично — попробуем добавить */ }

            row.Status     = "ok";
            row.StatusText = "✓ Готово к импорту";
        }

        private static void Fail(ImportRow row, string msg)
        {
            row.Status     = "error";
            row.StatusText = "✗ " + msg;
        }

        // ── Поиск по БД ───────────────────────────────────────────────────
        private bool TryParseClass(string text, out int classId, out int grade)
        {
            classId = 0; grade = 0;
            text = text.Trim().ToUpper();
            int i = 0;
            while (i < text.Length && char.IsDigit(text[i])) i++;
            if (i == 0 || i >= text.Length) return false;
            if (!int.TryParse(text.Substring(0, i), out grade)) return false;
            // Локальные копии для использования в лямбде (out-параметры нельзя захватывать)
            int   gradeVal  = grade;
            string letter   = text.Substring(i).Trim();

            object id = DbHelper.Scalar(
                "SELECT cl.ID_класса FROM Classes cl " +
                "JOIN ParallelClass pc ON cl.ID_параллели_класса = pc.ID_параллели_класса " +
                "JOIN LetterClass   lc ON cl.ID_буквы_класса     = lc.ID_буквы_класса " +
                "WHERE pc.Параллель = @g AND lc.Буква = @l",
                p => { p.AddWithValue("@g", gradeVal); p.AddWithValue("@l", letter); });
            if (id == null || id == DBNull.Value) return false;
            classId = Convert.ToInt32(id);
            return true;
        }

        private bool TryFindSubjectParallel(string name, int grade, decimal diff, out int id)
        {
            id = 0;
            object obj = DbHelper.Scalar(
                "SELECT sbp.ID_предмета_со_сложностью " +
                "FROM SubjectByParallel sbp " +
                "JOIN Subjects      sub ON sbp.ID_предмета  = sub.ID_предмета " +
                "JOIN ParallelClass pc  ON sbp.ID_параллели = pc.ID_параллели_класса " +
                "JOIN Difficulty    d   ON sbp.ID_сложности = d.ID_сложности " +
                "WHERE LOWER(sub.Название) = LOWER(@n) AND pc.Параллель = @g AND d.Сложность = @d",
                p => { p.AddWithValue("@n", name); p.AddWithValue("@g", grade); p.AddWithValue("@d", diff); });
            if (obj == null || obj == DBNull.Value) return false;
            id = Convert.ToInt32(obj);
            return true;
        }

        private bool TryFindTeacher(string name, out int id)
        {
            id = 0;
            if (string.IsNullOrWhiteSpace(name)) return false;
            name = name.Trim();

            // Попытка 1: полное имя "Фамилия Имя Отчество"
            object obj = DbHelper.Scalar(
                "SELECT ID_учителя FROM Teachers " +
                "WHERE LOWER(Фамилия || ' ' || Имя || ' ' || Отчество) = LOWER(@n)",
                p => p.AddWithValue("@n", name));
            if (obj != null && obj != DBNull.Value) { id = Convert.ToInt32(obj); return true; }

            // Попытка 2: "Фамилия И.О." или "Фамилия И. О."
            // Убираем пробелы вокруг точек, нормализуем
            string[] spaceParts = name.Split(new[] { ' ' }, 2, StringSplitOptions.RemoveEmptyEntries);
            if (spaceParts.Length >= 2)
            {
                string surname  = spaceParts[0];
                // initials: берём первые буквы из каждой части после разбивки по '.'
                string tail     = spaceParts[1];
                var    initials = tail.Split(new[] { '.', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (initials.Length >= 2)
                {
                    string fi = initials[0].Substring(0, 1).ToUpper();
                    string oi = initials[1].Substring(0, 1).ToUpper();
                    obj = DbHelper.Scalar(
                        "SELECT ID_учителя FROM Teachers " +
                        "WHERE LOWER(Фамилия) = LOWER(@s) " +
                        "AND UPPER(SUBSTR(Имя,1,1)) = @fi " +
                        "AND UPPER(SUBSTR(Отчество,1,1)) = @oi",
                        p => {
                            p.AddWithValue("@s",  surname);
                            p.AddWithValue("@fi", fi);
                            p.AddWithValue("@oi", oi);
                        });
                    if (obj != null && obj != DBNull.Value) { id = Convert.ToInt32(obj); return true; }
                }
            }

            // Попытка 3: только фамилия (если в БД ровно один учитель с такой фамилией)
            DataTable dt = DbHelper.Query(
                "SELECT ID_учителя FROM Teachers WHERE LOWER(Фамилия) = LOWER(@s)",
                p => p.AddWithValue("@s", name));
            if (dt.Rows.Count == 1) { id = Convert.ToInt32(dt.Rows[0][0]); return true; }

            return false;
        }

        // ── Предпросмотр ──────────────────────────────────────────────────
        private void BuildPreview()
        {
            previewGrid.Rows.Clear();
            foreach (var row in _rows)
            {
                previewGrid.Rows.Add(
                    row.LineNumber,
                    row.RawClass,
                    row.RawSubject,
                    row.RawDifficulty,
                    row.RawTeacher,
                    row.RawHours,
                    string.IsNullOrEmpty(row.RawSubgroup) ? "—" : row.RawSubgroup,
                    row.StatusText);
            }
        }

        private void previewGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0 || e.RowIndex >= _rows.Count) return;
            switch (_rows[e.RowIndex].Status)
            {
                case "ok":
                    e.CellStyle.BackColor = Color.Honeydew;
                    e.CellStyle.ForeColor = Color.DarkGreen;
                    break;
                case "duplicate":
                    e.CellStyle.BackColor = Color.LightYellow;
                    e.CellStyle.ForeColor = Color.DarkGoldenrod;
                    break;
                case "error":
                    e.CellStyle.BackColor = Color.MistyRose;
                    e.CellStyle.ForeColor = Color.Crimson;
                    break;
            }
        }

        private void UpdateButtons()
        {
            int okCnt  = _rows.Count(r => r.Status == "ok");
            int dupCnt = _rows.Count(r => r.Status == "duplicate");
            int errCnt = _rows.Count(r => r.Status == "error");

            labelStatus.Text = _rows.Count == 0
                ? "Откройте .xlsx файл для предпросмотра"
                : $"Всего строк: {_rows.Count}    ✓ Готово: {okCnt}    ⊘ Дублей: {dupCnt}    ✗ Ошибок: {errCnt}";

            buttonImport.Enabled = okCnt > 0;
            buttonImport.Text    = okCnt > 0
                ? $"▶  Импортировать {okCnt} строк"
                : "▶  Нет строк для импорта";
        }

        // ── Импорт ────────────────────────────────────────────────────────
        private void buttonImport_Click(object sender, EventArgs e)
        {
            var valid = _rows.Where(r => r.Status == "ok").ToList();
            if (valid.Count == 0) return;

            int imported = 0, skipped = 0;
            foreach (var row in valid)
            {
                try
                {
                    DbHelper.ExecProcNonQuery("sp_AddWorkload", p =>
                    {
                        p.AddWithValue("@ID_учителя",                row.TeacherId);
                        p.AddWithValue("@ID_класса",                 row.ClassId);
                        p.AddWithValue("@ID_предмета_параллели",     row.SubjectParallelId);
                        p.AddWithValue("@Количество_часов_в_неделю", (byte)row.Hours);
                        p.AddWithValue("@Подгруппа",
                            row.Subgroup.HasValue ? (object)row.Subgroup.Value : DBNull.Value);
                    });
                    row.Status     = "imported";
                    row.StatusText = "✓ Импортировано";
                    imported++;
                }
                catch (Exception ex)
                {
                    row.Status     = "error";
                    row.StatusText = "✗ " + ex.Message;
                    skipped++;
                }
            }

            BuildPreview();
            UpdateButtons();
            MessageBox.Show(
                $"Импорт завершён.\nДобавлено записей: {imported}" +
                (skipped > 0 ? $"\nПропущено (ошибок): {skipped}" : ""),
                "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (imported > 0) DialogResult = DialogResult.OK;
        }

        // ── Скачать шаблон ────────────────────────────────────────────────
        private void buttonTemplate_Click(object sender, EventArgs e)
        {
            using (var sfd = new SaveFileDialog
            {
                Title    = "Сохранить шаблон",
                Filter   = "Excel файл (*.xlsx)|*.xlsx",
                FileName = "шаблон_нагрузки.xlsx"
            })
            {
                if (sfd.ShowDialog() != DialogResult.OK) return;
                try
                {
                    CreateTemplate(sfd.FileName);
                    MessageBox.Show("Шаблон сохранён:\n" + sfd.FileName,
                        "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка сохранения:\n" + ex.Message,
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void CreateTemplate(string path)
        {
            using (var wb = new XLWorkbook())
            {
                var ws = wb.Worksheets.Add("Нагрузка");

                // Заголовки
                string[] headers = { "Класс", "Предмет", "Сложность", "Учитель", "Часов/нед", "Подгруппа" };
                for (int c = 0; c < headers.Length; c++)
                {
                    var cell = ws.Cell(1, c + 1);
                    cell.Value = headers[c];
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.LightSteelBlue;
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                }

                // Примеры строк
                object[,] examples =
                {
                    { "5А",  "Математика",    2, "Иванов И.И.",   3, ""  },
                    { "5А",  "Русский язык",  1, "Петрова А.В.",  4, ""  },
                    { "5Б",  "Математика",    2, "Иванов И.И.",   3, ""  },
                    { "10А", "Информатика",   3, "Сидоров К.М.",  2, "1" },
                    { "10А", "Информатика",   3, "Сидоров К.М.",  2, "2" },
                };
                for (int r = 0; r < examples.GetLength(0); r++)
                    for (int c = 0; c < examples.GetLength(1); c++)
                        ws.Cell(r + 2, c + 1).Value = XLCellValue.FromObject(examples[r, c]);

                // Ширины столбцов
                int[] widths = { 8, 22, 12, 22, 12, 12 };
                for (int c = 0; c < widths.Length; c++)
                    ws.Column(c + 1).Width = widths[c];

                // Лист с подсказками
                var wsHelp = wb.Worksheets.Add("Инструкция");
                wsHelp.Cell(1, 1).Value = "ФОРМАТ ФАЙЛА";
                wsHelp.Cell(1, 1).Style.Font.Bold = true;
                wsHelp.Cell(1, 1).Style.Font.FontSize = 14;
                string[] tips =
                {
                    "Класс       — название класса: 5А, 10Б и т.д. (буква кириллицей, регистр не важен)",
                    "Предмет     — точное название из справочника Предметы (регистр не важен)",
                    "Сложность   — числовое значение (например: 1, 2, 3)",
                    "Учитель     — ФИО полностью: «Иванов Иван Иванович»",
                    "              или сокращённо: «Иванов И.И.» или «Иванов И. И.»",
                    "Часов/нед   — целое число (например: 2, 3, 4)",
                    "Подгруппа   — 1, 2 или пусто (пусто = весь класс)",
                    "",
                    "Строки с незнакомыми данными будут отмечены красным в предпросмотре.",
                    "Дубли (уже существующие записи) будут отмечены жёлтым и пропущены.",
                    "Строка 1 (заголовок) всегда пропускается.",
                };
                for (int i = 0; i < tips.Length; i++)
                    wsHelp.Cell(i + 2, 1).Value = tips[i];
                wsHelp.Column(1).Width = 75;

                wb.SaveAs(path);
            }
        }

        private void buttonClose_Click(object sender, EventArgs e) => Close();
    }
}
