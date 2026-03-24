<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZSSDList.aspx.cs" Inherits="ZYRYJG.County.ZSSDList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
      <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQY" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridQY">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQY" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;信息查看 &gt;&gt;<strong>在施锁定记录</strong>
                </div>
            </div>
            <div class="content">
                <table class="cx" width="100%" border="0" align="center" cellspacing="1">
                    <tr id="TrPerson" runat="server">
                        <td width="12%" align="right" nowrap="nowrap">
                            <telerik:RadComboBox ID="RadComboBoxIten" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="姓名" Value="XM" />
                                    <telerik:RadComboBoxItem Text="证件号码" Value="ZJHM" />
                                    <telerik:RadComboBoxItem Text="注册号" Value="ZCH" />
                                    <telerik:RadComboBoxItem Text="合同编号" Value="HTBH" />
                                    <telerik:RadComboBoxItem Text="项目名称" Value="XMMC" />
                                    <telerik:RadComboBoxItem Text="中标企业" Value="ZBQY" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="left" width="30%">
                            <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>

                        </td>
                        <td width="80px" align="right" nowrap="nowrap">锁定状态：
                        </td>
                        <td align="left" width="80px">
                            <telerik:RadComboBox ID="RadComboBoxSDZT" runat="server" Width="80px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" />
                                    <telerik:RadComboBoxItem Text="锁定" Value="锁定" Selected="true" />
                                    <telerik:RadComboBoxItem Text="解锁" Value="解锁" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="left" style="padding-left: 40px">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </table>
                <div style="width: 98%; margin: 8px auto;">
                    <telerik:RadGrid ID="RadGridQY" runat="server"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15" Skin="Blue" EnableAjaxSkinRendering="False"
                        EnableEmbeddedSkins="False"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                            DataKeyNames="ID">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="ID" DataField="ID" HeaderText="ID" Visible="False">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="XM" DataField="XM" HeaderText="姓名">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="ZJHM" HeaderText="证件号码">
                                    <ItemTemplate>
                                        <%# Eval("ZJHM") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="ZCH" DataField="ZCH" HeaderText="注册号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="HTBH" DataField="HTBH" HeaderText="合同编号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="XMMC" HeaderText="项目名称">
                                    <ItemTemplate>
                                        <%# Eval("XMMC").ToString().Trim().Length > 15 ? Eval("XMMC").ToString().Substring(0, 15).Trim() + "..." : Eval("XMMC").ToString().Trim() %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridBoundColumn UniqueName="ZBQY" DataField="ZBQY" HeaderText="中标企业">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="SDZT" DataField="SDZT" HeaderText="锁定状态">
                                    <ItemTemplate>
                                        <%# Eval("SDZT").ToString().Trim() == "1"? "锁定" : "解锁" %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridDateTimeColumn DataField="SDSJ" HeaderText="锁定时间" UniqueName="SDSJ" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridDateTimeColumn>

                                <telerik:GridTemplateColumn UniqueName="Edit">
                                    <ItemTemplate>
                                        <span class="link_edit" onclick=' javascript:SetIfrmSrc("../County/ZSSDDetail.aspx?o=<%# Eval("ID")%>"); '>详细</span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>
                            </Columns>

                            <PagerStyle AlwaysVisible="True"></PagerStyle>

                            <HeaderStyle Font-Bold="True" />

                            <CommandItemStyle HorizontalAlign="Left"></CommandItemStyle>
                            <PagerTemplate>
                                <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>

                        <PagerStyle AlwaysVisible="True"></PagerStyle>

                        <FilterMenu EnableEmbeddedSkins="False">
                        </FilterMenu>
                        <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    </telerik:RadGrid>

                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.jcsjk_RY_JZS_ZSSDMDL"
                        SelectMethod="GetList" TypeName="DataAccess.jcsjk_RY_JZS_ZSSDDAL"
                        SelectCountMethod="SelectCount" EnablePaging="true"
                        MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
