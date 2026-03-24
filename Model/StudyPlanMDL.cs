using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--StudyPlanMDL填写类描述
	/// </summary>
	[Serializable]
	public class StudyPlanMDL
	{
		public StudyPlanMDL()
		{
		}

		
        /// <summary>
        /// 证件号码
        /// </summary>
		public string WorkerCertificateCode{ get; set; }

        /// <summary>
        /// 培训包ID
        /// </summary>
		public long? PackageID{ get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
		public string WorkerName{ get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime? CreateTime{ get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
		public string Creater{ get; set; }

        /// <summary>
        /// 学习计划加入方式：个人添加、系统指派
        /// </summary>
		public string AddType{ get; set; }

        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime? FinishDate { get; set; }

        /// <summary>
        /// 测试状态：未达标、已达标
        /// </summary>
        public string TestStatus { get; set; }
	}
}
