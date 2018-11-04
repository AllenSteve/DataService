using Dapper;
using DataModel;
using DataModel.ServiceModel;
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

        public List<StockSH> GetStocksByDate(DateTime key)
        {
            string queryStockSH = string.Format("select * from StockSH where date='{0}'", key.ToString("yyyy-MM-dd"));
            string queryStockSZ = string.Format("select * from StockSZ where date='{0}'", key.ToString("yyyy-MM-dd"));
            var stockSH = this.Connection.Query<StockSH>(queryStockSH).ToList();
            var stockSZ = this.Connection.Query<StockSZ>(queryStockSZ).Select(o => new StockSH { StockCode = o.StockCode, Date = o.Date }).ToList();
            return stockSH.Concat(stockSZ).ToList();
        }
    }
}
