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

namespace ZYRYJG.CertifManage
{
    public partial class CompanyNameChange : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "CompanyNameChange.aspx|CertifChange.aspx|CertifChangeCheckConfirm.aspx|ApplyQuerySLR.aspx";
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
                //Response.Redirect("~/ResultInfoPage.aspx?o=企业信息变更业务正在升级调整，调整期暂时无法提供服务，请耐心等待！", true);
                //return;

                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");

                ViewState["CERTIFICATECHANGEID"] = (string.IsNullOrEmpty( Request["a"])==true?Request["a"]: Utility.Cryptography.Decrypt(Request["a"]));//申请ID
                ViewState["zzjgdm"] = Request["zzjgdm"];//申请ID

                #region 根据权限显示面板

                //申请操作权限
                if (IfExistRoleID("2") == true)
                {
                    divQY.Visible = true;
                    Td2.Visible = true;
                    Td3.Visible = true;

                    //企业看不到各级申办人列
                    RadGridCheckHistory.Columns.FindByUniqueName("ActionMan").Visible = false;
                    //divCheckHistory.Visible = false;

                    // 验证工商信息是否通过
                    UnitMDL o = DataAccess.UnitDAL.GetObject(UserID);

                    if (o.ResultGSXX == 0 || o.ResultGSXX == 1)
                    {
                        Response.Write("<script>window.location.href='../Unit/UnitMgr.aspx'</script>");
                    }
                   
                
                }
               
              
                #endregion 根据权限显示面板

