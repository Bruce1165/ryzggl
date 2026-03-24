<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="WuYeCertLockSearch.aspx.cs" Inherits="ZYRYJG.StAnManage.WuYeCertLockSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 统计分析 &gt;&gt;
                综合查询 &gt;&gt; <strong>人员证书锁定记录查询（来自基础数据库）</strong>
            </div>
        </div>
        <table class="bar_cx">
            <tr>
                <td width="11%" align="right" nowrap="nowrap">企业名称：
                </td>
                <td width="39%" align="left">
                    <telerik:RadTextBox ID="RadTextBoxQYMC" runat="server" Width="95%" Skin="Default"
                        >
                    </telerik:RadTextBox>
                </td>
                <td width="11%" align="right" nowrap="nowrap">组织机构代码：
                </td>
                <td width="39%" align="left">
                    <telerik:RadTextBox ID="RadTextBoxZZJGDM" runat="server" Width="95%" Skin="Default"
                        >
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td width="11%" align="right" nowrap="nowrap">证书编号：
                </td>
                <td width="39%" align="left">
                    <telerik:RadTextBox ID="RadTextBoxZSBH" runat="server" Width="95%" Skin="Default"
                        >
                    </telerik:RadTextBox>
                </td>
                <td width="11%" align="right" nowrap="nowrap">岗位工种：
                </td>
                <td width="39%" align="left">
                    <telerik:RadTextBox ID="RadTextBoxGWGZ" runat="server" Width="95%" Skin="Default"
                        >
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td width="11%" align="right" nowrap="nowrap">锁定业务事项：
                </td>
                <td width="39%" align="left">
                    <telerik:RadTextBox ID="RadTextBoxSDYWSX" runat="server" Width="95%" Skin="Default"
                        >
                    </telerik:RadTextBox>
                </td>
                <td width="11%" align="right" nowrap="nowrap">锁定业务说明：
                </td>
                <td width="39%" align="left">
                    <telerik:RadTextBox ID="RadTextBoxSDYWSM" runat="server" Width="95%" Skin="Default"
                        >
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td align="right" nowrap="nowrap">锁定状态：
                </td>
                <td align="left" colspan="3">
                    <asp:RadioButtonList ID="RadioButtonListSDZT" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="false" Style="float: left;">
                        <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                        <asp:ListItem Value="加锁">加锁</asp:ListItem>
                        <asp:ListItem Value="解锁">解锁</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td align="center" colspan="4">
                    <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                </td>
            </tr>
        </table>

        <div class="table_cx">
            <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
            证书锁定记录列表
        </div>
        <div style="width: 98%; margin: 0 auto;">
            <telerik:RadGrid ID="RadGrid1" AutoGenerateColumns="False" runat="server" AllowAutomaticDeletes="True"
                AllowCustomPaging="true" AllowPaging="True" PageSize="10" AllowSorting="False"
                SortingSettings-SortToolTip="单击进行排序" Skin="Blue" EnableAjaxSkinRendering="false"
                EnableEmbeddedSkins="false" Width="100%" GridLines="None" OnPageIndexChanged="RadGrid1_PageIndexChanged">
                <ClientSettings EnableRowHoverStyle="true">
                </ClientSettings>
                <MasterTableView AllowMultiColumnSorting="True" NoMasterRecordsText="　没有可显示的记录">
                    <Columns>
                        <telerik:GridTemplateColumn UniqueName="GWGZ" HeaderText="岗位工种" SortExpression="GWGZ">
                            <ItemTemplate>
                                <span title='岗位类别：<%# Eval("GWLB")%>；岗位工种：<%# Eval("GWGZ")%>'>
                                    <%# Eval("GWGZ")%>
                                </span>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="ZSBH" HeaderText="证书编号" SortExpression="ZSBH">
                            <ItemTemplate>
                                <span title='人员证件号码：<%# Eval("RYZJHM")%>'>
                                    <%# Eval("ZSBH")%>
                                </span>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridTemplateColumn UniqueName="QYMC" HeaderText="企业名称" SortExpression="QYMC">
                            <ItemTemplate>
                                <span title='组织机构代码：<%# Eval("ZZJGDM")%>'>
                                    <%# Eval("QYMC")%>
                                </span>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn UniqueName="SDZT" DataField="SDZT" HeaderText="锁定状态">
                            <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="SDSJ" DataField="SDSJ" HeaderText="业务时间" HtmlEncode="false"
                            DataFormatString="{0:yyyy.MM.dd}">
                            <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="CJSJ" DataField="CJSJ" HeaderText="同步时间" HtmlEncode="false"
                            DataFormatString="{0:yyyy.MM.dd}">
                            <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridBoundColumn UniqueName="SDYWSX" DataField="SDYWSX" HeaderText="锁定业务事项">
                            <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridBoundColumn>
                        <telerik:GridTemplateColumn UniqueName="SDYWSM" HeaderText="锁定业务说明" SortExpression="SDYWSM">
                            <ItemTemplate>
                                <span title='备注：<%# Eval("BZ")%>'>
                                    <%# Eval("SDYWSM")%>
                                </span>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Left" />
                        </telerik:GridTemplateColumn>
                    </Columns>
                    <HeaderStyle Font-Bold="true" />
                    <PagerTemplate>
                        <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                    </PagerTemplate>
                </MasterTableView>
                <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
            </telerik:RadGrid>
            <br />
            <%--  <asp:Button ID="ButtonOutput" runat="server" Text="导 出" CssClass="button" OnClick="ButtonOutput_Click" />--%>
        </div>

        <br />
    </div>
</asp:Content>
