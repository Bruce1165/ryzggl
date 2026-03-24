using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--UserRoleMDL填写类描述
	/// </summary>
	[Serializable]
	public class UserRoleMDL
	{
		public UserRoleMDL()
		{			
		}
			
		//主键
		protected int? _ID;
		
		//其它属性
		protected string _UserID;
		protected string _RoleID;
		
		public int? ID
		{
			get {return _ID;}
			set {_ID = value;}
		}

		public string UserID
		{
			get {return _UserID;}
			set {_UserID = value;}
		}

		public string RoleID
		{
			get {return _RoleID;}
			set {_RoleID = value;}
		}
	}
}
