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
namespace ZYRYJG.EXamManage
{
    public partial class CertifRegisty : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
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
            ClearGridSelectedKeys(RadGridExamResult);
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            q.Add(string.Format("Status = '{0}'", EnumManager.CertificateUpdateType.WaitCheck));//待审批
            if(ExamPlanSelect1.ExamPlanID.HasValue ==true) q.Add("ExamPlanID=" + ExamPlanSelect1.ExamPlanID.ToString());// 考试计划
            if (RadTextBoxWorkerName.Text.Trim() != "") q.Add(string.Format("WorkerName like '%{0}%'", RadTextBoxWorkerName.Text.Trim()));// 考生姓名
            if (RadTextBoxWorkerCertificateCode.Text.Trim() != "") q.Add(string.Format("WorkerCertificateCode like '%{0}%'", RadTextBoxWorkerCertificateCode.Text.Trim()));// 企业名称

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridExamResult.CurrentPageIndex = 0;
            RadGridExamResult.DataSourceID = ObjectDataSource1.ID;
            GridSortExpression sortStr1 = new GridSortExpression();
            sortStr1.FieldName = "CertificateCode";
            sortStr1.SortOrder = GridSortOrder.Ascending;
            RadGridExamResult.MasterTableView.SortExpressions.AddSortExpression(sortStr1);
            ViewState["whereString"] = q.ToWhereString();
        }

        //Grid换页
        protected void RadGridExamResult_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGridExamResult, "CertificateID");
        }

        //Grid绑定勾选checkbox状态
        protected void RadGridExamResult_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGridExamResult, "CertificateID");

            if (RadGridExamResult.MasterTableView.Items.Count == 0)
            {
                ButtonCheck.Visible = false;
                ButtonNoCheck.Visible = false;
            }
            else
            {
                ButtonCheck.Visible = true;
                ButtonNoCheck.Visible = true;
            }
        }

        //审批通过
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridExamResult, "CertificateID");
            if (IsGridSelected(RadGridExamResult) == false)
            {
                UIHelp.layerAlert(Page, "你还没有选择证书！");
                return;
            }

            string filterString = "";//过滤条件

            if (GetGridIfCheckAll(RadGridExamResult) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGridExamResult) == true)//排除
                    filterString = string.Format(" {0} and CertificateID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridExamResult));
                else//包含
                    filterString = string.Format(" {0} and CertificateID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridExamResult));
            }

            CertificateOB ceron = new CertificateOB();
            
            ceron.CheckMan = PersonName;//审批人
            ceron.CheckAdvise = "通过";//审批结果
            ceron.CheckDate = DateTime.Now;//审批时间
            ceron.Status = EnumManager.CertificateUpdateType.first;//证书的更新状态
            ceron.ModifyTime = DateTime.Now;//修改时间
            ceron.ModifyPersonID = PersonID;//修改人
           
            int count=0;
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                count = CertificateDAL.SelectCount(filterString);
                CertificateDAL.UpdateExamSignup_ResultCertificatecode(tran, filterString);//更新报名表,写入证书编号
                CertificateDAL.Check(tran,ceron, filterString);//更新证书表
                tran.Commit();                
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "证书审批失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "审批证书", string.Format("审批证书数量：{0}本。{1}",count.ToString(),ExamPlanSelect1.ExamPlanName));

            UIHelp.layerAlert(Page, "证书审批成功！",6,3000);
            ClearGridSelectedKeys(RadGridExamResult);
            RadGridExamResult.DataBind();

        }

        //审批不通过
        protected void ButtonNoCheck_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridExamResult, "CertificateID");
            if (IsGridSelected(RadGridExamResult) == false)
            {
                UIHelp.layerAlert(Page, "你还没有选择证书！");
                return;
            }

            string filterString = "";//过滤条件

            if (GetGridIfCheckAll(RadGridExamResult) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGridExamResult) == true)//排除
                    filterString = string.Format(" {0} and CertificateID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridExamResult));
                else//包含
                    filterString = string.Format(" {0} and CertificateID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridExamResult));
            }

            int count = 0;
            try
            {
                count = CertificateDAL.SelectCount(filterString);
                CertificateDAL.NoCheck(null,filterString);//删除证书表                      
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "证书审批失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(PersonName, UserID, "审批证书", string.Format("审批不通过证书数量：{0}本。{1}", count.ToString(), ExamPlanSelect1.ExamPlanName));

            UIHelp.layerAlert(Page, "证书审批成功！",6,3000);
            ClearGridSelectedKeys(RadGridExamResult);
            RadGridExamResult.DataBind();
        }

    }
}
