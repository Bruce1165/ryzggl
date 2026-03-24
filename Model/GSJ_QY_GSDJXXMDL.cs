using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 工商登记信息--GSJ_QY_GSDJXXMDL
    /// </summary>
    [Serializable]
    public class GSJ_QY_GSDJXXMDL
    {
        public GSJ_QY_GSDJXXMDL() { }

        protected string _ID;
        protected string _ENT_NAME;
        protected string _REG_NO;
        protected string _OP_LOC;
        protected string _CORP_RPT;
        protected Decimal _REG_CAP;
        protected DateTime? _EST_DATE;
        protected string _ENT_TYPE;
        protected string _ENT_STATE;
        protected string _OP_SCOPE;
        protected string _PT_BUS_SCOPE;
        protected string _INDUSTRY_CO;
        protected DateTime? _UPDATE_DATE;
        protected string _BUSINESS_SCOPE;
        protected string _UNI_SCID;
        protected int _VALID;
        protected string _CJR;
        protected string _CJDEPTID;
        protected DateTime? _CJSJ;
        protected string _XGR;
        protected string _XGDEPTID;
        protected DateTime? _XGSJ;
        protected string _DOM;
        protected string _CAP_CUR;


        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public string ENT_NAME
        {
            get { return _ENT_NAME; }
            set { _ENT_NAME = value; }
        }
        public string REG_NO
        {
            get { return _REG_NO; }
            set { _REG_NO = value; }
        }
        public string OP_LOC
        {
            get { return _OP_LOC; }
            set { _OP_LOC = value; }
        }
        public string CORP_RPT
        {
            get { return _CORP_RPT; }
            set { _CORP_RPT = value; }
        }
        public Decimal REG_CAP
        {
            get { return _REG_CAP; }
            set { _REG_CAP = value; }
        }
        public DateTime? EST_DATE
        {
            get { return _EST_DATE; }
            set { _EST_DATE = value; }
        }
        public string ENT_TYPE
        {
            get { return _ENT_TYPE; }
            set { _ENT_TYPE = value; }
        }

        /// <summary>
        /// ENT_STATE
        ///1：开业
        ///2：吊销
        ///3：注销
        ///4：市内迁出
        ///5：迁往市外
        ///6：撤销
        ///7：歇业
        ///8：个体转企业
        /// </summary>
        public string ENT_STATE
        {
            get { return _ENT_STATE; }
            set { _ENT_STATE = value; }
        }
        public string OP_SCOPE
        {
            get { return _OP_SCOPE; }
            set { _OP_SCOPE = value; }
        }
        public string PT_BUS_SCOPE
        {
            get { return _PT_BUS_SCOPE; }
            set { _PT_BUS_SCOPE = value; }
        }
        public string INDUSTRY_CO
        {
            get { return _INDUSTRY_CO; }
            set { _INDUSTRY_CO = value; }
        }
        public DateTime? UPDATE_DATE
        {
            get { return _UPDATE_DATE; }
            set { _UPDATE_DATE = value; }
        }
        public string BUSINESS_SCOPE
        {
            get { return _BUSINESS_SCOPE; }
            set { _BUSINESS_SCOPE = value; }
        }
        /// <summary>
        /// 统一信用代码
        /// </summary>
        public string UNI_SCID
        {
            get { return _UNI_SCID; }
            set { _UNI_SCID = value; }
        }
        public int VALID
        {
            get { return _VALID; }
            set { _VALID = value; }
        }
        public string CJR
        {
            get { return _CJR; }
            set { _CJR = value; }
        }
        public string CJDEPTID
        {
            get { return _CJDEPTID; }
            set { _CJDEPTID = value; }
        }
        public DateTime? CJSJ
        {
            get { return _CJSJ; }
            set { _CJSJ = value; }
        }
        public string XGR
        {
            get { return _XGR; }
            set { _XGR = value; }
        }
        public string XGDEPTID
        {
            get { return _XGDEPTID; }
            set { _XGDEPTID = value; }
        }
        public DateTime? XGSJ
        {
            get { return _XGSJ; }
            set { _XGSJ = value; }
        }
        public string DOM
        {
            get { return _DOM; }
            set { _DOM = value; }
        }
        public string CAP_CUR
        {
            get { return _CAP_CUR; }
            set { _CAP_CUR = value; }
        }



        protected string _CreditCode;
        public string CreditCode
        {
            get { return _CreditCode; }
            set { _CreditCode = value; }
        }

    }
}
