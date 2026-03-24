using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WS_GetData.gb.Verification.Tzzy
{
    /// <summary>
    ///  特种作业数据校验请求数据
    /// </summary>
    public class AcceptData
    {
        /// <summary>
        ///  业务数据ID
        /// </summary>
        [JsonProperty("eCertID")]
        public string eCertID { get; set; }
        /// <summary>
        ///     所属省份(按照民政部官网《2020年中华人民共和国行政区划代码》)
        /// </summary>
        [JsonProperty("provinceNum")]
        public string ProvinceNum { get; set; }

        /// <summary>
        ///     身份证件号码(持证人员的有效身份证件号)
        /// </summary>
        [JsonProperty("identityCard")]
        public string IdentityCard { get; set; }

        /// <summary>
        ///     工种类别（详见7.2.3.2工种类别字典表 ）
        ///     01	建筑电工
        ///     02	建筑架子工（普通脚手架）
        ///     03	建筑架子工（附着升降脚手架）
        ///     04	建筑起重司索信号工
        ///     05	建筑起重机械司机（塔式起重机）
        ///     06	建筑起重机械司机（施工升降机）
        ///     07	建筑起重机械司机（物料提升机）
        ///     08	建筑起重机械安装拆卸工（塔式起重机）
        ///     09	建筑起重机械安装拆卸工（施工升降机）
        ///     10	建筑起重机械安装拆卸工（物料提升机）
        ///     11	高处作业吊篮安装拆卸工
        ///     99	经省级以上住房和城乡建设主管部门认定的其他工种类别
        /// </summary>
        [JsonProperty("operationCategory")]
        public string OperationCategory { get; set; }


        /// <summary>
        /// 工种类别描述，当操作类别为经省级以上住房和城乡建设主管部门认定的其他工种类别时，应对具体操作类别进行的补充描
        /// </summary>
        [JsonProperty("categoryDescription")]
        public string categoryDescription { get; set; }

        /// <summary>
        ///     证书编号（证书唯一编号，按照《全国一体化政务服务平台电子证照 建筑施工特种作业操作资格证书》附录A.1编号规则生成），当操作类型是办理延续、办理变更时，需要必填。
        /// </summary>
        [JsonProperty("certNum")]
        public string CertNum { get; set; }

        /// <summary>
        /// 操作类型
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
