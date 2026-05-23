using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace testing
{
    /// <summary>
    /// Центральный класс работы с базой данных SchoolTimetable2.
    /// Все обращения к SQL Server выполняются через этот класс.
    /// </summary>
    public static class DbHelper
    {
        /// <summary>
        /// Строка подключения к новой БД SchoolTimetable2.
        /// </summary>
        public const string ConnStr =
            "Server=(localdb)\\MSSQLLocalDB;Database=SchoolTimetable2;" +
            "Trusted_Connection=True;TrustServerCertificate=true";

        /// <summary>
        /// SQL-выражение полного ФИО учителя для использования в запросах.
        /// </summary>
        public const string TeacherFullName =
            "t.Фамилия + ' ' + t.Имя + ' ' + t.Отчество AS ФИО_учителя";

        public static DataTable Query(string sql,
            Action<SqlParameterCollection> addParams = null)
        {
            using (var c = new SqlConnection(ConnStr))
            {
                var cmd = new SqlCommand(sql, c);
                addParams?.Invoke(cmd.Parameters);
                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        public static int Execute(string sql,
            Action<SqlParameterCollection> addParams = null)
        {
            using (var c = new SqlConnection(ConnStr))
            {
                c.Open();
                var cmd = new SqlCommand(sql, c);
                addParams?.Invoke(cmd.Parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        public static bool Exists(string sql,
            Action<SqlParameterCollection> addParams = null)
        {
            using (var c = new SqlConnection(ConnStr))
            {
                c.Open();
                var cmd = new SqlCommand(sql, c);
                addParams?.Invoke(cmd.Parameters);
                var result = cmd.ExecuteScalar();
                return result != null && result != DBNull.Value
                       && Convert.ToInt32(result) > 0;
            }
        }

        /// <summary>
        /// Выполняет хранимую процедуру и возвращает DataTable.
        /// </summary>
        public static DataTable ExecProc(string procName,
            Action<SqlParameterCollection> addParams = null)
        {
            using (var c = new SqlConnection(ConnStr))
            {
                var cmd = new SqlCommand(procName, c)
                    { CommandType = CommandType.StoredProcedure };
                addParams?.Invoke(cmd.Parameters);
                var da = new SqlDataAdapter(cmd);
                var dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
        }

        /// <summary>
        /// Выполняет хранимую процедуру без возврата данных.
        /// </summary>
        public static int ExecProcNonQuery(string procName,
            Action<SqlParameterCollection> addParams = null)
        {
            using (var c = new SqlConnection(ConnStr))
            {
                c.Open();
                var cmd = new SqlCommand(procName, c)
                    { CommandType = CommandType.StoredProcedure };
                addParams?.Invoke(cmd.Parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        public static void ShowError(Exception ex, string context = "")
        {
            string msg = string.IsNullOrEmpty(context)
                ? ex.Message : context + ":\n" + ex.Message;
            MessageBox.Show(msg, "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