                BindData();
            }
            else if (Request["__EVENTTARGET"] == "refreshFile")
            {
                BindFile(ApplyCode);
            }

        }
        /// <summary>
        /// 绑定审核历史记录
        /// </summary>
        /// <param name="certificateChangeId">申请编号</param>
        private void BindCheckHistory(long certificateChangeId)
        {
            DataTable dt = CertificateChangeDAL.GetCheckHistoryList(certificateChangeId);
            RadGridCheckHistory.DataSource = dt;
            RadGridCheckHistory.DataBind();

        }

        //绑定申请记录
        private void BindData()
        {
            CertificateChangeOB ac = null;
            if (ViewState["CERTIFICATECHANGEID"] != null && ViewState["CERTIFICATECHANGEID"].ToString() != "")//查看
            {
                ac = CertificateChangeDAL.GetObject(Convert.ToInt64(ViewState["CERTIFICATECHANGEID"]));              
            }
            else if (ViewState["zzjgdm"] != null && ViewState["zzjgdm"].ToString()!="")//审批
            {
                string sql = "SELECT top 1 * FROM CertificateChange WHERE (UnitCode='{0}' OR UnitCode like '________{0}_') and STATUS='{1}' and [UNITNAME] <>[NEWUNITNAME] and [UNITCODE]=[NEWUNITCODE]  and [APPLYMAN]<>[WORKERNAME] and [APPLYMAN]<>[NEWWORKERNAME]";
                DataTable dt =null;
                if (ValidPageViewLimit(RoleIDs, "CertifChangeCheckConfirm.aspx") == true)//建委审核
                {
                    dt = CommonDAL.GetDataTable(string.Format(sql, ViewState["zzjgdm"].ToString(), EnumManager.CertificateChangeStatus.Applyed));
                }
               
                if (dt != null && dt.Rows.Count > 0)
                {
                    ac = CertificateChangeDAL.GetObject(Convert.ToInt64(dt.Rows[0]["CERTIFICATECHANGEID"]));                
                }
            }
            else if (IfExistRoleID("2") == true)//企业
            {
                string sql = "SELECT top 1 * FROM CertificateChange WHERE (UnitCode='{0}' OR UnitCode like '________{0}_') and [APPLYMAN]<>[WORKERNAME] and [APPLYMAN]<>[NEWWORKERNAME] and [UNITNAME] <>[NEWUNITNAME] and [UNITCODE]=[NEWUNITCODE] and [STATUS] <>'{1}'";
                DataTable dt = CommonDAL.GetDataTable(string.Format(sql, ZZJGDM, EnumManager.CertificateChangeStatus.Noticed));
                if (dt != null && dt.Rows.Count>0)
                {
                    ac = CertificateChangeDAL.GetObject(Convert.ToInt64(dt.Rows[0]["CERTIFICATECHANGEID"]));   
                }
                else//新增
                {                   
                    jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL = jcsjk_QY_ZHXXDAL.GetObjectZZJGDM(ZZJGDM);  //企业资质
                    GSJ_QY_GSDJXXMDL g = DataAccess.UnitDAL.GetObjectUni_scid(SHTJXYDM); //工商信息
                    UnitMDL o = DataAccess.UnitDAL.GetObject(UserID); //企业信息

                    if (_jcsjk_QY_ZHXXMDL !=null )//有企业信息
                    {
                        LabelENT_NameFrom.Text = o.ENT_Name;
                        
                        if (g != null && string.IsNullOrEmpty(g.ENT_NAME) == false)
                        {
                            RadTextBoxENT_NameTo.Text = g.ENT_NAME;
                        }
                        else
                        {
                            RadTextBoxENT_NameTo.Text = _jcsjk_QY_ZHXXMDL.QYMC;
                        }
                        UIHelp.SetReadOnly(RadTextBoxENT_NameTo, true);


                        //按钮
                        SetButtonEnable("");

                        //绑定附件信息
                        BindFile("0");

                        //变更内容提示
                        setFontColor();

                        //人员信息(只变更三类人员，其他从业人员电子证书都不显示企业，无需变更)
                        ViewState["sql"] = string.Format(" and [UnitCode]= '{0}' and UnitName <>'{1}'  and [VALIDENDDATE] > '{2}' and [STATUS] <>'注销' and [STATUS] <>'离京变更' and PostTypeID < 2 ", ZZJGDM, RadTextBoxENT_NameTo.Text, DateTime.Now.ToString("yyyy-MM-dd"));
                        DataTable dtPerson = CommonDAL.GetDataTable(string.Format("SELECT * FROM [VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE] WHERE [UnitCode]= '{0}' and UnitName <>'{1}'  and [VALIDENDDATE] > '{2}' and [STATUS] <>'注销' and [STATUS] <>'离京变更' and PostTypeID < 2 order by PostTypeID,PostID", ZZJGDM, RadTextBoxENT_NameTo.Text, DateTime.Now.ToString("yyyy-MM-dd")));
                        RadGridPerson.DataSource = dtPerson;
                        LabelPersonCount.Text = string.Format("(共计{0}人)", dtPerson.Rows.Count);
                        ViewState["PersonCount"] = dtPerson.Rows.Count;

                        foreach (DataRow r in dtPerson.Rows)
                        {
                            if (r["UnitName"].ToString() != RadTextBoxENT_NameTo.Text)
                            {
                                LabelENT_NameFrom.Text = r["UnitName"].ToString();
                                break;
                            }
                         
                        }                     
                        
                        return;
                    }
                    else //无企业资质
                    {
                     
                        //企业信息
                        LabelENT_NameFrom.Text =  o.ENT_Name;

                        RadTextBoxENT_NameTo.Text = g.ENT_NAME;
                        UIHelp.SetReadOnly(RadTextBoxENT_NameTo, true);

                        //人员信息
                        ViewState["sql"] = string.Format(" and [UnitCode]= '{0}' and UnitName <>'{1}'  and [VALIDENDDATE] > '{2}' and [STATUS] <>'注销' and [STATUS] <>'离京变更' and PostTypeID < 2 ", ZZJGDM, RadTextBoxENT_NameTo.Text, DateTime.Now.ToString("yyyy-MM-dd"));
                        DataTable dtPerson = CommonDAL.GetDataTable(string.Format("SELECT * FROM [VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE] WHERE [UnitCode]= '{0}' and UnitName <>'{1}'  and [VALIDENDDATE] > '{2}' and [STATUS] <>'注销' and [STATUS] <>'离京变更' and PostTypeID < 2 order by PostTypeID,PostID", ZZJGDM, RadTextBoxENT_NameTo.Text, DateTime.Now.ToString("yyyy-MM-dd")));
                        RadGridPerson.DataSource = dtPerson;
                        LabelPersonCount.Text = string.Format("(共计{0}人)", dtPerson.Rows.Count);
                        ViewState["PersonCount"] = dtPerson.Rows.Count;

                        foreach (DataRow r in dtPerson.Rows)
                        {
                            if (r["UnitName"].ToString() != RadTextBoxENT_NameTo.Text)
                            {
                                LabelENT_NameFrom.Text = r["UnitName"].ToString();
                                break;
                            }

                        }
                       
                        //按钮
                        SetButtonEnable("");

                        //绑定附件信息
                        BindFile("0");

                        //变更内容提示
                        setFontColor();

                      
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
                //变更前
                LabelENT_NameFrom.Text = ac.UnitName;


                //变更后名字
                RadTextBoxENT_NameTo.Text = ac.NewUnitName;

                //只读
                UIHelp.SetReadOnly(RadTextBoxENT_NameTo, true);

                CertificateChangeOB a = CertificateChangeDAL.GetObject(ac.CertificateChangeID.Value);
                ViewState["zzjgdm"] = a.UnitCode;

                ViewState["CertificateChangeOB"] = a;

                SetButtonEnable(a.Status);                    
 

                //附件申请号
                ViewState["ApplyCode"] = a.ApplyCode;

                //绑定附件信息
                BindFile(a.ApplyCode);

                //审核历史
                BindCheckHistory(a.CertificateChangeID.Value);

                DataTable dtPerson = CommonDAL.GetDataTable(string.Format("SELECT * FROM [View_CERTIFICATECHANGE] WHERE [ApplyCode]= '{0}'  order by PostTypeID,PostID", ViewState["ApplyCode"]));
                RadGridPerson.DataSource = dtPerson;
                LabelPersonCount.Text = string.Format("（共计{0}人）", dtPerson.Rows.Count);

                if (IfExistRoleID("2") == true
                     && (a.Status == EnumManager.CertificateChangeStatus.NewSave || a.Status == EnumManager.CertificateChangeStatus.SendBack)
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
            if (LabelENT_NameFrom.Text != RadTextBoxENT_NameTo.Text)
            {
                RadTextBoxENT_NameTo.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                RadTextBoxENT_NameTo.ForeColor = System.Drawing.Color.Black;
            }          
        }

        //保存
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (int.Parse(ViewState["PersonCount"].ToString()) == 0)
            {
                UIHelp.layerAlert(Page, "无可申请人员证书信息，请核对企业资质信息是否首先做了变更，没有资质企业直接修改办事大厅中的企业信息！");
                return;
            }

            if (LabelENT_NameFrom.Text.Trim() == RadTextBoxENT_NameTo.Text.Trim()
             )
            {
                UIHelp.layerAlert(Page, "变更前后信息没有变化，无法申请！");
                return;
            }

            //证书在办续期申请数量校验
            DataTable dtContinueApply = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.VIEW_CERTIFICATECONTINUE", "CertificateCode", string.Format(" and CertificateID in(select CertificateID from dbo.VIEW_CERTIFICATE_LEFT_CERTIFICATECHANGE where UnitCode ='{0}') and [STATUS] <> '{1}' ", ZZJGDM, EnumManager.CertificateContinueStatus.Decided), "CertificateID");
            if (dtContinueApply != null && dtContinueApply.Rows.Count > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataRow r in dtContinueApply.Rows)
                {
                    sb.Append("，").Append(r["CertificateCode"].ToString());
                }
                if (sb.Length > 0) sb.Remove(0, 1);
                UIHelp.layerAlert(Page, string.Format("查询结果存在{0}本正在办理续期的证书，未办结不能同时申请变更。证书编号“{1}”。", dtContinueApply.Rows.Count.ToString(), sb.ToString()));
                return;
            }

            //证书在办变更申请数量校验
            DataTable dtChangeApply = CommonDAL.GetDataTable(0, int.MaxValue - 1, "dbo.VIEW_CERTIFICATECHANGE", "CertificateCode", string.Format(" and UnitCode ='{0}' and [STATUS] <> '{1}' "
                , ZZJGDM, EnumManager.CertificateChangeStatus.Noticed), "CertificateID");
            if (dtChangeApply != null && dtChangeApply.Rows.Count > 0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataRow r in dtChangeApply.Rows)
                {
                    sb.Append("，").Append(r["CertificateCode"].ToString());
                }
                if (sb.Length > 0) sb.Remove(0, 1);
                UIHelp.layerAlert(Page, string.Format("查询结果存在{0}本正在办理变更的证书，未办结不能同时申请企业信息变更。证书编号“{1}”。", dtChangeApply.Rows.Count.ToString(), sb.ToString()));
                return;
            }

            //证书待 审批数量校验
            DataTable dtCheckingCert = CommonDAL.GetDataTable(string.Format("select count(*) CerCount,max([CERTIFICATECODE]) as CERTIFICATECODE  from [CERTIFICATE] where UnitCode ='{0}' and ([STATUS]='{1}' or [STATUS]='{2}') ", ZZJGDM, EnumManager.CertificateUpdateType.WaitCheck, EnumManager.CertificateUpdateType.EnterWaitCheck));
            if (dtCheckingCert != null && dtCheckingCert.Rows.Count > 0 && Convert.ToInt32(dtCheckingCert.Rows[0]["CerCount"])>0)
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (DataRow r in dtChangeApply.Rows)
                {
                    sb.Append("，").Append(r["CertificateCode"].ToString());
                }
                if (sb.Length > 0) sb.Remove(0, 1);
                UIHelp.layerAlert(Page, string.Format("查询到企业存在证书编号为：{0}等{1}本证书刚刚编号，正在待建委审核，此时不能申请企业信息变更，请等待审批通过后再申请。", dtCheckingCert.Rows[0]["CERTIFICATECODE"], dtCheckingCert.Rows[0]["CerCount"]));
                return;
            }

            try
            {
                if (ViewState["ApplyCode"] == null)
                {
                    ViewState["ApplyCode"] = UIHelp.GetNextBatchNumber("BGSQ");
                }
                CertificateChangeOB certhfchange = new CertificateChangeOB();
                certhfchange.ApplyCode = ViewState["ApplyCode"].ToString();//申请批次号
                certhfchange.ChangeType = "京内变更";   //变更类型 
                certhfchange.UnitName = LabelENT_NameFrom.Text.Trim();   //原单位名称
                certhfchange.NewUnitName = RadTextBoxENT_NameTo.Text.Trim();//现单位名称
                certhfchange.UnitCode = ZZJGDM; //原单位组织机构代码
                certhfchange.NewUnitCode = ZZJGDM;//现单位组织机构代码                   
                certhfchange.ApplyDate = DateTime.Now;
                certhfchange.ApplyMan = UserName;          //变更申请人
                certhfchange.CreatePersonID = 0;    //创建人
                certhfchange.CreateTime = DateTime.Now;  //创建时间
                certhfchange.LinkWay = "";     //联系方式
                certhfchange.Status = EnumManager.CertificateChangeStatus.NewSave;  //状态
                certhfchange.ModifyPersonID = 0;   //最后修改人
                certhfchange.ModifyTime = certhfchange.CreateTime;   //最后修改时间
                certhfchange.DealWay = ""; //证书处理方式

                //申请表
                CertificateChangeDAL.InsertBatch(certhfchange, ViewState["sql"].ToString());

                SetButtonEnable(EnumManager.CertificateChangeStatus.NewSave);

                //绑定附件信息
                BindFile(ViewState["ApplyCode"].ToString());

                UIHelp.WriteOperateLog(UserName, UserID, "企业信息变更申请保存成功", string.Format("变更后企业名称：{0}。", RadTextBoxENT_NameTo.Text));
                UIHelp.layerAlert(Page, "保存成功，请将盖章签字后的申请表扫描上传！（如果有其他二级注册建造师证书，请同时在二级建造师注册菜单中发起企业信息变更，以免造成后期无法申请。）");

            }
            catch (Exception ex)
            {
               
                UIHelp.WriteErrorLog(Page, "保存企业信息变更申请失败", ex);
            }

            BindData();
        }

        //申报
        protected void ButtonApply_Click(object sender, EventArgs e)
        {
            if (ButtonApply.Text != "撤销申报")
            {
                //必须上传附件集合
                System.Collections.Hashtable fj = new System.Collections.Hashtable{
                {EnumManager.FileDataTypeName.企业信息变更证明,0},
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

            string sql = " Update [CERTIFICATECHANGE] set [Status]='{0}',[APPLYDATE]='{1}',[MODIFYPERSONID]='{2}',[MODIFYTIME]='{3}' ,[GETDATE]=null,[GETRESULT]=null,[GETMAN]=null,[GETCODE]=null,[CHECKDATE]=null,[CHECKRESULT]=null,[CHECKMAN]=null,[CHECKCODE]=null,[CONFRIMDATE]=null,[CONFRIMRESULT]=null,[CONFRIMMAN]=null,[CONFRIMCODE]=null,[NOTICEDATE]=null,[NOTICERESULT]=null,[NOTICEMAN]=null,[NOTICECODE]=null where ApplyCode='{4}';";

            try
            {
               
                if (ButtonApply.Text == "撤销申报")
                {
                    CommonDAL.ExecSQL(string.Format(sql, EnumManager.CertificateChangeStatus.NewSave, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ViewState["ApplyCode"]));
                    SetButtonEnable(EnumManager.CertificateChangeStatus.NewSave);
                    UIHelp.WriteOperateLog(UserName, UserID, "企业信息变更申请撤销成功", string.Format("变更后企业名称：{0}。", RadTextBoxENT_NameTo.Text));
                    UIHelp.layerAlert(Page, "撤销申报成功！", 6, 2000);
                }
                else
                {
                    CommonDAL.ExecSQL(string.Format(sql, EnumManager.CertificateChangeStatus.Applyed, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), 0, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), ViewState["ApplyCode"]));
                    SetButtonEnable(EnumManager.CertificateChangeStatus.Applyed);
                    UIHelp.WriteOperateLog(UserName, UserID, "企业信息变更申请申报成功", string.Format("变更后企业名称：{0}。", RadTextBoxENT_NameTo.Text));
                    UIHelp.layerAlert(Page, "申报成功！（如果有其他二级注册建造师证书，请同时在二级建造师注册菜单中发起企业信息变更，以免造成后期无法申请。）", 6, 2000);
                   
                }

                BindData();

                //绑定附件信息
                BindFile(ViewState["ApplyCode"].ToString());
                //审核历史
                BindCheckHistory((ViewState["CertificateChangeOB"] as CertificateChangeOB).CertificateChangeID.Value);

            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "企业信息变更申报失败", ex);
                return;
            }
           
        }

        //删除
        protected void ButtonDelete_Click(object sender, EventArgs e)
        {
            try
            {

                //备份删除
                string HISsql = @"INSERT INTO DBO.CERTIFICATECHANGE_DEL (CERTIFICATECHANGEID,CERTIFICATEID,CHANGETYPE,WORKERNAME,SEX,BIRTHDAY,CONFERDATE,VALIDSTARTDATE,VALIDENDDATE,CONFERUNIT,DEALWAY,OLDUNITADVISE,NEWUNITADVISE,OLDCONFERUNITADVISE,NEWCONFERUNITADVISE,APPLYDATE,APPLYMAN,APPLYCODE,[GETDATE],GETRESULT,GETMAN,GETCODE,CHECKDATE,CHECKRESULT,CHECKMAN,CHECKCODE,CONFRIMDATE,CONFRIMRESULT,CONFRIMMAN,CONFRIMCODE,NOTICEDATE,NOTICERESULT,NOTICEMAN,NOTICECODE,STATUS,CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,UNITNAME,NEWUNITNAME,UNITCODE,NEWUNITCODE,WORKERCERTIFICATECODE,LINKWAY,NEWWORKERCERTIFICATECODE,NEWWORKERNAME,NEWSEX,NEWBIRTHDAY,SHEBAOCHECK,DELTIME )
                                    select CERTIFICATECHANGEID,CERTIFICATEID,CHANGETYPE,WORKERNAME,SEX,BIRTHDAY,CONFERDATE,VALIDSTARTDATE,VALIDENDDATE,CONFERUNIT,DEALWAY,OLDUNITADVISE,NEWUNITADVISE,OLDCONFERUNITADVISE,NEWCONFERUNITADVISE,APPLYDATE,APPLYMAN,APPLYCODE,[GETDATE],GETRESULT,GETMAN,GETCODE,CHECKDATE,CHECKRESULT,CHECKMAN,CHECKCODE,CONFRIMDATE,CONFRIMRESULT,CONFRIMMAN,CONFRIMCODE,NOTICEDATE,NOTICERESULT,NOTICEMAN,NOTICECODE,STATUS,CREATEPERSONID,CREATETIME,MODIFYPERSONID,MODIFYTIME,UNITNAME,NEWUNITNAME,UNITCODE,NEWUNITCODE,WORKERCERTIFICATECODE,LINKWAY,NEWWORKERCERTIFICATECODE,NEWWORKERNAME,NEWSEX,NEWBIRTHDAY,SHEBAOCHECK,getdate() 
                                    FROM DBO.CERTIFICATECHANGE where ApplyCode='{0}'";
                CommonDAL.ExecSQL(string.Format(HISsql, ViewState["ApplyCode"]));
                CommonDAL.ExecSQL(string.Format("DELETE FROM dbo.[CERTIFICATECHANGE] WHERE ApplyCode='{0}'", ViewState["ApplyCode"]));
                CommonDAL.ExecSQL(string.Format("delete from ApplyFile where ApplyID='{0}' ", ViewState["ApplyCode"]));
                CommonDAL.ExecSQL(string.Format("delete  from FileInfo where FileID in(select FileID from [ApplyFile] where applyid='{0}')", ViewState["ApplyCode"]));

                //审核历史
                BindCheckHistory((ViewState["CertificateChangeOB"] as CertificateChangeOB).CertificateChangeID.Value);
                ViewState.Clear();
                BindData();
                ViewState["dt_ApplyFile"] = ApplyFileDAL.GetListByApplyID("1");
                UIHelp.WriteOperateLog(UserName, UserID, "企业信息变更申请删除成功", string.Format("变更后企业名称：{0}。", RadTextBoxENT_NameTo.Text));
                UIHelp.layerAlert(Page, "删除成功！", 6, 2000);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "企业信息变更申请删除失败", ex);
            }
        }

        //展示办理进度
        protected void SetStep(string Status)
        {
            step_未申报.Attributes["class"] = step_未申报.Attributes["class"].Replace(" green", "");
            step_已申报.Attributes["class"] = step_已申报.Attributes["class"].Replace(" green", "");
            step_已告知.Attributes["class"] = step_已告知.Attributes["class"].Replace(" green", "");


            switch (Status)
            {
                case EnumManager.CertificateChangeStatus.NewSave:
                case EnumManager.CertificateChangeStatus.SendBack:
                    step_未申报.Attributes["class"] += " green";
                    break;
                case EnumManager.CertificateChangeStatus.Applyed:
                    step_已申报.Attributes["class"] += " green";
                    break;
                case EnumManager.CertificateChangeStatus.Noticed:
                    step_已告知.Attributes["class"] += " green";
                    break;               
                default:
                    step_未申报.Attributes["class"] += " green";
                    break;
            }
        }

        //操作按钮控制
        protected void SetButtonEnable(string ApplyStatus)
        {
            SetStep(ApplyStatus);
            if (IfExistRoleID("2") == true)//企业
            {
                divQY.Visible = true;
            }
            switch (ApplyStatus)
            {
                case "":
                    ButtonSave.Enabled = true;
                    ButtonApply.Enabled = false;
                    ButtonApply.Text = "申 报";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    trFuJanTitel.Visible = false;
                    trFuJan.Visible = false;
                  
                    break;
                case EnumManager.CertificateChangeStatus.NewSave:
                    ButtonSave.Enabled = false;
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "申 报";
                    ButtonDelete.Enabled = true;
                    ButtonOutput.Enabled = true;

                    if (IfExistRoleID("2") == true)//企业
                    {
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                    }                   

                    break;
                case EnumManager.CertificateChangeStatus.Applyed:
                    ButtonSave.Enabled = false;
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "撤销申报";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;

                    trFuJanTitel.Visible = false;
                    trFuJan.Visible = false;

                    //建委审核
                    if (ValidPageViewLimit(RoleIDs, "CertifChangeCheckConfirm.aspx") == true)
                    {
                        divQX.Visible = true;
                    }
                    break;
                case EnumManager.CertificateChangeStatus.SendBack:
                    ButtonSave.Enabled = true;
                    ButtonApply.Enabled = true;
                    ButtonApply.Text = "申 报";
                    ButtonDelete.Enabled = true;
                    ButtonOutput.Enabled = true;
                    if (IfExistRoleID("2") == true)//企业
                    {
                        trFuJanTitel.Visible = true;
                        trFuJan.Visible = true;
                    }      
                    break;
                default:
                    ButtonSave.Enabled = false;
                    ButtonApply.Enabled = false;
                    ButtonApply.Text = "撤销申报";
                    ButtonDelete.Enabled = false;
                    ButtonOutput.Enabled = false;
                    break;
            }

            ButtonSave.CssClass = ButtonSave.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonApply.CssClass = ButtonApply.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonDelete.CssClass = ButtonDelete.Enabled == true ? "bt_large" : "bt_large btn_no";
            ButtonOutput.CssClass = ButtonOutput.Enabled == true ? "bt_large" : "bt_large btn_no";
        }

        //打印
        protected void ButtonOutput_Click(object sender, EventArgs e)
        {
            try
            {

                string sourceFile = HttpContext.Current.Server.MapPath("~/Template/变更申请表-企业信息变更.docx");
                string fileName = "北京市住房城乡建设行业从业人员企业信息变更申请表";

                CertificateChangeOB c = ViewState["CertificateChangeOB"] as CertificateChangeOB;

                UnitMDL u = UnitDAL.GetObjectByENT_OrganizationsCode(c.UnitCode);
 
                //创建一个匿名类用来存企业变更前和变更后信息
                var ApplyChange = new
                {
                    ENT_NameFrom = LabelENT_NameFrom.Text,
                    ENT_NameTo = RadTextBoxENT_NameTo.Text.Trim(),
                    ApplyCode = c.ApplyCode,
                    ApplyDate = c.ApplyDate.Value.ToString("yyyy-MM-dd"),
                    ENT_Contact = u.ENT_Contact,
                    ENT_Telephone = u.ENT_Telephone,
                };
               
                var o = new List<object>();
                o.Add(ApplyChange);
                var ht = PrintDocument.GetProperties(o);
        
                //拿到企业下面人员信息
                DataTable dt = CommonDAL.GetDataTable(string.Format("SELECT PostName,[CERTIFICATECODE],case convert(varchar(10),[ValidEndDate],21) when '2050-01-01' then '当前有效证书' else convert(varchar(10),[ValidEndDate],21) end as ValidEndDate,[WORKERNAME],[WORKERCERTIFICATECODE] FROM VIEW_CERTIFICATECHANGE WHERE ApplyCode= '{0}' order by PostTypeID,PostID", ViewState["ApplyCode"]));
                ht["tableList"] = new List<DataTable> { dt };
                
                //表格的索引
                ht["tableIndex"] = new List<int> { 0 };
                //行的索引
                ht["insertIndex"] = new List<int> { 8 };
                ht["ContainsHeader"] = new List<bool> { false };
                ht["isCtable"] = true;
                PrintDocument.CreateDataToWordByHashtable2(sourceFile, fileName, ht);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "打印企业信息变更申请Word失败！", ex);
            }

        }

        //建委审核
        protected void BtnCheck_Click(object sender, EventArgs e)
        {
            DataTable dt = CommonDAL.GetDataTable(string.Format("SELECT * FROM VIEW_CERTIFICATECHANGE WHERE ApplyCode= '{0}' order by PostTypeID,PostID", ViewState["ApplyCode"]));
         
            DBHelper dbhelper = new DBHelper();
            DbTransaction dtr = dbhelper.BeginTransaction();
            try
            {
                string bgslp = UIHelp.GetNextBatchNumber(dtr, "BGSL");//变更受理编批号
                string bgsh = UIHelp.GetNextBatchNumber(dtr, "BGSH"); //变更审核编批号
                string bgjd = UIHelp.GetNextBatchNumber(dtr, "BGJD"); //变更决定编批号
                string bggz = UIHelp.GetNextBatchNumber(dtr, "BGGZ"); //变更告知编批号

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    
                    CertificateChangeOB certhfchange = CertificateChangeDAL.GetObject(null, (long)dt.Rows[i]["CertificateChangeID"]);
                    if (RadioButtonListApplyStatus.SelectedValue == "通过")
                    {
                        certhfchange.GetDate = DateTime.Now;   //变更受理时间
                        certhfchange.GetResult = "通过";     //变更受理结论
                        certhfchange.GetMan = PersonName;    //变更受理人
                        certhfchange.GetCode = bgslp;//变更受理编批号
                        certhfchange.CheckDate = DateTime.Now;  //变更审核时间
                        certhfchange.CheckResult = "通过";  //变更审核结论
                        certhfchange.CheckMan = PersonName; //变更审核人
                        certhfchange.CheckCode = bgsh;    //变更审核批号
                        certhfchange.DealWay = "重新制作证书";     //证书处理方式 
                        certhfchange.ConfrimDate = DateTime.Now;  //变更决定时间 
                        certhfchange.ConfrimResult = "通过";  //变更决定结论
                        certhfchange.ConfrimMan = PersonName;   //变更决定人
                        certhfchange.ConfrimCode = bgjd;   //变更决定批号
                        certhfchange.Status = EnumManager.CertificateChangeStatus.Noticed;     //告知状态
                        certhfchange.NoticeDate = DateTime.Now; //变更告知时间 
                        ViewState["rq"] = certhfchange.NoticeDate;
                        certhfchange.NoticeResult = "通过";  //变更告知结论
                        certhfchange.NoticeMan = PersonName;    //变更告知人
                        certhfchange.NoticeCode = bggz;   //变更告知批号
                        certhfchange.ModifyPersonID = PersonID;    //最后修改人
                        certhfchange.ModifyTime = DateTime.Now; ;   //最后修改时间

                        //修该变更记录
                        CertificateChangeDAL.Update(dtr, certhfchange);

                      

                        //修改原表数据
                        CertificateOB certificateob = CertificateDAL.GetObject(certhfchange.CertificateID.Value);
                        certificateob.UnitName = certhfchange.NewUnitName;   //工作单位    
                        certificateob.ApplyMan = certhfchange.ApplyMan;//申请人
                        certificateob.ModifyPersonID = certhfchange.ModifyPersonID;  //最后修改人
                        certificateob.ModifyTime = DateTime.Now;   //最后修改时间
                        certificateob.CheckDate = certhfchange.CheckDate;    //审批时间
                        certificateob.CheckMan = certhfchange.CheckMan;      //审批人
                        certificateob.CheckAdvise = certhfchange.CheckResult;//审批意见
                        certificateob.Status = certhfchange.ChangeType;      //证书更新状态（变更类型
                        if (certificateob.UnitCode == certhfchange.UnitCode)//申办途中变更了单位，不再更新企业名称。
                        {
                            //根据证书id向历史表插入历史数据
                            CertificateHistoryDAL.InsertChangeHistory(dtr, certhfchange.CertificateID.Value);

                            CertificateDAL.Update(dtr, certificateob);
                        }

                        if(i == 0)
                        {
                            UnitMDL u = UnitDAL.GetObjectByENT_OrganizationsCode(certhfchange.UnitCode);
                            if(u.ENT_Name != certhfchange.NewUnitName)
                            {
                                //企业表的数据写入历史
                                string sql = @"INSERT INTO [dbo].[Unit_His]([HisID],[UnitID],[BeginTime],[EndTime],[ENT_Name],[ENT_OrganizationsCode],[ENT_Economic_Nature],[ENT_Province],[ENT_Province_Code],[ENT_City],[END_Addess],[ENT_Corporate],[ENT_Correspondence],[ENT_Postcode],[ENT_Contact],[ENT_Telephone],[ENT_MobilePhone],[ENT_Type],[ENT_Sort],[ENT_Grade],[ENT_QualificationCertificateNo],[CreditCode],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],[GteDate])  
                                        SELECT newid() ,[UnitID],[BeginTime],[EndTime],[ENT_Name],[ENT_OrganizationsCode],[ENT_Economic_Nature],[ENT_Province],[ENT_Province_Code],[ENT_City],[END_Addess],[ENT_Corporate],[ENT_Correspondence],[ENT_Postcode],[ENT_Contact],[ENT_Telephone],[ENT_MobilePhone],[ENT_Type],[ENT_Sort],[ENT_Grade],[ENT_QualificationCertificateNo],[CreditCode],[CJR],[CJSJ],[XGR],[XGSJ],[Valid],[Memo],getdate() 
                                        FROM [dbo].[Unit] where [ENT_OrganizationsCode] ='{0}'";
                                CommonDAL.ExecSQL(dtr, string.Format(sql, certhfchange.UnitCode));

                                //更新企业表
                                u.ENT_Name = certhfchange.NewUnitName;
                                u.XGR = UserName;
                                u.XGSJ = DateTime.Now;
                                UnitDAL.Update(dtr, u);
                            }
                        }
                    }
                    else
                    {
                        certhfchange.Status = EnumManager.CertificateChangeStatus.SendBack;
                        certhfchange.GetDate = DateTime.Now;   //变更受理时间
                        certhfchange.GetResult = TextBoxCheckResult.Text.Trim();     //变更受理结论
                        certhfchange.GetMan = PersonName;    //变更受理人
                        certhfchange.GetCode = bgslp;//变更受理编批号

                        //修该变更记录
                        CertificateChangeDAL.Update(dtr, certhfchange);
                    }
                }

                dtr.Commit();

                UIHelp.WriteOperateLog(PersonName, UserID, "审查决定从业人员企业信息变更", string.Format("处理方式：变更告知批号：{0}；证书数量：{1}本。变更内容：从“{2}”变为“{3}”"
               , bggz, dt.Rows.Count, dt.Rows[0]["UnitName"], dt.Rows[0]["NewUnitName"]));
            }
            catch (Exception ex)
            {
                dtr.Rollback();
                UIHelp.WriteErrorLog(Page, "审查决定失败！", ex);
                return;
            }

            UIHelp.layerAlert(Page, "审查决定成功", "hideIfam(true);");
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
                UIHelp.WriteErrorLog(Page, "删除企业信息变更申请表附件失败！", ex);
                return;
            }
            BindFile(ApplyCode);
            UIHelp.WriteOperateLog(UserName, UserID, "企业信息变更申请表附件删除成功", string.Format("企业：{0}。", LabelENT_NameFrom.Text));
            UIHelp.layerAlert(Page, "删除成功！", 0, 2000);
        }


    }
}