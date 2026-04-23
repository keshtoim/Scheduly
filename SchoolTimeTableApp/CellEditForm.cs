using System;
using System.Data;
using System.Windows.Forms;

namespace testing
{
    /// <summary>
    /// Dialog for assigning or editing a single schedule slot.
    /// Shows available workload entries for the class, lets user pick classroom.
    /// Checks for conflicts (teacher busy / classroom busy) and warns.
    /// </summary>
    public partial class CellEditForm : Form
    {
        private readonly int _classId;
        private readonly int _day;
        private readonly int _lesson;
        private readonly DataRow _existing; // null = new slot

        private static readonly string[] DayNames = { "", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };

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

            LoadWorkload();
            LoadClassrooms();

            if (_existing != null)
            {
                // Pre-select current workload
                foreach (DataRowView drv in comboWorkload.Items)
                    if (drv["workload_id"].ToString() == _existing["workload_id"].ToString())
                    { comboWorkload.SelectedItem = drv; break; }

                // Pre-select current classroom
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

        private void LoadWorkload()
        {
            try
            {
                DataTable dt = DbHelper.Query(
                    "SELECT w.workload_id, " +
                    "sub.subject_name + ' (' + t.name + ', ' + CAST(w.hours_per_week AS NVARCHAR) + ' ч/нед)' AS display " +
                    "FROM Workload w " +
                    "JOIN Subjects sub ON w.subject_id = sub.subject_id " +
                    "JOIN Teachers t ON w.teacher_id = t.teacher_id " +
                    "WHERE w.class_id = @cid " +
                    "ORDER BY sub.subject_name",
                    p => p.AddWithValue("@cid", _classId));

                comboWorkload.DataSource    = dt;
                comboWorkload.DisplayMember = "display";
                comboWorkload.ValueMember   = "workload_id";
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка нагрузки"); }
        }

        private void LoadClassrooms()
        {
            try
            {
                DataTable dt = DbHelper.Query(
                    "SELECT cr.classroom_id, CAST(cr.room_number AS NVARCHAR) + ' (' + ct.classroom_type + ')' AS display " +
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            if (comboWorkload.SelectedValue == null || comboClassroom.SelectedValue == null)
            {
                MessageBox.Show("Выберите предмет/учителя и кабинет.",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int workloadId  = Convert.ToInt32(comboWorkload.SelectedValue);
            int classroomId = Convert.ToInt32(comboClassroom.SelectedValue);

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
                    int scheduleId = Convert.ToInt32(_existing["schedule_id"]);
                    DbHelper.Execute(
                        "UPDATE Schedule SET workload_id = @w, classroom_id = @cr WHERE schedule_id = @id",
                        p => {
                            p.AddWithValue("@w",  workloadId);
                            p.AddWithValue("@cr", classroomId);
                            p.AddWithValue("@id", scheduleId);
                        });
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Сохранение урока"); }
        }

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

        private string CheckConflicts(int workloadId, int classroomId)
        {
            string skipClause = _existing != null
                ? " AND s.schedule_id <> " + _existing["schedule_id"]
                : "";

            // Teacher busy?
            bool teacherBusy = DbHelper.Exists(
                "SELECT COUNT(*) FROM Schedule s " +
                "JOIN Workload w ON s.workload_id = w.workload_id " +
                "JOIN Workload w2 ON w2.workload_id = @wid " +
                "WHERE s.day_of_week = @d AND s.lesson_number = @l " +
                "AND w.teacher_id = w2.teacher_id" + skipClause,
                p => {
                    p.AddWithValue("@wid", workloadId);
                    p.AddWithValue("@d",   _day);
                    p.AddWithValue("@l",   _lesson);
                });

            // Classroom busy?
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
