using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--LockJZSMDL填写类描述
	/// </summary>
	[Serializable]
	public class LockJZSMDL
	{
		public LockJZSMDL()
		{
		}

		
        /// <summary>
        /// 
        /// </summary>
		public long? LockID{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string PSN_ServerID{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string LockType{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime? LockTime{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime? LockEndTime{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string LockPerson{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string LockRemark{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public DateTime? UnlockTime{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string UnlockPerson{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string UnlockRemark{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string LockStatus{ get; set; }
	}
}
