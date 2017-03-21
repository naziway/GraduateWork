using DatabaseService.Extension;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseService
{
    public class DataService
    {
        public User GetUser(string login, string password)
        {
            User currentUser = null;
            using (var database = new DoctorPhoneEntities2())
            {
                try
                {
                    var userB =
                               database.UsersDbs.FirstOrDefault(user => user.Login == login && user.Password == password);
                    currentUser = userB?.ToUserModel();
                }
                catch (Exception e)
                {
                    throw new PlatformNotSupportedException();
                }
            }
            return currentUser;
        }
        public List<OrderRecordModel> GetAllOrders()//TODO
        {
            List<OrderRecordModel> orders = null;
            using (var database = new DoctorPhoneEntities2())
            {
                var clients = database.ClientsDbs;
                var devices = database.DevicesDbs;
                var merge = clients.Join(devices, client => client.Id, device => device.ClientId, (client, device) => new { Name = client.Name });
                //  orders = database.Orders;
            }
            return orders;
        }
        public List<OrderRecordModel> GetOrdersByClient(Client client)
        {
            List<OrderRecordModel> orders = null;
            using (var database = new DoctorPhoneEntities2())
            {
                var devices = GetDevicesByClient(client).Select(device => device.Id);
                orders = database.Orders.Where(order => devices.Contains(order.DeviceId)).Select(x => x.ToOrderRecord()).ToList();
            }
            return orders;
        }
        public List<Device> GetDevicesByClient(Client client)
        {
            List<Device> devicesByClient = null;
            using (var database = new DoctorPhoneEntities2())
            {
                devicesByClient = database.DevicesDbs.Where(device => device.ClientId == client.Id).Select(dbClient => dbClient.ToDevice()).ToList();
            }
            return devicesByClient;
        }
        public List<Device> GetAllDevices()
        {
            List<Device> devices = null;
            using (var database = new DoctorPhoneEntities2())
            {
                devices = database.DevicesDbs.Select(dbClient => dbClient.ToDevice()).ToList();
            }
            return devices;
        }
    }
}
