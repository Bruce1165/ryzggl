<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckTaskMgr.aspx.cs" Inherits="ZYRYJG.CheckMgr.CheckTaskMgr" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true">
            <AjaxSettings>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;综合监管 &gt;&gt;<strong>监管任务管理</strong>
                </div>
            </div>
            <div class="content">
                <table class="cx" width="98%" border="0" align="center" cellspacing="5">
                    <tr id="TrPerson" runat="server">
                        <td style="text-align: right">时间筛选：
                        </td>
                        <td width="100px" style="text-align: left" nowrap="nowrap">
                            <telerik:RadComboBox ID="RadComboBoxItem" runat="server" Width="100%">
                                <Items>
                                    <telerik:RadComboBoxItem Text="创建时间" Value="CreateTime" />
                                    <telerik:RadComboBoxItem Text="发布时间" Value="PublishiTime" />
                                    <telerik:RadComboBoxItem Text="上报截止时间" Value="LastReportTime" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>

                        <td width="400px" style="text-align: right">

                            <telerik:RadDatePicker ID="RadDatePickerGetDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="180px" />
                            <div class="RadPicker md">至</div>
                            <telerik:RadDatePicker ID="RadDatePickerGetDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="180px" />
                        </td>
                        <td width="100px" style="text-align: right">监管类型：
                        </td>
                        <td width="150px" style="text-align: left">
                            <telerik:RadComboBox ID="RadComboBoxCheckType" runat="server" Width="150">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" />
                                    <telerik:RadComboBoxItem Text="打击挂证专项治理" Value="打击挂证专项治理" />
                                    <telerik:RadComboBoxItem Text="常态化监管" Value="常态化监管" />
                                    <telerik:RadComboBoxItem Text="双随机检查" Value="双随机检查" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="left">
                            <asp:Button ID="ButtonQuery" runat="server" Text="查 询" CssClass="bt_large" OnClick="ButtonQuery_Click" />
                            &nbsp; 
                            <input id="ButtonNew" runat="server" type="button" value="新建监管任务" class="bt_large" onclick='javascript: SetIfrmSrc("CheckTaskEdit.aspx"); ' />

                        </td>
                    </tr>
                </table>
                <div style="width: 99%; margin: 8px auto;">
                    <telerik:RadGrid ID="RadGridTask" runat="server"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15" OnItemCommand="RadGridTask_ItemCommand"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Center"
                            DataKeyNames="PatchCode">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CreateTime" DataField="CreateTime" HeaderText="创建时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CheckType" DataField="CheckType" HeaderText="监管类型">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="DataCount" DataField="DataCount" HeaderText="记录数量">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="LastReportTime" DataField="LastReportTime" HeaderText="上报截止时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PublishiTime" DataField="PublishiTime" HeaderText="发布时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="Status" HeaderText="状态">
                                    <ItemTemplate>
                                        <%# Eval("PublishiTime")== DBNull.Value?"未发布":"已发布" %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn UniqueName="Detail" HeaderText="详细">
                                    <ItemTemplate>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("CheckTaskEdit.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("PatchCode").ToString())%>");'>详细</span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridTemplateColumn UniqueName="Delete">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButtonDelete" CommandName="ButtonDelete" OnClientClick="return confirm('您确定要删除么?')"
                                            runat="server">删除</asp:LinkButton>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                            <PagerTemplate>
                                <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                        <FilterMenu EnableEmbeddedSkins="False">
                        </FilterMenu>
                        <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    </telerik:RadGrid>
                </div>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.CheckTaskMDL"
                    SelectMethod="GetList" TypeName="DataAccess.CheckTaskDAL"
                    SelectCountMethod="SelectCount" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>


            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
        <div id="winpop"></div>
    </form>
</body>
</html>
