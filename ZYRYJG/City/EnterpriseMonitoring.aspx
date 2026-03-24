<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EnterpriseMonitoring.aspx.cs" Inherits="ZYRYJG.City.EnterpriseMonitoring" %>

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
        .barpan
        {
             border: 1px solid #ccc;
             border-radius:8px 8px;
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
            <div  style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;人员全执业生命周期 &gt;&gt;<strong>企业监控</strong>
                    </div>
                </div>
                <div class="content">
                    <p class="jbxxbt">
                        企业监控
                    </p>
                    <div style="width: 100%; height: 100%; margin: 0; text-align: center;">
                         <div style="text-align:right; padding-bottom:8px; display:none;">
                          <input id="Button1" style="width:100px" type="button"  value="查看全市企业" class="button" onclick='javascript: location("CityEnterprise.aspx"); ' />
                        </div>
                        <div style="width: 100%; background-color: #5DA2EF; text-align: left">
                            <span style="font-size:20px;color:#444; line-height:250%; padding-left:20px ; color:white">截止<%=DateTime.Now.ToString("yyyy年MM月dd日") %>为止全市共有建筑业企业共计 <%=ViewState["施工企业数量"].ToString()%> 家 </span>
                        </div>
                        <br />
                        <div id="person" class="barpan" style="width: 33%; height: 300px;" onclick="javascript:window.location.href='QYZZWDBList.aspx'"></div>
                        <div id="item" class="barpan" style="width: 33%; height: 300px; margin-top: -301px; margin-left: 34%;"  onclick="javascript:window.location.href='QYAQXKWDBList.aspx'"></div>
                        <div id="ent" class="barpan" style="width: 32%; height: 300px; margin-top: -302px; margin-left: 68%; " onclick="javascript:window.location.href='PersonLostList.aspx'"></div>
                                               
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    //企业资质标准预警 
    var labelTop = {
        normal: {
            label: {
                show: true,
                position: 'center',
                formatter: "{b}\r\n{c}人（{d}%）",
                textStyle: {
                    x: 'left',
                    y: 'center'
                }
            },
            labelLine: {
                show: false
            }
        }
    };
    var labelBottom = {
        normal: {
            color: '#ccc',
            label: {
                show: false,
                position: 'center'
            },
            labelLine: {
                show: false
            }
        },
        emphasis: {
            color: 'rgba(0,0,0,0)'
        }
    }
    var myChart_person = echarts.init(document.getElementById('person'));
    option_person = {
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
                            width: '30%',
                            funnelAlign: 'center',
                            max: '<%=ViewState["施工企业数量"].ToString()%>',
                                       itemStyle: {
                                           normal: {
                                               label: {
                                                   show: true,
                                                   position: 'right',
                                                   x: '75%',
                                                   formatter: "{b}：{c}个（{d}%）"
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
                               }
                           },
                           restore: { show: true },
                           saveAsImage: { show: true }
                       }
                   },
                   title: {
                       text: '企业资质标准预警',
                       x: 'center',
                       textStyle: {
                           fontSize: 16,
                           fontWeight: 'normal',
                           color: '#333'
                       }
                   },
                   tooltip: {
                       trigger: 'item',
                       formatter: "{a} <br/>{b} : {c}个（{d}%）"
                   },
                   legend: {
                       orient: 'horizontal',
                       x: 'center',
                       y: 'bottom',
                       show: false,
                       padding: [8, 20, 8, 20],
                       data: ["未达标企业数量"]
                   },
                   series: [
                         {
                             type: 'pie',
                             center: ['50%', '50%'],
                             radius: ['40%', '60%'],
                             x: '0%',
                             data: [
                                 {
                                     name: '达标企业数量',
                                     value: '<%=ViewState["已达标企业数量"]%>',
                                     itemStyle: labelBottom
                                 },
                                  {
                                      name: '未达标企业数量',
                                      value: '<%=ViewState["企业资质预警"]%>',
                                      itemStyle: {
                                          normal: {
                                              label: {
                                                  show: true,
                                                  position: 'center',
                                                  formatter: "{b}\r\n{c}个",
                                                  textStyle: {
                                                      x: 'center',
                                                      y: 'center',
                                                      color: '#FF3203'
                                                  }
                                              },
                                              labelLine: {
                                                  show: false
                                              },
                                              color: '#FF3203'
                                          }
                                      }
                                  }
                             ]
                         }
                   ]
               };
    myChart_person.setOption(option_person);

    //企业人员稳定性预警   
    var myChart_ent = echarts.init(document.getElementById('ent'));
    option_ent = {
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
                            width: '30%',
                            funnelAlign: 'center',
                            max: '<%=ViewState["施工企业数量"].ToString()%>',
                            itemStyle: {
                                normal: {
                                    label: {
                                        show: true,
                                        position: 'right',
                                        x: '75%',
                                        formatter: "{b}：{c}个（{d}%）"
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
                    }
                },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        title: {
            text: '企业人员稳定性预警',
            x: 'center',
            textStyle: {
                fontSize: 16,
                fontWeight: 'normal',
                color: '#333'
            }
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c}个（{d}%）"
        },
        legend: {
            orient: 'horizontal',
            x: 'center',
            y: 'bottom',
            show: false,
            padding: [8, 20, 8, 20],
            data: ["建造师流失率超50%的企业数量"]
        },
        series: [
              {
                  type: 'pie',
                  center: ['50%', '50%'],
                  radius: ['40%', '60%'],
                  x: '0%',
                  data: [
                    {
                        name: '建造师流失率低于50%的企业数量',
                        value: '<%=ViewState["建造师流失率低于50%的企业数量"]%>',
                        itemStyle: labelBottom
                    },
                    {
                        name: '流失率超50%企业数',
                        value: '<%=ViewState["建造师流失率超50%的企业数量"]%>',
                        itemStyle: {
                            normal: {
                                label: {
                                    show: true,
                                    position: 'center',
                                    formatter: "{b}\r\n{c}个",
                                    textStyle: {
                                        x: 'center',
                                        y: 'center',
                                        color: '#FF3203'
                                    }
                                },
                                labelLine: {
                                    show: false
                                },
                                color: '#FF3203'
                            }
                        }
                    }
                    ]
                }
        ]
    };
    myChart_ent.setOption(option_ent);

    //var myChart = echarts.init(document.getElementById('person'));
    //option = {
    //    tooltip: {
    //        trigger: 'item',
    //        formatter: "{a} <br/>{b} : {c} ({d}%)"
    //    },
    //    legend: {
    //        orient: 'vertical',
    //        x: 'left',
    //        data: ['企业资质标准预警 763家 ']
    //    },
    //    toolbox: {
    //        show: true,
    //        feature: {
    //            mark: { show: true },
    //            dataView: { show: true, readOnly: false },
    //            magicType: {
    //                show: true,
    //                type: ['pie', 'funnel'],
    //                option: {
    //                    funnel: {
    //                        x: '25%',
    //                        width: '50%',
    //                        funnelAlign: 'center',
    //                        max: 1548
    //                    }
    //                }
    //            },
    //            restore: { show: true },
    //            saveAsImage: { show: true }
    //        }
    //    },
    //    calculable: true,
    //    series: [
    //        {
    //            type: 'pie',
    //            radius: ['50%', '70%'],
    //            itemStyle: {
    //                normal: {
    //                   color: function (value) {return "#" + ("00000" + ((Math.random() * 16777215 + 0.5) >> 0).toString(16)).slice(-6); },
    //                    label: {
    //                        show: false
    //                    },
    //                    labelLine: {
    //                        show: false
    //                    }
    //                },
    //                emphasis: {
    //                    label: {
    //                        show: true,
    //                        position: 'center',
    //                        textStyle: {
    //                            fontSize: '30',
    //                            fontWeight: 'bold'
    //                        }
    //                    }
    //                }
    //            },
    //            data: [
    //                { value: 335, name: '企业资质标准预警 763家 ' }
    //            ]
    //        }
    //    ]
    //};
    //// 使用刚指定的配置项和数据显示图表。
    //myChart.setOption(option);

    // 安全生产许可证标准预警 
    var myChart_item = echarts.init(document.getElementById('item'));
    option_item = {
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
                            width: '30%',
                            funnelAlign: 'center',
                            max: '<%=ViewState["施工企业数量"].ToString()%>',
                            itemStyle: {
                                normal: {
                                    label: {
                                        show: true,
                                        position: 'right',
                                        x: '75%',
                                        formatter: "{b}：{c}个（{d}%）"
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
                    }
                },
                restore: { show: true },
                saveAsImage: { show: true }
            }
        },
        title: {
            text: '安全生产许可证标准预警',
            x: 'center',
            textStyle: {
                fontSize: 16,
                fontWeight: 'normal',
                color: '#333'
            }
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c}个（{d}%）"
        },
        legend: {
            orient: 'horizontal',
            x: 'center',
            y: 'bottom',
            show: false,
            padding: [8, 20, 8, 20],
            data: ["未达标企业数量"]
        },
        series: [
              {
                  type: 'pie',
                  center: ['50%', '50%'],
                  radius: ['40%', '60%'],
                  x: '0%',
                  data: [
                      {
                          name: '达标企业数量',
                          value: '<%=ViewState["安全许可已达标企业数量"]%>',
                                     itemStyle: labelBottom
                                 },
                                  {
                                      name: '未达标企业数量',
                                      value: '<%=ViewState["安全许可预警"]%>',
                                      itemStyle: {
                                          normal: {
                                              label: {
                                                  show: true,
                                                  position: 'center',
                                                  formatter: "{b}\r\n{c}个",
                                                  textStyle: {
                                                      x: 'center',
                                                      y: 'center',
                                                      color: '#FF3203'
                                                  }
                                              },
                                              labelLine: {
                                                  show: false
                                              },
                                              color: '#FF3203'
                                          }
                                      }
                                  }
                             ]
                         }
                   ]
    };
    myChart_item.setOption(option_item);
    // 基于准备好的dom，初始化echarts实例
    //var myChartitme = echarts.init(document.getElementById('item'));
    //option = {
    //    tooltip: {
    //        trigger: 'item',
    //        formatter: "{a} <br/>{b} : {c} ({d}%)"
    //    },
    //    legend: {
    //        orient: 'vertical',
    //        x: 'left',
    //        data: ['安全生产许可证标准预警  360家 ']
    //    },
    //    toolbox: {
    //        show: true,
    //        feature: {
    //            mark: { show: true },
    //            dataView: { show: true, readOnly: false },
    //            magicType: {
    //                show: true,
    //                type: ['pie', 'funnel'],
    //                option: {
    //                    funnel: {
    //                        x: '25%',
    //                        width: '50%',
    //                        funnelAlign: 'center',
    //                        max: 1548
    //                    }
    //                }
    //            },
    //            restore: { show: true },
    //            saveAsImage: { show: true }
    //        }
    //    },
    //    calculable: true,
    //    series: [
    //        {
    //            type: 'pie',
    //            radius: ['50%', '70%'],
    //            itemStyle: {
    //                normal: {
    //                    color: function (value) { return "#" + ("00000" + ((Math.random() * 16777215 + 0.5) >> 0).toString(16)).slice(-6); },
    //                    label: {
    //                        show: false
    //                    },
    //                    labelLine: {
    //                        show: false
    //                    }
    //                },
    //                emphasis: {
    //                    label: {
    //                        show: true,
    //                        position: 'center',
    //                        textStyle: {
    //                            fontSize: '30',
    //                            fontWeight: 'bold'
    //                        }
    //                    }
    //                }
    //            },
    //            data: [
    //                { value: 335, name: '安全生产许可证标准预警  360家 ' }
    //            ]
    //        }
    //    ]
    //};

    //// 使用刚指定的配置项和数据显示图表。
    //myChartitme.setOption(option);


    //////企业人员稳定性预警 
    ////// 基于准备好的dom，初始化echarts实例
    //var myChartent = echarts.init(document.getElementById('ent'));

    //option = {
    //    tooltip: {
    //        trigger: 'item',
    //        formatter: "{a} <br/>{b} : {c} ({d}%)"
    //    },
    //    legend: {
    //        orient: 'vertical',
    //        x: 'left',
    //        data: ['企业人员稳定性预警 360家 ']
    //    },
    //    toolbox: {
    //        show: true,
    //        feature: {
    //            mark: { show: true },
    //            dataView: { show: true, readOnly: false },
    //            magicType: {
    //                show: true,
    //                type: ['pie', 'funnel'],
    //                option: {
    //                    funnel: {
    //                        x: '25%',
    //                        width: '50%',
    //                        funnelAlign: 'center',
    //                        max: 1548
    //                    }
    //                }
    //            },
    //            restore: { show: true },
    //            saveAsImage: { show: true }
    //        }
    //    },
    //    calculable: true,
    //    series: [
    //        {
    //            type: 'pie',
    //            radius: ['50%', '70%'],
    //            itemStyle: {
    //                normal: {
    //                    color: function (value) { return "#" + ("00000" + ((Math.random() * 16777215 + 0.5) >> 0).toString(16)).slice(-6); },
    //                    label: {
    //                        show: false
    //                    },
    //                    labelLine: {
    //                        show: false
    //                    }
    //                },
    //                emphasis: {
    //                    label: {
    //                        show: true,
    //                        position: 'center',
    //                        textStyle: {
    //                            fontSize: '30',
    //                            fontWeight: 'bold'
    //                        }
    //                    }
    //                }
    //            },
    //            data: [
    //                { value: 335, name: '企业人员稳定性预警 360家 ' }
    //            ]
    //        }
    //    ]
    //};
    //// 使用刚指定的配置项和数据显示图表。
    //myChartent.setOption(option);

</script>
