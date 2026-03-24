using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Telerik.Web.UI;
using DataAccess;
using Model;

namespace ZYRYJG.jxjy
{
    public partial class QuestionMgr : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (string.IsNullOrEmpty(Request["o"]) == false)//从课程页面进入添加试题
                {
                    long sourceID = Convert.ToInt64(Utility.Cryptography.Decrypt(Request["o"]));
                     SourceMDL _SourceMDL = SourceDAL.GetObject(sourceID);
                     if (_SourceMDL != null)
                     {
                         SelectSource.SetValue(_SourceMDL);
                     }

                     if (Request["t"] == "add")
                     {
                         tableSearch.Style.Add("height", "0");
                     }
                }           

                ButtonSearch_Click( sender,  e);
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
            ClearGridSelectedKeys(RadGridQuestion);
            ObjectDataSource1.SelectParameters.Clear();
            QueryParamOB q = new QueryParamOB();

            if (RadTextBoxTitle.Text.Trim() != "")    //标题
            {
                q.Add(string.Format("Title like '%{0}%'", RadTextBoxTitle.Text.Trim()));
            }
            if (RadTextBoxQuestionNo.Text.Trim() != "")    //试题编号
            {
                q.Add(string.Format("QuestionNo like '{0}'", RadTextBoxQuestionNo.Text.Trim()));
            }
            if (RadComboBoxQuestionType.SelectedValue != "")
            {
                q.Add(string.Format("QuestionType = '{0}'", RadComboBoxQuestionType.SelectedValue));//分类
            }
            if (RadioButtonListFlag.SelectedValue != "")
            {
                q.Add(string.Format("Flag = '{0}'", RadioButtonListFlag.SelectedValue));//状态
            }

            q.Add(string.Format("( LastModifyTime >=  '{0}' AND  LastModifyTime <'{1}')"
                    , RadDatePicker_TimeStart.SelectedDate.HasValue ? RadDatePicker_TimeStart.SelectedDate.Value.ToString() : "1950-1-1"
                    , RadDatePicker_TimeEnd.SelectedDate.HasValue ? RadDatePicker_TimeEnd.SelectedDate.Value.AddDays(1).ToString() : "2100-1-1"));
            if(SelectSource.SourceID.HasValue==true)
            {
                q.Add(string.Format("SourceID = {0}", SelectSource.SourceID));//课程ID
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());

