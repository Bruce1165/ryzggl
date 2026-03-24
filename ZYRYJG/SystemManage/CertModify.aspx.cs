using DataAccess;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.SystemManage
{
    public partial class CertModify : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "SystemManage/CertMgr.aspx";
            }
        }
       // public DataTable dt2;

        public string GetRegisteTypeCode(string type)
        {
            //string bgzcurl="";
            //if (ViewState["ApplyType"].ToString() == "c")
            //    bgzcurl= "ApplyChange.aspx";//变更注册
            //if (ViewState["ApplyType"].ToString() == "j")
            //    bgzcurl= "ApplyChangePerson.aspx";//变更注册
            switch (type)
            {
              
                case "初始注册":
                    return "01";//初始注册
                case "变更注册":
                    return "02";//变更注册
                case "延续注册":
                    return "03";//延续注册
                case "增项注册":
                    return "04";//增项注册
                case "重新注册":
                    return "05";//重新注册
                case "遗失补办":
                    return "06";//遗失补办
                case "注销":
                    return "07";//注销注册
                default:
                    throw new Exception("非法的注册类型。");
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        { 

            if (!IsPostBack)
            {
               
                //注册二级建造师人员基础信息
                COC_TOW_Person_BaseInfoMDL o = COC_TOW_Person_BaseInfoDAL.GetObject(Request["o"]);
                //注册二级建造师注册专业
                   DataTable dt = COC_TOW_Register_ProfessionDAL.GetListByPSN_ServerID(o.PSN_ServerID);
                //COC_TOW_Register_ProfessionMDL a = COC_TOW_Register_ProfessionDAL.GetObjectPSN_ServerID(o.PSN_ServerID);
                if (o != null)
                {
                    //绑定注册信息
                    ViewState["COC_TOW_Person_BaseInfoMDL"] = o;
                    UIHelp.SetData(EditTable, o, false);
                    switch (o.PSN_RegisteType)
                    {
                        case "01":
                            TextBoxPSN_RegisteType.Text = "初始注册";
                            break;
                        case "02":
                            TextBoxPSN_RegisteType.Text = "变更注册";
                            break;
                        case "03":
                            TextBoxPSN_RegisteType.Text = "延续注册";
                            break;
                        case "04":
                            TextBoxPSN_RegisteType.Text = "增项注册";
                            break;
                        case "05":
                            TextBoxPSN_RegisteType.Text = "重新注册";
                            break;
                        case "06":
                            TextBoxPSN_RegisteType.Text = "遗失补办";
                            break;
                        case "07":
                            TextBoxPSN_RegisteType.Text = "注销";
                            break;
                    }
                    //RadDatePickerPSN_CertificationDate.SelectedDate = Convert.ToDateTime(o.PSN_CertificationDate).ToString("yyyy-MM-dd"));
                    TextBoxPSN_CertificateValidity.Text = ((DateTime)o.PSN_CertificateValidity).ToString("yyyy-MM-dd");//证件有效期
                    //RadDatePickerPSN_RegistePermissionDate.SelectedDate = ((DateTime)o.PSN_RegistePermissionDate).ToString("yyyy-MM-dd");

                    ImgCode.Src = UIHelp.ShowFaceImage(o.PSN_CertificateNO);
                   


                }
                #region
                
                if (dt != null && dt.Rows.Count > 0)
                {
                    ViewState["DataTable"] = dt;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        switch (dt.Rows[i]["PRO_Profession"].ToString())
                        {
                            case "水利":
                                TextBoxPRO_ProfessionSL.Text = "水利";
                                RadDatePickerPRO_ValidityBeginSL.SelectedDate = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityBegin"]);
                                RadDatePickerPRO_ValidityEndSL.SelectedDate = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityEnd"]);
                                break;
                            case "建筑":
                                TextBoxPRO_ProfessionJZ.Text = "建筑";
                                RadDatePickerPRO_ValidityBeginJZ.SelectedDate = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityBegin"]);
                                RadDatePickerPRO_ValidityEndJZ.SelectedDate = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityEnd"]);
                                break;
                            case "机电":
                                TextBoxPRO_ProfessionJD.Text = "机电";
                                RadDatePickerPRO_ValidityBeginJD.SelectedDate = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityBegin"]);
                                RadDatePickerPRO_ValidityEndJD.SelectedDate = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityEnd"]);
                                break;
                            case "公路":
                                TextBoxPRO_ProfessionGL.Text = "公路";
                                RadDatePickerPRO_ValidityBeginGL.SelectedDate = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityBegin"]);
                                RadDatePickerPRO_ValidityEndGL.SelectedDate = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityEnd"]);
                                break;
                            case "市政":
                                TextBoxPRO_ProfessionSZ.Text = "市政";
                                RadDatePickerPRO_ValidityBeginSZ.SelectedDate = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityBegin"]);
                                RadDatePickerPRO_ValidityEndSZ.SelectedDate = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityEnd"]);
                                break;
                            case "矿业":
                                TextBoxPRO_ProfessionKY.Text = "矿业";
                                RadDatePickerPRO_ValidityBeginKY.SelectedDate = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityBegin"]);
                                RadDatePickerPRO_ValidityEndKY.SelectedDate = Convert.ToDateTime(dt.Rows[i]["PRO_ValidityEnd"]);
                                break;

                        }

                    }
                }
                #endregion


            }
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            COC_TOW_Person_BaseInfoMDL _COC_TOW_Person_BaseInfoMDL = ViewState["COC_TOW_Person_BaseInfoMDL"] == null ? new COC_TOW_Person_BaseInfoMDL() : (COC_TOW_Person_BaseInfoMDL)ViewState["COC_TOW_Person_BaseInfoMDL"];
            DBHelper db = new DBHelper();
            DbTransaction tran = db.BeginTransaction();
            try
            {
                if (ViewState["COC_TOW_Person_BaseInfoMDL"] != null)
                {
                    System.Text.StringBuilder sb = new System.Text.StringBuilder();
                    CompareObj("姓名", _COC_TOW_Person_BaseInfoMDL.PSN_Name, TextBoxPSN_Name.Text, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_Name = TextBoxPSN_Name.Text;//姓名

                    CompareObj("性别", _COC_TOW_Person_BaseInfoMDL.PSN_Sex, RadComboBoxPSN_Sex.SelectedValue, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_Sex = RadComboBoxPSN_Sex.SelectedValue;//性别
                    //_COC_TOW_Person_BaseInfoMDL.ps

                    CompareObj("出生日期", _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate, RadDatePickerPSN_BirthDate.SelectedDate, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_BirthDate = Convert.ToDateTime(RadDatePickerPSN_BirthDate.SelectedDate);//出生日期

                    CompareObj("民族", _COC_TOW_Person_BaseInfoMDL.PSN_National, TextBoxPSN_National.Text, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_National = TextBoxPSN_National.Text;//民族

                    CompareObj("证件类型", _COC_TOW_Person_BaseInfoMDL.PSN_CertificateType, TextBoxPSN_CertificateType.Text, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_CertificateType = TextBoxPSN_CertificateType.Text;//证件类型

                    CompareObj("证件号码", _COC_TOW_Person_BaseInfoMDL.PSN_CertificateNO, TextBoxPSN_CertificateNO.Text, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_CertificateNO = TextBoxPSN_CertificateNO.Text;//证件号码

                    CompareObj("毕业院校", _COC_TOW_Person_BaseInfoMDL.PSN_GraduationSchool, TextBoxPSN_GraduationSchool.Text, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_GraduationSchool = TextBoxPSN_GraduationSchool.Text;//毕业院校

                    CompareObj("所学专业", _COC_TOW_Person_BaseInfoMDL.PSN_Specialty, TextBoxPSN_Specialty.Text, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_Specialty = TextBoxPSN_Specialty.Text;//所学专业

                    CompareObj("学历", _COC_TOW_Person_BaseInfoMDL.PSN_Qualification, TextBoxPSN_Qualification.Text, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_Qualification = TextBoxPSN_Qualification.Text;//学历

                    CompareObj("学位", _COC_TOW_Person_BaseInfoMDL.PSN_Degree, TextBoxPSN_Degree.Text, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_Degree = TextBoxPSN_Degree.Text;//学位

                    CompareObj("毕业时间", _COC_TOW_Person_BaseInfoMDL.PSN_GraduationTime, RadDatePickerPSN_GraduationTime.SelectedDate, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_GraduationTime = Convert.ToDateTime(RadDatePickerPSN_GraduationTime.SelectedDate);//毕业时间

                    CompareObj("电子邮件", _COC_TOW_Person_BaseInfoMDL.PSN_Email, TextBoxPSN_Email.Text, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_Email = TextBoxPSN_Email.Text;//电子邮件

                    CompareObj("手机", _COC_TOW_Person_BaseInfoMDL.PSN_MobilePhone, TextBoxPSN_MobilePhone.Text, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_MobilePhone = TextBoxPSN_MobilePhone.Text;//手机

                    CompareObj("联系电话", _COC_TOW_Person_BaseInfoMDL.PSN_Telephone, TextBoxPSN_Telephone.Text, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_Telephone = TextBoxPSN_Telephone.Text;//联系电话

                    CompareObj("证书编号", _COC_TOW_Person_BaseInfoMDL.PSN_RegisterCertificateNo, TextBoxPSN_RegisterCertificateNo.Text, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_RegisterCertificateNo = TextBoxPSN_RegisterCertificateNo.Text;//证书编号

                    CompareObj("注册号", _COC_TOW_Person_BaseInfoMDL.PSN_RegisterNO, TextBoxPSN_RegisterNO.Text, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_RegisterNO = TextBoxPSN_RegisterNO.Text;//注册号

                    CompareObj("发证日期", _COC_TOW_Person_BaseInfoMDL.PSN_CertificationDate, RadDatePickerPSN_CertificationDate.SelectedDate, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_CertificationDate = Convert.ToDateTime(RadDatePickerPSN_CertificationDate.SelectedDate);//发证日期

                    CompareObj("证书有效期", _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity, TextBoxPSN_CertificateValidity.Text, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_CertificateValidity = Convert.ToDateTime(TextBoxPSN_CertificateValidity.Text);//证书有效期

                    CompareObj("注册专业", _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession, TextBoxPSN_RegisteProfession.Text, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_RegisteProfession = TextBoxPSN_RegisteProfession.Text;//注册专业

                    CompareObj("企业名称", _COC_TOW_Person_BaseInfoMDL.ENT_Name, TextBoxENT_Name.Text, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.ENT_Name = TextBoxENT_Name.Text;//企业名称

                    CompareObj("注册类别", _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType, GetRegisteTypeCode(TextBoxPSN_RegisteType.Text.Trim()), ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_RegisteType = GetRegisteTypeCode(TextBoxPSN_RegisteType.Text.Trim());//注册类别

                    CompareObj("注册审批日期", _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate, RadDatePickerPSN_RegistePermissionDate.SelectedDate, ref sb);
                    _COC_TOW_Person_BaseInfoMDL.PSN_RegistePermissionDate = Convert.ToDateTime(RadDatePickerPSN_RegistePermissionDate.SelectedDate);//注册审批日期
                     COC_TOW_Person_BaseInfoDAL.Update(tran, _COC_TOW_Person_BaseInfoMDL);
                     
                    //DataTable dt3=ViewState["DataTable"]==null? new DataTable():(DataTable)
                    DataTable dt2 = COC_TOW_Register_ProfessionDAL.GetListByPSN_ServerID(_COC_TOW_Person_BaseInfoMDL.PSN_ServerID);
                     #region 
                     
                     if (dt2 != null && dt2.Rows.Count > 0)
                     {
                         string sql = "";
                         for (int i = 0; i < dt2.Rows.Count; i++)
                         {

                             switch (dt2.Rows[i]["PRO_Profession"].ToString())
                             {
                                 case "水利":
                                     sql += string.Format("Update COC_TOW_Register_Profession set PRO_ValidityBegin='{0}', PRO_ValidityEnd='{1}' where PRO_ServerID='{2}';", RadDatePickerPRO_ValidityBeginSL.SelectedDate, RadDatePickerPRO_ValidityEndSL.SelectedDate, dt2.Rows[i]["PRO_ServerID"]);
                                     break;
                                 case "建筑":
                                     sql += string.Format("Update COC_TOW_Register_Profession set PRO_ValidityBegin='{0}', PRO_ValidityEnd='{1}' where PRO_ServerID='{2}';", RadDatePickerPRO_ValidityBeginJZ.SelectedDate, RadDatePickerPRO_ValidityEndJZ.SelectedDate, dt2.Rows[i]["PRO_ServerID"]);
                                     break;
                                 case "机电":
                                     sql += string.Format("Update COC_TOW_Register_Profession set PRO_ValidityBegin='{0}', PRO_ValidityEnd='{1}' where PRO_ServerID='{2}';", RadDatePickerPRO_ValidityBeginJD.SelectedDate, RadDatePickerPRO_ValidityEndJD.SelectedDate, dt2.Rows[i]["PRO_ServerID"]);
                                     break;
                                 case "公路":
                                     sql += string.Format("Update COC_TOW_Register_Profession set PRO_ValidityBegin='{0}', PRO_ValidityEnd='{1}' where PRO_ServerID='{2}';", RadDatePickerPRO_ValidityBeginGL.SelectedDate, RadDatePickerPRO_ValidityEndGL.SelectedDate, dt2.Rows[i]["PRO_ServerID"]);
                                     break;
                                 case "市政":
                                     sql += string.Format("Update COC_TOW_Register_Profession set PRO_ValidityBegin='{0}', PRO_ValidityEnd='{1}' where PRO_ServerID='{2}' ;", RadDatePickerPRO_ValidityBeginSZ.SelectedDate, RadDatePickerPRO_ValidityEndSZ.SelectedDate, dt2.Rows[i]["PRO_ServerID"]);
                                     break;
                                 case "矿业":
                                     sql += string.Format("Update COC_TOW_Register_Profession set PRO_ValidityBegin='{0}', PRO_ValidityEnd='{1}'where PRO_ServerID='{2}';", RadDatePickerPRO_ValidityBeginKY.SelectedDate, RadDatePickerPRO_ValidityEndKY.SelectedDate, dt2.Rows[i]["PRO_ServerID"]);
                                     break;

                             }
                         }
                         CommonDAL.ExecSQL(sql);
                     }
                     #endregion
              
                    
               
                     tran.Commit();
                     UIHelp.WriteOperateLog(UserName, UserID, "修正二级建造师证书信息", string.Format("修正证书“{0}”：{1}", _COC_TOW_Person_BaseInfoMDL.PSN_RegisterNO, sb));
                     UIHelp.layerAlert(Page, "证书修正成功");
                     ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
                }
               

            }
            catch (Exception ex )
            {

                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "修正二级建造师证书信息失败", ex);
            }
            
        }

        /// <summary>
        /// 比较修改内容
        /// </summary>
        /// <param name="ItemName">修改项目名称</param>
        /// <param name="o1">原值</param>
        /// <param name="o2">新值</param>
        /// <param name="rtn">不同内容描述</param>
        private void CompareObj(string ItemName,object o1,object o2,ref System.Text.StringBuilder rtn)
        {
            if(object.Equals(o1,o2)==false)
            {
                rtn.Append(string.Format("{0}从【{1}】修改为【{2}】；",ItemName, o1, o2));
            }
        }
    }
}