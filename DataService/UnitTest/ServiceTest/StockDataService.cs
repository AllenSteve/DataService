using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Infrastructure;

namespace UnitTest.ServiceTest
{
    /// <summary>
    /// StockDataService 的摘要说明
    /// </summary>
    [TestClass]
    public class StockDataService
    {
        public StockDataService()
        {
            //
            //TODO:  在此处添加构造函数逻辑
            //
        }

        [TestMethod]
        public void TestMethod1()
        {
            string headStr = "\"data\":";
            string tailStr = "error";
            string szUrl = "http://www.szse.cn/api/report/ShowReport/data?SHOWTYPE=JSON&CATALOGID=1110x&TABKEY=tab1&PAGENO=3";
            string content = WebUtil.Get(szUrl,Encoding.UTF8);
            content = content.Substring(content.IndexOf(headStr) + headStr.Length, content.IndexOf(tailStr) - tailStr.Length - 2);
            var json = JsonHelper.DeserializeJsonToObject<Object>(content);
        }
    }
}
