using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WS_GetData.gb.Imputation
{
    /// <summary>
    /// 赋码
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Core<T> where T : class
    {
        /// <summary>
        ///  获取二维码赋码
        /// </summary>
        /// <param name="accessToken">访问令牌</param>
        /// <param name="list">提交参数</param>
        /// <param name="url">接口地址</param>
        public static ResponseResult Imputation(string accessToken, List<T> list, string url)
        {
            /////测试查看参数（临时打开）//////////////////////////
            //FileLog.WriteLog(Api.JSON.Encode(new Request<T> { AcceptData = list }));
            ////////////////////////////////////////////////////////


            string str = HttpCore.SendHttpRequest(url+"?access_token=" + accessToken, Api.JSON.Encode(new Request<T> {AcceptData = list}), "application/json");

            return JsonConvert.DeserializeObject<ResponseResult>(str);
        }

        /// <summary>
        /// 批量变更本人多本ABC本证书单位
        /// </summary>
        /// <param name="accessToken">访问令牌</param>
        /// <param name="list">提交参数</param>
        /// <param name="url">接口地址</param>
        /// <returns></returns>
        public static ResponseResult PatchChangeUnit(string accessToken, List<T> list, string url)
        {
            string str = HttpCore.SendHttpRequest(url + "?access_token=" + accessToken, Api.JSON.Encode(new Request<T> { AcceptData = list }), "application/json");

            return JsonConvert.DeserializeObject<ResponseResult>(str);
        }
    }
}
