using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel
{
    public class EastMoneyTradeDetailReturn
    {
        public bool Result { get; set; }
        public string Message { get; set; }
        public string Total { get; set; }
        public EastMoneyDetailReturnValue Value { get; set; }
    }

    public class EastMoneyDetailReturnValue
    {
        public string PC { get; set; }
        public List<object> Data { get; set; }
    }
}
