using System;

namespace Model
{
    /// <summary>
    ///
    /// </summary>
    [Serializable()]
    public class RoleMDL
    {
        private string roleID;
        private string roleName = String.Empty;
        private string memo = String.Empty;
        private int orderID;

        public string RoleID
        {
            get { return this.roleID; }
            set { this.roleID = value; }
        }

        public string RoleName
        {
            get { return this.roleName; }
            set { this.roleName = value; }
        }

        public string Memo
        {
            get { return this.memo; }
            set { this.memo = value; }
        }

        public int OrderID
        {
            get { return this.orderID; }
            set { this.orderID = value; }
        }
    }
}