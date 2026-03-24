using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 考前培训内容详细
	/// </summary>
	[Serializable]
	public class ExamSignupTrainMDL
	{
		public ExamSignupTrainMDL()
		{
		}

		
        /// <summary>
        /// 
        /// </summary>
		public long? DetailID{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public long? ExamSignUpID{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public int? DataNo{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime? TrainDateStart{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime? TrainDateEnd{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string TrainType{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string TrainName{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string TrainWay{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public int? Period{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime? cjsj{ get; set; }
	}
}
