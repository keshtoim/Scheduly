using System;
using System.Data;
using System.Windows.Forms;

namespace testing
{
    /// <summary>
    /// Диалог назначения или редактирования одного урока в расписании.
    /// Открывается при клике на ячейку сетки во вкладке «Составление».
    /// Позволяет выбрать предмет, учителя и кабинет независимо друг от друга.
    /// Перед сохранением проверяет конфликты.
    /// </summary>
    public partial class CellEditForm : Form
    {
        private readonly int     _classId;   // ID класса для которого добавляется урок
        private readonly int     _day;       // День недели (1=Пн ... 5=Пт)
        private readonly int     _lesson;    // Номер урока (1-8)
        private readonly DataRow _existing;  // Существующая запись при редактировании, null при добавлении

        private static readonly string[] DayNames =
            { "", "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };

        /// <summary>
        /// Создаёт диалог для указанного слота.
        /// </summary>
        /// <param name="existing">null — режим добавления, DataRow — режим редактирования.</param>
        public CellEditForm(int classId, int day, int lesson, DataRow existing)
        {
            _classId  = classId;
            _day      = day;
            _lesson   = lesson;
            _existing = existing;
            InitializeComponent();
        }

        /// <summary>
        /// Загружает данные при открытии формы.
        /// Заполняет выпадающие списки и предварительно выбирает текущие значения при редактировании.
        /// </summary>
        private void CellEditForm_Load(object sender, EventArgs e)
        {
            labelSlot.Text = string.Format("{0}, урок {1}", DayNames[_day], _lesson);

            LoadSubjects();
            LoadTeachers();
            LoadClassrooms();

            if (_existing != null)
            {
                // Режим редактирования — предвыбираем текущие значения
                foreach (DataRowView drv in comboSubject.Items)
                    if (drv["subject_id"].ToString() == _existing["subject_id"].ToString())
                    { comboSubject.SelectedItem = drv; break; }

                foreach (DataRowView drv in comboTeacher.Items)
                    if (drv["teacher_id"].ToString() == _existing["teacher_id"].ToString())
                    { comboTeacher.SelectedItem = drv; break; }

                foreach (DataRowView drv in comboClassroom.Items)
                    if (drv["classroom_id"].ToString() == _existing["classroom_id"].ToString())
                    { comboClassroom.SelectedItem = drv; break; }

                buttonDelete.Visible = true; // Кнопка удаления только при редактировании
            }
            else
            {
                buttonDelete.Visible = false;
            }
        }

        // ── Загрузка списков ──────────────────────────────────────────────────

        /// <summary>
        /// Загружает предметы из нагрузки данного класса.
        /// Показывает только те предметы, которые уже назначены классу через таблицу Workload.
        /// </summary>
        private void LoadSubjects()
        {
            try
            {
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

        /// <summary>
        /// Загружает всех учителей без привязки к предмету.
        /// Пользователь может выбрать любого учителя — замены реализованы через это.
        /// </summary>
        private void LoadTeachers()
        {
            try
            {
                DataTable dt = DbHelper.Query(
                    "SELECT teacher_id, name FROM Teachers ORDER BY name", null);

                comboTeacher.DataSource    = dt;
                comboTeacher.DisplayMember = "name";
                comboTeacher.ValueMember   = "teacher_id";
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка учителей"); }
        }

        /// <summary>
        /// Загружает все кабинеты с указанием типа.
        /// room_number приводится к NVARCHAR чтобы избежать ошибки конкатенации с tinyint.
        /// </summary>
        private void LoadClassrooms()
        {
            try
            {
                DataTable dt = DbHelper.Query(
                    "SELECT cr.classroom_id, " +
                    "CAST(cr.room_number AS NVARCHAR) + ' (' + ct.classroom_type + ')' AS display " +
                    "FROM Classrooms cr " +
                    "JOIN ClassroomTypes ct ON cr.type_id = ct.type_id " +
                    "ORDER BY cr.room_number", null);

                comboClassroom.DataSource    = dt;
                comboClassroom.DisplayMember = "display";
                comboClassroom.ValueMember   = "classroom_id";
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка кабинетов"); }
        }

        // ── Сохранение ────────────────────────────────────────────────────────

        /// <summary>
        /// Сохраняет урок в расписании.
        /// Сначала находит или создаёт запись Workload для данной комбинации класс+предмет+учитель,
        /// затем проверяет конфликты и выполняет INSERT или UPDATE в таблице Schedule.
        /// </summary>
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

            // Находим или создаём запись нагрузки для этой комбинации
            int workloadId = ResolveWorkloadId(subjectId, teacherId);
            if (workloadId < 0) return;

            // Проверяем конфликты — занятость учителя и кабинета
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
                    // Новый урок — INSERT
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
                    // Редактирование — UPDATE по schedule_id
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
        /// Находит существующую запись Workload для комбинации класс+предмет+учитель.
        /// Если запись не найдена — создаёт новую с hours_per_week = 0.
        /// Возвращает workload_id или -1 при ошибке.
        /// </summary>
        private int ResolveWorkloadId(int subjectId, int teacherId)
        {
            try
            {
                // Ищем существующую запись нагрузки
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

                // Не найдена — создаём новую запись нагрузки
                // hours_per_week = 0, можно скорректировать во вкладке Нагрузка
                DbHelper.Execute(
                    "INSERT INTO Workload (class_id, subject_id, teacher_id, hours_per_week) " +
                    "VALUES (@c, @s, @t, 0)",
                    p => {
                        p.AddWithValue("@c", _classId);
                        p.AddWithValue("@s", subjectId);
                        p.AddWithValue("@t", teacherId);
                    });

                // Получаем ID только что созданной записи
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

        /// <summary>
        /// Удаляет урок из расписания после подтверждения.
        /// </summary>
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

        // ── Проверка конфликтов ───────────────────────────────────────────────

        /// <summary>
        /// Проверяет занятость учителя и кабинета в указанный слот расписания.
        /// При редактировании исключает текущую запись из проверки.
        /// </summary>
        /// <returns>Текст описания конфликта или null если конфликтов нет.</returns>
        private string CheckConflicts(int workloadId, int classroomId)
        {
            // При редактировании исключаем текущую запись из проверки
            string skipClause = _existing != null
                ? " AND s.schedule_id <> " + _existing["schedule_id"]
                : "";

            // Проверяем занятость учителя — через teacher_id из workload
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

            // Проверяем занятость кабинета — прямая проверка по classroom_id
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
