using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace Utility
{
    public class TemplateCustomGridColumn:System.Web.UI.ITemplate
    {
        protected string _format = "";
        List<string> _bindColumns = null;

        public TemplateCustomGridColumn(string format,List<string> bindColumns)
        {
            _format = format;
            _bindColumns = bindColumns;          
        }

        public void InstantiateIn(System.Web.UI.Control container)
        {
            Literal li = new Literal();
            container.Controls.Add(li);
             li.DataBinding += new EventHandler(this.LiteralBinding);
        }

        private void LiteralBinding(Object sender, EventArgs e)
        {
            string[] parms =  new string[_bindColumns.Count];
            Literal myLiteral = (Literal)sender;
            GridItem row = (GridItem)myLiteral.NamingContainer;
            for (int i = 0; i < _bindColumns.Count; i++)
            {
                if (_bindColumns[i].Substring(0, 1) == "#")//绑定列
                {
                    parms[i] = System.Web.UI.DataBinder.Eval(row.DataItem, _bindColumns[i].Remove(0,1)).ToString();
                }
                else//固定文本
                {
                    parms[i] = _bindColumns[i];
                }
            }
            myLiteral.Text = string.Format(_format, parms);              
        }
    }
}
