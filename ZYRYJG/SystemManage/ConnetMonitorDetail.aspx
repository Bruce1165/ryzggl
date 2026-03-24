<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ConnetMonitorDetail.aspx.cs" Inherits="ZYRYJG.SystemManage.ConnetMonitorDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
        <script type="text/javascript">           
        </script>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true" >
            <AjaxSettings>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;系统管理 &gt;&gt;<strong>数据库连接数统计</strong>
                </div>
            </div>
            <div class="content">
                
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    统计记录列表<asp:Button ID="ButtonOutput" runat="server" Text="导出列表" CssClass="bt_large" OnClick="ButtonOutput_Click" style="margin-left:100px"  />
                </div>
                <div style="width: 98%; margin: 0 auto; padding-bottom: 10px; text-align:center" runat="server" id="DivMain">
                    <telerik:RadGrid ID="RadGridConnet" runat="server" GridLines="None" AllowPaging="false"
                        AllowCustomPaging="true"  AllowSorting="True" AutoGenerateColumns="False"
                        SortingSettings-SortToolTip="单击进行排序" Width="800px" Skin="Blue" EnableAjaxSkinRendering="False" OnExcelExportCellFormatting="RadGridConnet_ExcelExportCellFormatting"
                        EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <MasterTableView DataKeyNames="dtime" NoMasterRecordsText="　没有可显示的记录">
                            <Columns>                                
                                <telerik:GridBoundColumn HeaderText="统计时间" DataField="dtime" UniqueName="dtime"
                                    HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd日HH时}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="峰值" DataField="MaxCount" UniqueName="MaxCount">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="平均值" DataField="AvgCount" UniqueName="AvgCount">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>                                
                                
                            </Columns>                          
                        </MasterTableView>
                    </telerik:RadGrid>
                   
                </div>
                
            </div>
        </div>
    </form>
</body>
</html>
