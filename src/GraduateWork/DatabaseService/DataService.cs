using DatabaseService.Extension;
using Model;
using Shared;
using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DatabaseService
{
    public class DataService
    {
        public User User { get; set; }
        public bool AddNewExaminate(Device device)
        {
            var exeminate = new Orders();

            var kodId = GetNewOrderIdAndCode();
            exeminate.Id = kodId.Item1;
            exeminate.OrderKods = kodId.Item2;
            exeminate.BeginDate = DateTime.Now;
            exeminate.OrderType = (int)OrderType.Review;
            exeminate.OrderStatus = (int)OrderStatus.Active;
            exeminate.DeviceId = device.Id;
            exeminate.PartId = null;
            exeminate.WorkerId = User.Id;
            exeminate.UserId = User.Id;
            exeminate.WorkId = 1;

            using (var database = new MobiDocContext())
            {
                database.Orders.Add(exeminate);
                try
                {
                    database.SaveChanges();
                }
                catch (Exception)
                {

                    return false;
                }
            }

            return true;
        }


        public Tuple<int, int> GetNewOrderIdAndCode()
        {
            Tuple<int, int> newIdAndCode = new Tuple<int, int>(0, 0);
            using (var data = new MobiDocContext())
            {
                var lastOrder = data.Orders.ToList().LastOrDefault();
                if (lastOrder != null)
                    newIdAndCode = new Tuple<int, int>(lastOrder.Id + 1, lastOrder.OrderKods + 1);
            }
            return newIdAndCode;
        }

        public User GetUser(string login, string password)
        {
            User currentUser = null;
            using (var database = new MobiDocContext())
            {
                try
                {
                    var userB =
                               database.UsersDbs.FirstOrDefault(user => user.Login == login && user.Password == password);
                    currentUser = userB?.ToUserModel();
                }
                catch (Exception e)
                {
                    throw new UserNotFoundException();
                }
            }
            return currentUser;
        }
        public List<OrderModel> GetAllOrders()//TODO
        {
            List<OrderModel> allOrders = null;
            using (var database = new MobiDocContext())
            {
                var clients = database.ClientsDbs;
                var devices = database.DevicesDbs;
                var orderTable = database.Orders;
                var works = database.WorksDbs;
                var parts = database.PartsDbs;
                var users = database.UsersDbs;

                var deviceWithClient = clients.Join(devices, client => client.Id, device => device.ClientId, (clientsDbs, devicesDbs) =>
                  new
                  {
                      Client = new Client
                      {
                          Id = clientsDbs.Id,
                          Name = clientsDbs.Name,
                          Surname = clientsDbs.Surname,
                          Phone = clientsDbs.Phone,
                          PassportData = clientsDbs.PassportData
                      },

                      Id = devicesDbs.Id,
                      ManufactureDate = devicesDbs.ManufactureDate,
                      DeviceType = devicesDbs.DeviceType,
                      PhoneMarka = devicesDbs.PhoneMarka,
                      SerialNumber = devicesDbs.SerialNumber,
                      PhoneModel = devicesDbs.PhoneModel
                  });

                var orderWork = orderTable.Join(works, orders => orders.WorkId, worksDbs => worksDbs.Id,
                    (orders, worksDbs) => new
                    {
                        OrderId = orders.Id,
                        OrderStatus = orders.OrderStatus,
                        OrderKods = orders.OrderKods,
                        PartId = orders.PartId,
                        UserId = orders.UserId,
                        DeviceId = orders.DeviceId,
                        OrderType = orders.OrderType,
                        SparePhone = orders.SparePhone,
                        BeginDate = orders.BeginDate,
                        Work = new WorkModel
                        {
                            Id = worksDbs.Id,
                            Title = worksDbs.Title,
                            ExecutionTime = worksDbs.ExecutionTime,
                            Price = worksDbs.Price
                        }
                    });
                var orderWorkPart = orderWork.Join(parts, orders => orders.PartId, partDbs => partDbs.Id,
                    (orders, part) => new
                    {
                        OrderId = orders.OrderId,
                        OrderKods = orders.OrderKods,
                        Part = new Part
                        {
                            Id = part.Id,
                            Title = part.Title,
                            Model = part.Model,
                            Marka = part.Marka,
                            Price = part.Price,
                            Count = part.Count,
                        },
                        UserId = orders.UserId,
                        DeviceId = orders.DeviceId,
                        OrderStatus = orders.OrderStatus,
                        OrderType = orders.OrderType,
                        SparePhone = orders.SparePhone,
                        BeginDate = orders.BeginDate,
                        Work = orders.Work
                    });
                var orderWorkPartUser = orderWorkPart.Join(users, orders => orders.UserId, user => user.Id,
                   (orders, user) => new
                   {
                       OrderId = orders.OrderId,
                       OrderKods = orders.OrderKods,
                       Part = orders.Part,
                       User = new User
                       {
                           Id = user.Id,
                           Name = user.Name,
                           Surname = user.Surname,
                           Login = user.Login,
                           Password = user.Password,
                           IsAdmin = user.IsAdmin,
                       },
                       DeviceId = orders.DeviceId,
                       OrderStatus = orders.OrderStatus,
                       OrderType = orders.OrderType,
                       SparePhone = orders.SparePhone,
                       BeginDate = orders.BeginDate,
                       Work = orders.Work
                   });
                var orderList = orderWorkPartUser.Join(deviceWithClient, orders => orders.DeviceId, device => device.Id,
                       (orders, device) => new
                       {
                           OrderId = orders.OrderId,
                           OrderKods = orders.OrderKods,
                           Part = orders.Part,
                           User = orders.User,
                           Device = new Device
                           {
                               Client = new Client
                               {
                                   Id = device.Client.Id,
                                   Name = device.Client.Name,
                                   Surname = device.Client.Surname,
                                   Phone = device.Client.Phone,
                                   PassportData = device.Client.PassportData
                               },

                               Id = device.Id,
                               ManufactureDate = device.ManufactureDate,
                               DeviceType = device.DeviceType,
                               PhoneMarka = device.PhoneMarka,
                               SerialNumber = device.SerialNumber,
                               PhoneModel = device.PhoneModel
                           },
                           OrderStatus = orders.OrderStatus,
                           OrderType = orders.OrderType,
                           SparePhone = orders.SparePhone,
                           BeginDate = orders.BeginDate,
                           Work = orders.Work
                       });
                allOrders = new List<OrderModel>();
                foreach (var order in orderList.GroupBy(arg => arg.OrderKods))
                {
                    var orderItem = new OrderModel { OrderKod = order.Key };

                    foreach (var item in order)
                    {
                        orderItem.Price += item.Work.Price + item.Part.Price;
                        orderItem.Parts.Add(item.Part);
                        orderItem.Works.Add(item.Work);
                    }
                    var firstOrderItem = order.First();
                    orderItem.StartData = firstOrderItem.BeginDate;
                    orderItem.OrderType = (OrderType)firstOrderItem.OrderType;
                    orderItem.OrderStatus = (OrderStatus)firstOrderItem.OrderStatus;
                    orderItem.Device = firstOrderItem.Device;
                    allOrders.Add(orderItem);
                }
            }
            return allOrders.Count > 0 ? allOrders : null;
        }
        public List<OrderModel> GetAllExaminates()//TODO
        {
            List<OrderModel> allOrders = null;
            using (var database = new MobiDocContext())
            {
                var clients = database.ClientsDbs;
                var devices = database.DevicesDbs;
                var orderTable = database.Orders;
                var works = database.WorksDbs;
                //var parts = database.PartsDbs;
                var users = database.UsersDbs;

                var deviceWithClient = clients.Join(devices, client => client.Id, device => device.ClientId, (clientsDbs, devicesDbs) =>
                  new
                  {
                      Client = new Client
                      {
                          Id = clientsDbs.Id,
                          Name = clientsDbs.Name,
                          Surname = clientsDbs.Surname,
                          Phone = clientsDbs.Phone,
                          PassportData = clientsDbs.PassportData
                      },

                      Id = devicesDbs.Id,
                      ManufactureDate = devicesDbs.ManufactureDate,
                      DeviceType = devicesDbs.DeviceType,
                      PhoneMarka = devicesDbs.PhoneMarka,
                      SerialNumber = devicesDbs.SerialNumber,
                      PhoneModel = devicesDbs.PhoneModel
                  });

                var orderWork = orderTable.Join(works, orders => orders.WorkId, worksDbs => worksDbs.Id,
                    (orders, worksDbs) => new
                    {
                        OrderId = orders.Id,
                        OrderStatus = orders.OrderStatus,
                        OrderKods = orders.OrderKods,
                        PartId = orders.PartId,
                        UserId = orders.UserId,
                        DeviceId = orders.DeviceId,
                        OrderType = orders.OrderType,
                        SparePhone = orders.SparePhone,
                        BeginDate = orders.BeginDate,
                        Work = new WorkModel
                        {
                            Id = worksDbs.Id,
                            Title = worksDbs.Title,
                            ExecutionTime = worksDbs.ExecutionTime,
                            Price = worksDbs.Price
                        }
                    });
                //var orderWorkPart = orderWork.Join(parts, orders => orders.PartId, partDbs => partDbs.Id,
                //    (orders, part) => new
                //    {
                //        OrderId = orders.OrderId,
                //        OrderKods = orders.OrderKods,
                //        Part = new Part
                //        {
                //            Id = part.Id,
                //            Title = part.Title,
                //            Model = part.Model,
                //            Marka = part.Marka,
                //            Price = part.Price,
                //            Count = part.Count,
                //        },
                //        UserId = orders.UserId,
                //        DeviceId = orders.DeviceId,
                //        OrderStatus = orders.OrderStatus,
                //        OrderType = orders.OrderType,
                //        SparePhone = orders.SparePhone,
                //        BeginDate = orders.BeginDate,
                //        Work = orders.Work
                //    });
                var orderWorkPartUser = orderWork.Join(users, orders => orders.UserId, user => user.Id,
                   (orders, user) => new
                   {
                       OrderId = orders.OrderId,
                       OrderKods = orders.OrderKods,
                       User = new User
                       {
                           Id = user.Id,
                           Name = user.Name,
                           Surname = user.Surname,
                           Login = user.Login,
                           Password = user.Password,
                           IsAdmin = user.IsAdmin,
                       },
                       DeviceId = orders.DeviceId,
                       OrderStatus = orders.OrderStatus,
                       OrderType = orders.OrderType,
                       SparePhone = orders.SparePhone,
                       BeginDate = orders.BeginDate,
                       Work = orders.Work
                   });
                var orderList = orderWorkPartUser.Join(deviceWithClient, orders => orders.DeviceId, device => device.Id,
                       (orders, device) => new
                       {
                           OrderId = orders.OrderId,
                           OrderKods = orders.OrderKods,
                           User = orders.User,
                           Device = new Device
                           {
                               Client = new Client
                               {
                                   Id = device.Client.Id,
                                   Name = device.Client.Name,
                                   Surname = device.Client.Surname,
                                   Phone = device.Client.Phone,
                                   PassportData = device.Client.PassportData
                               },

                               Id = device.Id,
                               ManufactureDate = device.ManufactureDate,
                               DeviceType = device.DeviceType,
                               PhoneMarka = device.PhoneMarka,
                               SerialNumber = device.SerialNumber,
                               PhoneModel = device.PhoneModel
                           },
                           OrderStatus = orders.OrderStatus,
                           OrderType = orders.OrderType,
                           SparePhone = orders.SparePhone,
                           BeginDate = orders.BeginDate,
                           Work = orders.Work
                       });
                allOrders = new List<OrderModel>();
                foreach (var order in orderList.Where(arg => arg.OrderType == (int)OrderType.Review).GroupBy(arg => arg.OrderKods))
                {
                    var orderItem = new OrderModel { OrderKod = order.Key };

                    orderItem.Price = 50;
                    var firstOrderItem = order.First();
                    orderItem.StartData = firstOrderItem.BeginDate;
                    orderItem.OrderType = (OrderType)firstOrderItem.OrderType;
                    orderItem.OrderStatus = (OrderStatus)firstOrderItem.OrderStatus;
                    orderItem.Device = firstOrderItem.Device;
                    allOrders.Add(orderItem);
                }
            }
            return allOrders.Count > 0 ? allOrders : null;
        }
        public List<OrderModel> GetOrdersByClient(Client client)
        {
            List<OrderModel> orders = null;
            using (var database = new MobiDocContext())
            {
                var devices = GetDevicesByClientId(client.Id).Select(device => device.Id);
                orders = database.Orders.Where(order => devices.Contains(order.DeviceId)).Select(ToOrderRecord).ToList();
            }
            return orders;
        }
        public List<Device> GetDevicesByClientId(int clientId)
        {
            List<Device> devicesByClient = null;
            using (var database = new MobiDocContext())
            {
                devicesByClient = database.DevicesDbs.Where(device => device.ClientId == clientId).Select(ToDevice).ToList();
            }
            return devicesByClient;
        }
        public List<Device> GetAllDevices()
        {
            List<Device> devices = null;
            using (var database = new MobiDocContext())
            {
                devices = database.DevicesDbs.Select(dbClient => dbClient.ToDevice()).ToList();
            }
            return devices;
        }
        public List<Client> GetClientsList()
        {
            List<Client> clients = null;
            using (var database = new MobiDocContext())
            {
                clients = database.ClientsDbs.Select(ToClient).ToList();
            }
            return clients;
        }


        #region Extension

        private Device JoinToDevice(ClientsDbs clientsDbs, DevicesDbs devicesDbs)
        {
            return new Device
            {
                Client = new Client
                {
                    Id = clientsDbs.Id,
                    Name = clientsDbs.Name,
                    Surname = clientsDbs.Surname,
                    Phone = clientsDbs.Phone,
                    PassportData = clientsDbs.PassportData
                },

                Id = devicesDbs.Id,
                ManufactureDate = devicesDbs.ManufactureDate,
                DeviceType = devicesDbs.DeviceType,
                PhoneMarka = devicesDbs.PhoneMarka,
                SerialNumber = devicesDbs.SerialNumber,
                PhoneModel = devicesDbs.PhoneModel
            };
        }

        public Device ToDevice(DevicesDbs device)
        {
            return new Device
            {
                Id = device.Id,
                // ClientId = device.ClientId,
                DeviceType = device.DeviceType,
                ManufactureDate = device.ManufactureDate,
                PhoneMarka = device.PhoneMarka,
                PhoneModel = device.PhoneModel,
                SerialNumber = device.SerialNumber
            };
        }
        private OrderModel ToOrderRecord(Orders order)
        {





            return new OrderModel();

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
                //Devices = new Devices(GetDevicesByClientId(client.Id))
            };
        }

        #endregion
    }





}
