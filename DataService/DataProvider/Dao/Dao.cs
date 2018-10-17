using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using System.Data.SqlClient;
using Dapper;

namespace DataProvider
{
    public abstract class Dao : IDao
    {
        private string connStr { get; set; }

        protected IDbConnection Connection { get; set; }

        public string ConnStr
        {
            get
            {
                if(string.IsNullOrWhiteSpace(connStr))
                {
                    connStr = ConfigurationManager.ConnectionStrings["DataService"].ConnectionString;
                }
                return connStr;
            }
        }

        public Dao()
        {
            this.Connection = new SqlConnection(this.ConnStr);
        }

        public virtual int Add(IDomainModel entity)
        {
            throw new NotImplementedException();
        }

        public virtual bool Contains(IDomainModel entity)
        {
            throw new NotImplementedException();
        }

        public virtual IEnumerable<TEntity> All<TEntity>()
            where TEntity : IDomainModel, new()
        {
            return this.Connection.Query<TEntity>(string.Concat("select * from ", typeof(TEntity).Name));
        }
    }
}
