
using DatabaseService;
using Model;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseService.Extension
{
    public static class Extension
    {
        public static User ToUserModel(this UsersDbs user)
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
        //public static List<Orders> ToOrder(this List<OrderRecordModel> orders, int id, int code)
        //{
        //    return orders.Select(orderRecordModel => orderRecordModel.ToOrder(id++, code)).ToList();
        //}
        //public static Order ToOrder(this OrderRecordModel order, int id, int code)//TODO finish converting
        //{
        //    return new Order
        //    {
        //        Id = id,
        //        OrderKods = code,
        //        UserId = order.UserId,
        //        DeviceId = order.Device.Id,
        //        OrderStatus = order.OrderStatus.ToString(),
        //        OrderType = order.OrderType.ToString(),
        //    };
        //}

        public static OrderModel ToOrderRecord(this Orders order)
        {
            //int count = 19;
            //var a = new List<Order>();
            //a.Reverse();
            //a.Take(count + 16).Skip(count);
            return new OrderModel();
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
        public static Device ToDevice(this DevicesDbs device)
        {
            return new Device
            {
                Id = device.Id,
             
                DeviceType = device.DeviceType,
                ManufactureDate = device.ManufactureDate,
                PhoneMarka = device.PhoneMarka,
                PhoneModel = device.PhoneModel,
                SerialNumber = device.SerialNumber
            };
        }
        public static Client ToClient(this ClientsDbs client)
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