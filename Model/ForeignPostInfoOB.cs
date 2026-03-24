using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--PostInfoOB填写类描述
	/// </summary>
	[Serializable]
	public class ForeignPostInfoOB
	{
		public ForeignPostInfoOB()
		{			
			//默认值
		}
			
		//主键
		protected int? _PostID;
		
		//其它属性
		protected string _PostType;
		protected string _PostName;
		protected int? _UpPostID;
		protected decimal? _ExamFee;
        protected long? _CurrentNumber;
        protected int? _CodeYear;
        protected string _CodeFormat;

        public long? CurrentNumber
        {
            get { return _CurrentNumber; }
            set { _CurrentNumber = value; }
        }
        
        public int? CodeYear
        {
            get { return _CodeYear; }
            set { _CodeYear = value; }
        }

        public string CodeFormat
        {
            get { return _CodeFormat; }
            set { _CodeFormat = value; }
        }
		
		public int? PostID
		{
			get {return _PostID;}
			set {_PostID = value;}
		}

		public string PostType
		{
			get {return _PostType;}
			set {_PostType = value;}
		}

		public string PostName
		{
			get {return _PostName;}
			set {_PostName = value;}
		}

		public int? UpPostID
		{
			get {return _UpPostID;}
			set {_UpPostID = value;}
		}

		public decimal? ExamFee
		{
			get {return _ExamFee;}
			set {_ExamFee = value;}
		}
	}
}
