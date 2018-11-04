using Dapper;
using DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider
{
    public class EastMoneyTradeDetailDao: Dao, IDao
    {
        public EastMoneyTradeDetailDao() : base()
        {
        }

        public override void AddList<EastMoneyTradeDetail>(List<EastMoneyTradeDetail> list)
        {
            if (list != null && list.Any() && typeof(EastMoneyTradeDetail) != null)
            {
                string executeSQL = string.Format("Insert into {0} values (@TradeTime,@StockCode,@Price,@Volume,@TradeType,@TradeTrend,@Amount,@Type,@TransactionAmount)", typeof(EastMoneyTradeDetail).Name);
                this.Connection.Execute(executeSQL, list);
            }
        }
    }
}
