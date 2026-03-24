<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ReportSelect.ascx.cs"
    Inherits="ZYRYJG.ReportSelect" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadCodeBlock ID="cb1" runat="server">

    <script type="text/javascript">
        //<![CDATA[
        function openWin() {
            var oWnd = radopen("<%=RootUrl %>/RenewCertifates/ReportSearch.aspx?o=<%=HiddenFieldtPostTypeID.Value %>", "RadWindow1");
            oWnd.maximize();
        }
        function OnClientClose(oWnd, args) {
            var arg = args.get_argument();
            if (arg) {
                var ReportCode = arg.ReportCode;

                document.getElementById("<%=TextBoxReportCode.ClientID %>").value = ReportCode;
                document.getElementById("<%=HiddenFieldReportCode.ClientID %>").value = ReportCode;

                if ("<%=AutoPostBack %>" == "True") {
                    __dpb("TextBoxReportCode_TextChanged", "");
                }
            }
        }


        function __dpb(eventTarget, eventArgument) {
            var ajaxManager = $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>");
            ajaxManager.ajaxRequestWithTarget('<%= TextBoxReportCode.UniqueID %>', eventArgument);
        }

        function clearSelect() {
            document.getElementById("<%=TextBoxReportCode.ClientID %>").value = "";
        }

        //]]>
    </script>

</telerik:RadCodeBlock>
<asp:TextBox ID="TextBoxReportCode" runat="server" CssClass="texbox" Width="90%" Skin="Default"
    ReadOnly="true" onclick="openWin();return false;" Style="cursor: pointer;">
</asp:TextBox>
<img id="imgDel" alt="清除" src="~/Images/Cancel.gif" runat="server" onclick="javascript:clearSelect();"
    style="cursor: pointer; border: none; vertical-align: bottom;" title="清空" />
<asp:HiddenField ID="HiddenFieldReportCode" runat="server" />
<asp:HiddenField ID="HiddenFieldtPostTypeID" runat="server" />