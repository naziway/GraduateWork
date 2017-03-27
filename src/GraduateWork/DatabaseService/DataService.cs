using DatabaseService.Extension;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseService
{
    public class DataService
    {
        public Tuple<int, int> GetNewOrderIdAndCode()
        {
            Tuple<int, int> newIdAndCode = new Tuple<int, int>(0, 0);
            using (var data = new DoctorPhoneEntities3())
            {
                var lastOrder = data.Orders.LastOrDefault();
                if (lastOrder != null)
                    newIdAndCode = new Tuple<int, int>(lastOrder.Id, lastOrder.OrderKods);
            }
            return newIdAndCode;
        }


        public void NewOrder(List<OrderRecordModel> orders)
        {
            var lastInfo = GetNewOrderIdAndCode();
            var newOrders = orders.ToOrder(lastInfo.Item1, lastInfo.Item2);
            using (var data = new DoctorPhoneEntities3())
            {
                data.Orders.AddRange(newOrders);
                data.SaveChangesAsync();
            }
        }


        public User GetUser(string login, string password)
        {
            User currentUser = null;
            using (var database = new DoctorPhoneEntities3())
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
            using (var database = new DoctorPhoneEntities3())
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
            using (var database = new DoctorPhoneEntities3())
            {
                var devices = GetDevicesByClientId(client.Id).Select(device => device.Id);
                orders = database.Orders.Where(order => devices.Contains(order.DeviceId.Value)).Select(ToOrderRecord).ToList();
            }
            return orders;
        }
        public List<Device> GetDevicesByClientId(int clientId)
        {
            List<Device> devicesByClient = null;
            using (var database = new DoctorPhoneEntities3())
            {
                devicesByClient = database.DevicesDbs.Where(device => device.ClientId == clientId).Select(ToDevice).ToList();
            }
            return devicesByClient;
        }
        public List<Device> GetAllDevices()
        {
            List<Device> devices = null;
            using (var database = new DoctorPhoneEntities3())
            {
                devices = database.DevicesDbs.Select(dbClient => dbClient.ToDevice()).ToList();
            }
            return devices;
        }
        public List<Client> GetClientsList()
        {
            List<Client> clients = null;
            using (var database = new DoctorPhoneEntities3())
            {
                clients = database.ClientsDbs.Select(ToClient).ToList();
            }
            return clients;
        }

        #region Extension
        public Device ToDevice(DevicesDb device)
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
        private OrderRecordModel ToOrderRecord(Order order)
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
        private Client ToClient(ClientsDb client)
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
