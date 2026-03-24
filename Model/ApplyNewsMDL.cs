using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--ApplyNewsMDL填写类描述
	/// </summary>
	[Serializable]
	public class ApplyNewsMDL
	{
		public ApplyNewsMDL()
		{			
		}
			
		//主键
		protected string _ID;
		
		//其它属性
		protected string _PSN_Name;
		protected string _PSN_CertificateNO;
		protected string _PSN_RegisterNo;
		protected string _ApplyType;
		protected bool? _SFCK;
		protected string _ENT_OrganizationsCode;
		
		public string ID
		{
			get {return _ID;}
			set {_ID = value;}
		}

		public string PSN_Name
		{
			get {return _PSN_Name;}
			set {_PSN_Name = value;}
		}

		public string PSN_CertificateNO
		{
			get {return _PSN_CertificateNO;}
			set {_PSN_CertificateNO = value;}
		}

		public string PSN_RegisterNo
		{
			get {return _PSN_RegisterNo;}
			set {_PSN_RegisterNo = value;}
		}

		public string ApplyType
		{
			get {return _ApplyType;}
			set {_ApplyType = value;}
		}

		public bool? SFCK
		{
			get {return _SFCK;}
			set {_SFCK = value;}
		}

		public string ENT_OrganizationsCode
		{
			get {return _ENT_OrganizationsCode;}
			set {_ENT_OrganizationsCode = value;}
		}
	}
}
