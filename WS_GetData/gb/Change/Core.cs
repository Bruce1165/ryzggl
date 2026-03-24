using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WS_GetData.gb.Change
{
    /// <summary>
    /// 更新证书身份证等唯一键信息
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Core<T> where T : class
    {
        /// <summary>
        ///  质量安全网身份证变更
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="list"></param>
        /// <param name="url"></param>
        public static ResponseResult changeIDCard(string accessToken, List<T> list, string url)
        {
            string str = HttpCore.SendHttpRequest(url+"?access_token=" + accessToken, Api.JSON.Encode(new Request<T> {AcceptData = list}), "application/json");

            return JsonConvert.DeserializeObject<ResponseResult>(str);
        }
    }
}
