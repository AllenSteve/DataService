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
            where TEntity : IDomainModel
        {
            return this.Connection.Query<TEntity>(string.Concat("select * from ", typeof(TEntity).Name));
        }

        public virtual int Add<TEntity>(TEntity entity)
            where TEntity : class
        {
            int ret = -1;
            if (entity != null)
            {
                string insertSQL = SQLHelper.CreateInsertSQL<TEntity>();
                ret = this.Connection.Execute(insertSQL, entity);
            }

            return ret;
        }

        public virtual void AddList<TEntity>(IEnumerable<TEntity> lst) where TEntity : class
        {
            if (lst != null && lst.Any())
            {
                string insertSQL = SQLHelper.CreateInsertSQL<TEntity>();
                this.Connection.Execute(insertSQL, lst);
            }
        }

        public virtual void BulkCopy(DataTable dt, string tableName)
        {
            string column;
            try
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(this.ConnStr))
                {
                    bulkCopy.BulkCopyTimeout = 10000;
                    bulkCopy.DestinationTableName = tableName;
                    foreach (DataColumn item in dt.Columns)
                    {
                        column = item.ColumnName;
                        bulkCopy.ColumnMappings.Add(item.ColumnName, item.ColumnName);
                    }
                    bulkCopy.WriteToServer(dt);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
