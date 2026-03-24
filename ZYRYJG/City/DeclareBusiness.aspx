<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeclareBusiness.aspx.cs" Inherits="ZYRYJG.City.DeclareBusiness" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html>

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
                    当前位置 &gt;&gt;统计分析 &gt;&gt;建造师业务统计<strong>受理人办件统计</strong>
                </div>
            </div>
            <div class="content">

                <div class="floatL" style="width: 98%; text-align: left; padding-left: 8px">

                    <table border="0" align="left" cellspacing="7">
                        <tr style="top: 10px">
                            <td>
                                <telerik:RadComboBox ID="RadComboBoxIten" runat="server" Width="99px" Height="45px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="受理时间：" Value="受理时间" Selected="true" />
                                        <telerik:RadComboBoxItem Text="决定时间：" Value="决定时间" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePickerStart" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                            </td>
                            <td>至</td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePickerEnd" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                            </td>
                            <td>
                                <asp:RadioButtonList ID="RadioButtonPSN_Level" runat="server" RepeatDirection="Horizontal" Width="150px" Height="24px">
                                    <asp:ListItem Selected="True" Value="二级">正式</asp:ListItem>
                                    <asp:ListItem Value="二级临时">临时</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td>
                                <asp:Button ID="BtnQuery" runat="server" CssClass="button" Text="查 询" OnClick="BtnQuery_Click" />
                            </td>
                            <td>
                                <asp:Button ID="ButtonExport" runat="server" CssClass="button" Text="导 出" OnClick="ButtonExport_Click" />
                            </td>
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
                                    <telerik:GridBoundColumn UniqueName="延期注册" DataField="延期注册" HeaderText="延续注册">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="增项注册" DataField="增项注册" HeaderText="增项注册">
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
                                    <telerik:GridBoundColumn UniqueName="企业信息变更" DataField="企业信息变更" HeaderText="企业信息变更">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="执业企业变更" DataField="执业企业变更" HeaderText="执业企业变更">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="个人信息变更" DataField="个人信息变更" HeaderText="个人信息变更">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="合计" DataField="合计" HeaderText="合计">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <HeaderStyle Font-Bold="True" />
                            </MasterTableView>

                        </telerik:RadGrid>

                        <div id="item" style="width: 100%; height: 400px; margin: 0 auto"></div>
                        <script type="text/javascript">
                            //注册人员事项统计图
                            // 基于准备好的dom，初始化echarts实例
                            var myChartitme = echarts.init(document.getElementById('item'));
                            <%--  option = {
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
                            series: [                                
                                {
                                    name: '',
                                    type: 'pie',
                                    radius: ['40%', '70%'],
                                    center: ['50%', '45%'],

                                    data: [
                                         <%=ViewState["SXSL"]%>
                                    ],
                                    itemStyle: {
                                        normal: {
                                            label: {
                                                show: true,
                                                formatter: "{b}，{d}%"
                                            },
                                            labelLine: {
                                                show: true
                                            }
                                        },
                                        emphasis: {
                                            shadowBlur: 10,
                                            shadowOffsetX: 0,
                                            shadowColor: 'rgba(0, 0, 0, 0.5)'
                                        }
                                    }
                                }
                            ]
                        };--%>
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
                        </script>
                    </div>

                </div>
            </div>
    </form>
</body>
</html>
