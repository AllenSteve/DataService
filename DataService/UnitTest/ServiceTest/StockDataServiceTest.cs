using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure;
using DataModel.ServiceModel;
using DataService;

namespace UnitTest.ServiceTest
{
    /// <summary>
    /// StockDataService 的摘要说明
    /// </summary>
    [TestClass]
    public class StockDataServiceTest
    {
        private StockDataService service { get; set; }

        public StockDataServiceTest()
        {
            service = new StockDataService();
        }

        [TestMethod]
        public void GetStocksByPageTest()
        {
            string szUrl = "http://www.szse.cn/api/report/ShowReport/data?SHOWTYPE=JSON&CATALOGID=1110x&TABKEY=tab1&PAGENO=3";
            var lst = service.GetSZStocksByPage(szUrl);
        }

        [TestMethod]
        public void GetStockListCountTest()
        {
            int count = service.GetStockSZListCount();
        }

        [TestMethod]
        public void GetStockListTest()
        {
            string str = "<a href='javascript:void(0);' a-back=1  a-param='/ShowReport/data?SHOWTYPE=JSON&CATALOGID=1815_stock_child_nm&TABKEY=tab1&txtDm=002589'>查看</a>";
            str = str.Truncate("a-param='", "'>查看");
            var lst = service.GetStockSZList();
            int count = lst.Count;
            service.SaveStock(lst);
        }

        [TestMethod]
        public void SaveStockSZListTest()
        {
            var lst = service.GetStockSZList();
            int count = lst.Count;
            service.SaveStock(lst);
        }

        [TestMethod]
        public void SaveStockSHListTest()
        {
            var lst = service.GetSHStocksByPage();
            int count = lst.Count;
            service.SaveStockList(lst);
        }
    }
}
