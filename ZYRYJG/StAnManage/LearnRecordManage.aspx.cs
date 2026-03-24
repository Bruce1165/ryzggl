using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using System.Data;
using System.IO;
using Model;
using DataAccess;

namespace ZYRYJG.StAnManage
{
    public partial class LearnRecordManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshGrid(0);
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
            RefreshGrid(0);
        }

        //分页
        protected void RadGridExamSubjectResult_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            RefreshGrid(e.NewPageIndex);
        }

        //导入
        protected void ButtonImportScore_Click(object sender, EventArgs e)
        {
            if (RadUploadSignUpTable.UploadedFiles.Count > 0)
            {
                //上传excel
                string targetFolder = Server.MapPath("~/App_Data/RadUploadTemp/");
                string filePath = Path.Combine(targetFolder, string.Format("signUpBat_{0}_{1}.xls", PersonID.ToString(), DateTime.Now.ToString("yyyyMMddHHmmss")));
                RadUploadSignUpTable.UploadedFiles[0].SaveAs(filePath, true);

                //读入DataSet再校验并保存
                DataSet dsImport = null;

                try
                {
                    dsImport = Utility.ExcelDealHelp.ImportExcell(filePath, "");
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "导入学习表数据失败", ex);
                    UIHelp.layerAlert(Page, "导入学习表数据失败!");
                    return;
                }

                if (dsImport == null || dsImport.Tables.Count == 0 || dsImport.Tables[0].Rows.Count == 0)
                {
                    RefreshGrid(0);
                    UIHelp.layerAlert(Page, "未找到任何数据!");
                    return;
                }
                else
                {
                    if (VaildImportTemplate(dsImport.Tables[0]) == false)
                    {
                        UIHelp.layerAlert(Page, "导入文件格式无效，请按考试计划名称查询后，再下载带有考生信息的模板，录入成绩后按科目分批导入!");
                        return;
                    }
                    SaveImportData(dsImport.Tables[0]);//保存
                }
            }
            else
            {
                RefreshGrid(0);
            }
        }

        //刷新成绩Grid
        protected void RefreshGrid(int pageIndex)
        {
            QueryParamOB queryExamResult = new QueryParamOB();
            if (this.RadTextBoxCertificateCode.Text.Trim() != "") queryExamResult.Add(string.Format("CertificateCode like '%{0}%'", this.RadTextBoxCertificateCode.Text.Trim()));
            if (this.RadTextBoxWorkerCertificateCode.Text.Trim() != "") queryExamResult.Add(string.Format("WorkerCertificateCode like '%{0}%'", this.RadTextBoxWorkerCertificateCode.Text.Trim()));
            if (this.RadTextBoxWorkerName.Text.Trim() != "") queryExamResult.Add(string.Format("WorkerName like '%{0}%'", this.RadTextBoxWorkerName.Text.Trim()));

            int rowCount = LearnRecordDAL.SelectCountView(queryExamResult.ToWhereString());
            string sortString = "";
            DataTable dtMain = LearnRecordDAL.GetListView(pageIndex * RadGridExamSubjectResult.PageSize, RadGridExamSubjectResult.PageSize, queryExamResult.ToWhereString(), sortString);

            //绑定数据
            RadGridExamSubjectResult.VirtualItemCount = rowCount;
            RadGridExamSubjectResult.CurrentPageIndex = pageIndex;
            RadGridExamSubjectResult.DataSource = dtMain;
            RadGridExamSubjectResult.DataBind();
        }

        //保存成绩信息
        protected void SaveImportData(DataTable dt)
        {
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();

            try
            {
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    LearnRecordOB learnRecordOB = new LearnRecordOB();
                    learnRecordOB.RecordNo = dt.Rows[m]["编号"].ToString();
                    learnRecordOB.PostName = dt.Rows[m]["专业"].ToString();
                    learnRecordOB.WorkerName = dt.Rows[m]["姓名"].ToString();
                    learnRecordOB.WorkerCertificateCode = dt.Rows[m]["证件号码"].ToString();
                    learnRecordOB.LinkTel = dt.Rows[m]["联系手机"].ToString();
                    learnRecordOB.CertificateCode = dt.Rows[m]["证书注册号"].ToString();
                    learnRecordOB.ClassHour = dt.Rows[m]["学时"].ToString();

                    int Rc = CommonDAL.SelectRowCount("dbo.LearnRecord", string.Format(" and CertificateCode='{0}'", learnRecordOB.CertificateCode));
                    if (Rc > 0)
                    {
                        LearnRecordDAL.Update(tran, learnRecordOB);
                    }
                    else
                    {
                        LearnRecordDAL.Insert(tran, learnRecordOB);
                    }
                }
                tran.Commit();

                UIHelp.layerAlert(Page, "学时导入成功！",6,3000);
                RefreshGrid(0);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "学时导入失败！", ex);
                return;
            }
        }

        //验证成绩导入母板
        protected bool VaildImportTemplate(DataTable dt)
        {
            if (dt.Columns.Count < 8) return false;//模版列：行号, 编号, 专业, 姓名, 证件号码, 联系手机, 证书注册号, 学时

            string[] colNames = new string[8] { "行号", "编号", "专业", "姓名", "证件号码", "联系手机", "证书注册号", "学时" };
            for (int i = 0; i < 8; i++)
            {
                if (dt.Columns[i].ColumnName != colNames[i]) return false;
            }

            return true;
        }

    }
}