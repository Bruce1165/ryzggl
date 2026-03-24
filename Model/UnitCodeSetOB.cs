using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--UnitCodeSetOB填写类描述
	/// </summary>
	[Serializable]
	public class UnitCodeSetOB
	{
		public UnitCodeSetOB()
		{			
			//默认值
		}
			
		//主键
		protected string _UnitCode;
		
		//其它属性
		protected string _UnitName;
		protected string _CreatePerson;
		protected DateTime? _CreateTime;
		protected string _ModifyPerson;
		protected DateTime? _ModifyTime;
		
		public string UnitCode
		{
			get {return _UnitCode;}
			set {_UnitCode = value;}
		}

		public string UnitName
		{
			get {return _UnitName;}
			set {_UnitName = value;}
		}

		public string CreatePerson
		{
			get {return _CreatePerson;}
			set {_CreatePerson = value;}
		}

		public DateTime? CreateTime
		{
			get {return _CreateTime;}
			set {_CreateTime = value;}
		}

		public string ModifyPerson
		{
			get {return _ModifyPerson;}
			set {_ModifyPerson = value;}
		}

		public DateTime? ModifyTime
		{
			get {return _ModifyTime;}
			set {_ModifyTime = value;}
		}
	}
}
