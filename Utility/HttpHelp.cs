using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Net;
using System.IO;

namespace Utility
{
    public class HttpHelp
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
            return Connect(url, "post", string.IsNullOrEmpty(data) ? "{}" : data);
        }

        /// <summary>
        /// 模拟POST请求
        /// </summary>
        /// <param name="url">发送网址</param>
        /// <param name="param">参数格式：key1=value1&key2=value2&...</param>
        /// <returns></returns>
         public static string DoPostWithUrlParams(string url, string param)
        {
            HttpWebRequest request = null;
            string str = "";

            try
            {
                request = (HttpWebRequest)WebRequest.Create(url);

                request.Method = "post";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Timeout = 1000 * 60 * 15;
                request.KeepAlive = true;
                request.Proxy = null;
                //request.ServicePoint.Expect100Continue = false;


                    byte[] dataBytes = Encoding.UTF8.GetBytes(param);
                    request.ContentLength = dataBytes.Length;

                    using (Stream postStream = request.GetRequestStream())
                    {
                        postStream.Write(dataBytes, 0, dataBytes.Length);
                        postStream.Flush();
                        postStream.Close();
                    }
              
                using (var httpWebResponse = (HttpWebResponse)request.GetResponse())
                {
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
                }

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
        /// <param name="ht"></param>
        /// <returns></returns>
        public static string DoGet(string url, Hashtable ht)
        {
            string queryString = string.Join("&", (from object key in ht.Keys select string.Format("{0}={1}", key, HttpUtility.UrlEncode(ht[key].ToString()))).ToArray());

            url = url.Contains("?") ? string.Format("{0}{1}", url, string.IsNullOrEmpty(queryString) ? "" : "&" + queryString) : string.Format("{0}?{1}", url, queryString);

            //if (Common.SystemSection.IsPrintPacket())
            //{
            //    FileLog.WriteLog("get请求url为：" + url);
            //}

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
            //if (Regex.IsMatch(url, "^https://"))
            //{
            //    return HttpsConnect(url, method, data);
            //}
            return HttpConnect(url, method, data);
        }

        /// <summary>
        ///     模拟Http协议,发送请求,将网页以流的形式读回来
        /// </summary>
        /// <param name="url"></param>
        /// <param name="method"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string HttpConnect(string url, string method, string data)
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

                using (var httpWebResponse = (HttpWebResponse)request.GetResponse())
                {
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
                }

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

        public static string HttpConnect(string url, string method, string data, Hashtable head)
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
                if (head != null)
                {
                    foreach (string s in head.Keys)
                    {
                        request.Headers.Add(s, head[s].ToString());
                    }                    
                }
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

                using (var httpWebResponse = (HttpWebResponse)request.GetResponse())
                {
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
                }

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

        public static string HttpPost(string url)
        {
            String ret = null;
            try
            {
                Uri address = new Uri(url);
                HttpWebRequest request = WebRequest.Create(address) as HttpWebRequest;
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    ret = reader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                ret = null;
                System.Diagnostics.Debug.Write(e.ToString());
            }
            return ret;
        }
        public static string HttpGet(string url)
        {
            String ret = null;
            try
            {
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    ret = reader.ReadToEnd();
                }
            }
            catch (WebException e)
            {
                ret = null;
                System.Diagnostics.Debug.Write(e.ToString());
            }
            return ret;
        }

        /// <summary>
        /// 调用社保查询接口
        /// </summary>
        /// <param name="param">参数格式：key1=value1&key2=value2&...</param>
        /// <returns></returns>
        public static string DoPostSheBaoQuery(string param)
        {
            //AppKey：6f56d219eb8646b2af6f8e2d243ed7d8
            //AppSecret：317TSpXvyDXWp8RTwnxqQOz4WPhs5JT2
            HttpWebRequest request = null;
            string str = "";

            try
            {
                request = (HttpWebRequest)WebRequest.Create("http://192.169.202.22/kong-api/zj/perfect-service-query/datasharing/common/query");
                request.Headers.Add("x-date", DateTime.UtcNow.ToString("r"));
                request.Headers.Add("X-KSCC-NONCE", Guid.NewGuid().ToString());
                request.Headers.Add("X-KSCC-PROFILE", "prod");

                string signing_string = string.Format("x-date: {0}\nX-KSCC-PROFILE: {1}\nX-KSCC-NONCE: {2}", request.Headers["x-date"], request.Headers["X-KSCC-PROFILE"], request.Headers["X-KSCC-NONCE"]);
                string _Authorization = string.Format("hmac username=\"6f56d219eb8646b2af6f8e2d243ed7d8\",algorithm=\"hmac-sm3\",headers=\"x-date X-KSCC-PROFILE X-KSCC-NONCE\",signature=\"{0}\"", SM2Utils.HMAC_SM3("317TSpXvyDXWp8RTwnxqQOz4WPhs5JT2", signing_string));
                //string _Authorization = string.Format("hamc username=\"6f56d219eb8646b2af6f8e2d243ed7d8\",algorithm=\"hmac-sm3\",headers=\"x-date X-KSCC-PROFILE X-KSCC-NONCE\",signature=\"{0}\"", Sm3Utils.Encrypt(signing_string,"317TSpXvyDXWp8RTwnxqQOz4WPhs5JT2"));
                request.Headers.Add("Authorization", _Authorization);
                

                request.Method = "post";
                request.ContentType = "application/x-www-form-urlencoded";
                request.Timeout = 1000 * 60 * 15;
                request.KeepAlive = true;
                request.Proxy = null;
                //request.ServicePoint.Expect100Continue = false;


                byte[] dataBytes = Encoding.UTF8.GetBytes(param);
                request.ContentLength = dataBytes.Length;

                using (Stream postStream = request.GetRequestStream())
                {
                    postStream.Write(dataBytes, 0, dataBytes.Length);
                    postStream.Flush();
                    postStream.Close();
                }

                using (var httpWebResponse = (HttpWebResponse)request.GetResponse())
                {
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
                }

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

    }
}
