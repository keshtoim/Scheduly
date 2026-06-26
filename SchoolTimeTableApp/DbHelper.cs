using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Data.Sqlite;

namespace testing
{
    /// <summary>
    /// Центральный класс работы с базой данных SchoolTimetable (SQLite).
    /// Все обращения к БД выполняются через этот класс.
    ///
    /// В отличие от прежней версии (SQL Server LocalDB), теперь используется
    /// встраиваемая база SQLite в одном файле SchoolTimetable.db, который
    /// лежит рядом с исполняемым файлом приложения. Сервер СУБД не требуется.
    ///
    /// При первом запуске (если файла базы нет) база создаётся автоматически
    /// из встроенных в приложение SQL-скриптов (schema_sqlite.sql и data_sqlite.sql).
    /// </summary>
    public static class DbHelper
    {
        // ----------------------------------------------------------------
        // Путь к файлу базы данных — рядом с .exe приложения.
        // ----------------------------------------------------------------
        private static readonly string DbPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SchoolTimetable.db");

        /// <summary>
        /// Строка подключения к файлу SQLite. Foreign Keys включаются явно
        /// в методе Open(), так как в SQLite они по умолчанию выключены.
        /// </summary>
        public static string ConnStr =>
            new SqliteConnectionStringBuilder
            {
                DataSource = DbPath
            }.ToString();

        /// <summary>
        /// SQL-выражение полного ФИО учителя для использования в запросах.
        /// В SQLite конкатенация выполняется оператором ||.
        /// </summary>
        public const string TeacherFullName =
            "t.Фамилия || ' ' || t.Имя || ' ' || t.Отчество AS ФИО_учителя";

        // ----------------------------------------------------------------
        // Открытие соединения с обязательным включением внешних ключей.
        // ----------------------------------------------------------------
        private static SqliteConnection Open()
        {
            var c = new SqliteConnection(ConnStr);
            c.Open();
            // В SQLite контроль внешних ключей нужно включать для каждого соединения
            using (var pragma = c.CreateCommand())
            {
                pragma.CommandText = "PRAGMA foreign_keys = ON;";
                pragma.ExecuteNonQuery();
            }
            return c;
        }

        /// <summary>
        /// Инициализация базы данных при старте приложения.
        /// Если файла базы нет — создаёт его и выполняет встроенные скрипты:
        /// сначала схему, затем данные. Вызывается один раз в Program.Main().
        /// </summary>
        public static void EnsureDatabase()
        {
            if (File.Exists(DbPath))
                return; // база уже создана — ничего не делаем

            try
            {
                using (var c = new SqliteConnection(ConnStr))
                {
                    c.Open();
                    // 1) Структура: таблицы, представления, триггеры, справочники
                    RunScript(c, "schema_sqlite.sql");
                    // 2) Данные: учителя, предметы, кабинеты, нагрузка, расписание
                    RunScript(c, "data_sqlite.sql");
                }
            }
            catch (Exception ex)
            {
                // Если что-то пошло не так — удаляем недоделанный файл,
                // чтобы при следующем запуске попытка повторилась с нуля.
                try { if (File.Exists(DbPath)) File.Delete(DbPath); } catch { }
                throw new Exception(
                    "Не удалось создать базу данных при первом запуске.\n" + ex.Message, ex);
            }
        }

        /// <summary>
        /// Читает встроенный SQL-скрипт из ресурсов сборки и выполняет его.
        /// Имя ресурса ищется по окончанию имени файла, чтобы не зависеть
        /// от точного пространства имён и имени папки в проекте.
        /// </summary>
        private static void RunScript(SqliteConnection c, string fileName)
        {
            var asm = Assembly.GetExecutingAssembly();

            // Находим ресурс, имя которого заканчивается на нужный файл
            string resourceName = asm.GetManifestResourceNames()
                .FirstOrDefault(n => n.EndsWith(fileName, StringComparison.OrdinalIgnoreCase));

            if (resourceName == null)
            {
                // Подсказка для отладки: показываем, какие ресурсы реально встроены
                string available = string.Join("\n",
                    asm.GetManifestResourceNames());
                throw new FileNotFoundException(
                    "Встроенный ресурс \"" + fileName + "\" не найден.\n" +
                    "Проверьте, что файл добавлен в проект с Build Action = Embedded Resource.\n\n" +
                    "Доступные ресурсы:\n" + available);
            }

            string sql;
            using (var stream = asm.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                sql = reader.ReadToEnd();
            }

            // Выполняем весь скрипт целиком (SQLite допускает несколько команд)
            using (var cmd = c.CreateCommand())
            {
                cmd.CommandText = "PRAGMA foreign_keys = ON;\n" + sql;
                cmd.ExecuteNonQuery();
            }
        }

        // ================================================================
        //  МЕТОДЫ ДОСТУПА К ДАННЫМ
        //  Сигнатуры сохранены прежними, чтобы остальной код приложения
        //  не требовал изменений. Тип параметров — Action<SqliteParameterCollection>.
        // ================================================================

        /// <summary>
        /// Выполняет SQL-запрос и возвращает результат в виде таблицы DataTable.
        /// </summary>
        public static DataTable Query(string sql,
            Action<SqliteParameterCollection> addParams = null)
        {
            using (var c = Open())
            using (var cmd = c.CreateCommand())
            {
                cmd.CommandText = sql;
                addParams?.Invoke(cmd.Parameters);

                var dt = new DataTable();
                using (var reader = cmd.ExecuteReader())
                {
                    dt.Load(reader);
                }
                return dt;
            }
        }

        /// <summary>
        /// Выполняет SQL-команду без возврата данных (INSERT / UPDATE / DELETE).
        /// Возвращает число затронутых строк.
        /// </summary>
        public static int Execute(string sql,
            Action<SqliteParameterCollection> addParams = null)
        {
            using (var c = Open())
            using (var cmd = c.CreateCommand())
            {
                cmd.CommandText = sql;
                addParams?.Invoke(cmd.Parameters);
                return cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Проверяет, существует ли хотя бы одна запись по заданному условию.
        /// Запрос должен возвращать число (например, SELECT COUNT(*) ...).
        /// </summary>
        public static bool Exists(string sql,
            Action<SqliteParameterCollection> addParams = null)
        {
            using (var c = Open())
            using (var cmd = c.CreateCommand())
            {
                cmd.CommandText = sql;
                addParams?.Invoke(cmd.Parameters);
                var result = cmd.ExecuteScalar();
                return result != null && result != DBNull.Value
                    && Convert.ToInt32(result) > 0;
            }
        }

        /// <summary>
        /// Возвращает скалярное значение (первый столбец первой строки).
        /// Удобно для получения id после вставки: SELECT last_insert_rowid();
        /// </summary>
        public static object Scalar(string sql,
            Action<SqliteParameterCollection> addParams = null)
        {
            using (var c = Open())
            using (var cmd = c.CreateCommand())
            {
                cmd.CommandText = sql;
                addParams?.Invoke(cmd.Parameters);
                return cmd.ExecuteScalar();
            }
        }

        /// <summary>
        /// Единая точка вывода сообщений об ошибках пользователю.
        /// </summary>
        public static void ShowError(Exception ex, string context = "")
        {
            string msg = string.IsNullOrEmpty(context)
                ? ex.Message : context + ":\n" + ex.Message;
            MessageBox.Show(msg, "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
