using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace testing
{
    public partial class ComposeScheduleControl : UserControl
    {
        private static readonly string[] DayNames =
            { "", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
        private const int MAX_LESSONS = 8;
        private int _selectedClassId = -1;
        private int _weekView = 0; // 0=обе, 1=нечётная, 2=чётная

        // Цвета чётности недели
        private static readonly Color ColorOddWeek  = Color.FromArgb(218, 234, 255); // голубой — нечётные
        private static readonly Color ColorEvenWeek = Color.FromArgb(235, 218, 255); // лиловый — чётные

        // Цвета сложности (не совпадают с MistyRose конфликтов)
        private static readonly Color ColorEasy   = Color.FromArgb(220, 255, 220); // светло-зелёный  1-3
        private static readonly Color ColorMedium = Color.FromArgb(255, 253, 200); // светло-жёлтый   4-7
        private static readonly Color ColorHard   = Color.FromArgb(255, 220, 180); // светло-оранжевый 8-12
        private static readonly Color ColorEasyFore   = Color.FromArgb(0, 128, 0);
        private static readonly Color ColorMediumFore = Color.FromArgb(160, 120, 0);
        private static readonly Color ColorHardFore   = Color.FromArgb(180, 60, 0);

        // Сложность по уроку за день: diffByRow[урок-1][день-1] = сумма сложностей
        private int[,] _diffBySlot = new int[MAX_LESSONS, 5];

        public ComposeScheduleControl() { InitializeComponent(); }

        public void LoadData()
        {
            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;
            AddWeekLegendItems();
            LoadClasses();
        }

        private void AddWeekLegendItems()
        {
            int y = panelLegend.Controls[panelLegend.Controls.Count - 1].Bottom + 8;

            var picOdd = new System.Windows.Forms.PictureBox
                { BackColor = ColorOddWeek, BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                  Location = new System.Drawing.Point(8, y), Size = new System.Drawing.Size(16, 16) };
            var lblOdd = new System.Windows.Forms.Label
                { AutoSize = true, Location = new System.Drawing.Point(28, y + 2),
                  Text = "Нечётная неделя" };

            int y2 = y + 24;
            var picEven = new System.Windows.Forms.PictureBox
                { BackColor = ColorEvenWeek, BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle,
                  Location = new System.Drawing.Point(8, y2), Size = new System.Drawing.Size(16, 16) };
            var lblEven = new System.Windows.Forms.Label
                { AutoSize = true, Location = new System.Drawing.Point(28, y2 + 2),
                  Text = "Чётная неделя" };

            panelLegend.Controls.AddRange(new System.Windows.Forms.Control[]
                { picOdd, lblOdd, picEven, lblEven });
        }

        private void LoadClasses()
        {
            try
            {
                DataTable dt = DbHelper.Query(
                    "SELECT ID_класса, " +
                    "CAST(pc.Параллель AS TEXT) || lc.Буква AS class_name " +
                    "FROM Classes cl " +
                    "JOIN ParallelClass pc ON cl.ID_параллели_класса = pc.ID_параллели_класса " +
                    "JOIN LetterClass lc ON cl.ID_буквы_класса = lc.ID_буквы_класса " +
                    "ORDER BY pc.Параллель, lc.Буква");
                listBoxClasses.DisplayMember = "class_name";
                listBoxClasses.ValueMember   = "ID_класса";
                listBoxClasses.DataSource    = dt;
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка классов"); }
        }

        private void listBoxClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxClasses.SelectedItem == null) return;
            var drv = listBoxClasses.SelectedItem as DataRowView;
            if (drv == null) return;
            _selectedClassId = Convert.ToInt32(drv["ID_класса"]);
            BuildScheduleGrid();
        }

        private void radioWeekView_CheckedChanged(object sender, EventArgs e)
        {
            if (radioWeekViewOdd.Checked)       _weekView = 1;
            else if (radioWeekViewEven.Checked) _weekView = 2;
            else                                _weekView = 0;
            FillGridFromDb();
        }

        private void BuildScheduleGrid()
        {
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();

            // Колонка «Урок»
            var lessonCol = new DataGridViewTextBoxColumn
            {
                Name = "lesson", HeaderText = "Урок", Width = 50, ReadOnly = true
            };
            lessonCol.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            lessonCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGrid.Columns.Add(lessonCol);

            for (int d = 1; d <= 5; d++)
            {
                var col = new DataGridViewButtonColumn
                {
                    Name = "day" + d, HeaderText = DayNames[d],
                    Width = 180, FlatStyle = FlatStyle.Flat
                };
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGrid.Columns.Add(col);
            }

            // Строки уроков 1–8
            for (int l = 1; l <= MAX_LESSONS; l++)
                dataGrid.Rows.Add(l.ToString());

            // Строка сложности дня (под уроками)
            int diffRowIdx = dataGrid.Rows.Add("Сл.");
            dataGrid.Rows[diffRowIdx].Tag = "diffRow";
            dataGrid.Rows[diffRowIdx].Height = 28;
            dataGrid.Rows[diffRowIdx].Cells["lesson"].Style.Font =
                new Font("Segoe UI", 8F, FontStyle.Bold);
            dataGrid.Rows[diffRowIdx].Cells["lesson"].Style.BackColor = Color.WhiteSmoke;
            dataGrid.Rows[diffRowIdx].Cells["lesson"].ToolTipText =
                "Суммарная сложность предметов за день по СанПиН";
            for (int d = 1; d <= 5; d++)
            {
                var c = dataGrid.Rows[diffRowIdx].Cells["day" + d];
                c.Style.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
                c.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                c.Value = "";
            }

            FillGridFromDb();
        }

        private void FillGridFromDb()
        {
            if (_selectedClassId < 0) return;

            // Сброс уроков
            _diffBySlot = new int[MAX_LESSONS, 5];
            int diffRowIdx = -1;
            for (int i = 0; i < dataGrid.Rows.Count; i++)
                if ((string)dataGrid.Rows[i].Tag == "diffRow") { diffRowIdx = i; break; }

            for (int l = 0; l < MAX_LESSONS; l++)
                for (int d = 1; d <= 5; d++)
                {
                    var cell = dataGrid.Rows[l].Cells["day" + d];
                    cell.Value = "+";
                    cell.Style.BackColor = Color.FromArgb(245, 245, 245);
                    cell.Style.ForeColor = Color.LightGray;
                    cell.Tag = null;
                }

            // Сброс строки сложности
            if (diffRowIdx >= 0)
                for (int d = 1; d <= 5; d++)
                {
                    var c = dataGrid.Rows[diffRowIdx].Cells["day" + d];
                    c.Value = "";
                    c.Style.BackColor = Color.WhiteSmoke;
                    c.Style.ForeColor = Color.Silver;
                    c.ToolTipText = "";
                }

            try
            {
                string weekCond = _weekView == 1 ? " AND Чётность_недели IN (0, 1)"
                                : _weekView == 2 ? " AND Чётность_недели IN (0, 2)"
                                : "";
                DataTable dt = DbHelper.Query(
                    "SELECT ID_расписания, ID_нагрузки, ID_кабинета, Кабинет, " +
                    "ID_дня_недели, Номер_урока, ID_учителя, ФИО_учителя, " +
                    "Предмет, ID_предмета_со_сложностью, Сложность, " +
                    "Чётность_недели, Пометка_недели, Подгруппа, Пометка_подгруппы " +
                    "FROM vw_Schedule WHERE ID_класса = @cid" + weekCond,
                    p => p.AddWithValue("@cid", _selectedClassId));

                foreach (DataRow row in dt.Rows)
                {
                    int day    = Convert.ToInt32(row["ID_дня_недели"]);
                    int lesson = Convert.ToInt32(row["Номер_урока"]);
                    int diff   = Convert.ToInt32(row["Сложность"]);
                    if (day < 1 || day > 5 || lesson < 1 || lesson > MAX_LESSONS) continue;

                    var cell = dataGrid.Rows[lesson - 1].Cells["day" + day];

                    // Tag хранит List<DataRow> для поддержки нескольких уроков в одном слоте
                    if (!(cell.Tag is List<DataRow>))
                        cell.Tag = new List<DataRow>();
                    ((List<DataRow>)cell.Tag).Add(row);

                    cell.Value           = BuildCellText((List<DataRow>)cell.Tag, _weekView);
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.BackColor = GetCellWeekColor((List<DataRow>)cell.Tag);

                    _diffBySlot[lesson - 1, day - 1] += diff;
                }

                // Заполняем строку сложности: суммируем по каждому дню
                if (diffRowIdx >= 0)
                {
                    for (int d = 1; d <= 5; d++)
                    {
                        int total = 0;
                        for (int l = 0; l < MAX_LESSONS; l++)
                            total += _diffBySlot[l, d - 1];

                        var c = dataGrid.Rows[diffRowIdx].Cells["day" + d];
                        if (total > 0)
                        {
                            c.Value = total.ToString();
                            ApplyDiffColor(c, total);
                        }
                        else
                        {
                            c.Value = "—";
                            c.Style.BackColor = Color.WhiteSmoke;
                            c.Style.ForeColor = Color.Silver;
                        }
                    }
                }

                HighlightConflicts();
                LoadWarnings();
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка расписания класса"); }
        }

        /// <summary>
        /// Окрашивает ячейку сложности по СанПиН:
        /// 1–14 — лёгкий (зелёный), 15–25 — средний (жёлтый), 26+ — тяжёлый (оранжевый).
        /// Границы соответствуют суммарной сложности ЗА ДЕНЬ (не за один урок).
        /// Цвета специально не совпадают с цветом конфликтов (MistyRose / DarkRed).
        /// </summary>
        private static void ApplyDiffColor(DataGridViewCell cell, int diff)
        {
            if (diff <= 14)
            {
                cell.Style.BackColor = ColorEasy;
                cell.Style.ForeColor = ColorEasyFore;
                cell.ToolTipText = $"Суммарная сложность дня: {diff} — лёгкий день";
            }
            else if (diff <= 25)
            {
                cell.Style.BackColor = ColorMedium;
                cell.Style.ForeColor = ColorMediumFore;
                cell.ToolTipText = $"Суммарная сложность дня: {diff} — средняя нагрузка";
            }
            else
            {
                cell.Style.BackColor = ColorHard;
                cell.Style.ForeColor = ColorHardFore;
                cell.ToolTipText = $"Суммарная сложность дня: {diff} — высокая нагрузка (СанПиН: не рекомендуется)";
            }
        }

        private void HighlightConflicts()
        {
            try
            {
                DataTable conflicts = DbHelper.Query(
                    "SELECT ID_дня_недели, Номер_урока FROM vw_Conflicts " +
                    "WHERE ID_расписания_1 IN (" +
                    "  SELECT s.ID_расписания FROM Schedule s " +
                    "  JOIN Workload w ON s.ID_нагрузки = w.ID_нагрузки " +
                    "  WHERE w.ID_класса = @cid)",
                    p => p.AddWithValue("@cid", _selectedClassId));

                foreach (DataRow cr in conflicts.Rows)
                {
                    int d = Convert.ToInt32(cr["ID_дня_недели"]);
                    int l = Convert.ToInt32(cr["Номер_урока"]);
                    if (d < 1 || d > 5 || l < 1 || l > MAX_LESSONS) continue;
                    var cell = dataGrid.Rows[l - 1].Cells["day" + d];
                    cell.Style.BackColor = Color.MistyRose;
                    cell.Style.ForeColor = Color.DarkRed;
                }
            }
            catch { }
        }

        private void LoadWarnings()
        {
            listBoxWarnings.Items.Clear();
            try
            {
                DataTable dt = DbHelper.ExecProc("sp_GetClassConflicts",
                    p => p.AddWithValue("@ID_класса", _selectedClassId));

                bool hasConflicts = dt.Rows.Count > 0;

                // Проверка превышения ставки учителей
                List<string> overloadWarnings = CheckTeacherOverload();

                if (!hasConflicts && overloadWarnings.Count == 0)
                {
                    listBoxWarnings.Items.Add("Конфликтов нет ✓");
                    listBoxWarnings.ForeColor    = Color.Green;
                    labelWarningsTitle.BackColor = Color.SeaGreen;
                    labelWarningsTitle.Text      = "  ✓ Конфликтов нет";
                    return;
                }

                int totalWarnings = dt.Rows.Count + overloadWarnings.Count;
                labelWarningsTitle.BackColor = Color.Crimson;
                labelWarningsTitle.Text = string.Format("  ⚠ Предупреждения ({0})", totalWarnings);
                listBoxWarnings.ForeColor = Color.DarkRed;

                // Конфликты расписания
                foreach (DataRow row in dt.Rows)
                {
                    string type   = row["Тип_конфликта"].ToString();
                    string day    = DayNames[Convert.ToInt32(row["ID_дня_недели"])];
                    string lesson = row["Номер_урока"].ToString();
                    string t1     = row["Учитель_1"].ToString().Trim();
                    string t2     = row["Учитель_2"].ToString().Trim();
                    string cls2   = row["Класс_2"].ToString();

                    string msg = type == "учитель"
                        ? string.Format("⚠ {0}, ур.{1} — {2} занят в {3}", day, lesson, t1, cls2)
                        : string.Format("⚠ {0}, ур.{1} — кабинет занят ({2}, {3})", day, lesson, t2, cls2);
                    listBoxWarnings.Items.Add(msg);
                }

                // Превышение ставки учителей
                foreach (string w in overloadWarnings)
                    listBoxWarnings.Items.Add(w);
            }
            catch (Exception ex)
            {
                listBoxWarnings.Items.Add("Ошибка: " + ex.Message);
            }
        }

        /// <summary>
        /// Проверяет, не превышает ли количество поставленных уроков в неделю
        /// ставку (Ставка) каждого учителя, задействованного в расписании класса.
        /// </summary>
        private List<string> CheckTeacherOverload()
        {
            var warnings = new List<string>();
            try
            {
                // Для каждого учителя, задействованного у данного класса,
                // считаем сколько уроков в неделю у него стоит ВСЕГО (по всем классам)
                // и сравниваем со ставкой
                DataTable dt = DbHelper.Query(
                    "SELECT t.ID_учителя, t.Фамилия || ' ' || t.Имя || ' ' || t.Отчество AS ФИО, " +
                    "       t.Ставка, " +
                    "       (SELECT COUNT(*) FROM Schedule s2 " +
                    "        JOIN Workload w2 ON s2.ID_нагрузки = w2.ID_нагрузки " +
                    "        WHERE w2.ID_учителя = t.ID_учителя) AS ПоставленоУроков " +
                    "FROM Teachers t " +
                    "WHERE t.ID_учителя IN ( " +
                    "    SELECT DISTINCT w.ID_учителя FROM Workload w " +
                    "    JOIN Schedule s ON s.ID_нагрузки = w.ID_нагрузки " +
                    "    JOIN Workload w2 ON s.ID_нагрузки = w2.ID_нагрузки " +
                    "    WHERE w2.ID_класса = @cid " +
                    ") " +
                    "AND t.Ставка IS NOT NULL",
                    p => p.AddWithValue("@cid", _selectedClassId));

                foreach (DataRow row in dt.Rows)
                {
                    decimal stavka  = Convert.ToDecimal(row["Ставка"]);
                    int posted      = Convert.ToInt32(row["ПоставленоУроков"]);
                    string name     = row["ФИО"].ToString().Trim();

                    if (posted > (int)stavka)
                        warnings.Add(string.Format(
                            "📋 {0}: поставлено {1} ур., ставка {2} ч/нед — превышение!",
                            name, posted, stavka));
                }
            }
            catch { /* не блокируем загрузку при ошибке */ }
            return warnings;
        }

        private void UpdateInfoPanel(DataRow row, int day, int lesson)
        {
            labelInfoDayVal.Text    = DayNames[day];
            labelInfoLessonVal.Text = lesson.ToString();
            if (row == null)
            {
                labelInfoSubjectVal.Text = "—";
                labelInfoTeacherVal.Text = "—";
                labelInfoRoomVal.Text    = "—";
                return;
            }
            labelInfoSubjectVal.Text = row["Предмет"]?.ToString()     ?? "—";
            labelInfoTeacherVal.Text = row["ФИО_учителя"]?.ToString() ?? "—";
            labelInfoRoomVal.Text    = row["Кабинет"]?.ToString()     ?? "—";
        }

        private static string BuildCellText(List<DataRow> entries, int weekView = 0)
        {
            if (entries == null || entries.Count == 0) return "+";
            var sb = new System.Text.StringBuilder();
            foreach (var row in entries)
            {
                if (sb.Length > 0) sb.Append("\n──\n");
                string week = row["Пометка_недели"]?.ToString() ?? "";
                string sub  = row["Пометка_подгруппы"]?.ToString() ?? "";
                // При фильтре по неделе пометку недели не показываем (она очевидна)
                string pfx  = weekView != 0
                    ? (sub.Length > 0 ? sub + " " : "")
                    : ((week + sub).Length > 0 ? (week + sub + " ") : "");
                sb.AppendFormat("{0}{1}\n{2}/{3}", pfx, row["Предмет"], row["ФИО_учителя"], row["Кабинет"]);
            }
            return sb.ToString();
        }

        private static Color GetCellWeekColor(List<DataRow> entries)
        {
            if (entries == null || entries.Count == 0) return Color.White;
            bool hasOdd  = false;
            bool hasEven = false;
            foreach (var row in entries)
            {
                int p = Convert.ToInt32(row["Чётность_недели"]);
                if (p == 0) return Color.White; // хотя бы один «каждую неделю» — белый
                if (p == 1) hasOdd  = true;
                if (p == 2) hasEven = true;
            }
            if (hasOdd && hasEven) return Color.White; // оба типа — белый (смешанный)
            if (hasOdd)  return ColorOddWeek;
            if (hasEven) return ColorEvenWeek;
            return Color.White;
        }

        private void dataGrid_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex <= 0) return;
            if ((string)dataGrid.Rows[e.RowIndex].Tag == "diffRow") return;

            var entries = dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Tag as List<DataRow>;
            if (entries == null || entries.Count <= 1) return; // обычная отрисовка

            e.Handled = true;

            // Фон ячейки
            e.Graphics.FillRectangle(SystemBrushes.Window,
                new System.Drawing.Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width - 1, e.CellBounds.Height - 1));

            bool isSelected = (e.State & DataGridViewElementStates.Selected) != 0;
            bool isConflict = e.CellStyle.BackColor == Color.MistyRose;
            int partH = e.CellBounds.Height / entries.Count;

            for (int i = 0; i < entries.Count; i++)
            {
                var row = entries[i];
                int y = e.CellBounds.Y + i * partH;
                int h = (i == entries.Count - 1)
                    ? (e.CellBounds.Y + e.CellBounds.Height - 1 - y)
                    : partH;
                var partRect = new System.Drawing.Rectangle(e.CellBounds.X + 1, y, e.CellBounds.Width - 2, h);

                // Цвет фона по чётности недели
                Color bg;
                if (isConflict)
                    bg = Color.MistyRose;
                else
                {
                    int parity = Convert.ToInt32(row["Чётность_недели"]);
                    bg = parity == 1 ? ColorOddWeek : parity == 2 ? ColorEvenWeek : Color.White;
                }
                using (var brush = new System.Drawing.SolidBrush(bg))
                    e.Graphics.FillRectangle(brush, partRect);

                // Разделитель между секциями
                if (i > 0)
                    using (var pen = new System.Drawing.Pen(Color.FromArgb(160, 160, 200)))
                        e.Graphics.DrawLine(pen, e.CellBounds.X + 2, y, e.CellBounds.Right - 3, y);

                // Текст
                string week = _weekView == 0 ? (row["Пометка_недели"]?.ToString() ?? "") : "";
                string sub  = row["Пометка_подгруппы"]?.ToString() ?? "";
                string pfx  = (week + sub).Trim();
                string text = (pfx.Length > 0 ? pfx + " " : "")
                    + row["Предмет"] + "\n"
                    + row["ФИО_учителя"] + "/" + row["Кабинет"];

                Color fg = isConflict ? Color.DarkRed : Color.Black;
                var textRect = new System.Drawing.Rectangle(
                    partRect.X + 3, partRect.Y + 2, partRect.Width - 6, partRect.Height - 4);
                TextRenderer.DrawText(e.Graphics, text,
                    e.CellStyle.Font ?? dataGrid.DefaultCellStyle.Font,
                    textRect, fg,
                    TextFormatFlags.WordBreak | TextFormatFlags.Left | TextFormatFlags.Top);
            }

            // Выделение (полупрозрачный оверлей)
            if (isSelected)
                using (var selBrush = new System.Drawing.SolidBrush(Color.FromArgb(60, 0, 120, 215)))
                    e.Graphics.FillRectangle(selBrush,
                        new System.Drawing.Rectangle(e.CellBounds.X + 1, e.CellBounds.Y,
                            e.CellBounds.Width - 2, e.CellBounds.Height - 1));

            // Граница ячейки
            using (var gridPen = new System.Drawing.Pen(dataGrid.GridColor))
                e.Graphics.DrawRectangle(gridPen,
                    new System.Drawing.Rectangle(e.CellBounds.X, e.CellBounds.Y,
                        e.CellBounds.Width - 1, e.CellBounds.Height - 1));
        }

        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex <= 0 || _selectedClassId < 0) return;
            if ((string)dataGrid.Rows[e.RowIndex].Tag == "diffRow") return;

            int day    = e.ColumnIndex;
            int lesson = e.RowIndex + 1;
            var cell   = dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
            var entries = cell.Tag as List<DataRow>;

            DataRow infoRow = entries?.Count > 0 ? entries[0] : null;
            UpdateInfoPanel(infoRow, day, lesson);

            if (entries == null || entries.Count == 0)
            {
                using (var dlg = new CellEditForm(_selectedClassId, day, lesson, null))
                    if (dlg.ShowDialog(this) == DialogResult.OK) FillGridFromDb();
                return;
            }

            if (entries.Count == 1)
            {
                DataRow existing  = entries[0];
                bool isConflict   = cell.Style.BackColor == Color.MistyRose;
                if (isConflict)
                {
                    string desc = GetConflictDescription(existing);
                    using (var dlg = new ConflictDialogForm(desc))
                    {
                        dlg.ShowDialog(this);
                        if (dlg.Choice == ConflictDialogForm.ConflictChoice.Cancel) return;
                        if (dlg.Choice == ConflictDialogForm.ConflictChoice.EditOther)
                        {
                            var other = FindConflictingRow(existing);
                            if (other != null)
                            {
                                int od = Convert.ToInt32(other["ID_дня_недели"]);
                                int ol = Convert.ToInt32(other["Номер_урока"]);
                                using (var ed = new CellEditForm(_selectedClassId, od, ol, other))
                                    if (ed.ShowDialog(this) == DialogResult.OK) FillGridFromDb();
                            }
                            return;
                        }
                    }
                }
                using (var dlg = new CellEditForm(_selectedClassId, day, lesson, existing))
                    if (dlg.ShowDialog(this) == DialogResult.OK) FillGridFromDb();
                return;
            }

            // Несколько уроков в слоте (подгруппы / двухнедельные) — контекстное меню
            ShowMultiEntryMenu(cell, entries, day, lesson);
        }

        private void ShowMultiEntryMenu(DataGridViewCell cell, List<DataRow> entries, int day, int lesson)
        {
            var menu = new ContextMenuStrip();
            foreach (var entry in entries)
            {
                string week = entry["Пометка_недели"]?.ToString() ?? "";
                string sub  = entry["Пометка_подгруппы"]?.ToString() ?? "";
                string pfx  = (week + sub).Length > 0 ? (week + sub + " ") : "";
                string label = pfx + entry["Предмет"] + " — " + entry["ФИО_учителя"];
                var item = new ToolStripMenuItem("Изменить: " + label);
                DataRow captured = entry;
                item.Click += (s, ev) => {
                    using (var dlg = new CellEditForm(_selectedClassId, day, lesson, captured))
                        if (dlg.ShowDialog(this) == DialogResult.OK) FillGridFromDb();
                };
                menu.Items.Add(item);
            }
            menu.Items.Add(new ToolStripSeparator());
            var addItem = new ToolStripMenuItem("+ Добавить урок в этот слот");
            addItem.Click += (s, ev) => {
                using (var dlg = new CellEditForm(_selectedClassId, day, lesson, null))
                    if (dlg.ShowDialog(this) == DialogResult.OK) FillGridFromDb();
            };
            menu.Items.Add(addItem);
            menu.Show(dataGrid, dataGrid.PointToClient(Cursor.Position));
        }

        private string GetConflictDescription(DataRow row)
        {
            var parts = new List<string>();
            try
            {
                int scheduleId = Convert.ToInt32(row["ID_расписания"]);
                int teacherId  = Convert.ToInt32(row["ID_учителя"]);
                int cabinetId  = Convert.ToInt32(row["ID_кабинета"]);
                int day        = Convert.ToInt32(row["ID_дня_недели"]);
                int lesson     = Convert.ToInt32(row["Номер_урока"]);

                DataTable tc = DbHelper.Query(
                    "SELECT Предмет, ФИО_учителя, Класс FROM vw_Schedule " +
                    "WHERE ID_дня_недели = @d AND Номер_урока = @l " +
                    "  AND ID_учителя = @tid AND ID_расписания <> @sid",
                    p => { p.AddWithValue("@tid", teacherId); p.AddWithValue("@d", day);
                           p.AddWithValue("@l", lesson);      p.AddWithValue("@sid", scheduleId); });
                if (tc.Rows.Count > 0)
                    parts.Add(string.Format("Учитель «{0}» ведёт «{1}» у класса {2}.",
                        tc.Rows[0]["ФИО_учителя"], tc.Rows[0]["Предмет"], tc.Rows[0]["Класс"]));

                DataTable rc = DbHelper.Query(
                    "SELECT Предмет, ФИО_учителя, Класс, Кабинет FROM vw_Schedule " +
                    "WHERE ID_дня_недели = @d AND Номер_урока = @l " +
                    "  AND ID_кабинета = @cr AND ID_расписания <> @sid",
                    p => { p.AddWithValue("@d", day);        p.AddWithValue("@l", lesson);
                           p.AddWithValue("@cr", cabinetId); p.AddWithValue("@sid", scheduleId); });
                if (rc.Rows.Count > 0)
                    parts.Add(string.Format("Кабинет «{0}» занят — там идёт «{1}» ({2}) у класса {3}.",
                        rc.Rows[0]["Кабинет"], rc.Rows[0]["Предмет"],
                        rc.Rows[0]["ФИО_учителя"], rc.Rows[0]["Класс"]));
            }
            catch (Exception ex) { return "Ошибка: " + ex.Message; }
            return parts.Count > 0 ? string.Join("\n", parts) : "Конфликт расписания.";
        }

        private DataRow FindConflictingRow(DataRow row)
        {
            try
            {
                int sid = Convert.ToInt32(row["ID_расписания"]);
                int tid = Convert.ToInt32(row["ID_учителя"]);
                int cid = Convert.ToInt32(row["ID_кабинета"]);
                int d   = Convert.ToInt32(row["ID_дня_недели"]);
                int l   = Convert.ToInt32(row["Номер_урока"]);

                DataTable dt = DbHelper.Query(
                    "SELECT * FROM vw_Schedule " +
                    "WHERE ID_дня_недели = @d AND Номер_урока = @l " +
                    "  AND ID_расписания <> @sid " +
                    "  AND (ID_учителя = @tid OR ID_кабинета = @cr)",
                    p => { p.AddWithValue("@tid", tid); p.AddWithValue("@d", d);
                           p.AddWithValue("@l", l);     p.AddWithValue("@sid", sid);
                           p.AddWithValue("@cr", cid); });
                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
            catch { return null; }
        }
    }
}
