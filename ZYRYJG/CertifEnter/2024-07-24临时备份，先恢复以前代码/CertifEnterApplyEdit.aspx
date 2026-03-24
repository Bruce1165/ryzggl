<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="CertifEnterApplyEdit.aspx.cs" Inherits="ZYRYJG.CertifEnter.CertifEnterApplyEdit" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <link href="../Skins/Hot/Upload.hot.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form2" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager2" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
            ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
        </telerik:RadWindowManager>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">

                function onRequestStart(sender, args) {
                    if (args.get_eventTarget().indexOf('<%=ButtonSave.UniqueID %>') >= 0
                || args.get_eventTarget().indexOf('<%=ButtonExport.UniqueID %>') >= 0) {
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


                    //建委受理结果
                    $("#<%= RadioButtonListJWAccept.ClientID%> input").each(function () {
                        $(this).click(function () {
                            var TextBoxGetResult = $("#<%= TextBoxGetResult.ClientID%>");

                            if ($(this).val() == "通过") {

                                TextBoxGetResult.val("通过");
                            }
                            else {

                                TextBoxGetResult.val("退回个人");

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
                });


                //选择文件
                function tips_pop(code, ftype, fsname, pid) {

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

                function CheckClientValidate() {
                    Page_ClientValidate();
                    if (Page_IsValid) {
                        return true;
                    } else {
                        return false;
                    }
                }

                //选择企业
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
                                var RadTextBoxCreditCode = $find("<%= RadTextBoxCreditCode.ClientID%>");
                                    var RadTextBoxUnitName = $find("<%= RadTextBoxUnitName.ClientID%>");
                                    RadTextBoxCreditCode.set_value(data.ENT_OrganizationsCode);
                                    RadTextBoxUnitName.set_value(data.ENT_Name)
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
                    //选择进京证书
                    function selectCert() {
                        layer.open({
                            type: 2,
                            title: ['选择一个要进京的证书 ', 'font-weight:bold;'],//标题
                            maxmin: true, //开启最大化最小化按钮,
                            offset: $(parent.document).scrollTop() + 20 + 'px',
                            area: ['1200px', '500px'],
                            shadeClose: false, //点击遮罩关闭
                            content: './SelectOutCert.aspx',
                            btn: ['确定', '关闭'],
                            yes: function (index, oArg) {
                                //获取选择的row,并加载到页面
                                var data = window["layui-layer-iframe" + index].callbackdata();
                                if (data) {
                                    __doPostBack('selectCert', data.CertCode);
                                    layer.close(index);
                                    return false;
                                } else {
                                    layer.msg('选择一个要进京的证书', { icon: 0 });
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
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <%--<telerik:AjaxSetting AjaxControlID="RadTextBoxWorkerCertificateCode">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadDatePickerBirthday" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="RadioButtonMan" UpdatePanelRenderMode="Inline" />
                        <telerik:AjaxUpdatedControl ControlID="RadioButtonWoman" UpdatePanelRenderMode="Inline" />
                        <telerik:AjaxUpdatedControl ControlID="RadTextBoxWorkerName" UpdatePanelRenderMode="Inline" />
                        <telerik:AjaxUpdatedControl ControlID="ImgCode" UpdatePanelRenderMode="Inline" />
                    </UpdatedControls>
                </telerik:AjaxSetting>--%>
                <%-- <telerik:AjaxSetting AjaxControlID="DivEdit">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DivEdit" />
                         <telerik:AjaxUpdatedControl ControlID="PostSelect1" />
                        
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="PostSelect1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="PostSelect1" />
                        <telerik:AjaxUpdatedControl ControlID="DivEdit" />
                    </UpdatedControls>
                </telerik:AjaxSetting>--%>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
            Skin="Windows7" />
        <div class="div_out">
            <div class="dqts">
                <div id="div_top" style="float: left;" class="dqts">
                    当前位置 &gt;&gt; 证书进京 &gt;&gt;
                <asp:Label ID="LabelPostType" runat="server" Text="三类人员"></asp:Label>
                    &gt;&gt; <strong>进京申请</strong>
                </div>
            </div>
            <div class="step">
                <div class="stepLabel">办理进度：</div>
                <div id="step_填报中" runat="server" class="stepItem lgray">填报中></div>
                <div id="step_待单位确认" runat="server" class="stepItem lgray">待单位确认></div>
                <div id="step_已申请" runat="server" class="stepItem lgray">已申请></div>
                <div id="step_已受理" runat="server" class="stepItem lgray">市级受理></div>
                <div id="step_已审核" runat="server" class="stepItem lgray">市级审核></div>
                <div id="step_已编号" runat="server" class="stepItem lgray">证书编号></div>
                <div id="step_证书已审核" runat="server" class="stepItem lgray">证书已审核（办结，下载电子证书）</div>
                <div class="stepArrow">▶</div>
            </div>

            <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                填报格式说明
            </div>
            <div class="DivContent" id="Td3">
                1、办事流程:个人网上申请并上传扫描件-企业网上审核确认-住建委网上审批（咨询电话:010-89150138）。<br />
                2、身份证样式：须为18位二代身份证（带X字母的必须使用英文大写）。<br />
                3、组织机构代码：9位数字或大写字母组合,带“-”的去掉“-”，社会统一信用代码中的第9位至第17位为企业的组织机构代码。<br />
                4、不再提供纸质证书补办业务，请企业或个人自行下载电子证书。<br />
                5、若个人无法在进京申请单加盖原企业和原发证机关的公章，可上传含有原企业和原发证机关公章的原省转出批准材料扫描件。<br />
                6、<b style="color: red">请申请人登录“全国工程质量安全监管信息平台公共服务门户”- 安全生产管理人员考核合格证书信息栏目查询，原证书信息应为“办理转出”状态方可提交进京申请。</b>
            </div>
            <div class="content">
                <div id="DivEdit" runat="server" style="width: 100%; margin: 10px auto; text-align: center; overflow: hidden;">
                    <div style="width: 66%; float: left; clear: left">
                        <table cellpadding="5" cellspacing="1" border="0" width="95%" align="center">
                            <tr>
                                <td align="left">申请日期：
                                <asp:Label ID="LabelApplyDate" runat="server" Text=""></asp:Label>
                                </td>
                                <td align="right">申请编号：
                                <asp:Label ID="LabelApplyCode" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table id="TableEdit" runat="server" width="95%" border="0" cellpadding="5" cellspacing="1"
                            class="table2" align="center">
                            <tr class="GridLightBK">
                                <td colspan="5" class="barTitle">个人基本信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="15%" nowrap="nowrap" align="center">
                                    <span style="color: Red">* </span>姓 名
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="200px" Skin="Default" MaxLength="20">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorWorkerName" runat="server"
                                        ErrorMessage="必填" ControlToValidate="RadTextBoxWorkerName"></asp:RequiredFieldValidator>
                                </td>
                                <td rowspan="4" align="center" style="width: 110px;">
                                    <img id="ImgCode" runat="server" height="140" width="110" alt="照片" src="" />
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="center">
                                    <span style="color: Red">* </span>性别
                                </td>
                                <td width="30%">
                                    <asp:RadioButton ID="RadioButtonMan" runat="server" Text="男" GroupName="1" Checked="true"
                                        Enabled="false" />&nbsp;<asp:RadioButton ID="RadioButtonWoman" Text="女" GroupName="1"
                                            runat="server" Enabled="false" />
                                </td>

                                <td align="center">
                                    <span style="color: Red">* </span>出生日期
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="RadDatePickerBirthday" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                        Width="98%" Enabled="false" />
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="center">
                                    <span style="color: Red">* </span>身份证号
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="RadTextBoxWorkerCertificateCode" runat="server" Width="80%"
                                        Skin="Default" MaxLength="18">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadTextBoxWorkerCertificateCode"></asp:RequiredFieldValidator>
                                </td>

                            </tr>
                            <tr class="GridLightBK">
                                <td align="center" nowrap="nowrap">
                                    <span style="color: Red">* </span>联系电话
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="RadTextBoxPhone" runat="server" Width="95%" Skin="Default" MaxLength="11">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPhone" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadTextBoxPhone" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="GridLightBK" id="tr_upPhotoTitle" runat="server" visible="false">
                                <td align="left" style="padding-left: 20px; line-height: 20px;" colspan="5">
                                    <span style="color: Red">* </span><span id="SpanTip" runat="server" style="font-size: 16px;">附件-照片：（格式要求：一寸近期彩色白底免冠标准证件照，jpg格式，最大为50K，宽高110 X 140像素）</span>
                                </td>
                            </tr>
                            <tr class="GridLightBK" id="tr_upPhoto" runat="server" visible="false">
                                <td colspan="2"><span onclick="javascript:layer.alert('<p style=\'font-size:15.0pt;font-family:仿宋;color:#333333\'><p style=\'font-size:18.0pt; text-align:center\'>上传电子照片要求</p><p>1.电子照片规格</p><p style=\'text-indent:30.0pt;\'>须上传近期彩色一寸白底标准正面免冠证件照。上传前，必须使用网上提供的“一寸照片生成器”将照片处理成要求的像素，以保证格式的正确。（本人近期彩色一寸白底标准正面免冠证件照，照片必须清晰，亮度足够，一寸jpg格式图片，最大为50K，宽高110 X 140像素）。</p><p>2.电子照片用途</p><p style=\'text-indent:30.0pt;\'>电子照片供考生参加考试和电子证书使用，请务必按要求上传照片。(避免因照片原因影响审核、考试及电子证书。)</p><p>3.上传照片注意</p><p style=\'text-indent:30.0pt;\'>（1）严禁上传风景照或生活照或艺术照，头像后不能出现杂物；</p><p style=\'text-indent:30.0pt;\'>（2）严禁上传使用摄像头、手机等非专业摄像装置拍摄的电子照片；</p><p style=\'text-indent:30.0pt;\'>（3）确保编辑好的电子照片头像轮廓清晰，不能模糊，照片上严禁出现姓名、号码和印章痕迹。</p></p>',{offset:'30px',icon:1,time:0,area: ['1000px', 'auto']});" style="color: blue; cursor: pointer;">【上传电子照片要求说明】</span>
                                    &nbsp;&nbsp;<a href="../Images/1寸照片生成器.exe" style="text-decoration: underline; color: Blue; font-size: 16px">
                                    >>1寸照片处理工具下载</a>
                                </td>
                                <td align="center">上传照片
                                </td>
                                <td align="left" style="font-size: 13px; font-weight: bold;" colspan="2">

                                    <telerik:RadUpload ID="RadUploadFacePhoto" runat="server" ControlObjectsVisibility="None"
                                        Height="23px" MaxFileInputsCount="1" OverwriteExistingFiles="True" Skin="Hot"
                                        EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" ReadOnlyFileInputs="False"
                                        Width="250px">
                                        <Localization Select="选 择" />
                                    </telerik:RadUpload>
                                </td>

                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="5" class="barTitle">现聘用单位信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="center">&nbsp;<span style="color: Red">* </span>现聘用单位全称
                                </td>
                                <td colspan="4">
                                    <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="70%" Skin="Default" MaxLength="100">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadTextBoxUnitName" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <%--  </td>
                                <td align="left" colspan="2">--%>
                                    <span id="divSelectUnit" runat="server" style="display: none;">
                                        <input id="ButtonSelectQY" type="button" value="请选择一个单位" class="bt_maxlarge" onclick="javascript: selectQY();" />
                                    </span>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right" nowrap="nowrap">
                                    <span style="color: Red">* </span>职务
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxJob" runat="server" Width="95%" NoWrap="true" OnSelectedIndexChanged="RadComboBoxJob_SelectedIndexChanged" AutoPostBack="true" CausesValidation="false">
                                    </telerik:RadComboBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <span style="color: Red">* </span>技术职称
                                </td>
                                <td colspan="2">
                                    <telerik:RadComboBox ID="RadComboBoxSKILLLEVEL" runat="server" Width="95%" NoWrap="true">
                                    </telerik:RadComboBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="center">
                                    <span style="color: Red">* </span>现聘用单位<br />
                                    机构代码
                                </td>
                                <td colspan="4">
                                    <telerik:RadTextBox ID="RadTextBoxCreditCode" runat="server" Width="250px" Skin="Default"
                                        MaxLength="18">
                                    </telerik:RadTextBox>
                                    <span style="color: #999999">（社会统一信用代码18位或组织机构代码9位）</span>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadTextBoxCreditCode" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="5" class="barTitle">跨省转入证书信息<asp:Label ID="LabelCertCheckStatus" runat="server" Text="" ForeColor="Green"></asp:Label></td>
                            </tr>
                            <tr class="GridLightBK">
                                <td></td>
                                <td colspan="4" align="left">
                                    <span id="SpanSelectCert" runat="server" style="display: none;">
                                        <input id="ButtonSelectCert" type="button" value=" 请选择一个证书 " class="bt_maxlarge" onclick="javascript: selectCert();" />
                                    </span>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right" nowrap="nowrap">
                                    <span style="color: Red">* </span>岗位工种
                                </td>
                                <td colspan="4">
                                    <uc1:PostSelect ID="PostSelect1" runat="server" Enabled="false" />

                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right" nowrap="nowrap">
                                    <span style="color: Red">* </span>发证机关
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="RadTextBoxConferUnit" runat="server" Width="97%" Skin="Default" MaxLength="100" ReadOnly="true">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadTextBoxConferUnit" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <span style="color: Red">* </span>发证日期
                                </td>
                                <td colspan="2">
                                    <telerik:RadDatePicker ID="RadDatePickerConferDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日" Enabled="false"
                                        Width="40%">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput DisplayDateFormat="yyyy-M-d" DateFormat="yyyy-M-d">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadDatePickerConferDate" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">
                                    <span style="color: Red">* </span>证书编号
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="RadTextBoxCertificateCode" runat="server" Width="97%" Skin="Default" MaxLength="50" ReadOnly="true">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadTextBoxCertificateCode" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <span style="color: Red">* </span>证书有效期
                                </td>
                                <td align="left" colspan="2">
                                    <div class="RadPicker">自</div>
                                    <telerik:RadDatePicker ID="RadDatePickerValidStartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日" Enabled="false"
                                        Width="40%">
                                        <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                            ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput DisplayDateFormat="yyyy-M-d" DateFormat="yyyy-M-d">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadDatePickerValidStartDate" Display="Dynamic"></asp:RequiredFieldValidator>
                                    <div class="RadPicker">至</div>
                                    <telerik:RadDatePicker ID="RadDatePickerValidEndDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日" Enabled="false"
                                        Width="40%">
                                        <Calendar runat="server" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False"
                                            ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput DisplayDateFormat="yyyy-M-d" DateFormat="yyyy-M-d">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadDatePickerValidEndDate" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="center">&nbsp;<span style="color: Red">* </span>原聘用单位全称
                                </td>
                                <td colspan="4">
                                    <telerik:RadTextBox ID="RadTextBoxOldUnitName" runat="server" Width="90%" Skin="Default" MaxLength="100" ReadOnly="true">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorOldUnitName" runat="server"
                                        ErrorMessage="必填" ControlToValidate="RadTextBoxOldUnitName" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="5" class="barTitle">数据校验</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="5">
                                    <div runat="server" id="divSheBao" style="width: 95%; padding-left: 20px; text-align: left; line-height: 200%;"></div>
                                    <div runat="server" id="divFR" style="margin: 0 auto; padding-left: 20px; text-align: left; line-height: 200%;"></div>
                                    <div runat="server" id="divZACheckResult" style="margin: 0 auto; padding-left: 20px; text-align: left; line-height: 150%;"></div>
                                </td>
                            </tr>
                            <tr class="GridLightBK" id="trFuJanTitel" runat="server" visible="false">
                                <td colspan="5" class="barTitle">附件上传</td>
                            </tr>
                            <tr class="GridLightBK" id="trFuJan" runat="server" visible="false">
                                <td align="left" style="line-height: 20px;" colspan="5">
                                    <div class="DivContent">
                                        <p>附件要求说明：</p>
                                        <p runat="server" id="p_PostTyppe1_qyfzr" visible="false">
                                            三类人员安全生产考核（企业主要负责人）变更材料：<br />
                                            <div runat="server" id="p_FaRen" visible="true">
                                                企业法定代表人：<br />
                                                1.变更申请表：申请人签字、原聘用企业加盖公章、现聘用企业加盖公章、原发证机关公章齐全；<br />
                                                2.身份证：正反面上传；<br />
                                                3.变更申请表中职务选择“法定代表人”的，上传《营业执照》至电子扫描件“社保权益记录及其他劳动关系证明材料”；<br />
                                                4.原发证机关安全生产考核合格证书。<br />
                                                <br />
                                            </div>
                                            <div runat="server" id="p_NoFaRen" visible="true">
                                                其他企业主要负责人：<br />
                                                1.变更申请表：申请人签字、原聘用企业加盖公章、现聘用企业加盖公章、原发证机关公章齐全；<br />
                                                2.身份证：正反面上传；<br />
                                                3.变更申请表中职务选择“总经理（总裁）、分管安全生产的副总经理（副总裁）、分管生产经营的副总经理（副总裁）、技术负责人、安全总监”的，上传以下材料至电子扫描件“社保权益记录及其他劳动关系证明材料”：（社保校验结果为符合的，不用上传；不符合的，需上传社保权益记录及其他劳动关系证明材料。）<br />
                                                &nbsp;&nbsp;①已在申请单位缴纳申请之日上一个月社会保险的，须提交《社会保险个人权益记录》；<br />
                                                &nbsp;&nbsp;②已在申请单位本市或外省市分支机构缴纳申请之日上一个月社会保险的，须提交该单位分支机构的《营业执照》和《社会保险个人权益记录》。<br />
                                                4.原发证机关安全生产考核合格证书。<br />
                                            </div>
                                        </p>
                                        <p runat="server" id="p_PostTyppe1_xmfzr" visible="false">
                                            三类人员安全生产考核（项目负责人）变更材料：<br />
                                            1.变更申请表：申请人签字、原聘用企业加盖公章、现聘用企业加盖公章、原发证机关公章齐全；<br />
                                            2.身份证：正反面上传；<br />
                                            3.变更申请表中职务选择“项目负责人”的，上传以下材料至电子扫描件“社保权益记录及其他劳动关系证明材料”：（社保校验结果为符合的，不用上传；不符合的，需上传社保权益记录及其他劳动关系证明材料。）<br />
                                            &nbsp;&nbsp;①已在申请单位缴纳申请之日上一个月社会保险的，须提交《社会保险个人权益记录》；<br />
                                            &nbsp;&nbsp;②已在申请单位本市或外省市分支机构缴纳申请之日上一个月社会保险的，须提交该单位分支机构的《营业执照》和《社会保险个人权益记录》。<br />
                                            4.原发证机关安全生产考核合格证书。<br />
                                        </p>
                                        <p runat="server" id="p_PostTyppe1_aqy" visible="false">
                                            三类人员安全生产考核（专职安全生产管理人员）变更材料：<br />
                                            1.变更申请表：申请人签字、原聘用企业加盖公章、现聘用企业加盖公章、原发证机关公章齐全；<br />
                                            2.身份证：正反面上传；<br />
                                            3.社保权益记录及其他劳动关系证明材料：（社保校验结果为符合的，不用上传；不符合的，需上传社保权益记录及其他劳动关系证明材料。）<br />
                                            &nbsp;&nbsp;①已在申请单位缴纳申请之日上一个月社会保险的，须提交《社会保险个人权益记录》；<br />
                                            &nbsp;&nbsp;②已在申请单位本市或外省市分支机构缴纳申请之日上一个月社会保险的，须提交该单位分支机构的《营业执照》和《社会保险个人权益记录》。<br />
                                            &nbsp;&nbsp;③已在劳务派遣单位缴纳申请之日上一个月社会保险的，须提交劳务派遣单位的《劳务派遣经营许可证》、劳务派遣单位与申请人订立的劳动合同、劳务派遣单位与用工单位签订的劳务派遣协议和申请人在劳务派遣单位参加社会保险的《社会保险个人权益记录》。<br />
                                            4.原发证机关安全生产考核合格证书。<br />
                                        </p>
                                    </div>
                                    <div class="fujian">
                                        变更申请表扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.变更申请表扫描件%>','变更申请表扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（请打印申请表，聘用单位盖章签字后扫描上传，jpg格式图片最大500K）</span>
                                    </div>
                                    <div class="fujian">
                                        身份证扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.证件扫描件%>','证件扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（上传身份证正反面扫描件原件、jpg格式图片，jpg格式图片最大500K）</span>
                                    </div>
                                    <div class="fujian" runat="server" id="div_SheBao" visible="true">
                                        社保权益记录及其它劳动关系证明材料：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.社保扫描件%>','社保扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（上传个人社保权益记录，jpg格式图片最大500K）</span>
                                    </div>
                                    <div class="fujian" runat="server" id="div_YingYeZhiZhao" visible="false">
                                        企业营业执照扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.企业营业执照扫描件%>','企业营业执照扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传企业营业执照扫描件原件，jpg格式图片,最大500K）</span>
                                    </div>
                                    <div class="fujian">
                                        安全生产考核证书扫描件（原省级主管机关核发安全生产考核合格证书或电子证书）：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.安全生产考核证书扫描件%>','安全生产考核证书扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传安全生产考核证书扫描件原件、jpg格式图片,最大500K）</span>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <%--  <div runat="server" id="divSheBao" style="width: 95%; padding-left: 20px; text-align: left; line-height: 40px;"></div>--%>
                        <div id="divCheckHistory" runat="server" style="width: 95%; padding-top: 20px; text-align: center; clear: both; margin: 10px auto;">
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
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="流程" UniqueName="Action" DataField="Action">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="办理人" UniqueName="ActionMan" DataField="ActionMan" Display="false">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="办理时间" UniqueName="ActionData" DataField="ActionData" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="办理结果" UniqueName="ActionResult" DataField="ActionResult">
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="办理意见" UniqueName="ActionRemark" DataField="ActionRemark">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <HeaderStyle Font-Bold="True" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" />
                                                <ItemStyle CssClass="subtable" />
                                                <AlternatingItemStyle CssClass="subtable" />
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divWorker" runat="server" visible="false" style="padding: 30px 20px 50px 20px; width: auto">
                            <div id="divStepDesc" runat="server" style="line-height: 300%;">操作流程：个人申请保存-->点击导出打印(导出申请表)-->上传相关附件-->提交单位审核</div>
                            <asp:Button ID="ButtonSave" Text='保 存' runat="server" CssClass="button" OnClick="ButtonSave_Click"></asp:Button>&nbsp;
                             <asp:Button ID="ButtonExport" runat="server" Text="导出打印" CssClass="bt_large" OnClick="ButtonExport_Click"
                                 Enabled="False" />&nbsp;
                        <asp:Button ID="ButtonExit" Text="取消申报" runat="server" CssClass="bt_large" Enabled="false"
                            OnClick="ButtonExit_Click"></asp:Button>&nbsp;
                                <asp:Button ID="ButtonDelete" Text='删 除' runat="server" CssClass="button" OnClick="ButtonDelete_Click"
                                    OnClientClick="return confirm('你确定要删除吗?');" Enabled="false"></asp:Button>&nbsp;                              
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
                                    <asp:Button ID="ButtonUnitCheck" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonUnitCheck_Click" />&nbsp;&nbsp;
                                         
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
                        <table id="TableJWAccept" runat="server" visible="false" border="0" cellpadding="5" cellspacing="1" width="100%" class="table" style="text-align: center; margin: 10px auto">
                            <tr class="GridLightBK">
                                <td colspan="2" class="barTitle">进京受理</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="20%" align="right">处理结果：</td>
                                <td width="80%" align="left">
                                    <asp:RadioButtonList ID="RadioButtonListJWAccept" runat="server" RepeatDirection="Vertical" TextAlign="right">
                                        <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="20%" align="right">处理意见：</td>
                                <td width="80%" align="left">

                                    <asp:TextBox ID="TextBoxGetResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="100" Text="通过"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="2" align="center">
                                    <asp:Button ID="ButtonAccept" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonAccept_Click" />&nbsp;&nbsp;
                                         
                                </td>
                            </tr>
                        </table>
                        <table id="TableJWCheck" runat="server" visible="false" border="0" cellpadding="5" cellspacing="1" width="100%" class="table" style="text-align: center; margin: 10px auto">
                            <tr class="GridLightBK">
                                <td colspan="2" class="barTitle">进京审核</td>
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

                                    <asp:TextBox ID="TextBoxCheckResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="100" Text="通过"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="2" align="center">
                                    <asp:Button ID="ButtonCheck" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonCheck_Click" />&nbsp;&nbsp;
                                         
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
                                                                    <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="../images/Cancel.gif" CommandName="Delete" Visible='<%#(IfExistRoleID("0") == true && ((ViewState["CertificateEnterApplyOB"] as Model.CertificateEnterApplyOB).ApplyStatus == Model.EnumManager.CertificateEnterStatus.NewSave||(ViewState["CertificateEnterApplyOB"] as Model.CertificateEnterApplyOB).ApplyStatus == Model.EnumManager.CertificateEnterStatus.SendBack))?true:false%>' OnClientClick="javascript:if(confirm('您确定要删除么?')==false) return false" />

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
            </div>
        </div>
        <div id="winpop"></div>
    </form>
</body>
</html>

