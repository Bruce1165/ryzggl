namespace LicenseSystemService.Api.Model.Zm
{
    /// <summary>
    ///     建筑工程施工许可证
    /// </summary>
    public class GC_SGXK : BaseLicence
    {
        /// <summary>
        ///     工程名称
        /// </summary>
        public string GCMC { get; set; }

        /// <summary>
        ///     建设地址
        /// </summary>
        public string JSDZ { get; set; }

        /// <summary>
        ///     建设规模
        /// </summary>
        public string JSGM { get; set; }

        /// <summary>
        ///     合同价格
        /// </summary>
        public string GHJG { get; set; }

        /// <summary>
        ///     勘察单位
        /// </summary>
        public string KCDW { get; set; }

        /// <summary>
        ///     设计单位
        /// </summary>
        public string SJDW { get; set; }

        /// <summary>
        ///     施工单位
        /// </summary>
        public string SGDW { get; set; }

        /// <summary>
        ///     监理单位
        /// </summary>
        public string JLDW { get; set; }

        /// <summary>
        ///     勘察单位项目负责人
        /// </summary>
        public string KCDWXMFZR { get; set; }

        /// <summary>
        ///     设计单位项目负责人
        /// </summary>
        public string SJDWXMFZR { get; set; }

        /// <summary>
        ///     施工单位项目负责人
        /// </summary>
        public string SGDWXMFZR { get; set; }

        /// <summary>
        ///     总监理工程师
        /// </summary>
        public string ZJLGCS { get; set; }

        /// <summary>
        ///     合同工期
        /// </summary>
        public string HTGQ { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string BZ { get; set; }

        /// <summary>
        /// 许可证编号国家
        /// </summary>
        public string XKZBH { get; set; }
    }
}