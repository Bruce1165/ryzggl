using System;

namespace WS_GetData.Api.Model.Zm
{
    /// <summary>
    ///  国标采集字段：安全生产考核三类人
    /// </summary>
    [Serializable]
    public class EXAM_AQSC_GB : BaseLicence
    {        
        /// <summary>
        /// 证照类型代码：固定为“11100000000013338W011”； 
        /// </summary>
        public string ZZLXDM { set; get; }
        /// <summary>
        /// 证照编号 
        /// </summary>
        public string ZZBH { set; get; }
        /// <summary>
        /// 发证机关 
        /// </summary>
        public string FZJG { set; get; }
        /// <summary>
        /// 发证机关代码 
        /// </summary>
        public string FZJGDM { set; get; }
        /// <summary>
        /// 初次领证日期 
        /// </summary>
        public string CCLZRQ { set; get; }
        /// <summary>
        /// 有效期起始日期 
        /// </summary>
        public string YXQQSRQ { set; get; }
        /// <summary>
        /// 安管人员类别 
        /// </summary>
        public string AGRYLB { set; get; }
        /// <summary>
        /// 姓名 
        /// </summary>
        public string XM { set; get; }
        /// <summary>
        /// 性别 
        /// </summary>
        public string XB { set; get; }
        /// <summary>
        /// 性别代码 
        /// </summary>
        public string XBDM { set; get; }
        /// <summary>
        /// 出生日期 
        /// </summary>
        public string CSNY { set; get; }
        /// <summary>
        /// 身份证件号码 
        /// </summary>
        public string SFZJHM { set; get; }
        /// <summary>
        /// 身份证件号码类型 
        /// </summary>
        public string SFZJHMLX { set; get; }
        /// <summary>
        /// 身份证件号码类型代码 
        /// </summary>
        public string SFZJHMLXDM { set; get; }
        /// <summary>
        /// 企业名称 
        /// </summary>
        public string DWMC { set; get; }
        /// <summary>
        /// 统一社会信用代码 
        /// </summary>
        public string TYSHXYDM { set; get; }
        /// <summary>
        /// 职务 
        /// </summary>
        public string ZW { set; get; }
        /// <summary>
        /// 技术职称 
        /// </summary>
        public string JSZC { set; get; }
        /// <summary>
        /// 文化程度 
        /// </summary>
        public string WHCD { set; get; }
        /// <summary>
        /// 专业 
        /// </summary>
        public string ZY { set; get; }
        /// <summary>
        /// 照片 
        /// </summary>
        public string ZP { set; get; }
        /// <summary>
        /// 证书状态 
        /// </summary>
        public string ZSZT { set; get; }
        /// <summary>
        /// 证书状态的代码 
        /// </summary>
        public string ZSZTDM { set; get; }
        /// <summary>
        /// 证书状态描述 
        /// </summary>
        public string ZSZTMS { set; get; }
        /// <summary>
        /// 关联类型 
        /// </summary>
        public string GLLX { set; get; }
        /// <summary>
        /// 关联证照标识 
        /// </summary>
        public string GLZZBZ { set; get; }
        /// <summary>
        /// 业务类型代码 
        /// </summary>
        public string YWLXDM { set; get; }
        /// <summary>
        /// 业务信息 
        /// </summary>
        public string YWXX { set; get; }

    }
}