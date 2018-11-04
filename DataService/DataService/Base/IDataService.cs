using DataProvider;
using HtmlAgilityPack;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public interface IDataService
    {
        string baseUrl { get; set; }

        IDao dao { get; set; }

        HtmlNodeCollection GetNodes(string source = null);

        IEnumerable<IDomainModel> ParseNodes(HtmlNodeCollection nodes);

        void Save(HtmlNodeCollection nodes);

        void Run();
    }
}
