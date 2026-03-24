using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;

namespace ZYRYJG.EXamManage
{
    public partial class ExamPlanSearch : BasePage
    {
        protected override bool IsNeedLogin
        {
            get
            {
                return false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int i = 2010; i <= (DateTime.Now.Year + 1); i++)
                {
                    RadComboBoxYear.Items.Insert(0, new RadComboBoxItem(i.ToString(), i.ToString()));
                }
                RadComboBoxYear.Items.Insert(0, new RadComboBoxItem("全部", ""));

                if(IfExistRoleID("26")==true)//教研室业务员只处理新版职业技能业务
                {
                    //初始化岗位类别
                    for (int i = 1; i < 6; i++)
                    {
                        PostSelect2.RadComboBoxPostTypeID.Items.FindItemByValue(i.ToString()).Remove();
                    }
                    PostSelect2.RadComboBoxPostTypeID.Items.FindItemByText("请选择").Remove();
                    PostSelect2.PostTypeID = "4000";
                }
                else if (IfExistRoleID("1") == false)//非管理员，屏蔽新版职业技能
                {
                    PostSelect2.RadComboBoxPostTypeID.Items.FindItemByValue("4000").Remove();
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
            //计划名称
            if (RadTextBoxExam_PlanName.Text.Trim() != "") q.Add(string.Format("ExamPlanName like '%{0}%'", RadTextBoxExam_PlanName.Text.Trim()));
            //岗位工种
            if(PostSelect2.PostID !="")
                q.Add(string.Format("PostID = {0}", PostSelect2.PostID));
            else if (PostSelect2.PostTypeID != "")
                q.Add(string.Format("PostTypeID = {0}", PostSelect2.PostTypeID));
            else if (IfExistRoleID("1") == false && IfExistRoleID("6") == false)//非管理员和注册中心领导，不能查询新版职业技能考试计划（除教研室业务员）
            {
                q.Add("PostTypeID < 6");
            }

            //考试时间
            if (RadComboBoxYear.SelectedValue != "") q.Add(string.Format("DATEPART(year,ExamStartDate) = {0}", RadComboBoxYear.SelectedValue));//年
            if (RadComboBoxMonth.SelectedValue != "") q.Add(string.Format("DATEPART(month,ExamStartDate) = {0}", RadComboBoxMonth.SelectedValue));//月

            //考试状态
            if (RadioButtonListStatus.SelectedValue == "1")//未考试
            {
                q.Add(string.Format("ExamStartDate > '{0}'", DateTime.Now.ToString("yyyy-MM-dd")));
            }
            else if (RadioButtonListStatus.SelectedValue == "2")//已考试
            {
                q.Add(string.Format("ExamStartDate <= '{0}'", DateTime.Now.ToString("yyyy-MM-dd")));
            }

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

                    //q.Add(string.Format("(IfPublish='{0}' or IfPublish='{2}' or (IfPublish='{1}' and ExamPlanID in(select ExamPlanID from dbo.ExamPlanForUser where CertificateCode='{3}' or CertificateCode='{4}')))"
                    //    , "完全公开", "部分公开","完全公开培训点受限", IDCard15, IDCard18));

                    q.Add(string.Format("(IfPublish='{0}' or IfPublish='{2}' or (IfPublish='{1}' and ExamPlanID in(select ExamPlanID from dbo.ExamPlanForUser where CertificateCode='{3}')))"
                    , "完全公开", "部分公开", "完全公开培训点受限", WorkerCertificateCode));
                    break;
                case 3://企业
                    q.Add(string.Format("(IfPublish='{0}' or IfPublish='{2}' or (IfPublish='{1}' and ExamPlanID in(select ExamPlanID from dbo.ExamPlanForUser where UnitCode='{3}')))", "完全公开", "部分公开", "完全公开培训点受限", ZZJGDM));
                    break;
                case 4://培训点
                    q.Add(string.Format("(IfPublish='{0}' or ((IfPublish='{1}' or IfPublish='{2}') and ExamPlanID in(select ExamPlanID from dbo.ExamPlanForUser where TrainUnitID={3})))", "完全公开", "部分公开", "完全公开培训点受限", UnitID.ToString()));
                    break;
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridExamPlan.CurrentPageIndex = 0;
            RadGridExamPlan.DataSourceID = ObjectDataSource1.ID;
          
        }

        protected void RadGridExamPlan_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                e.Item.Style.Add("cursor", "pointer");
                e.Item.Attributes.Add("onclick", string.Format("returnToParent('{0}','{1}','{2}','{3}','{4}','{5}')"
                    ,e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ExamPlanID"].ToString()
                    ,e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ExamPlanName"].ToString()
                  
                    , e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PostTypeID"].ToString()
                    , e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PostID"].ToString()
                    , e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PostTypeName"].ToString()
                    , e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["PostName"].ToString()
                    ));
            }
        }
     
    }
}
