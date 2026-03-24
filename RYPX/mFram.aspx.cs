using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mFram : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!IsPostBack)
        {
            if(string.IsNullOrEmpty(Request["y"])==false)
            {
                sFram.Attributes["src"]="./sFram.aspx";
            }
            //UIHelp.layerTips(Page, "诚邀您参加【北京市建设行业从业人员公益培训平台】年度问卷调查");
            //UIHelp.layerAlertWithHtml(Page, "诚邀您参加【北京市建设行业从业人员公益培训平台】年度问卷调查");
            //UIHelp.layerAlertWithHtml(Page, "<div><p>诚邀您参加【北京市建设行业从业人员公益培训平台】年度问卷调查</p><div><input id=\"Button1\" type=\"button\" value=\"返 回\" class=\"button\" onclick=\"javascript: location.href = 'ExamSignList.aspx';\" /></div></div>");
        }
    }
}