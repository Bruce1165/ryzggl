using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--TypesOB填写类描述
	/// </summary>
	[Serializable]
	public class TypesOB
	{
		public TypesOB()
		{			
			//默认值
		}
			
		//主键
		protected string _TypeID;
		
		//其它属性
		protected int? _SortID;
		protected string _TypeName;
		protected string _TypeValue;
		
		public string TypeID
		{
			get {return _TypeID;}
			set {_TypeID = value;}
		}

		public int? SortID
		{
			get {return _SortID;}
			set {_SortID = value;}
		}

		public string TypeName
		{
			get {return _TypeName;}
			set {_TypeName = value;}
		}

		public string TypeValue
		{
			get {return _TypeValue;}
			set {_TypeValue = value;}
		}
	}
}
