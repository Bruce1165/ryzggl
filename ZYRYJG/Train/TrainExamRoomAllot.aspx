<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="TrainExamRoomAllot.aspx.cs" Inherits="ZYRYJG.Train.TrainExamRoomAllot" %>

<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc3" %>
<%@ Register Src="~/myhelp.ascx" TagPrefix="uc4" TagName="myhelp" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="tableMain">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tableMain" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        Skin="Windows7" VisibleStatusbar="false" EnableShadow="true" EnableEmbeddedScripts="true"
        OnClientClose="OnClientClose">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 培训点业务 &gt;&gt; <strong>准考证管理</strong>
            </div>
             <uc4:myhelp ID="myhelp1" runat="server" PageID="执业技能准考证管理.htm" />
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
                    <td colspan="2" >
                        <table cellpadding="2" cellspacing="0" border="0" width="100%">
                            <tr>
                                <td width="100px" align="right" nowrap="nowrap">岗位工种：</td>
                                <td align="left" width="300px"> <uc3:PostSelect ID="PostSelect2" runat="server" /></td>
                                <td width="100px" align="right" nowrap="nowrap">考试时间：</td>
                                <td align="left" width="200px" >                                   
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
                                     <td align="left" style="padding-left:40px" >  
                                     <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="bt_large" OnClick="ButtonSearch_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="5" style="padding-top:8px">
                                    <telerik:RadGrid ID="RadGridExamPlan" AutoGenerateColumns="False" PagerStyle-AlwaysVisible="true"
                                        runat="server" AllowPaging="True" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序" PageSize="15"
                                        Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" Width="100%"
                                        GridLines="None"  OnDeleteCommand="RadGridExamPlan_DeleteCommand" >
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
                <tr>
                    <td valign="top" colspan="2">
                        <div style="float: right; width: 100%">                         
                            <div class="table_cx">
                                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                                考场分配情况<asp:Label ID="LabelAllotHelp" runat="server" Text="" ForeColor="#ff3300"></asp:Label>
                            </div>
                            <telerik:RadGrid ID="RadGridExamPlaceAllot" DataSourceID="ObjectDataSourceExamPlaceAllot" Skin="Blue" EnableAjaxSkinRendering="false" AllowMultiRowSelection="true"
                                EnableEmbeddedSkins="false"
                                runat="server" GridLines="None" AllowPaging="True" PageSize="5" AutoGenerateColumns="False"
                                AllowAutomaticDeletes="true" Width="98%" OnItemDataBound="RadGridExamPlaceAllot_ItemDataBound"
                                OnUpdateCommand="RadGridExamPlaceAllot_UpdateCommand" OnInsertCommand="RadGridExamPlaceAllot_InsertCommand"
                                OnDetailTableDataBind="RadGridExamPlaceAllot_DetailTableDataBind" OnItemCommand="RadGridExamPlaceAllot_ItemCommand">
                                <ClientSettings EnableRowHoverStyle="true">
                                    <Selecting AllowRowSelect="true" />
                                </ClientSettings>
                                <MasterTableView CommandItemDisplay="None" DataKeyNames="ExamPlaceAllotID,ExamPlaceID,ExamPlanID,Status"
                                    NoMasterRecordsText="　没有可显示的记录" Name="ExamPlaceAllot" HierarchyDefaultExpanded="true">
                                    <DetailTables>
                                        <telerik:GridTableView Name="ExamRoomAllot" DataSourceID="ObjectDataSourceExamRoomAllot"
                                            runat="server" GridLines="None" AllowPaging="True" PageSize="10000" AutoGenerateColumns="False"
                                            Width="100%" CommandItemDisplay="Top" EditMode="EditForms"
                                            DataKeyNames="ExamRoomAllotID,ExamPlaceID,PersonNumber,Status" NoDetailRecordsText="　没有可显示的记录">
                                            <ParentTableRelation>
                                                <telerik:GridRelationFields DetailKeyField="ExamPlaceID,ExamPlanID" MasterKeyField="ExamPlaceID,ExamPlanID" />
                                            </ParentTableRelation>
                                            <Columns>
                                                <telerik:GridBoundColumn UniqueName="ExamRoomCode" DataField="ExamRoomCode" HeaderText="考场号">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn UniqueName="ExamStartTime" HeaderText="考试时间">
                                                    <ItemTemplate>
                                                        <nobr><%# Convert.ToDateTime(Eval("ExamStartTime")).ToString("yyyy-MM-dd")==Convert.ToDateTime(Eval("ExamEndTime")).ToString("yyyy-MM-dd")?string.Format("{0} - {1}",Convert.ToDateTime(Eval("ExamStartTime")).ToString("yyyy.MM.dd日 HH:mm"),Convert.ToDateTime(Eval("ExamEndTime")).ToString("HH:mm时")):string.Format("{0} - {1}",Convert.ToDateTime(Eval("ExamStartTime")).ToString("yyyy.MM.dd日 HH:mm时"),Convert.ToDateTime(Eval("ExamEndTime")).ToString("yyyy.MM.dd日 HH:mm时"))%></nobr>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn UniqueName="PersonNumber" DataField="PersonNumber" HeaderText="考场人数">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="ExamCardIDFromTo" DataField="ExamCardIDFromTo"
                                                    HeaderText="准考证号范围">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridEditCommandColumn UniqueName="Edit" EditText="编辑">
                                                    <HeaderStyle HorizontalAlign="Center" Width="36px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="36px" />
                                                </telerik:GridEditCommandColumn>
                                                <telerik:GridButtonColumn UniqueName="Delete" CommandName="ButtonDelete" Text="&lt;span onclick=&quot;if(!confirm('您确定要删除么?'))return false; &quot; &gt;删除&lt;/span&gt;">
                                                    <HeaderStyle HorizontalAlign="Center" Width="36px" />
                                                    <ItemStyle HorizontalAlign="Center" Width="36px" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                            <CommandItemSettings AddNewRecordText="添加考场" ShowRefreshButton="false" />
                                            <EditFormSettings InsertCaption="添加考场" CaptionFormatString="编辑考场{0}信息 " CaptionDataField="ExamRoomCode"
                                                EditFormType="Template" PopUpSettings-Modal="true">
                                                <EditColumn UniqueName="EditCommandColumn1">
                                                </EditColumn>
                                                <FormTemplate>
                                                    <contenttemplate>
                                                <br />   
                                                        <table id="Table1" cellspacing="1" cellpadding="5" width="100%" border="0" style="padding-top: 20px;margin-left:20px">
                                                            <tr>
                                                                <td align="right">
                                                                    <span style="color: Red">* </span>考场号：
                                                                </td>
                                                                <td align="left">
                                                                   <telerik:RadNumericTextBox ID="RadNumericTextBoxExamRoomCode" runat="server" MaxLength="3"  Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true"  MinValue="0" Width="100px" />                                                               
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorExamRoomCode" runat="server" ErrorMessage="必填" Display="Dynamic" ControlToValidate="RadNumericTextBoxExamRoomCode"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    <span style="color: Red">* </span>考场人数：
                                                                </td>
                                                                <td align="left">
                                                                   <telerik:RadNumericTextBox ID="RadNumericTextBoxPersonNumber" runat="server" MaxLength="5"  Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="100px"  MinValue="0" />
                                                                   <asp:RequiredFieldValidator ID="RequiredFieldValidatorPersonNumber" runat="server" ErrorMessage="必填" Display="Dynamic" ControlToValidate="RadNumericTextBoxPersonNumber"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>
                                                             <tr>
                                                               <td align="right">
                                                                    <span style="color: Red">* </span>考试开始时间：
                                                                </td>
                                                                <td align="left">
                                                                    <telerik:RadDateTimePicker ID="pickerExamStartTime" runat="server" DbSelectedDate='<%# Eval("ExamEndTime")==DBNull.Value?null:Eval("ExamStartTime") %>'
                                                                        Width="200px"  style="float: left;">
                                                                        <TimeView runat="server" Skin="Default" ShowHeader="False" StartTime="08:00:00" Interval="00:30:00"
                                                                            EndTime="18:00:00" Columns="5" Width="100%">
                                                                        </TimeView>
                                                                    </telerik:RadDateTimePicker>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorExamStartTime" runat="server"  style="float: left;" ErrorMessage="必填" ControlToValidate="pickerExamStartTime" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                 </td>
                                                            </tr>
                                                            <tr> 
                                                                <td align="right">
                                                                    <span style="color: Red">* </span>考试结束时间：
                                                                </td>
                                                                <td align="left">
                                                                    <telerik:RadDateTimePicker ID="pickerExamEndTime"  runat="server" DbSelectedDate='<%# Eval("ExamEndTime")==DBNull.Value?null:Eval("ExamEndTime") %>'
                                                                        Width="200px"  style="float: left;">
                                                                        <TimeView runat="server" Skin="Default" ShowHeader="False" StartTime="08:00:00" Interval="00:30:00" EndTime="19:00:00" Columns="5" Width="100%">
                                                                        </TimeView>
                                                                    </telerik:RadDateTimePicker>
                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorExamEndTime" runat="server" style="float: left;"
                                                                        ErrorMessage="必填" ControlToValidate="pickerExamEndTime" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                </td>
                                                            </tr>   
                                                        </table>                                       
                                                 </contenttemplate>
                                                    <table style="width: 100%; padding: 10px 10px;">
                                                        <tr>
                                                            <td align="center" colspan="2">
                                                                <asp:Button ID="Button1" CssClass="button" Text='<%# (Container is GridEditFormInsertItem) ? "保存" : "更新" %>'
                                                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                                                    <asp:Button ID="Button2" CssClass="button" Text="取消" runat="server" CausesValidation="False"
                                                                        CommandName="Cancel"></asp:Button>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </FormTemplate>
                                                <PopUpSettings Modal="True" Width="95%"></PopUpSettings>
                                            </EditFormSettings>
                                            <PagerTemplate>
                                                <uc2:GridPagerTemple ID="GridPagerTemple3" runat="server" />
                                            </PagerTemplate>
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <ExpandCollapseColumn Visible="True">
                                    </ExpandCollapseColumn>
                                    <Columns>
                                        <telerik:GridClientSelectColumn>
                                            <HeaderStyle HorizontalAlign="Center" Width="10px" />
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                        </telerik:GridClientSelectColumn>
                                        <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Width="36px" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ExamPlaceName" DataField="ExamPlaceName" HeaderText="考点名称">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="RoomNum" DataField="RoomNum" HeaderText="使用考场数量">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ExamPersonNum" DataField="ExamPersonNum" HeaderText="考生人数">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridEditCommandColumn UniqueName="Edit" EditText="批量分配考场">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridEditCommandColumn>
                                    </Columns>
                                    <EditFormSettings CaptionFormatString="分配考场: {0}" CaptionDataField="ExamPlanName"
                                        EditFormType="Template" PopUpSettings-Modal="true">
                                        <EditColumn UniqueName="EditCommandColumn1">
                                        </EditColumn>
                                        <FormTemplate>
                                            <contenttemplate>
                                              <table id="Table1" style="padding-top: 10px; min-height:300px;margin-left:20px" >
                                                            <tr>
                                                                <td align="right">
                                                                    考点名称：
                                                                </td>
                                                                <td align="left">
                                                                    <telerik:RadTextBox ID="RadTextBoxExamPlaceName" runat="server" Width="90%" Skin="Default">
                                                                    </telerik:RadTextBox>
                                                                </td>
                                                            </tr>
                                                              <tr>
                                                                <td align="right">
                                                                    使用考场数量：
                                                                </td>
                                                                <td align="left">
                                                                   <telerik:RadNumericTextBox ID="RadNumericTextBoxRoomNum" runat="server"  MinValue="1" MaxLength="5"  Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="100px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    容纳总人数：
                                                                </td>
                                                                <td align="left">
                                                                   <telerik:RadNumericTextBox ID="RadNumericTextBoxExamPersonNum"  MinValue="1" runat="server" MaxLength="5"  Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="100px" />
                                                                   （使用考场数量总容纳人数）
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right">
                                                                    起始考场号：
                                                                </td>
                                                                <td align="left">
                                                                   <telerik:RadNumericTextBox ID="RadNumericTextBoxFirstRoomCode"  MinValue="1" runat="server" MaxLength="5"  Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="100px" Value="1" />
                                                                （考场从这个号码开始依次分配）
                                                                </td>
                                                            </tr>
                                                   <tr>
                                                                <td align="right">
                                                                    考试开始时间：
                                                                </td>
                                                                <td align="left">
                                                                   <telerik:RadDateTimePicker ID="pickerExamStartTime"  runat="server" 
                                                                        Width="200px"  style="float: left;">
                                                                        <TimeView runat="server" Skin="Default" ShowHeader="False" StartTime="08:00:00" Interval="00:30:00"
                                                                            EndTime="19:00:00" Columns="5" Width="100%">
                                                                        </TimeView>
                                                                    </telerik:RadDateTimePicker>
                                                                  
                                                                </td>
                                                                <td align="left">   
                                                                                                                                  
                                                                </td>
                                                            </tr>
                                                   <tr>
                                                                <td align="right">
                                                                    考试结束时间：
                                                                </td>
                                                                <td align="left">
                                                                    <telerik:RadDateTimePicker ID="pickerExamEndTime"  runat="server" 
                                                                        Width="200px"  style="float: left;">
                                                                        <TimeView runat="server" Skin="Default" ShowHeader="False" StartTime="08:00:00" Interval="00:30:00"
                                                                            EndTime="19:00:00" Columns="5" Width="100%">
                                                                        </TimeView>
                                                                    </telerik:RadDateTimePicker>
                                                                </td>
                                                                <td align="left">   
                                                                                                                                  
                                                                </td>
                                                            </tr>
                                                        </table>    
                                                                                                      
                                            </contenttemplate>

                                            <table style="width: 100%; padding: 10px 10px;">
                                                <tr>
                                                    <td align="center" colspan="2">
                                                        <asp:Button ID="Button1" CssClass="bt_large" Text='<%# (Container is GridEditFormInsertItem) ? "保存" : "分配考场" %>'
                                                            runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                            OnClientClick="if (!confirm('如果已分配过考场，此操作将覆盖以前的数据，是否继续？')) return false;" CommandArgument="分配考场"></asp:Button>
                                                        &nbsp;
                                                            <asp:Button ID="Button3" CssClass="button" Text="追加考场" runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'
                                                                CommandArgument="追加考场"></asp:Button>
                                                        &nbsp;&nbsp;
                                                            <asp:Button ID="Button2" CssClass="button" Text="取消" runat="server" CausesValidation="False"
                                                                CommandName="Cancel"></asp:Button>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; color: darkblue">提示：【分配考场】将覆盖原有分配结果，全新分配。【追加考场】保留原有分配结果，增加新的分配场次。
                                                    </td>
                                                </tr>
                                            </table>
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
                                    <HeaderStyle Font-Bold="True" />
                                    <PagerTemplate>
                                        <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                    </PagerTemplate>
                                </MasterTableView>
                            </telerik:RadGrid>
                            <asp:ObjectDataSource ID="ObjectDataSourceExamPlaceAllot" runat="server" SelectCountMethod="SelectCount"
                                EnablePaging="True" SortParameterName="orderBy" TypeName="DataAccess.ExamPlaceAllotDAL"
                                SelectMethod="GetList" UpdateMethod="Update" DataObjectTypeName="Model.ExamPlaceAllotOB">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                        DefaultValue="" ConvertEmptyStringToNull="false" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="ObjectDataSourceExamRoomAllot" runat="server" SelectCountMethod="SelectCount"
                                EnablePaging="True" SortParameterName="orderBy" TypeName="DataAccess.ExamRoomAllotDAL"
                                SelectMethod="GetList" UpdateMethod="Update" DeleteMethod="Delete" DataObjectTypeName="Model.ExamRoomAllotOB">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                        DefaultValue="" ConvertEmptyStringToNull="false" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <div style="padding: 8px 0px;">
                                <asp:Button ID="ButtonCreateExamCard" runat="server" Text="准考证放号" CssClass="bt_large"
                                    OnClick="ButtonCreateExamCard_Click" />&nbsp;&nbsp;
                                         <asp:Button ID="ButtonDeleteeExamCard" runat="server" Text="取消准考证放号" CssClass="bt_maxlarge" OnClientClick="if(!confirm('您确定要取消准考证放号?'))return false;"
                                             OnClick="ButtonDeleteeExamCard_Click" />
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
