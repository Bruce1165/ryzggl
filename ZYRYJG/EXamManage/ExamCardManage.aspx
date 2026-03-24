<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ExamCardManage.aspx.cs" Inherits="ZYRYJG.EXamManage.ExamCardManage" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../ExamPlanSelect.ascx" TagName="ExamPlanSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc4" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {

                if (args.get_eventTarget().indexOf("ButtonPrint") >= 0) {
                    args.set_enableAjax(false);

                }
            }
            function setDivManDisplay(display) {
                document.getElementById("DivMain").style.display = display;
                if (display == "inline") document.getElementById("DivPrintProgress").style.display = "none";
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        runat="server" Skin="Windows7" EnableShadow="true" OnClientClose="OnClientClose">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamResult" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGridExamResult">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamResult" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="PostSelect2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PostSelect2" LoadingPanelID="RadAjaxLoadingPanel1" />
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
                考场管理 &gt;&gt; <strong>准考证管理</strong>
            </div>
        </div>
        <div class="content">
            <div style="float: right; padding: 10px 30px 10px 0px;">
                <img alt="" src="../Images/light.gif" />
                <a id="A2" runat="server" href="http://zjw.beijing.gov.cn/bjjs/gcjs/kszczn/kddt/index.shtml" target="_blank"><font style="color: Blue; font-size: 16px; text-decoration: underline;">查看考点地图</font></a>
            </div>
            <div id="divExamplan" runat="server" visible="false" style="width: 98%; margin: 20px auto; clear: both;">
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    准考证下载时间提示 
                </div>
                <telerik:RadGrid ID="RadGridExamPlan" AutoGenerateColumns="False" 
                    runat="server" AllowAutomaticDeletes="True" AllowPaging="false"
                    AllowSorting="True" SortingSettings-SortToolTip="单击进行排序" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false" Width="100%" GridLines="None">
                    <ClientSettings EnableRowHoverStyle="false">
                    </ClientSettings>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ExamPlanID" NoMasterRecordsText="　没有可显示的记录">
                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="ExamPlanName" DataField="ExamPlanName" HeaderText="考试计划名称">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="准考证下载时间">
                                <ItemTemplate>
                                    <nobr><%# Convert.ToDateTime(Eval("ExamCardSendStartDate")).ToString("yyyy.MM.dd") + "-"%></nobr>
                                    <nobr><%# Convert.ToDateTime(Eval("ExamCardSendEndDate")).ToString("yyyy.MM.dd")%></nobr>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="考试时间">
                                <ItemTemplate>
                                    <nobr><%# Convert.ToDateTime(Eval("ExamStartDate")).CompareTo(Convert.ToDateTime(Eval("ExamEndDate")))==0?Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd"):Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd") + "-"  %></nobr>
                                    <nobr><%# Convert.ToDateTime(Eval("ExamStartDate")).CompareTo(Convert.ToDateTime(Eval("ExamEndDate")))==0?"":Convert.ToDateTime(Eval("ExamEndDate")).ToString("yyyy.MM.dd") %></nobr>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                        <HeaderStyle Font-Bold="True" />
                    </MasterTableView>
                    <FilterMenu EnableEmbeddedSkins="False">
                    </FilterMenu>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
            </div>
            <table class="bar_cx">
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">考试名称：
                    </td>
                    <td align="left" colspan="3">
                        <uc1:ExamPlanSelect ID="ExamPlanSelect1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">考点名称：
                    </td>
                    <td align="left" width="39%">
                        <telerik:RadTextBox ID="RadTextBoxExamPlaceName" runat="server" CssClass="texbox"
                            Width="50%" Skin="Default">
                        </telerik:RadTextBox>
                        &nbsp;&nbsp; 考场号：
                                        <telerik:RadTextBox ID="RadTextBoxExamRoomCode" runat="server" CssClass="texbox"
                                            Skin="Default" Width="20%">
                                        </telerik:RadTextBox>
                    </td>
                    <td align="right" width="11%" nowrap="nowrap">准考证号：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="RadTextBoxExamCardID" runat="server" CssClass="texbox" Skin="Default"
                            Width="97%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td width="11%" align="right" nowrap="nowrap">考生姓名：
                    </td>
                    <td width="39%" align="left">
                        <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" CssClass="texbox" Skin="Default"
                            Width="97%">
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">证件号码：
                    </td>
                    <td align="left">
                        <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" CssClass="texbox"
                            Skin="Default" Width="97%">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td width="11%" align="right" nowrap="nowrap">岗位工种：
                    </td>
                    <td width="39%" align="left">
                        <uc4:PostSelect ID="PostSelect2" runat="server" />
                    </td>
                    <td align="right" width="11%" nowrap="nowrap">考试时间：
                    </td>
                    <td width="39%" align="left">
                        <telerik:RadComboBox ID="RadComboBoxYear" runat="server" Skin="Office2007" CausesValidation="False"
                            Width="50px" ExpandAnimation-Duration="0">
                        </telerik:RadComboBox>
                        &nbsp;年&nbsp;
                                        <telerik:RadComboBox ID="RadComboBoxMonth" runat="server" Skin="Office2007" CausesValidation="False"
                                            Width="50px" ExpandAnimation-Duration="0">
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
                                        <asp:CheckBox ID="CheckBoxExamNotBeigin" runat="server" Text="（只显示未考试记录）" ForeColor="Red" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">报名批号：
                    </td>
                    <td align="left" width="38%">
                        <telerik:RadTextBox ID="RadTextBoxSignUpCode" runat="server" Width="97%" Skin="Default">
                        </telerik:RadTextBox>
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">
                        <span id="spanUnit" runat="server" visible="false">单位名称：</span>

                    </td>
                    <td align="left" width="38%">
                        <telerik:RadTextBox ID="RadTxtUnitName" runat="server" Width="95%" Skin="Default" Visible="false">
                        </telerik:RadTextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查询" CssClass="bt_large" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>
            <div class="table_cx">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                准考证列表（按考试名称查询后才能批量打印，单个打印请进入详细页面）
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGridExamResult" AutoGenerateColumns="False"
                    runat="server" AllowPaging="True" PageSize="10" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序"
                    Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="100%"
                    GridLines="None" PagerStyle-AlwaysVisible="true"
                    OnPageIndexChanged="RadGridAccept_PageIndexChanged" OnDataBound="RadGridAccept_DataBound">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ExamResultID,ExamCardID"
                        NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="Highlight <br/> ship name">
                                <HeaderTemplate>
                                    <uc3:CheckAll ID="CheckAll1" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn UniqueName="PostName" HeaderText="岗位工种">
                                <ItemTemplate>
                                    <nobr><%# Eval("PostName")%><%# Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("暖通") ? "（暖通）":
                                                   Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("电气") ? "（电气）" : ""%></nobr>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="SkillLevel" DataField="SkillLevel" HeaderText="技术职称或等级"
                                Visible="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="考试时间">
                                <ItemTemplate>
                                   <%-- <nobr><%# Convert.ToDateTime(Eval("ExamStartDate")).CompareTo(Convert.ToDateTime(Eval("ExamEndDate")))==0?Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd"):Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd") + " - " + Convert.ToDateTime(Eval("ExamEndDate")).ToString("yyyy.MM.dd")%></nobr>--%>
                                     <nobr><%# Convert.ToDateTime(Eval("ExamStartDate")).ToString()==Convert.ToDateTime(Eval("ExamStartTime")).ToString()?Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy年MM月dd日"):Convert.ToDateTime(Eval("ExamStartTime")).ToString("yyyy年MM月dd日 HH:mm") + " - " + Convert.ToDateTime(Eval("ExamEndTime")).ToString("HH:mm")%></nobr>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="ExamPlaceName" DataField="ExamPlaceName" HeaderText="考点名称">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ExamRoomCode" DataField="ExamRoomCode" HeaderText="考场号">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>                           
                            <telerik:GridTemplateColumn UniqueName="ExamCardID" HeaderText="准考证号">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("./ExamCard.aspx?o=<%# Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("ExamResultID").ToString())) %>");'>
                                        <nobr><%# Eval("ExamCardID").ToString()%></nobr>
                                    </span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="考生姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="企业名称">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="TrainUnitName" DataField="TrainUnitName" HeaderText="报名点名称"
                                Visible="false">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>                         
                        </Columns>
                        <HeaderStyle Font-Bold="True" />
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <ClientSettings>
                    </ClientSettings>
                    <PagerStyle Mode="NextPrevNumericAndAdvanced" FirstPageToolTip="首页" LastPageToolTip="末页"
                        NextPageToolTip="下一页" PrevPageToolTip="上一页" PageSizeLabelText="每页记录数：" />
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.ExamResultDAL"
                    DataObjectTypeName="Model.ExamResultOB" SelectMethod="GetListView" EnablePaging="true"
                    SelectCountMethod="SelectCountView" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <div id="divPrint" runat="server" style="padding: 10px 0px; text-align: center; font-size: 12px;">
                <asp:Button ID="ButtonExportToExcel" CssClass="bt_maxlarge" Text="导出列表Excel" OnClick="ButtonExportToExcel_Click"
                    ToolTip="请先查询过滤后再导出列表" runat="server"></asp:Button>&nbsp;&nbsp;
                <%--    <asp:Button ID="ButtonPrint" runat="server" Text="批量打印" CssClass="bt_large" OnClick="ButtonPrint_Click" Visible="false" />
                &nbsp;&nbsp;--%>
                    <asp:Button ID="ButtonExport" runat="server" Text="批量导出打印" CssClass="bt_large" OnClick="ButtonExport_Click" />
               &nbsp;&nbsp;
                    <asp:Button ID="ButtonOutputComputExamData" runat="server" Text="导出机考考生数据" CssClass="bt_maxlarge" OnClick="ButtonOutputComputExamData_Click" Visible="false" />
                 &nbsp;&nbsp;
                    <asp:Button ID="ButtonOutputNetExamData" runat="server" Text="导出网考考生数据" CssClass="bt_maxlarge" OnClick="ButtonOutputNetExamData_Click" Visible="false" />
                <br />
            </div>
        </div>
    </div>
    <uc5:IframeView ID="IframeView" runat="server" />
    <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Enabled="false" Interval="5000"
        EnableViewState="true">
    </asp:Timer>
    <asp:UpdatePanel ID="UpdatePanelPrint" runat="server" Visible="false">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="Timer1" />
        </Triggers>
        <ContentTemplate>
            <center>
                <div id="DivPrintProgress" style="width: 300px; height: 70px; margin-top: 100px; vertical-align: middle; display: block; border: solid 8px #F15C04; padding-top: 30px; background-color: White;">
                    <asp:Label ID="LabelTip" runat="server" Text="" Font-Bold="true"></asp:Label>
                </div>
            </center>
        </ContentTemplate>
    </asp:UpdatePanel>
   <%-- <div id="floadAD" class="floadAd">
        <a class="close" href="javascript:void();" style="color: #000000; text-align: right; float: right; clear: both; padding-bottom: 4px; font-size: 28px">×</a>
        <p class="item">
            <b>考生请注意：</b><br />
             一、考生须考前14天进行日常体温监测和健康监测、并提前进行核酸检测。在入场前提交《从业人员考试疫情防控告知暨承诺书》<span style="color: red">（打印准考证时自动下载）</span>和48小时内采样的核酸检测阴性证明。<br /><br />
	    二、考试当天考生应提前1小时到达考点，预留充足时间配合考点工作人员进行防疫检测。现场测量体温正常、北京健康宝显示“未见异常”且应持48小时内采样的核酸检测阴性证明<span style="color: red">（已经打过疫苗也要核酸检测）</span>、《从业人员考试疫情防控告知暨承诺书》的考生方可进入考点参加考试。<br /><br />
	    三、建设行业考试防疫相关要求将根据全市疫情防控措施规定的变化随时进行调整，请考生及时关注市住建委微信公众号“安居北京”，及时了解最新考试通知，以免影响正常参考。<br />
        </p>
    </div>
    <script src="../Scripts/FloatMessage.js" type="text/javascript"></script>
    <script type="text/javascript" language="javascript"> FloatAd("#floadAD");</script>--%>
     
     <%--数秒提示--%>
        <div id="DivExamConfirm" runat="server" style="line-height: 30px; width: 680px; display: none; margin-top: 30px; position: absolute; top: 100px; left: 200px; background-color: #dedede; padding: 20px 50px 50px 50px; border-left: 4px solid #eee; border-top: 4px solid #eee; border-right: 4px solid #999; border-bottom: 4px solid #999; color: #000">
            <p style="font-size: 30px; font-weight: bold; text-align: center;">系统提示</p>
            <p style="text-align: left;" id="p_ExamConvfirmDesc" runat="server">
            </p>
            <p style="text-align: center;">               
                <asp:Button ID="ButtonExamNo" runat="server" Text="已知晓‥关闭页面" CssClass="bt_large btn_no" OnClick="ButtonExamNo_Click" CausesValidation="false"  Enabled="false" />
                <span style="padding-left:20px;font-size:30px;color:red;font-weight:bold" id="spanCount">20</span>
            </p>            
        </div>

</asp:Content>
