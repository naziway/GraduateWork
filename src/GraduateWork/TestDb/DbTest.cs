using DatabaseService;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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

    }
}