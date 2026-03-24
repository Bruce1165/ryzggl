using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--ApplyCheckTaskItemMDL填写类描述
	/// </summary>
	[Serializable]
	public class ApplyCheckTaskItemMDL
	{
		public ApplyCheckTaskItemMDL()
		{
		}

		
        /// <summary>
        /// 抽查明细ID
        /// </summary>
		public long? TaskItemID{ get; set; }

        /// <summary>
        /// 抽查ID
        /// </summary>
		public long? TaskID{ get; set; }

        /// <summary>
        /// 业务类型ID
        /// </summary>
		public int? BusTypeID{ get; set; }

        /// <summary>
        /// 申请单类型
        /// </summary>
		public string ApplyType{ get; set; }

        /// <summary>
        /// 申请单名称
        /// </summary>
		public string ApplyTableName{ get; set; }

        /// <summary>
        /// 数据ID
        /// </summary>
		public string DataID{ get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
		public string WorkerName{ get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
		public string IDCard{ get; set; }

        /// <summary>
        /// 证书类别
        /// </summary>
		public string IDCardType{ get; set; }

        /// <summary>
        /// 证书编号
        /// </summary>
		public string CertificateCode{ get; set; }

        /// <summary>
        /// 业务办结时间
        /// </summary>
		public DateTime? ApplyFinishTime{ get; set; }

        /// <summary>
        /// 抽查人
        /// </summary>
		public string CheckMan{ get; set; }

        /// <summary>
        /// 抽查时间
        /// </summary>
		public DateTime? CheckTime{ get; set; }

        /// <summary>
        /// 抽查审查结果
        /// </summary>
		public string CheckResult{ get; set; }

        /// <summary>
        /// 抽查审查结果说明
        /// </summary>
		public string CheckResultDesc{ get; set; }

        /// <summary>
        /// 复核人
        /// </summary>
		public string ReCheckMan{ get; set; }

        /// <summary>
        /// 复核时间
        /// </summary>
		public DateTime? ReCheckTime{ get; set; }
	}
}
