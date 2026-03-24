<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="WorkerInfoEdit.aspx.cs" Inherits="ZYRYJG.PersonnelFile.WorkerInfoEdit" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
            function showImgSign(radUpload, eventArgs) {
                var input = eventArgs.get_fileInputField();
                var inputs = radUpload.getFileInputs();
                var ImgSign = document.getElementById("<%=ImgSign.ClientID %>");
                for (i = 0; i < inputs.length; i++) {
                    ImgSign.src = inputs[i].value;
                    break;
                }
            }
            function showImgIDCard(radUpload, eventArgs) {
                var input = eventArgs.get_fileInputField();
                var inputs = radUpload.getFileInputs();
                var ImgIDCard = document.getElementById("<%=ImgIDCard.ClientID %>");
                for (i = 0; i < inputs.length; i++) {
                    ImgIDCard.src = inputs[i].value;
                    break;
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 我的信息 
                &gt;&gt; 个人信息维护
            </div>
        </div>
        <div class="content">
            <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                信息维护说明
            </div>
            <div class="DivContent" id="Td3">
                个人部分关键信息来自北京首都之窗系统，此处无法修改，请下载北京通App进行修改，或访问<a target="_blank" href="https://www.beijing.gov.cn/">【首都之窗】</a>登录个人中心修改。

            </div>
            <div style="width: 95%; margin: 0 auto; padding: 5px;" runat="server" id="divExamSignUp">

                <table id="tableWorker" runat="server" width="95%" border="0" cellpadding="5" cellspacing="1" class="table2" align="center">
                    <tr class="GridLightBK">
                        <td width="12%" nowrap="nowrap" align="right">
                            <span style="color: Red">*</span>姓名：
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="95%" Skin="Default" MaxLength="50">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadTextBoxWorkerName" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td width="10%" nowrap="nowrap" align="right">
                            <span style="color: Red">*</span>性别：
                        </td>
                        <td width="40%">
                            <asp:RadioButton ID="RadioButtonMan" runat="server" Text="男" GroupName="1" Checked="true" />
                            &nbsp;<asp:RadioButton ID="RadioButtonWoman" Text="女" GroupName="1" runat="server" />
                        </td>

                    </tr>
                    <tr class="GridLightBK">
                        <td width="10%" nowrap="nowrap" align="right">
                            <span style="color: Red">*</span>证件类别：
                        </td>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxCertificateType" runat="server" Width="95%" NoWrap="true">
                            </telerik:RadComboBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadComboBoxCertificateType" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                        <td width="10%" nowrap="nowrap" align="right">
                            <span style="color: Red">*</span>证件号码：
                        </td>
                        <td width="40%">
                            <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="95%" Skin="Default"
                                AutoPostBack="True" ReadOnly="True" MaxLength="18">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadTextBoxCertificateCode" Display="Dynamic"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right"><span style="color: Red">*</span>出生日期：
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="RadDatePickerBirthday" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                Width="98%" />
                        </td>

                        <td align="right" width="10%" nowrap="nowrap"><span style="color: Red">*</span>联系电话：
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBoxPhone" runat="server" Width="95%" Skin="Default" MaxLength="11">
                            </telerik:RadTextBox>
                        </td>

                    </tr>
                    <tr class="GridLightBK">
                        <td width="10%" nowrap="nowrap" align="right">民族：
                        </td>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxNation" runat="server" Width="95%" NoWrap="true">
                            </telerik:RadComboBox>
                        </td>
                        <td nowrap="nowrap" align="right">文化程度：
                        </td>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxCulturalLevel" runat="server" Width="95%" NoWrap="true">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right" width="10%" nowrap="nowrap">政治面貌：
                        </td>
                        <td>
                            <telerik:RadComboBox ID="RadComboBoxPoliticalBackground" runat="server" Width="95%" NoWrap="true">
                            </telerik:RadComboBox>
                        </td>
                        <td align="right">电子邮箱：
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBoxEmail" runat="server" Width="95%" Skin="Default" MaxLength="50">
                            </telerik:RadTextBox>
                        </td>
                    </tr>

                    <tr class="GridLightBK">

                        <td align="right">通讯地址：
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBoxAddress" runat="server" Width="95%" Skin="Default" MaxLength="100">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" width="10%" nowrap="nowrap">邮政编码：
                        </td>
                        <td>
                            <telerik:RadTextBox ID="RadTextBoxZipCode" runat="server" Width="95%" Skin="Default" MaxLength="6">
                            </telerik:RadTextBox>
                        </td>
                    </tr>


                    <tr class="GridLightBK">

                        <td width="10%" nowrap="nowrap" align="right" rowspan="3">一寸照片：
                        </td>
                        <td width="40%" align="left">
                            <span onclick="javascript:layer.alert('<p style=\'font-size:15.0pt;font-family:仿宋;color:#333333\'><p style=\'font-size:18.0pt; text-align:center\'>上传电子照片要求</p><p>1.电子照片规格</p><p style=\'text-indent:30.0pt;\'>须上传近期彩色一寸白底标准正面免冠证件照。上传前，必须使用网上提供的“一寸照片生成器”将照片处理成要求的像素，以保证格式的正确。（本人近期彩色一寸白底标准正面免冠证件照，照片必须清晰，亮度足够，一寸jpg格式图片，最大为50K，宽高110 X 140像素）。</p><p>2.电子照片用途</p><p style=\'text-indent:30.0pt;\'>电子照片供考生参加考试和电子证书使用，请务必按要求上传照片。(避免因照片原因影响审核、考试及电子证书。)</p><p>3.上传照片注意</p><p style=\'text-indent:30.0pt;\'>（1）严禁上传风景照或生活照或艺术照，头像后不能出现杂物；</p><p style=\'text-indent:30.0pt;\'>（2）严禁上传使用摄像头、手机等非专业摄像装置拍摄的电子照片；</p><p style=\'text-indent:30.0pt;\'>（3）确保编辑好的电子照片头像轮廓清晰，不能模糊，照片上严禁出现姓名、号码和印章痕迹。</p></p>',{offset:'30px',icon:1,time:0,area: ['1000px', 'auto']});" style="color: blue; cursor: pointer;">【上传电子照片要求说明】</span>
                            &nbsp;&nbsp; <a href="../Images/1寸照片生成器.exe" style="text-decoration: underline; color: Blue;">>> 1寸照片处理辅助工具下载</a>

                        </td>
                        <td align="right" rowspan="5">手持身份证<br />
                            半身照：
                        </td>
                        <td>格式要求：JPG图片格式、大小控制在500k以内</td>
                    </tr>

                    <tr class="GridLightBK">
                        <td>
                            <img id="ImgCode" runat="server" height="140" width="110" alt="一寸照片"  src="#"  />（格式要求：JPG图片格式、大小控制在50k以内）
                            <asp:Button ID="ButtonImgEdit" runat="server" Text="微 调" CssClass="button" OnClick="ButtonImgEdit_Click" Visible="false" />
                        </td>
                        <td rowspan="3">
                            <img id="ImgIDCard" runat="server" height="300" width="400" alt="手持身份证半身照" style="border: 1px solid #dddddd;"  src="#"  />
                            <asp:Button ID="ButtonImgIDCard" runat="server" Text="微 调" CssClass="button" OnClick="ButtonImgIDCard_Click" Visible="false" />
                        </td>
                    </tr>
                    <tr class="GridLightBK">

                        <td align="left">
                            <telerik:RadUpload ID="RadUploadFacePhoto" runat="server" ControlObjectsVisibility="None"
                                MaxFileInputsCount="1" OverwriteExistingFiles="True" Skin="Hot"
                                EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" ReadOnlyFileInputs="False" OnClientFileSelected="showImg"
                                Width="200px">
                                <Localization Select="选择图片" />
                            </telerik:RadUpload>
                        </td>
                    </tr>

                    <tr class="GridLightBK">
                        <td align="right" width="10%" nowrap="nowrap" rowspan="2">手写签名照：
                        </td>
                        <td style="vertical-align: middle">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <img id="ImgSign" runat="server" height="43" width="99" alt="手写签名照" style="border: 1px solid #dddddd;" src="#" />
                                    </td>
                                    <td>（格式要求：JPG图片格式、大小控制在50k以内）<br />
                                        <asp:Button ID="ButtonImgEditQianMing" runat="server" Text="微调签名" CssClass="button" OnClick="ButtonImgEditQianMing_Click" Visible="false" />
                                        <asp:Button ID="ButtonSignPhotoUpdate" runat="server" Text="提交签名" CssClass="button" OnClick="ButtonSignPhotoUpdate_Click" Visible="false"  />
                                        <%--<asp:Button ID="ButtonSignPhotoUpdate" runat="server" Text="提交签名" CssClass="button" OnClick="ButtonSignPhotoUpdate_Click" Visible="false" OnClientClick="javascript:if(!confirm('重要提示：提交前请确认签名显示正确，提交后将触发重新生成带有签名的电子证书，请次日下载。签名提交后没有管理员授权将不可修改，您确定要提交签名么？'))return false;" />--%>
                                    </td>
                                </tr>
                            </table>

                        </td>

                    </tr>

                    <tr class="GridLightBK">
                        <td>
                            <telerik:RadUpload ID="RadUploadImgSign" runat="server" ControlObjectsVisibility="None" Style="display: table-cell"
                                MaxFileInputsCount="1" OverwriteExistingFiles="True" Skin="Hot" OnClientFileSelected="showImgSign"
                                EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" ReadOnlyFileInputs="False"
                                Width="200px">
                                <Localization Select="选择图片" />
                            </telerik:RadUpload>（选择更换图片后记得先保存，再提交签名，否则直接提交的是原签名图片。提交成功后，修改签名照片需要申请个人信息变更。）

                        </td>
                        <td>
                            <telerik:RadUpload ID="RadUploadImgIDCard" runat="server" ControlObjectsVisibility="None"
                                MaxFileInputsCount="1" OverwriteExistingFiles="True" Skin="Hot" OnClientFileSelected="showImgIDCard"
                                EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" ReadOnlyFileInputs="False"
                                Width="200px">
                                <Localization Select="选择图片" />
                            </telerik:RadUpload>
                        </td>
                    </tr>
                   <%-- <tr class="GridLightBK">
                        <td colspan="4">
                            <p style='font-size: 15.0pt; color: blue; float: left; letter-spacing: 2px;line-height:150%'>温馨提示：<br />二级建造师申请注册时，应提供手写签名照、手持身份证半身照。<br />
                                补传手工签名照并确认显示无误后提交签名，提交后将触发重新生成带签名的电子证书。请于次日重新下载电子证书。<br />
                                注意：签名提交后没有管理员授权将不可修改,请提交前确认签名显示是否正常（显示不全或比例失调请使用微调功能）。

                            </p>
                        </td>
                    </tr>--%>
                </table>
            </div>
            <br />

            <div style="width: 50%; padding: 5px; margin: 0 auto; text-align: center;">
                <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="button" OnClick="btnSave_Click" />
            </div>
            <br />
        </div>
    </div>

</asp:Content>
