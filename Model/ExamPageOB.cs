using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{

    /// <summary>
    /// 业务实体类--ExamPageOB填写类描述
    /// </summary>
    [Serializable]
    public class ExamPageOB
    {
        public ExamPageOB()
        {
            //默认值
        }

        //主键
        protected long? _ExamPageID;
        //其它属性
        protected int? _ExamYear;
        protected int? _SubjectID;
        protected string _ExamPageTitle;
        protected string _Remark;
        protected long? _CreatePersonID;
        protected DateTime? _CreateTime;
        protected long? _ModifyPersonID;
        protected DateTime? _ModifyTime;
        protected string _Difficulty;
        protected int? _Score;
        protected string _Flag;
        protected int? _TimeLimit;



        public long? ExamPageID { get { return _ExamPageID; } set { _ExamPageID = value; } }

        public int? ExamYear
        {
            get
            {
                return _ExamYear;
            }
            set { _ExamYear = value; }
        }
        public int? SubjectID
        {
            get { return _SubjectID; }
            set { _SubjectID = value; }
        }
        public string ExamPageTitle { get { return _ExamPageTitle; } set { _ExamPageTitle = value; } }
        public string Remark { get { return _Remark; } set { _Remark = value; } }
        public long? CreatePersonID { get { return _CreatePersonID; } set { _CreatePersonID = value; } }
        public DateTime? CreateTime { get { return _CreateTime; } set { _CreateTime = value; } }
        public long? ModifyPersonID { get { return _ModifyPersonID; } set { _ModifyPersonID = value; } }
        public DateTime? ModifyTime { get { return _ModifyTime; } set { _ModifyTime = value; } }
        public string Difficulty { get { return _Difficulty; } set { _Difficulty = value; } }
        public int? Score { get { return _Score; } set { _Score = value; } }
        public string Flag { get { return _Flag; } set { _Flag = value; } }
        public int? TimeLimit { get { return _TimeLimit; } set { _TimeLimit = value; } }
    }
}
