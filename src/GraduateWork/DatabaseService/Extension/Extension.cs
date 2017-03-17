
using Model;

namespace DatabaseService.Extension
{
    public static class Extension
    {
        public static User ToUserModel(this UsersDb user)
        {
            return new User
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Login = user.Login,
                Password = user.Password
            };
        }
        public static OrderRecord ToOrderRecord(this Order order)
        {
            //int count = 19;
            //var a = new List<Order>();
            //a.Reverse();
            //a.Take(count + 16).Skip(count);

            return new OrderRecord
            {
                Id = order.Id,
                DeviceId = order.DeviceId,
                OrderKods = order.OrderKods,
                OrderType = order.OrderType,
                PartId = order.PartId,
                WorkId = order.WorkId
            };
        }
    }
}