using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--UnitWatchMDL填写类描述
	/// </summary>
	[Serializable]
	public class UnitWatchMDL
	{
		public UnitWatchMDL()
		{			
		}
			
		//主键
        protected string _CreditCode;

        protected string _newCreditCode;
        
		
		//其它属性
		protected string _UnitName;
		protected string _UnitCertCode;
		protected string _ENT_City;
		protected string _FaRen;
		protected string _FRPhone;
		protected string _Address;
		protected DateTime? _CJSJ;
		protected DateTime? _ValidEnd;
		
		public string UnitName
		{
			get {return _UnitName;}
			set {_UnitName = value;}
		}

        public string newCreditCode
        {
            get { return _newCreditCode; }
            set { _newCreditCode = value; }
        }

		public string CreditCode
		{
			get {return _CreditCode;}
			set {_CreditCode = value;}
		}

		public string UnitCertCode
		{
			get {return _UnitCertCode;}
			set {_UnitCertCode = value;}
		}

		public string ENT_City
		{
			get {return _ENT_City;}
			set {_ENT_City = value;}
		}

		public string FaRen
		{
			get {return _FaRen;}
			set {_FaRen = value;}
		}

		public string FRPhone
		{
			get {return _FRPhone;}
			set {_FRPhone = value;}
		}

		public string Address
		{
			get {return _Address;}
			set {_Address = value;}
		}

		public DateTime? CJSJ
		{
			get {return _CJSJ;}
			set {_CJSJ = value;}
		}

		public DateTime? ValidEnd
		{
			get {return _ValidEnd;}
			set {_ValidEnd = value;}
		}
	}
}
