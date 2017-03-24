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
                var devices = GetDevicesByClientId(client.Id).Select(device => device.Id);
                orders = database.Orders.Where(order => devices.Contains(order.DeviceId)).Select(ToOrderRecord).ToList();
            }
            return orders;
        }
        public List<Device> GetDevicesByClientId(int clientId)
        {
            List<Device> devicesByClient = null;
            using (var database = new DoctorPhoneEntities2())
            {
                devicesByClient = database.DevicesDbs.Where(device => device.ClientId == clientId).Select(ToDevice).ToList();
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
        public List<Client> GetClientsList()
        {
            List<Client> clients = null;
            using (var database = new DoctorPhoneEntities2())
            {
                clients = database.ClientsDbs.Select(ToClient).ToList();
            }
            return clients;
        }

        #region Extension
        public Device ToDevice(DevicesDbs device)
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
        private OrderRecordModel ToOrderRecord(Orders order)
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
        private Client ToClient(ClientsDbs client)
        {
            return new Client
            {
                Id = client.Id,
                Name = client.Name,
                Surname = client.Surname,
                PassportData = client.PassportData,
                Phone = client.Phone,
                Devices = new Devices(GetDevicesByClientId(client.Id))
            };
        }

        #endregion
    }
}
