namespace LicenseSystemService.Api.Model.Zm
{
    /// <summary>
    ///     造价咨询
    /// </summary>
    public class QY_ZJZX : BaseLicence
    {
        /// <summary>
        ///     详细地址
        /// </summary>
        public string XXDZ { get; set; }

        /// <summary>
        ///     法定代表人
        /// </summary>
        public string FDDBR { get; set; }

        /// <summary>
        ///     注册资本
        /// </summary>
        public string ZCZB { get; set; }

        /// <summary>
        ///     经济性质
        /// </summary>
        public string JJXZ { get; set; }

        /// <summary>
        ///     使用限制
        /// </summary>
        public string SYXZ { get; set; }

        /// <summary>
        ///     使用开始时间
        /// </summary>
        public string SYKSSJ { get; set; }

        /// <summary>
        ///     使用结束时间
        /// </summary>
        public string SYJSSJ { get; set; }
    }
}