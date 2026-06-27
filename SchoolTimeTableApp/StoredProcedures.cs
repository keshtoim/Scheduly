using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.Sqlite;

namespace testing
{
    /// <summary>
    /// Реализация бизнес-логики, которая в прежней версии (SQL Server) была
    /// оформлена в виде хранимых процедур. SQLite хранимых процедур не
    /// поддерживает, поэтому их логика перенесена в код C#.
    ///
    /// Каждый метод соответствует одной процедуре и сохраняет её поведение:
    ///   • проверки занятости учителя / дублирования / ставки;
    ///   • атомарность за счёт транзакций (BEGIN / COMMIT / ROLLBACK);
    ///   • те же тексты сообщений об ошибках.
    ///
    /// Класс вызывается из DbHelper.ExecProc / ExecProcNonQuery по имени
    /// процедуры, поэтому остальной код приложения менять не требуется.
    /// </summary>
    internal static class StoredProcedures
    {
        // Удобный доступ к значению параметра по имени (с ведущим @ или без).
        private static object Get(Dictionary<string, object> p, string name)
        {
            if (p.TryGetValue(name, out var v)) return v;
            if (p.TryGetValue("@" + name, out v)) return v;
            return null;
        }
        private static int GetInt(Dictionary<string, object> p, string name)
            => Convert.ToInt32(Get(p, name));
        private static int GetIntOrDefault(Dictionary<string, object> p, string name, int def = 0)
        {
            var v = Get(p, name);
            return (v == null || v == DBNull.Value) ? def : Convert.ToInt32(v);
        }
        private static int? GetNullableInt(Dictionary<string, object> p, string name)
        {
            var v = Get(p, name);
            return (v == null || v == DBNull.Value) ? (int?)null : Convert.ToInt32(v);
        }
        private static double GetDouble(Dictionary<string, object> p, string name)
            => Convert.ToDouble(Get(p, name));

        // ----------------------------------------------------------------
        //  Точки входа: возвращающие данные (ExecProc) и без данных.
        // ----------------------------------------------------------------

        /// <summary>
        /// Выполняет «процедуру», возвращающую таблицу (аналог ExecProc).
        /// </summary>
        public static DataTable RunQuery(string name, Dictionary<string, object> p,
            SqliteConnection c)
        {
            switch (name)
            {
                case "sp_AddWorkload":        return AddWorkload(p, c);
                case "sp_AddTeacher":         return AddTeacher(p, c);
                case "sp_AddClassroom":       return AddClassroom(p, c);
                case "sp_AddLesson":          return AddLesson(p, c);
                case "sp_GetClassSchedule":   return GetClassSchedule(p, c);
                case "sp_GetClassConflicts":  return GetClassConflicts(p, c);
                default:
                    throw new ArgumentException("Неизвестная процедура: " + name);
            }
        }

        /// <summary>
        /// Выполняет «процедуру» без возврата данных (аналог ExecProcNonQuery).
        /// </summary>
        public static int RunNonQuery(string name, Dictionary<string, object> p,
            SqliteConnection c)
        {
            switch (name)
            {
                case "sp_AddLesson":     AddLesson(p, c);     return 1;
                case "sp_UpdateLesson":  return UpdateLesson(p, c);
                case "sp_DeleteLesson":  return DeleteLesson(p, c);
                case "sp_AddWorkload":   AddWorkload(p, c);   return 1;
                case "sp_DeleteWorkload": return DeleteWorkload(p, c);
                case "sp_AddTeacher":    AddTeacher(p, c);    return 1;
                case "sp_AddClassroom":  AddClassroom(p, c);  return 1;
                default:
                    throw new ArgumentException("Неизвестная процедура: " + name);
            }
        }

