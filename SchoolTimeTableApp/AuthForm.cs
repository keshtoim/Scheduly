using System;
using System.Windows.Forms;

namespace testing
{
    public partial class AuthForm : Form
    {
        public AppUser AuthenticatedUser { get; private set; }

        public AuthForm()
        {
            InitializeComponent();
        }

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
            if (e.KeyCode == Keys.Enter)
                buttonLogin_Click(sender, e);
        }
    }
}
