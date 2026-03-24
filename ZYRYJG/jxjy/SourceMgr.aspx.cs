using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using DataAccess;
using Model;
using System.Data;
using System.IO;

namespace ZYRYJG.jxjy
{
    public partial class SourceMgr : BasePage
    {
        protected string uid()
        {
            return "1" + "0".PadLeft(9, '0');
        }

        protected string getPlayKey()
        {
            return Server.UrlEncode(Utility.Cryptography.Encrypt(string.Format("{0},{1},{2}"
                , DateTime.Now.ToString("yyyy-MM-ddHH:mm:ss")//学习时间
                , -1//课件ID
                , -1//学习人证件
                 )));
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                for (int i = 2021; i <= (DateTime.Now.Year + 1); i++)
                {
                    RadComboBoxSourceYear.Items.Insert(0, new RadComboBoxItem(i.ToString(), i.ToString()));
                }
                RadComboBoxSourceYear.Items.Insert(0, new RadComboBoxItem("全部", ""));
                ButtonSearch_Click(sender, e);
            }
        }

        //查询
        protected void ButtonSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();
            q.Add("ParentSourceID=0");

            if (RadioButtonListSourceType.SelectedValue != "")//课程类型
            {
                q.Add(string.Format("SourceType='{0}'", RadioButtonListSourceType.SelectedValue));
            }

            if (RadComboBoxBarType.SelectedValue != "")//所属栏目
            {
                if (RadComboBoxBarType.SelectedValue == "无")
                    q.Add("BarType is null");
                else
                    q.Add(string.Format("BarType='{0}'", RadComboBoxBarType.SelectedValue));
            }

            if (RadTextBoxValue.Text.Trim() != "")//自定义查询项
            {
                q.Add(string.Format("{0} like '%{1}%'", RadComboBoxIten.SelectedValue, RadTextBoxValue.Text.Trim()));
            }          

