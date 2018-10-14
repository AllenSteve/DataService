using DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarthquakeParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new EarthquakeService();
            var nodes = service.GetNodes();
            service.Save(nodes);
        }
    }
}
