using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ServiceModel
{
    public class Earthquake
    {
        public string Scale { get; set; }
        public DateTime CreateTime { get; set; }
        public string Latitude { get; set; }
        public string Logitude { get; set; }
        public string Depth { get; set; }
        public string Position { get; set; }
    }
}
