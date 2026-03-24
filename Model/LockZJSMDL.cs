using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--LockZJSMDL填写类描述
	/// </summary>
	[Serializable]
	public class LockZJSMDL
	{
		public LockZJSMDL()
		{
		}

		
        /// <summary>
        /// 锁定ID
        /// </summary>
		public long? LockID{ get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
		public string PSN_CertificateNO{ get; set; }

        /// <summary>
        /// 锁类型
        /// </summary>
		public string LockType{ get; set; }

        /// <summary>
        /// 锁定时间
        /// </summary>
		public DateTime? LockTime{ get; set; }

        /// <summary>
        /// 锁定截止时间
        /// </summary>
		public DateTime? LockEndTime{ get; set; }

        /// <summary>
        /// 锁定人
        /// </summary>
		public string LockPerson{ get; set; }

        /// <summary>
        /// 锁定说明
        /// </summary>
		public string LockRemark{ get; set; }

        /// <summary>
        /// 解锁时间
        /// </summary>
		public DateTime? UnlockTime{ get; set; }

        /// <summary>
        /// 解锁人
        /// </summary>
		public string UnlockPerson{ get; set; }

        /// <summary>
        /// 解锁说明
        /// </summary>
		public string UnlockRemark{ get; set; }

        /// <summary>
        /// 锁定状态
        /// </summary>
		public string LockStatus{ get; set; }
	}
}
