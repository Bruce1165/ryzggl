using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WS_GetData.Api.Model.Zm
{
    /// <summary>
    ///  专业技术管理人员证书
    /// </summary>
    [Serializable]
    public class EXAM_ZYJS : BaseLicence
    {
        /// <summary>
        /// 企业名称
        /// </summary>
        public string QY_Name { set; get; }

        /// <summary>
        /// 企业代码
        /// </summary>
        public string CreditCode { set; get; }

        /// <summary>
        /// 岗位类别
        /// </summary>
        public string GWLB { set; get; }

        /// <summary>
        /// 岗位工种
        /// </summary>
        public string GWGZ { set; get; }

        /// <summary>
        /// 制证日期
        /// </summary>
        public string MakeDate { set; get; }

    }
}
