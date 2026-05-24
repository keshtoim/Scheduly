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
                    "SELECT ID_класса AS class_id, " +
                    "CONVERT(NVARCHAR, pc.Параллель) + lc.Буква AS class_name " +
                    "FROM Classes cl " +
                    "JOIN ParallelClass pc ON cl.ID_параллели_класса = pc.ID_параллели_класса " +
                    "JOIN LetterClass lc   ON cl.ID_буквы_класса     = lc.ID_буквы_класса " +
                    "ORDER BY pc.Параллель, lc.Буква");
                DataRow allC = dtClass.NewRow();
                allC["class_id"] = DBNull.Value; allC["class_name"] = "Все классы";
                dtClass.Rows.InsertAt(allC, 0);
                comboClass.DisplayMember = "class_name";
                comboClass.ValueMember   = "class_id";
                comboClass.DataSource    = dtClass;

                DataTable dtTeacher = DbHelper.Query(
                    "SELECT ID_учителя AS teacher_id, " +
                    "Фамилия + ' ' + Имя + ' ' + Отчество AS full_name " +
                    "FROM Teachers ORDER BY Фамилия, Имя");
                DataRow allT = dtTeacher.NewRow();
                allT["teacher_id"] = DBNull.Value; allT["full_name"] = "Все учителя";
                dtTeacher.Rows.InsertAt(allT, 0);
                comboTeacher.DisplayMember = "full_name";
                comboTeacher.ValueMember   = "teacher_id";
                comboTeacher.DataSource    = dtTeacher;

                // Фильтр по ступени обучения
                comboLevel.Items.Clear();
                comboLevel.Items.Add("Все ступени");
                comboLevel.Items.Add("Начальная школа (1–4)");
                comboLevel.Items.Add("Основная школа (5–9)");
                comboLevel.Items.Add("Средняя школа (10–11)");
                comboLevel.SelectedIndex = 0;
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка фильтров"); }
        }

        /// <summary>
        /// Возвращает SQL-условие для фильтра по ступени обучения.
        /// </summary>
        private string GetLevelFilter()
        {
            switch (comboLevel.SelectedIndex)
            {
                case 1: return " AND pc.Параллель BETWEEN 1 AND 4";
                case 2: return " AND pc.Параллель BETWEEN 5 AND 9";
                case 3: return " AND pc.Параллель BETWEEN 10 AND 11";
                default: return "";
            }
        }

        private void RebuildGrid()
        {
            string classFilter   = comboClass.SelectedValue?.ToString();
            string teacherFilter = comboTeacher.SelectedValue?.ToString();
            int    dayFilter     = comboDayFilter.SelectedIndex;
            bool   filterByClass = !string.IsNullOrEmpty(classFilter) && classFilter != "";

            if (filterByClass)
                BuildSingleClassView(classFilter, teacherFilter, dayFilter);
            else
                BuildAllClassesView(teacherFilter, dayFilter);
        }

        private void BuildSingleClassView(string classId, string teacherFilter, int dayFilter)
        {
            dataGrid.Rows.Clear(); dataGrid.Columns.Clear();

            var lc = new DataGridViewTextBoxColumn
                { Name = "lesson", HeaderText = "Урок", Width = 60, ReadOnly = true };
            lc.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            lc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            lc.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            dataGrid.Columns.Add(lc);

            for (int d = 1; d <= 5; d++)
            {
                if (dayFilter > 0 && dayFilter != d) continue;
                var col = new DataGridViewTextBoxColumn
                    { Name = "day" + d, HeaderText = DayNames[d], Width = 220, ReadOnly = true };
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGrid.Columns.Add(col);
            }

            for (int l = 1; l <= MAX_LESSONS; l++) dataGrid.Rows.Add(l.ToString());
            StyleRows();

            string sql = "SELECT * FROM vw_Schedule WHERE ID_класса = " + classId;
            if (!string.IsNullOrEmpty(teacherFilter) && teacherFilter != "")
                sql += " AND ID_учителя = " + teacherFilter;
            if (dayFilter > 0) sql += " AND ID_дня_недели = " + dayFilter;

            try
            {
                DataTable dt = DbHelper.Query(sql);
                DataTable conflicts = GetAllConflicts();

                foreach (DataRow row in dt.Rows)
                {
                    int day    = Convert.ToInt32(row["ID_дня_недели"]);
                    int lesson = Convert.ToInt32(row["Номер_урока"]);
                    string cn  = "day" + day;
                    if (!dataGrid.Columns.Contains(cn)) continue;
                    var cell = dataGrid.Rows[lesson - 1].Cells[cn];
                    cell.Value = string.Format("{0}\n{1}\nКаб. {2}",
                        row["Предмет"], row["ФИО_учителя"], row["Кабинет"]);
                    if (IsConflict(conflicts, day, lesson))
                    { cell.Style.BackColor = Color.MistyRose; cell.Style.ForeColor = Color.DarkRed; }
                }
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка данных"); }
        }

        private void BuildAllClassesView(string teacherFilter, int dayFilter)
        {
            dataGrid.Rows.Clear(); dataGrid.Columns.Clear();

            var cc = new DataGridViewTextBoxColumn
                { Name = "class_name", HeaderText = "Класс", Width = 70, ReadOnly = true };
            cc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            cc.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            cc.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dataGrid.Columns.Add(cc);

            var lc = new DataGridViewTextBoxColumn
                { Name = "lesson", HeaderText = "Ур.", Width = 40, ReadOnly = true };
            lc.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            lc.DefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240);
            dataGrid.Columns.Add(lc);

            for (int d = 1; d <= 5; d++)
            {
                if (dayFilter > 0 && dayFilter != d) continue;
                var col = new DataGridViewTextBoxColumn
                    { Name = "day" + d, HeaderText = DayNames[d], Width = 200, ReadOnly = true };
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGrid.Columns.Add(col);
            }

            try
            {
                DataTable classes = DbHelper.Query(
                    "SELECT cl.ID_класса AS class_id, " +
                    "CONVERT(NVARCHAR, pc.Параллель) + lc.Буква AS class_name " +
                    "FROM Classes cl " +
                    "JOIN ParallelClass pc ON cl.ID_параллели_класса = pc.ID_параллели_класса " +
                    "JOIN LetterClass lc   ON cl.ID_буквы_класса     = lc.ID_буквы_класса " +
                    "WHERE 1=1" + GetLevelFilter() +
                    " ORDER BY pc.Параллель, lc.Буква");

                DataTable conflicts = GetAllConflicts();

                foreach (DataRow cls in classes.Rows)
                {
                    int    cid   = Convert.ToInt32(cls["class_id"]);
                    string cname = cls["class_name"].ToString().Trim();
                    int    start = dataGrid.Rows.Count;
                    bool   odd   = (classes.Rows.IndexOf(cls) % 2 == 0);
                    Color  baseBg = odd ? Color.White : Color.FromArgb(248, 252, 255);

                    for (int l = 1; l <= MAX_LESSONS; l++)
                    {
                        int ri = dataGrid.Rows.Add();
                        dataGrid.Rows[ri].Cells["class_name"].Value = (l == 1) ? cname : "";
                        dataGrid.Rows[ri].Cells["lesson"].Value     = l.ToString();
                        dataGrid.Rows[ri].Height = 46;
                        for (int d = 1; d <= 5; d++)
                            if (dataGrid.Columns.Contains("day" + d))
                                dataGrid.Rows[ri].Cells["day" + d].Style.BackColor = baseBg;
                    }

                    string sql = "SELECT * FROM vw_Schedule WHERE ID_класса = " + cid;
                    if (!string.IsNullOrEmpty(teacherFilter) && teacherFilter != "")
                        sql += " AND ID_учителя = " + teacherFilter;
                    if (dayFilter > 0) sql += " AND ID_дня_недели = " + dayFilter;

                    DataTable dt = DbHelper.Query(sql);
                    foreach (DataRow row in dt.Rows)
                    {
                        int day    = Convert.ToInt32(row["ID_дня_недели"]);
                        int lesson = Convert.ToInt32(row["Номер_урока"]);
                        string cn  = "day" + day;
                        if (!dataGrid.Columns.Contains(cn)) continue;
                        int ri = start + lesson - 1;
                        var cell = dataGrid.Rows[ri].Cells[cn];
                        cell.Value = string.Format("{0}\n{1} / {2}",
                            row["Предмет"], row["ФИО_учителя"], row["Кабинет"]);
                        if (IsConflict(conflicts, day, lesson))
                        { cell.Style.BackColor = Color.MistyRose; cell.Style.ForeColor = Color.DarkRed; }
                    }

                    for (int i = start; i < start + MAX_LESSONS; i++)
                    {
                        dataGrid.Rows[i].Cells["class_name"].Style.BackColor = Color.FromArgb(230, 240, 255);
                        dataGrid.Rows[i].Cells["class_name"].Style.Font =
                            new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
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
                dataGrid.Rows[i].DefaultCellStyle.BackColor =
                    i % 2 == 0 ? Color.White : Color.FromArgb(248, 252, 255);
            }
        }

        private DataTable GetAllConflicts()
        {
            return DbHelper.Query(
                "SELECT DISTINCT ID_дня_недели, Номер_урока FROM vw_Conflicts");
        }

        private bool IsConflict(DataTable conflicts, int day, int lesson)
        {
            foreach (DataRow cr in conflicts.Rows)
                if (Convert.ToInt32(cr["ID_дня_недели"]) == day &&
                    Convert.ToInt32(cr["Номер_урока"])   == lesson) return true;
            return false;
        }

        private void buttonApplyFilter_Click(object sender, EventArgs e) { RebuildGrid(); }

        private void buttonResetFilter_Click(object sender, EventArgs e)
        {
            comboClass.SelectedIndex     = 0;
            comboTeacher.SelectedIndex   = 0;
            comboDayFilter.SelectedIndex = 0;
            comboLevel.SelectedIndex     = 0;
            textSearch.Clear();
            RebuildGrid();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            var dlg = new SaveFileDialog
                { Filter = "Excel (*.xlsx)|*.xlsx", FileName = "Расписание.xlsx" };
            if (dlg.ShowDialog() != DialogResult.OK) return;
            try
            {
                ScheduleExporter.Export(dlg.FileName);
                MessageBox.Show("Файл сохранён:\n" + dlg.FileName,
                    "Экспорт", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Ошибка экспорта"); }
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(textSearch.Text)) ApplySearch("");
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            ApplySearch(textSearch.Text.Trim());
        }

        private void textSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) ApplySearch(textSearch.Text.Trim());
        }

        private void ApplySearch(string query)
        {
            bool hasQuery = !string.IsNullOrEmpty(query);

            foreach (DataGridViewRow row in dataGrid.Rows)
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Style.ForeColor == Color.DarkRed) continue;
                    cell.Style.BackColor = Color.Empty;
                    cell.Style.ForeColor = Color.Empty;
                }

            if (!hasQuery) { labelSearchHint.Text = ""; return; }

            int count = 0;
            foreach (DataGridViewRow row in dataGrid.Rows)
                foreach (DataGridViewCell cell in row.Cells)
                {
                    string cn = dataGrid.Columns[cell.ColumnIndex].Name;
                    if (cn == "class_name" || cn == "lesson") continue;
                    string val = cell.Value?.ToString() ?? "";
                    if (string.IsNullOrWhiteSpace(val) || val == "+") continue;
                    bool isConflict = cell.Style.ForeColor == Color.DarkRed;
                    if (val.IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        cell.Style.BackColor = Color.Gold;
                        if (!isConflict) cell.Style.ForeColor = Color.Black;
                        count++;
                    }
                    else { if (!isConflict) cell.Style.ForeColor = Color.LightGray; }
                }

            labelSearchHint.Text      = count > 0 ? string.Format("Найдено: {0}", count) : "Не найдено";
            labelSearchHint.ForeColor = count > 0 ? Color.SeaGreen : Color.Crimson;
        }
    }
}
