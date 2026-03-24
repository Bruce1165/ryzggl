using System;

namespace WS_GetData.Api.Model.Zm
{
    /// <summary>
    /// 职业技能人员证书
    /// </summary>
    [Serializable]
    public class EXAM_ZYJN : BaseLicence
    {
        ///// <summary>
        /////     企业名称
        ///// </summary>
        //public string QYMC { set; get; }

        ///// <summary>
        /////     企业证照类型
        ///// </summary>
        //public string QYZZLX { set; get; }

        ///// <summary>
        /////     企业证照号码
        ///// </summary>
        //public string QYZZHM { set; get; }

        ///// <summary>
        /////     制证日期
        ///// </summary>
        //public string ZZRQ { set; get; }

        ///// <summary>
        /////     岗位名称
        ///// </summary>
        //public string GWMC { set; get; }

        ///// <summary>
        /////     二维码
        ///// </summary>
        //public string RWM { set; get; }

        ///// <summary>
        /////     等级
        ///// </summary>
        //public string DJ { set; get; }

        ///// <summary>
        /////     照片
        ///// </summary>
        //public string ZP { set; get; }


        /// <summary>
        ///     企业名称
        /// </summary>
        public string QY_Name { set; get; }
        /// <summary>
        ///     企业代码
        /// </summary>
        public string CreditCode { set; get; }
        /// <summary>
        ///     岗位类别
        /// </summary>
        public string GWLB { set; get; }
        /// <summary>
        ///     岗位工种
        /// <summary/>
        public string GWGZ { set; get; }
        /// <summary>
        ///     制证日期
        /// </summary>
        public string MakeDate { set; get; }
        /// <summary>
        ///     等级
        /// </summary>
        public string Level { set; get; }

    }
}