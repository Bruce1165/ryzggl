using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--GYPXWhiteMDL填写类描述
	/// </summary>
	[Serializable]
	public class GYPXWhiteMDL
	{
		public GYPXWhiteMDL()
		{
		}

		
        /// <summary>
        /// 数据主键
        /// </summary>
		public long? DataID{ get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
		public string WorkerCertificateCode{ get; set; }

        /// <summary>
        /// 岗位类别ID
        /// </summary>
		public int? PostTypeID{ get; set; }

        /// <summary>
        /// 有效期起始
        /// </summary>
		public DateTime? FreeStart{ get; set; }

        /// <summary>
        /// 有效期截止
        /// </summary>
		public DateTime? FreeEnd{ get; set; }
	}
}
