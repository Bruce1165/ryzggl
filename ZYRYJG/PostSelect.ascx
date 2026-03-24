<%@ Control Language="C#" AutoEventWireup="True" CodeBehind="PostSelect.ascx.cs"
    Inherits="ZYRYJG.PostSelect" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    .multipleRowsColumns .rcbItem, .multipleRowsColumns .rcbHovered {
    float: left;
    margin: 0 1px;
    min-height: 13px;
    overflow: hidden;
    padding: 2px 19px 2px 6px;
    width: 260px!important;
}
    .RadComboBox_Default, .RadComboBox_Default .rcbInput, .RadComboBoxDropDown_Default {
    font-size: 14px!important;
}
</style>
<div>
    <telerik:RadComboBox ID="RadComboBoxPostTypeID" runat="server" DataTextField="PostName"
        AppendDataBoundItems="true" DataValueField="PostID" NoWrap="true" OnInit="RadComboBoxPostTypeID_Init"
        OnSelectedIndexChanged="RadComboBoxPostTypeID_SelectedIndexChanged" AutoPostBack="true"
        EmptyMessage="请选择一个岗位" LoadingMessage="加载中..." Skin="Default" Width="200px"
        CausesValidation="False">
    </telerik:RadComboBox>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="!"
        ControlToValidate="RadComboBoxPostTypeID" CssClass="validator"></asp:RequiredFieldValidator>
    <telerik:RadComboBox ID="RadComboBoxPostID" runat="server" DataTextField="PostName"
        DataValueField="PostID" EmptyMessage="请选择一个工种" LoadingMessage="加载中..." NoWrap="true"
        AppendDataBoundItems="true" OnSelectedIndexChanged="RadComboBoxPostID_SelectedIndexChanged"
        AutoPostBack="true" CausesValidation="False" DataTextFormatString="♀ {0}"
        Width="280px" DropDownCssClass="multipleRowsColumns" DropDownWidth="660px" Height="300px" >
      
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

