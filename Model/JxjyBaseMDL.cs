using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
    /// 业务实体类--JxjyBaseMDL继续教育记录
	/// </summary>
	[Serializable]
	public class JxjyBaseMDL
	{
		public JxjyBaseMDL()
		{
		}

		
        /// <summary>
        /// 培训ID
        /// </summary>
		public long? BaseID{ get; set; }

        /// <summary>
        /// 岗位类别ID
        /// </summary>
		public int? PostTypeID{ get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
		public string WorkerName{ get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
		public string WorkerCertificateCode{ get; set; }

        /// <summary>
        /// 岗位类别
        /// </summary>
		public string PostTypeName{ get; set; }

        /// <summary>
        /// 岗位工种
        /// </summary>
		public string PostName{ get; set; }

        /// <summary>
        /// 证书编号
        /// </summary>
		public string CertificateCode{ get; set; }

        /// <summary>
        /// 证书有效期截止日期
        /// </summary>
		public DateTime? ValidEndDate{ get; set; }

        /// <summary>
        /// 注册单位
        /// </summary>
		public string UnitName{ get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime? cjsj{ get; set; }

        /// <summary>
        /// 业务申请ID
        /// </summary>
		public string ApplyID{ get; set; }
	}
}
