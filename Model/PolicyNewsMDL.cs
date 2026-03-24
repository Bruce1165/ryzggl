using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--PolicyNewsMDL填写类描述
    /// </summary>
    [Serializable]
    public class PolicyNewsMDL
    {
        public PolicyNewsMDL()
        {
        }

        //主键
        protected string _ID;

        //其它属性
        protected string _Title;
        protected string _Content;
        protected string _FileUrl;
        protected DateTime? _GetDateTime;
        protected int _States;
        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        public string Content
        {
            get { return _Content; }
            set { _Content = value; }
        }

        public string FileUrl
        {
            get { return _FileUrl; }
            set { _FileUrl = value; }
        }

        public DateTime? GetDateTime
        {
            get { return _GetDateTime; }
            set { _GetDateTime = value; }
        }
        public int States
        {

            get { return _States; }
            set { _States = value; }
        }
    }
}
