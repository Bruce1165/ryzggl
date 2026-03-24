using WS_GetData.Api.Model;
using WS_GetData.Api.Utils;
using Newtonsoft.Json;

namespace WS_GetData.Api
{
    /// <summary>
    ///     API核心操作
    /// </summary>
    public static class Core
    {
        /// <summary>
        ///     4.1.1 登录
        /// </summary>
        /// <returns></returns>
        public static LoginResponseResult Login()
        {
            //人员（特种作业、直接技能、三类人）
            const string appKey = "BYehDgJUdUyQESG";
            const string appSecret = "XVLgyVhdQLYUsmJ";
            const string account = "bjszfhcxjsw";
            const string password = "jNihGBpScvVHfxj";

            return Login(appKey, appSecret, account, password);
        }

        /// <summary>
        ///     4.1.1 登录
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="appSecret"></param>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static LoginResponseResult Login(string appKey, string appSecret, string account, string password)
        {
            string apiUrl = string.Format("{0}{1}", Constants.BaseUrl, Constants.LoginUrl);

            string str = ConnectionHelper.DoPost(apiUrl, JSON.Encode(new LoginMDL
            {
                AppKey = appKey,
                AppSecret = appSecret,
                Account = account,
                Password = password
            }));

            return JsonConvert.DeserializeObject<LoginResponseResult>(str);
        }

        /// <summary>
        ///     注销会话
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static ResponseResult LogOut(string accessToken)
        {
            string apiUrl = string.Format("{0}{1}?access_token={2}", Constants.BaseUrl, Constants.LogoutUrl, accessToken);

            return JsonConvert.DeserializeObject<ResponseResult>(ConnectionHelper.DoPost(apiUrl, null));
        }

        /// <summary>
        ///     4.4.1 创建制证数据
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="itemCode">电子证照目录编码</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static CreateResponseResult Create(string accessToken, string itemCode, CreateRequestMdl model)
        {
            string apiUrl = string.Format("{0}/license/{1}/create?access_token={2}", Constants.BaseUrl, itemCode, accessToken);

            return JsonConvert.DeserializeObject<CreateResponseResult>(ConnectionHelper.DoPost(apiUrl, JSON.Encode(model)));
        }

        /// <summary>
        ///     4.5.1 签发一张证照
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="itemCode">电子证照目录编码</param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static SignResponseResult Sign(string accessToken, string itemCode, SignRequestMdl model)
        {
            string apiUrl = string.Format("{0}/license/{1}/sign?access_token={2}", Constants.BaseUrl, itemCode, accessToken);

            return JsonConvert.DeserializeObject<SignResponseResult>(ConnectionHelper.DoPost(apiUrl, JSON.Encode(model)));
        }

        /// <summary>
        /// 废止一张证照
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="itemCode"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        public static AbolishResponseResult Abolish(string accessToken, string itemCode, AbolishRequestMdl model)
        {
            string apiUrl = string.Format("{0}/license/{1}/abolish?access_token={2}", Constants.BaseUrl, itemCode, accessToken);
            //FileLog.WriteLog(string.Format("废止一张证照Abolish，url：【{0}】。参数：【{1}】", apiUrl, JSON.Encode(model)));
            return JsonConvert.DeserializeObject<AbolishResponseResult>(ConnectionHelper.DoPost(apiUrl, JSON.Encode(model)));
        }

        ///// <summary>
        ///// 废止一张证照
        ///// </summary>
        ///// <param name="accessToken"></param>
        ///// <param name="itemCode"></param>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //public static AbolishResponseResult AbolishOfd(string accessToken, string itemCode, AbolishRequestMdl model)
        //{
        //    string apiUrl = string.Format("{0}/license/{1}/abolish?access_token={2}", Constants.BaseUrlOfd, itemCode, accessToken);
        //    //FileLog.WriteLog(string.Format("废止一张证照Abolish，url：【{0}】。参数：【{1}】", apiUrl, JSON.Encode(model)));
        //    return JsonConvert.DeserializeObject<AbolishResponseResult>(ConnectionHelper.DoPost(apiUrl, JSON.Encode(model)));
        //}

