using Model;
using Shared.Enum;
using System;
using System.Collections.Generic;

namespace DatabaseService
{
    public interface IDataProvider
    {
        User GetUser(string login, string password);
        bool AddUser(Userr user);

        #region GetFullList

        List<Client> GetClients();
        List<Devicee> GetDevices();
        List<RepairDevice> GetRepairDevices();
        List<Selling> GetSellings();
        List<Review> GetReviews();
        List<Repair> GetRepairs();
        List<Part> GetParts();
        List<Work> GetWorks();
        List<Userr> GetUsers();
        List<UserData> GetUsersDataList();

        #endregion

        #region Device Data
        List<Devicee> GetDevicesByClientId(int id);
        bool AddDevice(Devicee device);
        bool RemoveDeviceById(Devicee device);
        #endregion

        #region Repair Device Data
        RepairDevice GetRepairDevicesById(int id);
        bool AddRepairDevice(RepairDevice device);

        #endregion

        #region Client Data
        bool AddClient(Client client);
        Client GetClientByDeviceId(int id);

        #endregion

        #region Personal Data
        UserData GetUserDataById(int id);
        bool AddPersonalData(UserData userData);

        #endregion

        #region Part Data
        Part GetPartById(int id);
        bool AddPart(Part part);

        #endregion

        #region Work Data
        Work GetWorkById(int id);
        bool AddWork(Work work);

        #endregion

        #region Selling

        List<Selling> GetSellingByKod(int kod);
        List<Selling> GetSellingsByClientId(int clientId);
        List<Selling> GetSellingsByUserId(int userId);
        bool AddSellings(List<Selling> sellings);
        bool ChangeSellingsStatusByKod(int kod, SellingStatus newStatus);

        #endregion

        #region Review

        Review GetReviewById(int id);
        List<Review> GetReviewsByClientId(int clientId);
        List<Review> GetReviewsByUserId(int userId);
        List<Review> GetReviewsByWorkerId(int workerId);
        bool AddReview(Review review);
        bool ChangeReviewStatusById(int id, ReviewStatus newStatus);
        bool ChangeReviewStatusAndSetRefToRepairById(int id, ReviewStatus newStatus, int kodRepair);

        #endregion

        #region Repair

        List<Repair> GetRepairsByClientId(int clientId);
        List<Repair> GetReviewsByKod(int kod);
        List<Repair> GetRepairsByWorkerId(int workerId);
        bool AddRepairs(List<Repair> repairs);
        bool ChangeRepairsStatusByKod(int id, RepairStatus newStatus);

        #endregion

    }

    public class Devicee
    {
        public int Id { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int DeviceType { get; set; }
        public string SerialNumber { get; set; }
        public System.DateTime ManufactureDate { get; set; }
        public Client Client { get; set; }
    }

    public class Userr
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public UserType UserType { get; set; }
        public UserData UserData { get; set; }
    }

    public class UserData
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PasportData { get; set; }
        public System.DateTime BirthDate { get; set; }
    }

    public class Work
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        public System.TimeSpan Time { get; set; }
    }

    public class Repair
    {
        public int Id { get; set; }
        public int Kod { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public bool IsWarranty { get; set; }
        public RepairDevice RepairDevice { get; set; }
        public User Worker { get; set; }
        public Device Device { get; set; }
        public Part Part { get; set; }
        public Work Work { get; set; }
    }

    public class Review
    {
        public int Id { get; set; }
        public int Kod { get; set; }
        public DateTime OrderDate { get; set; }
        public int Status { get; set; }
        public User User { get; set; }
        public User Worker { get; set; }
        public Device Device { get; set; }
        public Repair Repair { get; set; }

    }

    public class Selling
    {
        public int Id { get; set; }
        public int Kod { get; set; }
        public DateTime OrderDate { get; set; }
        public SellingStatus Status { get; set; }
        public Userr User { get; set; }
        public Client Client { get; set; }
        public Part Part { get; set; }

    }

    public class RepairDevice
    {
        public int Id { get; set; }
        public string Marka { get; set; }
        public string Model { get; set; }
        public int DeviceType { get; set; }
        public string SerialNumber { get; set; }
        public System.DateTime ManufactureDate { get; set; }
        public int Count { get; set; }
    }
}