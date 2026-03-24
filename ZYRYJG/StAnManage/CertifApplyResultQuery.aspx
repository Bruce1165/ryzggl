<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifApplyResultQuery.aspx.cs" Inherits="ZYRYJG.StAnManage.CertifApplyResultQuery" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../Scripts/highcharts.js"></script>
    <script type="text/javascript" src="../Scripts/highcharts-3d.js"></script>
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
                当前位置 &gt;&gt;  统计分析 &gt;&gt;
               从业人员业务统计 &gt;&gt; <strong>业务申请与办理统计</strong>
            </div>
        </div>
        <div class="content">
            <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                统计说明
            </div>
            <div class="DivContent" id="Td3">
                【业务申请与办理统计】是统计查询时间段“考试、续期、京内变更、离京变更、进京变更、注销、遗失污损补办”申请数量和办理通过数据。<br />
                &nbsp;&nbsp;注意，“通过数量”指的是查询时间段里“审核通过”的数量，但不代表这些数据都是这个时间段里申请的，所以“通过数量”有可能大于“申请数量”。<br />
            </div>
            <div id="Divquery" runat="server">
                <table class="bar_cx">
                    <tr>
                        <td align="right" nowrap="nowrap" style="width: 100px;">统计时间范围：自
                        </td>
                        <td align="left">
                            <telerik:RadDatePicker ID="RadDatePicker_StartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="180px" />
                            <div class="RadPicker">至</div>
                            <telerik:RadDatePicker ID="RadDatePicker_EndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="180px" />
                            &nbsp;&nbsp;
                                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" OnClientClick="changepageView()" />

                        </td>
                    </tr>
                </table>
                <div style="width: 98%; margin: 0 auto; padding-top: 8px; border: none;">
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1"
                        SelectedIndex="0" Skin="Outlook" OnClientTabSelected="OnClientTabSelected">
                        <Tabs>
                            <telerik:RadTab runat="server" Text="数据表" PageViewID="RadPageView1" Selected="True">
                            </telerik:RadTab>
                            <telerik:RadTab runat="server" Text="图表" PageViewID="RadPageView2">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                        <telerik:RadPageView ID="RadPageView1" runat="server">
                            <div style="width: 100%; margin: 0 auto; padding-top: 8px; border: none;">
                                <telerik:RadGrid ID="RadGrid1" AutoGenerateColumns="False" runat="server" Skin="Blue"
                                    EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" Width="99%" GridLines="None"
                                    BorderWidth="0" OnExcelExportCellFormatting="RadGrid1_ExcelExportCellFormatting"
                                    OnItemCreated="RadGrid1_ItemCreated">
                                    <ClientSettings EnableRowHoverStyle="true">
                                    </ClientSettings>
                                    <HeaderContextMenu EnableEmbeddedSkins="False">
                                    </HeaderContextMenu>
                                    <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录" Width="100%"
                                        CellPadding="0" CellSpacing="0">
                                        <Columns>
                                            <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="证书类别">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="考试申请个数" DataField="考试申请个数" HeaderText="参加考试个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="考试通过个数" DataField="考试通过个数" HeaderText="考试通过个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                             <telerik:GridBoundColumn UniqueName="缺考个数" DataField="缺考人数" HeaderText="缺考个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="续期申请个数" DataField="续期申请个数" HeaderText="续期申请个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="续期通过个数" DataField="续期通过个数" HeaderText="续期通过个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="续期申请并通过个数" DataField="续期申请并通过个数" HeaderText="续期申请并通过个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="京内变更申请个数" DataField="京内变更申请个数" HeaderText="京内变更申请个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="京内变更通过个数" DataField="京内变更通过个数" HeaderText="京内变更通过个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="京内变更申请并通过个数" DataField="京内变更申请并通过个数" HeaderText="京内变更申请并通过个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="离京变更申请个数" DataField="离京变更申请个数" HeaderText="离京变更申请个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="离京变更通过个数" DataField="离京变更通过个数" HeaderText="离京变更通过个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="离京变更申请并通过个数" DataField="离京变更申请并通过个数" HeaderText="离京变更申请并通过个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="进京变更申请个数" DataField="进京变更申请个数" HeaderText="进京变更申请个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="进京变更通过个数" DataField="进京变更通过个数" HeaderText="进京变更通过个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="进京变更申请并通过个数" DataField="进京变更申请并通过个数" HeaderText="进京变更申请并通过个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="注销申请个数" DataField="注销申请个数" HeaderText="注销申请个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="注销通过个数" DataField="注销通过个数" HeaderText="注销通过个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="注销申请并通过个数" DataField="注销申请并通过个数" HeaderText="注销申请并通过个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                          <%--  <telerik:GridBoundColumn UniqueName="补办申请个数" DataField="补办申请个数" HeaderText="补办申请个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="补办通过个数" DataField="补办通过个数" HeaderText="补办通过个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="补办申请并通过个数" DataField="补办申请并通过个数" HeaderText="补办申请并通过个数">
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Center" />
                                            </telerik:GridBoundColumn>--%>
                                        </Columns>
                                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" />
                                    </MasterTableView>
                                    <FilterMenu EnableEmbeddedSkins="False">
                                    </FilterMenu>
                                </telerik:RadGrid>
                            </div>
                            <div style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                                <asp:Button ID="ButtonOutput" runat="server" Text="导出列表" CssClass="bt_large" OnClick="ButtonOutput_Click" />
                            </div>
                        </telerik:RadPageView>
                        <telerik:RadPageView ID="RadPageView2" runat="server">
                            <!--图例-->
                            <div id="container" style="width: 100%">
                            </div>
                            <div id="renewalcontainer" style="margin-top: 20px; width: 100%">
                            </div>
                            <div id="changesbjcontainer" style="margin-top: 20px; width: 100%">
                            </div>
                            <div id="changeslbjcontainer" style="margin-top: 20px; width: 100%">
                            </div>
                            <div id="changesibjcontainer" style="margin-top: 20px; width: 100%">
                            </div>
                            <div id="cancellationcontainer" style="margin-top: 20px; width: 100%">
                            </div>
                            <div id="reissuecontainer" style="margin-top: 20px; width: 100%">
                            </div>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //考试
        var showExaminationHighcharts = function (type, xAxisValue, examinationobj) {
            var options = {
                chart: {
                    type: type,
                    margin: 75,
                    options3d: {
                        enabled: true,
                        alpha: 0,
                        beta: 0,
                        depth: 50,
                        viewDistance: 25
                    }
                },
                title: {
                    text: examinationobj.Title,
                    x: -20
                },
                subtitle: {
                    text: ''
                },
                credits: { enabled: false },
                xAxis: {
                    categories: xAxisValue,
                    title: { text: '' }
                },
                yAxis: {
                    opposite: true,
                    title: { text: '人数' }
                },
                plotOptions: {
                    column: {
                        depth: 20
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:15px">{point.key}</span><table style="width:150px;">',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                                                '<td style="padding:0;"><span style="color:red">{point.y}</span> </td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                series: [
                            {
                                name: examinationobj.SeriesNameList[0],
                                data: examinationobj.SeriesDataDictionary[0],
                                dataLabels: {
                                    enabled: true
                                }

                            }, {
                                name: examinationobj.SeriesNameList[1],
                                data: examinationobj.SeriesDataDictionary[1],
                                dataLabels: {
                                    enabled: true
                                }

                            }
                ]
            }
            $('#container').highcharts(options);
        }
        //显示图例
        var showHighcharts = function (id, type, xAxisValue, obj) {
            var options = {
                chart: {
                    type: type,
                    margin: 75,
                    options3d: {
                        enabled: true,
                        alpha: 0,
                        beta: 0,
                        depth: 50,
                        viewDistance: 25
                    }
                },
                title: {
                    text: obj.Title,
                    x: -20
                },
                subtitle: {
                    text: ''
                },
                credits: { enabled: false },
                xAxis: {
                    categories: xAxisValue,
                    title: { text: '' }
                },
                yAxis: {
                    opposite: true,
                    title: { text: '人数' }
                },
                plotOptions: {
                    column: {
                        depth: 20
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:15px">{point.key}</span><table style="width:150px;">',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                        '<td style="padding:0;"><span style="color:red">{point.y}</span> </td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                series: [
                    {
                        name: obj.SeriesNameList[0],
                        data: obj.SeriesDataDictionary[0],
                        dataLabels: {
                            enabled: true
                        }

                    }, {
                        name: obj.SeriesNameList[1],
                        data: obj.SeriesDataDictionary[1],
                        dataLabels: {
                            enabled: true
                        }
                    }, {
                        name: obj.SeriesNameList[2],
                        data: obj.SeriesDataDictionary[2],
                        dataLabels: {
                            enabled: true
                        }
                    }
                ]
            }
            $('#' + id + '').highcharts(options);
        }
        var gethighchartsdata = function () {
            $.ajax({
                type: "POST",
                dataType: "JSON",
                url: "../StAnManage/GetHighchartsDataHandler.ashx",
                data: { "Key": "CertifApplyResultQuery" },
                success: function (obj) {
                    if (obj.length > 6) {
                        var xAxisValue = ['项目负责人', '企业主要负责人', '专职安全生产管理人员', '关键岗位专业技术管理人员', '建设职业技能岗位', '造价员', '建筑施工特种作业'];

                        showExaminationHighcharts("column", xAxisValue, eval('(' + obj[0] + ')'));
                        showHighcharts("renewalcontainer", "column", xAxisValue, eval('(' + obj[1] + ')'));
                        showHighcharts("changesbjcontainer", "column", xAxisValue, eval('(' + obj[2] + ')'));
                        showHighcharts("changeslbjcontainer", "column", xAxisValue, eval('(' + obj[3] + ')'));
                        showHighcharts("changesibjcontainer", "column", xAxisValue, eval('(' + obj[4] + ')'));
                        showHighcharts("cancellationcontainer", "column", xAxisValue, eval('(' + obj[5] + ')'));
                        showHighcharts("reissuecontainer", "column", xAxisValue, eval('(' + obj[6] + ')'));
                    }
                },
                error: function () {
                    alert("网络故障！");
                }
            });
        }
        function OnClientTabSelected(sender, eventArgs) {
            var tab = eventArgs.get_tab();
            if (tab.get_text() == "图表") {
                gethighchartsdata();
            }
        }
        var changepageView = function () {
            var multiPage = $find("<%=RadTabStrip1.ClientID %>");
            var tab = multiPage.findTabByText("数据表");
            if (tab)
                tab.set_selected(true);
        }
    </script>
</asp:Content>
