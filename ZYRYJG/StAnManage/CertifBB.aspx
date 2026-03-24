<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CertifBB.aspx.cs" Inherits="ZYRYJG.StAnManage.CertifBB" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Charting" TagPrefix="telerik" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Css/styleRed.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="PostSelect1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PostSelect1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Sunset" />
        <div class="div_out">
            <div class="dqts">
                <img src="../Images/ts.gif" width="17" height="11" />当前位置 &gt;&gt; 统计分析 &gt;&gt;
            从业人员业务统计 &gt;&gt; <strong>证书管理报表</strong>
            </div>
            <div class="content">
                <table class="bar_cx">
                    <tr>
                        <td align="right" nowrap="nowrap" style="width: 11%">日期：
                        </td>
                        <td align="left" nowrap="nowrap" style="width: 39%">
                            <telerik:RadDatePicker ID="txtStartDate" MinDate="01/01/1900" runat="server" Style="width: 45%" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                            </telerik:RadDatePicker>
                            <div class="RadPicker">至</div>
                            <telerik:RadDatePicker ID="txtEndtDate" MinDate="01/01/1900" runat="server" Style="width: 45%" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                            </telerik:RadDatePicker>
                        </td>
                        <td align="right" nowrap="nowrap" style="width: 11%">岗位工种：
                        </td>
                        <td align="left" nowrap="nowrap" style="width: 39%">
                            <uc1:PostSelect ID="PostSelect1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="Button1" runat="server" Text="查询" CssClass="button" OnClick="Button1_Click" />
                        </td>
                    </tr>
                </table>
                <div align="center" style="width: 100%; padding: 10px 0px 20px 0px;">
                    <telerik:RadChart ID="RadChart_FDC" runat="server" Height="250px" AutoTextWrap="True"
                        Skin="Gray" AutoLayout="True" OnItemDataBound="RadChart_FDC_ItemDataBound" Width="700px"
                        PlotArea-EmptySeriesMessage-TextBlock-Text="">
                        <PlotArea>
                            <XAxis>
                                <Appearance Color="226, 218, 202" MajorTick-Color="216, 207, 190">
                                    <MajorGridLines Color="226, 218, 202" PenStyle="Solid"></MajorGridLines>
                                    <TextAppearance TextProperties-Color="112, 93, 56">
                                    </TextAppearance>
                                </Appearance>
                                <AxisLabel>
                                    <Appearance Position-AlignedPosition="Left">
                                    </Appearance>
                                    <TextBlock>
                                        <Appearance TextProperties-Color="112, 93, 56">
                                        </Appearance>
                                    </TextBlock>
                                </AxisLabel>
                            </XAxis>
                            <YAxis>
                                <Appearance Color="226, 218, 202" MinorTick-Color="226, 218, 202" MajorTick-Color="226, 218, 202">
                                    <MajorGridLines Color="231, 225, 212"></MajorGridLines>
                                    <MinorGridLines Color="231, 225, 212"></MinorGridLines>
                                    <TextAppearance TextProperties-Color="112, 93, 56">
                                    </TextAppearance>
                                </Appearance>
                                <AxisLabel>
                                    <TextBlock>
                                        <Appearance TextProperties-Color="112, 93, 56">
                                        </Appearance>
                                    </TextBlock>
                                </AxisLabel>
                            </YAxis>
                            <Appearance Dimensions-Margins="18%, 23%, 12%, 10%">
                                <FillStyle MainColor="254, 255, 228" SecondColor="Transparent" FillType="Solid">
                                </FillStyle>
                                <Border Color="226, 218, 202"></Border>
                            </Appearance>
                        </PlotArea>
                        <Appearance Corners="Round, Round, Round, Round, 7">
                            <FillStyle MainColor="235, 249, 213" FillType="ComplexGradient">
                                <FillSettings GradientMode="Horizontal">
                                    <ComplexGradient>
                                        <telerik:GradientElement Color="236, 236, 236" />
                                        <telerik:GradientElement Color="248, 248, 248" Position="0.5" />
                                        <telerik:GradientElement Color="236, 236, 236" Position="1" />
                                    </ComplexGradient>
                                </FillSettings>
                            </FillStyle>
                            <Border Color="203, 225, 169"></Border>
                        </Appearance>
                        <Series>
                            <telerik:ChartSeries Name="Series 1" DataYColumn="value">
                                <Appearance LegendDisplayMode="ItemLabels">
                                    <FillStyle MainColor="243, 206, 119" FillType="ComplexGradient">
                                        <FillSettings>
                                            <ComplexGradient>
                                                <telerik:GradientElement Color="243, 206, 119"></telerik:GradientElement>
                                                <telerik:GradientElement Color="236, 190, 82" Position="0.5"></telerik:GradientElement>
                                                <telerik:GradientElement Color="210, 157, 44" Position="1"></telerik:GradientElement>
                                            </ComplexGradient>
                                        </FillSettings>
                                    </FillStyle>
                                    <TextAppearance TextProperties-Color="112, 93, 56">
                                    </TextAppearance>
                                    <Border Color="223, 170, 40"></Border>
                                </Appearance>
                            </telerik:ChartSeries>
                        </Series>
                        <ChartTitle>
                            <Appearance Position-AlignedPosition="Top">
                                <FillStyle MainColor="">
                                </FillStyle>
                            </Appearance>
                            <TextBlock Text="">
                                <Appearance TextProperties-Color="77, 153, 4" TextProperties-Font="Arial, 10.5pt, style=Bold">
                                </Appearance>
                            </TextBlock>
                        </ChartTitle>
                        <Legend>
                            <Appearance Corners="Round, Round, Round, Round, 7" Dimensions-Margins="17.6%, 3%, 1px, 1px"
                                Dimensions-Paddings="2px, 8px, 6px, 3px" Position-AlignedPosition="TopRight">
                                <ItemTextAppearance TextProperties-Color="113, 94, 57">
                                </ItemTextAppearance>
                                <ItemMarkerAppearance Figure="Rectangle">
                                    <Border Width="0" />
                                </ItemMarkerAppearance>
                                <FillStyle MainColor="">
                                </FillStyle>
                                <Border Color="225, 217, 201" Width="0"></Border>
                            </Appearance>
                        </Legend>
                    </telerik:RadChart>
                </div>

            </div>
        </div>
    </form>
</body>
</html>
