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
        /// Если файла базы нет — создаёт его из встроенных скриптов.
        /// Если файл уже есть — запускает миграцию для добавления новых колонок.
        /// </summary>
        public static void EnsureDatabase()
        {
            if (File.Exists(DbPath))
            {
                MigrateIfNeeded();
                return;
            }

            try
            {
                using (var c = new SqliteConnection(ConnStr))
                {
                    c.Open();
                    RunScript(c, "schema_sqlite.sql");
                    RunScript(c, "data_sqlite.sql");
                }
            }
            catch (Exception ex)
            {
                try { if (File.Exists(DbPath)) File.Delete(DbPath); } catch { }
                throw new Exception(
                    "Не удалось создать базу данных при первом запуске.\n" + ex.Message, ex);
            }
        }

        /// <summary>
        /// Применяет инкрементальные миграции к существующей базе.
        /// Порядок: FK выкл → снести все триггеры и вьюхи → мигрировать таблицы →
        /// воссоздать триггеры и вьюхи → FK вкл.
        /// Каждая DDL-команда выполняется отдельным SqliteCommand — это исключает
        /// проблему с многострочными командами в старых версиях Microsoft.Data.Sqlite
        /// и не даёт SQLite компилировать чужие триггеры в момент DROP TABLE.
        /// </summary>
        public static void MigrateIfNeeded()
        {
            try
            {
                using (var c = new SqliteConnection(ConnStr))
                {
                    c.Open();

                    bool hasSubgroup   = ColumnExists(c, "Workload", "Подгруппа");
                    bool hasWeekParity = ColumnExists(c, "Schedule", "Чётность_недели");
                    if (hasSubgroup && hasWeekParity)
                        return;

                    // PRAGMA нельзя ставить внутри транзакции — ставим до
                    One(c, "PRAGMA foreign_keys = OFF");

                    using (var tx = c.BeginTransaction())
                    {
                        // 1. Снести все триггеры и представления, чтобы они не
                        //    ссылались на таблицы во время DDL-операций ниже.
                        One(c, tx, "DROP TRIGGER IF EXISTS trg_PreventDoubleBooking");
                        One(c, tx, "DROP TRIGGER IF EXISTS trg_PreventClassroomDeletion");
                        One(c, tx, "DROP TRIGGER IF EXISTS trg_PreventTeacherDeletion");
                        One(c, tx, "DROP TRIGGER IF EXISTS trg_PreventSubjectDeletion");
                        One(c, tx, "DROP VIEW IF EXISTS vw_Conflicts");
                        One(c, tx, "DROP VIEW IF EXISTS vw_Workload");
                        One(c, tx, "DROP VIEW IF EXISTS vw_Schedule");

                        // 2. Workload: добавить колонку Подгруппа
                        if (!hasSubgroup)
                            One(c, tx,
                                "ALTER TABLE Workload ADD COLUMN Подгруппа INTEGER DEFAULT NULL " +
                                "CHECK (Подгруппа IS NULL OR Подгруппа IN (1, 2))");

                        // 3. Schedule: пересоздать без UNIQUE-ограничения, с колонкой Чётность_недели
                        if (!hasWeekParity)
                        {
                            One(c, tx, "DROP TABLE IF EXISTS Schedule_v2");
                            One(c, tx, @"CREATE TABLE Schedule_v2 (
    ID_расписания   INTEGER PRIMARY KEY AUTOINCREMENT,
    ID_нагрузки     INTEGER NOT NULL REFERENCES Workload(ID_нагрузки),
    ID_кабинета     INTEGER NOT NULL REFERENCES Classrooms(ID_кабинета),
    ID_дня_недели   INTEGER NOT NULL REFERENCES DayOfWeek(ID_дня_недели),
    ID_номера_урока INTEGER NOT NULL REFERENCES LessonNumber(ID_номера_урока),
    Чётность_недели INTEGER NOT NULL DEFAULT 0 CHECK (Чётность_недели IN (0, 1, 2))
)");
                            One(c, tx,
                                "INSERT INTO Schedule_v2 " +
                                "SELECT ID_расписания, ID_нагрузки, ID_кабинета, " +
                                "       ID_дня_недели, ID_номера_урока, 0 FROM Schedule");
                            One(c, tx, "DROP TABLE Schedule");
                            One(c, tx, "ALTER TABLE Schedule_v2 RENAME TO Schedule");
                        }

                        // 4. Воссоздать все триггеры с обновлённой логикой
                        One(c, tx, @"CREATE TRIGGER trg_PreventDoubleBooking
BEFORE INSERT ON Schedule FOR EACH ROW
WHEN EXISTS (
    SELECT 1 FROM Schedule s
    WHERE s.ID_кабинета     = NEW.ID_кабинета
      AND s.ID_дня_недели   = NEW.ID_дня_недели
      AND s.ID_номера_урока = NEW.ID_номера_урока
      AND (s.Чётность_недели = 0 OR NEW.Чётность_недели = 0 OR s.Чётность_недели = NEW.Чётность_недели)
)
BEGIN
    SELECT RAISE(ABORT, 'Кабинет уже занят в это время!');
END");
                        One(c, tx, @"CREATE TRIGGER trg_PreventClassroomDeletion
BEFORE DELETE ON Classrooms FOR EACH ROW
WHEN EXISTS (SELECT 1 FROM Schedule s WHERE s.ID_кабинета = OLD.ID_кабинета)
BEGIN
    SELECT RAISE(ABORT, 'Нельзя удалить кабинет — он используется в расписании!');
END");
                        One(c, tx, @"CREATE TRIGGER trg_PreventTeacherDeletion
BEFORE DELETE ON Teachers FOR EACH ROW
WHEN EXISTS (SELECT 1 FROM Workload w WHERE w.ID_учителя = OLD.ID_учителя)
BEGIN
    SELECT RAISE(ABORT, 'Нельзя удалить учителя — у него есть активная нагрузка!');
END");
                        One(c, tx, @"CREATE TRIGGER trg_PreventSubjectDeletion
BEFORE DELETE ON Subjects FOR EACH ROW
WHEN EXISTS (SELECT 1 FROM SubjectByParallel sbp WHERE sbp.ID_предмета = OLD.ID_предмета)
BEGIN
    SELECT RAISE(ABORT, 'Нельзя удалить предмет — он используется в программе по параллелям!');
END");

                        // 5. Воссоздать все представления
                        One(c, tx, @"CREATE VIEW vw_Schedule AS
SELECT
    s.ID_расписания, s.ID_нагрузки, s.ID_кабинета,
    cr.Номер AS Кабинет, ct.Тип_кабинета,
    d.ID_дня_недели, d.День_недели,
    ln.ID_номера_урока, ln.Номер_урока,
    w.ID_учителя,
    t.Фамилия || ' ' || t.Имя || ' ' || t.Отчество AS ФИО_учителя,
    t.Фамилия, t.Имя, t.Отчество,
    w.ID_класса,
    CAST(pc.Параллель AS TEXT) || lc.Буква AS Класс,
    pc.Параллель, lc.Буква,
    sbp.ID_предмета_со_сложностью,
    sub.Название AS Предмет,
    diff.Сложность,
    s.Чётность_недели,
    CASE s.Чётность_недели WHEN 0 THEN '' WHEN 1 THEN '[Нч]' ELSE '[Чт]' END AS Пометка_недели,
    w.Подгруппа,
    CASE WHEN w.Подгруппа IS NULL THEN '' WHEN w.Подгруппа = 1 THEN '[П1]' ELSE '[П2]' END AS Пометка_подгруппы
FROM Schedule s
JOIN Workload w            ON s.ID_нагрузки           = w.ID_нагрузки
JOIN Teachers t            ON w.ID_учителя            = t.ID_учителя
JOIN Classes cl            ON w.ID_класса             = cl.ID_класса
JOIN ParallelClass pc      ON cl.ID_параллели_класса  = pc.ID_параллели_класса
JOIN LetterClass lc        ON cl.ID_буквы_класса      = lc.ID_буквы_класса
JOIN SubjectByParallel sbp ON w.ID_предмета_параллели = sbp.ID_предмета_со_сложностью
JOIN Subjects sub          ON sbp.ID_предмета         = sub.ID_предмета
JOIN Difficulty diff       ON sbp.ID_сложности        = diff.ID_сложности
JOIN Classrooms cr         ON s.ID_кабинета           = cr.ID_кабинета
JOIN ClassroomTypes ct     ON cr.ID_типа_кабинета     = ct.ID_типа_кабинета
JOIN DayOfWeek d           ON s.ID_дня_недели         = d.ID_дня_недели
JOIN LessonNumber ln       ON s.ID_номера_урока       = ln.ID_номера_урока");
                        One(c, tx, @"CREATE VIEW vw_Workload AS
SELECT
    w.ID_нагрузки, w.ID_учителя,
    t.Фамилия || ' ' || t.Имя || ' ' || t.Отчество AS ФИО_учителя,
    w.ID_класса,
    CAST(pc.Параллель AS TEXT) || lc.Буква AS Класс,
    pc.Параллель,
    sub.Название AS Предмет,
    diff.Сложность,
    w.Количество_часов_в_неделю,
    w.Подгруппа,
    CASE WHEN w.Подгруппа IS NULL THEN 'Весь класс'
         WHEN w.Подгруппа = 1    THEN 'Подгруппа 1'
         ELSE 'Подгруппа 2' END AS Тип_нагрузки,
    (SELECT COUNT(*) FROM Schedule s WHERE s.ID_нагрузки = w.ID_нагрузки) AS Поставлено_уроков
FROM Workload w
JOIN Teachers t            ON w.ID_учителя            = t.ID_учителя
JOIN Classes cl            ON w.ID_класса             = cl.ID_класса
JOIN ParallelClass pc      ON cl.ID_параллели_класса  = pc.ID_параллели_класса
JOIN LetterClass lc        ON cl.ID_буквы_класса      = lc.ID_буквы_класса
JOIN SubjectByParallel sbp ON w.ID_предмета_параллели = sbp.ID_предмета_со_сложностью
JOIN Subjects sub          ON sbp.ID_предмета         = sub.ID_предмета
JOIN Difficulty diff       ON sbp.ID_сложности        = diff.ID_сложности");
                        One(c, tx, @"CREATE VIEW vw_Conflicts AS
SELECT DISTINCT
    s1.ID_расписания AS ID_расписания_1, s2.ID_расписания AS ID_расписания_2,
    d.День_недели, d.ID_дня_недели,
    ln.Номер_урока, ln.ID_номера_урока,
    CAST(pc1.Параллель AS TEXT) || lc1.Буква AS Класс_1,
    CAST(pc2.Параллель AS TEXT) || lc2.Буква AS Класс_2,
    t1.Фамилия || ' ' || t1.Имя || ' ' || t1.Отчество AS Учитель_1,
    t2.Фамилия || ' ' || t2.Имя || ' ' || t2.Отчество AS Учитель_2,
    sub1.Название AS Предмет_1, sub2.Название AS Предмет_2,
    CASE WHEN w1.ID_учителя = w2.ID_учителя THEN 'учитель' ELSE 'кабинет' END AS Тип_конфликта
FROM Schedule s1
JOIN Schedule s2            ON s1.ID_дня_недели   = s2.ID_дня_недели
                           AND s1.ID_номера_урока = s2.ID_номера_урока
                           AND s1.ID_расписания   < s2.ID_расписания
                           AND (s1.Чётность_недели = 0 OR s2.Чётность_недели = 0
                                OR s1.Чётность_недели = s2.Чётность_недели)
JOIN Workload w1            ON s1.ID_нагрузки = w1.ID_нагрузки
JOIN Workload w2            ON s2.ID_нагрузки = w2.ID_нагрузки
JOIN Teachers t1            ON w1.ID_учителя  = t1.ID_учителя
JOIN Teachers t2            ON w2.ID_учителя  = t2.ID_учителя
JOIN Classes cl1            ON w1.ID_класса   = cl1.ID_класса
JOIN Classes cl2            ON w2.ID_класса   = cl2.ID_класса
JOIN ParallelClass pc1      ON cl1.ID_параллели_класса = pc1.ID_параллели_класса
JOIN ParallelClass pc2      ON cl2.ID_параллели_класса = pc2.ID_параллели_класса
JOIN LetterClass lc1        ON cl1.ID_буквы_класса     = lc1.ID_буквы_класса
JOIN LetterClass lc2        ON cl2.ID_буквы_класса     = lc2.ID_буквы_класса
JOIN SubjectByParallel sbp1 ON w1.ID_предмета_параллели = sbp1.ID_предмета_со_сложностью
JOIN SubjectByParallel sbp2 ON w2.ID_предмета_параллели = sbp2.ID_предмета_со_сложностью
JOIN Subjects sub1          ON sbp1.ID_предмета = sub1.ID_предмета
JOIN Subjects sub2          ON sbp2.ID_предмета = sub2.ID_предмета
JOIN DayOfWeek d            ON s1.ID_дня_недели   = d.ID_дня_недели
JOIN LessonNumber ln        ON s1.ID_номера_урока = ln.ID_номера_урока
WHERE w1.ID_учителя = w2.ID_учителя
   OR s1.ID_кабинета = s2.ID_кабинета");

                        tx.Commit();
                    }

                    One(c, "PRAGMA foreign_keys = ON");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка миграции базы данных.\n" + ex.Message, ex);
            }
        }

        /// <summary>Проверяет, существует ли колонка в таблице.</summary>
        private static bool ColumnExists(SqliteConnection c, string table, string column)
        {
            using (var cmd = c.CreateCommand())
            {
                cmd.CommandText = "PRAGMA table_info(" + table + ")";
                using (var r = cmd.ExecuteReader())
                    while (r.Read())
                        if (r["name"].ToString() == column) return true;
                return false;
            }
        }

        /// <summary>Выполняет одну DDL-команду без транзакции (только для PRAGMA).</summary>
        private static void One(SqliteConnection c, string sql)
        {
            using (var cmd = c.CreateCommand())
            { cmd.CommandText = sql; cmd.ExecuteNonQuery(); }
        }

        /// <summary>Выполняет одну DDL-команду внутри транзакции.</summary>
        private static void One(SqliteConnection c, SqliteTransaction tx, string sql)
        {
            using (var cmd = c.CreateCommand())
            { cmd.Transaction = tx; cmd.CommandText = sql; cmd.ExecuteNonQuery(); }
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

        // ================================================================
        //  СОВМЕСТИМОСТЬ С ХРАНИМЫМИ ПРОЦЕДУРАМИ
        //  SQLite не поддерживает процедуры, поэтому их логика перенесена
        //  в класс StoredProcedures. Эти два метода сохраняют прежний
        //  интерфейс вызова (ExecProc / ExecProcNonQuery с лямбдой
        //  заполнения параметров), поэтому остальной код не меняется.
        //  Параметры собираются во временную коллекцию, из неё — в словарь,
        //  который передаётся в реализацию на C#.
        // ================================================================

        /// <summary>
        /// Считывает параметры, заполненные лямбдой, в словарь «имя -> значение».
        /// </summary>
        private static System.Collections.Generic.Dictionary<string, object>
            CollectParams(Action<SqliteParameterCollection> addParams)
        {
            var dict = new System.Collections.Generic.Dictionary<string, object>();
            if (addParams != null)
            {
                // Создаём временную команду только ради сбора параметров
                using (var tmp = new SqliteCommand())
                {
                    addParams(tmp.Parameters);
                    foreach (SqliteParameter prm in tmp.Parameters)
                        dict[prm.ParameterName] = prm.Value;
                }
            }
            return dict;
        }

        /// <summary>
        /// Аналог прежнего вызова хранимой процедуры, возвращающей таблицу.
        /// </summary>
        public static DataTable ExecProc(string procName,
            Action<SqliteParameterCollection> addParams = null)
        {
            var p = CollectParams(addParams);
            using (var c = Open())
            {
                return StoredProcedures.RunQuery(procName, p, c);
            }
        }

        /// <summary>
        /// Аналог прежнего вызова хранимой процедуры без возврата данных.
        /// </summary>
        public static int ExecProcNonQuery(string procName,
            Action<SqliteParameterCollection> addParams = null)
        {
            var p = CollectParams(addParams);
            using (var c = Open())
            {
                return StoredProcedures.RunNonQuery(procName, p, c);
            }
        }
    }
}
