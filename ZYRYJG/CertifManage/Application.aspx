<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Application.aspx.cs" Inherits="ZYRYJG.CertifManage.Application" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <link href="../Skins/Hot/Upload.hot.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style type="text/css">
        .EnforceApply {
            border: none;
            background-color: none;
            text-decoration: underline;
            color: blue;
            cursor: pointer;
        }
  
        @keyframes blink {         
          /*70% { padding-right:20px; }*/ 
         
          70%{
             font-weight:bold;
          }
        } 
        .blinking-text {
          animation: blink 2s infinite;
        }
  
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>

        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <ClientEvents OnRequestStart="onRequestStart" />
            <AjaxSettings>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
            Skin="Windows7" />
        <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
            EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt; 证书管理 &gt;&gt;
                <asp:Label ID="LabelPostType" runat="server" Text="三类人员"></asp:Label>
                    &gt;&gt; <strong>变更申请</strong> &gt;&gt; <strong>申请表</strong>
                </div>
            </div>

            <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                填报格式说明
            </div>
            <div class="content" runat="server" id="divmain">
                <div class="DivContent" id="Td3">
                    1、办事流程:个人网上申请并上传扫描件<span id="spanSanLeiRenTip" runat="server">-企业网上审核确认</span>-住建委网上审批（咨询电话：010-89150138）。<br />
                    2、身份证样式：须为18位二代身份证（带X字母的必须使用英文大写）。<br />
                    3、组织机构代码：9位数字或大写字母组合,带“-”的去掉“-”，社会统一信用代码中的第9位至第17位为企业的组织机构代码。<br />
                    4、不再提供纸质证书补办业务，请企业或个人自行下载电子证书。<br />
                    5、个人身份信息有误的，请携带相关材料到北京市政务服务中心现场办理（咨询电话：010-89150138）。（不包含“专业技术管理人员”）<br />
                    6、根据有关规定，停止造价员、关键岗位专业技术管理人员考核、变更和续期等相关业务。（咨询电话：010-55598091）<br />
                    <span class="blinking-text" runat="server" id="tipPosttype1" >
                    7、本次业务申请最终审批结果以住房和城乡建设部生成电子证书为准，在未生成电子证书前，请不要办理营业执照法定代表人变更或一、二级建造师变更、重新、注销注册等业务，以免无法正常生成电子证书，影响后续业务办理。<br />
                    8、持有安管人员A、C本的人员，近12个月内证书变更工作单位达到4次后，系统将暂停受理再次变更工作单位（企业更名除外）的申请。<br />
                    9、持有安管人员B本的人员，近12个月内证书变更工作单位达到2次后，系统将暂停受理再次变更工作单位（企业更名除外）的申请。
                    </span>
                </div>
                <div id="DivEdit" runat="server" style="width: 100%; margin: 10px auto; text-align: center; overflow: hidden;">
                     <div class="step">
                        <div class="stepLabel">办理进度：</div>
                        <div id="step_填报中" runat="server" class="stepItem lgray">个人填写<asp:Label ID="LabelCheckBack" runat="server" Text="（退回修改）" style="display:none" ForeColor="red" ></asp:Label>></div>
                        <div id="step_待单位确认" runat="server" class="stepItem lgray">待单位审核></div>
                        <div id="step_已申请" runat="server" class="stepItem lgray">待建委审核></div>
                        <div id="step_已审核" runat="server" class="stepItem lgray">市级审核></div>
                        <div id="step_已告知" runat="server" class="stepItem lgray">市级决定></div>
                        <div id="step_已办结" runat="server" class="stepItem lgray">住建部生成电子证书（办结，下载电子证书）</div>
                        <div class="stepArrow">▶</div>
                    </div>
                    <p style="text-align: left">
                        <asp:Label ID="LabelApplyCode" runat="server" Text=""></asp:Label>
                    </p>
                    <div style="width: 66%; float: left; clear: left">
                        <table cellpadding="5" cellspacing="1" border="0" width="99%" class="table" align="center">
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">申请表</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right" style="width: 20%">申请日期：
                                </td>
                                <td style="width: 30%">
                                    <asp:Label ID="lblApplyDate" runat="server" Text=""></asp:Label>
                                </td>
                                <td rowspan="5" colspan="2" style="text-align: center;">
                                    <img id="ImgCode" runat="server" style="border: 1px solid #efefef;" height="140" width="110" src="~/Images/photo_ry.jpg"
                                        alt="一寸照片" />

                                    <img id="ImgUpdatePhoto" runat="server" style="border: none; margin-left: 12px" height="140" width="110" src="~/Images/null.gif"
                                        alt="一寸照片" />
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">
                                    <span style="color: Red">*</span>变更类型：
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadTextBoxChangeType" runat="server" Width="97%" Skin="Default" Enabled="false"
                                        OnClientSelectedIndexChanged="OnClientSelectedIndexChanged">
                                    </telerik:RadComboBox>
                                </td>

                            </tr>
                            <tr class="GridLightBK">
                                <td align="right" nowrap="nowrap">证书类别：
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="RadTextBoxPostID" runat="server" Width="97%" ReadOnly="True">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">证书编号：
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="97%" Skin="Default"
                                        Enabled="False" ReadOnly="True">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">联系方式：
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="RadTextBoxLinkWay" runat="server" Width="97%" Skin="Default">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK" runat="server" id="trJob" visible="true ">
                                <td align="right" nowrap="nowrap">职务
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxJob" runat="server" Width="97%" NoWrap="true" OnSelectedIndexChanged="RadComboBoxJob_SelectedIndexChanged" AutoPostBack="true">
                                    </telerik:RadComboBox>
                                </td>
                                <td align="right" nowrap="nowrap">技术职称
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxSKILLLEVEL" runat="server" Width="97%" NoWrap="true">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="5" cellspacing="1" border="0" width="99%" class="table" align="center" id="tableOldCert1" runat="server">
                            <tr class="GridLightBK">
                                <td colspan="4">
                                    <asp:CheckBox ID="CheckBoxModifyPhoto" Checked="false" runat="server" Text="我要修改照片" onclick="javascript:CheckBoxModifyPhotoSelect(this.checked);" />
                                    <table width="95%" id="TableUploadPhoto" runat="server">
                                        <tr>
                                            <td align="left" style="padding-left: 20px; line-height: 180%;" colspan="3">
                                                <span style="color: Red">* </span><span id="SpanTip" runat="server" style="font-size: 16px;">附件-照片：（格式要求：近期彩色一寸白底标准免冠证件照，jpg格式，名称不限，最大为50K，宽高110 X 140像素）</span> 
                                                <br />
                                                <span onclick="javascript:layer.alert('<p style=\'font-size:15.0pt;font-family:仿宋;color:#333333\'><p style=\'font-size:18.0pt; text-align:center\'>考试报名上传电子照片要求</p><p>1.电子照片规格</p><p style=\'text-indent:30.0pt;\'>须上传近期彩色一寸白底标准正面免冠证件照。上传前，必须使用网上报名流程中提供的“一寸照片生成器”将照片处理成报考文件中要求的像素，以保证格式的正确。（本人近期彩色一寸白底标准正面免冠证件照，照片必须清晰，亮度足够，一寸jpg格式图片，最大为50K，宽高110 X 140像素）。</p><p>2.电子照片用途</p><p style=\'text-indent:30.0pt;\'>电子照片供考生参加考试和电子证书使用，请考生务必按要求上传照片。(避免因照片原因影响审核、考试及电子证书。)</p><p>3.上传照片注意</p><p style=\'text-indent:30.0pt;\'>（1）严禁上传风景照或生活照或艺术照，头像后不能出现杂物；</p><p style=\'text-indent:30.0pt;\'>（2）严禁上传使用摄像头、手机等非专业摄像装置拍摄的电子照片；</p><p style=\'text-indent:30.0pt;\'>（3）确保编辑好的电子照片头像轮廓清晰，不能模糊，照片上严禁出现姓名、号码和印章痕迹。</p></p>',{offset:'30px',icon:1,time:0,area: ['1000px', 'auto']});" style="color: blue; cursor: pointer;">【考试报名上传电子照片要求说明】</span>
                                                &nbsp;&nbsp; <a href="../Images/1寸照片生成器.exe" style="text-decoration: underline; color: Blue;">>> 1寸照片处理辅助工具下载</a>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="font-size: 13px; font-weight: bold;">上传照片：
                                            </td>
                                            <td align="left">
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
                            <tr class="GridLightBK">
                                <td colspan="2" align="center">变更前
                                </td>
                                <td colspan="2" align="center">变更后
                                </td>
                            </tr>
                            <tr id="TSTr" style="display: none; text-align: left" runat="server">
                                <td colspan="4" style="color: red">系统说明：离京变更后证书在京视为无效，无法使用此证书在京办理其他业务，不能下载电子证书（只提供电子离京证明）。如果在外省办理证书入省需要提供北京电子证书，请提前下载。</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right" nowrap="nowrap" style="background-color: #fbfbfb">姓名：
                                </td>
                                <td style="background-color: #fbfbfb">
                                    <asp:Label ID="lblWorkerName" runat="server" Width="97%" Skin="Default">
                                    </asp:Label>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <span style="color: Red">*</span>姓名：
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadTextBoxWorkerName" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server"
                                        Width="97%" Skin="Default">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right" nowrap="nowrap" style="background-color: #fbfbfb">性别：
                                </td>
                                <td style="background-color: #fbfbfb">
                                    <asp:Label ID="lblSex" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <span style="color: Red">*</span>性别：
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="必填"
                                        ControlToValidate="rdbtnlistSex" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <asp:RadioButtonList ID="rdbtnlistSex" runat="server" RepeatDirection="Horizontal"
                                        ValidationGroup="rdbsex">
                                        <asp:ListItem Value="男">男</asp:ListItem>
                                        <asp:ListItem Value="女">女</asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right" nowrap="nowrap" style="background-color: #fbfbfb">出生日期：
                                </td>
                                <td style="background-color: #fbfbfb">
                                    <asp:Label ID="lblBirthday" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <span style="color: Red">*</span>出生日期：
                                </td>
                                <td>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadDatePickerNewBirthday" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <telerik:RadDatePicker ID="RadDatePickerNewBirthday" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                        Width="97%">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput DisplayDateFormat="yyyy-M-d" DateFormat="yyyy-M-d">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right" style="background-color: #fbfbfb">证件号码：
                                </td>
                                <td style="background-color: #fbfbfb">
                                    <asp:Label ID="lblWorkerCertificateCode" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="right">
                                    <%--<span style="color: Red">*</span>--%>
                                    证件号码：
                                </td>
                                <td>
                                    <asp:Label ID="LabelWorkerCertificateCode" runat="server" Text=""></asp:Label>
                                   <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadTextBoxWorkerCertificateCode" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="RadTextBoxWorkerCertificateCode" runat="server"
                                        Width="97%" Skin="Default">
                                    </telerik:RadTextBox>--%>
                                </td>
                            </tr>
                            <tr class="GridLightBK" id="trSelectUnit" runat="server" style="display: none">
                                <td nowrap="nowrap" align="right" colspan="4">
                                    <input id="ButtonSelectQY" type="button" value="请选择一个单位" class="bt_maxlarge" onclick="javascript: selectQY();" />
                                </td>
                            </tr>
                            <tr class="GridLightBK" id="trUnitName" runat="server">
                                <td align="right" nowrap="nowrap" style="background-color: #fbfbfb">原单位名称：
                                </td>
                                <td style="background-color: #fbfbfb">
                                    <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="97%" Skin="Default"
                                        BackColor="Transparent" BorderStyle="None" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td align="right" id="tdLableNewUnitName" runat="server">
                                    <span style="color: Red">*</span>现单位名称：
                                </td>
                                <td id="tdNewUnitName" runat="server">
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadTextBoxNewUnitName" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="RadTextBoxNewUnitName" runat="server" Width="97%" Skin="Default">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK" id="trUnitCode" runat="server">
                                <td align="right" style="background-color: #fbfbfb">原单位组织机构代码：
                                </td>
                                <td style="background-color: #fbfbfb">
                                    <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="97%" Skin="Default"
                                        BackColor="Transparent" BorderStyle="None" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                                <td align="right">
                                    <div id="divLableNewUnitCode" runat="server" visible="true">
                                        <span style="color: Red">*</span>现单位组织机构代码：
                                    </div>
                                </td>
                                <td>
                                    <div id="divNewUnitCode" runat="server" visible="true">
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填"
                                            ControlToValidate="RadTextBoxNewUnitCode" Display="Dynamic"></asp:RequiredFieldValidator>
                                        <telerik:RadTextBox ID="RadTextBoxNewUnitCode" runat="server" Width="97%" Skin="Default"
                                            MaxLength="18">
                                        </telerik:RadTextBox>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="5" cellspacing="1" border="0" width="99%" class="table" align="center" id="tableOldCert2" runat="server" style="display:none">
                            <tr class="GridLightBK">
                                <td align="right" nowrap="nowrap" style="width: 20%">姓名：
                                </td>
                                <td colspan="3" >
                                    <asp:Label ID="LabelTS_WorkerName" runat="server" Width="97%" Skin="Default">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right" nowrap="nowrap">性别：
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="LabelTS_Sex" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right" nowrap="nowrap">出生日期：
                                </td>
                                <td  colspan="3">
                                    <asp:Label ID="LabelTS_Birthday" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">证件号码：
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="LabelTS_WorkerCertificateCode" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridLightBK"  id="trTS_OldUnitName" runat="server">
                                <td align="right" nowrap="nowrap">原单位名称：
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="RadTextBoxTS_OldUnitName" runat="server" Width="97%" Skin="Default"
                                        BackColor="Transparent" BorderStyle="None" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK" id="trTS_OldUnitCode" runat="server">
                                <td align="right">原单位组织机构代码：
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="RadTextBoxTS_OldUnitCode" runat="server" Width="97%" Skin="Default"
                                        BackColor="Transparent" BorderStyle="None" ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr id="tr_enforce" runat="server" style="display: none">
                                  <td align="right" nowrap="nowrap">是否申请强制执行：
                                </td>
                                <td  style="text-align:left">
                                      <asp:RadioButtonList ID="RadioButtonList_enforce" runat="server" RepeatDirection="Horizontal" Style="display: inline" OnSelectedIndexChanged="RadioButtonList_enforce_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="否" Value="" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="是" Value="申请强制执行"></asp:ListItem>
                                        </asp:RadioButtonList>
                                 </td>
                                <td  style="text-align:left;" colspan="2">
                                    <div id="div_enforceType" runat="server" style="text-align:left;display: none"><div style="float:left;line-height:200%">申请强制执行原因：</div><asp:RadioButtonList ID="RadioButtonList_enforceType" runat="server" RepeatDirection="Horizontal" style="display:inline"  >
                                            <asp:ListItem Text="企业不配合办理" Value="4" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="无法与该企业取得联系" Value="5"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div id="divCancelReason" runat="server" style="text-align:left;display: none;vertical-align:middle;"><div style="float:left;line-height:200%">注销原因：</div><asp:RadioButtonList ID="RadioButtonListCancelReason" runat="server" RepeatDirection="Horizontal" style="display:inline" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListCancelReason_SelectedIndexChanged"  >
                                            <asp:ListItem Text="依法解除劳动关系" Value="6" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不在从事安全生产管理工作" Value="7"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div id="div_DelContractDate" runat="server" style="text-align:left;display: none;vertical-align:middle"><div style="float:left">解除劳动关系日期：</div><telerik:RadDatePicker ID="RadDatePickerDelContractDate" runat="server" MinDate="2020-1-1" MaxDate="2050-1-1" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"  style="display:inline"   ></telerik:RadDatePicker></div>
                                    <div id="div_NotWorkDate" runat="server" style="text-align:left;display: none"><div style="float:left">不在该单位从事安全生产管理工作日期：</div><telerik:RadDatePicker ID="RadDatePickerNotWorkDate" runat="server" MinDate="2020-1-1" MaxDate="2050-1-1" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"  style="display:inline"  ></telerik:RadDatePicker></div>
                                </td>                               
                            </tr>                            
                        </table>
                        <table cellpadding="5" cellspacing="1" border="0" width="99%" class="table" align="center" id="tableHeTong" runat="server" style="display:none">
                            <tr class="GridLightBK">
                                <td align="right"><span style="color: Red">* </span>合同类型</td>
                                <td align="left" colspan="4">
                                    <asp:RadioButtonList ID="RadioButtonListENT_ContractType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListENT_ContractType_SelectedIndexChanged">
                                        <asp:ListItem Text="固定期限" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="无固定期限" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="以完成一定工作任务为期限" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right" width="15%"><span style="color: Red">* </span>劳动合同开始时间</td>
                                <td align="left">
                                    <telerik:RadDatePicker ID="RadDatePickerENT_ContractStartTime" runat="server" MinDate="1900-1-1" MaxDate="2090-1-1" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="ValidatorENT_ContractStartTime" runat="Server" ControlToValidate="RadDatePickerENT_ContractStartTime"
                                        ErrorMessage="请输入劳动合同开始时间" Display="Dynamic">*请输入劳动合同开始时间</asp:RequiredFieldValidator>
                                </td>
                                <td align="right" width="15%">
                                    <asp:Label ID="LabelJZSJ" runat="server" Text="劳动合同结束时间"></asp:Label></td>
                                <td align="left" colspan="2">
                                    <telerik:RadDatePicker ID="RadDatePickerENT_ContractENDTime" runat="server" MinDate="1900-1-1" MaxDate="2090-1-1" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="ValidatorENT_ContractENDTime" runat="Server" ControlToValidate="RadDatePickerENT_ContractENDTime"
                                        ErrorMessage="请输入劳动合同结束时间" Display="Dynamic">*请输入劳动合同结束时间</asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="5" cellspacing="1" border="0" width="99%" class="table" align="center">
                            <tr class="GridLightBK">
                                <td colspan="4" id="td_ChangeRemark" runat="server" style="color: red;"></td>
                            </tr>
                            <tr class="GridLightBK" id="trCheckHead" runat="server">
                                <td colspan="4" class="barTitle">数据校验</td>
                            </tr>
                            <tr class="GridLightBK" id="trCheckData" runat="server">
                                <td colspan="4">
                                    <div runat="server" id="divSheBao" style="width: 95%; padding-left: 20px; text-align: left; line-height: 200%;"></div>
                                    <div runat="server" id="divFR" style="margin: 0 auto; padding-left: 20px; text-align: left; line-height: 200%;"></div>
                                    <div runat="server" id="divZACheckResult" style="margin: 0 auto; padding-left: 20px; text-align: left; line-height: 150%;"></div>
                                </td>
                            </tr>
                            <tr class="GridLightBK" id="trFuJanTitel" runat="server" visible="false">
                                <td colspan="4" class="barTitle">附件上传</td>
                            </tr>
                            <tr class="GridLightBK" id="trFuJan" runat="server" visible="false">
                                <td align="left" style="line-height: 20px;" colspan="4">
                                    <div class="DivContent">
                                        <p>附件要求说明：</p>
                                        <p runat="server" id="p_PostTyppe1_qyfzr" visible="false">
                                            三类人员安全生产考核（企业主要负责人）变更材料：<br />
                                            <div runat="server" id="p_FaRen" visible="true">
                                                企业法定代表人：<br />
                                                1.变更申请表：申请人签字并加盖聘用单位公章；<br />
                                                2.身份证：正反面上传；<br />
                                                3.变更申请表中职务选择“法定代表人”的，上传《营业执照》至电子扫描件“社保权益记录及其他劳动关系证明材料”。<br />
                                                <br />
                                            </div>
                                            <div runat="server" id="p_NoFaRen" visible="true">
                                                其他企业主要负责人：<br />
                                                1.变更申请表：申请人签字并加盖聘用单位公章；<br />
                                                2.身份证：正反面上传；<br />
                                                3.变更申请表中职务选择“总经理（总裁）、分管安全生产的副总经理（副总裁）、分管生产经营的副总经理（副总裁）、技术负责人、安全总监”的，上传以下材料至电子扫描件“社保权益记录及其他劳动关系证明材料”：（社保校验结果为符合的，不用上传；不符合的，需上传社保权益记录及其他劳动关系证明材料。）<br />
                                                &nbsp;&nbsp;①已在申请单位缴纳申请之日上一个月社会保险的，须提交《社会保险个人权益记录》；<br />
                                                &nbsp;&nbsp;②已在申请单位本市或外省市分支机构缴纳申请之日上一个月社会保险的，须提交该单位分支机构的《营业执照》和《社会保险个人权益记录》。<br />
                                            </div>
                                        </p>
                                        <p runat="server" id="p_PostTyppe1_xmfzr" visible="false">
                                            三类人员安全生产考核（项目负责人）变更材料：<br />
                                            1.变更申请表：申请人签字并加盖聘用单位公章；<br />
                                            2.身份证：正反面上传；<br />
                                            3.变更申请表中职务选择“项目负责人”的，上传以下材料至电子扫描件“社保权益记录及其他劳动关系证明材料”：（社保校验结果为符合的，不用上传；不符合的，需上传社保权益记录及其他劳动关系证明材料。）<br />
                                            &nbsp;&nbsp;①已在申请单位缴纳申请之日上一个月社会保险的，须提交《社会保险个人权益记录》；<br />
                                            &nbsp;&nbsp;②已在申请单位本市或外省市分支机构缴纳申请之日上一个月社会保险的，须提交该单位分支机构的《营业执照》和《社会保险个人权益记录》。<br />
                                        </p>
                                        <p runat="server" id="p_PostTyppe1_aqy" visible="false">
                                            三类人员安全生产考核（专职安全生产管理人员）变更材料：<br />
                                            1.变更申请表：申请人签字并加盖聘用单位公章；<br />
                                            2.身份证：正反面上传；<br />
                                            3.社保权益记录及其他劳动关系证明材料：（社保校验结果为符合的，不用上传；不符合的，需上传社保权益记录及其他劳动关系证明材料。）<br />
                                            &nbsp;&nbsp;①已在申请单位缴纳申请之日上一个月社会保险的，须提交《社会保险个人权益记录》；<br />
                                            &nbsp;&nbsp;②已在申请单位本市或外省市分支机构缴纳申请之日上一个月社会保险的，须提交该单位分支机构的《营业执照》和《社会保险个人权益记录》。<br />
                                            &nbsp;&nbsp;③已在劳务派遣单位缴纳申请之日上一个月社会保险的，须提交劳务派遣单位的《劳务派遣经营许可证》、劳务派遣单位与申请人订立的劳动合同、劳务派遣单位与用工单位签订的劳务派遣协议和申请人在劳务派遣单位参加社会保险的《社会保险个人权益记录》。<br />
                                        </p>
                                        <p runat="server" id="p_PostTyppe2" visible="false">
                                            北京市建筑施工特种作业人员变更材料：<br />
                                            1.变更申请表：申请人签字；<br />
                                            2.身份证：正反面上传。<br />
                                        </p>
                                        <p runat="server" id="p_PostTyppe4" visible="false">
                                            北京市住房和城乡建设行业技能人员变更材料：<br />
                                            1.变更申请表：申请人签字并加盖聘用单位公章；<br />
                                            2.身份证：正反面上传；<br />
                                            <br />
                                        </p>
                                        <p runat="server" id="p_zhuxiao" visible="false">
                                            1.注销申请表：申请人签字并加盖聘用单位公章；（申请强制执行不用盖章）<br />
                                            2.身份证：正反面上传；<br />
                                           <%-- 3.申请强制注销的还需提供：<br />
                                            &nbsp;&nbsp;（1）本人强制注销申请；<br />
                                            &nbsp;&nbsp;（2）解除劳动关系证明材料（上传以下任一材料电子扫描件）：<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;①解除劳动合同（关系）协议书；<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;②劳动人事争议仲裁裁决书；<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;③司法判决书。<br />--%>
                                            <br />
                                        </p>
                                        <p runat="server" id="p_zhuxiao_tzzy" visible="false">
                                            1.注销申请表：申请人签字；<br />
                                            2.身份证：正反面上传；<br />
                                            <br />
                                        </p>
                                        <p runat="server" id="p_lijing" visible="false">
                                            1.变更申请表：申请人签字并加盖聘用单位公章；（申请强制执行不用盖章）<br />
                                            2.身份证：正反面上传；<br />
                                           <%-- 3.申请强制离京的还需提供：<br />
                                            &nbsp;&nbsp;（1）本人强制注销申请；<br />
                                            &nbsp;&nbsp;（2）解除劳动关系证明材料（上传以下任一材料电子扫描件）：<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;①解除劳动合同（关系）协议书；<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;②劳动人事争议仲裁裁决书；<br />
                                            &nbsp;&nbsp;&nbsp;&nbsp;③司法判决书。<br />--%>
                                            <br />
                                        </p>
                                    </div>                                   
                                    <div class="fujian">
                                        变更申请表扫描件：<span class="link" onclick="javascript:tips_pop('bgsqb','变更申请表扫描件','')">选择文件</span><span class="tishi">（上传签字、盖章后申请表扫描件，jpg格式图片最大500K）</span>
                                    </div>
                                    <div class="fujian">
                                        身份证扫描件：<span class="link" onclick="javascript:tips_pop('zj','证件扫描件','')">选择文件</span><span class="tishi">（上传身份证正反面扫描件原件、jpg格式图片，jpg格式图片最大500K）</span>
                                    </div>
                                    <div class="fujian" runat="server" id="div_SheBao" visible="true">
                                        社保权益记录及其它劳动关系证明材料：<span class="link" onclick="javascript:tips_pop('sb','社保扫描件','')">选择文件</span><span class="tishi">（上传个人社保权益记录，jpg格式图片最大500K）</span>
                                    </div>
                                    <div class="fujian" runat="server" id="div_YingYeZhiZhao" visible="false">
                                        企业营业执照扫描件：<span class="link" onclick="javascript:tips_pop('yyzz','企业营业执照扫描件','')">选择文件</span><span class="tishi">（要求：上传企业营业执照扫描件原件，jpg格式图片,最大500K）</span>
                                    </div>
                                    
                                   <%-- <div runat="server" id="div_enforce" style="display: none; margin-top: 20px">
                                        <div style="line-height: 100%; margin: 12px 48px;">申请强制执行，还需提供下列资料<span class="tishi">（提示：强制执行申请将由住建委直接审批，无需企业盖章、确认流程）</span></div>
                                        <div class="fujian" style="margin-left: 48px">
                                            强制执行申请表：<span class="link" onclick="javascript:tips_pop('enforce','强制执行申请表','')">选择文件</span><span class="tishi">（要求：<asp:Button ID="ButtonDownEnforceApply" OnClick="ButtonDownEnforceApply_Click" runat="server" Text="下载模板" CssClass="EnforceApply" />，填写后扫描件上传，jpg格式图片,最大500K）</span>
                                        </div>
                                        <div class="fujian" style="margin-left: 48px">
                                            解除劳动合同证明：<span class="link" onclick="javascript:tips_pop('jcldht','解除劳动合同证明','')">选择文件</span><span class="tishi">（要求：相关证明扫描件上传，jpg格式图片,最大500K）</span>
                                        </div>
                                    </div>--%>
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="5" cellspacing="1" border="0" width="99%" class="table" align="center" id="tableCurCertStatus" runat="server" style="display:none">
                            <tr class="GridLightBK">
                                <td class="barTitle">申请人当前持证状态（审核参考：多证同时变更单位，可能存在持证校验不通过情况，请参考以下在途变更目标单位是否符合持证要求）</td>
                            </tr>
                            <tr class="GridLightBK">                               
                                <td align="left" id="tdCurCertStatus" runat="server" style="padding:2px 20px;line-height:150%" >
                                   
                                </td>
                            </tr>
                        </table>
                        <div id="divCheckHistory" runat="server" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                            <table id="Table1" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td class="barTitle">审办记录</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td align="left" style="border-collapse: collapse;">
                                        <telerik:RadGrid ID="RadGridCheckHistory" runat="server" ShowHeader="true" CellPadding="0" CellSpacing="0"
                                            GridLines="None" AllowPaging="False" AllowSorting="False" AutoGenerateColumns="False"
                                            Width="100%" EnableEmbeddedSkins="false" PagerStyle-AlwaysVisible="False">
                                            <ClientSettings EnableRowHoverStyle="False">
                                            </ClientSettings>
                                            <MasterTableView NoMasterRecordsText="" CommandItemDisplay="None">
                                                <Columns>
                                                    <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNo" DataField="RowNo">
                                                        <ItemStyle Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="流程" UniqueName="Action" DataField="Action">
                                                     <ItemStyle Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="办理人" UniqueName="ActionMan" DataField="ActionMan" Display="false">
                                                        <ItemStyle Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="办理时间" UniqueName="ActionData" DataField="ActionData" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                                        <ItemStyle Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="办理结果" UniqueName="ActionResult" DataField="ActionResult">
                                                        <ItemStyle Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="办理意见" UniqueName="ActionRemark" DataField="ActionRemark">                                                           
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <HeaderStyle Font-Bold="True" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" Wrap="false" />
                                                <ItemStyle CssClass="subtable"  />
                                                <AlternatingItemStyle CssClass="subtable" />
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divCtl" runat="server" visible="false" style="width: 100%; margin: 10px auto 50px auto; text-align: center; overflow: hidden;">
                             <div id="divStepDesc" runat="server" style="line-height: 300%;">操作流程：个人申请保存-->点击导出打印(导出申请表)-->上传相关附件-->提交单位审核</div>
                            <table style="width: 100%; padding-bottom: 20px;">
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Button ID="btnSave" Text='保 存' runat="server" OnClick="btnSave_Click" CssClass="button" Enabled="false"></asp:Button>&nbsp;
                                         <asp:Button ID="ButtonExport" runat="server" Text="导出打印" CssClass="bt_large" OnClick="ButtonExport_Click"  CausesValidation="false"
                                             Enabled="False" />&nbsp;
                                         <asp:Button ID="ButtonDelete" Text="删 除" runat="server" CssClass="button" CausesValidation="false" Enabled="false" OnClientClick="javascript:if(confirm('确认要删除么？')==false) return false;"
                                             OnClick="ButtonDelete_Click"></asp:Button>&nbsp;
                                <asp:Button ID="ButtonExit" Text="取消申报" runat="server" CssClass="button" Enabled="false"  CausesValidation="false" OnClientClick="return myconfirm(this);"
                                    OnClick="ButtonExit_Click"></asp:Button>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table id="TableUnitCheck" runat="server" visible="false" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                            <tr class="GridLightBK">
                                <td colspan="2" class="barTitle">单位审核</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="20%" align="right">处理结果：</td>
                                <td width="80%" align="left">
                                    <asp:RadioButtonList ID="RadioButtonListOldUnitCheckResult" runat="server" RepeatDirection="Vertical" TextAlign="right">
                                        <asp:ListItem Text="同意" Value="同意" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="不同意" Value="不同意"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="20%" align="right">处理意见：</td>
                                <td width="80%" align="left">

                                    <asp:TextBox ID="TextBoxOldUnitCheckRemark" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="100" Text="提交建委审核"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="2" align="center">
                                    <asp:Button ID="ButtonUnitCheck" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonUnitCheck_Click"  OnClientClick="return unitconfirm(this);" />&nbsp;&nbsp;
                                         
                                </td>
                            </tr>
                        </table>
                        <div id="DivExamConfirm" runat="server" style="line-height: 30px; width: 680px; display: none; margin-bottom: 30px; position: relative; top: -300px; left: 150px; background-color: #dedede; padding: 20px 50px 50px 50px; border-left: 4px solid #eee; border-top: 4px solid #eee; border-right: 4px solid #999; border-bottom: 4px solid #999; color: #000">
                            <p style="font-size: 30px; font-weight: bold; text-align: center;">系统提示</p>
                            <p style="text-align: left;" id="p_ExamConvfirmDesc" runat="server">
                            </p>
                            <p style="text-align: center;">
                                <asp:Button ID="ButtonYes" runat="server" Text="是" CssClass="button  btn_no" OnClick="ButtonYes_Click" CausesValidation="false" Enabled="false" />
                                &nbsp;&nbsp; 
                <asp:Button ID="ButtonNo" runat="server" Text="否" CssClass="button  btn_no" OnClick="ButtonNo_Click" CausesValidation="false" Enabled="false" />
                                <span style="padding-left: 20px; font-size: 30px; color: red; font-weight: bold" id="spanCount">8</span>
                            </p>
                        </div>
                        <table id="TableJWCheck" runat="server" visible="false" border="0" cellpadding="5" cellspacing="1" width="100%" class="table" style="text-align: center; margin: 10px auto">
                            <tr class="GridLightBK">
                                <td colspan="2" class="barTitle">变更审核</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="20%" align="right">处理结果：</td>
                                <td width="80%" align="left">
                                    <asp:RadioButtonList ID="RadioButtonListJWCheck" runat="server" RepeatDirection="Vertical" TextAlign="right">
                                        <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="20%" align="right">处理意见：</td>
                                <td width="80%" align="left">

                                    <asp:TextBox ID="TextBoxCheckResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="200" Text="通过"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="2" align="center">
                                    <asp:Button ID="ButtonCheck" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonCheck_Click" OnClientClick="if(JWSubmitTip()==false) return false;" />&nbsp;&nbsp;
                                         
                                </td>
                            </tr>
                        </table>
                        <table id="TableConfrim" runat="server" visible="false" border="0" cellpadding="5" cellspacing="1" width="100%" class="table" style="text-align: center; margin: 10px auto">
                            <tr class="GridLightBK">
                                <td colspan="2" class="barTitle">变更决定</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="20%" align="right">处理结果：</td>
                                <td width="80%" align="left">
                                    <asp:RadioButtonList ID="RadioButtonListConfrim" runat="server" RepeatDirection="Vertical" TextAlign="right">
                                        <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="20%" align="right">处理意见：</td>
                                <td width="80%" align="left">

                                    <asp:TextBox ID="TextBoxConfrimResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="200" Text="通过"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="2" align="center">
                                    <asp:Button ID="ButtonConfrim" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonConfrim_Click" OnClientClick="if(JWConfrimTip()==false) return false;" />&nbsp;&nbsp;
                                         
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divImg" style="width: 32%; float: left; clear: right; margin-left: 1%; overflow: auto; border: 1px solid #cccccc; margin-bottom: 200px">
                        <telerik:RadGrid ID="RadGridFile" runat="server"
                            GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                            Width="100%" Skin="Default" EnableAjaxSkinRendering="false"
                            EnableEmbeddedSkins="false" OnItemDataBound="RadGridFile_ItemDataBound">
                            <ClientSettings EnableRowHoverStyle="false">
                            </ClientSettings>
                            <MasterTableView NoMasterRecordsText=" 没有相关附件" GridLines="None" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                                DataKeyNames="ApplyID,FileName,FileUrl">
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="ApplyFile" HeaderText="附件">
                                        <ItemTemplate>
                                            <div class="DivTitleOn" onclick="DivOnOff(this,'Div<%# Eval("DataType") %>',event);" title="折叠">
                                                <%# Eval("DataType") %>
                                            </div>
                                            <div class="DivContent" id="Div<%# Eval("DataType") %>" style="position: relative;">
                                                <telerik:RadGrid ID="RadGrid1" runat="server" ShowHeader="false"
                                                    GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                                                    Width="100%" Skin="Default" EnableAjaxSkinRendering="false" OnDeleteCommand="RadGridFile_DeleteCommand"
                                                    EnableEmbeddedSkins="false">
                                                    <ClientSettings EnableRowHoverStyle="false">
                                                    </ClientSettings>
                                                    <MasterTableView NoMasterRecordsText="" GridLines="None" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                                                        DataKeyNames="ApplyID,FileID">
                                                        <Columns>
                                                            <telerik:GridTemplateColumn UniqueName="ApplyFile" HeaderText="附件">
                                                                <ItemTemplate>
                                                                    <img class="img200" alt="图片" src='<%# ZYRYJG.UIHelp.ShowFile(Eval("FileUrl").ToString())%>' />
                                                                    <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="../images/Cancel.gif" CommandName="Delete" Visible='<%#(IfExistRoleID("0") == true && ViewState["certhfchange"] !=null && ((ViewState["certhfchange"] as Model.CertificateChangeOB).Status == Model.EnumManager.CertificateChangeStatus.NewSave||(ViewState["certhfchange"] as Model.CertificateChangeOB).Status == Model.EnumManager.CertificateChangeStatus.SendBack))?true:false%>' OnClientClick="javascript:if(confirm('您确定要删除么?')==false) return false" />

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" Height="30px" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </telerik:GridTemplateColumn>

                                                        </Columns>
                                                        <HeaderStyle BackColor="#E4E4E4" Height="22px" Font-Bold="true" />
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" Height="30px" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                </Columns>
                                <HeaderStyle BackColor="#E4E4E4" Height="22px" Font-Bold="true" />
                            </MasterTableView>
                        </telerik:RadGrid>
                    </div>

                </div>
                <uc4:IframeView ID="IframeView" runat="server" />
            </div>
        </div>
        <div id="winpop"></div>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
               
                function myconfirm(send)
                {                    
                    if (send.value == '提交单位审核') {                     
                        var rect = send.getBoundingClientRect();
                        var changelimit = "<%=IfChangeUnitLimit%>";

                        if (changelimit == "0") {
                            layer.confirm('本次业务申请最终审批结果以住房和城乡建设部生成电子证书为准，在未生成电子证书前，请不要办理营业执照法定代表人变更或一、二级建造师变更、重新、注销注册等业务，以免无法正常生成电子证书，影响后续业务办理。', { title: '提示', btn: ['已知悉上述要求'], offset: rect.top - 400, area: ['600px', '250px'] }, function (index) { layer.close(index); __doPostBack('ButtonExit', ''); });
                        }
                        else
                        {
                            layer.confirm('本次业务申请最终审批结果以住房和城乡建设部生成电子证书为准，在未生成电子证书前，请不要办理营业执照法定代表人变更或一、二级建造师变更、重新、注销注册等业务，以免无法正常生成电子证书，影响后续业务办理。<br><br><span style=\'color:red\'>该证书完成本次工作单位变更后，近12个月内累计变更次数已达到' + changelimit + '次，系统将暂停受理再次变更工作单位的申请。是否继续上报？</span>', { title: '提示', btn: ['是', '否'], offset: rect.top - 400, area: ['600px', '300px'] }, function (index) { layer.close(index); __doPostBack('ButtonExit', ''); }, function (index) { layer.close(index); });
                        }
                        return false;
                        
                    }
                    else
                    {
                        __doPostBack('ButtonExit', '');
                        return false;
                    }
                }

                function unitconfirm(send) {
                    var CheckSelect = $("input[name='RadioButtonListOldUnitCheckResult']:checked").val();
                    if (CheckSelect == "不同意") {
                        return true;
                    }
                    else {
                        var rect = send.getBoundingClientRect();
                        var changelimit = "<%=IfChangeUnitLimit%>";

                        if (changelimit == "0") {
                            return true;
                        }
                        else {
                            layer.confirm('<span style=\'color:red\'>该证书完成本次工作单位变更后，近12个月内累计变更次数已达到' + changelimit + '次，系统将暂停受理再次变更工作单位的申请。是否继续上报？</span>', { title: '提示', btn: ['是', '否'], offset: rect.top - 400, area: ['600px', '250px'] }, function (index) { layer.close(index); __doPostBack('ButtonUnitCheck', ''); }, function (index) { layer.close(index); });
                            return false;
                        }
                    }
                }

                function onRequestStart(sender, args) {
                    if (args.get_eventTarget().indexOf("ButtonExport") >= 0
                        || args.get_eventTarget().indexOf("btnSave") >= 0
                        || args.get_eventTarget().indexOf("ButtonDownEnforceApply") >= 0
                    )
                    {
                        args.set_enableAjax(false);
                    }
                }

                function SelectMeOnly(objRadioButton, grdName) {

                    var i, obj;
                    for (i = 0; i < document.all.length; i++) {
                        obj = document.all(i);

                        if (obj.type == "radio") {
                            if (obj.id.indexOf(grdName) > 0) {
                                if (objRadioButton.id == obj.id)
                                    obj.checked = true;
                                else
                                    obj.checked = false;
                            }
                        }
                    }
                }

                $(function () {
                    var imgWid = 0;
                    var imgHei = 0; //变量初始化
                    var big = 2.5;//放大倍数
                    $(".img200").hover(function () {

                        $(this).stop(true, true);
                        var imgWid2 = 0; var imgHei2 = 0;//局部变量
                        imgWid = $(this).width();
                        imgHei = $(this).height();
                        imgWid2 = imgWid * big;
                        imgHei2 = imgHei * big;

                        $("#divImg").css({ "float": "right", "overflow": "visible" });
                        $(this).animate({ "width": imgWid2, "height": imgHei2, "margin-left": -imgWid * (big - 1), "position": "absolute", "z-index": 999 });
                    }, function () {
                        $("#divImg").css({ "float": "right", "overflow": "auto" });
                        $(this).stop().animate({ "width": imgWid, "height": imgHei, "margin-left": 0, "position": "relative", "float": "none" });
                    });

                    $(".img200").click(function () {
                        var nw = window.open($(this)[0].src, "_blank", 'resizable=yes');
                    });

                    //单位审核结果
                    $("#<%= RadioButtonListOldUnitCheckResult.ClientID%> input").each(function () {
                        $(this).click(function () {
                            var TextBoxOldUnitCheckRemark = $("#<%= TextBoxOldUnitCheckRemark.ClientID%>");

                            if ($(this).val() == "同意") {
                                TextBoxOldUnitCheckRemark.val("提交建委审核");
                            }
                            else {
                                TextBoxOldUnitCheckRemark.val("退回个人");
                            }
                        });
                    });

                    //建委审核结果
                    $("#<%= RadioButtonListJWCheck.ClientID%> input").each(function () {
                        $(this).click(function () {
                            var TextBoxCheckResult = $("#<%= TextBoxCheckResult.ClientID%>");

                            if ($(this).val() == "通过") {
                                TextBoxCheckResult.val("通过");
                            }
                            else {
                                TextBoxCheckResult.val("退回个人");
                            }
                        });
                    });

                    //建委决定结果
                    $("#<%= RadioButtonListConfrim.ClientID%> input").each(function () {
                        $(this).click(function () {
                            var TextBoxConfrimResult = $("#<%= TextBoxConfrimResult.ClientID%>");

                            if ($(this).val() == "通过") {
                                TextBoxConfrimResult.val("通过");
                            }
                            else {
                                TextBoxConfrimResult.val("退回个人");
                            }
                        });
                    });
                });

                //审核确认
                function JWSubmitTip() {
                    var CheckSelect = $("input[name='RadioButtonListJWCheck']:checked").val();
                    var CheckResult = $("#<%= TextBoxCheckResult.ClientID%>").val();
                    if (CheckSelect == "通过" && CheckResult != "通过") {
                        return confirm('您选择了审核通过，却修改了审核意见，您确定要提交审核意见么?');
                    }
                    if (CheckSelect == "不通过" && CheckResult == "退回个人") {
                        return confirm('您选择了审核不通过，却没有修改详细的审核意见，您确定要提交审核意见么?');
                    }
                    return true;
                }

                //决定确认JWConfrimTip
                function JWConfrimTip() {
                    var ConfrimSelect = $("input[name='RadioButtonListConfrim']:checked").val();
                    var ConfrimResult = $("#<%= TextBoxConfrimResult.ClientID%>").val();
                    if (ConfrimSelect == "通过" && ConfrimResult != "通过") {
                        return confirm('您选择了决定通过，却修改了决定意见，您确定要提交决定意见么?');
                    }
                    if (ConfrimSelect == "不通过" && ConfrimResult == "退回个人") {
                        return confirm('您选择了决定不通过，却没有修改详细的决定意见，您确定要提交决定意见么?');
                    }
                    return true;
                }


                function CheckClientValidate() {
                    Page_ClientValidate();
                    if (Page_IsValid) {
                        return true;
                    } else {
                        return false;
                    }
                }




                function OnClientSelectedIndexChanged(sender, eventArgs) {
                    var item = eventArgs.get_item();
                    if (item.get_text() == "离京变更") {
                        alert('离京变更后证书在京视为无效，无法使用此证书在京办理其他业务。');
                    }
                }
                TSTrShow();
                function TSTrShow() {
                    var changetype = document.getElementById("<%=RadTextBoxChangeType.ClientID%>");
                    var tstr = document.getElementById("TSTr");
                    if ($(changetype).val() == "离京变更") {
                        tstr.style.display = "block";
                        //$("#TSTr").show();
                    }
                }

                function showImg(radUpload, eventArgs) {

                    debugger;
                    var input = eventArgs.get_fileInputField();
                    var inputs = radUpload.getFileInputs();
                    var CheckBoxModifyPhoto = document.getElementById("<%=CheckBoxModifyPhoto.ClientID%>");

                    var ImgCode = (CheckBoxModifyPhoto != null && CheckBoxModifyPhoto.checked == true) ? document.getElementById("<%=ImgUpdatePhoto.ClientID %>") : document.getElementById("<%=ImgCode.ClientID %>");
                    for (i = 0; i < inputs.length; i++) {
                        ImgCode.src = inputs[i].value;
                        break;
                    }
                }

                function CheckBoxModifyPhotoSelect(ifselect) {
                    var TableUploadPhoto = document.getElementById("<%=TableUploadPhoto.ClientID %>");
                    if (ifselect == true) {
                        TableUploadPhoto.style.display = "block";
                    }
                    else {
                        TableUploadPhoto.style.display = "none";
                    }
                }
                //选择文件
                var pid = "<%=ApplyID %>";
                function tips_pop(code, ftype, fsname) {
                    layer.open({
                        type: 2,
                        title: ['资料上传 - ' + ftype, 'font-weight:bold;'],//标题
                        maxmin: true, //开启最大化最小化按钮,
                        offset: $(parent.document).scrollTop() + 20 + 'px',
                        area: ['800px', '500px'],
                        shadeClose: false, //点击遮罩关闭
                        content: '../uploader/Upload.aspx?o=' + code + '&t=' + ftype + '&s=' + fsname + '&a=' + pid,
                        cancel: function (index, layero) {
                            __doPostBack('refreshFile', '');
                            layer.close(index);
                            return false;
                        }
                    });
                    var MsgPop = document.getElementById("winpop");//获取窗口这个对象,即ID为winpop的对象
                    MsgPop.style.display = "block";
                    MsgPop.style.height = "400px";//高度增加4个象素
                }
                //选择文件
                function selectQY() {

                    layer.open({
                        type: 2,
                        title: ['选择一个企业 ', 'font-weight:bold;'],//标题
                        maxmin: true, //开启最大化最小化按钮,
                        offset: $(parent.document).scrollTop() + 20 + 'px',
                        area: ['800px', '500px'],
                        shadeClose: false, //点击遮罩关闭
                        content: '../SelectQY.aspx',
                        btn: ['确定', '关闭'],
                        yes: function (index, oArg) {
                            //获取选择的row,并加载到页面
                            var data = window["layui-layer-iframe" + index].callbackdata();
                            if (data) {
                                var RadTextBoxNewUnitCode = $find("<%= RadTextBoxNewUnitCode.ClientID%>");
                                var RadTextBoxNewUnitName = $find("<%= RadTextBoxNewUnitName.ClientID%>");
                                RadTextBoxNewUnitCode.set_value(data.ENT_OrganizationsCode);
                                RadTextBoxNewUnitName.set_value(data.ENT_Name)
                                layer.close(index);
                                return false;
                            } else {
                                layer.msg('请选择一个企业', { icon: 0 });
                            }
                        },
                        cancel: function (index, layero) {
                            layer.close(index);
                            return false;
                        }
                    });
                    var MsgPop = document.getElementById("winpop");//获取窗口这个对象,即ID为winpop的对象
                    MsgPop.style.display = "block";
                    MsgPop.style.height = "400px";//高度增加4个象素
                }

            </script>
        </telerik:RadCodeBlock>
    </form>
</body>
</html>
