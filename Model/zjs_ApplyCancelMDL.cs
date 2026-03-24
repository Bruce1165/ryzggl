using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--zjs_ApplyCancelMDL填写类描述
	/// </summary>
	[Serializable]
	public class zjs_ApplyCancelMDL
	{
		public zjs_ApplyCancelMDL()
		{			
		}
			
		//主键
		protected string _ApplyID;
		
		//其它属性
		protected string _END_Addess;
		protected string _ENT_Postcode;
		protected string _ENT_Telephone;
		protected string _PSN_MobilePhone;
		protected string _PSN_RegisterCertificateNo;
		protected string _PSN_RegisterNO;
		protected DateTime? _RegisterValidity;
		protected string _CancelReason;
		protected string _PSN_Email;
		protected string _LinkMan;
		protected string _ApplyManType;
		protected string _FR;
		protected string _ENT_Economic_Nature;
		
		public string ApplyID
		{
			get {return _ApplyID;}
			set {_ApplyID = value;}
		}

		public string END_Addess
		{
			get {return _END_Addess;}
			set {_END_Addess = value;}
		}

		public string ENT_Postcode
		{
			get {return _ENT_Postcode;}
			set {_ENT_Postcode = value;}
		}

		public string ENT_Telephone
		{
			get {return _ENT_Telephone;}
			set {_ENT_Telephone = value;}
		}

		public string PSN_MobilePhone
		{
			get {return _PSN_MobilePhone;}
			set {_PSN_MobilePhone = value;}
		}

		public string PSN_RegisterCertificateNo
		{
			get {return _PSN_RegisterCertificateNo;}
			set {_PSN_RegisterCertificateNo = value;}
		}

		public string PSN_RegisterNO
		{
			get {return _PSN_RegisterNO;}
			set {_PSN_RegisterNO = value;}
		}

		public DateTime? RegisterValidity
		{
			get {return _RegisterValidity;}
			set {_RegisterValidity = value;}
		}

		public string CancelReason
		{
			get {return _CancelReason;}
			set {_CancelReason = value;}
		}

		public string PSN_Email
		{
			get {return _PSN_Email;}
			set {_PSN_Email = value;}
		}

		public string LinkMan
		{
			get {return _LinkMan;}
			set {_LinkMan = value;}
		}

		public string ApplyManType
		{
			get {return _ApplyManType;}
			set {_ApplyManType = value;}
		}

		public string FR
		{
			get {return _FR;}
			set {_FR = value;}
		}

		public string ENT_Economic_Nature
		{
			get {return _ENT_Economic_Nature;}
			set {_ENT_Economic_Nature = value;}
		}
	}
}
