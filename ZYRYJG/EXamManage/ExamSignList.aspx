<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ExamSignList.aspx.cs" Inherits="ZYRYJG.EXamManage.ExamSignList" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="~/ExamNotice.ascx" TagPrefix="uc1" TagName="ExamNotice" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <style type="text/css">
        @keyframes blink {         
          70% { padding-right:20px; } 
        } 
        .blinking-text {
          animation: blink 2s infinite;
        }
    </style>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridExamPlan">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamPlan" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamPlan" LoadingPanelID="RadAjaxLoadingPanel1" />
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
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true">
    </telerik:RadWindowManager>

    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                报名管理 &gt;&gt; <strong>考试报名</strong>
            </div>
        </div>
        <div style="float: right; padding: 10px 30px 10px 0;">
            <span>
                <%--<uc1:ExamNotice runat="server" ID="ExamNotice" />--%>
            </span>
            <a id="A2" runat="server" href="http://zjw.beijing.gov.cn/bjjs/gcjs/kszczn/qnksjh/index.shtml" target="_blank" class="blinking-text "><font style="color: blue; font-size: 18px;font-weight:bold; text-decoration: none; margin-left: 10px;">【建筑业从业人员全年考试工作计划】</font></a>
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
                            Width="46%" Style="float: left;" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="RadDatePicker_SignUpEndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" Style="float: left;" />
                    </td>
                    <td align="right" nowrap="nowrap">考试时间：
                    </td>
                    <td width="39%" align="left">
                        <telerik:RadDatePicker ID="RadDatePicker_ExamStartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" Style="float: left;" />
                        <div class="RadPicker">至</div>
                        <telerik:RadDatePicker ID="RadDatePicker_ExamEndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                            Width="46%" Style="float: left;" />
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">状态：
                    </td>
                    <td width="39%" align="left">
                        <asp:RadioButtonList ID="rbl" runat="server" RepeatDirection="Horizontal" AutoPostBack="false" Width="300px">
                            <asp:ListItem Value="0">全部</asp:ListItem>
                            <asp:ListItem Value="1">未考试</asp:ListItem>
                            <asp:ListItem Value="2">已考试</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>
            <div class="table_cx" >
                <img alt="" src="../Images/jglb.png" width="15" height="15" />
                考试计划列表
                <div style="float: right; padding-right: 30px;">
                    <%--<a id="A1" runat="server" href="http://120.52.185.14/Register/NewsView.aspx?o=GSX4F7eG/gds3S6khOb0hhgUix0BBdfAe8fUFASy1ErQ/lCW2zkmyA==" target="_blank"><font style="color: Blue; font-size: 16px; text-decoration: underline; margin-left: 10px;">【安管人员网络在线考核报考须知（2023年9月）】</font></a>--%>
                   <a href="../Template/安管人员网络在线考核要求及系统操作手册.zip" target="_blank" style="color: blue; font-size: 18px;font-weight:bold;" class="blinking-text">【 安管人员网络在线考核要求及系统操作手册】</a >
                </div>
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGridExamPlan" AutoGenerateColumns="False" PagerStyle-AlwaysVisible="true"
                    runat="server" AllowAutomaticDeletes="True" AllowPaging="True" PageSize="10"
                    AllowSorting="True" SortingSettings-SortToolTip="单击进行排序" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false" Width="100%" GridLines="None">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="ExamPlanID" NoMasterRecordsText="　没有可显示的记录">
                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="ExamPlanName" HeaderText="考试计划名称">
                                <ItemTemplate>
                                    <%# string.Format("<a href='ExamPlanDetail.aspx?o={0}'>{1}</a>", Utility.Cryptography.Encrypt(Eval("ExamPlanID").ToString()), Eval("ExamPlanName"))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Font-Underline="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="PostName" HeaderText="岗位工种">
                                <ItemTemplate>
                                    <%# Eval("PostTypeName")%><br />
                                    <%# Eval("PostName")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Font-Underline="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="个人报名提交时间">
                                <ItemTemplate>
                                    <nobr><%# Convert.ToDateTime(Eval("SignUpStartDate")).ToString("yyyy.MM.dd") + "-"%></nobr>
                                    <nobr><%# Convert.ToDateTime(Eval("SignUpEndDate")).ToString("MM.dd")%></nobr>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="企业确认时间">
                                <ItemTemplate>
                                    <nobr><%# Convert.ToDateTime(Eval("SignUpStartDate")).AddDays(1).ToString("yyyy.MM.dd") + "-"%></nobr>
                                    <nobr><%# Convert.ToDateTime(Eval("SignUpEndDate")).AddDays(2).ToString("MM.dd")%></nobr>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="建委审核时间">
                                <ItemTemplate>
                                    <nobr><%# Eval("StartCheckDate") == DBNull.Value ? Convert.ToDateTime(Eval("SignUpEndDate")).ToString("yyyy.MM.dd") + "-" : Convert.ToDateTime(Eval("StartCheckDate")).ToString("yyyy.MM.dd") + "-"%></nobr>
                                    <nobr><%# Convert.ToDateTime(Eval("LatestCheckDate")).ToString("MM.dd")%></nobr>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="准考证发放">
                                <ItemTemplate>
                                    <nobr><%# Convert.ToDateTime(Eval("ExamCardSendStartDate")).ToString("yyyy.MM.dd") + "-"%></nobr>
                                    <nobr><%# Convert.ToDateTime(Eval("ExamCardSendEndDate")).ToString("MM.dd")%></nobr>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="考试时间">
                                <ItemTemplate>
                                    <nobr><%# Convert.ToDateTime(Eval("ExamStartDate")).CompareTo(Convert.ToDateTime(Eval("ExamEndDate"))) == 0 ? Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd") : Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd") + "-"%></nobr>
                                    <nobr><%# Convert.ToDateTime(Eval("ExamStartDate")).CompareTo(Convert.ToDateTime(Eval("ExamEndDate"))) == 0 ? "" : Convert.ToDateTime(Eval("ExamEndDate")).ToString("MM.dd")%></nobr>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>

                            <telerik:GridTemplateColumn UniqueName="SingUp" HeaderText="我要报名">
                                <ItemTemplate>
                                    <%# (Convert.ToDateTime(Eval("SignUpStartDate")) <= DateTime.Now && Convert.ToDateTime(Eval("SignUpEndDate")).AddDays(1) > DateTime.Now) ?
                                    "<a href='ExamSign.aspx?o=" + Utility.Cryptography.Encrypt(Eval("ExamPlanID").ToString()) + "'><nobr>我要报名</nobr></a>" : ""%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>

                        </Columns>
                        <EditFormSettings>
                            <EditColumn InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif" EditImageUrl="Edit.gif"
                                CancelImageUrl="Cancel.gif">
                            </EditColumn>
                        </EditFormSettings>
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
                    DataObjectTypeName="Model.ExamPlanOB" SelectMethod="GetList" EnablePaging="true"
                    SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <div style="padding: 12px 12px; font-size: 20px; font-weight: bold; background-color: #EDF6FB; border: 1px solid #EDF6FB; border-radius: 8px 8px; text-align: center; margin: 20px 20px;">
                安管人员模拟练习题及参考答案下载：<a href="../Template/test2022.zip" target="_blank">【 安管人员模拟练习题及参考答案】</a>
            </div>
            <div class="table_cx" style="margin: 8px 8px;">
                <img alt="" src="../Images/jglb.png" width="15" height="15" />
                我的报名列表<span style="color: orangered">（注意：所在企业未在企业网上确认日期内确认提交住建委审核的，预约考试申请无效。请个人自行联系企业登录系统进行确认。</span>
            </div>
            <div style="width: 98%; margin: 0 auto; padding-bottom: 10px;">
                <telerik:RadGrid ID="RadGridExamSingup" runat="server" Width="100%" PagerStyle-AlwaysVisible="true"
                    PageSize="10" AllowSorting="True" AllowPaging="True" GridLines="None" AutoGenerateColumns="False"
                    Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ExamSignUpID" NoMasterRecordsText="　没有可显示的记录">
                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                        <Columns>
                            <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="LockTime" HeaderText="">
                                <ItemTemplate>
                                    <%# Eval("LockEndTime") != DBNull.Value && Convert.ToDateTime(Eval("LockEndTime")).AddDays(1) > DateTime.Now ? string.Format("<img alt=\"\" src=\"../Images/s_lock.png\" title=\"违规申报锁定，锁定时间：{0} - {1}，\r\n锁定原因：{2}\" />", Convert.ToDateTime(Eval("LockTime")).ToString("yyyy.MM.dd"), Convert.ToDateTime(Eval("LockEndTime")).ToString("yyyy.MM.dd"), Eval("LockReason")) : ""%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Detail" HeaderText="详细">
                                <ItemTemplate>
                                    <%#string.Format("<a href='ExamSign.aspx?o={0}&s={1}'><nobr>查看</nobr></a>", Utility.Cryptography.Encrypt(Eval("ExamPlanID").ToString()), Utility.Cryptography.Encrypt(Eval("EXAMSIGNUPID").ToString()))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="PostName" HeaderText="申报岗位工种">
                                <ItemTemplate>
                                    <nobr><%# Eval("PostName")%><%# Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("暖通") ? "（暖通）" :
                                                   Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("电气") ? "（电气）" : ""%></nobr>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="考试时间">
                                <ItemTemplate>
                                    <nobr><%# Convert.ToDateTime(Eval("ExamStartDate")).CompareTo(Convert.ToDateTime(Eval("ExamEndDate"))) == 0 ? Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd") : Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd") + " - " + Convert.ToDateTime(Eval("ExamEndDate")).ToString("yyyy.MM.dd")%></nobr>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SignUpDate" DataField="SignUpDate" HeaderText="报名时间"
                                HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>

                            <telerik:GridTemplateColumn UniqueName="Status" HeaderText="审核状态">
                                <ItemTemplate>
                                    <%#  Eval("Status").ToString() == "未提交" ? "未提交审核" : (Eval("Status").ToString() == "退回修改" && Eval("StartCheckDate") != DBNull.Value && Convert.ToDateTime(Eval("StartCheckDate")) < DateTime.Now) ? "退回" : Eval("Status").ToString() == "已缴费" ? "审核确认" : Eval("Status")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="CheckDate" HeaderText="审核意见">
                                <ItemTemplate>
                                    <%# Eval("HIREUNITADVISE") != DBNull.Value && Eval("HIREUNITADVISE").ToString() != "" && Eval("FIRSTTRIALTIME") != DBNull.Value ? string.Format("<div>{0} {1}{2}</div>", Convert.ToDateTime(Eval("FIRSTTRIALTIME")).ToString("yyyy.MM.dd日"), "单位确认：", Eval("HIREUNITADVISE")) : ""%>
                                    <%# Eval("CheckDate") != DBNull.Value ? string.Format("<div>{0} {1}{2}</div>"
                                    , Convert.ToDateTime(Eval("CheckDate")).ToString("yyyy.MM.dd日")
                                    , Eval("PostTypeID").ToString() == "4000"?"培训点审核":"住建委审核："
                                    , Eval("CHECKRESULT")) : ""%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridTemplateColumn>
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
                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" DeleteMethod="Delete"
                    InsertMethod="Insert" SelectMethod="GetList_New" TypeName="DataAccess.ExamSignUpDAL"
                    UpdateMethod="Update" SelectCountMethod="SelectCount_New" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />

                    </SelectParameters>
                </asp:ObjectDataSource>

            </div>
        </div>
        <br />
    </div>
    <%--数秒提示--%>
    <div id="DivExamConfirm" runat="server" style="line-height: 30px; width: 880px; display: none; margin-top: 30px; position: absolute; top: 100px; left: 200px; background-color: #dedede; padding: 20px 50px 50px 50px; border-left: 4px solid #eee; border-top: 4px solid #eee; border-right: 4px solid #999; border-bottom: 4px solid #999; color: #000; z-index: 9999999">
        <p style="font-size: 30px; font-weight: bold; text-align: center;">系统提示</p>
        <p style="text-align: left;" id="p_ExamConvfirmDesc" runat="server">
        </p>
        <p style="text-align: center;">
            <asp:Button ID="ButtonExamNo" runat="server" Text="已知晓‥关闭页面" CssClass="bt_large btn_no" OnClick="ButtonExamNo_Click" CausesValidation="false" Enabled="false" />
            <span style="padding-left: 20px; font-size: 30px; color: red; font-weight: bold" id="spanCount">10</span>
        </p>
    </div>
     <div id="divSignupTip" runat="server" style="display:none"></div>
 <%-- <div id="divSignupTip" runat="server" style="display:none">
       <b>考试通知：</b><br /><br />
        2023年12月份（第五批）安全生产管理人员网络在线考核：<br />
        考生报名：2023年11月13日至11月15日<br />
        企业确认：2023年11月14日至11月17日<br />
        准考证下载：2023年12月13日至12月20日<br />
        模拟测试：2023年12月13日至12月19日（每日9:00至17:00，技术支持电话：4008703877）<br />
        正式考核：2023年12月21日<br />
        <span style="color:red">重要提示：逾期未按要求参加模拟测试或不满足网络在线考核相关要求的，视为考生自动放弃报考资格。</span><br /><br />
        <a href="../Template/安管人员网络在线考核要求及系统操作手册（2024年6月）.zip" target="_blank">【 安管人员网络在线考核要求及系统操作手册】</a >
  </div>--%>
</asp:Content>
