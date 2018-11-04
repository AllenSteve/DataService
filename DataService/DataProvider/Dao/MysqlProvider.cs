using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DataModel;
using MySql.Data.MySqlClient;

namespace DataProvider
{
    public class MysqlProvider : Dao
    {
        private static string TableDefinition = @"CREATE TABLE if not exists `{0}` (
                                              `ID` bigint(20) unsigned NOT NULL AUTO_INCREMENT,
                                              `Exchange` varchar(8) NOT NULL,
                                              `StockCode` varchar(8) NOT NULL,
                                              `StockName` varchar(32) NOT NULL,
                                              `TradeType` varchar(16) NOT NULL,
                                              `TradePrice` decimal(18,2) NOT NULL,
                                              `TradeVolume` decimal(18,0) NOT NULL,
                                              `TradeAmount` decimal(18,2) NOT NULL,
                                              `TradeDate` date DEFAULT NULL,
                                              `TradeTime` time DEFAULT NULL,
                                              `TradeTimestamp` timestamp NULL DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP,
                                              PRIMARY KEY (`ID`),
                                              KEY `{0}_TradeType_Index` (`TradeType`),
                                              KEY `{0}_TradeDate_Index` (`TradeDate`)
                                            ) ENGINE=MyISAM AUTO_INCREMENT=1 DEFAULT CHARSET=utf8";

        public MysqlProvider() 
        {
            this.ConnectMysql(ConfigurationManager.ConnectionStrings["MysqlProvider"].ConnectionString);
        }

        public void CreateTable(string tableName)
        {
            this.Connection.Execute(string.Format(TableDefinition,tableName));
        }

        public void AddListToTable(List<StockTradeDetail> list, string tableName)
        {
            if (list != null && list.Any() && !string.IsNullOrEmpty(tableName))
            {
                this.Connection.Execute("Insert into `" + tableName+ "` (Exchange,StockCode,StockName,TradeType,TradePrice,TradeVolume,TradeAmount,TradeDate,TradeTime,TradeTimestamp) values (@Exchange,@StockCode,@StockName,@TradeType,@TradePrice,@TradeVolume,@TradeAmount,@TradeDate,@TradeTime,@TradeTimestamp)", list);
            }
        }

        public int BulkCopy(string tableName, string path, Encoding encode)
        {
            try
            {
                int ret = -1;
                MySqlBulkLoader bulk = new MySqlBulkLoader(this.Connection as MySqlConnection)
                {
                    FieldTerminator = "\t",
                    CharacterSet = encode.BodyName,
                    FieldQuotationCharacter = '"',
                    EscapeCharacter = '"',
                    LineTerminator = "\r\n",
                    FileName = path,
                    //NumberOfLinesToSkip = 0,
                    TableName = string.Format("`{0}`", tableName),
                };
                //bulk.Columns.AddRange(table.Columns.Cast<DataColumn>().Select(colum => colum.ColumnName).ToArray());
                return ret = bulk.Load();
            }
            catch (MySqlException ex)
            {
                // if (tran != null) tran.Rollback();
                throw ex;
            }
        }
    }
}
