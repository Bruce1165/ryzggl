using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Model
{
	/// <summary>
	/// 业务实体类--SheBaoMDL社保查询结果
	/// </summary>
	[Serializable]
	public class SheBaoMDL
	{
		public SheBaoMDL()
		{
		}

		
        /// <summary>
        /// 社会保障号码
        /// </summary>
		public string CertificateCode{ get; set; }

        /// <summary>
        /// 缴费月份
        /// </summary>
		public int? JFYF{ get; set; }

        /// <summary>
        /// 险种代码
        /// </summary>
		public string XZCode{ get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
		public string WorkerName{ get; set; }

        /// <summary>
        /// 社会统一信用代码
        /// </summary>
		public string CreditCode{ get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
		public string ENT_Name{ get; set; }

        /// <summary>
        /// 险种名称
        /// </summary>
		public string XZName{ get; set; }

        /// <summary>
        /// 创建时间 
        /// </summary>
		public DateTime? CJSJ{ get; set; }
	}

    /// <summary>
    ///  社保查询Response
    /// </summary>
    [Serializable]
    public class ResponseResultSheBao
    {

        /// <summary>
        ///  查询时间：格式：yyyymmddhhmmss
        /// </summary>
        [JsonProperty("time")]
        public string time { get; set; }
        /// <summary>
        ///  查询结果编码，  200=成功
        /// </summary>
        [JsonProperty("code")]
        public int code { get; set; }

        /// <summary>
        ///  查询结果描述
        /// </summary>
        [JsonProperty("message")]
        public string message { get; set; }

        /// <summary>
        ///   返回的数据
        /// </summary>
        [JsonProperty("data")]
        public SheBaoData data { get; set; }
    }

    /// <summary>
    /// 社保查询结果
    /// </summary>
    [Serializable]
    public class SheBaoData
    {
        /// <summary>
        ///  社保缴费记录集合（五险）
        /// </summary>
        [JsonProperty("CZZGSBJFYJL")]
        public List<SBJFYJL> CZZGSBJFYJL { get; set; }
    }

    /// <summary>
    ///  社保缴费记录
    /// </summary>
    [Serializable]
    public class SBJFYJL
    {
        /// <summary>
        ///  缴费日期
        /// </summary>
        [JsonProperty("PAE001")]
        public string PAE001 { get; set; }

        /// <summary>
        ///  社会统一信用代码
        /// </summary>
        [JsonProperty("AAB003")]
        public string AAB003 { get; set; }

        /// <summary>
        ///  缴费单位
        /// </summary>
        [JsonProperty("AAB004")]
        public string AAB004 { get; set; }

        /// <summary>
        ///  险种编码
        /// </summary>
        [JsonProperty("BZE016")]
        public string BZE016 { get; set; }

        /// <summary>
        ///  险种描述
        /// </summary>
        [JsonProperty("BZE016D")]
        public string BZE016D { get; set; }
    }
}
