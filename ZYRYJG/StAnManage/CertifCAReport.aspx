<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifCAReport.aspx.cs" Inherits="ZYRYJG.StAnManage.CertifCAReport" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .tb {
            border: solid;
            border-width: 1px;
            border-color: #666666;
            border-collapse: collapse;
            width: 100%;
        }

            .tb td {
                text-align: center;
                background-color: #ffffee;
                line-height: 20px;
                border: solid;
                border-width: 1px;
                border-color: #cccccc;
                border-collapse: collapse;
            }
    </style>
    <script type="text/javascript" src="../Scripts/echarts-all.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-3.4.1.min.js"></script>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ButtonOutput") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ButtonSearch" LoadingPanelID="RadAjaxLoadingPanel1"
                        UpdatePanelRenderMode="Inline" />
                    <telerik:AjaxUpdatedControl ControlID="ButtonOutput" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonOutput">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ButtonOutput" />
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 统计分析 &gt;&gt;
                从业人员业务统计 &gt;&gt; <strong>电子证书统计</strong>
            </div>
        </div>
        <div style="margin: 5px auto;">
            <div class="content">
                <%--<div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                    统计说明
                </div>
                <div class="DivContent" id="Td3">
                    【电子证书统计】是按岗位统计生成电子证书数量，及下载人次。<br />
                    &nbsp;&nbsp;输入查询时间段，如“自 2018-1-1 至 2018-12-31 ” 即可查询全年电子证书变下载人次。
                </div>--%>
                <%-- <div id="Divquery" runat="server">
                    <table class="bar_cx">
                        <tr>
                            <td align="right" nowrap="nowrap" style="width: 100px;">下载时间范围：自
                            </td>
                            <td align="left">
                                <telerik:RadDatePicker ID="RadDatePicker_StartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="180px" />
                                <div class="RadPicker">至</div>
                                <telerik:RadDatePicker ID="RadDatePicker_EndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="180px" />
                                &nbsp;&nbsp;
                                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </div>--%>
                <div style="border: 1px solid #999; width: 98%; margin: 0 auto; border-collapse: collapse;">

                    <div style="text-align: center; background-color: #5C9DD3; font-weight: bold; color: white; width: 100%; clear: both; line-height: 40px; position: relative">
                        电子证书整体情况统计（截止到今日）
                    </div>

                    <telerik:RadGrid ID="RadGrid1" AutoGenerateColumns="False" runat="server" Skin="Blue"
                        EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" Width="100%" GridLines="None"
                        BorderWidth="0" OnExcelExportCellFormatting="RadGrid1_ExcelExportCellFormatting">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录" Width="100%">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CerCount" DataField="CerCount" HeaderText="有效证书数量">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ECerCount" DataField="ECerCount" HeaderText="电子证书数量">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="downCount" DataField="downCount" HeaderText="下载人次">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                        </MasterTableView>
                        <FilterMenu EnableEmbeddedSkins="False">
                        </FilterMenu>
                    </telerik:RadGrid>
                </div>
                <div style="width: 98%; margin: 5px auto; text-align: center; padding-top: 8px;">
                    <asp:Button ID="ButtonOutput" runat="server" Text="导出列表" CssClass="bt_large" OnClick="ButtonOutput_Click" />
                </div>
                <%--<div style="width: 98%; margin: 0 auto; padding-top: 8px; border: none;">
                    <telerik:RadGrid ID="RadGridDataByMonth" AutoGenerateColumns="False" runat="server" Skin="Blue"
                        EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" Width="99%" GridLines="None"
                        BorderWidth="0" OnExcelExportCellFormatting="RadGridDataByMonth_ExcelExportCellFormatting">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录" Width="100%">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CerCount" DataField="DMonth" HeaderText="统计时间（年-月）">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ECerCount" DataField="CerCount" HeaderText="产生电子证书数量（本）">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="downCount" DataField="downCount" HeaderText="下载人次（次）">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                        </MasterTableView>
                        <FilterMenu EnableEmbeddedSkins="False">
                        </FilterMenu>
                    </telerik:RadGrid>
                </div>--%>

                <div style="width: 98%; margin: 12px auto; text-align: left">
                    <div style="float: left; text-align: right">请输入统计年份：</div>
                    <div style="float: left; text-align: left">
                        <telerik:RadNumericTextBox ID="RadNumericTextBoxYear" runat="server" Width="70px" MaxLength="4" MaxValue="2050" MinValue="1950" DataType="Number" AutoPostBack="true" OnTextChanged="RadNumericTextBoxYear_TextChanged" ShowSpinButtons="true" NumberFormat-DecimalDigits="0" NumberFormat-GroupSeparator="" Style="float: left"></telerik:RadNumericTextBox>
                    </div>
                    <div style="clear: both;"></div>
                </div>
                <div id="divValid" runat="server" style="border: 1px solid #999; width: 98%; margin: 0 auto; border-collapse: collapse;">

                    <div style="text-align: center; background-color: #5C9DD3; font-weight: bold; color: white; width: 100%; clear: both; line-height: 40px; position: relative">
                        电子证书生成情况年度按月统计
                    </div>

                    <div id="Div_TableECertCreateByMonth" runat="server" style="width: 100%;"></div>
                    <div id="Div_ECharECertCreateByMonth" style="width: 100%; height: 250px;"></div>
                </div>

                <div id="divDown" runat="server" style="border: 1px solid #999; width: 98%; margin: 8px auto; border-collapse: collapse;">
                    <div style="text-align: center; background-color: #5C9DD3; font-weight: bold; color: white; width: 100%; clear: both; line-height: 40px; position: relative">
                        电子证书下载情况年度按月统计
                    </div>
                    <div id="Div_TableECertDownByMonth" runat="server" style="width: 100%;"></div>
                    <div id="Div_ECharECertDownByMonth" style="width: 100%; height: 250px;"></div>
                </div>
            </div>
        </div>
        <script type="text/javascript">
            // --电子证书生成统计--
            function BindECertCreateByMonth(o1, g1, o2, g2) {

                var myChartQXRY = echarts.init(document.getElementById('Div_ECharECertCreateByMonth'));
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
                        data: eval("[" + g1 + "]"),
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
                    series: eval("[" + o1 + "]")
                };
                myChartQXRY.setOption(option);
                myChartQXRY.setTheme('macarons');



                var myChartDown = echarts.init(document.getElementById('Div_ECharECertDownByMonth'));
                optionDown = {
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
                        data: eval("[" + g2 + "]"),
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
                    series: eval("[" + o2 + "]")
                };
                myChartDown.setOption(optionDown);
                myChartDown.setTheme('macarons');



            }
        </script>
</asp:Content>
