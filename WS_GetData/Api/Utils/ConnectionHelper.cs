using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using WS_GetData.Api;

namespace WS_GetData.Api.Utils
{
    /// <summary>
    /// </summary>
    public class ConnectionHelper
    {
        /// <summary>
        ///     模拟POST请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string DoPost(string url, string data)
        {
            //if (Common.SystemSection.IsPrintPacket())
            //{
            //    FileLog.WriteLog("post请求url：" + url);
            //    if (data != null)
            //    {
            //        FileLog.WriteLog("post请求内容：" + data);
            //    }
            //}
            DateTime start = DateTime.Now;
            try
            {
                
                string tmp = Connect(url, "post", string.IsNullOrEmpty(data) ? "{}" : data);
                //if (start.AddSeconds(30) < DateTime.Now)
                //{
                //    FileLog.WriteLog(string.Format("{0} - {1}接口调用。url = 【{2}】，data = 【{3}】", start, DateTime.Now, url, data));
                //}
                return tmp;
            }
            catch(Exception ex)
            {
                FileLog.WriteLog(string.Format("{0} - {1}接口调用报错。\r\n地址url = 【{2}】，\r\n传入参数data = 【{3}】", start, DateTime.Now, url, data));
                throw ex;
            }

            //return Connect(url, "post", string.IsNullOrEmpty(data) ? "{}" : data);
        }

        /// <summary>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="ht"></param>
        /// <returns></returns>
        public static string DoGet(string url, Hashtable ht)
        {
            string queryString = string.Join("&", (from object key in ht.Keys select string.Format("{0}={1}", key, HttpUtility.UrlEncode(ht[key].ToString()))).ToArray());

            url = url.Contains("?") ? string.Format("{0}{1}", url, string.IsNullOrEmpty(queryString) ? "" : "&" + queryString) : string.Format("{0}?{1}", url, queryString);

            if (Common.SystemSection.IsPrintPacket())
            {
                FileLog.WriteLog("get请求url为：" + url);
            }

            return Connect(url, "GET", null);
        }

        /// <summary>
        ///     定义区分https和http协议的请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string Connect(string url, string method, string data)
        {
            if (string.IsNullOrEmpty(url) || string.IsNullOrEmpty(method))
            {
                throw new Exception("url或method为空！");
            }
            if (Regex.IsMatch(url, "^https://"))
            {
                return HttpsConnect(url, method, data);
            }
            return HttpConnect(url, method, data);
        }

        ///// <summary>
        /////     模拟Http协议,发送请求,将网页以流的形式读回来
        ///// </summary>
        ///// <param name="url"></param>
        ///// <param name="method"></param>
        ///// <param name="data"></param>
        ///// <returns></returns>
        //private static string HttpConnect(string url, string method, string data)
        //{
        //    HttpWebRequest request = null;
        //    string str = "";

        //    try
        //    {
        //        request = (HttpWebRequest) WebRequest.Create(url);

        //        request.Method = method;
        //        request.ContentType = "application/json";
        //        request.Timeout = 1000*60*15;
        //        request.KeepAlive = true;
        //        request.Proxy = null;
        //        //request.ServicePoint.Expect100Continue = false;

        //        if (method.ToLower() == "post")
        //        {
        //            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
        //            request.ContentLength = dataBytes.Length;

        //            using (Stream postStream = request.GetRequestStream())
        //            {
        //                postStream.Write(dataBytes, 0, dataBytes.Length);
        //                postStream.Flush();
        //                postStream.Close();
        //            }
        //        }




        //        try
        //        {
        //            using (HttpWebResponse httpWebResponse = (HttpWebResponse)request.GetResponse())
        //            {
        //                if (httpWebResponse.StatusCode.Equals(HttpStatusCode.OK))
        //                {
        //                    Stream stream = httpWebResponse.GetResponseStream();

