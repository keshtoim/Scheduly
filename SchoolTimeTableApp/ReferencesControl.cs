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
            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadTeachers();
            LoadSubjects();
            LoadSubjectParallel();
            LoadClassrooms();
            LoadClasses();
        }

        // ── Учителя ────────────────────────────────────────────────────────
        private void LoadTeachers()
        {
            gridTeachers.DataSource = DbHelper.Query(
                "SELECT ID_учителя, Фамилия, Имя, Отчество, Ставка AS \"Часов\" " +
                "FROM Teachers ORDER BY Фамилия, Имя");
            if (gridTeachers.Columns.Contains("ID_учителя"))
                gridTeachers.Columns["ID_учителя"].Visible = false;
        }

        private void buttonAddTeacher_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTeacherName.Text)) return;
            decimal hours = 0; decimal.TryParse(txtTeacherHours.Text, out hours);
            string[] parts    = txtTeacherName.Text.Trim().Split(' ');
            string surname    = parts.Length > 0 ? parts[0] : txtTeacherName.Text.Trim();
            string firstName  = parts.Length > 1 ? parts[1] : "";
            string patronymic = parts.Length > 2 ? parts[2] : "";
            try
            {
                DbHelper.ExecProcNonQuery("sp_AddTeacher",
                    p => { p.AddWithValue("@Фамилия",  surname);
                           p.AddWithValue("@Имя",      firstName);
                           p.AddWithValue("@Отчество", patronymic);
                           p.AddWithValue("@Ставка",   hours > 0 ? hours : 1m); });
                txtTeacherName.Clear(); txtTeacherHours.Clear();
                LoadTeachers();
            }
            catch (Exception ex) { DbHelper.ShowError(ex); }
        }

        private void buttonDeleteTeacher_Click(object sender, EventArgs e)
        {
            DeleteSelected(gridTeachers, "Teachers", "ID_учителя", LoadTeachers);
        }

        private void buttonEditTeacher_Click(object sender, EventArgs e)
        {
            if (gridTeachers.CurrentRow == null) return;
            var dt = gridTeachers.DataSource as DataTable;
            if (dt == null) return;
            var row = dt.Rows[gridTeachers.CurrentRow.Index];
            int id = Convert.ToInt32(row["ID_учителя"]);
            using (var dlg = new QuickAddForm("Изменить учителя",
                new[] { "Фамилия", "Имя", "Отчество", "Часов в неделю" },
                new[] { row["Фамилия"].ToString(), row["Имя"].ToString(),
                        row["Отчество"].ToString(), row["Часов"].ToString() }))
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                decimal hours = 0; decimal.TryParse(dlg.Values[3], out hours);
                try
                {
                    DbHelper.Execute(
                        "UPDATE Teachers SET Фамилия=@f, Имя=@n, Отчество=@p, Ставка=@h WHERE ID_учителя=@id",
                        p => { p.AddWithValue("@f", dlg.Values[0].Trim());
                               p.AddWithValue("@n", dlg.Values[1].Trim());
                               p.AddWithValue("@p", dlg.Values[2].Trim());
                               p.AddWithValue("@h", hours > 0 ? hours : 1m);
                               p.AddWithValue("@id", id); });
                    LoadTeachers();
                }
                catch (Exception ex) { DbHelper.ShowError(ex); }
            }
        }

        // ── Предметы ────────────────────────────────────────────────────────
        private void LoadSubjects()
        {
            gridSubjects.DataSource = DbHelper.Query(
                "SELECT ID_предмета, Название AS \"Предмет\" " +
                "FROM Subjects ORDER BY Название");
            if (gridSubjects.Columns.Contains("ID_предмета"))
                gridSubjects.Columns["ID_предмета"].Visible = false;
        }

        private void buttonAddSubject_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSubjectName.Text)) return;
            try
            {
                DbHelper.Execute(
                    "INSERT INTO Subjects (Название) VALUES (@n)",
                    p => p.AddWithValue("@n", txtSubjectName.Text.Trim()));
                txtSubjectName.Clear();
                LoadSubjects();
            }
            catch (Exception ex) { DbHelper.ShowError(ex); }
        }

        private void buttonDeleteSubject_Click(object sender, EventArgs e)
        {
            DeleteSelected(gridSubjects, "Subjects", "ID_предмета", LoadSubjects);
        }

        private void buttonEditSubject_Click(object sender, EventArgs e)
        {
            if (gridSubjects.CurrentRow == null) return;
            var dt = gridSubjects.DataSource as DataTable;
            if (dt == null) return;
            var row = dt.Rows[gridSubjects.CurrentRow.Index];
            int id = Convert.ToInt32(row["ID_предмета"]);
            using (var dlg = new QuickAddForm("Изменить предмет",
                new[] { "Название" },
                new[] { row["Предмет"].ToString() }))
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                string name = dlg.Values[0].Trim();
                if (string.IsNullOrEmpty(name)) return;
                try
                {
                    DbHelper.Execute(
                        "UPDATE Subjects SET Название=@n WHERE ID_предмета=@id",
                        p => { p.AddWithValue("@n", name); p.AddWithValue("@id", id); });
                    LoadSubjects();
                }
                catch (Exception ex) { DbHelper.ShowError(ex); }
            }
        }

        // ── Предметы по параллелям ─────────────────────────────────────────
        private void LoadSubjectParallel()
        {
            gridSubjPar.DataSource = DbHelper.Query(
                "SELECT sbp.ID_предмета_со_сложностью, " +
                "       pc.Параллель, " +
                "       sub.Название AS \"Предмет\", " +
                "       d.Сложность " +
                "FROM SubjectByParallel sbp " +
                "JOIN Subjects      sub ON sbp.ID_предмета  = sub.ID_предмета " +
                "JOIN ParallelClass pc  ON sbp.ID_параллели = pc.ID_параллели_класса " +
                "JOIN Difficulty    d   ON sbp.ID_сложности = d.ID_сложности " +
                "ORDER BY pc.Параллель, sub.Название");
            if (gridSubjPar.Columns.Contains("ID_предмета_со_сложностью"))
                gridSubjPar.Columns["ID_предмета_со_сложностью"].Visible = false;

            DataTable dtGrades = DbHelper.Query(
                "SELECT ID_параллели_класса, Параллель AS name FROM ParallelClass ORDER BY Параллель");
            comboSubjParGrade.DisplayMember = "name";
            comboSubjParGrade.ValueMember   = "ID_параллели_класса";
            comboSubjParGrade.DataSource    = dtGrades;

            DataTable dtSub = DbHelper.Query(
                "SELECT ID_предмета, Название AS name FROM Subjects ORDER BY Название");
            comboSubjParSubject.DisplayMember = "name";
            comboSubjParSubject.ValueMember   = "ID_предмета";
            comboSubjParSubject.DataSource    = dtSub;
        }

        private void buttonAddSubjPar_Click(object sender, EventArgs e)
        {
            if (comboSubjParGrade.SelectedValue == null ||
                comboSubjParSubject.SelectedValue == null) return;
            if (!decimal.TryParse(txtSubjParDiff.Text.Trim(), out decimal diffValue) || diffValue <= 0)
            {
                MessageBox.Show("Укажите корректное значение сложности (число > 0).",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                object diffIdObj = DbHelper.Scalar(
                    "SELECT ID_сложности FROM Difficulty WHERE Сложность = @d",
                    p => p.AddWithValue("@d", diffValue));
                int diffId;
                if (diffIdObj == null || diffIdObj == DBNull.Value)
                {
                    DbHelper.Execute("INSERT INTO Difficulty (Сложность) VALUES (@d)",
                        p => p.AddWithValue("@d", diffValue));
                    diffId = Convert.ToInt32(DbHelper.Scalar("SELECT last_insert_rowid()"));
                }
                else diffId = Convert.ToInt32(diffIdObj);

                DbHelper.Execute(
                    "INSERT INTO SubjectByParallel (ID_предмета, ID_параллели, ID_сложности) " +
                    "VALUES (@s, @p, @d)",
                    p => {
                        p.AddWithValue("@s", comboSubjParSubject.SelectedValue);
                        p.AddWithValue("@p", comboSubjParGrade.SelectedValue);
                        p.AddWithValue("@d", diffId);
                    });
                txtSubjParDiff.Clear();
                LoadSubjectParallel();
            }
            catch (Exception ex) { DbHelper.ShowError(ex); }
        }

        private void buttonDeleteSubjPar_Click(object sender, EventArgs e)
        {
            DeleteSelected(gridSubjPar, "SubjectByParallel",
                "ID_предмета_со_сложностью", LoadSubjectParallel);
        }

        private void gridSubjPar_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            var dt = gridSubjPar.DataSource as DataTable;
            if (dt == null) return;
            var row = dt.Rows[e.RowIndex];
            // Pre-fill controls from the selected row
            int parallelId = Convert.ToInt32(DbHelper.Scalar(
                "SELECT ID_параллели_класса FROM ParallelClass WHERE Параллель = @p",
                p => p.AddWithValue("@p", row["Параллель"])));
            int subjectId = Convert.ToInt32(DbHelper.Scalar(
                "SELECT ID_предмета FROM Subjects WHERE Название = @n",
                p => p.AddWithValue("@n", row["Предмет"].ToString())));
            comboSubjParGrade.SelectedValue   = parallelId;
            comboSubjParSubject.SelectedValue = subjectId;
            txtSubjParDiff.Text = row["Сложность"].ToString();
        }

        private void txtSubjParDiff_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            { e.SuppressKeyPress = true; buttonAddSubjPar_Click(null, EventArgs.Empty); }
        }

        // ── Кабинеты ────────────────────────────────────────────────────────
        private void LoadClassrooms()
        {
            gridClassrooms.DataSource = DbHelper.Query(
                "SELECT cr.ID_кабинета, cr.Номер AS \"Кабинет\", " +
                "cr.Вместимость, ct.Тип_кабинета AS \"Тип\" " +
                "FROM Classrooms cr " +
                "JOIN ClassroomTypes ct ON cr.ID_типа_кабинета = ct.ID_типа_кабинета " +
                "ORDER BY cr.Номер");
            if (gridClassrooms.Columns.Contains("ID_кабинета"))
                gridClassrooms.Columns["ID_кабинета"].Visible = false;

            DataTable dtTypes = DbHelper.Query(
                "SELECT ID_типа_кабинета, Тип_кабинета AS name " +
                "FROM ClassroomTypes ORDER BY Тип_кабинета");
            comboClassroomType.DisplayMember = "name";
            comboClassroomType.ValueMember   = "ID_типа_кабинета";
            comboClassroomType.DataSource    = dtTypes;
        }

        private void buttonAddClassroom_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtRoomNumber.Text) ||
                comboClassroomType.SelectedValue == null) return;
            int.TryParse(txtRoomCapacity.Text, out int cap);
            try
            {
                DbHelper.ExecProcNonQuery("sp_AddClassroom",
                    p => {
                        p.AddWithValue("@Номер",            int.Parse(txtRoomNumber.Text));
                        p.AddWithValue("@Вместимость",      cap > 0 ? cap : 30);
                        p.AddWithValue("@ID_типа_кабинета", comboClassroomType.SelectedValue);
                    });
                txtRoomNumber.Clear(); txtRoomCapacity.Clear();
                LoadClassrooms();
            }
            catch (Exception ex) { DbHelper.ShowError(ex); }
        }

        private void buttonDeleteClassroom_Click(object sender, EventArgs e)
        {
            DeleteSelected(gridClassrooms, "Classrooms", "ID_кабинета", LoadClassrooms);
        }

        private void buttonEditClassroom_Click(object sender, EventArgs e)
        {
            if (gridClassrooms.CurrentRow == null) return;
            var dt = gridClassrooms.DataSource as DataTable;
            if (dt == null) return;
            var row = dt.Rows[gridClassrooms.CurrentRow.Index];
            int id = Convert.ToInt32(row["ID_кабинета"]);
            DataTable dtTypes = DbHelper.Query(
                "SELECT ID_типа_кабинета, Тип_кабинета AS name FROM ClassroomTypes ORDER BY Тип_кабинета");
            using (var dlg = new QuickAddClassroomForm(dtTypes,
                Convert.ToInt32(row["Кабинет"]),
                Convert.ToInt32(row["Вместимость"])))
            {
                if (dlg.ShowDialog(this) != DialogResult.OK) return;
                try
                {
                    DbHelper.Execute(
                        "UPDATE Classrooms SET Номер=@n, Вместимость=@c, ID_типа_кабинета=@t WHERE ID_кабинета=@id",
                        p => { p.AddWithValue("@n", dlg.RoomNumber);
                               p.AddWithValue("@c", dlg.Capacity > 0 ? dlg.Capacity : 30);
                               p.AddWithValue("@t", dlg.TypeId);
                               p.AddWithValue("@id", id); });
                    LoadClassrooms();
                }
                catch (Exception ex) { DbHelper.ShowError(ex); }
            }
        }

        // ── Классы ──────────────────────────────────────────────────────────
        private void LoadClasses()
        {
            gridClasses.DataSource = DbHelper.Query(
                "SELECT cl.ID_класса, " +
                "CAST(pc.Параллель AS TEXT) || lc.Буква AS \"Класс\" " +
                "FROM Classes cl " +
                "JOIN ParallelClass pc ON cl.ID_параллели_класса = pc.ID_параллели_класса " +
                "JOIN LetterClass lc   ON cl.ID_буквы_класса     = lc.ID_буквы_класса " +
                "ORDER BY pc.Параллель, lc.Буква");
            if (gridClasses.Columns.Contains("ID_класса"))
                gridClasses.Columns["ID_класса"].Visible = false;

            DataTable dtP = DbHelper.Query(
                "SELECT ID_параллели_класса, Параллель AS name FROM ParallelClass ORDER BY Параллель");
            comboParallel.DisplayMember = "name";
            comboParallel.ValueMember   = "ID_параллели_класса";
            comboParallel.DataSource    = dtP;

            DataTable dtL = DbHelper.Query(
                "SELECT ID_буквы_класса, Буква AS name FROM LetterClass ORDER BY Буква");
            comboLetter.DisplayMember = "name";
            comboLetter.ValueMember   = "ID_буквы_класса";
            comboLetter.DataSource    = dtL;
        }

        private void buttonAddClass_Click(object sender, EventArgs e)
        {
            if (comboParallel.SelectedValue == null || comboLetter.SelectedValue == null) return;
            try
            {
                DbHelper.Execute(
                    "INSERT INTO Classes (ID_параллели_класса, ID_буквы_класса) VALUES (@p, @l)",
                    p => { p.AddWithValue("@p", comboParallel.SelectedValue);
                           p.AddWithValue("@l", comboLetter.SelectedValue); });
                LoadClasses();
            }
            catch (Exception ex) { DbHelper.ShowError(ex); }
        }

        private void buttonAddLetter_Click(object sender, EventArgs e)
        {
            string letter = txtNewLetter.Text.Trim().ToUpper();
            if (string.IsNullOrEmpty(letter) || letter.Length != 1)
            {
                MessageBox.Show("Введите одну заглавную букву кириллицы.",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Проверяем что это кириллица
            if (letter[0] < 'А' || letter[0] > 'Я')
            {
                MessageBox.Show("Допустимы только заглавные буквы кириллицы (А-Я).",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                // Проверяем что такая буква ещё не существует
                bool exists = DbHelper.Exists(
                    "SELECT COUNT(*) FROM LetterClass WHERE Буква = @b",
                    p => p.AddWithValue("@b", letter));
                if (exists)
                {
                    MessageBox.Show(string.Format("Буква «{0}» уже есть в справочнике.", letter),
                        "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                DbHelper.Execute(
                    "INSERT INTO LetterClass (Буква) VALUES (@b)",
                    p => p.AddWithValue("@b", letter));
                txtNewLetter.Clear();
                LoadClasses(); // обновит и comboLetter
                MessageBox.Show(string.Format("Буква «{0}» добавлена.", letter),
                    "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Добавление буквы"); }
        }

        private void buttonDeleteClass_Click(object sender, EventArgs e)
        {
            DeleteSelected(gridClasses, "Classes", "ID_класса", LoadClasses);
        }

        private void buttonEditClass_Click(object sender, EventArgs e)
        {
            if (gridClasses.CurrentRow == null) return;
            var dt = gridClasses.DataSource as DataTable;
            if (dt == null) return;
            var row = dt.Rows[gridClasses.CurrentRow.Index];
            int id = Convert.ToInt32(row["ID_класса"]);
            // Меняем параллель и/или букву через те же комбобоксы
            if (comboParallel.SelectedValue == null || comboLetter.SelectedValue == null)
            {
                MessageBox.Show("Выберите новую параллель и букву в полях выше.",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show(
                string.Format("Изменить класс «{0}» на «{1}{2}»?",
                    row["Класс"],
                    comboParallel.Text, comboLetter.Text),
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;
            try
            {
                DbHelper.Execute(
                    "UPDATE Classes SET ID_параллели_класса=@p, ID_буквы_класса=@l WHERE ID_класса=@id",
                    p => { p.AddWithValue("@p", comboParallel.SelectedValue);
                           p.AddWithValue("@l", comboLetter.SelectedValue);
                           p.AddWithValue("@id", id); });
                LoadClasses();
            }
            catch (Exception ex) { DbHelper.ShowError(ex); }
        }

        // ── Enter-клавиша и двойной клик ───────────────────────────────────
        private void txtTeacherName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            { e.SuppressKeyPress = true; buttonAddTeacher_Click(null, EventArgs.Empty); }
        }

        private void gridTeachers_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) buttonEditTeacher_Click(null, EventArgs.Empty);
        }

        private void txtSubjectName_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            { e.SuppressKeyPress = true; buttonAddSubject_Click(null, EventArgs.Empty); }
        }

        private void gridSubjects_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) buttonEditSubject_Click(null, EventArgs.Empty);
        }

        private void txtRoomNumber_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            { e.SuppressKeyPress = true; buttonAddClassroom_Click(null, EventArgs.Empty); }
        }

        private void gridClassrooms_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) buttonEditClassroom_Click(null, EventArgs.Empty);
        }

        private void txtNewLetter_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            { e.SuppressKeyPress = true; buttonAddLetter_Click(null, EventArgs.Empty); }
        }

        private void gridClasses_CellDoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) buttonEditClass_Click(null, EventArgs.Empty);
        }

        // ── Универсальное удаление ──────────────────────────────────────────
        private void DeleteSelected(DataGridView grid, string table, string pk, Action reload)
        {
            if (grid.CurrentRow == null) return;
            if (!(grid.DataSource is DataTable dt)) return;
            DataRow row = dt.Rows[grid.CurrentRow.Index];
            int id = Convert.ToInt32(row[pk]);
            if (MessageBox.Show("Удалить выбранную запись?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            try
            {
                DbHelper.Execute(
                    string.Format("DELETE FROM [{0}] WHERE [{1}] = @id", table, pk),
                    p => p.AddWithValue("@id", id));
                reload();
            }
            catch (Exception ex) { DbHelper.ShowError(ex); }
        }
    }
}
