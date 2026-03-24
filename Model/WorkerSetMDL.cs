using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--个人空间设置
	/// </summary>
	[Serializable]
	public class WorkerSetMDL
	{
		public WorkerSetMDL()
		{
		}

		
        /// <summary>
        /// 证件号码
        /// </summary>
		public string WorkerCertificateCode{ get; set; }

        /// <summary>
        /// 个人收藏课程ID集合，用英文逗号分割
        /// </summary>
		public string SaveSource{ get; set; }
	}
}
