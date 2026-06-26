-- ============================================================
--  Школьное расписание — схема базы данных для SQLite
--  Перенос с Microsoft SQL Server (SchoolTimetable2) на SQLite.
--  Файл базы: SchoolTimetable.db (создаётся рядом с приложением).
--
--  Отличия от SQL Server, учтённые при переносе:
--    • IDENTITY(1,1)         -> INTEGER PRIMARY KEY AUTOINCREMENT
--    • TINYINT/INT/DECIMAL   -> INTEGER / REAL (динамическая типизация)
--    • NVARCHAR(n)           -> TEXT (SQLite хранит весь текст в UTF-8/UTF-16)
--    • конкатенация (+)      -> оператор || в представлениях
--    • LIKE N'[А-Я]'         -> GLOB '[А-Я]' (классы символов есть только у GLOB)
--    • RAISERROR в триггере  -> RAISE(ABORT, '...')
--    • хранимые процедуры    -> переносятся в код C# (SQLite их не поддерживает)
--  Включение проверки внешних ключей обязательно при каждом подключении:
--    PRAGMA foreign_keys = ON;
-- ============================================================

PRAGMA foreign_keys = ON;

-- ============================================================
--  ТАБЛИЦЫ-СПРАВОЧНИКИ
-- ============================================================

-- Параллели классов (1-11)
CREATE TABLE ParallelClass (
    ID_параллели_класса INTEGER PRIMARY KEY AUTOINCREMENT,
    Параллель           INTEGER NOT NULL CHECK (Параллель >= 1 AND Параллель <= 11)
);

-- Буквы классов (только кириллица А-Я)
CREATE TABLE LetterClass (
    ID_буквы_класса INTEGER PRIMARY KEY AUTOINCREMENT,
    Буква           TEXT NOT NULL CHECK (Буква GLOB '[А-Я]')
);

-- Классы (комбинация параллели и буквы)
CREATE TABLE Classes (
    ID_класса           INTEGER PRIMARY KEY AUTOINCREMENT,
    ID_параллели_класса INTEGER NOT NULL REFERENCES ParallelClass(ID_параллели_класса),
    ID_буквы_класса     INTEGER NOT NULL REFERENCES LetterClass(ID_буквы_класса)
);

-- Учителя
CREATE TABLE Teachers (
    ID_учителя INTEGER PRIMARY KEY AUTOINCREMENT,
    Фамилия    TEXT NOT NULL,
    Имя        TEXT NOT NULL,
    Отчество   TEXT NOT NULL,
    Ставка     REAL CHECK (Ставка > 0)
);

-- Предметы
CREATE TABLE Subjects (
    ID_предмета INTEGER PRIMARY KEY AUTOINCREMENT,
    Название    TEXT NOT NULL
);

-- Сложность (баллы СанПиН 1-13)
CREATE TABLE Difficulty (
    ID_сложности INTEGER PRIMARY KEY AUTOINCREMENT,
    Сложность    INTEGER NOT NULL CHECK (Сложность >= 1 AND Сложность <= 13)
);

-- Предмет по параллели (предмет + параллель + балл сложности)
CREATE TABLE SubjectByParallel (
    ID_предмета_со_сложностью INTEGER PRIMARY KEY AUTOINCREMENT,
    ID_сложности              INTEGER NOT NULL REFERENCES Difficulty(ID_сложности),
    ID_параллели              INTEGER NOT NULL REFERENCES ParallelClass(ID_параллели_класса),
    ID_предмета               INTEGER NOT NULL REFERENCES Subjects(ID_предмета)
);

-- Нагрузка (учитель + класс + предмет + часы в неделю)
CREATE TABLE Workload (
    ID_нагрузки               INTEGER PRIMARY KEY AUTOINCREMENT,
    ID_учителя                INTEGER NOT NULL REFERENCES Teachers(ID_учителя),
    ID_класса                 INTEGER NOT NULL REFERENCES Classes(ID_класса),
    Количество_часов_в_неделю INTEGER CHECK (Количество_часов_в_неделю > 0 AND Количество_часов_в_неделю <= 20),
    ID_предмета_параллели     INTEGER NOT NULL REFERENCES SubjectByParallel(ID_предмета_со_сложностью)
);

