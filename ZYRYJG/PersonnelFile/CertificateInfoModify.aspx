<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/RadControls.Master"
    CodeBehind="CertificateInfoModify.aspx.cs" Inherits="ZYRYJG.PersonnelFile.CertificateInfoModify" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">

            function showImg(radUpload, eventArgs) {
                var input = eventArgs.get_fileInputField();
                var inputs = radUpload.getFileInputs();
                var ImgCode = document.getElementById("<%=ImgCode.ClientID %>");
                for (i = 0; i < inputs.length; i++) {
                    ImgCode.src = inputs[i].value;
                    break;
                }
            }

        </script>

    </telerik:RadCodeBlock>
    <div class="div_out">
        <div id="div_top" class="dqts">
            <div id="divRoad" runat="server" style="float: left;">
                当前位置 &gt;&gt; 证书信息修正
            </div>
        </div>
        <div class="table_border" style="width: 98%; margin: 5px auto;">
            <div class="content">

                <p class="jbxxbt" style="text-align: center">
                    证书信息修正
                </p>
                <table cellpadding="5" cellspacing="1" border="0" width="95%" class="table" align="center">
                    <tr class="GridLightBK">
                        <td width="9%" align="right" nowrap="nowrap">
                            <strong>岗位类别：</strong>
                        </td>
                        <td width="30%">
                            <asp:Label ID="LabelPostTypeID" runat="server" Text=""></asp:Label>
                        </td>
                        <td width="9%" align="right" nowrap="nowrap">
                            <strong>岗位工种：</strong>
                        </td>
                        <td>
                            <asp:Label ID="LabelPostID" runat="server" Text=""></asp:Label>
                        </td>
                        <td width="110px" rowspan="4" align="center" nowrap="nowrap">
                            <img id="ImgCode" runat="server" height="140" width="110" src="~/Images/photo_ry.jpg"
                                alt="一寸照片" />
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right" nowrap="nowrap">
                            <strong>证书编号：</strong>
                        </td>
                        <td>
                            <asp:Label ID="LabelCertificateCode" runat="server" Text=""></asp:Label>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <strong>最新业务状态：</strong>
                        </td>
                        <td>
                            <asp:Label ID="LabelStatus" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right" nowrap="nowrap">
                            <span style="color: Red">*</span><strong>姓 名：</strong>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="95%" Skin="Default"
                                MaxLength="50">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadTextBoxWorkerName" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:HiddenField ID="HiddenFieldPhone" runat="server" />
                        </td>
                        <td align="right" nowrap="nowrap">
                            <span style="color: Red">*</span><strong>证件号码：</strong>
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBoxWorkerCertificateCode" runat="server" Width="300px"
                                Skin="Default" MaxLength="18">
                            </telerik:RadTextBox>
                            <asp:Button ID="ButtonCheckIDCard" runat="server" Text="校验" CssClass="button" OnClick="ButtonCheckIDCard_Click" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadTextBoxWorkerCertificateCode" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right" nowrap="nowrap">
                            <span style="color: Red">*</span><strong>出生日期：</strong>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="RadDatePickerBirthday" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="200px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorBirthday" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadDatePickerBirthday" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <span style="color: Red">*</span><strong>性 别：</strong>
                        </td>
                        <td>
                            <asp:RadioButton ID="RadioButtonMan" runat="server" Text="男" GroupName="3" Checked="true" />&nbsp;<asp:RadioButton
                                ID="RadioButtonWoman" Text="女" GroupName="3" runat="server" />
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td colspan="5">
                            <table width="95%" id="TableUploadPhoto" runat="server">
                                <tr>
                                    <td align="left" style="padding-left: 20px; line-height: 20px;" colspan="3">
                                        <span id="SpanTip" runat="server" style="font-size: 16px;">附件-照片：（格式要求：一寸jpg格式图片，名称不限，最大为50K，宽高110 X 140像素）</span> &nbsp;&nbsp;&nbsp;&nbsp;辅助工具下载：<a
                                            href="../Images/1寸照片生成器.exe" style="text-decoration: underline; color: Blue;"><img
                                                alt="" src="../Images/Soft_common.gif" style="border-width: 0;" />
                                            1寸照片生成器.exe</a>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" style="font-size: 13px; font-weight: bold;">上传照片：
                                    </td>
                                    <td align="left" style="width: 120px">
                                        <telerik:RadUpload ID="RadUploadFacePhoto" runat="server" ControlObjectsVisibility="None"
                                            Height="23px" MaxFileInputsCount="1" OverwriteExistingFiles="True" Skin="Hot"
                                            EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" ReadOnlyFileInputs="False"
                                            Width="300px" OnClientFileSelected="showImg">
                                            <Localization Select="选 择" />
                                        </telerik:RadUpload>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right" nowrap="nowrap">
                            <span style="color: Red">*</span><strong>单位全称：</strong>
                        </td>
                        <td colspan="4">
                            <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="95%" Skin="Default"
                                MaxLength="100">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorRadTextBoxUnitName" runat="server"
                                ErrorMessage="必填" ControlToValidate="RadTextBoxUnitName" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right" nowrap="nowrap">
                            <span style="color: Red">*</span><strong>组织机构代码：</strong>
                        </td>
                        <td colspan="4">
                            <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="150px" Skin="Default"
                                MaxLength="9">
                            </telerik:RadTextBox>
                            <asp:Button ID="ButtonCheckUnitcode" runat="server" Text="校验" CssClass="button" OnClick="ButtonCheckUnitcode_Click" />（9位，不带“-”）
                                <asp:RequiredFieldValidator ID="RequiredFieldValidatorRadTextBoxUnitCode" runat="server"
                                    ErrorMessage="必填" ControlToValidate="RadTextBoxUnitCode" Display="Dynamic"></asp:RequiredFieldValidator>
                            &nbsp;<a title="组织机构代码查询" href="https://www.cods.org.cn" target="_blank" style="color: Blue; text-decoration: underline;">组织机构代码查询网站</a>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right" nowrap="nowrap">
                            <strong>职 务：</strong>
                        </td>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxJob" runat="server" Width="95%" NoWrap="true">
                            </telerik:RadComboBox>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <strong>技术职称（或技术等级）：</strong>
                        </td>
                        <td colspan="2">
                            <telerik:RadComboBox ID="RadComboBoxSKILLLEVEL" runat="server" Width="95%" NoWrap="true">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right" nowrap="nowrap">
                            <span style="color: Red">*</span><strong>首次发证时间：</strong>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="RadDatePickerConferDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="200px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadDatePickerConferDate" Display="Dynamic"></asp:RequiredFieldValidator>


                        </td>
                        <td align="right" nowrap="nowrap">发证机关：
                        </td>
                        <td colspan="2">
                            <asp:Label ID="LabelConferUnit" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right" nowrap="nowrap">
                            <span style="color: Red">*</span><strong>有效期起始：</strong>
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="RadDatePickerValidStartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="200px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadDatePickerValidStartDate" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <span style="color: Red">*</span><strong>有效期截止：</strong>
                        </td>
                        <td colspan="2">
                            <telerik:RadDatePicker ID="RadDatePickerValidEndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="200px">
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadDatePickerValidEndDate" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="GridLightBK" id="TR_Remark" runat="server">
                        <td align="left" colspan="5">
                            <strong>备注（历史数据）：</strong>
                            <span id="P_Remark" runat="server" style="line-height: 20px;"></span>
                        </td>
                    </tr>
                </table>

                <br />
                <div style="width: 95%; margin: 10px auto; text-align: center;">
                    <asp:Button ID="ButtonModify" runat="server" Text="保 存" CssClass="button" OnClick="ButtonModify_Click" />
                    <asp:Button ID="ButtonFH" runat="server" Text="返 回" CssClass="button" OnClick="ButtonFH_Click"
                        CausesValidation="false" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
