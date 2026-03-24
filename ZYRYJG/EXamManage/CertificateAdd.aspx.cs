using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;
using Telerik.Web.UI;
using System.Data;
using Utility;
using System.IO;
using System.Text.RegularExpressions;

namespace ZYRYJG.EXamManage
{
    public partial class CertificateAdd : BasePage
    {       

        protected void Page_Load(object sender, EventArgs e)
        {  
            if (!this.IsPostBack)
            {
                UIHelp.FillDropDownList(RadComboBoxCertificateType, "102");//证件类型
                UIHelp.FillDropDownList(RadComboBoxNation, "108", "请选择", "");//民族
                UIHelp.FillDropDownList(RadComboBoxCulturalLevel, "109", "请选择", "");//学历
                UIHelp.FillDropDownList(RadComboBoxPoliticalBackground, "110", "请选择", "");//政治面貌       
                UIHelp.FillDropDownList(RadComboBoxSKILLLEVEL, "111", "请选择", "");//技术等级 
                UIHelp.FillDropDownList(RadComboBoxJob, "112", "请选择", "");//职务 
            }
        }

        //根据证件号码显示相应的信息
        protected void RadTextCertificateCode_TextChanged(object sender, EventArgs e)
        {            
            if (RadTextCertificateCode.Text.Length < 5)
            {
                UIHelp.layerAlert(Page, "证件号码的位数不对！");
                return;
            }
            RadTextBoxWorkerName.Text = "";
            RadComboBoxNation.Text = "";
            RadComboBoxNation.SelectedIndex = -1;
            RadComboBoxCulturalLevel.Text = "";
            RadComboBoxCulturalLevel.SelectedIndex = -1;
            RadComboBoxPoliticalBackground.Text = "";
            RadComboBoxPoliticalBackground.SelectedIndex = -1;
            RadTextBoxUnitCode.Text = "";
            RadTextBoxUnitName.Text = "";

            RadDatePickerBirthday.SelectedDate = null;

            //考生信息
            string certificatecode = RadTextCertificateCode.Text.ToString();
            WorkerOB workerob = WorkerDAL.GetUserObject(certificatecode);   //根据证件号码得到用户
            if (workerob != null)
            {
                RadComboBoxCertificateType.FindItemByValue(workerob.CertificateType).Selected = true;
                RadComboBoxCertificateType.Text = workerob.CertificateType;   //证件类别
                System.Random rm = new Random();
                ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", rm.Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(certificatecode))); //绑定照片;

                RadTextBoxWorkerName.Text = workerob.WorkerName;     //姓名
                if (workerob.Sex == "男")  //性别
                {
                    RadioButtonMan.Checked = true;
                    RadioButtonWoman.Checked = false;
                }
                else
                {
                    RadioButtonMan.Checked = false;
                    RadioButtonWoman.Checked = true;
                }
                RadDatePickerBirthday.SelectedDate = workerob.Birthday;  //出身日期
                RadTextBoxPhone.Text = workerob.Phone;  //联系电话
                if (RadComboBoxNation.FindItemByText(workerob.Nation) != null) //民族
                {
                    RadComboBoxNation.FindItemByText(workerob.Nation).Selected = true;
                    RadComboBoxNation.Text = workerob.Nation;
                }
                if (RadComboBoxCulturalLevel.FindItemByText(workerob.CulturalLevel) != null) //文化程度
                {
                    RadComboBoxCulturalLevel.FindItemByText(workerob.CulturalLevel).Selected = true;
                    RadComboBoxCulturalLevel.Text = workerob.CulturalLevel;
                }
                if (RadComboBoxPoliticalBackground.FindItemByText(workerob.PoliticalBackground) != null) //政治面貌  
                {
                    RadComboBoxPoliticalBackground.FindItemByText(workerob.PoliticalBackground).Selected = true;
                    RadComboBoxPoliticalBackground.Text = workerob.PoliticalBackground;
                }        
            }
            else
            {
                if (RadComboBoxCertificateType.SelectedItem.Value == "身份证")//根据身份证读取性别和出生日期
                {
                    if (RadComboBoxCertificateType.SelectedItem.Value == "身份证")
                    {
                        if (RadTextCertificateCode.Text.Trim().Length != 18)
                        {
                            UIHelp.layerAlert(Page, "“身份证”只能为18位（请使用最新证件）！");//不能用15为证件
                            return;
                        }
                        else if (Utility.Check.isChinaIDCard(RadTextCertificateCode.Text.Trim()) == false)
                        {
                            UIHelp.layerAlert(Page, "“身份证”格式不正确！");
                            return;
                        }
                    }

                    //18位身份证号码：第7、8、9、10位为出生年份(四位数)，第11、第12位为出生月份，第13、14位代表出生日期，第17位代表性别，奇数为男，偶数为女。
                    if (certificatecode.Length == 18)
                    {
                        string BirthDay = string.Format("{0}-{1}-{2}", certificatecode.Substring(6, 4), certificatecode.Substring(10, 2), certificatecode.Substring(12, 2));
                        RadDatePickerBirthday.SelectedDate = Convert.ToDateTime(BirthDay);
                        //性别
                        int sex = Convert.ToInt32(certificatecode.Substring(16, 1));
                        if (sex % 2 == 0)
                        {
                            RadioButtonWoman.Checked = true;
                            RadioButtonMan.Checked = false;
                        }
                        else
                        {
                            RadioButtonMan.Checked = true;//代表男
                            RadioButtonWoman.Checked = false;
                        }
                    }
                    //else if (certificatecode.Length == 15)
                    //{
                    //    //15位身份证号码：第7、8位为出生年份(两位数)，第9、10位为出生月份，第11、12位代表出生日期，第15位代表性别，奇数为男，偶数为女。 
                    //    string BirthDay = string.Format("19{0}-{1}-{2}", certificatecode.Substring(6, 2), certificatecode.Substring(8, 2), certificatecode.Substring(10, 2));
                    //    RadDatePickerBirthday.SelectedDate = Convert.ToDateTime(BirthDay);
                    //    //性别
                    //    int sex = Convert.ToInt32(certificatecode.Substring(14, 1));
                    //    if (sex % 2 == 0)
                    //    {
                    //        RadioButtonWoman.Checked = true;
                    //    }
                    //    else
                    //    {
                    //        RadioButtonMan.Checked = true;//代表男
                    //    }
                    //}
                }

                ImgCode.Src = "~/Images/photo_ry.jpg";
            }
        }

