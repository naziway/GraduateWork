using DatabaseService.Extension;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseService
{
    public class DataService
    {
        public Model.User GetUser(string login, string password)
        {
            Model.User currentUser;
            using (var database = new DoctorPhoneEntities1())
            {
                var userB =
                    database.UsersDbs.FirstOrDefault(user => user.Login == login && user.Password == password);
                currentUser = userB?.ToUserModel();
            }

            return currentUser;
        }
        public List<OrderRecord> GetOrders()
        {
            List<OrderRecord> orders = null;
            using (var database = new DoctorPhoneEntities1())
            {
                orders = database.Orders.Select(x => x.ToOrderRecord()).ToList();
            }
            return orders;
        }
        public List<OrderRecord> GetOrders(User user)
        {
            List<OrderRecord> orders = null;
            using (var database = new DoctorPhoneEntities1())
            {
                orders = database.Orders.Select(x => x.ToOrderRecord()).ToList();
            }
            return orders;
        }
    }
}
