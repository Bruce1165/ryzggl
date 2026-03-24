<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ApplyView.aspx.cs" Inherits="ZYRYJG.RenewCertifates.ApplyView" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <div class="div_out">
         <div id="div_top" class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 证书续期 &gt;&gt;
                <strong>查看续期申请表</strong>
            </div>
        </div>
        <div class="table_border" style="width: 21cm; margin: 5px auto;">
            <div class="content" id="div_content">
                <div class="jbxxbt">
                    续期申请表
                </div>
                <div style="width: 95%; margin: 0 auto; padding: 5px;" runat="server" id="divExamSignUp">
                    <div style="float: right; padding-right: 30px;">
                        申请批号:<asp:Label ID="LabelApplyCode" runat="server" Text=""></asp:Label></div>
                    <table width="95%" border="0" cellpadding="5" cellspacing="1" class="table" align="center">
                        <tr class="GridLightBK">
                            <td width="7%" nowrap="nowrap" align="center">
                                姓名
                            </td>
                            <td width="31%">
                                <asp:Label ID="LabelWorkerName" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="center" width="9%">
                                <nobr>性别</nobr>
                            </td>
                            <td width="9%">
                                <asp:Label ID="LabelSex" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="center" width="7%">
                                出生日期
                            </td>
                            <td width="19%">
                                <asp:Label ID="LabelBirthday" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="110px" rowspan="3" align="center">
                                <img id="ImgCode" runat="server" height="140" width="110" src="~/Images/photo_ry.jpg"
                                    alt="一寸照片" />
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="center">
                                证件号码
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelWorkerCertificateCode" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="center" nowrap="nowrap">
                                联系电话
                            </td>
                            <td>
                                <asp:Label ID="LabelPhone" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="center">
                                岗位工种
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelPostName" runat="server" Text=""></asp:Label>
                            </td>
                            <td nowrap="nowrap" align="center">
                                &nbsp;&nbsp;&nbsp;&nbsp;文化程度&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="LabelCulturalLevel" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="center">
                                技术职称<br />
                                或技术等级
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelSKILLLEVEL" runat="server" Text=""></asp:Label>
                            </td>
                            <td nowrap="nowrap" align="center">
                                从事本岗位<br />
                                工作的时间
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelWorkStartDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="center">
                                  <asp:Label ID="LabelUnit" runat="server" Text="聘用单位名称"></asp:Label>
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelUnitName" runat="server" Text=""></asp:Label>
                            </td>
                            <td nowrap="nowrap" align="center">
                                 <asp:Label ID="LabelCode" runat="server" Text=" 组织机构代码"></asp:Label>
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelUnitCode" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                          <tr class="GridLightBK" runat="server" id="trNewUnit" visible="false">
                            <td nowrap="nowrap" align="center">
                                现单位名称
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelNewUnitName" runat="server" Text=""></asp:Label>
                            </td>
                            <td nowrap="nowrap" align="center">
                                现单位组织机构代码
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelNewUnitCode" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td nowrap="nowrap" align="center">
                                证书编号
                            </td>
                            <td colspan="3">
                                <asp:Label ID="LabelCertificateCode" runat="server" Text="" Font-Bold="true"></asp:Label><asp:Label ID="LabelPrintCount" runat="server" Text="" Font-Bold="true" ForeColor="Red"></asp:Label>
                            </td>
                            <td nowrap="nowrap" align="center">
                                有效期至
                            </td>
                            <td colspan="2">
                                <asp:Label ID="LabelValidDataTo" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                    </table>
                   
                </div>
                <div style="width: 95%; margin: 10px auto; text-align: center;">
                 <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript:hideIfam();" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
