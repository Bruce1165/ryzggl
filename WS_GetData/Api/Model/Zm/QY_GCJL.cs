namespace LicenseSystemService.Api.Model.Zm
{
    /// <summary>
    ///     工程监理
    /// </summary>
    public class QY_GCJL : BaseLicence
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
    }
}