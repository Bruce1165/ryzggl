<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.master" AutoEventWireup="true" CodeFile="ExamStep.aspx.cs" Inherits="Student_ExamStep" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ButtonSave">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DivTip" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="DivQuestion" />
                    <telerik:AjaxUpdatedControl ControlID="div_grid" />
                    <telerik:AjaxUpdatedControl ControlID="RadGridQuestOption" />
                    <telerik:AjaxUpdatedControl ControlID="ButtonSave" />
                    <telerik:AjaxUpdatedControl ControlID="DivQuestionAnswerTip" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadWindowManager ID="Singleton" Skin="Sunset" Width="400" Height="430" VisibleStatusbar="false"
        Behaviors="Close,Move, Resize" runat="server">
        <AlertTemplate>
            <div class="alertText">
                {1}
            </div>
            <div class="confrimButton">
                <input onclick="$find('{0}').close();" class="button" id="ButtonOK" type="button"
                    value="确 定" />
            </div>
        </AlertTemplate>
        <ConfirmTemplate>
            <div class="confrimText">
                {1}
            </div>
            <div class="confrimButton">
                <input onclick="$find('{0}').close(true);" class="button" id="ButtonOK" type="button"
                    value="确 定" />&nbsp;&nbsp;
                <input onclick="$find('{0}').close(false);" class="button" id="ButtonCancel" type="button"
                    value="取 消" />
            </div>
        </ConfirmTemplate>
    </telerik:RadWindowManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Sunset">
    </telerik:RadAjaxLoadingPanel>
    <div class="div_main" style="padding: 8px 8px;">
        <div id="div_top" class="div_mainTop">
            <div class="div_road">
            </div>
        </div>
        <div class="div_fun">
            在线测评
        </div>
        <div class="content">
            <div id="showData" style="width: 100%; line-height: 20px; text-align: left; padding-left: 20px; font-size: 16px;">
                &nbsp;
            </div>
            <div id="DivTip" class="DivContent" style="text-align: left; font-size: 16px;" runat="server">
            </div>
            <div runat="server" style="margin: 5px 10px; padding: 5px 20px;">
                <div id="DivQuestion" runat="server" style="text-align: left; padding-left: 10px; font-size: 18px; line-height: 150%;">
                </div>
                <telerik:RadGrid ID="RadGridQuestOption" runat="server" AutoGenerateColumns="False"
                    GridLines="None" AllowPaging="false" PageSize="10" EnableAjaxSkinRendering="true"
                    EnableEmbeddedSkins="false" Width="100%" ShowHeader="false">
                    <ClientSettings EnableRowHoverStyle="false">
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="None" DataKeyNames="QuestOptionID,OptionNo" NoMasterRecordsText="　没有可显示的记录">
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="Highlight <br/> ship name">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" Width="20" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="Highlight <br/> ship name">
                                <ItemTemplate>
                                    <%# Eval("OptionNo")%>、<%# Eval("OptionContent")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Font-Size="18px" />
                            </telerik:GridTemplateColumn>
                        </Columns>
                    </MasterTableView>
                </telerik:RadGrid>
            </div>
            <br />
            <asp:Button ID="ButtonSave" runat="server" Text="开始考试" CssClass="bt_large" OnClick="ButtonSave_Click" />&nbsp;&nbsp;
                <div id="DivQuestionAnswerTip" runat="server" style="margin: 30px 30px; text-align: center; line-height: 200%; color: red; font-size: 16px; font-weight: bold;">
                </div>
        </div>
    </div>
    <uc1:IframeView ID="IframeView" runat="server" />

    <script type="text/javascript">

        var interval = 1000;
        function ShowCountDown(year, month, day, _hour, _minute, _second, divname) {
            var now = new Date();
            var endDate = new Date(year, month, day, _hour, _minute, _second);
            var leftTime = endDate.getTime() - now.getTime();
            if (endDate < now) leftTime = 0;

            var leftsecond = parseInt(leftTime / 1000);
            var day1 = Math.floor(leftsecond / (60 * 60 * 24));
            var hour1 = Math.floor((leftsecond - day1 * 24 * 60 * 60) / 3600);
            var hour = Math.floor((leftsecond - 60 * 60) / 3600);
            var cc = document.getElementById(divname);

            //如果小时为负数 显示0 
            if (hour > 0) {
            }
            else {
                hour = 0;
            }
            if (day1 < 0) {
                hour = hour1
            }
            var minute = Math.floor((leftsecond - day1 * 24 * 60 * 60 - hour1 * 3600) / 60);
            var second = Math.floor(leftsecond - day1 * 24 * 60 * 60 - hour1 * 3600 - minute * 60);

            //如果结束时间为负数 就显示0 
            if (endDate >= now) {
                cc.innerHTML = "剩余时间：<span style=\"color:red; font-size:18px;\">" + hour1 + "</span> 时<span style=\"color:red; font-size:18px;\">" + minute + "</span> 分<span style=\"color:red; font-size:18px;\">" + second + "</span> 秒";
            }
            else {
                cc.innerHTML = 0 + "小时" + 0 + "分" + 0 + "秒";
            }
        }
    </script>
</asp:Content>

