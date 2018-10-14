using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider;
using HtmlAgilityPack;
using Infrastructure;
using DataModel.ServiceModel;

namespace DataService
{
    public class AgriculturalProductsDataService : IDataService
    {
        public string baseUrl { get; set; }

        public IDao dao { get; set; }

        public AgriculturalProductsDataService()
        {
            baseUrl = "http://www.xinfadi.com.cn/marketanalysis/0/list/";
            this.dao = new AgriculturalProductsDao();
        }

        public HtmlNodeCollection GetNodes(string source = null)
        {
            string url = string.IsNullOrWhiteSpace(source) ? baseUrl : source;
            try
            {
                if (!string.IsNullOrWhiteSpace(url))
                {
                    HtmlWeb webClient = new HtmlWeb();
                    HtmlDocument doc = webClient.Load(url);
                    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//table[@class='hq_table']/tr/td");
                    return nodes;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IEnumerable<IDomainModel> ParseNodes(HtmlNodeCollection nodes)
        {
            List<AgriculturalProducts> lst = new List<AgriculturalProducts>();
            if (nodes != null && nodes.Any())
            {
                for (int startIndex = 8, arrLength = 8; 0 < nodes.Count - startIndex; startIndex += arrLength)
                {
                    var row = nodes.Skip(startIndex).Take(arrLength).ToArray();
                    AgriculturalProducts entity = new AgriculturalProducts();
                    entity.LowPrice = row[1].InnerText;
                    entity.AveragePrice = row[2].InnerText;
                    entity.HighPrice = row[3].InnerText;
                    entity.Category = row[4].InnerText;
                    entity.Unit = row[5].InnerText;
                    entity.CreateTime = DateTime.Parse(row[6].InnerText);
                    entity.ProductName = row[0].InnerText;
                    lst.Add(entity);
                }
            }
            return lst;
        }

        public void Run()
        {
            for (int index = 0; index < 13455; ++index)
            {
                string url = string.Concat(this.baseUrl, ++index, ".shtml");
                this.Save(this.GetNodes(url));
            }
        }

        public void Save(HtmlNodeCollection nodes)
        {
            var lst = this.ParseNodes(nodes).Select(o => o as AgriculturalProducts);
            foreach (var item in lst)
            {
                this.dao.Add(item);
            }
        }
    }
}
