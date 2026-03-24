<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifUpdateQuery.aspx.cs" Inherits="ZYRYJG.StAnManage.CertifUpdateQuery" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
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
                当前位置 &gt;&gt; 统计分析 &gt;&gt;
                从业人员业务统计 &gt;&gt; <strong>证书数据更新统计</strong>
            </div>
        </div>
        <div class="content">
            <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                统计说明
            </div>
            <div class="DivContent" id="Td3">
                【证书数据更新统计】是按最后更新时间统计证书状态为“首次发证、续期、京内变更、离京变更、进京变更、注销、遗失污损补办”的数量。<br />
                &nbsp;&nbsp;注意“最后更新时间”不代表“业务办理时间”。<br />
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
                                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click"
                                            OnClientClick="changepageView()" />
                        </td>
                    </tr>
                </table>
            </div>
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
                                BorderWidth="0" OnExcelExportCellFormatting="RadGrid1_ExcelExportCellFormatting">
                                <ClientSettings EnableRowHoverStyle="true">
                                    <%-- <Scrolling AllowScroll="True" UseStaticHeaders="true" ScrollHeight="250px" />--%>
                                </ClientSettings>
                                <HeaderContextMenu EnableEmbeddedSkins="False">
                                </HeaderContextMenu>
                                <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录" Width="100%">
                                    <Columns>
                                        <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="首次" DataField="首次" HeaderText="首次<br />发证">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="续期" DataField="续期" HeaderText="续期">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="京内变更" DataField="京内变更" HeaderText="京内<br />变更">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="离京变更" DataField="离京变更" HeaderText="离京<br />变更">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="进京变更" DataField="进京变更" HeaderText="进京<br />变更">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="注销" DataField="注销" HeaderText="注销">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="补办" DataField="补办" HeaderText="遗失污<br />损补办">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="小计" DataField="小计" HeaderText="小计">
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
                        <div style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                            <asp:Button ID="ButtonOutput" runat="server" Text="导出列表" CssClass="bt_large" OnClick="ButtonOutput_Click" />
                        </div>
                    </telerik:RadPageView>
                    <telerik:RadPageView ID="RadPageView2" runat="server">
                        <!--图例-->
                        <div id="container" style="width: 100%">
                        </div>
                    </telerik:RadPageView>
                </telerik:RadMultiPage>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //显示图例
        var showHighcharts = function (id, type, xAxisValue, obj) {
            var options = {
                chart: {
                    type: type,
                    margin: 70,
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
                    title: { text: '证书数据更新统计' + obj.Title + '人数' }
                },
                plotOptions: {
                    column: {
                        depth: 20
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:15px">{point.key}</span><table style="width:180px;">',
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
                    },
                    {
                        name: obj.SeriesNameList[1],
                        data: obj.SeriesDataDictionary[1],
                        dataLabels: {
                            enabled: true
                        }
                    },
                    {
                        name: obj.SeriesNameList[2],
                        data: obj.SeriesDataDictionary[2],
                        dataLabels: {
                            enabled: true
                        }
                    },
                    {
                        name: obj.SeriesNameList[3],
                        data: obj.SeriesDataDictionary[3],
                        dataLabels: {
                            enabled: true
                        }
                    },
                    {
                        name: obj.SeriesNameList[4],
                        data: obj.SeriesDataDictionary[4],
                        dataLabels: {
                            enabled: true
                        }
                    },
                    {
                        name: obj.SeriesNameList[5],
                        data: obj.SeriesDataDictionary[5],
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
                data: { "Key": "CertifUpdateQuery" },
                success: function (obj) {
                    if (obj.length > 0) {
                        var xAxisValue = ['安全生产考核三类人员', '建筑施工特种作业', '造价员', '建设职业技能岗位', '关键岗位专业技术管理人员'];
                        showHighcharts("container", "column", xAxisValue, eval('(' + obj[0] + ')'));
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
