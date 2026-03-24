using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace WS_GetData.gb.OutCheck
{
    /// <summary>
    /// 变更证件接口成功信息
    /// </summary>
    [Serializable]
    public class SuccessData
    {
        /// <summary>
        /// 证书编号
        /// </summary>
        [JsonProperty("certNum")]
        public string certNum { get; set; }
        /// <summary>
        /// 所属省份
        /// </summary>
        [JsonProperty("provinceNum")]
        public string provinceNum { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [JsonProperty("gender")]
        public string gender { get; set; }
        /// <summary>
        /// 身份证件号码类型代码
        /// </summary>
        [JsonProperty("identityCardType")]
        public string identityCardType { get; set; }
        /// <summary>
        /// 身份证件号码
        /// </summary>
        [JsonProperty("identityCard")]
        public string identityCard { get; set; }
        /// <summary>
        /// 出生日期
        /// </summary>
        [JsonProperty("birthDate")]
        public DateTime birthDate { get; set; }
        /// <summary>
        /// 岗位类别代码
        /// </summary>
        [JsonProperty("categoryCode")]
        public string categoryCode { get; set; }
        /// <summary>
        /// 企业名称
        /// </summary>
        [JsonProperty("companyName")]
        public string companyName { get; set; }
        /// <summary>
        /// 统一社会信用代码
        /// </summary>
        [JsonProperty("creditCode")]
        public string creditCode { get; set; }
        /// <summary>
        /// 发证机关
        /// </summary>
        [JsonProperty("issuAuth")]
        public string issuAuth { get; set; }
        /// <summary>
        /// 证书状态
        /// </summary>
        [JsonProperty("certState")]
        public string certState { get; set; }
        /// <summary>
        /// 证书状态描述
        /// </summary>
        [JsonProperty("certStatusDescription")]
        public string certStatusDescription { get; set; }
        /// <summary>
        /// 初次领证日期
        /// </summary>
        [JsonProperty("issuedDate")]
        public DateTime issuedDate { get; set; }
        /// <summary>
        /// 发证日期
        /// </summary>
        [JsonProperty("issuDate")]
        public DateTime issuDate { get; set; }
        /// <summary>
        /// 有效期起始日期
        /// </summary>
        [JsonProperty("effectiveDate")]
        public DateTime effectiveDate { get; set; }
        /// <summary>
        /// 有效期结束日期
        /// </summary>
        [JsonProperty("expiringDate")]
        public DateTime expiringDate { get; set; }
        /// <summary>
        /// 证照标识
        /// </summary>
        [JsonProperty("zzeCertID")]
        public string zzeCertID { get; set; }
        /// <summary>
        /// 成功信息
        /// </summary>
        [JsonProperty("SuccessMsg")]
        public string SuccessMsg { get; set; }
    }
}
