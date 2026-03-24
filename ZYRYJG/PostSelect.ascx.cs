using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using System.Reflection;

namespace ZYRYJG
{
    public partial class PostSelect : System.Web.UI.UserControl
    {
        //定义一个委托
        public delegate void userEvent(object sender, EventArgs arg);

        public event userEvent PostSelectChange;

        public event userEvent PostTypeSelectChange;

        /// <summary>
        /// 岗位类别下拉选择控件
        /// </summary>
        public RadComboBox RadComboBoxPostTypeID
        {
            get
            {
                return (RadComboBox)FindControl("RadComboBoxPostTypeID");
            }
        }
        /// <summary>
        /// 岗位工种下拉选择控件
        /// </summary>
        public RadComboBox RadComboBoxPostID
        {
            get
            {
                return (RadComboBox)FindControl("RadComboBoxPostID");
            }
        }

        /// <summary>
        /// 岗位类别ID
        /// </summary>
        public string PostTypeID
        {
            get
            {
                RadComboBox rcbPostType = (RadComboBox)FindControl("RadComboBoxPostTypeID");
                return rcbPostType.SelectedValue;
            }
            set
            {
                RadComboBox rcbPostType = (RadComboBox)FindControl("RadComboBoxPostTypeID");
                rcbPostType.FindItemByValue(value).Selected = true;
                BindRadComboBoxPostID(rcbPostType.SelectedValue);
            }
        }

         /// <summary>
        /// 岗位类别名称
        /// </summary>
        public string PostTypeName
        {
            get
            {
                RadComboBox rcbPostType = (RadComboBox)FindControl("RadComboBoxPostTypeID");
                return rcbPostType.SelectedItem.Text;
            }
            set
            {
                RadComboBox rcbPostType = (RadComboBox)FindControl("RadComboBoxPostTypeID");
                RadComboBoxItem item = rcbPostType.FindItemByText(value);
                if (item != null) item.Selected = true;
                BindRadComboBoxPostID(rcbPostType.SelectedValue);
            }
        }

        /// <summary>
        /// 锁住岗位类别下拉框
        /// </summary>
        public void LockPostTypeID()
        {
            RadComboBox rcbPostType = (RadComboBox)FindControl("RadComboBoxPostTypeID");
            rcbPostType.Enabled = false;
        }
        /// <summary>
        /// 岗位工种ID
        /// </summary>
        public string PostID
        {
            get
            {
                RadComboBox rcbPostID = (RadComboBox)FindControl("RadComboBoxPostID");
                return rcbPostID.SelectedValue;
            }
            set
            {
                RadComboBox rcbPostType = (RadComboBox)FindControl("RadComboBoxPostTypeID");
                BindRadComboBoxPostID(rcbPostType.SelectedValue);

                RadComboBox rcbPostID = (RadComboBox)FindControl("RadComboBoxPostID");
                rcbPostID.FindItemByValue(value).Selected = true;
            }
        }
        /// <summary>
        /// 岗位工种名称
        /// </summary>
        public string PostName
        {
            get
            {
                RadComboBox rcbPost = (RadComboBox)FindControl("RadComboBoxPostID");
                return rcbPost.SelectedItem.Text.Replace("♀ ","");
            }
            set
            {
                RadComboBox rcbPostID = (RadComboBox)FindControl("RadComboBoxPostID");
                RadComboBoxItem item = rcbPostID.FindItemByText("♀ " + value);
                if (item != null) item.Selected = true;
            }
        }

        /// <summary>
        /// 控件是否可用
        /// </summary>
        public bool Enabled
        {
            set
            {
                RadComboBox rcbPostType = (RadComboBox)FindControl("RadComboBoxPostTypeID");
                rcbPostType.Enabled = value;

                RadComboBox rcbPostID = (RadComboBox)FindControl("RadComboBoxPostID");
                rcbPostID.Enabled = value;
            }
        }

