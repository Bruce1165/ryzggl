using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WS_GetData.gb.Upcert
{
    /// <summary>
    /// 归档
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Core<T> where T : class
    {
        /// <summary>
        /// 归档
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="list"></param>
        /// <param name="url"></param>
        public static ResponseResult UpUrl(string accessToken, List<T> list,string url)
        {
            string str = HttpCore.SendHttpRequest(url + "?access_token=" + accessToken, Api.JSON.Encode(new Request<T> { AcceptData = list }), "application/json");

            return JsonConvert.DeserializeObject<ResponseResult>(str);
        }
    }
}
