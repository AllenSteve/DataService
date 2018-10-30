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

        public IDao dao { get; set; }

        public StockDataService()
        {
            this.baseUrl = "http://www.szse.cn/api/report/ShowReport/data?SHOWTYPE=JSON&CATALOGID=1110x&TABKEY=tab1&PAGENO=";
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
            this.SaveStockSZ(this.GetStockList());
        }

        public void Save(HtmlNodeCollection nodes)
        {
            throw new NotImplementedException();
        }

        public List<StockSZ> GetStocksByPage(string url)
        {
            string content = WebUtil.Get(url, Encoding.UTF8).Truncate("\"data\":", "error");
            return JsonHelper.DeserializeJsonToList<StockSZ>(content);
        }

        public List<StockSZ> GetStockList()
        {
            BlockingCollection<StockSZ> lst = new BlockingCollection<StockSZ>();
            int pageCount = this.GetStockListCount();
            Parallel.For(0, pageCount, (i) =>
            {
                var stocks = this.GetStocksByPage(string.Concat(this.baseUrl, i + 1))
                                 .Select(o=>o.Format());
                foreach(var stock in stocks)
                {
                    lst.Add(stock);
                }
            });
            return lst.OrderBy(o => o.zqdm).ToList();
        }

        public int GetStockListCount()
        {
            string pageCount = WebUtil.Get(string.Concat(this.baseUrl, 1), Encoding.UTF8).Truncate("pagecount\":", "recordcount");
            return int.Parse(pageCount);
        }

        public void SaveStockSZ(IEnumerable<StockSZ> lst)
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
