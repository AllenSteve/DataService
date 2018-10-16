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
    public class StockDao : Dao, IDao
    {
        public StockDao() : base()
        {
        }

        public override int Add(IDomainModel source)
        {
            var entity = source as StockSZ;
            if (entity != null)
            {
                return this.Connection.Execute("Insert into StockSZ values (@zqdm, @gsjc, @gsqc, @sshymc, @http, @jqhq,@gsxq)",
                new { zqdm = entity.zqdm, gsjc = entity.gsjc, gsqc = entity.gsqc, sshymc = entity.sshymc, http = entity.http, jqhq = entity.jqhq, gsxq = entity.gsxq});
            }
            return -1;
        }
    }
}
