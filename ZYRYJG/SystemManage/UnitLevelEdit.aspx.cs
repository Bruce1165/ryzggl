using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DataAccess;
using Model;
using Telerik.Web.UI;

namespace ZYRYJG.SystemManage
{
    public partial class UnitLevelEdit : BasePage
    {

        protected override void Page_Init(object sender, EventArgs e)
        {
            BindJG();
            base.Page_Init(sender, e);
        }

        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "UnitLevelSet.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //添加子公司
                if (string.IsNullOrEmpty(Request["u"]) == true
                    && string.IsNullOrEmpty(Request["n"]) == true)
                {
                    DivFindUnit.Visible = true;
                    tableLSGX.Visible = false;
                    return;
                }

                if (string.IsNullOrEmpty(Request["u"]) == false)
                {
                    TextBoxUnitCode.Text = Request["u"];
                }
                if (string.IsNullOrEmpty(Request["n"]) == false)
                {
                    TextBoxUnitName.Text = Request["n"];
                }

              
            }
        }


        /// <summary>
        /// 绑定待选机构
        /// </summary>
        private void BindJG()
        {
            //三类人初审机构（区县、市属集团总公司、中央驻京单位）
            string sql = @"SELECT USERID,ORGANID,RELUSERNAME
                            FROM DBO.[USER]
                            where (ORGANID=242 or ORGANID=246 or ORGANID=247)
                            and userid<>328 and userid<>338
                            order by ORGANID,RELUSERNAME;
                            ";
            DataTable dt = CommonDAL.GetDataTable(sql);

            QY_HYLSGXOB _QY_HYLSGXOB=null;//隶属机构信息
            if (string.IsNullOrEmpty(Request["o"]) == false)//
            {
                _QY_HYLSGXOB = QY_LSGXDAL.GetObject(Request["o"]);
                if (_QY_HYLSGXOB != null)
                {
                    ViewState["QY_HYLSGXOB"] = _QY_HYLSGXOB;
                }
            }

            string ORGANID = "";
            System.Web.UI.HtmlControls.HtmlGenericControl cur_p = null;
            int colno = 0;
            RadioButton myRb = null;//当前登陆机构选项
            foreach (DataRow dr in dt.Rows)
            {
                if (dr["ORGANID"].ToString() != ORGANID)
                {
                    colno = 0;
                    System.Web.UI.HtmlControls.HtmlGenericControl p_title = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
                    tdLSJG.Controls.Add(p_title);
                    p_title.Style.Add("font-weight","bold");
                    switch(dr["ORGANID"].ToString())
                    {
                        case "242":
                            p_title.InnerText = "区县建委";
                            break;
                            case "246":
                            p_title.InnerText = "市属集团总公司";
                            break;
                            case "247":
                            p_title.InnerText = "中央驻京单位";
                            break;
                    }
                

                    System.Web.UI.HtmlControls.HtmlGenericControl p = new System.Web.UI.HtmlControls.HtmlGenericControl("p");
                    p.ID = "p_" + dr["ORGANID"].ToString();
                    
                    cur_p = p;
                    tdLSJG.Controls.Add(p);

                    ORGANID = dr["ORGANID"].ToString();
                }

                RadioButton rb = new RadioButton();
                cur_p.Controls.Add(rb);
      
                rb.Text = dr["RELUSERNAME"].ToString();
                rb.GroupName = "lsgx";
               
                rb.ID = string.Format("RadioButton_{0}", dr["USERID"]);

                if (string.IsNullOrEmpty(Request["o"]) == false
                    && dr["USERID"] !=null
                    && _QY_HYLSGXOB !=null
                    && _QY_HYLSGXOB.USERID.HasValue==true
                    && _QY_HYLSGXOB.USERID .ToString()== dr["USERID"].ToString()
                    )//隶属机构ID
                {
                    rb.Checked = true;
                }

                //缓存当前登陆机构选项所在控件
                if (dr["USERID"].ToString()== PersonID.ToString())//隶属机构ID
                {
                    myRb = rb;
                }


                colno++;
                if(colno %4 ==0)
                {
                    cur_p.Controls.Add(new System.Web.UI.HtmlControls.HtmlGenericControl("br"));
                }
            }


            //当传递Request["i"]表示要添加子公司 Request["i"]=母公司ID
            if (string.IsNullOrEmpty(Request["i"]) == false
                && myRb !=null
                )//隶属机构ID
            {
                myRb.Checked=true;
                trLSJGList.Visible = false;
                LabelCurDept.Text = myRb.Text;
                trImport.Visible = true;
            }
        }
    

        protected void btnSave_Click(object sender, EventArgs e)
        {
            QY_HYLSGXOB _QY_HYLSGXOB = (ViewState["QY_HYLSGXOB"] == null) ? new QY_HYLSGXOB() : (QY_HYLSGXOB)ViewState["QY_HYLSGXOB"];

            bool IfSelectRadioButton = false;
            foreach (Control c in tdLSJG.Controls)
            {
                foreach (Control r in c.Controls)
                {
                    if (r.GetType() == typeof(System.Web.UI.WebControls.RadioButton))
                    {
                        if ((r as RadioButton).Checked == true)
                        {
                            IfSelectRadioButton = true;

                            _QY_HYLSGXOB.ORGANID = Convert.ToInt64(c.ID.Substring(2));
                            _QY_HYLSGXOB.USERID = Convert.ToInt64(r.ID.Substring(12));
                            _QY_HYLSGXOB.LSGX = (r as RadioButton).Text;
                        }
                    }
                }
            }

            if (IfSelectRadioButton == false)
            {
                UIHelp.layerAlert(Page, "请选择一个隶属机构！");
                return;
            }
            if (UnitID == "242" || UnitID == "246" || UnitID == "247")//市属集团总公司,中央驻京单位，允许添加子公司
            {
                if (_QY_HYLSGXOB.USERID.Value != PersonID)
                {
                    UIHelp.layerAlert(Page, "保存失败，您只能调入企业到本机构下！");
                    return;
                }
            }

            try
            {
                if (_QY_HYLSGXOB.ID == null)//new
                {
                    _QY_HYLSGXOB.QYMC = TextBoxUnitName.Text;
                    _QY_HYLSGXOB.ZZJGDM = TextBoxUnitCode.Text;
                    _QY_HYLSGXOB.ID = Guid.NewGuid().ToString();
                    QY_LSGXDAL.Insert(_QY_HYLSGXOB);
                }
                else//update
                {
                    QY_LSGXDAL.Update(_QY_HYLSGXOB);
                }
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "更新企业隶属机构失败！", ex);
                return;
            }     
            ViewState["QY_HYLSGXOB"] = _QY_HYLSGXOB;


            UIHelp.WriteOperateLog(PersonName, UserID, "更新企业隶属机构", string.Format("企业：{0}，隶属机构：{1}", _QY_HYLSGXOB.QYMC, _QY_HYLSGXOB.LSGX));

            UIHelp.layerAlert(Page, "保存成功！");

            ClientScript.RegisterStartupScript(this.GetType(), "isfresh", "var isfresh=true;", true);
        }

        protected void ButtonFindUnit_Click(object sender, EventArgs e)
        {
            QY_HYLSGXOB _QY_HYLSGXOB = QY_LSGXDAL.GetObjectByZZJGDM(TextBoxFindUnitCode.Text.Trim());
            if (_QY_HYLSGXOB != null)//有隶属关系
            {
                //检查是否是外地进京企业
                int wdCount =UnitDAL.SelectCount_QY_BWDZZZS(string.Format(" and zzjgdm= '{0}' and  qylb like '外地进京%'",TextBoxFindUnitCode.Text.Trim()));
                if(wdCount >0)
                {
                    UIHelp.layerAlert(Page, "查询企业是外地进京企业，不允许变更隶属关系");
                    return;
                }

                //检查现在是否隶属于央企或大集团
                if (_QY_HYLSGXOB.ORGANID.HasValue && (_QY_HYLSGXOB.ORGANID.Value == 246 || _QY_HYLSGXOB.ORGANID.Value == 247))
                {
                    UIHelp.layerAlert(Page, string.Format("查询企业已经隶属于{0}，无法变更隶属关系！（除非现隶属机构主动解除隶属关系）", _QY_HYLSGXOB.LSGX));
                    return;
                }
                else
                {
                    Response.Redirect(string.Format("UnitLevelEdit.aspx?o={0}&u={1}&n={2}&i={3}", _QY_HYLSGXOB.ID, _QY_HYLSGXOB.ZZJGDM, _QY_HYLSGXOB.QYMC, PersonID));
                }
            }
            else//无隶属关系
            {
                DataTable dt = QY_LSGXDAL.GetListView(0, 1, string.Format(" and UnitCode='{0}'", TextBoxFindUnitCode.Text.Trim()), "");
                if (dt != null && dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["WDJJ"] != null && dt.Rows[0]["WDJJ"].ToString() == "外地进京")
                    {
                        UIHelp.layerAlert(Page, "查询企业是外地进京企业，不允许变更隶属关系");
                        return;
                    }
                    else
                    {
                        Response.Redirect(string.Format("UnitLevelEdit.aspx?o={0}&u={1}&n={2}&i={3}", dt.Rows[0]["ID"], dt.Rows[0]["UnitCode"], dt.Rows[0]["UnitName"], PersonID));
                    }
                }
                else
                {
                    UIHelp.layerAlert(Page, "查无企业或者该企业目前无安全生产考核三类人员证书，无法添加！");
                    return;
                }
            }

            //foreach (Control c in tdLSJG.Controls)
            //{
            //    foreach (Control r in c.Controls)
            //    {
            //        if (r.GetType() == typeof(System.Web.UI.WebControls.RadioButton))
            //        {
            //            if (Convert.ToInt64(r.ID.Substring(12))== PersonID)
            //            {
            //                (r as RadioButton).Checked = true;                       
            //            }
            //        }
            //    }
            //}
        }
    }
}
