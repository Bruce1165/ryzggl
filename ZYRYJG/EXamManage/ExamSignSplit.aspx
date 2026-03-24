<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamSignSplit.aspx.cs"
    MasterPageFile="~/RadControls.Master" Inherits="ZYRYJG.EXamManage.ExamSignSplit" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../ExamPlanSelect.ascx" TagName="ExamPlanSelect" TagPrefix="uc1" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            //function onRequestStart(sender, args) {
            //}
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <%--  <ClientEvents OnRequestStart="onRequestStart" />--%>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="divmain">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="divmain" LoadingPanelID="RadAjaxLoadingPanel1" />
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
                报名管理 &gt;&gt; <strong>考试计划拆分</strong>
            </div>
        </div>
        <div id="divmain" runat="server" class="content">
            <div class="jbxxbt">
                考试计划拆分
            </div>

            <div class="table_cx">
                原考试计划：<asp:Label ID="LabelOldExamPlan" runat="server" Text=""></asp:Label>
            </div>
            <div class="table_cx" style="padding-bottom: 8px;">
                <img alt="" src="../Images/1034.gif" width="11" height="11" style="margin-bottom: -2px; padding-right: 2px;" />
                筛选条件
            </div>
            <table class="bar_cx">
                <tr>
                    <td align="left">
                        <asp:RadioButtonList ID="RadioButtonListSearchBy" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListSearchBy_SelectedIndexChanged" Width="300px">
                            <asp:ListItem Text="按人数筛选" Value="0" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="按培训点筛选" Value="1" Selected="false"></asp:ListItem>
                        </asp:RadioButtonList>

                    </td>
                </tr>
                <tr id="tr_TrainUnit" runat="server" visible="false">

                    <td align="left">
                        <asp:CheckBoxList ID="CheckBoxListTrainUnit" runat="server" RepeatDirection="Horizontal" RepeatColumns="4" Width="100%"></asp:CheckBoxList>
                    </td>
                </tr>
                <tr id="tr_ManCount" runat="server" visible="true">
                    <td align="left">
                        <telerik:RadNumericTextBox ID="RadNumericTextBoxManCount" runat="server" MaxLength="5"
                            Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="200px" Label="请输入要拆分的人数："
                            Value="0">
                            <NumberFormat DecimalDigits="0"></NumberFormat>
                        </telerik:RadNumericTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="bt_large" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>

            <div id="DivList" runat="server">
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    原考试计划报名人员列表
                </div>
                <div style="width: 98%; margin: 0 auto; padding-bottom: 10px;">
                    <telerik:RadGrid ID="RadGridOld" runat="server" Width="100%"
                        PageSize="10" AllowSorting="True" AllowPaging="True" GridLines="None" AutoGenerateColumns="False"
                        Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false">
                        <ClientSettings EnableRowHoverStyle="false">
                        </ClientSettings>
                        <MasterTableView DataKeyNames="ExamSignUpID" NoMasterRecordsText="　没有可显示的记录">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="TrainUnitName" DataField="TrainUnitName" HeaderText="报名点（培训点）">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="单位名称">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                    HeaderText="证件号码">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="PostName" HeaderText="申报岗位工种">
                                    <ItemTemplate>
                                        <nobr><%# Eval("PostName")%><%# Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("暖通") ? "（暖通）":
                                                   Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("电气") ? "（电气）" : ""%></nobr>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="考试时间">
                                    <ItemTemplate>
                                        <nobr><%# Convert.ToDateTime(Eval("ExamStartDate")).CompareTo(Convert.ToDateTime(Eval("ExamEndDate")))==0?Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd"):Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd") + " - " + Convert.ToDateTime(Eval("ExamEndDate")).ToString("yyyy.MM.dd")%></nobr>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="Status" DataField="Status" HeaderText="报名状态">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                            <ItemStyle Height="12" />
                            <AlternatingItemStyle Height="12" />
                            <PagerTemplate>
                                <div style="width: 600px; text-align: left; float: left;">
                                    <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                </div>
                            </PagerTemplate>
                        </MasterTableView>
                        <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                        <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSourceOld" runat="server" DeleteMethod="Delete"
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
                    <asp:Button ID="ButtonMove" runat="server" Text="▼ 转移人员" CssClass="bt_large" OnClick="ButtonMove_Click" />
                </div>
                <table width="98%" border="0" align="center" cellspacing="1">
                    <tr>
                        <td align="right" width="11%" nowrap="nowrap">选择要拆分到的新考试计划：
                        </td>
                        <td align="left" colspan="3">
                            <uc1:ExamPlanSelect ID="ExamPlanSelectNew" runat="server" OnExamPlanSelectChange="ExamPlanSelectNew_Changed" />
                        </td>
                    </tr>
                </table>
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    新考试计划报名人员列表
                </div>
                <div style="width: 98%; margin: 0 auto; padding-bottom: 10px;">
                    <telerik:RadGrid ID="RadGridNew" runat="server" Width="100%"
                        PageSize="10" AllowSorting="True" AllowPaging="True" GridLines="None" AutoGenerateColumns="False"
                        Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false">
                        <ClientSettings EnableRowHoverStyle="false">
                        </ClientSettings>
                        <MasterTableView DataKeyNames="ExamSignUpID" NoMasterRecordsText="　没有可显示的记录">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="TrainUnitName" DataField="TrainUnitName" HeaderText="报名点（培训点）">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="单位名称">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                    HeaderText="证件号码">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="PostName" HeaderText="申报岗位工种">
                                    <ItemTemplate>
                                        <nobr><%# Eval("PostName")%><%# Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("暖通") ? "（暖通）":
                                                   Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("电气") ? "（电气）" : ""%></nobr>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn HeaderText="考试时间">
                                    <ItemTemplate>
                                        <nobr><%# Convert.ToDateTime(Eval("ExamStartDate")).CompareTo(Convert.ToDateTime(Eval("ExamEndDate")))==0?Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd"):Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd") + " - " + Convert.ToDateTime(Eval("ExamEndDate")).ToString("yyyy.MM.dd")%></nobr>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="Status" DataField="Status" HeaderText="报名状态">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                            <ItemStyle Height="12" />
                            <AlternatingItemStyle Height="12" />
                            <PagerTemplate>
                                <div style="width: 600px; text-align: left; float: left;">
                                    <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                </div>
                            </PagerTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSourceNew" runat="server" DeleteMethod="Delete"
                        InsertMethod="Insert" SelectMethod="GetList_New" TypeName="DataAccess.ExamSignUpDAL"
                        UpdateMethod="Update" SelectCountMethod="SelectCount_New" EnablePaging="true"
                        MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                        SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