        /// <summary>
        ///     PDF签章制证
        /// </summary>
        /// <param name="accessToken">登录令牌</param>
        /// <param name="itemCode">电子证照目录编码</param>
        /// <param name="model">对象</param>
        /// <returns></returns>
        public static CreateResponseResult CreatePDF(string accessToken, string itemCode, CreateRequestMdl model)
        {
           
            string apiUrl = string.Format("{0}/license/{1}/issue?access_token={2}", Constants.BaseUrl, itemCode, accessToken);

            //FileLog.WriteLog(string.Format("CreatePDF，url：【{0}】。参数：【{1}】", apiUrl, JSON.Encode(model)));

            return JsonConvert.DeserializeObject<CreateResponseResult>(ConnectionHelper.DoPost(apiUrl, JSON.Encode(model)));
        }

        /// <summary>
        ///     PDF签章制证
        /// </summary>
        /// <param name="accessToken">登录令牌</param>
        /// <param name="itemCode">电子证照目录编码</param>
        /// <param name="model">对象</param>
        /// <returns></returns>
        public static CreateResponseResult CreatePDF_EJUse(string accessToken, string itemCode, CreateRequestGBMdl model)
        {

            string apiUrl = string.Format("{0}/license/{1}/issue?access_token={2}", Constants.BaseUrl, itemCode, accessToken);

            //FileLog.WriteLog(string.Format("CreatePDF，url：【{0}】。参数：【{1}】", apiUrl, JSON.Encode(model)));

            return JsonConvert.DeserializeObject<CreateResponseResult>(ConnectionHelper.DoPost(apiUrl, JSON.Encode(model)));
        }


        /// <summary>
        ///  Ofd签章制证
        /// </summary>
        /// <param name="accessToken">登录令牌</param>
        /// <param name="itemCode">电子证照目录编码</param>
        /// <param name="model">对象</param>
        /// <returns></returns>
        public static CreateResponseResult CreateOfd(string accessToken, string itemCode, CreateRequestGBMdl model)
        {
            //http://IP:10002/v1/license/目录实施码/issue?access_token=ACCESS_TOKEN
            string apiUrl = string.Format("{0}/license/{1}/issue?access_token={2}", Constants.BaseUrlOfd, itemCode, accessToken);

            //FileLog.WriteLog(string.Format("CreatePDF，url：【{0}】。参数：【{1}】", apiUrl, JSON.Encode(model)));

            return JsonConvert.DeserializeObject<CreateResponseResult>(ConnectionHelper.DoPost(apiUrl, JSON.Encode(model)));
        }


        /// <summary>
        /// 下载PDF签章结果
        /// </summary>
        /// <param name="accessToken">令牌</param>
        /// <param name="auth_code">电子证照用证码</param>
        /// <returns></returns>
        public static DownResponseResult DownPDF(string accessToken, string auth_code)
        {
            string apiUrl = string.Format("{0}/license/archive?", Constants.BaseUrl);

            System.Collections.Hashtable hash = new System.Collections.Hashtable();
            hash.Add("access_token", accessToken);
            hash.Add("auth_code", auth_code);

            return JsonConvert.DeserializeObject<DownResponseResult>(ConnectionHelper.DoGet(apiUrl, hash));
        }

        /// <summary>
        /// 下载Ofd签章结果
        /// </summary>
        /// <param name="accessToken">令牌</param>
        /// <param name="auth_code">电子证照用证码</param>
        /// <returns></returns>
        public static DownResponseResult DownOfd(string accessToken, string auth_code)
        {
            string apiUrl = string.Format("{0}/license/archive?", Constants.BaseUrlOfd);

            System.Collections.Hashtable hash = new System.Collections.Hashtable();
            hash.Add("access_token", accessToken);
            hash.Add("auth_code", auth_code);

            return JsonConvert.DeserializeObject<DownResponseResult>(ConnectionHelper.DoGet(apiUrl, hash));
        }
    }
}