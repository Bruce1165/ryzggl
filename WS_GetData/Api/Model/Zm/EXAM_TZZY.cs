using System;

namespace WS_GetData.Api.Model.Zm
{
    /// <summary>
    ///     建筑施工特种作业操作资格证书
    /// </summary>
    [Serializable]
    public class EXAM_TZZY : BaseLicence
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