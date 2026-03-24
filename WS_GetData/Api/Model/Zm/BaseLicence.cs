using System;

namespace WS_GetData.Api.Model.Zm
{
    /// <summary>
    ///     基本照面信息
    /// </summary>
    [Serializable]
    public class BaseLicence
    {
        /// <summary>
        ///     证照名称
        /// </summary>
        public string ZZMC { set; get; }

        /// <summary>
        ///     证书编号
        /// </summary>
        public string ZZHM { set; get; }

        /// <summary>
        ///     持有人名称
        /// </summary>
        public string CYRMC { set; get; }

        /// <summary>
        ///     持有人身份证件类型
        /// </summary>
        public string CYRSFZJLX { set; get; }

        /// <summary>
        ///     持有人身份证件号码
        /// </summary>
        public string CYRSFZJHM { set; get; }

        /// <summary>
        ///     颁发机关
        /// </summary>
        public string FZJGMC { set; get; }

        /// <summary>
        ///     发证机构唯一标识
        /// </summary>
        public string FZJGZZJGDM { set; get; }

        /// <summary>
        ///     发证机构所属行政区划代码
        /// </summary>
        public string FZJGSSXZQHDM { set; get; }

        /// <summary>
        ///     颁发日期
        /// </summary>
        public string FZRQ { set; get; }

        /// <summary>
        ///     有效期结束日期
        /// </summary>
        public string YXQJSRQ { set; get; }
    }
}