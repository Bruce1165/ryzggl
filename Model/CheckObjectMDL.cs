using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--CheckObjectMDL填写类描述
	/// </summary>
	[Serializable]
	public class CheckObjectMDL
	{
		public CheckObjectMDL()
		{
		}

		
        /// <summary>
        /// 对象ID
        /// </summary>
		public string CheckID{ get; set; }

        /// <summary>
        /// 检查年度
        /// </summary>
		public int? CheckYear{ get; set; }

        /// <summary>
        /// 证书编号
        /// </summary>
		public string PSN_RegisterNo{ get; set; }

        /// <summary>
        /// 岗位类型
        /// </summary>
		public string PostTypeName{ get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
		public string PSN_Name{ get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
		public string PSN_CertificateNO{ get; set; }

        /// <summary>
        /// 企业名称
        /// </summary>
		public string ENT_Name{ get; set; }

        /// <summary>
        /// 专业及有效期
        /// </summary>
		public string ProfessionWithValid{ get; set; }

        /// <summary>
        /// 申报事项
        /// </summary>
		public string ApplyType{ get; set; }

        /// <summary>
        /// 申报时间
        /// </summary>
		public DateTime? ApplyTime{ get; set; }

        /// <summary>
        /// 办结时间
        /// </summary>
		public DateTime? NoticeDate{ get; set; }
	}
}
