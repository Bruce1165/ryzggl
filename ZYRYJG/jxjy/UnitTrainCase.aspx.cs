using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataAccess;
using System.Data;

namespace ZYRYJG.jxjy
{
    public partial class UnitTrainCase : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                BindTj();
            }
        }

        protected void BindTj()
        {
            try
            {
                DataTable dt = CommonDAL.GetDataTableDB("DBRYPX"
                    , @"select 2023 as 'TrainYear',[PostTypeName],period as 'signupCount',period * (packageid -1) / packageid  as finishCount  from dbo.Package");


                RadGridStudyPlan.DataSource = dt;
                RadGridStudyPlan.DataBind();
            }
            catch (Exception ex)
            {
            }
        }

        protected void ButtonSearch_Click(object sender, EventArgs e)
        {

        }
    }
}