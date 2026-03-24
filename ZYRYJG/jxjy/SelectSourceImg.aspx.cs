using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using System.IO;

namespace ZYRYJG.jxjy
{
    public partial class SelectSourceImg : BasePage
    {
        protected override bool IsNeedLogin
        {
            get
            {
                return false;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
//                 string path=@"X:\XXX\XX";
//DirectoryInfo root = new DirectoryInfo(path);
// FileInfo[] files=root.GetFiles();

                string[] files = Directory.GetFiles(Server.MapPath(@"~\Images\jz"), "*.jpg");
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                foreach (var f in files)
                {
                    sb.AppendFormat("<div class='simg' style=\"background:url(../Images/jz/{0});background-size:cover;\" onclick=\"returnToParent('{0}')\">&nbsp;</div>", Path.GetFileName(f));
                }
                sb.Append("<div style='clear:both;'></div>");

                divImgSet.InnerHtml = sb.ToString();
            }
        }
     

     

        //protected void RadGridSource_ItemDataBound(object sender, GridItemEventArgs e)
        //{
        //    if (e.Item is GridDataItem)
        //    {
        //        e.Item.Style.Add("cursor", "pointer");
        //        e.Item.Attributes.Add("onclick", string.Format("returnToParent('{0}','{1}')"
        //            ,e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SourceID"]
        //            ,e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["SourceName"]
        //            ));
        //    }
        //}
     
    }
}
