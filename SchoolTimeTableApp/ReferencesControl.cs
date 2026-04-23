using System;
using System.Data;
using System.Windows.Forms;

namespace testing
{
    public partial class ReferencesControl : UserControl
    {
        public ReferencesControl() { InitializeComponent(); }

        public void LoadData()
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            LoadTeachers();
            LoadSubjects();
            LoadClassrooms();
            LoadClasses();
        }

        // ── Teachers ────────────────────────────────────────────────────────

        private void LoadTeachers()
        {
            gridTeachers.DataSource = DbHelper.Query("SELECT teacher_id, name AS [Имя], teaching_hours AS [Часов] FROM Teachers ORDER BY name");
            if (gridTeachers.Columns.Contains("teacher_id")) gridTeachers.Columns["teacher_id"].Visible = false;
        }

        private void buttonAddTeacher_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTeacherName.Text)) return;
            int hours = 0; int.TryParse(txtTeacherHours.Text, out hours);
            try
            {
                DbHelper.Execute("INSERT INTO Teachers (name, teaching_hours) VALUES (@n, @h)",
                    p => { p.AddWithValue("@n", txtTeacherName.Text.Trim()); p.AddWithValue("@h", hours); });
                txtTeacherName.Clear(); txtTeacherHours.Clear();
                LoadTeachers();
            }
            catch (Exception ex) { DbHelper.ShowError(ex); }
        }

        private void buttonDeleteTeacher_Click(object sender, EventArgs e)
        {
            DeleteSelected(gridTeachers, "Teachers", "teacher_id", LoadTeachers);
        }

        // ── Subjects ────────────────────────────────────────────────────────

        private void LoadSubjects()
        {
            gridSubjects.DataSource = DbHelper.Query("SELECT subject_id, subject_name AS [Предмет], difficulty_level AS [Сложность] FROM Subjects ORDER BY subject_name");
            if (gridSubjects.Columns.Contains("subject_id")) gridSubjects.Columns["subject_id"].Visible = false;
        }

        private void buttonAddSubject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubjectName.Text)) return;
            int diff = 0; int.TryParse(txtSubjectDiff.Text, out diff);
            try
            {
                DbHelper.Execute("INSERT INTO Subjects (subject_name, difficulty_level) VALUES (@n, @d)",
                    p => { p.AddWithValue("@n", txtSubjectName.Text.Trim()); p.AddWithValue("@d", diff); });
                txtSubjectName.Clear(); txtSubjectDiff.Clear();
                LoadSubjects();
            }
            catch (Exception ex) { DbHelper.ShowError(ex); }
        }

        private void buttonDeleteSubject_Click(object sender, EventArgs e)
        {
            DeleteSelected(gridSubjects, "Subjects", "subject_id", LoadSubjects);
        }

        // ── Classrooms ──────────────────────────────────────────────────────

        private void LoadClassrooms()
        {
            gridClassrooms.DataSource = DbHelper.Query(
                "SELECT cr.classroom_id, cr.room_number AS [Кабинет], cr.capacity AS [Вместимость], ct.classroom_type AS [Тип] " +
                "FROM Classrooms cr JOIN ClassroomTypes ct ON cr.type_id = ct.type_id ORDER BY room_number");
            if (gridClassrooms.Columns.Contains("classroom_id")) gridClassrooms.Columns["classroom_id"].Visible = false;

            // Load type combo
            DataTable dtTypes = DbHelper.Query("SELECT type_id, classroom_type AS name FROM ClassroomTypes ORDER BY classroom_type");
            comboClassroomType.DataSource = dtTypes;
            comboClassroomType.DisplayMember = "name";
            comboClassroomType.ValueMember = "type_id";
        }

        private void buttonAddClassroom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomNumber.Text) || comboClassroomType.SelectedValue == null) return;
            int cap = 0; int.TryParse(txtRoomCapacity.Text, out cap);
            try
            {
                DbHelper.Execute("INSERT INTO Classrooms (room_number, capacity, type_id) VALUES (@r, @c, @t)",
                    p => { p.AddWithValue("@r", txtRoomNumber.Text.Trim()); p.AddWithValue("@c", cap);
                           p.AddWithValue("@t", comboClassroomType.SelectedValue); });
                txtRoomNumber.Clear(); txtRoomCapacity.Clear();
                LoadClassrooms();
            }
            catch (Exception ex) { DbHelper.ShowError(ex); }
        }

        private void buttonDeleteClassroom_Click(object sender, EventArgs e)
        {
            DeleteSelected(gridClassrooms, "Classrooms", "classroom_id", LoadClassrooms);
        }

        // ── Classes ─────────────────────────────────────────────────────────

        private void LoadClasses()
        {
            gridClasses.DataSource = DbHelper.Query(
                "SELECT cl.class_id, CAST(cp.parallel AS NVARCHAR) + lc.letterClass AS [Класс] " +
                "FROM Classes cl " +
                "JOIN ClassParallel cp ON cl.id_parallel_class = cp.id_parallel_class " +
                "JOIN LetterOfTheClass lc ON cl.id_letter_class = lc.id_letter_class " +
                "ORDER BY cp.parallel, lc.letterClass");
            if (gridClasses.Columns.Contains("class_id")) gridClasses.Columns["class_id"].Visible = false;

            DataTable dtParallel = DbHelper.Query("SELECT id_parallel_class, parallel AS name FROM ClassParallel ORDER BY parallel");
            comboParallel.DataSource = dtParallel; comboParallel.DisplayMember = "name"; comboParallel.ValueMember = "id_parallel_class";

            DataTable dtLetter = DbHelper.Query("SELECT id_letter_class, letterClass AS name FROM LetterOfTheClass ORDER BY letterClass");
            comboLetter.DataSource = dtLetter; comboLetter.DisplayMember = "name"; comboLetter.ValueMember = "id_letter_class";
        }

        private void buttonAddClass_Click(object sender, EventArgs e)
        {
            if (comboParallel.SelectedValue == null || comboLetter.SelectedValue == null) return;
            try
            {
                DbHelper.Execute("INSERT INTO Classes (id_parallel_class, id_letter_class) VALUES (@p, @l)",
                    p => { p.AddWithValue("@p", comboParallel.SelectedValue); p.AddWithValue("@l", comboLetter.SelectedValue); });
                LoadClasses();
            }
            catch (Exception ex) { DbHelper.ShowError(ex); }
        }

        private void buttonDeleteClass_Click(object sender, EventArgs e)
        {
            DeleteSelected(gridClasses, "Classes", "class_id", LoadClasses);
        }

        // ── Shared delete helper ─────────────────────────────────────────────

        private void DeleteSelected(DataGridView grid, string table, string pkCol, Action reload)
        {
            if (grid.CurrentRow == null) return;
            if (!(grid.DataSource is DataTable dt)) return;
            DataRow row = dt.Rows[grid.CurrentRow.Index];
            int id = Convert.ToInt32(row[pkCol]);
            if (MessageBox.Show("Удалить выбранную запись?", "Подтверждение",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                DbHelper.Execute(string.Format("DELETE FROM [{0}] WHERE [{1}] = @id", table, pkCol),
                    p => p.AddWithValue("@id", id));
                reload();
            }
            catch (Exception ex) { DbHelper.ShowError(ex); }
        }
    }
}
