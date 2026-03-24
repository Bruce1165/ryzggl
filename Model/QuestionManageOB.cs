using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--QuestionManageOB填写类描述
    /// </summary>
    [Serializable]
    public class QuestionManageOB
    {
        public QuestionManageOB()
        {
            //默认值
        }
        //主键
        protected long? _QuestionID;

        //其它属性
        protected long? _SubjectID;
    
        protected string _TagCode;
        protected string _Title;
        protected string _Flag;
        protected string _Answer;
        protected string _QuestionType;
        protected string _Option;
        protected string _Difficulty;
        protected long _CreatePersonID;
        protected int? _OptionColumn;
        protected DateTime? _CreateTime;
        protected long? _ModifyPersonID;
        protected DateTime? _ModifyTime;


        public long? QuestionID { get { return _QuestionID; } set { _QuestionID = value; } }
        public long? SubjectID { get { return _SubjectID; }set{_SubjectID=value;}} 
     
        public string TagCode { get { return _TagCode; } set { _TagCode = value; } }
        public string Title { get { return _Title; } set { _Title = value; } }
        public string Flag { get { return _Flag; } set { _Flag = value; } }
        public string Answer { get { return _Answer; } set { _Answer = value; } }
        public string QuestionType { get { return _QuestionType; } set { _QuestionType = value; } }
        public string Option { get { return _Option; } set { _Option = value; } }
        public string Difficulty { get { return _Difficulty; } set { _Difficulty = value; } }
        public long CreatePersonID { get { return _CreatePersonID; } set { _CreatePersonID = value; } }
        public DateTime? CreateTime { get { return _CreateTime; } set { _CreateTime = value; } }
        public long? ModifyPersonID { get { return _ModifyPersonID; } set { _ModifyPersonID = value; } }
        public int? OptionColumn { get { return _OptionColumn; } set { _OptionColumn = value; } }
        public DateTime? ModifyTime { get { return _ModifyTime; } set { _ModifyTime = value; } }
    }
}
