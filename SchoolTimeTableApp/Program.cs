using System;
using System.Windows.Forms;

namespace testing
{
    /// <summary>
    /// Точка входа в приложение.
    /// Сначала гарантирует наличие базы данных (создаёт при первом запуске),
    /// затем открывает форму авторизации и, при успешном входе, главное окно.
    /// </summary>
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // При первом запуске создаём файл базы SQLite из встроенных скриптов.
            // Если база уже существует — метод ничего не делает.
            try
            {
                DbHelper.EnsureDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Не удалось подготовить базу данных:\n" + ex.Message,
                    "Критическая ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // без базы продолжать нельзя
            }

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
