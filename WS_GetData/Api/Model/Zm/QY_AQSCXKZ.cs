namespace LicenseSystemService.Api.Model.Zm
{
    /// <summary>
    ///     安全生产许可证
    /// </summary>
    public class QY_AQSCXKZ : BaseLicence
    {
        /// <summary>
        ///     主要负责人
        /// </summary>
        public string ZYFZR { get; set; }

        /// <summary>
        ///     单位地址
        /// </summary>
        public string DWDZ { get; set; }

        /// <summary>
        ///     经济类型
        /// </summary>
        public string JJLX { get; set; }

        /// <summary>
        ///     许可范围
        /// </summary>
        public string XKFW { get; set; }

        /// <summary>
        ///     有效期开始日期
        /// </summary>
        public string YXQKSRQ { get; set; }

        /// <summary>
        ///     证书状态
        /// </summary>
        public string ZSZT { get; set; }

        /// <summary>
        ///     备注信息
        /// </summary>
        public string BZXX { get; set; }
    }
}