using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--COC_TOW_Person_FileMDL填写类描述
	/// </summary>
	[Serializable]
	public class COC_TOW_Person_FileMDL
	{
		public COC_TOW_Person_FileMDL()
		{			
		}
			
		//主键
		protected string _FileID;
		
		//其它属性
		protected string _PSN_RegisterNO;
		protected bool? _IsHistory;
		
		public string FileID
		{
			get {return _FileID;}
			set {_FileID = value;}
		}

		public string PSN_RegisterNO
		{
			get {return _PSN_RegisterNO;}
			set {_PSN_RegisterNO = value;}
		}

		public bool? IsHistory
		{
			get {return _IsHistory;}
			set {_IsHistory = value;}
		}
	}
}
