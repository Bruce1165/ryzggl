using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--RoleOB填写类描述
	/// </summary>
	[Serializable]
	public class RoleOB
	{
		public RoleOB()
		{			
			//默认值
		}
			
		//主键
		protected int? _RoleID;
		
		//其它属性
		protected string _RoleName;
		protected string _Description;
		protected byte? _Status;
		
		public int? RoleID
		{
			get {return _RoleID;}
			set {_RoleID = value;}
		}

		public string RoleName
		{
			get {return _RoleName;}
			set {_RoleName = value;}
		}

		public string Description
		{
			get {return _Description;}
			set {_Description = value;}
		}

		public byte? Status
		{
			get {return _Status;}
			set {_Status = value;}
		}
	}
}