        /// <summary>
        /// 获取个人照片地址
        /// </summary>
        /// <param name="CertificateCode">证件号码</param>
        /// <returns></returns>
        public string GetFacePhotoPath(string CertificateCode)
        {
            if (CertificateCode == "") return "~/Images/photo_ry.jpg";
            string path = path = string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", CertificateCode.Substring(CertificateCode.Length - 3, 3), CertificateCode);
            if (File.Exists(Server.MapPath(path)) == true)
                return path;
            else
                return "~/Images/photo_ry.jpg";
        }

        //根据组织机构代码显示单位名称
        protected void RadTextBoxUnitCode_TextChanged(object sender, EventArgs e)
        {
            ToDBC(RadTextBoxUnitCode.Text.ToString());
            string unitcode = RadTextBoxUnitCode.Text.ToString();  //组织机构代码
            UnitMDL unitInfoob = UnitDAL.GetObjectByENT_OrganizationsCode(unitcode);
            if (unitInfoob != null)
            {
                RadTextBoxUnitName.Text = unitInfoob.ENT_Name;  //单位名称
            }
        }

        //根据企业名称获取企业信息
        protected void RadTextBoxUnitName_TextChanged(object sender, EventArgs e)
        {
            string UnitName = RadTextBoxUnitName.Text.ToString();  //单位名称
            UnitMDL unitInfoob = UnitDAL.GetObjectByUnitName(UnitName);
            if (unitInfoob != null)
            {
                RadTextBoxUnitCode.Text = unitInfoob.ENT_OrganizationsCode;  //组织机构代码
            }
        }

        //将半角转换为全角
        public string ToDBC(string strInput)
        {
            /*全角字符从的unicode编码从65281~65374,半角字符从的unicode编码从33~126   
        差值65248,空格比较特殊,全角为12288,半角为32 */
            char[] c = strInput.ToCharArray();
            for (int i = 0; i < c.Length; i++)
            {
                if (c[i] == 12288)
                {
                    c[i] = (char)32;
                }
                else if (c[i] > 65280 && c[i] < 65375)
                {
                    c[i] = (char)(c[i] - 65248);
                }
                if (checkString(c[i].ToString()))
                {
                    UIHelp.layerAlert(Page, "不能输入特殊字符！");
                    break;
                }
            }
            return new string(c);
        }

