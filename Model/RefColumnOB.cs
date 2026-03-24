using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    /// <summary>
    /// 引用列映射对象
    /// </summary>
    [Serializable()]
    public class RefColumnOB
    {
        public RefColumnOB()
		{
            _RefMenuType = new List<string>();
            _RefTable = new List<string>();
		}

        private List<string> _RefMenuType;
        /// <summary>
        /// 枚举表映射集合：格式（Grid列名,枚举类型ID）
        /// </summary>
        public List<string> RefMenuType
        {
            get { return _RefMenuType; }
        }
        private List<string> _RefTable;
        /// <summary>
        /// 外键表引用映射集合：格式（Grid列名,外键表名,显示列,值列,值列是否为数值型）
        /// </summary>
        public List<string> RefTable
        {
            get { return _RefTable; }
        }

        /// <summary>
        /// 添加枚举引用（Types表）
        /// </summary>
        /// <param name="gridColumnUniqueName">GridView引用列唯一名称(UniqueName)</param>
        /// <param name="menuTypeID">枚举类型ID（Types表的TypeID）</param>
        public void AddMenuRef(string gridColumnUniqueName, string menuTypeID)
        {
            string fmt = "{0},{1}";
            RefMenuType.Add(string.Format(fmt,gridColumnUniqueName, menuTypeID));
        }

        /// <summary>
        /// 添加外键表引用（Types表）
        /// </summary>
        /// <param name="gridColumnUniqueName">GridView引用列唯一名称(UniqueName)</param>
        /// <param name="refTableName">外键表名</param>
        /// <param name="textColumnName">外键表显示列</param>
        /// <param name="valueColumnName">外键表值列</param>
        /// <param name="valueColumnIsNumericType">外键表值列是否是数值类型</param>
        public void AddTableRef(string gridColumnUniqueName, string refTableName, string textColumnName, string valueColumnName, bool valueColumnIsNumericType)
        {
            string fmt = "{0},{1},{2},{3},{4}";
            RefTable.Add(string.Format(fmt, gridColumnUniqueName, refTableName, textColumnName, valueColumnName, valueColumnIsNumericType.ToString()));
        }
    }
}
