<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ItemsMonitoring.aspx.cs" Inherits="ZYRYJG.City.ItemsMonitoring" %>


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
                    当前位置 &gt;&gt;人员全执业生命周期 &gt;&gt;<strong>项目监控</strong>
                </div>
            </div>
            <div class="content">
                <p class="jbxxbt">
                    项目监控
                </p>
                <div style="width: 100%; height: 100%; margin: 10px 0px 20px 0px; text-align: center;">
                     <div class="tjbarw">
                        <div class="tjbar">
                            &nbsp;&nbsp;&nbsp;&nbsp;在施项目总体情况 
                        </div>
                        <div id="person" style="width: 49%; height: 400px;"></div>
                        <div id="item" style="width: 50%; height: 400px; margin-top: -400px; margin-left: 50%;" onclick="javascript:window.location.href='GCList.aspx'"></div>
                    </div>
                      <br />    
                  <div class="tjbarw">
                        <div class="tjbar">
                            &nbsp;&nbsp;&nbsp;&nbsp;在施项目人员预警
                        </div>
                        <div id="person_gouqi" style="width: 49%; height: 400px;"></div>
                        <div id="person_KaDanWei" style="width: 50%; height: 400px; margin-top: -400px; margin-left: 50%;"></div>
                    </div>
                      <br />    
                    <div class="tjbarw">
                        <div class="tjbar">
                            &nbsp;&nbsp;&nbsp;&nbsp;职业历史分析（建造师）
                        </div>
                        <div id="ent" style="width: 100%; height: 400px;"></div>
                    </div>                   
                </div>
            </div>
        </div>
    </form>
