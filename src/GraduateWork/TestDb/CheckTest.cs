using DatabaseService;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Linq;
using ViewModel;

namespace TestDb
{
    [TestClass]
    public class CheckTest
    {

        [TestMethod]
        public void CreateSellCheckTest()
        {
            CheckManager checkManager = new CheckManager(new DataService());

            Process.Start(checkManager.CreateSellCheck(new DataService().GetSellingsByClientId(1).Take(10).ToList()));
        }
        [TestMethod]
        public void CreateRepairCheckTest()
        {
            CheckManager checkManager = new CheckManager(new DataService());

            Process.Start(checkManager.CreateRepairCheck(new DataService().GetRepairsByKod(1)));
        }
        [TestMethod]
        public void CreateReviewCheckTest()
        {

            CheckManager checkManager = new CheckManager(new DataService());

            Process.Start(checkManager.CreateReviewCheck(new DataService().GetReviewById(1).First()));

        }

    }
}