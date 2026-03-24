using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务查询参数实体类--QueryParamOB填写类描述
    /// </summary>
    [Serializable]
    public class QueryParamOB
    {
        public QueryParamOB()
        {
            _Item = new List<string>();
        }

        ///// <summary>
        ///// 创建查询参数对象
        ///// </summary>
        ///// <param name="IsAll">是否查询全部</param>
        //public QueryParamOB(bool IsAll)
        //{
        //    if (IsAll == true)
        //    {
        //        _PageIndex = 0;
        //        _PageSize = int.MaxValue - 1;
        //    }
        //    _Item = new List<string>();
        //}

        #region 公共属性

        protected List<string> _Item;

        /// <summary>
        /// 查询条件集合(添加请用Add方法):格式如“TableName='Test'”,
        /// </summary>
        public List<string> Item
        {
            get { return _Item; }
        }

        //protected string _TableName;
        //protected string _ClolumnList;
        //protected string _SortBy;
        //private int _PageIndex;
        //private int _PageSize;
        //private int _RecordCount;

        ///// <summary>
        ///// 排序键(可以为组合键)
        ///// </summary>
        //public string SortBy
        //{
        //    get { return _SortBy; }
        //    set { _SortBy = value; }
        //}
        ///// <summary>
        ///// 查询表(试图)名称,或表连接格式(如：Table1 inner join Table2 on Table1.id=Table2.id)
        ///// </summary>
        //public string TableName
        //{
        //    get { return _TableName; }
        //    set { _TableName = value; }
        //}
        ///// <summary>
        ///// 查询字段集合，用“,”号分割
        ///// </summary>
        //public string ClolumnList
        //{
        //    get { return _ClolumnList; }
        //    set { _ClolumnList = value; }
        //}

        ///// <summary>
        ///// 当前页Index,从0开始
        ///// </summary>
        //public int PageIndex
        //{
        //    get { return _PageIndex; }
        //    set { _PageIndex = value; }
        //}
        ///// <summary>
        ///// 每页行数
        ///// </summary>
        //public int PageSize
        //{
        //    get { return _PageSize; }
        //    set { _PageSize = value; }
        //}
        ///// <summary>
        ///// 总记录数
        ///// </summary>
        //public int RecordCount
        //{
        //    get { return _RecordCount; }
        //    set { _RecordCount = value; }
        //}

        #endregion 公共属性

        /// <summary>
        /// 添加查询条件
        /// </summary>
        /// <param name="whereString">查询条件语句，格式如：</param>
        public void Add(string whereString)
        {
            if (_Item == null) _Item = new List<string>();
            if (_Item.Contains(whereString) == false) _Item.Add(whereString);
        }

        /// <summary>
        /// 获取格式化后的查询Where条件语句
        /// </summary>
        public string ToWhereString()
        {
            StringBuilder strWhere = new StringBuilder();

            if (_Item != null && _Item.Count > 0)
            {
                foreach (string s in _Item)
                {
                    strWhere.Append(" and ").Append(s);
                }
            }

            return strWhere.ToString();
        }
    }
}