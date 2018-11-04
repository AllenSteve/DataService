using DataModel;
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
            string queryURL = string.Concat(this.baseUrl, stock.StockCode, GetExchangeType(stock.StockCode));
            string content = WebUtil.Get(queryURL, Encoding.Default);
            content = content.Truncate("\"value\":", ")")
                             .Replace("[","[[")
                             .Replace("\",\"", "\"],[\"")
                             .ReplaceFirst("\"],[\"", "\",\"")
                             +"]}";
            var ret = JsonHelper.DeserializeJsonToObject<EastMoneyDetailReturnValue>(content);
            if (ret != null && ret.Data != null && ret.Data.Any())
            {
                return ret.Data.Select(o => new EastMoneyTradeDetail(o).SetStock(stock)).ToList();
            }
            return default(List<EastMoneyTradeDetail>);
        }

        public void SaveList(IEnumerable<EastMoneyTradeDetail> lst)
        {
            this.dao.AddList<EastMoneyTradeDetail>(lst.ToList());
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
