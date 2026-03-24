using System;
using Newtonsoft.Json;

namespace WS_GetData.Api.Model
{
    /// <summary>
    ///     制证操作人信息
    /// </summary>
    [Serializable]
    public class UserInfoMdl
    {
        /// <summary>
        ///     app账号
        /// </summary>
        [JsonProperty("account")]
        public string Account { set; get; }

        /// <summary>
        ///     工作人员姓名
        /// </summary>
        [JsonProperty("name")]
        public string Name { set; get; }

        /// <summary>
        ///     窗口工作人员身份证号码
        /// </summary>
        [JsonProperty("identity_num")]
        public string IdentityNum { set; get; }

        /// <summary>
        ///     工作人员角色
        /// </summary>
        [JsonProperty("role")]
        public string Role { set; get; }

        /// <summary>
        ///     所属机构
        /// </summary>
        [JsonProperty("service_org")]
        public string ServiceOrg { set; get; }

        /// <summary>
        ///     行政区划名称
        /// </summary>
        [JsonProperty("division")]
        public string Division { set; get; }

        /// <summary>
        ///     行政区划代码
        /// </summary>
        [JsonProperty("division_code")]
        public string DivisionCode { set; get; }


        /// <summary>
        /// </summary>
        /// <returns></returns>
        public static UserInfoMdl SetData()
        {
            return new UserInfoMdl
            {
                Account = "bjszfhcxjsw",
                Division = "北京市",
                DivisionCode = "110000",
                ServiceOrg = "市住建委",
                Role = "市住建委基础库",
                Name = "住建委基础库",
                IdentityNum = ""
            };
        }
    }
}