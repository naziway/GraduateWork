using System.Collections.Generic;
using Model;

namespace DatabaseService
{
    public interface IDataProvider
    {
        User GetUser(string login, string password);

        #region GetFullList

        List<Client> GetClients();
        List<Device> GetDevices();
        List<RepairDevice> GetRepairDevices();
        List<Selling> GetSellings();
        List<Review> GetReviews();
        List<Repair> GetRepairs();
        List<Part> GetParts();
        List<Work> GetWorks();
        List<User> GetUsers();
        List<UserData> GetUsersDataList();

        #endregion

        #region Device Data
        List<Device> GetDevicesByClientId(int id);
        bool AddDevice(Device device);
        bool RemoveDeviceById(Device device);
        #endregion

        #region Client Data
        bool AddClient(Client client);
        Client GetClientByDeviceId(int id);

        #endregion

        #region Personal Data



        #endregion


    }

    public class UserData
    {
    }

    public class Work
    {
    }

    public class Repair
    {
    }

    public class Review
    {
    }

    public class Selling
    {
    }

    public class RepairDevice
    {
    }
}