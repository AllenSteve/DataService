using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public static class SQLHelper
    {
        public static string CreateInsertSQL<TEntity>() where TEntity : class
        {
            Type type = typeof(TEntity);
            PropertyInfo[] properties = type.GetProperties();
            string tableName = type.GetCustomAttribute<DescriptionAttribute>().Description;
            StringBuilder sqlBuffer = new StringBuilder();
            sqlBuffer.Append(" Insert into ");
            sqlBuffer.Append(tableName);
            sqlBuffer.Append(" values ");
            sqlBuffer.Append(" (@");
            sqlBuffer.Append(string.Join(",@", properties.Where(o => o.GetCustomAttribute<KeyAttribute>() == null).Select(o => o.Name)));
            sqlBuffer.Append(" ) ");
            return sqlBuffer.ToString();
        }

        public static DataTable ToDataTable<TEntity>(IEnumerable<TEntity> lst) where TEntity : class
        {
            DataTable dt = new DataTable();
            Type type = typeof(TEntity);
            PropertyInfo[] properties = type.GetProperties();
            foreach (var prop in properties)
            {
                dt.Columns.Add(prop.Name);
            }

            foreach(var item in lst)
            {
                try
                {
                    DataRow row = dt.NewRow();
                    foreach(var prop in properties)
                    {
                        object value = prop.GetValue(item);
                        if (value != null)
                        {
                            row[prop.Name] = value;
                        }
                        else
                        {
                            row[prop.Name] = DBNull.Value;
                        }
                    }
                }
                catch(Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            return dt;
        }
    }
}
