using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataService;
using Infrastructure;

namespace UnitTest
{
    [TestClass]
    public class EarthquakeDataServiceTest
    {
        public EarthquakeDataService service { get; set; }

        [TestMethod]
        public void GetNodesTest()
        {
            service = new DataService.EarthquakeDataService();
            var nodes = service.GetNodes();
            service.ParseNodes(nodes);
        }

        [TestMethod]
        public void SaveNodesTest()
        {
            service = new DataService.EarthquakeDataService();
            var nodes = service.GetNodes();
            service.Save(nodes);
        }

        [TestMethod]
        public void GetTest()
        {
            service = new DataService.EarthquakeDataService();
            var nodes = service.Request();
            var json = JsonHelper.DeserializeJsonToObject<Object>(nodes);
        }
    }
}
