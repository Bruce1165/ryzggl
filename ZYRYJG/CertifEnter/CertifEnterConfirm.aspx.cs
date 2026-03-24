using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Model;
using DataAccess;
using System.Data;
using Utility;
namespace ZYRYJG.CertifEnter
{
    public partial class CertifEnterConfirm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["o"]) == false)
                {

                    PostSelect1.PostTypeID = Request["o"];//岗位类别ID
                    PostSelect1.LockPostTypeID();
                    LabelPostType.Text = UIHelp.GetPostTypeNameByID(Request["o"]);
                }
                ButtonSearch_Click(sender, e);
            }
        }
        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ClearGridSelectedKeys(RadGrid1);
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            q.Add(string.Format("Status = '{0}'", EnumManager.CertificateUpdateType.EnterWaitCheck));//进京待审批
            if (PostSelect1.PostTypeID != "") q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));      //岗位类别
            if (PostSelect1.PostID != "") q.Add(string.Format("PostID = {0}", PostSelect1.PostID));
            if (rdtxtWorkerName.Text.Trim() != "")    //姓名
            {
                q.Add(string.Format("WorkerName like '%{0}%'", rdtxtWorkerName.Text.Trim()));
            }

            if (rdtxtQYMC.Text.Trim() != "")   //单位名称
            {
                q.Add(string.Format("UnitName like '%{0}%'", rdtxtQYMC.Text.Trim()));
            }

            if (rdtxtApplyCode.Text.Trim() != "")   //编号批次号
            {
                q.Add(string.Format("CertificateID in(select CertificateID from dbo.CertificateEnterApply where ConfrimCode like '%{0}%')", rdtxtApplyCode.Text.Trim()));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
            ViewState["whereString"] = q.ToWhereString();
        }

        //Grid换页
        protected void RadGridAccept_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "CertificateID");
        }

        //Grid绑定勾选checkbox状态
        protected void RadGridAccept_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGrid1, "CertificateID");

            if (RadGrid1.MasterTableView.Items.Count == 0)
                ButtonCheck.Visible = false;
            else
                ButtonCheck.Visible = true;
        }

        //审批
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "CertificateID");
            if (IsGridSelected(RadGrid1) == false)
            {
                UIHelp.layerAlert(Page, "你还没有选择证书！");
                return;
            }

            string filterString = "";//过滤条件

            if (GetGridIfCheckAll(RadGrid1) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGrid1) == true)//排除
                    filterString = string.Format(" {0} and CertificateID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
                else//包含
                    filterString = string.Format(" {0} and CertificateID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
            }

            CertificateOB ceron = new CertificateOB();
            
            ceron.CheckMan = PersonName;//审批人
            ceron.CheckAdvise = "通过";//审批结果
            ceron.CheckDate = DateTime.Now;//审批时间
            ceron.Status = EnumManager.CertificateUpdateType.InBeiJing;//证书的更新状态
            ceron.ModifyTime = DateTime.Now;//修改时间
            ceron.ModifyPersonID = PersonID;//修改人
            DataTable dt= null;
            try
            {
                dt=CertificateDAL.GetList(0,int.MaxValue -1,filterString," CertificateID");
                CertificateDAL.Check(null,ceron, filterString);                
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "证书审批失败！", ex);
                return;
            }
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (DataRow dr in dt.Rows)
            {
                sb.Append("、").Append(dr["CertificateCode"].ToString());
            }
            if (sb.Length > 0) sb.Remove(0, 1);
            UIHelp.WriteOperateLog(PersonName, UserID, "决定证书进京", string.Format("证书编号：{0}。", sb.ToString()));

            UIHelp.layerAlert(Page, "证书审批成功！",6,3000);
            ClearGridSelectedKeys(RadGrid1);
            RadGrid1.DataBind();
        }     
    }
}
