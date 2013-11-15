using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using UnitTestExample;
using System.Collections.Generic;
using System.Linq;

namespace MockUnitTest
{
    [TestClass]
    public class UnitTest
    {
        List<Product> _products = new List<Product>();

        [TestInitialize]
        public void Initialize()
        {
            _products = new List<Product>()
            {
                new Product (){GTIN = "1",Merchant = "Amazon.com", PriceAmount = 0,ProductApiStep = "upc"},
                new Product (){GTIN = "2",Merchant = "Amazon.com", PriceAmount = 25,ProductApiStep = "upc"},
                new Product (){GTIN = "3",Merchant = "Amazon.com", PriceAmount = 29,ProductApiStep = "upc"},
                new Product (){GTIN = "4",Merchant = "BestBuy", PriceAmount = 30,ProductApiStep = "text"},
                new Product (){GTIN = "5",Merchant = "BestBuy", PriceAmount = 26,ProductApiStep = "upc"},
                new Product (){GTIN = "6",Merchant = "BestBuy", PriceAmount = 27,ProductApiStep = "upc"},
                new Product (){GTIN = "7",Merchant = "BestBuy", PriceAmount = 28,ProductApiStep = "upc"},
                new Product (){GTIN = "8",Merchant = "BestBuy", PriceAmount = 29,ProductApiStep = "upc"},
                new Product (){GTIN = "9",Merchant = "BestBuy", PriceAmount = 30,ProductApiStep = "upc"},
            };
        }

        [TestMethod]
        public void TestWithMock()
        {
            //Dummy
            var mockLog = new Mock<ILog>();

            //Mock
            var mockConfig = new Mock<IFilterConfiguration>();

            mockConfig.Setup(c => c.NumMaxPerMerchant).Returns(3);
            mockConfig.Setup(c => c.ShowByTextIfExistGtin).Returns(false);
            mockConfig.Setup(c => c.ShowWithoutPrice).Returns(true);

            WhereToBuyFilterService filterService = new WhereToBuyFilterService(mockConfig.Object, mockLog.Object);

            filterService.Filter(ref _products);

            //Mock verifications
            mockConfig.Verify(x => x.NumMaxPerMerchant, Times.AtLeastOnce());
            mockConfig.Verify(x => x.ShowByTextIfExistGtin, Times.AtLeastOnce());
            mockConfig.Verify(x => x.ShowWithoutPrice, Times.AtLeastOnce());

            CollectionAssert.AreEqual(_products, GetExpectedProducts());
        }

        [TestMethod]
        public void TestWithStub()
        {
            //Dummy
            var mockLog = new Mock<ILog>();

            //Stub
            var mockConfig = new Mock<IFilterConfiguration>();

            mockConfig.Setup(c => c.NumMaxPerMerchant).Returns(3);
            mockConfig.Setup(c => c.ShowByTextIfExistGtin).Returns(false);
            mockConfig.Setup(c => c.ShowWithoutPrice).Returns(true);

            WhereToBuyFilterService filterService = new WhereToBuyFilterService(mockConfig.Object, mockLog.Object);

            filterService.Filter(ref _products);
            CollectionAssert.AreEqual(GetExpectedProducts(),_products);
        }


        [TestMethod]
        public void TestWithDummies()
        {
            //Dummies
            var mockConfig = new Mock<IFilterConfiguration>();
            var mockLog = new Mock<ILog>();

            WhereToBuyFilterService filterService = new WhereToBuyFilterService(mockConfig.Object, mockLog.Object);

            Assert.IsTrue(filterService != null);
        }

        List<Product> GetExpectedProducts()
        {
            List<string> validProducts = new List<string>() {"2","3","5","6","7"};

            var ProductWithoutPrice = _products.Where(x => (!(x.PriceAmount.HasValue) || (x.PriceAmount.HasValue && x.PriceAmount.Value == 0))).ToList();

            var expectedProducts = _products.Where(p => validProducts.Contains(p.GTIN)).OrderBy(p => p.PriceAmount).ToList();

            expectedProducts.AddRange(ProductWithoutPrice);

            return expectedProducts;
        }
    }
}
