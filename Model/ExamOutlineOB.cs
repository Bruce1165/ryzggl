using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class ExamOutlineOB
    {
        public ExamOutlineOB()
        {
            //默认值
        }
        //ExamRange表
        //主键
        protected int? _ExamYear;
        protected int? _SubjectID;

        //其它属性
        protected int? _Flag;
        protected long? _CreatePersonID;
        protected DateTime? _CreateTime;
        protected long? _ModifyPersonID;
        protected DateTime? _ModifyTime;




        //ExamRangeSub
        //主键
        protected int? _TageID;
        protected int? _TagCode;
        protected string _Title;




        public int? ExamYear { get { return _ExamYear; } set { _ExamYear = value; ; } }
        public int? SubjectID { get { return _SubjectID; } set { _SubjectID = value; } }

        //其它属性
        public int? Flag { get { return _Flag; } set { _Flag = value; } }
        public long? CreatePersonID { get { return _CreatePersonID; } set { _CreatePersonID = value; } }
        public DateTime? CreateTime { get { return _CreateTime; } set { _CreateTime = value; } }
        public long? ModifyPersonID { get { return _ModifyPersonID; } set { _ModifyPersonID = value; } }
        public DateTime? ModifyTime { get { return _ModifyTime; } set { _ModifyTime = value; } }





        public int? TageID { get { return _TageID; } set { _TageID = value; } }
        public int? TagCode { get { return _TagCode; } set { _TagCode = value; } }
        public string Title { get { return _Title; } set { _Title = value; } }



    }
}
