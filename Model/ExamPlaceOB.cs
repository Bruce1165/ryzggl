using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--ExamPlaceOB填写类描述
    /// </summary>
    [Serializable]
    public class ExamPlaceOB
    {
        public ExamPlaceOB()
        {
            //默认值
        }

        //主键
        protected long? _ExamPlaceID;

        //其它属性
        protected string _ExamPlaceName;
        protected string _ExamPlaceAddress;
        protected string _LinkMan;
        protected string _Phone;
        protected int? _RoomNum;
        protected int? _ExamPersonNum;
        protected string _Status;
         
        public long? ExamPlaceID
        {
            get { return _ExamPlaceID; }
            set { _ExamPlaceID = value; }
        }

        public string ExamPlaceName
        {
            get { return _ExamPlaceName; }
            set { _ExamPlaceName = value; }
        }

        public string ExamPlaceAddress
        {
            get { return _ExamPlaceAddress; }
            set { _ExamPlaceAddress = value; }
        }

        public string LinkMan
        {
            get { return _LinkMan; }
            set { _LinkMan = value; }
        }

        public string Phone
        {
            get { return _Phone; }
            set { _Phone = value; }
        }

        public int? RoomNum
        {
            get { return _RoomNum; }
            set { _RoomNum = value; }
        }

        public int? ExamPersonNum
        {
            get { return _ExamPersonNum; }
            set { _ExamPersonNum = value; }
        }

        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }
    }
}

