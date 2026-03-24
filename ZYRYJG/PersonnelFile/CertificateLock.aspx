<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/RadControls.Master"
    CodeBehind="CertificateLock.aspx.cs" Inherits="ZYRYJG.PersonnelFile.CertificateLock" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <div class="div_out">
        <div id="div_top" class="dqts">
            <div id="divRoad" runat="server" style="float: left;">
            </div>
        </div>
        <div class="table_border" style="width: 98%; margin: 1% auto; padding: 15px 0px;">
            <div class="jbxxbt">
                证书加锁</div>
            <div class="content">
                <div id="DivContent">
                    <table runat="server" id="TableDetail" cellpadding="5" cellspacing="1" border="0"
                        width="100%" class="table" align="center">
                        <tr class="GridLightBK">
                            <td align="right" nowrap="nowrap" style="width: 14%">
                                <strong>证书编号</strong>
                            </td>
                            <td align="left" style="width: 36%">
                                <asp:Label ID="LabelCertificateCode" runat="server" Text=""></asp:Label>
                            </td>
                            <td align="right" nowrap="nowrap" style="width: 14%">
                                <strong>姓名</strong>
                            </td>
                            <td align="left" style="width: 36%">
                                <asp:Label ID="LabelWorkerName" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="right" nowrap="nowrap">
                                <strong>加锁人</strong>
                            </td>
                            <td align="left">
                                <asp:Label ID="LabelLockPerson" runat="server" Text=""></asp:Label>
                            </td>
                            <td nowrap="nowrap" align="right">
                                <strong>加锁日期</strong>
                            </td>
                            <td align="left">
                                <asp:Label ID="LabelLockTime" runat="server" Text=""></asp:Label>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="right" nowrap="nowrap">
                                <strong>锁定截止日期</strong>
                            </td>
                            <td align="left" colspan="3">
                                <telerik:RadDatePicker ID="RadDatePickerLockEndTime" MinDate="01/01/2012" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="200px" />
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorLockEndTime" runat="server"
                                    ErrorMessage="必填" ControlToValidate="RadDatePickerLockEndTime" Display="Dynamic"></asp:RequiredFieldValidator>
                                <span style="color: #999999">（到期后锁定无效）</span>
                            </td>
                        </tr>
                        <tr class="GridLightBK">
                            <td align="right" nowrap="nowrap" width="11%" valign="top">
                                <strong>锁定原因说明</strong>
                            </td>
                            <td align="left" colspan="3">
                                <telerik:RadTextBox ID="RadTextBoxRemark" runat="server" Width="95%" Skin="Default"
                                    MaxLength="3000"  TextMode="MultiLine" Rows="3">
                                </telerik:RadTextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                    ControlToValidate="RadTextBoxRemark" Display="Dynamic"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
                <div style="width: 95%; margin: 10px auto; text-align: center;">
                    <asp:Button ID="ButtonLock" runat="server" Text="确 定" CssClass="button" OnClick="ButtonLock_Click" />
                    &nbsp;
                    <asp:Button ID="ButtonCancel" runat="server" Text="取 消" CssClass="button" OnClick="ButtonCancel_Click"
                        CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
