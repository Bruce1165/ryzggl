using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--CertificateOutMDL填写类描述
	/// </summary>
	[Serializable]
	public class CertificateOutMDL
	{
		public CertificateOutMDL()
		{
		}

		
        /// <summary>
        /// 
        /// </summary>
		public string out_certNum{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string out_provinceNum{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string out_name{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string out_gender{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string out_identityCard{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string out_identityCardType{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime? out_birthDate{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string out_categoryCode{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string out_companyName{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string out_creditCode{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string out_issuAuth{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string out_certState{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime? out_issuedDate{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime? out_effectiveDate{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime? out_expiringDate{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string out_zzeCertID{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime? cjsj{ get; set; }
	}
}
