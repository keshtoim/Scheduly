using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace testing
{
    /// <summary>
    /// Центральный вспомогательный класс для работы с базой данных.
    /// Все обращения к SQL Server выполняются через этот класс.
    /// Строку подключения менять только здесь — она используется во всём приложении.
    /// </summary>
    public static class DbHelper
    {
        /// <summary>
        /// Строка подключения к локальной базе данных SQL Server.
        /// Для смены БД — измените только эту константу.
        /// </summary>
        public const string ConnStr =
            "Server=(localdb)\\MSSQLLocalDB;Database=SchoolTimetable;" +
            "Trusted_Connection=True;TrustServerCertificate=true";

        /// <summary>
        /// SQL-выражение для получения полного ФИО учителя.
        /// Используется во всех запросах где нужно имя учителя.
        /// </summary>
        public const string TeacherFullName =
            "t.surname + ' ' + t.name + ' ' + ISNULL(t.patronymic, '') AS teacher_name";
        /// <param name="sql">Текст SQL-запроса.</param>
        /// <param name="addParams">Лямбда для добавления параметров запроса (можно передать null).</param>
        public static DataTable Query(string sql, Action<SqlParameterCollection> addParams = null)
        {
            using (SqlConnection c = new SqlConnection(ConnStr))
            {
                SqlCommand cmd = new SqlCommand(sql, c);
                addParams?.Invoke(cmd.Parameters);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// Выполняет INSERT, UPDATE или DELETE запрос.
        /// </summary>
        /// <returns>Количество затронутых строк.</returns>
        public static int Execute(string sql, Action<SqlParameterCollection> addParams = null)
        {
            using (SqlConnection c = new SqlConnection(ConnStr))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand(sql, c);
                addParams?.Invoke(cmd.Parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Проверяет наличие строк по условию запроса.
        /// </summary>
        /// <returns>true если найдена хотя бы одна строка.</returns>
        public static bool Exists(string sql, Action<SqlParameterCollection> addParams = null)
        {
            using (SqlConnection c = new SqlConnection(ConnStr))
            {
                c.Open();
                SqlCommand cmd = new SqlCommand(sql, c);
                addParams?.Invoke(cmd.Parameters);
                object result = cmd.ExecuteScalar();
                return result != null && result != DBNull.Value && Convert.ToInt32(result) > 0;
            }
        }

        /// <summary>
        /// Показывает диалоговое окно с описанием ошибки.
        /// </summary>
        /// <param name="ex">Исключение.</param>
        /// <param name="context">Контекст где произошла ошибка (необязательно).</param>
        public static void ShowError(Exception ex, string context = "")
        {
            string msg = string.IsNullOrEmpty(context) ? ex.Message : context + ":\n" + ex.Message;
            MessageBox.Show(msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
