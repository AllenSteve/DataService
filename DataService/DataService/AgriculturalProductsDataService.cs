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
    public class AgriculturalProductsDataService : IDataService
    {
        public string baseUrl { get; set; }

        public IDao dao { get; set; }

        public AgriculturalProductsDataService()
        {
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
            throw new NotImplementedException();
        }

        public void Save(HtmlNodeCollection nodes)
        {
            throw new NotImplementedException();
        }
    }
}