        //输入组织机构代码是检验
        protected void RadTextBoxUnitCode_KeyPress(object sender, EventArgs e)
        {
            ToDBC(RadTextBoxUnitCode.Text.ToString());
        }

        /// <summary>
        /// 严整特殊字符
        /// </summary>
        public static bool checkString(string source)
        {
            Regex regExp = new Regex("[~!@#$%^&*()=+[\\]{}''\";:/?.,><`|！·￥…—（）\\-、；：。，》《]");
            return regExp.IsMatch(source);
        }

        //保存证书
        protected void ButtonSave_Click(object sender, EventArgs e)
        {
            if (PostSelect1.PostID == "")
            {
                UIHelp.layerAlert(Page, " 请选择一个岗位工种！");
                return;
            }
            if (RadComboBoxCertificateType.SelectedItem.Value == "身份证")
            {
                if (RadTextCertificateCode.Text.Trim().Length != 18)
                {
                    UIHelp.layerAlert(Page, "“身份证”只能为18位（请使用最新证件）！");//不能用15为证件
                    return;
                }
                else if (Utility.Check.isChinaIDCard(RadTextCertificateCode.Text.Trim()) == false)
                {
                    UIHelp.layerAlert(Page, "“身份证”格式不正确！");
                    return;
                }
            }
            if (UIHelp.UnitCodeCheck(this.Page, RadTextBoxUnitCode.Text) == false)
            {
                UIHelp.layerAlert(Page, "“组织机构代码”格式不正确！（请使用9位数字或大写字母组，其中不能带有“-”横杠）");
                return;
            }
            if (PostSelect1.PostTypeID=="1"  && RadComboBoxJob.SelectedValue == "")
            {
                UIHelp.layerAlert(Page, "三类人证书必须选择所在单位职务。", 5, 0);
                return;
            }

            if (PostSelect1.PostID == "148" && RadComboBoxJob.SelectedItem.Text != "项目负责人（项目经理）")
            {
                UIHelp.layerAlert(Page, "项目负责人职务必须选择“项目负责人（项目经理）”。", 5, 0);
                return;
            }
            if ((PostSelect1.PostID == "6" || PostSelect1.PostID == "1123" || PostSelect1.PostID == "1125") && RadComboBoxJob.SelectedItem.Text != "专职安全生产管理人员")
            {
                UIHelp.layerAlert(Page, "专职安全生产管理人员职务必须选择“专职安全生产管理人员”。", 5, 0);
                return;
            }
            if (PostSelect1.PostID == "147"
                && (RadComboBoxJob.SelectedItem.Text == "项目负责人（项目经理）" || RadComboBoxJob.SelectedItem.Text == "专职安全生产管理人员")
                )
            {
                UIHelp.layerAlert(Page, "企业主要负责人职务不允许选择“项目负责人（项目经理）”或“专职安全生产管理人员”，请选择其他职务。", 5, 0);
                return;
            }

            if (RadUploadFacePhoto.UploadedFiles.Count > 0)
            {
                if (RadUploadFacePhoto.UploadedFiles[0].GetExtension().ToLower() != ".jpg")
                {
                    UIHelp.layerAlert(Page, "报名照片格式不正确！只能是有jpg格式图片");
                    return;
                }
                if (RadUploadFacePhoto.UploadedFiles[0].ContentLength > 51200)
                {
                    UIHelp.layerAlert(Page, "报名照片大小不能超过50k！");
                    return;
                }
            }
            if (RadUploadFacePhoto.UploadedFiles.Count == 0)//照片
            {
                if (!File.Exists(Server.MapPath(string.Format("~/UpLoad/WorkerPhoto/{0}/{1}.jpg", RadTextCertificateCode.Text.Trim().Substring(RadTextCertificateCode.Text.Trim().Length - 3, 3), RadTextCertificateCode.Text.Trim()))))
                {
                    UIHelp.layerAlert(Page, "必须上传照片！");
                    return;
                }
            }

            //检查该人是否持有相同岗位工种有效未过期的证书
            CertificateOB certificateob = CertificateDAL.GetCertificateOBObject(RadTextCertificateCode.Text.Trim().Replace("x", "X"), Convert.ToInt32(PostSelect1.PostID), DateTime.Now);
            if (certificateob != null && CertificateDAL.CheckRegular_UnitMaster(certificateob, RadTextBoxUnitCode.Text.Trim()) == false)
            {
                //特殊处理：三类人岗位
                //每人允许持有多个“企业主要负责人”证书，但必须是不同单位的；
                //每人只能有一个“项目负责人”和“专职安全生产管理人员”证，但两本必须在同一单位。
                UIHelp.layerAlert(Page, "已有相同岗位证书，不能创建！");
                return;
            }
            if (certificateob != null)
            {
                //报名限制检查:不能在同一家公司取得多个企业负责人证书
                if (PostSelect1.PostID == "147")
                {
                    if (CertificateDAL.CheckRegular_UnitMaster(certificateob, RadTextBoxUnitCode.Text.Trim()) == false)
                    {
                        UIHelp.layerAlert(Page, "不能在同一单位取得多个“企业主要负责人”证书！");
                        return;
                    }
                }
                else //报名限制检查:检查该人是否持有有效的（有效期未过）
                {
                    UIHelp.layerAlert(Page, "已有相同岗位证书，不能创建！");
                    return;
                }
            }
            //检验：每人只能有一个"项目负责人"和"专职安全生产管理人员"证，但两本必须在同一单位
            if (CertificateDAL.CheckRegular_ItemMaster(RadTextCertificateCode.Text.Trim(), RadTextBoxUnitCode.Text.Trim(), Convert.ToInt32(PostSelect1.PostID), RadComboBoxJob.SelectedItem.Text) == false)
            {
                UIHelp.layerAlert(Page, "同时持有多本安管人员ABC证书的，其项目负责人B本、专职安管人员C本应与其一本法人A本证书工作单位一致。只允许在同一单位同时取得“项目负责人”和“专职安全生产管理人员(C1、C2、C3)”证书，并且有C3不能再持有C1和C2！");
                return;
            }
   
            DBHelper dbhelper = new DBHelper();
            DbTransaction dtr = dbhelper.BeginTransaction();

            WorkerOB workerob = WorkerDAL.GetUserObject(RadTextCertificateCode.Text.ToString());   //根据证件号码得到用户
            UnitMDL _UnitMDL = UnitDAL.GetObjectByENT_OrganizationsCode(RadTextBoxUnitCode.Text.ToString());//组织机构代码  
            bool IsAddItem = false;//是否为增项
            string Temp_CertificateCode = "";//证书编号
            try
            {
                #region 向从业人员表插入/修改数据

                if (workerob == null) workerob = new WorkerOB();
                workerob.Sex = RadioButtonMan.Checked == true ? "男" : "女";   //性别
                workerob.Birthday = RadDatePickerBirthday.SelectedDate.Value;  //出身日期
                workerob.Nation = (RadComboBoxNation.Text == "请选择" ? "" : RadComboBoxNation.Text);   //民族
                workerob.CulturalLevel = (RadComboBoxCulturalLevel.Text == "请选择" ? "" : RadComboBoxCulturalLevel.Text);   //文化程度
                workerob.PoliticalBackground = (RadComboBoxPoliticalBackground.Text == "请选择" ? "" : RadComboBoxPoliticalBackground.Text);  //政治面貌
                workerob.CertificateCode = RadTextCertificateCode.Text.Trim().Replace("x", "X");  //证件号码
                workerob.Phone = RadTextBoxPhone.Text.Trim();  //联系电话
                workerob.WorkerName = RadTextBoxWorkerName.Text.ToString();     //姓名
                if (workerob.WorkerID.HasValue == false)//new
                {
                    workerob.CertificateType = RadComboBoxCertificateType.SelectedItem.Value;   //证件类别
                    WorkerDAL.Insert(dtr, workerob);
                }

                #endregion

                #region 向机构表插入数据

                if (_UnitMDL == null) 
                {
                    _UnitMDL = new UnitMDL();

                    //企业资质
                    jcsjk_QY_ZHXXMDL _jcsjk_QY_ZHXXMDL = jcsjk_QY_ZHXXDAL.GetObjectZZJGDM(RadTextBoxUnitCode.Text.ToString());

                    if (_jcsjk_QY_ZHXXMDL != null)//有资质
                    {
                        _UnitMDL.UnitID = Guid.NewGuid().ToString();
                        _UnitMDL.BeginTime = _jcsjk_QY_ZHXXMDL.JLSJ;//建立时间
                        _UnitMDL.EndTime = Convert.ToDateTime("2500-01-01");//截止时间
                        _UnitMDL.ENT_Name = _jcsjk_QY_ZHXXMDL.QYMC;//企业名称
                        _UnitMDL.ENT_OrganizationsCode = RadTextBoxUnitCode.Text.ToString();//组织机构代码
                        _UnitMDL.ENT_Economic_Nature = _jcsjk_QY_ZHXXMDL.JJLX;//企业类型
                        _UnitMDL.ENT_City = _jcsjk_QY_ZHXXMDL.XZDQBM;//区县
                        _UnitMDL.END_Addess = _jcsjk_QY_ZHXXMDL.ZCDZ;//注册地址
                        _UnitMDL.ENT_Corporate = _jcsjk_QY_ZHXXMDL.FDDBR;//法定代表人
                        _UnitMDL.ENT_Correspondence = _jcsjk_QY_ZHXXMDL.XXDZ;//企业通讯地址
                        _UnitMDL.ENT_Type = _jcsjk_QY_ZHXXMDL.SJLX;  //企业类型

                        if (_jcsjk_QY_ZHXXMDL.ZXZZ == null)
                        {
                            _UnitMDL.ENT_Sort = "";
                            _UnitMDL.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ;
                        }
                        else
                        {
                            //企业资质等级
                            if (string.IsNullOrEmpty(_jcsjk_QY_ZHXXMDL.ZXZZDJ) == true)
                            {
                                if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("特级"))
                                {
                                    _UnitMDL.ENT_Grade = "特级";
                                }
                                else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("壹级"))
                                {
                                    _UnitMDL.ENT_Grade = "一级";
                                }
                                else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("贰级"))
                                {
                                    _UnitMDL.ENT_Grade = "二级";
                                }
                                else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("叁级"))
                                {
                                    _UnitMDL.ENT_Grade = "三级";
                                }
                                else if (_jcsjk_QY_ZHXXMDL.ZXZZ.Contains("不分等级"))
                                {
                                    _UnitMDL.ENT_Grade = "不分等级";
                                }
                                else
                                {
                                    _UnitMDL.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ;
                                }
                            }
                            else
                            {
                                _UnitMDL.ENT_Grade = _jcsjk_QY_ZHXXMDL.ZXZZDJ; //企业资质等级
                            }
                            _UnitMDL.ENT_Sort = _jcsjk_QY_ZHXXMDL.ZXZZ.Split(';')[0];   //资质序列
                            string[] ZZ = _jcsjk_QY_ZHXXMDL.ZXZZ.Split(';');
                            if (_UnitMDL.ENT_Grade != null)
                            {
                                foreach (var item in ZZ)
                                {
                                    if (item.Replace("壹级", "一级").Replace("贰级", "二级").Replace("叁级", "三级").Contains(_UnitMDL.ENT_Grade) == true)
                                    {
                                        _UnitMDL.ENT_Sort = item;   //资质序列
                                        break;
                                    }

                                }
                            }

                        }
                        _UnitMDL.ENT_QualificationCertificateNo = _jcsjk_QY_ZHXXMDL.ZZZSBH;  //企业资质证书编号
                        _UnitMDL.CreditCode = "";//社会统一信用代码
                        _UnitMDL.Valid = 1;//是否有效
                        _UnitMDL.ResultGSXX = 0;
                    }
                    else//无资质设置为新设立企业
                    {
                        _UnitMDL.UnitID = Guid.NewGuid().ToString();
                        _UnitMDL.BeginTime = DateTime.Now;//建立时间
                        _UnitMDL.EndTime = Convert.ToDateTime("2500-01-01");//截止时间
                        _UnitMDL.ENT_Name = RadTextBoxUnitName.Text.Trim();//企业名称
                        _UnitMDL.ENT_OrganizationsCode = RadTextBoxUnitName.Text.Trim();//组织机构代码
                        _UnitMDL.ENT_Economic_Nature = "";//企业类型
                        _UnitMDL.ENT_City = "";//区县
                        _UnitMDL.END_Addess = "";//注册地址
                        _UnitMDL.ENT_Corporate = "";//法定代表人
                        //_UnitMDL.ENT_Correspondence = corpinfo.Reg_Address;//企业通讯地址 大厅无企业通讯地址企业自行维护
                        _UnitMDL.ENT_Type = "";  //企业类型
                        _UnitMDL.ENT_Sort = "新设立企业";   //企业类别
                        _UnitMDL.ENT_Grade = "新设立企业"; //企业资质等级
                        _UnitMDL.ENT_QualificationCertificateNo = "无";  //企业资质证书编号
                        _UnitMDL.CreditCode = "";//社会统一信用代码
                        _UnitMDL.Valid = 1;//是否有效
                        _UnitMDL.Memo = "新设立企业";
                        _UnitMDL.CJSJ = DateTime.Now;
                        _UnitMDL.ResultGSXX = 0;
                    }

