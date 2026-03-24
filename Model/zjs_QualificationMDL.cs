using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
	/// <summary>
	/// 业务实体类--造价工程师资格证书
	/// </summary>
	[Serializable]
	public class zjs_QualificationMDL
	{
		public zjs_QualificationMDL()
		{
		}

        /// <summary>
        /// 旧资格证号
        /// </summary>
        public string Old_ZGZSBH { get; set; }

        /// <summary>
        /// 资格证号
        /// </summary>
        public string ZGZSBH { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string XM { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string ZJHM { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        public string SF { get; set; }

        /// <summary>
        /// 工作单位
        /// </summary>
        public string GZDW { get; set; }

        /// <summary>
        /// 取得年份
        /// </summary>
        public string QDNF { get; set; }

        /// <summary>
        /// 取得方式
        /// </summary>
        public string QDFS { get; set; }

        /// <summary>
        /// 管理号
        /// </summary>
        public string GLH { get; set; }

        /// <summary>
        /// 专业类别
        /// </summary>
        public string ZYLB { get; set; }

        /// <summary>
        /// 毕业学校
        /// </summary>
        public string BYXX { get; set; }

        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime? BYSJ { get; set; }

        /// <summary>
        /// 所学专业
        /// </summary>
        public string SXZY { get; set; }

        /// <summary>
        /// 最高学历
        /// </summary>
        public string ZGXL { get; set; }

        /// <summary>
        /// 签发时间
        /// </summary>
        public DateTime? QFSJ { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        public DateTime? LastModifyTime { get; set; }
	}
}
