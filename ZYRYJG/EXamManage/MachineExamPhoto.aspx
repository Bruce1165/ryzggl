<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true" CodeBehind="MachineExamPhoto.aspx.cs" Inherits="ZYRYJG.EXamManage.MachineExamPhoto" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxManager runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" RequestQueueSize="1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="PostSelect2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PostSelect2" />
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
                考场管理 &gt;&gt; <strong>上机考试照片管理</strong>
            </div>
        </div>
        <div class="content">
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
                            <asp:ListItem Value="1" Selected="True">未考试</asp:ListItem>
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
                考试计划列表（注意：发起生成下载任务后需要1分钟左右时间等待系统准备下载资源，请重新查询后点击下载链接或鼠标右键另存下载）
            </div>
            <div id="DivExamPlan" runat="server" style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGridExamPlan" AutoGenerateColumns="False" PagerStyle-AlwaysVisible="true"
                    runat="server" AllowPaging="True" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序"
                    Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" Width="100%" OnItemDataBound="RadGridExamPlan_ItemDataBound" OnItemCommand="RadGridExamPlan_ItemCommand"
                    GridLines="None">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView  CommandItemDisplay="None" DataKeyNames="ExamPlanID,PostName,ExamStartDate"
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
                            <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>                         
                            <telerik:GridTemplateColumn HeaderText="考试时间">
                                <ItemTemplate>
                                    <nobr><%# Convert.ToDateTime(Eval("ExamStartDate")).CompareTo(Convert.ToDateTime(Eval("ExamEndDate")))==0?Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd"):Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd") + " - " + Convert.ToDateTime(Eval("ExamEndDate")).ToString("yyyy.MM.dd")%></nobr>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                             <telerik:GridBoundColumn UniqueName="JWPassCount" DataField="JWPassCount" HeaderText="审核通过人数">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                           <telerik:GridTemplateColumn UniqueName="photo" HeaderText="一寸照片">
                                    <ItemTemplate>
                                        <asp:Button ID="ButtonCreate" runat="server" Text="生成下载任务" CommandName="down" CommandArgument='<%#Eval("ExamPlanID")%>' CssClass="bt_large"  />
                                        <asp:HyperLink ID="HyperLink1" runat="server"></asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>           
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
                    DataObjectTypeName="Model.ExamPlanOB" SelectMethod="GetListSingupCount"  SelectCountMethod="SelectSingupCount"  EnablePaging="true"
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
     <div id="winpop">
        </div>

</asp:Content>
