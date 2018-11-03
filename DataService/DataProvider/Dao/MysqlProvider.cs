using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

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
    }
}
