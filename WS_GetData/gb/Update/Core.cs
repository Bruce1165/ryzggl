using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WS_GetData.gb.Update
{
    /// <summary>
    /// 证书状态更新(不需要照片等参数，有效装转为注销、离京、其他，或注销状态转为有效)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Core<T> where T : class
    {
        /// <summary>
        ///  质量安全网证书状态变更
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="list"></param>
        /// <param name="url"></param>
        public static ResponseResult UpdateCertData(string accessToken, List<T> list, string url)
        {
            string str = HttpCore.SendHttpRequest(url+"?access_token=" + accessToken, Api.JSON.Encode(new Request<T> {AcceptData = list}), "application/json");

            return JsonConvert.DeserializeObject<ResponseResult>(str);
        }
    }
}