</body>
</html>
<script type="text/javascript">
    //在建项目人员
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
                            max: '<%=ViewState["personCount"].ToString()%>',
                            itemStyle: {
                                normal: {
                                    label: {
                                        show: true,
                                        formatter: "{b}：{c}"
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
            text: '在施项目人员总量：<%=ViewState["personCount"].ToString()%> 人',
            x: 'center',
            textStyle: {
                fontSize: 16,
                fontWeight: 'normal',
                color: '#333'
            }
        },
        tooltip: {
            trigger: 'item',
            formatter: "{a} <br/>{b} : {c}人（{d}%）"
        },
        legend: {
            show: false,
            orient: 'horizontal',
            x: 'left',
            y: '75%',
            padding: [8, 20, 8, 20],
            data: ["一级建造师", "一级临时建造师", "二级建造师", "二级临时建造师", "监理师"]
        },
        series: [
            {
                name: '',
                type: 'pie',
                startAngle: 15,
                radius: ['25%', '45%'],
                center: ['50%', '55%'],
                data: [<%=ViewState["person"].ToString()%>],
                itemStyle: {
                    normal: {
                        label: {
                            show: true,
                            formatter: "{b}\r\n{c}人"
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
        myChart.setOption(option);

        //在建项目人员证书过期预警
        var myChart_person_gouqi = echarts.init(document.getElementById('person_gouqi'));
        option_person_gouqi = {
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
                                max: '<%=ViewState["sum_person_gouqi"].ToString()%>'
                            }
                        }
                    },
                    restore: { show: true },
                    saveAsImage: { show: true }
                }
            },
            title: {
                text: '人员证书过期',
                x: 'center',
                textStyle: {
                    fontSize: 16,
                    fontWeight: 'normal',
                    color: '#333'
                }
            },
            tooltip: {
                trigger: 'item',
                formatter: "{a} <br/>{b} : {c}人（{d}%）"
            },
            legend: {
                show: false,
                orient: 'horizontal',
                x: 'left',
                y: '75%',
                padding: [8, 20, 8, 20],
                data: ["一级建造师", "一级临时建造师", "二级建造师", "二级临时建造师", "监理师"]
            },
            series: [
                {
                    name: '',
                    type: 'pie',
                    startAngle: 15,
                    radius: ['25%', '45%'],
                    center: ['50%', '55%'],
                    data: [<%=ViewState["person_gouqi"].ToString()%>],

                    itemStyle: {
                        normal: {
                            label: {
                                show: true,
                                formatter: "{b}\r\n{c}人"
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
            myChart_person_gouqi.setOption(option_person_gouqi);

            //在建项目人员注册单位与实际单位不一致（跨单位）预警
            var myChart_person_KaDanWei = echarts.init(document.getElementById('person_KaDanWei'));
            option_person_KaDanWei = {
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
                                    max: '<%=ViewState["sum_person_KaDanWei"].ToString()%>'
                                }
                            }
                        },
                        restore: { show: true },
                        saveAsImage: { show: true }
                    }
                },
                title: {
                    text: '人员注册单位与实际单位不一致',
                    x: 'center',
                    textStyle: {
                        fontSize: 16,
                        fontWeight: 'normal',
                        color: '#333'
                    }
                },
                tooltip: {
                    trigger: 'item',
                    formatter: "{a} <br/>{b} : {c}人({d}%)"
                },
                legend: {
                    show: false,
                    orient: 'horizontal',
                    x: 'left',
                    y: '75%',
                    padding: [8, 20, 8, 20],
                    data: ["一级建造师", "一级临时建造师", "二级建造师", "二级临时建造师", "监理师"]
                },
                series: [
                    {
                        name: '',
                        type: 'pie',
                        startAngle: 15,
                        radius: ['25%', '45%'],
                        center: ['50%', '55%'],
                        data: [<%=ViewState["person_KaDanWei"].ToString()%>],
                        itemStyle: {
                            normal: {
                                label: {
                                    show: true,
                                    formatter: "{b}\r\n{c}人"
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
                myChart_person_KaDanWei.setOption(option_person_KaDanWei);


                //建造师历史执业分析
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

                var myChartent = echarts.init(document.getElementById('ent'));
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
                                        width: '30%',
                                        funnelAlign: 'center',
                                        max: '<%=ViewState["参与项目"].ToString()%>',
                                        itemStyle: {
                                            normal: {
                                                label: {
                                                    show: true,
                                                    formatter: "{b}：{c}人"
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
                        text: '参与项目人数总数：<%=ViewState["参与项目"].ToString()%>人',
                        x: 'center',
                        textStyle: {
                            fontSize: 16,
                            fontWeight: 'normal',
                            color: '#333'
                        }
                    },
                    tooltip: {
                        trigger: 'item',
                        formatter: "{a} <br/>{b} : {c} 人（{d}%）"
                    },
                    legend: {
                        orient: 'horizontal',
                        x: 'center',
                        y: 'bottom',
                        show:false,
                        padding: [8, 20, 8, 20],
                        data: ["只投不施", "只施不投"]
                    },
                    series: [
                          {
                              type: 'pie',
                              center: ['30%', '50%'],
                              radius: ['35%', '55%'],
                              x: '30%', // for funnel
                              data: [
                                  {
                                      name: '其他参与项目人数',
                                      value: '<%=ViewState["非只投不施"]%>',
                                      itemStyle: labelBottom
                                  },
                                  {
                                      name: '只投不施人数',
                                      value: '<%=ViewState["只投不施"]%>',
                                      itemStyle: labelTop
                                  }
                              ]
                          },
                           {
                               type: 'pie',
                               center: ['70%', '50%'],
                               radius: ['35%', '55%'],
                               x: '70%', // for funnel
                               data: [
                                   {
                                       name: '其他参与项目人数',
                                       value: '<%=ViewState["非只施不投"]%>',
                                       itemStyle: labelBottom
                                   },
                                   {
                                       name: '只施不投人数',
                                       value: '<%=ViewState["只施不投"]%>',
                                       itemStyle: labelTop
                                   }
                               ]
                           }
                    ]
                };
               myChartent.setOption(option);

               //在施频繁变更项目经理项目数量
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
                                       max: '<%=ViewState["在施项目数量"].ToString()%>',
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
                       text: '在施项目总数：<%=ViewState["在施项目数量"]%>个',
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
                       data: ["频繁更换项目经理项目数量"]
                   },
                   series: [
                         {
                             type: 'pie',
                             center: ['50%', '50%'],
                             radius: ['30%', '50%'],
                             x: '0%',
                             data: [
                                 {
                                     name: '其他项目数量',
                                     value: '<%=ViewState["在施项目其它数量"]%>',
                                     itemStyle: labelBottom
                                 },
                                  {
                                      name: '频繁更换项目经理\r\n项目数量',
                                      value: '<%=ViewState["在施频繁更换项目经理项目数量"]%>',
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


</script>
