using System;
using System.Collections.Generic;
using DatabaseService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Enum;

namespace TestDb
{
    [TestClass]
    public class DbTest
    {
        [TestMethod]
        public void CheckGetAllSelling()
        {
            DatabaseService.DataService service = new DataService();

            var sellings = service.GetSellings();
            Assert.AreNotEqual(sellings.Count, 0);

        }
        [TestMethod]
        public void CheckGetReview()
        {
            DatabaseService.DataService service = new DataService();

            var reviews = service.GetReviews();
            Assert.AreNotEqual(reviews.Count, 0);

        }
        [TestMethod]
        public void CheckGetRepairs()
        {
            DatabaseService.DataService service = new DataService();

            var repairs = service.GetRepairs();
            Assert.AreNotEqual(repairs.Count, 0);

        }
        [TestMethod]
        public void AddNewSellings()
        {
            DatabaseService.DataService service = new DataService();

            var users = service.GetUsers();
            var clients = service.GetClients();
            var part = service.GetParts();

            var sellings = new List<Selling>();

            sellings.Add(new Selling
            {
                OrderDate = DateTime.Now,
                Client = clients[1],
                Part = part[0],
                Status = SellingStatus.New,
                User = users[1]
            });
            sellings.Add(new Selling
            {
                OrderDate = DateTime.Now,
                Client = clients[1],
                Part = part[2],
                Status = SellingStatus.New,
                User = users[1]
            });
            sellings.Add(new Selling
            {
                OrderDate = DateTime.Now,
                Client = clients[1],
                Part = part[1],
                Status = SellingStatus.New,
                User = users[1]
            });

            var result = service.AddSellings(sellings);


            //var sellings = service.GetSellings();
            Assert.AreNotEqual(result, false);

        }

    }
}