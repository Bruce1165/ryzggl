<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="PMCertPrintManage.aspx.cs" Inherits="ZYRYJG.EXamManage.PMCertPrintManage" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc4" %>
<%@ Register Src="../ExamPlanSelect.ascx" TagName="ExamPlanSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <script type="text/javascript">
        //function onRequestStart(sender, args) {

        //    if (args.get_eventTarget().indexOf("ButtonExportExcel") >= 0) {
        //        args.set_enableAjax(false);

        //    }
        //}
        //function getEventObject(W3CEvent) {   //事件标准化函数
        //    return W3CEvent || window.event;
        //}
        //function getPointerPosition(e) {   //兼容浏览器的鼠标x,y获得函数
        //    e = e || getEventObject(e);
        //    var x = e.pageX || (e.clientX + (document.documentElement.scrollLeft || document.body.scrollLeft));
        //    var y = e.pageY || (e.clientY + (document.documentElement.scrollTop || document.body.scrollTop));

        //    return { 'x': x, 'y': y };
        //}

        //function setImgSize(img, imgWidth, timgHeight, position, e) {
        //    img.style.width = imgWidth + "px";
        //    img.style.height = timgHeight + "px";

        //    var pos = getPointerPosition(e);

        //    img.style.position = position;
        //    if (position == "absolute") {
        //        img.style.top = -timgHeight + 20 + "px";
        //        img.style.left = -imgWidth + 40 + "px";
        //    }
        //    else {
        //        img.style.top = 0;
        //        img.style.left = 0;
        //    }
        //}
    </script>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        runat="server" Skin="Windows7" EnableShadow="true" OnClientClose="OnClientClose">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <%-- <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridCertificate" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="ExamPlan" />
                    <telerik:AjaxUpdatedControl ControlID="Post" />
                    <telerik:AjaxUpdatedControl ControlID="PostSelect1" />
                    <telerik:AjaxUpdatedControl ControlID="RadWindowManager1" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
            <%--<telerik:AjaxSetting AjaxControlID="RadGridCertificate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridCertificate" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PostSelect1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PostSelect1" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridCertificate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonCaseUpdate">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridCertificate" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonExportExcel">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ButtonExportExcel" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>--%>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <%--<div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                证书制作 &gt;&gt; <strong>证书打印（喷墨）</strong>
            </div>
        </div>
        <div class="content">          
            <table id="tableSearch" runat="server" class="bar_cx">
                <tr id="ExamPlan" runat="server">
                    <td align="right" nowrap="nowrap" width="7%">考试名称：
                    </td>
                    <td align="left" colspan="3">
                        <uc1:ExamPlanSelect ID="ExamPlanSelect1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap" width="7%">证书编号：
                    </td>
                    <td align="left" width="43%">前缀
                                    <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="100px" Skin="Default"
                                        >
                                    </telerik:RadTextBox>
                        流水号
                                    <telerik:RadTextBox ID="RadTextBoxCodeFrom" runat="server" Width="60px" MaxLength="9"
                                        Skin="Default" >
                                    </telerik:RadTextBox>
                        -
                                    <telerik:RadTextBox ID="RadTextBoxCodeTo" runat="server" Width="60px" MaxLength="9"
                                        Skin="Default" >
                                    </telerik:RadTextBox>
                        <asp:RegularExpressionValidator ID="CodeFromValidator" runat="server" Display="Dynamic"
                            ErrorMessage="<br />流水号只能输数字" ValidationExpression="^[0-9]*$" ControlToValidate="RadTextBoxCodeFrom">
                        </asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="CodeToValidator" runat="server" Display="Dynamic"
                            ErrorMessage="<br />流水号只能输数字" ValidationExpression="^[0-9]*$" ControlToValidate="RadTextBoxCodeTo">
                        </asp:RegularExpressionValidator>
                    </td>
                    <td align="right" nowrap="nowrap" width="7%">打印状态：
                    </td>
                    <td align="left" width="43%">
                        <telerik:RadComboBox ID="RadComboBoxPrint" runat="server" Width="80px">
                            <Items>
                                <telerik:RadComboBoxItem Text="全部" Value="0" Selected="true" />
                                <telerik:RadComboBoxItem Text="已打印" Value="1" />
                                <telerik:RadComboBoxItem Text="未打印" Value="2" />
                            </Items>
                        </telerik:RadComboBox>
                        &nbsp;&nbsp;&nbsp;&nbsp;归档状态：
                                    <telerik:RadComboBox ID="RadComboBoxCaseStatus" runat="server" Width="80px">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="全部" Value="0" Selected="true" />
                                            <telerik:RadComboBoxItem Text="已归档" Value="1" />
                                            <telerik:RadComboBoxItem Text="未归档" Value="2" />
                                        </Items>
                                    </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">打印人：
                    </td>
                    <td width="43%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxPrintMan" runat="server" Width="93%" Skin="Default"
                            MaxLength="40" >
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap">打印时间：
                    </td>
                    <td width="43%" align="left">
                        <telerik:RadDatePicker ID="RadDatePicker_PrintStartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="RadDatePicker_PrintEndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">企业名称：
                    </td>
                    <td align="left" width="43%">
                        <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="93%" Skin="Default"
                            MaxLength="100" >
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap">培训点名称：
                    </td>
                    <td align="left" width="43%" >
                        <telerik:RadTextBox ID="RadTextBoxPeiXunUnitName" runat="server" Width="93%" Skin="Default"
                            MaxLength="100">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">考生姓名：
                    </td>
                    <td align="left" width="43%">
                        <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="93%" Skin="Default"
                            MaxLength="40" >
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <div id="Post" runat="server">
                            岗位工种：
                        </div>
                    </td>
                    <td align="left" width="43%">
                        <uc4:PostSelect ID="PostSelect1" runat="server" OnPostTypeSelectChange="PostSelect1_PostTypeSelectChange" />
                    </td>
                </tr>
                <tr id="TrChangeQueryParam" runat="server">
                    <td align="right" nowrap="nowrap">受理批号：
                    </td>
                    <td align="left" width="43%">
                        <telerik:RadTextBox ID="RadTextBoxNoticeCode" runat="server" Width="93%" Skin="Default"
                            MaxLength="50" >
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap">受理时间：
                    </td>
                    <td width="43%" align="left">
                        <telerik:RadDatePicker ID="RadDatePickerNoticeDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="RadDatePickerNoticeDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                    </td>
                </tr>
                <tr id="TrContinueParm" runat="server" visible="false">
                    <td align="right" nowrap="nowrap">决定批号：
                    </td>
                    <td align="left" width="43%">
                        <telerik:RadTextBox ID="RadTextBoxConfirmCode" runat="server" Width="93%" Skin="Default"
                            MaxLength="50" >
                        </telerik:RadTextBox>
                    </td>
                    <td align="right" nowrap="nowrap">决定日期：
                    </td>
                    <td width="43%" align="left">
                        <telerik:RadDatePicker ID="RadDatePickerConfirmDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="RadDatePickerConfirmDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" />
                    </td>
                </tr>
                <tr id="TrPage" runat="server" visible="false">
                    <td align="right" nowrap="nowrap" width="7%">页码范围：
                    </td>
                    <td align="left" width="43%">从
                                    <telerik:RadTextBox ID="RadTextBoxPageFrom" runat="server" Width="60px" MaxLength="6"
                                        Skin="Default" >
                                    </telerik:RadTextBox>
                        至 
                                    <telerik:RadTextBox ID="RadTextBoxPageTo" runat="server" Width="60px" MaxLength="6"
                                        Skin="Default" >
                                    </telerik:RadTextBox>
                        页
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Display="Dynamic"
                                        ErrorMessage="<br />页码范围只能输数字" ValidationExpression="^[0-9]*$" ControlToValidate="RadTextBoxPageFrom">
                                    </asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" Display="Dynamic"
                            ErrorMessage="<br />页码范围只能输数字" ValidationExpression="^[0-9]*$" ControlToValidate="RadTextBoxPageTo">
                        </asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>

            <table width="100%">
                <tr>
                    <td align="left">
                        <div class="table_cx">
                            <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                            证书列表
                        </div>
                    </td>
                    <td align="right">
                        <div id="Div_UserResourceList" runat="server" style="padding-right: 20px; clear: right; line-height: 30px;">
                            <a target="_blank" style="text-decoration: underline; color: Blue;" href="CertificateSendTrainList.aspx">>> 证书下发单位对照表</a>
                        </div>
                    </td>
                </tr>
            </table>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGridCertificate" AutoGenerateColumns="False" runat="server"
                    AllowPaging="true" AllowCustomPaging="true"
                    PageSize="10" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序" Skin="Blue"
                    EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="100%" GridLines="None"
                    OnExcelExportCellFormatting="RadGridCertificate_ExcelExportCellFormatting" OnItemDataBound="RadGridCertificate_ItemDataBound"
                    OnPageIndexChanged="RadGridAccept_PageIndexChanged" OnDataBound="RadGridAccept_DataBound">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="CertificateID,PostTypeID,ValidStartDate,ValidEndDate,PrintMan"
                        NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <uc3:CheckAll ID="CheckAll1" runat="server" />
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="certificateCode"
                                HeaderText="证书编号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ValidEndDate" DataField="ValidEndDate" HeaderText="有效期至"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerCertificateCode" DataField="WorkerCertificateCode"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PrintStatus" DataField="PrintMan" HeaderText="打印状态">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="DetailView" DataField="PrintMan" HeaderText="详细">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Font-Underline="true" ForeColor="Blue" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PrintMan" DataField="PrintMan" HeaderText="打印人"
                                Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PrintDate" DataField="PrintDate" HeaderText="打印时间"
                                Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="Faceimage" HeaderText="照片">
                                <ItemTemplate>
                                    <div style="position: relative;">
                                        <asp:Image ID="Image1" runat="server" Width="14" Height="18" ImageUrl='<%#  ZYRYJG.UIHelp.ShowFaceImage(Eval("FACEPHOTO").ToString(),Eval("WorkerCertificateCode").ToString())  %>'
                                            onmouseover="setImgSize(this,100,140,'absolute',event);" onmouseout="setImgSize(this,14,18,'relative',event);"></asp:Image>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>

                        </Columns>
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.CertificateOB"
                    DeleteMethod="Delete" InsertMethod="Insert" SelectMethod="GetList" TypeName="DataAccess.CertificateDAL"
                    UpdateMethod="Update" SelectCountMethod="SelectCount" EnablePaging="true" MaximumRowsParameterName="maximumRows"
                    StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <br />
                <div id="anniudiv" runat="server">
                    <asp:Button ID="ButtonPrint" runat="server" Text="打印证书" CssClass="bt_maxlarge" OnClick="ButtonPrint_Click" />&nbsp;
                            <asp:Button ID="btnPrint" runat="server" Text="打印证书照片阵列" ToolTip="" CssClass="bt_maxlarge"
                                OnClick="btnPrint_Click" />&nbsp;
                            <asp:Button ID="ButtonCaseUpdate" runat="server" Text="归档（办结）" CssClass="bt_maxlarge"
                                OnClick="ButtonCaseUpdate_Click" />
                    &nbsp;
                            <asp:Button ID="ButtonExportExcel" runat="server" Text="导出查询结果列表" CssClass="bt_maxlarge"
                                OnClick="ButtonExportExcel_Click" ToolTip="请先根据查询条件筛选后再导出结果集" />

                </div>
                <br />
            </div>
        </div>
    </div>--%>
</asp:Content>
