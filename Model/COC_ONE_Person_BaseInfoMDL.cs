using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--COC_ONE_Person_BaseInfoMDL填写类描述
	/// </summary>
	[Serializable]
	public class COC_ONE_Person_BaseInfoMDL
	{
		public COC_ONE_Person_BaseInfoMDL()
		{			
		}
			
		//主键
		protected string _Fid;
		
		//其它属性
		protected string _PSN_ServerID;
		protected string _PSN_ShareID;
		protected string _PSN_LocalID;
		protected string _ENT_ServerID;
		protected string _ENT_LocalID;
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
		protected string _DogID;
		protected string _ENT_Province_Code;
		protected string _DownType;
		protected DateTime? _LastModifyTime;
		
		public string Fid
		{
			get {return _Fid;}
			set {_Fid = value;}
		}

		public string PSN_ServerID
		{
			get {return _PSN_ServerID;}
			set {_PSN_ServerID = value;}
		}

		public string PSN_ShareID
		{
			get {return _PSN_ShareID;}
			set {_PSN_ShareID = value;}
		}

		public string PSN_LocalID
		{
			get {return _PSN_LocalID;}
			set {_PSN_LocalID = value;}
		}

		public string ENT_ServerID
		{
			get {return _ENT_ServerID;}
			set {_ENT_ServerID = value;}
		}

		public string ENT_LocalID
		{
			get {return _ENT_LocalID;}
			set {_ENT_LocalID = value;}
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

		public string DogID
		{
			get {return _DogID;}
			set {_DogID = value;}
		}

		public string ENT_Province_Code
		{
			get {return _ENT_Province_Code;}
			set {_ENT_Province_Code = value;}
		}

		public string DownType
		{
			get {return _DownType;}
			set {_DownType = value;}
		}

		public DateTime? LastModifyTime
		{
			get {return _LastModifyTime;}
			set {_LastModifyTime = value;}
		}
	}
}
