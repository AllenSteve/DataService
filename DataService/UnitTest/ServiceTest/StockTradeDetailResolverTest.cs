using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataService;
using System.IO;

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

        [TestMethod]
        public void GetDetailListTest()
        {
            service = new StockTradeDetailResolver();
            var files = service.GetFiles();
            foreach (var file in files)
            {
                var lst = service.GetList(file);
                service.Save(lst, Path.GetFileNameWithoutExtension(file));
            }
        }
    }
}
