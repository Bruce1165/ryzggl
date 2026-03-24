using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using Model;
using DataAccess;
using System.Data;
using Utility;
using System.IO;

namespace ZYRYJG.EXamManage
{
    public partial class PayConfirm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                for (int i = 2010; i <= (DateTime.Now.Year + 1); i++)
                {
                    RadComboBoxYear.Items.Insert(0, new RadComboBoxItem(i.ToString(), i.ToString()));
                }
                RadComboBoxYear.Items.Insert(0, new RadComboBoxItem("全部", ""));

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

            string _ExamPlanID = ExamPlanSelect1.ExamPlanID.HasValue ? ExamPlanSelect1.ExamPlanID.ToString() : "0";//考试计划
            if (_ExamPlanID != "0")
            {
                q.Add("ExamPlanID=" + _ExamPlanID);
            }
           

            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxItem.SelectedValue, RadTextBoxValue.Text.Trim()));
            }

            ////缴费通知批次号
            //if (RadTextBoxPayNoticeCode.Text != "")
            //{
            //    q.Add(string.Format("PayNoticeCode like '%{0}%'", RadTextBoxPayNoticeCode.Text.Trim()));
            //}
            ////报名批次号
            //if (RadTextBoxSignUpCode.Text.Trim() != "")
            //{
            //    q.Add(string.Format("SignUpCode='{0}'", RadTextBoxSignUpCode.Text.Trim()));
            //}
            ////培训点名称
            //if (RadTextBoxTrainUnitID.Text != "")
            //{
            //    q.Add(string.Format("TRAINUNITNAME like '%{0}%'", RadTextBoxTrainUnitID.Text.Trim()));
            //}
            //// 单位名称
            //if (RadTxtUnitName.Text != "")
            //{
            //    q.Add(string.Format("UnitName like '%{0}%'", RadTxtUnitName.Text.Trim()));
            //}
            ////姓名
            //if (RadTxtWorkerName.Text != "")
            //{
            //    q.Add(string.Format("WorkerName like '%{0}%'", RadTxtWorkerName.Text.Trim()));
            //}
            ////证件号码
            //if (RadTxtCertificateCode.Text != "")
            //{
            //    q.Add(string.Format("CertificateCode like '%{0}%'", RadTxtCertificateCode.Text.Trim()));
            //}

            //岗位工种
            if (PostSelect2.PostID != "")
                q.Add(string.Format("PostID = {0}", PostSelect2.PostID));
            else if (PostSelect2.PostTypeID != "")
                q.Add(string.Format("PostTypeID = {0}", PostSelect2.PostTypeID));

            //考试时间
            if (RadComboBoxYear.SelectedValue != "") q.Add(string.Format("DATEPART(year,ExamStartDate) = {0}", RadComboBoxYear.SelectedValue));//年
            if (RadComboBoxMonth.SelectedValue != "") q.Add(string.Format("DATEPART(month,ExamStartDate) = {0}", RadComboBoxMonth.SelectedValue));//月

            //if (RadioButtonListApplyStatus.SelectedValue == "未确认")
            //{
                q.Add(string.Format("LATESTCHECKDATE >='{0}'", DateTime.Now.ToString("yyyy-MM-dd")));//审核截止前

                //状态
                q.Add(string.Format("(Status ='{0}' or Status ='{1}')", EnumManager.SignUpStatus.Checked, EnumManager.SignUpStatus.PayNoticed));

                //没有发放准考证
                q.Add(@"ExamPlanID in(select p.ExamPlanID from dbo.examplan p 
                    left join (select distinct ExamPlanID from dbo.ExamResult) t  
                    on p.ExamPlanID = t.ExamPlanID where t.ExamPlanID is null)");
            //}
            //else//已确认
            //{
            //    //状态
            //    q.Add(string.Format("(Status ='{0}')", EnumManager.SignUpStatus.PayConfirmed));
            //}

            //考试方式
            if (RadComboBoxExamWay.SelectedValue != "")
            {
                q.Add(string.Format("ExamWay ='{0}'", RadComboBoxExamWay.SelectedValue));
            }

            RadGrid1.MasterTableView.SortExpressions.Clear();
            GridSortExpression sortStr1 = new GridSortExpression();
            sortStr1.FieldName = "ZACheckResult,ExamSignUpID desc";
            RadGrid1.MasterTableView.SortExpressions.AddSortExpression(sortStr1);

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        //Grid换页
        protected void RadGridAccept_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "ExamSignUpID");
        }

        //Grid绑定勾选checkbox状态
        protected void RadGridAccept_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGrid1, "ExamSignUpID");
        }

        //审核确认
        protected void ButtonComfirm_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "ExamSignUpID");
            if (!IsGridSelected(RadGrid1))
            {
                UIHelp.layerAlert(Page, "你还没有选择数据！");
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
                    filterString = string.Format(" {0} and ExamSignUpID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
                else//包含
                    filterString = string.Format(" {0} and ExamSignUpID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
            }
            
            int count = ExamSignUpDAL.SelectCount_New(filterString);//记录数

            ExamSignUpOB esob = new ExamSignUpOB();
            esob.PayConfirmDate = DateTime.Now;
            esob.PayConfirmMan = PersonName;
            esob.PayConfirmRult = "通过";
            esob.Status = EnumManager.SignUpStatus.PayConfirmed;
            esob.ModifyPersonID = PersonID;
            esob.ModifyTime = esob.PayConfirmDate;

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                esob.PayConfirmCode = UIHelp.GetNextBatchNumber(tran, "JFQR");//缴费确认编号
                ExamSignUpDAL.PayConfirm(tran, esob, string.Format(" and ExamSignUpID in (select ExamSignUpID from dbo.VIEW_EXAMSIGNUP_NEW where 1=1 {0})", filterString));
                tran.Commit();
                UIHelp.WriteOperateLog(PersonName, UserID, "报名审核通过确认", string.Format("确认批次号：{0}，确认人数{1}人。", esob.PayConfirmCode, count.ToString()));  
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "报名审核通过确认失败！", ex);
                return;
            }
            UIHelp.layerAlert(Page, string.Format("报名审核通过确认成功！确认批次号：{0}，确认人数{1}人。", esob.PayConfirmCode, count.ToString()));  
            ClearGridSelectedKeys(RadGrid1);
            RadGrid1.DataBind();
        }

        //删除
        protected void RadGrid1_DeleteCommand(object source, GridCommandEventArgs e)
        {
            ////获取类型Id
            //Int64 _ExamSignUpID = Convert.ToInt64(RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ExamSignUpID"]);             //Convert.ToInt32(e.CommandArgument);
            //DBHelper db = new DBHelper();
            //DbTransaction tran = db.BeginTransaction();
            //try
            //{
            //    ExamSignUp_DelDAL.Insert(tran, _ExamSignUpID, PersonName, DateTime.Now);
            //    ExamSignUpDAL.Delete(tran, _ExamSignUpID);
            //    tran.Commit();
            //}
            //catch (Exception ex)
            //{
            //    tran.Commit();
            //    UIHelp.WriteErrorLog(Page, "删除报名信息失败！", ex);
            //    return;
            //}
            //UIHelp.WriteOperateLog(PersonName, UserID, "删除报名", string.Format("报名审核通过确认时删除报名。报名批次号：{0}，报名人：{1}，详情见“查看报名情况/查看删除历史”。",
            // RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["SignUpCode"].ToString(),
            // e.Item.Cells[RadGrid1.MasterTableView.Columns.FindByUniqueName("WorkerName").OrderIndex].Text));
            //UIHelp.layerAlert(Page, "删除成功！",6,3000);
        }

        //根据证件号码显示照片
        protected string ShowFaceimage(string CertificateCode, string ExamPlanID)
        {
            System.Random rm = new Random();
            string img = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(ExamPlanID, CertificateCode))); //绑定照片;
            return img;
        }

        /// <summary>
        /// 获取个人照片地址
        /// </summary>
        /// <param name="ExamPlanID">考试计划ID</param>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        public string GetFacePhotoPath(string ExamPlanID, string CertificateCode)
        {
            if (CertificateCode == "") return "~/Images/photo_ry.jpg";
            string path = string.Format("~/UpLoad/SignUpPhoto/{0}/{1}.jpg", ExamPlanID, CertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
                return path;
            else
            {
                path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
                if (File.Exists(Server.MapPath(path)) == true)
                    return path;
                else
                    return "~/Images/tup.gif";
            }
        }
        protected string FormatFirstCheckType(string FirstCheckTypeID)
        {
            switch (FirstCheckTypeID)
            {
                case "-1":

                    return "人工审核";
                case "0":
                    return "人工审核";

                case "1":
                    return "人工审核";

                case "3":
                    return "社保符合";

                case "4":
                    return "法人符合";
                default:
                    return "人工审核";
            }
        }
    }
}
