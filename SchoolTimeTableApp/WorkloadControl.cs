using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace testing
{
    public partial class WorkloadControl : UserControl
    {
        public WorkloadControl() { InitializeComponent(); }

        public void LoadData()
        {
            if (System.ComponentModel.LicenseManager.UsageMode ==
                System.ComponentModel.LicenseUsageMode.Designtime) return;
            LoadWorkloadGrid();
            LoadCombos();
        }

        private void LoadWorkloadGrid()
        {
            try
            {
                DataTable dt = DbHelper.Query(
                    "SELECT ID_нагрузки, Класс, Предмет, ФИО_учителя AS \"Учитель\", " +
                    "Тип_нагрузки AS \"Группа\", " +
                    "Количество_часов_в_неделю AS \"Часов/нед\", " +
                    "Поставлено_уроков AS \"Поставлено уроков\" " +
                    "FROM vw_Workload " +
                    "ORDER BY Параллель, Класс, Предмет");

                dataGrid.DataSource = dt;
                if (dataGrid.Columns.Contains("ID_нагрузки"))
                    dataGrid.Columns["ID_нагрузки"].Visible = false;
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка нагрузки"); }
        }

        private void LoadCombos()
        {
            try
            {
                // Используем CONVERT вместо CAST для совместимости с collation
                DataTable dtClass = DbHelper.Query(
                    "SELECT ID_класса AS class_id, " +
                    "CAST(pc.Параллель AS TEXT) || lc.Буква AS name " +
                    "FROM Classes cl " +
                    "JOIN ParallelClass pc ON cl.ID_параллели_класса = pc.ID_параллели_класса " +
                    "JOIN LetterClass lc   ON cl.ID_буквы_класса     = lc.ID_буквы_класса " +
                    "ORDER BY pc.Параллель, lc.Буква");
                comboClass.DisplayMember = "name";
                comboClass.ValueMember   = "class_id";
                comboClass.DataSource    = dtClass;

                // Упрощённый список: только название предмета + параллель
                DataTable dtSubject = DbHelper.Query(
                    "SELECT sbp.ID_предмета_со_сложностью AS subject_id, " +
                    "sub.Название || ' (' || CAST(pc.Параллель AS TEXT) || ' кл., сл.' || " +
                    "CAST(d.Сложность AS TEXT) || ')' AS name " +
                    "FROM SubjectByParallel sbp " +
                    "JOIN Subjects sub      ON sbp.ID_предмета  = sub.ID_предмета " +
                    "JOIN ParallelClass pc  ON sbp.ID_параллели = pc.ID_параллели_класса " +
                    "JOIN Difficulty d      ON sbp.ID_сложности = d.ID_сложности " +
                    "ORDER BY sub.Название, pc.Параллель");
                comboSubject.DisplayMember = "name";
                comboSubject.ValueMember   = "subject_id";
                comboSubject.DataSource    = dtSubject;

                DataTable dtTeacher = DbHelper.Query(
                    "SELECT ID_учителя AS teacher_id, " +
                    "Фамилия || ' ' || Имя || ' ' || Отчество AS name " +
                    "FROM Teachers ORDER BY Фамилия, Имя");
                comboTeacher.DisplayMember = "name";
                comboTeacher.ValueMember   = "teacher_id";
                comboTeacher.DataSource    = dtTeacher;

                // Подгруппа: 0=весь класс, 1=подгруппа 1, 2=подгруппа 2
                comboSubgroup.Items.Clear();
                comboSubgroup.Items.Add("Весь класс");
                comboSubgroup.Items.Add("Подгруппа 1");
                comboSubgroup.Items.Add("Подгруппа 2");
                comboSubgroup.SelectedIndex = 0;
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка справочников"); }
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            if (comboClass.SelectedValue == null ||
                comboSubject.SelectedValue == null ||
                comboTeacher.SelectedValue == null)
            {
                MessageBox.Show("Заполните все поля.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!int.TryParse(textHours.Text, out int hours) || hours <= 0)
            {
                MessageBox.Show("Укажите корректное количество часов.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 0=весь класс → null, 1=П1 → 1, 2=П2 → 2
            object subgroupParam = comboSubgroup.SelectedIndex <= 0
                ? (object)System.DBNull.Value
                : (object)comboSubgroup.SelectedIndex;

            try
            {
                DbHelper.ExecProcNonQuery("sp_AddWorkload",
                    p => {
                        p.AddWithValue("@ID_учителя",               comboTeacher.SelectedValue);
                        p.AddWithValue("@ID_класса",                comboClass.SelectedValue);
                        p.AddWithValue("@ID_предмета_параллели",    comboSubject.SelectedValue);
                        p.AddWithValue("@Количество_часов_в_неделю", (byte)hours);
                        p.AddWithValue("@Подгруппа",                subgroupParam);
                    });
                textHours.Clear();
                comboSubgroup.SelectedIndex = 0;
                LoadWorkloadGrid();
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Добавление нагрузки"); }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (dataGrid.CurrentRow == null) return;
            if (!(dataGrid.DataSource is DataTable dt)) return;

            DataRow row = dt.Rows[dataGrid.CurrentRow.Index];
            int id = Convert.ToInt32(row["ID_нагрузки"]);

            if (MessageBox.Show(
                "Удалить запись о нагрузке?\nСвязанные уроки в расписании тоже будут удалены.",
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Warning)
                != DialogResult.Yes) return;

            try
            {
                // Хранимая процедура атомарно удаляет нагрузку + уроки
                DbHelper.ExecProcNonQuery("sp_DeleteWorkload",
                    p => p.AddWithValue("@ID_нагрузки", id));
                LoadWorkloadGrid();
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Удаление нагрузки"); }
        }

        private void textSearch_TextChanged(object sender, EventArgs e)
        {
            string query = textSearch.Text.Trim();
            bool hasQuery = !string.IsNullOrEmpty(query);
            int found = 0;

            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                bool match = false;
                if (hasQuery)
                    foreach (DataGridViewCell cell in row.Cells)
                        if ((cell.Value?.ToString() ?? "")
                            .IndexOf(query, StringComparison.OrdinalIgnoreCase) >= 0)
                        { match = true; break; }

                if (!hasQuery)
                {
                    row.DefaultCellStyle.BackColor = Color.Empty;
                    row.DefaultCellStyle.ForeColor = Color.Empty;
                }
                else if (match)
                {
                    row.DefaultCellStyle.BackColor = Color.Gold;
                    row.DefaultCellStyle.ForeColor = Color.Black;
                    found++;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.Empty;
                    row.DefaultCellStyle.ForeColor = Color.LightGray;
                }
            }

            labelSearchHint.Text = hasQuery
                ? (found > 0 ? string.Format("Найдено: {0}", found) : "Не найдено") : "";
            labelSearchHint.ForeColor = found > 0 ? Color.SeaGreen : Color.Crimson;
        }
    }
}
