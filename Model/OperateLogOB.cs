using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--OperateLogOB填写类描述
	/// </summary>
	[Serializable]
	public class OperateLogOB
	{
		public OperateLogOB()
		{			
			//默认值
		}
			
		//主键
		protected long? _LogID;
		
		//其它属性
		protected DateTime? _LogTime;
		protected string _PersonName;
        protected string _PersonID;
		protected string _OperateName;
		protected string _LogDetail;
		
		public long? LogID
		{
			get {return _LogID;}
			set {_LogID = value;}
		}

		public DateTime? LogTime
		{
			get {return _LogTime;}
			set {_LogTime = value;}
		}

		public string PersonName
		{
			get {return _PersonName;}
			set {_PersonName = value;}
		}

        public string PersonID
		{
			get {return _PersonID;}
			set {_PersonID = value;}
		}

		public string OperateName
		{
			get {return _OperateName;}
			set {_OperateName = value;}
		}

		public string LogDetail
		{
			get {return _LogDetail;}
			set {_LogDetail = value;}
		}
	}
}
