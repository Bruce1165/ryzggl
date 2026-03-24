using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LicenseSystemService.Api.Model.Zm
{
    /// <summary>
    /// 建设项目年度计划通知书
    /// </summary>
    public class GC_JSXMNDJHTZS : BaseLicence
    {
        /// <summary>
        ///     项目名称
        /// </summary>
        public string XMMC { set; get; }

        /// <summary>
        ///     建设内容
        /// </summary>
        public string JSNR { set; get; }

        /// <summary>
        ///     建设地点
        /// </summary>
        public string JSDD { set; get; }

        /// <summary>
        ///     建设规模
        /// </summary>
        public string JSGM { set; get; }

        /// <summary>
        ///     占地面积
        /// </summary>
        public string ZDMJ { set; get; }

        /// <summary>
        ///     资金情况
        /// </summary>
        public string ZJQK { set; get; }

        /// <summary>
        ///     年度计划投资
        /// </summary>
        public string NDJHTZ { set; get; }

        /// <summary>
        ///     资金来源
        /// </summary>
        public string ZJLY { set; get; }

        /// <summary>
        ///     项目计划开工时间
        /// </summary>
        public string XMJHKGSJ { set; get; }
    }
}
