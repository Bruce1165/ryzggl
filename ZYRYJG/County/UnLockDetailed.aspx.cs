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
    public partial class UnLockDetailed :BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "County/LockList.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Lbl_PSN_RegisterCertificateNo.Text = Request["numno"];
                Lbl_PSN_Name.Text = Request["name"];
                Lbl_UnLockPeople.Text = UserName;
                Lbl_UnLockTime.Text = DateTime.Now.ToString("yyyy-MM-dd");
                Lbl_lx.Text = Request["lx"];
                Lbl_SFZH.Text = Request["sfzh"];
                GetList();
            }

        }

        protected void ButtonOK_Click(object sender, EventArgs e)
        {
            //if (RadDatePickerGetDateEnd.SelectedDate.HasValue != true)
            //{
            //    UIHelp.layerAlert(Page, "请选择截止时间！！！");
            //    return;
            //}
            if (TextBox1.Text.Trim() == "")
            {
                UIHelp.layerAlert(Page, "请填写解锁原因说明！！！");
                return;
            }
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                Certificate_LockMDL _Certificate_LockMDL = new Certificate_LockMDL();
                Certificate_LockHisMDL _Certificate_LockHisMDL = new Certificate_LockHisMDL();
                #region 更新锁定表

                _Certificate_LockMDL.Id = Guid.NewGuid().ToString();
                _Certificate_LockMDL.Fid = Request["id"];
                _Certificate_LockMDL.UnlockTime = DateTime.Parse(Lbl_UnLockTime.Text);
                _Certificate_LockMDL.LockEndTime = DateTime.Parse(Lbl_UnLockTime.Text);
                _Certificate_LockMDL.UnlockPeople = UserName;
                _Certificate_LockMDL.PSN_CertificateNO = Request["sfzh"];
                _Certificate_LockMDL.UnlockContent = TextBox1.Text.Trim();
                _Certificate_LockMDL.LockStates = "解锁";
                _Certificate_LockMDL.LX = Request["lx"];
                _Certificate_LockMDL.PSN_RegisterCertificateNo = Request["numno"];
                _Certificate_LockMDL.PSN_Name = Request["name"];
                #endregion

                #region 添加锁定记录表

                _Certificate_LockHisMDL.HisId = Guid.NewGuid().ToString();
                _Certificate_LockHisMDL.Id = _Certificate_LockMDL.Id;
                _Certificate_LockHisMDL.Fid = Request["id"];
                _Certificate_LockHisMDL.UnlockTime = DateTime.Parse(Lbl_UnLockTime.Text);
                _Certificate_LockHisMDL.LockEndTime = DateTime.Parse(Lbl_UnLockTime.Text);
                _Certificate_LockHisMDL.UnlockPeople = UserName;
                _Certificate_LockHisMDL.PSN_CertificateNO = Request["sfzh"];
                _Certificate_LockHisMDL.LockContent = TextBox1.Text.Trim();
                _Certificate_LockHisMDL.LockStates = "解锁";
                _Certificate_LockHisMDL.LX = Request["lx"];
                _Certificate_LockHisMDL.PSN_RegisterCertificateNo = Request["numno"];
                _Certificate_LockHisMDL.PSN_Name = Request["name"];
                _Certificate_LockHisMDL.WriteDateTime = DateTime.Now;
                #endregion

                Certificate_LockDAL.Update(tran, _Certificate_LockMDL);
                Certificate_LockDAL.Insert(tran, _Certificate_LockHisMDL);

                tran.Commit();
                UIHelp.WriteOperateLog(UserName, UserID, "保存解锁成功", string.Format("姓名：{0}，保存时间：{1}。", UserName, DateTime.Now.Date));
                UIHelp.layerAlert(Page, "解锁成功");
                ButtonOK.Enabled = false;
                ButtonOK.CssClass = ButtonOK.Enabled == true ? "bt_large" : "bt_large btn_no";
                GetList();

            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "保存解锁失败", ex);
             
            }

        }
        /// <summary>
        /// 加载证书锁定记录
        /// </summary>
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