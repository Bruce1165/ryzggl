using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--ReEducateCertificateMDL填写类描述
	/// </summary>
	[Serializable]
	public class ReEducateCertificateMDL
	{
		public ReEducateCertificateMDL()
		{
		}

		
        /// <summary>
        /// 
        /// </summary>
		public long? ReEducateCertificateID{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string CertificateCode{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string PostTypeName{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string PostName{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime? ConferDate{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime? ValidEndDate{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string WorkerName{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string WorkerCertificateCode{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string ReEducateCertificateCode{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime? ReEducateConferDate{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public int? Period{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public long? PackageID{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string PackageTitle{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string TestStatus{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public int? PackageYear{ get; set; }
	}
}
