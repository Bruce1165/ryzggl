<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitWatchInput.aspx.cs" Inherits="ZYRYJG.SystemManage.UnitWatchInpu" %>

<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.1" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.005" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        </telerik:RadCodeBlock>
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ButtonPrint") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
            ReloadOnShow="true" runat="server" Skin="Sunset" EnableShadow="true">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
            <ClientEvents OnRequestStart="onRequestStart" />
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DivMain" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
            Skin="Sunset" />
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt; 系统管理 &gt;&gt; 
                <strong>电子证书错误日志</strong>
                </div>
            </div>

            <div class="content">
                <table class="cx" width="98%" border="0" align="center" cellspacing="5">
                    <tr>
                        <td align="right" nowrap="nowrap">证书类别：
                        </td>
                        <td align="left" width="39%">
                            <telerik:RadComboBox ID="RadComboBoxCertType" runat="server" Width="97%">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                    <telerik:RadComboBoxItem Text="二级注册建造师" Value="二级注册建造师" />
                                    <telerik:RadComboBoxItem Text="安全生产考核三类人员" Value="安全生产考核三类人员" />
                                    <telerik:RadComboBoxItem Text="建筑施工特种作业" Value="建筑施工特种作业" />
                                    <telerik:RadComboBoxItem Text="建设职业技能岗位" Value="建设职业技能岗位" />
                                    <telerik:RadComboBoxItem Text="关键岗位专业技术管理人员" Value="关键岗位专业技术管理人员" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="right" nowrap="nowrap">证书编号：
                        </td>
                        <td align="left" width="39%">
                            <telerik:RadTextBox ID="RadTextBoxCertNo" runat="server" Width="97%" Skin="Default"
                                MaxLength="50">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">操作环节：
                        </td>
                        <td align="left" width="39%">
                            <telerik:RadComboBox ID="RadComboBoxStepName" runat="server" Width="97%">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                    <telerik:RadComboBoxItem Text="创建" Value="创建" />
                                    <telerik:RadComboBoxItem Text="发送" Value="发送" />
                                    <telerik:RadComboBoxItem Text="签发" Value="签发" />
                                    <telerik:RadComboBoxItem Text="下载" Value="下载" />
                                    <telerik:RadComboBoxItem Text="废止" Value="废止" />
                                    <telerik:RadComboBoxItem Text="取回" Value="取回" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="right" nowrap="nowrap">操作时间：
                        </td>
                        <td align="left" width="39%">
                            <telerik:RadDatePicker ID="datePickerFrom" runat="server" Width="45%" DateInput-DateFormat="yyyy-MM-dd"
                                DateInput-DisplayDateFormat="yyyy-MM-dd">
                            </telerik:RadDatePicker>
                            <div class="RadPicker">至</div>
                            <telerik:RadDatePicker ID="datePickerEnd" runat="server" Width="45%" DateInput-DateFormat="yyyy-MM-dd"
                                DateInput-DisplayDateFormat="yyyy-MM-dd">
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">错误内容关键字：
                        </td>
                        <td align="left" colspan="3">
                            <telerik:RadTextBox ID="RadTextBoxErrorMessage" runat="server" Width="97%" Skin="Default"
                                MaxLength="200">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />

                        </td>
                    </tr>
                </table>
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    日志列表
                </div>
                <div style="width: 98%; margin: 0 auto; padding-bottom: 10px;" runat="server" id="DivMain">
                    <telerik:RadGrid ID="RadGridOperateLog" runat="server" GridLines="None" AllowPaging="True"
                        AllowCustomPaging="true" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False"
                        EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true" OnPageIndexChanged="RadGridOperateLog_PageIndexChanged"
                        OnDataBound="RadGridOperateLog_DataBound">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <MasterTableView DataKeyNames="CertNo" NoMasterRecordsText="　没有可显示的记录">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="Highlight <br/> ship name">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <uc3:CheckAll ID="CheckAll1" runat="server" />
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="行号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="36" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="证书类别" DataField="CertType" UniqueName="CertType">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="证书编号" DataField="CertNo" UniqueName="CertNo">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="操作时间" DataField="DoTime" UniqueName="DoTime"
                                    HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd HH:mm}">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="环节" DataField="StepName" UniqueName="StepName">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="失败次数" DataField="ErrorCount" UniqueName="ErrorCount">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ErrorMessage" HeaderText="错误描述" UniqueName="ErrorMessage">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <PagerTemplate>
                                <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.CertificateDAL"
                        SelectMethod="GetListEleCertError" EnablePaging="true" SelectCountMethod="SelectCountEleCertError" MaximumRowsParameterName="maximumRows"
                        StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </div>
        </div>
        <uc4:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
