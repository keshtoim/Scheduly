using System;
using System.Windows.Forms;

namespace testing
{
    /// <summary>
    /// Форма авторизации — первое окно которое видит пользователь.
    /// Проверка логина и пароля выполняется через AppUsers.Authenticate(),
    /// без обращения к базе данных.
    /// </summary>
    public partial class AuthForm : Form
    {
        /// <summary>
        /// Авторизованный пользователь после успешного входа.
        /// Доступен только если форма закрыта с DialogResult.OK.
        /// </summary>
        public AppUser AuthenticatedUser { get; private set; }

        public AuthForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Обработчик кнопки «Войти».
        /// Проверяет заполненность полей, затем передаёт данные в AppUsers.Authenticate().
        /// </summary>
        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string login    = textBoxLogin.Text.Trim();
            string password = textBoxPassword.Text;

            // Проверяем что оба поля заполнены
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                labelError.Text    = "Введите логин и пароль.";
                labelError.Visible = true;
                return;
            }

            AppUser user = AppUsers.Authenticate(login, password);

            if (user == null)
            {
                // Неверные данные — показываем ошибку и очищаем поле пароля
                labelError.Text    = "Неверный логин или пароль.";
                labelError.Visible = true;
                textBoxPassword.Clear();
                return;
            }

            // Успех — сохраняем пользователя и закрываем форму
            AuthenticatedUser = user;
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>
        /// Обработчик кнопки «Отмена» — закрывает форму без входа.
        /// </summary>
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        /// <summary>
        /// Позволяет войти нажатием Enter из поля пароля.
        /// </summary>
        private void textBoxPassword_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonLogin_Click(sender, e);
        }

        private void buttonShowPassword_Click(object sender, EventArgs e)
        {
            if (textBoxPassword.PasswordChar == '*')
            {
                textBoxPassword.PasswordChar = '\0';
                buttonShowPassword.Text = "🙈";
            }
            else
            {
                textBoxPassword.PasswordChar = '*';
                buttonShowPassword.Text = "👁";
            }
        }
    }
}
