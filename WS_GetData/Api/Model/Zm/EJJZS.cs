using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WS_GetData.Api.Model.Zm
{
    /// <summary>
    ///  二级建造师注册证书
    /// </summary>
    [Serializable]
    public class EJJZS : BaseLicence
    {
        /// <summary>
        /// 证照类型代码：“11100000000013338W001”
        /// </summary>	
        public string ZZLXDM { set; get; }
        /// <summary>
        /// 证照编号（）	
        /// </summary>
        public string ZZBH { set; get; }
        /// <summary>
        /// 证照标识
        /// </summary>

        public string ZZBS { set; get; }
        /// <summary>
        /// 持证主体
        /// </summary>

        public string CZZT { set; get; }
        /// <summary>
        /// 持证主体代码
        /// </summary>
        public string CZZTDM { set; get; }
        /// <summary>
        /// 持证主体代码类型
        /// </summary>
        public string CZZTDMLX { set; get; }
        /// <summary>
        /// 证照有效期起始时间
        /// </summary>
        public string ZZYXQQSSJ { set; get; }
        /// <summary>
        /// 证照有效期截止时间
        /// </summary>
        public string ZZYXQJZSJ { set; get; }
        /// <summary>
        /// 证照颁发机构
        /// </summary>
        public string ZZBFJG { set; get; }
        /// <summary>
        /// 证照颁发机构代码
        /// </summary>
        public string ZZBFJGDM { set; get; }
        /// <summary>
        /// 证照颁发日期
        /// </summary>
        public string ZZBFRQ { set; get; }
        /// <summary>
        /// 证书级别
        /// </summary>
        public string KZ_certClass { set; get; }
        /// <summary>
        /// 聘用企业
        /// </summary>
        public string KZ_empEnp { set; get; }
        /// <summary>
        /// 聘用企业代码
        /// </summary>
        public string KZ_empEnpCode { set; get; }
        /// <summary>
        /// 专业
        /// </summary>
        public string KZ_specSub { set; get; }
        /// <summary>
        /// 专业代码
        /// </summary>
        public string KZ_specSubCode { set; get; }
        /// <summary>
        /// 执业资格证书管理号
        /// </summary>
        public string KZ_quaCertNum { set; get; }
        /// <summary>
        /// 专业有效期起始日期
        /// </summary>
        public string KZ_subBeginDate { set; get; }
        /// <summary>
        /// 专业有效期截止日期
        /// </summary>    
        public string KZ_subEndDate { set; get; }




    }
}