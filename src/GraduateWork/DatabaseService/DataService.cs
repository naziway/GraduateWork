using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseService.Extension;
using Model;

namespace DatabaseService
{
    public class DataService
    {
        public User GetUser(string login, string password)
        {
            User currentUser;
            using (var database = new DataBaseEntities())
            {
                var userB =
                    database.Users.FirstOrDefault(user => user.Login == login && user.Password == password);
                currentUser = userB?.ToUserModel();
            }

            return currentUser;
        }
    }
}
