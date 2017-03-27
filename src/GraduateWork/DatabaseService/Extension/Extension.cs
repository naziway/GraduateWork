
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
        public static OrderRecordModel ToOrderRecord(this Order order)
        {
            //int count = 19;
            //var a = new List<Order>();
            //a.Reverse();
            //a.Take(count + 16).Skip(count);
            return new OrderRecordModel();
            //return new OrderRecordModel//TODO
            //{
            //    Id = order.Id,
            //    DeviceId = order.DeviceId,
            //    OrderKods = order.OrderKods,
            //    OrderType = order.OrderType,
            //    PartId = order.PartId,
            //    WorkId = order.WorkId
            //};
        }
        public static Device ToDevice(this DevicesDb device)
        {
            return new Device
            {
                Id = device.Id,
                ClientId = device.ClientId,
                DeviceType = device.DeviceType,
                ManufactureDate = device.ManufactureDate,
                PhoneMarka = device.PhoneMarka,
                PhoneModel = device.PhoneModel,
                SerialNumber = device.SerialNumber
            };
        }
        public static Client ToClient(this ClientsDb client)
        {
            return new Client
            {
                Id = client.Id,
                Name = client.Name,
                Surname = client.Surname,
                PassportData = client.PassportData,
                Phone = client.Phone
            };
        }
    }
}