        // ================================================================
        //  ПРОЦЕДУРА 1: Добавление урока в расписание
        //  Проверяет, не занят ли учитель в этот день и номер урока.
        //  Двойное бронирование кабинета пресекается триггером в БД.
        // ================================================================
        private static DataTable AddLesson(Dictionary<string, object> p, SqliteConnection c)
        {
            int workloadId  = GetInt(p, "ID_нагрузки");
            int classroomId = GetInt(p, "ID_кабинета");
            int day         = GetInt(p, "ID_дня_недели");
            int lesson      = GetInt(p, "ID_номера_урока");
            int week        = GetIntOrDefault(p, "Чётность_недели", 0); // 0=каждую, 1=нечётные, 2=чётные

            using (var tx = c.BeginTransaction())
            {
                // Проверка занятости учителя с учётом чётности недели
                bool teacherBusy = ScalarInt(c, tx,
                    @"SELECT COUNT(*)
                      FROM Schedule s
                      JOIN Workload w  ON s.ID_нагрузки = w.ID_нагрузки
                      JOIN Workload w2 ON w2.ID_нагрузки = $wl
                      WHERE s.ID_дня_недели   = $day
                        AND s.ID_номера_урока = $les
                        AND w.ID_учителя      = w2.ID_учителя
                        AND (s.Чётность_недели = 0 OR $week = 0 OR s.Чётность_недели = $week)",
                    ("$wl", workloadId), ("$day", day), ("$les", lesson), ("$week", week)) > 0;

                if (teacherBusy)
                    throw new Exception("Учитель уже ведёт другой урок в это время!");

                Exec(c, tx,
                    @"INSERT INTO Schedule (ID_нагрузки, ID_кабинета, ID_дня_недели, ID_номера_урока, Чётность_недели)
                      VALUES ($wl, $room, $day, $les, $week)",
                    ("$wl", workloadId), ("$room", classroomId), ("$day", day), ("$les", lesson), ("$week", week));

                int newId = ScalarInt(c, tx, "SELECT last_insert_rowid()");
                tx.Commit();

                var dt = new DataTable();
                dt.Columns.Add("ID_расписания", typeof(int));
                dt.Rows.Add(newId);
                return dt;
            }
        }

        // ================================================================
        //  ПРОЦЕДУРА 2: Обновление урока в расписании
        // ================================================================
        private static int UpdateLesson(Dictionary<string, object> p, SqliteConnection c)
        {
            int schedId     = GetInt(p, "ID_расписания");
            int workloadId  = GetInt(p, "ID_нагрузки");
            int classroomId = GetInt(p, "ID_кабинета");
            int week        = GetIntOrDefault(p, "Чётность_недели", 0);

            using (var tx = c.BeginTransaction())
            {
                int day = ScalarInt(c, tx,
                    "SELECT ID_дня_недели FROM Schedule WHERE ID_расписания = $id",
                    ("$id", schedId));
                int lesson = ScalarInt(c, tx,
                    "SELECT ID_номера_урока FROM Schedule WHERE ID_расписания = $id",
                    ("$id", schedId));

                // Проверка занятости учителя с учётом чётности (исключая текущую запись)
                bool teacherBusy = ScalarInt(c, tx,
                    @"SELECT COUNT(*)
                      FROM Schedule s
                      JOIN Workload w  ON s.ID_нагрузки = w.ID_нагрузки
                      JOIN Workload w2 ON w2.ID_нагрузки = $wl
                      WHERE s.ID_дня_недели   = $day
                        AND s.ID_номера_урока = $les
                        AND w.ID_учителя      = w2.ID_учителя
                        AND s.ID_расписания  <> $id
                        AND (s.Чётность_недели = 0 OR $week = 0 OR s.Чётность_недели = $week)",
                    ("$wl", workloadId), ("$day", day), ("$les", lesson),
                    ("$id", schedId), ("$week", week)) > 0;

                if (teacherBusy)
                    throw new Exception("Учитель уже ведёт другой урок в это время!");

                int n = Exec(c, tx,
                    @"UPDATE Schedule
                      SET ID_нагрузки = $wl, ID_кабинета = $room, Чётность_недели = $week
                      WHERE ID_расписания = $id",
                    ("$wl", workloadId), ("$room", classroomId),
                    ("$id", schedId), ("$week", week));
                tx.Commit();
                return n;
            }
        }

