using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WS_GetData.Api.Model.Zm
{
    /// <summary>
    ///  二级造价工程师注册证书
    /// </summary>
    [Serializable]
    public class EJZJGCS : BaseLicence
    {
        /// <summary>
        /// 证照类型代码：二级造价工程师（土木建筑工程）注册证书=“1110000000001332XW059”
        ///               二级造价工程师（安装工程）注册证书=“1110000000001332XW082”
        /// </summary>	
        public string ZZLXDM { set; get; }
        /// <summary>
        /// 证照编号（）	
        /// </summary>
        public string ZSBH { set; get; }


        /// <summary>
        /// 电子证照标识
        /// </summary>
        public string DZZZBZ { set; get; }

        /// <summary>
        /// 发证机关
        /// </summary>
        public string FZJG { set; get; }

        /// <summary>
        /// 发证机关代码
        /// </summary>
        public string FZJGDM { set; get; }

        /// <summary>
        /// 有效期起始日期
        /// </summary>
        public string YXQQSRQ { set; get; }
        /// <summary>
        /// 有效期截止日期
        /// </summary>
        public string YXQJZRQ { set; get; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string XM { set; get; }
        /// <summary>
        /// 性别
        /// </summary>
        public string XB { set; get; }
        /// <summary>
        /// 出生年月
        /// </summary>
        public string CSNY { set; get; }
        /// <summary>
        /// 身份证件号码
        /// </summary>
        public string SFZJHM { set; get; }
        /// <summary>
        /// 身份证件类型
        /// </summary>
        public string SFZJLX { set; get; }
        /// <summary>
        /// 持证人头像
        /// </summary>
        public string CZRTX { set; get; }
        /// <summary>
        /// 个人签名图像
        /// </summary>
        public string GRQMTX { set; get; }
        /// <summary>
        /// 专业
        /// </summary>
        public string ZY { set; get; }
        /// <summary>
        /// 聘用单位
        /// </summary>
        public string PYDW { set; get; }
        /// <summary>
        /// 聘用单位代码
        /// </summary>
        public string PYDWDM { set; get; }
        /// <summary>
        /// 学历
        /// </summary>
        public string XL { set; get; }
        /// <summary>
        /// 学历专业
        /// </summary>
        public string XLZY { set; get; }
        /// <summary>
        /// 资格证书编号
        /// </summary>
        public string ZGZSBH { set; get; }
        /// <summary>
        /// 资格证书批准日期（签发日期）
        /// </summary>
        public string ZGZSPZRQ { set; get; }
        /// <summary>
        /// 初始注册日期
        /// </summary>
        public string CSZCRQ { set; get; }
        /// <summary>
        /// 原证书编号
        /// </summary>
        public string YZSBH { set; get; }
        /// <summary>
        /// 原证照标识
        /// </summary>
        public string YZZBZ { set; get; }
        /// <summary>
        /// 证书状态
        /// </summary>
        public string ZSZT { set; get; }
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
    }
}