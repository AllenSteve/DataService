﻿using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ServiceModel
{
    public class StockSZ : IStock
    {
        public long Id { get; set; }

        /// <summary>
        /// 公司代码
        /// </summary>
        public string zqdm { get; set; }

        /// <summary>
        /// 公司简称
        /// </summary>
        public string gsjc { get; set; }

        /// <summary>
        /// 公司全称
        /// </summary>
        public string gsqc { get; set; }

        /// <summary>
        /// 所属行业
        /// </summary>
        public string sshymc { get; set; }

        /// <summary>
        /// 公司网址
        /// </summary>
        public string http { get; set; }

        /// <summary>
        /// 查询近期行情
        /// </summary>
        public string jqhq { get; set; }

        /// <summary>
        /// 公司详情
        /// </summary>
        public string gsxq { get; set; }

        public DateTime Date { get; set; }

        public string StockCode
        {
            get
            {
                return zqdm;
            }

            set
            {
                zqdm = value;
            }
        }

        public StockSZ Format()
        {
            gsjc = gsjc.GetHtmlContent("u");
            gsxq = gsjc.GetHtmlContent("a", "href");
            jqhq = jqhq.Truncate("a-param='", "'>查看");
            return this;
        }

        public StockSZ SetDate(string source)
        {
            this.Date = DateTime.Parse(source);
            return this;
        }
    }

    public class StockSZList
    {
        public string error { get; set; }
        public object metadata { get; set; }
    }
}
