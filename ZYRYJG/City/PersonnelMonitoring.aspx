<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PersonnelMonitoring.aspx.cs" Inherits="ZYRYJG.City.PersonnelMonitoring" %>

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
    <script type="text/javascript" src="../Scripts/jquery-3.4.1.min.js"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body style="padding-bottom:20px">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
         <script type="text/javascript">
             function onRequestStart(sender, args) {

                 if (args.get_eventTarget().indexOf("ButtonExport") >= 0) {
                     args.set_enableAjax(false);

                 }
             }
    </script>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true" ClientEvents-OnRequestStart="onRequestStart">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="divValid">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divValid" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="divRepeat">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divRepeat" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;人员全执业生命周期 &gt;&gt;<strong>人员监控</strong>
                </div>
            </div>
            <div class="content">
                <p class="jbxxbt">
                    人员监控
                </p>
                <div style="width: 100%; height: 100%; margin: 10px auto; text-align: center;">
                    <div class="tjbarw">
                        <div class="tjbar">
                            &nbsp;&nbsp;&nbsp;&nbsp;执业人员整体情况 
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
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 200px; text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;注册有效期到期预警</td>
                                    <td style="text-align: right; width: 80px">年份：</td>
                                    <td style="text-align: left">
                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxYear" runat="server" Width="70px" MaxLength="4" MaxValue="2050" MinValue="1950" DataType="Number" AutoPostBack="true" OnTextChanged="RadNumericTextBoxYear_TextChanged" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator=""></telerik:RadNumericTextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div id="Div_RegionPerson" runat="server" style="width: 100%;"></div>
                        <div id="QXRY" style="width: 100%; height: 370px;"></div>
                    </div>
                    <br />

                    <div id="divRepeat" runat="server" class="tjbarw">
                         <div class="tjbar">
                            <table style="width: 100%">
                                <tr>
                                    <td style="width: 200px; text-align: left">&nbsp;&nbsp;&nbsp;&nbsp;重复注册人员预警</td>
                                    <td style="text-align: right; width: 80px">单位名称：</td>
                                    <td style="text-align: left;">
                                        <telerik:RadTextBox ID="RadTextBoxQYMC" runat="server" Width="200px" MaxLength="50"></telerik:RadTextBox>
                                   &nbsp;&nbsp;<asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click"   /> 
                                        &nbsp;&nbsp;<asp:Button ID="ButtonExport" runat="server" Text="导 出" CssClass="button" OnClick="ButtonExport_Click" />
                                         <span id="spanOutput" runat="server" class="excel" style="padding-right:40px; font-weight:bold"></span>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <telerik:RadGrid ID="RadGridRepeat" runat="server"
                            GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                            Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false">
                            <ClientSettings EnableRowHoverStyle="false" Scrolling-AllowScroll="true" Scrolling-ScrollHeight="250px" Scrolling-UseStaticHeaders="true">
                            </ClientSettings>
                            <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None">
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="46" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="70" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="SFZH" DataField="SFZH" HeaderText="证件号码">
                                        <HeaderStyle HorizontalAlign="left" Wrap="false" Width="120" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="一级建造师" DataField="一级建造师" HeaderText="一级建造师">
                                        <HeaderStyle HorizontalAlign="left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </telerik:GridBoundColumn>                                    
                                    <telerik:GridBoundColumn UniqueName="二级建造师" DataField="二级建造师" HeaderText="二级建造师">
                                        <HeaderStyle HorizontalAlign="left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </telerik:GridBoundColumn>                                  
                                    <telerik:GridBoundColumn UniqueName="监理师" DataField="监理师" HeaderText="监理师">
                                        <HeaderStyle HorizontalAlign="left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="造价师" DataField="造价师" HeaderText="造价师">
                                        <HeaderStyle HorizontalAlign="left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="RepeatNum" DataField="RepeatNum" HeaderText="重复度">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="65" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <HeaderStyle Font-Bold="True" Width="100" />
                            </MasterTableView>

                        </telerik:RadGrid>
                    </div>
                </div>

            </div>
        </div>
        <script type="text/javascript">           
            //--各执业类型分类统计--
            function BindChartPerson(o) {
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
                            data: eval("[" + o + "]"),
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
            }

            //--执业人员增长趋势--
            function BindChartPersonAdd(data,startMonth,minData) {
                //生成x轴数据
                var xAxis='';
                for(var i=1;i<13;i++)
                {
                    xAxis += ',\'' + startMonth + '月\'';
                  
                    startMonth++;
                    if(startMonth==13)
                    {
                        startMonth=1;
                    }
                }
                if(xAxis !='')
                {
                    xAxis = xAxis.substring(1,xAxis.length);
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
                            data: eval("[" + data + "]"),
                            //itemStyle: {
                            //    normal: {
                            //        color: function (value) { return "#" + ("00000" + ((Math.random() * 16777215 + 0.5) >> 0).toString(16)).slice(-6); }
                            //    }
                            //}
                        }
                    ]
                };
                myChartitem.setOption(option); // 使用刚指定的配置项和数据显示图表。
                myChartitem.setTheme('macarons');
            }

            // --全年注册有效期到期预警--
            function BindChartQXRY(o) {
                
                var myChartQXRY = echarts.init(document.getElementById('QXRY'));
                option = {
                    grid: {
                        x: 60,
                        y: 60,
                        x2: 20,
                        y2: 60
                    },
                    tooltip: {
                        trigger: 'axis',
                        textStyle: { align: 'left' }
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
                        y: 'bottom'
                    },
                    xAxis: [
                        {
                            type: 'category',
                            padding: 5,
                            data: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
                            categoryWidth: 1,
                            axisLabel: {
                                margin: 2,
                                interval: 0,
                                formatter: '{value}月',
                                textStyle: {
                                    fontSize: 16,
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
                    series: eval("[" + o + "]")
                };
                myChartQXRY.setOption(option);
                myChartQXRY.setTheme('macarons');
            }
        </script>
    </form>
</body>
</html>

