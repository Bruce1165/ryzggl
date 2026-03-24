<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AlreadyMatter.aspx.cs" Inherits="ZYRYJG.Unit.AlreadyMatter" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
    <%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
    <style type="text/css">
        a {
            display: block;
            color: blue;
            line-height: 34px;
        }

            a:hover {
                background-color: #3186E1;
                display: block;
                line-height: 34px;
            }

        .a0 {
            background-color: transparent;
        }

        .a1 {
            background-color: transparent;
            /*background-color: #eeffff;*/
        }

        .md {
            vertical-align: middle !important;
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
        <div class="div_out">

            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;统计分析 &gt;&gt;<strong>申报事项统计</strong>
                </div>
            </div>
            <div class="content">
                <p class="jbxxbt">
                    申报事项统计
                </p>
                <div style="width: 100%; margin: 12px 12px">
                    <div class="RadPicker md">申报日期：</div>
                    <telerik:RadDatePicker ID="RadDatePickerGetDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                        Width="150px" />
                    <div class="RadPicker md">至</div>
                    <telerik:RadDatePicker ID="RadDatePickerGetDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                        Width="150px" />
                    <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                </div>
                <telerik:RadGrid ID="RadGridData" runat="server"
                    GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                    Width="100%"  Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
                    <ClientSettings EnableRowHoverStyle="false">
                    </ClientSettings>
                    <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                        DataKeyNames="ApplyType" CellPadding="0" CellSpacing="0">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="ApplyType" DataField="ApplyType" HeaderText="申报事项">
                                <ItemStyle ForeColor="Black" Font-Underline="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="未申报" HeaderText="未申报">
                                <ItemTemplate>
                                    <%# Convert.ToInt32(Eval("未申报"))>0?
                                    string.Format("<a class='a1' href='ApplyHistory.aspx?o={0}&s=未申报&b={2}&e={3}'>{1}</a>"
                                    , ApplyTypeParam( Eval("ApplyType").ToString()) 
                                    ,Eval("未申报")
                                    ,RadDatePickerGetDateStart.SelectedDate.HasValue==true?RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd"):""
                                    ,RadDatePickerGetDateEnd.SelectedDate.HasValue==true?RadDatePickerGetDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59"):"")
                                    :Eval("未申报")  %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                             <telerik:GridTemplateColumn UniqueName="待确认" HeaderText="待确认">
                                <ItemTemplate>
                                    <%# Convert.ToInt32(Eval("待确认"))>0?
                                    string.Format("<a class='a1' href='ApplyHistory.aspx?o={0}&s=待确认&b={2}&e={3}'>{1}</a>"
                                    , ApplyTypeParam( Eval("ApplyType").ToString()) 
                                    ,Eval("待确认")
                                     ,RadDatePickerGetDateStart.SelectedDate.HasValue==true?RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd"):""
                                    ,RadDatePickerGetDateEnd.SelectedDate.HasValue==true?RadDatePickerGetDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59"):"")
                                    :Eval("待确认")  %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="已申报" HeaderText="已申报">
                                <ItemTemplate>
                                    <%# Convert.ToInt32(Eval("已申报"))>0?
                                    string.Format("<a class='a1' href='ApplyHistory.aspx?o={0}&s=已申报&b={2}&e={3}'>{1}</a>"
                                    , ApplyTypeParam( Eval("ApplyType").ToString()) 
                                    ,Eval("已申报")
                                     ,RadDatePickerGetDateStart.SelectedDate.HasValue==true?RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd"):""
                                    ,RadDatePickerGetDateEnd.SelectedDate.HasValue==true?RadDatePickerGetDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59"):"")
                                    :Eval("已申报")  %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="已受理" HeaderText="已受理">
                                <ItemTemplate>
                                    <%# Convert.ToInt32(Eval("已受理"))>0?
                                    string.Format("<a class='a1' href='ApplyHistory.aspx?o={0}&s=已受理&b={2}&e={3}'>{1}</a>"
                                    , ApplyTypeParam( Eval("ApplyType").ToString())
                                    ,Eval("已受理")
                                     ,RadDatePickerGetDateStart.SelectedDate.HasValue==true?RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd"):""
                                    ,RadDatePickerGetDateEnd.SelectedDate.HasValue==true?RadDatePickerGetDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59"):"")
                                    :Eval("已受理")  %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="已驳回" HeaderText="已驳回">
                                <ItemTemplate>
                                    <%# Convert.ToInt32(Eval("已驳回"))>0?
                                    string.Format("<a class='a1' href='ApplyHistory.aspx?o={0}&s=已驳回&b={2}&e={3}'>{1}</a>"
                                    , ApplyTypeParam( Eval("ApplyType").ToString())
                                    ,Eval("已驳回")
                                     ,RadDatePickerGetDateStart.SelectedDate.HasValue==true?RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd"):""
                                    ,RadDatePickerGetDateEnd.SelectedDate.HasValue==true?RadDatePickerGetDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59"):"")
                                    :Eval("已驳回")  %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="已办结" HeaderText="已办结">
                                <ItemTemplate>
                                    <%# Convert.ToInt32(Eval("已办结"))>0?
                                    string.Format("<a class='a1' href='ApplyHistory.aspx?o={0}&s=已办结&b={2}&e={3}'>{1}</a>"
                                    , ApplyTypeParam( Eval("ApplyType").ToString())
                                    ,Eval("已办结")
                                     ,RadDatePickerGetDateStart.SelectedDate.HasValue==true?RadDatePickerGetDateStart.SelectedDate.Value.ToString("yyyy-MM-dd"):""
                                    ,RadDatePickerGetDateEnd.SelectedDate.HasValue==true?RadDatePickerGetDateEnd.SelectedDate.Value.ToString("yyyy-MM-dd 23:59:59"):"")
                                    :Eval("已办结")  %>
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Height="30" Font-Size="14px" />
                        <ItemStyle HorizontalAlign="Center" Height="40" Font-Size="14px" />
                        <AlternatingItemStyle HorizontalAlign="Center" Height="40" Font-Size="14px" />
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
        </div>
    </form>
</body>
</html>
