using System;
using System.Collections.Generic;
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
                // Предвыбираем текущие значения
                foreach (DataRowView drv in comboSubject.Items)
                    if (drv["ID_предмета_со_сложностью"].ToString() ==
                        _existing["ID_предмета_со_сложностью"].ToString())
                    { comboSubject.SelectedItem = drv; break; }

                foreach (DataRowView drv in comboTeacher.Items)
                    if (drv["teacher_id"].ToString() == _existing["ID_учителя"].ToString())
                    { comboTeacher.SelectedItem = drv; break; }

                foreach (DataRowView drv in comboClassroom.Items)
                    if (drv["classroom_id"].ToString() == _existing["ID_кабинета"].ToString())
                    { comboClassroom.SelectedItem = drv; break; }

                // Предвыбираем чётность недели из существующей записи
                int parity = Convert.ToInt32(_existing["Чётность_недели"]);
                if      (parity == 1) radioWeekOdd.Checked  = true;
                else if (parity == 2) radioWeekEven.Checked = true;
                else                  radioWeekAll.Checked  = true;

                buttonDelete.Visible = true;
            }
            else
            {
                buttonDelete.Visible = false;
            }
        }

        private void LoadSubjects()
        {
            try
            {
                // Предметы из нагрузки данного класса через новую схему
                DataTable dt = DbHelper.Query(
                    "SELECT DISTINCT sbp.ID_предмета_со_сложностью, sub.Название AS subject_name " +
                    "FROM Workload w " +
                    "JOIN SubjectByParallel sbp ON w.ID_предмета_параллели = sbp.ID_предмета_со_сложностью " +
                    "JOIN Subjects sub ON sbp.ID_предмета = sub.ID_предмета " +
                    "WHERE w.ID_класса = @cid " +
                    "ORDER BY sub.Название",
                    p => p.AddWithValue("@cid", _classId));

                comboSubject.DisplayMember = "subject_name";
                comboSubject.ValueMember   = "ID_предмета_со_сложностью";
                comboSubject.DataSource    = dt;
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка предметов"); }
        }

        private void LoadTeachers()
        {
            try
            {
                DataTable dtAll = DbHelper.Query(
                    "SELECT t.ID_учителя AS teacher_id, " +
                    "t.Фамилия || ' ' || t.Имя || ' ' || t.Отчество AS full_name " +
                    "FROM Teachers t " +
                    "WHERE t.ID_учителя IN " +
                    "  (SELECT DISTINCT w.ID_учителя FROM Workload w WHERE w.ID_класса = @cid) " +
                    "ORDER BY t.Фамилия, t.Имя",
                    p => p.AddWithValue("@cid", _classId));

                // Занятые в данный слот учителя
                DataTable dtBusy = DbHelper.Query(
                    "SELECT DISTINCT ID_учителя FROM vw_Schedule " +
                    "WHERE ID_дня_недели = @d AND Номер_урока = @l" +
                    (_existing != null ? " AND ID_расписания <> @sid" : ""),
                    p => {
                        p.AddWithValue("@d", _day); p.AddWithValue("@l", _lesson);
                        if (_existing != null) p.AddWithValue("@sid", _existing["ID_расписания"]);
                    });

                var busyIds = new HashSet<int>();
                foreach (DataRow r in dtBusy.Rows) busyIds.Add(Convert.ToInt32(r["ID_учителя"]));

                DataTable dtDisplay = new DataTable();
                dtDisplay.Columns.Add("teacher_id", typeof(int));
                dtDisplay.Columns.Add("full_name",  typeof(string));

                foreach (DataRow r in dtAll.Rows)
                {
                    int id = Convert.ToInt32(r["teacher_id"]);
                    if (!busyIds.Contains(id))
                        dtDisplay.Rows.Add(id, r["full_name"].ToString());
                }
                foreach (DataRow r in dtAll.Rows)
                {
                    int id = Convert.ToInt32(r["teacher_id"]);
                    if (busyIds.Contains(id))
                        dtDisplay.Rows.Add(id, r["full_name"] + "  ⛔ ЗАНЯТ");
                }

                comboTeacher.DisplayMember = "full_name";
                comboTeacher.ValueMember   = "teacher_id";
                comboTeacher.DataSource    = dtDisplay;
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка учителей"); }
        }

        private void LoadClassrooms()
        {
            try
            {
                DataTable dtAll = DbHelper.Query(
                    "SELECT cr.ID_кабинета AS classroom_id, cr.Номер, ct.Тип_кабинета, " +
                    "CAST(cr.Номер AS TEXT) || ' (' || ct.Тип_кабинета || ')' AS display " +
                    "FROM Classrooms cr JOIN ClassroomTypes ct ON cr.ID_типа_кабинета = ct.ID_типа_кабинета " +
                    "ORDER BY cr.Номер");

                // Занятые кабинеты в данный слот
                DataTable dtBusy = DbHelper.Query(
                    "SELECT DISTINCT ID_кабинета FROM Schedule s " +
                    "WHERE ID_дня_недели = @d AND ID_номера_урока = @l" +
                    (_existing != null ? " AND ID_расписания <> @sid" : ""),
                    p => {
                        p.AddWithValue("@d", _day); p.AddWithValue("@l", _lesson);
                        if (_existing != null) p.AddWithValue("@sid", _existing["ID_расписания"]);
                    });

                var busyIds = new HashSet<int>();
                foreach (DataRow r in dtBusy.Rows) busyIds.Add(Convert.ToInt32(r["ID_кабинета"]));

                DataTable dtDisplay = new DataTable();
                dtDisplay.Columns.Add("classroom_id", typeof(int));
                dtDisplay.Columns.Add("display",      typeof(string));

                foreach (DataRow r in dtAll.Rows)
                {
                    int id = Convert.ToInt32(r["classroom_id"]);
                    if (!busyIds.Contains(id))
                        dtDisplay.Rows.Add(id, r["display"].ToString());
                }
                foreach (DataRow r in dtAll.Rows)
                {
                    int id = Convert.ToInt32(r["classroom_id"]);
                    if (busyIds.Contains(id))
                        dtDisplay.Rows.Add(id, r["display"] + "  ⛔ ЗАНЯТ");
                }

                comboClassroom.DisplayMember = "display";
                comboClassroom.ValueMember   = "classroom_id";
                comboClassroom.DataSource    = dtDisplay;
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка кабинетов"); }
        }

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

            int subjectParallelId = Convert.ToInt32(comboSubject.SelectedValue);
            int teacherId         = Convert.ToInt32(comboTeacher.SelectedValue);
            int classroomId       = Convert.ToInt32(comboClassroom.SelectedValue);
            int weekParity        = radioWeekOdd.Checked ? 1 : (radioWeekEven.Checked ? 2 : 0);

            // Проверка лимита уроков
            string limitMsg = CheckDayLimit();
            if (limitMsg != null)
            {
                if (MessageBox.Show(limitMsg + "\n\nВсё равно добавить?",
                    "Превышение лимита", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    != DialogResult.Yes) return;
            }

            // Проверка конфликта учителя с учётом чётности недели
            string conflictMsg = CheckTeacherConflict(teacherId, weekParity);
            if (conflictMsg != null)
            {
                if (MessageBox.Show("Обнаружен конфликт:\n" + conflictMsg + "\n\nВсё равно сохранить?",
                    "Конфликт расписания", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                    != DialogResult.Yes) return;
            }

            // Находим или создаём нагрузку
            int workloadId = ResolveWorkloadId(subjectParallelId, teacherId);
            if (workloadId < 0) return;

            try
            {
                if (_existing == null)
                {
                    DbHelper.ExecProcNonQuery("sp_AddLesson",
                        p => {
                            p.AddWithValue("@ID_нагрузки",      workloadId);
                            p.AddWithValue("@ID_кабинета",      classroomId);
                            p.AddWithValue("@ID_дня_недели",    _day);
                            p.AddWithValue("@ID_номера_урока",  _lesson);
                            p.AddWithValue("@Чётность_недели",  weekParity);
                        });
                }
                else
                {
                    DbHelper.ExecProcNonQuery("sp_UpdateLesson",
                        p => {
                            p.AddWithValue("@ID_расписания",    Convert.ToInt32(_existing["ID_расписания"]));
                            p.AddWithValue("@ID_нагрузки",      workloadId);
                            p.AddWithValue("@ID_кабинета",      classroomId);
                            p.AddWithValue("@Чётность_недели",  weekParity);
                        });
                }
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Сохранение урока"); }
        }

        private int ResolveWorkloadId(int subjectParallelId, int teacherId)
        {
            try
            {
                DataTable dt = DbHelper.Query(
                    "SELECT ID_нагрузки FROM Workload " +
                    "WHERE ID_класса = @c AND ID_предмета_параллели = @s AND ID_учителя = @t",
                    p => { p.AddWithValue("@c", _classId);
                           p.AddWithValue("@s", subjectParallelId);
                           p.AddWithValue("@t", teacherId); });

                if (dt.Rows.Count > 0)
                    return Convert.ToInt32(dt.Rows[0]["ID_нагрузки"]);

                // Создаём через хранимую процедуру
                DataTable dt2 = DbHelper.ExecProc("sp_AddWorkload",
                    p => { p.AddWithValue("@ID_учителя",               teacherId);
                           p.AddWithValue("@ID_класса",                _classId);
                           p.AddWithValue("@ID_предмета_параллели",    subjectParallelId);
                           p.AddWithValue("@Количество_часов_в_неделю", (byte)1);
                           p.AddWithValue("@Подгруппа", System.DBNull.Value); });

                return dt2.Rows.Count > 0 ? Convert.ToInt32(dt2.Rows[0]["ID_нагрузки"]) : -1;
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Определение нагрузки"); return -1; }
        }

        private string CheckDayLimit()
        {
            try
            {
                DataTable dtCls = DbHelper.Query(
                    "SELECT CAST(pc.Параллель AS TEXT) || lc.Буква AS class_name " +
                    "FROM Classes cl " +
                    "JOIN ParallelClass pc ON cl.ID_параллели_класса = pc.ID_параллели_класса " +
                    "JOIN LetterClass lc   ON cl.ID_буквы_класса     = lc.ID_буквы_класса " +
                    "WHERE cl.ID_класса = @cid",
                    p => p.AddWithValue("@cid", _classId));
                if (dtCls.Rows.Count == 0) return null;

                string className = dtCls.Rows[0]["class_name"].ToString();
                int limit = SettingsForm.GetLimit(className);

                string skip = _existing != null
                    ? " AND s.ID_расписания <> " + _existing["ID_расписания"] : "";
                DataTable dtCount = DbHelper.Query(
                    "SELECT COUNT(*) AS cnt FROM Schedule s " +
                    "JOIN Workload w ON s.ID_нагрузки = w.ID_нагрузки " +
                    "WHERE w.ID_класса = @cid AND s.ID_дня_недели = @d" + skip,
                    p => { p.AddWithValue("@cid", _classId); p.AddWithValue("@d", _day); });

                int current = Convert.ToInt32(dtCount.Rows[0]["cnt"]);
                if (current >= limit)
                    return string.Format(
                        "Для класса {0} установлен лимит {1} уроков в день.\n" +
                        "В этот день уже стоит {2} уроков.", className, limit, current);
            }
            catch { }
            return null;
        }

        private string CheckTeacherConflict(int teacherId, int weekParity)
        {
            string skip = _existing != null
                ? " AND ID_расписания <> " + _existing["ID_расписания"] : "";
            bool busy = DbHelper.Exists(
                "SELECT COUNT(*) FROM vw_Schedule " +
                "WHERE ID_дня_недели = @d AND Номер_урока = @l AND ID_учителя = @tid" +
                " AND (Чётность_недели = 0 OR @week = 0 OR Чётность_недели = @week)" + skip,
                p => { p.AddWithValue("@tid",  teacherId);
                       p.AddWithValue("@d",    _day);
                       p.AddWithValue("@l",    _lesson);
                       p.AddWithValue("@week", weekParity); });
            return busy ? "Учитель уже ведёт другой урок в это время." : null;
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (_existing == null) return;
            if (MessageBox.Show("Удалить этот урок из расписания?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                DbHelper.ExecProcNonQuery("sp_DeleteLesson",
                    p => p.AddWithValue("@ID_расписания",
                         Convert.ToInt32(_existing["ID_расписания"])));
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

        private void buttonAddTeacher_Click(object sender, EventArgs e)
        {
            using (var dlg = new QuickAddForm("Новый учитель",
                new[] { "Фамилия", "Имя", "Отчество", "Часов в неделю" }))
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                string surname = dlg.Values[0].Trim();
                string name    = dlg.Values[1].Trim();
                string patr    = dlg.Values[2].Trim();
                if (string.IsNullOrEmpty(surname)) return;
                decimal hours  = 0;
                decimal.TryParse(dlg.Values[3], out hours);
                try
                {
                    DbHelper.ExecProcNonQuery("sp_AddTeacher",
                        p => { p.AddWithValue("@Фамилия",  surname);
                               p.AddWithValue("@Имя",      name);
                               p.AddWithValue("@Отчество", patr);
                               p.AddWithValue("@Ставка",   hours > 0 ? hours : 1m); });
                    LoadTeachers();
                    string fullName = surname + " " + name + " " + patr;
                    foreach (DataRowView drv in comboTeacher.Items)
                        if (drv["full_name"].ToString().StartsWith(surname))
                        { comboTeacher.SelectedItem = drv; break; }
                }
                catch (Exception ex) { DbHelper.ShowError(ex, "Добавление учителя"); }
            }
        }

        private void buttonAddClassroom_Click(object sender, EventArgs e)
        {
            DataTable dtTypes = DbHelper.Query(
                "SELECT ID_типа_кабинета, Тип_кабинета AS name FROM ClassroomTypes ORDER BY Тип_кабинета");
            if (dtTypes.Rows.Count == 0) { MessageBox.Show("Нет типов кабинетов."); return; }

            using (var dlg = new QuickAddClassroomForm(dtTypes))
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    DbHelper.ExecProcNonQuery("sp_AddClassroom",
                        p => { p.AddWithValue("@Номер",            dlg.RoomNumber);
                               p.AddWithValue("@Вместимость",      dlg.Capacity > 0 ? dlg.Capacity : 30);
                               p.AddWithValue("@ID_типа_кабинета", dlg.TypeId); });
                    LoadClassrooms();
                }
                catch (Exception ex) { DbHelper.ShowError(ex, "Добавление кабинета"); }
            }
        }
    }
}
