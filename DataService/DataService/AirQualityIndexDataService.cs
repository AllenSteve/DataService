using DataModel.ServiceModel;
using DataProvider;
using HtmlAgilityPack;
using Infrastructure;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataService
{
    public class AirQualityIndexDataService : DataService
    {
        public AirQualityIndexDataService()
        {
            this.baseUrl = "http://www.86pm25.com/";
            this.dao = new AirQualityIndexDao();
        }

        public IEnumerable<HtmlNode> GetCityList()
        {
            return base.Parse(baseUrl, "//dd/a");
        }

        public IEnumerable<AirQualityIndex> GetAQIList()
        {
            BlockingCollection<AirQualityIndex> lst = new BlockingCollection<AirQualityIndex>();
            var cityList = this.GetCityList();
            Parallel.ForEach(cityList, (city) =>
            {
                if (city != null)
                {
                    string createTime = base.Parse(string.Concat(this.baseUrl, city.Attributes[0].Value), "//div[@class='remark']").First().InnerText;
                    var AQIs = this.ParseAQI(city);
                    if (AQIs != null && AQIs.Any())
                    {
                        foreach (var AQI in AQIs)
                        {
                            lst.Add(this.Format(AQI, city.InnerText, DateTime.Parse(createTime.Replace("更新：", string.Empty))));
                        }
                    }
                }
            });

            return lst;
        }

        public IEnumerable<HtmlNode> ParseAQI(HtmlNode node)
        {
            string url = string.Concat(this.baseUrl, node.Attributes[0].Value);
            var lst = base.Parse(url, "//tr");
            if (lst != null && lst.Any())
            {
                return lst.Skip(1).TakeWhile(o => !o.InnerText.Contains("AQI指数：")).ToList();
            }
            return null;
        }

        public virtual AirQualityIndex Format(HtmlNode node, string cityName, DateTime createTime)
        {
            if (node != null)
            {
                AirQualityIndex AQI = new AirQualityIndex();
                AQI.City = cityName;
                AQI.Monitor = node.ChildNodes[0].InnerText;
                AQI.AQI = node.ChildNodes[1].InnerText;
                AQI.Quality = string.Concat(this.baseUrl, node.ChildNodes[2].ChildNodes[0].Attributes[0].Value.Replace("../", string.Empty));
                AQI.PM25 = node.ChildNodes[3].InnerText;
                AQI.PM10 = node.ChildNodes[4].InnerText;
                AQI.CreateTime = createTime;
                return AQI;
            }
            return default(AirQualityIndex);
        }

        public override void Run()
        {
            var lst = this.GetAQIList();
            foreach(var item in lst)
            {
                this.dao.Add(item);
            }
        }
    }
}
