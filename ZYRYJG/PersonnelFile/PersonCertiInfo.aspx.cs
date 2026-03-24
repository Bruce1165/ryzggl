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
using System.IO;

namespace ZYRYJG.PersonnelFile
{
    public partial class PersonCertiInfo : BasePage
    {
        /// <summary>
        /// 下载pdf传参验证
        /// </summary>
        /// <returns></returns>
        protected string GedReadParam()
        {
            return Server.UrlEncode( Utility.Cryptography.Encrypt(string.Format("{0},{1},{2},{3}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "synergyFileAccess",UserName,UserID)));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ValidResourceIDLimit(RoleIDs, "OutputCertificate") == false)//批量导出权限
                {
                    DivButtonOutput.Visible = false;
                }

                //if (ValidResourceIDLimit(RoleIDs, "CertinfoSearch") == true)
                //{
                //    for (int i = 1; i <= 5; i++)
                //    {
                //        if (ValidResourceIDLimit(RoleIDs, string.Format("CertinfoSearch_Type{0}", i.ToString())) == false)
                //        {
                //            PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue(i.ToString()).Remove();//屏蔽证书类别
                //        }
                //    }
                //}

                if(string.IsNullOrEmpty(Request["w"])==false)
                {
                    string _workercertificatecodre = Utility.Cryptography.Decrypt(Request["w"]);
                    RadTextBoxWorkerCertificateCode.Text = _workercertificatecodre;
                }
                if (string.IsNullOrEmpty(Request["p"]) == false)
                {
                    string _posttypeid = Utility.Cryptography.Decrypt(Request["p"]);
                    PostSelect1.PostTypeID = _posttypeid;
                }
                


                ButtonSearch_Click(sender, e);
            }
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>查询参数类</returns>
        private QueryParamOB GetQueryParamOB()
        {
            QueryParamOB q = new QueryParamOB();
            if (RadTextBoxCertificateCode.Text.Trim() != "")
            {
                q.Add(string.Format("CERTIFICATECODE like '{0}%'", RadTextBoxCertificateCode.Text.Trim()));
            }
            if (RadTextBoxWorkerName.Text.Trim() != "")
            {
                q.Add(string.Format("WorkerName like '{0}%'", RadTextBoxWorkerName.Text.Trim()));
            }
            if (PostSelect1.PostTypeID != "")
            {
                q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));
            }
            else if (PostSelect1.RadComboBoxPostTypeID.Items.Count < 6)//如果不是有5类证书查询权限，“全部”需要排除无权限类别
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (RadComboBoxItem it in PostSelect1.RadComboBoxPostTypeID.Items)
                {
                    if (it.Value != "")
                    {
                        sb.Append("or PostTypeID = ").Append(it.Value);
                    }
                }
                if (sb.Length > 0)
                {
                    sb.Remove(0, 2);
                    q.Add(string.Format("({0})", sb.ToString()));
                }
            }
            if (PostSelect1.PostID != "")
            {
                switch (PostSelect1.PostID)
                {
                    case "9"://土建
                        q.Add(string.Format("((PostID >= {0} and PostID <= {0}) or PostName like '%增土建')", PostSelect1.PostID));
                        break;
                    case "12"://安装
                        q.Add(string.Format("((PostID >= {0} and PostID <= {0}) or PostName like '%增安装')", PostSelect1.PostID));
                        break;
                    default:
                        q.Add(string.Format("PostID >= {0} and PostID <= {0}", PostSelect1.PostID));
                        break;
                }
            }
            if (PostSelect1.PostTypeID == "4" )//职业技能列表及导出添加“技术等级”显示列
            {
                RadGrid1.Columns.FindByUniqueName("SkillLevel").Visible = true;
                RadGrid1.Columns.FindByUniqueName("UnitName").Visible = true;
            }
            else if (PostSelect1.PostTypeID == "4000")//习新版职业技能
            {
                RadGrid1.Columns.FindByUniqueName("SkillLevel").Visible = true;
                RadGrid1.Columns.FindByUniqueName("UnitName").Visible = false;
            }
            else
            {
                RadGrid1.Columns.FindByUniqueName("SkillLevel").Visible = false;
                RadGrid1.Columns.FindByUniqueName("UnitName").Visible = true;
            }
            if (RadTextBoxWorkerCertificateCode.Text.Trim() != "")
            {
                q.Add(string.Format("WorkerCertificateCode like '{0}%'", RadTextBoxWorkerCertificateCode.Text.Trim()));
            }
            if (RadTextBoxUnitName.Text.Trim() != "")
            {
                q.Add(string.Format("UnitName like '{0}%'", RadTextBoxUnitName.Text.Trim()));
            }
            if (RadTextBoxUnitCode.Text.Trim() != "")
            {
                q.Add(string.Format("UnitCode like '{0}%'", RadTextBoxUnitCode.Text.Trim()));
            }
            //发证年度
            if (RadNumericTextBoxConferData.Value.HasValue == true)
            {
                q.Add(string.Format("DATEPART(year,conferdate)={0}", RadNumericTextBoxConferData.Value));
            }
            //证书有效期
            if (!RadDatePickerFrom.IsEmpty)
            {
                q.Add(string.Format("ValidEndDate >='{0}'", RadDatePickerFrom.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (!RadDatePickerEnd.IsEmpty)
            {
                q.Add(string.Format("ValidEndDate <='{0} 23:59:59'", RadDatePickerEnd.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            switch (RadComboBoxStatus.SelectedValue)//状态
            {
                case "当前有效":
                    q.Add(string.Format("ValidEndDate >='{0}' and ([STATUS] = '首次' or [STATUS] = '续期' or [STATUS] = '进京变更' or [STATUS] = '京内变更' or [STATUS] = '补办')", DateTime.Now.ToString("yyyy-MM-dd")));
                    break;
                case "首次":
                    q.Add(string.Format("ValidEndDate >='{0}' and [STATUS] = '首次'", DateTime.Now.ToString("yyyy-MM-dd")));
                    break;
                case "续期":
                    q.Add(string.Format("ValidEndDate >='{0}' and [STATUS] = '续期'", DateTime.Now.ToString("yyyy-MM-dd")));
                    break;
                case "京内变更":
                    q.Add(string.Format("ValidEndDate >='{0}' and [STATUS] = '京内变更'", DateTime.Now.ToString("yyyy-MM-dd")));
                    break;
                case "进京变更":
                    q.Add(string.Format("ValidEndDate >='{0}' and [STATUS] = '进京变更'", DateTime.Now.ToString("yyyy-MM-dd")));
                    break;
                case "补办":
                    q.Add(string.Format("ValidEndDate >='{0}' and [STATUS] = '补办'", DateTime.Now.ToString("yyyy-MM-dd")));
                    break;
                case "已过期":
                    q.Add(string.Format("ValidEndDate <'{0}'", DateTime.Now.ToString("yyyy-MM-dd")));
                    break;
                case "离京变更":
                    q.Add(string.Format("[STATUS] = '{0}'", "离京变更"));
                    break;
                case "注销":
                    q.Add(string.Format("[STATUS] = '{0}'", "注销"));
                    break;
                case "超龄注销":
                    q.Add(string.Format("[STATUS] = '{0}' and remark like '%超龄注销%'", "注销"));
                    break;
                case "待审批":
                    q.Add("([STATUS] = '待审批' or [STATUS] = '进京待审批' )");
                    break;

            }

            if (RadioButtonListLockStatus.SelectedValue == "锁定中")
            {
                q.Add(string.Format("CertificateID in (select CertificateID from dbo.CertificateLock where LockStatus='加锁' and LockEndTime >'{0}')", DateTime.Now.ToString("yyyy-MM-dd")));
            }
            else if (RadioButtonListLockStatus.SelectedValue == "未锁定")
            {
                q.Add(string.Format("CertificateID not in (select CertificateID from dbo.CertificateLock where LockStatus='加锁' and LockEndTime >'{0}')", DateTime.Now.ToString("yyyy-MM-dd")));
            }

            //打印时间
            if (RadDatePicker_PrintStartDate.SelectedDate.HasValue || RadDatePicker_PrintEndDate.SelectedDate.HasValue)
            {
                q.Add(string.Format("( PrintDate BETWEEN  '{0}' AND '{1}')"
                    , RadDatePicker_PrintStartDate.SelectedDate.HasValue ? RadDatePicker_PrintStartDate.SelectedDate.Value.ToString("yyyy-MM-dd") : DateTime.MinValue.AddDays(1).ToString()
                    , RadDatePicker_PrintEndDate.SelectedDate.HasValue ? RadDatePicker_PrintEndDate.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59") : DateTime.MaxValue.AddDays(-1).ToString()));
            }



            //电子证书生成时间
            if (RadDatePickerDZZSBegin.SelectedDate.HasValue || RadDatePickerDZZSEnd.SelectedDate.HasValue)
            {
                q.Add(string.Format("( ReturnCATime BETWEEN  '{0}' AND '{1}')"
                    , RadDatePickerDZZSBegin.SelectedDate.HasValue ? RadDatePickerDZZSBegin.SelectedDate.Value.ToString("yyyy-MM-dd") : DateTime.MinValue.AddDays(1).ToString()
                    , RadDatePickerDZZSEnd.SelectedDate.HasValue ? RadDatePickerDZZSEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59") : DateTime.MaxValue.AddDays(-1).ToString()));
            }
            if (CheckBoxOnlyShowDZZS.Checked == true)
            {
                q.Add(" ReturnCATime >  '2010-01-01'");
            }

            return q;
        }

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if(UIHelp.CheckSQLParam()==false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = GetQueryParamOB();

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        //导出csv
        protected void ButtonExportToExcel_Click(object sender, EventArgs e)
        {
            QueryParamOB q = GetQueryParamOB();

            string saveFile = string.Format("~/UpLoad/CertifEnterApply/证书信息_{0}{1}.xls", PersonID,DateTime.Now.ToString("yyyyMMddHHmmss"));//保存文件名
            try
            {
                //检查临时目录
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));

                string colHead = "";
                string colName = "";

                //导出数据到数据库服务器
                if (PostSelect1.PostTypeID == "4" || PostSelect1.PostTypeID == "4000")//职业技能列表及导出添加“技术等级”显示列
                {
                    colHead = @"姓名\证件号码\单位名称\组织机构代码\岗位类别\岗位工种\等级\证书编号\发证时间\有效期至\证书最后状态";
                    colName = @"WorkerName\WorkerCertificateCode\UnitName\UnitCode\POSTTYPENAME\POSTNAME\SkillLevel\CERTIFICATECODE\CONVERT(varchar(10), CONFERDATE, 20)\CONVERT(varchar(10), VALIDENDDATE, 20)\STATUS";

                }
                else
                {
                    colHead = @"姓名\证件号码\单位名称\组织机构代码\岗位类别\岗位工种\证书编号\发证时间\有效期至\证书最后状态";
                    colName = @"WorkerName\WorkerCertificateCode\UnitName\UnitCode\POSTTYPENAME\POSTNAME\CERTIFICATECODE\CONVERT(varchar(10), ConferDate, 20)\CONVERT(varchar(10), VALIDENDDATE, 20)\STATUS";
                }

                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1), "DBO.CERTIFICATE", q.ToWhereString(), "CertificateID", colHead, colName);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出从业人员证书信息失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("证书信息下载", saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }
    }
}