             if (RadComboBoxSourceYear.SelectedValue != "")//年度
            {
                q.Add(string.Format("SourceYear={0}", RadComboBoxSourceYear.SelectedValue));
            }            

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());


            GridSortExpression sortStr1 = new GridSortExpression();
            sortStr1.FieldName = " [SourceYear] desc,[SortID]";
            sortStr1.SortOrder = GridSortOrder.Descending;
            RadGridSource.MasterTableView.SortExpressions.AddSortExpression(sortStr1);

            RadGridSource.CurrentPageIndex = 0;
            RadGridSource.DataSourceID = ObjectDataSource1.ID;
        }

        //删除
        protected void RadGridSource_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridItem item = (GridItem)e.Item;
            Int64 SourceID = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["SourceID"].ToString());
            Int64 ParentSourceID = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["ParentSourceID"].ToString());

            DBHelper db = new DBHelper("DBRYPX");
            int subClassCount = SourceDAL.SelectCount(string.Format("and ParentSourceID={0}", SourceID));//子课程个数
            if (subClassCount > 0)
            {
                UIHelp.layerAlert(Page, "该课程下含有子课程，必须先删除子课程！");
                return;
            }
            DbTransaction trans = db.BeginTransaction();
            try
            {
                SourceDAL.Delete(trans, SourceID);
                if (ParentSourceID != 0)
                {
                    SourceDAL.UpdatePeriod(trans, ParentSourceID);
                    SourceDAL.UpdateSourceWareCount(trans, ParentSourceID);
                    ScriptManager.RegisterStartupScript(Page, Page.GetType(), "rtn", "refreshGrid();", true);
                }
                trans.Commit();
            }
            catch (Exception ex)
            {
                trans.Rollback();
                UIHelp.WriteErrorLog(Page, "删除失败！", ex);
                return;
            }
            if (ParentSourceID != 0)//课件
                UIHelp.WriteOperateLog(UserName, UserID, "删除课件", string.Format("课件名称：{0}。", item.OwnerTableView.DataKeyValues[item.ItemIndex]["SourceName"]));
            else
                UIHelp.WriteOperateLog(UserName, UserID, "删除课程", string.Format("课程名称：{0}。", item.OwnerTableView.DataKeyValues[item.ItemIndex]["SourceName"]));

            UIHelp.layerAlert(Page, "删除成功！");
        }

        //绑定子课程
        protected void RadGridSource_DetailTableDataBind(object source, GridDetailTableDataBindEventArgs e)
        {
            GridDataItem dataItem = (GridDataItem)e.DetailTableView.ParentItem;
            switch (e.DetailTableView.Name)
            {
                case "subSource":
                    {
                        string SourceID = dataItem.GetDataKeyValue("SourceID").ToString();
                        e.DetailTableView.DataSource = SourceDAL.GetList(0, int.MaxValue - 1, string.Format("and ParentSourceID={0}", SourceID), "SortID,SourceID");
                        break;
                    }
            }

        }

        //导入课程
        protected void ButtonImport_Click(object sender, EventArgs e)
        {
            if (RadUploadSignUpTable.UploadedFiles.Count > 0)
            {
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/Excel/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/Excel/"));

                //上传excel
                string targetFolder = Server.MapPath("~/Upload/Excel/");
                string filePath = Path.Combine(targetFolder, string.Format("ImortSource_{0}_{1}.xls", PersonID.ToString(), DateTime.Now.ToString("yyyyMMddHHmmss")));
                RadUploadSignUpTable.UploadedFiles[0].SaveAs(filePath, true);

                //读入DataSet再校验并保存
                DataSet dsImport = null;

                try
                {
                    dsImport = Utility.ExcelDealHelp.ImportExcell(filePath, "");
                }
                catch (Exception ex)
                {
                    UIHelp.WriteErrorLog(Page, "导入课程失败", ex);
                    UIHelp.layerAlert(Page, "导入课程失败!");
                    return;
                }

                if (dsImport == null || dsImport.Tables.Count == 0 || dsImport.Tables[0].Rows.Count == 0)
                {
                    UIHelp.layerAlert(Page, "未找到任何数据!");
                    return;
                }
                else
                {
                    try
                    {
                        SaveImportData(dsImport.Tables[0]);//保存
                    }
                    catch (Exception ex)
                    {
                        UIHelp.WriteErrorLog(Page, "导入课程失败", ex);
                        UIHelp.layerAlert(Page, "导入课程失败!");
                        return;
                    }
                    ButtonSearch_Click(sender, e);
                }
            }
            else
            {
                //RefreshGrid(0);
            }
        }


        //保存成绩信息
        protected void SaveImportData(DataTable dt)
        {
            System.Text.StringBuilder rtnErr = new System.Text.StringBuilder();//表单数据错误信息
            System.Text.StringBuilder rowErr = new System.Text.StringBuilder();//行数据错误信息

            // 删除空行
            for (int m = dt.Rows.Count - 1; m >= 0; m--)
            {
                if (dt.Rows[m]["课程名称"].ToString().Trim() == ""
                && dt.Rows[m]["课件名称"].ToString().Trim() == ""
                && dt.Rows[m]["教师名称"].ToString().Trim() == ""
                && dt.Rows[m]["工作单位"].ToString().Trim() == ""

                )
                {
                    dt.Rows.RemoveAt(m);
                }
            }



            DataTable dtMain = CommonDAL.GetDataTableDB("DBRYPX", "select distinct SourceName,SourceYear from source");
            DataColumn[] key = new DataColumn[2];
            key[0] = dtMain.Columns["SourceName"];
            key[1] = dtMain.Columns["SourceYear"];
            dtMain.PrimaryKey = key;

            List<string> listCase = new List<string>() { "必修", "选修" };
            //判断导入课程是否在库中已经存在
            for (int m = 0; m < dt.Rows.Count; m++)
            {
                rowErr.Remove(0, rowErr.Length);//行错误清空
                if (dtMain.Rows.Find(new object[] { dt.Rows[m]["课程名称"].ToString().Trim(), dt.Rows[m]["年度"].ToString().Trim() }) != null)
                {
                    rowErr.AppendFormat("<br />{0}《{1}》已经在库中存在，不能重复导入。", dt.Rows[m]["课程名称"].ToString().Trim(), dt.Rows[m]["年度"].ToString().Trim());
                }
                if (listCase.Contains(dt.Rows[m]["课程类型"].ToString().Trim()) == false)
                {
                    rowErr.Append("<br />课程类型只能输入（选修，必修）中的一项。");
                }

                if (rowErr.Length > 0)
                {
                    rtnErr.Append("<br />---第【").Append(Convert.ToString(m + 2)).Append("】行：-------------------------------");
                    rtnErr.Append(rowErr.ToString());
                }
            }

            if (rtnErr.Length > 0)
            {
                UIHelp.layerAlert(Page, rtnErr.ToString());
                return;
            }

            DateTime createTime = DateTime.Now;
            DBHelper db = new DBHelper("DBRYPX");
            DbTransaction tran = db.BeginTransaction();

            System.Text.StringBuilder hg = new System.Text.StringBuilder();
            System.Text.StringBuilder bhg = new System.Text.StringBuilder();
            DateTime modifyTime = DateTime.Now;
            try
            {
                SourceMDL ParentSource = null;
                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    if (ParentSource == null || dt.Rows[m]["课程名称"].ToString().Trim() != ParentSource.SourceName)//课程
                    {
                        //课程
                        SourceMDL _SourceMDL = new SourceMDL();
                        _SourceMDL.SourceName = dt.Rows[m]["课程名称"].ToString().Trim();
                        _SourceMDL.Teacher = dt.Rows[m]["教师名称"].ToString().Trim();
                        _SourceMDL.WorkUnit = dt.Rows[m]["工作单位"].ToString().Trim();
                        _SourceMDL.SourceType = dt.Rows[m]["课程类型"].ToString().Trim();
                        _SourceMDL.Status = "启用";
                        _SourceMDL.Description = dt.Rows[m]["课程简介"].ToString().Trim();
                        _SourceMDL.ParentSourceID = 0;
                        _SourceMDL.SortID = (m + 1) * 10;
                        _SourceMDL.Period = Convert.ToInt32(dt.Rows[m]["学时(分钟)"]);
                        _SourceMDL.ShowPeriod = SourceDAL.ConvertShowPeriod(_SourceMDL.Period.Value);
                        _SourceMDL.SourceWareCount = 1;
                        _SourceMDL.SourceWareUrl = "";
                        _SourceMDL.SourceYear = Convert.ToInt32(dt.Rows[m]["年度"]);
                        _SourceMDL.BarType = dt.Rows[m]["栏目"].ToString().Trim();
                        SourceDAL.Insert(tran, _SourceMDL);
                        ParentSource = _SourceMDL;

                        //课件
                        SourceMDL _SourceMDL1 = new SourceMDL();
                        _SourceMDL1.SourceName = dt.Rows[m]["课件名称"].ToString().Trim();
                        _SourceMDL1.Teacher = dt.Rows[m]["教师名称"].ToString().Trim();
                        _SourceMDL1.WorkUnit = dt.Rows[m]["工作单位"].ToString().Trim();
                        _SourceMDL1.SourceType = dt.Rows[m]["课程类型"].ToString().Trim();
                        _SourceMDL1.Status = "启用";
                        _SourceMDL1.Description = dt.Rows[m]["课程简介"].ToString().Trim();
                        _SourceMDL1.ParentSourceID = ParentSource.SourceID;
                        _SourceMDL1.SortID = (m + 1) * 10;
                        _SourceMDL1.Period = Convert.ToInt32(dt.Rows[m]["学时(分钟)"]);
                        _SourceMDL1.ShowPeriod = SourceDAL.ConvertShowPeriod(_SourceMDL1.Period.Value);
                        _SourceMDL1.SourceWareCount = 0;
                        _SourceMDL1.SourceWareUrl = dt.Rows[m]["课件地址url"].ToString().Trim();
                        _SourceMDL1.SourceWarePlayParam = dt.Rows[m]["SDKID"].ToString().Trim();
                        _SourceMDL1.SourceYear = Convert.ToInt32(dt.Rows[m]["年度"]);
                        SourceDAL.Insert(tran, _SourceMDL1);

                    }
                    else//课件
                    {
                        SourceMDL _SourceMDL = new SourceMDL();
                        _SourceMDL.SourceName = dt.Rows[m]["课件名称"].ToString().Trim();
                        _SourceMDL.Teacher = dt.Rows[m]["教师名称"].ToString().Trim();
                        _SourceMDL.WorkUnit = dt.Rows[m]["工作单位"].ToString().Trim();
                        _SourceMDL.SourceType = dt.Rows[m]["课程类型"].ToString().Trim();
                        _SourceMDL.Status = "启用";
                        _SourceMDL.Description = dt.Rows[m]["课程简介"].ToString().Trim();
                        _SourceMDL.ParentSourceID = ParentSource.SourceID;
                        _SourceMDL.SortID = (m + 1) * 10;
                        _SourceMDL.Period = Convert.ToInt32(dt.Rows[m]["学时(分钟)"]);
                        _SourceMDL.ShowPeriod = SourceDAL.ConvertShowPeriod(_SourceMDL.Period.Value);
                        _SourceMDL.SourceWareCount = 0;
                        _SourceMDL.SourceWareUrl = dt.Rows[m]["课件地址url"].ToString().Trim();
                        _SourceMDL.SourceWarePlayParam = dt.Rows[m]["SDKID"].ToString().Trim();
                        _SourceMDL.SourceYear = Convert.ToInt32(dt.Rows[m]["年度"]);
                        SourceDAL.Insert(tran, _SourceMDL);

                        ParentSource.ShowPeriod += _SourceMDL.ShowPeriod;
                        ParentSource.SourceWareCount += 1;
                        SourceDAL.Update(tran, ParentSource);
                    }

                }
                tran.Commit();

                UIHelp.layerAlert(Page, "课程课件导入成功！");

            }
            catch (Exception ex)
            {
                tran.Rollback();
                UIHelp.WriteErrorLog(Page, "程课件导入失败！", ex);
                return;
            }

            UIHelp.WriteOperateLog(UserName, UserID, "导入课件信息", string.Format("导入数据行数：{0}行。", dt.Rows.Count));

        }
    }
}