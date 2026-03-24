using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WS_GetData.gb.Update.Tzzy
{
    /// <summary>
    ///  特种作业证书信息更新状态请求数据
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
        /// 工种类别代码（必填）
        /// 01	建筑电工
        /// 02	建筑架子工（普通脚手架）
        /// 03	建筑架子工（附着升降脚手架）
        /// 04	建筑起重司索信号工
        /// 05	建筑起重机械司机（塔式起重机）
        /// 06	建筑起重机械司机（施工升降机）
        /// 07	建筑起重机械司机（物料提升机）
        /// 08	建筑起重机械安装拆卸工（塔式起重机）
        /// 09	建筑起重机械安装拆卸工（施工升降机）
        /// 10	建筑起重机械安装拆卸工（物料提升机）
        /// 11	高处作业吊篮安装拆卸工
        /// 99	经省级以上住房和城乡建设主管部门认定的其他工种类别
        /// </summary>
        [JsonProperty("operationCategory")]
        public string operationCategory { get; set; }

        /// <summary>
        /// 工种类别描述(当工种类别代码99时必填，固定格式为：“经省级以上住房和城乡建设主管部门认定的其他工种类别（XXX）)
        /// </summary>
        [JsonProperty("categoryDescription")]
        public string categoryDescription { get; set; }


        /// <summary>
        /// 证书状态（必填）
        /// 01	有效
        /// 02	暂扣
        /// 03	吊销
        /// 04	撤销
        /// 05	注销
        /// 06	失效
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
        /// 05	办理注销（撤销、吊销）
        /// 06	办理暂扣发还
        /// 07	办理其他业务
        /// 08	办理变更        
        /// </summary>
        [JsonProperty("operateType")]
        public string operateType { get; set; }

    }
}
