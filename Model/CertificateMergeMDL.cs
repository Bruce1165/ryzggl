using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--CertificateMergeMDL填写类描述
	/// </summary>
	[Serializable]
	public class CertificateMergeMDL
	{
		public CertificateMergeMDL()
		{			
		}
			
		//主键
		protected long? _ApplyID;
		
		//其它属性
		protected long? _WorkerID;
		protected string _CertificateType;
		protected string _WorkerName;
		protected string _WorkerCertificateCode;
		protected string _Sex;
		protected DateTime? _Birthday;
		protected string _FacePhoto;
		protected string _UnitName;
		protected string _UnitCode;
		protected long? _CertificateID1;
		protected string _PostName1;
		protected string _CertificateCode1;
		protected DateTime? _ConferDate1;
		protected DateTime? _ValidStartDate1;
		protected DateTime? _ValidEndDate1;
		protected string _PostName2;
		protected long? _CertificateID2;
		protected string _CertificateCode2;
		protected DateTime? _ConferDate2;
		protected DateTime? _ValidStartDate2;
		protected DateTime? _ValidEndDate2;
		protected string _ConferUnit;
		protected DateTime? _ApplyDate;
		protected string _ApplyMan;
		protected DateTime? _UnitCheckTime;
		protected string _UnitAdvise;
		protected string _CheckMan;
		protected string _CheckAdvise;
		protected DateTime? _CheckDate;
		protected string _ApplyStatus;
		protected long? _NewCertificateID;
		protected string _NewCertificateCode;
		protected long? _CreatePersonID;
		protected DateTime? _CreateTime;
		protected long? _ModifyPersonID;
		protected DateTime? _ModifyTime;
		
		public long? ApplyID
		{
			get {return _ApplyID;}
			set {_ApplyID = value;}
		}

		public long? WorkerID
		{
			get {return _WorkerID;}
			set {_WorkerID = value;}
		}

		public string CertificateType
		{
			get {return _CertificateType;}
			set {_CertificateType = value;}
		}

		public string WorkerName
		{
			get {return _WorkerName;}
			set {_WorkerName = value;}
		}

		public string WorkerCertificateCode
		{
			get {return _WorkerCertificateCode;}
			set {_WorkerCertificateCode = value;}
		}

		public string Sex
		{
			get {return _Sex;}
			set {_Sex = value;}
		}

		public DateTime? Birthday
		{
			get {return _Birthday;}
			set {_Birthday = value;}
		}

		public string FacePhoto
		{
			get {return _FacePhoto;}
			set {_FacePhoto = value;}
		}

		public string UnitName
		{
			get {return _UnitName;}
			set {_UnitName = value;}
		}

		public string UnitCode
		{
			get {return _UnitCode;}
			set {_UnitCode = value;}
		}

		public long? CertificateID1
		{
			get {return _CertificateID1;}
			set {_CertificateID1 = value;}
		}

		public string PostName1
		{
			get {return _PostName1;}
			set {_PostName1 = value;}
		}

		public string CertificateCode1
		{
			get {return _CertificateCode1;}
			set {_CertificateCode1 = value;}
		}

		public DateTime? ConferDate1
		{
			get {return _ConferDate1;}
			set {_ConferDate1 = value;}
		}

		public DateTime? ValidStartDate1
		{
			get {return _ValidStartDate1;}
			set {_ValidStartDate1 = value;}
		}

		public DateTime? ValidEndDate1
		{
			get {return _ValidEndDate1;}
			set {_ValidEndDate1 = value;}
		}

		public string PostName2
		{
			get {return _PostName2;}
			set {_PostName2 = value;}
		}

		public long? CertificateID2
		{
			get {return _CertificateID2;}
			set {_CertificateID2 = value;}
		}

		public string CertificateCode2
		{
			get {return _CertificateCode2;}
			set {_CertificateCode2 = value;}
		}

		public DateTime? ConferDate2
		{
			get {return _ConferDate2;}
			set {_ConferDate2 = value;}
		}

		public DateTime? ValidStartDate2
		{
			get {return _ValidStartDate2;}
			set {_ValidStartDate2 = value;}
		}

		public DateTime? ValidEndDate2
		{
			get {return _ValidEndDate2;}
			set {_ValidEndDate2 = value;}
		}

		public string ConferUnit
		{
			get {return _ConferUnit;}
			set {_ConferUnit = value;}
		}

		public DateTime? ApplyDate
		{
			get {return _ApplyDate;}
			set {_ApplyDate = value;}
		}

		public string ApplyMan
		{
			get {return _ApplyMan;}
			set {_ApplyMan = value;}
		}

		public DateTime? UnitCheckTime
		{
			get {return _UnitCheckTime;}
			set {_UnitCheckTime = value;}
		}

		public string UnitAdvise
		{
			get {return _UnitAdvise;}
			set {_UnitAdvise = value;}
		}

		public string CheckMan
		{
			get {return _CheckMan;}
			set {_CheckMan = value;}
		}

		public string CheckAdvise
		{
			get {return _CheckAdvise;}
			set {_CheckAdvise = value;}
		}

		public DateTime? CheckDate
		{
			get {return _CheckDate;}
			set {_CheckDate = value;}
		}

		public string ApplyStatus
		{
			get {return _ApplyStatus;}
			set {_ApplyStatus = value;}
		}

		public long? NewCertificateID
		{
			get {return _NewCertificateID;}
			set {_NewCertificateID = value;}
		}

		public string NewCertificateCode
		{
			get {return _NewCertificateCode;}
			set {_NewCertificateCode = value;}
		}

		public long? CreatePersonID
		{
			get {return _CreatePersonID;}
			set {_CreatePersonID = value;}
		}

		public DateTime? CreateTime
		{
			get {return _CreateTime;}
			set {_CreateTime = value;}
		}

		public long? ModifyPersonID
		{
			get {return _ModifyPersonID;}
			set {_ModifyPersonID = value;}
		}

		public DateTime? ModifyTime
		{
			get {return _ModifyTime;}
			set {_ModifyTime = value;}
		}
	}
}
