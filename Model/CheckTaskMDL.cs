using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--CheckTaskMDL填写类描述
	/// </summary>
	[Serializable]
	public class CheckTaskMDL
	{
		public CheckTaskMDL()
		{
		}

		
        /// <summary>
        /// 批次号
        /// </summary>
		public int? PatchCode{ get; set; }

        /// <summary>
        /// 监管类型
        /// </summary>
		public string CheckType{ get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
		public DateTime? CreateTime{ get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
		public string CJR{ get; set; }

        /// <summary>
        /// 发布时间
        /// </summary>
		public DateTime? PublishiTime{ get; set; }

        /// <summary>
        /// 上报截止时间
        /// </summary>
		public DateTime? LastReportTime{ get; set; }

        /// <summary>
        /// 是否短信通知
        /// </summary>
		public bool? ifPhoneNotice{ get; set; }

        /// <summary>
        /// 短信内容
        /// </summary>
		public string PhoneNotice{ get; set; }

        /// <summary>
        /// 是否弹框提示
        /// </summary>
		public bool? ifTipNotice{ get; set; }

        /// <summary>
        /// 弹框内容
        /// </summary>
		public string TipNotice{ get; set; }
	}
}
