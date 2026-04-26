using System.Collections.Generic;

namespace testing
{
    /// <summary>
    /// Роли пользователей приложения.
    /// Admin и Director имеют полный доступ, Teacher — только просмотр.
    /// </summary>
    public enum UserRole { Admin, Director, Teacher }

    /// <summary>
    /// Модель пользователя приложения.
    /// </summary>
    public class AppUser
    {
        /// <summary>Логин для входа.</summary>
        public string Login       { get; set; }
        /// <summary>Пароль (хранится в открытом виде — только для учебного проекта).</summary>
        public string Password    { get; set; }
        /// <summary>Роль пользователя.</summary>
        public UserRole Role      { get; set; }
        /// <summary>Имя отображаемое в шапке приложения.</summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Возвращает true если пользователь имеет право редактировать данные.
        /// </summary>
        public bool CanEdit => Role == UserRole.Admin || Role == UserRole.Director;
    }

    /// <summary>
    /// Статический список пользователей приложения.
    /// Пользователи хранятся в памяти — БД для авторизации не используется.
    /// </summary>
    public static class AppUsers
    {
        /// <summary>Список всех доступных учётных записей.</summary>
        public static readonly List<AppUser> Users = new List<AppUser>
        {
            new AppUser { Login = "admin",    Password = "admin123",    Role = UserRole.Admin,    DisplayName = "Администратор" },
            new AppUser { Login = "director", Password = "director123", Role = UserRole.Director, DisplayName = "Директор"      },
            new AppUser { Login = "teacher",  Password = "teacher123",  Role = UserRole.Teacher,  DisplayName = "Учитель"       }
        };

        /// <summary>
        /// Проверяет логин и пароль.
        /// </summary>
        /// <returns>Объект AppUser при успехе, null если данные неверны.</returns>
        public static AppUser Authenticate(string login, string password)
        {
            foreach (AppUser u in Users)
                if (u.Login == login && u.Password == password)
                    return u;
            return null;
        }
    }
}
