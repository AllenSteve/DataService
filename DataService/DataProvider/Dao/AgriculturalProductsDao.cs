using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using System.Data.SqlClient;
using DataModel.ServiceModel;
using Dapper;

namespace DataProvider
{
    public class AgriculturalProductsDao : Dao, IDao
    {
        public AgriculturalProductsDao() : base()
        {
        }

        public override int Add(IDomainModel source)
        {
            var entity = source as AgriculturalProducts;
            if (entity != null)
            {
                return this.Connection.Execute("Insert into AgriculturalProducts values (@LowPrice, @AveragePrice, @HighPrice, @Category, @Unit, @CreateTime, @ProductName)",
                new { LowPrice = entity.LowPrice, AveragePrice = entity.AveragePrice, HighPrice = entity.HighPrice, Category = entity.Category, Unit = entity.Unit, CreateTime = entity.CreateTime, ProductName = entity.ProductName });
            }
            return -1;
        }

        public override bool Contains(IDomainModel source)
        {
            var entity = source as AgriculturalProducts;
            if (entity != null)
            {
                var ret = this.Connection.Query<AgriculturalProducts>("select * from AgriculturalProducts where CreateTime=@CreateTime and ProductName=@ProductName", new { CreateTime = entity.CreateTime, ProductName = entity.ProductName });
                return ret != null && ret.Any();
            }
            return false;
        }
    }
}
