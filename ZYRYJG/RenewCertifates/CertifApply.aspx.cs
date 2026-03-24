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
using System.Threading;

namespace ZYRYJG.RenewCertifates
{
    //允许续期证书类型：三类人，特种作业，造价员(造价员不用选具体的初审单位)
    //三类人员证书续期
    //1、凡是外地进京企业，初审单位只能选择： 区县建委——外师培训中心。
    //2、凡是起重机械租赁企业，初审单位只能选择： 区县建委——特种作业续期初审机构。

    public partial class CertifApply : BasePage
    {
        protected override void OnInit(EventArgs e)
        {
            PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("3").Remove();//屏蔽造价员类别
            PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("4").Remove();//屏蔽职业技能类别
            PostSelect1.RadComboBoxPostTypeID.Items.FindItemByValue("5").Remove();//屏蔽专业管理人员类别

            Dictionary<string, string> postFilterString = new Dictionary<string, string>();
            postFilterString.Add("5", "PostID=55");//专业管理人员允许续期工种ID：55=拆迁员

            PostSelect1.PostFilterString = postFilterString;
            base.OnInit(e);
        }

      
        /// <summary>
        /// 判断证书是否在续期允许时间内
        /// </summary>
        /// <param name="EndValidDate">证书有效截止时间</param>
        /// <returns></returns>
        protected bool IfCanApply(DateTime ValidEndDate)
        {
            if (ValidEndDate.AddDays(-90) <DateTime.Now && ValidEndDate > DateTime.Now)   
            {
                return true;
            }
            else
            {
                return false;//没到续期时间
            }
        }


        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //续期开放时间段提醒
          
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append("提示：未到续期申请开放时间的将无法申请续期，目前开放时间如下，如有变化另行通知，<b> 如有其它疑问请阅读本页右上角《续期须知》</b><br /><br />");
                sb.Append("<b> 特别说明：</b><br />");
                sb.Append("1、安管人员安全生产考核证书延期复核中要求的继续教育培训由初审单位组织（咨询电话见各区住建委对外办公电话）；<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;年度安全生产教育培训有聘用单位自行组织，分三个年度在线填写。<br /><br />");
                sb.Append("2、特种作业操作资格证书延期复核中要求的继续教育培训持证人在“公益教育培训”栏目自行完成；<br />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;年度安全生产教育培训由聘用单位组织。<br /> <br />");
                sb.Append("<b> 续期时间：</b><br />");
                sb.Append("证书有效期截止日期前90天的当日至有效期截止日期当日。<br />");
                sb.Append("企业确认的截止期限为证书有效期截止日期当日。<br />");
               
                //sb.Append("<br />造价员：根据有关规定，停止造价员考核、变更、续期业务。");

                //sb.Append("<br />受疫情影响，未能在2020年6月30日前提交证书续期申请的特种作业证书（2020-06-30过期）、未能在2020年12月31日前提交证书续期申请的三类人员证书和特种作业证书（2020-12-31过期），可于2021年2月28日前继续提交证书延续申请，在此期间证书可正常使用。其他2021年即将过期的证书续期时间待定。");
                
               
                ViewState["Help"] = sb.ToString();
                UIHelp.layerAlert(Page, sb.ToString());

                if (string.IsNullOrEmpty(Request["o"]) == false)
                {
                    ViewState["PostTypeID"] = Request["o"];
                    switch (Request["o"].ToString())
                    {
                        case "1":
                            LabelApply.Text = "三类人员";
                            break;
                        case "2":
                            LabelApply.Text = "特种作业";
                            break;
                        case "3":
                            LabelApply.Text = "造价员";
                            break;
                    }
                    PostSelect1.PostTypeID = Request["o"].ToString();
                    PostSelect1_OnPostTypeSelectChange(sender, null);
                }
                else
                {
                    LabelRoad.Text = "业务办理";
                    LabelApply.Text = "申请证书续期";
                }
                switch (PersonType)
                {
                    case 1://管理者
                    case 6:
                        PostSelect1.LockPostTypeID();
                        break;
                    case 2://个人
                        TrUnit.Visible = false;
                        TrPerson.Visible = false;
                        WorkerOB _WorkerOB = WorkerDAL.GetObject(PersonID);
                        RadTextBoxWorkerCertificateCode.Text = _WorkerOB.CertificateCode;
                        tableSearch.Visible = false;
                        break;
                    case 3://企业
                        TrUnit.Visible = false;
                        RadTextBoxUnitCode.Text = ZZJGDM;//企业查询与自己组织机构代码相同的证书   

                        string UnitName = UnitDAL.GetUnitNameByCodeFromQY_BWDZZZS(RadTextBoxUnitCode.Text.Trim(), true);
                        if (string.IsNullOrEmpty(UnitName))
                        {
                            ViewState["QYZZ"] = -1;//无本地企业资质
                        }
                        else if (UnitName != PersonName)
                        {
                            ViewState["QYZZ"] = -2;//企业名称与资质证书上名称不符 
                            ViewState["QYZZ_UnitName"] = UnitName;
                        }
                        else
                        {
                            ViewState["QYZZ"] = 1;     //企业资质符合三类人要求                       
                        }
                        break;
                }

