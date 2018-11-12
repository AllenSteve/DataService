using DataModel.ServiceModel;
using DataProvider;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public class Stock_SinaDataService : DataService
    {
        protected Repository<StockSina> repository { get; set; }

        protected List<string> cache { get; set; }

        public Stock_SinaDataService()
        {
            repository = new Repository<StockSina>();
            cache = new List<string>();
            this.baseUrl = "http://hq.sinajs.cn/list=";
        }

        public void Flush(string path)
        {
            if (cache != null && cache.Any())
            {
                using (FileStream stream = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    StreamWriter writer = new StreamWriter(stream);
                    writer.Write(string.Join("\r\n", cache));
                    writer.Flush();
                    writer.Close();
                }
            }
        }

        public List<string> GetStocks(string date = null)
        {
            var stocks = this.repository.GetStockSHList(date);
            List<string> lst = new List<string>();
            foreach(var stock in stocks)
            {
                string response = WebUtil.Get(string.Concat(baseUrl, "sh", stock.StockCode), Encoding.Default);
                string stockInfo = response.Substring(response.IndexOf("\"") + 1).Replace("\";", string.Empty).Trim();
                lst.Add(stockInfo);
            }
            return lst;
        }

    }
}
