using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--ApplyFileMDL填写类描述
	/// </summary>
	[Serializable]
	public class ApplyFileMDL
	{
		public ApplyFileMDL()
		{			
		}
			
		//主键
		protected string _FileID;
		
		//其它属性
		protected string _ApplyID;
		protected int? _CheckResult;
		protected string _CheckDesc;
		
		public string FileID
		{
			get {return _FileID;}
			set {_FileID = value;}
		}

		public string ApplyID
		{
			get {return _ApplyID;}
			set {_ApplyID = value;}
		}

		public int? CheckResult
		{
			get {return _CheckResult;}
			set {_CheckResult = value;}
		}

		public string CheckDesc
		{
			get {return _CheckDesc;}
			set {_CheckDesc = value;}
		}
	}
}
