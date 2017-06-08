using DatabaseService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System.Collections.Generic;
using ViewModel;

namespace TestDb
{
    [TestClass]
    public class CheckTest
    {

        [TestMethod]
        public void CreateFileTest()
        {
            List<Selling> list = new List<Selling>();

            CheckManager checkManager = new CheckManager(new DataService());


            checkManager.CreateSellCheck(new DataService().GetSellingsByClientId(1));



        }

    }
}