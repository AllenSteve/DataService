using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider;
using HtmlAgilityPack;
using Infrastructure;
using DataModel.ServiceModel;
using System.Collections.Concurrent;

namespace DataService
{
    public class StockDataService : IDataService
    {
        public string baseUrl { get; set; }
        public string baseUrlOfSH { get; set; }

        public IDao dao { get; set; }

        public StockDataService()
        {
            this.baseUrl = "http://www.szse.cn/api/report/ShowReport/data?SHOWTYPE=JSON&CATALOGID=1110x&TABKEY=tab1&PAGENO=";
            this.baseUrlOfSH = "http://yunhq.sse.com.cn:32041/v1/sh1/list/exchange/equity";
            this.dao = new StockDao();
        }

        public HtmlNodeCollection GetNodes(string source = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IDomainModel> ParseNodes(HtmlNodeCollection nodes)
        {
            throw new NotImplementedException();
        }

        public void Run()
        {
            this.SaveStock(this.GetStockSZList());
        }

        public void Save(HtmlNodeCollection nodes)
        {
            throw new NotImplementedException();
        }

        public List<StockSZ> GetSZStocksByPage(string url)
        {
            string content = WebUtil.Get(url, Encoding.UTF8).Truncate("\"data\":", "error");
            return JsonHelper.DeserializeJsonToList<StockSZ>(content);
        }

        public List<StockSH> GetSHStocksByPage()
        {
            string content = WebUtil.Get(this.baseUrlOfSH, Encoding.Default);
            var ret = JsonHelper.DeserializeJsonToObject<StockSHList>(content);
            return ret.List.Select(o => new StockSH(o).SetDate(ret.Date)).ToList();
        }

        public List<StockSZ> GetStockSZList()
        {
            BlockingCollection<StockSZ> lst = new BlockingCollection<StockSZ>();
            int pageCount = this.GetStockSZListCount();
            Parallel.For(0, pageCount, (i) =>
            {
                var stocks = this.GetSZStocksByPage(string.Concat(this.baseUrl, i + 1))
                                 .Select(o=>o.Format());
                foreach(var stock in stocks)
                {
                    lst.Add(stock);
                }
            });
            return lst.OrderBy(o => o.zqdm).ToList();
        }

        public int GetStockSZListCount()
        {
            string pageCount = WebUtil.Get(string.Concat(this.baseUrl, 1), Encoding.UTF8).Truncate("pagecount\":", "recordcount");
            return int.Parse(pageCount);
        }

        public void SaveStock(IEnumerable<IDomainModel> lst)
        {
            if (lst != null && lst.Any())
            {
                foreach (var stock in lst)
                {
                    this.dao.Add(stock);
                }
            }
        }

    }
}
