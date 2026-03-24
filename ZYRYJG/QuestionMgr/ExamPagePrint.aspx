<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ExamPagePrint.aspx.cs" Inherits="ZYRYJG.QuestionMgr.ExamPagePrint" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" />
    <div class="div_out">
        <div id="div_top" class="dqts">
        </div>
        <div class="table_border" style="width: 98%; margin: 5px auto;">
            <div class="content">
                <p class="jbxxbt">
                    试卷预览</p>
                    <div class="floatDiv" style="width: 98%; margin: 0 auto; font-size: 12px; padding-left: 20px; font-weight: bold;
                    text-align: left; line-height: 30px; ">
                    <asp:Button ID="Button1" runat="server" Text="导出试卷" CssClass="bt_large" OnClick="ButtonPrint_Click" />&nbsp;&nbsp; <asp:Button ID="ButtonAnwser" runat="server" Text="导出答案" CssClass="bt_large" OnClick="ButtonAnwser_Click" />&nbsp;&nbsp;   <input id="Button2" type="button" value="返 回" class="bt_large" onclick="javascript:hideIfam();" />
                        <asp:HiddenField ID="HiddenFieldCreateTime" runat="server" />
                </div>
                <div class="DivContent" style="width: 98%; margin: 0 auto; padding: 20px 8px 20px 8px;"
                    id="divExamPage" runat="server">
                </div>
                <div class="floatDiv" style="width: 98%; margin: 0 auto; font-size: 12px; padding-left: 20px; font-weight: bold;
                    text-align: left; line-height: 30px; margin:20px 0; ">
                    <asp:Button ID="ButtonPrint" runat="server" Text="导出Word" CssClass="bt_large" OnClick="ButtonPrint_Click" />&nbsp;&nbsp; <asp:Button ID="Button3" runat="server" Text="导出答案" CssClass="bt_large" OnClick="ButtonAnwser_Click" />&nbsp;&nbsp;   <input id="ButtonReturn" type="button" value="返 回" class="bt_large" onclick="javascript:hideIfam();" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
