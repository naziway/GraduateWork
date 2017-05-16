﻿using DatabaseService.Extension;
using Model;
using Shared;
using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DatabaseService
{
    public class DataService : IDataProvider
    {
        MobileDoc database = new MobileDoc();
        ConverterToSystemStructure converter = new ConverterToSystemStructure();

        #region Old Service
        public User User { get; set; }
        public bool AddNewExaminate(Device device)
        {
            var exeminate = new Orders();

            var kodId = GetNewOrderIdAndCode();
            exeminate.Id = kodId.Item1;
            exeminate.OrderKods = kodId.Item2;
            exeminate.BeginDate = DateTime.Now;
            exeminate.OrderType = (int)OrderType.Обстеження;
            exeminate.OrderStatus = (int)OrderStatus.Активний;
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

        //public User GetUser(string login, string password)
        //{
        //    User currentUser = null;
        //    using (var database = new MobileDoc())
        //    {
        //        try
        //        {
        //            var userBd = database.Users.FirstOrDefault(user => user.Login == login && user.Password == password);
        //            currentUser = userBd?.ToUserModel();
        //        }
        //        catch (Exception e)
        //        {
        //            throw new UserNotFoundException();
        //        }
        //    }
        //    return currentUser;
        //}
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
                foreach (var order in orderList.Where(arg => arg.OrderType == (int)OrderType.Обстеження).GroupBy(arg => arg.OrderKods))
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
        //public List<OrderModel> GetOrdersByClient(Client client)
        //{
        //    List<OrderModel> orders = null;
        //    using (var database = new MobiDocContext())
        //    {
        //        var devices = GetDevicesByClientId(client.Id).Select(device => device.Id);
        //        orders = database.Orders.Where(order => devices.Contains(order.DeviceId)).Select(ToOrderRecord).ToList();
        //    }
        //    return orders;
        //}
        //public List<Device> GetDevicesByClientId(int clientId)
        //{
        //    List<Device> devicesByClient = null;
        //    using (var database = new MobiDocContext())
        //    {
        //        devicesByClient = database.DevicesDbs.Where(device => device.ClientId == clientId).Select(ToDevice).ToList();
        //    }
        //    return devicesByClient;
        //}
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
        #endregion

        public User GetUser(string login, string password)
        {
            User currentUser = null;
            try
            {
                var userBd = database.Users.FirstOrDefault(user => user.Login == login && user.Password == password);
                currentUser = userBd?.ToUserModel();
            }
            catch (Exception e)
            {
                throw new UserNotFoundException();
            }
            return currentUser;
        }

        public bool AddUser(Userr user)//Test
        {
            try
            {
                database.Users.Add(user.Convert());
                database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public List<Client> GetClients()//Test
        {
            return database.Clients.Select(converter.ToClient).ToList();
        }

        public List<Devicee> GetDevices()//Test
        {
            var clients = GetClients();
            var devices = database.Devices;
            var list = devices.Select(device => new Devicee
            {
                Id = device.Id,
                DeviceType = device.DeviceType,
                ManufactureDate = device.ManufactureDate,
                Marka = device.Marka,
                Model = device.Model,
                SerialNumber = device.SerialNumber,
                Client = clients.First(client => client.Id == device.ClientId)
            }).ToList();

            return list;
        }

        public List<RepairDevice> GetRepairDevices()//Test
        {
            return database.RepairDevices.Select(converter.ToDevice).ToList();
        }

        public List<Part> GetParts()//Test
        {
            return database.Parts.Select(converter.ToPart).ToList();

        }

        public List<Work> GetWorks()//Test
        {
            return database.Works.Select(converter.Convert).ToList();
        }

        public List<Userr> GetUsers()//Done
        {
            var userData = GetUsersDataList();
            var users = database.Users;
            var list = new List<Userr>();
            foreach (var user in users)
            {
                list.Add(new Userr
                {
                    Id = user.Id,
                    Login = user.Login,
                    Password = user.Password,
                    RegistrationDate = user.RegistrationDate,
                    UserType = (UserType)user.UserType,
                    UserData = userData.First(data => data.Id == user.PersonalDataId)
                });
            }
            return list;
        }

        public List<UserData> GetUsersDataList()//Test
        {
            return database.PersonalData.Select(converter.Convert).ToList();
        }

        public List<Devicee> GetDevicesByClientId(int id)//Test
        {
            return GetDevices().Where(devicee => devicee.Client.Id == id).ToList();
        }

        public bool AddDevice(Devicee device)//Test
        {
            try
            {
                database.Devices.Add(device.Convert());
                database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public RepairDevice GetRepairDevicesById(int id)//Test
        {
            return GetRepairDevices().First(device => device.Id == id);
        }

        public bool AddRepairDevice(RepairDevice device)//Test
        {
            try
            {
                database.RepairDevices.Add(device.Convert());
                database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool AddClient(Client client)//Test
        {
            try
            {
                database.Clients.Add(client.Convert());
                database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public Client GetClientByDeviceId(int id)//Test
        {
            return GetClients().First(client => client.Id == id);
        }

        public UserData GetUserDataById(int id)//Test
        {
            return GetUsersDataList().First(data => data.Id == id);
        }

        public bool AddPersonalData(UserData userData)//Test
        {
            try
            {
                database.PersonalData.Add(userData.Convert());
                database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public Part GetPartById(int id)//Test
        {
            return GetParts().First(part => part.Id == id);
        }

        public bool AddPart(Part part)//Test
        {
            try
            {
                database.Parts.Add(part.Convert());
                database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public Work GetWorkById(int id)//Test
        {
            return GetWorks().First(work => work.Id == id);
        }

        public bool AddWork(Work work)//Test
        {
            try
            {
                database.Works.Add(work.Convert());
                database.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public List<Selling> GetSellings()//Done
        {
            var users = GetUsers();
            var clients = GetClients();
            var part = GetParts();

            var list = new List<Selling>();

            foreach (var sellingse in database.Sellings)
            {
                var selling = new Selling
                {
                    Id = sellingse.Id,
                    Part = part.First(p => p.Id == sellingse.PartId),
                    User = users.First(p => p.Id == sellingse.UserId),
                    Client = clients.First(p => p.Id == sellingse.ClientId),
                    Kod = sellingse.Kod,
                    OrderDate = sellingse.OrderDate,
                    Status = (SellingStatus)sellingse.Status
                };
                list.Add(selling);
            }
            return list;
        }

        public List<Selling> GetSellingByKod(int kod)//Test
        {
            return GetSellings().Where(selling => selling.Kod == kod).ToList();
        }

        public List<Selling> GetSellingsByClientId(int clientId)//Test
        {
            return GetSellings().Where(selling => selling.Client.Id == clientId).ToList();
        }

        public List<Selling> GetSellingsByUserId(int userId)//Test
        {
            return GetSellings().Where(selling => selling.User.Id == userId).ToList();
        }

        public bool ChangeSellingsStatusByKod(int kod, SellingStatus newStatus)//Test
        {
            database.Sellings.Where(sellings => sellings.Kod == kod)
                .ForEachAsync(sellings => sellings.Status = (int)newStatus);
            try
            {
                database.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool RemoveDeviceById(Devicee device)
        {
            throw new NotImplementedException();
        }

        public List<Review> GetReviews()
        {
            throw new NotImplementedException();
        }

        public List<Repair> GetRepairs()
        {
            throw new NotImplementedException();
        }

        public bool AddSellings(List<Selling> sellings)
        {
            throw new NotImplementedException();
        }

        public Review GetReviewById(int id)
        {
            throw new NotImplementedException();
        }

        public List<Review> GetReviewsByClientId(int clientId)
        {
            throw new NotImplementedException();
        }

        public List<Review> GetReviewsByUserId(int userId)
        {
            throw new NotImplementedException();
        }

        public List<Review> GetReviewsByWorkerId(int workerId)
        {
            throw new NotImplementedException();
        }

        public bool AddReview(Review review)
        {
            throw new NotImplementedException();
        }

        public bool ChangeReviewStatusById(int id, ReviewStatus newStatus)
        {
            throw new NotImplementedException();
        }

        public bool ChangeReviewStatusAndSetRefToRepairById(int id, ReviewStatus newStatus, int kodRepair)
        {
            throw new NotImplementedException();
        }

        public List<Repair> GetRepairsByClientId(int clientId)
        {
            throw new NotImplementedException();
        }

        public List<Repair> GetReviewsByKod(int kod)
        {
            throw new NotImplementedException();
        }

        public List<Repair> GetRepairsByWorkerId(int workerId)
        {
            throw new NotImplementedException();
        }

        public bool AddRepairs(List<Repair> repairs)
        {
            throw new NotImplementedException();
        }

        public bool ChangeRepairsStatusByKod(int id, RepairStatus newStatus)
        {
            throw new NotImplementedException();
        }
    }





}
