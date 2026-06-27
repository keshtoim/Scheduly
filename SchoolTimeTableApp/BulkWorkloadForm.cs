using System;
using System.Data;
using System.Windows.Forms;

namespace testing
{
    public partial class BulkWorkloadForm : Form
    {
        private DataTable _classesTable;
        private DataTable _subjectsAll;   // все предметы из SubjectByParallel

        public BulkWorkloadForm()
        {
            InitializeComponent();
        }

        private void BulkWorkloadForm_Load(object sender, EventArgs e)
        {
            LoadTeachers();
            LoadSubjects();   // заполняет _subjectsAll, привязывает comboSubject
            LoadClasses();    // заполняет _classesTable, checkedListClasses

            comboSubgroup.Items.AddRange(new[] { "Весь класс", "Подгруппа 1", "Подгруппа 2" });
            comboSubgroup.SelectedIndex = 0;
            textHours.Text = "2";

            // Параллель — первый выбор
            comboGrade.Items.Add("Все");
            for (int g = 1; g <= 11; g++)
                comboGrade.Items.Add(g.ToString());
            comboGrade.SelectedIndex = 0;
        }

        private void LoadTeachers()
        {
            DataTable dt = DbHelper.Query(
                "SELECT ID_учителя AS teacher_id, " +
                "Фамилия || ' ' || Имя || ' ' || Отчество AS name " +
                "FROM Teachers ORDER BY Фамилия, Имя");
            comboTeacher.DisplayMember = "name";
            comboTeacher.ValueMember   = "teacher_id";
            comboTeacher.DataSource    = dt;
        }

        private void LoadSubjects()
        {
            _subjectsAll = DbHelper.Query(
                "SELECT sbp.ID_предмета_со_сложностью AS subject_id, " +
                "       pc.Параллель                  AS grade, " +
                "       sub.Название || ' (сл.' || CAST(d.Сложность AS TEXT) || ')' AS name " +
                "FROM SubjectByParallel sbp " +
                "JOIN Subjects      sub ON sbp.ID_предмета  = sub.ID_предмета " +
                "JOIN ParallelClass pc  ON sbp.ID_параллели = pc.ID_параллели_класса " +
                "JOIN Difficulty    d   ON sbp.ID_сложности = d.ID_сложности " +
                "ORDER BY sub.Название, pc.Параллель");
            comboSubject.DisplayMember = "name";
            comboSubject.ValueMember   = "subject_id";
            comboSubject.DataSource    = _subjectsAll;
        }

        private void LoadClasses()
        {
            _classesTable = DbHelper.Query(
                "SELECT cl.ID_класса AS class_id, " +
                "       pc.Параллель, " +
                "       CAST(pc.Параллель AS TEXT) || lc.Буква AS class_name " +
                "FROM Classes cl " +
                "JOIN ParallelClass pc ON cl.ID_параллели_класса = pc.ID_параллели_класса " +
                "JOIN LetterClass   lc ON cl.ID_буквы_класса     = lc.ID_буквы_класса " +
                "ORDER BY pc.Параллель, lc.Буква");

            checkedListClasses.Items.Clear();
            foreach (DataRow row in _classesTable.Rows)
                checkedListClasses.Items.Add(row["class_name"].ToString(), false);
        }

        // ─── Главный триггер: выбор параллели ─────────────────────────────
        // Автоматически: фильтрует список предметов и отмечает классы параллели
        private void comboGrade_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboGrade.SelectedIndex <= 0)
            {
                // «Все» — показываем все предметы, не трогаем классы
                comboSubject.DataSource = _subjectsAll;
                return;
            }
            int grade = comboGrade.SelectedIndex; // индекс 1..11 = параллель 1..11

            // Фильтруем предметы только для выбранной параллели
            DataTable filtered = _subjectsAll.Clone();
            foreach (DataRow row in _subjectsAll.Rows)
                if (Convert.ToInt32(row["grade"]) == grade)
                    filtered.ImportRow(row);
            comboSubject.DataSource = filtered;

            // Автоматически отмечаем все классы этой параллели
            for (int i = 0; i < checkedListClasses.Items.Count; i++)
                checkedListClasses.SetItemChecked(i,
                    Convert.ToInt32(_classesTable.Rows[i]["Параллель"]) == grade);
        }

        private void buttonSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListClasses.Items.Count; i++)
                checkedListClasses.SetItemChecked(i, true);
        }

        private void buttonClearAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < checkedListClasses.Items.Count; i++)
                checkedListClasses.SetItemChecked(i, false);
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            labelStatus.ForeColor = System.Drawing.Color.Gray;
            labelStatus.Text = "";

            if (comboTeacher.SelectedValue == null || comboSubject.SelectedValue == null)
            { labelStatus.ForeColor = System.Drawing.Color.Crimson; labelStatus.Text = "Выберите учителя и предмет."; return; }
            if (!int.TryParse(textHours.Text.Trim(), out int hours) || hours <= 0)
            { labelStatus.ForeColor = System.Drawing.Color.Crimson; labelStatus.Text = "Укажите часов в неделю (целое > 0)."; return; }
            if (checkedListClasses.CheckedIndices.Count == 0)
            { labelStatus.ForeColor = System.Drawing.Color.Crimson; labelStatus.Text = "Отметьте хотя бы один класс."; return; }

            int teacherId  = Convert.ToInt32(comboTeacher.SelectedValue);
            int subjectId  = Convert.ToInt32(comboSubject.SelectedValue);
            object sgParam = comboSubgroup.SelectedIndex <= 0
                ? (object)System.DBNull.Value
                : (object)comboSubgroup.SelectedIndex;

            int added = 0, skipped = 0;
            foreach (int idx in checkedListClasses.CheckedIndices)
            {
                int classId = Convert.ToInt32(_classesTable.Rows[idx]["class_id"]);
                try
                {
                    DbHelper.ExecProcNonQuery("sp_AddWorkload", p =>
                    {
                        p.AddWithValue("@ID_учителя",                teacherId);
                        p.AddWithValue("@ID_класса",                 classId);
                        p.AddWithValue("@ID_предмета_параллели",     subjectId);
                        p.AddWithValue("@Количество_часов_в_неделю", (byte)hours);
                        p.AddWithValue("@Подгруппа",                 sgParam);
                    });
                    added++;
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("существует")) skipped++;
                    else { DbHelper.ShowError(ex, "sp_AddWorkload"); break; }
                }
            }

            string msg = $"Добавлено: {added}.";
            if (skipped > 0) msg += $" Пропущено (дубли): {skipped}.";
            labelStatus.ForeColor = added > 0 ? System.Drawing.Color.SeaGreen : System.Drawing.Color.Crimson;
            labelStatus.Text = msg;
        }

        private void buttonClose_Click(object sender, EventArgs e) => Close();
    }
}
