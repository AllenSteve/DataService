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
    }
}
