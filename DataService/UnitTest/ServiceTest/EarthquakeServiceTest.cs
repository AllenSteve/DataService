using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataService;

namespace UnitTest
{
    [TestClass]
    public class EarthquakeServiceTest
    {
        public EarthquakeService service { get; set; }

        [TestMethod]
        public void GetNodesTest()
        {
            service = new EarthquakeService();
            var nodes = service.GetNodes();
            service.ParseList(nodes);
        }

        [TestMethod]
        public void SaveNodesTest()
        {
            service = new EarthquakeService();
            var nodes = service.GetNodes();
            service.Save(nodes);
        }
    }
}
