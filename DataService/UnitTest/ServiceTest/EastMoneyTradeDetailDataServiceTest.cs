using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataService;
using DataModel.ServiceModel;

namespace UnitTest.ServiceTest
{
    [TestClass]
    public class EastMoneyTradeDetailDataServiceTest
    {
        public EastMoneyTradeDetailDataService service { get; set; }

        [TestMethod]
        public void GetListTest()
        {
            StockSH stock = new StockSH(null);
            stock.StockCode = "600519";
            stock.Date = DateTime.Parse("2018-11-02");
            service = new EastMoneyTradeDetailDataService();
            var lst = service.GetTradeDetailByStock(stock);
        }

        [TestMethod]
        public void SaveListTest()
        {
            StockSH stock = new StockSH(null);
            stock.StockCode = "600519";
            stock.Date = DateTime.Parse("2018-11-02");
            service = new EastMoneyTradeDetailDataService();
            var lst = service.GetTradeDetailByStock(stock);
            service.SaveList(lst);
        }

        [TestMethod]
        public void SaveDailyTradeDetailTest()
        {
            DateTime Date = DateTime.Parse("2018-11-02");
            service = new EastMoneyTradeDetailDataService();
            var stocks = service.GetStocksByDate(Date);
            foreach(var stock in stocks)
            {
                var lst = service.GetTradeDetailByStock(stock);
                service.SaveList(lst);
            }
        }
    }
}
