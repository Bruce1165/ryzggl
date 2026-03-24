using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;
using DataAccess;

namespace ZYRYJG.CertifEnter
{
    public partial class CertifEnterApplyList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //Response.Redirect("~/ResultInfoPage.aspx?o=三类人证书进京业务正在升级调整，调整期暂时无法提供服务，请耐心等待！", true);
                //return;

                if (IfExistRoleID("0") == true)  //个人登录
                {
                    div_New.Visible = true;
                }
                if (IfExistRoleID("2") == true)//企业
                {
                    td_QYQuergyParm.Visible = true;
                }

                if (string.IsNullOrEmpty(Request["o"]) == false)
                {
                    ViewState["PostTypeID"] = Request["o"];//岗位类别ID
                    LabelPostType.Text = UIHelp.GetPostTypeNameByID(Request["o"]);
                }
                else
                {
                    LabelRoad.Text = "业务办理";
                    LabelPostType.Text = "申请证书进京";
                }
                BindGrid();
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
            BindGrid();
        }

        protected void BindGrid()
        {
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            switch (PersonType)
            {
                case 2://考生
                    //WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);
                    //string IDCard15 = "";
                    //string IDCard18 = "";
                    //if (_WorkerOB == null)
                    //{
                    //    IDCard15 = IDCard18 = "null";
                    //}
                    //else if (_WorkerOB.CertificateCode.Length == 15)
                    //{
                    //    IDCard15 = _WorkerOB.CertificateCode;
                    //    IDCard18 = Utility.Check.ConvertoIDCard15To18(IDCard15);
                    //}
                    //else if (_WorkerOB.CertificateCode.Length == 18)
                    //{
                    //    IDCard15 = _WorkerOB.CertificateCode.Remove(17, 1).Remove(6, 2);
                    //    IDCard18 = _WorkerOB.CertificateCode;
                    //}
                    //else
                    //{
                    //    IDCard15 = _WorkerOB.CertificateCode;
                    //    IDCard18 = _WorkerOB.CertificateCode;
                    //}
                    //q.Add(string.Format("(WorkerCertificateCode='{0}' or WorkerCertificateCode='{1}')", IDCard15, IDCard18));
                    q.Add(string.Format("WorkerCertificateCode='{0}'", WorkerCertificateCode));
                    break;
                case 3://企业

                    q.Add(string.Format("UnitCode='{0}'", ZZJGDM));
                    //q.Add(string.Format("[ApplySTATUS]='{0}'", EnumManager.CertificateEnterStatus.WaitUnitCheck));
                    switch (RadioButtonListQYCheckStatus.SelectedValue)
                    {
                        case "待单位确认":
                            q.Add(string.Format("[ApplySTATUS]='{0}'", EnumManager.CertificateEnterStatus.WaitUnitCheck));
                            break;
                        case "单位已确认":
                            q.Add(string.Format("[ApplySTATUS]<>'{0}' and [ApplySTATUS]<>'{1}'", EnumManager.CertificateEnterStatus.NewSave, EnumManager.CertificateEnterStatus.WaitUnitCheck));
                            break;
                        case "全部":
                            q.Add(string.Format("[ApplySTATUS]<>'{0}'", EnumManager.CertificateEnterStatus.NewSave));
                            break;
                        default:
                            q.Add(string.Format("[ApplySTATUS]<>'{0}'", EnumManager.CertificateEnterStatus.NewSave));
                            break;
                    }
                    break;
                default:
                    q.Add(string.Format("CreatePersonID={0}", PersonID.ToString()));//创建者
                    break;

            }
            if(ViewState["PostTypeID"] !=null) q.Add(string.Format("PostTypeID={0}", ViewState["PostTypeID"].ToString()));//岗位类别

            //申请批次号
            if (RadTextBoxApplyCode.Text.Trim() != "")
            {
                q.Add(string.Format("ApplyCode like '%{0}%'", RadTextBoxApplyCode.Text.Trim()));
            }

            //证书号码
            if (RadTextBoxCertificateCode.Text != "")
            {
                q.Add(string.Format("CertificateCode like '%{0}%'", RadTextBoxCertificateCode.Text.Trim()));
            }
           
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        //格式化工种，有增项的需要显示
        protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item.ItemType == GridItemType.Item || e.Item.ItemType == GridItemType.AlternatingItem)
            {
                if(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AddPostID"].ToString()=="9")
                    e.Item.Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text = "安装,增土建";
                else if (RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["AddPostID"].ToString() == "12")
                    e.Item.Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("PostName").OrderIndex].Text = "土建,增安装";
            }
        }


    }
}
