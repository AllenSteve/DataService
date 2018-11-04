using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataService;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace UnitTest.ServiceTest
{
    [TestClass]
    public class StockTradeDetailResolverTest
    {
        public StockTradeDetailResolver service { get; set; }

        [TestMethod]
        public void CreateMySQLTablesTest()
        {
            service = new StockTradeDetailResolver();
            var files = service.GetFiles();
            service.CreateMysqlTables();
        }

        [TestMethod]
        public void SaveTest()
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            TimeSpan span = end - start;
            service = new StockTradeDetailResolver();
            var files = service.GetFiles();

            end = DateTime.Now;
            span = end - start;
            start = DateTime.Now;
            Debug.WriteLine("GetFiles: " + span);

            service.CreateMysqlTables();

            end = DateTime.Now;
            span = end - start;
            start = DateTime.Now;
            Debug.WriteLine("CreateMysqlTables: " + span);

            string file = files.First();
            var lst = service.GetList(file);
            end = DateTime.Now;
            span = end - start;
            start = DateTime.Now;
            Debug.WriteLine("File name: " + Path.GetFileNameWithoutExtension(file));
            Debug.WriteLine("GetList: " + span);

            service.BulkInsert(file, Encoding.Default);
            end = DateTime.Now;
            span = end - start;
            start = DateTime.Now;
            Debug.WriteLine("BulkInsert: " + span);
        }

        [TestMethod]
        public void SaveListTest()
        {
            DateTime start = DateTime.Now;
            DateTime end = DateTime.Now;
            TimeSpan span = end - start;
            service = new StockTradeDetailResolver();
            var files = service.GetFiles();

            end = DateTime.Now;
            span = end - start;
            start = DateTime.Now;
            Debug.WriteLine("GetFiles: "+ span);

            service.CreateMysqlTables();

            end = DateTime.Now;
            span = end - start;
            start = DateTime.Now;
            Debug.WriteLine("CreateMysqlTables: " + span);

            foreach (var file in files)
            {
                var lst = service.GetList(file);
                end = DateTime.Now;
                span = end - start;
                start = DateTime.Now;
                Debug.WriteLine("File name: " + Path.GetFileNameWithoutExtension(file));
                Debug.WriteLine("GetList: " + span);

                service.BulkInsert(file, Encoding.Default);

                end = DateTime.Now;
                span = end - start;
                start = DateTime.Now;
                Debug.WriteLine("Save: " + span);
            }
        }
    }
}
