<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertificateInfo.aspx.cs" Inherits="ZYRYJG.EXamManage.CertificateInfo" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script language="javascript" type="text/javascript">
            //触发按钮事件
            function EnterKeyClick(e) {
                if (!e) {
                    e = window.event;
                }

                if (e.keyCode == 13) {
                    e.keyCode = 9;
                    e.returnValue = false;

                    if ($find("<%= RadTextBoxCertificateCode.ClientID %>").get_value() == "") return;
                    if (document.getElementById("<%=HiddenFieldCertificateCode.ClientID %>").value == $find("<%= RadTextBoxCertificateCode.ClientID %>").get_value()) {
                        document.getElementById("<%=ButtonPrint.ClientID %>").click(); //打印
                    }
                    else {
                        document.getElementById("<%=ButtonSearch.ClientID %>").click(); // 查询
                    }
                }
            }
            function Blur(sender, eventArgs) {
                $find("<%= RadTextBoxCertificateCode.ClientID %>").focus();
            }
            function Focus(sender, eventArgs) {
                var txt = $find("<%= RadTextBoxCertificateCode.ClientID %>");
                txt.set_caretPosition(txt.get_value().length);
            }
            function OnClientSelectedIndexChanged(sender, eventArgs) {
                document.getElementById("<%=HiddenFieldCertificateCode.ClientID %>").value = "";
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <div class="div_out" onkeydown="EnterKeyClick(event);">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 证书打印
            </div>
        </div>
        <div class="table_border" style="width: 98%; margin: 1px auto;">
            <div class="content">
                <div id="DivContent">
                    <span class="jbxxbt" style="text-align: center">证书打印</span>
                    <div style="width: 95%; padding: 5px 0px 5px 2px;" align="center"">
                        <div style="color: #4F6F99; line-height: 28px; text-align: left;">
                            提示：输入证书编号后几位数字，按回车键即可查到所要证书，再按一次回车即为打印！
                        </div>
                        <div style="float: left; line-height: 28px;">
                            <b>岗位类别：</b></div>
                        <div style="float: left;">
                            <uc1:PostSelect ID="PostSelect1" runat="server" />
                        </div>
                        <div style="float: left; line-height: 28px; padding-left: 8px;">
                            <b>证书编号：</b></div>
                        <div style="float: left;">
                            <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="140px" Skin="Default"
                                ClientEvents-OnBlur="Blur" ClientEvents-OnFocus="Focus" MaxLength="50">
                            </telerik:RadTextBox><asp:HiddenField ID="HiddenFieldCertificateCode" runat="server" />
                            &nbsp;<asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </div>
                        <p id="p_tip" runat="server" visible="false" style="color: Red; font-weight: bold;">
                            没有查到相关数据！</p>
                    </div>
                    <table cellpadding="5" cellspacing="1" border="0" width="95%" class="table" align="center">
                        <tr class="GridLightBK">
                            <td align="right" nowrap="nowrap" style="width: 15%">
                                <strong>姓 名：</strong>
                            </td>
                            <td style="width: 30%">
                                <asp:Label ID="LabelWorkerName" runat="server" Text=""></asp:Label>
                            </td>
                            <td width="15%" align="right" nowrap="nowrap">
                                <strong>企业名称：</strong>
                            </td>
                            <td width="40%">
                                <asp:Label ID="LabelUnitName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="right" nowrap="nowrap">
                                <strong>性 别：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelSex" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right" nowrap="nowrap">
                                <strong>岗位类别：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelPostTypeID" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="right" nowrap="nowrap">
                                <strong>出生日期：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelBirthday" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right" nowrap="nowrap">
                                <strong>岗位工种：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelPostID" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="right" nowrap="nowrap">
                                <strong>证件号码：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelWorkerCertificateCode" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right" nowrap="nowrap">
                                <strong>证书编号：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelCertificateCode" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="right" nowrap="nowrap">
                                <strong>发证时间：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelConferDate" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right" nowrap="nowrap">
                                <strong>有效期至：</strong>
                            </td>
                            <td>
                                <asp:Label ID="LabelValidDate" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="left" colspan="4">
                                <strong>备注（历史数据）：</strong>
                                <p id="P_Remark" runat="server" style="line-height: 20px;">
                                </p>
                            </td>
                        </tr>
                    </table>
                </div>
        
                <div style="width: 95%; margin: 10px auto; text-align: center;">
                    <p id="p_tzzy" runat="server" visible="false">
                        请选择打印<asp:RadioButton ID="RadioButtonZB" runat="server" Text="正本" Checked="true"
                            GroupName="t" />
                        &nbsp;<asp:RadioButton ID="RadioButtonFB" runat="server" Text="副本" GroupName="t" />
                    </p>
                    <div runat="server" id="P_PrintTimeSpan" style="width: 85%; padding-bottom: 8px;">
                        <div style="float: left;">
                            <asp:CheckBox ID="CheckBoxAutoPrint" runat="server" Text="自动完成批量打印" TextAlign="Left" />&nbsp;&nbsp;&nbsp;时间间隔：
                        </div>
                        <div style="float: left;">
                            <asp:RadioButtonList ID="RadioButtonListPrintTimeSpan" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="10秒" Value="10"></asp:ListItem>
                                <asp:ListItem Text="15秒" Value="15"></asp:ListItem>
                                <asp:ListItem Text="20秒（默认）" Value="20" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="25秒" Value="25"></asp:ListItem>
                                <asp:ListItem Text="30秒" Value="30"></asp:ListItem>
                                <asp:ListItem Text="40秒" Value="40"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <asp:Button ID="ButtonPrev" runat="server" Text="上一条" CssClass="button" Visible="False"
                        OnClick="ButtonPrev_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="ButtonPrint" runat="server" Text="打 印" CssClass="button" OnClick='ButtonPrint_Click'
                        ToolTip="提示：直接按回车键（Enter）也可打印" />
                    &nbsp;&nbsp;
                    <asp:Button ID="ButtonNext" runat="server" Text="下一条" CssClass="button" Visible="False"
                        OnClick="ButtonNext_Click" />
                    &nbsp;&nbsp;
                    <asp:Button ID="ButtonReturn" runat="server" Text="返 回" CssClass="button" OnClick="ButtonReturn_Click" />
                </div>
                <div style="width: 95%; margin: 10px auto; text-align: center;">
                    <asp:Label ID="LabelPage" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                </div>
                <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Enabled="false" Interval="10000"
                    EnableViewState="true">
                </asp:Timer>
            </div>
        </div>
    </div>
</asp:Content>
