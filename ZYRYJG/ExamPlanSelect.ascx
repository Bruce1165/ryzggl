<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ExamPlanSelect.ascx.cs"    Inherits="ZYRYJG.ExamPlanSelect" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<telerik:RadCodeBlock ID="cb1" runat="server">

    <script type="text/javascript">
        //<![CDATA[
        function openWin() {
            var oWnd = radopen("<%=RootUrl %>/EXamManage/ExamPlanSearch.aspx", "RadWindow1");
            oWnd.maximize();

        }

        function OnClientClose(oWnd, args) {
            //get the transferred arguments
            var arg = args.get_argument();
            if (arg) {
                var examPlanID = arg.ExamPlanID;
                var examPlanName = arg.ExamPlanName;
                document.getElementById("<%=TextBoxExamPlan.ClientID %>").value = examPlanName + "【岗位：" + arg.PostTypeName + "，工种：" + arg.PostName + "】";
                document.getElementById("<%=HiddenFieldExamPlanName.ClientID %>").value = examPlanName;
                document.getElementById("<%=HiddenFieldExamPlanID.ClientID %>").value = examPlanID;

                document.getElementById("<%=HiddenFieldPostTypeID.ClientID %>").value = arg.PostTypeID;
                document.getElementById("<%=HiddenFieldPostID.ClientID %>").value = arg.PostID;
                document.getElementById("<%=HiddenFieldPostTypeName.ClientID %>").value = arg.PostTypeName;
                document.getElementById("<%=HiddenFieldPostName.ClientID %>").value = arg.PostName;
                if ("<%=AutoPostBack %>" == "True") {
                    __dpb("TextBoxExamPlan_TextChanged", "");
                }
            }
        }
        function get_ExamPlanID() {
            return document.getElementById("<%=HiddenFieldExamPlanID.ClientID %>").value;
        }


        function __dpb(eventTarget, eventArgument) {
            var ajaxManager = $find("<%= RadAjaxManager.GetCurrent(Page).ClientID %>");
            ajaxManager.ajaxRequestWithTarget('<%= TextBoxExamPlan.UniqueID %>', eventArgument);
        }

        function clearSelect() {
            document.getElementById("<%=TextBoxExamPlan.ClientID %>").value = "";
            document.getElementById("<%=HiddenFieldExamPlanName.ClientID %>").value = "";
            document.getElementById("<%=HiddenFieldExamPlanID.ClientID %>").value = "";
            document.getElementById("<%=HiddenFieldPostTypeID.ClientID %>").value = "";
            document.getElementById("<%=HiddenFieldPostID.ClientID %>").value = "";
            document.getElementById("<%=HiddenFieldPostTypeName.ClientID %>").value = "";
            document.getElementById("<%=HiddenFieldPostName.ClientID %>").value = "";
        }

        //]]>
    </script>

</telerik:RadCodeBlock>
    <asp:TextBox ID="TextBoxExamPlan" runat="server" CssClass="texbox" Width="90%" Skin="Default"
    ReadOnly="true" onclick="openWin();return false;" Style="cursor: pointer;">
</asp:TextBox>
<img id="ImgDel" alt="清除" src="~/Images/Cancel.gif" runat="server" onclick="javascript:clearSelect();"
    style="cursor: pointer; border: none; vertical-align: middle; margin-bottom:1px" title="清空" />
<asp:HiddenField ID="HiddenFieldExamPlanID" runat="server" />
<asp:HiddenField ID="HiddenFieldExamPlanName" runat="server" />
<asp:HiddenField ID="HiddenFieldPostTypeID" runat="server" />
<asp:HiddenField ID="HiddenFieldPostID" runat="server" />
<asp:HiddenField ID="HiddenFieldPostTypeName" runat="server" />
<asp:HiddenField ID="HiddenFieldPostName" runat="server" />
