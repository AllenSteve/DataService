using DataModel;
using DataProvider;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public class StockTradeDetailResolver
    {
        private MysqlProvider mysqlProvider { get; set; }

        private static string filePath { get; set; }

        public static string FilePath
        {
            get
            {
                if(string.IsNullOrEmpty(filePath))
                {
                    filePath = ConfigurationManager.AppSettings["TradeDetailPath"];
                }
                return filePath;
            }
        }


        public StockTradeDetailResolver()
        {
            mysqlProvider = new MysqlProvider();
        }

        public string[] GetFiles()
        {
            return Directory.GetFiles(FilePath);
        }

        public void CreateMysqlTables()
        {
            var files = this.GetFiles();
            var tableNames = files.Select(o=>Path.GetFileNameWithoutExtension(o));
            foreach(var name in tableNames)
            {
                mysqlProvider.CreateTable(name);
            }
        }

        public List<StockTradeDetail> GetList(string filePath)
        {
            if(!string.IsNullOrEmpty(filePath) && File.Exists(filePath))
            {
                List<StockTradeDetail> lst = new List<StockTradeDetail>();
                using (var fs = File.Open(filePath, FileMode.Open))
                {
                    using (var stream = new StreamReader(fs, System.Text.Encoding.Default))
                    {
                        while (!stream.EndOfStream)
                        {
                            lst.Add(new StockTradeDetail(stream.ReadLine()));
                        }
                        return lst;
                    }
                }
            }
            return null;
        }

        public void Save(List<StockTradeDetail> lst, string tableName)
        {
            this.mysqlProvider.AddListToTable(lst, tableName);
        }
    }
}
