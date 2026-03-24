using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--FinishSourceWareMDL填写类描述
	/// </summary>
	[Serializable]
	public class FinishSourceWareMDL
	{
		public FinishSourceWareMDL()
		{
		}

		
        /// <summary>
        /// 证件号码
        /// </summary>
		public string WorkerCertificateCode{ get; set; }

        /// <summary>
        /// 课件ID
        /// </summary>
		public long? SourceID{ get; set; }

        /// <summary>
        /// 最后学习时间
        /// </summary>
		public DateTime? LearnTime{ get; set; }

        /// <summary>
        /// 要求学时
        /// </summary>
		public int? Period{ get; set; }

        /// <summary>
        /// 完成学时
        /// </summary>
		public int? FinishPeriod{ get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string WorkerName{ get; set; }

         /// <summary>
        /// 测试状态，0：未达标；1：已达标。
        /// </summary>
        public int? StudyStatus { get; set; }

        /// <summary>
        /// 播放状态：111：点播开始，112：点播结束
        /// </summary>
        public int? PlayAction { get; set; }
	}
}
