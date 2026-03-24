using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;
using DataAccess;
using System.Data;
using System.IO;

namespace ZYRYJG.CheckMgr
{
    public partial class ApplyCheckDetail : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "ApplyCheckMgr.aspx";
            }
        }

        /// <summary>
        /// 抽查任务ID
        /// </summary>
        protected long TaskID
        {
            get { return Convert.ToInt64(ViewState["TaskID"]); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ValidResourceIDLimit(RoleIDs, "ApplyCheckInput") == true)
                {
                    TableEdit.Visible = true;
                }                

                ViewState["TaskID"] = Utility.Cryptography.Decrypt(Request["o"]);

                ApplyCheckTaskMDL o= ApplyCheckTaskDAL.GetObject(TaskID);
                Labelcjsj.Text = o.cjsj.Value.ToString("yyyy-MM-dd");
                RadioButtonListStatus.Items[0].Text = string.Format("全部（{0}）", o.ItemCount);
                ViewState["ItemCount"] = o.ItemCount;
                LabelTimeSpan.Text = string.Format("{0} - {1}", o.BusStartDate.Value.ToString("yyyy.MM.dd"), o.BusEndDate.Value.ToString("yyyy.MM.dd"));
                for(int i=CheckBoxListBusRange.Items.Count -1;i>=0;i--)
                {
                    if(o.BusRangeCode.Contains(CheckBoxListBusRange.Items[i].Value) == false)
                    {
                        CheckBoxListBusRange.Items.RemoveAt(i);
                    }
                }

                BindData();

            }
        }

        //绑定抽查数据列表
        protected void BindData()
        {
            ClearGridSelectedKeys(RadGridApplyCheckItem);
            ObjectDataSource1.SelectParameters.Clear();

            QueryParamOB q = new QueryParamOB();
            q.Add(string.Format("TaskID = {0}",TaskID));

            if (RadioButtonListStatus.SelectedValue != "")
            {
                if (RadioButtonListStatus.SelectedValue == "未审查")
                {
                    q.Add("CheckTime is null");
                }
                else// 已审查
                {
                    q.Add("CheckTime > '2000-01-01'");
                }
            }

            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            bool AllSelect = true;//是否选择了全部证书类别
            foreach (ListItem i in CheckBoxListBusRange.Items)
            {
                if (i.Selected == true)
                {
                    sb.AppendFormat(" or [BusTypeID] = {0}",i.Value);

                }
                else
                {
                    AllSelect = false;
                }
            }
            if (AllSelect == false)
            {
                if (sb.Length == 0)
                {
                    q.Add("2 < 1");
                }
                else
                {
                    q.Add(string.Format("({0})", sb.Remove(0, 3)));
                }
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridApplyCheckItem.CurrentPageIndex = 0;
            RadGridApplyCheckItem.DataSourceID = ObjectDataSource1.ID;


            int checkCount=ApplyCheckTaskDAL.SelectCheckCount(TaskID);
            RadioButtonListStatus.Items[1].Text = string.Format("未审查（{0}）", Convert.ToInt32(ViewState["ItemCount"]) -checkCount);
            RadioButtonListStatus.Items[2].Text = string.Format("已审查（{0}）", checkCount);

        }

        //导出
        protected void ButtonOut_Click(object sender, EventArgs e)
        {
            if (RadGridApplyCheckItem.MasterTableView.VirtualItemCount == 0)
            {
                UIHelp.layerAlert(Page, "没有可导出的数据！");
                return;
            }

            string saveFile = string.Format("~/UpLoad/CertifEnterApply/人员资格业务抽查结果_{0}.xls",DateTime.Now.ToString("yyyyMMddhhmmss"));//保存文件名
            try
            {
                //检查临时目录
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/CertifEnterApply/"));

                //导出数据到数据库服务器              
                string colHead = @"人员姓名\证件号码\证书编号\证书类别\业务类别\业务办结日期\抽查审查时间\审查结果\审查结果说明";
                string colName = @"WorkerName\IDCard\CertificateCode\case [BusTypeID] when 1 then '二建注册建造师' when 2 then '二级注册造价工程师' when 3 then '安全生产管理人员' when 4 then '特种作业人员' end\ApplyType\CONVERT(varchar(10), ApplyFinishTime, 20)\case when CheckTime is null then '' else CONVERT(varchar(10), CheckTime, 20) end\ISNULL(CheckResult,'')\ISNULL(CheckResultDesc,'')";
                CommonDAL.OutputXls(MyWebConfig.DBOutputPath + Server.MapPath(saveFile).Substring(1)
                    , "ApplyCheckTaskItem"
                    , ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, "TaskItemID", colHead, colName);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "导出人员资格业务抽查审核结果失败！", ex);
                return;
            }

            List<ResultUrl> url = new List<ResultUrl>();
            url.Add(new ResultUrl("人员资格业务抽查审核结果下载", saveFile));
            UIHelp.ShowMsgAndRedirect(Page, UIHelp.DownLoadTip, url);
        }

        //变更审核状态
        protected void RadioButtonListStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        /// <summary>
        /// 业务类型格式化
        /// </summary>
        /// <param name="BusTypeID"></param>
        /// <returns></returns>
        protected string GetBusTypeName(int BusTypeID)
        {
            switch (BusTypeID)
            {
                case 1:
                    return "二建注册建造师";
                case 2:
                    return "二级注册造价工程师";
                case 3:
                    return "安全生产管理人员";
                case 4:
                    return "特种作业人员";
                default:
                    return "";
            }
        }

        //获取查看申请单详细链接
        protected string GetUrl(string ApplyType, string BusTypeID, string DataID,string CertificateCode)
        {
            switch (ApplyType)
            {
                case "考试报名":
                    return string.Format("<span class=\"link_edit\" onclick=\"javascript:SetIfrmSrc('../EXamManage/ExamSignView.aspx?o={0}');\">{1}</span>", Server.UrlEncode(Utility.Cryptography.Encrypt(DataID)), CertificateCode);
                case "证书进京":
                    return string.Format("<span class=\"link_edit\" onclick=\"javascript:SetIfrmSrc('../CertifEnter/CertifEnterApplyEdit.aspx?t={0}&o={1}');\">{2}</span>",Server.UrlEncode(Utility.Cryptography.Encrypt("1")), Server.UrlEncode(Utility.Cryptography.Encrypt(DataID)), CertificateCode);
                case "证书续期":
                    return string.Format("<span class=\"link_edit\" onclick=\"javascript:SetIfrmSrc('../RenewCertifates/ApplyDetail.aspx?o2={0}');\">{1}</span>", Server.UrlEncode(Utility.Cryptography.Encrypt(DataID)), CertificateCode);
                case "初始注册":
                    if (BusTypeID == "1")
                        return string.Format("<span class=\"link_edit\" onclick=\"javascript:SetIfrmSrc('../Unit/ApplyFirstAdd.aspx?a={0}&v=1');\">{1}</span>", Server.UrlEncode(Utility.Cryptography.Encrypt(DataID)), CertificateCode);
                    else if (BusTypeID == "2")
                        return string.Format("<span class=\"link_edit\" onclick=\"javascript:SetIfrmSrc('../zjs/zjsApplyFirstAdd.aspx?a={0}&v=1');\">{1}</span>", Server.UrlEncode(Utility.Cryptography.Encrypt(DataID)), CertificateCode);
                    else
                        return "";
                case "增项注册":
                    return string.Format("<span class=\"link_edit\" onclick=\"javascript:SetIfrmSrc('../Unit/ApplyAddItem.aspx?a={0}&v=1');\">{1}</span>", Server.UrlEncode(Utility.Cryptography.Encrypt(DataID)), CertificateCode);
                case "重新注册":
                    return string.Format("<span class=\"link_edit\" onclick=\"javascript:SetIfrmSrc('../Unit/ApplyRenew.aspx?a={0}&v=1');\">{1}</span>", Server.UrlEncode(Utility.Cryptography.Encrypt(DataID)), CertificateCode);
                case "延期注册":
                    if (BusTypeID == "1")
                        return string.Format("<span class=\"link_edit\" onclick=\"javascript:SetIfrmSrc('../Unit/ApplyContinue.aspx?a={0}&v=1');\">{1}</span>", Server.UrlEncode(Utility.Cryptography.Encrypt(DataID)), CertificateCode);
                    else if (BusTypeID == "2")
                        return string.Format("<span class=\"link_edit\" onclick=\"javascript:SetIfrmSrc('../zjs/zjsApplyContinue.aspx?a={0}&v=1');\">{1}</span>", Server.UrlEncode(Utility.Cryptography.Encrypt(DataID)), CertificateCode);
                    else
                        return "";
                default:
                    return "";
            }
        }

        //批量审核
        protected void ButtonCheck_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridApplyCheckItem, "TaskItemID");
            if (IsGridSelected(RadGridApplyCheckItem) == false)
            {
                UIHelp.layerAlert(Page, "你还没有选择数据！");
                return;
            }

            int rowCount = 0;//处理记录数量
            string filterString = "";//过滤条件

            if (GetGridIfCheckAll(RadGridApplyCheckItem) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGridApplyCheckItem) == true)//排除
                    filterString = string.Format(" {0} and TaskItemID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridApplyCheckItem));
                else//包含
                    filterString = string.Format(" {0} and TaskItemID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridApplyCheckItem));
            }
            try
            {
                rowCount = ApplyCheckTaskItemDAL.SelectCount(filterString);
                ApplyCheckTaskItemDAL.BatCheck(UserName, RadioButtonListCheckResult.SelectedValue, TextBoxCheckResultDesc.Text, filterString);
            }
            catch (Exception ex)
            {

                UIHelp.WriteErrorLog(Page, "批量业务抽查审核失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(PersonName, UserID, "批量业务抽查审核", string.Format("审核了{0}条记录！审核结果为“{1}”。", rowCount, RadioButtonListCheckResult.SelectedValue));

            UIHelp.layerAlert(Page, string.Format("您成功审核了{0}条记录！审核结果为“{1}”。", rowCount, RadioButtonListCheckResult.SelectedValue));
            ClearGridSelectedKeys(RadGridApplyCheckItem);
            BindData();
        }

        //变换审核结果
        protected void RadioButtonListCheckResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonListCheckResult.SelectedValue == "合格")
            {
                TextBoxCheckResultDesc.Text = "符合要求。";
            }
            else
            {
                TextBoxCheckResultDesc.Text = "不符合要求，原因：";
            }
        }

        protected void RadGridApplyCheckItem_DataBound(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridApplyCheckItem, "TaskItemID");
        }

        protected void RadGridApplyCheckItem_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGridApplyCheckItem, "TaskItemID");
        }

        //变换了抽查业务范围
        protected void CheckBoxListBusRange_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }

        protected void ButtonRtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("./ApplyCheckMgr.aspx");
        }
    }
}