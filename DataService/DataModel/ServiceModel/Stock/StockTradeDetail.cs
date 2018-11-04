using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class StockTradeDetail
    {
        public int ID { get; set; }
        public String Exchange { get; set; }
        public String StockCode { get; set; }
        public String StockName { get; set; }
        public String TradeType { get; set; }
        public Decimal TradePrice { get; set; }
        public Decimal TradeVolume { get; set; }
        public Decimal TradeAmount { get; set; }
        public DateTime TradeDate { get; set; }
        public DateTime TradeTime { get; set; }
        public DateTime TradeTimestamp { get; set; }

        public StockTradeDetail(string content)
        {
            if(!string.IsNullOrEmpty(content))
            {
                string[] arr = content.Split('\t');
                Exchange = arr[0];
                StockCode = arr[1];
                StockName = arr[2];
                TradeType = arr[3];
                TradePrice = Decimal.Parse(arr[4]);
                TradeVolume = Decimal.Parse(arr[5]);
                TradeAmount = Decimal.Parse(arr[6]);
                TradeDate = DateTime.Parse(arr[7]);
                TradeTime = DateTime.Parse(arr[8]);
                TradeTimestamp = DateTime.Parse(arr[9]);
            }
        }
    }
}