            RadGridQuestion.CurrentPageIndex = 0;
            RadGridQuestion.DataSourceID = ObjectDataSource1.ID;
        }

        //删除
        protected void RadGridQuestion_DeleteCommand(object source, GridCommandEventArgs e)
        {
            GridItem item = (GridItem)e.Item;
            Int64 QuestionID = Convert.ToInt64(item.OwnerTableView.DataKeyValues[item.ItemIndex]["QuestionID"]);
            //if (questionDAL.IfQuestionUsing(QuestionID) == true)
            //{
            //    UIHelp.layerAlert(Page, "试题已经使用，无法删除，但你可以使用停用功能！");
            //    return;
            //}
            try
            {              

                //删除主信息
                TrainQuestionDAL.Delete(QuestionID);                
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "删除公益教育试题失败！", ex);
                return;
            }
            UIHelp.layerAlert(Page, "删除公益教育试题成功！");
        }

        //导入试题
        //特别提示：如果前8行没有超过255长度，会被限制长度。处理办法：把一条长字段数据提前到前8行！！！！！！！！！！！！！！！！！！！！！！！
        protected void ButtonImportScore_Click(object sender, EventArgs e)
        {
            if(SelectSource.SourceID.HasValue==false)
            {
                UIHelp.layerAlert(Page, "请先按隶属课程查询后，在导入查询课程的试题！");
                return;
            }
            if (RadUploadSignUpTable.UploadedFiles.Count > 0)
            {
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/Question/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/Question/"));

                //上传excel
                string targetFolder = Server.MapPath("~/Upload/Question/");
                string filePath = Path.Combine(targetFolder, string.Format("ImortQuestion_{0}_{1}.xls", PersonID.ToString(), DateTime.Now.ToString("yyyyMMddHHmmss")));
                RadUploadSignUpTable.UploadedFiles[0].SaveAs(filePath, true);

                //读入DataSet再校验并保存
                DataSet dsImport = null;

                try
                {
                    dsImport = Utility.ExcelDealHelp.ImportExcell(filePath, "");
                }
                catch (Exception ex)
                {
            
                    Utility.FileLog.WriteLog("公益教育试题导入失败！", ex);
                    UIHelp.layerAlert(Page, string.Format("公益教育试题导入失败！{0}", ex.Message));
                    return;
                }

                if (dsImport == null || dsImport.Tables.Count == 0 || dsImport.Tables[0].Rows.Count == 0)
                {
                    RefreshGrid(0);
                    UIHelp.layerAlert(Page, "未找到任何数据!");
                    return;
                }
                else if (dsImport.Tables.Contains("试题导入模板")==false)
                {
                    UIHelp.layerAlert(Page, "导入excel未发现名称为“试题导入模板”的标签页!");
                    return;
                }
                else
                {
                    //if (VaildImportTemplate(dsImport.Tables[0]) == false)
                    //{
                    //    UIHelp.layerAlert(Page, "导入文件格式无效，请按考试计划名称查询后，再下载带有考生信息的模板，录入成绩后按科目分批导入!");
                    //    return;
                    //}
                    SaveImportData(dsImport.Tables["试题导入模板"]);//保存
                }
            }
            else
            {
                RefreshGrid(0);
            }
        }

        //保存试题信息
        protected void SaveImportData(DataTable dt)
        {
            System.Text.StringBuilder rtnErr = new System.Text.StringBuilder();//表单数据错误信息
            System.Text.StringBuilder rowErr = new System.Text.StringBuilder();//行数据错误信息

            // 删除空行
            for (int m = dt.Rows.Count - 1; m >= 0; m--)
            {
                if (dt.Rows[m]["试题类型"].ToString().Trim() == ""
                && dt.Rows[m]["题目"].ToString().Trim() == ""               
                && dt.Rows[m]["选项"].ToString().Trim() == ""
                )
                {
                    dt.Rows.RemoveAt(m);
                }
            }

            List<string> listQuestionType = new List<string>() { "单选题", "多选题", "判断题" };


//            DataTable tdPost= CommonDAL.GetDataTable(@"
//                select  distinct [POSTTYPENAME],[POSTNAME]
//                from VIEW_POSTINFO
//                union all
//                select distinct '二级建造师',[PRO_Profession]
//                from [dbo].[COC_TOW_Register_Profession]
//                union all
//                select distinct '二级造价工程师',[PSN_RegisteProfession]
//                from [dbo].[zjs_Certificate]");
//            DataColumn[] pk=new DataColumn[2];
//            pk[0]=tdPost.Columns["POSTTYPENAME"];
//            pk[1]=tdPost.Columns["POSTNAME"];
//            tdPost.PrimaryKey = pk;

            for (int m = 0; m < dt.Rows.Count; m++)
            {
                rowErr.Remove(0, rowErr.Length);//行错误清空

                ValidNull(dt, m, "试题编号", rowErr);
                ValidNull(dt, m, "试题类型", rowErr);
                ValidNull(dt, m, "题目", rowErr);
                ValidNull(dt, m, "标准答案", rowErr);
                ValidNull(dt, m, "选项", rowErr);

                if (listQuestionType.Contains(dt.Rows[m]["试题类型"].ToString().Trim()) == false)
                {
                    rowErr.Append("<br />试题类型只能输入（判断题，单选题，多选题）中的一项。");
                }
               
  

                if (rowErr.Length > 0)
                {
                    rtnErr.Append("<br />---第【").Append(Convert.ToString(m + 2)).Append("】行：-------------------------------");
                    rtnErr.Append(rowErr.ToString());
                }
            }

            if (rtnErr.Length > 0)
            {
                RefreshGrid(0);
                UIHelp.layerAlert(Page, rtnErr.ToString());
                return;
            }

            string[] answerlist = null;
            DateTime createTime = DateTime.Now;
            DBHelper db = new DBHelper("DBRYPX");	
            DbTransaction tran = db.BeginTransaction();

            DateTime modifyTime = DateTime.Now;
            try
            {
                TrainQuestionDAL.DeleteBySourceID(tran, SelectSource.SourceID.Value);

                for (int m = 0; m < dt.Rows.Count; m++)
                {
                    TrainQuestionMDL _questionOB = new TrainQuestionMDL();
                    _questionOB.QuestionNo = dt.Rows[m]["试题编号"].ToString().Trim();
                    _questionOB.Title = dt.Rows[m]["题目"].ToString().Replace("•", ".").Trim(); //标题
                    
                    _questionOB.QuestionType = dt.Rows[m]["试题类型"].ToString().Trim();//类型
                    _questionOB.LastModifyTime = DateTime.Now;//修改时间
                    _questionOB.Score = Convert.ToInt32(dt.Rows[m]["分数"]);//分数
                    _questionOB.Flag = "停用";//状态
                    _questionOB.Answer = dt.Rows[m]["标准答案"].ToString().Trim();  //标准答案
                    _questionOB.SourceID = SelectSource.SourceID;//隶属课程

                    TrainQuestionDAL.Insert(tran, _questionOB);

                    answerlist = dt.Rows[m]["选项"].ToString().Trim().Replace("；", ";").Split(';');
                    foreach (string a in answerlist)
                    {
                        if (a.Trim().Length < 2)
                        {
                            tran.Rollback();
                            UIHelp.layerAlert(Page, string.Format("导入失败，试题编号：{0}，的备选答案数据存在问题，请检查。 ！", _questionOB.QuestionNo));
                            return;
                        }
                        TrainQuestOptionMDL _questoptionOB = new TrainQuestOptionMDL();
                        _questoptionOB.QuestionID = _questionOB.QuestionID;
                        _questoptionOB.OptionNo = a.Trim().Substring(0, 1);
                        if (Utility.Check.IsQjChar(Convert.ToChar(_questoptionOB.OptionNo))==true)
                        {
                            _questoptionOB.OptionNo = Utility.Check.ToBj(_questoptionOB.OptionNo);
                        }
                        if ("ABCDEFG".Contains(_questoptionOB.OptionNo) == false)
                        {
                            tran.Rollback();
                            UIHelp.layerAlert(Page,string.Format( "导入失败，试题编号：{0}，的备选答案数据存在问题，请检查。 ！",_questionOB.QuestionNo));
                            return;
                        }

                        _questoptionOB.OptionContent = a.Trim().Substring(1).Trim().Replace("•", ".");
                      
                        if (_questoptionOB.OptionContent.Substring(0,1)==".")
                        {
                            if (_questoptionOB.OptionContent.Length<2)
                            {
                                tran.Rollback();
                                UIHelp.layerAlert(Page, string.Format("导入失败，试题编号：{0}，的备选答案数据存在问题，请检查。 ！", _questionOB.QuestionNo));
                                return;
                            }
                            _questoptionOB.OptionContent = _questoptionOB.OptionContent.Substring(1).Trim();
                        }
                        if (_questoptionOB.OptionContent.Substring(0,1) == "、")
                        {
                            if (_questoptionOB.OptionContent.Length < 2)
                            {
                                tran.Rollback();
                                UIHelp.layerAlert(Page, string.Format("导入失败，试题编号：{0}，的备选答案数据存在问题，请检查。 ！", _questionOB.QuestionNo));
                                return;
                            }
                            _questoptionOB.OptionContent = _questoptionOB.OptionContent.Substring(1).Trim();
                        }
                        TrainQuestOptionDAL.Insert(tran, _questoptionOB);
                    }
                }              

                tran.Commit();

                UIHelp.layerAlert(Page, "公益教育试题导入成功！");
                RefreshGrid(0);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                if (ex.Message.Contains("UK_TrainQuestion_QuestionNo")==true)
                {
                    UIHelp.layerAlert(Page, "导入失败，请检查试题编号是否重复 ！");
                    return;
                }
                Utility.FileLog.WriteLog("公益教育试题导入失败！", ex);
                UIHelp.layerAlert(Page, string.Format("公益教育试题导入失败！{0}",ex.Message));
                return;
            }
            UIHelp.WriteOperateLog(UserName,UserID, "导入公益教育试题", "");
        }

        /// <summary>
        /// 验证数据是否为空值
        /// </summary>
        /// <param name="dt">数据表</param>
        /// <param name="rowIndex">行</param>
        /// <param name="colName">列名</param>
        /// <param name="rowErr">行校验结果</param>
        private void ValidNull(DataTable dt, int rowIndex, string colName, System.Text.StringBuilder rowErr)
        {
            if (dt.Rows[rowIndex][colName].ToString().ToString().Trim() == "") rowErr.Append("<br />“").Append(colName).Append("”不能为空值！");
        }

        //刷新成绩Grid
        protected void RefreshGrid(int pageIndex)
        {
            ClearGridSelectedKeys(RadGridQuestion);
            RadGridQuestion.Rebind();
        }

        //Grid换页
        protected void RadGridQuestion_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGridQuestion, "QuestionID");
        }

        //Grid绑定勾选checkbox状态
        protected void RadGridQuestion_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGridQuestion, "QuestionID");
        }

        //启用试题
        protected void ButtonUsing_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridQuestion, "QuestionID");
            if (IsGridSelected(RadGridQuestion) == false)
            {
                UIHelp.layerAlert(Page, "至少选择一条数据！");
                return;
            }

            string filterString = "";//过滤条件
            if (GetGridIfCheckAll(RadGridQuestion) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGridQuestion) == true)//排除
                    filterString = string.Format(" {0} and QuestionID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridQuestion));
                else//包含
                    filterString = string.Format(" {0} and QuestionID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridQuestion));
            }


            //启用
            DateTime updateTime = DateTime.Now;
            try
            {
                TrainQuestionDAL.UpdateFlag(filterString, "启用", updateTime);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "批量启用公益教育试题失败！", ex);
                return;
            }
            ClearGridSelectedKeys(RadGridQuestion);
            RadGridQuestion.DataBind();
            UIHelp.WriteOperateLog(UserName,UserID,  "批量启用公益教育试题", "");
        }

        //停用试题
        protected void ButtonStop_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGridQuestion, "QuestionID");
            if (IsGridSelected(RadGridQuestion) == false)
            {
                UIHelp.layerAlert(Page, "至少选择一条数据！");
                return;
            }

            string filterString = "";//过滤条件
            if (GetGridIfCheckAll(RadGridQuestion) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGridQuestion) == true)//排除
                    filterString = string.Format(" {0} and QuestionID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridQuestion));
                else//包含
                    filterString = string.Format(" {0} and QuestionID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGridQuestion));
            }


            //停用
            DateTime updateTime = DateTime.Now;
            try
            {
                TrainQuestionDAL.UpdateFlag(filterString, "停用", updateTime);
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "批量停用公益教育试题失败！", ex);
                return;
            }
            ClearGridSelectedKeys(RadGridQuestion);
            RadGridQuestion.DataBind();
            UIHelp.WriteOperateLog(UserName,UserID, "批量停用公益教育试题", "");
        }
    }
}