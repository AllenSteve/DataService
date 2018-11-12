using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataService;

namespace UnitTest.ServiceTest
{
    [TestClass]
    public class Stock_SinaDataServiceTest
    {
        public Stock_SinaDataService service { get; set; }

        public Stock_SinaDataServiceTest()
        {
            service = new Stock_SinaDataService();
        }

        [TestMethod]
        public void TestMethod1()
        {
            var lst = service.GetStocks("2018-11-12");
        }
    }
}
