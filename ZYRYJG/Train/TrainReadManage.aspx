<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="TrainReadManage.aspx.cs" Inherits="ZYRYJG.Train.TrainReadManage" %>

<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc3" %>
<%@ Register Src="~/myhelp.ascx" TagPrefix="uc4" TagName="myhelp" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function validateRadUploadTaboe(source, arguments) {
                arguments.IsValid = getRadUpload('<%= RadUploadSignUpTable.ClientID %>').validateExtensions();
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1" EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 培训点业务 &gt;&gt; <strong>考试成绩录入</strong>
            </div>
             <uc4:myhelp ID="myhelp1" runat="server" PageID="职业技能考试成绩录入.htm" />
        </div>
        <div class="content">
            <table cellpadding="2" cellspacing="0" border="0" width="100%" id="tableMain" runat="server">
                <tr id="trSelectExamPlan" runat="server">
                    <td align="right" style="width: 20%;">
                        <div class="table_cx" style="float: right;">
                            <img alt="" src="../Images/1034.gif" width="11" height="11" style="margin-bottom: -2px; padding-right: 2px;" /><nobr>请选择一个考试计划：</nobr>
                        </div>
                    </td>
                    <td align="left" style="width: 80%;">
                        <telerik:RadTextBox ID="RadTextBoxExamPlan" runat="server" Width="500px" Skin="Default" ReadOnly="true" Style="cursor: not-allowed;">
                        </telerik:RadTextBox>
                        <asp:Button ID="ButtonSelectExamPlan" runat="server" Text="选 择" CssClass="button" OnClick="ButtonSelectExamPlan_Click" />
                    </td>
                </tr>
                <tr id="TrExamPlan" runat="server" style="display: none;">
                    <td colspan="2">
                        <table cellpadding="2" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td width="100px" align="right" nowrap="nowrap">岗位工种：</td>
                                <td align="left" width="300px">
                                    <uc3:PostSelect ID="PostSelect2" runat="server" />
                                </td>
                                <td width="100px" align="right" nowrap="nowrap">考试时间：</td>
                                <td align="left" width="200px">
                                    <telerik:RadComboBox ID="RadComboBoxYear" runat="server" Skin="Office2007" CausesValidation="False"
                                        Width="60px" ExpandAnimation-Duration="0">
                                    </telerik:RadComboBox>
                                    &nbsp;年&nbsp;
                                        <telerik:RadComboBox ID="RadComboBoxMonth" runat="server" Skin="Office2007" CausesValidation="False"
                                            Width="60px" ExpandAnimation-Duration="0">
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
                                <td align="left" style="padding-left: 40px">
                                    <asp:Button ID="Button1" runat="server" Text="查 询" CssClass="bt_large" OnClick="ButtonSearch_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" style="padding-top: 8px">
                                    <telerik:RadGrid ID="RadGridExamPlan" AutoGenerateColumns="False" PagerStyle-AlwaysVisible="true"
                                        runat="server" AllowPaging="True" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序" PageSize="15"
                                        Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" Width="100%"
                                        GridLines="None" OnDeleteCommand="RadGridExamPlan_DeleteCommand">
                                        <ClientSettings EnableRowHoverStyle="true">
                                        </ClientSettings>
                                        <HeaderContextMenu EnableEmbeddedSkins="False">
                                        </HeaderContextMenu>
                                        <MasterTableView CommandItemDisplay="None" DataKeyNames="ExamPlanID,ExamPlanName"
                                            NoMasterRecordsText="　没有可显示的记录">
                                            <Columns>
                                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="ExamPlanName" DataField="ExamPlanName" HeaderText="考试计划名称">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn HeaderText="个人报名时间" UniqueName="SignUpEndDate">
                                                    <ItemTemplate>
                                                        <nobr><%# Convert.ToDateTime(Eval("SignUpStartDate")).ToString("yyyy.MM.dd") + "-" %></nobr>
                                                        <nobr><%# Convert.ToDateTime(Eval("SignUpEndDate")).ToString("MM.dd")%></nobr>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="建委审核时间">
                                                    <ItemTemplate>
                                                        <nobr><%# Eval("StartCheckDate")==DBNull.Value?Convert.ToDateTime(Eval("SignUpEndDate")).ToString("yyyy.MM.dd")  + "-": Convert.ToDateTime(Eval("StartCheckDate")).ToString("yyyy.MM.dd") + "-" %></nobr>
                                                        <nobr><%# Convert.ToDateTime(Eval("LatestPayDate")).ToString("MM.dd")%></nobr>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="准考证发放时间">
                                                    <ItemTemplate>
                                                        <nobr><%# Convert.ToDateTime(Eval("ExamCardSendStartDate")).ToString("yyyy.MM.dd") + "-"%></nobr>
                                                        <nobr><%# Convert.ToDateTime(Eval("ExamCardSendEndDate")).ToString("MM.dd")%></nobr>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn HeaderText="考试时间">
                                                    <ItemTemplate>
                                                        <nobr><%# Convert.ToDateTime(Eval("ExamStartDate")).CompareTo(Convert.ToDateTime(Eval("ExamEndDate")))==0?Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd"):Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd") + " - " + Convert.ToDateTime(Eval("ExamEndDate")).ToString("MM.dd")%></nobr>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridButtonColumn UniqueName="Delete" HeaderText="选择" CommandName="Delete" Text="选择">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                            <HeaderStyle Font-Bold="True" />
                                            <PagerTemplate>
                                                <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                            </PagerTemplate>
                                        </MasterTableView>
                                        <FilterMenu EnableEmbeddedSkins="False">
                                        </FilterMenu>
                                        <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                                        <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                                    </telerik:RadGrid>
                                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.ExamPlanDAL"
                                        DataObjectTypeName="Model.ExamPlanOB" SelectMethod="GetList" InsertMethod="Insert"
                                        EnablePaging="true" UpdateMethod="Update" DeleteMethod="Delete" SelectCountMethod="SelectCount"
                                        MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                                        SortParameterName="orderBy">
                                        <SelectParameters>
                                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                                DefaultValue="" ConvertEmptyStringToNull="false" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div class="table_cx" style="padding-top: 10px;">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                成绩列表
            </div>
            <div style="overflow: auto;">
                <telerik:RadGrid ID="RadGridExamSubjectResult" AutoGenerateColumns="False" runat="server"
                    AllowCustomPaging="true" AllowPaging="True" PageSize="10" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false" Width="98%" GridLines="None" OnExcelExportCellFormatting="RadGridExamSubjectResult_ExcelExportCellFormatting"
                    OnPageIndexChanged="RadGridExamSubjectResult_PageIndexChanged">
                    <ExportSettings FileName="ChengJi" OpenInNewWindow="true">
                    </ExportSettings>
                    <ClientSettings EnableRowHoverStyle="false">
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ExamPlanID" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                        </Columns>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Wrap="false" />
                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                        <AlternatingItemStyle HorizontalAlign="Left" Wrap="false" />
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
            </div>
            <br />
            <div style="text-align: center; font-size: 12px; float: right; padding-right: 40px; padding-bottom: 10px; clear: both;">
                <asp:Button ID="ButtonPrint" runat="server" Text="备份成绩(Excel)" CssClass="bt_maxlarge"
                    OnClick="ButtonPrint_Click" ToolTip="导出成绩结果集" />
            </div>
            <hr style="clear: both;" />
            <div class="table_cx">
                <img alt="" src="../Images/Soft_common.gif" style="margin-bottom: -2px; padding-right: 2px;" />
                成绩导入（合格线设定前可多次导入成绩，每次导入时，系统会自动清除本科目前一次导入的成绩！）
            </div>
            <div style="padding-left: 20px; text-align: left; padding-bottom: 20px;margin:8px 8px;">
                <table style="line-height: 24px;">
                    <tr>
                        <td align="right">模板下载：
                        </td>
                        <td colspan="3" align="left">
                             <asp:Button ID="ButtonDownLoadScoreTemplat" runat="server" Text="下载成绩导入模版" CssClass="bt_maxlarge"
                                OnClick="ButtonDownLoadScoreTemplat_Click" />
                            <span>（提示：请按考试计划名称查询后，再下载带有考生信息的模板，录入成绩后按科目分批导入）</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            考试科目：
                        </td>
                        <td align="left">
                            <telerik:RadComboBox ID="RadComboBoxPostTypeID" runat="server" DataTextField="PostName"
                                DataValueField="PostID" AppendDataBoundItems="true" NoWrap="true" OnInit="RadComboBoxPostTypeID_Init"
                                EmptyMessage="请选择科目" LoadingMessage="加载中..." Skin="Default" CausesValidation="False">
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="!"
                                ControlToValidate="RadComboBoxPostTypeID" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                        </td>
                        <td align="right">
                            成绩导入：
                        </td>
                        <td align="left">
                            <div style="float: left; text-align: left; width: 300px;">
                                <telerik:RadUpload ID="RadUploadSignUpTable" runat="server" InitialFileInputsCount="1"
                                    AllowedFileExtensions="xls" ControlObjectsVisibility="None" MaxFileInputsCount="1"
                                    MaxFileSize="1073741824" Width="220px" Enabled="true" Skin="Hot" EnableAjaxSkinRendering="false"
                                    EnableEmbeddedSkins="false">
                                    <Localization Select="选择文件" />
                                </telerik:RadUpload>
                                <asp:CustomValidator ID="Customvalidator1" runat="server" Display="Dynamic" ClientValidationFunction="validateRadUploadTaboe"
                                    ErrorMessage="只能上传扩展名为xls的Excel文件！"> </asp:CustomValidator>
                            </div>
                            <div style="float: left; padding-left: 3px;">
                                <asp:Button ID="ButtonImportScore" runat="server" Text="导 入" CssClass="bt_large" OnClick="ButtonImportScore_Click"
                                    Enabled="true" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
