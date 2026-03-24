using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;

namespace ZYRYJG.Unit
{
    public partial class UnitMgr : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!this.IsPostBack)
            {
                BindDate();

                if (RadComboBoxENT_City.SelectedValue == "" || RadTextBoxEND_Addess.Text == "" || RadTextBoxENT_Corporate.Text == "" || RadTextBoxENT_Correspondence.Text == "" || TextBoxENT_Contact.Text == "" || TextBoxENT_Telephone.Text == "")
                {
                    UIHelp.layerAlert(Page, "请完善企业信息 ！！！");
                }

            }
        }

        /// <summary>
        /// 绑定企业信息
        /// </summary>
        private void BindDate()
        {            
            UnitMDL o = DataAccess.UnitDAL.GetObject(UserID);
          
            if (o != null)
            {
                 ViewState["UnitMDL"] = o;
                jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL = jcsjk_QY_ZHXXDAL.GetObjectZZJGDM(o.ENT_OrganizationsCode);
                 //System.Data.DataTable dt= jcsjk_QY_ZHXXDAL.GetListZhaoBiaoDaiLi(o.ENT_OrganizationsCode);//招标代理

                if (_jcsjk_QY_ZHXXMDL!=null)//有资质
                {
                    UIHelp.SetData(TableEdit, o, true);
                    UIHelp.SetReadOnly(TextBoxENT_Contact, false);
                    UIHelp.SetReadOnly(TextBoxENT_Telephone, false);
                    UIHelp.SetReadOnly(TextBoxENT_MobilePhone, false);

                    if (string.IsNullOrEmpty(o.ENT_City) == true)
                    {
                        RadComboBoxENT_City.Visible = true;
                        RadTextBoxENT_City.Visible = false;
                    }
                    else
                    {
                        RadComboBoxENT_City.Visible = false;
                        RadTextBoxENT_City.Visible = true;
                    }
                }
                else//新设立企业
                {
                    UIHelp.SetData(TableEdit, o, false);
                    UIHelp.SetReadOnly(RadTextBoxENT_Name, true);
                    UIHelp.SetReadOnly(RadTextBoxENT_OrganizationsCode, true);

                    RadComboBoxENT_City.Visible = true;
                    RadTextBoxENT_City.Visible = false;

                    RadComboBoxENT_Economic_Nature.Visible = true;
                    RadTextBoxENT_Economic_Nature.Visible = false;
                    LabelXSL.Visible = true;

                    UIHelp.SetReadOnly(RadTextBoxENT_QualificationCertificateNo, true);
                    RadTextBoxENT_QualificationCertificateNo.Text = "无";
                    UIHelp.SetReadOnly(RadTextBoxENT_Sort, true);
                    RadTextBoxENT_Sort.Text = "新设立企业";
                    UIHelp.SetReadOnly(RadTextBoxENT_Grade, true);
                    RadTextBoxENT_Grade.Text = "新设立企业";

                    RadTextBoxENT_Type.Text = "新设立企业";
                    UIHelp.SetReadOnly(RadTextBoxENT_Type, true);
                    
                }

                if (o.ENT_City != null)
                {

                    RadComboBoxENT_City.SelectedValue = o.ENT_City;
                }
                
                RadComboBoxENT_Economic_Nature.SelectedValue = o.ENT_Economic_Nature;

            }

            Unit_AQSCXKZMDL _Unit_AQSCXKZMDL = Unit_AQSCXKZDAL.GetObjectZzjgdm(ZZJGDM);
            if(_Unit_AQSCXKZMDL!=null)
            {
                RadTextBoxXKZH.Text = _Unit_AQSCXKZMDL.AQSCXKZBH;
                UIHelp.SetReadOnly(RadTextBoxXKZH, true);
                RadTextBoxFZRQ.Text = ((DateTime)_Unit_AQSCXKZMDL.AQSCXKZFZRQ).ToString("yyyy-MM-dd");
                UIHelp.SetReadOnly(RadTextBoxFZRQ, true);
                RadTextBoxKSYXQ.Text = ((DateTime)_Unit_AQSCXKZMDL.AQSCXKZKSRQ).ToString("yyyy-MM-dd")+"至"+((DateTime)_Unit_AQSCXKZMDL.AQSCXKZJSRQ).ToString("yyyy-MM-dd");
                UIHelp.SetReadOnly(RadTextBoxKSYXQ, true);
            }

            if (IfExistRoleID("2") == true)//企业
            {
                if (string.IsNullOrEmpty(o.CreditCode) == false && o.CreditCode.Length == 18)
                {
                    RadTextBoxTYXYDM.Text = o.CreditCode;
                }
           
                if (o.ResultGSXX == 2)//已通过工商验证
                {
                    lblGSXX.ForeColor = System.Drawing.Color.Blue;
                    lblGSXX.Text = "✔ 已通过工商信息验证！";
                   
                }
                else
                {           
                    if (o.ResultGSXX == 0)//未验证
                    {
                        lblGSXX.ForeColor = System.Drawing.Color.Red;
                        lblGSXX.Text = "* 尚未通过工商信息验证，请输入统一社会信用代码进行验证，否则无法办理注册业务！";
                    }
                    else if (o.ResultGSXX == 1)//验证不通过
                    {
                        lblGSXX.ForeColor = System.Drawing.Color.Red;
                        lblGSXX.Text = string.Format("* 工商信息验证失败，请核对企业是否具有本市合法的工商信息，否则无法办理注册业务！验证时间：{0}", o.ApplyTimeGSXX);
                    }                    
                }
                UIHelp.SetReadOnly(RadTextBoxTYXYDM, true);
            }            
        }

        /// <summary>
        /// 保存企业信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            UnitMDL o = (UnitMDL)ViewState["UnitMDL"];
            // 首先用统一信用代码申请验证，通过之后才能修改信息，0 未对比，1 对比失败
            if (IfExistRoleID("2") == true)//企业
            {
                if (o.ResultGSXX == 0 || o.ResultGSXX == 1)
                {
                    UIHelp.layerAlert(Page, "没有通过工商信息验证，无法进行修改！");
                    return;
                }
            }
            
            
            if (RadComboBoxENT_City.Visible == true && RadComboBoxENT_City.SelectedItem.Value == "")
            {
                UIHelp.layerAlert(Page, "请选择一个隶属区县！");
                return;
            }

            try
            {
                UIHelp.GetData(TableEdit, o);
                //企业ID
                o.XGR = UserName;
                o.XGSJ = DateTime.Now;
                if (RadComboBoxENT_City.Visible == true)
                {
                    o.ENT_City = RadComboBoxENT_City.SelectedValue;
                }
                if (RadComboBoxENT_Economic_Nature.Visible == true)
                {
                    o.ENT_Economic_Nature = RadComboBoxENT_Economic_Nature.SelectedValue;
                }
                DataAccess.UnitDAL.Update(o);
                UIHelp.layerAlert(Page, "修改成功！",6,20000);
                UIHelp.WriteOperateLog(UserName, UserID, "企业信息修改成功", string.Format("修改时间：{0}",DateTime.Now));
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "系统忙，请稍后再试！", ex);
                return;
            }
        }

        ///// <summary>
        ///// 验证工商信息，如果成功则继续，否则，申请验证
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (Utility.Check.isSocialCreditCode(RadTextBoxTYXYDM.Text.Trim().ToUpper()) == false)
        //        {
        //            UIHelp.layerAlert(Page, "您填写的统一信用代码不正确，请检查输入！", 2, 20000);
        //            return;
        //        }
        //        var uni_scid = RadTextBoxTYXYDM.Text.ToUpper().Trim();
        //        // 验证获取的组织机构代码和企业填写的统一信用代码是否匹配
        //        var zzjgdm_test = uni_scid.Substring(8, 9);
        //        if (zzjgdm_test.ToUpper() != RadTextBoxENT_OrganizationsCode.Text.Trim().ToUpper())
        //        {
        //            UIHelp.layerAlert(Page, "您填写的统一信用代码和组织机构代码不匹配，请验证后重新提交！", 2, 20000);
        //            return;
        //        }

        //        GSJ_QY_GSDJXXMDL g = DataAccess.UnitDAL.GetObjectUni_scid(uni_scid);
        //        if (g != null)
        //        {
        //            UnitMDL o = (UnitMDL)ViewState["UnitMDL"];
        //            o.ResultGSXX = 2;
        //            o.ApplyTimeGSXX = DateTime.Now;
        //            DataAccess.UnitDAL.UpdateResultGSXX(o);

        //            div_Checking.Visible = false;

        //            //UIHelp.layerAlert(Page, "验证工商信息成功！", 6, 20000);
        //            //Response.Write("<script>window.location.href='UnitMgr.aspx'</script>");

        //            lblGSXX.ForeColor = System.Drawing.Color.Blue;
        //            lblGSXX.Text = "✔ 已通过工商信息验证！";
        //            UIHelp.SetReadOnly(RadTextBoxTYXYDM, true);
        //            btnSubmit.Visible = false;
        //        }
        //        else
        //        {
        //            //读取当日发起验证次数
        //            int sendCount = UnitDAL.GetSendCheckGSCount(RadTextBoxTYXYDM.Text.Trim());

        //            if (sendCount < 3)
        //            {
        //                UnitMDL o = (UnitMDL)ViewState["UnitMDL"];

        //                if (o.CreditCode != RadTextBoxTYXYDM.Text.Trim().ToUpper())
        //                {
        //                    o.CreditCode = RadTextBoxTYXYDM.Text.Trim().ToUpper();
        //                    UnitDAL.Update(o);//更新企业统一信用代码
        //                }
        //                //企业ID
        //                o.ENT_Name = RadTextBoxENT_Name.Text;
        //                o.CreditCode = RadTextBoxTYXYDM.Text.Trim().ToUpper();
        //                UnitDAL.InsertSubmit(o);

        //                UIHelp.layerAlert(Page, "已经发起了申请验证工商信息！请耐心等待工商返回结果（一般需要5 - 10分钟）。", 4, 20000);
        //                UIHelp.WriteOperateLog(UserName, UserID, "企业申请验证工商信息成功。", string.Format("申请时间：{0}", DateTime.Now));

        //                UIHelp.SetReadOnly(RadTextBoxTYXYDM, true);
        //                div_Checking.Visible = true;
        //                btnSubmit.Visible = false;
        //                SetRefreshTimeSpan(90000);
                        
        //            }
        //            else
        //            {
        //                UIHelp.layerAlert(Page, "申请验证工商信息次数超出当日允许范围，请明日再试。", 5, 10000);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "系统忙，请稍后再试！", ex);
        //        return;
        //    }
        //}

        ///// <summary>
        ///// 设置页面刷新时间
        ///// </summary>
        ///// <param name="_timespan">毫秒</param>
        //protected void SetRefreshTimeSpan(int _timespan)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "reload", string.Format("window.setTimeout(function(){{ window.location.href='UnitMgr.aspx?{0}';}},{1});", DateTime.Now.Ticks,_timespan), true);
        //}

        ///// <summary>
        ///// 定时刷新工商验证结果
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void TimerGSXX_Tick(object sender, EventArgs e)
        //{
        //    var uni_scid = RadTextBoxTYXYDM.Text.ToUpper().Trim();
        //    GSJ_QY_GSDJXXMDL g = UnitDAL.GetObjectUni_scid(uni_scid);
        //    if (g == null)
        //    {
        //        DateTime? sendTime =  UnitDAL.GetLastSendCheckGSTime(uni_scid);
        //        if (sendTime == null //没有发送
        //            || sendTime.Value.AddMinutes(10) < DateTime.Now)//发送超10分钟没有返回
        //        {
        //            //记录验证未通过
        //            UnitMDL o = (UnitMDL)ViewState["UnitMDL"];
        //            o.ResultGSXX = 1;
        //            o.ApplyTimeGSXX = DateTime.Now;
        //            DataAccess.UnitDAL.UpdateResultGSXX(o);

        //            TimerGSXX.Enabled = false;
        //            div_Checking.Visible = false;

        //            UIHelp.layerAlert(Page, "查不到企业工商信息，请核对企业信息是否正确后再次申请验证！", 2, 0);
        //            Response.Write("<script>window.location.href='UnitMgr.aspx'</script>");
        //        }
        //        else//发送未满10分钟，继续等待
        //        {

        //        }
        //    }
        //    else//已经验证
        //    {
        //        TimerGSXX.Enabled = false;
        //        UnitMDL o = (UnitMDL)ViewState["UnitMDL"];
        //        o.ResultGSXX = 2;
        //        o.ApplyTimeGSXX = DateTime.Now;
        //        DataAccess.UnitDAL.UpdateResultGSXX(o);

        //        div_Checking.Visible = false;
        //        btnSubmit.Visible = false;
        //        TimerGSXX.Enabled = false;
        //        UIHelp.SetReadOnly(RadTextBoxTYXYDM, true);

        //        Response.Write("<script>window.location.href='UnitMgr.aspx'</script>");
                
        //    }
            

             
        //}
    }
}