<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SelectSource.ascx.cs" Inherits="ZYRYJG.SelectSource" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadCodeBlock ID="cb1" runat="server">

    <script type="text/javascript">
        //<![CDATA[
        function openWin() {
            var oWnd = radopen("<%=RootUrl %>/jxjy/SourceSearch.aspx", "RadWindow1");
            oWnd.maximize();

        }

        function OnClientClose(oWnd, args) {
            //get the transferred arguments
            var arg = args.get_argument();
            if (arg) {
                var SourceID = arg.SourceID;
                var SourceName = arg.SourceName;
           
                document.getElementById("<%=TextBoxSource.ClientID %>").value = SourceName;
                document.getElementById("<%=HiddenFieldSourceID.ClientID %>").value = SourceID;
                if ("<%=AutoPostBack %>" == "True") {
                    __dpb("TextBoxSource_TextChanged", "");
                }
            }
        }
        function get_SourceID() {
            return document.getElementById("<%=HiddenFieldSourceID.ClientID %>").value;
        }


        function __dpb(eventTarget, eventArgument) {
            var ajaxManager = $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>");
            ajaxManager.ajaxRequestWithTarget('<%= TextBoxSource.UniqueID %>', eventArgument);
        }

        function clearSelect() {
            document.getElementById("<%=TextBoxSource.ClientID %>").value = "";
            document.getElementById("<%=HiddenFieldSourceID.ClientID %>").value = "";
        }

        //]]>
    </script>

</telerik:RadCodeBlock>

<asp:TextBox ID="TextBoxSource" runat="server" CssClass="texbox" Width="90%" Skin="Default"
    ReadOnly="true" onclick="openWin();return false;" Style="cursor: pointer;">
</asp:TextBox>
<img id="ImgDel" alt="清除" src="~/Images/Cancel.gif" runat="server" onclick="javascript:clearSelect();"
    style="cursor: pointer; border: none; vertical-align: middle; margin-bottom: 1px" title="清空" />
<asp:HiddenField ID="HiddenFieldSourceID" runat="server" Value="" />
