<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CheckAll.ascx.cs" Inherits="ZYRYJG.CheckAll" %>
<asp:HiddenField ID="HiddenFieldSelectAll" runat="server" />
<asp:HiddenField ID="HiddenFieldSelectAllRefreshCount" runat="server" />
<asp:CheckBox ID="CheckBoxAll" runat="server" onclick="checkBoxAllClick(this.id);checkAllTip(this.id);" Style="cursor: pointer;" ToolTip="全选(所有页数据)" />

<script type="text/javascript">
        var CheckBoxAllClientID = '<%=CheckBoxAll.ClientID %>';
</script>