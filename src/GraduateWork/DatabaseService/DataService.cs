using DatabaseService.Extension;
using System.Linq;

namespace DatabaseService
{
    public class DataService
    {
        public Model.User GetUser(string login, string password)
        {
            Model.User currentUser;
            using (var database = new DoctorPhoneEntities())
            {
                var userB =
                    database.UsersDbs.FirstOrDefault(user => user.Login == login && user.Password == password);
                currentUser = userB?.ToUserModel();
            }

            return currentUser;
        }
    }
}
