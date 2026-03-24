using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--zjs_CertificateMDL填写类描述
	/// </summary>
	[Serializable]
	public class zjs_CertificateMDL
	{
		public zjs_CertificateMDL()
		{			
		}
			
		//主键
		protected string _PSN_ServerID;
		
		//其它属性
		protected string _ENT_Name;
		protected string _ENT_OrganizationsCode;
		protected string _ENT_City;
		protected string _END_Addess;
		protected string _PSN_Name;
		protected string _PSN_Sex;
		protected DateTime? _PSN_BirthDate;
		protected string _PSN_National;
		protected string _PSN_CertificateType;
		protected string _PSN_CertificateNO;
		protected string _PSN_GraduationSchool;
		protected string _PSN_Specialty;
		protected DateTime? _PSN_GraduationTime;
		protected string _PSN_Qualification;
		protected string _PSN_Degree;
		protected string _PSN_MobilePhone;
		protected string _PSN_Email;
		protected string _PSN_Telephone;
		protected string _PSN_RegisteType;
		protected string _PSN_RegisterNO;
		protected string _PSN_RegisterCertificateNo;
		protected string _PSN_RegisteProfession;
		protected DateTime? _PSN_CertificationDate;
		protected DateTime? _PSN_CertificateValidity;
		protected DateTime? _PSN_RegistePermissionDate;
		protected string _PSN_Level;
		protected string _ZGZSBH;
		protected string _CJR;
		protected DateTime? _CJSJ;
		protected string _XGR;
		protected DateTime? _XGSJ;
		protected int? _Valid;
		protected string _Memo;
		protected DateTime? _ApplyCATime;
		protected DateTime? _SendCATime;
		protected DateTime? _ReturnCATime;
		protected string _CertificateCAID;
		protected string _license_code;
		protected string _auth_code;
		protected DateTime? _SignCATime;
		
        /// <summary>
        /// 造价师ID
        /// </summary>
		public string PSN_ServerID
		{
			get {return _PSN_ServerID;}
			set {_PSN_ServerID = value;}
		}
        /// <summary>
        /// 企业名称
        /// </summary>
		public string ENT_Name
		{
			get {return _ENT_Name;}
			set {_ENT_Name = value;}
		}
        /// <summary>
        /// 机构代码(社会统一信用代码/组织机构代码) 
        /// </summary>
		public string ENT_OrganizationsCode
		{
			get {return _ENT_OrganizationsCode;}
			set {_ENT_OrganizationsCode = value;}
		}
        /// <summary>
        /// 所属区县
        /// </summary>
		public string ENT_City
		{
			get {return _ENT_City;}
			set {_ENT_City = value;}
		}
        /// <summary>
        /// 企业工商注册地址
        /// </summary>
		public string END_Addess
		{
			get {return _END_Addess;}
			set {_END_Addess = value;}
		}
        /// <summary>
        /// 姓名
        /// </summary>
		public string PSN_Name
		{
			get {return _PSN_Name;}
			set {_PSN_Name = value;}
		}
        /// <summary>
        /// 性别
        /// </summary>
		public string PSN_Sex
		{
			get {return _PSN_Sex;}
			set {_PSN_Sex = value;}
		}
        /// <summary>
        /// 出生日期
        /// </summary>
		public DateTime? PSN_BirthDate
		{
			get {return _PSN_BirthDate;}
			set {_PSN_BirthDate = value;}
		}
        /// <summary>
        /// 民族
        /// </summary>
		public string PSN_National
		{
			get {return _PSN_National;}
			set {_PSN_National = value;}
		}              
        /// <summary>
        /// 证件类别
        /// </summary>
		public string PSN_CertificateType
		{
			get {return _PSN_CertificateType;}
			set {_PSN_CertificateType = value;}
		}
        /// <summary>
        /// 证件号码
        /// </summary>
		public string PSN_CertificateNO
		{
			get {return _PSN_CertificateNO;}
			set {_PSN_CertificateNO = value;}
		}
        /// <summary>
        /// 毕业院校
        /// </summary>
		public string PSN_GraduationSchool
		{
			get {return _PSN_GraduationSchool;}
			set {_PSN_GraduationSchool = value;}
		}
        /// <summary>
        /// 所学专业
        /// </summary>
		public string PSN_Specialty
		{
			get {return _PSN_Specialty;}
			set {_PSN_Specialty = value;}
		}
        /// <summary>
        /// 毕业时间
        /// </summary>
		public DateTime? PSN_GraduationTime
		{
			get {return _PSN_GraduationTime;}
			set {_PSN_GraduationTime = value;}
		}
        /// <summary>
        /// 学历
        /// </summary>
		public string PSN_Qualification
		{
			get {return _PSN_Qualification;}
			set {_PSN_Qualification = value;}
		}
        /// <summary>
        /// 学位
        /// </summary>
		public string PSN_Degree
		{
			get {return _PSN_Degree;}
			set {_PSN_Degree = value;}
		}
        /// <summary>
        /// 手机
        /// </summary>
		public string PSN_MobilePhone
		{
			get {return _PSN_MobilePhone;}
			set {_PSN_MobilePhone = value;}
		}
        /// <summary>
        /// 电子邮件
        /// </summary>
		public string PSN_Email
		{
			get {return _PSN_Email;}
			set {_PSN_Email = value;}
		}
        /// <summary>
        /// 联系电话
        /// </summary>
		public string PSN_Telephone
		{
			get {return _PSN_Telephone;}
			set {_PSN_Telephone = value;}
		}
        /// <summary>
        /// 注册类别
        /// </summary>
		public string PSN_RegisteType
		{
			get {return _PSN_RegisteType;}
			set {_PSN_RegisteType = value;}
		}
        /// <summary>
        /// 注册号
        /// </summary>
		public string PSN_RegisterNO
		{
			get {return _PSN_RegisterNO;}
			set {_PSN_RegisterNO = value;}
		}
        /// <summary>
        /// 注册证书编号
        /// </summary>
		public string PSN_RegisterCertificateNo
		{
			get {return _PSN_RegisterCertificateNo;}
			set {_PSN_RegisterCertificateNo = value;}
		}
        /// <summary>
        /// 注册专业
        /// </summary>
		public string PSN_RegisteProfession
		{
			get {return _PSN_RegisteProfession;}
			set {_PSN_RegisteProfession = value;}
		}
        /// <summary>
        /// 发证日期
        /// </summary>
		public DateTime? PSN_CertificationDate
		{
			get {return _PSN_CertificationDate;}
			set {_PSN_CertificationDate = value;}
		}
        /// <summary>
        /// 证书有效期
        /// </summary>
		public DateTime? PSN_CertificateValidity
		{
			get {return _PSN_CertificateValidity;}
			set {_PSN_CertificateValidity = value;}
		}
        /// <summary>
        /// 注册审批日期
        /// </summary>
		public DateTime? PSN_RegistePermissionDate
		{
			get {return _PSN_RegistePermissionDate;}
			set {_PSN_RegistePermissionDate = value;}
		}
        /// <summary>
        /// 证书等级
        /// </summary>
		public string PSN_Level
		{
			get {return _PSN_Level;}
			set {_PSN_Level = value;}
		}
        /// <summary>
        /// 资格证书编号
        /// </summary>
		public string ZGZSBH
		{
			get {return _ZGZSBH;}
			set {_ZGZSBH = value;}
		}

        /// <summary>
        /// 创建人
        /// </summary>
        public string CJR
        {
            get { return _CJR; }
            set { _CJR = value; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CJSJ
        {
            get { return _CJSJ; }
            set { _CJSJ = value; }
        }
        /// <summary>
        /// 最后修改人
        /// </summary>
        public string XGR
        {
            get { return _XGR; }
            set { _XGR = value; }
        }
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? XGSJ
        {
            get { return _XGSJ; }
            set { _XGSJ = value; }
        }
        /// <summary>
        /// 有效标志：1有效，0无效
        /// </summary>
        public int? Valid
        {
            get { return _Valid; }
            set { _Valid = value; }
        }
        /// <summary>
        /// 备注
        /// </summary>
        public string Memo
        {
            get { return _Memo; }
            set { _Memo = value; }
        }               
        /// <summary>
        /// 申请电子证照时间
        /// </summary>
		public DateTime? ApplyCATime
		{
			get {return _ApplyCATime;}
			set {_ApplyCATime = value;}
		}
        /// <summary>
        /// 发送电子签章时间
        /// </summary>
		public DateTime? SendCATime
		{
			get {return _SendCATime;}
			set {_SendCATime = value;}
		}
        /// <summary>
        /// 取回电子证照时间
        /// </summary>
		public DateTime? ReturnCATime
		{
			get {return _ReturnCATime;}
			set {_ReturnCATime = value;}
		}
        /// <summary>
        /// 电子证照唯一标识
        /// </summary>
		public string CertificateCAID
		{
			get {return _CertificateCAID;}
			set {_CertificateCAID = value;}
		}
        /// <summary>
        /// 电子证照标识码
        /// </summary>
		public string license_code
		{
			get {return _license_code;}
			set {_license_code = value;}
		}
        /// <summary>
        /// 电子证照查验码
        /// </summary>
		public string auth_code
		{
			get {return _auth_code;}
			set {_auth_code = value;}
		}
        /// <summary>
        /// 签章回写时间
        /// </summary>
		public DateTime? SignCATime
		{
			get {return _SignCATime;}
			set {_SignCATime = value;}
		}
	}
}
