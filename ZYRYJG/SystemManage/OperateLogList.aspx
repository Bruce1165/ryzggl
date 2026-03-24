<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperateLogList.aspx.cs" Inherits="ZYRYJG.SystemManage.OperateLogList" %>

<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
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
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        </telerik:RadCodeBlock>
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ButtonPrint") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
            ReloadOnShow="true" runat="server" Skin="Sunset" EnableShadow="true">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
            <ClientEvents OnRequestStart="onRequestStart" />
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DivMain" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ButtonPrint">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DivMain" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
            Skin="Sunset" />
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt; 系统管理 &gt;&gt; 操作日志 &gt;&gt;
                <strong>操作日志</strong>
                </div>
            </div>

            <div class="content">
                <table class="cx" width="98%" border="0" align="center" cellspacing="5">
                    <tr>
                        <td align="right" nowrap="nowrap">操作者：
                        </td>
                        <td align="left" width="39%">
                            <telerik:RadTextBox ID="RadTextBoxPersonName" runat="server" Width="97%" Skin="Default"
                                 MaxLength="50">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap">操作时间：
                        </td>
                        <td align="left" width="39%">
                            <telerik:RadDatePicker ID="datePickerFrom" runat="server" Width="45%" DateInput-DateFormat="yyyy-MM-dd" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                DateInput-DisplayDateFormat="yyyy-MM-dd">
                            </telerik:RadDatePicker>
                            <div class="RadPicker">至</div>
                            <telerik:RadDatePicker ID="datePickerEnd" runat="server" Width="45%" DateInput-DateFormat="yyyy-MM-dd" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                DateInput-DisplayDateFormat="yyyy-MM-dd">
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">操作名称：
                        </td>
                        <td align="left" width="39%">
                            <telerik:RadTextBox ID="RadTextBoxOperateName" runat="server" Width="97%" Skin="Default"
                                 MaxLength="50">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" nowrap="nowrap">内容关键字：
                        </td>
                        <td align="left" width="39%">
                            <telerik:RadTextBox ID="RadTextBoxLogDetail" runat="server" Width="97%" Skin="Default"
                                 MaxLength="200">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </table>
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    日志列表
                </div>
                <div style="width: 98%; margin: 0 auto; padding-bottom: 10px;" runat="server" id="DivMain">
                    <telerik:RadGrid ID="RadGridOperateLog" runat="server" GridLines="None" AllowPaging="True"
                        AllowCustomPaging="true" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False"
                        EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <MasterTableView DataKeyNames="LogID" NoMasterRecordsText="　没有可显示的记录">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="行号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="36" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="操作时间" DataField="LogTime" UniqueName="LogTime"
                                    HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd HH:mm}">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="操作者" DataField="PersonName" UniqueName="PersonName">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="操作名称" DataField="OperateName" UniqueName="OperateName">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="LogDetail" HeaderText="具体内容" UniqueName="具体内容">
                                </telerik:GridBoundColumn>
                                <%--  <telerik:GridTemplateColumn UniqueName="Detail" HeaderText="">
                                            <ItemTemplate>
                                                <span class="link_edit" onclick='javascript:SetIfrmSrc("OperateLogView.aspx?o=<%# Eval("LogID") %>");'>详细</span>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                        </telerik:GridTemplateColumn>--%>
                            </Columns>
                            <PagerTemplate>
                                <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.OperateLogDAL"
                        SelectMethod="GetList" EnablePaging="true" SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows"
                        StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </div>
        </div>
        <uc4:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
