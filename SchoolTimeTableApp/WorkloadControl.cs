using System;
using System.Data;
using System.Windows.Forms;

namespace testing
{
    public partial class WorkloadControl : UserControl
    {
        public WorkloadControl() { InitializeComponent(); }

        public void LoadData()
        {
            if (System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime)
                return;
            LoadWorkloadGrid();
            LoadCombos();
        }

        private void LoadWorkloadGrid()
        {
            try
            {
                DataTable dt = DbHelper.Query(
                    "SELECT w.workload_id, " +
                    "CAST(cp.parallel AS NVARCHAR) + lc.letterClass AS [Класс], " +
                    "sub.subject_name AS [Предмет], " +
                    "t.name AS [Учитель], " +
                    "w.hours_per_week AS [Часов/нед], " +
                    "(SELECT COUNT(*) FROM Schedule s WHERE s.workload_id = w.workload_id) AS [Поставлено уроков] " +
                    "FROM Workload w " +
                    "JOIN Classes cl ON w.class_id = cl.class_id " +
                    "JOIN ClassParallel cp ON cl.id_parallel_class = cp.id_parallel_class " +
                    "JOIN LetterOfTheClass lc ON cl.id_letter_class = lc.id_letter_class " +
                    "JOIN Subjects sub ON w.subject_id = sub.subject_id " +
                    "JOIN Teachers t ON w.teacher_id = t.teacher_id " +
                    "ORDER BY cp.parallel, lc.letterClass, sub.subject_name");

                dataGrid.DataSource = dt;
                if (dataGrid.Columns.Contains("workload_id"))
                    dataGrid.Columns["workload_id"].Visible = false;
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка нагрузки"); }
        }

        private void LoadCombos()
        {
            try
            {
                DataTable dtClass = DbHelper.Query(
                    "SELECT cl.class_id, CAST(cp.parallel AS NVARCHAR) + lc.letterClass AS name " +
                    "FROM Classes cl " +
                    "JOIN ClassParallel cp ON cl.id_parallel_class = cp.id_parallel_class " +
                    "JOIN LetterOfTheClass lc ON cl.id_letter_class = lc.id_letter_class " +
                    "ORDER BY cp.parallel, lc.letterClass");
                comboClass.DataSource = dtClass; comboClass.DisplayMember = "name"; comboClass.ValueMember = "class_id";

                DataTable dtSubject = DbHelper.Query("SELECT subject_id, subject_name AS name FROM Subjects ORDER BY subject_name");
                comboSubject.DataSource = dtSubject; comboSubject.DisplayMember = "name"; comboSubject.ValueMember = "subject_id";

                DataTable dtTeacher = DbHelper.Query("SELECT teacher_id, name FROM Teachers ORDER BY name");
                comboTeacher.DataSource = dtTeacher; comboTeacher.DisplayMember = "name"; comboTeacher.ValueMember = "teacher_id";
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка справочников"); }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboClass.SelectedValue == null || comboSubject.SelectedValue == null || comboTeacher.SelectedValue == null)
            {
                MessageBox.Show("Заполните все поля.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(textHours.Text, out int hours) || hours <= 0)
            {
                MessageBox.Show("Укажите корректное количество часов.", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                DbHelper.Execute(
                    "INSERT INTO Workload (class_id, subject_id, teacher_id, hours_per_week) VALUES (@c, @s, @t, @h)",
                    p => { p.AddWithValue("@c", comboClass.SelectedValue); p.AddWithValue("@s", comboSubject.SelectedValue);
                           p.AddWithValue("@t", comboTeacher.SelectedValue); p.AddWithValue("@h", hours); });
                textHours.Clear();
                LoadWorkloadGrid();
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Добавление нагрузки"); }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid.CurrentRow == null) return;
            if (!(dataGrid.DataSource is DataTable dt)) return;
            DataRow row = dt.Rows[dataGrid.CurrentRow.Index];
            int id = Convert.ToInt32(row["workload_id"]);

            if (MessageBox.Show("Удалить запись о нагрузке? Связанные уроки в расписании тоже будут удалены.",
                    "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes) return;
            try
            {
                DbHelper.Execute("DELETE FROM Schedule WHERE workload_id = @id", p => p.AddWithValue("@id", id));
                DbHelper.Execute("DELETE FROM Workload WHERE workload_id = @id",  p => p.AddWithValue("@id", id));
                LoadWorkloadGrid();
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Удаление нагрузки"); }
        }
    }
}
