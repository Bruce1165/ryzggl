<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertificateAdd.aspx.cs" Inherits="ZYRYJG.EXamManage.CertificateAdd" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="PostSelect1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PostSelect1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadTextCertificateCode">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="tableEdit" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadTextBoxUnitCode">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadTextBoxUnitName" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RequiredFieldValidatorRadTextBoxUnitCode" />
                    <telerik:AjaxUpdatedControl ControlID="RequiredFieldValidatorRadTextBoxUnitName" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadTextBoxUnitName">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadTextBoxUnitCode" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="RequiredFieldValidatorRadTextBoxUnitCode" />
                    <telerik:AjaxUpdatedControl ControlID="RequiredFieldValidatorRadTextBoxUnitName" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">

            function RadioButtonChange() {
                var tbox = $find('<%=RadTextBoxCertificateCode.ClientID %>');
                if (document.getElementById('<%=RadioButtonAutoAllocateNo.ClientID %>').checked == true) {
                    tbox.disable();
                    tbox.set_value('自动编号');
                }
                else {
                    tbox.enable();
                    tbox.set_value('');
                }
            }
        </script>

    </telerik:RadCodeBlock>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;证书制作
                &gt;&gt; 证书补登记
            </div>
        </div>
        <p style="padding-left: 20px; text-align: left;">
            <a href='CertificateAddList.aspx' style="color: #DC2804; font-weight: bold; width: 100%">
                <img alt="" src="../Images/jia.gif" style="width: 14px; height: 15px; margin-bottom: -2px; padding-right: 5px; border: none;" />查看补登记历史记录</a>
        </p>


        <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
            填写格式说明
        </div>
        <div class="DivContent" id="Td3">
            1、身份证样式：必须为18位新版身份证（带X字母的必须使用大写）；<br />
            2、日期格式：2010-01-01或2010-1-1，其中分隔符为英文减号“-”；<br />
            3、组织机构代码：9位数字或大写字母组合（不带“-”横杠）；不知道的请登录 <a title="组织机构代码查询" href="https://www.cods.org.cn"
                target="_blank" style="color: Blue; text-decoration: underline;">https://www.cods.org.cn</a>
            网站，在“信息核查”栏目中查询；<br />
            4、照片格式：50k以内，宽高110 x 140像素且必须为jpg格式图片（可使用“1寸照片生成器”工具调整大小）。
        </div>
        <div class="content">
            <div id="DivContent">
                <p class="jbxxbt" style="text-align: center">
                    证书补登记
                </p>
                <table id="tableEdit" runat="server" width="95%" border="0" cellpadding="5" cellspacing="1"
                    class="table2" align="center">
                    <tr class="GridLightBK">
                        <td nowrap="nowrap" align="right">
                            <span style="color: Red">*</span>证件类别
                        </td>
                        <td width="35%">
                            <telerik:RadComboBox ID="RadComboBoxCertificateType" runat="server" Width="95%" NoWrap="true">
                            </telerik:RadComboBox>
                        </td>
                        <td align="right">
                            <span style="color: Red">*</span>证件号码
                        </td>
                        <td width="35%">
                            <telerik:RadTextBox ID="RadTextCertificateCode" runat="server" Width="95%" Skin="Default"
                                OnTextChanged="RadTextCertificateCode_TextChanged" AutoPostBack="True">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadTextCertificateCode" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td rowspan="5" align="center" width="110px">
                            <img id="ImgCode" src="~/Images/photo_ry.jpg" runat="server" height="140" width="110"
                                alt="" />
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td nowrap="nowrap" align="right">
                            <span style="color: Red">*</span>姓名
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="95%" Skin="Default">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadTextBoxWorkerName" Display="Dynamic"></asp:RequiredFieldValidator>
                            <asp:HiddenField ID="HiddenFieldPhone" runat="server" />
                        </td>
                        <td align="right">
                            <span style="color: Red">*</span>性别
                        </td>
                        <td>
                            <asp:RadioButton ID="RadioButtonMan" runat="server" Text="男" GroupName="3" Checked="true" />&nbsp;<asp:RadioButton
                                ID="RadioButtonWoman" Text="女" GroupName="3" runat="server" />
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right">
                            <span style="color: Red">*</span> 出生日期
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="RadDatePickerBirthday" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="150px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorBirthday" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadDatePickerBirthday" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td align="right">民族
                        </td>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxNation" runat="server" Width="95%" NoWrap="true">
                            </telerik:RadComboBox>
                            <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxNation"
                                ErrorMessage="必填" runat="server" ID="Comparevalidator4" ForeColor="Red" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td nowrap="nowrap" align="right">文化程度
                        </td>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxCulturalLevel" runat="server" Width="95%" NoWrap="true">
                            </telerik:RadComboBox>
                            <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxCulturalLevel"
                                ErrorMessage="必填" runat="server" ID="Comparevalidator3" ForeColor="Red" Display="Dynamic" />
                        </td>
                        <td align="right" nowrap="nowrap">政治面貌
                        </td>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxPoliticalBackground" runat="server" Width="95%" NoWrap="true">
                            </telerik:RadComboBox>
                            <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxPoliticalBackground"
                                ErrorMessage="必填" runat="server" ID="Comparevalidator2" ForeColor="Red" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right"><span style="color: Red">*</span>技术职称或技术等级
                        </td>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxSKILLLEVEL" runat="server" Width="95%" NoWrap="true">
                            </telerik:RadComboBox>
                            <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxSKILLLEVEL"
                                ErrorMessage="必填" runat="server" ID="Comparevalidator1" ForeColor="Red" Display="Dynamic" />
                        </td>
                        <td align="right" nowrap="nowrap">联系电话
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBoxPhone" runat="server" Width="95%" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td nowrap="nowrap" align="right">
                            <span style="color: Red">*</span>组织机构代码（9位,不带“-”）
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="95%" Skin="Default"
                                AutoPostBack="True" OnTextChanged="RadTextBoxUnitCode_TextChanged" MaxLength="9">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorRadTextBoxUnitCode" runat="server"
                                ErrorMessage="必填" ControlToValidate="RadTextBoxUnitCode" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td nowrap="nowrap" align="right">&nbsp;<span style="color: Red">*</span>单位全称
                        </td>
                        <td colspan="2">
                            <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="95%" AutoPostBack="True"
                                Skin="Default" OnTextChanged="RadTextBoxUnitName_TextChanged">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorRadTextBoxUnitName" runat="server"
                                ErrorMessage="必填" ControlToValidate="RadTextBoxUnitName" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td nowrap="nowrap" align="right">职务（三类人员必填）
                        </td>
                         <td>
                             <telerik:RadComboBox ID="RadComboBoxJob" runat="server" Width="95%" NoWrap="true">
                            </telerik:RadComboBox>
                         </td>
                        <td nowrap="nowrap" align="right">
                            <span style="color: Red">*</span>岗位工种
                        </td>
                        <td colspan="2">
                            <uc1:PostSelect ID="PostSelect1" runat="server" />
                        </td>
                       
                    </tr>
                    <tr class="GridLightBK">
                        <td nowrap="nowrap" align="right">编号方式</td>
                        <td >
                            <asp:RadioButton ID="RadioButtonAutoAllocateNo" runat="server" Text="自动编号" Checked="true"
                                GroupName="1" onclick="RadioButtonChange();" />&nbsp;&nbsp;<asp:RadioButton ID="RadioButtonCustomerAllocateNo"
                                    runat="server" Text="手工编号" GroupName="1" onclick="RadioButtonChange();" />
                        </td>
                        <td nowrap="nowrap" align="right">
                            <span style="color: Red">*</span>证书编号
                        </td>
                        <td colspan="2" >
                            <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="95%" Skin="Default"
                                Text="自动编号" Enabled="false">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="<br />输入证书编号"
                                ControlToValidate="RadTextBoxCertificateCode" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        
                        <td nowrap="nowrap" align="right">
                            <span style="color: Red">*</span>有效期起始
                        </td>
                         <td>
                              <telerik:RadDatePicker ID="RadDatePickerValidStartDate" MinDate="01/01/1900" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                runat="server" Width="150px">
                                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                <DateInput DisplayDateFormat="yyyy-M-d" DateFormat="yyyy-M-d">
                                </DateInput>
                            </telerik:RadDatePicker>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadDatePickerValidStartDate" Display="Dynamic"></asp:RequiredFieldValidator>
                         </td>
                          <td nowrap="nowrap" align="right">
                            <span style="color: Red">*</span>有效期截止
                        </td>
                        <td colspan="2" align="left">
                           
                            <telerik:RadDatePicker ID="RadDatePickerValidEndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="150px">
                                <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                </Calendar>
                                <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                <DateInput DisplayDateFormat="yyyy-M-d" DateFormat="yyyy-M-d">
                                </DateInput>
                            </telerik:RadDatePicker>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadDatePickerValidEndDate" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right">
                            <span style="color: Red">*</span>发证日期
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="RadDatePickerConferDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="150px" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadDatePickerConferDate" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td align="right">
                            <span style="color: Red">*</span>发证单位
                        </td>
                        <td colspan="2">
                            <telerik:RadTextBox ID="RadTextBoxConferUnit" runat="server" Width="80%" Skin="Default"
                                Text="北京市住建委">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadTextBoxConferUnit" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <table width="95%" border="0" cellpadding="5" cellspacing="1" align="center">
                    <tr class="GridLightBK">
                        <td colspan="5">
                            <table width="95%">
                                <tr>
                                    <td align="left" style="padding-left: 20px; line-height: 20px;" colspan="3">
                                        <span id="SpanTip" runat="server" style="font-size: 12px;">附件-照片：（格式要求：一寸jpg格式图片，最大为50K，宽高110
                                                X 140像素）</span> &nbsp;&nbsp;&nbsp;&nbsp;辅助工具下载：<a href="../Images/1寸照片生成器.exe" style="text-decoration: underline; color: Blue;"><img alt="" src="../Images/Soft_common.gif" style="border-width: 0;" />
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
                                            Width="300px">
                                            <Localization Select="选 择" />
                                        </telerik:RadUpload>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <hr style="background-color: #db1a2d; border: 1px solid #db1a2d; margin: 20px 0" />
            <div style="width: 95%; margin: 10px auto; text-align: center;">
                <asp:Button ID="ButtonSave" runat="server" Text="保 存" CssClass="button" OnClick="ButtonSave_Click"
                    OnClientClick="if(IfSelectPost()==false) {OpenAlert('请选择岗位工种');return false;}" />
                &nbsp;&nbsp;&nbsp;&nbsp;
                    <input id="Button1" type="button" value="清 空" class="button" onclick="javascript: location.href = 'CertificateAdd.aspx?r=';" />
            </div>
        </div>

    </div>
</asp:Content>
