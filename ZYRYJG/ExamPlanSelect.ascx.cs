using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using Model;

namespace ZYRYJG
{
    public partial class ExamPlanSelect : System.Web.UI.UserControl
    {
        //定义一个委托
        public delegate void userEvent(object sender, EventArgs arg);

        public event userEvent ExamPlanSelectChange;

        public bool AutoPostBack
        {
            get
            {
                if (this.ExamPlanSelectChange != null)
                    return true;
                else
                    return false;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (IsPostBack)
            {
                if(HiddenFieldExamPlanName.Value !="") TextBoxExamPlan.Text = HiddenFieldExamPlanName.Value + "【岗位：" + HiddenFieldPostTypeName.Value + "，工种：" + HiddenFieldPostName.Value + "】";
                if (Request.Params["__EVENTTARGET"] == TextBoxExamPlan.UniqueID)//选择考试计划后回发处理
                {                   
                    this.ExamPlanSelectChange(this, e);
                }
            }
        }
   
        /// <summary>
        /// 考试ID
        /// </summary>
        public Int64? ExamPlanID
        {
            get
            {
                if (HiddenFieldExamPlanID.Value == "")
                    return null;
                else
                    return Convert.ToInt64(HiddenFieldExamPlanID.Value);
            }
        }

        /// <summary>
        /// 考试计划名称
        /// </summary>
        public string ExamPlanName
        {
            get
            {
               return TextBoxExamPlan.Text;
            }
        }

        /// <summary>
        /// 岗位ID（1：安全生产三类人员，2:建筑施工特种作业，3:造价员，4:建设职业技能岗位，5:关键岗位专业技术管理人员，4000：新版建设职业技能岗位）
        /// </summary>
        public int? PostTypeID
        {
            get
            {
                if (HiddenFieldPostTypeID.Value == "")
                    return null;
                else
                    return Convert.ToInt32(HiddenFieldPostTypeID.Value);
            }
        }

        /// <summary>
        /// 工种ID（147：企业主要负责人，148:项目负责人，6:土建类专职安全生产管理人员，1123:机械类专职安全生产管理人员，1125:综合类专职安全生产管理人员）
        /// </summary>
        public int? PostID
        {
            get
            {
                if (HiddenFieldPostID.Value == "")
                    return null;
                else
                    return Convert.ToInt32(HiddenFieldPostID.Value);
            }
        }

        /// <summary>
        /// 岗位名称
        /// </summary>
        public string PostTypeName
        {
            get
            {
                return HiddenFieldPostTypeName.Value;
            }
        }

        /// <summary>
        /// 工种名称
        /// </summary>
        public string PostName
        {
            get
            {
                return HiddenFieldPostName.Value;
            }
        }

        /// <summary>
        /// 网站根节点url
        /// </summary>
        public static string RootUrl
        {
            get
            {
                HttpContext context = HttpContext.Current;
                string executionPath = context.Request.ApplicationPath;
                return string.Format("{0}://{1}{2}", context.Request.Url.Scheme,
                context.Request.Url.Authority,
                executionPath.Length == 1 ? string.Empty : executionPath);
            }
        }

        /// <summary>
        /// 为控件赋值
        /// </summary>
        /// <param name="ob">考试计划OB</param>
        public void SetValue(ExamPlanOB ob)
        {
            TextBoxExamPlan.Text =string.Format("{0}【岗位：{1}，工种：{2}】",ob.ExamPlanName, ob.PostTypeName ,ob.PostName);
            HiddenFieldExamPlanName.Value = ob.ExamPlanName;
            HiddenFieldExamPlanID.Value = ob.ExamPlanID.ToString();

            HiddenFieldPostTypeID.Value = ob.PostTypeID.ToString();
            HiddenFieldPostID.Value = ob.PostID.ToString();
            HiddenFieldPostTypeName.Value = ob.PostTypeName;
            HiddenFieldPostName.Value = ob.PostName;
        }

    }
}