<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckTaskEdit.aspx.cs" Inherits="ZYRYJG.CheckMgr.CheckTaskEdit" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
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
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true">
            <AjaxSettings>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;综合监管 &gt;&gt;监管任务管理 &gt;&gt;<strong>任务详细</strong>
                </div>
            </div>
            <div class="content">
                <table class="detailTable" cellpadding="5" cellspacing="1">
                    <tr class="GridLightBK">
                        <td colspan="4" class="barTitle">任务基本信息</td>
                    </tr>
                    <tr>
                        <td class="infoHead" style="width: 150px">监管类型：
                        </td>
                        <td class="formItem_1">
                            <telerik:RadComboBox ID="RadComboBoxCheckType" runat="server" Width="150">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" />
                                    <telerik:RadComboBoxItem Text="打击挂证专项治理" Value="打击挂证专项治理" />
                                    <telerik:RadComboBoxItem Text="常态化监管" Value="常态化监管" />
                                    <telerik:RadComboBoxItem Text="双随机检查" Value="双随机检查" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td class="infoHead" style="width: 150px">上报截止日期：</td>
                        <td class="formItem_1">
                            <telerik:RadDatePicker ID="RadDatePickerLastReportTime" runat="server" MinDate="2024-1-1" MaxDate="2050-1-1" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                            </telerik:RadDatePicker>
                        </td>

                    </tr>
                    <tr>
                        <td class="infoHead">创建时间：
                        </td>
                        <td class="formItem_1">
                            <asp:Label ID="LabelCreateTime" runat="server" Text="未创建"></asp:Label>
                        </td>
                        <td class="infoHead">发布时间：</td>
                        <td class="formItem_1">
                            <asp:Label ID="LabelPublishiTime" runat="server" Text="未发布"></asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <td class="infoHead">是否短信通知：
                        </td>
                        <td class="formItem_2">
                            <asp:RadioButtonList ID="RadioButtonListifPhoneNotice" runat="server" RepeatDirection="Horizontal" TextAlign="right" Width="100">
                                <asp:ListItem Text="是" Value="True"></asp:ListItem>
                                <asp:ListItem Text="否" Value="False" Selected="true"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td class="infoHead">是否弹框通知：
                        </td>
                        <td class="formItem_2">
                            <asp:RadioButtonList ID="RadioButtonListifTipNotice" runat="server" RepeatDirection="Horizontal" TextAlign="right" Width="100">
                                <asp:ListItem Text="是" Value="True" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="否" Value="False"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td class="infoHead">短信内容：
                        </td>
                        <td class="formItem_1">
                            <telerik:RadTextBox ID="RadTextBoxPhoneNotice" runat="server" CssClass="textEdit" Width="98%" MaxLength="500" Rows="5" TextMode="MultiLine"></telerik:RadTextBox>
                        </td>
                        <td class="infoHead">弹框内容：
                        </td>
                        <td class="formItem_1">
                            <telerik:RadTextBox ID="RadTextBoxTipNotice" runat="server" CssClass="textEdit" Width="98%" MaxLength="3000" Rows="5" TextMode="MultiLine"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center" colspan="4">
                            <asp:Button ID="ButtonSave" runat="server" Text="保 存" CssClass="bt_large" OnClick="ButtonSave_Click" />&nbsp;
                            <asp:Button ID="ButtonPublish" runat="server" Text="发 布" CssClass="bt_large" OnClick="ButtonPublish_Click" />
                            <%--<asp:Button ID="ButtonPublish" runat="server" Text="发 布" CssClass="bt_large" OnClick="ButtonPublish_Click" 
                                        OnClientClick="return confirm('注意：发布后如果设置了发送短信，系统将自动发送短信。你确定要删除吗?');"  />--%>
                        </td>                                    
                    </tr>
                    <tr class="GridLightBK">
                        <td colspan="4" class="barTitle">导入人员信息
                        </td>
                    </tr>
                    <tr class="GridLightBK" id="trFuJan" runat="server">
                        <td colspan="4">选择要导入的文件：&nbsp;<asp:FileUpload ID="FileUpload1" runat="server" />&nbsp;
                                    <asp:Button ID="ButtonImport" runat="server" Text="导 入" CssClass="button" OnClick="ButtonImport_Click" />
                            （注意：再次导入操作将清空上次导入数据。）&nbsp;&nbsp; <a href="../Template/监管任务导入模板.xlsx">
                                <img src="../Images/xls.gif" title="下载模板" alt="下载模板" style="padding-right: 4px" />下载导入模板</a>（如果下载不了请尝试鼠标右键另存）
                        </td>
                    </tr>
                     <tr class="GridLightBK">
                        <td colspan="4" id="tdImportError" runat="server" style="padding:16px 16px;">
                        </td>
                    </tr>
                </table>
                <div style="width: 99%; margin: 12px auto; padding-bottom: 140px;">
                    <telerik:RadGrid ID="RadGridTask" runat="server"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Center"
                            DataKeyNames="DataID,PatchCode">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CheckType" DataField="CheckType" HeaderText="监管类型">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode" HeaderText="证件号码">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode" HeaderText="证书编号">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="注册类别">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Unit" DataField="Unit" HeaderText="注册单位">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Country" DataField="Country" HeaderText="所属区">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="SheBaoCase" DataField="SheBaoCase" HeaderText="社保情况">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ShebaoUnit" DataField="ShebaoUnit" HeaderText="社保单位">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="GongjijinCase" DataField="GongjijinCase" HeaderText="公积金情况">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ProjectCase" DataField="ProjectCase" HeaderText="在施项目">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="phone" DataField="phone" HeaderText="联系方式">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="SourceTime" DataField="SourceTime" HeaderText="数据来源时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="DataStatus" DataField="DataStatus" HeaderText="状态">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                            <PagerTemplate>
                                <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                        <FilterMenu EnableEmbeddedSkins="False">
                        </FilterMenu>
                        <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    </telerik:RadGrid>
                </div>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.CheckFeedBackMDL"
                    SelectMethod="GetList" TypeName="DataAccess.CheckFeedBackDAL"
                    SelectCountMethod="SelectCount" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>

            </div>
        </div>
    </form>
</body>
</html>
