using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--ApplyCancelMDL填写类描述
    /// </summary>
    [Serializable]
    public class ApplyCancelMDL
    {
        public ApplyCancelMDL()
        {
        }

        //主键
        protected string _ApplyID;

        //其它属性
        protected string _ENT_Correspondence;
        protected string _ENT_Postcode;
        protected string _ENT_Telephone;
        protected string _PSN_MobilePhone;
        protected string _PSN_RegisterCertificateNo;
        protected string _PSN_RegisterNO;
        protected DateTime? _RegisterValidity;
        protected string _CancelReason;
        protected string _PSN_Email;
        protected string _LinkMan;
        protected string _ApplyManType;
        protected string _ZyIDItem;
        protected string _ZyItem;
        protected string _Nation;

        public string ApplyID
        {
            get { return _ApplyID; }
            set { _ApplyID = value; }
        }

        public string ENT_Correspondence
        {
            get { return _ENT_Correspondence; }
            set { _ENT_Correspondence = value; }
        }

        public string ENT_Postcode
        {
            get { return _ENT_Postcode; }
            set { _ENT_Postcode = value; }
        }

        public string ENT_Telephone
        {
            get { return _ENT_Telephone; }
            set { _ENT_Telephone = value; }
        }

        public string PSN_MobilePhone
        {
            get { return _PSN_MobilePhone; }
            set { _PSN_MobilePhone = value; }
        }

        public string PSN_RegisterCertificateNo
        {
            get { return _PSN_RegisterCertificateNo; }
            set { _PSN_RegisterCertificateNo = value; }
        }

        public string PSN_RegisterNO
        {
            get { return _PSN_RegisterNO; }
            set { _PSN_RegisterNO = value; }
        }

        public DateTime? RegisterValidity
        {
            get { return _RegisterValidity; }
            set { _RegisterValidity = value; }
        }

        public string CancelReason
        {
            get { return _CancelReason; }
            set { _CancelReason = value; }
        }

        public string PSN_Email
        {
            get { return _PSN_Email; }
            set { _PSN_Email = value; }
        }

        public string LinkMan
        {
            get { return _LinkMan; }
            set { _LinkMan = value; }
        }

        public string ApplyManType
        {
            get { return _ApplyManType; }
            set { _ApplyManType = value; }
        }

        public string ZyIDItem
        {
            get { return _ZyIDItem; }
            set { _ZyIDItem = value; }
        }

        public string ZyItem
        {
            get { return _ZyItem; }
            set { _ZyItem = value; }
        }
        public string Nation
        {
            get { return _Nation; }
            set { _Nation = value; }
        }
    }
}
