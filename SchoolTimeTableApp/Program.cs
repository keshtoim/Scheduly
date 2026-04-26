using System;
using System.Windows.Forms;

namespace testing
{
    /// <summary>
    /// Точка входа в приложение.
    /// Сначала открывает форму авторизации, и только при успешном входе — главное окно.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Открываем форму авторизации
            AuthForm auth = new AuthForm();

            if (auth.ShowDialog() == DialogResult.OK)
            {
                // Успешный вход — запускаем главное окно с авторизованным пользователем
                Application.Run(new MainForm(auth.AuthenticatedUser));
            }
            // При отмене — приложение просто завершается
        }
    }
}
