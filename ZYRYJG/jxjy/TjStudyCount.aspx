<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TjStudyCount.aspx.cs" Inherits="ZYRYJG.jxjy.TjStudyCount" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc1" %>
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
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGridStudyPlan">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridStudyPlan" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadComboBoxPackageYear">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridStudyPlan" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7">
        </telerik:RadAjaxLoadingPanel>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;在线公益教育培训 &gt;&gt;<strong>培训人次统计</strong>
                </div>
            </div>
            <div class="content">
                <div class="table_cx">
                    <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    统计结果列表
                </div>
                <div style="width: 99%; margin: 8px auto;">
                    <telerik:RadGrid ID="RadGridStudyPlan" runat="server" AutoGenerateColumns="False" AllowPaging="false" OnExcelExportCellFormatting="RadGridStudyPlan_ExcelExportCellFormatting"
                        EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Skin="Blue" Width="100%">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录">
                            <Columns>
                                 <telerik:GridTemplateColumn HeaderText="" UniqueName="up">
                                    <ItemTemplate>
                                        <%# Eval("DTime").ToString().Length == 4 ? "" : string.Format("<a href=\"TjStudyCount.aspx?o={0}\"><img alt=\"up\" src=\"../Images/upload.png\" style=\"border:none;\" /></a>", up(Eval("DTime")))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn> 
                                <telerik:GridTemplateColumn HeaderText="学习时间" UniqueName="DTime">
                                    <ItemTemplate>
                                        <%# FormatDtime(Eval("DTime").ToString())%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>     
                                 <telerik:GridTemplateColumn HeaderText="点击量" UniqueName="ManCount">
                                    <ItemTemplate>
                                        <%# Eval("DTime").ToString().Length > 7 ? Eval("clickCount") : string.Format("<a href=\"TjStudyCount.aspx?o={0}\">{1}</a>", Eval("DTime"), Eval("clickCount"))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>                         
                                 <telerik:GridTemplateColumn HeaderText="学习人数(人)" UniqueName="ManCount">
                                    <ItemTemplate>
                                        <%# Eval("DTime").ToString().Length > 7 ? Eval("ManCount") : string.Format("<a href=\"TjStudyCount.aspx?o={0}\">{1}</a>", Eval("DTime"), Eval("ManCount"))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="听课总时长" UniqueName="FinishPeriod">
                                    <ItemTemplate>
                                        <%# Convert.ToInt64(Eval("FinishPeriod")) / 3600 == 0 ? "" : string.Format("{0}小时", Convert.ToString(Convert.ToInt64(Eval("FinishPeriod")) / 3600))%><%# string.Format("{0}分", Convert.ToString(Convert.ToInt64(Eval("FinishPeriod")) / 60 % 60))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                 <telerik:GridTemplateColumn HeaderText="测试通过总时长" UniqueName="TestPeriod">
                                    <ItemTemplate>
                                        <%# Convert.ToInt64(Eval("TestPeriod")) / 3600 == 0 ? "" : string.Format("{0}小时", Convert.ToString(Convert.ToInt64(Eval("TestPeriod")) / 3600))%><%# string.Format("{0}分", Convert.ToString(Convert.ToInt64(Eval("TestPeriod")) / 60 % 60))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                 <telerik:GridTemplateColumn HeaderText="人均听课时长" UniqueName="FinishPeriod">
                                    <ItemTemplate>
                                        <%# Convert.ToInt64(Eval("ManCount"))==0?"":Convert.ToInt64(Eval("FinishPeriod")) / Convert.ToInt64(Eval("ManCount")) / 3600 == 0 ? "" : string.Format("{0}小时", Convert.ToString(Convert.ToInt64(Eval("FinishPeriod")) / Convert.ToInt64(Eval("ManCount")) / 3600))%><%# Convert.ToInt64(Eval("ManCount"))==0?"0分":string.Format("{0}分", Convert.ToString(Convert.ToInt64(Eval("FinishPeriod")) / Convert.ToInt64(Eval("ManCount")) / 60 % 60))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                 <telerik:GridTemplateColumn HeaderText="人均测试通过时长" UniqueName="TestPeriod">
                                    <ItemTemplate>
                                        <%# Convert.ToInt64(Eval("ManCount"))==0?"":Convert.ToInt64(Eval("TestPeriod")) / Convert.ToInt64(Eval("ManCount")) / 3600 == 0 ? "" : string.Format("{0}小时", Convert.ToString(Convert.ToInt64(Eval("TestPeriod")) / Convert.ToInt64(Eval("ManCount")) / 3600))%><%# Convert.ToInt64(Eval("ManCount"))==0?"0分":string.Format("{0}分", Convert.ToString(Convert.ToInt64(Eval("TestPeriod")) / Convert.ToInt64(Eval("ManCount")) / 60 % 60))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="true" />                             
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </div>
        <div id="winpop">
        </div>
        <uc1:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
