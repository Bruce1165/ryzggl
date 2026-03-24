using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.County
{
    public partial class BusinessAccomplishDetail : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {

                string applyid = Request.QueryString["a"].ToString();
                BindFile(applyid);
                BindDate(applyid);
                DataTable dt = new DataTable();
                dt.Columns.Add("RowNum", typeof(object));
                dt.Columns.Add("AA", typeof(object));
                dt.Columns.Add("BB", typeof(object));
                dt.Columns.Add("CC", typeof(object));
                dt.Columns.Add("DD", typeof(object));
                dt.Columns.Add("EE", typeof(object));
                dt.Rows.Add(new object[] { 1, "受理", "钱丽姿", "2015-04-09 09:50:23", "通过", "予以受理" });
                dt.Rows.Add(new object[] { 2, "审查", "朱雅韶", "2015-04-10 09:50:23", "通过", "审查通过" });
                RadGridSBJL.DataSource = dt;
                RadGridSBJL.DataBind();
            }
        }
        /// <summary>
        /// 绑定附件
        /// </summary>
        /// <param name="ApplyID"></param>
        private void BindFile(string ApplyID)
        {
            DataTable dt_ApplyFile = ApplyFileDAL.GetListByApplyID(ApplyID);
            RadGridFile.DataSource = dt_ApplyFile;
            RadGridFile.DataBind();

            //ViewState["ApplyFile"] = dt_ApplyFile;
        }
        private void BindDate(string ApplyID)
        {
            try
            {
                //获取申请表对象
                ApplyMDL _ApplyMDL = ApplyDAL.GetObject(ApplyID);
                //根据申请表对象获得人员表信息
                COC_TOW_Person_BaseInfoMDL Person = COC_TOW_Person_BaseInfoDAL.GetObject(_ApplyMDL.PSN_ServerID);
                //如果人员信息表为空，就代表他为初始注册没有人员信息
                if (Person != null)
                {
                    UIHelp.SetData(GetTable, Person);
                }
                else
                {
                    //申请表，和初始注册申请表对象信息
                    ApplyFirstMDL ApplyFirstMDL = ApplyFirstDAL.GetObject(_ApplyMDL.PSN_ServerID);
                    UIHelp.SetData(GetTable, _ApplyMDL);
                    UIHelp.SetData(GetTable, ApplyFirstMDL);
                }
            }
            catch(Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "获取初始注册表信息失败或者人员表信息失败", ex);
            }
        }
    }
}