using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--PackageMDL填写类描述
	/// </summary>
	[Serializable]
	public class PackageMDL
	{
		public PackageMDL()
		{
		}

		
        /// <summary>
        /// 
        /// </summary>
		public long? PackageID{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string PackageTitle{ get; set; }
        
        /// <summary>
        /// 
        /// </summary>
		public int? Period{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string Description{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string Status{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string PostTypeName{ get; set; }

        /// <summary>
        /// 
        /// </summary>
		public string PostName{ get; set; }

	}
}
