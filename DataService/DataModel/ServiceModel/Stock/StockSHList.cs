using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ServiceModel
{
    public class StockSHList
    {
        public string Date { get; set; }
        public string Time { get; set; }
        public string Total { get; set; }
        public string Begin { get; set; }
        public string End { get; set; }
        public List<object> List { get; set; }
    }
}
