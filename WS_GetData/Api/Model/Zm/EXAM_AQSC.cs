using System;

namespace WS_GetData.Api.Model.Zm
{
    /// <summary>
    ///     安全生产考核三类人
    /// </summary>
    [Serializable]
    public class EXAM_AQSC : BaseLicence
    {
        /// <summary>
        ///     企业名称
        /// </summary>
        public string QYMC { set; get; }

        /// <summary>
        ///     企业证照类型
        /// </summary>
        public string QYZZLX { set; get; }

        /// <summary>
        ///     企业证照号码
        /// </summary>
        public string QYZZHM { set; get; }

        /// <summary>
        ///     制证日期
        /// </summary>
        public string ZZRQ { set; get; }

        /// <summary>
        ///     岗位名称
        /// </summary>
        public string GWMC { set; get; }

        /// <summary>
        ///     二维码
        /// </summary>
        public string RWM { set; get; }

        /// <summary>
        ///     照片
        /// </summary>
        public string ZP { set; get; }
    }
}