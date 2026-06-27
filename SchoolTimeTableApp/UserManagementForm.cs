using System;
using System.Data;
using System.Windows.Forms;

namespace testing
{
    /// <summary>
    /// Форма управления пользователями (только для Заместителя директора).
    /// При выборе пользователя показывает его текущий пароль (скрытый),
    /// позволяет его отобразить и изменить, а также сменить роль.
    /// </summary>
    public partial class UserManagementForm : Form
    {
        private readonly AppUser _currentUser;
        private string _currentPassword = "";  // текущий пароль выбранного юзера

        public UserManagementForm(AppUser currentUser)
        {
            _currentUser = currentUser;
            InitializeComponent();
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadRoles();
            ClearPasswordPanel();
        }

        // ── Загрузка ─────────────────────────────────────────────────────────

        private void LoadUsers()
        {
            try
            {
                // Загружаем с паролем чтобы показывать его при выборе
                DataTable dt = DbHelper.Query(
                    "SELECT u.ID_пользователя, u.Логин, u.Отображаемое_имя, " +
                    "r.Роль, u.Пароль, u.Активен " +
                    "FROM Users u JOIN Roles r ON u.ID_роли = r.ID_роли " +
                    "ORDER BY r.ID_роли, u.Отображаемое_имя");

                dataGridUsers.DataSource = dt;

                if (dataGridUsers.Columns.Contains("ID_пользователя"))
                    dataGridUsers.Columns["ID_пользователя"].Visible = false;
                if (dataGridUsers.Columns.Contains("Пароль"))
                    dataGridUsers.Columns["Пароль"].Visible = false;
                if (dataGridUsers.Columns.Contains("Активен"))
                    dataGridUsers.Columns["Активен"].Visible = false;
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка пользователей"); }
        }

        private void LoadRoles()
        {
            try
            {
                DataTable dt = AppUsers.GetAllRoles();
                comboRole.DisplayMember = "Роль";
                comboRole.ValueMember   = "ID_роли";
                comboRole.DataSource    = dt;
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка ролей"); }
        }

        // ── Выбор пользователя ───────────────────────────────────────────────

        private void dataGridUsers_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridUsers.CurrentRow == null ||
                !(dataGridUsers.DataSource is DataTable dt)) return;

            int idx = dataGridUsers.CurrentRow.Index;
            if (idx < 0 || idx >= dt.Rows.Count) return;

            DataRow row = dt.Rows[idx];

            // Показываем текущий пароль скрытым
            _currentPassword = row["Пароль"].ToString();
            textCurrentPassword.Text         = _currentPassword;
            textCurrentPassword.PasswordChar = '*';
            buttonShowCurrent.Text           = "Показать";

            // Поле нового пароля — очищаем
            textNewPassword.Text         = "";
            textNewPassword.PasswordChar = '*';
            buttonShowNew.Text           = "Показать";

            // Выбираем текущую роль в комbobox
            string roleName = row["Роль"].ToString();
            foreach (DataRowView drv in comboRole.Items)
                if (drv["Роль"].ToString() == roleName)
                { comboRole.SelectedItem = drv; break; }

            groupPassword.Text = string.Format(
                "Пароль пользователя: {0}", row["Логин"]);
        }

        private void ClearPasswordPanel()
        {
            _currentPassword             = "";
            textCurrentPassword.Text     = "";
            textNewPassword.Text         = "";
            groupPassword.Text           = "Выберите пользователя";
        }

        // ── Показ/скрытие паролей ────────────────────────────────────────────

        private void buttonShowCurrent_Click(object sender, EventArgs e)
        {
            if (textCurrentPassword.PasswordChar == '*')
            { textCurrentPassword.PasswordChar = '\0'; buttonShowCurrent.Text = "Скрыть"; }
            else
            { textCurrentPassword.PasswordChar = '*';  buttonShowCurrent.Text = "Показать"; }
        }

        private void buttonShowNew_Click(object sender, EventArgs e)
        {
            if (textNewPassword.PasswordChar == '*')
            { textNewPassword.PasswordChar = '\0'; buttonShowNew.Text = "Скрыть"; }
            else
            { textNewPassword.PasswordChar = '*';  textNewPassword.Text = textNewPassword.Text;
              buttonShowNew.Text = "Показать"; }
        }

        // ── Смена пароля ─────────────────────────────────────────────────────

        private void buttonChangePassword_Click(object sender, EventArgs e)
        {
            int userId = GetSelectedUserId();
            if (userId < 0)
            {
                MessageBox.Show("Выберите пользователя.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string newPwd = textNewPassword.Text.Trim();
            if (string.IsNullOrEmpty(newPwd) || newPwd.Length < 4)
            {
                MessageBox.Show("Введите новый пароль (не менее 4 символов).",
                    "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Изменить пароль пользователя?", "Подтверждение",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

            if (AppUsers.ChangePassword(userId, newPwd))
            {
                // Обновляем отображаемый текущий пароль
                _currentPassword             = newPwd;
                textCurrentPassword.Text     = newPwd;
                textCurrentPassword.PasswordChar = '*';
                buttonShowCurrent.Text       = "Показать";
                textNewPassword.Clear();

                MessageBox.Show("Пароль успешно изменён.", "Готово",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
            }
            else
                MessageBox.Show("Не удалось изменить пароль.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ── Смена роли ───────────────────────────────────────────────────────

        private void buttonChangeRole_Click(object sender, EventArgs e)
        {
            int userId = GetSelectedUserId();
            if (userId < 0 || comboRole.SelectedValue == null)
            {
                MessageBox.Show("Выберите пользователя и роль.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int roleId = Convert.ToInt32(comboRole.SelectedValue);

            if (MessageBox.Show(
                string.Format("Назначить роль «{0}»?", comboRole.Text),
                "Подтверждение", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                != DialogResult.Yes) return;

            if (AppUsers.ChangeRole(userId, roleId))
            {
                MessageBox.Show("Роль изменена.", "Готово",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadUsers();
            }
            else
                MessageBox.Show("Не удалось изменить роль.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // ── Вспомогательные ─────────────────────────────────────────────────

        private int GetSelectedUserId()
        {
            if (dataGridUsers.CurrentRow == null) return -1;
            if (!(dataGridUsers.DataSource is DataTable dt)) return -1;
            int idx = dataGridUsers.CurrentRow.Index;
            if (idx < 0 || idx >= dt.Rows.Count) return -1;
            return Convert.ToInt32(dt.Rows[idx]["ID_пользователя"]);
        }

        private void buttonClose_Click(object sender, EventArgs e) { Close(); }
    }
}
