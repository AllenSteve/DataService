using DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            //new EarthquakeDataService().Run();
            //new AgriculturalProductsDataService().Run();
            new AirQualityIndexDataService().Run();
        }
    }
}
