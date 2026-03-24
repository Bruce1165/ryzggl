using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LicenseSystemService.Api.Model.Zm
{
    /// <summary>
    /// 建设项目备案通知书
    /// </summary>
    public class GC_JSXMBATZS : BaseLicence
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
        ///     资金情况
        /// </summary>
        public string ZJQK { set; get; }

        /// <summary>
        ///     项目计划开工时间
        /// </summary>
        public string XMJHKGSJ { set; get; }

        /// <summary>
        ///     占地面积
        /// </summary>
        public string ZDMJ { set; get; }

        /// <summary>
        ///     主管部门批复的项目立项文件
        /// </summary>
        public string ZGBMPFXMWH { set; get; }

        /// <summary>
        ///     市规划主管部门批复的规划条件
        /// </summary>
        public string SGHZGBMPFWH { set; get; }
    }
}
