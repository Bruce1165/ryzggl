using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WS_GetData.gb.InCheck
{
    /// <summary>
    /// 安管证-查询本省转出证书是否已经在外省转入
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Core<T> where T : class
    {
        /// <summary>
        ///  安管证-办理转出证照查询
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="list"></param>
        /// <param name="url"></param>
        public static ResponseResult CheckCertInStatus(string accessToken, List<T> list, string url)
        {
            string str = HttpCore.SendHttpRequest(url+"?access_token=" + accessToken, Api.JSON.Encode(new Request<T> {AcceptData = list}), "application/json");

            return JsonConvert.DeserializeObject<ResponseResult>(str);
        }
    }
}
