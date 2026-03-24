using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WS_GetData.gb.Update.Aqscglry
{
    /// <summary>
    ///  安管人员证书信息更正状态请求数据
    /// </summary>
    public class AcceptData
    {
        /// <summary>
        ///  证书ID（必填）
        /// </summary>
        [JsonProperty("eCertID")]
        public string eCertID { get; set; }

        /// <summary>
        /// 证照标识（必填，《全国一体化政务服务平台 电子证照 建筑施工企业安全生产管理人员考核合格证书》标准附录A.2生成，例如："1.2.156.3005.2.11100000000013338W011.11430000006122244L.20230000403.002.S"）。
        /// </summary>
        [JsonProperty("zzeCertID")]
        public string zzeCertID { get; set; }

        /// <summary>
        /// 所属省份 110000。（必填）
        /// </summary>
        [JsonProperty("provinceNum")]
        public string provinceNum { get; set; }

        /// <summary>
        /// 证书编号。（必填）
        /// </summary>
        [JsonProperty("certNum")]
        public string certNum { get; set; }

        /// <summary>
        /// 身份证件号码（必填）
        /// </summary>
        [JsonProperty("identityCard")]
        public string identityCard { get; set; }

        /// <summary>
        /// 统一社会信用代码（必填）
        /// </summary>
        [JsonProperty("creditCode")]
        public string creditCode { get; set; }

        /// <summary>
        /// 岗位类别代码。A；B；C1；C2；C3；（必填）
        /// </summary>
        [JsonProperty("categoryCode")]
        public string categoryCode { get; set; }

        /// <summary>
        /// 证书状态（必填）
        /// 01	有效
        /// 02	暂扣
        /// 03	撤销
        /// 04	注销
        /// 05	失效
        /// 06	办理转出
        /// 07	吊销
        /// 99	其他
        /// </summary>
        [JsonProperty("certState")]
        public string certState { get; set; }

        /// <summary>
        /// 证书状态描述(当证书状态为“99”其他时，必填,进行的补充描述)。
        /// </summary>
        [JsonProperty("certStatusDescription")]
        public string certStatusDescription { get; set; }

        /// <summary>
        /// 操作类型（必填）
        /// 01	办理新发电子证照
        /// 02	办理延续
        /// 03	办理暂扣
        /// 04	办理过期失效
        /// 05	办理注销（撤销、吊销、办理转出）
        /// 06	办理暂扣发还
        /// 07	办理其他业务
        /// 08	办理转入
        /// 09	办理变更
        /// 10  取消办理转出
        /// </summary>
        [JsonProperty("operateType")]
        public string operateType { get; set; }
    }
}
