using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataService;

namespace UnitTest.ServiceTest
{
    [TestClass]
    public class StockTradeDetailResolverTest
    {
        public StockTradeDetailResolver service { get; set; }

        [TestMethod]
        public void CreateMySQLTablesTest()
        {
            service = new StockTradeDetailResolver();
            var files = service.GetFiles();
            service.CreateMysqlTables();
        }
    }
}
