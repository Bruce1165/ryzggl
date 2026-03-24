using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Model;
using Telerik.Web.UI;
using DataAccess;
using System.Data;
using System.IO;

namespace ZYRYJG.CertifManage
{
    public partial class CertifChangeConfirm : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PostSelect1.PostTypeID = string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString();
                LabelPostType.Text = UIHelp.GetPostTypeNameByID(string.IsNullOrEmpty(Request["o"]) ? "1" : Request["o"].ToString());//岗位类别ID
                PostSelect1.LockPostTypeID();

                btnSearch_Click(sender, e);
            }
        }

        //查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (UIHelp.CheckSQLParam() == false)
            {
                UIHelp.layerAlert(Page, "输入信息存在非法字符，请勿非法提交。");
                return;
            }

            ClearGridSelectedKeys(RadGrid1);
            ObjectDataSource1.SelectParameters.Clear();

            QueryParamOB q = new QueryParamOB();
            if (rdtxtWorkerName.Text.Trim() != "")    //姓名
            {
                q.Add(string.Format("WorkerName like '%{0}%'", rdtxtWorkerName.Text.Trim()));
            }
            if (rdtxtCertificateCode.Text.Trim() != "")  //证书编码
            {
                q.Add(string.Format("CertificateCode like '%{0}%'", rdtxtCertificateCode.Text.Trim()));
            }
            if (rdtxtZJHM.Text.Trim() != "")   //证件号码
            {
                q.Add(string.Format("WorkerCertificateCode like '%{0}%'", rdtxtZJHM.Text.Trim()));
            }
            if (rdtxtQYMC.Text.Trim() != "")   //原企业名称
            {
                q.Add(string.Format("UnitName like '%{0}%'", rdtxtQYMC.Text.Trim()));
            }
            if (RadTextBoxNewUnit.Text.Trim() != "")   //新企业名称
            {
                q.Add(string.Format("NewUnitName like '%{0}%'", RadTextBoxNewUnit.Text.Trim()));
            }
            if (RadTextBoxUnitCode.Text.Trim() != "")   //新企业机构代码
            {
                q.Add(string.Format("NewUnitcode like '{0}%'", RadTextBoxUnitCode.Text.Trim()));
            }
            if (PostSelect1.PostTypeID != "") //岗位类别
            {
                q.Add(string.Format("PostTypeID = {0}", PostSelect1.PostTypeID));
            }
            if (PostSelect1.PostID != "")//岗位工种
            {
                q.Add(string.Format("PostID >= {0} and PostID <= {0}", PostSelect1.PostID));
            }

            int posttypeid = Request.QueryString["o"] == null ? 1 : Convert.ToInt32(Request.QueryString["o"].ToString());
            q.Add(string.Format("PostTypeID={0}", posttypeid));

            if (RadioButtonListStatus.SelectedItem.Value == "已决定")
            {
                q.Add("[CONFRIMDATE] >'2000-1-1'");
                TableConfirm.Visible = false;
            }
            else//未决定
            {
                q.Add(string.Format("Status = '{0}'", EnumManager.CertificateChangeStatus.Checked));//已审核
                TableConfirm.Visible = true;
            }

            if (RadComboBoxChangeType.SelectedItem.Value != "")//变更类型
            {
                q.Add(string.Format("ChangeType='{0}'", RadComboBoxChangeType.SelectedItem.Value));
            }
            if (RadDatePicker_GetDateStart.SelectedDate.HasValue)//受理时间段起始
            {
                q.Add(string.Format("[GETDATE]>='{0}'", RadDatePicker_GetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd")));
            }
            if (RadDatePicker_GetDateEnd.SelectedDate.HasValue)//受理时间段截止
            {
                q.Add(string.Format("[GETDATE]<'{0}'", RadDatePicker_GetDateEnd.SelectedDate.Value.AddDays(1).ToString("yyyy-MM-dd")));
            }
            if (RadTextBoxGetMan.Text.Trim() != "")//受理人
            {
                q.Add(string.Format("GetMan ='{0}'", RadTextBoxGetMan.Text.Trim()));
            }

            ObjectDataSource1.SelectParameters.Add("filterWhereString", q.ToWhereString());
            RadGrid1.CurrentPageIndex = 0;
            RadGrid1.DataSourceID = ObjectDataSource1.ID;
        }

        //变更决定
        protected void ButtonDecide_Click(object sender, EventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "CertificateChangeID");//更新选择状态
            if (IsGridSelected(RadGrid1) == false)
            {
                UIHelp.layerAlert(Page, "至少选择一条数据！");
                return;
            }

            ViewState["certificateChangeIDList"] = null;

            string filterString = "";//过滤条件
            DataTable dt = null;//变更申请            

            if (GetGridIfCheckAll(RadGrid1) == true)//全选
            {
                filterString = ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue;
            }
            else
            {
                if (GetGridIfSelectedExclude(RadGrid1) == true)//排除
                    filterString = string.Format(" {0} and CertificateChangeID not in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
                else//包含
                    filterString = string.Format(" {0} and CertificateChangeID in({1})", ObjectDataSource1.SelectParameters["filterWhereString"].DefaultValue, GetGridSelectedKeysToString(RadGrid1));
            }

            try
            {
                dt = CertificateChangeDAL.GetList(0, int.MaxValue - 1, filterString, "CertificateChangeID");
            }
            catch (Exception ex)
            {
                UIHelp.WriteErrorLog(Page, "决定失败！", ex);
                return;
            }

            string _path = "";
            string _sourcePath = "";
            DBHelper dbhelper = new DBHelper();
            DbTransaction dtr = dbhelper.BeginTransaction();
            try
            {
                string bgjd = UIHelp.GetNextBatchNumber(dtr, "BGJD"); //变更决定编批号
                string bggz = UIHelp.GetNextBatchNumber(dtr, "BGGZ"); //变更告知编批号
                ViewState["NoticeCode"] = bggz;

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    CertificateChangeOB certhfchange = CertificateChangeDAL.GetObject(null, (long)dt.Rows[i]["CertificateChangeID"]);

                    if (RadioButtonListDecide.SelectedValue == "通过")
                    {
                        //更新变更记录
                        certhfchange.DealWay = "证书信息修改";      //证书处理方式 
                        certhfchange.ConfrimDate = DateTime.Now;  //变更决定时间 
                        certhfchange.ConfrimResult = "通过";  //变更决定结论
                        certhfchange.ConfrimMan = PersonName;   //变更决定人
                        certhfchange.ConfrimCode = bgjd;   //变更决定批号
                        certhfchange.Status = EnumManager.CertificateChangeStatus.Noticed;     //告知状态
                        certhfchange.NoticeDate = DateTime.Now; //变更告知时间                        
                        certhfchange.NoticeResult = "通过";  //变更告知结论
                        certhfchange.NoticeMan = PersonName;    //变更告知人
                        certhfchange.NoticeCode = bggz;   //变更告知批号
                        certhfchange.ModifyPersonID = PersonID;    //最后修改人
                        certhfchange.ModifyTime = DateTime.Now; ;   //最后修改时间                        
                        CertificateChangeDAL.Update(dtr, certhfchange);

                        //根据证书id向历史表插入历史数据
                        CertificateHistoryDAL.InsertChangeHistory(dtr, certhfchange.CertificateID.Value);

                        //获取原证书数据
                        CertificateOB certificateob = CertificateDAL.GetObject(certhfchange.CertificateID.Value);

                        #region 更换证书照片

                        //更换证书照片
                        if (certhfchange.IfUpdatePhoto.HasValue && certhfchange.IfUpdatePhoto.Value == 1)
                        { 
                            _sourcePath = string.Format("../UpLoad/ChangePhoto/{0}/{1}.jpg", certificateob.CertificateCode.Substring(certificateob.CertificateCode.Length - 3, 3), certhfchange.CertificateChangeID);
                            certificateob.FacePhoto = _sourcePath.Replace("..", "~");
                        }
                        //补充老照片
                        if (string.IsNullOrEmpty(certificateob.FacePhoto) == true)
                        {
                            certificateob.FacePhoto = null;//夜间同步照片
                        }

                        ////更换证书照片（专网无法更新照片）
                        //if (certhfchange.IfUpdatePhoto.HasValue && certhfchange.IfUpdatePhoto.Value == 1)
                        //{
                        //    _path = string.Format("../UpLoad/CertificatePhoto/{0}/{1}/", certificateob.PostTypeID, certificateob.CertificateCode.Substring(certificateob.CertificateCode.Length - 3, 3));
                        //    if (!Directory.Exists(Server.MapPath(_path)))
                        //    {
                        //        System.IO.Directory.CreateDirectory(Server.MapPath(_path));
                        //    }
                        //    _path = string.Format("{0}{1}.jpg", _path, certificateob.CertificateCode);
                        //    _sourcePath = string.Format("../UpLoad/ChangePhoto/{0}/{1}.jpg", certificateob.CertificateCode.Substring(certificateob.CertificateCode.Length - 3, 3), certhfchange.CertificateChangeID);
                        //    File.Copy(Server.MapPath(_sourcePath), Server.MapPath(_path), true);
                        //    certificateob.FacePhoto = _path.Replace("..", "~");
                        //}
                        ////补充老照片
                        //if (string.IsNullOrEmpty(certificateob.FacePhoto) == true)
                        //{
                        //    if (string.IsNullOrEmpty(certhfchange.WorkerCertificateCode) == false && certhfchange.WorkerCertificateCode.Length > 2)
                        //    {
                        //        _sourcePath = string.Format("../UpLoad/WorkerPhoto/{0}/{1}.jpg", certhfchange.WorkerCertificateCode.Substring(certhfchange.WorkerCertificateCode.Length - 3, 3), certhfchange.WorkerCertificateCode);
                        //    }
                        //    else
                        //    {
                        //        _sourcePath = string.Format("../UpLoad/WorkerPhoto/{0}/{1}.jpg", certhfchange.NewWorkerCertificateCode.Substring(certhfchange.NewWorkerCertificateCode.Length - 3, 3), certhfchange.NewWorkerCertificateCode);
                        //    }

                        //    _path = string.Format("../UpLoad/CertificatePhoto/{0}/{1}/", certificateob.PostTypeID, certificateob.CertificateCode.Substring(certificateob.CertificateCode.Length - 3, 3));

                        //    if (!Directory.Exists(Server.MapPath(_path)))
                        //    {
                        //        System.IO.Directory.CreateDirectory(Server.MapPath(_path));
                        //    }
                        //    _path = string.Format("{0}{1}.jpg", _path, certificateob.CertificateCode);
                        //    if (File.Exists(Server.MapPath(_sourcePath)) == true)//立即同步照片
                        //    {
                        //        File.Copy(Server.MapPath(_sourcePath), Server.MapPath(_path), true);
                        //        certificateob.FacePhoto = _path.Replace("..", "~");
                        //    }
                        //    else//夜间同步照片
                        //    {
                        //        certificateob.FacePhoto = null;
                        //    }
                        //}
                        #endregion 更换证书照片

                        //修改原表数据
                        certificateob.WorkerCertificateCode = certhfchange.NewWorkerCertificateCode;   //证件号码
                        certificateob.Birthday = certhfchange.NewBirthday;//出生日期
                        certificateob.Sex = certhfchange.NewSex;//性别                    
                        certificateob.WorkerName = certhfchange.NewWorkerName;    //姓名
                        certificateob.UnitName = certhfchange.NewUnitName;   //工作单位
                        certificateob.UnitCode = certhfchange.NewUnitCode;   //组织机构代码
                        certificateob.ModifyPersonID = certhfchange.ModifyPersonID;  //最后修改人
                        certificateob.ModifyTime = DateTime.Now;   //最后修改时间
                        certificateob.ValidStartDate = certhfchange.CheckDate.Value.Date;
                        certificateob.CheckDate = certhfchange.NoticeDate;    //审批时间
                        certificateob.CheckMan = certhfchange.ConfrimMan;      //审批人
                        certificateob.CheckAdvise = certhfchange.ConfrimResult;//审批意见
                        certificateob.Status = certhfchange.ChangeType;      //证书更新状态（变更类型）
                        //certificateob.CaseStatus = "已归档";//归档状态                        
                        certificateob.PrintCount = 1;
                        certificateob.ApplyMan = certhfchange.ApplyMan;//申请人
                        if (string.IsNullOrEmpty(certhfchange.Job) == false)
                        {
                            certificateob.Job = certhfchange.Job;
                        }
                        if (string.IsNullOrEmpty(certhfchange.SkillLevel) == false)
                        {
                            certificateob.SkillLevel = certhfchange.SkillLevel;
                        }
                        CertificateDAL.Update(dtr, certificateob);

                        #region 更新人员基本信息
                        WorkerOB _WorkerOB = WorkerDAL.GetUserObject(certificateob.WorkerCertificateCode);
                        if (_WorkerOB != null)//update
                        {
                            if (_WorkerOB.Birthday != certificateob.Birthday
                                || _WorkerOB.Sex != certificateob.Sex
                                || _WorkerOB.WorkerName != certificateob.WorkerName
                                )
                            {
                                _WorkerOB.Birthday = certificateob.Birthday;
                                _WorkerOB.Sex = certificateob.Sex;
                                _WorkerOB.WorkerName = certificateob.WorkerName;
                                WorkerDAL.Update(dtr, _WorkerOB);
                            }
                        }
                        else//new
                        {
                            _WorkerOB = new WorkerOB();
                            _WorkerOB.Birthday = certificateob.Birthday;
                            _WorkerOB.Sex = certificateob.Sex;
                            _WorkerOB.WorkerName = certificateob.WorkerName;
                            _WorkerOB.Phone = certhfchange.LinkWay;
                            _WorkerOB.CertificateCode = certificateob.WorkerCertificateCode;
                            if (certificateob.WorkerCertificateCode.Length == 15 || certificateob.WorkerCertificateCode.Length == 18)
                                _WorkerOB.CertificateType = "身份证";
                            else
                                _WorkerOB.CertificateType = "其它证件";
                            WorkerDAL.Insert(dtr, _WorkerOB);
                        }
                        #endregion 更新人员基本信息


                        #region 更新证书标准附件库

                        //更新证书附件中需要被覆盖的附件为历史附件
                        CommonDAL.ExecSQL(dtr, string.Format(@"
                                                            Insert into  COC_TOW_Person_FileHistory(HisID,FileID,PSN_RegisterNO ,WriteTime) 
                                                            SELECT newid(),[COC_TOW_Person_File].[FileID],[COC_TOW_Person_File].[PSN_RegisterNO],getdate()
                                                            from [dbo].[COC_TOW_Person_File]
                                                            inner join 
                                                            (
                        	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
                        	                                    from 
                        	                                    (
                        		                                    select distinct [FileInfo].DataType,[VIEW_CERTIFICATECHANGE].[CERTIFICATECODE] as PSN_RegisterNo
                        		                                    from [dbo].[FileInfo]
                        		                                    inner join [dbo].[ApplyFile]
                        		                                    on [FileInfo].FileID = [ApplyFile].FileID
                        		                                    inner join [dbo].[VIEW_CERTIFICATECHANGE] on [ApplyFile].ApplyID = 'BG-'+cast([VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID as varchar(64))
                        		                                    where [VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID={0} and [VIEW_CERTIFICATECHANGE].GetResult='通过'
                        	                                    ) a
                        	                                    inner join 
                        	                                    (
                        		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
                        		                                    from [dbo].[FileInfo]
                        		                                    inner join [dbo].[COC_TOW_Person_File]
                        		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                                                    inner join [dbo].[VIEW_CERTIFICATECHANGE] 
                                                                    on [COC_TOW_Person_File].PSN_RegisterNO = [VIEW_CERTIFICATECHANGE].CERTIFICATECODE
                        		                                    where  [VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID={0} and [VIEW_CERTIFICATECHANGE].GetResult='通过'
                        	                                    ) b 
                        	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                                            ) t
                                                            on [COC_TOW_Person_File].FileID = t.FileID", certhfchange.CertificateChangeID));


                        CommonDAL.ExecSQL(dtr, string.Format(@"
                                                            delete from [dbo].[COC_TOW_Person_File]
                                                            where FileID in( select [COC_TOW_Person_File].[FileID]
                                                            from [dbo].[COC_TOW_Person_File]
                                                            inner join 
                                                            (
                        	                                    select b.FileID, b.PSN_RegisterNO,b.DataType
                        	                                    from 
                        	                                    (
                        		                                    select distinct [FileInfo].DataType,[VIEW_CERTIFICATECHANGE].CERTIFICATECODE as PSN_RegisterNo
                        		                                    from [dbo].[FileInfo]
                        		                                    inner join [dbo].[ApplyFile]
                        		                                    on [FileInfo].FileID = [ApplyFile].FileID
                        		                                    inner join [dbo].[VIEW_CERTIFICATECHANGE] on [ApplyFile].ApplyID = 'BG-'+cast([VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID as varchar(64))
                        		                                    where [VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID={0} and [VIEW_CERTIFICATECHANGE].GetResult='通过'
                        	                                    ) a
                        	                                    inner join 
                        	                                    (
                        		                                    select [COC_TOW_Person_File].FileID, [COC_TOW_Person_File].PSN_RegisterNO,[FileInfo].DataType
                        		                                    from [dbo].[FileInfo]
                        		                                    inner join [dbo].[COC_TOW_Person_File]
                        		                                    on [FileInfo].FileID = [COC_TOW_Person_File].FileID
                                                                    inner join [dbo].[VIEW_CERTIFICATECHANGE] 
                                                                    on [COC_TOW_Person_File].PSN_RegisterNO = [VIEW_CERTIFICATECHANGE].CERTIFICATECODE
                        		                                    where  [VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID={0} and [VIEW_CERTIFICATECHANGE].GetResult='通过'
                        	                                    ) b 
                        	                                    on a.PSN_RegisterNo = b.PSN_RegisterNO and a.DataType = b.DataType
                                                            ) t
                                                            on [COC_TOW_Person_File].FileID = t.FileID
                                                            )", certhfchange.CertificateChangeID));

                        //将申请单附件写入证书附件库
                        CommonDAL.ExecSQL(dtr, string.Format(@"
                                                            INSERT INTO [dbo].[COC_TOW_Person_File]([FileID],[PSN_RegisterNO],[IsHistory])
                                                            select [ApplyFile].FileID,[VIEW_CERTIFICATECHANGE].CERTIFICATECODE,0 
                                                            from [dbo].[ApplyFile]
                                                            inner join [dbo].[VIEW_CERTIFICATECHANGE] 
                                                            on [ApplyFile].ApplyID = 'BG-'+cast([VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID as varchar(64))
                                                            where [VIEW_CERTIFICATECHANGE].CERTIFICATECHANGEID='{0}' and [VIEW_CERTIFICATECHANGE].GetResult='通过'", certhfchange.CertificateChangeID));



                        #endregion 更新证书标准附件库
                    }
                    else//决定不通过
                    {
                        certhfchange.Status = EnumManager.CertificateChangeStatus.SendBack;
                        certhfchange.ConfrimDate = DateTime.Now;   //变更决定时间
                        certhfchange.ConfrimResult = TextBoxConfrimResult.Text.Trim();     //变更决定结论
                        certhfchange.ConfrimMan = PersonName;    //变更决定人
                        certhfchange.ConfrimCode = bgjd;//变更决定批号

                        //修该变更记录
                        CertificateChangeDAL.Update(dtr, certhfchange);
                    }
                }

                dtr.Commit();

                UIHelp.WriteOperateLog(PersonName, UserID, "批量决定证书变更", string.Format("变更决定批号：{0}；证书数量：{1}本。", bgjd, dt.Rows.Count.ToString()));
            }
            catch (Exception ex)
            {
                dtr.Rollback();
                UIHelp.WriteErrorLog(Page, "批量决定变更失败！", ex);
                return;
            }
            ClearGridSelectedKeys(RadGrid1);
            RadGrid1.DataBind();//刷新grid

            UIHelp.layerAlert(Page, string.Format("您已经成功的决定了 {0} 条数据！", dt.Rows.Count), 6, 3000);
        }

        //Grid换页
        protected void RadGridAccept_PageIndexChanged(object source, GridPageChangedEventArgs e)
        {
            UpdateGridSelectedKeys(RadGrid1, "CertificateChangeID");
        }

        //Grid绑定勾选checkbox状态
        protected void RadGridAccept_DataBound(object sender, EventArgs e)
        {
            UpdateGriSelectedStatus(RadGrid1, "CertificateChangeID");
        }

        protected void RadioButtonListDecide_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (RadioButtonListDecide.SelectedValue == "通过")
            {
                TextBoxConfrimResult.Text = "决定通过";
            }
            else
            {
                TextBoxConfrimResult.Text = "退回修改，原因：";
            }
        }


    }
}
