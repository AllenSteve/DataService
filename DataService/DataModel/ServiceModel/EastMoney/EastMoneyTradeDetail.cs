using Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    [Description("EastMoneyTradeDetail")]
    public class EastMoneyTradeDetail
    {
        public long Id { get; set; }
        public DateTime TradeTime { get; set; }
        public string StockCode { get; set; }
        public decimal Price { get; set; }
        /// <summary>
        /// 现手
        /// https://zhidao.baidu.com/question/342776515.html
        /// </summary>
        public decimal Volume { get; set; }
        public int TradeType { get; set; }
        public int TradeTrend { get; set; }
        public int Amount { get; set; }
        public int Type { get; set; }
        /// <summary>
        /// 笔数
        /// </summary>
        public int TransactionAmount { get; set; }

        public EastMoneyTradeDetail(object source)
        {
            if (source != null)
            {
                string[] arr = source.ToString()
                                     .Replace("\"", string.Empty)
                                     .Replace("\r\n", string.Empty)
                                     .Replace("[", string.Empty)
                                     .Replace("]", string.Empty)
                                     .Split(',')
                                     .Select(o => o.Trim().Trim('"'))
                                     .ToArray();
                TradeTime = DateTime.Parse(arr[0]);
                Price = decimal.Parse(arr[1]);
                Volume = decimal.Parse(arr[2]);
                TradeType = int.Parse(arr[3]);
                TradeTrend = int.Parse(arr[4]);
                Amount = int.Parse(arr[5]);
                Type = int.Parse(arr[6]);
                TransactionAmount = int.Parse(arr[7]);
            }
        }

        public EastMoneyTradeDetail SetStock(IStock stock)
        {
            this.StockCode = stock.StockCode;
            this.TradeTime = this.TradeTime.AddDays((stock.Date - this.TradeTime).Days);
            return this;
        }
    }
}
