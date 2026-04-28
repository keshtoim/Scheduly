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

        public ComposeScheduleControl() { InitializeComponent(); }

        public void LoadData()
        {
            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadClasses();
        }

        // ── Classes ──────────────────────────────────────────────────────────

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
            DataRowView drv = listBoxClasses.SelectedItem as DataRowView;
            if (drv == null) return;
            _selectedClassId = Convert.ToInt32(drv["class_id"]);
            BuildScheduleGrid();
        }

        // ── Grid ─────────────────────────────────────────────────────────────

        private void BuildScheduleGrid()
        {
            dataGrid.Rows.Clear();
            dataGrid.Columns.Clear();

            DataGridViewTextBoxColumn lessonCol = new DataGridViewTextBoxColumn();
            lessonCol.Name      = "lesson";
            lessonCol.HeaderText = "Урок";
            lessonCol.Width     = 50;
            lessonCol.ReadOnly  = true;
            lessonCol.DefaultCellStyle.BackColor = Color.WhiteSmoke;
            lessonCol.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGrid.Columns.Add(lessonCol);

            for (int d = 1; d <= 5; d++)
            {
                DataGridViewButtonColumn col = new DataGridViewButtonColumn();
                col.Name        = "day" + d;
                col.HeaderText  = DayNames[d];
                col.Width       = 180;
                col.FlatStyle   = FlatStyle.Flat;
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

            // Reset
            for (int l = 0; l < MAX_LESSONS; l++)
                for (int d = 1; d <= 5; d++)
                {
                    var cell = dataGrid.Rows[l].Cells["day" + d];
                    cell.Value = "+";
                    cell.Style.BackColor = Color.FromArgb(245, 245, 245);
                    cell.Style.ForeColor = Color.LightGray;
                    cell.Tag = null;
                }

            try
            {
                DataTable dt = DbHelper.Query(
                    "SELECT s.schedule_id, s.day_of_week, s.lesson_number, s.workload_id, s.classroom_id, " +
                    "w.subject_id, w.teacher_id, " +
                    "sub.subject_name, t.surname + ' ' + t.name + ' ' + t.patronymic AS teacher_name, cr.room_number " +
                    "FROM Schedule s " +
                    "JOIN Workload w   ON s.workload_id  = w.workload_id " +
                    "JOIN Subjects sub ON w.subject_id   = sub.subject_id " +
                    "JOIN Teachers t   ON w.teacher_id   = t.teacher_id " +
                    "JOIN Classrooms cr ON s.classroom_id = cr.classroom_id " +
                    "WHERE w.class_id = @cid",
                    p => p.AddWithValue("@cid", _selectedClassId));

                foreach (DataRow row in dt.Rows)
                {
                    int day    = Convert.ToInt32(row["day_of_week"]);
                    int lesson = Convert.ToInt32(row["lesson_number"]);
                    if (day < 1 || day > 5 || lesson < 1 || lesson > MAX_LESSONS) continue;
                    var cell = dataGrid.Rows[lesson - 1].Cells["day" + day];
                    cell.Value = string.Format("{0}\n{1} / {2}",
                        row["subject_name"], row["teacher_name"], row["room_number"]);
                    cell.Style.ForeColor = Color.Black;
                    cell.Style.BackColor = Color.White;
                    cell.Tag = row;
                }

                HighlightConflicts();
                LoadWarnings();
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
                    "WHERE w1.class_id = @cid " +
                    "  AND (w1.teacher_id = w2.teacher_id OR s1.classroom_id = s2.classroom_id)",
                    p => p.AddWithValue("@cid", _selectedClassId));

                foreach (DataRow cr in conflicts.Rows)
                {
                    int d = Convert.ToInt32(cr["day_of_week"]);
                    int l = Convert.ToInt32(cr["lesson_number"]);
                    if (d < 1 || d > 5 || l < 1 || l > MAX_LESSONS) continue;
                    var cell = dataGrid.Rows[l - 1].Cells["day" + d];
                    cell.Style.BackColor = Color.MistyRose;
                    cell.Style.ForeColor = Color.DarkRed;
                }
            }
            catch { }
        }

        // ── Warnings panel ───────────────────────────────────────────────────

        private void LoadWarnings()
        {
            listBoxWarnings.Items.Clear();
            try
            {
                // Get all conflicts for this class with full details
                DataTable dt = DbHelper.Query(
                    "SELECT DISTINCT s1.day_of_week, s1.lesson_number, " +
                    "sub1.subject_name AS subj1, " +
                    "t1.surname + ' ' + t1.name + ' ' + ISNULL(t1.patronymic,'') AS teach1, " +
                    "sub2.subject_name AS subj2, " +
                    "t2.surname + ' ' + t2.name + ' ' + ISNULL(t2.patronymic,'') AS teach2, " +
                    "CASE WHEN w1.teacher_id = w2.teacher_id THEN 'учитель' ELSE 'кабинет' END AS conflict_type, " +
                    "CAST(cp2.parallel AS NVARCHAR) + lc2.letterClass AS other_class " +
                    "FROM Schedule s1 " +
                    "JOIN Workload w1   ON s1.workload_id = w1.workload_id " +
                    "JOIN Subjects sub1 ON w1.subject_id  = sub1.subject_id " +
                    "JOIN Teachers t1   ON w1.teacher_id  = t1.teacher_id " +
                    "JOIN Schedule s2   ON s1.day_of_week = s2.day_of_week " +
                    "  AND s1.lesson_number = s2.lesson_number " +
                    "  AND s1.schedule_id  <> s2.schedule_id " +
                    "JOIN Workload w2   ON s2.workload_id = w2.workload_id " +
                    "JOIN Subjects sub2 ON w2.subject_id  = sub2.subject_id " +
                    "JOIN Teachers t2   ON w2.teacher_id  = t2.teacher_id " +
                    "JOIN Classes cl2   ON w2.class_id    = cl2.class_id " +
                    "JOIN ClassParallel cp2    ON cl2.id_parallel_class = cp2.id_parallel_class " +
                    "JOIN LetterOfTheClass lc2 ON cl2.id_letter_class   = lc2.id_letter_class " +
                    "WHERE w1.class_id = @cid " +
                    "  AND (w1.teacher_id = w2.teacher_id OR s1.classroom_id = s2.classroom_id)",
                    p => p.AddWithValue("@cid", _selectedClassId));

                if (dt.Rows.Count == 0)
                {
                    listBoxWarnings.Items.Add("Конфликтов нет ✓");
                    listBoxWarnings.ForeColor        = Color.Green;
                    labelWarningsTitle.BackColor     = Color.SeaGreen;
                    labelWarningsTitle.Text          = "  ✓ Конфликтов нет";
                    return;
                }

                labelWarningsTitle.BackColor = Color.Crimson;
                labelWarningsTitle.Text      = string.Format("  ⚠ Конфликты ({0})", dt.Rows.Count);
                listBoxWarnings.ForeColor    = Color.DarkRed;

                foreach (DataRow row in dt.Rows)
                {
                    string conflictType = row["conflict_type"].ToString();
                    string day    = DayNames[Convert.ToInt32(row["day_of_week"])];
                    string lesson = row["lesson_number"].ToString();
                    string teach1 = row["teach1"].ToString().Trim();
                    string teach2 = row["teach2"].ToString().Trim();
                    string cls2   = row["other_class"].ToString();

                    string msg = conflictType == "учитель"
                        ? string.Format("{0}, ур.{1} — {2} занят в {3}", day, lesson, teach1, cls2)
                        : string.Format("{0}, ур.{1} — кабинет занят ({2}, {3})", day, lesson, teach2, cls2);

                    listBoxWarnings.Items.Add(msg);
                }
            }
            catch (Exception ex)
            {
                listBoxWarnings.Items.Add("Ошибка загрузки: " + ex.Message);
            }
        }

        // ── Info panel ───────────────────────────────────────────────────────

        private void UpdateInfoPanel(DataRow row, int day, int lesson)
        {
            if (row == null)
            {
                labelInfoDayVal.Text     = DayNames[day];
                labelInfoLessonVal.Text  = lesson.ToString();
                labelInfoSubjectVal.Text = "—";
                labelInfoTeacherVal.Text = "—";
                labelInfoRoomVal.Text    = "—";
                return;
            }

            labelInfoDayVal.Text     = DayNames[day];
            labelInfoLessonVal.Text  = lesson.ToString();
            labelInfoSubjectVal.Text = row["subject_name"]?.ToString() ?? "—";
            labelInfoTeacherVal.Text = row["teacher_name"]?.ToString() ?? "—";
            labelInfoRoomVal.Text    = row["room_number"]?.ToString() ?? "—";
        }

        // ── Cell click ───────────────────────────────────────────────────────

        private void dataGrid_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex <= 0) return;
            if (_selectedClassId < 0) return;

            int day    = e.ColumnIndex;
            int lesson = e.RowIndex + 1;
            var cell   = dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex];
            DataRow existing = cell.Tag as DataRow;

            // Update info panel on every click
            UpdateInfoPanel(existing, day, lesson);

            bool isConflict = cell.Style.BackColor == Color.MistyRose;
            if (isConflict && existing != null)
            {
                string conflictDesc = GetConflictDescription(existing);
                using (ConflictDialogForm dlg = new ConflictDialogForm(conflictDesc))
                {
                    dlg.ShowDialog(this);
                    if (dlg.Choice == ConflictDialogForm.ConflictChoice.Cancel) return;
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
                }
            }

            using (CellEditForm dlg = new CellEditForm(_selectedClassId, day, lesson, existing))
            {
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    FillGridFromDb();
            }
        }

        // ── Conflict helpers ─────────────────────────────────────────────────

        private string GetConflictDescription(DataRow row)
        {
            var parts = new List<string>();
            try
            {
                if (!row.Table.Columns.Contains("teacher_id") ||
                    !row.Table.Columns.Contains("classroom_id"))
                    return "Конфликт расписания (недостаточно данных).";

                int teacherId   = Convert.ToInt32(row["teacher_id"]);
                int classroomId = Convert.ToInt32(row["classroom_id"]);
                int day         = Convert.ToInt32(row["day_of_week"]);
                int lesson      = Convert.ToInt32(row["lesson_number"]);
                int scheduleId  = Convert.ToInt32(row["schedule_id"]);

                DataTable teacherConflict = DbHelper.Query(
                    "SELECT sub.subject_name, t.surname + ' ' + t.name + ' ' + t.patronymic AS tname, " +
                    "CAST(cp.parallel AS NVARCHAR) + lc.letterClass AS class_name " +
                    "FROM Schedule s " +
                    "JOIN Workload w   ON s.workload_id = w.workload_id " +
                    "JOIN Subjects sub ON w.subject_id  = sub.subject_id " +
                    "JOIN Teachers t   ON w.teacher_id  = t.teacher_id " +
                    "JOIN Classes cl   ON w.class_id    = cl.class_id " +
                    "JOIN ClassParallel cp    ON cl.id_parallel_class = cp.id_parallel_class " +
                    "JOIN LetterOfTheClass lc ON cl.id_letter_class   = lc.id_letter_class " +
                    "WHERE s.day_of_week = @d AND s.lesson_number = @l " +
                    "  AND w.teacher_id = @tid AND s.schedule_id <> @sid",
                    p => { p.AddWithValue("@tid", teacherId); p.AddWithValue("@d", day);
                           p.AddWithValue("@l", lesson);      p.AddWithValue("@sid", scheduleId); });

                if (teacherConflict.Rows.Count > 0)
                {
                    DataRow tc = teacherConflict.Rows[0];
                    parts.Add(string.Format("Учитель «{0}» ведёт урок «{1}» у класса {2} в это время.",
                        tc["tname"], tc["subject_name"], tc["class_name"]));
                }

                DataTable roomConflict = DbHelper.Query(
                    "SELECT sub.subject_name, t.surname + ' ' + t.name + ' ' + t.patronymic AS tname, " +
                    "CAST(cp.parallel AS NVARCHAR) + lc.letterClass AS class_name, " +
                    "CAST(cr2.room_number AS NVARCHAR) AS rnum " +
                    "FROM Schedule s " +
                    "JOIN Workload w   ON s.workload_id  = w.workload_id " +
                    "JOIN Subjects sub ON w.subject_id   = sub.subject_id " +
                    "JOIN Teachers t   ON w.teacher_id   = t.teacher_id " +
                    "JOIN Classes cl   ON w.class_id     = cl.class_id " +
                    "JOIN ClassParallel cp    ON cl.id_parallel_class = cp.id_parallel_class " +
                    "JOIN LetterOfTheClass lc ON cl.id_letter_class   = lc.id_letter_class " +
                    "JOIN Classrooms cr2      ON s.classroom_id       = cr2.classroom_id " +
                    "WHERE s.day_of_week = @d AND s.lesson_number = @l " +
                    "  AND s.classroom_id = @cr AND s.schedule_id <> @sid",
                    p => { p.AddWithValue("@d", day);   p.AddWithValue("@l", lesson);
                           p.AddWithValue("@cr", classroomId); p.AddWithValue("@sid", scheduleId); });

                if (roomConflict.Rows.Count > 0)
                {
                    DataRow rc = roomConflict.Rows[0];
                    parts.Add(string.Format("Кабинет «{0}» занят — там идёт «{1}» ({2}) у класса {3}.",
                        rc["rnum"], rc["subject_name"], rc["tname"], rc["class_name"]));
                }
            }
            catch (Exception ex) { return "Ошибка: " + ex.Message; }

            return parts.Count > 0 ? string.Join("\n", parts) : "Конфликт расписания.";
        }

        private DataRow FindConflictingRow(DataRow row)
        {
            try
            {
                int teacherId   = Convert.ToInt32(row["teacher_id"]);
                int classroomId = Convert.ToInt32(row["classroom_id"]);
                int day         = Convert.ToInt32(row["day_of_week"]);
                int lesson      = Convert.ToInt32(row["lesson_number"]);
                int scheduleId  = Convert.ToInt32(row["schedule_id"]);

                DataTable dt = DbHelper.Query(
                    "SELECT s.schedule_id, s.day_of_week, s.lesson_number, s.workload_id, s.classroom_id, " +
                    "w.subject_id, w.teacher_id, sub.subject_name, t.surname + ' ' + t.name + ' ' + t.patronymic AS teacher_name, cr.room_number " +
                    "FROM Schedule s " +
                    "JOIN Workload w   ON s.workload_id  = w.workload_id " +
                    "JOIN Subjects sub ON w.subject_id   = sub.subject_id " +
                    "JOIN Teachers t   ON w.teacher_id   = t.teacher_id " +
                    "JOIN Classrooms cr ON s.classroom_id = cr.classroom_id " +
                    "WHERE s.day_of_week = @d AND s.lesson_number = @l " +
                    "  AND s.schedule_id <> @sid " +
                    "  AND (w.teacher_id = @tid OR s.classroom_id = @cr)",
                    p => { p.AddWithValue("@tid", teacherId); p.AddWithValue("@d", day);
                           p.AddWithValue("@l", lesson);      p.AddWithValue("@sid", scheduleId);
                           p.AddWithValue("@cr", classroomId); });

                return dt.Rows.Count > 0 ? dt.Rows[0] : null;
            }
            catch { return null; }
        }
    }
}
