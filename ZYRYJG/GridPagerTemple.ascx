<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="GridPagerTemple.ascx.cs" Inherits="ZYRYJG.GridPagerTemple" %>
<table border="0" cellpadding="5" height="18px" width="100%">
    <tr>
        <td style="border-style: none; padding-left: 8px;" align="left" nowrap="nowrap">第&nbsp;<%# (int)DataBinder.Eval(Container, "Paging.CurrentPageIndex") + 1 %>&nbsp;页&nbsp;&nbsp;&nbsp;共&nbsp;<%# DataBinder.Eval(Container, "Paging.PageCount")%>&nbsp;页&nbsp;&nbsp;&nbsp;<%# DataBinder.Eval(Container, "Paging.PageSize")%>&nbsp;条/页
            &nbsp;&nbsp;&nbsp;共&nbsp;
            <%# DataBinder.Eval(Container, "Paging.DataSourceCount")%>&nbsp;条记录
        </td>
        <td style="border-style: none; width: 70%; padding-right: 8px" align="right">
            <asp:LinkButton ID="LinkButtonFirst" CommandName="Page" CausesValidation="false"
                CommandArgument="First" runat="server">首页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonPrev" CommandName="Page" CausesValidation="false" CommandArgument="Prev"
                runat="server">上一页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonNext" CommandName="Page" CausesValidation="false" CommandArgument="Next"
                runat="server">下一页</asp:LinkButton>
            <asp:LinkButton ID="LinkButtonLast" CommandName="Page" CausesValidation="false" CommandArgument="Last"
                runat="server">尾页</asp:LinkButton>
            &nbsp;&nbsp;<asp:TextBox ID="tbPageNumber" runat="server" Columns="3" Width="30px"
                Text='<%# (int)DataBinder.Eval(Container, "OwnerTableView.CurrentPageIndex") + 1 %>' />
            <asp:LinkButton ID="LinkButtonGo" runat="server" CommandName="Page" OnClick="LinkButtonGo_Click">GO</asp:LinkButton>
            <asp:RangeValidator runat="Server" ID="RangeValidator1" ControlToValidate="tbPageNumber"
                EnableClientScript="true" MinimumValue="1" Type="Integer" MaximumValue='<%# DataBinder.Eval(Container, "Paging.PageCount") %>'
                ErrorMessage='<%# "页码必须在 1 - " + DataBinder.Eval(Container, "Paging.PageCount") +" 之间" %> '
                Display="Dynamic"></asp:RangeValidator>
        </td>
    </tr>
</table>
<script type="text/javascript">
    //刷新GridView当前页数据
    function refreshGrid() {
        var LinkButtonGo = document.getElementById('<%= LinkButtonGo.ClientID %>');
        if (LinkButtonGo == null || (LinkButtonGo != null && LinkButtonGo.style.display == "none")) {
            location.href = location.href;
        }
        else {
            if (LinkButtonGo != null) LinkButtonGo.click();

        }
    }
    //刷新GridView当前页数据
    function refreshGridToFirtPage() {
        var LinkButtonGo = document.getElementById('<%= LinkButtonGo.ClientID %>');
        var tbPageNumber = document.getElementById('<%= tbPageNumber.ClientID %>');
        if (LinkButtonGo == null || (LinkButtonGo != null && LinkButtonGo.style.display == "none")) {
            location.href = location.href;
        }
        else {
            if (tbPageNumber != null) tbPageNumber.value = "1";
            if (LinkButtonGo != null) LinkButtonGo.click();
        }
    }
</script>