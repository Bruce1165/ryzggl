using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WS_GetData.gb.Verification
{
    /// <summary>
    /// 校验
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Core<T> where T : class
    {
        /// <summary>
        /// 校验
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="list"></param>
        /// <param name="url"></param>
        public static ResponseResult Verification(string accessToken, List<T> list,string url)
        {
            string str = HttpCore.SendHttpRequest(url + "?access_token=" + accessToken, Api.JSON.Encode(new Request<T> { AcceptData = list }), "application/json");

            return JsonConvert.DeserializeObject<ResponseResult>(str);
        }
    }
}
