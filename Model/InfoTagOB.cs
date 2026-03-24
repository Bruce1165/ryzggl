using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--InfoTagOB填写类描述
	/// </summary>
	[Serializable]
	public class InfoTagOB
	{
		public InfoTagOB()
		{			
			//默认值
		}
			
		//主键
		protected long? _TagID;
		
		//其它属性
		protected int? _SubjectID;
		protected int? _TagCode;
		protected string _ShowCode;
		protected string _Title;
		protected byte? _Flag;
		protected long? _CreatePersonID;
		protected DateTime? _CreateTime;
		protected long? _ModifyPersonID;
		protected DateTime? _ModifyTime;
        protected int? _Weight;

        public int? Weight
        {
            get { return _Weight; }
            set { _Weight = value; }
        }
		
		public long? TagID
		{
			get {return _TagID;}
			set {_TagID = value;}
		}

		public int? SubjectID
		{
			get {return _SubjectID;}
			set {_SubjectID = value;}
		}

		public int? TagCode
		{
			get {return _TagCode;}
			set {_TagCode = value;}
		}

		public string ShowCode
		{
			get {return _ShowCode;}
			set {_ShowCode = value;}
		}

		public string Title
		{
			get {return _Title;}
			set {_Title = value;}
		}

		public byte? Flag
		{
			get {return _Flag;}
			set {_Flag = value;}
		}

		public long? CreatePersonID
		{
			get {return _CreatePersonID;}
			set {_CreatePersonID = value;}
		}

		public DateTime? CreateTime
		{
			get {return _CreateTime;}
			set {_CreateTime = value;}
		}

		public long? ModifyPersonID
		{
			get {return _ModifyPersonID;}
			set {_ModifyPersonID = value;}
		}

		public DateTime? ModifyTime
		{
			get {return _ModifyTime;}
			set {_ModifyTime = value;}
		}
	}
}
