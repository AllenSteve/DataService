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
        public void ParseAQITest()
        {
            service = new AirQualityIndexDataService();
            var cityLst = service.GetCityList();
            var cityPage = cityLst.FirstOrDefault();
            Assert.IsNotNull(cityPage);
            var monitors = service.ParserAQI(cityPage);
            var all = service.GetAQIList();

        }
    }
}
