using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModel.ServiceModel
{
    public class StockSH : IStock
    {
        public long Id { get; set; }
        public string StockCode { get; set; }
        public string StockName { get; set; }
        public string Price { get; set; }
        public DateTime Date { get; set; }

        public StockSH() { }

        public StockSH(object source)
        {
            if (source != null)
            {
                string[] arr = source.ToString()
                                     .Replace("\"", string.Empty)
                                     .Replace("\r\n", string.Empty)
                                     .Replace("[", string.Empty)
                                     .Replace("]", string.Empty)
                                     .Split(',')
                                     .Select(o => o.Trim())
                                     .ToArray();
                StockCode = arr[0];
                StockName = arr[1];
                Price = arr[2];
            }
        }

        public StockSH SetDate(string date)
        {
            if (!string.IsNullOrEmpty(date))
            {
                this.Date = DateTime.ParseExact(date, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);
            }
            return this;
        }
    }
}