                ButtonSearch_Click(sender, e);

                ////*****************  临时添加飘窗****************************
                //if (DateTime.Now < Convert.ToDateTime("2022-09-30"))
                //{
                //    ClientScript.RegisterStartupScript(this.GetType(), "message", " FloatAd('#floadAD');", true);
                //}

                ////******************************************************
            }            
        }


        protected void PostSelect1_OnPostTypeSelectChange(object source, EventArgs e)
        {
            ButtonSearch_Click(source, e);
        }

        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns>查询参数类</returns>
        private QueryParamOB GetQueryParamOB()
        {
            QueryParamOB q = new QueryParamOB();

            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            switch (RadioButtonListApplyStatus.SelectedValue)
            {
                case "0"://应续期                  

                    //证书有效期截止日期前90天的当日至有效期截止日期当日
                    q.Add("DATEADD(day,-90, validenddate) <getdate() and validenddate > dateadd(day,-1,getdate())");

                    q.Add(string.Format("(CertificateContinueID =0 or ConfirmResult='{0}' or ApplyStatus='{1}' or ApplyStatus='{2}')"
                        , EnumManager.CertificateContinueCheckResult.DecidNoPass
                        , EnumManager.CertificateContinueStatus.NewSave
                        , EnumManager.CertificateContinueStatus.SendBack));
                  
                    break;
                case "1"://已申请（在办中）
                    q.Add(string.Format("CertificateContinueID >0 and (ApplyStatus='{0}' or (ApplyStatus='{1}' and GetResult='{3}') or ApplyStatus='{2}' or ApplyStatus='{4}')"
                        , EnumManager.CertificateContinueStatus.Applyed
                        , EnumManager.CertificateContinueStatus.Accepted
                        , EnumManager.CertificateContinueStatus.Checked
                        , EnumManager.CertificateContinueCheckResult.GetPass
                         , EnumManager.CertificateContinueStatus.WaitUnitCheck));
                    break;
                default:
                    //证书有效期截止日期前90天的当日至有效期截止日期当日
                    q.Add("DATEADD(day,-90, validenddate) <getdate() and validenddate > dateadd(day,-1,getdate())");
                    break;
            }

            if (RadTextBoxCertificateCode.Text.Trim() != "")
            {
                //证书编号
                q.Add(string.Format("CERTIFICATECODE like '%{0}%'", RadTextBoxCertificateCode.Text.Trim()));
            }
            if (RadTextBoxWorkerName.Text.Trim() != "")
            {
                //证书人姓名
                q.Add(string.Format("WorkerName like '%{0}%'", RadTextBoxWorkerName.Text.Trim()));
            }
            if (PostSelect1.PostTypeID != "")
            {
                //岗位
                q.Add(string.Format("PostTypeID >= {0} and PostTypeID <= {0} ", PostSelect1.PostTypeID));
            }
            else
            {
                q.Add("PostTypeID <3");
            }

            //工种类别
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
            if (RadTextBoxWorkerCertificateCode.Text.Trim() != "")
            {
                //证件号码
                q.Add(string.Format("WorkerCertificateCode like '{0}%'", RadTextBoxWorkerCertificateCode.Text.Trim()));
            }
            if (RadTextBoxUnitName.Text.Trim() != "") //所在单位
            {
                q.Add(string.Format("UnitName like '%{0}%'", RadTextBoxUnitName.Text.Trim()));
            }
            if (RadTextBoxUnitCode.Text.Trim() != "")//企业组织机构代码
            {
                q.Add(string.Format("UnitCode='{0}'", RadTextBoxUnitCode.Text.Trim()));
            }
            if (RadTextBoxApplyCode.Text.Trim() != "")//申请批次号
            {
                q.Add(string.Format("ApplyCode='{0}'", RadTextBoxApplyCode.Text.Trim()));
            }
            //证书有效期至
            if (!txtValidEndtDate.IsEmpty)
            {
                q.Add(string.Format("ValidEndDate ='{0}'", txtValidEndtDate.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            return q;
        }

        //根据条件查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();

            QueryParamOB q = GetQueryParamOB();
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridApply.CurrentPageIndex = 0;
            RadGridApply.DataSourceID = ObjectDataSource1.ID;
        }

    }
}