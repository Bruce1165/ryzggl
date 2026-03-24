<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="BlackListEdit.aspx.cs" Inherits="ZYRYJG.EXamManage.BlackListEdit" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Sunset" EnableShadow="true">
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
                <div class="jbxxbt">
                    报考黑名单</div>
                <div class="blue_center" style="width: 98%; margin: 8px auto;">
                    <div>
                        <b class="subxtop"><b class="subxb1"></b><b class="subxb2"></b><b class="subxb3"></b>
                            <b class="subxb4"></b></b>
                    </div>
                    <div class="subxboxcontent">
                        <br />
                        <table id="TdEdit" runat="server" width="90%" cellpadding="2" cellspacing="0" border="0"
                            align="center">
                            <tr>
                                <td align="right" width="11%" nowrap="nowrap">
                                    <font color="red">*</font>岗位工种：
                                </td>
                                <td width="39%" align="left">
                                    <uc1:PostSelect ID="PostSelect1" runat="server" />
                                </td>
                                <td align="right" width="11%" nowrap="nowrap">
                                    <font color="red">*</font>开始生效时间：
                                </td>
                                <td width="39%" align="left">
                                    <telerik:RadDatePicker ID="RadDatePickerStartTime" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="RadDatePickerStartTime"
                                        ForeColor="Red" runat="server" ErrorMessage="必填" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="11%" nowrap="nowrap">
                                    <font color="red">*</font>姓名：
                                </td>
                                <td width="39%" align="left">
                                    <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" MaxLength="100" Width="85%">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="RadTextBoxWorkerName"
                                        ErrorMessage="必填"></asp:RequiredFieldValidator>
                                </td>
                                <td align="right" width="11%" nowrap="nowrap">
                                    <font color="red">*</font>证件号码：
                                </td>
                                <td width="39%" align="left">
                                    <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" MaxLength="100"
                                        Width="85%">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="RadTextBoxCertificateCode"
                                        ErrorMessage="必填"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="11%" nowrap="nowrap">
                                    <font color="red">*</font>黑名单类型：
                                </td>
                                <td width="39%" align="left">
                                    <telerik:RadComboBox ID="RadComboBoxBlackType" runat="server" Skin="Office2007" CausesValidation="False"
                                        ExpandAnimation-Duration="0">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="请选择" Value="" Selected="true" />
                                            <telerik:RadComboBoxItem Text="替人考试" Value="替人考试" />
                                            <telerik:RadComboBoxItem Text="虚假申报" Value="虚假申报" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxBlackType"
                                        ErrorMessage="必填" runat="server" ID="Comparevalidator1" ForeColor="Red" Display="Dynamic" />
                                </td>
                                <td align="right" width="11%" nowrap="nowrap">
                                    <font color="red">*</font>状态：
                                </td>
                                <td width="39%" align="left">
                                    <telerik:RadComboBox ID="RadComboBoxBlackStatus" runat="server" Skin="Office2007"
                                        CausesValidation="False" ExpandAnimation-Duration="0">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="请选择" Value="" />
                                            <telerik:RadComboBoxItem Text="有效" Value="有效" Selected="true" />
                                            <telerik:RadComboBoxItem Text="失效" Value="失效" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxBlackStatus"
                                        ErrorMessage="必填" runat="server" ID="Comparevalidator2" ForeColor="Red" Display="Dynamic" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tousu_content" nowrap="nowrap">
                                    单位名称：
                                </td>
                                <td align="left">
                                    <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="97%" Skin="Default"
                                        MaxLength="100" >
                                    </telerik:RadTextBox>
                                </td>
                                <td align="right" width="11%" nowrap="nowrap">
                                    培训点：
                                </td>
                                <td width="39%" align="left">
                                    <telerik:RadTextBox ID="RadTextBoxTrainUnitName" runat="server" MaxLength="128" Width="85%">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" width="11%" nowrap="nowrap">
                                    备注：
                                </td>
                                <td width="39%" align="left" colspan="3">
                                    <telerik:RadTextBox ID="RadTextBoxRemark" runat="server" MaxLength="4000" Width="90%"
                                        Rows="3" TextMode="MultiLine">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" align="center" style="padding-top:15px;">
                                    <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="button" OnClick="btnSave_Click" />&nbsp;
                                    <asp:Button ID="ButtonSaveAs" runat="server" Text="另存为新数据" CssClass="bt_large" OnClick="ButtonSaveAs_Click" />&nbsp;
                                    <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript:hideIfam();" />
                                </td>
                            </tr>
                        </table>
                        <br />
                    </div>
                    <div>
                        <b class="subxbottom"></b><b class="subxb4"></b><b class="subxb3"></b><b class="subxb2">
                        </b><b class="subxb1"></b>
                    </div>
                </div>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
