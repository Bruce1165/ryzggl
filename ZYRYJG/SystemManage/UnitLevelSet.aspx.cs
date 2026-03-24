using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Telerik.Web.UI;
using Model;
using DataAccess;
using System.Data;

namespace ZYRYJG.SystemManage
{
    public partial class UnitLevelSet : BasePage
    {       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonSearch_Click(sender, e);
            }
        }      

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();

            //区县不允许更改隶属关系
            if (UnitID == "242")
            {
                q.Add(string.Format("USERID = {0}", PersonID));// 机构ID

                RadGrid1.Columns.FindByUniqueName("Edit").Visible = false;
                RadGrid1.Columns.FindByUniqueName("Delete").Visible = false;
                RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
            }
            //市属集团总公司,中央驻京单位，允许添加子公司，删除子公司
            //添加：已添加到其其他央企和集团的不允许添加，是初审单位的企业不允许添加
            //删除：按资质归还属地，无属地设置为空
            else if (UnitID == "246" || UnitID == "247")
            {
                q.Add(string.Format("USERID = {0}", PersonID));// 机构ID

                RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.Top;
                RadGrid1.Columns.FindByUniqueName("Delete").Visible = true;
                RadGrid1.Columns.FindByUniqueName("Edit").Visible = false;
            }
            else if(PersonType!=1)
            {
                RadGrid1.MasterTableView.CommandItemDisplay = GridCommandItemDisplay.None;
                RadGrid1.Columns.FindByUniqueName("Delete").Visible = false;
                RadGrid1.Columns.FindByUniqueName("jcsjk_lsgx").Visible = true;
                
            }
            
            if (RadTextBoxUnitName.Text.Trim() != "")
            {
                q.Add(string.Format("UnitName like '%{0}%'", RadTextBoxUnitName.Text.Trim()));// 企业名称
            }
            if (RadTextBoxUnitCode.Text.Trim() != "")
            {
                q.Add(string.Format("UnitCode like '%{0}%'", RadTextBoxUnitCode.Text.Trim()));// 机构代码
            }
            if (RadTextBoxUserName.Text.Trim() != "")
            {
                q.Add(string.Format("(RELUSERNAME like '%{0}%' or jcsjk_lsgx like '%{0}%')", RadTextBoxUserName.Text.Trim()));// 隶属机构名称
            }

            //隶属管理类型
             if (RadComboBoxLSGX.SelectedItem.Value != "")
            {
                switch (RadComboBoxLSGX.SelectedItem.Value)
                {
                    case "无隶属关系":
                        q.Add("USERID is null");
                        break;
                    case "区县":
                        q.Add("ORGANID=242");
                        break;
                    case "市属集团总公司":
                        q.Add("ORGANID=246");
                        break;
                    case "中央驻京单位":
                        q.Add("ORGANID=247");
                        break;
                }
            } 

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        //删除
        protected void RadGrid1_ItemCommand(object source, GridCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                string ID = RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["ID"].ToString();
                string UnitCode = RadGrid1.MasterTableView.DataKeyValues[e.Item.ItemIndex]["UNITCODE"].ToString();

                //隶属机构信息
                QY_HYLSGXOB _QY_HYLSGXOB = QY_LSGXDAL.GetObject(ID);

                //查询资质中隶隶属关系
                string sql = @"SELECT L.ID,L.ZZJGDM,L.QYMC,L.LSGX ,u.username,u.ORGANID,u.userid
FROM DBO.QY_HYLSGX_ING L
left join DBO.[USER] u
on 
(u.ORGANID =242 or u.ORGANID =246 or u.ORGANID =247)
and
(
L.LSGX 
like  replace(replace(replace(replace(u.USERNAME,'延庆县','延庆区'),'密云县','密云区'),'怀柔建委','怀柔区'),'开发区','北京市经济技术开发区')
or L.LSGX like  
replace(replace(replace(replace(replace(replace(replace(replace(
replace(u.USERNAME,'北京市政路桥集团有限公司','市政路桥集团')
,'北京中关村开发建设集团','中关村建设')
,'中国建筑一局（集团）有限公司','中建一局')
,'北京住总集团有限责任公司','住总集团')
,'北京建工集团有限责任公司','建工集团')
,'北京市政路桥集团有限公司','市政路桥集团')
,'中国建筑第二工程局有限公司','中建二局')
,'中国新兴建设开发总公司','新兴建设集团')
,'北京城建集团有限责任公司','城建集团')
)
where L.zzjgdm='{0}'
order by u.ORGANID;
";
                DataTable dt = CommonDAL.GetDataTable(string.Format(sql, _QY_HYLSGXOB.ZZJGDM));

                //更新 "LSGX" ,u.username,u.ORGANID,u.userid
                if (dt != null && dt.Rows.Count > 0)//存在资质隶属管理记录
                {

                    if (dt.Rows[0]["ORGANID"] != null && Convert.ToInt64(dt.Rows[0]["USERID"]) == PersonID)
                    {
                        //资质中的隶属管理就是我自己，允许删除
                        _QY_HYLSGXOB.LSGX = null;
                        _QY_HYLSGXOB.ORGANID = null;
                        _QY_HYLSGXOB.USERID = null;
                    }
                    else//恢复资质中的隶属管理
                    {
                        _QY_HYLSGXOB.LSGX = (dt.Rows[0]["LSGX"] == null ? null : dt.Rows[0]["LSGX"].ToString());
                        if (dt.Rows[0]["ORGANID"] == null)
                        {
                            _QY_HYLSGXOB.ORGANID = null;
                        }
                        else
                        {
                            _QY_HYLSGXOB.ORGANID = Convert.ToInt64(dt.Rows[0]["ORGANID"]);
                        }
                        if (dt.Rows[0]["USERID"] == null)
                        {
                            _QY_HYLSGXOB.USERID = null;
                        }
                        else
                        {
                            _QY_HYLSGXOB.USERID = Convert.ToInt64(dt.Rows[0]["USERID"]);
                        }
                    }
                }
                else//不存在资质隶属管理记录
                {
                    _QY_HYLSGXOB.LSGX = null;
                    _QY_HYLSGXOB.ORGANID = null;
                    _QY_HYLSGXOB.USERID = null;
                }
                try
                {
                    QY_LSGXDAL.Update(_QY_HYLSGXOB);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "删除隶属子公司失败！", ex);
                    return;
                }
                if (Cache["UnitCodeSet"] != null) Cache.Remove("UnitCodeSet");
                UIHelp.WriteOperateLog(PersonName, UserID, "删除隶属子公司。", string.Format("单位名称：{0}；组织机构代码：{1}。"
                    , _QY_HYLSGXOB.QYMC
                    , _QY_HYLSGXOB.ZZJGDM));
                UIHelp.layerAlert(Page, "删除成功！",6,3000);
                RadGrid1.DataBind();
            }
        }
    }
}
