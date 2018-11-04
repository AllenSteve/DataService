using DataModel;
using DataModel.ServiceModel;
using DataProvider;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public class EastMoneyTradeDetailDataService : DataService
    {
        public EastMoneyTradeDetailDataService()
        {
            this.baseUrl = "http://mdfm.eastmoney.com/EM_UBG_MinuteApi/Js/Get?dtype=all&rows=100000&page=1&id=";
            this.dao = new EastMoneyTradeDetailDao();
        }

        public List<EastMoneyTradeDetail> GetTradeDetailByStock(IStock stock)
        {
            try
            {
                string queryURL = string.Concat(this.baseUrl, stock.StockCode, GetExchangeType(stock.StockCode));
                string content = WebUtil.Get(queryURL, Encoding.Default);
                content = content.Truncate("\"value\":", ")")
                                 .Replace("[", "[[")
                                 .Replace("\",\"", "\"],[\"")
                                 .ReplaceFirst("\"],[\"", "\",\"")
                                 + "]}";
                var ret = JsonHelper.DeserializeJsonToObject<EastMoneyDetailReturnValue>(content);
                if (ret != null && ret.Data != null && ret.Data.Count > 1)
                {
                    return ret.Data.Select(o => new EastMoneyTradeDetail(o).SetStock(stock)).ToList();
                }
            }
            catch(Exception ex)
            {
            }
            return default(List<EastMoneyTradeDetail>);
        }

        public void SaveList(IEnumerable<EastMoneyTradeDetail> lst)
        {
            if (lst != null && lst.Any())
            {
                this.dao.AddList<EastMoneyTradeDetail>(lst.ToList());
            }
        }

        public override void Run()
        {
            if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
            {
                var stocks = this.GetStocksByDate(DateTime.Now);
                foreach (var stock in stocks)
                {
                    var lst = this.GetTradeDetailByStock(stock);
                    this.SaveList(lst);
                }
            }
        }

        public List<StockSH> GetStocksByDate(DateTime date)
        {
            return (this.dao as EastMoneyTradeDetailDao).GetStocksByDate(date);
        }

        private static int GetExchangeType(string stockCode)
        {
            if (!string.IsNullOrEmpty(stockCode) && stockCode.StartsWith("6"))
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}
