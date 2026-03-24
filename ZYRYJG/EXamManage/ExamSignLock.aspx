<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamSignLock.aspx.cs"
    MasterPageFile="~/RadControls.Master" Inherits="ZYRYJG.EXamManage.ExamSignLock" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../ExamPlanSelect.ascx" TagName="ExamPlanSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc3" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {

                if (args.get_eventTarget().indexOf("ButtonExport") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DivList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ButtonExport" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" OnClientClose="OnClientClose"
        Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                报名管理 &gt;&gt; <strong>违规申报锁定管理</strong>
            </div>
        </div>      
        <div class="content">
            <table class="bar_cx">
                <tr>
                     <td width="11%" align="right" nowrap="nowrap">岗位工种：
                    </td>
                    <td width="39%" align="left">
                        <uc3:PostSelect ID="PostSelect2" runat="server" />
                    </td>
                    
                    <td align="right" width="11%" nowrap="nowrap">考试计划：
                    </td>
                    <td align="left" colspan="3">
                        <uc1:ExamPlanSelect ID="ExamPlanSelect1" runat="server" />
                    </td>
                </tr>
                
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">姓名：
                    </td>
                    <td align="left" width="38%">
                        <telerik:RadTextBox ID="RadTxtWorkerName" runat="server" Width="97%" Skin="Default"
                            >
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">证件号码：
                    </td>
                    <td align="left" width="38%">
                        <telerik:RadTextBox ID="RadTxtCertificateCode" runat="server" Width="95%" Skin="Default"
                            >
                        </telerik:RadTextBox>
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
                </tr>
                <tr>
                  
                    <td width="11%" align="right" nowrap="nowrap">单位名称：
                    </td>
                    <td align="left" width="38%">
                        <telerik:RadTextBox ID="RadTxtUnitName" runat="server" Width="95%" Skin="Default"
                            >
                        </telerik:RadTextBox>
                    </td>
                      <td width="11%" align="right" nowrap="nowrap">锁定状态：
                    </td>
                    <td width="39%" align="left">
                      
                     <asp:RadioButtonList ID="RadioButtonListLock" runat="server" RepeatDirection="Horizontal" Width="400">
                            <asp:ListItem Text="全部" Value="" Selected="True"></asp:ListItem>          
                            <asp:ListItem Text="锁定中" Value="锁定中"></asp:ListItem>
                            <asp:ListItem Text="已解锁" Value="已解锁"></asp:ListItem>                        
                        </asp:RadioButtonList>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="bt_large" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>
            <div id="DivList" runat="server">
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    考试报名列表
                </div>
                <div style="width: 98%; margin: 0 auto; padding-bottom: 10px;">
                    <telerik:RadGrid ID="RadGrid1" runat="server" Width="100%"  PagerStyle-AlwaysVisible="true"
                        PageSize="10" AllowSorting="True" AllowPaging="True" GridLines="None" AutoGenerateColumns="False"
                        Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false">
                        <ClientSettings EnableRowHoverStyle="true">
                            <Scrolling AllowScroll="true" ScrollHeight="400" />
                        </ClientSettings>
                        <MasterTableView DataKeyNames="ExamSignUpID" NoMasterRecordsText="　没有可显示的记录">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridTemplateColumn UniqueName="LockTime" HeaderText="" >
                                  <ItemTemplate>                                   
                                   <%# Eval("LockEndTime") != DBNull.Value && Convert.ToDateTime(Eval("LockEndTime")).AddDays(1)> DateTime.Now?string.Format("<img alt=\"\" src=\"../Images/s_lock.png\" title=\"违规申报锁定：{0}\" />",Eval("LockReason")):""%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                                  <telerik:GridTemplateColumn UniqueName="Detail" HeaderText="详细">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("ExamSignLockView.aspx?o=<%# Eval("ExamSignUpID")%>");'>详细</span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="考试时间">
                                    <ItemTemplate>
                                        <nobr><%# Convert.ToDateTime(Eval("ExamStartDate")).CompareTo(Convert.ToDateTime(Eval("ExamEndDate")))==0?Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd"):Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd") + " - " + Convert.ToDateTime(Eval("ExamEndDate")).ToString("yyyy.MM.dd")%></nobr>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="PostName" HeaderText="申报岗位工种">
                                    <ItemTemplate>
                                        <nobr><%# Eval("PostName")%><%# Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("暖通") ? "（暖通）":
                                                   Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("电气") ? "（电气）" : ""%></nobr>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>                             
                                <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                    HeaderText="证件号码">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="单位名称">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="LockTime" DataField="LockTime" HeaderText="锁定时间"
                                    HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>                            
                            
                                <telerik:GridBoundColumn UniqueName="LockEndTime" DataField="LockEndTime" HeaderText="解锁时间"
                                    HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="LockMan" DataField="LockMan" HeaderText="锁定人">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                            <ItemStyle Height="12" />
                            <AlternatingItemStyle Height="12" />
                            <PagerTemplate>
                                    <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                        <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                        <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete"
                        InsertMethod="Insert" SelectMethod="GetList_New" TypeName="DataAccess.ExamSignUpDAL"
                        UpdateMethod="Update" SelectCountMethod="SelectCount_New" EnablePaging="true"
                        MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                        SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                </div>
                <div style="width: 95%; margin: 0 auto; text-align: center; padding-bottom: 20px;">
                    <asp:Button ID="ButtonExport" runat="server" Text="导出查询结果" CssClass="bt_large" OnClick="ButtonExport_Click" />
                </div>
            </div>
        </div>
    </div>
      <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
