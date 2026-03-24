using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using Utility;
using DataAccess;
using Model;

namespace ZYRYJG.zjs
{
    //资格校验
    public partial class zjsQualification : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           if(!this.IsPostBack)
           {
               ButtonQuery_Click(sender, e);
           }
        }

        //导入
        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile)
                {
                    if (!System.IO.Directory.Exists(HttpContext.Current.Server.MapPath("~/Upload/Excel/"))) System.IO.Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/Upload/Excel/"));
                    string filepath = HttpContext.Current.Server.MapPath("~/Upload/Excel/") + "/" + FileUpload1.FileName;
                    FileUpload1.SaveAs(filepath);
                    DataSet ds = ExcelDealHelp.ImportExcell(filepath, null);

                    if (ds == null || ds.Tables[0].Rows.Count == 0)
                    {
                        UIHelp.layerAlert(Page, "没有发现需要导入的有效数据");
                        return;
                    }

                    System.Text.StringBuilder rtnErr = new System.Text.StringBuilder();//表单数据错误信息
                    System.Text.StringBuilder rowErr = new System.Text.StringBuilder();//行数据错误信息
                    string zy = "安装工程，土木建筑工程";
                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        rowErr.Remove(0, rowErr.Length);//行错误清空

                        if (ds.Tables[0].Rows[i]["姓名"].ToString() == "")
                        {
                            rowErr.Append("<br />姓名不能为空。");
                        }
                        if (ds.Tables[0].Rows[i]["证件号码"].ToString() == "")
                        {
                            rowErr.Append("<br />证件号码不能为空。");
                        }

                        if (ds.Tables[0].Rows[i]["取得年份"].ToString() == "")
                        {
                            rowErr.Append("<br />取得年份不能为空。");
                        }
                        else if (Utility.Check.IsNumber(ds.Tables[0].Rows[i]["取得年份"].ToString()) == false)
                        {
                            rowErr.Append("<br />取得年份必须位4位数字年份。");
                        }
                        else if (1950 > Convert.ToInt32(ds.Tables[0].Rows[i]["取得年份"]) || Convert.ToInt32(ds.Tables[0].Rows[i]["取得年份"]) > DateTime.Now.Year)
                        {
                            rowErr.Append("<br />取得年份疑似不正确，请检查。");
                        }

                        if (ds.Tables[0].Rows[i]["管理号"].ToString() == "")
                        {
                            rowErr.Append("<br />管理号不能为空。（可以与资格证书编号保持一致）");
                        }

                        if (ds.Tables[0].Rows[i]["资格证书编号"].ToString() == "")
                        {
                            rowErr.Append("<br />资格证书编号不能为空（可以与管理号保持一致）。");
                        }


                        if (ds.Tables[0].Rows[i]["专业类别"].ToString() == "" || zy.Contains(ds.Tables[0].Rows[i]["专业类别"].ToString()) == false)
                        {
                            rowErr.Append("<br />专业类别允许范围【安装工程，土木建筑工程】。");
                        }

                        if (ds.Tables[0].Rows[i]["毕业时间"].ToString() == "")
                        {
                            rowErr.Append("<br />毕业时间不能为空。");
                        }
                        else if (Utility.Check.IfDateOrDateTimeFormat(ds.Tables[0].Rows[i]["毕业时间"].ToString()) == false)
                        {
                            rowErr.Append("<br />毕业时间必须是有效的日期格式（如2008-10-01）");
                        }

                        if (ds.Tables[0].Rows[i]["签发时间"].ToString() == "")
                        {
                            rowErr.Append("<br />签发时间不能为空。");
                        }
                        else if (Utility.Check.IfDateOrDateTimeFormat(ds.Tables[0].Rows[i]["签发时间"].ToString()) == false)
                        {
                            rowErr.Append("<br />签发时间必须是有效的日期格式（如2008-10-01）");
                        }

                        if (rowErr.Length > 0)
                        {
                            rtnErr.Append("<br />---第【").Append(Convert.ToString(i + 1)).Append("】行：姓名“").Append(ds.Tables[0].Rows[i]["姓名"].ToString()).Append("”-------------------------------");
                            rtnErr.Append(rowErr.ToString());
                        }
                    }

                    if (rtnErr.Length > 0)
                    {
                        UIHelp.layerAlert(Page, rtnErr.ToString());
                        return;
                    }

                    zjs_QualificationDAL.ImportByExcel(ds.Tables[0]);
                    UIHelp.layerAlert(Page, string.Format("导入成功（共{0}条）",ds.Tables[0].Rows.Count));
                    ButtonQuery_Click(sender, e);
                }
                else
                {
                    UIHelp.layerAlert(Page, "请上传文件！");
                    return;
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("数据源中的任意列均不匹配") == true)
                {
                    UIHelp.layerAlert(Page, "使用导入模板有误，请检查使用模板是否为最新版本！");
                    return;
                }
                else
                {
                    UIHelp.WriteErrorLog(Page, "造价工程师资格库数据导入失败！", ex);
                }
           
            }            

        }


        //查询
        protected void ButtonQuery_Click(object sender, EventArgs e)
          {
              if (UIHelp.CheckSQLParam() == false)
              {
                  UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                  return;
              }
              ObjectDataSource1.SelectParameters.Clear();
              var q = new QueryParamOB();
              if (RadComboBoxItem.SelectedValue!=null && RadTextBoxZJHM.Text.Trim() != "")
              {
                  q.Add(string.Format("{1} = '{0}'", RadTextBoxZJHM.Text.Trim(),RadComboBoxItem.SelectedValue));
              }
             ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
             RadGridZGGL.CurrentPageIndex = 0;
             RadGridZGGL.DataSourceID = ObjectDataSource1.ID;
          }
     
    }
}