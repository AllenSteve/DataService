using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;
using System.Data.SqlClient;

namespace DataProvider
{
    public class AgriculturalProductsDao : Dao, IDao
    {
        public AgriculturalProductsDao() : base()
        {
        }

        public override int Add(IDomainModel entity)
        {
            throw new NotImplementedException();
        }

        public override bool Contains(IDomainModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
