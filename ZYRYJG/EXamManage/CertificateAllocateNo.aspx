<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertificateAllocateNo.aspx.cs" Inherits="ZYRYJG.EXamManage.CertificateAllocateNo" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../ExamPlanSelect.ascx" TagName="ExamPlanSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ButtonOutput") >= 0 || args.get_eventTarget().indexOf("ButtonOutPutAddItem") >= 0) {
                    args.set_enableAjax(false);
                }
            }
            function openCheckWin() {
                var divBase = document.getElementById("<%=DivBase.ClientID %>");
                var divPop = document.getElementById("<%=TableTime.ClientID %>");
                divBase.style.display = "none";
                divPop.style.display = "block";
            }
            function Cancel() {
                var divBase = document.getElementById("<%=DivBase.ClientID %>");
                var divPop = document.getElementById("<%=TableTime.ClientID %>");
                divBase.style.display = "block";
                divPop.style.display = "none";
            }
            function check() {
                var calendarStart = $find("<%= RadDatePicker1.ClientID %>");
                var startDate = calendarStart.get_textBox().value;
                var calendarEnd = $find("<%= RadDatePicker2.ClientID %>");
                var endDate = calendarEnd.get_textBox().value;
                var calendarConferDate = $find("<%= RadDatePickerConferDate.ClientID %>");
                var ConferDate = calendarConferDate.get_textBox().value;

                var startDate1 = startDate.replace(/-/g, "/");
                var endDate1 = endDate.replace(/-/g, "/");
                if (Date.parse(startDate1) - Date.parse(endDate1) > 0) {
                    alert("有效期起始时间不能大于有效期截止时间!");
                    return false;
                }
                return true;
            }
        </script>
    </telerik:RadCodeBlock>
    <%--    <asp:HiddenField ID="HiddenFieldStartTime" runat="server" />
    <asp:HiddenField ID="HiddenFieldEndTime" runat="server" />
    <asp:HiddenField ID="HiddenFieldConferDate" runat="server" />--%>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="DivBase">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DivBase" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="TableTime" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="TableTime">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DivBase" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="TableTime" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        runat="server" Skin="Windows7" EnableShadow="true">
        <Windows>
            <telerik:RadWindow ID="RadWindow1" runat="server" OnClientClose="OnClientClose">
            </telerik:RadWindow>
       
        </Windows>
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                证书制作 &gt;&gt; <strong>证书编号</strong>
            </div>
        </div>
        <div id="DivBase" class="content" runat="server" style="width: 100%; display: block;">
            <table class="bar_cx">
                <tr>
                    <td align="right" nowrap="nowrap" width="10%">考试名称：
                    </td>
                    <td align="left" width="70%">
                        <uc1:ExamPlanSelect ID="ExamPlanSelect1" runat="server" />
                    </td>
                    <td align="left" width="10%">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>
            <div class="table_cx">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                未编号列表
            </div>
            <div style="width: 95%; margin: 0 auto;" runat="server" id="DivMain">
                <telerik:RadGrid ID="RadGridExamResult" AutoGenerateColumns="False" runat="server"
                    AllowPaging="True" AllowCustomPaging="true" PageSize="10" AllowSorting="True"
                    SortingSettings-SortToolTip="单击进行排序" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false" Width="100%" GridLines="None" OnDataBound="RadGridExamResult_DataBound">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="考生姓名">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TrainUnitName" DataField="TrainUnitName" HeaderText="报名点名称">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别"
                                Visible="false">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SkillLevel" DataField="SkillLevel" HeaderText="技术等级或职称">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </telerik:GridBoundColumn>
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
                <asp:ObjectDataSource ID="ObjectDataSourceExamResult" runat="server" TypeName="DataAccess.ExamResultDAL"
                    DataObjectTypeName="Model.CertificateOB" SelectMethod="GetListView_ExamScore"
                    EnablePaging="true" SelectCountMethod="SelectCountView_ExamScore" MaximumRowsParameterName="maximumRows"
                    StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <div style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                    <input id="BtnBH" type="button" value="证书编号" onclick="openCheckWin(); return false;"
                        class="bt_large" runat="server" />
                </div>
            </div>
            <div class="table_cx">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                已编号列表
            </div>
            <div style="width: 95%; margin: 0 auto;" runat="server" id="Div1">
                <telerik:RadGrid ID="RadGridCertificate" AutoGenerateColumns="False" runat="server"
                    AllowPaging="True" AllowCustomPaging="true" PageSize="10" AllowSorting="True"
                    SortingSettings-SortToolTip="单击进行排序" Skin="Blue" EnableAjaxSkinRendering="false"
                    OnDataBound="RadGridCertificate_DataBound" EnableEmbeddedSkins="false" Width="100%"
                    GridLines="None" OnExcelExportCellFormatting="RadGridCertificate_ExcelExportCellFormatting">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Width="8%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="考生姓名">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TrainUnitName" DataField="TrainUnitName" HeaderText="报名点名称">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Left" Width="10%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别"
                                Visible="false">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                HeaderText="证书编号">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="15%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ConferDate" DataField="ConferDate" HeaderText="发证时间"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ValidEndDate" DataField="ValidEndDate" HeaderText="有效期至"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SkillLevel" DataField="SkillLevel" HeaderText="技术等级或职称">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="10%" />
                            </telerik:GridBoundColumn>
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
                <asp:ObjectDataSource ID="ObjectDataSourceCertificate" runat="server" TypeName="DataAccess.CertificateDAL"
                    DataObjectTypeName="Model.CertificateOB" SelectMethod="GetList" EnablePaging="true"
                    SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <div style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                    <asp:Button ID="ButtonOutput" runat="server" Text="导出列表" CssClass="bt_large" OnClick="ButtonOutput_Click" />
                </div>
            </div>
            <div id="div_additem" runat="server" style="display: none;">
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    增项列表
                </div>
                <div style="width: 95%; margin: 0 auto;" runat="server" id="Div2">
                    <telerik:RadGrid ID="RadGridAddItem" AutoGenerateColumns="False" runat="server" AllowPaging="True"
                        AllowCustomPaging="true" PageSize="10" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序"
                        OnDataBound="RadGridAddItem_DataBound" Skin="Blue" EnableAjaxSkinRendering="false"
                        EnableEmbeddedSkins="false" Width="100%" OnExcelExportCellFormatting="RadGridAddItem_ExcelExportCellFormatting"
                        GridLines="None">
                        <ClientSettings EnableRowHoverStyle="true">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="　没有可显示的记录">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Width="8%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="考生姓名">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="TrainUnitName" DataField="TrainUnitName" HeaderText="报名点名称">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Left" Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别"
                                    Visible="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                                    HeaderText="证件号码">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                    HeaderText="证书编号">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="15%" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ConferDate" DataField="ConferDate" HeaderText="发证时间"
                                    HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ValidEndDate" DataField="ValidEndDate" HeaderText="有效期至"
                                    HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="SkillLevel" DataField="SkillLevel" HeaderText="技术等级或职称">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                </telerik:GridBoundColumn>
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
                    <asp:ObjectDataSource ID="ObjectDataSourceAddItem" runat="server" TypeName="DataAccess.CertificateDAL"
                        DataObjectTypeName="Model.CertificateOB" SelectMethod="GetList" EnablePaging="true"
                        SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                        SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <div style="width: 90%; margin: 5px auto; text-align: center; padding-top: 8px;">
                        <asp:Button ID="ButtonOutPutAddItem" runat="server" Text="导出列表" CssClass="bt_large"
                            OnClick="ButtonOutPutAddItem_Click" />
                    </div>
                </div>
            </div>
        </div>
        <table id="TableTime" runat="server" style="line-height: 30px; width: 98%; display: none; margin-top: 30px;">
            <tr>
                <td style="width: 50%" align="right">证书发证时间：
                </td>
                <td align="left">
                    <telerik:RadDatePicker ID="RadDatePickerConferDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                        Width="120px" />
                </td>
            </tr>
            <tr>
                <td align="right">有效期起始时间：
                </td>
                <td align="left">
                    <telerik:RadDatePicker ID="RadDatePicker1" MinDate="01/01/1900" runat="server" Width="120px"  Calendar-DayCellToolTipFormat="yyyy年MM月dd日"/>
                </td>
            </tr>
            <tr>
                <td align="right">有效期截止时间：
                </td>
                <td align="left">
                    <telerik:RadDatePicker ID="RadDatePicker2" MinDate="01/01/1900" runat="server" Width="120px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日" />
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="padding: 20px 0px 100px 0px; text-align: center">
                    <asp:Button ID="ButtonOK" runat="server" Text="确 定" CssClass="button"
                        OnClientClick="javascript:if(check()==false) return false;" OnClick="ZSBH" />
                    <input id="Button1" type="button" value="取 消" class="button" onclick="javascript: Cancel();" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
