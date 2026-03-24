using System;

namespace Model
{
    [Serializable()]
    public class DepartmentMDL
    {
        private string deptID;
        private OrganizationMDL organ;
        private string pDeptID = String.Empty;
        private string deptName = String.Empty;
        private int orderID;

        public DepartmentMDL()
        {
        }

        public string DeptID
        {
            get { return this.deptID; }
            set { this.deptID = value; }
        }

        public OrganizationMDL Organ
        {
            get { return this.organ; }
            set { this.organ = value; }
        }

        public string PDeptID
        {
            get { return this.pDeptID; }
            set { this.pDeptID = value; }
        }

        public string DeptName
        {
            get { return this.deptName; }
            set { this.deptName = value; }
        }

        public int OrderID
        {
            get { return this.orderID; }
            set { this.orderID = value; }
        }
    }
}