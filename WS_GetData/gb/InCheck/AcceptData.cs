using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace WS_GetData.gb.InCheck
{
    /// <summary>
    ///  本省转出三类人证书在外省转入数据查询请求
    /// </summary>
    public class AcceptData
    {
        /// <summary>
        /// 指定页号，以 0 为起始数字，表示第1页
        /// </summary>
        [JsonProperty("pageindex")]
        public int pageindex { get; set; }

        /// <summary>
        /// 每页记录数，最多不能超过 100
        /// </summary>
        [JsonProperty("pagesize")]
        public int pagesize { get; set; }

        /// <summary>
        /// 所属省份，按照民政部官网《2020年中华人民共和国行政区划代码》,C6
        /// </summary>
        [JsonProperty("provinceNum")]
        public string provinceNum { get; set; }

        /// <summary>
        ///     证书编号（证书唯一编号，按照《全国一体化政务服务平台电子证照 建筑施工企业安全生产管理人员考核合格证书》附录A.1编号规则生成）
        /// </summary>
        [JsonProperty("certNum")]
        public string certNum { get; set; }
    }
}