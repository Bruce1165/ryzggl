using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZYRYJG.SystemManage
{
    public partial class CashManage : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void ButtonClearChecked_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (ListItem li in CheckBoxListCash.Items)
            {
                if (li.Selected == true) 
                {
                    if (Cache[li.Value] != null)
                    {
                        Cache.Remove(li.Value);
                        RadTextBoxLog.Text += string.Format("成功清除缓存“{0}”\r\n",li.Text) ;
                    }
                }
            }
        }

        protected void ButtonClearAll_Click(object sender, EventArgs e)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            foreach (ListItem li in CheckBoxListCash.Items)
            {
                if (Cache[li.Value] != null)
                {
                    Cache.Remove(li.Value);
                    RadTextBoxLog.Text += string.Format("成功清除缓存“{0}”\r\n", li.Text);
                }
            }         
        }
    }
}