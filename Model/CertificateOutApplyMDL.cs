using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--CertificateOutApplyMDL填写类描述
	/// </summary>
	[Serializable]
	public class CertificateOutApplyMDL
	{
		public CertificateOutApplyMDL()
		{
		}

		
        /// <summary>
        /// 
        /// </summary>
		public long? ApplyID{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime? ApplyTime{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string WorkerCertificateCode{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public int? checkRtnCode{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string checkInfo{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime? checkTime{ get; set; }
	}
}
