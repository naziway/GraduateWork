using DatabaseService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestDb
{
    [TestClass]
    public class SellingTest
    {
        [TestMethod]
        public void CheckGetAllSelling()
        {
            DatabaseService.DataService service = new DataService();

            var sellings = service.GetSellings();
            Assert.AreNotEqual(sellings.Count, 0);

        }

    }
}