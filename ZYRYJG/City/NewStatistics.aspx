<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewStatistics.aspx.cs" Inherits="ZYRYJG.City.NewStatistics" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
    <%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
    <script type="text/javascript" src="../Scripts/echarts-all.js"></script>
    <style type="text/css">
        .floatL {
            float: left;
            padding: 4px 0 4px 4px;
            line-height: 26px;
        }

        .clearL {
            clear: left;
            padding-left: 12px;
        }

        .clearR {
            clear: right;
        }

        .hide {
            display: none;
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
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;统计分析 &gt;&gt;<strong>新设立办理量统计</strong>
                </div>
            </div>
            <div class="content">
                
                <div class="floatL" style="width: 98%; text-align: left; padding-left: 8px">
                  
                    <table border="0" align="left" cellspacing="7">
                        <tr style="top: 10px">
                            <td  >
                                <%--<telerik:RadComboBox ID="RadComboBoxIten" runat="server" Width="99px" Height="45px" >
                                <Items>
                                    <telerik:RadComboBoxItem Text="受理时间：" Value="受理时间" Selected="true" />
                                    <telerik:RadComboBoxItem Text="决定时间：" Value="决定时间" />
                                </Items>
                                </telerik:RadComboBox>--%>  
                                办理时间：
                            </td>
                            <td>  <telerik:RadDatePicker ID="RadDatePickerStart" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker> </td>
                            <td>  至</td>
                            <td><telerik:RadDatePicker ID="RadDatePickerEnd" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker> </td>
                            
                            <td><asp:Button ID="BtnQuery" runat="server" CssClass="button" Text="查 询" OnClick="BtnQuery_Click" /> </td>
                            <td><asp:Button ID="ButtonExport" runat="server" CssClass="button" Text="导 出" OnClick="ButtonExport_Click" /> </td>
                        </tr>

                    </table>


                 
                <div class="floatL" style="width: 98%; text-align: left; padding-left: 8px">

                    <telerik:RadGrid ID="RadGridTJ" runat="server"
                        GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                        Width="100%" Skin="Default" EnableAjaxSkinRendering="true"
                        EnableEmbeddedSkins="true" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="false">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                        <Columns>
                              
                                <telerik:GridBoundColumn UniqueName="ENT_City" DataField="ENT_City" HeaderText="区县">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="初始注册" DataField="初始注册" HeaderText="初始注册">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="重新注册" DataField="重新注册" HeaderText="重新注册">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                               
                                <telerik:GridBoundColumn UniqueName="执业企业变更" DataField="执业企业变更" HeaderText="执业企业变更">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn UniqueName="通过" DataField="通过" HeaderText="通过">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn UniqueName="未通过" DataField="未通过" HeaderText="未通过">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn UniqueName="未办结" DataField="未办结" HeaderText="未办结">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                             
                                <telerik:GridBoundColumn UniqueName="人数" DataField="人数" HeaderText="人数">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn UniqueName="企业个数" DataField="企业个数" HeaderText="企业个数">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                        </MasterTableView>

                    </telerik:RadGrid>
                    
       
                   
                </div>

            </div>
        </div>

    </form>
</body>
</html>
