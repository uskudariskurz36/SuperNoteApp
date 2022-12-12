using SuperNoteApp.Entities;
using System.Data;

namespace SuperNoteApp.Helpers
{
    public class UserManager
    {
        private DatabaseContext db = new DatabaseContext();


        public bool AddUser(string username, string password)
        {
            User user = new User();
            user.Username = username;
            user.Password = password;
            user.Picture = "default.png";

            db.Users.Add(user);
            int result = db.SaveChanges();

            return result > 0;
        }

        public User Authenticate(string username, string password)
        {
            User user = (from u in db.Users
                        where u.Username == username && u.Password == password
                        select u).FirstOrDefault();

            return user;
        }

        public User GetUserById(int userId)
        {
            User user = (from u in db.Users
                         where u.Id == userId
                         select u).FirstOrDefault();

            return user;
        }

        public bool UpdateProfile(int userId, string name, string surname)
        {
            User user = GetUserById(userId);
            
            if (user != null)
            {
                user.Name = name;
                user.Surname = surname;

                return db.SaveChanges() > 0;
            }

            return false;
        }

        public bool UpdatePassword(int userId, string? password)
        {
            User user = GetUserById(userId);

            if (user != null)
            {
                user.Password = password;

                return db.SaveChanges() > 0;
            }

            return false;
        }

        public bool UpdateProfilePicture(int userId, string filename)
        {
            User user = GetUserById(userId);

            if (user != null)
            {
                user.Picture = filename;

                return db.SaveChanges() > 0;
            }

            return false;
        }
    }
}
