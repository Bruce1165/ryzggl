using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--ApplyCheckTaskMDL填写类描述
	/// </summary>
	[Serializable]
	public class ApplyCheckTaskMDL
	{
		public ApplyCheckTaskMDL()
		{
		}

		
        /// <summary>
        /// 抽查ID
        /// </summary>
		public long? TaskID{ get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
		public string cjr{ get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime? cjsj{ get; set; }

        /// <summary>
        /// 抽查业务范围
        /// </summary>
		public string BusRange{ get; set; }

        /// <summary>
        /// 抽查业务范围编码
        /// </summary>
		public string BusRangeCode{ get; set; }

        /// <summary>
        /// 业务起始日期
        /// </summary>
		public DateTime? BusStartDate{ get; set; }

        /// <summary>
        /// 业务截止日期
        /// </summary>
		public DateTime? BusEndDate{ get; set; }

        /// <summary>
        /// 抽查千分比例
        /// </summary>
		public int? CheckPer{ get; set; }

        /// <summary>
        /// 抽查记录数量
        /// </summary>
		public int? ItemCount{ get; set; }
	}
}
