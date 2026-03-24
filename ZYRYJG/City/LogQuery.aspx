<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LogQuery.aspx.cs" Inherits="ZYRYJG.City.LogQuery" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />     
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
    <%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
    <style type="text/css">
        .link {
            border: none;
            color: blue;
            background-color: transparent;
            cursor: pointer;
            font-size: 12px;
        }
        .auto-style1 {
            width: 40%;
        }
        .auto-style2 {
            width: 50%;
        }
    </style>
    
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
                <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridZGGL" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
               <%-- <telerik:AjaxSetting AjaxControlID="RadGridZGGL">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridZGGL" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>--%>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        <img alt="" src="../Images/ts.gif" width="17" height="11" />当前位置 &gt;&gt;查询统计 &gt;&gt;<strong>操作日志查询</strong>
                    </div>
                </div>
                <div class="content">
                   <%-- <p class="jbxxbt">
                        资格校验
                    </p>--%>
                    <div style="width: 95%; height: 100%; margin: 10px auto; text-align: center;">
                        <table>
                            <tr>
                                <td style="text-align: left" class="auto-style1">

                                          <div class="RadPicker md">写入日期：</div>
                            <telerik:RadDatePicker ID="RadDatePickerGetDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="100px" />
                            <div class="RadPicker md">至</div>
                            <telerik:RadDatePicker ID="RadDatePickerGetDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="100px" />
                              
                                
<%--&nbsp;<telerik:RadTextBox ID="RadTextBoxZJHM" runat="server" Width="150px" Text=""></telerik:RadTextBox>--%>
                                   
                                </td>
                                <td style="text-align: left" class="auto-style2">
                                    <div class="RadPicker">写入人：</div>
                                     <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="150px" Skin="Default">
                                </telerik:RadTextBox>
                                       <asp:Button ID="ButtonQuery" runat="server" Text="查 询" CssClass="button" OnClick="ButtonQuery_Click" />

                                </td>
                                <td align="right" style="width: 20%;">
                             
                                </td>
                              
                            </tr>
                        </table>
                        <div style="text-align: right" id="spanOutput" runat="server"></div>
                        <telerik:RadGrid ID="RadGridZGGL" runat="server"
                            GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                         
                            SortingSettings-SortToolTip="单击进行排序" Width="98%" PagerStyle-AlwaysVisible="true" PageSize="20">
                           
                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <HeaderContextMenu EnableEmbeddedSkins="False">
                            </HeaderContextMenu>
                            <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>

                                <Columns>
                                    <telerik:GridBoundColumn DataField="LogTime" DataFormatString="{0:yyyy.MM.dd}" HeaderText="写入时间" UniqueName="写入时间">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="PersonName" HeaderText="写入人" UniqueName="写入人">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="OperateName" HeaderText="申请业务" UniqueName="申请业务">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="LogDetail" HeaderText="具体内容" UniqueName="具体内容">
                                    </telerik:GridBoundColumn>
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
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.OperateLogOB"
                            SelectMethod="GetList" TypeName="DataAccess.OperateLogDAL"
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
        </div>
    </form>

</body>
</html>
