using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WS_GetData.gb.Login
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Custom
    {
        /// <summary>
        /// 最终获取到的token
        /// </summary>
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        /// <summary>
        /// 刷新token票据
        /// </summary>
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [JsonProperty("jsessionid")]
        public string Jsessionid { get; set; }

        /// <summary>
        /// token有效期
        /// </summary>
        [JsonProperty("expires_in")]
        public string ExpiresIn { get; set; }
    }
}
