<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="AcceptLineSet.aspx.cs" Inherits="ZYRYJG.EXamManage.AcceptLineSet" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <%-- <script type="text/javascript">
          function InitiateAjaxRequest(arguments) {
              var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");

              ajaxManager.ajaxRequest(arguments, '');
          }
        </script>--%>
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
        <AjaxSettings>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" />
    <div class="div_out">
        <div class="dqts">
        <div style="float:left;">
            当前位置 &gt;&gt; 考务管理 &gt;&gt;考试管理
            &gt;&gt; <strong>合格线管理</strong>&gt;&gt; <strong>设定合格线</strong>
        </div>
        </div>
        <div class="table_border" style="width: 95%; margin: 5px auto;">
            <div class="content">
                <p class="jbxxbt">
                    考试合格线设定</p>
              
                    <table cellpadding="5" cellspacing="1" border="0" width="95%" class="table" align="center">
                        <tr class="GridLightBK">
                            <td width="15%" align="right" nowrap="nowrap">
                                <strong>岗位名称：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelPostTypeName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">
                                <strong>工种名称：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelPostName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">
                                <strong>考试日期：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelExamDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="right" nowrap="nowrap">
                                <strong>参加考试人数：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelExamerCount" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">
                                <strong>合格线分数：</strong>
                            </td>
                            <td>
                                <asp:PlaceHolder ID="PlaceHolderPassLine" runat="server"></asp:PlaceHolder>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">
                                <strong>合格人数：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelPassCount" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">
                                <strong>合格率：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelPassPercent" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
             
                <div style="width: 95%; margin: 10px auto; font-size: 16px; color:#7A0101; text-align:left;line-height:200% ">
                    <strong>说明：</strong>输入合格线分数后，移开输入焦点，程序将自动计算合格人数、合格率。<br />请确保导入考试成绩后在设定合格线，设定合格线后无法再修改成绩（导入成绩）。<br />
                    合格线分数背景变为绿色表示该科目已经设置了合格线。<br />
                    <div id="div_tip" runat="server" style="width: 95%;  color: Red;
                         text-align: center; padding-left: 20px;">
                    </div>
                </div>
                <div style="width: 95%; margin: 10px auto; text-align: center;">
                    <asp:Button ID="ButtonSetPassline" runat="server" Text="设定分数线" CssClass="bt_large"
                        OnClick="ButtonSetPassline_Click" />
                    &nbsp;&nbsp;
                    <input type="button" class="bt_large" onclick="location='AcceptLineManage.aspx?o=<%=Request["o"] %>';"
                        value="返 回" />
                    
                </div>
            </div>
        </div>
    </div>
</asp:Content>