-- Типы кабинетов
CREATE TABLE ClassroomTypes (
    ID_типа_кабинета INTEGER PRIMARY KEY AUTOINCREMENT,
    Тип_кабинета     TEXT NOT NULL
        CHECK (Тип_кабинета IN (
            'Обычный', 'Компьютерный класс',
            'Спортзал', 'Лаборатория химии/физики', 'Мастерская'))
);

-- Кабинеты
CREATE TABLE Classrooms (
    ID_кабинета      INTEGER PRIMARY KEY AUTOINCREMENT,
    Номер            INTEGER NOT NULL,
    Вместимость      INTEGER CHECK (Вместимость > 0),
    ID_типа_кабинета INTEGER NOT NULL REFERENCES ClassroomTypes(ID_типа_кабинета)
);

-- Дни недели (справочник)
CREATE TABLE DayOfWeek (
    ID_дня_недели INTEGER PRIMARY KEY AUTOINCREMENT,
    День_недели   TEXT NOT NULL
);

-- Номера уроков (справочник, 1-8)
CREATE TABLE LessonNumber (
    ID_номера_урока INTEGER PRIMARY KEY AUTOINCREMENT,
    Номер_урока     INTEGER NOT NULL CHECK (Номер_урока >= 1 AND Номер_урока <= 8)
);

-- Расписание (нагрузка + кабинет + день + урок)
-- UNIQUE гарантирует, что кабинет не занят дважды в один слот.
CREATE TABLE Schedule (
    ID_расписания   INTEGER PRIMARY KEY AUTOINCREMENT,
    ID_нагрузки     INTEGER NOT NULL REFERENCES Workload(ID_нагрузки),
    ID_кабинета     INTEGER NOT NULL REFERENCES Classrooms(ID_кабинета),
    ID_дня_недели   INTEGER NOT NULL REFERENCES DayOfWeek(ID_дня_недели),
    ID_номера_урока INTEGER NOT NULL REFERENCES LessonNumber(ID_номера_урока),
    CONSTRAINT UQ_Classroom_Time
        UNIQUE (ID_кабинета, ID_дня_недели, ID_номера_урока)
);

-- Роли пользователей
CREATE TABLE Roles (
    ID_роли INTEGER PRIMARY KEY AUTOINCREMENT,
    Роль    TEXT NOT NULL UNIQUE
);

-- Учётные записи пользователей
CREATE TABLE Users (
    ID_пользователя   INTEGER PRIMARY KEY AUTOINCREMENT,
    Логин             TEXT NOT NULL UNIQUE,
    Пароль            TEXT NOT NULL,
    Отображаемое_имя  TEXT NOT NULL,
    ID_роли           INTEGER NOT NULL REFERENCES Roles(ID_роли),
    Активен           INTEGER NOT NULL DEFAULT 1
);

-- ============================================================
--  ТРИГГЕРЫ ЦЕЛОСТНОСТИ
--  В SQLite вместо RAISERROR используется RAISE(ABORT, '...').
--  Эти триггеры дублируют проверки на уровне БД, дополняя
--  бизнес-логику, которая перенесена в код C#.
-- ============================================================

-- Триггер 1: запрет двойного бронирования кабинета (понятное сообщение)
CREATE TRIGGER trg_PreventDoubleBooking
BEFORE INSERT ON Schedule
FOR EACH ROW
WHEN EXISTS (
    SELECT 1 FROM Schedule s
    WHERE s.ID_кабинета     = NEW.ID_кабинета
      AND s.ID_дня_недели   = NEW.ID_дня_недели
      AND s.ID_номера_урока = NEW.ID_номера_урока
)
BEGIN
    SELECT RAISE(ABORT, 'Кабинет уже занят в это время!');
END;

