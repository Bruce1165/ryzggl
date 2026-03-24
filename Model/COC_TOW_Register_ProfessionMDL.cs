using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--COC_TOW_Register_ProfessionMDL填写类描述
	/// </summary>
	[Serializable]
	public class COC_TOW_Register_ProfessionMDL
	{
		public COC_TOW_Register_ProfessionMDL()
		{			
		}
			
		//主键
		protected string _PRO_ServerID;
		
		//其它属性
		protected string _PSN_ServerID;
		protected string _PRO_Profession;
		protected DateTime? _PRO_ValidityBegin;
		protected DateTime? _PRO_ValidityEnd;
		protected string _DogID;
		protected string _ENT_Province_Code;
		protected string _DownType;
		protected DateTime? _LastModifyTime;
		
		public string PRO_ServerID
		{
			get {return _PRO_ServerID;}
			set {_PRO_ServerID = value;}
		}

		public string PSN_ServerID
		{
			get {return _PSN_ServerID;}
			set {_PSN_ServerID = value;}
		}

		public string PRO_Profession
		{
			get {return _PRO_Profession;}
			set {_PRO_Profession = value;}
		}

		public DateTime? PRO_ValidityBegin
		{
			get {return _PRO_ValidityBegin;}
			set {_PRO_ValidityBegin = value;}
		}

		public DateTime? PRO_ValidityEnd
		{
			get {return _PRO_ValidityEnd;}
			set {_PRO_ValidityEnd = value;}
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
