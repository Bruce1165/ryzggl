using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--TrainUnitMDL填写类描述
	/// </summary>
	[Serializable]
	public class TrainUnitMDL
	{
		public TrainUnitMDL()
		{
		}

		
        /// <summary>
        /// 
        /// </summary>
		public string UnitNo{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string TrainUnitName{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string UnitCode{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string PostSet{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public int? UseStatus{ get; set; }
	}
}
