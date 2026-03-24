<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterStatistical.aspx.cs" Inherits="ZYRYJG.County.RegisterStatistical" %>

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
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;统计分析 &gt;&gt;建造师业务统计&gt;&gt;<strong>注册人员及事项统计</strong>
                    </div>
                </div>
                <div class="content">
                     <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                        说明
                    </div>
                    <div class="DivContent" id="Td3">
                        人员类型只对左边饼图有效，左边饼图时间为专业有效期起始时间至专业有效期截止时间，右边饼图时间段为申请表整个业务流程走完，公告时间！
                        <br />
                    </div>
                    <table style="margin-left: 20px">
                        <tr>
                            <td>
                                <telerik:RadComboBox ID="RadComboBoxIfContinue1" runat="server" Width="100px">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="二级" Value="二级" />
                                        <telerik:RadComboBoxItem Text="二级临时" Value="二级临时" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>时间：</td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePickerPRO_ValidityBegin" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                            </td>
                            <td>至</td>
                            <td>
                                <telerik:RadDatePicker ID="RadDatePickerPRO_ValidityEnd" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                            </td>
                            <td>&nbsp; &nbsp; &nbsp; &nbsp; 
                                <asp:Button ID="BtnQuery" runat="server" CssClass="button" Text="查 询" OnClick="BtnQuery_Click" /></td>
                        </tr>

                    </table>
                    <br />
                    <div>
                        <div id="person" style="width: 48%; height: 400px;"></div>
                        <div id="item" style="width: 48%; height: 400px; margin-top: -400px; margin-left: 50%"></div>
                    </div>
                    <script type="text/javascript">
                        //注册人员统计
                        // 基于准备好的dom，初始化echarts实例
                        var myChart = echarts.init(document.getElementById('person'));
                       <%--  option = {
                            title: {
                                text: '注册人员专业统计',
                                subtext: '',
                                x: 'center'
                            },
                            tooltip: {
                                trigger: 'item',
                                formatter: "{a} <br/>{b} : {c} ({d}%)"
                            },
                            legend: {
                                orient: 'horizontal',
                                x: 'center',
                                y: 'bottom',
                                show: true,
                                padding: [8, 20, 8, 20],
                                data: [<%=ViewState["LX"]%>]
                                        },
                                        series: [
                                            {
                                                name: '',
                                                type: 'pie',
                                                radius: '45%',
                                                center: ['50%', '50%'],
                                                data: [
                                        <%=ViewState["SL"]%>
                                    ],
                                    itemStyle: {
                                        normal: {
                                            label: {
                                                show: true,
                                                formatter: "{b}，{c}人"
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
                                    };
                 --%>
                        option = {
                            title: {
                                text: '注册人员专业统计',
                                subtext: '',
                                x: 'center'

                            },
                            tooltip: {
                                trigger: 'item',
                                formatter: "{a} <br/>{b} : {c} ({d}%)"
                            },
                            legend: {
                                orient: 'horizontal',
                                x: 'center',
                                y: 'bottom',
                                data: [<%=ViewState["LX"]%>]
                            },
                            toolbox: {
                                show: true,
                                orient: 'vertical',
                                x: 'left',
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
                                    data: [<%=ViewState["SL"]%>],
                                    itemStyle: {
                                        normal: {
                                            label: {
                                                show: true,
                                                formatter: "{b}{c}人"
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
                        myChart.setOption(option);
                        //注册人员事项统计图
                        // 基于准备好的dom，初始化echarts实例
                        var myChartitme = echarts.init(document.getElementById('item'));
                        <%-- option = {
                            title: {
                                text: '事项类型总数统计',
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
                                    selectedMode: 'single',
                                    radius: [0, '30%'],
                                    center: ['50%', '50%'],

                                    label: {
                                        normal: {
                                            position: 'inner'
                                        }
                                    },
                                    labelLine: {
                                        normal: {
                                            show: false
                                        }
                                    },
                                    data: [
                                         <%=ViewState["BGSL"]%>
                                ],
                                itemStyle: {
                                    normal: {
                                        label: {
                                            show: true,
                                            formatter: "{b}，{c}人"
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

                            },
                                {
                                    name: '',
                                    type: 'pie',
                                    radius: ['30%', '45%'],
                                    center: ['50%', '50%'],

                                    data: [
                                         <%=ViewState["SXSL"]%>
                                    ],
                                    itemStyle: {
                                        normal: {
                                            label: {
                                                show: true,
                                                formatter: "{b}，{c}人"
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
                                text: '事项类型总数统计',
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
                                //padding: [8, 20, 8, 20],
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
                                                funnelAlign: 'center',
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
                                     name: '',
                                     type: 'pie',
                                     selectedMode: 'single',
                                     radius: [0, '30%'],
                                     center: ['50%', '50%'],

                                     label: {
                                         normal: {
                                             position: 'inner'
                                         }
                                     },
                                     labelLine: {
                                         normal: {
                                             show: false
                                         }
                                     },
                                     data: [
                                         <%=ViewState["BGSL"]%>
                                     ],
                                     itemStyle: {
                                         normal: {
                                             label: {
                                                 show: true,
                                                 formatter: "{b}，{c}人"
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

                                 },
                                {
                                    name: '',
                                    type: 'pie',
                                    radius: ['30%', '45%'],
                                    center: ['50%', '50%'],

                                    data: [
                                         <%=ViewState["SXSL"]%>
                                    ],
                                    itemStyle: {
                                        normal: {
                                            label: {
                                                show: true,
                                                formatter: "{b}，{c}人"
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
