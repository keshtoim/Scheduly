using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace testing
{
    public partial class ComposeScheduleControl : UserControl
    {
        private static readonly string[] DayNames = { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
        private const int MAX_LESSONS = 8;

        private int _selectedClassId = -1;

        public ComposeScheduleControl()
        {
            InitializeComponent();
        }

        public void LoadData()
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            LoadClasses();
        }

        private void LoadClasses()
        {
            try
            {
                DataTable dt = DbHelper.Query(
                    "SELECT cl.class_id, CAST(cp.parallel AS NVARCHAR) + lc.letterClass AS class_name " +
                    "FROM Classes cl " +
                    "JOIN ClassParallel cp ON cl.id_parallel_class = cp.id_parallel_class " +
                    "JOIN LetterOfTheClass lc ON cl.id_letter_class = lc.id_letter_class " +
                    "ORDER BY cp.parallel, lc.letterClass");

                listBoxClasses.DataSource    = dt;
                listBoxClasses.DisplayMember = "class_name";
                listBoxClasses.ValueMember   = "class_id";
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка классов"); }
        }

        private void listBoxClasses_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxClasses.SelectedItem == null) return;

            // ListBox with DataTable source returns DataRowView, not the raw value
            DataRowView drv = listBoxClasses.SelectedItem as DataRowView;
            if (drv == null) return;

            _selectedClassId = Convert.ToInt32(drv["class_id"]);
            BuildScheduleGrid();
        }

        private void BuildScheduleGrid()
        {
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();

            // Column: lesson number
            dataGrid.Columns.Add("lesson", "Урок");
            dataGrid.Columns["lesson"].Width    = 50;
            dataGrid.Columns["lesson"].ReadOnly = true;
            dataGrid.Columns["lesson"].DefaultCellStyle.BackColor = Color.WhiteSmoke;

            // Columns: Mon–Fri
            for (int d = 1; d <= 5; d++)
            {
                DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                col.Name       = "day" + d;
                col.HeaderText = DayNames[d - 1];
                col.Width      = 200;
                col.FlatStyle  = FlatStyle.Flat;
                col.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dataGrid.Columns.Add(col);
            }

            for (int l = 1; l <= MAX_LESSONS; l++)
                dataGrid.Rows.Add(l.ToString());

            FillGridFromDb();
        }

        private void FillGridFromDb()
        {
            if (_selectedClassId < 0) return;

            // Reset cells
            for (int l = 0; l < MAX_LESSONS; l++)
                for (int d = 1; d <= 5; d++)
                {
                    var cell = dataGrid.Rows[l].Cells["day" + d];
                    cell.Value = "+";
                    cell.Style.BackColor = Color.White;
                    cell.Style.ForeColor = Color.Gray;
                    cell.Tag = null;
                }

            try
            {
                DataTable dt = DbHelper.Query(
                    "SELECT s.schedule_id, s.day_of_week, s.lesson_number, s.workload_id, s.classroom_id, " +
                    "sub.subject_name, t.name AS teacher_name, cr.room_number " +
                    "FROM Schedule s " +
                    "JOIN Workload w ON s.workload_id = w.workload_id " +
                    "JOIN Subjects sub ON w.subject_id = sub.subject_id " +
                    "JOIN Teachers t ON w.teacher_id = t.teacher_id " +
                    "JOIN Classrooms cr ON s.classroom_id = cr.classroom_id " +
                    "WHERE w.class_id = @cid",
                    p => p.AddWithValue("@cid", _selectedClassId));

                foreach (DataRow row in dt.Rows)
                {
                    int day    = Convert.ToInt32(row["day_of_week"]);
                    int lesson = Convert.ToInt32(row["lesson_number"]);
                    var cell   = dataGrid.Rows[lesson - 1].Cells["day" + day];

                    cell.Value = string.Format("{0}\n{1} / {2}",
                        row["subject_name"], row["teacher_name"], row["room_number"]);
                    cell.Style.ForeColor = Color.Black;
                    cell.Tag = row; // store full row for editing
                }

                HighlightConflicts();
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка расписания класса"); }
        }

        private void HighlightConflicts()
        {
            try
            {
                DataTable conflicts = DbHelper.Query(
                    "SELECT s1.day_of_week, s1.lesson_number FROM Schedule s1 " +
                    "JOIN Workload w1 ON s1.workload_id = w1.workload_id " +
                    "JOIN Schedule s2 ON s1.day_of_week = s2.day_of_week " +
                    "  AND s1.lesson_number = s2.lesson_number " +
                    "  AND s1.schedule_id <> s2.schedule_id " +
                    "JOIN Workload w2 ON s2.workload_id = w2.workload_id " +
                    "WHERE w1.class_id = @cid AND (w1.teacher_id = w2.teacher_id OR s1.classroom_id = s2.classroom_id)",
                    p => p.AddWithValue("@cid", _selectedClassId));

                foreach (DataRow cr in conflicts.Rows)
                {
                    int d = Convert.ToInt32(cr["day_of_week"]);
                    int l = Convert.ToInt32(cr["lesson_number"]);
                    var cell = dataGrid.Rows[l - 1].Cells["day" + d];
                    cell.Style.BackColor = Color.MistyRose;
                    cell.Style.ForeColor = Color.DarkRed;
                }
            }
            catch { /* non-critical */ }
        }

        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex <= 0) return;
            if (_selectedClassId < 0) return;

            int day    = e.ColumnIndex; // 1-based (col 0 = lesson)
            int lesson = e.RowIndex + 1;
            var cell   = dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
            DataRow existing = cell.Tag as DataRow;

            // If cell is highlighted as conflict — show conflict dialog first
            bool isConflict = cell.Style.BackColor == System.Drawing.Color.MistyRose;
            if (isConflict && existing != null)
            {
                string conflictDesc = GetConflictDescription(existing);

                using (ConflictDialogForm dlg = new ConflictDialogForm(conflictDesc))
                {
                    dlg.ShowDialog(this);

                    if (dlg.Choice == ConflictDialogForm.ConflictChoice.Cancel)
                        return;

                    if (dlg.Choice == ConflictDialogForm.ConflictChoice.EditOther)
                    {
                        DataRow other = FindConflictingRow(existing);
                        if (other != null)
                        {
                            int otherDay    = Convert.ToInt32(other["day_of_week"]);
                            int otherLesson = Convert.ToInt32(other["lesson_number"]);
                            using (CellEditForm editDlg = new CellEditForm(_selectedClassId, otherDay, otherLesson, other))
                                if (editDlg.ShowDialog(this) == DialogResult.OK) FillGridFromDb();
                        }
                        else
                        {
                            MessageBox.Show("Конфликтующая запись не найдена в текущем классе.",
                                "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        return;
                    }
                    // EditThis — fall through to open current cell
                }
            }

            using (CellEditForm dlg = new CellEditForm(_selectedClassId, day, lesson, existing))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    FillGridFromDb();
            }
        }

        private string GetConflictDescription(DataRow row)
        {
            try
            {
                int workloadId  = Convert.ToInt32(row["workload_id"]);
                int classroomId = Convert.ToInt32(row["classroom_id"]);
                int day         = Convert.ToInt32(row["day_of_week"]);
                int lesson      = Convert.ToInt32(row["lesson_number"]);
                int scheduleId  = Convert.ToInt32(row["schedule_id"]);

                var parts = new System.Collections.Generic.List<string>();

                // Teacher conflict — get details of the other lesson
                DataTable teacherConflict = DbHelper.Query(
                    "SELECT sub.subject_name, " +
                    "CAST(cp.parallel AS NVARCHAR) + lc.letterClass AS class_name " +
                    "FROM Schedule s " +
                    "JOIN Workload w  ON s.workload_id  = w.workload_id " +
                    "JOIN Workload w2 ON w2.workload_id = @wid " +
                    "JOIN Subjects sub ON w.subject_id  = sub.subject_id " +
                    "JOIN Classes cl   ON w.class_id    = cl.class_id " +
                    "JOIN ClassParallel cp   ON cl.id_parallel_class = cp.id_parallel_class " +
                    "JOIN LetterOfTheClass lc ON cl.id_letter_class  = lc.id_letter_class " +
                    "WHERE s.day_of_week = @d AND s.lesson_number = @l " +
                    "AND w.teacher_id = w2.teacher_id AND s.schedule_id <> @sid",
                    p => { p.AddWithValue("@wid", workloadId);
                           p.AddWithValue("@d",   day);
                           p.AddWithValue("@l",   lesson);
                           p.AddWithValue("@sid", scheduleId); });

                if (teacherConflict.Rows.Count > 0)
                {
                    DataRow tc = teacherConflict.Rows[0];
                    parts.Add(string.Format(
                        "Учитель «{0}» ведёт урок «{1}» у класса {2} в это время.",
                        row["teacher_name"], tc["subject_name"], tc["class_name"]));
                }

                // Classroom conflict — get details of the other lesson
                DataTable roomConflict = DbHelper.Query(
                    "SELECT sub.subject_name, t.name AS teacher_name, " +
                    "CAST(cp.parallel AS NVARCHAR) + lc.letterClass AS class_name " +
                    "FROM Schedule s " +
                    "JOIN Workload w ON s.workload_id = w.workload_id " +
                    "JOIN Subjects sub ON w.subject_id = sub.subject_id " +
                    "JOIN Teachers t   ON w.teacher_id  = t.teacher_id " +
                    "JOIN Classes cl   ON w.class_id    = cl.class_id " +
                    "JOIN ClassParallel cp   ON cl.id_parallel_class = cp.id_parallel_class " +
                    "JOIN LetterOfTheClass lc ON cl.id_letter_class  = lc.id_letter_class " +
                    "WHERE s.day_of_week = @d AND s.lesson_number = @l " +
                    "AND s.classroom_id = @cr AND s.schedule_id <> @sid",
                    p => { p.AddWithValue("@d",   day);
                           p.AddWithValue("@l",   lesson);
                           p.AddWithValue("@cr",  classroomId);
                           p.AddWithValue("@sid", scheduleId); });

                if (roomConflict.Rows.Count > 0)
                {
                    DataRow rc = roomConflict.Rows[0];
                    parts.Add(string.Format(
                        "Кабинет «{0}» занят — там идёт «{1}» ({2}) у класса {3}.",
                        row["room_number"], rc["subject_name"], rc["teacher_name"], rc["class_name"]));
                }

                return parts.Count > 0
                    ? string.Join("\n", parts)
                    : "Конфликт расписания.";
            }
            catch { return "Конфликт расписания."; }
        }

        private DataRow FindConflictingRow(DataRow row)
        {
            try
            {
                int workloadId  = Convert.ToInt32(row["workload_id"]);
                int classroomId = Convert.ToInt32(row["classroom_id"]);
                int day         = Convert.ToInt32(row["day_of_week"]);
                int lesson      = Convert.ToInt32(row["lesson_number"]);
                int scheduleId  = Convert.ToInt32(row["schedule_id"]);

                // Find another schedule entry that conflicts (same teacher or same room, same slot)
                DataTable dt = DbHelper.Query(
                    "SELECT s.schedule_id, s.day_of_week, s.lesson_number, s.workload_id, s.classroom_id, " +
                    "sub.subject_name, t.name AS teacher_name, cr.room_number " +
                    "FROM Schedule s " +
                    "JOIN Workload w ON s.workload_id = w.workload_id " +
                    "JOIN Workload w2 ON w2.workload_id = @wid " +
                    "JOIN Subjects sub ON w.subject_id = sub.subject_id " +
                    "JOIN Teachers t ON w.teacher_id = t.teacher_id " +
                    "JOIN Classrooms cr ON s.classroom_id = cr.classroom_id " +
                    "WHERE s.day_of_week = @d AND s.lesson_number = @l " +
                    "AND s.schedule_id <> @sid " +
                    "AND (w.teacher_id = w2.teacher_id OR s.classroom_id = @cr)",
                    p => { p.AddWithValue("@wid", workloadId); p.AddWithValue("@d", day);
                           p.AddWithValue("@l", lesson);       p.AddWithValue("@sid", scheduleId);
                           p.AddWithValue("@cr", classroomId); });

                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
            catch { return null; }
        }
    }
}
