using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--CertificatePauseMDL 电子证书暂扣记录（推送国家库）
	/// </summary>
	[Serializable]
	public class CertificatePauseMDL
	{
		public CertificatePauseMDL()
		{
		}

		
        /// <summary>
        /// 暂扣记录ID
        /// </summary>
		public long? PauseID{ get; set; }

        /// <summary>
        /// 岗位类别ID
        /// </summary>
		public int? PostTypeID{ get; set; }

        /// <summary>
        /// 证书编号
        /// </summary>
		public string CertificateCode{ get; set; }

        /// <summary>
        /// 申请暂扣时间
        /// </summary>
		public DateTime? PauseApplyTime{ get; set; }

        /// <summary>
        /// 暂扣确定时间
        /// </summary>
		public DateTime? PauseDoTime{ get; set; }

        /// <summary>
        /// 暂扣申请人
        /// </summary>
		public string PauseApplyMan{ get; set; }

        /// <summary>
        /// 解除暂扣申请时间
        /// </summary>
		public DateTime? EndPauseApplyTime{ get; set; }

        /// <summary>
        /// 解除暂扣确定时间
        /// </summary>
		public DateTime? EndPauseDoTime{ get; set; }

        /// <summary>
        /// 暂扣解除人
        /// </summary>
		public string EndPauseApplyMan{ get; set; }

        /// <summary>
        /// 暂扣状态编码【1：申请暂扣；2：已暂扣；3：申请返还；4：已返还】
        /// </summary>
		public int? PauseStatusCode{ get; set; }

        /// <summary>
        /// 暂扣状态：申请暂扣；已暂扣；申请返还；已返还。
        /// </summary>
		public string PauseStatus{ get; set; }

        /// <summary>
        /// 备注
        /// </summary>
		public string Remark{ get; set; }
	}
}
