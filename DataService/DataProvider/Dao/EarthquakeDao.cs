﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataModel.ServiceModel;
using Infrastructure;

namespace DataProvider
{
    public class EarthquakeDao : Dao, IDao
    {
        public EarthquakeDao() : base()
        {
        }

        public override int Add(IDomainModel source)
        {
            var entity = source as Earthquake;
            if (entity != null)
            {
                return this.Connection.Execute("Insert into Earthquake values (@Scale, @Latitude, @Logitude, @Depth, @CreateTime, @Position)",
                new { Scale = entity.Scale, Latitude = entity.Latitude, Logitude = entity.Logitude, Depth = entity.Depth, CreateTime = entity.CreateTime, Position = entity.Position });
            }
            return -1;
        }

        public override bool Contains(IDomainModel source)
        {
            var entity = source as Earthquake;
            if (entity != null)
            {
                var ret = this.Connection.Query<Earthquake>("select * from Earthquake where CreateTime=@CreateTime", new { CreateTime = entity.CreateTime });
                return ret != null && ret.Any();
            }
            return false;
        }
    }
}
