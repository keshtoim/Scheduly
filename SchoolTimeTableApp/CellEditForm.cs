using System;
using System.Data;
using System.Windows.Forms;

namespace testing
{
    public partial class CellEditForm : Form
    {
        private readonly int     _classId;
        private readonly int     _day;
        private readonly int     _lesson;
        private readonly DataRow _existing;

        private static readonly string[] DayNames =
            { "", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };

        public CellEditForm(int classId, int day, int lesson, DataRow existing)
        {
            _classId  = classId;
            _day      = day;
            _lesson   = lesson;
            _existing = existing;
            InitializeComponent();
        }

        private void CellEditForm_Load(object sender, EventArgs e)
        {
            labelSlot.Text = string.Format("{0}, урок {1}", DayNames[_day], _lesson);

            LoadSubjects();
            LoadTeachers();
            LoadClassrooms();

            if (_existing != null)
            {
                // Pre-select subject
                foreach (DataRowView drv in comboSubject.Items)
                    if (drv["subject_id"].ToString() == _existing["subject_id"].ToString())
                    { comboSubject.SelectedItem = drv; break; }

                // Pre-select teacher
                foreach (DataRowView drv in comboTeacher.Items)
                    if (drv["teacher_id"].ToString() == _existing["teacher_id"].ToString())
                    { comboTeacher.SelectedItem = drv; break; }

                // Pre-select classroom
                foreach (DataRowView drv in comboClassroom.Items)
                    if (drv["classroom_id"].ToString() == _existing["classroom_id"].ToString())
                    { comboClassroom.SelectedItem = drv; break; }

                buttonDelete.Visible = true;
            }
            else
            {
                buttonDelete.Visible = false;
            }
        }

        // ── Loaders ──────────────────────────────────────────────────────────

