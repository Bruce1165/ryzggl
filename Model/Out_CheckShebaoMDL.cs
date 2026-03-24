using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--Out_CheckShebaoMDL填写类描述
	/// </summary>
	[Serializable]
	public class Out_CheckShebaoMDL
	{
		public Out_CheckShebaoMDL()
		{			
		}
			
		//主键
		protected string _ID;
		
		//其它属性
		protected string _UserName;
		protected string _IDCard;
		protected string _RegType;
		protected string _Unit;
		protected string _Provice;
		protected string _ShebaoUnit;
		protected string _PublishDateTime;
		protected string _Question;
		protected string _Sex;
		protected string _Birthday;
		protected string _Remark;
		protected string _IDCard18;
		protected string _RegCode;
		protected string _RegProfession;
		protected string _Region;
		protected DateTime? _Createdate;
		
		public string ID
		{
			get {return _ID;}
			set {_ID = value;}
		}

		public string UserName
		{
			get {return _UserName;}
			set {_UserName = value;}
		}

		public string IDCard
		{
			get {return _IDCard;}
			set {_IDCard = value;}
		}

		public string RegType
		{
			get {return _RegType;}
			set {_RegType = value;}
		}

		public string Unit
		{
			get {return _Unit;}
			set {_Unit = value;}
		}

		public string Provice
		{
			get {return _Provice;}
			set {_Provice = value;}
		}

		public string ShebaoUnit
		{
			get {return _ShebaoUnit;}
			set {_ShebaoUnit = value;}
		}

		public string PublishDateTime
		{
			get {return _PublishDateTime;}
			set {_PublishDateTime = value;}
		}

		public string Question
		{
			get {return _Question;}
			set {_Question = value;}
		}

		public string Sex
		{
			get {return _Sex;}
			set {_Sex = value;}
		}

		public string Birthday
		{
			get {return _Birthday;}
			set {_Birthday = value;}
		}

		public string Remark
		{
			get {return _Remark;}
			set {_Remark = value;}
		}

		public string IDCard18
		{
			get {return _IDCard18;}
			set {_IDCard18 = value;}
		}

		public string RegCode
		{
			get {return _RegCode;}
			set {_RegCode = value;}
		}

		public string RegProfession
		{
			get {return _RegProfession;}
			set {_RegProfession = value;}
		}

		public string Region
		{
			get {return _Region;}
			set {_Region = value;}
		}

		public DateTime? Createdate
		{
			get {return _Createdate;}
			set {_Createdate = value;}
		}
	}
}
