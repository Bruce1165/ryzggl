<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="PostAllSelect.ascx.cs"
    Inherits="ZYRYJG.PostAllSelect" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<div>
    <telerik:RadComboBox ID="RadComboBoxPostTypeID" runat="server" DataTextField="PostName"
        AppendDataBoundItems="true" DataValueField="PostID" NoWrap="true" OnInit="RadComboBoxPostTypeID_Init"
        OnSelectedIndexChanged="RadComboBoxPostTypeID_SelectedIndexChanged" AutoPostBack="true"
        EmptyMessage="请选择一个岗位" LoadingMessage="加载中..." Skin="Default"
        CausesValidation="False">
    </telerik:RadComboBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="!"
        ControlToValidate="RadComboBoxPostTypeID" CssClass="validator"></asp:RequiredFieldValidator>
    <telerik:RadComboBox ID="RadComboBoxPostID" runat="server" DataTextField="PostName"
        DataValueField="PostID" EmptyMessage="请选择一个工种" LoadingMessage="加载中..." NoWrap="true"
        AppendDataBoundItems="true" OnSelectedIndexChanged="RadComboBoxPostID_SelectedIndexChanged"
        AutoPostBack="true" CausesValidation="False" DataTextFormatString="♀ {0}"
        Width="240px" DropDownCssClass="multipleRowsColumns" DropDownWidth="660px" Height="300px">
    </telerik:RadComboBox>
</div>
<script type="text/javascript">
    function IfSelectPost() {
        return document.getElementById("<%=RadComboBoxPostID.ClientID %>").value != "请选择";
    }
    function IfSelectPostType() {
        return document.getElementById("<%=RadComboBoxPostTypeID.ClientID %>").value != "请选择";
    }
</script>

