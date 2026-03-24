using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--QualificationMDL填写类描述
    /// </summary>
    [Serializable]
    public class QualificationMDL
    {
        public QualificationMDL()
        {
        }

        //主键
        protected string _ZGZSBH;

        //其它属性
        protected string _XM;
        protected string _ZJHM;
        protected string _SF;
        protected string _GZDW;
        protected string _QDNF;
        protected string _QDFS;
        protected string _GLH;
        protected string _ZYLB;
        protected string _BYXX;
        protected DateTime? _BYSJ;
        protected string _SXZY;
        protected string _ZGXL;
        protected DateTime? _QFSJ;

        /// <summary>
        /// 签发时间
        /// </summary>
        public DateTime? QFSJ
        {
            get { return _QFSJ; }
            set { _QFSJ = value; }
        }

        /// <summary>
        /// 毕业学校
        /// </summary>
        public string BYXX
        {
            get { return _BYXX; }
            set { _BYXX = value; }
        }

        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime? BYSJ
        {
            get { return _BYSJ; }
            set { _BYSJ = value; }
        }
        /// <summary>
        /// 所学专业
        /// </summary>
        public string SXZY
        {
            get { return _SXZY; }
            set { _SXZY = value; }
        }
        /// <summary>
        /// 最高学历
        /// </summary>
        public string ZGXL
        {
            get { return _ZGXL; }
            set { _ZGXL = value; }
        }
        /// <summary>
        /// 资格证书编号
        /// </summary>
        public string ZGZSBH
        {
            get { return _ZGZSBH; }
            set { _ZGZSBH = value; }
        }

        /// <summary>
        /// 姓名
        /// </summary>
        public string XM
        {
            get { return _XM; }
            set { _XM = value; }
        }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string ZJHM
        {
            get { return _ZJHM; }
            set { _ZJHM = value; }
        }

        /// <summary>
        /// 省份
        /// </summary>
        public string SF
        {
            get { return _SF; }
            set { _SF = value; }
        }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string GZDW
        {
            get { return _GZDW; }
            set { _GZDW = value; }
        }
        /// <summary>
        /// 取得年度
        /// </summary>
        public string QDNF
        {
            get { return _QDNF; }
            set { _QDNF = value; }
        }
        /// <summary>
        /// 取得方式
        /// </summary>
        public string QDFS
        {
            get { return _QDFS; }
            set { _QDFS = value; }
        }
        /// <summary>
        /// 管理号
        /// </summary>
        public string GLH
        {
            get { return _GLH; }
            set { _GLH = value; }
        }
        /// <summary>
        /// 专业类别
        /// </summary>
        public string ZYLB
        {
            get { return _ZYLB; }
            set { _ZYLB = value; }
        }
    }
}