        // ================================================================
        //  ПРОЦЕДУРА 3: Удаление урока из расписания
        // ================================================================
        private static int DeleteLesson(Dictionary<string, object> p, SqliteConnection c)
        {
            int schedId = GetInt(p, "ID_расписания");
            using (var tx = c.BeginTransaction())
            {
                int n = Exec(c, tx,
                    "DELETE FROM Schedule WHERE ID_расписания = $id", ("$id", schedId));
                tx.Commit();
                return n;
            }
        }

        // ================================================================
        //  ПРОЦЕДУРА 4: Добавление нагрузки (с проверкой дублирования)
        // ================================================================
        private static DataTable AddWorkload(Dictionary<string, object> p, SqliteConnection c)
        {
            int   teacherId = GetInt(p, "ID_учителя");
            int   classId   = GetInt(p, "ID_класса");
            int   subjParId = GetInt(p, "ID_предмета_параллели");
            int   hours     = Convert.ToInt32(Get(p, "Количество_часов_в_неделю"));
            int?  subgroup  = GetNullableInt(p, "Подгруппа"); // null=весь класс, 1/2=подгруппа
            object sgParam  = (object)subgroup ?? DBNull.Value;

            using (var tx = c.BeginTransaction())
            {
                // Нельзя иметь две записи нагрузки для одной и той же комбинации класс+предмет+подгруппа
                bool dup = ScalarInt(c, tx,
                    @"SELECT COUNT(*) FROM Workload
                      WHERE ID_класса = $cl
                        AND ID_предмета_параллели = $sp
                        AND Подгруппа IS $sg",
                    ("$cl", classId), ("$sp", subjParId), ("$sg", sgParam)) > 0;

                if (dup)
                    throw new Exception("Такая нагрузка уже существует!");

                Exec(c, tx,
                    @"INSERT INTO Workload
                        (ID_учителя, ID_класса, ID_предмета_параллели, Количество_часов_в_неделю, Подгруппа)
                      VALUES ($t, $cl, $sp, $h, $sg)",
                    ("$t", teacherId), ("$cl", classId), ("$sp", subjParId),
                    ("$h", hours), ("$sg", sgParam));

                int newId = ScalarInt(c, tx, "SELECT last_insert_rowid()");
                tx.Commit();

                var dt = new DataTable();
                dt.Columns.Add("ID_нагрузки", typeof(int));
                dt.Rows.Add(newId);
                return dt;
            }
        }

        // ================================================================
        //  ПРОЦЕДУРА 5: Удаление нагрузки вместе со всеми уроками
        //  Транзакция гарантирует атомарность: либо всё, либо ничего.
        // ================================================================
        private static int DeleteWorkload(Dictionary<string, object> p, SqliteConnection c)
        {
            int workloadId = GetInt(p, "ID_нагрузки");
            using (var tx = c.BeginTransaction())
            {
                // Сначала уроки расписания, затем саму нагрузку
                Exec(c, tx, "DELETE FROM Schedule WHERE ID_нагрузки = $id", ("$id", workloadId));
                int n = Exec(c, tx, "DELETE FROM Workload WHERE ID_нагрузки = $id", ("$id", workloadId));
                tx.Commit();
                return n;
            }
        }

        // ================================================================
        //  ПРОЦЕДУРА 6: Добавление учителя (с проверкой ставки)
        // ================================================================
        private static DataTable AddTeacher(Dictionary<string, object> p, SqliteConnection c)
        {
            string surname = Convert.ToString(Get(p, "Фамилия"));
            string name = Convert.ToString(Get(p, "Имя"));
            string patr = Convert.ToString(Get(p, "Отчество"));
            double stake = GetDouble(p, "Ставка");

            using (var tx = c.BeginTransaction())
            {
                if (stake <= 0)
                    throw new Exception("Ставка должна быть положительным числом!");

                Exec(c, tx,
                    @"INSERT INTO Teachers (Фамилия, Имя, Отчество, Ставка)
                      VALUES ($f, $n, $p, $s)",
                    ("$f", surname), ("$n", name), ("$p", patr), ("$s", stake));

                int newId = ScalarInt(c, tx, "SELECT last_insert_rowid()");
                tx.Commit();

                var dt = new DataTable();
                dt.Columns.Add("ID_учителя", typeof(int));
                dt.Rows.Add(newId);
                return dt;
            }
        }

