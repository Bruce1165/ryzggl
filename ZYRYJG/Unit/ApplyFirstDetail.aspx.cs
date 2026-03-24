using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using System.Data;


namespace ZYRYJG.Unit
{
    public partial class ApplyFirstDetail : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string applyid = Request.QueryString["a"];
            //申请表信息
            if (!this.IsPostBack)
            {
                ApplyMDL _ApplyMDL = ApplyDAL.GetObject(applyid);
                //初始注册信息
                ApplyFirstMDL _ApplyFirst = ApplyFirstDAL.GetObject(applyid);
                UIHelp.SetData(SetTable, _ApplyMDL, true);
                UIHelp.SetData(SetTable, _ApplyFirst, true);
                if (_ApplyFirst.IfSameUnit == true)
                {
                    RadioButton1.Checked = true;
                }
                RadioButton1.Enabled = false;
                RadioButton2.Enabled = false;
                ViewState["ApplyMDL"] = _ApplyMDL;
                ViewState["ApplyFirst"] = _ApplyFirst;
                SetButtonEnable(_ApplyMDL.ApplyStatus);
            }
        }
        //申报
        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表
            _ApplyMDL.XGR = UserName;
            _ApplyMDL.XGSJ = DateTime.Now;
            if (ButtonApply.Text == "撤销申报")
            {
                _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.未申报;
                _ApplyMDL.ApplyTime = null;
            }
            else
            {
                _ApplyMDL.ApplyStatus = EnumManager.ApplyStatus.已申报;
                _ApplyMDL.ApplyTime = DateTime.Now;

            }
            ApplyDAL.Update(_ApplyMDL);
            ViewState["ApplyMDL"] = _ApplyMDL;
            SetButtonEnable(_ApplyMDL.ApplyStatus);
            if (_ApplyMDL.ApplyStatus == EnumManager.ApplyStatus.已申报)
                UIHelp.Alert(Page, "申报成功！");
            else
                UIHelp.Alert(Page, "撤销成功！");
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //删除申请
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            ApplyMDL _ApplyMDL = (ApplyMDL)ViewState["ApplyMDL"];//申请表
            ApplyDAL.Delete(_ApplyMDL.ApplyID);
            ViewState.Remove("ApplyMDL");
            SetButtonEnable(_ApplyMDL.ApplyStatus);

            UIHelp.Alert(Page, "删除成功！");
            ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
        }

        //操作按钮控制
        protected void SetButtonEnable(string ApplyStatus)
        {
            switch (ApplyStatus)
            {
                case "":
                    // ButtonSave.Enabled = true;
                    ButtonApply.Enabled = false;
                    ButtonApply.Text = "申 报";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    break;
                case EnumManager.ApplyStatus.未申报:
                    // ButtonSave.Enabled = true;
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "申 报";
                    ButtonDelete.Enabled = true;
                    ButtonOutput.Enabled = true;
                    break;
                case EnumManager.ApplyStatus.已申报:
                    // ButtonSave.Enabled = false;
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "撤销申报";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = true;
                    break;
                case EnumManager.ApplyStatus.已驳回:
                    //ButtonSave.Enabled = true;
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "申 报";
                    ButtonDelete.Enabled = true;
                    ButtonOutput.Enabled = true;
                    break;
                default:
                    // ButtonSave.Enabled = false;
                    ButtonApply.Enabled = false;
                    ButtonApply.Text = "撤销申报";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = true;
                    break;
            }
            //ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonApply.CssClass = ButtonApply.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonDelete.CssClass = ButtonDelete.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonOutput.CssClass = ButtonOutput.Enabled == true ? "bt_large" : "bt_large btn_no";
        }

        //导出打印
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            string sourceFile = HttpContext.Current.Server.MapPath("~/Template/北京市二级建造师初始注册申请表.docx");
            string fileName = "北京市二级建造师初始注册申请表";
            ApplyMDL _ApplyMDL = ViewState["ApplyMDL"] as ApplyMDL;
            ApplyFirstMDL _ApplyFirstMDL = ViewState["ApplyFirst"] as ApplyFirstMDL;
            var o = new List<object>();
            o.Add(_ApplyMDL);
            o.Add(_ApplyFirstMDL);
            var ht = PrintDocument.GetProperties(o);
            ht["isCtable"] = false;
            //对时间类型进行格式转换
            ht["Birthday"] = _ApplyFirstMDL.Birthday == null ? "" : ((DateTime)_ApplyFirstMDL.Birthday).ToString("yyyyMMdd");
            ht["GraduationTime"] = _ApplyFirstMDL.GraduationTime == null ? "" : ((DateTime)_ApplyFirstMDL.GraduationTime).ToString("yyyyMMdd");
            ht["ConferDate"] = _ApplyFirstMDL.ConferDate == null ? "" : ((DateTime)_ApplyFirstMDL.ConferDate).ToString("yyyyMMdd");
            //证件类型
            string ZjType = ht["PSN_CertificateType"].ToString();
            ht["SFZ"] = ZjType == "身份证" ? "☑" : "□";
            ht["JGZ"] = ZjType == "军官证" ? "☑" : "□";
            ht["JG"] = ZjType == "警官证" ? "☑" : "□";
            ht["HZ"] = ZjType == "护照" ? "☑" : "□";
            //企业类型
            string QyType = ht["ENT_Type"].ToString();
            ht["SG"] = QyType == "施工" ? "☑" : "□";
            ht["KC"] = QyType == "勘察" ? "☑" : "□";
            ht["SJ"] = QyType == "设计" ? "☑" : "□";
            ht["JL"] = QyType == "监理" ? "☑" : "□";
            ht["ZBDL"] = QyType == "招标代理" ? "☑" : "□";
            ht["ZJZX"] = QyType == "造价咨询" ? "☑" : "□";
            //将身份证拆开，小于18位的后三位用空补齐
            string PSN_CertificateNO = ht["PSN_CertificateNO"].ToString();
            char[] a = PSN_CertificateNO.ToCharArray();
            if (a.Length < 17)
            {
                a[15] = Convert.ToChar("");
                a[16] = Convert.ToChar("");
                a[17] = Convert.ToChar("");
            }
            for (int i = 0; i < a.Length; i++)
            {
                ht["Sfz" + i + ""] = a[i];
            }
            PrintDocument.CreateDataToWordByHashtable(sourceFile, fileName, ht);
        }
    }
}