using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--FinishCertMDL公益培训学习证明
	/// </summary>
	[Serializable]
	public class FinishCertMDL
	{
		public FinishCertMDL()
		{
		}

		
        /// <summary>
        /// 证书编号
        /// </summary>
		public string CertificateCode{ get; set; }

        /// <summary>
        /// 有效期至
        /// </summary>
		public DateTime? ValidEndDate{ get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
		public string PostTypeName{ get; set; }

        /// <summary>
        /// 岗位工种
        /// </summary>
		public string PostName{ get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
		public string WorkerCertificateCode{ get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
		public string WorkerName{ get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime? CreateTime{ get; set; }

        /// <summary>
        /// 要求学时
        /// </summary>
		public int? Period{ get; set; }

        /// <summary>
        /// 完成学时
        /// </summary>
		public int? FinishPeriod{ get; set; }

        /// <summary>
        /// 达标时间
        /// </summary>
		public DateTime? FinishDate{ get; set; }

        /// <summary>
        /// 完成状态，0：未达标；1：已达标。
        /// </summary>
		public int? StudyStatus{ get; set; }
	}
}
