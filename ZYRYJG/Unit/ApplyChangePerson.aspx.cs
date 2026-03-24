using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.Unit
{
    public partial class ApplyChangePerson : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "Unit/ApplyList.aspx|County/Agency.aspx|County/BusinessQuery.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                // 验证工商信息是否通过

                if (IfExistRoleID("2") == true)//企业
                {
                    UnitMDL o = DataAccess.UnitDAL.GetObject(UserID);
                    if (o.ResultGSXX == 0 || o.ResultGSXX == 1)
                    {
                        Response.Write("<script>window.location.href='UnitMgr.aspx'</script>");
                    }
                }                

                //ButtonQuery_Click(sender,e);
                BindData();
                ButtonQYDR_Click(sender, e);
            }
        }

        //绑定调出
        public void BindData()
        {
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            if (IfExistRoleID("2") == true)//企业
            {
               // q.Add(string.Format("OldENT_Name like'{0}'", UserName));  南静注释  2019-11-06
                q.Add(string.Format("OldEnt_QYZJJGDM like'{0}%'", ZZJGDM));

            }
            if (RoleIDs == "0")//个人
            {
                q.Add(string.Format("(PSN_CertificateNO = '{0}')", WorkerCertificateCode));
            }
            q.Add("ApplyTypeSub='执业企业变更' ");
            if (!string.IsNullOrEmpty(RadTextBoxValue.Text))
            {
                q.Add(string.Format("{0} = '{1}'", RadComboBoxItem.SelectedValue, RadTextBoxValue.Text));
            }
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }
        //调出查询
        protected void ButtonQuery_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            BindData();
        }

        //是否同意调出
        protected void RadGridQY_ItemCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            switch (e.CommandName)
            {
                case "report"://同意
                    try
                    {
                        //DataTable dt = CommonDAL.GetDataTable("SELECT * FROM ApplyFile A INNER JOIN FileInfo B ON A.FILEID=B.FILEID WHERE A.APPLYID='" + RadGridQY.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyID"].ToString() + "' AND B.[FILENAME]='申请表扫描件'");
                        //if (dt.Rows.Count == 0)
                        //{
                        //    UIHelp.Alert(Page, "请通知申请单位提交申请表扫描件！");
                        //}
                        //else
                        //{
                        ApplyDAL.ApplyChangeOldUnitCheck(true, UserName, DateTime.Now, RadGridQY.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyID"].ToString());
                        BindData();
                        UIHelp.layerAlert(Page, "已同意调出！", 6, 2000);
                        //}
                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "执业企业变更同意失败", ex);
                    }
                    break;
                case "Cancelreport"://取消重新选择
                    try
                    {
                        ApplyDAL.ApplyChangeOldUnitCheck(false, UserName, DateTime.Now, RadGridQY.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyID"].ToString());
                        BindData();
                        UIHelp.layerAlert(Page, "已拒绝调出！", 6, 2000);
                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "执业企业变更拒绝失败", ex);
                    }
                    break;
            }
        }

        //调入查询
        protected void ButtonQYDR_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource2.SelectParameters.Clear();
            var q = new QueryParamOB();

            if (IfExistRoleID("2") == true)//企业
            {
                q.Add(string.Format("(ENT_ServerID='{0}' or OldEnt_QYZJJGDM like'{1}%')", UserID, ZZJGDM));
            }
            if (RoleIDs == "0")//个人
            {
                q.Add(string.Format("(PSN_CertificateNO = '{0}')", WorkerCertificateCode));
            }
            q.Add("ApplyTypeSub='执业企业变更' ");

            if (!string.IsNullOrEmpty(RadTextBoxQYDR.Text))
            {
                q.Add(string.Format("{0} = '{1}'", RadComboBoxQYDR.SelectedValue, RadTextBoxQYDR.Text));
            }
            ObjectDataSource2.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQYDR.CurrentPageIndex = 0;
            RadGridQYDR.DataSourceID = ObjectDataSource2.ID;
        }

        /// <summary>
        /// 转化申请状态为友好提示
        /// </summary>
        /// <param name="ApplyStatus"></param>
        /// <returns></returns>
        protected string GetShowStatus(string ApplyStatus)
        {
            switch (ApplyStatus)
            {
                case EnumManager.ApplyStatus.未申报:
                    return "待原单位确认";

                case EnumManager.ApplyStatus.已申报:
                    return "原单位同意";
                case EnumManager.ApplyStatus.已公告:
                    return "已办结";
                default:
                    return ApplyStatus;
            }
        }
    }
}