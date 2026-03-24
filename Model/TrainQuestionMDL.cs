using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--TrainQuestionMDL公益培训试题
	/// </summary>
	[Serializable]
	public class TrainQuestionMDL
	{
		public TrainQuestionMDL()
		{
		}

		
        /// <summary>
        /// 试题ID
        /// </summary>
		public long? QuestionID{ get; set; }

        /// <summary>
        /// 试题类型：判断题，单选题，多选题
        /// </summary>
		public string QuestionType{ get; set; }

        /// <summary>
        /// 题目
        /// </summary>
		public string Title{ get; set; }

        /// <summary>
        /// 分数
        /// </summary>
		public int? Score{ get; set; }

        /// <summary>
        /// 标准答案
        /// </summary>
		public string Answer{ get; set; }

        /// <summary>
        /// 状态：启用，停用
        /// </summary>
		public string Flag{ get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
		public DateTime? LastModifyTime{ get; set; }

        /// <summary>
        /// 试题编号
        /// </summary>
        public string QuestionNo { get; set; }

        /// <summary>
        /// 课程ID
        /// </summary>
        public long? SourceID { get; set; }
	}
}
