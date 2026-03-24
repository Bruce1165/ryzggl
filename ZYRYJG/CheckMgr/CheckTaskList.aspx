<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckTaskList.aspx.cs" Inherits="ZYRYJG.CheckMgr.CheckTaskList" %>

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
    <script type="text/javascript">
        function onRequestStart(sender, args) {
            if (args.get_eventTarget().indexOf("ButtonOut") >= 0) {
                args.set_enableAjax(false);
            }
        }
    </script>
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
            <ClientEvents OnRequestStart="onRequestStart" />
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="divMain">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divMain" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;信息查看 &gt;&gt;<strong>监管异常人员名单</strong>
                </div>
            </div>
            <div class="content" id="divMain" runat="server">
                <table class="cx" width="98%" border="0" align="center" cellspacing="5">
                    <tr id="Tr1" runat="server">
                        <td width="100px" style="text-align: right">筛选条件：
                        </td>
                        <td width="100px" style="text-align: left" nowrap="nowrap">
                            <telerik:RadComboBox ID="RadComboBoxItem" runat="server" Width="100%">
                                <Items>
                                    <telerik:RadComboBoxItem Text="姓名" Value="WorkerName" />
                                    <telerik:RadComboBoxItem Text="证件号码" Value="WorkerCertificateCode" />
                                    <telerik:RadComboBoxItem Text="注册类别" Value="PostTypeName" />
                                    <telerik:RadComboBoxItem Text="证书编号" Value="CertificateCode" />
                                    <telerik:RadComboBoxItem Text="社保情况" Value="SheBaoCase" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="200px" style="text-align: left">
                            <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Skin="Default" Width="200px">
                            </telerik:RadTextBox>
                        </td>
                        <td width="100px" style="text-align: right">发布时间：
                        </td>
                        <td width="200px" style="text-align: left">
                            <telerik:RadComboBox ID="RadComboBoxYear" runat="server" Skin="Office2007" CausesValidation="False"
                                Width="80px" ExpandAnimation-Duration="0">
                            </telerik:RadComboBox>
                            &nbsp;年&nbsp;
                                        <telerik:RadComboBox ID="RadComboBoxMonth" runat="server" Skin="Office2007" CausesValidation="False"
                                            Width="50px" ExpandAnimation-Duration="0">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="全部" Value="" />
                                                <telerik:RadComboBoxItem Text="1" Value="1" />
                                                <telerik:RadComboBoxItem Text="2" Value="2" />
                                                <telerik:RadComboBoxItem Text="3" Value="3" />
                                                <telerik:RadComboBoxItem Text="4" Value="4" />
                                                <telerik:RadComboBoxItem Text="5" Value="5" />
                                                <telerik:RadComboBoxItem Text="6" Value="6" />
                                                <telerik:RadComboBoxItem Text="7" Value="7" />
                                                <telerik:RadComboBoxItem Text="8" Value="8" />
                                                <telerik:RadComboBoxItem Text="9" Value="9" />
                                                <telerik:RadComboBoxItem Text="10" Value="10" />
                                                <telerik:RadComboBoxItem Text="11" Value="11" />
                                                <telerik:RadComboBoxItem Text="12" Value="12" />
                                            </Items>
                                        </telerik:RadComboBox>
                            &nbsp;月
                        </td>
                        <td align="left">
                            <asp:Button ID="ButtonFind" runat="server" Text="查 询" CssClass="bt_large" OnClick="ButtonFind_Click" />
                            &nbsp;
                                 <asp:Button ID="ButtonOut" runat="server" Text="导 出" CssClass="bt_large" OnClick="ButtonOut_Click" />

                        </td>
                    </tr>
                </table>
                <div style="width: 98%; margin: 8px auto;">
                    <telerik:RadGrid ID="RadGridCheckFeedBack" runat="server"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="5" OnExcelExportCellFormatting="RadGridCheckFeedBack_ExcelExportCellFormatting"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Center"
                            DataKeyNames="DataID">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="WorkerCertificateCode" HeaderText="人员信息">
                                    <ItemTemplate>
                                        <p>
                                            <b>姓名：</b><%#Eval("WorkerName") %><br />
                                            <b>证件：</b><%#Eval("WorkerCertificateCode") %><br />
                                            <b>电话：</b><%#Eval("phone") %>
                                        </p>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="CertificateCode" HeaderText="注册证书">
                                    <ItemTemplate>
                                        <p>
                                            <b><%#Eval("PostTypeName") %>：</b><%#Eval("CertificateCode") %><br />
                                            <b>注册单位：</b><%#Eval("Unit") %><br />
                                            <b>所属区：</b><%#Eval("Country") %>
                                        </p>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="question" HeaderText="存在问题">
                                    <ItemTemplate>
                                        <p>
                                            <b>数据来源：</b>住房和城乡建设部（<%#Convert.ToDateTime(Eval("SourceTime")).ToString("yyyy年M月d日") %>）<br />
                                            <b>社保情况：</b><%#Eval("SheBaoCase") %><br />
                                            <b>公积金情况：</b><%#Eval("GongjijinCase") %><br />
                                            <b>社保单位：</b><%#Eval("ShebaoUnit") %>
                                        </p>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="LastReportTime" HeaderText="反馈要求与结果">
                                    <ItemTemplate>
                                        <p>
                                            <b>监管发布时间：</b><%# Convert.ToDateTime(Eval("PublishiTime")).ToString("yyyy年M月d日") %><br />
                                            <b>反馈截止时间：</b><%# Convert.ToDateTime(Eval("LastReportTime")).ToString("yyyy年M月d日") %><br />
                                            <b>个人反馈时间：</b><%#Eval("WorkerRerpotTime") == DBNull.Value?"尚未反馈":Convert.ToDateTime(Eval("WorkerRerpotTime")).ToString("yyyy年M月d日") %><br />
                                            <b>反馈审批结果：</b><%#Eval("DataStatusCode").ToString()=="7"?"审批通过（办结）":
                                                                Eval("DataStatusCode").ToString()=="2"?string.Format("{0}<apan style='color:red'>审批不通过。{1}</apan>",Eval("BackUnit"),Eval("BackReason")):
                                                                Eval("DataStatusCode").ToString()=="3"?"待审批":
                                                                Eval("DataStatusCode").ToString()=="1"?"":"审核中"%>
                                        </p>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridTemplateColumn>
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
                    <asp:ObjectDataSource ID="ObjectDataSourceCheckFeedBack" runat="server" DataObjectTypeName="Model.CheckFeedBackMDL"
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
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
        <div id="winpop"></div>
    </form>
</body>
</html>
