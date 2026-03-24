<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="TrainExamPlan.aspx.cs" Inherits="ZYRYJG.Train.TrainExamPlan" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc3" %>
<%@ Register Src="~/myhelp.ascx" TagPrefix="uc4" TagName="myhelp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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

        <telerik:RadAjaxManager runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" RequestQueueSize="1">
            <AjaxSettings>

                <telerik:AjaxSetting AjaxControlID="ButtonSave">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DivEdit" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="PostSelect2">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PostSelect2" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="PostSelect1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PostSelect1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridExamPlan" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridExamPlan">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridExamPlan" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7"
            Height="1000px" />
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        </telerik:RadCodeBlock>
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt; 培训点业务 &gt;&gt;<strong>考试计划</strong>
                </div>
                 <uc4:myhelp ID="myhelp1" runat="server" PageID="职业技能考试计划.htm" />
            </div>
            <div class="content">
              
                <div class="DivContent" style="color: #444; line-height: 180%">
                    <b>新版职业技能岗位考证流程说明：</b> 
                    <div style="padding:8px 104px">培训点创建考试计划  > 个人登录市住房城乡建设委门户网站（办事大厅系统人员资格管理信息系统）填写报考信息，提交培训点审核 > 培训点审核报名 > 培训点发放准考证 > 个人下载打印准考证 > 个人参加培训点组织的考试 >培训点录入理论和实操成绩 >市建委设定合格线、公告成绩、发放电子证书 > 个人自行下载打印电子证照。
                    </div>
                </div>
                <table class="bar_cx">
                    <tr>
                        <td align="right" width="11%" nowrap="nowrap">计划名称：
                        </td>
                        <td width="39%" align="left">
                            <telerik:RadTextBox ID="RadTextBoxExam_PlanName" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                        <td width="11%" align="right" nowrap="nowrap">岗位工种：
                        </td>
                        <td width="39%" align="left">
                            <uc3:PostSelect ID="PostSelect2" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">报名时间：
                        </td>
                        <td width="39%" align="left">
                            <telerik:RadDatePicker ID="RadDatePicker_SignUpStartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="46%" />
                            <div class="RadPicker">至</div>
                            <telerik:RadDatePicker ID="RadDatePicker_SignUpEndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="46%" />
                        </td>
                        <td align="right" nowrap="nowrap">考试时间：
                        </td>
                        <td width="39%" align="left">
                            <telerik:RadDatePicker ID="RadDatePicker_ExamStartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="46%" />
                            <div class="RadPicker">至</div>
                            <telerik:RadDatePicker ID="RadDatePicker_ExamEndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="46%" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">状态：
                        </td>
                        <td width="39%" align="left">
                            <asp:RadioButtonList ID="rbl" runat="server" RepeatDirection="Horizontal" AutoPostBack="false" Width="300px">
                                <asp:ListItem Value="0">全部</asp:ListItem>
                                <asp:ListItem Value="1">未考试</asp:ListItem>
                                <asp:ListItem Value="2">已考试</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </table>
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    考试计划列表
                </div>
                <div id="DivExamPlan" runat="server" style="width: 98%; margin: 0 auto;">
                    <telerik:RadGrid ID="RadGridExamPlan" AutoGenerateColumns="False" PagerStyle-AlwaysVisible="true"
                        runat="server" AllowPaging="True" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序"
                        Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" Width="100%"
                        GridLines="None" OnItemDataBound="RadGridExamPlan_ItemDataBound" OnInsertCommand="RadGridExamPlan_InsertCommand"
                        OnUpdateCommand="RadGridExamPlan_UpdateCommand" OnItemCommand="RadGridExamPlan_ItemCommand"
                        OnDeleteCommand="RadGridExamPlan_DeleteCommand">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView EditMode="PopUp" CommandItemDisplay="Top" DataKeyNames="ExamPlanID"
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
                                <telerik:GridTemplateColumn HeaderText="培训点审核时间">
                                    <ItemTemplate>
                                        <nobr><%# Eval("StartCheckDate")==DBNull.Value?Convert.ToDateTime(Eval("SignUpEndDate")).ToString("yyyy.MM.dd")  + "-": Convert.ToDateTime(Eval("StartCheckDate")).ToString("yyyy.MM.dd") + "-" %></nobr>
                                        <nobr><%# Convert.ToDateTime(Eval("LatestPayDate")).ToString("MM.dd")%></nobr>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="准考证下载时间">
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
                                <telerik:GridEditCommandColumn UniqueName="Edit" EditText="编辑" HeaderText="编辑">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridButtonColumn UniqueName="Delete" HeaderText="删除"  CommandName="Delete" Text="&lt;span onclick=&quot;if(!confirm('您确定要删除么?'))return false; &quot; &gt;删除&lt;/span&gt;">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <CommandItemSettings AddNewRecordText="添加考试计划" ShowRefreshButton="false" />
                            <EditFormSettings InsertCaption="添加考试计划" CaptionFormatString="编辑考试计划: {0}" CaptionDataField="ExamPlanName"
                                EditFormType="Template" PopUpSettings-Modal="true" FormCaptionStyle-HorizontalAlign="Left">
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                                <FormTemplate>
                                    <div id="DivEdit" runat="server" style="width: 100%; margin: 0 auto">

                                        <table id="Table1" class="bar_cx" cellspacing="0" cellpadding="1" width="98%;">
                                            <tr>
                                                <td align="right" style="width: 7%">
                                                    <font style="color: Red">*</font>考试计划名称：
                                                </td>
                                                <td align="left" style="width: 93%" colspan="3">
                                                    <telerik:RadTextBox runat="server" ID="RadTextBoxExamPlanName" Width="80%" Skin="Default">
                                                    </telerik:RadTextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                                        ControlToValidate="RadTextBoxExamPlanName" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 7%">
                                                    <font style="color: Red">*</font>岗位工种：
                                                </td>
                                                <td align="left">
                                                    <uc3:PostSelect ID="PostSelect1" runat="server" OnPostTypeSelectChange="PostSelect1_OnPostTypeSelectChange" />
                                                </td>
                                                <td align="right" style="width: 7%">
                                                    <font style="color: Red">*</font>技术等级：
                                                </td>
                                                <td align="left">
                                                    <telerik:RadComboBox ID="RadComboBoxPLANSKILLLEVEL" runat="server" Width="80px" Visible="false">
                                                        <Items>
                                                            <telerik:RadComboBoxItem Text="请选择" Value="" Selected="true" />
                                                            <telerik:RadComboBoxItem Text="初级工" Value="初级工" />
                                                            <telerik:RadComboBoxItem Text="中级工" Value="中级工" />
                                                            <telerik:RadComboBoxItem Text="高级工" Value="高级工" />
                                                            <telerik:RadComboBoxItem Text="技师" Value="技师" />
                                                            <telerik:RadComboBoxItem Text="高级技师" Value="高级技师" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 7%; color: #999">勾选包含工种</td>
                                                <td align="left" colspan="3">
                                                    <asp:CheckBoxList ID="CheckBoxListPostID" runat="server" RepeatColumns="7" DataTextField="PostName" Style="margin: 2px 2px; width: auto;"
                                                        DataValueField="PostID" Visible="false">
                                                    </asp:CheckBoxList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" style="width: 7%">
                                                    <font style="color: Red">*</font> 报名时间：
                                                </td>
                                                <td align="left" style="width: 43%">
                                                    <telerik:RadDatePicker ID="RadDatePickerSignUpStartDate" runat="server" Width="40%" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填"
                                                        ControlToValidate="RadDatePickerSignUpStartDate" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <div class="RadPicker">至</div>
                                                    <telerik:RadDatePicker ID="RadDatePickerSignUpEndDate" runat="server" Width="40%" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="必填"
                                                        ControlToValidate="RadDatePickerSignUpEndDate" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right" nowrap="nowrap">
                                                    <font style="color: Red">*</font> 审核时间：
                                                </td>
                                                <td align="left">
                                                    <div class="RadPicker" style="width: 40%">
                                                        <telerik:RadDatePicker ID="RadDatePickerStartCheckDate" runat="server" Width="100%" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                                        </telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ErrorMessage="必填"
                                                            ControlToValidate="RadDatePickerStartCheckDate" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="RadPicker">至</div>

                                                    <div class="RadPicker" style="width: 40%">
                                                        <telerik:RadDatePicker ID="RadDatePickerLatestCheckDate" runat="server" Width="100%" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                                        </telerik:RadDatePicker>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="必填"
                                                            ControlToValidate="RadDatePickerLatestCheckDate" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" nowrap="nowrap">
                                                    <font style="color: Red">*</font> 准考证发放时间：
                                                </td>
                                                <td align="left">
                                                    <telerik:RadDatePicker ID="RadDatePickerExamCardSendStartDate" runat="server" Width="40%" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="必填"
                                                        ControlToValidate="RadDatePickerExamCardSendStartDate" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <div class="RadPicker">至</div>
                                                    <telerik:RadDatePicker ID="RadDatePickerExamCardSendEndDate" runat="server" Width="40%" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="必填"
                                                        ControlToValidate="RadDatePickerExamCardSendEndDate" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                                <td align="right" nowrap="nowrap">
                                                    <font style="color: Red">*</font> 考试时间：
                                                </td>
                                                <td align="left">
                                                    <telerik:RadDatePicker ID="RadDatePickerExamStartDate" runat="server" Width="40%" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="必填"
                                                        ControlToValidate="RadDatePickerExamStartDate" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    <div class="RadPicker">至</div>
                                                    <telerik:RadDatePicker ID="RadDatePickerExamEndDate" runat="server" Width="40%" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="必填"
                                                        ControlToValidate="RadDatePickerExamEndDate" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr id="TrExamPlanSubject" runat="server">
                                                <td align="right" valign="top">考试科目：
                                                </td>
                                                <td align="left" style="height: 120px" valign="top" colspan="3">
                                                    <telerik:RadGrid ID="RadGridExamPlanSubject" runat="server" AutoGenerateColumns="false"
                                                        Width="95%">
                                                        <ClientSettings EnableRowHoverStyle="true">
                                                            <Selecting AllowRowSelect="True" />
                                                        </ClientSettings>
                                                        <MasterTableView NoMasterRecordsText="　没有可显示的记录" DataKeyNames="SubjectID,ExamPlanSubjectID,PostName,ExamStartTime,ExamEndTime">
                                                            <Columns>
                                                                <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="科目名称">
                                                                    <HeaderStyle HorizontalAlign="Center" Width="16%" />
                                                                    <ItemStyle HorizontalAlign="Center" Width="16%" />
                                                                </telerik:GridBoundColumn>
                                                                <telerik:GridTemplateColumn UniqueName="ExamStartTime" HeaderText="考试开始时间">
                                                                    <ItemTemplate>
                                                                        <span style="color: Red">*</span>
                                                                        <telerik:RadDateTimePicker ID="pickerExamStartTime" runat="server" DbSelectedDate='<%# Eval("ExamEndTime")==DBNull.Value?null:Eval("ExamStartTime") %>'
                                                                            Width="90%">
                                                                            <TimeView runat="server" Skin="Default" ShowHeader="False" StartTime="08:00:00" Interval="00:30:00"
                                                                                EndTime="18:00:00" Columns="5">
                                                                            </TimeView>
                                                                        </telerik:RadDateTimePicker>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorExamStartTime" runat="server"
                                                                            ErrorMessage="必填" ControlToValidate="pickerExamStartTime" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                                                    <ItemStyle HorizontalAlign="Center" Width="25%" />
                                                                </telerik:GridTemplateColumn>
                                                                <telerik:GridTemplateColumn UniqueName="ExamEndTime" HeaderText="考试结束时间">
                                                                    <ItemTemplate>
                                                                        <span style="color: Red">*</span>
                                                                        <telerik:RadDateTimePicker ID="pickerExamEndTime" runat="server" DbSelectedDate='<%# Eval("ExamEndTime")==DBNull.Value?null:Eval("ExamEndTime") %>'
                                                                            Width="90%">
                                                                            <TimeView runat="server" Skin="Default" ShowHeader="False" StartTime="09:00:00" Interval="00:30:00"
                                                                                EndTime="19:00:00" Columns="5" Width="90%">
                                                                            </TimeView>
                                                                        </telerik:RadDateTimePicker>
                                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorExamEndTime" runat="server"
                                                                            ErrorMessage="必填" ControlToValidate="pickerExamEndTime" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" Width="25%" />
                                                                    <ItemStyle HorizontalAlign="Center" Width="25%" />
                                                                </telerik:GridTemplateColumn>
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>
                                                    <div style="display: none;">
                                                        <telerik:RadCalendar ID="sharedCalendar" runat="server" EnableMultiSelect="false" />
                                                        <telerik:RadDateTimePicker ID="RadDateTimePicker1" runat="server">
                                                        </telerik:RadDateTimePicker>
                                                    </div>
                                                    <telerik:RadTimeView ID="sharedTimeView" runat="server" StartTime="9:00" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="right" nowrap="nowrap" style="line-height: 200%;">可报名人员：
                                                </td>
                                                <td align="center" nowrap="nowrap" style="line-height: 200%;">待添加身份证（输入身份证号，每行一个身份证号【注意：字母要大写】，支持多行）
                                                </td>
                                                <td align="center" nowrap="nowrap" colspan="2" style="line-height: 200%;">已添加身份证列表（可删除选中行身份证或清空所有行）
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" nowrap="nowrap"></td>
                                                <td align="center" nowrap="nowrap">
                                                    <telerik:RadTextBox ID="RadTextBoxIDCard" runat="server" Width="400px" Height="125px" MaxLength="18000" TextMode="MultiLine" Rows="8"
                                                        Skin="Default">
                                                    </telerik:RadTextBox>

                                                </td>
                                                <td align="center" nowrap="nowrap" colspan="2">
                                                    <telerik:RadListBox ID="RadListBoxIDCard" runat="server" CheckBoxes="false" Width="400px"
                                                        Height="125px">
                                                        <Items>
                                                        </Items>
                                                    </telerik:RadListBox>
                                                    <br />
                                                    <asp:Label ID="LabelIDCardCount" runat="server" Text="" ForeColor="Red"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" nowrap="nowrap"></td>
                                                <td align="center" nowrap="nowrap">
                                                    <asp:Button ID="ButtonAddIDCard" CssClass="button" Text="添加身份证" runat="server" CausesValidation="False"
                                                        CommandName="AddIDCard" ToolTip="每行一个身份证号，一次可添加多行"></asp:Button>
                                                </td>
                                                <td align="center" nowrap="nowrap" colspan="2">
                                                    <asp:Button ID="ButtonDeleteIDCard"
                                                        CssClass="bt_maxlarge" Text="删除选中身份证" runat="server" CausesValidation="False" CommandName="DeleteIDCard"
                                                        ToolTip="删除选中的已添加身份证号"></asp:Button>
                                                    &nbsp;&nbsp;<asp:Button ID="ButtonClearIDCard"
                                                        CssClass="button" Text="清空身份证" runat="server" CausesValidation="False" CommandName="ClearIDCard"
                                                        ToolTip="删除所有已添加身份证号"></asp:Button>
                                                </td>
                                            </tr>
                                            <tr>
                                        </table>
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: center">
                                                    <div style="margin: 20px 0px 30px 0px;">
                                                        <asp:Button ID="ButtonSave" CssClass="button" Text='<%# (Container is GridEditFormInsertItem) ? "保 存" : "更 新" %>'
                                                            runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                                            <asp:Button ID="Button2" CssClass="button" Text="取 消" runat="server" CausesValidation="False"
                                                                CommandName="Cancel"></asp:Button>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <telerik:RadInputManager ID="RadInputManager1" runat="server">
                                        <telerik:TextBoxSetting Validation-IsRequired="true" ErrorMessage="请输入考试计划名称">
                                            <TargetControls>
                                                <telerik:TargetInput ControlID="RadTextBoxExamPlanName" />
                                            </TargetControls>
                                        </telerik:TextBoxSetting>
                                    </telerik:RadInputManager>
                                </FormTemplate>
                                <PopUpSettings Modal="True" Width="95%"></PopUpSettings>
                            </EditFormSettings>
                            <CommandItemStyle />
                            <HeaderStyle Font-Bold="True" />
                            <PagerTemplate>
                                <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
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
                </div>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
        <div id="winpop">
        </div>
    </form>
</body>
</html>
