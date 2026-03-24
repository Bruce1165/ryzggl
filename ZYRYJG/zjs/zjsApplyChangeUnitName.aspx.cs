using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using System.Data;
using System.IO;
using System.Drawing;

namespace ZYRYJG.zjs
{
    public partial class zjsApplyChangeUnitName : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "zjs/zjsApplyList.aspx|zjs/zjsAgency.aspx|zjs/zjsBusinessQuery.aspx";
            }
        }

        /// <summary>
        /// 申请单批次号
        /// </summary>
        protected string ApplyCode
        {
            get { return ViewState["ApplyCode"] == null ? "" : ViewState["ApplyCode"].ToString(); }
        }

        protected void Page_Load(object sender, EventArgs e)
        {           
            if (!this.IsPostBack)
            {
                // 验证工商信息是否通过                
                if (IfExistRoleID("2") == true)//企业
                {
                    try
                    {
                        UnitMDL o = DataAccess.UnitDAL.GetObject(UserID);
                        if (o.ResultGSXX == 0 || o.ResultGSXX == 1 || string.IsNullOrEmpty(o.ENT_City) == true)
                        {
                            Response.Write("<script>window.location.href='../Unit/UnitMgr.aspx'</script>");
                            return;
                        }
                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "企业信息变更申请时，获取企业信息时失败", ex);
                        return;
                    }
                }

                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");
                ViewState["ApplyID"] = Utility.Cryptography.Decrypt(Request["a"]);//申请ID
                ViewState["zzjgdm"] = Utility.Cryptography.Decrypt(Request["zzjgdm"]);//机构代码

                #region 根据权限显示面板

                //是否为查看
                bool ViewLimit = (string.IsNullOrEmpty(Request["v"]) == false && Request["v"] == "1") ? true : false;
                //申请操作权限
                if (IfExistRoleID("2") == true && ViewLimit == false)
                {
                    divQY.Visible = true;
                    TdInputRemark1.Visible = true;
                    TdInputRemark2.Visible = true;

                    //企业看不到各级申办人列
                    RadGridCheckHistory.Columns.FindByUniqueName("ActionMan").Visible = false;
                    //divCheckHistory.Visible = false;
                }

                #endregion 根据权限显示面板

                BindData();
            }
            else if (Request["__EVENTTARGET"] == "refreshFile")
            {
                BindFile(ApplyCode);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonOutput.ClientID), true);
            }
            else if (Request["__EVENTTARGET"] == "Decide")//发现决定结果与审核结果不一致，仍然继续执行决定。
            {
                Decide();
            }  
        }

        /// <summary>
        /// 绑定审核历史记录
        /// </summary>
        /// <param name="ApplyCode">申请批次号</param>
        private void BindCheckHistory(string ApplyID)
        {
            try
            {
                DataTable dt = zjs_ApplyDAL.GetCheckHistoryList(ApplyID);
                RadGridCheckHistory.DataSource = dt;
                RadGridCheckHistory.DataBind();
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "读取审核历史记录时失败", ex);
                return;
            }
        }

        ///// <summary>
        ///// 绑定附件
        ///// </summary>
        ///// <param name="ApplyID"></param>
        //private void BindFile(string ApplyCode)
        //{
        //    DataTable dt_ApplyFile = COC_TOW_Person_FileDAL.GetListByPSN_RegisterNO(ApplyCode);
        //    RadGridFile.DataSource = dt_ApplyFile;
        //    RadGridFile.DataBind();
        //}

        //绑定申请记录
        private void BindData()
        {
            zjs_ApplyChangeMDL ac = null;
            if (ViewState["ApplyID"] != null && ViewState["ApplyID"].ToString() != "")//查看
            {
                try
                {
                    ac = zjs_ApplyChangeDAL.GetObject(ViewState["ApplyID"].ToString());
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "读取造价工程师企业信息变更申请单时失败", ex);
                    return;
                }
            }
            else if (ViewState["zzjgdm"] != null && ViewState["zzjgdm"].ToString()!="")//审批
            {
                try
                {
                    string sql = "SELECT top 1 * FROM zjs_APPLY WHERE (ENT_OrganizationsCode='{0}' OR ENT_OrganizationsCode like '________{0}_') and (APPLYSTATUS='{1}' ) and [ApplyTypeSub]='企业信息变更'";
                    DataTable dt = null;
                    if (IfExistRoleID("20") == true)//受理
                    {
                        dt = CommonDAL.GetDataTable(string.Format(sql, ViewState["zzjgdm"].ToString(), EnumManager.ZJSApplyStatus.已申报));
                    }
                    else if (IfExistRoleID("21") == true)//审核
                    {
                        dt = CommonDAL.GetDataTable(string.Format(sql, ViewState["zzjgdm"].ToString(), EnumManager.ZJSApplyStatus.已受理));
                    }                   
                    else if (IfExistRoleID("23") == true)//决定
                    {
                        dt = CommonDAL.GetDataTable(string.Format(sql, ViewState["zzjgdm"].ToString(), EnumManager.ZJSApplyStatus.已审核));
                    }
                    else
                    {
                        dt = CommonDAL.GetDataTable(string.Format("SELECT top 1 * FROM zjs_APPLY WHERE (ENT_OrganizationsCode='{0}' OR ENT_OrganizationsCode like '________{0}_') and (APPLYSTATUS<>'{1}' ) and [ApplyTypeSub]='企业信息变更'"
                            , ViewState["zzjgdm"].ToString(), EnumManager.ZJSApplyStatus.已决定));
                    }
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        ac = zjs_ApplyChangeDAL.GetObject(dt.Rows[0]["ApplyID"].ToString());
                    }
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "读取造价工程师企业信息变更申请单时失败", ex);
                    return;
                }
            }
            else if (IfExistRoleID("2") == true)//企业
            {
                //string sql = "SELECT top 1 * FROM zjs_APPLY WHERE (ENT_OrganizationsCode='{0}'OR ENT_OrganizationsCode like '________{0}_') and [ApplyTypeSub]='企业信息变更' and APPLYSTATUS <>'{1}'";
                string sql = "SELECT top 1 * FROM zjs_APPLY WHERE (ENT_OrganizationsCode='{0}'OR ENT_OrganizationsCode like '________{0}_') and [ApplyTypeSub]='企业信息变更' and NoticeDate is null";
                DataTable dt = CommonDAL.GetDataTable(string.Format(sql, ZZJGDM));
                if (dt != null && dt.Rows.Count>0)
                {
                    try
                    {
                        ac = zjs_ApplyChangeDAL.GetObject(dt.Rows[0]["ApplyID"].ToString());
                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "读取造价工程师企业信息变更申请单时失败", ex);
                        return;
                    }
                }
                else//新增
                {
                    UnitMDL u = UnitDAL.GetObjectByENT_OrganizationsCode(ZZJGDM);
                    if(u!=null)
                    {
                        LabelENT_Type.Text = u.ENT_Type;//企业类型
                        LabelFR.Text = u.ENT_Corporate;//法人
                        LabelLinkMan.Text = u.ENT_Contact;//联系人
                        LabelENT_Telephone.Text = u.ENT_Telephone;//联系电话
                        LabelEND_Addess.Text = u.END_Addess;//工商注册地址
                    }

                    //工商信息             
                    GSJ_QY_GSDJXXMDL g = DataAccess.UnitDAL.GetObjectUni_scid(SHTJXYDM); //工商信息

                    if (g != null)//有工商信息
                    {
                        LabelENT_NameTo.Text = g.ENT_NAME;//变更后名称                                                
                        SetButtonEnable(null);//按钮                        
                        BindFile("0");//绑定附件信息                       
                       

                        //人员证书列表
                        DataTable dtPerson = CommonDAL.GetDataTable(string.Format("SELECT row_number()  over(ORDER BY PSN_Name) as rn,ENT_Name,PSN_Name,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisteProfession  FROM [zjs_Certificate] WHERE  (ENT_OrganizationsCode='{0}' OR ENT_OrganizationsCode like '________{0}_') and (ENT_Name <>'{1}' ) and [PSN_RegisteType] < '07' ", ZZJGDM, g.ENT_NAME));
                        if (dtPerson.Rows.Count > 0)
                        {
                            LabelENT_NameFrom.Text = dtPerson.Rows[0]["ENT_Name"].ToString();//变更前名称
                        }
                        RadGridPerson.DataSource = dtPerson;
                        RadGridPerson.DataBind();
                        LabelPersonCount.Text = string.Format("(共计{0}本)", dtPerson.Rows.Count);
                        ViewState["PersonCount"] = dtPerson.Rows.Count;

                        foreach (DataRow r in dtPerson.Rows)
                        {
                            if (r["ENT_Name"].ToString() != g.ENT_NAME)
                            {
                                LabelENT_NameFrom.Text = r["ENT_Name"].ToString();
                                break;
                            }
                        }
                        if (LabelENT_NameFrom.Text=="")
                        {
                            LabelENT_NameFrom.Text = LabelENT_NameTo.Text;
                        }
                        setFontColor(); //变更内容提示
                        return;
                    }
                    else
                    {
                        UIHelp.layerAlert(Page, "没有查询到企业的工商信息，无法申请企业信息变更!");
                        return;
                    }
                }
            }
            else
            {
                throw new Exception("参数错误！无法为你显示");
            }

            if (ac != null)
            {
                
                LabelENT_NameFrom.Text = ac.ENT_NameFrom;//变更前企业名称              
                LabelENT_NameTo.Text = ac.ENT_NameTo; //变更后企业名称
                LabelENT_Type.Text = ac.ENT_Type;//企业类型
                LabelFR.Text = ac.FR;//法人
                LabelLinkMan.Text = ac.LinkMan;//联系人
                LabelENT_Telephone.Text = ac.ENT_Telephone;//联系电话
                LabelEND_Addess.Text = ac.END_Addess;//工商注册地址

                zjs_ApplyMDL a = zjs_ApplyDAL.GetObject(ac.ApplyID);
                ViewState["zzjgdm"] = a.ENT_OrganizationsCode;
                ViewState["zjs_ApplyMDL"] = a;
                SetButtonEnable(a); 

                //附件申请号
                ViewState["ApplyCode"] = a.ApplyCode;

                //绑定附件信息
                BindFile(a.ApplyCode);

                //审核历史
                BindCheckHistory(a.ApplyID);

                DataTable dtPerson = CommonDAL.GetDataTable(string.Format("SELECT row_number()  over(ORDER BY PSN_Name) as rn,PSN_Name,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisteProfession FROM zjs_Apply WHERE ApplyCode= '{0}'", ViewState["ApplyCode"]));
                RadGridPerson.DataSource = dtPerson;
                RadGridPerson.DataBind();
                LabelPersonCount.Text = string.Format("（共计{0}本）", dtPerson.Rows.Count);
                ViewState["PersonCount"] = dtPerson.Rows.Count;

                if (IfExistRoleID("2") == true
                     && (a.ApplyStatus == EnumManager.ZJSApplyStatus.未申报 || a.ApplyStatus == EnumManager.ZJSApplyStatus.已驳回)
                     )
                {
                    trFuJanTitel.Visible = true;
                    trFuJan.Visible = true;
                }
            }
            setFontColor();
        }

        //变更颜色提示
        protected void setFontColor() {
            //变更内容变色
            if (LabelENT_NameFrom.Text.Replace("（", "(").Replace("）", ")") != LabelENT_NameTo.Text.Replace("（", "(").Replace("）", ")"))
            {
                LabelENT_NameTo.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                LabelENT_NameTo.ForeColor = System.Drawing.Color.Black;
            }        
        }

        //保存
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            #region 规则检查

            if (int.Parse(ViewState["PersonCount"].ToString()) == 0)
            {
                UIHelp.layerAlert(Page, "无可申请人员证书信息，请核对企业工商信息是否首先做了变更！");
                return;
            }

            if (LabelENT_NameFrom.Text.Trim() == LabelENT_NameTo.Text.Trim())
            {
                UIHelp.layerAlert(Page, "变更前后企业名称没有变化，无法申请！");
                return;
            }

            //if (LabelENT_NameFrom.Text.Trim().Replace("（", "(").Replace("）", ")") != LabelENT_NameTo.Text.Trim().Replace("（", "(").Replace("）", ")"))//如果变更了企业名称
            //{
            //    DataTable yj = CommonDAL.GetDataTable(string.Format("SELECT COUNT(1) FROM [dbo].[jcsjk_zjs] WHERE replace(replace([PYDW],'（','('),'）',')')='{0}'", LabelENT_NameFrom.Text.Replace("（", "(").Replace("）", ")")));
            //    if (yj.Rows[0][0].ToString() != "0")
            //    {
            //        UIHelp.layerAlert(Page, "请先变更完一级造价工程师证书的企业名称再变更二级证书！");
            //        return;
            //    }
            //}

            //原单位在办业务数量
            int ydwApplyingCount = zjs_ApplyDAL.SelectCountApplyView(string.Format(" and (ENT_OrganizationsCode='{0}' or  ENT_OrganizationsCode like '________{0}_') and applytype is not null  and ApplyStatus <>'已公告' and ApplyStatus <>'已驳回' and  PSN_RegisteType <'07'", ZZJGDM));

            DataTable Xdt = CommonDAL.GetDataTable(string.Format("select * from zjs_apply where NoticeDate is null and (ENT_OrganizationsCode='{0}' or  ENT_OrganizationsCode like '________{0}_')", ZZJGDM));//查询申请单位是否有在办业务
            if (ydwApplyingCount > 0 || Xdt.Rows.Count > 0)
            {
                UIHelp.layerAlert(Page, "企业有未办结的注册事项，无法变更企业信息！");
                return;
            }

            #endregion

            DBHelper db = new DBHelper();
            string sql = "";
            DbTransaction tran = db.BeginTransaction();
            try
            {
                if (ViewState["ApplyCode"] == null)
                {
                    ViewState["ApplyCode"] = zjs_ApplyDAL.GetNextApplyCode(tran, "企业信息变更");
                }

                //申请主表
                sql = @"INSERT INTO [dbo].[zjs_Apply]([ApplyID],[ApplyType],[ApplyTypeSub],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[PSN_Name],[PSN_Sex],[PSN_CertificateType],[PSN_CertificateNO],[PSN_RegisterNo],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[ApplyTime],[ApplyCode],[ApplyStatus],[CJR],[CJSJ],[XGR],[XGSJ],[Valid])	
                        select newid(),'变更注册','企业信息变更',[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[PSN_Name],[PSN_Sex],[PSN_CertificateType],[PSN_CertificateNO],[PSN_RegisterNo],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],null,'{2}','未申报','{0}','{1}','{0}','{1}',1 
                        FROM dbo.zjs_Certificate 
                        where (ENT_OrganizationsCode = '{3}' or ENT_OrganizationsCode like '________{3}_')  and (ENT_Name <>'{4}') and [PSN_RegisteType] < '07'";

                CommonDAL.ExecSQL(tran, string.Format(sql, UserName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ViewState["ApplyCode"], ZZJGDM, LabelENT_NameTo.Text));

                //申请子表
                sql = @"INSERT INTO [dbo].[zjs_ApplyChange]([ApplyID],[PSN_RegisterNo],[ValidDate],[ChangeReason],[ENT_NameFrom],[ENT_NameTo],[ENT_Name],[FR],[END_Addess],[LinkMan],[ENT_Telephone],[ENT_Type]) 
                        select a.[ApplyID],p.[PSN_RegisterNo],p.[PSN_CertificateValidity],'企业信息变更','{0}','{1}','{0}','{2}','{3}','{4}','{5}','{6}'
                        from [dbo].[zjs_Apply] a inner join [dbo].[zjs_Certificate] p on a.ApplyCode='{7}' and a.[PSN_RegisterNo] = p.[PSN_RegisterNo] 
                        where a.ApplyCode='{7}' ";

                CommonDAL.ExecSQL(tran, string.Format(sql, LabelENT_NameFrom.Text, LabelENT_NameTo.Text,LabelFR.Text,LabelEND_Addess.Text,LabelLinkMan.Text,LabelENT_Telephone.Text,LabelENT_Type.Text, ViewState["ApplyCode"]));

                tran.Commit();

                zjs_ApplyMDL _zjs_ApplyMDL = new zjs_ApplyMDL();
                _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.未申报;

                SetButtonEnable(_zjs_ApplyMDL);

                //绑定附件信息
                BindFile(ViewState["ApplyCode"].ToString());

                trFuJanTitel.Visible = true;
                trFuJan.Visible = true;

                UIHelp.WriteOperateLog(UserName, UserID, "造价工程师企业信息变更申请保存成功", string.Format("变更后企业名称：{0}", LabelENT_NameTo.Text));
                UIHelp.layerAlert(Page, "保存成功，请将盖章签字后的申请表扫描上传！（注意：不得对导出的申请表内容进行任何形式的编辑或修改，否则按提供虚假资料申报注册处理。）如果有其他从业人员证书，请同时在从业人员证书管理中发起企业信息变更，以免造成后期无法申请。");

            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "保存造价工程师企业信息变更申请失败", ex);
            }

            BindData();
        }

        //申报
        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            if (ButtonApply.Text != "取消申报")
            {
                //必须上传附件集合
                System.Collections.Hashtable fj = new System.Collections.Hashtable{
                {EnumManager.FileDataTypeName.企业信息变更证明,0},
                {EnumManager.FileDataTypeName.企业营业执照扫描件,0},                
                {EnumManager.FileDataTypeName.申请表扫描件,0}            };

                //已上传附件集合
                DataTable dt = ApplyDAL.GetApplyFile(ApplyCode);

                //计数
                foreach (DataRow r in dt.Rows)
                {
                    if (fj.ContainsKey(r["DataType"].ToString()) == true)
                    {
                        fj[r["DataType"].ToString()] = Convert.ToInt32(fj[r["DataType"].ToString()]) + 1;
                    }
                }
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (string k in fj.Keys)
                {
                    if (Convert.ToInt32(fj[k]) == 0)
                    {
                        sb.Append(string.Format("、“{0}”", k));
                    }
                }

                if (sb.Length > 0)
                {
                    sb.Remove(0, 1);
                    UIHelp.layerAlert(Page, string.Format("缺少{0}，请先上传再申报！", sb), 5, 0);
                    return;
                }
            }

            string sql = " Update zjs_Apply set ApplyStatus='{0}',ApplyTime='{1}',[OldUnitCheckTime]='{1}',XGR='{2}',XGSJ='{3}' ,[GetDateTime]=null,[GetResult]=null,[GetRemark]=null,[GetMan]=null,[ExamineDatetime]=null,[ExamineResult]=null,[ExamineRemark]=null,[ExamineMan]=null ,[ReportDate] =null,[ReportMan] =null,[ReportCode] =null,[CheckDate] =null,[CheckResult] =null,[CheckRemark] =null,[CheckMan] =null,[ConfirmDate] =null,[ConfirmResult] =null,[ConfirmMan] =null where ApplyCode='{4}';";

            zjs_ApplyMDL _zjs_ApplyMDL = new zjs_ApplyMDL();
            _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.未申报;
            try
            {
                if (ButtonApply.Text == "取消申报")
                {
                    CommonDAL.ExecSQL(string.Format(sql, EnumManager.ZJSApplyStatus.未申报, null, UserName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ViewState["ApplyCode"]));
                    SetButtonEnable(_zjs_ApplyMDL);
                    UIHelp.WriteOperateLog(UserName, UserID, "撤销造价工程师企业信息变更申请成功", string.Format("企业名称从【{0}】变更到【{1}】。", LabelENT_NameFrom, LabelENT_NameTo.Text));
                    UIHelp.layerAlert(Page, "撤销申报成功!", 6, 2000);             
                }
                else
                {
                    CommonDAL.ExecSQL(string.Format(sql, EnumManager.ZJSApplyStatus.已申报, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), UserName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ViewState["ApplyCode"]));
                    _zjs_ApplyMDL.ApplyStatus = EnumManager.ZJSApplyStatus.已申报;
                    SetButtonEnable(_zjs_ApplyMDL);
                    UIHelp.WriteOperateLog(UserName, UserID, "造价工程师企业信息变更申请申报成功", string.Format("企业名称从【{0}】变更到【{1}】。", LabelENT_NameFrom, LabelENT_NameTo.Text));
                    UIHelp.layerAlert(Page, "申报成功!（如果有其他从业人员证书，请同时在从业人员证书管理中发起企业信息变更，以免造成后期无法申请。）", 6,0); 
                    //UIHelp.layerAlert(Page, "申报成功!（如果有其他从业人员证书，请同时在从业人员证书管理中发起企业信息变更，以免造成后期无法申请。）", string.Format("window.location.href='zjsApplyHistory.aspx?c={0}';", ApplyCode)); 
                }

                //绑定附件信息
                BindFile(ViewState["ApplyCode"].ToString());
                SetButtonEnable(_zjs_ApplyMDL);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "造价工程师企业信息变更申报失败", ex);
                return;
            }
        }

        //删除
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {
                CommonDAL.ExecSQL(string.Format("DELETE FROM dbo.zjs_Apply WHERE ApplyCode='{0}'", ViewState["ApplyCode"]));
                CommonDAL.ExecSQL(string.Format("delete  from FileInfo where FileID in(select FileID from [ApplyFile] where applyid='{0}')", ViewState["ApplyCode"]));
                CommonDAL.ExecSQL(string.Format("delete from ApplyFile where ApplyID='{0}' ", ViewState["ApplyCode"]));
                ViewState.Clear();
                BindData();
                UIHelp.WriteOperateLog(UserName, UserID, "造价工程师企业信息变更申请删除成功", string.Format("变更后企业名称：{0}。", LabelENT_NameTo.Text));
                UIHelp.layerAlert(Page, "删除成功！", 6, 2000);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "造价工程师企业信息申报删除失败", ex);
            }
        }

        //展示办理进度
        protected void SetStep(string ApplyStatus)
        {
            step_未申报.Attributes["class"].Replace(" green", "");
            step_已申报.Attributes["class"] = step_已申报.Attributes["class"].Replace(" green", "");
            step_已受理.Attributes["class"] = step_已受理.Attributes["class"].Replace(" green", "");
            step_已审核.Attributes["class"] = step_已审核.Attributes["class"].Replace(" green", "");
            step_已决定.Attributes["class"] = step_已决定.Attributes["class"].Replace(" green", "");

            switch (ApplyStatus)
            {
                case EnumManager.ZJSApplyStatus.未申报:
                case EnumManager.ZJSApplyStatus.已驳回:
                    step_未申报.Attributes["class"] += " green";
                    break;
                case EnumManager.ZJSApplyStatus.已申报:
                    step_已申报.Attributes["class"] += " green";
                    break;
                case EnumManager.ZJSApplyStatus.已受理:
                    step_已受理.Attributes["class"] += " green";
                    break;
                case EnumManager.ZJSApplyStatus.已审核:
                    step_已审核.Attributes["class"] += " green";
                    break;
                case EnumManager.ZJSApplyStatus.已决定:
                    step_已决定.Attributes["class"] += " green";
                    break;
                default:
                    step_未申报.Attributes["class"] += " green";
                    break;
            }
        }

        //操作按钮控制
        protected void SetButtonEnable(zjs_ApplyMDL o)
        {
            SetStep(o == null ? "" : o.ApplyStatus);

            switch (o == null ? "" : o.ApplyStatus)
            {
                case "":
                    ButtonSave.Enabled = true;
                    ButtonApply.Enabled = false;
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;

                    break;
                case EnumManager.ZJSApplyStatus.未申报:
                    ButtonSave.Enabled = false;
                    ButtonApply.Enabled = true;
                    ButtonDelete.Enabled = true;
                    ButtonOutput.Enabled = true;

                    //企业登录
                    if (IfExistRoleID("2") == true)
                    {
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                        ButtonApply.Text = "申 报";
                    }
                    break;
                case EnumManager.ZJSApplyStatus.已申报:
                    ButtonSave.Enabled = false;             
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;

                    //企业
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "取消申报";

                    //受理权限
                    if (IfExistRoleID("20") == true)
                    {
                        divQX.Visible = true;
                    }
                    break;
                case EnumManager.ZJSApplyStatus.已驳回:
                    ButtonSave.Enabled = false;
                    ButtonApply.Enabled = true;
                    ButtonDelete.Enabled = true;
                    ButtonOutput.Enabled = true;
                    ButtonApply.Text = "申 报";

                    //企业登录
                    if (IfExistRoleID("2") == true)
                    {
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                    }

                    if (o.GetDateTime.HasValue == true && o.ExamineDatetime.HasValue == false)//受理驳回
                    {
                        //注册室管理员
                        if (IfExistRoleID("1") == true || IfExistRoleID("15") == true)
                        {
                            UIHelp.SetZJSReturnStatusList(RadComboBoxReturnApplyStatus, o.ApplyStatus);
                            divSendBack.Visible = true;//后退
                        }
                    }      
                    break;
                case EnumManager.ZJSApplyStatus.已受理:
                    //注册室管理员
                    if (IfExistRoleID("1") == true || IfExistRoleID("15") == true)
                    {
                        UIHelp.SetZJSReturnStatusList(RadComboBoxReturnApplyStatus, o.ApplyStatus);
                        divSendBack.Visible = true;//后退
                    }
                    //市级审核
                    if (IfExistRoleID("21") == true)
                    {
                        divQXCK.Visible = true;
                        BttSave.Text = "确认提交";
                    }
                    break;
                case EnumManager.ZJSApplyStatus.已审核:
                    //注册室管理员
                    if (IfExistRoleID("1") == true || IfExistRoleID("15") == true)
                    {
                        UIHelp.SetZJSReturnStatusList(RadComboBoxReturnApplyStatus, o.ApplyStatus);
                        divSendBack.Visible = true;//后退
                    }
                    //市级决定权限
                    if (IfExistRoleID("23") == true)
                    {
                        ButtonDecide.Text = "确认提交";
                        divDecide.Visible = true;
                    }
                    break;
                case EnumManager.ZJSApplyStatus.已决定:
                    //注册室管理员
                    if (IfExistRoleID("1") == true || IfExistRoleID("15") == true)
                    {
                        if (o.ConfirmResult == "不通过")//只有决定不通过才能后退，如果通过了（修改了证书），需要人工后退。
                        {
                            UIHelp.SetZJSReturnStatusList(RadComboBoxReturnApplyStatus, o.ApplyStatus);
                            divSendBack.Visible = true;//后退
                        }
                    }
                    break;
                default:
                    ButtonSave.Enabled = false;
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    ButtonApply.Enabled = false;
                    ButtonApply.Text = "撤销申报";
                    break;
            }
            ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonDelete.CssClass = ButtonDelete.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonOutput.CssClass = ButtonOutput.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonApply.CssClass = ButtonApply.Enabled == true ? "bt_large" : "bt_large btn_no";
        }

        //打印
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            try
            {
                string sourceFile = HttpContext.Current.Server.MapPath("~/Template/造价师表企业信息变更注册申请表.docx");
                string fileName = "北京市二级注册造价工程师企业信息变更申请表";
                var ApplyChange = new
                {
                    ENT_NameFrom = LabelENT_NameFrom.Text,
                    ENT_NameTo = LabelENT_NameTo.Text,                
                };
                //zjs_ApplyMDL _zjs_ApplyMDL = ViewState["zjs_ApplyMDL"] as zjs_ApplyMDL;
                //UnitMDL u = UnitDAL.GetObjectByENT_OrganizationsCode(_zjs_ApplyMDL.ENT_OrganizationsCode);
                
                var o = new List<object>();
                o.Add(ApplyChange);
                var ht = PrintDocument.GetProperties(o);
                ht["ApplyCode"] = ViewState["ApplyCode"];
                ht["ENT_Type"] = LabelENT_Type.Text;
                ht["FR"] = LabelFR.Text;
                ht["LinkMan"] = LabelLinkMan.Text;
                ht["ENT_Telephone"] = LabelENT_Telephone.Text;
                ht["END_Addess"] = LabelEND_Addess.Text;

                //ht["photo"] ="";
                //拿到企业下面人员信息
                DataTable dt = CommonDAL.GetDataTable(string.Format("SELECT row_number()  over(ORDER BY PSN_Name) as rn,PSN_Name,PSN_CertificateNO,PSN_RegisterNo,PSN_RegisteProfession FROM zjs_Apply WHERE ApplyCode= '{0}'", ViewState["ApplyCode"]));
                ht["tableList"] = new List<DataTable> { dt };
                
                //表格的索引
                ht["tableIndex"] = new List<int> { 0 };
                //行的索引
                ht["insertIndex"] = new List<int> { 8 };
                ht["ContainsHeader"] = new List<bool> { false };
                ht["isCtable"] = true;
                System.Collections.Hashtable style = new System.Collections.Hashtable();
                style.Add("Alignment","center");
                Dictionary<int,Dictionary<int,System.Collections.Hashtable>> dic =new Dictionary<int,Dictionary<int,System.Collections.Hashtable>>();
                Dictionary<int,System.Collections.Hashtable> dicColumn=new Dictionary<int,System.Collections.Hashtable>();
                dicColumn.Add(0,style);
                 dicColumn.Add(1,style);
                 dicColumn.Add(2,style);
                 dicColumn.Add(3,style);
                 dicColumn.Add(4, style);
                 dic.Add(0, dicColumn);

                ht["ContextStyle"] = dic;
                PrintDocument.CreateDataToWordByHashtable2(sourceFile, fileName, ht);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "打印造价工程师企业信息变更申请Word失败！", ex);
            }
        }

        //受理
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            DateTime GetDateTime = DateTime.Now;
            string GetMan = UserName;
            string GetResult = RadioButtonListApplyStatus.SelectedValue;
            string GetRemark = TextBoxApplyGetResult.Text.Trim();
            string ApplyStatus = RadioButtonListApplyStatus.SelectedValue == "通过" ? EnumManager.ZJSApplyStatus.已受理 : EnumManager.ZJSApplyStatus.已驳回;
            try
            {
                if (RadioButtonListApplyStatus.SelectedValue == "不通过")//不同意时记录最后驳回意见
                {
                    CommonDAL.ExecSQL(string.Format(@"Update zjs_Apply SET GetDateTime='{0}',GetMan='{1}',GetResult='{2}',GetRemark='{3}',ApplyStatus='{4}',[XGSJ]='{0}',[XGR]='{1}',LastBackResult='{8}' WHERE (ENT_OrganizationsCode='{5}' OR ENT_OrganizationsCode like '________{5}_')  AND ApplyStatus='{6}' and ApplyCode='{7}'"
                        , GetDateTime, GetMan, GetResult, GetRemark, ApplyStatus, ViewState["zzjgdm"].ToString(), EnumManager.ZJSApplyStatus.已申报, ViewState["ApplyCode"]
                        , string.Format("{0}受理环节驳回申请，驳回说明：{1}", GetDateTime.ToString("yyyy-MM-dd HH:mm:ss"), TextBoxApplyGetResult.Text.Trim())));
                }
                else
                {
                    CommonDAL.ExecSQL(string.Format(@"Update zjs_Apply SET GetDateTime='{0}',GetMan='{1}',GetResult='{2}',GetRemark='{3}',ApplyStatus='{4}',[XGSJ]='{0}',[XGR]='{1}' WHERE (ENT_OrganizationsCode='{5}' OR ENT_OrganizationsCode like '________{5}_')  AND ApplyStatus='{6}' and ApplyCode='{7}'", GetDateTime, GetMan, GetResult, GetRemark, ApplyStatus, ViewState["zzjgdm"], EnumManager.ZJSApplyStatus.已申报, ViewState["ApplyCode"]));
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "受理造价工程师企业信息变更申请失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "受理造价工程师企业信息变更申请", string.Format("企业名称从【{0}】变更到【{1}】{2}。受理结果：{3}，受理意见：{4}。", LabelENT_NameFrom, LabelENT_NameTo.Text, LabelPersonCount.Text, GetResult, GetRemark));
            UIHelp.ParentAlert(Page, "受理成功！", true);
            //Response.Redirect(string.Format("~/zjs/zjsBusinessQuery.aspx?id={0}&&type={1}", ViewState["ApplyCode"],"企业信息变更"), true);
        }

        //审核
        protected void BttSave_Click(object sender, EventArgs e)
        {
            DateTime ExamineDatetime = DateTime.Now;
            string ExamineMan = UserName;
            string ExamineResult = RadioButtonListExamineResult.SelectedValue;
            string ExamineRemark = TextBoxExamineRemark.Text.Trim();
            string ApplyStatus = EnumManager.ZJSApplyStatus.已审核 ;

            try
            {
                CommonDAL.ExecSQL(string.Format("Update zjs_Apply SET ExamineDatetime='{0}',ExamineMan='{1}',ExamineResult='{2}',ExamineRemark='{3}',ApplyStatus='{4}',XGR='{1}',XGSJ='{0}' WHERE (ENT_OrganizationsCode='{5}' OR ENT_OrganizationsCode like '________{5}_') AND ApplyStatus='{6}' and ApplyCode='{7}'", ExamineDatetime, ExamineMan, ExamineResult, ExamineRemark, ApplyStatus, ViewState["zzjgdm"], EnumManager.ZJSApplyStatus.已受理, ViewState["ApplyCode"]));
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "审核造价工程师企业信息变更申请失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(UserName, UserID, "审核造价工程师企业信息变更申请", string.Format("企业名称从【{0}】变更到【{1}】{2}。审核结果：{3}，审核意见：{4}。", LabelENT_NameFrom, LabelENT_NameTo.Text, LabelPersonCount.Text, ExamineResult, ExamineRemark));
            UIHelp.ParentAlert(Page, "审核成功！", true);
        }
     
        //市级决定
        protected void ButtonDecide_Click(object sender, EventArgs e)
        {
            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];
            if (_zjs_ApplyMDL.ExamineResult != RadioButtonListDecide.SelectedValue)
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "confirm",
                   string.Format(@"layer.confirm('【决定结果】与【审核结果】不一致，是否真的要继续操作？', {{btn: ['继续审核', '重新审核'],icon:3, title: '警告'}}, function () {{ __doPostBack('Decide', '');}});")
                    , true);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "scrollTo", string.Format("window.setTimeout(function(){{$('#{0}').focus();}},500);", ButtonDecide.ClientID), true);
                return;
            }
            Decide();   
        }

        /// <summary>
        /// 申请单决定
        /// </summary>
        protected void Decide()
        {
            //#region 查询证书是否锁定
            //if (CheckCertLock(_zjs_ApplyMDL.PSN_RegisterNo) == true) return;           
            //#endregion

            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                //更新申请单      //zjs_ApplyDAL.Update(tran, _zjs_ApplyMDL);
                CommonDAL.ExecSQL(tran, string.Format("Update zjs_Apply SET ConfirmDate='{0}',ConfirmMan='{1}',ConfirmResult='{2}',[NoticeDate]='{0}',ApplyStatus='{3}',XGR='{1}',XGSJ='{0}' WHERE (ENT_OrganizationsCode='{4}' OR ENT_OrganizationsCode like '________{4}_') and ApplyCode='{5}'"
                     , DateTime.Now, UserName, RadioButtonListDecide.SelectedValue, EnumManager.ZJSApplyStatus.已决定, ViewState["zzjgdm"], ViewState["ApplyCode"]));

                if (RadioButtonListDecide.SelectedValue == "通过")
                {
                    #region 办结更新证书表

                    string sql = "";

                    //正式表写入历史表                 
                    sql = @"INSERT INTO [dbo].[zjs_Certificate_His]
                                    ([HisID],[PSN_ServerID],[ENT_Name],[ENT_OrganizationsCode],[ENT_City],[END_Addess],[PSN_Name],[PSN_Sex],[PSN_BirthDate],[PSN_National],[PSN_CertificateType],[PSN_CertificateNO],[PSN_GraduationSchool],[PSN_Specialty],[PSN_GraduationTime],[PSN_Qualification],[PSN_Degree],[PSN_MobilePhone],[PSN_Email],[PSN_Telephone],[PSN_RegisteType],[PSN_RegisterNO],[PSN_RegisterCertificateNo],[PSN_RegisteProfession],[PSN_CertificationDate],[PSN_CertificateValidity],[PSN_RegistePermissionDate],[PSN_Level],[ZGZSBH],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[ApplyCATime],[SendCATime],[ReturnCATime],[CertificateCAID],[license_code],[auth_code],[SignCATime],[HisTime])
                                     select newid(),c.[PSN_ServerID],c.[ENT_Name],c.[ENT_OrganizationsCode],c.[ENT_City],c.[END_Addess],c.[PSN_Name],c.[PSN_Sex],c.[PSN_BirthDate],c.[PSN_National],c.[PSN_CertificateType],c.[PSN_CertificateNO],c.[PSN_GraduationSchool],c.[PSN_Specialty],c.[PSN_GraduationTime],c.[PSN_Qualification],c.[PSN_Degree],c.[PSN_MobilePhone],c.[PSN_Email],c.[PSN_Telephone],c.[PSN_RegisteType],c.[PSN_RegisterNO],c.[PSN_RegisterCertificateNo],c.[PSN_RegisteProfession],c.[PSN_CertificationDate],c.[PSN_CertificateValidity],c.[PSN_RegistePermissionDate],c.[PSN_Level],c.[ZGZSBH],c.[CJR],c.[CJSJ],c.[XGR],c.[XGSJ],c.[Valid],c.[Memo],c.[ApplyCATime],c.[SendCATime],c.[ReturnCATime],c.[CertificateCAID],c.[license_code],c.[auth_code],c.[SignCATime],getdate() 
                                    FROM [dbo].[zjs_Certificate] c inner join dbo.[zjs_Apply] a on c.[PSN_RegisterNO] = a.[PSN_RegisterNO]
                                   where a.ApplyCode='{0}' and a.ConfirmResult='通过' and c.[ENT_OrganizationsCode] = a.[ENT_OrganizationsCode]";

                    CommonDAL.ExecSQL(tran, string.Format(sql, ViewState["ApplyCode"]));

                    //更新正式表
                    sql = @"UPDATE [dbo].[zjs_Certificate]
                                            SET [zjs_Certificate].[ENT_Name] = [zjs_ApplyChange].ENT_NameTo                                                                               
                                            ,[zjs_Certificate].[PSN_RegisteType]='{2}'
                                            ,[zjs_Certificate].[PSN_RegistePermissionDate]='{3}'
                                            ,[zjs_Certificate].[XGR] = '{1}' 
                                            ,[zjs_Certificate].[XGSJ] = '{3}' 
                                        FROM [dbo].[zjs_Certificate] inner join dbo.[zjs_Apply]
                                        on [zjs_Certificate].[PSN_RegisterNO] = [zjs_Apply].[PSN_RegisterNO]
                                        inner join [dbo].[zjs_ApplyChange] 
                                        on [zjs_Apply].ApplyID=[zjs_ApplyChange].ApplyID
                                        where [zjs_Apply].ApplyCode='{0}' and [zjs_Apply].ConfirmResult='通过' and [zjs_Certificate].[ENT_OrganizationsCode] = [zjs_Apply].[ENT_OrganizationsCode]";

                    CommonDAL.ExecSQL(tran, string.Format(sql
                        , ViewState["ApplyCode"]
                        , UserName
                        , EnumManager.ZJSApplyTypeCode.变更注册//"02"
                        , DateTime.Now
                        ));


                    //企业表的数据写入历史
                    sql = @"INSERT INTO [dbo].[Unit_His]([HisID],[UnitID],[BeginTime],[EndTime],[ENT_Name],[ENT_OrganizationsCode],[ENT_Economic_Nature],[ENT_Province],[ENT_Province_Code],[ENT_City],[END_Addess],[ENT_Corporate],[ENT_Correspondence],[ENT_Postcode],[ENT_Contact],[ENT_Telephone],[ENT_MobilePhone],[ENT_Type],[ENT_Sort],[ENT_Grade],[ENT_QualificationCertificateNo],[CreditCode],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate])  
                                        SELECT newid() ,[UnitID],[BeginTime],[EndTime],[ENT_Name],[ENT_OrganizationsCode],[ENT_Economic_Nature],[ENT_Province],[ENT_Province_Code],[ENT_City],[END_Addess],[ENT_Corporate],[ENT_Correspondence],[ENT_Postcode],[ENT_Contact],[ENT_Telephone],[ENT_MobilePhone],[ENT_Type],[ENT_Sort],[ENT_Grade],[ENT_QualificationCertificateNo],[CreditCode],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],getdate() 
                                        FROM [dbo].[Unit] where [CreditCode] in(select distinct ENT_OrganizationsCode from [dbo].[zjs_Apply] where ApplyCode='{0}' and ConfirmResult='通过') and [ENT_Name] <>'{1}'";
                    CommonDAL.ExecSQL(tran, string.Format(sql, ViewState["ApplyCode"], LabelENT_NameTo.Text));

                    //修改企业表的数据
                    sql = @"UPDATE [dbo].[Unit]
                                       SET [Unit].[ENT_Name] = a.ENT_NameTo      
                                          ,[Unit].[XGR] = '{1}' 
                                          ,[Unit].[XGSJ] = getdate()   
	                                    from [dbo].[Unit] inner join 
	                                    (
	                                        select distinct zjs_Apply.[ENT_OrganizationsCode], [zjs_ApplyChange].ENT_NameTo
	                                        from dbo.zjs_Apply 
		                                    inner join [dbo].[zjs_ApplyChange] 
                                            on zjs_Apply.ApplyID=[zjs_ApplyChange].ApplyID
	                                        where zjs_Apply.ApplyCode='{0}'  and zjs_Apply.ConfirmResult='通过'
	                                    ) a on [Unit].[CreditCode]=a.[ENT_OrganizationsCode]  and [Unit].[ENT_Name] <>'{2}'";
                    CommonDAL.ExecSQL(tran, string.Format(sql, ViewState["ApplyCode"], UserName, LabelENT_NameTo.Text));

                    #endregion 办结更新证书表
                }

                tran.Commit();
            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "批准造价工程师执业企业变更申请失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(UserName, UserID, "批准造价工程师执业企业变更申请", string.Format("企业名称从【{0}】变更到【{1}】{2}。批准结果：{3}"
                , LabelENT_NameFrom, LabelENT_NameTo.Text, LabelPersonCount.Text, RadioButtonListDecide.SelectedValue));
            UIHelp.ParentAlert(Page, "批准成功！", true);
        }

        //审批节点后退
        protected void ButtonSendBack_Click(object sender, EventArgs e)
        {
            string sql = "";
            zjs_ApplyMDL _zjs_ApplyMDL = (zjs_ApplyMDL)ViewState["zjs_ApplyMDL"];

            if (_zjs_ApplyMDL.ApplyStatus == RadComboBoxReturnApplyStatus.SelectedValue)
            {
                UIHelp.layerAlert(Page, "后退节点与当前申请单所处节点相同，无需后退！");
                return;
            }

            string log = "";
            switch (_zjs_ApplyMDL.ApplyStatus)
            {
                case EnumManager.ZJSApplyStatus.已受理:
                case EnumManager.ZJSApplyStatus.已驳回:
                    log = string.Format("企业名称从【{0}】变更到【{1}】{2}。受理时间：{3}，受理结果：{4}，受理意见：{5}。后退到“{6}”状态。"
                   , LabelENT_NameFrom, LabelENT_NameTo.Text, LabelPersonCount.Text, _zjs_ApplyMDL.GetDateTime, _zjs_ApplyMDL.GetResult, _zjs_ApplyMDL.GetRemark, RadComboBoxReturnApplyStatus.SelectedValue);
                    if (_zjs_ApplyMDL.ApplyStatus == EnumManager.ZJSApplyStatus.已驳回)//取消最后驳回意见
                    {
                        sql = string.Format("Update zjs_Apply SET GetDateTime=null,GetMan=null,GetResult=null,GetRemark=null,LastBackResult=null,[XGSJ]='{0}',[XGR]='{1}',ApplyStatus='{2}' WHERE (ENT_OrganizationsCode='{3}' OR ENT_OrganizationsCode like '________{3}_') and ApplyCode='{4}'"
                        , DateTime.Now, UserName, EnumManager.ZJSApplyStatus.已申报, ViewState["zzjgdm"], ViewState["ApplyCode"]);
                    }
                    else
                    {
                        sql = string.Format("Update zjs_Apply SET GetDateTime=null,GetMan=null,GetResult=null,GetRemark=null,[XGSJ]='{0}',[XGR]='{1}',ApplyStatus='{2}' WHERE (ENT_OrganizationsCode='{3}' OR ENT_OrganizationsCode like '________{3}_') and ApplyCode='{4}'"
                            , DateTime.Now, UserName, EnumManager.ZJSApplyStatus.已申报, ViewState["zzjgdm"], ViewState["ApplyCode"]);
                    }
                    break;
                case EnumManager.ZJSApplyStatus.已审核:
                    log = string.Format("企业名称从【{0}】变更到【{1}】{2}。审核时间：{3}，审核结果：{4}，审核意见：{5}。后退到“{6}”状态。"
                           , LabelENT_NameFrom, LabelENT_NameTo.Text, LabelPersonCount.Text, _zjs_ApplyMDL.ExamineDatetime, _zjs_ApplyMDL.ExamineResult, _zjs_ApplyMDL.ExamineRemark, RadComboBoxReturnApplyStatus.SelectedValue);
                    switch (RadComboBoxReturnApplyStatus.SelectedValue)
                    {
                        case EnumManager.ZJSApplyStatus.已申报:
                            sql = string.Format("Update zjs_Apply SET GetDateTime=null,GetMan=null,GetResult=null,GetRemark=null,ExamineDatetime=null,ExamineMan=null,ExamineResult=null,ExamineRemark=null,[XGSJ]='{0}',[XGR]='{1}',ApplyStatus='{2}' WHERE (ENT_OrganizationsCode='{3}' OR ENT_OrganizationsCode like '________{3}_') and ApplyCode='{4}'"
                            , DateTime.Now, UserName, EnumManager.ZJSApplyStatus.已申报, ViewState["zzjgdm"], ViewState["ApplyCode"]);
                            break;
                        case EnumManager.ZJSApplyStatus.已受理:
                            sql = string.Format("Update zjs_Apply SET ExamineDatetime=null,ExamineMan=null,ExamineResult=null,ExamineRemark=null,[XGSJ]='{0}',[XGR]='{1}',ApplyStatus='{2}' WHERE (ENT_OrganizationsCode='{3}' OR ENT_OrganizationsCode like '________{3}_') and ApplyCode='{4}'"
                            , DateTime.Now, UserName, EnumManager.ZJSApplyStatus.已受理, ViewState["zzjgdm"], ViewState["ApplyCode"]);
                            break;
                    }
                    break;
                case EnumManager.ZJSApplyStatus.已决定:
                    log = string.Format("企业名称从【{0}】变更到【{1}】{2}。决定时间：{3}，决定结果：{4}。后退到“{5}”状态。"
                           , LabelENT_NameFrom, LabelENT_NameTo.Text, LabelPersonCount.Text, _zjs_ApplyMDL.ConfirmDate, _zjs_ApplyMDL.ConfirmResult, RadComboBoxReturnApplyStatus.SelectedValue);
                    switch (RadComboBoxReturnApplyStatus.SelectedValue)
                    {
                        case EnumManager.ZJSApplyStatus.已申报:   
                            sql = string.Format("Update zjs_Apply SET ConfirmDate=null,ConfirmMan=null,ConfirmResult=null,GetDateTime=null,GetMan=null,GetResult=null,GetRemark=null,ExamineDatetime=null,ExamineMan=null,ExamineResult=null,ExamineRemark=null,[XGSJ]='{0}',[XGR]='{1}',ApplyStatus='{2}' WHERE (ENT_OrganizationsCode='{3}' OR ENT_OrganizationsCode like '________{3}_') and ApplyCode='{4}'"
                           , DateTime.Now, UserName, EnumManager.ZJSApplyStatus.已申报, ViewState["zzjgdm"], ViewState["ApplyCode"]);
                            
                            break;
                        case EnumManager.ZJSApplyStatus.已受理:
                           
                            sql = string.Format("Update zjs_Apply SET ConfirmDate=null,ConfirmMan=null,ConfirmResult=null,ExamineDatetime=null,ExamineMan=null,ExamineResult=null,ExamineRemark=null,[XGSJ]='{0}',[XGR]='{1}',ApplyStatus='{2}' WHERE (ENT_OrganizationsCode='{3}' OR ENT_OrganizationsCode like '________{3}_') and ApplyCode='{4}'"
                            , DateTime.Now, UserName, EnumManager.ZJSApplyStatus.已受理, ViewState["zzjgdm"], ViewState["ApplyCode"]);
                            
                            break;
                        case EnumManager.ZJSApplyStatus.已审核:
                            sql = string.Format("Update zjs_Apply SET ConfirmDate=null,ConfirmMan=null,ConfirmResult=null,[XGSJ]='{0}',[XGR]='{1}',ApplyStatus='{2}' WHERE (ENT_OrganizationsCode='{3}' OR ENT_OrganizationsCode like '________{3}_') and ApplyCode='{4}'"
                            , DateTime.Now, UserName, EnumManager.ZJSApplyStatus.已审核, ViewState["zzjgdm"], ViewState["ApplyCode"]);
                            break;
                    }
                    break;
                default:
                    UIHelp.layerAlert(Page, "当前处在无法后退节点！");
                    return;
            }

            try
            {
                CommonDAL.ExecSQL(sql);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "后退造价工程师企业信息变更审批节点失败！", ex);
                return;
            }
            UIHelp.WriteOperateLog(UserName, UserID, "后退造价工程师企业信息变更审批节点", log);
            UIHelp.ParentAlert(Page, "后退成功！", true);
        }

        /// <summary>
        /// 绑定附件
        /// </summary>
        /// <param name="ApplyCode"></param>
        private void BindFile(string ApplyCode)
        {
            DataTable dt_ApplyFile = ApplyFileDAL.GetListByApplyID(ApplyCode);
            DataTable HB_File = dt_ApplyFile.Clone();
            HB_File.Columns["FileUrl"].MaxLength = 8000;
            string DataType = "";
            foreach (DataRow r in dt_ApplyFile.Rows)
            {
                if (r["DataType"].ToString() != DataType)
                {
                    HB_File.ImportRow(r);
                    HB_File.Rows[HB_File.Rows.Count - 1]["FileUrl"] = string.Format("{0}|{1}", r["FileUrl"], r["FileID"]);
                    DataType = r["DataType"].ToString();
                }
                else
                {
                    HB_File.Rows[HB_File.Rows.Count - 1]["FileUrl"] = string.Format("{0},{1}|{2}", HB_File.Rows[HB_File.Rows.Count - 1]["FileUrl"], r["FileUrl"], r["FileID"]);
                }
            }
            RadGridFile.DataSource = HB_File;
            RadGridFile.DataBind();
        }

        //格式化附件
        protected void RadGridFile_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                RadGrid rg = item.FindControl("RadGrid1") as RadGrid;
                DataTable dt_ApplyFile = (ViewState["dt_ApplyFile"] as DataTable).Clone();
                string[] imgurl = RadGridFile.MasterTableView.DataKeyValues[e.Item.ItemIndex]["FileUrl"].ToString().Split(',');
                string[] atrt = null;
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (string s in imgurl)
                {
                    DataRow dr = dt_ApplyFile.NewRow();

                    atrt = s.Split('|');
                    dr["FileUrl"] = atrt[0];
                    dr["FileID"] = atrt[1];
                    dr["ApplyID"] = ApplyCode;
                    dt_ApplyFile.Rows.Add(dr);
                }
                rg.DataSource = dt_ApplyFile;
                rg.DataBind();
            }
        }

        //删除附件
        protected void RadGridFile_DeleteCommand(object source, Telerik.Web.UI.GridCommandEventArgs e)
        {
            //获取类型Id
            string FileID = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["FileID"].ToString();
            string ApplyCode = e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["ApplyID"].ToString();
            try
            {
                ApplyFileDAL.Delete(FileID, ApplyCode);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除造价工程师企业信息变更申请表附件失败！", ex);
                return;
            }
            BindFile(ApplyCode);
            UIHelp.WriteOperateLog(UserName, UserID, "删除造价工程师企业信息变更申请表附件", string.Format("企业：{0}。", LabelENT_NameFrom.Text));
            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
        }

    }
}