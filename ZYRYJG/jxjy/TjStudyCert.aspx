<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TjStudyCert.aspx.cs" Inherits="ZYRYJG.jxjy.TjStudyCert" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc1" %>
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
                <telerik:AjaxSetting AjaxControlID="RadGridStudyPlan">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridStudyPlan" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadComboBoxPackageYear">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridStudyPlan" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7">
        </telerik:RadAjaxLoadingPanel>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;在线公益教育培训 &gt;&gt;<strong>培训专业人数统计</strong>
                </div>
            </div>
            <div class="content">
                <table class="cx" width="98%" border="0" align="center" cellspacing="5">
                    <tr>
                        <td align="right" nowrap="nowrap" width="200px">年度：
                        </td>
                        <td align="left" width="100px">
                            <telerik:RadComboBox ID="RadComboBoxPackageYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxPackageYear_SelectedIndexChanged">
                            </telerik:RadComboBox>
                        </td>
                        <td style="text-align: left">&nbsp;&nbsp;<asp:Button ID="ButtonOutput" runat="server" Text="导出列表" CssClass="bt_large" OnClick="ButtonOutput_Click" />
                        </td>
                    </tr>
                </table>
                <div class="table_cx">
                    <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    统计结果列表
                </div>
                <div style="width: 99%; margin: 8px auto;">
                    <telerik:RadGrid ID="RadGridStudyPlan" runat="server" AutoGenerateColumns="False" AllowPaging="false" OnExcelExportCellFormatting="RadGridStudyPlan_ExcelExportCellFormatting"
                        EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Skin="Blue" Width="100%">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录">
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="时间" UniqueName="时间" DataField="时间">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false"  />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="二级建造师" UniqueName="二级建造师" DataField="二级建造师">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="二级造价工程师" UniqueName="二级造价工程师" DataField="二级造价工程师">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="安全生产考核三类人员" UniqueName="安全生产考核三类人员" DataField="安全生产考核三类人员">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="建筑施工特种作业" UniqueName="建筑施工特种作业" DataField="建筑施工特种作业">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="关键岗位专业技术管理人员" UniqueName="关键岗位专业技术管理人员" DataField="关键岗位专业技术管理人员">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="建设职业技能岗位" UniqueName="建设职业技能岗位" DataField="建设职业技能岗位">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="小计" UniqueName="小计" DataField="小计">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>                               
                            </Columns>
                            <HeaderStyle Font-Bold="true" />                             
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
        </div>
        <div id="winpop">
        </div>
        <uc1:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
