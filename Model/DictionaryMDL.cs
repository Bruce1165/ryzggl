using System;

namespace Model
{
    /// <summary>
    ///字典表实体类
    /// </summary>
    [Serializable()]
    public class DictionaryMDL
    {
        /// <summary>
        /// 字典表主键
        /// </summary>
        public string DicID { set; get; }

        /// <summary>
        /// 类型ID
        /// </summary>
        public int TypeID { set; get; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string TypeName { set; get; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int OrderID { set; get; }

        /// <summary>
        /// 名称
        /// </summary>
        public string DicName { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string DicDesc { set; get; }

        //分类
        public int? Category { set; get; }
    }
}