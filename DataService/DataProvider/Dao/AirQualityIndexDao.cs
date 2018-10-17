using Dapper;
using DataModel.ServiceModel;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataProvider
{
    public class AirQualityIndexDao : Dao, IDao
    {
        public AirQualityIndexDao() : base()
        {
        }

        public override int Add(IDomainModel source)
        {
            var entity = source as AirQualityIndex;
            if (entity != null)
            {
                return this.Connection.Execute("Insert into AirQualityIndex values (@City, @Monitor, @AQI, @Quality, @PM25, @PM10, @CreateTime)",
                new { City = entity.City, Monitor = entity.Monitor, AQI = entity.AQI, Quality = entity.Quality, PM25 = entity.PM25, PM10 = entity.PM10, CreateTime = entity.CreateTime });
            }
            return -1;
        }
    }
}
