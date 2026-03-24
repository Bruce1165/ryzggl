<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ExamCard.aspx.cs" Inherits="ZYRYJG.EXamManage.ExamCard" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
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
        <div  id="div_top" class="dqts">
            <div id="divRoad" runat="server" style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;考场管理
                &gt;&gt; <strong>准考证管理</strong>
            </div>
        </div>
        <div class="table_border" style="width: 80%; margin: 5px auto;">
            <div class="content">
                <div id="DivContent">
                    <p  style="text-align: center;font-size: 30px;font-weight:bold">
                        准考证</p>
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
                                <strong>工作单位：</strong>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelUnitName" runat="server" Text=""></asp:Label>
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
                    <asp:Button ID="ButtonExport" runat="server" Text="导出打印" CssClass="bt_large" OnClick="ButtonExport_Click" />&nbsp;&nbsp;
                    <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript: hideIfam();" />
                    <br />
                </div>
                <div style="width:80%">
                    <div style="float:left;text-align:right;">
                        <p style="font-size: 20px;font-weight: bold;text-align:center">微信公众号</p>
<p style="margin:0px;padding:0px;"> <img src="../Images/1616028088.jpg" width="161px" height="161px" alt="二维码"/>
<p style="margin:0px;color:#0000ff;text-align:center;">安居北京</p></p>
                    </div>
                    <div style="width:49%;float:left;font-size: 20px;text-align:left; line-height:180%;color:orangered; margin:80px 40px;vertical-align:middle">
                         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;因不确定因素，考试计划可能做出相应调整，请考生关注“安居北京”微信公众号并及时关注最新通知。
                    </div>
                    <div style="clear:both;"></div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
