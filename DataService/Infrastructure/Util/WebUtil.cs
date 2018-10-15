using System;
using System.IO;
using System.Net;
using System.Text;

namespace Infrastructure
{
    public static class WebUtil
    {
        /// <summary>
        /// 根据Url获取页面所有内容
        /// </summary>
        /// <param name="URL">请求的url</param>
        /// <returns>返回页面的内容</returns>
        public static string GetContent(string URL)
        {
            try
            {
                int byteRead = 0;
                int len = 1024;
                StringBuilder buffer = new StringBuilder();
                char[] cbuffer = new char[len];
                HttpWebRequest httpReq = (HttpWebRequest)WebRequest.Create(new Uri(URL));
                HttpWebResponse httpResp = (HttpWebResponse)httpReq.GetResponse();
                using (Stream respStream = httpResp.GetResponseStream())
                {
                    using (StreamReader respStreamReader = new StreamReader(respStream, System.Text.Encoding.UTF8))
                    {
                        while ((byteRead = respStreamReader.Read(cbuffer, 0, len)) != 0)
                        {
                            buffer.Append(new string(cbuffer, 0, byteRead));
                        }
                    }
                }
                return buffer.ToString();
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string Get(string url, Encoding encode)
        {
            string strMsg = string.Empty;
            try
            {
                WebRequest request = WebRequest.Create(url);
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), encode))
                    {
                        return strMsg = reader.ReadToEnd();
                    }
                }
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
