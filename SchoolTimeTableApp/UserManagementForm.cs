using System;
using System.Data;
using System.Windows.Forms;

namespace testing
{
    /// <summary>
    /// Форма управления пользователями системы.
    /// Доступна только Директору: смена пароля и назначение роли Администратора.
    /// </summary>
    public partial class UserManagementForm : Form
    {
        private readonly AppUser _currentUser;

        public UserManagementForm(AppUser currentUser)
        {
            _currentUser = currentUser;
            InitializeComponent();
        }

        private void UserManagementForm_Load(object sender, EventArgs e)
        {
            LoadUsers();
            LoadRoles();
        }

        private void LoadUsers()
        {
            try
            {
                dataGridUsers.DataSource = AppUsers.GetAllUsers();
                if (dataGridUsers.Columns.Contains("ID_пользователя"))
                    dataGridUsers.Columns["ID_пользователя"].Visible = false;
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
                comboRole.DisplayMember = "Название";
                comboRole.ValueMember   = "ID_роли";
                comboRole.DataSource    = dt;
            }
            catch (Exception ex) { DbHelper.ShowError(ex, "Загрузка ролей"); }
        }

        private int GetSelectedUserId()
        {
            if (dataGridUsers.CurrentRow == null) return -1;
            if (!(dataGridUsers.DataSource is DataTable dt)) return -1;
            return Convert.ToInt32(dt.Rows[dataGridUsers.CurrentRow.Index]["ID_пользователя"]);
        }

        /// <summary>
        /// Смена пароля выбранному пользователю (только для Директора).
        /// </summary>
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
                MessageBox.Show("Введите новый пароль (не менее 4 символов).", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (AppUsers.ChangePassword(userId, newPwd))
            {
                MessageBox.Show("Пароль успешно изменён.", "Готово",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                textNewPassword.Clear();
            }
            else
                MessageBox.Show("Не удалось изменить пароль.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Смена роли выбранному пользователю.
        /// </summary>
        private void buttonChangeRole_Click(object sender, EventArgs e)
        {
            int userId = GetSelectedUserId();
            if (userId < 0)
            {
                MessageBox.Show("Выберите пользователя.", "Внимание",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (comboRole.SelectedValue == null) return;
            int roleId = Convert.ToInt32(comboRole.SelectedValue);

            if (MessageBox.Show(
                string.Format("Изменить роль пользователя на «{0}»?", comboRole.Text),
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

        private void buttonClose_Click(object sender, EventArgs e) { Close(); }
    }
}