        /// <summary>
        /// 自定义岗位工种过滤查询条件,字典格式：Dictionary<岗位类别ID,过滤sql>
        /// </summary>
        public Dictionary<string,string> PostFilterString
        {
            get { return ViewState["PostFilterString"] == null ? new Dictionary<string, string>() : ViewState["PostFilterString"] as Dictionary<string, string>; }
            set { ViewState["PostFilterString"] = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //RadComboBox rcb = (RadComboBox)this.FindControl("RadComboBoxPostTypeID");
            //if (rcb.Items.Count == 0)
            //{
            //    rcb.Items.Clear();
            //    QueryParamOB q = new QueryParamOB();
            //    q.Add("PostType = 1 ");
            //    DataTable dt = PostInfoDAL.GetList(0, int.MaxValue - 1, q.ToWhereString(), "PostName");
            //    rcb.DataSource = dt;
            //    rcb.DataBind();
            //    rcb.Items.Insert(0, new RadComboBoxItem("请选择", ""));

            //    BindRadComboBoxPostID("");
            //}
        }
        public void BindRadComboBoxPostType()
        {
            RadComboBox rcb = (RadComboBox)this.FindControl("RadComboBoxPostTypeID");
                rcb.Items.Clear();
                QueryParamOB q = new QueryParamOB();
                q.Add("PostType = 1 ");
                DataTable dt = PostInfoDAL.GetList(0, int.MaxValue - 1, q.ToWhereString(), "PostID");
                rcb.DataSource = dt;
                rcb.DataBind();
                rcb.Items.Insert(0, new RadComboBoxItem("请选择", ""));

                BindRadComboBoxPostID("");
        }

        //绑定岗位类别
        protected void RadComboBoxPostTypeID_Init(object sender, EventArgs e)
        {
            RadComboBox rcb = (RadComboBox)sender;
            if (rcb.Items.Count == 0)
            {
                //rcb.Items.Clear();
                QueryParamOB q = new QueryParamOB();
                q.Add("PostType = 1 ");
                DataTable dt = PostInfoDAL.GetList(0, int.MaxValue - 1, q.ToWhereString(), "PostID");
                rcb.DataSource = dt;
                rcb.DataBind();
                rcb.Items.Insert(0, new RadComboBoxItem("请选择", ""));

                BindRadComboBoxPostID("");
            }
           
        }
        //变换选择岗位类别
        protected void RadComboBoxPostTypeID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PostTypeSelectChange != null) this.PostTypeSelectChange(this, e);

            RadComboBox rcb = (RadComboBox)FindControl("RadComboBoxPostID");
            if (rcb.Visible == false) return;
            RadComboBox rcbPostType = (RadComboBox)sender; 
            BindRadComboBoxPostID(rcbPostType.SelectedValue);            
            if (this.PostSelectChange != null) this.PostSelectChange(this, e);
        }
        //绑定岗位工种
        protected void BindRadComboBoxPostID(string TypeID)
        {            
            RadComboBox rcb = (RadComboBox)FindControl("RadComboBoxPostID");
            rcb.Items.Clear();
            if (TypeID == "")
            {                
                rcb.Items.Insert(0, new RadComboBoxItem("请选择", ""));
                return;
            }
            QueryParamOB q = new QueryParamOB();
            q.Add("PostType = 2");
            q.Add("UpPostID = " + TypeID);
            if (PostFilterString.Keys.Contains(TypeID)) q.Add(PostFilterString[TypeID]);//过滤条件
            DataTable dt = PostInfoDAL.GetList(0, int.MaxValue - 1, q.ToWhereString(), "CODEFORMAT,PostName");
            rcb.DataSource = dt;
            rcb.DataBind();
            rcb.Items.Insert(0, new RadComboBoxItem("请选择", ""));
        }
        //变换选择岗位工种
        protected void RadComboBoxPostID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.PostSelectChange != null) this.PostSelectChange(this, e);
        }
        /// <summary>
        /// 隐藏岗位工种选择框
        /// </summary>
        public void HideRadComboBoxPostID()
        {
            RadComboBox rcb = (RadComboBox)FindControl("RadComboBoxPostID");
            rcb.Visible = false;           
        }

        /// <summary>
        /// 隐藏岗位类别选择框
        /// </summary>
        public void HideRadComboBoxPostType()
        {
            RadComboBox rcb = (RadComboBox)FindControl("RadComboBoxPostTypeID");
            rcb.Style.Add("display","none");
        }
    }
}