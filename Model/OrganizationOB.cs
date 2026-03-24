using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--OrganizationOB填写类描述
    /// </summary>
    [Serializable]
    public class OrganizationOB
    {
        public OrganizationOB()
        {
            //默认值
        }

        //主键
        protected long? _OrganID;

        //其它属性
        protected int? _OrderID;
        protected string _OrganCoding;
        protected string _OrganType;
        protected string _OrganNature;
        protected string _OrganName;
        protected string _OrganDescription;
        protected string _BusinessProperties;
        protected string _OrganTelphone;
        protected string _OrganAddress;
        protected string _OrganCode;

        public long? OrganID
        {
            get { return _OrganID; }
            set { _OrganID = value; }
        }

        public int? OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }

        public string OrganCoding
        {
            get { return _OrganCoding; }
            set { _OrganCoding = value; }
        }

        public string OrganType
        {
            get { return _OrganType; }
            set { _OrganType = value; }
        }

        public string OrganNature
        {
            get { return _OrganNature; }
            set { _OrganNature = value; }
        }

        public string OrganName
        {
            get { return _OrganName; }
            set { _OrganName = value; }
        }

        public string OrganDescription
        {
            get { return _OrganDescription; }
            set { _OrganDescription = value; }
        }

        public string BusinessProperties
        {
            get { return _BusinessProperties; }
            set { _BusinessProperties = value; }
        }

        public string OrganTelphone
        {
            get { return _OrganTelphone; }
            set { _OrganTelphone = value; }
        }

        public string OrganAddress
        {
            get { return _OrganAddress; }
            set { _OrganAddress = value; }
        }

        public string OrganCode
        {
            get { return _OrganCode; }
            set { _OrganCode = value; }
        }
    }
}
