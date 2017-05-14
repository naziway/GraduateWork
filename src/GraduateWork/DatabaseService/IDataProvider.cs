﻿using Model;
using Shared.Enum;
using System.Collections.Generic;

namespace DatabaseService
{
    public interface IDataProvider
    {
        User GetUser(string login, string password);
        bool AddUser(User user);

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

        Selling GetSellingByKod(int kod);
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