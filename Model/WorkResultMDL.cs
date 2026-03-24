using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--WorkResultMDL填写类描述
	/// </summary>
	[Serializable]
	public class WorkResultMDL
	{
		public WorkResultMDL()
		{
		}

		
        /// <summary>
        /// 明细ID
        /// </summary>
		public long? DetailID{ get; set; }

        /// <summary>
        /// 申请ID
        /// </summary>
		public string ApplyID{ get; set; }

        /// <summary>
        /// 序号
        /// </summary>
		public int? DataNo{ get; set; }

        /// <summary>
        /// 日期起始
        /// </summary>
		public DateTime? DateStart{ get; set; }

        /// <summary>
        /// 日期截止
        /// </summary>
		public DateTime? DateEnd{ get; set; }

        /// <summary>
        /// 工程项目名称
        /// </summary>
		public string ProjectName{ get; set; }

        /// <summary>
        /// 负责工作内容
        /// </summary>
		public string Job{ get; set; }

        /// <summary>
        /// 总造价（万元）
        /// </summary>
		public string Cost{ get; set; }
	}
}
