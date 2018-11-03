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
using MySql.Data.MySqlClient;

namespace DataProvider
{
    public abstract class Dao : IDao
    {
        private string database { get; set; }
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
            database = ConfigurationManager.AppSettings["Database"].ToUpper();
            if(database.Equals("SqlServer".ToUpper()))
            {
                this.ConnectSqlServer(this.ConnStr);
            }
            else if(database.Equals("Mysql".ToUpper()))
            {
                this.ConnectMysql(this.ConnStr);
            }
        }

        public void ConnectSqlServer(string conn)
        {
            this.Connection = new SqlConnection(conn);
        }

        public void ConnectMysql(string conn)
        {
            this.Connection = new MySqlConnection(conn);
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
