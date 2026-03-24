<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ExamPhotoList.aspx.cs" Inherits="ZYRYJG.EXamManage.ExamPhotoList" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../ExamPlanSelect.ascx" TagName="ExamPlanSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridExamResult">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamResult" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonOutput">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="HiddenFieldOutputOk" />
                    <telerik:AjaxUpdatedControl ControlID="ButtonOutput" LoadingPanelID="RadAjaxLoadingPanel1" UpdatePanelRenderMode="Inline" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="Timer2">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ButtonOutput" UpdatePanelRenderMode="Inline" UpdatePanelHeight="0" />
                    <telerik:AjaxUpdatedControl ControlID="HiddenFieldOutputOk" />

                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        runat="server" Skin="Default" EnableShadow="true" OnClientClose="OnClientClose">
        <Windows>
            <telerik:RadWindow ID="RadWindow2" runat="server" Animation="Fade">
            </telerik:RadWindow>
        </Windows>
    </telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function openWin2(id) {
                var oWnd2 = radopen("<%=RootUrl %>/EXamManage/ExamPhoto.aspx?o=" + id, "RadWindow2");
                oWnd2.setUrl("<%=RootUrl %>/EXamManage/ExamPhoto.aspx?o=" + id);
                oWnd2.show();
                oWnd2.maximize();
            }
            function setDivManDisplay(display) {
                document.getElementById("DivMain").style.display = display;
                if (display == "inline") document.getElementById("DivPrintProgress").style.display = "none";
            }
        </script>
    </telerik:RadCodeBlock>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                考场管理 &gt;&gt; <strong>照片阵列</strong>
            </div>
        </div>
        <div id="DivMain" class="table_border">
            <div class="content">
                <table class="bar_cx">
                    <tr>
                        <td align="right" nowrap="nowrap" width="11%">考试名称：
                        </td>
                        <td nowrap="nowrap" align="left" colspan="3">
                            <uc1:ExamPlanSelect ID="ExamPlanSelect1" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="nowrap">考点名称：
                        </td>
                        <td align="left" width="39%">
                            <telerik:RadTextBox ID="RadTextBoxExamPlaceName" runat="server" CssClass="texbox"
                                Width="95%" Skin="Default" >
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" width="11%" nowrap="nowrap">考场号：
                        </td>
                        <td nowrap="nowrap" align="left">
                            <telerik:RadTextBox ID="RadTextBoxExamRoomCode" runat="server" CssClass="texbox"
                                Width="97%" Skin="Default" >
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </table>

                <div class="table_cx" style="padding-top: 5px;">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    考场列表
                </div>
                <div style="width: 98%; margin: 0 auto;">
                    <telerik:RadGrid ID="RadGridExamResult" AutoGenerateColumns="False"  PagerStyle-AlwaysVisible="true"
                        runat="server" AllowPaging="True" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序"
                        Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="100%"
                        GridLines="None" OnPageIndexChanged="RadGridExamResult_PageIndexChanged"
                        OnDataBound="RadGridExamResult_DataBound">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <MasterTableView PageSize="10" CommandItemDisplay="None" DataKeyNames="ExamPlaceAllotID,ExamRoomAllotID"
                            NoMasterRecordsText="　没有可显示的记录">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="Highlight <br/> ship name">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <uc3:CheckAll ID="CheckAll1" runat="server" />
                                    </HeaderTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridTemplateColumn HeaderText="考试时间">
                                    <ItemTemplate>
                                        <nobr><%# Convert.ToDateTime(Eval("ExamStartTime")).ToString()==Convert.ToDateTime(Eval("ExamEndTime")).ToString()?Convert.ToDateTime(Eval("ExamStartTime")).ToString("yyyy年MM月dd日"):Convert.ToDateTime(Eval("ExamStartTime")).ToString("yyyy年MM月dd日 HH:mm -")+ Convert.ToDateTime(Eval("ExamEndTime")).ToString("HH:mm")%>
                                       </nobr>
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
                                <telerik:GridBoundColumn UniqueName="PersonNumber" DataField="PersonNumber" HeaderText="考生人数">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ExamCardIDFromTo" DataField="ExamCardIDFromTo"
                                    HeaderText="准考证号段">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="SingUp" HeaderText="">
                                    <ItemTemplate>
                                        <span onclick="openWin2('<%# Eval("ExamRoomAllotID").ToString() %>');return false;"
                                            style="cursor: pointer;">查看照片阵列</span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                            <PagerTemplate>
                                <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                        <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                        <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.ExamRoomAllotDAL"
                        DataObjectTypeName="Model.ExamRoomAllotOB" SelectMethod="GetListView" EnablePaging="true"
                        SelectCountMethod="SelectCountView" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                        SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="and ExamPlaceAllot.ExamPlaceAllotID=0" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
                <div style="width: 95%; margin: 10px auto; text-align: center; clear: left;">
                    <asp:Button ID="ButtonPrint" runat="server" Text="批量打印" CssClass="bt_maxlarge" OnClick="ButtonPrint_Click" Visible="false" />
                    &nbsp;&nbsp;

                         <asp:Button ID="ButtonOutput" runat="server" Text="批量导出" CssClass="bt_maxlarge" OnClick="ButtonOutput_Click"/>
                </div>
                <asp:HiddenField ID="HiddenFieldOutputOk" runat="server" Value="" EnableViewState="true" />
            </div>
            <br />
        </div>

        <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Enabled="false" Interval="10000"
            EnableViewState="true">
        </asp:Timer>
        <asp:Timer ID="Timer2" runat="server" Enabled="false" Interval="7000" OnTick="Timer2_Tick" EnableViewState="true"></asp:Timer>
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
    </div>
</asp:Content>
