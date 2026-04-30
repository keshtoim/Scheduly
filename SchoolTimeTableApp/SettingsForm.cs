using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;

namespace testing
{
    /// <summary>
    /// Форма настроек — максимальное количество уроков в день для каждого класса.
    /// Значения хранятся в app.config через ConfigurationManager, БД не затрагивается.
    /// </summary>
    public partial class SettingsForm : Form
    {
        // Значения по умолчанию из реального расписания 2025-2026
        public static readonly Dictionary<string, int> Defaults = new Dictionary<string, int>
        {
            {"1А",5},{"1Б",5},{"2А",5},{"2Б",5},
            {"3А",5},{"3Б",5},{"4А",5},{"4Б",5},
            {"5А",6},{"5Б",6},
            {"6А",7},{"6Б",7},
            {"7А",7},{"7Б",7},
            {"8А",7},{"8Б",7},
            {"9А",7},{"9Б",7},
            {"10А",7},{"10Б",7},
            {"11А",7},
        };

        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }

        // ── Загрузка ──────────────────────────────────────────────────────────

        private void LoadGrid()
        {
            try
            {
                var dt = DbHelper.Query(
                    "SELECT cl.class_id, " +
                    "CAST(cp.parallel AS NVARCHAR) + lc.letterClass AS class_name " +
                    "FROM Classes cl " +
                    "JOIN ClassParallel cp ON cl.id_parallel_class = cp.id_parallel_class " +
                    "JOIN LetterOfTheClass lc ON cl.id_letter_class = lc.id_letter_class " +
                    "ORDER BY cp.parallel, lc.letterClass");

                dataGridLimits.Rows.Clear();
                foreach (System.Data.DataRow row in dt.Rows)
                {
                    string cls   = row["class_name"].ToString();
                    int    limit = GetLimit(cls);
                    dataGridLimits.Rows.Add(cls, limit);
                }
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка классов"); }
        }

        // ── Лимиты ────────────────────────────────────────────────────────────

        /// <summary>
        /// Возвращает сохранённый лимит уроков для класса из app.config.
        /// Если не задан — возвращает значение по умолчанию из реального расписания.
        /// </summary>
        public static int GetLimit(string className)
        {
            string key = "limit_" + className;
            string val = ConfigurationManager.AppSettings[key];
            if (!string.IsNullOrEmpty(val) && int.TryParse(val, out int result) && result > 0)
                return result;
            return Defaults.ContainsKey(className) ? Defaults[className] : 7;
        }

        // ── Кнопки ────────────────────────────────────────────────────────────

        private void buttonSave_Click(object sender, EventArgs e)
        {
            try
            {
                var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

                foreach (DataGridViewRow row in dataGridLimits.Rows)
                {
                    if (row.IsNewRow) continue;
                    string cls = row.Cells["colClass"].Value?.ToString();
                    if (string.IsNullOrEmpty(cls)) continue;

                    if (!int.TryParse(row.Cells["colLimit"].Value?.ToString(), out int limit) || limit < 1)
                    {
                        MessageBox.Show($"Некорректное значение для класса {cls}.",
                            "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    string key = "limit_" + cls;
                    if (config.AppSettings.Settings[key] != null)
                        config.AppSettings.Settings[key].Value = limit.ToString();
                    else
                        config.AppSettings.Settings.Add(key, limit.ToString());
                }

                config.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection("appSettings");

                MessageBox.Show("Настройки сохранены.", "Готово",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Сохранение настроек"); }
        }

        private void buttonDefaults_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridLimits.Rows)
            {
                if (row.IsNewRow) continue;
                string cls = row.Cells["colClass"].Value?.ToString();
                if (string.IsNullOrEmpty(cls)) continue;
                row.Cells["colLimit"].Value = Defaults.ContainsKey(cls) ? Defaults[cls] : 7;
            }
        }

        private void buttonClose_Click(object sender, EventArgs e) { Close(); }
    }
}
