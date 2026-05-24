using System;
using System.Windows.Forms;

namespace testing
{
    /// <summary>
    /// Форма авторизации. Проверка выполняется через таблицу Users в БД.
    /// </summary>
    public partial class AuthForm : Form
    {
        public AppUser AuthenticatedUser { get; private set; }

        public AuthForm() { InitializeComponent(); }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string login    = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Text;

            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                labelError.Text    = "Введите логин и пароль.";
                labelError.Visible = true;
                return;
            }

            // Авторизация через БД
            AppUser user = AppUsers.Authenticate(login, password);

            if (user == null)
            {
                labelError.Text    = "Неверный логин или пароль.";
                labelError.Visible = true;
                textBoxPassword.Clear();
                return;
            }

            AuthenticatedUser = user;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) buttonLogin_Click(sender, e);
        }

        private void buttonShowPassword_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.PasswordChar == '*')
            { textBoxPassword.PasswordChar = '\0'; buttonShowPassword.Text = "🙈"; }
            else
            { textBoxPassword.PasswordChar = '*';  buttonShowPassword.Text = "👁"; }
        }
    }
}
