using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace testing
{
    public partial class ViewScheduleControl : UserControl
    {
        private static readonly string[] DayNames =
            { "", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
        private const int MAX_LESSONS = 8;

        public ViewScheduleControl() { InitializeComponent(); }

        public void LoadSchedule()
        {
            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadFilters();
            RebuildGrid();
        }

        private void LoadFilters()
        {
            try
            {
                DataTable dtClass = DbHelper.Query(
                    "SELECT class_id, CAST(cp.parallel AS NVARCHAR) + lc.letterClass AS class_name " +
                    "FROM Classes cl " +
                    "JOIN ClassParallel cp ON cl.id_parallel_class = cp.id_parallel_class " +
                    "JOIN LetterOfTheClass lc ON cl.id_letter_class = lc.id_letter_class " +
                    "ORDER BY cp.parallel, lc.letterClass");
                DataRow allClasses = dtClass.NewRow();
                allClasses["class_id"]   = DBNull.Value;
                allClasses["class_name"] = "Все классы";
                dtClass.Rows.InsertAt(allClasses, 0);
                // DisplayMember/ValueMember до DataSource
                comboClass.DisplayMember = "class_name";
                comboClass.ValueMember   = "class_id";
                comboClass.DataSource    = dtClass;

                DataTable dtTeacher = DbHelper.Query(
                    "SELECT teacher_id, " +
                    "surname + ' ' + name + ' ' + ISNULL(patronymic, '') AS full_name " +
                    "FROM Teachers ORDER BY surname, name");
                DataRow allTeachers = dtTeacher.NewRow();
                allTeachers["teacher_id"] = DBNull.Value;
                allTeachers["full_name"]  = "Все учителя"; // правильное имя колонки
                dtTeacher.Rows.InsertAt(allTeachers, 0);
                // DisplayMember/ValueMember до DataSource
                comboTeacher.DisplayMember = "full_name";
                comboTeacher.ValueMember   = "teacher_id";
                comboTeacher.DataSource    = dtTeacher;
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка фильтров"); }
        }

        private void RebuildGrid()
        {
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();

            string classFilter   = comboClass.SelectedValue?.ToString();
            string teacherFilter = comboTeacher.SelectedValue?.ToString();
            int    dayFilter     = comboDayFilter.SelectedIndex;

            bool filterByClass   = !string.IsNullOrEmpty(classFilter)   && classFilter   != "";
            bool filterByTeacher = !string.IsNullOrEmpty(teacherFilter) && teacherFilter != "";

            // ── Build columns ────────────────────────────────────────────────
            // When showing all classes: columns = Classes, rows = Day+Lesson
            // When filtering by class: columns = Days, rows = Lesson (classic view)

            if (filterByClass)
                BuildSingleClassView(classFilter, teacherFilter, dayFilter);
            else
                BuildAllClassesView(teacherFilter, dayFilter);
        }

        // Classic single-class view: rows = lessons, columns = days
        private void BuildSingleClassView(string classId, string teacherFilter, int dayFilter)
        {
            // Header column: lesson number
            var lessonCol = new DataGridViewTextBoxColumn();
            lessonCol.Name = "lesson"; lessonCol.HeaderText = "Урок";
            lessonCol.Width = 60; lessonCol.ReadOnly = true;
            lessonCol.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            lessonCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            lessonCol.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dataGrid.Columns.Add(lessonCol);

            for (int d = 1; d <= 5; d++)
            {
                if (dayFilter > 0 && dayFilter != d) continue;
                var col = new DataGridViewTextBoxColumn();
                col.Name = "day" + d; col.HeaderText = DayNames[d];
                col.Width = 220; col.ReadOnly = true;
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGrid.Columns.Add(col);
            }

            for (int l = 1; l <= MAX_LESSONS; l++)
                dataGrid.Rows.Add(l.ToString());

            StyleRows();
            FillSingleClassData(classId, teacherFilter, dayFilter);
        }

        private void FillSingleClassData(string classId, string teacherFilter, int dayFilter)
        {
            string sql =
                "SELECT s.day_of_week, s.lesson_number, sub.subject_name, " +
                "t.surname + ' ' + t.name + ' ' + t.patronymic AS teacher_name, cr.room_number, w.teacher_id " +
                "FROM Schedule s " +
                "JOIN Workload w   ON s.workload_id  = w.workload_id " +
                "JOIN Subjects sub ON w.subject_id   = sub.subject_id " +
                "JOIN Teachers t   ON w.teacher_id   = t.teacher_id " +
                "JOIN Classrooms cr ON s.classroom_id = cr.classroom_id " +
                "WHERE w.class_id = " + classId;
            if (!string.IsNullOrEmpty(teacherFilter) && teacherFilter != "")
                sql += " AND w.teacher_id = " + teacherFilter;
            if (dayFilter > 0)
                sql += " AND s.day_of_week = " + dayFilter;

            try
            {
                DataTable dt = DbHelper.Query(sql);
                DataTable conflicts = GetAllConflicts();

                foreach (DataRow row in dt.Rows)
                {
                    int day    = Convert.ToInt32(row["day_of_week"]);
                    int lesson = Convert.ToInt32(row["lesson_number"]);
                    string colName = "day" + day;
                    if (!dataGrid.Columns.Contains(colName)) continue;
                    var cell = dataGrid.Rows[lesson - 1].Cells[colName];
                    cell.Value = string.Format("{0}\n{1}\nКаб. {2}",
                        row["subject_name"], row["teacher_name"], row["room_number"]);

                    if (IsConflict(conflicts, day, lesson))
                    {
                        cell.Style.BackColor = Color.MistyRose;
                        cell.Style.ForeColor = Color.DarkRed;
                    }
                }
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка данных"); }
        }

        // All-classes view: rows = class+lesson, columns = days
        private void BuildAllClassesView(string teacherFilter, int dayFilter)
        {
            // Columns: Class | Lesson | Mon | Tue | Wed | Thu | Fri
            var classCol = new DataGridViewTextBoxColumn();
            classCol.Name = "class_name"; classCol.HeaderText = "Класс";
            classCol.Width = 70; classCol.ReadOnly = true;
            classCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            classCol.DefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            classCol.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dataGrid.Columns.Add(classCol);

            var lessonCol = new DataGridViewTextBoxColumn();
            lessonCol.Name = "lesson"; lessonCol.HeaderText = "Ур.";
            lessonCol.Width = 40; lessonCol.ReadOnly = true;
            lessonCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            lessonCol.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dataGrid.Columns.Add(lessonCol);

            for (int d = 1; d <= 5; d++)
            {
                if (dayFilter > 0 && dayFilter != d) continue;
                var col = new DataGridViewTextBoxColumn();
                col.Name = "day" + d; col.HeaderText = DayNames[d];
                col.Width = 200; col.ReadOnly = true;
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGrid.Columns.Add(col);
            }

            try
            {
                DataTable classes = DbHelper.Query(
                    "SELECT cl.class_id, CAST(cp.parallel AS NVARCHAR) + lc.letterClass AS class_name " +
                    "FROM Classes cl " +
                    "JOIN ClassParallel cp ON cl.id_parallel_class = cp.id_parallel_class " +
                    "JOIN LetterOfTheClass lc ON cl.id_letter_class = lc.id_letter_class " +
                    "ORDER BY cp.parallel, lc.letterClass");

                DataTable conflicts = GetAllConflicts();

                foreach (DataRow cls in classes.Rows)
                {
                    int classId = Convert.ToInt32(cls["class_id"]);
                    string className = cls["class_name"].ToString();

                    // Add MAX_LESSONS rows per class
                    int startRow = dataGrid.Rows.Count;
                    for (int l = 1; l <= MAX_LESSONS; l++)
                    {
                        int rowIdx = dataGrid.Rows.Add();
                        dataGrid.Rows[rowIdx].Cells["class_name"].Value = (l == 1) ? className : "";
                        dataGrid.Rows[rowIdx].Cells["lesson"].Value     = l.ToString();
                        dataGrid.Rows[rowIdx].Height = 46;

                        // Alternating class background
                        bool odd = (classes.Rows.IndexOf(cls) % 2 == 0);
                        Color baseBg = odd ? Color.White : Color.FromArgb(248, 252, 255);
                        for (int d = 1; d <= 5; d++)
                            if (dataGrid.Columns.Contains("day" + d))
                                dataGrid.Rows[rowIdx].Cells["day" + d].Style.BackColor = baseBg;
                    }

                    // Fill data for this class
                    string sql =
                        "SELECT s.day_of_week, s.lesson_number, sub.subject_name, " +
                        "t.surname + ' ' + t.name + ' ' + t.patronymic AS teacher_name, cr.room_number " +
                        "FROM Schedule s " +
                        "JOIN Workload w   ON s.workload_id  = w.workload_id " +
                        "JOIN Subjects sub ON w.subject_id   = sub.subject_id " +
                        "JOIN Teachers t   ON w.teacher_id   = t.teacher_id " +
                        "JOIN Classrooms cr ON s.classroom_id = cr.classroom_id " +
                        "WHERE w.class_id = " + classId;
                    if (!string.IsNullOrEmpty(teacherFilter) && teacherFilter != "")
                        sql += " AND w.teacher_id = " + teacherFilter;
                    if (dayFilter > 0)
                        sql += " AND s.day_of_week = " + dayFilter;

                    DataTable dt = DbHelper.Query(sql);
                    foreach (DataRow row in dt.Rows)
                    {
                        int day    = Convert.ToInt32(row["day_of_week"]);
                        int lesson = Convert.ToInt32(row["lesson_number"]);
                        string colName = "day" + day;
                        if (!dataGrid.Columns.Contains(colName)) continue;
                        int rowIdx = startRow + lesson - 1;
                        var cell = dataGrid.Rows[rowIdx].Cells[colName];
                        cell.Value = string.Format("{0}\n{1} / {2}",
                            row["subject_name"], row["teacher_name"], row["room_number"]);

                        if (IsConflict(conflicts, day, lesson))
                        {
                            cell.Style.BackColor = Color.MistyRose;
                            cell.Style.ForeColor = Color.DarkRed;
                        }
                    }


                }

                // Make class name cells span visually with bold
                foreach (DataGridViewRow row in dataGrid.Rows)
                {
                    if (row.Cells["class_name"].Value?.ToString() != "")
                    {
                        row.Cells["class_name"].Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                        row.Cells["class_name"].Style.BackColor = Color.FromArgb(230, 240, 255);
                    }
                    else
                    {
                        row.Cells["class_name"].Style.BackColor = Color.FromArgb(230, 240, 255);
                    }
                }
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка расписания"); }
        }

        private void StyleRows()
        {
            for (int i = 0; i < dataGrid.Rows.Count; i++)
            {
                dataGrid.Rows[i].Height = 52;
                dataGrid.Rows[i].DefaultCellStyle.BackColor = (i % 2 == 0)
                    ? Color.White : Color.FromArgb(248, 252, 255);
            }
        }

        private DataTable GetAllConflicts()
        {
            return DbHelper.Query(
                "SELECT DISTINCT s1.day_of_week, s1.lesson_number FROM Schedule s1 " +
                "JOIN Workload w1 ON s1.workload_id = w1.workload_id " +
                "JOIN Schedule s2 ON s1.day_of_week = s2.day_of_week " +
                "  AND s1.lesson_number = s2.lesson_number AND s1.schedule_id <> s2.schedule_id " +
                "JOIN Workload w2 ON s2.workload_id = w2.workload_id " +
                "WHERE w1.teacher_id = w2.teacher_id OR s1.classroom_id = s2.classroom_id");
        }

        private bool IsConflict(DataTable conflicts, int day, int lesson)
        {
            foreach (DataRow cr in conflicts.Rows)
                if (Convert.ToInt32(cr["day_of_week"]) == day &&
                    Convert.ToInt32(cr["lesson_number"]) == lesson)
                    return true;
            return false;
        }

        private void buttonApplyFilter_Click(object sender, EventArgs e) { RebuildGrid(); }

        private void buttonResetFilter_Click(object sender, EventArgs e)
        {
            comboClass.SelectedIndex     = 0;
            comboTeacher.SelectedIndex   = 0;
            comboDayFilter.SelectedIndex = 0;
            textSearch.Clear();
            RebuildGrid();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            ExportToExcel();
        }

        // ── Поиск ────────────────────────────────────────────────────────────

        /// <summary>
        /// Подсвечивает ячейки содержащие текст поиска.
        /// Вызывается при изменении текста в поле поиска.
        /// </summary>
        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            // Если поле очищено — сразу сбрасываем подсветку
            if (string.IsNullOrEmpty(textSearch.Text))
                ApplySearch("");
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            ApplySearch(textSearch.Text.Trim());
        }

        private void textSearch_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
                ApplySearch(textSearch.Text.Trim());
        }

        /// <summary>
        /// Проходит по всем ячейкам грида и подсвечивает совпадения жёлтым.
        /// Если строка поиска пустая — снимает подсветку поиска
        /// (конфликты при этом сохраняют свой красный цвет).
        /// </summary>
        private void ApplySearch(string query)
        {
            bool hasQuery = !string.IsNullOrEmpty(query);

            // Шаг 1 — сбрасываем ВСЕ ячейки кроме конфликтных (без исключений по пустым)
            foreach (DataGridViewRow row in dataGrid.Rows)
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Style.ForeColor == Color.DarkRed) continue;
                    cell.Style.BackColor = Color.Empty;
                    cell.Style.ForeColor = Color.Empty;
                }

            if (!hasQuery)
            {
                labelSearchHint.Text = "";
                return;
            }

            // Шаг 2 — применяем новую подсветку
            int count = 0;
            foreach (DataGridViewRow row in dataGrid.Rows)
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string colName = dataGrid.Columns[cell.ColumnIndex].Name;
                    if (colName == "class_name" || colName == "lesson") continue;

                    string val = cell.Value?.ToString() ?? "";
                    if (string.IsNullOrWhiteSpace(val) || val == "+") continue;

                    bool isConflict = cell.Style.ForeColor == Color.DarkRed;

                    if (val.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        cell.Style.BackColor = Color.Gold;
                        if (!isConflict) cell.Style.ForeColor = Color.Black;
                        count++;
                    }
                    else
                    {
                        if (!isConflict) cell.Style.ForeColor = Color.LightGray;
                    }
                }

            labelSearchHint.Text      = count > 0 ? string.Format("Найдено: {0}", count) : "Не найдено";
            labelSearchHint.ForeColor = count > 0 ? Color.SeaGreen : Color.Crimson;
        }

        private void ExportToExcel()
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter   = "Excel файл (*.xlsx)|*.xlsx",
                FileName = "Расписание.xlsx",
                Title    = "Сохранить расписание"
            };
            if (dlg.ShowDialog() != DialogResult.OK) return;

            try
            {
                ScheduleExporter.Export(dlg.FileName);
                MessageBox.Show("Файл сохранён:\n" + dlg.FileName,
                    "Экспорт завершён", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Ошибка экспорта"); }
        }
    }
}