        private void LoadSubjects()
        {
            try
            {
                // Show only subjects that have workload for this class
                DataTable dt = DbHelper.Query(
                    "SELECT DISTINCT s.subject_id, s.subject_name " +
                    "FROM Subjects s " +
                    "JOIN Workload w ON w.subject_id = s.subject_id " +
                    "WHERE w.class_id = @cid " +
                    "ORDER BY s.subject_name",
                    p => p.AddWithValue("@cid", _classId));

                comboSubject.DataSource    = dt;
                comboSubject.DisplayMember = "subject_name";
                comboSubject.ValueMember   = "subject_id";
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка предметов"); }
        }

        private void LoadTeachers()
        {
            try
            {
                // Show all teachers — user picks freely
                DataTable dt = DbHelper.Query(
                    "SELECT teacher_id, name FROM Teachers ORDER BY name",
                    null);

                comboTeacher.DataSource    = dt;
                comboTeacher.DisplayMember = "name";
                comboTeacher.ValueMember   = "teacher_id";
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка учителей"); }
        }

        private void LoadClassrooms()
        {
            try
            {
                DataTable dt = DbHelper.Query(
                    "SELECT cr.classroom_id, " +
                    "CAST(cr.room_number AS NVARCHAR) + ' (' + ct.classroom_type + ')' AS display " +
                    "FROM Classrooms cr " +
                    "JOIN ClassroomTypes ct ON cr.type_id = ct.type_id " +
                    "ORDER BY cr.room_number",
                    null);

                comboClassroom.DataSource    = dt;
                comboClassroom.DisplayMember = "display";
                comboClassroom.ValueMember   = "classroom_id";
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка кабинетов"); }
        }

        // ── Save ─────────────────────────────────────────────────────────────

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboSubject.SelectedValue == null ||
                comboTeacher.SelectedValue == null ||
                comboClassroom.SelectedValue == null)
            {
                MessageBox.Show("Выберите предмет, учителя и кабинет.",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int subjectId   = Convert.ToInt32(comboSubject.SelectedValue);
            int teacherId   = Convert.ToInt32(comboTeacher.SelectedValue);
            int classroomId = Convert.ToInt32(comboClassroom.SelectedValue);

            // Find or create workload entry for this class + subject + teacher
            int workloadId = ResolveWorkloadId(subjectId, teacherId);
            if (workloadId < 0) return; // error already shown

            // Conflict check
            string conflictMsg = CheckConflicts(workloadId, classroomId);
            if (!string.IsNullOrEmpty(conflictMsg))
            {
                DialogResult dr = MessageBox.Show(
                    "Обнаружен конфликт:\n" + conflictMsg + "\n\nВсё равно сохранить?",
                    "Конфликт расписания", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (dr != DialogResult.Yes) return;
            }

            try
            {
                if (_existing == null)
                {
                    DbHelper.Execute(
                        "INSERT INTO Schedule (workload_id, classroom_id, day_of_week, lesson_number) " +
                        "VALUES (@w, @cr, @d, @l)",
                        p => {
                            p.AddWithValue("@w",  workloadId);
                            p.AddWithValue("@cr", classroomId);
                            p.AddWithValue("@d",  _day);
                            p.AddWithValue("@l",  _lesson);
                        });
                }
                else
                {
                    DbHelper.Execute(
                        "UPDATE Schedule SET workload_id = @w, classroom_id = @cr " +
                        "WHERE schedule_id = @id",
                        p => {
                            p.AddWithValue("@w",  workloadId);
                            p.AddWithValue("@cr", classroomId);
                            p.AddWithValue("@id", Convert.ToInt32(_existing["schedule_id"]));
                        });
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Сохранение урока"); }
        }

        /// <summary>
        /// Finds existing workload_id for class+subject+teacher.
        /// If not found — creates a new Workload record with hours_per_week = 0
        /// (can be corrected later in the Workload tab).
        /// Returns -1 on error.
        /// </summary>
        private int ResolveWorkloadId(int subjectId, int teacherId)
        {
            try
            {
                DataTable dt = DbHelper.Query(
                    "SELECT workload_id FROM Workload " +
                    "WHERE class_id = @c AND subject_id = @s AND teacher_id = @t",
                    p => {
                        p.AddWithValue("@c", _classId);
                        p.AddWithValue("@s", subjectId);
                        p.AddWithValue("@t", teacherId);
                    });

                if (dt.Rows.Count > 0)
                    return Convert.ToInt32(dt.Rows[0]["workload_id"]);

                // Not found — create it
                DbHelper.Execute(
                    "INSERT INTO Workload (class_id, subject_id, teacher_id, hours_per_week) " +
                    "VALUES (@c, @s, @t, 0)",
                    p => {
                        p.AddWithValue("@c", _classId);
                        p.AddWithValue("@s", subjectId);
                        p.AddWithValue("@t", teacherId);
                    });

                // Return new id
                DataTable dt2 = DbHelper.Query(
                    "SELECT TOP 1 workload_id FROM Workload " +
                    "WHERE class_id = @c AND subject_id = @s AND teacher_id = @t " +
                    "ORDER BY workload_id DESC",
                    p => {
                        p.AddWithValue("@c", _classId);
                        p.AddWithValue("@s", subjectId);
                        p.AddWithValue("@t", teacherId);
                    });

                return dt2.Rows.Count > 0 ? Convert.ToInt32(dt2.Rows[0]["workload_id"]) : -1;
            }
            catch (Exception ex)
            {
                DbHelper.ShowError(ex, "Определение нагрузки");
                return -1;
            }
        }

        // ── Delete ───────────────────────────────────────────────────────────

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (_existing == null) return;
            if (MessageBox.Show("Удалить этот урок из расписания?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                DbHelper.Execute("DELETE FROM Schedule WHERE schedule_id = @id",
                    p => p.AddWithValue("@id", Convert.ToInt32(_existing["schedule_id"])));
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Удаление урока"); }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        // ── Conflict check ───────────────────────────────────────────────────

        private string CheckConflicts(int workloadId, int classroomId)
        {
            string skipClause = _existing != null
                ? " AND s.schedule_id <> " + _existing["schedule_id"]
                : "";

            // Teacher busy? — find teacher_id from workload, then check schedule
            bool teacherBusy = DbHelper.Exists(
                "SELECT COUNT(*) FROM Schedule s " +
                "JOIN Workload w  ON s.workload_id  = w.workload_id " +
                "JOIN Workload w2 ON w2.workload_id = @wid " +
                "WHERE s.day_of_week = @d AND s.lesson_number = @l " +
                "AND w.teacher_id = w2.teacher_id" + skipClause,
                p => {
                    p.AddWithValue("@wid", workloadId);
                    p.AddWithValue("@d",   _day);
                    p.AddWithValue("@l",   _lesson);
                });

            // Classroom busy? — direct check on classroom_id, no workload join needed
            bool roomBusy = DbHelper.Exists(
                "SELECT COUNT(*) FROM Schedule s " +
                "WHERE s.day_of_week = @d AND s.lesson_number = @l " +
                "AND s.classroom_id = @cr" + skipClause,
                p => {
                    p.AddWithValue("@d",  _day);
                    p.AddWithValue("@l",  _lesson);
                    p.AddWithValue("@cr", classroomId);
                });

            if (teacherBusy && roomBusy) return "Учитель и кабинет уже заняты в это время.";
            if (teacherBusy)             return "Учитель уже ведёт другой урок в это время.";
            if (roomBusy)                return "Кабинет уже занят в это время.";
            return null;
        }
    }
}
