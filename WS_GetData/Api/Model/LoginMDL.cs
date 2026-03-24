using System;
using Newtonsoft.Json;

namespace WS_GetData.Api.Model
{
    /// <summary>
    /// </summary>
    [Serializable]
    public class LoginMDL
    {
        /// <summary>
        /// </summary>
        [JsonProperty("app_key")]
        public string AppKey { set; get; }

        /// <summary>
        /// </summary>
        [JsonProperty("app_secret")]
        public string AppSecret { set; get; }

        /// <summary>
        /// </summary>
        [JsonProperty("account")]
        public string Account { set; get; }

        /// <summary>
        /// </summary>
        [JsonProperty("password")]
        public string Password { set; get; }

        /// <summary>
        /// </summary>
        [JsonProperty("org_code")]
        public string OrgCode { set; get; }
    }
}