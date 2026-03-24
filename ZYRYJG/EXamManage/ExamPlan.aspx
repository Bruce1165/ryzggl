<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true" CodeBehind="ExamPlan.aspx.cs" Inherits="ZYRYJG.EXamManage.ExamPlan" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                报名管理 &gt;&gt; <strong>考试计划</strong>
            </div>
        </div>
        <div class="content">
            <div class="jbxxbt">
                考试工作计划
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
                        <uc1:PostSelect ID="PostSelect2" runat="server" />
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
                            <%-- <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>--%>
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
                            <telerik:GridTemplateColumn HeaderText="企业确认时间" UniqueName="UnitCheckEndDate">
                                <ItemTemplate>
                                    <nobr><%# Convert.ToDateTime(Eval("SignUpStartDate")).AddDays(1).ToString("yyyy.MM.dd") + "-" %></nobr>
                                    <nobr><%# Convert.ToDateTime(Eval("SignUpEndDate")).AddDays(2).ToString("MM.dd")%></nobr>
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
                            <telerik:GridHyperLinkColumn DataNavigateUrlFields="ExamPlanID" DataNavigateUrlFormatString="ExamSignSplit.aspx?o={0}"
                                HeaderText="拆分" Text="拆分" UniqueName="split" Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" ForeColor="Blue" Wrap="false" />
                            </telerik:GridHyperLinkColumn>
                            <telerik:GridEditCommandColumn UniqueName="Edit" EditText="编辑"  HeaderText="编辑">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridEditCommandColumn>
                            <telerik:GridButtonColumn UniqueName="Delete" HeaderText="删除" CommandName="Delete" Text="&lt;span onclick=&quot;if(!confirm('您确定要删除么?'))return false; &quot; &gt;删除&lt;/span&gt;">
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
                                            <td align="left" colspan="3">
                                                <table>
                                                    <tr>
                                                        <td align="left">
                                                            <uc1:PostSelect ID="PostSelect1" runat="server" OnPostTypeSelectChange="PostSelect1_OnPostTypeSelectChange" />
                                                        </td>
                                                        <td align="left">
                                                            <telerik:RadComboBox ID="RadComboBoxPLANSKILLLEVEL" runat="server" Width="80px" Visible="false" Label="技术等级：">
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
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="width: 7%"></td>
                                            <td align="left" colspan="3">
                                                <asp:CheckBoxList ID="CheckBoxListPostID" runat="server" RepeatColumns="4" DataTextField="PostName"
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
                                            <td align="right" style="width: 7%">
                                                <font style="color: Red">*</font> 报名地址：
                                            </td>
                                            <td align="left" style="width: 43%">
                                                <telerik:RadTextBox runat="server" ID="RadTextBoxSignUpPlace" Width="80%" Skin="Default">
                                                </telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必填"
                                                    ControlToValidate="RadTextBoxSignUpPlace" Display="Dynamic"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
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
                                            <td align="right" nowrap="nowrap">
                                                <font style="color: Red">*</font> 缴费截止日期：
                                            </td>
                                            <td align="left">
                                                <div class="RadPicker" style="width: 40%">
                                                    <telerik:RadDatePicker ID="RadDatePickerLatestPayDate" runat="server" Width="100%" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                                    </telerik:RadDatePicker>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="必填"
                                                        ControlToValidate="RadDatePickerLatestPayDate" Display="Dynamic"></asp:RequiredFieldValidator>
                                                </div>
                                                &nbsp;<font style="color: Red">*</font>收费金额(元)：
                                                                <telerik:RadNumericTextBox ID="RadNumericTextBoxExamFee" runat="server" Type="Currency"
                                                                    Width="70px" />
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorExamFee" runat="server" ErrorMessage="必填"
                                                    ControlToValidate="RadNumericTextBoxExamFee" Display="Dynamic"></asp:RequiredFieldValidator>
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
                                        <tr>
                                            <td align="right" nowrap="nowrap">报名人数上限(人)：
                                            </td>
                                            <td align="left"  style="padding: 5px 0">
                                                <telerik:RadNumericTextBox ID="RadNumericTextBoxPersonLimit" runat="server" MaxLength="5"
                                                    Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="70px"
                                                    Value="10000">
                                                    <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>

                                                </telerik:RadNumericTextBox>
                                            </td>
                                             <td align="right" nowrap="nowrap">
                                                <font style="color: Red">*</font> 考试方式：
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioButtonListExamWay" runat="server" RepeatDirection="Horizontal" Width="150px">
                                                    <asp:ListItem Text="机考" Value="机考" Selected="true"></asp:ListItem>
                                                    <asp:ListItem Text="网考" Value="网考" ></asp:ListItem>
                                                </asp:RadioButtonList>                                              
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" nowrap="nowrap">备 注：
                                            </td>
                                            <td align="left" colspan="3" style="padding: 5px 0">
                                                <telerik:RadTextBox runat="server" ID="RadTextBoxRemark" Width="95%" Skin="Default"
                                                    MaxLength="6000" TextMode="MultiLine" Rows="2">
                                                </telerik:RadTextBox>
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
                                    </table>
                                    <div style="width: 98%; margin: 2 auto; float: left; clear: right; text-align: left; padding: 8px 0px 8px 20px;">
                                        <font style="color: Red">*</font>考试计划公开范围：<asp:RadioButton ID="RadioButtonPublish"
                                            runat="server" Text="完全公开" GroupName="g1" AutoPostBack="true" OnCheckedChanged="RadioButtonPublish_OnCheckedChanged" />&nbsp;
                                                <asp:RadioButton ID="RadioButtonTrainUnitLimit" runat="server" Text="完全公开（但培训点部分可见）"
                                                    GroupName="g1" AutoPostBack="true" OnCheckedChanged="RadioButtonPublish_OnCheckedChanged" />&nbsp;<asp:RadioButton
                                                        ID="RadioButtonUnPublish" runat="server" Text="部分公开（在以下选择可见用户）" GroupName="g1"
                                                        AutoPostBack="true" OnCheckedChanged="RadioButtonPublish_OnCheckedChanged" />
                                    </div>
                                    <div style="width: 98%; margin: 2px auto; padding: 8px 0px 8px 8px; float: left;">
                                        <div style="width: 32%; margin: 0 auto; float: left; text-align: left;">
                                            <div style="line-height: 20px; font-weight: bold;">
                                                选择可见培训点:
                                            </div>
                                            <telerik:RadListBox ID="RadListBoxTrainUnit" runat="server" CheckBoxes="true" Width="290px"
                                                Skin="Windows7" Height="290px">
                                                <Items>
                                                </Items>
                                            </telerik:RadListBox>
                                        </div>
                                        <div style="width: 66%; margin: 0 auto; float: left; text-align: left; padding-left: 2px;">
                                            <div style="background-color: #F7F7F7; padding: 20px 20px">
                                                <div style="margin: 4px 0">
                                                    <b>可见企业组织机构代码：</b>
                                                    <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="100px" MaxLength="9"
                                                        Skin="Default">
                                                    </telerik:RadTextBox>
                                                    <asp:Button ID="ButtonAddUnitCode" CssClass="button" Text="添 加" runat="server" CausesValidation="False"
                                                        CommandName="AddUnitCode"></asp:Button>&nbsp;&nbsp;<asp:Button ID="ButtonDeleteUnitCode"
                                                            CssClass="button" Text="删 除" runat="server" CausesValidation="False" CommandName="DeleteUnitCode"
                                                            ToolTip="删除选中的已添加组织机构代码"></asp:Button>
                                                </div>
                                                <div style="width: 100%; margin: 0 auto; float: left; clear: right;">
                                                    <telerik:RadListBox ID="RadListBoxUnitCode" runat="server" CheckBoxes="false" Width="400px"
                                                        Height="125px">
                                                        <Items>
                                                        </Items>
                                                    </telerik:RadListBox>
                                                </div>
                                                <div style="clear: both;"></div>
                                            </div>
                                            <div style="background-color: #F7F7F7; margin-top: 12px; padding: 20px 20px;">
                                                <div style="margin: 4px 0">
                                                    <b>可见从业人员身份证号：</b>
                                                    <telerik:RadTextBox ID="RadTextBoxIDCard" runat="server" Width="200px" MaxLength="18000" TextMode="MultiLine" Rows="3"
                                                        Skin="Default">
                                                    </telerik:RadTextBox>（可多行）
                                                    <asp:Button ID="ButtonAddIDCard" CssClass="button" Text="添 加" runat="server" CausesValidation="False"
                                                        CommandName="AddIDCard" ToolTip="每行一个身份证号，一次可添加多行"></asp:Button>&nbsp;&nbsp;<asp:Button ID="ButtonDeleteIDCard"
                                                            CssClass="button" Text="删 除" runat="server" CausesValidation="False" CommandName="DeleteIDCard"
                                                            ToolTip="删除选中的已添加身份证号"></asp:Button>
                                                    &nbsp;&nbsp;<asp:Button ID="ButtonClearIDCard"
                                                        CssClass="button" Text="清 空" runat="server" CausesValidation="False" CommandName="ClearIDCard"
                                                        ToolTip="删除所有已添加身份证号"></asp:Button>
                                                </div>
                                                <div style="width: 100%; margin: 0 auto; float: left;">
                                                    <telerik:RadListBox ID="RadListBoxIDCard" runat="server" CheckBoxes="false" Width="400px"
                                                        Height="125px">
                                                        <Items>
                                                        </Items>
                                                    </telerik:RadListBox>
                                                </div>
                                                <div style="clear: both; color: red">
                                                    <asp:Label ID="LabelIDCardCount" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <table width="100%">
                                        <tr>
                                            <td style="text-align: center">
                                                <div style="margin: 20px 0px 30px 0px; width: 80%">
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
            </div>

        </div>
    </div>
</asp:Content>
