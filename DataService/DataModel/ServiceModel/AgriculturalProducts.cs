using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ServiceModel
{
    public class AgriculturalProducts : IDomainModel
    {
        public long Id { get; set; }
        public string ProductName { get; set; }
        public string LowPrice { get; set; }
        public string AveragePrice { get; set; }
        public string HighPrice { get; set; }
        public string Category { get; set; }
        public string Unit { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
