<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CityMonitoring.aspx.cs" Inherits="ZYRYJG.CityMonitoring" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    <link href="../Css/monitor.css?v=1.0.1" rel="stylesheet" type="text/css" /> 
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/echarts-all.js"></script>
</head>
<body style="padding-bottom:20px">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;人员全执业生命周期 &gt;&gt;<strong>全市监控</strong>
                </div>
            </div>
            <div class="content">
                <p class="jbxxbt">
                    全市监控
                </p>
                <div style="width: 100%; height: 100%; margin: 10px auto; text-align: center; border-collapse: collapse;">
                    <div class="tjbarw">
                        <div class="tjbar">
                            &nbsp;&nbsp;&nbsp;&nbsp;全市执业人员整体情况 
                        </div>
                        <div style="width: 100%; line-height: 300%; text-align: left; padding-left: 20px; font-size: 18px; color: #333333; font-weight: bold">
                            <asp:Label ID="LabelPersonCount" runat="server"></asp:Label>
                        </div>
                        <div id="person" style="width: 48%; height: 370px; padding-left: 1%;"></div>
                        <div id="item" style="width: 48%; height: 370px; margin-top: -370px; margin-left: 50%"></div>
                    </div>
                    <br />                
                   <div class="tjbarw">
                       <div class="tjbar">
                            &nbsp;&nbsp;&nbsp;&nbsp;在施项目执业人员情况 
                        </div>
                        <div id="Div_ZaiShiRegionPerson" runat="server" style="width: 100%;"></div>
                        <div id="Div_ZaiShiQXRY" style="width: 100%; height: 370px;"></div>
                    </div>
                    <br />
                    <div class="tjbarw">
                       <div class="tjbar">
                            &nbsp;&nbsp;&nbsp;&nbsp;各区县人员总量及分布情况 
                        </div>
                        <div id="Div_RegionPerson" runat="server" style="width: 100%;"></div>
                        <div id="QXRY" style="width: 100%; height: 370px;"></div>
                    </div>

                </div>
            </div>
        </div>
        <script type="text/javascript">
            //全市执业人员整体情况 

            //--各执业类型分类统计--
            var myChart = echarts.init(document.getElementById('person'));
            option = {
                toolbox: {
                    show: true,
                    orient: 'vertical',
                    x: 'left',
                    y: 'center',
                    feature: {
                        mark: { show: false },
                        dataView: { show: true, readOnly: false },
                        magicType: {
                            show: true,
                            type: ['pie', 'funnel'],
                            option: {
                                funnel: {
                                    x: '25%',
                                    width: '50%',
                                    funnelAlign: 'center',
                                    max: 60000
                                }
                            }
                        },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                title: {
                    text: '各执业类型分类统计',
                    x: 'center',
                    textStyle: {
                        fontSize: 16,
                        fontWeight: 'normal',
                        color: '#333'
                    }
                },
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c} ({d}%)"
                },
                legend: {
                    orient: 'horizontal',
                    x: 'left',
                    y: '85%',
                    padding: [8, 20, 8, 20],
                    data: ["一级建造师", "一级临时建造师", "二级建造师", "二级临时建造师", "", "监理师", "造价师"]
                },
                series: [
                    {
                        name: '',
                        type: 'pie',
                        startAngle: 15,
                        radius: ['25%', '45%'],
                        center: ['50%', '45%'],
                        data: [<%=ViewState["person"].ToString()%>],
                        itemStyle: {
                            emphasis: {
                                shadowBlur: 10,
                                shadowOffsetX: 0,
                                shadowColor: 'rgba(0, 0, 0, 0.5)'
                            }
                        }
                    }
                ]
            };
            myChart.setOption(option);

            //--执业人员增长趋势--
            function BindChartPersonAdd(data, startMonth, minData) {
                //生成x轴数据
                var xAxis = '';
                for (var i = 1; i < 13; i++) {
                    xAxis += ',\'' + startMonth + '月\'';

                    startMonth++;
                    if (startMonth == 13) {
                        startMonth = 1;
                    }
                }
                if (xAxis != '') {
                    xAxis = xAxis.substring(1, xAxis.length);
                }
                var myChartitem = echarts.init(document.getElementById('item'));
                option = {
                    title: {
                        text: '执业人员增长趋势（近12月）',
                        x: 'center',
                        textStyle: {
                            fontSize: 16,
                            fontWeight: 'normal',
                            color: '#333'
                        }
                    },
                    tooltip: {
                        trigger: 'axis'
                    },
                    legend: {
                        data: []
                    },
                    toolbox: {
                        show: true,
                        orient: 'vertical',
                        x: 'right',
                        y: 'center',
                        feature: {
                            mark: { show: false },
                            dataView: { show: true, readOnly: false },
                            magicType: {
                                show: true,
                                type: ['line', 'bar']
                            },
                            restore: { show: true },
                            saveAsImage: { show: true }
                        }
                    },
                    calculable: true,

                    xAxis: [
                        {
                            radius: '40%',
                            center: ['50%', '60%'],
                            type: 'category',
                            data: eval("[" + xAxis + "]")
                        }
                    ],
                    yAxis: [
                        {
                            type: 'value',
                            scale: true,
                            min: minData
                        }
                    ],
                    series: [
                        {
                            name: '执业人员数量',
                            type: 'bar',
                            data: eval("[" + data + "]")
                        }
                    ]
                };
                myChartitem.setOption(option); // 使用刚指定的配置项和数据显示图表。
                myChartitem.setTheme('macarons');
            }

            // --各区县在施人员总量及分布情况--
            var myChartZaiShiQXRY = echarts.init(document.getElementById('Div_ZaiShiQXRY'));
            option = {
                grid: {
                    x: 60,
                    y: 60,
                    x2: 20,
                    y2: 60
                },
                tooltip: {
                    trigger: 'axis'
                },
                toolbox: {
                    show: true,
                    x: 'right',
                    y: '20',
                    feature: {
                        mark: { show: false },
                        dataView: { show: true, readOnly: false },
                        magicType: { show: true, type: ['line', 'bar', 'stack', 'tiled'] },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                calculable: true,
                legend: {
                    data: ['在施一级建造师', '在施一级临时建造师', '在施二级建造师', '在施二级临时建造师', '在施监理师'],
                    x: 'center',
                    y: 'bottom'
                },
                xAxis: [
                    {
                        type: 'category',
                        padding: 5,
                        data: ['东城', '西城', '海淀', '朝阳', '石景山', '丰台', '大兴', '亦庄', '房山', '门头沟', '密云', '平谷', '通州', '顺义', '昌平', '延庆', '怀柔'],
                        categoryWidth: 1,
                        axisLabel: {
                            margin: 2,
                            interval: 0,
                            textStyle: {
                                fontSize: 12,
                                fontWeight: 'normal',
                                color: '#333'
                            }
                        }
                    }
                ],
                yAxis: [
                    {
                        type: 'value'
                    }
                ],
                series: [<%=ViewState["RegionZaiShiPersonValue"]%>]
            };
            myChartZaiShiQXRY.setOption(option);
            myChartZaiShiQXRY.setTheme('macarons');

            // --各区县人员总量及分布情况--
            var myChartQXRY = echarts.init(document.getElementById('QXRY'));
            option = {
                grid: {
                    x: 60,
                    y: 60,
                    x2: 20,
                    y2: 60
                },
                tooltip: {
                    trigger: 'axis'
                },
                toolbox: {
                    show: true,
                    x: 'right',
                    y: '20',
                    feature: {
                        mark: { show: false },
                        dataView: { show: true, readOnly: false },
                        magicType: { show: true, type: ['line', 'bar', 'stack', 'tiled'] },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                calculable: true,
                legend: {
                    data: ['一级建造师', '一级临时建造师', '二级建造师', '二级临时建造师', '监理师', '造价师'],
                    x: 'center',
                    y:'bottom'
                },
                xAxis: [
                    {
                        type: 'category',
                        padding: 5,
                        data: ['东城', '西城', '海淀', '朝阳', '石景山', '丰台', '大兴', '亦庄', '房山', '门头沟', '密云', '平谷', '通州', '顺义', '昌平', '延庆', '怀柔'],
                        categoryWidth: 1,
                        axisLabel: {
                            margin: 2,
                            interval: 0,
                            textStyle: {
                                fontSize: 12,
                                fontWeight: 'normal',
                                color: '#333'
                            }
                        }
                    }
                ],
                yAxis: [
                    {
                        type: 'value'
                    }
                ],
                series: [<%=ViewState["RegionPersonValue"]%>]
            };
            myChartQXRY.setOption(option);
            myChartQXRY.setTheme('macarons');
        </script>
    </form>
</body>
</html>
