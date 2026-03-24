<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="LearnRecordManage.aspx.cs" Inherits="ZYRYJG.StAnManage.LearnRecordManage" %>

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
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
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
                当前位置 &gt;&gt; 统计分析 &gt;&gt;
                综合查询 &gt;&gt; <strong>三类人员续期学习情况</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx">
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">姓名：
                    </td>
                    <td width="39%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Skin="Default" Width="97%"
                            MaxLength="50" >
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">证件号码：
                    </td>
                    <td width="39%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxWorkerCertificateCode" runat="server" Skin="Default"
                            Width="97%" MaxLength="50" >
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td width="11%" align="right" nowrap="nowrap">注册证书号：
                    </td>
                    <td width="39%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Skin="Default"
                            Width="97%" MaxLength="50" >
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap"></td>
                    <td width="39%" align="left"></td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>
            <div class="table_cx" style="padding-top: 10px;">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                继续教育学时列表
            </div>
            <div style="overflow: auto;">
                <telerik:RadGrid ID="RadGridExamSubjectResult" AutoGenerateColumns="False" runat="server"
                    AllowCustomPaging="true" AllowPaging="True" PageSize="10" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false" Width="98%" GridLines="None" OnPageIndexChanged="RadGridExamSubjectResult_PageIndexChanged">
                    <ClientSettings EnableRowHoverStyle="false">
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="RecordID" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="RecordNo" DataField="RecordNo" HeaderText="编号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="专业">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                HeaderText="证书注册号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="LinkTel" DataField="LinkTel" HeaderText="联系手机">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ClassHour" DataField="ClassHour" HeaderText="学时">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Font-Bold="True" HorizontalAlign="Center" Wrap="false" />
                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
            </div>
            <br />
            <hr style="clear: both;" />
            <div style="font-size: 12px; float: left;">
                <div class="table_cx">
                    <img alt="" src="../Images/Soft_common.gif" style="margin-bottom: -2px; padding-right: 2px;" />
                    学习情况导入
                </div>
                <div style="padding-left: 20px">
                    <table style="line-height: 24px;">
                        <tr>
                            <td align="right">模板下载：
                            </td>
                            <td align="left">
                                <a id="A1" runat="server" href="~/Template/学时导入模版.xls"><font style="color: blue; text-decoration: underline;">学时导入模版.xls</font></a> &nbsp; &nbsp;（如果不能下载，请检查是否安装了其他下载软件的影响）
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <nobr>学习情况导入：</nobr>
                            </td>
                            <td align="left">
                                <div style="float: left; text-align: left;">
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
                                    <asp:Button ID="ButtonImportScore" runat="server" Text="导 入" CssClass="button" OnClick="ButtonImportScore_Click"
                                        Enabled="true" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
