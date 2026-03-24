using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    class ExamRangeSubOB
    {
        public ExamRangeSubOB()
		{			
			//默认值
		}
			
		//主键
		protected int? _ExamYear;
        protected int? _SubjectID;
        protected int? _TagID;
        //其它属性
        protected string _Title;
        protected int? _TagCode;







        public int? ExamYear { get { return _ExamYear; } set { _ExamYear = value; } }
        public int? SubjectID { get { return _SubjectID; } set { _SubjectID = value; } }
        public int? TagID { get { return _TagID; } set { _TagID = value; } }
        public string Title { get { return _Title; } set { _Title = value; } }
        public int? TagCode { get { return _TagCode; } set { _TagCode = value; } }


   
    }
}
