using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataService;
using HtmlAgilityPack;

namespace UnitTest
{
    [TestClass]
    public class AgriculturalProductsDataServiceTest
    {
        public AgriculturalProductsDataService service { get; set; }

        [TestMethod]
        public void GetNodesTest()
        {
            service = new DataService.AgriculturalProductsDataService();
            HtmlNodeCollection nodes = service.GetNodes();
            service.ParseNodes(nodes);
        }

        [TestMethod]
        public void SaveNodeTest()
        {
            service = new DataService.AgriculturalProductsDataService();
            var nodes = service.GetNodes("http://www.xinfadi.com.cn/marketanalysis/0/list/13455.shtml");
            service.Save(nodes);
        }

        [TestMethod]
        public void SaveNodesTest()
        {
            service = new DataService.AgriculturalProductsDataService();
            var nodes = service.GetNodes();
            service.Save(nodes);
        }

        [TestMethod]
        public void RunTest()
        {
            service = new DataService.AgriculturalProductsDataService();
            service.Run();
        }
    }
}
