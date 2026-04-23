using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace testing
{
    public static class DbHelper
    {
        public const string ConnStr =
            "Server=(localdb)\\MSSQLLocalDB;Database=SchoolTimetable0;" +
            "Trusted_Connection=True;TrustServerCertificate=true";

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

        /// <summary>Returns true if any row matches the query.</summary>
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

        public static void ShowError(Exception ex, string context = "")
        {
            string msg = string.IsNullOrEmpty(context) ? ex.Message : context + ":\n" + ex.Message;
            MessageBox.Show(msg, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
