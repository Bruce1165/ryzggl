using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Utility;
using DataAccess;
using Model;

namespace ZYRYJG.CheckMgr
{
    public partial class ApplyCheckEdit : BasePage
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
        protected long? TaskID
        {
            get
            {
                if (ViewState["TaskID"] == null)
                    return null;
                else
                    return Convert.ToInt64(ViewState["TaskID"]);
            }
        }

        /// <summary>
        /// 
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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["o"]) == false)//修改
                {
                    try
                    {
                        ViewState["TaskID"] = Utility.Cryptography.Decrypt(Request["o"]);
                        ApplyCheckTaskMDL o = ApplyCheckTaskDAL.GetObject(TaskID.Value);
                        ViewState["ApplyCheckTaskMDL"] = o;

                        RadDatePickerStart.SelectedDate = o.BusStartDate;
                        RadDatePickerEnd.SelectedDate = o.BusEndDate;
                        RadNumericTextBoxCheckPer.Value = o.CheckPer;
                        LabelItemCount.Text = o.ItemCount.ToString();
                        Labelcjsj.Text = o.cjsj.Value.ToString("yyyy-MM-dd HH:mm");

                        foreach (string s in o.BusRangeCode.Split(','))
                        {
                            CheckBoxListBusRange.Items.FindByValue(s).Selected = true;
                        }
                    }
                    catch (Exception ex)
                    {
                         UIHelp.WriteErrorLog(Page, "获取业务申请单抽查失败！", ex);
                    }
                    
                }
                else
                {
                    
                }

            }
        }

     
        //随机创建抽查业务
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            DateTime start = RadDatePickerStart.SelectedDate.Value;//业务起始时间
            DateTime end = RadDatePickerEnd.SelectedDate.Value;//业务截止时间
            System.Text.StringBuilder sbName = new System.Text.StringBuilder();
            System.Text.StringBuilder sbID = new System.Text.StringBuilder();
            foreach (ListItem item in CheckBoxListBusRange.Items)
            {
                if (item.Selected == true)
                {
                    sbName.Append("、").Append(item.Text);
                    sbID.Append(",").Append(item.Value);
                }
            }
            if (sbName.Length > 0)
            {
                sbName.Remove(0, 1);
                sbID.Remove(0, 1);
            }

            if (sbName.Length == 0)
            {
                UIHelp.layerAlert(Page, "请勾选抽查业务范围。");
                return;
            }
            if (RadDatePickerStart.SelectedDate.HasValue == false || RadDatePickerEnd.SelectedDate.HasValue == false)
            {
                UIHelp.layerAlert(Page, "请选择业务办结日期范围。");
                return;
            }
            if (RadDatePickerStart.SelectedDate.Value > RadDatePickerEnd.SelectedDate.Value)
            {
                UIHelp.layerAlert(Page, "业务办结日期参数设置有误，请修改。");
                return;
            }

            ApplyCheckTaskMDL o = (TaskID.HasValue == true ? (ApplyCheckTaskMDL)ViewState["ApplyCheckTaskMDL"] : new ApplyCheckTaskMDL());
            if (o.TaskID.HasValue == true && CheckBoxIfDelHis.Checked == false)
            {
                UIHelp.layerAlert(Page, "已经创建抽查过数据，无法覆盖，若要重新抽查，请勾选“覆盖上次抽查结果”。");
                return;
            }

            DBHelper db = new DBHelper();
            DbTransaction trans = db.BeginTransaction();
            try
            {
                o.cjsj = DateTime.Now;
                o.cjr = UserName;
                o.BusRange = sbName.ToString();
                o.BusRangeCode = sbID.ToString();
                o.BusStartDate = RadDatePickerStart.SelectedDate.Value;
                o.BusEndDate = RadDatePickerEnd.SelectedDate.Value;
                o.CheckPer = Convert.ToInt32(RadNumericTextBoxCheckPer.Value);
                if (o.TaskID.HasValue == true)//edit
                {
                    ApplyCheckTaskItemDAL.DeleteByTaskID(trans, o.TaskID.Value);
                    ApplyCheckTaskDAL.Update(trans, o);
                }
                else//new
                {
                    ApplyCheckTaskDAL.Insert(trans, o);
                }
                ApplyCheckTaskItemDAL.InsertPatch(trans, o);
                ApplyCheckTaskDAL.UpdateItemCount(trans, o.TaskID.Value);

                trans.Commit();

                o =ApplyCheckTaskDAL.GetObject(o.TaskID.Value);
                ViewState["ApplyCheckTaskMDL"] = o;
                ViewState["TaskID"] = o.TaskID;
                LabelItemCount.Text = o.ItemCount.ToString();
                Labelcjsj.Text = o.cjsj.Value.ToString("yyyy-MM-dd HH:mm");

                UIHelp.layerAlert(Page, "创建业务申请单抽查成功，可以通知审核人对抽查数据进行审核。");

                UIHelp.WriteOperateLog(UserName, UserID, "创建业务申请单抽查任务", string.Format("业务范围：{0}。抽查记录数：{1}。", o.BusRange,o.ItemCount));
                ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "创建业务申请单抽查失败！", ex);
            }
        }
       
    }
}