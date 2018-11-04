using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

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
            try
            {
                WebRequest request = WebRequest.Create(url);
                using (WebResponse response = request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), encode))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string Truncate(this string source, string head, string tail)
        {
            if (!string.IsNullOrWhiteSpace(source))
            {
                int startIndex = source.IndexOf(head) + head.Length;
                int length = source.IndexOf(tail) - startIndex - 2;
                return source.Substring(startIndex, length);
            }
            return source;
        }

        public static string ReplaceFirst(this string source, string src, string dest)
        {
            if (!string.IsNullOrWhiteSpace(source) && source.IndexOf(src) > -1)
            {
                return string.Concat(source.Substring(0, source.IndexOf(src)), dest, source.Substring(source.IndexOf(src) + src.Length));
            }
            return source;
        }

        /// <summary>  
        /// 获取字符中指定标签的值  
        /// </summary>  
        /// <param name="str">字符串</param>  
        /// <param name="title">标签</param>  
        /// <returns>值</returns>  
        public static string GetHtmlContent(this string str, string title)
        {
            //获取<title>之间内容  
            string tmpStr = string.Format("<{0}[^>]*?>(?<Text>[^<]*)</{1}>", title, title); 

            Match TitleMatch = Regex.Match(str, tmpStr, RegexOptions.IgnoreCase);

            string result = TitleMatch.Groups["Text"].Value;
            return result;
        }

        /// <summary>  
        /// 获取字符中指定标签的值  
        /// </summary>  
        /// <param name="str">字符串</param>  
        /// <param name="title">标签</param>  
        /// <param name="attrib">属性名</param>  
        /// <returns>属性</returns>  
        public static string GetHtmlContent(this string str, string title, string attrib)
        {

            string tmpStr = string.Format("<{0}[^>]*?{1}=(['\"\"]?)(?<url>[^'\"\"\\s>]+)\\1[^>]*>", title, attrib); //获取<title>之间内容  

            Match TitleMatch = Regex.Match(str, tmpStr, RegexOptions.IgnoreCase);

            string result = TitleMatch.Groups["url"].Value;
            return result;
        }
    }
}
