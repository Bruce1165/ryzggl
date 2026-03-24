using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--EJCertUseMDL填写类描述
	/// </summary>
	[Serializable]
	public class EJCertUseMDL
	{
		public EJCertUseMDL()
		{
		}


        /// <summary>
        /// 电子证书使用件ID
        /// </summary>
        public string CertificateCAID { get; set; }

        /// <summary>
        /// 证书ID
        /// </summary>
        public string PSN_ServerID { get; set; }

        /// <summary>
        /// 使用开始日期
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 使用结束日期
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CJSJ { get; set; }

        /// <summary>
        /// 状态：1：有效，0：失效
        /// </summary>
        public int? Valid { get; set; }

        /// <summary>
        /// 生成电子证书时间
        /// </summary>
        public DateTime? ApplyCATime { get; set; }

        /// <summary>
        /// Pdf_签章时间
        /// </summary>
		public DateTime? Pdf_SignCATime{ get; set; }

        /// <summary>
        /// Pdf_电子证照标识码
        /// </summary>
		public string Pdf_license_code{ get; set; }

        /// <summary>
        /// Pdf_电子证照查验码
        /// </summary>
		public string Pdf_auth_code{ get; set; }

        /// <summary>
        /// Pdf_签章结果返回时间
        /// </summary>
		public DateTime? Pdf_ReturnCATime{ get; set; }

        /// <summary>
        /// Ofd_签章时间
        /// </summary>
        public DateTime? Ofd_SignCATime { get; set; }

        /// <summary>
        /// Ofd_电子证照标识码
        /// </summary>
        public string Ofd_license_code { get; set; }

        /// <summary>
        /// Ofd_电子证照查验码
        /// </summary>
        public string Ofd_auth_code { get; set; }

        /// <summary>
        /// Ofd_签章结果返回时间
        /// </summary>
        public DateTime? Ofd_ReturnCATime { get; set; }

        /// <summary>
        /// 国标证书标识
        /// </summary>
        public string ZZBS { get; set; }

        /// <summary>
        /// 企业ID
        /// </summary>
        public string ENT_ServerID { get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
        public string ENT_Name { get; set; }

        /// <summary>
        /// 企业社会统一信用代码
        /// </summary>
        public string ENT_OrganizationsCode { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string PSN_Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string PSN_Sex { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? PSN_BirthDate { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string PSN_CertificateType { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string PSN_CertificateNO { get; set; }

        /// <summary>
        /// 注册类型
        /// </summary>
        public string PSN_RegisteType { get; set; }

        /// <summary>
        /// 注册编号
        /// </summary>
        public string PSN_RegisterNO { get; set; }

        /// <summary>
        /// 证书编号
        /// </summary>
        public string PSN_RegisterCertificateNo { get; set; }

        /// <summary>
        /// 注册专业
        /// </summary>
        public string PSN_RegisteProfession { get; set; }

        /// <summary>
        /// 发证日期
        /// </summary>
        public DateTime? PSN_CertificationDate { get; set; }

        /// <summary>
        /// 有效期至
        /// </summary>
        public DateTime? PSN_CertificateValidity { get; set; }

        /// <summary>
        /// 注册审批日期
        /// </summary>
        public DateTime? PSN_RegistePermissionDate { get; set; }

        /// <summary>
        /// 资格证书编号
        /// </summary>
        public string ZGZSBH { get; set; }
	}
}
