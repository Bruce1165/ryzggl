using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--CheckFeedBackMDL填写类描述
	/// </summary>
	[Serializable]
	public class CheckFeedBackMDL
	{
		public CheckFeedBackMDL()
		{
		}

		
        /// <summary>
        /// 填报单ID
        /// </summary>
		public string DataID{ get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
		public int? PatchCode{ get; set; }

        /// <summary>
        /// 监管类型
        /// </summary>
		public string CheckType{ get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime? CreateTime{ get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
		public string CJR{ get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
		public DateTime? PublishiTime{ get; set; }

        /// <summary>
        /// 上报截止时间
        /// </summary>
		public DateTime? LastReportTime{ get; set; }

        /// <summary>
        /// 状态
        /// </summary>
		public string DataStatus{ get; set; }

        /// <summary>
        /// 状态编码：未发布 = 0; 待反馈 = 1; 已驳回 = 2; 待审查 = 3; 待复审 = 4; 待决定 = 6; 已决定 = 7;
        /// </summary>
		public int? DataStatusCode{ get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
		public string WorkerName{ get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
		public string WorkerCertificateCode{ get; set; }

        /// <summary>
        /// 证书编号
        /// </summary>
		public string CertificateCode{ get; set; }

        /// <summary>
        /// 电话
        /// </summary>
		public string phone{ get; set; }

        /// <summary>
        /// 注册类别
        /// </summary>
		public string PostTypeName{ get; set; }

        /// <summary>
        /// 注册单位
        /// </summary>
		public string Unit{ get; set; }

        /// <summary>
        /// 单位社会统一代码
        /// </summary>
		public string UnitCode{ get; set; }

        /// <summary>
        /// 所属区
        /// </summary>
		public string Country{ get; set; }

        /// <summary>
        /// 社保情况
        /// </summary>
		public string SheBaoCase{ get; set; }

        /// <summary>
        /// 社保单位
        /// </summary>
		public string ShebaoUnit{ get; set; }

        /// <summary>
        /// 公积金情况
        /// </summary>
		public string GongjijinCase{ get; set; }

        /// <summary>
        /// 在施项目
        /// </summary>
		public string ProjectCase{ get; set; }

        /// <summary>
        /// 数据来源时间
        /// </summary>
		public DateTime? SourceTime{ get; set; }

        /// <summary>
        /// 个人情况说明
        /// </summary>
		public string CaseDesc{ get; set; }

        /// <summary>
        /// 个人上报时间
        /// </summary>
		public DateTime? WorkerRerpotTime{ get; set; }

        /// <summary>
        /// 初审机构
        /// </summary>
		public string AcceptCountry{ get; set; }

        /// <summary>
        /// 初审受理时间
        /// </summary>
        public DateTime? AcceptTime { get; set; }

        /// <summary>
        /// 初审人
        /// </summary>
		public string AcceptMan{ get; set; }

        /// <summary>
        /// 初审意见
        /// </summary>
		public string AcceptResult{ get; set; }

        /// <summary>
        /// 初审上报时间
        /// </summary>
		public DateTime? CountryReportTime{ get; set; }

        /// <summary>
        /// 初审少报批次号
        /// </summary>
		public string CountryReportCode{ get; set; }

        /// <summary>
        /// 市建委审核时间
        /// </summary>
		public DateTime? CheckTime{ get; set; }

        /// <summary>
        /// 市建委审核人
        /// </summary>
		public string CheckMan{ get; set; }

        /// <summary>
        /// 市建委审核意见
        /// </summary>
		public string CheckResult{ get; set; }

        /// <summary>
        /// 市建委决定时间
        /// </summary>
		public DateTime? ConfirmTime{ get; set; }

        /// <summary>
        /// 市建委决定人
        /// </summary>
		public string ConfirmMan{ get; set; }

        /// <summary>
        /// 市建委决定意见
        /// </summary>
		public string ConfirmResult{ get; set; }

        /// <summary>
        /// 社保比对时间
        /// </summary>
		public DateTime? SheBaoCheckTime{ get; set; }

        /// <summary>
        /// 社保比对返回结果时间
        /// </summary>
		public DateTime? SheBaoRtnTime{ get; set; }

        /// <summary>
        /// 导入排序号
        /// </summary>
        public int? sn { get; set; }

        /// <summary>
        /// 驳回原因
        /// </summary>
        public string BackReason { get; set; }

        /// <summary>
        /// 驳回机构
        /// </summary>
        public string BackUnit { get; set; }

        /// <summary>
        /// 合格类型：已注销完成整改；社保一致完成整改；特殊六类总分公司；特殊六类已退休；特殊六类事业单位改制；特殊六类其他
        /// </summary>
        public string PassType { get; set; }
	}
}
