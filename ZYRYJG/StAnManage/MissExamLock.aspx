<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="MissExamLock.aspx.cs" Inherits="ZYRYJG.StAnManage.MissExamLock" %>

<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        runat="server" Skin="Windows7" EnableShadow="true" >
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamResult" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGridExamResult">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamResult" />
                </UpdatedControls>
            </telerik:AjaxSetting>         
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                考试管理 &gt;&gt; <strong>缺考锁定记录</strong>
            </div>
        </div>
        <div class="content">
            <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                锁定说明
            </div>
            <div class="DivContent" id="Td3">
                从第一次缺考开始计算,一年内累计缺考三次的人员自动计入，从第三次开始锁定，为期一年。<br />
 
            </div>
            <table class="bar_cx">
                 <tr>
                    <td width="11%" align="right" nowrap="nowrap">考生姓名：
                    </td>
                    <td width="39%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Skin="Default"
                            Width="97%">
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">证件号码：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" 
                            Skin="Default" Width="97%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">锁定日期：
                    </td>
                    <td width="39%" align="left">
                        <telerik:RadDatePicker ID="RadDatePicker_ExamStartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="RadDatePicker_ExamEndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                    </td>
                     <td align="right" nowrap="nowrap">锁定状态：
                    </td>
                    <td width="39%" align="left">
                        <asp:RadioButtonList ID="RadioButtonListLockSatatus" runat="server" RepeatDirection="Horizontal" Width="250px">
                            <asp:ListItem Text="全部" Value=""></asp:ListItem>
                            <asp:ListItem Text="锁定中" Value="锁定中" Selected="True"></asp:ListItem>
                             <asp:ListItem Text="已解锁" Value="已解锁"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
              <tr>
                    <td align="center"  colspan="4" style="padding-top:12px">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                  </tr>
            </table>
            <div class="table_cx">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                缺考锁定记录
            </div>
            <div style="width: 98%; margin: 0 auto;" runat="server" id="DivMain">
                <telerik:RadGrid ID="RadGridExamResult" runat="server" GridLines="None" AllowPaging="True"  PagerStyle-AlwaysVisible="true"
                    AllowCustomPaging="true" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false" OnDataBound="RadGridExamResult_DataBound" OnExcelExportCellFormatting="RadGridExamResult_ExcelExportCellFormatting">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView DataKeyNames="" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>                          
                            <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode" HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="LockStartDate" DataField="LockStartDate" HeaderText="锁定日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn UniqueName="LockEndDate" DataField="LockEndDate" HeaderText="解锁日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="详细">
                                    <ItemTemplate>
                                        <span class="link_edit" onclick=' javascript:SetIfrmSrc("MissExamDetail.aspx?o1=<%# Utility.Cryptography.Encrypt(Eval("WorkerCertificateCode").ToString()) %>&o2=<%# Utility.Cryptography.Encrypt(System.Convert.ToDateTime(Eval("LockStartDate")).ToString("yyyy-MM-dd")) %>"); '>详细</span>
                                        </ItemTemplate>
                                </telerik:GridTemplateColumn>
                        </Columns>
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.TJ_MissExamLockDAL"
                    DataObjectTypeName="Model.TJ_MissExamLockMDL" SelectMethod="GetList" EnablePaging="true"
                    SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows"
                    StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                             ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <div style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                    <asp:Button ID="ButtonPrint" runat="server" Text="导出Excel" CssClass="bt_large" OnClick="ButtonPrint_Click"
                        Enabled="False" />
                </div>
            </div>
        </div>
    </div>
     <uc1:IframeView ID="IframeView" runat="server" />
        <div id="winpop">
        </div>
</asp:Content>