-- Триггер 2: запрет удаления кабинета, используемого в расписании
CREATE TRIGGER trg_PreventClassroomDeletion
BEFORE DELETE ON Classrooms
FOR EACH ROW
WHEN EXISTS (SELECT 1 FROM Schedule s WHERE s.ID_кабинета = OLD.ID_кабинета)
BEGIN
    SELECT RAISE(ABORT, 'Нельзя удалить кабинет — он используется в расписании!');
END;

-- Триггер 3: запрет удаления учителя с активной нагрузкой
CREATE TRIGGER trg_PreventTeacherDeletion
BEFORE DELETE ON Teachers
FOR EACH ROW
WHEN EXISTS (SELECT 1 FROM Workload w WHERE w.ID_учителя = OLD.ID_учителя)
BEGIN
    SELECT RAISE(ABORT, 'Нельзя удалить учителя — у него есть активная нагрузка!');
END;

-- Триггер 4: запрет удаления предмета, используемого в программе по параллелям
CREATE TRIGGER trg_PreventSubjectDeletion
BEFORE DELETE ON Subjects
FOR EACH ROW
WHEN EXISTS (SELECT 1 FROM SubjectByParallel sbp WHERE sbp.ID_предмета = OLD.ID_предмета)
BEGIN
    SELECT RAISE(ABORT, 'Нельзя удалить предмет — он используется в программе по параллелям!');
END;

-- ============================================================
--  ПРЕДСТАВЛЕНИЯ (VIEW)
--  Конкатенация переведена с (+) на ||, CAST(... AS TEXT).
-- ============================================================

-- Полное расписание — основной вид для вкладки «Расписание»
CREATE VIEW vw_Schedule AS
SELECT
    s.ID_расписания,
    s.ID_нагрузки,
    s.ID_кабинета,
    cr.Номер                         AS Кабинет,
    ct.Тип_кабинета                  AS Тип_кабинета,
    d.ID_дня_недели,
    d.День_недели,
    ln.ID_номера_урока,
    ln.Номер_урока,
    w.ID_учителя,
    t.Фамилия || ' ' || t.Имя || ' ' || t.Отчество AS ФИО_учителя,
    t.Фамилия,
    t.Имя,
    t.Отчество,
    w.ID_класса,
    CAST(pc.Параллель AS TEXT) || lc.Буква AS Класс,
    pc.Параллель,
    lc.Буква,
    sbp.ID_предмета_со_сложностью,
    sub.Название                     AS Предмет,
    diff.Сложность
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
JOIN LessonNumber ln       ON s.ID_номера_урока       = ln.ID_номера_урока;

-- Нагрузка с количеством фактически поставленных уроков
CREATE VIEW vw_Workload AS
SELECT
    w.ID_нагрузки,
    w.ID_учителя,
    t.Фамилия || ' ' || t.Имя || ' ' || t.Отчество AS ФИО_учителя,
    w.ID_класса,
    CAST(pc.Параллель AS TEXT) || lc.Буква AS Класс,
    pc.Параллель,
    sub.Название                     AS Предмет,
    diff.Сложность,
    w.Количество_часов_в_неделю,
    (SELECT COUNT(*) FROM Schedule s WHERE s.ID_нагрузки = w.ID_нагрузки) AS Поставлено_уроков
FROM Workload w
JOIN Teachers t            ON w.ID_учителя            = t.ID_учителя
JOIN Classes cl            ON w.ID_класса             = cl.ID_класса
JOIN ParallelClass pc      ON cl.ID_параллели_класса  = pc.ID_параллели_класса
JOIN LetterClass lc        ON cl.ID_буквы_класса      = lc.ID_буквы_класса
JOIN SubjectByParallel sbp ON w.ID_предмета_параллели = sbp.ID_предмета_со_сложностью
JOIN Subjects sub          ON sbp.ID_предмета         = sub.ID_предмета
JOIN Difficulty diff       ON sbp.ID_сложности        = diff.ID_сложности;

