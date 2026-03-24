using System;

namespace Model
{
    /// <summary>
    /// 组织
    /// </summary>
    [Serializable]
    public class OrganizationMDL
    {

        /// <summary>
        /// OrganID
        /// </summary>
        public string OrganID
        {
            get;
            set;
        }
        /// <summary>
        /// OrderID
        /// </summary>
        public int OrderID
        {
            get;
            set;
        }
        /// <summary>
        /// 机构编码
        /// </summary>
        public string OrganCoding
        {
            get;
            set;
        }
        /// <summary>
        /// OrganType
        /// </summary>
        public string OrganType
        {
            get;
            set;
        }
        /// <summary>
        /// 机构性质
        /// </summary>
        public string OrganNature
        {
            get;
            set;
        }
        /// <summary>
        /// 机构名称
        /// </summary>
        public string OrganName
        {
            get;
            set;
        }
        /// <summary>
        /// 机构描述
        /// </summary>
        public string OrganDescription
        {
            get;
            set;
        }
        /// <summary>
        /// BusinessProperties
        /// </summary>
        public string BusinessProperties
        {
            get;
            set;
        }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string OrganTelphone
        {
            get;
            set;
        }
        /// <summary>
        /// 机构地址
        /// </summary>
        public string OrganAddress
        {
            get;
            set;
        }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string OrganCode
        {
            get;
            set;
        }
        /// <summary>
        /// RegionID
        /// </summary>
        public string RegionID
        {
            get;
            set;
        }
        /// <summary>
        /// IsVisible
        /// </summary>
        public int IsVisible
        {
            get;
            set;
        }

    }
}