using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Newtonsoft.Json;

namespace WS_GetData.gb.Login
{
    /// <summary>
    /// 
    /// </summary>
    public class Core
    {
        ///// <summary>
        /////     登陆
        ///// </summary>
        ///// <returns></returns>
        //public static ResponseResult Login(string url)
        //{
        //    ////测试账号
        //    //const string key = "e3251d0c-0cf2-49f5-bd00-b82c4930d3db";
        //    //const string secret = "0c33505a-beac-4561-af4d-c54bc938d7ae";

        //    //正式账号
        //    const string key = "bd477abc-9c43-4b0b-985c-1016451c46d7";
        //    const string secret = "3d541c8f-0795-41eb-882c-6041f48ebfbf";
        //    var ht = new Hashtable {{"client_id", key}, {"client_secret", secret}, {"grant_type", "client_credentials"}};
        //    //string queryString = string.Join("&", (from object item in ht.Keys select string.Format("{0}={1}", item, HttpUtility.UrlEncode(ht[key].ToString()))).ToArray());
        //    string queryString = string.Format("client_id={0}&client_secret={1}&grant_type=client_credentials", key, secret);
        //    string result = HttpCore.SendHttpRequest(url, queryString);
        //    return JsonConvert.DeserializeObject<ResponseResult>(result);
        //}


        /// <summary>
        ///     登陆
        /// </summary>
        /// <returns></returns>
        public static ResponseResult Login(string url, string type )
        {
            //正式账号
            //安管人员key	= 578e617b-8056-45b9-a111-7b0f990d0467，secret = b12d7107-487d-425a-b183-dffa0272be86
            //特种作业key	= bd477abc-9c43-4b0b-985c-1016451c46d7，secret = 3d541c8f-0795-41eb-882c-6041f48ebfbf

            ////测试账号
            //string key = "e3251d0c-0cf2-49f5-bd00-b82c4930d3db";
            //string secret = "0c33505a-beac-4561-af4d-c54bc938d7ae";

            //正式账号
            string key = (type == CertType.特种作业 ? "bd477abc-9c43-4b0b-985c-1016451c46d7" : "578e617b-8056-45b9-a111-7b0f990d0467");
            string secret = (type == CertType.特种作业 ? "3d541c8f-0795-41eb-882c-6041f48ebfbf" : "b12d7107-487d-425a-b183-dffa0272be86");
            var ht = new Hashtable { { "client_id", key }, { "client_secret", secret }, { "grant_type", "client_credentials" } };
            string queryString = string.Format("client_id={0}&client_secret={1}&grant_type=client_credentials", key, secret);
            string result = HttpCore.SendHttpRequest(url, queryString);
            return JsonConvert.DeserializeObject<ResponseResult>(result);
        }

       
    }
    public static class CertType
    {
        public const string 三类人 = "三类人";
        public const string 特种作业 = "特种作业";
    }
}
