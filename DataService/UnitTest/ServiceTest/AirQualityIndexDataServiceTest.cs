using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataService;
using System.Linq;

namespace UnitTest.ServiceTest
{
    [TestClass]
    public class AirQualityIndexDataServiceTest
    {
        private AirQualityIndexDataService service { get; set; }

        [TestMethod]
        public void AirQualityIndexTest()
        {
            service = new AirQualityIndexDataService();
            var cityLst = service.GetCityList();
        }

        [TestMethod]
        public void IndexOfAQITest()
        {
            string[] arr = new string[] { "更新", "1", "2", "3", "AQI指数" };
            var ret = arr.TakeWhile(o=>!o.Equals("AQI指数")).ToList();

        }

        [TestMethod]
        public void ParseAQITest()
        {
            service = new AirQualityIndexDataService();
            var cityLst = service.GetCityList();
            var cityPage = cityLst.FirstOrDefault();
            Assert.IsNotNull(cityPage);
            var monitors = service.ParseAQI(cityPage);
            var all = service.GetAQIList();
        }

        [TestMethod]
        public void RunTest()
        {
            new AirQualityIndexDataService().Run();
        }
    }
}
