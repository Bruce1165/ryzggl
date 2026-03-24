using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
    /// 业务实体类--信息中心提供名单，按该名单修改企业隶属区县（如果企业变更区县，可能会出现问题，需要手工维护）
	/// </summary>
	[Serializable]
	public class jcsjk_QY_ZHXX_CITY_ERRORMDL
	{
		public jcsjk_QY_ZHXX_CITY_ERRORMDL()
		{
		}

		
        /// <summary>
        /// 企业名称
        /// </summary>
		public string QYMC{ get; set; }

        /// <summary>
        /// 隶属区县
        /// </summary>
		public string XZDQBM{ get; set; }
	}
}
