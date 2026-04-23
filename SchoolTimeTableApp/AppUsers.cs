using System.Collections.Generic;

namespace testing
{
    public enum UserRole { Admin, Director, Teacher }

    public class AppUser
    {
        public string Login       { get; set; }
        public string Password    { get; set; }
        public UserRole Role      { get; set; }
        public string DisplayName { get; set; }

        public bool CanEdit => Role == UserRole.Admin || Role == UserRole.Director;
    }

    public static class AppUsers
    {
        public static readonly List<AppUser> Users = new List<AppUser>
        {
            new AppUser { Login = "admin",    Password = "admin123",    Role = UserRole.Admin,    DisplayName = "Администратор" },
            new AppUser { Login = "director", Password = "director123", Role = UserRole.Director, DisplayName = "Директор"      },
            new AppUser { Login = "teacher",  Password = "teacher123",  Role = UserRole.Teacher,  DisplayName = "Учитель"       }
        };

        public static AppUser Authenticate(string login, string password)
        {
            foreach (AppUser u in Users)
                if (u.Login == login && u.Password == password)
                    return u;
            return null;
        }
    }
}
