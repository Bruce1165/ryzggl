using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// 业务实体类--DepartmentOB填写类描述
    /// </summary>
    [Serializable]
    public class DepartmentOB
    {
        public DepartmentOB()
        {
            //默认值
        }

        //主键
        protected long? _DeptID;

        //其它属性
        protected long? _pDeptID;
        protected long? _OrganID;
        protected string _DeptName;
        protected int? _OrderID;

        public long? DeptID
        {
            get { return _DeptID; }
            set { _DeptID = value; }
        }

        public long? pDeptID
        {
            get { return _pDeptID; }
            set { _pDeptID = value; }
        }

        public long? OrganID
        {
            get { return _OrganID; }
            set { _OrganID = value; }
        }

        public string DeptName
        {
            get { return _DeptName; }
            set { _DeptName = value; }
        }

        public int? OrderID
        {
            get { return _OrderID; }
            set { _OrderID = value; }
        }
    }
}
