<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckTaskConfirm.aspx.cs" Inherits="ZYRYJG.CheckMgr.CheckTaskConfirm" %>

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
                <%-- <telerik:AjaxSetting AjaxControlID="divMain">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divMain" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>--%>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server"></telerik:RadCodeBlock>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;综合监管 &gt;&gt;<strong>监管反馈决定</strong>
                </div>
            </div>
            <div class="content" id="divMain" runat="server">
                <div id="divTask" runat="server">
                    <table class="cx" width="98%" border="0" align="center" cellspacing="5">
                        <tr id="TrPerson" runat="server">
                            <td width="100px" style="text-align: right" nowrap="nowrap">统计方式：
                            </td>
                            <td width="150px" style="text-align: right">
                                <asp:RadioButtonList ID="RadioButtonListGroupType" runat="server" RepeatDirection="Horizontal">
                                    <asp:ListItem Text="全市" Value="全市" Selected="true"></asp:ListItem>
                                    <asp:ListItem Text="按区" Value="按区"></asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <td width="100px" style="text-align: right" nowrap="nowrap">发布时间：
                            </td>
                            <td width="360px" style="text-align: right">

                                <telerik:RadDatePicker ID="RadDatePickerGetDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="130px" />
                                <div class="RadPicker md">至</div>
                                <telerik:RadDatePicker ID="RadDatePickerGetDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="130px" />
                            </td>
                            <td width="100px" style="text-align: right">监管类型：
                            </td>
                            <td width="150px" style="text-align: left">
                                <telerik:RadComboBox ID="RadComboBoxCheckType" runat="server" Width="150">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="全部" Value="" />
                                        <telerik:RadComboBoxItem Text="打击挂证专项治理" Value="打击挂证专项治理" />
                                        <telerik:RadComboBoxItem Text="常态化监管" Value="常态化监管" />
                                        <telerik:RadComboBoxItem Text="双随机检查" Value="双随机检查" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td align="left">
                                <asp:Button ID="ButtonQuery" runat="server" Text="查 询" CssClass="bt_large" OnClick="ButtonQuery_Click" /><a href=""></a>
                            </td>
                        </tr>
                    </table>
                    <div style="width: 98%; margin: 8px auto;">

                        <telerik:RadGrid ID="RadGridTask" runat="server"
                            GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15" OnItemCommand="RadGridTask_ItemCommand"
                            SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <HeaderContextMenu EnableEmbeddedSkins="False">
                            </HeaderContextMenu>
                            <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Center"
                                DataKeyNames="PatchCode,Country">
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Country" DataField="Country" HeaderText="所属区/集团">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="CheckType" DataField="CheckType" HeaderText="监管类型">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="PublishiTime" DataField="PublishiTime" HeaderText="任务发布时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="LastReportTime" DataField="LastReportTime" HeaderText="个人上报<br/>截止时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="DataCount" HeaderText="记录总量">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonDataCount" CommandName="DataCount" runat="server" Style="display: block"><%#Eval("DataCount") %></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                     <telerik:GridTemplateColumn UniqueName="CanceledCount" HeaderText="整改完成<br/>(未办结)">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonCanceledCount" CommandName="CanceledCount" runat="server" Style="display: block"><%#Eval("CanceledCount") %></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" ForeColor="red" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="UnWorkerReportCount" HeaderText="个人未反馈">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonWorkerUnReportCount" CommandName="WorkerUnReportCount" runat="server" Style="display: block"><%#Eval("WorkerUnReportCount") %></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="BackCount" HeaderText="已驳回">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonBackCount" CommandName="BackCount" runat="server" Style="display: block"><%#Eval("BackCount") %></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="UnAcceptCount" HeaderText="区县未审核">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonUnAcceptCount" CommandName="UnAcceptCount" runat="server" Style="display: block"><%#Eval("UnAcceptCount") %></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                     <telerik:GridTemplateColumn UniqueName="UnCheckCount" HeaderText="市级未复审">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonUnCheckCount" CommandName="UnCheckCount" runat="server" Style="display: block"><%#Eval("UnCheckCount") %></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="UnConfirmCheckPassCount" HeaderText="待决定<br/>(市级复审通过)">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonUnConfirmCheckPassCount" CommandName="UnConfirmCheckPassCount" runat="server" Style="display: block; background-color: #7FFFD4"><%#Eval("UnConfirmCheckPassCount") %></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="UnConfirmCheckBackCount" HeaderText="待决定<br/>(市级复审不通过)">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonUnConfirmCheckBackCount" CommandName="UnConfirmCheckBackCount" runat="server" Style="display: block; background-color: #FFE4E1"><%#Eval("UnConfirmCheckBackCount") %></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>

                                    <telerik:GridTemplateColumn UniqueName="ConfirmCount" HeaderText="市级已决定">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButtonConfirmCount" CommandName="ConfirmCount" runat="server" Style="display: block;"><%#Eval("ConfirmCount") %></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
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
                        <asp:ObjectDataSource ID="ObjectDataSourceTask" runat="server" DataObjectTypeName="Model.CheckTaskMDL"
                            SelectMethod="GetListTjByCountryView" TypeName="DataAccess.CheckTaskDAL"
                            SelectCountMethod="SelectCountTjByCountryView" EnablePaging="true"
                            MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                    DefaultValue="" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="ObjectDataSourceTaskCity" runat="server" DataObjectTypeName="Model.CheckTaskMDL"
                            SelectMethod="GetListTjView" TypeName="DataAccess.CheckTaskDAL"
                            SelectCountMethod="SelectCountTjView" EnablePaging="true"
                            MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                    DefaultValue="" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                </div>
                <div id="divCheckFeedBack" runat="server" style="display: none;">
                    <table class="cx" width="98%" border="0" align="center" cellspacing="5">
                        <tr id="Tr1" runat="server">
                            <td width="100px" style="text-align: right">筛选条件：
                            </td>
                            <td width="100px" style="text-align: left" nowrap="nowrap">
                                <telerik:RadComboBox ID="RadComboBoxItem" runat="server" Width="100%">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="姓名" Value="WorkerName" />
                                        <telerik:RadComboBoxItem Text="证件号码" Value="WorkerCertificateCode" />
                                        <telerik:RadComboBoxItem Text="注册单位" Value="Unit" />
                                        <telerik:RadComboBoxItem Text="注册类别" Value="PostTypeName" />
                                        <telerik:RadComboBoxItem Text="证书编号" Value="CertificateCode" />
                                        <telerik:RadComboBoxItem Text="社保情况" Value="SheBaoCase" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td width="400px" style="text-align: left">
                                <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Skin="Default" Width="200px">
                                </telerik:RadTextBox>
                            </td>

                            <td align="left">
                                <asp:Button ID="ButtonFind" runat="server" Text="查 询" CssClass="bt_large" OnClick="ButtonFind_Click" />
                                &nbsp;
                                 <asp:Button ID="ButtonOut" runat="server" Text="导 出" CssClass="bt_large" OnClick="ButtonOut_Click" />
                                &nbsp;
                                <asp:Button ID="ButtonRtn" runat="server" Text="返 回" CssClass="bt_large" OnClick="ButtonRtn_Click" />
                            </td>
                        </tr>
                    </table>
                    <div style="width: 98%; margin: 8px auto;">
                        <telerik:RadGrid ID="RadGridCheckFeedBack" runat="server"
                            GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15" OnExcelExportCellFormatting="RadGridCheckFeedBack_ExcelExportCellFormatting"
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
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="CheckType" DataField="CheckType" HeaderText="监管类型" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode" HeaderText="证件号码">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="phone" DataField="phone" HeaderText="联系方式" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode" HeaderText="证书编号">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="注册类别">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Unit" DataField="Unit" HeaderText="注册单位">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Country" DataField="Country" HeaderText="所属区">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center"  />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="SheBaoCase" DataField="SheBaoCase" HeaderText="社保情况">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>

                                    <telerik:GridBoundColumn UniqueName="GongjijinCase" DataField="GongjijinCase" HeaderText="公积金情况" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ShebaoUnit" DataField="ShebaoUnit" HeaderText="社保单位" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="SourceTime" HeaderText="数据来源" Visible="false">
                                        <ItemTemplate>
                                            <%# string.Format("住房和城乡建设部（{0}）",Convert.ToDateTime(Eval("SourceTime")).ToString("yyyy年M月d日"))%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn UniqueName="WorkerRerpotTime" DataField="WorkerRerpotTime" HeaderText="个人反馈时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="AcceptTime" DataField="AcceptTime" HeaderText="区县审查时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="CheckTime" DataField="CheckTime" HeaderText="复审时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}" Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="CheckResult" DataField="CheckResult" HeaderText="复审结果">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center"  Wrap="false"/>
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="CheckResult" DataField="PassType" HeaderText="合格类型">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ConfirmTime" DataField="ConfirmTime" HeaderText="决定时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="DataStatus" DataField="DataStatus" HeaderText="进度">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left"  Wrap="false"/>
                                    </telerik:GridBoundColumn>
                                     <telerik:GridTemplateColumn UniqueName="SourceTime" HeaderText="整改扫描" >
                                        <ItemTemplate>
                                            <%# Eval("CertCancelCheckResult") == DBNull.Value || Eval("CertCancelCheckResult").ToString()=="0"?"":"整改完成"%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="red" />
                                    </telerik:GridTemplateColumn>
                                  
                                    <telerik:GridTemplateColumn UniqueName="Detail" HeaderText="详细">
                                        <ItemTemplate>
                                            <span class="link_edit" onclick='javascript:SetIfrmSrc("./QuestionFeedBackDetail.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("DataID").ToString())%>");'>详细</span>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
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
                       
                        <table id="TableConfirm" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 20px auto">
                            <tr class="GridLightBK">
                                <td colspan="2" class="barTitle">市级批量决定</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="2" style="color: red">注意：批量操作将对本页面查询结果所有数据进行操作（不分页）</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="20%" align="right">决定结果：</td>
                                <td width="80%" align="left">
                                    <asp:RadioButtonList ID="RadioButtonListDecide" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                        <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="20%" align="right">驳回原因：</td>
                                <td width="80%" align="left">
                                    <asp:TextBox ID="TextBoxConfirmDesc" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="允许通过"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="2" align="center">
                                    <asp:Button ID="ButtonDecide" runat="server" CssClass="bt_large" Text="确认提交" OnClick="ButtonDecide_Click" />&nbsp;&nbsp;
                                          
                                </td>
                            </tr>
                        </table>                       
                    </div>
                      <div style="text-align:center;padding-bottom:40px">
                        <asp:Button ID="ButtonOutReport" runat="server" Text="导出汇总表" CssClass="bt_maxlarge" OnClick="ButtonOutReport_Click" Visible="true" />
                          <div id="downurl" runat="server"></div>
                    </div>
                </div>
            </div>
            <uc2:IframeView ID="IframeView" runat="server" />
            <div id="winpop"></div>
        </div>

    </form>
</body>
</html>
