using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ServiceModel
{
    public class Earthquake
    {
        /// <summary>
        /// 地震等级
        /// </summary>
        public string Scale { get; set; }
        
        /// <summary>
        /// 地震发生时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// 纬度
        /// </summary>
        public string Logitude { get; set; }

        /// <summary>
        /// 深度
        /// </summary>
        public string Depth { get; set; }

        /// <summary>
        /// 参考位置
        /// </summary>
        public string Position { get; set; }
    }
}
