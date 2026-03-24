using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--TJ_VisitMDL 网站访问人次统计
	/// </summary>
	[Serializable]
	public class TJ_VisitMDL
	{
		public TJ_VisitMDL()
		{
		}

		
        /// <summary>
        /// 
        /// </summary>
		public int? TJ_Year{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public long? TJ_Count{ get; set; }
	}
}
