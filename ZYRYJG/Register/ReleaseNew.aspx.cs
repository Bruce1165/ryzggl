using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using System.IO;

namespace ZYRYJG.Register
{
    public partial class ReleaseNew : BasePage
    {
        protected override string CheckVisiteRgihtUrl
        {
            get
            {
                return "Register/ReleaseNews.aspx";
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["m"])==false)//修改
                {
                    PolicyNewsMDL m = PolicyNewsDAL.GetObject(Request["m"]);
                    ViewState["PolicyNewsMDL"] = m;
                    RadTextBoxtitle.Text = m.Title;
                    string cont = m.Content;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "fj"
                   , string.Format(@"$(document).ready(
                                                    function () {{
 
                                                        UE.getEditor('container').ready(
                                                            function(){{
                                                               
                                                                this.setContent('{0}');
                                                          
                                                            }}
                                                        )
                                                    }}
                                            );", cont.Replace("\r\n","<br/>")),true);

                    if (m.FileUrl!="")
                    {
                        Btn_Next.Visible = true;
                    }
                }
            }            
        }

        public void Setvalue()
        {        
                PolicyNewsMDL m = ViewState["PolicyNewsMDL"] as PolicyNewsMDL;
                string cont = m.Content;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "fj"
                      , string.Format(@"$(document).ready(
                                                    function () {{
                                                        UE.getEditor('container').ready(
                                                            function(){{
                                                                this.setContent('{0}');
                                                          
                                                            }}
                                                        )
                                                    }}
                                            );", cont.Replace("\r\n", "<br/>")), true);
                if (m.FileUrl != "")
                {
                    Btn_Next.Visible = true;
                }


        }

        protected bool CheckUpFileType()
        {

            string fType= Path.GetExtension(FileUpload1.FileName);
            if (".doc|.docx|.xls|.xlsx|.pdf|.rar|.zip|.jpg|.png|.gif".Contains(fType) == false)
                return false;
            else
                return true;
            
        }

        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (ViewState["PolicyNewsMDL"] == null)
            {
                try
                {
                    string title = RadTextBoxtitle.Text;
                    string content = HiddenField1.Value;
                    string fileurl = "";
                    if (FileUpload1.HasFile)
                    {
                        if (CheckUpFileType() == false)
                        {
                            UIHelp.layerAlert(Page, "上传文件类型不允许，请使用其他格式。");
                            return;
                        }
                        
                        //判断文件夹存不存在，不存在则创建
                        if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/PolicyNews/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/PolicyNews/"));
                        string filepath = HttpContext.Current.Server.MapPath("~/Upload/PolicyNews/") + "/" + FileUpload1.FileName;
                        FileUpload1.SaveAs(filepath);
                        fileurl = string.Format("~/Upload/PolicyNews/{0}", FileUpload1.FileName);
                    }
                    PolicyNewsMDL _PolicyNewsMDL = new PolicyNewsMDL();
                    _PolicyNewsMDL.ID = Guid.NewGuid().ToString();
                    _PolicyNewsMDL.Title = title;
                    _PolicyNewsMDL.Content = content;
                    _PolicyNewsMDL.FileUrl = fileurl;
                    _PolicyNewsMDL.States = '0';//未发布
                    // _PolicyNewsMDL.GetDateTime = DateTime.Now;
                    PolicyNewsDAL.Insert(_PolicyNewsMDL);
                    ViewState["PolicyNewsMDL"] = _PolicyNewsMDL;
                    Setvalue();
                    UIHelp.WriteOperateLog(UserName, UserID, "政策添加成功", string.Format("发布时间：{0}", DateTime.Now));
                    UIHelp.layerAlert(Page, "政策添加成功！", 6, 2000);
                    ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "政策添加失败", ex);
                    return;
                }
            }
            //修改
            else
            {
                
                PolicyNewsMDL m = (PolicyNewsMDL)ViewState["PolicyNewsMDL"];
                try
                {
                    m.Title = RadTextBoxtitle.Text;
                    m.Content = HiddenField1.Value;
                    string fileurl = "";
                    if (FileUpload1.HasFile)
                    {
                        if (CheckUpFileType() == false)
                        {
                            UIHelp.layerAlert(Page, "上传文件类型不允许，请使用其他格式。");
                            return;
                        }

                        //判断文件夹存不存在，不存在则创建
                        if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/PolicyNews/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/PolicyNews/"));
                        string filepath = HttpContext.Current.Server.MapPath("~/Upload/PolicyNews/") + "/" + FileUpload1.FileName;
                        FileUpload1.SaveAs(filepath);
                        fileurl = string.Format("~/Upload/PolicyNews/{0}", FileUpload1.FileName);
                    }
                    if (fileurl == "")
                    {
                        m.FileUrl = m.FileUrl;
                    }
                    else
                    {
                        m.FileUrl = fileurl;
                    }
                    PolicyNewsDAL.Update(m);
                    ViewState["PolicyNewsMDL"] = m;
                    Setvalue();
                    UIHelp.WriteOperateLog(UserName, UserID, "政策添加成功", string.Format("修改时间：{0}", DateTime.Now));
                    UIHelp.layerAlert(Page, "政策修改成功！", 6, 2000);
                    ClientScript.RegisterStartupScript(GetType(), "isfresh", "var isfresh=true;", true);
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "政策修改失败", ex);
                    return;
                }
                Utility.CacheHelp.RemoveCache(Page, "DataTableZCTZ");
            }
        }

        protected void Btn_Next_Click(object sender, EventArgs e)
        {
            PolicyNewsMDL m = (PolicyNewsMDL)ViewState["PolicyNewsMDL"];
            string fileName =Path.GetFileName(m.FileUrl);
            //string fileName = "北京市二级注册建造师延续注册申请表 (1)";//客户端保存的文件名
            //string filePath = Server.MapPath("~/Upload/PolicyNews/" + "北京市二级注册建造师延续注册申请表 (1).docx");//路径
            string filePath = Server.MapPath(m.FileUrl);
            FileInfo fileInfo = new FileInfo(filePath);
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.WriteFile(fileInfo.FullName);
            Response.Flush();
            Response.End();
        }


    }
}