using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--PackageSourceMDL填写类描述
	/// </summary>
	[Serializable]
	public class PackageSourceMDL
	{
		public PackageSourceMDL()
		{
		}

		
        /// <summary>
        /// 课程ID
        /// </summary>
		public long? SourceID{ get; set; }

        /// <summary>
        /// 课程包ID
        /// </summary>
		public long? PackageID{ get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
		public int? SortID{ get; set; }

        /// <summary>
        /// 上架年度
        /// </summary>
        public int? SourceYear { get; set; }
	}
}
