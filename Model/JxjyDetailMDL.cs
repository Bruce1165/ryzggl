using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
    /// 继续教育记录明细（个人自培）
	/// </summary>
	[Serializable]
	public class JxjyDetailMDL
	{
		public JxjyDetailMDL()
		{
		}

		
        /// <summary>
        /// 培训记录ID
        /// </summary>
		public long? DetailID{ get; set; }

        /// <summary>
        /// 培训主表ID
        /// </summary>
		public long? BaseID{ get; set; }

        /// <summary>
        /// 序号
        /// </summary>
		public int? DataNo{ get; set; }

        /// <summary>
        /// 培训开始日期
        /// </summary>
		public DateTime? TrainDateStart{ get; set; }

        /// <summary>
        /// 培训截止时间
        /// </summary>
		public DateTime? TrainDateEnd{ get; set; }

        /// <summary>
        /// 培训内容
        /// </summary>
		public string TrainName{ get; set; }

        /// <summary>
        /// 培训方式：网络；现场
        /// </summary>
		public string TrainWay{ get; set; }

        /// <summary>
        /// 培训单位
        /// </summary>
		public string TrainUnit{ get; set; }

        /// <summary>
        /// 学时
        /// </summary>
		public int? Period{ get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? cjsj{ get; set; }

        /// <summary>
        /// 考核结果：合格/不合格
        /// </summary>
        public string ExamResult { get; set; }
	}
}