        //                    if (stream != null)
        //                    {
        //                        using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
        //                        {
        //                            str = reader.ReadToEnd();
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        catch (WebException ex)
        //        {
        //            FileLog.WriteLog("HttpConnect请求失败！错误：" + ex.Message, ex);
        //            using (HttpWebResponse httpWebResponse = (HttpWebResponse)ex.Response)
        //            {
        //                if (httpWebResponse.StatusCode.Equals(HttpStatusCode.OK))
        //                {
        //                    Stream stream = httpWebResponse.GetResponseStream();

        //                    if (stream != null)
        //                    {
        //                        using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
        //                        {
        //                            str = reader.ReadToEnd();
        //                        }
        //                    }
        //                }
        //            }
        //        }

        //        //HttpWebResponse httpWebResponse ;
        //        //try
        //        //{
        //        //    httpWebResponse = (HttpWebResponse)request.GetResponse();
        //        //}
        //        //catch (WebException ex)
        //        //{
        //        //    httpWebResponse = (HttpWebResponse)ex.Response;
        //        //}

        //        //if (httpWebResponse.StatusCode.Equals(HttpStatusCode.OK))
        //        //{
        //        //    Stream stream = httpWebResponse.GetResponseStream();

        //        //    if (stream != null)
        //        //    {
        //        //        using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
        //        //        {
        //        //            str = reader.ReadToEnd();
        //        //        }
        //        //    }
        //        //}


        //        //using (var httpWebResponse = (HttpWebResponse) request.GetResponse())
        //        //{
        //        //    if (httpWebResponse.StatusCode.Equals(HttpStatusCode.OK))
        //        //    {
        //        //        Stream stream = httpWebResponse.GetResponseStream();

        //        //        if (stream != null)
        //        //        {
        //        //            using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
        //        //            {
        //        //                str = reader.ReadToEnd();
        //        //            }
        //        //        }
        //        //    }
        //        //}

        //        return str;
        //    }
        //    catch (Exception exp)
        //    {
        //        FileLog.WriteLog("请求失败！错误：" + exp.Message, exp);
        //        throw;
        //    }
        //    finally
        //    {
        //        if (request != null)
        //        {
        //            request.Abort();
        //        }
        //    }
        //}

        /// <summary>
        ///     模拟Http协议,发送请求,将网页以流的形式读回来
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string HttpConnect(string url, string method, string data)
        {
            HttpWebRequest request = null;
            string str = "";

            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = method;
                request.ContentType = "application/json";
                request.Timeout = 1000 * 60 * 15;
                request.KeepAlive = true;
                request.Proxy = null;
                //request.ServicePoint.Expect100Continue = false;

                if (method.ToLower() == "post")
                {
                    byte[] dataBytes = Encoding.UTF8.GetBytes(data);
                    request.ContentLength = dataBytes.Length;

                    using (Stream postStream = request.GetRequestStream())
                    {
                        postStream.Write(dataBytes, 0, dataBytes.Length);
                        postStream.Flush();
                        postStream.Close();
                    }
                }


                HttpWebResponse httpWebResponse;
                try
                {
                    httpWebResponse = (HttpWebResponse)request.GetResponse();
                }
                catch (WebException ex)
                {
                    httpWebResponse = (HttpWebResponse)ex.Response;
                }

                if (httpWebResponse.StatusCode.Equals(HttpStatusCode.OK))
                {
                    Stream stream = httpWebResponse.GetResponseStream();

                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
                        {
                            str = reader.ReadToEnd();
                        }
                    }
                }


                //using (var httpWebResponse = (HttpWebResponse) request.GetResponse())
                //{
                //    if (httpWebResponse.StatusCode.Equals(HttpStatusCode.OK))
                //    {
                //        Stream stream = httpWebResponse.GetResponseStream();

                //        if (stream != null)
                //        {
                //            using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
                //            {
                //                str = reader.ReadToEnd();
                //            }
                //        }
                //    }
                //}

                return str;
            }
            catch (Exception exp)
            {
                FileLog.WriteLog("请求失败！错误：" + exp.Message, exp);
                throw;
            }
            finally
            {
                if (request != null)
                {
                    request.Abort();
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static string HttpsConnect(string url, string method, string data)
        {
            return "";
        }
    }
}