using DataModel.ServiceModel;
using DataProvider;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public class EarthquakeDataService
    {
        public string baseUrl { get; set; }

        public EarthquakeDao dao { get; set; }

        public EarthquakeDataService()
        {
            baseUrl = "http://news.ceic.ac.cn/index.html";
            this.dao = new EarthquakeDao();
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
                    HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes("//tr");
                    return nodes;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<Earthquake> ParseList(HtmlNodeCollection nodes)
        {
            List<Earthquake> lst = new List<Earthquake>();
            if (nodes != null && nodes.Any())
            {
                foreach (var node in nodes)
                {
                    if (!string.IsNullOrWhiteSpace(node.InnerText))
                    {
                        string[] content = node.InnerText.Split('\n').Select(o => o.Trim()).ToArray();
                        int startIndex = this.GetStartIndex(content);
                        if (startIndex < 2) continue;
                        Earthquake entity = new Earthquake();
                        entity.Scale = content[startIndex];
                        entity.CreateTime = DateTime.Parse(content[startIndex + 1]);
                        entity.Latitude = content[startIndex + 2];
                        entity.Logitude = content[startIndex + 3];
                        entity.Depth = content[startIndex + 4];
                        entity.Position = content[startIndex + 5];
                        lst.Add(entity);
                    }
                }
            }
            return lst;
        }

        public void Save(HtmlNodeCollection nodes)
        {
            var lst = this.ParseList(nodes).OrderBy(o => o.CreateTime);
            foreach (var item in lst)
            {
                if (!this.dao.Contains(item))
                {
                    this.dao.Add(item);
                }
            }
        }

        public void Run()
        {
            this.Save(this.GetNodes());
        }

        private int GetStartIndex(string[] arr)
        {
            int startIndex = 0;
            while (string.IsNullOrEmpty(arr[startIndex++])) ;
            return startIndex - 1;
        }
    }
}
