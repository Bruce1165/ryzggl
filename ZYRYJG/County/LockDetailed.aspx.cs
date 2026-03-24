using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.County
{
    public partial class LockDetailed : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "County/LockList.aspx";
            }
        }
        public void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Lbl_PSN_RegisterCertificateNo.Text = Request["numno"];
                Lbl_PSN_Name.Text = Request["name"];
                Lbl_LockPeople.Text = UserName;
                Lbl_LockStartTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                Lbl_lx.Text = Request["lx"];
                Lbl_SFZH.Text = Request["sfzh"];
                GetList();
            }
        }

        protected void ButtonOK_Click(object sender, EventArgs e)
        {
           
            if (RadDatePickerGetDateEnd.SelectedDate.HasValue!=true)
            {
                UIHelp.layerAlert(Page, "请选择截止时间！！！");
                return;
            }
            if (TextBox1.Text.Trim()=="")
            {
                UIHelp.layerAlert(Page, "请填写锁定原因说明！！！");
                return;
            }
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                Certificate_LockMDL _Certificate_LockMDL = new Certificate_LockMDL();
                Certificate_LockHisMDL _Certificate_LockHisMDL = new Certificate_LockHisMDL();
                #region 加入锁定表


                _Certificate_LockMDL.Id = Guid.NewGuid().ToString();
                _Certificate_LockMDL.Fid = Request["id"];
                _Certificate_LockMDL.LockStartTime = DateTime.Parse(Lbl_LockStartTime.Text);
                _Certificate_LockMDL.LockEndTime = RadDatePickerGetDateEnd.SelectedDate.Value;
                _Certificate_LockMDL.LockPeople = UserName;
                _Certificate_LockMDL.PSN_CertificateNO = Request["sfzh"];
                _Certificate_LockMDL.LockContent = TextBox1.Text.Trim();
                _Certificate_LockMDL.LockStates = "加锁";
                _Certificate_LockMDL.LX = Request["lx"];
                _Certificate_LockMDL.PSN_RegisterCertificateNo = Request["numno"];
                _Certificate_LockMDL.PSN_Name = Request["name"];
                #endregion

                #region  加入锁定历史表      
              
                _Certificate_LockHisMDL.HisId = Guid.NewGuid().ToString();
                _Certificate_LockHisMDL.Id = _Certificate_LockMDL.Id;
                _Certificate_LockHisMDL.Fid = Request["id"];
                _Certificate_LockHisMDL.LockStartTime = DateTime.Parse(Lbl_LockStartTime.Text);
                _Certificate_LockHisMDL.LockEndTime = RadDatePickerGetDateEnd.SelectedDate.Value;
                _Certificate_LockHisMDL.LockPeople = UserName;
                _Certificate_LockHisMDL.PSN_CertificateNO = _Certificate_LockMDL.PSN_CertificateNO;
                _Certificate_LockHisMDL.LockContent = TextBox1.Text.Trim();
                _Certificate_LockHisMDL.LockStates = "加锁";
                _Certificate_LockHisMDL.LX = Request["lx"];
                _Certificate_LockHisMDL.PSN_RegisterCertificateNo = Request["numno"];
                _Certificate_LockHisMDL.PSN_Name = Request["name"];
                _Certificate_LockHisMDL.WriteDateTime = DateTime.Now;
                #endregion
                Certificate_LockDAL.Insert(tran,_Certificate_LockMDL);
                Certificate_LockDAL.Insert(tran, _Certificate_LockHisMDL);

                tran.Commit();
                UIHelp.WriteOperateLog(UserName, UserID, "保存锁定成功", string.Format("姓名：{0}，保存时间：{1}。", UserName, DateTime.Now.Date));
               
                ButtonOK.Enabled = false;
                ButtonOK.CssClass = ButtonOK.Enabled == true ? "bt_large" : "bt_large btn_no";
                GetList();
                UIHelp.layerAlert(Page, "锁定成功");
              
                //ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "保存锁定失败", ex);
              
            }
            
        }

        public void GetList()
        {
            ObjectDataSource1.SelectParameters.Clear();
            var q = new QueryParamOB();
            q.Add(string.Format("Fid = '{0}'", Request["id"]));
            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGridQY.CurrentPageIndex = 0;
            RadGridQY.DataSourceID = ObjectDataSource1.ID;
        }
    }
}