        // ================================================================
        //  ПРОЦЕДУРА 7: Добавление кабинета (с проверкой номера)
        // ================================================================
        private static DataTable AddClassroom(Dictionary<string, object> p, SqliteConnection c)
        {
            int number = GetInt(p, "Номер");
            int capacity = GetInt(p, "Вместимость");
            int typeId = GetInt(p, "ID_типа_кабинета");

            using (var tx = c.BeginTransaction())
            {
                bool exists = ScalarInt(c, tx,
                    "SELECT COUNT(*) FROM Classrooms WHERE Номер = $num", ("$num", number)) > 0;

                if (exists)
                    throw new Exception("Кабинет с таким номером уже существует!");

                Exec(c, tx,
                    @"INSERT INTO Classrooms (Номер, Вместимость, ID_типа_кабинета)
                      VALUES ($num, $cap, $type)",
                    ("$num", number), ("$cap", capacity), ("$type", typeId));

                int newId = ScalarInt(c, tx, "SELECT last_insert_rowid()");
                tx.Commit();

                var dt = new DataTable();
                dt.Columns.Add("ID_кабинета", typeof(int));
                dt.Rows.Add(newId);
                return dt;
            }
        }

        // ================================================================
        //  ПРОЦЕДУРА 8: Получение расписания класса
        // ================================================================
        private static DataTable GetClassSchedule(Dictionary<string, object> p, SqliteConnection c)
        {
            int classId = GetInt(p, "ID_класса");
            return ReadTable(c,
                @"SELECT ID_расписания, ID_нагрузки, ID_кабинета, Кабинет,
                         ID_дня_недели, День_недели, ID_номера_урока, Номер_урока,
                         ID_учителя, ФИО_учителя, Предмет, Сложность
                  FROM vw_Schedule
                  WHERE ID_класса = $cl
                  ORDER BY ID_дня_недели, Номер_урока",
                ("$cl", classId));
        }

        // ================================================================
        //  ПРОЦЕДУРА 9: Получение конфликтов класса
        // ================================================================
        private static DataTable GetClassConflicts(Dictionary<string, object> p, SqliteConnection c)
        {
            int classId = GetInt(p, "ID_класса");
            return ReadTable(c,
                @"SELECT *
                  FROM vw_Conflicts
                  WHERE ID_расписания_1 IN (
                        SELECT s.ID_расписания FROM Schedule s
                        JOIN Workload w ON s.ID_нагрузки = w.ID_нагрузки
                        WHERE w.ID_класса = $cl)
                  ORDER BY ID_дня_недели, Номер_урока",
                ("$cl", classId));
        }

        // ================================================================
        //  ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ
        // ================================================================

        private static int Exec(SqliteConnection c, SqliteTransaction tx, string sql,
            params (string, object)[] ps)
        {
            using (var cmd = c.CreateCommand())
            {
                cmd.Transaction = tx;
                cmd.CommandText = sql;
                foreach (var (k, v) in ps) cmd.Parameters.AddWithValue(k, v ?? DBNull.Value);
                return cmd.ExecuteNonQuery();
            }
        }

        private static int ScalarInt(SqliteConnection c, SqliteTransaction tx, string sql,
            params (string, object)[] ps)
        {
            using (var cmd = c.CreateCommand())
            {
                cmd.Transaction = tx;
                cmd.CommandText = sql;
                foreach (var (k, v) in ps) cmd.Parameters.AddWithValue(k, v ?? DBNull.Value);
                var r = cmd.ExecuteScalar();
                return (r == null || r == DBNull.Value) ? 0 : Convert.ToInt32(r);
            }
        }

        private static DataTable ReadTable(SqliteConnection c, string sql,
            params (string, object)[] ps)
        {
            using (var cmd = c.CreateCommand())
            {
                cmd.CommandText = sql;
                foreach (var (k, v) in ps) cmd.Parameters.AddWithValue(k, v ?? DBNull.Value);
                var dt = new DataTable();
                using (var reader = cmd.ExecuteReader())
                    dt.Load(reader);
                return dt;
            }
        }
    }
}