-- Конфликты расписания — готовый список для панели предупреждений
CREATE VIEW vw_Conflicts AS
SELECT DISTINCT
    s1.ID_расписания                 AS ID_расписания_1,
    s2.ID_расписания                 AS ID_расписания_2,
    d.День_недели,
    d.ID_дня_недели,
    ln.Номер_урока,
    ln.ID_номера_урока,
    CAST(pc1.Параллель AS TEXT) || lc1.Буква AS Класс_1,
    CAST(pc2.Параллель AS TEXT) || lc2.Буква AS Класс_2,
    t1.Фамилия || ' ' || t1.Имя || ' ' || t1.Отчество AS Учитель_1,
    t2.Фамилия || ' ' || t2.Имя || ' ' || t2.Отчество AS Учитель_2,
    sub1.Название                    AS Предмет_1,
    sub2.Название                    AS Предмет_2,
    CASE WHEN w1.ID_учителя = w2.ID_учителя THEN 'учитель' ELSE 'кабинет' END AS Тип_конфликта
FROM Schedule s1
JOIN Schedule s2           ON s1.ID_дня_недели    = s2.ID_дня_недели
                          AND s1.ID_номера_урока  = s2.ID_номера_урока
                          AND s1.ID_расписания    < s2.ID_расписания
JOIN Workload w1           ON s1.ID_нагрузки = w1.ID_нагрузки
JOIN Workload w2           ON s2.ID_нагрузки = w2.ID_нагрузки
JOIN Teachers t1           ON w1.ID_учителя  = t1.ID_учителя
JOIN Teachers t2           ON w2.ID_учителя  = t2.ID_учителя
JOIN Classes cl1           ON w1.ID_класса   = cl1.ID_класса
JOIN Classes cl2           ON w2.ID_класса   = cl2.ID_класса
JOIN ParallelClass pc1     ON cl1.ID_параллели_класса = pc1.ID_параллели_класса
JOIN ParallelClass pc2     ON cl2.ID_параллели_класса = pc2.ID_параллели_класса
JOIN LetterClass lc1       ON cl1.ID_буквы_класса     = lc1.ID_буквы_класса
JOIN LetterClass lc2       ON cl2.ID_буквы_класса     = lc2.ID_буквы_класса
JOIN SubjectByParallel sbp1 ON w1.ID_предмета_параллели = sbp1.ID_предмета_со_сложностью
JOIN SubjectByParallel sbp2 ON w2.ID_предмета_параллели = sbp2.ID_предмета_со_сложностью
JOIN Subjects sub1         ON sbp1.ID_предмета = sub1.ID_предмета
JOIN Subjects sub2         ON sbp2.ID_предмета = sub2.ID_предмета
JOIN DayOfWeek d           ON s1.ID_дня_недели   = d.ID_дня_недели
JOIN LessonNumber ln       ON s1.ID_номера_урока = ln.ID_номера_урока
WHERE w1.ID_учителя = w2.ID_учителя
   OR s1.ID_кабинета = s2.ID_кабинета;

-- ============================================================
--  ЗАПОЛНЕНИЕ БАЗОВЫХ СПРАВОЧНИКОВ
-- ============================================================

INSERT INTO ParallelClass (Параллель) VALUES (1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11);
INSERT INTO LetterClass (Буква) VALUES ('А'),('Б');
INSERT INTO Classes (ID_параллели_класса, ID_буквы_класса) VALUES
(1,1),(1,2),(2,1),(2,2),(3,1),(3,2),(4,1),(4,2),
(5,1),(5,2),(6,1),(6,2),(7,1),(7,2),(8,1),(8,2),
(9,1),(9,2),(10,1),(10,2),(11,1);

INSERT INTO Difficulty (Сложность) VALUES
(1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11),(12),(13);

INSERT INTO ClassroomTypes (Тип_кабинета) VALUES
('Обычный'),('Компьютерный класс'),('Спортзал'),
('Лаборатория химии/физики'),('Мастерская');

INSERT INTO DayOfWeek (День_недели) VALUES
('Понедельник'),('Вторник'),('Среда'),('Четверг'),('Пятница');

INSERT INTO LessonNumber (Номер_урока) VALUES (1),(2),(3),(4),(5),(6),(7),(8);

INSERT INTO Roles (Роль) VALUES
('Администратор'),('Заместитель директора'),('Учитель');
