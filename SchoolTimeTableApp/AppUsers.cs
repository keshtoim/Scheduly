using System;
using System.Data;

namespace testing
{
    /// <summary>
    /// Роли пользователей системы (соответствуют таблице Roles в БД).
    /// </summary>
    public enum UserRole
    {
        Администратор        = 1,
        Директор             = 2,
        ЗаместительДиректора = 3,
        Учитель              = 4
    }

    /// <summary>
    /// Модель авторизованного пользователя.
    /// </summary>
    public class AppUser
    {
        public int      Id          { get; set; }
        public string   Login       { get; set; }
        public string   DisplayName { get; set; }
        public UserRole Role        { get; set; }
        public string   RoleName    { get; set; }

        /// <summary>Может редактировать расписание, нагрузку и справочники.</summary>
        public bool CanEdit =>
            Role == UserRole.Администратор ||
            Role == UserRole.ЗаместительДиректора;

        /// <summary>Может просматривать расписание.</summary>
        public bool CanView => true;

        /// <summary>Может управлять пользователями и менять пароли.</summary>
        public bool CanManageUsers =>
            Role == UserRole.Директор ||
            Role == UserRole.ЗаместительДиректора;

        /// <summary>Имеет полный доступ к БД (добавление/удаление таблиц и т.д.).</summary>
        public bool CanManageDatabase => Role == UserRole.Администратор;
    }

    /// <summary>
    /// Авторизация через таблицу Users в БД.
    /// </summary>
    public static class AppUsers
    {
        /// <summary>
        /// Проверяет логин и пароль по таблице Users в БД.
        /// Возвращает AppUser при успехе, null при неверных данных.
        /// </summary>
        public static AppUser Authenticate(string login, string password)
        {
            try
            {
                DataTable dt = DbHelper.Query(
                    "SELECT u.ID_пользователя, u.Логин, u.Отображаемое_имя, " +
                    "u.ID_роли, r.Название AS Роль " +
                    "FROM Users u " +
                    "JOIN Roles r ON u.ID_роли = r.ID_роли " +
                    "WHERE u.Логин = @login AND u.Пароль = @pwd AND u.Активен = 1",
                    p => {
                        p.AddWithValue("@login", login);
                        p.AddWithValue("@pwd",   password);
                    });

                if (dt.Rows.Count == 0) return null;

                DataRow row = dt.Rows[0];
                int roleId = Convert.ToInt32(row["ID_роли"]);

                return new AppUser
                {
                    Id          = Convert.ToInt32(row["ID_пользователя"]),
                    Login       = row["Логин"].ToString(),
                    DisplayName = row["Отображаемое_имя"].ToString(),
                    RoleName    = row["Роль"].ToString(),
                    Role        = (UserRole)roleId,
                };
            }
            catch (Exception ex)
            {
                DbHelper.ShowError(ex, "Ошибка авторизации");
                return null;
            }
        }

        /// <summary>
        /// Меняет пароль пользователя. Доступно только Директору.
        /// </summary>
        public static bool ChangePassword(int userId, string newPassword)
        {
            try
            {
                int rows = DbHelper.Execute(
                    "UPDATE Users SET Пароль = @pwd WHERE ID_пользователя = @id",
                    p => {
                        p.AddWithValue("@pwd", newPassword);
                        p.AddWithValue("@id",  userId);
                    });
                return rows > 0;
            }
            catch { return false; }
        }

        /// <summary>
        /// Загружает список всех активных пользователей с ролями.
        /// </summary>
        public static DataTable GetAllUsers()
        {
            return DbHelper.Query(
                "SELECT u.ID_пользователя, u.Логин, u.Отображаемое_имя, " +
                "r.Название AS Роль, u.Активен " +
                "FROM Users u JOIN Roles r ON u.ID_роли = r.ID_роли " +
                "ORDER BY r.ID_роли, u.Отображаемое_имя");
        }

        /// <summary>
        /// Загружает список ролей из БД.
        /// </summary>
        public static DataTable GetAllRoles()
        {
            return DbHelper.Query(
                "SELECT ID_роли, Название FROM Roles ORDER BY ID_роли");
        }

        /// <summary>
        /// Меняет роль пользователя. Доступно только Директору.
        /// </summary>
        public static bool ChangeRole(int userId, int roleId)
        {
            try
            {
                int rows = DbHelper.Execute(
                    "UPDATE Users SET ID_роли = @rid WHERE ID_пользователя = @id",
                    p => {
                        p.AddWithValue("@rid", roleId);
                        p.AddWithValue("@id",  userId);
                    });
                return rows > 0;
            }
            catch { return false; }
        }
    }
}
