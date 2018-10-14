using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataModel.ServiceModel;

namespace DataProvider
{
    public class EarthquakeDao : Dao
    {
        protected IDbConnection Connection { get; set; }

        public EarthquakeDao()
        {
            this.Connection = new SqlConnection(this.ConnStr);
        }

        public int Add(Earthquake entity)
        {
            return this.Connection.Execute("Insert into Earthquake values (@Scale, @Latitude, @Logitude, @Depth, @CreateTime, @Position)",
            new { Scale = entity.Scale, Latitude = entity.Latitude, Logitude = entity.Logitude, Depth = entity.Depth, CreateTime = entity.CreateTime, Position = entity.Position });
        }

        public bool Contains(Earthquake entity)
        {
            var ret = this.Connection.Query<Earthquake>("select * from Earthquake where CreateTime=@CreateTime", new { CreateTime = entity.CreateTime });
            return ret != null && ret.Any();
        }
    }
}
