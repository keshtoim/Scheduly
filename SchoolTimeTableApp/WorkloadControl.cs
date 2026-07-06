using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace testing
{
    public partial class WorkloadControl : UserControl
    {
        // Full subject table: subject_id, grade, name_short, name_full
        private DataTable _subjectsAll;
        // Full class table: class_id, grade, name
        private DataTable _classTable;
        // Suppresses auto-logic during programmatic DataSource changes
        private bool _suppressAutoLogic;

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
                _suppressAutoLogic = true;

                // ── Классы (с информацией о параллели для авто-фильтра предметов) ──
                _classTable = DbHelper.Query(
                    "SELECT cl.ID_класса AS class_id, " +
                    "       pc.Параллель AS grade, " +
                    "       CAST(pc.Параллель AS TEXT) || lc.Буква AS name " +
                    "FROM Classes cl " +
                    "JOIN ParallelClass pc ON cl.ID_параллели_класса = pc.ID_параллели_класса " +
                    "JOIN LetterClass   lc ON cl.ID_буквы_класса     = lc.ID_буквы_класса " +
                    "ORDER BY pc.Параллель, lc.Буква");
                comboClass.DisplayMember = "name";
                comboClass.ValueMember   = "class_id";
                comboClass.DataSource    = _classTable;

                // ── Предметы (две версии названия: короткая и с параллелью) ──
                _subjectsAll = DbHelper.Query(
                    "SELECT sbp.ID_предмета_со_сложностью AS subject_id, " +
                    "       pc.Параллель AS grade, " +
                    "       sub.Название || ' (сл.' || CAST(d.Сложность AS TEXT) || ')' AS name_short, " +
                    "       sub.Название || ' [' || CAST(pc.Параллель AS TEXT) || ' кл.] (сл.' || CAST(d.Сложность AS TEXT) || ')' AS name_full " +
                    "FROM SubjectByParallel sbp " +
                    "JOIN Subjects      sub ON sbp.ID_предмета  = sub.ID_предмета " +
                    "JOIN ParallelClass pc  ON sbp.ID_параллели = pc.ID_параллели_класса " +
                    "JOIN Difficulty    d   ON sbp.ID_сложности = d.ID_сложности " +
                    "ORDER BY sub.Название, pc.Параллель");
                comboSubject.DisplayMember = "name_full";
                comboSubject.ValueMember   = "subject_id";
                comboSubject.DataSource    = _subjectsAll;

                // ── Учителя ──
                DataTable dtTeacher = DbHelper.Query(
                    "SELECT ID_учителя AS teacher_id, " +
                    "Фамилия || ' ' || Имя || ' ' || Отчество AS name " +
                    "FROM Teachers ORDER BY Фамилия, Имя");
                comboTeacher.DisplayMember = "name";
                comboTeacher.ValueMember   = "teacher_id";
                comboTeacher.DataSource    = dtTeacher;

                comboSubgroup.Items.Clear();
                comboSubgroup.Items.Add("Весь класс");
                comboSubgroup.Items.Add("Подгруппа 1");
                comboSubgroup.Items.Add("Подгруппа 2");
                comboSubgroup.SelectedIndex = 0;

                if (string.IsNullOrEmpty(textHours.Text))
                    textHours.Text = "2";

                _suppressAutoLogic = false;
            }
            catch (Exception ex)
            {
                _suppressAutoLogic = false;
                DbHelper.ShowError(ex, "Загрузка справочников");
            }
        }

        // ── Авто-фильтр предметов при смене класса ────────────────────────
        private void comboClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressAutoLogic || _subjectsAll == null || _classTable == null) return;
            if (comboClass.SelectedValue == null)
            {
                SetSubjectsDataSource(_subjectsAll, "name_full");
                return;
            }

            int classId = Convert.ToInt32(comboClass.SelectedValue);
            DataRow[] classRows = _classTable.Select("class_id = " + classId);
            if (classRows.Length == 0) return;
            int grade = Convert.ToInt32(classRows[0]["grade"]);

            DataTable filtered = _subjectsAll.Clone();
            foreach (DataRow row in _subjectsAll.Rows)
                if (Convert.ToInt32(row["grade"]) == grade)
                    filtered.ImportRow(row);

            SetSubjectsDataSource(
                filtered.Rows.Count > 0 ? filtered : _subjectsAll,
                filtered.Rows.Count > 0 ? "name_short" : "name_full");
        }

        private void SetSubjectsDataSource(DataTable dt, string displayMember)
        {
            _suppressAutoLogic = true;
            comboSubject.DataSource    = dt;
            comboSubject.DisplayMember = displayMember;
            comboSubject.ValueMember   = "subject_id";
            _suppressAutoLogic = false;
        }

        // ── Авто-подстановка учителя при смене предмета ───────────────────
        private void comboSubject_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_suppressAutoLogic || comboSubject.SelectedValue == null) return;
            int subjectId = Convert.ToInt32(comboSubject.SelectedValue);
            try
            {
                // Чаще всего используемый учитель для этого предмета
                object teacherIdObj = DbHelper.Scalar(
                    "SELECT ID_учителя FROM Workload " +
                    "WHERE ID_предмета_параллели = @s " +
                    "GROUP BY ID_учителя ORDER BY COUNT(*) DESC LIMIT 1",
                    p => p.AddWithValue("@s", subjectId));
                if (teacherIdObj != null && teacherIdObj != DBNull.Value)
                    comboTeacher.SelectedValue = teacherIdObj;
            }
            catch { /* не критично */ }
        }

        // ── Enter в поле «Часов» → добавить ──────────────────────────────
        private void textHours_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                buttonAdd_Click(null, EventArgs.Empty);
            }
        }

        // ── Двойной клик по строке → заполнить форму ─────────────────────
        private void dataGrid_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            if (!(dataGrid.DataSource is DataTable dt)) return;
            int workloadId = Convert.ToInt32(dt.Rows[e.RowIndex]["ID_нагрузки"]);
            try
            {
                DataTable details = DbHelper.Query(
                    "SELECT ID_учителя, ID_класса, ID_предмета_параллели, " +
                    "       Количество_часов_в_неделю, Подгруппа " +
                    "FROM Workload WHERE ID_нагрузки = @id",
                    p => p.AddWithValue("@id", workloadId));
                if (details.Rows.Count == 0) return;
                DataRow r = details.Rows[0];

                // Сначала класс → auto-фильтр предметов по параллели
                comboClass.SelectedValue = Convert.ToInt32(r["ID_класса"]);
                // Затем предмет (из уже отфильтрованного списка)
                comboSubject.SelectedValue = Convert.ToInt32(r["ID_предмета_параллели"]);
                // Учитель — перезаписываем авто-подсказку фактическим значением
                comboTeacher.SelectedValue = Convert.ToInt32(r["ID_учителя"]);
                textHours.Text = r["Количество_часов_в_неделю"].ToString();
                comboSubgroup.SelectedIndex = r["Подгруппа"] == DBNull.Value
                    ? 0 : Convert.ToInt32(r["Подгруппа"]);
                comboSubject.Focus();
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Предзаполнение формы"); }
        }

        // ── Добавление нагрузки ───────────────────────────────────────────
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

            object subgroupParam = comboSubgroup.SelectedIndex <= 0
                ? (object)System.DBNull.Value
                : (object)comboSubgroup.SelectedIndex;

            try
            {
                DbHelper.ExecProcNonQuery("sp_AddWorkload",
                    p => {
                        p.AddWithValue("@ID_учителя",                comboTeacher.SelectedValue);
                        p.AddWithValue("@ID_класса",                 comboClass.SelectedValue);
                        p.AddWithValue("@ID_предмета_параллели",     comboSubject.SelectedValue);
                        p.AddWithValue("@Количество_часов_в_неделю", (byte)hours);
                        p.AddWithValue("@Подгруппа",                 subgroupParam);
                    });
                // Поля не сбрасываем: класс, учитель, часы, подгруппа остаются —
                // пользователь меняет только предмет и сразу нажимает Enter снова
                LoadWorkloadGrid();
                comboSubject.Focus();
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Добавление нагрузки"); }
        }

        // ── Удаление нагрузки ─────────────────────────────────────────────
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
                DbHelper.ExecProcNonQuery("sp_DeleteWorkload",
                    p => p.AddWithValue("@ID_нагрузки", id));
                LoadWorkloadGrid();
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Удаление нагрузки"); }
        }

        // ── Массовое заполнение ───────────────────────────────────────────
        private void buttonBulk_Click(object sender, EventArgs e)
        {
            using (var form = new BulkWorkloadForm())
                form.ShowDialog(this.ParentForm);
            LoadWorkloadGrid();
        }

        // ── Импорт из Excel ───────────────────────────────────────────────
        private void buttonImport_Click(object sender, EventArgs e)
        {
            using (var form = new ImportWorkloadForm())
            {
                if (form.ShowDialog(this.ParentForm) == System.Windows.Forms.DialogResult.OK)
                    LoadWorkloadGrid();
            }
        }

        // ── Поиск ─────────────────────────────────────────────────────────
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
