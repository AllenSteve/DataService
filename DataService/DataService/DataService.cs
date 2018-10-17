using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider;
using HtmlAgilityPack;
using Infrastructure;

namespace DataService
{
    public abstract class DataService : IDataService
    {
        public string baseUrl { get; set; }

        public IDao dao { get; set; }

        public virtual IEnumerable<HtmlNode> Parser(string url, string tag)
        {
            HtmlWeb client = new HtmlWeb();
            HtmlDocument doc = client.Load(url);
            HtmlNodeCollection nodes = doc.DocumentNode.SelectNodes(tag);
            return nodes;
        }

        public virtual HtmlNodeCollection GetNodes(string source = null)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<IDomainModel> ParseNodes(HtmlNodeCollection nodes)
        {
            throw new NotImplementedException();
        }

        public virtual void Run()
        {
            throw new NotImplementedException();
        }

        public virtual void Save(HtmlNodeCollection nodes)
        {
            throw new NotImplementedException();
        }
    }
}
