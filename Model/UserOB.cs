using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--UserOB填写类描述
    /// </summary>
    [Serializable]
    public class UserOB
    {
        public UserOB()
        {
            //默认值
        }

        //主键
        protected long? _UserID;

        //其它属性
        protected long? _OrganID;
        protected long? _DeptID;
        protected string _UserName;
        protected string _UserPwd;
        protected string _RelUserName;
        protected string _License;
        protected string _Telphone;
        protected string _Mobile;
        protected string _Code;

        public long? UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public long? OrganID
        {
            get { return _OrganID; }
            set { _OrganID = value; }
        }

        public long? DeptID
        {
            get { return _DeptID; }
            set { _DeptID = value; }
        }

        public string UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public string UserPwd
        {
            get { return _UserPwd; }
            set { _UserPwd = value; }
        }

        public string RelUserName
        {
            get { return _RelUserName; }
            set { _RelUserName = value; }
        }

        public string License
        {
            get { return _License; }
            set { _License = value; }
        }

        public string Telphone
        {
            get { return _Telphone; }
            set { _Telphone = value; }
        }

        public string Mobile
        {
            get { return _Mobile; }
            set { _Mobile = value; }
        }

        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }
    }
}
