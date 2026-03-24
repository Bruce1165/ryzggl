using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
    /// 业务实体类--TrainQuestOptionMDL公益培训试题选项
	/// </summary>
	[Serializable]
	public class TrainQuestOptionMDL
	{
		public TrainQuestOptionMDL()
		{
		}

		
        /// <summary>
        /// 试题选项ID
        /// </summary>
		public long? QuestOptionID{ get; set; }

        /// <summary>
        /// 试题ID
        /// </summary>
		public long? QuestionID{ get; set; }

        /// <summary>
        /// 选项编号
        /// </summary>
		public string OptionNo{ get; set; }

        /// <summary>
        /// 选项内容
        /// </summary>
		public string OptionContent{ get; set; }
	}
}
