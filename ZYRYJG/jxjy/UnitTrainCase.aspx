<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitTrainCase.aspx.cs" Inherits="ZYRYJG.jxjy.UnitTrainCase" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc3" %>
<%@ Register Src="~/PostAllSelect.ascx" TagPrefix="uc4" TagName="PostAllSelect" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7">
        </telerik:RadAjaxLoadingPanel>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;在线公益教育培训 &gt;&gt;<strong>企业培训计划完成情况</strong>
                </div>
            </div>
            <div class="content">
                <table class="cx" width="98%" border="0" align="center" cellspacing="5">
                    <tr>
                        <td align="right" nowrap="nowrap" width="11%">年度：
                        </td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxPackageTitle" runat="server" Width="100" Skin="Default" onkeydown="ButtonSearchClick(event);">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right">培训专业方向：
                        </td>
                        <td align="left">
                            <uc4:PostAllSelect runat="server" ID="PostSelect1" />
                        </td>


                        <td align="left">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </table>
                <div style="width: 99%; margin: 20px auto;">
                    <telerik:RadGrid ID="RadGridStudyPlan" runat="server" AutoGenerateColumns="False" AllowPaging="false"
                        EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Skin="Blue" Width="98%">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录">
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="培训年度" UniqueName="TrainYear" DataField="TrainYear">

                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="培训专业方向" UniqueName="PostTypeName" DataField="PostTypeName">

                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="参加培训人数" UniqueName="signupCount" DataField="signupCount">

                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="完成培训人数" UniqueName="finishCount" DataField="finishCount">

                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="培训完成率" UniqueName="FinishPer">
                                    <ItemTemplate>
                                        <%# Convert.ToInt32(Eval("finishCount")) *100 /Convert.ToInt32(Eval("signupCount")) %>%
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="true" />
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
