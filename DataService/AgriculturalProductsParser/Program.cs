using DataService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgriculturalProductsParser
{
    class Program
    {
        static void Main(string[] args)
        {
            new AgriculturalProductsDataService().Run();
        }
    }
}
