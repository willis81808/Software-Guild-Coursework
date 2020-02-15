using FlooringMastery.BLL;
using FlooringMastery.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Tests
{
    [TestFixture]
    public class FileRepositoryTests
    {
        DataManager repository;
        Models.Order testOrder;

        [SetUp]
        public void Init()
        {
            repository = new DataManager(new FileDataRepository());
            testOrder = new Models.Order("Milleniam Luau", new Models.State("HI", "Hawaii", 2.15m), new Models.Product("Sand", 0.50m, 2.00m), 100);

            repository.AddOrder(new DateTime(2000, 1, 1), testOrder);
        }
        [TearDown]
        public void Cleanup()
        {
            repository.RemoveOrder(new DateTime(2000, 1, 1), 1);
        }

        [TestCase("01/01/2000", 1, true)]
        [TestCase("01/01/2000", 2, false)]
        public void OrderLookupTest(DateTime date, int orderNumber, bool expectedResult)
        {
            var result = repository.LookupOrder(date, orderNumber);
            Assert.AreEqual(result.Success, expectedResult);
        }

        [TestCase("01/01/2000", 1, true)]
        [TestCase("01/01/2000", 2, false)]
        public void OrderUpdateTest(DateTime date, int orderNumber, bool expectedResult)
        {
            var lookupResult = repository.LookupOrder(date, orderNumber);

            Assert.AreEqual(lookupResult.Success, expectedResult);

            if (lookupResult.Success)
            {
                var newOrder = CopyOrder(lookupResult.Order);
                newOrder.CustomerName = "Party Inc.";
                var result = repository.UpdateOrder(newOrder, date, orderNumber);

                lookupResult = repository.LookupOrder(date, orderNumber);

                Assert.AreEqual("Party Inc.", lookupResult.Order.CustomerName);
            }
        }

        [TestCase("01/01/2000", 1, true)]
        [TestCase("01/01/2000", 2, false)]
        public void OrderRemoveTest(DateTime date, int orderNumber, bool expectedResult)
        {
            var result = repository.RemoveOrder(date, orderNumber);
            Assert.AreEqual(result.Success, expectedResult);
        }

        private Models.Order CopyOrder(Models.Order parent)
        {
            return new Models.Order()
            {
                OrderNumber = parent.OrderNumber,
                CustomerName = parent.CustomerName,
                State = parent.State,
                TaxRate = parent.TaxRate,
                ProductType = parent.ProductType,
                Area = parent.Area,
                CostPerSquareFoot = parent.CostPerSquareFoot,
                LaborCostPerSquareFoot = parent.LaborCostPerSquareFoot,
                MaterialCost = parent.MaterialCost,
                LaborCost = parent.LaborCost,
                Tax = parent.Tax,
                Total = parent.Total
            };
        }
    }
}
