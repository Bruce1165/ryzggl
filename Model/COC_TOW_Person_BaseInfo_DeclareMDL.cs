using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--COC_TOW_Person_BaseInfo_DeclareMDL填写类描述
	/// </summary>
	[Serializable]
	public class COC_TOW_Person_BaseInfo_DeclareMDL
	{
		public COC_TOW_Person_BaseInfo_DeclareMDL()
		{			
		}
			
		//主键
		protected string _PSN_ServerID;
		
		//其它属性
		protected string _ENT_ServerID;
		protected DateTime? _BeginTime;
		protected DateTime? _EndTime;
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
		protected string _PSN_Telephone;
		protected string _PSN_Email;
		protected string _PSN_PMGrade;
		protected string _PSN_PMCertificateNo;
		protected string _PSN_RegisteType;
		protected string _PSN_RegisterNO;
		protected string _PSN_RegisterCertificateNo;
		protected string _PSN_RegisteProfession;
		protected DateTime? _PSN_CertificationDate;
		protected DateTime? _PSN_CertificateValidity;
		protected DateTime? _PSN_RegistePermissionDate;
		protected string _PSN_ChangeReason;
		protected string _PSN_BeforENT_Name;
		protected string _PSN_BeforENT_ServerID;
		protected string _PSN_BeforPersonName;
		protected string _PSN_InterprovincialChange;
		protected string _PSN_ExpiryReasons;
		protected DateTime? _PSN_ExpiryDate;
		protected string _PSN_RenewalProfession;
		protected string _PSN_AddProfession;
		protected string _PSN_CancelPerson;
		protected string _PSN_CancelReason;
		protected string _PSN_ReReasons;
		protected string _PSN_ReContent;
		protected string _PSN_CheckCode;
		protected string _ENT_Province_Code;
		protected string _PSN_Level;
		protected string _CJR;
		protected DateTime? _CJSJ;
		protected string _XGR;
		protected DateTime? _XGSJ;
		protected int? _Valid;
		protected string _Memo;
		protected string _SBLX;
		protected bool? _QXSLZT;
		protected DateTime? _QXSLSJ;
		protected string _QXSLBZ;
		protected bool? _ZYZGSCZT;
		protected DateTime? _ZYZGSCSJ;
		protected string _ZYZGSCBZ;
		protected bool? _ZYZGZCZXJD;
		protected DateTime? _ZYZGZCZXJDSJ;
		protected string _ZYZGZCZXJDBZ;
		
		public string PSN_ServerID
		{
			get {return _PSN_ServerID;}
			set {_PSN_ServerID = value;}
		}

		public string ENT_ServerID
		{
			get {return _ENT_ServerID;}
			set {_ENT_ServerID = value;}
		}

		public DateTime? BeginTime
		{
			get {return _BeginTime;}
			set {_BeginTime = value;}
		}

		public DateTime? EndTime
		{
			get {return _EndTime;}
			set {_EndTime = value;}
		}

		public string PSN_Name
		{
			get {return _PSN_Name;}
			set {_PSN_Name = value;}
		}

		public string PSN_Sex
		{
			get {return _PSN_Sex;}
			set {_PSN_Sex = value;}
		}

		public DateTime? PSN_BirthDate
		{
			get {return _PSN_BirthDate;}
			set {_PSN_BirthDate = value;}
		}

		public string PSN_National
		{
			get {return _PSN_National;}
			set {_PSN_National = value;}
		}

		public string PSN_CertificateType
		{
			get {return _PSN_CertificateType;}
			set {_PSN_CertificateType = value;}
		}

		public string PSN_CertificateNO
		{
			get {return _PSN_CertificateNO;}
			set {_PSN_CertificateNO = value;}
		}

		public string PSN_GraduationSchool
		{
			get {return _PSN_GraduationSchool;}
			set {_PSN_GraduationSchool = value;}
		}

		public string PSN_Specialty
		{
			get {return _PSN_Specialty;}
			set {_PSN_Specialty = value;}
		}

		public DateTime? PSN_GraduationTime
		{
			get {return _PSN_GraduationTime;}
			set {_PSN_GraduationTime = value;}
		}

		public string PSN_Qualification
		{
			get {return _PSN_Qualification;}
			set {_PSN_Qualification = value;}
		}

		public string PSN_Degree
		{
			get {return _PSN_Degree;}
			set {_PSN_Degree = value;}
		}

		public string PSN_MobilePhone
		{
			get {return _PSN_MobilePhone;}
			set {_PSN_MobilePhone = value;}
		}

		public string PSN_Telephone
		{
			get {return _PSN_Telephone;}
			set {_PSN_Telephone = value;}
		}

		public string PSN_Email
		{
			get {return _PSN_Email;}
			set {_PSN_Email = value;}
		}

		public string PSN_PMGrade
		{
			get {return _PSN_PMGrade;}
			set {_PSN_PMGrade = value;}
		}

		public string PSN_PMCertificateNo
		{
			get {return _PSN_PMCertificateNo;}
			set {_PSN_PMCertificateNo = value;}
		}

		public string PSN_RegisteType
		{
			get {return _PSN_RegisteType;}
			set {_PSN_RegisteType = value;}
		}

		public string PSN_RegisterNO
		{
			get {return _PSN_RegisterNO;}
			set {_PSN_RegisterNO = value;}
		}

		public string PSN_RegisterCertificateNo
		{
			get {return _PSN_RegisterCertificateNo;}
			set {_PSN_RegisterCertificateNo = value;}
		}

		public string PSN_RegisteProfession
		{
			get {return _PSN_RegisteProfession;}
			set {_PSN_RegisteProfession = value;}
		}

		public DateTime? PSN_CertificationDate
		{
			get {return _PSN_CertificationDate;}
			set {_PSN_CertificationDate = value;}
		}

		public DateTime? PSN_CertificateValidity
		{
			get {return _PSN_CertificateValidity;}
			set {_PSN_CertificateValidity = value;}
		}

		public DateTime? PSN_RegistePermissionDate
		{
			get {return _PSN_RegistePermissionDate;}
			set {_PSN_RegistePermissionDate = value;}
		}

		public string PSN_ChangeReason
		{
			get {return _PSN_ChangeReason;}
			set {_PSN_ChangeReason = value;}
		}

		public string PSN_BeforENT_Name
		{
			get {return _PSN_BeforENT_Name;}
			set {_PSN_BeforENT_Name = value;}
		}

		public string PSN_BeforENT_ServerID
		{
			get {return _PSN_BeforENT_ServerID;}
			set {_PSN_BeforENT_ServerID = value;}
		}

		public string PSN_BeforPersonName
		{
			get {return _PSN_BeforPersonName;}
			set {_PSN_BeforPersonName = value;}
		}

		public string PSN_InterprovincialChange
		{
			get {return _PSN_InterprovincialChange;}
			set {_PSN_InterprovincialChange = value;}
		}

		public string PSN_ExpiryReasons
		{
			get {return _PSN_ExpiryReasons;}
			set {_PSN_ExpiryReasons = value;}
		}

		public DateTime? PSN_ExpiryDate
		{
			get {return _PSN_ExpiryDate;}
			set {_PSN_ExpiryDate = value;}
		}

		public string PSN_RenewalProfession
		{
			get {return _PSN_RenewalProfession;}
			set {_PSN_RenewalProfession = value;}
		}

		public string PSN_AddProfession
		{
			get {return _PSN_AddProfession;}
			set {_PSN_AddProfession = value;}
		}

		public string PSN_CancelPerson
		{
			get {return _PSN_CancelPerson;}
			set {_PSN_CancelPerson = value;}
		}

		public string PSN_CancelReason
		{
			get {return _PSN_CancelReason;}
			set {_PSN_CancelReason = value;}
		}

		public string PSN_ReReasons
		{
			get {return _PSN_ReReasons;}
			set {_PSN_ReReasons = value;}
		}

		public string PSN_ReContent
		{
			get {return _PSN_ReContent;}
			set {_PSN_ReContent = value;}
		}

		public string PSN_CheckCode
		{
			get {return _PSN_CheckCode;}
			set {_PSN_CheckCode = value;}
		}

		public string ENT_Province_Code
		{
			get {return _ENT_Province_Code;}
			set {_ENT_Province_Code = value;}
		}

		public string PSN_Level
		{
			get {return _PSN_Level;}
			set {_PSN_Level = value;}
		}

		public string CJR
		{
			get {return _CJR;}
			set {_CJR = value;}
		}

		public DateTime? CJSJ
		{
			get {return _CJSJ;}
			set {_CJSJ = value;}
		}

		public string XGR
		{
			get {return _XGR;}
			set {_XGR = value;}
		}

		public DateTime? XGSJ
		{
			get {return _XGSJ;}
			set {_XGSJ = value;}
		}

		public int? Valid
		{
			get {return _Valid;}
			set {_Valid = value;}
		}

		public string Memo
		{
			get {return _Memo;}
			set {_Memo = value;}
		}

		public string SBLX
		{
			get {return _SBLX;}
			set {_SBLX = value;}
		}

		public bool? QXSLZT
		{
			get {return _QXSLZT;}
			set {_QXSLZT = value;}
		}

		public DateTime? QXSLSJ
		{
			get {return _QXSLSJ;}
			set {_QXSLSJ = value;}
		}

		public string QXSLBZ
		{
			get {return _QXSLBZ;}
			set {_QXSLBZ = value;}
		}

		public bool? ZYZGSCZT
		{
			get {return _ZYZGSCZT;}
			set {_ZYZGSCZT = value;}
		}

		public DateTime? ZYZGSCSJ
		{
			get {return _ZYZGSCSJ;}
			set {_ZYZGSCSJ = value;}
		}

		public string ZYZGSCBZ
		{
			get {return _ZYZGSCBZ;}
			set {_ZYZGSCBZ = value;}
		}

		public bool? ZYZGZCZXJD
		{
			get {return _ZYZGZCZXJD;}
			set {_ZYZGZCZXJD = value;}
		}

		public DateTime? ZYZGZCZXJDSJ
		{
			get {return _ZYZGZCZXJDSJ;}
			set {_ZYZGZCZXJDSJ = value;}
		}

		public string ZYZGZCZXJDBZ
		{
			get {return _ZYZGZCZXJDBZ;}
			set {_ZYZGZCZXJDBZ = value;}
		}
	}
}
