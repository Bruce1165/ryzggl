using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LicenseSystemService.Api.Model.Zm
{
    /// <summary>
    /// 工程竣工验收备案表
    /// </summary>
    public class GC_JGYSBA : BaseLicence
    {
        /// <summary>
        ///     工程名称
        /// </summary>
        public string GCMC { set; get; }

        /// <summary>
        ///     本次备案内容
        /// </summary>
        public string BCBANR { set; get; }

        /// <summary>
        ///     本次备案规模
        /// </summary>
        public string BCBAGM { set; get; }

        /// <summary>
        ///     合同价格
        /// </summary>
        public string HTJG { set; get; }

        /// <summary>
        ///     工程类别
        /// </summary>
        public string GCLB { set; get; }

        /// <summary>
        ///     工程地址
        /// </summary>
        public string GCDZ { set; get; }

        /// <summary>
        ///     规划许可证号
        /// </summary>
        public string GHXKZH { set; get; }

        /// <summary>
        ///     规划验收编号
        /// </summary>
        public string GHYSBH { set; get; }

        /// <summary>
        ///     消防验收编号
        /// </summary>
        public string XFYSBH { set; get; }

        /// <summary>
        ///     档案预验收编号
        /// </summary>
        public string DAYYSBH { set; get; }

        /// <summary>
        ///     施工许可证号
        /// </summary>
        public string SGXKZH { set; get; }

        /// <summary>
        ///     开工时间
        /// </summary>
        public string KGSJ { set; get; }

        /// <summary>
        ///     竣工验收时间
        /// </summary>
        public string JGYSSJ { set; get; }

        /// <summary>
        ///     建设单位项目负责人
        /// </summary>
        public string JSDWXMFZR { set; get; }

        /// <summary>
        ///     施工单位名称
        /// </summary>
        public string SGDWMC { set; get; }

        /// <summary>
        ///     施工单位项目负责人
        /// </summary>
        public string SGDWXMFZR { set; get; }

        /// <summary>
        ///     监理单位名称
        /// </summary>
        public string JLDWMC { set; get; }

        /// <summary>
        ///     监理单位目负责人
        /// </summary>
        public string JLDWXMFZR { set; get; }

        /// <summary>
        ///     勘察单位名称
        /// </summary>
        public string KCDWMC { set; get; }

        /// <summary>
        ///     勘察单位目负责人
        /// </summary>
        public string KCDWXMFZR { set; get; }

        /// <summary>
        ///     建设单位经办人
        /// </summary>
        public string JSDWJBR { set; get; }

        /// <summary>
        ///     建设单位经办人手机号
        /// </summary>
        public string JSDWJBRSJH { set; get; }

        /// <summary>
        ///     质量监督机构
        /// </summary>
        public string ZLJDJG { set; get; }

        /// <summary>
        ///     质量监督注册号
        /// </summary>
        public string ZLJDZCH { set; get; }
    }
}
