using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using DataAccess;
using System.Data;

namespace ZYRYJG.SystemManage
{
    public partial class ShebaoReSend : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }

            QueryParamOB q = new QueryParamOB();
            if (RadTextBoxUnitName.Text.Trim() != "")   //企业名称
            {
                q.Add(string.Format("ENT_Name like '%{0}%'", RadTextBoxUnitName.Text.Trim()));
            }
            if (RadTextBoxUnitCode.Text.Trim() != "")   //企业名称
            {
                q.Add(string.Format("ENT_OrganizationsCode like '{0}%'", RadTextBoxUnitCode.Text.Trim()));
            }

            DataTable dtQy = CommonDAL.GetDataTable(string.Format("select distinct ENT_OrganizationsCode from unit where 1=1 {0}", q.ToWhereString()));

            if (dtQy == null || dtQy.Rows.Count == 0)
            {
                ViewState["ENT_OrganizationsCode"] = null;
                RadGrid1.DataSource = null;
                RadGrid1.DataBind();
                UIHelp.layerAlert(Page, "没有查到相关企业信息！");
                return;
            }

            if (dtQy.Rows.Count > 1)
            {
                ViewState["ENT_OrganizationsCode"] = null;
                RadGrid1.DataSource = null;
                RadGrid1.DataBind();
                UIHelp.layerAlert(Page, "匹配多家企业，请修改查询条件重新查询！");
                return;
            }

            ViewState["ENT_OrganizationsCode"] = dtQy.Rows[0][0].ToString().Substring(0, 8);

            //社保法人库
            DataTable dtFR = CommonDAL.GetDataTable(string.Format(@"SELECT [ID]
                                                                          ,[YZZJGDM]
                                                                          ,[YSHBXDJH]
                                                                          ,[TYSHXYDM]
                                                                          ,[SJLY]
                                                                          ,[VALID]
                                                                          ,[CJSJ]
                                                                          ,[XGSJ]
                                                                          ,[CJR]
                                                                          ,[XGR]
                                                                          ,[CJDEPTID]
                                                                          ,[XGDEPTID]
                                                                      FROM [192.168.7.56].[SJZX_SBCJ].[dbo].[SBCJ_DMDZB] 
                                                                      where [YZZJGDM] ='{0}'", dtQy.Rows[0][0].ToString().Substring(0, 8)));

            //            DataTable dtFR = CommonDAL.GetDataTable(string.Format(@"SELECT *
            //                                                                      FROM [ShareDB].[dbo].[SBCJ_DMDZB] 
            //                                                                      where [YZZJGDM] ='{0}'", dtQy.Rows[0][0].ToString().Substring(0,8)));

            if (dtFR != null && dtFR.Rows.Count > 0)
            {
                RadTextBoxYZZJGDM.Text = dtFR.Rows[0]["YZZJGDM"].ToString();
                RadTextBoxTYSHXYDM.Text = dtFR.Rows[0]["TYSHXYDM"].ToString();
                RadTextBoxSJLY.Text = (dtFR.Rows[0]["SJLY"] == DBNull.Value ? "" : dtFR.Rows[0]["SJLY"].ToString());

                CheckBoxValid.Checked = (Convert.ToInt32(dtFR.Rows[0]["VALID"]) == 1);
            }
            else
            {
                RadTextBoxYZZJGDM.Text = "";
                RadTextBoxTYSHXYDM.Text = "";
                RadTextBoxSJLY.Text = "";
                CheckBoxValid.Checked = false;
            }

            string sql = "";
            switch (RadioButtonListType.SelectedValue)
            {
                case "初始":
                    sql = @"select 0 rn,cjsj,applytype,psn_name,PSN_CertificateNO,ent_name,ENT_OrganizationsCode 
                                from apply
                                where ApplyStatus = '已申报' and ApplyType = '初始注册'  and ENT_OrganizationsCode = '{0}'";
                    break;
                case "重新":
                    sql = @"select 0 rn,cjsj,applytype,psn_name,PSN_CertificateNO,ent_name,ENT_OrganizationsCode 
                                from apply 
                                where ApplyStatus = '已申报' and ApplyType = '重新注册' and ENT_OrganizationsCode = '{0}'";
                    break;
                case "变更":
                    sql = @"select 0 rn,cjsj,applytype,psn_name,PSN_CertificateNO,ent_name,ENT_OrganizationsCode 
                                from apply
                                where ApplyStatus = '已申报' and ApplyType = '变更注册' and ApplyTypeSub = '执业企业变更' and ENT_OrganizationsCode = '{0}'";
                    break;
                default:
                    sql = @"-- 初始注册
                                select 0 rn,cjsj,applytype,psn_name,PSN_CertificateNO,ent_name,ENT_OrganizationsCode 
                                from apply
                                where ApplyStatus = '已申报' and ApplyType = '初始注册'  and ENT_OrganizationsCode = '{0}'
                                union all
                                -- 重新注册
                               select 0 rn,cjsj,applytype,psn_name,PSN_CertificateNO,ent_name,ENT_OrganizationsCode 
                                from apply 
                                where ApplyStatus = '已申报' and ApplyType = '重新注册' and ENT_OrganizationsCode = '{0}'
                                union all
                                -- 变更注册（执业企业变更）
                                select 0 rn,cjsj,applytype,psn_name,PSN_CertificateNO,ent_name,ENT_OrganizationsCode 
                                from apply
                                where ApplyStatus = '已申报' and ApplyType = '变更注册' and ApplyTypeSub = '执业企业变更' and ENT_OrganizationsCode = '{0}'
                                ";
                    break;
            }

            //业务申请
            DataTable dt = CommonDAL.GetDataTable(string.Format(sql, dtQy.Rows[0][0]));
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dt.Rows[i]["RowNum"] = i + 1;
            }
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();
        }

        //protected void ButtonReSend_Click(object sender, EventArgs e)
        //{
        //    List<string> list = GetGridSelectedKeys(RadGrid1);
        //    System.Web.UI.WebControls.CheckBox cbox = null;
        //    string key = "";
        //    for (int i = 0; i <= RadGrid1.Items.Count - 1; i++)
        //    {
        //        key = RadGrid1.MasterTableView.DataKeyValues[i]["CHECKID"].ToString();
        //        cbox = (System.Web.UI.WebControls.CheckBox)RadGrid1.Items[i].FindControl("CheckBox1");
        //        if (cbox.Checked == true && list.Contains(key) == false)
        //        {
        //            list.Add(key);
        //        }
        //        else if (cbox.Checked == false && list.Contains(key) == true)
        //        {
        //            list.Remove(key);
        //        }
        //    }
        //    ViewState[string.Format("{0}_SelectedKeys", RadGrid1.ID)] = list;

        //    if (IsGridSelected(RadGrid1) == false)
        //    {
        //        UIHelp.Alert(Page, "至少选择一条数据！");
        //        return;
        //    }

        //    try
        //    {
        //        string sql = "delete from dbo.ShebaoCheck where CHECKID in({0})";
        //        CommonDAL.ExecSQL(string.Format(sql, GetGridSelectedKeysToString(RadGrid1)));
        //    }
        //    catch (Exception ex)
        //    {
        //        UIHelp.WriteErrorLog(Page, "重发社保失败！", ex);
        //        return;
        //    }

        //    btnSearch_Click(sender, e);
        //    UIHelp.Alert(Page, "重发社保成功！");

        //}



        protected void ButtonResetUnitCode_Click(object sender, EventArgs e)
        {
            if (RadTextBoxTYSHXYDM.Text.Trim().Length != 18 && RadTextBoxTYSHXYDM.Text.Trim().Length != 8)
            {
                UIHelp.layerAlert(Page, "统一信用代码输入有误！");
                return;
            }
            if (ViewState["ENT_OrganizationsCode"] == null || ViewState["ENT_OrganizationsCode"].ToString().Length != 8)
            {
                UIHelp.layerAlert(Page, "组织机构代码输入有误！");
                return;
            }

            string sql = @" UPDATE [192.168.7.56].[SJZX_SBCJ].[dbo].[SBCJ_DMDZB]
                       SET [TYSHXYDM] = '{1}'
                           ,[SJLY] = '{2}'
                           ,[VALID] ={3}
                          ,[XGSJ] = getdate()      
                     WHERE [YZZJGDM]='{0}'";

            try
            {

                CommonDAL.ExecSQL(string.Format(sql
                    , ViewState["ENT_OrganizationsCode"]
                    , RadTextBoxTYSHXYDM.Text.Trim().ToUpper()
                    , RadTextBoxSJLY.Text.Trim()
                    , (CheckBoxValid.Checked == true ? 1 : 0)
                    ));
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除社保法人库统一信用代码映射失败！", ex);
                return;
            }


            //社保法人库
            DataTable dtFR = CommonDAL.GetDataTable(string.Format(@"SELECT [ID]
                                                                          ,[YZZJGDM]
                                                                          ,[YSHBXDJH]
                                                                          ,[TYSHXYDM]
                                                                          ,[SJLY]
                                                                          ,[VALID]
                                                                          ,[CJSJ]
                                                                          ,[XGSJ]
                                                                          ,[CJR]
                                                                          ,[XGR]
                                                                          ,[CJDEPTID]
                                                                          ,[XGDEPTID]
                                                                      FROM [192.168.7.56].[SJZX_SBCJ].[dbo].[SBCJ_DMDZB] 
                                                                      where [YZZJGDM] ='{0}'", ViewState["ENT_OrganizationsCode"]));

            if (dtFR != null && dtFR.Rows.Count > 0)
            {
                RadTextBoxYZZJGDM.Text = dtFR.Rows[0]["YZZJGDM"].ToString();
                RadTextBoxTYSHXYDM.Text = dtFR.Rows[0]["TYSHXYDM"].ToString();
                RadTextBoxSJLY.Text = (dtFR.Rows[0]["SJLY"] == DBNull.Value ? "" : dtFR.Rows[0]["SJLY"].ToString());

                CheckBoxValid.Checked = (Convert.ToInt32(dtFR.Rows[0]["VALID"]) == 1);
            }
            else
            {
                RadTextBoxYZZJGDM.Text = "";
                RadTextBoxTYSHXYDM.Text = "";
                RadTextBoxSJLY.Text = "";
                CheckBoxValid.Checked = false;
            }
        }
    }
}