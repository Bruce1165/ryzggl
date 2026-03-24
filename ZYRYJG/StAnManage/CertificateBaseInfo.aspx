<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertificateBaseInfo.aspx.cs" Inherits="ZYRYJG.StAnManage.CertificateBaseInfo" %>
<%@ Import Namespace="ZYRYJG.StAnManage" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.min.js"></script>
    <script type="text/javascript" src="../Scripts/highcharts.js"></script>
     <script type="text/javascript" src="../Scripts/highcharts-3d.js"></script>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 统计分析 &gt;&gt;
                从业人员业务统计 &gt;&gt; <strong>证书数量整体情况</strong>
            </div>
        </div>
        <div  style="width: 98%; margin: 5px auto;">
            <div class="content">
                <p class="jbxxbt">
                    证书数量整体情况</p>
                <p style="color: Blue; text-align: left;">
                    说明：证书总数量= 有效证书数量 + 过期证书数量 + 注销数量 + 离京变更数量。</p>
                <div class="DivContent" runat="server" id="divBaseInfo" style="text-align: left;
                    font-size: 18px; font-weight: bold; line-height: 200%; padding: 20px 20px">
                    自<span style="color: Red;">2011-03-01日</span> 上线以来，截止<asp:Label ID="LabelToday" runat="server"
                        Text="" ForeColor="Red"></asp:Label>日，<br />
                    系统中共有证书总数<asp:Label ID="LabelAllCount" runat="server" Text="" ForeColor="Red"></asp:Label>条，
                    （其中历史数据<asp:Label ID="LabelHistoryCount" runat="server" Text="" ForeColor="Red"></asp:Label>条，
                    人员考务系统产生<asp:Label ID="LabelSysCreateCount" runat="server" Text="" ForeColor="Red"></asp:Label>条）。<br />
                    有效证书<asp:Label ID="LabelValidCount" runat="server" Text="" ForeColor="Red"></asp:Label>条
                    （其中历史数据<asp:Label ID="LabelValidHistoryCount" runat="server" Text="" ForeColor="Red"></asp:Label>条，
                    人员考务系统产生<asp:Label ID="LabelValidSysCreateCount" runat="server" Text="" ForeColor="Red"></asp:Label>条）。<br />
                    证书过期<asp:Label ID="LabelExpireCount" runat="server" Text="" ForeColor="Red"></asp:Label>条
                    （其中历史数据<asp:Label ID="LabelExpireHistoryCount" runat="server" Text="" ForeColor="Red"></asp:Label>条，
                    人员考务系统产生<asp:Label ID="LabelExpireSysCreateCount" runat="server" Text="" ForeColor="Red"></asp:Label>条）。<br />
                    证书注销<asp:Label ID="LabelZuXiaoCount" runat="server" Text="" ForeColor="Red"></asp:Label>条
                    （其中历史数据<asp:Label ID="LabelZuXiaoHistoryCount" runat="server" Text="" ForeColor="Red"></asp:Label>条，
                    人员系统产生数据<asp:Label ID="LabelZuXiaoSysCreateCount" runat="server" Text="" ForeColor="Red"></asp:Label>条）。<br />
                    证书离京变更<asp:Label ID="LabelLiJingCount" runat="server" Text="" ForeColor="Red"></asp:Label>条
                    （其中历史数据<asp:Label ID="LabelLiJingHistoryCount" runat="server" Text="" ForeColor="Red"></asp:Label>条，
                    人员系统产生数据<asp:Label ID="LabelLiJingSysCreateCount" runat="server" Text="" ForeColor="Red"></asp:Label>条）。
                </div>
                <br />
                <br />
            </div>
            <div id="container" style="min-width:700px;height:400px">
            </div>
        </div> 
    </div>
    <script type="text/javascript">
        var showOperateLoghighcharts = function (type) {
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
                    text: '',
                    x: -20
                },
                subtitle: {
                    text: ''
                },
                credits: { enabled: false },
                xAxis: {
                    categories: [
                '系统中证书总数',
                '有效证书',
                '证书过期',
                '证书注销',
                '证书离京变更'
            ]
                },
                plotOptions: {
                    column: {
                        depth: 20
                    }
                },
                tooltip: {
                    headerFormat: '<span style="font-size:15px">{point.key}</span><table style="width:250px;">',
                    pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
            '<td style="padding:0;"><span style="color:red">{point.y}</span> 条</td></tr>',
                    footerFormat: '</table>',
                    shared: true,
                    useHTML: true
                },
                series: [{
                    name: '历史数据',
                    data: <%=Newtonsoft.Json.JsonConvert.SerializeObject(HistroyAarry) %>,
                    dataLabels: {
                        enabled: true
                    }

                }, {
                    name: '人员考务系统产生的数据',
                    data: <%=Newtonsoft.Json.JsonConvert.SerializeObject(ValidSysCreateAarry) %>,
                    dataLabels: {
                        enabled: true
                    }

                }]
            }
            $('#container').highcharts(options);
        }
        $(document).ready(function () {
            showOperateLoghighcharts("column");
        });
    </script>
</asp:Content>
