<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ReadManage.aspx.cs" Inherits="ZYRYJG.EXamManage.ReadManage" %>

<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../ExamPlanSelect.ascx" TagName="ExamPlanSelect" TagPrefix="uc1" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function validateRadUploadTaboe(source, arguments) {
                arguments.IsValid = getRadUpload('<%= RadUploadSignUpTable.ClientID %>').validateExtensions();
            }
        </script>

    </telerik:RadCodeBlock>

    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" OnClientClose="OnClientClose"
        VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamSubjectResult" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RadComboBoxPostTypeID" UpdatePanelRenderMode="Block" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGridExamSubjectResult">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamSubjectResult" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <%-- <telerik:AjaxSetting AjaxControlID="ButtonImportScore">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamSubjectResult" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                考试管理 &gt;&gt; <strong>阅卷管理</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx">
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">考试计划名称：
                    </td>
                    <td align="left" colspan="3">
                        <uc1:ExamPlanSelect ID="ExamPlanSelect1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">考生姓名：
                    </td>
                    <td width="39%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Skin="Default" Width="97%"
                            MaxLength="50" >
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">证件号码：
                    </td>
                    <td width="39%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Skin="Default"
                            Width="97%" MaxLength="50" >
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td width="11%" align="right" nowrap="nowrap">准考证号：
                    </td>
                    <td width="39%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxExamCardID" runat="server" Skin="Default" Width="97%"
                            MaxLength="50" >
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">考点名称：
                    </td>
                    <td width="39%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxExamPlaceName" runat="server" Width="50%" Skin="Default"
                            MaxLength="100" >
                        </telerik:RadTextBox>
                        &nbsp;&nbsp;考场号：
                                        <telerik:RadTextBox ID="RadTextBoxExamRoomCode" runat="server" Skin="Default" Width="20%" >
                                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">报名点：
                    </td>
                    <td width="39%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxTrainUnit" runat="server" Skin="Default" Width="97%"
                            MaxLength="100" >
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">企业名称：
                    </td>
                    <td width="39%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxUnit" runat="server" Skin="Default" Width="97%"
                            MaxLength="100" >
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">排序规则：
                    </td>
                    <td width="39%" align="left">
                        <asp:RadioButtonList ID="RadioButtonListSortBy" runat="server" RepeatDirection="Horizontal" Width="300px">
                            <asp:ListItem Text="按准考证号" Value="ExamCardID" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="按报名点" Value="TrainUnitName"></asp:ListItem>
                            <asp:ListItem Text="按单位名称" Value="UnitName"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="bt_large" OnClick="ButtonSearch_Click" />
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
            <div style="padding-left: 20px; text-align: left; padding-bottom: 20px;">
                <table style="line-height: 24px;">
                    <tr>
                        <td align="right">模板下载：
                        </td>
                        <td colspan="3" align="left">
                            <asp:LinkButton ID="LinkButtonDownLoadScoreTemplate" runat="server" OnClick="LinkButtonDownLoadScoreTemplate_Click"
                                Font-Underline="true" ForeColor="Blue" Style="cursor: pointer; line-height: 16px; height: 16px;"> <img alt="" style="border:none; height:14px;" src="../Images/xls.gif" />&nbsp;成绩导入模版</asp:LinkButton>
                            <span>（提示：请按考试计划名称查询后，再下载带有考生信息的模板，录入成绩后按科目分批导入）</span>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <nobr>考试科目：</nobr>
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
                            <nobr>成绩导入：</nobr>
                        </td>
                        <td align="left">
                            <div style="float: left; text-align: left;width:300px;">
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
