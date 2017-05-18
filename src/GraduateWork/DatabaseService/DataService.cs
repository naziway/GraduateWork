using DatabaseService.Extension;
using Model;
using Shared;
using Shared.Enum;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace DatabaseService
{
    public class DataService : IDataProvider
    {
        MobileDoc database = new MobileDoc();
        public User User { get; set; }

        ConverterToSystemStructure converter = new ConverterToSystemStructure();

        private int GetKodForSelling => database.Sellings.ToList().LastOrDefault()?.Kod + 1 ?? 1;
        private int GetIdForSelling => database.Sellings.ToList().LastOrDefault()?.Id + 1 ?? 1;
        private int GetKodForReview => database.Reviews.ToList().LastOrDefault()?.Kod + 1 ?? 1;
        private int GetIdForReview => database.Reviews.ToList().LastOrDefault()?.Id + 1 ?? 1;
        private int GetKodForRepair => database.Repairs.ToList().LastOrDefault()?.Kod + 1 ?? 1;
        private int GetIdForRepair => database.Repairs.ToList().LastOrDefault()?.Id + 1 ?? 1;



        public User GetUser(string login, string password)
        {
            User findUser = null;
            try
            {
                findUser = GetUsers().FirstOrDefault(user => user.Login == login && user.Password == password);
            }
            catch (Exception e)
            {
                throw new UserNotFoundException();
            }
            return findUser;
        }

        public bool AddUser(User user)//Test
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
            return database.Clients.Select(converter.Convert).ToList();
        }

        public List<Device> GetDevices()//Test
        {
            var clients = GetClients();
            var devices = database.Devices;
            var list = new List<Device>();
            foreach (var device in devices)
            {
                var item = new Device
                {
                    Id = device.Id,
                    DeviceType = device.DeviceType,
                    ManufactureDate = device.ManufactureDate,
                    Marka = device.Marka,
                    Model = device.Model,
                    SerialNumber = device.SerialNumber,
                    Client = clients.First(client => client.Id == device.ClientId)
                };
                list.Add(item);
            }

            return list;
        }

        public List<RepairDevice> GetRepairDevices()//Test
        {
            return database.RepairDevices.Select(converter.Convert).ToList();
        }

        public List<Part> GetParts()//Test
        {
            return database.Parts.Select(converter.Convert).ToList();

        }

        public List<Work> GetWorks()//Test
        {
            return database.Works.Select(converter.Convert).ToList();
        }

        public List<User> GetUsers()//Done
        {
            var userData = GetUsersDataList();
            var users = database.Users;
            var list = new List<User>();
            foreach (var user in users)
            {
                list.Add(new User
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

        public List<Device> GetDevicesByClientId(int id)//Test
        {
            return GetDevices().Where(devicee => devicee.Client.Id == id).ToList();
        }

        public bool AddDevice(Device device)//Test
        {
            try
            {
                database.Devices.Add(device.Convert());
                database.SaveChanges();
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

        public bool RemoveDeviceById(Device device)
        {
            throw new NotImplementedException();
        }

        public List<Review> GetReviews()
        {
            var users = GetUsers();
            var devices = GetDevices();
            var list = new List<Review>();

            foreach (var review in database.Reviews)
            {
                var item = new Review
                {
                    Id = review.Id,
                    Kod = review.Kod,
                    OrderDate = review.OrderDate,
                    Status = (ReviewStatus)review.Status,
                    Worker = users.First(user => user.Id == review.WorkerId),
                    Device = devices.First(devicee => devicee.Id == review.DeviceId),
                    User = users.First(userr => userr.Id == review.UserId)
                };
                if (review.RepairId != null)
                    item.Repair = GetRepairs().First(repair => review.Id == review.RepairId);
                list.Add(item);
            }
            return list;
        }

        public List<Repair> GetRepairs()
        {
            var repairDevices = GetRepairDevices();
            var workers = GetUsers();
            var devices = GetDevices();
            var parts = GetParts();
            var works = GetWorks();

            var list = new List<Repair>();

            foreach (var repair in database.Repairs)
            {
                var item = new Repair
                {
                    Id = repair.Id,
                    IsWarranty = repair.IsWarranty,
                    Kod = repair.Kod,
                    OrderDate = repair.OrderDate,
                    Status = (RepairStatus)repair.Status,
                    Worker = workers.First(user => user.Id == repair.WorkerId),
                    Device = devices.First(devicee => devicee.Id == repair.DeviceId),
                    Work = works.First(work => work.Id == repair.WorkId)
                };
                if (repair.PartId != null)
                    item.Part = parts.First(part => part.Id == repair.PartId);
                if (repair.RepairDeviceId != null)
                    item.RepairDevice = repairDevices.First(device => device.Id == repair.RepairDeviceId);

                list.Add(item);
            }
            return list;
        }

        public List<Review> GetReviewById(int id)//Test
        {
            return GetReviews().Where(review => review.Id == id).ToList();
        }

        public List<Review> GetReviewsByClientId(int clientId)//Test
        {
            return GetReviews().Where(review => review.Device.Client.Id == clientId).ToList();
        }

        public List<Review> GetReviewsByUserId(int userId)//Test
        {
            return GetReviews().Where(review => review.User.Id == userId).ToList();
        }

        public List<Review> GetReviewsByWorkerId(int workerId)//Test
        {
            return GetReviews().Where(review => review.Worker.Id == workerId).ToList();
        }

        public List<Repair> GetRepairsByClientId(int clientId)//Test
        {
            return GetRepairs().Where(repair => repair.Device.Client.Id == clientId).ToList();
        }

        public List<Repair> GetReviewsByKod(int kod)//Test
        {
            return GetRepairs().Where(repair => repair.Kod == kod).ToList();
        }

        public List<Repair> GetRepairsByWorkerId(int workerId)//Test
        {
            return GetRepairs().Where(repair => repair.Worker.Id == workerId).ToList();
        }

        //Test//Test//Test//Test//Test//Test//Test//Test//Test
        public async Task<int> ChangeSellingsStatusByKod(int kod, SellingStatus newStatus)//Test
        {
            await database.Sellings.Where(sellings => sellings.Kod == kod)
                  .ForEachAsync(sellings => sellings.Status = (int)newStatus);
            try
            {
                return await database.SaveChangesAsync();
            }
            catch (Exception)
            {
                return await Task.FromResult(0);
            }
        }

        public async Task<int> AddSellings(List<Selling> sellings)//DONE
        {
            int kod = GetKodForSelling;
            int id = GetIdForSelling;
            try
            {
                foreach (var selling in sellings)
                {
                    database.Sellings.Add(selling.Convert(id++, kod));
                }
                var a = await database.SaveChangesAsync();
                return a;
            }
            catch (Exception)
            {
                return await Task.FromResult(-1);
            }

        }
        public Review AddReview(Review review)//Done
        {
            int kod = GetKodForReview;
            int id = GetIdForReview;
            review.User = User;
            try
            {
                var modReview = review.Convert(id, kod);
                database.Reviews.Add(modReview);
                database.SaveChanges();
                review.Kod = modReview.Kod;
                review.Id = modReview.Id;
                return review;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public bool AddRepairs(List<Repair> repairs)//Test
        {
            int kod = GetKodForRepair;
            int id = GetIdForRepair;
            try
            {
                foreach (var repair in repairs)
                {
                    database.Repairs.Add(repair.Convert(id++, kod));
                }
                database.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<int> ChangeReviewStatusById(int id, ReviewStatus newStatus)//Test
        {
            await database.Reviews.Where(reviews => reviews.Id == id)
                   .ForEachAsync(reviews =>
                   {
                       reviews.Status = (int)newStatus;
                   });
            try
            {
                return await database.SaveChangesAsync();
            }
            catch (Exception)
            {
                return await Task.FromResult(-1);
            }
        }

        public async Task<int> ChangeReviewStatusAndSetRefToRepairById(int id, ReviewStatus newStatus, int kodRepair)//Test
        {
            await database.Reviews.Where(reviews => reviews.Id == id)
                   .ForEachAsync(reviews =>
                   {
                       reviews.Status = (int)newStatus;
                       reviews.RepairId = kodRepair;
                   });
            try
            {
                return await database.SaveChangesAsync();
            }
            catch (Exception)
            {
                return await Task.FromResult(-1);
            }
        }


        public async Task<int> ChangeRepairsStatusByKod(int id, RepairStatus newStatus)//Test
        {
            await database.Repairs.Where(repairs => repairs.Id == id)
                  .ForEachAsync(repairs =>
                  {
                      repairs.Status = (int)newStatus;
                  });
            try
            {
                return await database.SaveChangesAsync();
            }
            catch (Exception)
            {
                return await Task.FromResult(-1);
            }
        }
    }





}
