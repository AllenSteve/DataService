using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ServiceModel
{
    public class AirQualityIndex : IDomainModel
    {
        public long Id { get; set; }
        public string City { get; set; }
        public string Monitor { get; set; }
        public string AQI { get; set; }
        public string Quality { get; set; }
        public string PM25 { get; set; }
        public string PM10 { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
