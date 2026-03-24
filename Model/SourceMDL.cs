using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--课程或课件
	/// </summary>
	[Serializable]
	public class SourceMDL
	{
		public SourceMDL()
		{
		}

		
        /// <summary>
        /// 课程/课件ID
        /// </summary>
		public long? SourceID{ get; set; }

        /// <summary>
        /// 课程/课件名称
        /// </summary>
		public string SourceName{ get; set; }

        /// <summary>
        /// 讲师
        /// </summary>
		public string Teacher{ get; set; }

        /// <summary>
        /// 工作单位及职称
        /// </summary>
		public string WorkUnit{ get; set; }

        /// <summary>
        /// 课程类型：选修、必修
        /// </summary>
		public string SourceType{ get; set; }

        /// <summary>
        /// 状态：启用、停用
        /// </summary>
		public string Status{ get; set; }

        /// <summary>
        /// 课程简介
        /// </summary>
		public string Description{ get; set; }

        /// <summary>
        /// 课件隶属课程ID
        /// </summary>
		public long? ParentSourceID{ get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
		public int? SortID{ get; set; }

        /// <summary>
        /// 学时（分钟）
        /// </summary>
		public int? Period{ get; set; }

        /// <summary>
        /// 课程包含课件数量
        /// </summary>
		public int? SourceWareCount{ get; set; }

        /// <summary>
        /// 课件播放url
        /// </summary>
		public string SourceWareUrl{ get; set; }

        /// <summary>
        /// 播放参数
        /// </summary>
		public string SourceWarePlayParam{ get; set; }

        /// <summary>
        /// 上架年度
        /// </summary>
        public int? SourceYear{ get; set; }

        /// <summary>
        /// 内容方向：废弃
        /// </summary>
        public string Lab { get; set; }

        /// <summary>
        /// 所属栏目
        /// </summary>
        public string BarType { get; set; }

        /// <summary>
        /// 课程背景图片
        /// </summary>
        public string SourceImg { get; set; }

        /// <summary>
        /// 显示学时：根据学时[Period]字段计算，规则：1、学时小数部分0.1~0.4学时统一升级到0.5学时；2、学时小数部分0.6~0.9学时统一升级到1.0学时。
        /// </summary>
        public decimal? ShowPeriod { get; set; }
	}
}
