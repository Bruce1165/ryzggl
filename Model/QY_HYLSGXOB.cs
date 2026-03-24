using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--TypesOB填写类描述
    /// </summary>
    [Serializable]
    public class QY_HYLSGXOB
    {
        public QY_HYLSGXOB()
        {
            //默认值
        }


        //主键
        protected string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        //其它属性

        private string _ZZJGDM;

        public string ZZJGDM
        {
            get { return _ZZJGDM; }
            set { _ZZJGDM = value; }
        }
        private string _QYMC;

        public string QYMC
        {
            get { return _QYMC; }
            set { _QYMC = value; }
        }
        private string _LSGX;

        public string LSGX
        {
            get { return _LSGX; }
            set { _LSGX = value; }
        }
        private Int64? _USERID;

        public Int64? USERID
        {
            get { return _USERID; }
            set { _USERID = value; }
        }
        private Int64? _ORGANID;

        public Int64? ORGANID
        {
            get { return _ORGANID; }
            set { _ORGANID = value; }
        }

    }
}