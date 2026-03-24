using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;
using DataAccess;

namespace ZYRYJG.CertifManage
{
    public partial class CertifMoreApplyList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //1、个人或待增发单位发起申请，填写申请单。
            //    系统比对个人是否存在有效A本
            //        比对增发单位法人是否与该A本姓名一致（不必对身份证，拿不到）


            if (!IsPostBack)
            {
                //个人登录后
                if (IfExistRoleID("0") == true)
                {
                    div_New.Visible = true;
                }

                //Response.Redirect("~/ResultInfoPage.aspx?o=A本增发业务正在升级调整，调整期暂时无法提供服务，请耐心等待！", true);
                //return;
                if (PersonType == 2 || PersonType == 3)
                {
                    tbCX.Visible = false;
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
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            switch (PersonType)
            {
                case 2://考生
                    WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);
                    q.Add(string.Format("(WorkerCertificateCode='{0}' )", _WorkerOB.CertificateCode));
                    break;
                case 3://企业

                    q.Add(string.Format("(UnitCodeMore='{0}' )", ZZJGDM));
                    break;
                default:
                    q.Add(string.Format("[CreatePerson]={0}", PersonName.ToString()));//创建者
                    break;

            }


            //身份证号码
            if (RadTxtSFZHM.Text.Trim() != "")
            {
                q.Add(string.Format("WorkerCertificateCode = '{0}'", RadTxtSFZHM.Text.Trim()));
            }

            //证书号码
            if (RadTxtWorkerName.Text != "")
            {
                q.Add(string.Format("WorkerName = '{0}'", RadTxtWorkerName.Text.Trim()));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        protected void BindGrid()
        {
            
        }

        //删除无效申请
        //protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        //{
        //    //获取类型Id
        //    Int64 _ApplyID = Convert.ToInt64(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ApplyID"]);
        //    try
        //    {
        //        CertificateEnterApplyDAL.Delete(_ApplyID);
        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "删除证书进京申请信息失败！", ex);
        //        return;
        //    }
        //    UIHelp.WriteOperateLog(PersonName, UserID, "受理证书进京", string.Format("处理结果：删除。证书编号：{0}。"
        //    , e.Item.Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("CertificateCode").OrderIndex].Text));

        //    UIHelp.layerAlert(Page, "删除成功！", 6, 3000);
        //}

        ////Grid换页
        //protected void RadGrid1_PageIndexChanged(object source, GridPageChangedEventArgs e)
        //{
        //    UpdateGridSelectedKeys(RadGrid1, "ApplyID");
        //}

        ////Grid绑定勾选checkbox状态
        //protected void RadGrid1_DataBound(object sender, EventArgs e)
        //{
        //    UpdateGriSelectedStatus(RadGrid1, "ApplyID");
        //}
    }
}