                    UnitDAL.Insert(dtr, _UnitMDL);
                }

                #endregion

                #region 添加证书

                //检查证书增项
                DataTable dtCertificate = null;
                if (PostSelect1.PostTypeID == "3")//造价员
                {
                    //查看是否有造价员其它工种证书
                    dtCertificate = CertificateDAL.GetList(0, 1,
                        string.Format(" and WorkerCertificateCode='{0}' and PostTypeID={1} and PostID <> {2} and VALIDENDDATE >= getdate() and [STATUS] <>'注销' and [STATUS] <>'离京变更'"
                        , workerob.CertificateCode
                        , PostSelect1.PostTypeID
                    , PostSelect1.PostID)
                    , "CertificateID");
                }

                if (dtCertificate != null && dtCertificate.Rows.Count > 0)//增项
                {
                    IsAddItem = true;
                    CertificateOB o = CertificateDAL.GetObject(Convert.ToInt64(dtCertificate.Rows[0]["CertificateID"]));

                    //添加增项记录
                    CertificateAddItemOB _CertificateAddItemOB = new CertificateAddItemOB();
                    _CertificateAddItemOB.CertificateID = o.CertificateID;//证书ID
                    _CertificateAddItemOB.PostTypeID = Convert.ToInt32(PostSelect1.PostTypeID);//岗位
                    _CertificateAddItemOB.PostID = Convert.ToInt32(PostSelect1.PostID);//工种
                    CertificateAddItemDAL.Insert(dtr, _CertificateAddItemOB);

                    //更新证书表增项名称
                    o.ModifyTime = DateTime.Now;//修改时间
                    o.ModifyPersonID = PersonID;//修改人
                    o.AddItemName = dtCertificate.Rows[0]["PostName"].ToString() + CertificateAddItemDAL.GetAddItemNameString(dtr,o.CertificateID.Value);
                    CertificateDAL.Update(dtr, o);
                    Temp_CertificateCode = o.CertificateCode;
                }
                else//发证
                {
                    //创建证书
                    CertificateOB ceron = new CertificateOB();
                    ceron.ExamPlanID = EnumManager.CertificateExamPlanID.CertificateAdd;//考试计划ID
                    ceron.WorkerID = workerob.WorkerID;//从业人员ID
                    ceron.WorkerName = workerob.WorkerName;//姓名
                    ceron.WorkerCertificateCode = workerob.CertificateCode;//证件号码                        
                    ceron.Sex = RadioButtonMan.Checked == true ? "男" : "女";   //性别
                    ceron.Birthday = RadDatePickerBirthday.SelectedDate.Value;  //出身日期

                    ceron.UnitName = _UnitMDL.ENT_Name;//工作单位
                    ceron.UnitCode = _UnitMDL.ENT_OrganizationsCode;//组织机构代码                       

                    ceron.PostTypeID = Convert.ToInt32(PostSelect1.PostTypeID);//岗位
                    ceron.PostID = Convert.ToInt32(PostSelect1.PostID);//工种
                    ceron.PostTypeName = PostSelect1.PostTypeName;//岗位
                    ceron.PostName = PostSelect1.PostName;//工种
                    if (RadioButtonAutoAllocateNo.Checked == true)//自动编号
                    {
                        PostInfoOB _PostInfoOB = PostInfoDAL.GetObject(Convert.ToInt32(PostSelect1.PostID));
                        ceron.CertificateCode = PostInfoDAL.GetNextCertificateNo(ref _PostInfoOB, dtr);//证书编号
                        Temp_CertificateCode = ceron.CertificateCode;
                    }
                    else//手工编号
                    {
                        ceron.CertificateCode = RadTextBoxCertificateCode.Text;
                        Temp_CertificateCode = RadTextBoxCertificateCode.Text;
                    }
                    ceron.ValidStartDate = RadDatePickerValidStartDate.SelectedDate.Value;//证书有效期起
                    ceron.ValidEndDate = RadDatePickerValidEndDate.SelectedDate.Value;//证书有效期止
                    ceron.ConferDate = RadDatePickerConferDate.SelectedDate.Value;//发证日期
                    ceron.ConferUnit = RadTextBoxConferUnit.Text; //发证单位
                    ceron.CreatePersonID = PersonID;//创建人ID
                    ceron.CreateTime = DateTime.Now;//创建时间
                    ceron.Status = EnumManager.CertificateUpdateType.WaitCheck;
                    ceron.Remark = "证书补登记";
                    ceron.ApplyMan = PersonName;
                    ceron.SkillLevel = (RadComboBoxSKILLLEVEL.Text == "请选择" ? "" : RadComboBoxSKILLLEVEL.Text);//技术职称(技术等级)
                    ceron.CaseStatus = "已办结";//不打印

                    if (RadComboBoxJob.SelectedItem.Text != "请选择")
                    {
                        ceron.Job = RadComboBoxJob.SelectedItem.Text;//职务
                    }

                    CertificateDAL.Insert(dtr, ceron);
                }

                #endregion

                #region 上传个人照片
                //个人照片存放路径(按证件号码后3位)
                //if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/"));
                if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/WorkerPhoto/"))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/WorkerPhoto/"));

                if (RadUploadFacePhoto.UploadedFiles.Count > 0)//上传照片
                {
                    string workerPhotoFolder = "~/UpLoad/WorkerPhoto/";//个人照片存放路径
                    string subPath = "";//照片分类目录（证件号码后3位）
                    foreach (UploadedFile validFile in RadUploadFacePhoto.UploadedFiles)
                    {
                        subPath = workerob.CertificateCode;
                        subPath = subPath.Substring(subPath.Length - 3, 3);//图片按证件号后3位分目录存储
                        if (!Directory.Exists(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath))) System.IO.Directory.CreateDirectory(Page.Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath));
                        workerPhotoFolder = Server.MapPath("~/UpLoad/WorkerPhoto/" + subPath + "/");
                        validFile.SaveAs(Path.Combine(workerPhotoFolder, workerob.CertificateCode + ".jpg"), true);
                        break;
                    }
                }

                //绑定照片
                ImgCode.Src = string.Format("~/EXamManage/ExamSignimage.aspx?r={0}&o={1}", new Random().Next().ToString(), Utility.Cryptography.Encrypt(GetFacePhotoPath(workerob.CertificateCode))); //绑定照片;
                #endregion

                dtr.Commit();
            }
            catch (Exception ex)
            {
                dtr.Rollback();
                UIHelp.WriteErrorLog(Page, "证书补登记失败。", ex);
                return;
            }

            if (IsAddItem == true)
            {
                UIHelp.WriteOperateLog(PersonName, UserID, "证书补登记", string.Format("为编号为“{0}”的{1}证书添加了一个增项，增项岗位工种：{2}。"
                   , Temp_CertificateCode, PostSelect1.PostTypeName, PostSelect1.PostName));

                UIHelp.layerAlert(Page, "添加了一个证书增项，没有产生新的证书编号，请通知相关人员到服务大厅办理证书增项。");
            }
            else
            {
                UIHelp.WriteOperateLog(PersonName, UserID, "证书补登记", string.Format("证书编号：{0}；岗位类别：{1}；岗位工种：{2}；姓名：{3}"
                   , Temp_CertificateCode
                   , PostSelect1.PostTypeName, PostSelect1.PostName
                , RadTextBoxWorkerName.Text));

                UIHelp.layerAlert(Page, "证书补登记成功，已提交审核人审核。");
            }

        }
    }
}
