<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TransactStatistical.aspx.cs" Inherits="ZYRYJG.County.TransactStatistical" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                    当前位置 &gt;&gt;统计分析 &gt;&gt;建造师业务统计&gt;&gt;<strong>受理人办件统计</strong>
                </div>
            </div>
            <div class="content">

                <div class="floatL" style="width: 98%; text-align: left; padding-left: 8px">
                    <div class="floatL">
                        办理时间：从
                    </div>
                    <div class="floatL">
                        <telerik:RadDatePicker ID="RadDatePickerStart" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                    </div>
                    <div class="floatL">
                        至
                    </div>
                    <div class="floatL">
                        <telerik:RadDatePicker ID="RadDatePickerEnd" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                    </div>
                    <div class="floatL">
                        <asp:Button ID="BtnQuery" runat="server" CssClass="button" Text="查 询" OnClick="BtnQuery_Click" />
                    </div>
                </div>
                <div class="floatL" style="width: 98%; text-align: left; padding: 20px 12px">

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
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="GetMan" DataField="GetMan" HeaderText="姓名">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="初始注册" DataField="初始注册" HeaderText="初始注册">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="变更注册" DataField="变更注册" HeaderText="变更注册">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="延期注册" DataField="延期注册" HeaderText="延续注册">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="增项注册" DataField="增项注册" HeaderText="增项注册">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="重新注册" DataField="重新注册" HeaderText="重新注册">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="遗失补办" DataField="遗失补办" HeaderText="遗失补办">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="注销" DataField="注销" HeaderText="注销">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="小计" DataField="小计" HeaderText="小计">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                        </MasterTableView>

                    </telerik:RadGrid>
                    
                    <%--<div id="item" style="width: 100%; height: 400px; margin: 0 auto"></div>
                        <script type="text/javascript">
                        //注册人员事项统计图
                        // 基于准备好的dom，初始化echarts实例
                        var myChartitme = echarts.init(document.getElementById('item'));                         
                            option = {
                                title: {
                                    text: '',
                                    subtext: '',
                                    x: 'center'
                                },
                                tooltip: {
                                    trigger: 'item',
                                    formatter: "{a} <br/>{b}: {c} ({d}%)"
                                },
                                legend: {
                                    orient: 'horizontal',
                                    x: 'center',
                                    y: 'bottom',
                                    show: true,
                                    padding: [8, 20, 8, 20],
                                    data: [<%=ViewState["SXLX"]%>]
                                },
                                toolbox: {
                                    show: true,
                                    orient: 'vertical',
                                    x: 'right',
                                    y: 'center',
                                    feature: {
                                        mark: { show: true },
                                        dataView: { show: true, readOnly: false },
                                        magicType: {
                                            show: true,
                                            type: ['pie', 'funnel'],
                                            option: {
                                                funnel: {
                                                    x: '25%',
                                                    width: '50%',
                                                    funnelAlign: 'left',
                                                    max: 1548
                                                }
                                            }
                                        },
                                        restore: { show: true },
                                        saveAsImage: { show: true }
                                    }
                                },
                                calculable: true,
                                series: [
                                    {
                                        type: 'pie',
                                        radius: '45%',
                                        center: ['50%', '50%'],
                                        data: [<%=ViewState["SXSL"]%>],
                                        itemStyle: {
                                            normal: {
                                                label: {
                                                    show: true,
                                                    formatter: "{b}，{d}%"
                                                },
                                                labelLine: {
                                                    show: true
                                                }
                                            }

                                        },
                                    }

                                ]
                            };
                        // 使用刚指定的配置项和数据显示图表。
                        myChartitme.setOption(option);
                    </script>--%>
                </div>

            </div>
        </div>

    </form>
</body>
</html>
