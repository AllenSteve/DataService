using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace DataProvider
{
    public abstract class Dao : IDao
    {
        private string connStr { get; set; }

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

        public int Add(IDomainModel entity)
        {
            throw new NotImplementedException();
        }

        public bool Contains(IDomainModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
