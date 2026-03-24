<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ScoreDetail.aspx.cs" Inherits="ZYRYJG.EXamManage.ScoreDetail" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc1" %>
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
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt;  <strong>考试成绩详细</strong>
            </div>
        </div>
        <div class="table_border" style="width: 80%; margin: 5px auto;">
            <div class="content">
                <div id="DivContent">
                    <p class="jbxxbt" style="text-align: center">
                        考试成绩详细</p>
                    <table cellpadding="5" cellspacing="1" border="0" width="85%" class="table" align="center">
                        <tr class="GridLightBK">
                            <td width="20%" align="right" nowrap="nowrap">
                                <strong>姓名：</strong>
                            </td>
                            <td width="70%">
                                <asp:Label ID="LabelWorkerName" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="102" rowspan="5" align="center" valign="middle">
                                <asp:Image ID="ImageFaceImg" runat="server" BorderWidth="0" Height="140" Width="102"
                                    AlternateText="免冠照片" ImageUrl="~/Images/photo_ry.jpg" />
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">
                                <strong>岗位类别：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelPostTypeName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">
                                <strong>岗位工种：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelPostName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">
                                <strong>准考证号：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelExamCardID" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="right" nowrap="nowrap">
                                <strong>证件号码：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelCertificateCode" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">
                                <strong>考点名称：</strong>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelExamPlaceName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">
                                <strong>考场号：</strong>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelExamRoomCode" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="right">
                                <strong>考试日期：</strong>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelExamDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </table>
                </div>
                <div style="width: 95%; margin: 10px auto; text-align: center;">
                    <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript:hideIfam();" />
                   <br />
                </div>
            </div>
        </div>
    </div>

</asp:Content>
