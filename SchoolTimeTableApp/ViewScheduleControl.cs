using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace testing
{
    public partial class ViewScheduleControl : UserControl
    {
        private static readonly string[] Days = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
        private const int MAX_LESSONS = 8;

        public ViewScheduleControl()
        {
            InitializeComponent();
        }

        public void LoadSchedule()
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            LoadFilters();
            RebuildGrid();
        }

        private void LoadFilters()
        {
            // Classes
            string selectedClass   = (comboClass.SelectedItem   as DataRowView)?["class_id"]?.ToString();
            string selectedTeacher = (comboTeacher.SelectedItem as DataRowView)?["teacher_id"]?.ToString();

            comboClass.DataSource    = null;
            comboTeacher.DataSource  = null;

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

                comboClass.DataSource    = dtClass;
                comboClass.DisplayMember = "class_name";
                comboClass.ValueMember   = "class_id";

                DataTable dtTeacher = DbHelper.Query("SELECT teacher_id, name FROM Teachers ORDER BY name");
                DataRow allTeachers = dtTeacher.NewRow();
                allTeachers["teacher_id"] = DBNull.Value;
                allTeachers["name"]       = "Все учителя";
                dtTeacher.Rows.InsertAt(allTeachers, 0);

                comboTeacher.DataSource    = dtTeacher;
                comboTeacher.DisplayMember = "name";
                comboTeacher.ValueMember   = "teacher_id";
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка фильтров"); }
        }

        private void RebuildGrid()
        {
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();

            // Determine day filter
            int dayFilter = comboDayFilter.SelectedIndex; // 0=all, 1-5=specific day

            // Build columns: Урок | Day1 | Day2 ...
            dataGrid.Columns.Add("lesson", "Урок");
            dataGrid.Columns["lesson"].Width = 60;
            dataGrid.Columns["lesson"].ReadOnly = true;

            for (int d = 1; d <= 5; d++)
            {
                if (dayFilter > 0 && dayFilter != d) continue;
                DataGridViewColumn col = new DataGridViewTextBoxColumn();
                col.Name     = "day" + d;
                col.HeaderText = Days[d - 1];
                col.Width    = 200;
                col.ReadOnly = true;
                dataGrid.Columns.Add(col);
            }

            // Add rows
            for (int lesson = 1; lesson <= MAX_LESSONS; lesson++)
                dataGrid.Rows.Add(lesson.ToString());

            // Load data
            try
            {
                string classFilter   = comboClass.SelectedValue?.ToString();
                string teacherFilter = comboTeacher.SelectedValue?.ToString();

                string sql =
                    "SELECT s.day_of_week, s.lesson_number, " +
                    "sub.subject_name, t.name AS teacher_name, cr.room_number, " +
                    "w.teacher_id, w.class_id " +
                    "FROM Schedule s " +
                    "JOIN Workload w ON s.workload_id = w.workload_id " +
                    "JOIN Subjects sub ON w.subject_id = sub.subject_id " +
                    "JOIN Teachers t ON w.teacher_id = t.teacher_id " +
                    "JOIN Classrooms cr ON s.classroom_id = cr.classroom_id " +
                    "JOIN Classes cl ON w.class_id = cl.class_id " +
                    "WHERE 1=1";

                if (!string.IsNullOrEmpty(classFilter) && classFilter != "")
                    sql += " AND w.class_id = " + classFilter;
                if (!string.IsNullOrEmpty(teacherFilter) && teacherFilter != "")
                    sql += " AND w.teacher_id = " + teacherFilter;
                if (dayFilter > 0)
                    sql += " AND s.day_of_week = " + dayFilter;

                DataTable dt = DbHelper.Query(sql);

                // Check for conflicts: same teacher or classroom at same time
                DataTable conflicts = DbHelper.Query(
                    "SELECT s1.day_of_week, s1.lesson_number FROM Schedule s1 " +
                    "JOIN Workload w1 ON s1.workload_id = w1.workload_id " +
                    "JOIN Schedule s2 ON s1.day_of_week = s2.day_of_week AND s1.lesson_number = s2.lesson_number AND s1.schedule_id <> s2.schedule_id " +
                    "JOIN Workload w2 ON s2.workload_id = w2.workload_id " +
                    "WHERE w1.teacher_id = w2.teacher_id OR s1.classroom_id = s2.classroom_id");

                foreach (DataRow row in dt.Rows)
                {
                    int day    = Convert.ToInt32(row["day_of_week"]);
                    int lesson = Convert.ToInt32(row["lesson_number"]);
                    string colName = "day" + day;

                    if (!dataGrid.Columns.Contains(colName)) continue;

                    int rowIdx = lesson - 1;
                    string cellText = string.Format("{0}\n{1} / {2}",
                        row["subject_name"], row["teacher_name"], row["room_number"]);

                    dataGrid.Rows[rowIdx].Cells[colName].Value = cellText;

                    // Highlight conflicts
                    bool isConflict = false;
                    foreach (DataRow cr in conflicts.Rows)
                        if (Convert.ToInt32(cr["day_of_week"]) == day &&
                            Convert.ToInt32(cr["lesson_number"]) == lesson)
                        { isConflict = true; break; }

                    if (isConflict)
                    {
                        dataGrid.Rows[rowIdx].Cells[colName].Style.BackColor = Color.MistyRose;
                        dataGrid.Rows[rowIdx].Cells[colName].Style.ForeColor = Color.DarkRed;
                    }
                }
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка расписания"); }
        }

        private void buttonApplyFilter_Click(object sender, EventArgs e)
        {
            RebuildGrid();
        }

        private void buttonResetFilter_Click(object sender, EventArgs e)
        {
            comboClass.SelectedIndex   = 0;
            comboTeacher.SelectedIndex = 0;
            comboDayFilter.SelectedIndex = 0;
            RebuildGrid();
        }
    }
}
