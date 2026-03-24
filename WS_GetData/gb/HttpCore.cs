using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Security.Cryptography;

namespace WS_GetData.gb
{
    /// <summary>
    ///     Http操作类
    /// </summary>
    public class HttpCore
    {
        /// <summary>
        ///     发送请求
        /// </summary>
        /// <param name="url"></param>
        /// <param name="postData"></param>
        /// <param name="contentType"></param>
        /// <return></return>
        public static string SendHttpRequest(string url, string postData, string contentType = "application/x-www-form-urlencoded")
        {
            string result = "";
            var request = (HttpWebRequest) WebRequest.Create(url);
            //基本设置
            request.Method = "POST";
            request.ContentType = contentType;
            request.Timeout = 1000*60*10;
            request.KeepAlive = true;
            request.Proxy = null;


            //设置请求内容
            byte[] data = Encoding.UTF8.GetBytes(postData);
            var dataLength = data.Length;
            request.ContentLength = dataLength;
            using (Stream postStream = request.GetRequestStream())
            {
                postStream.Write(data, 0, dataLength);
                postStream.Flush();
            }

            //创建响应对象
            using (var httpWebResponse = (HttpWebResponse) request.GetResponse())
            {
                if (httpWebResponse.StatusCode.Equals(HttpStatusCode.OK))
                {
                    Stream stream = httpWebResponse.GetResponseStream();
                    if (stream != null)
                    {
                        using (var reader = new StreamReader(stream, Encoding.GetEncoding("utf-8")))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
            }

            return result;
        }

         /// <summary>
        /// 加密码
        /// </summary>
        /// <param name="strinput">加密字符串</param>
        /// <param name="strkey">密钥</param>
        /// <returns></returns>
        public static string DESEncrypt(string strinput, string strkey)
        {

            strkey = strkey.Substring(0, 8);

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray;
            inputByteArray = Encoding.UTF8.GetBytes(strinput);
            des.Key = ASCIIEncoding.ASCII.GetBytes(strkey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(strkey);
            des.Padding = PaddingMode.PKCS7;
            des.Mode = CipherMode.ECB;
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }

        //解密码
        public static string DESDecrypt(string pToDecrypt, string key)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = new byte[pToDecrypt.Length / 2];

            for (int x = 0; x < pToDecrypt.Length / 2; x++)
            {
                int i = (Convert.ToInt32(pToDecrypt.Substring(x * 2, 2), 16));
                inputByteArray[x] = (byte)i;
            }
            des.Key = UTF8Encoding.UTF8.GetBytes(key);
            des.IV = UTF8Encoding.UTF8.GetBytes(key);
            des.Padding = PaddingMode.PKCS7;
            des.Mode = CipherMode.ECB;

            MemoryStream ms = new MemoryStream();

            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);

            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            StringBuilder ret = new StringBuilder();
            return Encoding.UTF8.GetString(ms.ToArray());

        }
    }
}
