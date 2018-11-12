using Dapper;
using DataModel.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider
{
    public class Repository<TEntity> : Dao
        where TEntity : class
    {
        public List<StockSH> GetStockSHList(string date=null)
        {
            string queryStockSH = string.Format("select * from StockSH where date='{0}'", string.IsNullOrEmpty(date) ? DateTime.Now.ToString("yyyy-MM-dd") : date);
            var shStocks = this.Connection.Query<StockSH>(queryStockSH).ToList();
            return shStocks;
        }
    }
}
