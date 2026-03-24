<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamSign.aspx.cs" Inherits="ZYRYJG.EXamManage.ExamSign" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../ExamNotice.ascx" TagName="ExamNotice" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head2" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <link href="../Skins/Hot/Upload.hot.css" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form2" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager2" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">

                function onRequestStart(sender, args) {
                    if (args.get_eventTarget().indexOf('<%=btnSave.UniqueID %>') >= 0
                || args.get_eventTarget().indexOf('<%=ButtonExport.UniqueID %>') >= 0
                        || args.get_eventTarget().indexOf('<%=ButtonOutPutSignupPromise.UniqueID %>') >= 0
                        ) {
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

                //function RowSelecting(sender, args) {
                //    var id = args.get_id();
                //    var inputCheckBox = $get(id).getElementsByTagName("input")[0];
                //    SelectMeOnly(inputCheckBox, 'CheckBoxSIGNUPPLACEID');
                //}


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
                function selectQY(){
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
                                var RadTextBoxUnitCode = $find("<%= RadTextBoxUnitCode.ClientID%>");
                                var RadTextBoxUnitName = $find("<%= RadTextBoxUnitName.ClientID%>");
                                RadTextBoxUnitCode.set_value(data.ENT_OrganizationsCode);
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

                function toFullWidth(str) {
                    return str.replace(/[!-~]/g, function (match) {
                        return String.fromCharCode(match.charCodeAt(0) + 0xfee0);
                    });
                }
                function OnDateChanged(dateInput, args) {
                    dateInput.set_value(toFullWidth(dateInput.get_value()));
                }  
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <ClientEvents OnRequestStart="onRequestStart" />
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="ButtonExport">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="ButtonExport" UpdatePanelRenderMode="Inline" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="main">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="main" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
            Skin="Windows7" />
        <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
            ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt; 考务管理 &gt;&gt;
                考试报名 &gt;&gt; <strong>考试报名</strong>
                </div>
            </div>
            <div runat="server" id="main" style="margin: 5px auto; padding: 20px 0px;">
                <div style="float: right; padding: 10px 30px 0px 0px;">
                    <%-- <uc1:ExamNotice ID="ExamNotice1" runat="server" />--%>
                </div>
                <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                    填报格式说明
                </div>
                <div class="DivContent" id="Td3">
                    <div id="divSignupDesc1" runat="server">
                        1、办事流程：考生应登录市住房城乡建设委门户网站在线填写报考信息提交企业确认 >> 企业初审确认后提交市住房城乡建设委审核 >> 市住房城乡建设委审核考生报考资格 
                        <br />
                        <div style="padding-left: 104px">>> 考生查询报考资格审核结果 >> 通过审核的考生自行下载打印准考证（在线网考的考生按规定时间及相关要求参加模拟测试） >> 考生按规定时间及相关要求参加考核  </div>
                        <div style="padding-left: 104px">>> 市住房城乡建设委在门户网站发布考核合格人员通告 >> 考核合格的考生自行下载打印电子证照。</div>

                        2、报名时填写的个人和单位信息必须真实有效，否则对后期系统使用和证书制作带来的后果自负。当日报名,次日显示社保比对结果，请考生及时登录系统查看审核结果。<br />
                        3、<b>个人报名截止前，个人可以点击“取消申报”，及时修改、补充材料后再次提交企业确认；个人报名截止后，个人在本次报名期间从未提交企业确认的，本次考试报名提交不成功。</b><br />
                        4、<b>企业确认截止前，企业驳回个人申请，个人应及时修改、补充材料后再次提交企业确认；企业确认截止后，个人无法修改、补充材料，企业无法确认。</b><br />
                        5、<b>企业确认截止前，个人或企业均可点击“取消申报”撤回已提交市住建委审核的申请单，个人应及时修改、补充材料后再次提交企业确认。</b><br />
                        <div style="padding-left: 24px"><b>企业确认截止后，市住建委进行一次性审核。审核不通过的，个人无法进行修改、补充及提交，请申请人认真复核考试报名材料，确保符合审核要求。</b></div>
                        6、审核合格者按全年考试计划的规定日期打印准考证，考生持准考证和身份证按准考证上的要求参加考试。<br />
                        7、日期格式：2010-01-01或2010-1-1，其中分隔符为英文减号“-”。<br />
                        8、组织机构代码：9位数字或大写字母组合,带“-”横杠的去掉横杠，社会统一信用代码中的第9位至第17位就是企业的组织机构代码；查询请登录<a title="组织机构代码查询" href="https://www.cods.org.cn"
                            target="_blank" style="color: Blue; text-decoration: underline;">https://www.cods.org.cn</a>
                        网站，在“信息核查”栏目中查询。<br />
                        9、照片规格：<span style="color: red">近期彩色标准1寸半身免冠证件照，照片底色背景为白色，附件是在正确位置上传。</span>50k以内，宽高110 x 140像素且必须为jpg格式图片。<br />
                        <div style="padding-left: 24px">推荐使用“<a href="../Images/1寸照片生成器.exe" style="text-decoration: underline; color: Blue;">1寸照片生成器.exe</a>”工具调整大小，确保图片可用。</div>
                        10、如一年内缺考累计三次（统计日期自第一次缺考之日起，按周年计算），第三次缺考系统将自动锁定；自系统锁定之日起，一年内不得参加我市建设行业各类考试报名。
                    </div>
                    <div id="divSignupDesc2" runat="server" style="display: none;">
                        1、办事流程：个人登录市住房城乡建设委门户网站（办事大厅系统人员资格管理信息系统）填写报考信息 > 个人提交报名申请至培训点 > 培训点网上审核报名资格，组织考试
                        <br />
                        <div style="padding-left: 104px">> 个人按时按培训点要求参加考试 > 市住房城乡建设委门户网站发布考核合格人员通告 > 个人自行下载打印电子证照。</div>
                        2、报名时填写的个人信息必须真实有效，否则对后期系统使用和证书制作带来的后果自负。<br />
                        3、照片规格：<span style="color: red">近期彩色标准1寸半身免冠证件照，照片底色背景为白色，附件是在正确位置上传。</span>50k以内，宽高110 x 140像素且必须为jpg格式图片。<br />
                        <div style="padding-left: 24px">推荐使用“<a href="../Images/1寸照片生成器.exe" style="text-decoration: underline; color: Blue;">1寸照片生成器.exe</a>”工具调整大小，确保图片可用。</div>
                    </div>
                </div>
                <div style="text-align: left; padding-left: 40px;">
                    报名批号:<asp:Label ID="lblSignUpCode" runat="server" Text=""></asp:Label>
                </div>
                <div style="width: 100%; margin: 0; text-align: center; overflow: hidden;">
                    <div style="width: 66%; float: left; clear: left" runat="server" id="divExamSignUp">
                        <table id="tableUnit" runat="server" border="0" cellpadding="5" cellspacing="1" class="table2" align="center" style="width: 100%">
                            <tr class="GridLightBK">
                                <td colspan="5" class="barTitle">所在单位信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="right" colspan="5">
                                    <input id="ButtonSelectQY" type="button" value="请选择一个单位" class="bt_maxlarge" onclick="javascript: selectQY();" />
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="right" colspan="2">
                                    <span style="color: Red">* </span>社会统一信用代码/组织机构代码
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="RadTextBoxUnitCode" runat="server" Width="95%" Skin="Default"
                                        AutoPostBack="True" OnTextChanged="RadTextBoxUnitCode_TextChanged" MaxLength="18">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadTextBoxUnitCode" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="right" colspan="2">&nbsp;<span style="color: Red">* </span>单位名称（全称）&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox ID="RadTextBoxUnitName" runat="server" Width="95%" AutoPostBack="False"
                                        Skin="Default" MaxLength="100">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadTextBoxUnitName" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
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
                            <tr class="GridLightBK" runat="server" id="trJob" visible="true">
                                <td align="right" width="15%"><span style="color: Red">* </span>职务</td>
                                <td align="left">
                                    <telerik:RadComboBox ID="RadComboBoxJob" runat="server" Width="95%" NoWrap="true" OnSelectedIndexChanged="RadComboBoxJob_SelectedIndexChanged" AutoPostBack="true">
                                    </telerik:RadComboBox>
                                </td>
                                <td align="left" colspan="3">安管人员在受聘企业担任的职务</td>
                            </tr>
                        </table>
                        <table id="tableTrainUnit" runat="server" border="0" cellpadding="5" cellspacing="1" class="table2" align="center" style="width: 100%" visible="false">
                            <tr class="GridLightBK">
                                <td colspan="5" class="barTitle">培训考核机构</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="10%" nowrap="nowrap" align="right"></td>
                                <td nowrap="nowrap" align="left" colspan="4">
                                    <asp:Label ID="LabelTrainUnit" runat="server" Text="Label"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table border="0" cellpadding="5" cellspacing="1" class="table2" align="center" style="width: 100%">
                            <tr class="GridLightBK">
                                <td colspan="5" class="barTitle">人员基本信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="5" style="text-align: left; color: orangered;">重要提示：报名表照片须上传标准证件照,附件须按要求在正确的位置上传,重要提示：报名表照片须上传标准证件照,附件须按要求在正确的位置上传,恶意上传照片和附件或提供虚假材料的将严肃处理。</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="10%" nowrap="nowrap" align="right">
                                    <span style="color: Red">* </span>证件类别
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxCertificateType" runat="server" Width="95%" NoWrap="true" Enabled="false">
                                    </telerik:RadComboBox>
                                </td>
                                <td align="right">
                                    <span style="color: Red">* </span>证件号码
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="RadTextCertificateCode" runat="server" Width="80%" Skin="Default" Enabled="false" OnTextChanged="RadTextCertificateCode_TextChanged" AutoPostBack="true" CausesValidation="false"
                                        MaxLength="50">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadTextCertificateCode"></asp:RequiredFieldValidator>
                                </td>
                                <td width="110px" rowspan="3" align="center">
                                    <img id="ImgCode" runat="server" height="140" width="110" alt="一寸照片" />
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="right">
                                    <span style="color: Red">* </span>姓名
                                </td>
                                <td>
                                    <telerik:RadTextBox ID="RadTextBoxWorkerName" runat="server" Width="80%" Skin="Default" Enabled="false"
                                        MaxLength="50">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadTextBoxWorkerName"></asp:RequiredFieldValidator>
                                    <asp:HiddenField ID="HiddenFieldPhone" runat="server" />
                                    <asp:HiddenField ID="HiddenFieldBirthday" runat="server" />
                                </td>
                                <td align="right">
                                    <span style="color: Red">* </span>性别
                                </td>
                                <td>
                                    <asp:RadioButton ID="RadioButtonMan" runat="server" Text="男" GroupName="1" Checked="true" />&nbsp;<asp:RadioButton
                                        ID="RadioButtonWoman" Text="女" GroupName="1" runat="server" />
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">
                                    <span style="color: Red">* </span>出生日期
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="RadDatePickerBirthday" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                        Width="98%" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorBirthday" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadDatePickerBirthday" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td align="right"><span style="color: Red">* </span>民族
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxNation" runat="server" Width="95%" NoWrap="true">
                                    </telerik:RadComboBox>
                                    <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxNation"
                                        ErrorMessage="必填" runat="server" ID="Comparevalidator4" ForeColor="Red" Display="Dynamic" />
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="5" align="left">
                                    <div style="text-align: left; padding-right: 40px; font-size: 14px;">
                                        <span style="color: Red">* </span><span id="SpanTip" runat="server">报名照片：（格式要求：一寸近期彩色白底免冠标准证件照，jpg格式，最大为50K，宽高110 X 140像素）</span>
                                        <a href="../Images/1寸照片生成器.exe" style="text-decoration: underline; color: Blue;">>> 1寸照片处理辅助工具下载.exe</a>
                                    </div>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="center" colspan="2">
                                    <p onclick="javascript:layer.alert('<p style=\'font-size:15.0pt;font-family:仿宋;color:#333333\'><p style=\'font-size:18.0pt; text-align:center\'>考试报名上传电子照片要求</p><p>1.电子照片规格</p><p style=\'text-indent:30.0pt;\'>考生须上传近期彩色一寸白底标准正面免冠证件照。上传前，必须使用网上报名流程中提供的“一寸照片生成器”将照片处理成报考文件中要求的像素，以保证格式的正确。（本人近期彩色一寸白底标准正面免冠证件照，照片必须清晰，亮度足够，一寸jpg格式图片，最大为50K，宽高110 X 140像素）。</p><p>2.电子照片用途</p><p style=\'text-indent:30.0pt;\'>电子照片供考生参加考试和电子证书使用，请考生务必按要求上传照片。(避免因照片原因影响审核、考试及电子证书。)</p><p>3.上传照片注意</p><p style=\'text-indent:30.0pt;\'>（1）严禁上传风景照或生活照或艺术照，头像后不能出现杂物；</p><p style=\'text-indent:30.0pt;\'>（2）严禁上传使用摄像头、手机等非专业摄像装置拍摄的电子照片；</p><p style=\'text-indent:30.0pt;\'>（3）确保编辑好的电子照片头像轮廓清晰，不能模糊，照片上严禁出现姓名、号码和印章痕迹。</p></p>',{offset:'30px',icon:1,time:0,area: ['1000px', 'auto']});" style="color: blue; cursor: pointer;">【考试报名上传电子照片要求说明】</p>
                                </td>
                                <td align="right">上传照片：
                                </td>
                                <td colspan="2">
                                    <telerik:RadUpload ID="RadUploadFacePhoto" runat="server" ControlObjectsVisibility="None"
                                        MaxFileInputsCount="1" OverwriteExistingFiles="True" Skin="Hot"
                                        EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" ReadOnlyFileInputs="False"
                                        Width="200px">
                                        <Localization Select="选 择" />
                                    </telerik:RadUpload>
                                </td>
                            </tr>

                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="right"><span style="color: Red">* </span>文化程度
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxCulturalLevel" runat="server" Width="95%" NoWrap="true">
                                    </telerik:RadComboBox>
                                    <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxCulturalLevel"
                                        ErrorMessage="必填" runat="server" ID="Comparevalidator3" ForeColor="Red" Display="Dynamic" />
                                </td>
                                <td align="right" width="10%" nowrap="nowrap"><span style="color: Red">* </span>政治面貌
                                </td>
                                <td colspan="2">
                                    <telerik:RadComboBox ID="RadComboBoxPoliticalBackground" runat="server" Width="95%" NoWrap="true">
                                    </telerik:RadComboBox>
                                    <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxPoliticalBackground"
                                        ErrorMessage="必填" runat="server" ID="Comparevalidator2" ForeColor="Red" Display="Dynamic" />
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right">
                                    <span style="color: Red">* </span>技术职称<br />
                                    或技术等级
                                </td>
                                <td>
                                    <telerik:RadComboBox ID="RadComboBoxSKILLLEVEL" runat="server" Width="95%" NoWrap="true">
                                    </telerik:RadComboBox>
                                    <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxSKILLLEVEL"
                                        ErrorMessage="必填" runat="server" ID="Comparevalidator1" ForeColor="Red" Display="Dynamic" />
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <span style="color: Red">* </span>联系电话
                                </td>
                                <td colspan="2">
                                    <telerik:RadTextBox ID="RadTextBoxPhone" runat="server" Width="95%" Skin="Default"
                                        MaxLength="50">
                                    </telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPhone" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadTextBoxPhone" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>

                            <tr class="GridLightBK">
                                <td colspan="5" class="barTitle">报名信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="right">考试计划
                                </td>
                                <td colspan="4">
                                    <telerik:RadTextBox ID="RadTextBoxExamPlanName" runat="server" Width="95%" Skin="Default"
                                        ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="right">申报岗位工种
                                </td>
                                <td colspan="4">
                                    <telerik:RadTextBox ID="RadTextPostID" runat="server" Width="95%" Skin="Default"
                                        ReadOnly="true">
                                    </telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="right">报名时间
                                </td>
                                <td>
                                    <asp:Label ID="SignUpStartDate" runat="server" Text=""></asp:Label>~
                                <asp:Label ID="SignUpEndDate" runat="server" Text=""></asp:Label>
                                </td>


                                <td nowrap="nowrap" align="right">审核时间</td>
                                <td colspan="2">
                                    <asp:Label ID="LatestCheckDate" runat="server" Text=""></asp:Label>
                                </td>

                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="right">准考证发放开时间</td>
                                <td>
                                    <asp:Label ID="ExamCardSendStartDate" runat="server"></asp:Label>~<asp:Label ID="ExamCardSendEndDate" runat="server"></asp:Label>
                                </td>

                                <td nowrap="nowrap" align="right">考试日期
                                </td>
                                <td colspan="2">
                                    <asp:Label ID="ExamStartDate" runat="server" Text=""></asp:Label>~ 
                                <asp:Label ID="ExamEndDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="right">从事本岗位工作的时间
                                </td>
                                <td>
                                    <telerik:RadDatePicker ID="RadDatePickerWorkStartDate" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                        Width="98%">
                                        <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                        </Calendar>
                                        <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        <DateInput DisplayDateFormat="yyyy-M-d" DateFormat="yyyy-M-d">
                                        </DateInput>
                                    </telerik:RadDatePicker>
                                </td>
                                <td nowrap="nowrap" align="right">从事工作年限
                                </td>
                                <td colspan="2">
                                    <telerik:RadNumericTextBox ID="RadTextBoxWorkYearNumer" runat="server" MaxLength="3"
                                        Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true"
                                        MinValue="0">
                                        <NumberFormat DecimalDigits="0"></NumberFormat>
                                    </telerik:RadNumericTextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="5" class="barTitle"><span style="color: Red">* </span>个人简历</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="5">
                                    <telerik:RadTextBox ID="RadTextBoxPersonDetail" runat="server" Width="98%" Skin="Default" Font-Size="14px"  
                                        TextMode="MultiLine" Height="60px" MaxLength="250">
                                        <ClientEvents OnValueChanged="OnDateChanged" />
                                    </telerik:RadTextBox>
                                                       <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="必填"
                                        ControlToValidate="RadTextBoxPersonDetail" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="5" class="barTitle"><span style="color: Red">* </span>本人承诺</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="5">
                                    <asp:CheckBox ID="CheckBoxPromise" runat="server" />
                                    本人承诺所提供的个人信息和证明材料真实准确, 对因提供有关信息, 证件不实或违反有关规定造成的后果, 责任自负。
                                </td>
                            </tr>
                            <tr class="GridLightBK" id="trSafteTrain" runat="server" visible="false">
                                <td colspan="5" style="padding: 0; margin: 0">
                                    <table border="0" cellpadding="5" cellspacing="1" class="table2" align="center" style="width: 100%;">
                                        <tr class="GridLightBK">
                                            <td colspan="4" class="barTitle">考前安全作业培训记录</td>
                                        </tr>
                                        <tr class="GridLightBK" id="tr1" runat="server">
                                            <td align="right" width="15%">安全作业培训类型
                                            </td>
                                            <td colspan="3">
                                                <asp:RadioButtonList ID="RadioButtonListSafeTrainType" runat="server" AutoPostBack="true" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonListSafeTrainType_SelectedIndexChanged">
                                                    <asp:ListItem Text="企业自行培训" Value="自行" Selected="true"></asp:ListItem>
                                                    <asp:ListItem Text="委托培训机构" Value="委托培训机构"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK" id="trTrainUnit1" runat="server" visible="false">
                                            <td align="right">培训机构名称
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="RadTextBoxSafeTrainUnit" runat="server" Width="95%" Skin="Default"
                                                    MaxLength="128">
                                                </telerik:RadTextBox>
                                            </td>
                                            <td align="right" nowrap="nowrap">办学许可证编号
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="RadTextBoxSafeTrainUnitCode" runat="server" Width="95%" Skin="Default"
                                                    MaxLength="64">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK" id="trTrainUnit2" runat="server" visible="false">
                                            <td align="right">办学许可证有效期
                                            </td>
                                            <td>
                                                <telerik:RadDatePicker ID="RadDatePickerSafeTrainUnitValidEndDate" MinDate="01/01/2000" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                                    Width="98%">
                                                    <Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x">
                                                    </Calendar>
                                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                    <DateInput DisplayDateFormat="yyyy-M-d" DateFormat="yyyy-M-d">
                                                    </DateInput>
                                                </telerik:RadDatePicker>
                                            </td>
                                            <td align="right" nowrap="nowrap">办学许可证发证机关
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="RadTextBoxSafeTrainUnitOfDept" runat="server" Width="95%" Skin="Default"
                                                    MaxLength="128">
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td colspan="4" style="border-collapse: collapse">
                                                <%--                   <div id="divAnQuanPXFinishCase" runat="server" style="color: red"></div>--%>
                                                <telerik:RadGrid ID="RadGridAnQuanPX" runat="server" AutoGenerateColumns="False" AllowSorting="false" HeaderStyle-BorderColor="#eee"
                                                    GridLines="None" CellPadding="0" Width="98%" Skin="Blue" EnableAjaxSkinRendering="false"
                                                    EnableEmbeddedSkins="false" OnItemDataBound="RadGridAnQuanPX_ItemDataBound" OnInsertCommand="RadGridAnQuanPX_InsertCommand" OnUpdateCommand="RadGridAnQuanPX_UpdateCommand"
                                                    OnDeleteCommand="RadGridAnQuanPX_DeleteCommand" OnNeedDataSource="RadGridAnQuanPX_NeedDataSource">
                                                    <ClientSettings EnableRowHoverStyle="true">
                                                        <Selecting AllowRowSelect="True" />
                                                    </ClientSettings>
                                                    <MasterTableView DataKeyNames="DetailID" EditMode="EditForms" CommandItemDisplay="Top" NoMasterRecordsText="没有可显示的记录">
                                                        <Columns>
                                                            <telerik:GridBoundColumn HeaderText="序号" UniqueName="DataNo" DataField="DataNo" AllowSorting="false">
                                                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </telerik:GridBoundColumn>

                                                            <telerik:GridTemplateColumn HeaderText="培训时间" UniqueName="TrainDateStart">
                                                                <ItemTemplate>
                                                                    <%# string.Format("{0}至{1}",Eval("TrainDateStart")==DBNull.Value?"":Convert.ToDateTime(Eval("TrainDateStart")).ToString("yyyy-MM-dd")
                                                        ,Eval("TrainDateEnd")==DBNull.Value?"":Convert.ToDateTime(Eval("TrainDateEnd")).ToString("yyyy-MM-dd"))
                                                                    %>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </telerik:GridTemplateColumn>
                                                            <telerik:GridBoundColumn UniqueName="TrainType" DataField="TrainType" HeaderText="培训类型">
                                                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn UniqueName="TrainName" DataField="TrainName" HeaderText="培训内容">
                                                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn UniqueName="TrainWay" DataField="TrainWay" HeaderText="培训方式">
                                                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridBoundColumn UniqueName="Period" DataField="Period" HeaderText="学时">
                                                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </telerik:GridBoundColumn>
                                                            <telerik:GridEditCommandColumn UniqueName="Edit" EditText="编辑" EditImageUrl="../Images/jia.gif" ButtonType="ImageButton">
                                                            </telerik:GridEditCommandColumn>
                                                            <telerik:GridButtonColumn UniqueName="Delete" CommandName="Delete" ConfirmText="您确定要删除么?" Text="&lt;span onclick=&quot;if(!confirm('您确定要删除么?'))return false; &quot; &gt;删除&lt;/span&gt;" ButtonType="ImageButton" ImageUrl="../Images/close.png">
                                                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                                            </telerik:GridButtonColumn>
                                                        </Columns>
                                                        <CommandItemSettings AddNewRecordText="点击这里添加培训记录" ShowRefreshButton="false" />
                                                        <EditFormSettings InsertCaption="添加培训记录" CaptionFormatString="编辑培训记录"
                                                            EditFormType="Template" PopUpSettings-Modal="false" FormCaptionStyle-HorizontalAlign="Left">
                                                            <EditColumn UniqueName="EditCommandColumn1">
                                                            </EditColumn>
                                                            <FormTemplate>
                                                                <div id="DivEdit" runat="server" style="width: 100%; margin: 0 auto; background-color: #fefefe;">
                                                                    <br />
                                                                    <table class="bar_cx" style="border-collapse: collapse; width: 100%;">
                                                                        <tr>
                                                                            <td align="right" style="width: 20%;">序号：
                                                                            </td>
                                                                            <td align="left" style="width: 80%">
                                                                                <telerik:RadNumericTextBox ID="RadNumericTextBoxDataNo" runat="server" MaxLength="5"
                                                                                    Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="70px" MinValue="1" MaxValue="1000">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                                                </telerik:RadNumericTextBox>（用于排序）
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必填"
                                                                                    ControlToValidate="RadNumericTextBoxDataNo" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">培训时间：
                                                                            </td>
                                                                            <td align="left">
                                                                                <telerik:RadDatePicker ID="RadDatePickerTrainDateStart" runat="server" Width="120px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                                                                </telerik:RadDatePicker>
                                                                                <div style="float: left; padding: 0 12px">至</div>
                                                                                <telerik:RadDatePicker ID="RadDatePickerTrainDateEnd" runat="server" Width="120px" Calendar-DayCellToolTipFormat="yyyy年MM月dd日">
                                                                                </telerik:RadDatePicker>（要求：考试报名前1年内）
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="必填"
                                                                                    ControlToValidate="RadDatePickerTrainDateStart" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填"
                                                                                    ControlToValidate="RadDatePickerTrainDateEnd" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr id="trTrainUnit" runat="server">
                                                                            <td align="right">培训类型：
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:RadioButtonList ID="RadioButtonListTrainType" runat="server" RepeatDirection="Horizontal" Width="150px">
                                                                                    <asp:ListItem Text="理论" Value="理论" Selected="true"></asp:ListItem>
                                                                                    <asp:ListItem Text="实际操作" Value="实际操作"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">培训内容：
                                                                            </td>
                                                                            <td align="left">
                                                                                <telerik:RadTextBox runat="server" ID="RadTextBoxTrainName" Width="95%" Skin="Default"
                                                                                    MaxLength="1000" TextMode="MultiLine" Rows="2">
                                                                                </telerik:RadTextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="必填"
                                                                                    ControlToValidate="RadTextBoxTrainName" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">培训方式：
                                                                            </td>
                                                                            <td align="left">
                                                                                <asp:RadioButtonList ID="RadioButtonListTrainWay" runat="server" RepeatDirection="Horizontal" Width="150px">
                                                                                    <asp:ListItem Text="网络" Value="网络" Selected="true"></asp:ListItem>
                                                                                    <asp:ListItem Text="现场" Value="现场"></asp:ListItem>
                                                                                </asp:RadioButtonList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td align="right">学时：
                                                                            </td>
                                                                            <td align="left">
                                                                                <telerik:RadNumericTextBox ID="RadNumericTextBoxPeriod" runat="server" MaxLength="5"
                                                                                    Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="70px" MinValue="1" MaxValue="100">
                                                                                    <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                                                                                </telerik:RadNumericTextBox>
                                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="数字，必填"
                                                                                    ControlToValidate="RadNumericTextBoxPeriod" Display="Dynamic"></asp:RequiredFieldValidator>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    <div style="padding: 20px 100px 30px 0px; text-align: right">
                                                                        <asp:Button ID="ButtonSave" CssClass="button" Text='<%# (Container is GridEditFormInsertItem) ? "保 存" : "更 新" %>'
                                                                            runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                        <asp:Button ID="Button2" CssClass="button" Text="取 消" runat="server" CausesValidation="False"
                                            CommandName="Cancel"></asp:Button>
                                                                    </div>
                                                                </div>
                                                            </FormTemplate>
                                                            <PopUpSettings Modal="True" Width="500px"></PopUpSettings>
                                                        </EditFormSettings>
                                                        <HeaderStyle Font-Bold="True" />
                                                    </MasterTableView>
                                                </telerik:RadGrid>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trDataCheckHead" class="GridLightBK" runat="server">
                                <td colspan="5" class="barTitle">数据校验</td>
                            </tr>
                            <tr id="trDataChcekRow" class="GridLightBK" runat="server">
                                <td colspan="5">
                                    <div style="text-align: left; padding-left: 20px; line-height: 200%; font-size: 16px">
                                        <asp:Label ID="LabelCheckStep" runat="server" Text="当前审核阶段：待审核"></asp:Label>
                                    </div>
                                    <div runat="server" id="divSheBao" style="width: 95%; padding-left: 20px; text-align: left; line-height: 200%;"></div>
                                    <div runat="server" id="divFR" style="margin: 0 auto; padding-left: 20px; text-align: left; line-height: 200%;"></div>
                                    <div runat="server" id="divZACheckResult" style="margin: 0 auto; padding-left: 20px; text-align: left; line-height: 150%;"></div>
                                </td>
                            </tr>
                            <tr class="GridLightBK" id="trFuJanTitel" runat="server" visible="false">
                                <td colspan="5" class="barTitle">附件上传</td>
                            </tr>
                            <tr class="GridLightBK" id="trFuJan" runat="server" visible="false">
                                <td align="left" style="padding-left: 20px; line-height: 20px;" colspan="5">
                                    <div class="DivContent">
                                        <p>附件要求说明：</p>
                                        <p runat="server" id="p_PostTyppe1_qyfzr" visible="false">
                                            三类人员安全生产考核（企业主要负责人）报考材料：<br />
                                            <div runat="server" id="p_FaRen" visible="true">
                                                ①企业法定代表人：<br />
                                                &nbsp;1.考试报名表：申请人签字并加盖聘用单位公章；<br />
                                                &nbsp;2.身份证：正反面上传；<br />
                                                &nbsp;<span style="color: red">3.考试报名表中职务选择“法定代表人”的，上传聘用单位的《营业执照》至电子扫描件“社保权益记录及其他劳动关系证明材料”。</span><br />
                                                <br />
                                            </div>
                                            <div runat="server" id="p_NoFaRen" visible="true">
                                                ②其他企业主要负责人：<br />
                                                &nbsp;1.考试报名表：申请人签字并加盖聘用单位公章；<br />
                                                &nbsp;2.身份证：正反面上传；<br />
                                                &nbsp;3.学历证书：相应学历的学历证书；<br />
                                                &nbsp;4.职称证书：相应职称的职称证书；<br />
                                                &nbsp;5.学历和职称证书选择承诺制申报的：上传承诺书；<br />
                                                &nbsp;6.考试报名表中职务选择“总经理（总裁）、分管安全生产的副总经理（副总裁）、分管生产经营的副总经理（副总裁）、技术负责人、安全总监”的，上传以下材料至电子扫描件“社保权益记录及其他劳动关系证明材料”：（社保校验结果为符合的，不用上传；不符合的，需上传社保权益记录及其他劳动关系证明材料。）<br />
                                                &nbsp;&nbsp;1）已在申请单位缴纳申请之日上一个月社会保险的，须提交《社会保险个人权益记录》；<br />
                                                &nbsp;&nbsp;2）已在申请单位本市或外省市分支机构缴纳申请之日上一个月社会保险的，须提交该单位分支机构的《营业执照》和《社会保险个人权益记录》。<br />
                                            </div>
                                        </p>
                                        <p runat="server" id="p_PostTyppe1_xmfzr" visible="false">
                                            三类人员安全生产考核（项目负责人）报考材料：<br />
                                            1.考试报名表：申请人签字并加盖聘用单位公章；<br />
                                            2.身份证：正反面上传；<br />
                                            3.社保权益记录及其它劳动关系证明材料扫描件（社保校验结果为符合的，不用上传；不符合的，需上传社保权益记录及其他劳动关系证明材料。）；<br />
                                            &nbsp;&nbsp;①已在申请单位缴纳申请之日上一个月社会保险的，须提交《社会保险个人权益记录》；<br />
                                            &nbsp;&nbsp;②已在申请单位本市或外省市分支机构缴纳申请之日上一个月社会保险的，须提交该单位分支机构的《营业执照》和《社会保险个人权益记录》。<br />
                                        </p>
                                        <p runat="server" id="p_PostTyppe1_aqy" visible="false">
                                            三类人员安全生产考核（专职安全生产管理人员）报考材料：<br />
                                            1.考试报名表：申请人签字并加盖聘用单位公章；<br />
                                            2.身份证：正反面上传；<br />
                                            3.学历或职称证书：中专（含高中、中技、职高）及以上文化程度学历证书或初级及以上技术职称职称证书；<br />
                                            4.学历或职称证书选择承诺制申报的：上传承诺书；<br />
                                            5.社保权益记录及其他劳动关系证明材料：（社保校验结果为符合的，不用上传；不符合的，需上传社保权益记录及其他劳动关系证明材料。）<br />
                                            &nbsp;&nbsp;①已在申请单位缴纳申请之日上一个月社会保险的，须提交《社会保险个人权益记录》；<br />
                                            &nbsp;&nbsp;②已在申请单位本市或外省市分支机构缴纳申请之日上一个月社会保险的，须提交该单位分支机构的《营业执照》和《社会保险个人权益记录》。<br />
                                            &nbsp;&nbsp;③已在劳务派遣单位缴纳申请之日上一个月社会保险的，须提交劳务派遣单位的《劳务派遣经营许可证》、劳务派遣单位与申请人订立的劳动合同、劳务派遣单位与用工单位签订的劳务派遣协议和申请人在劳务派遣单位参加社会保险的《社会保险个人权益记录》。<br />
                                        </p>
                                        <p runat="server" id="p_PostTyppe2" visible="false">
                                            北京市建筑施工特种作业人员考核报考材料：<br />
                                            1.考试报名表：申请人签字并加盖聘用单位公章、培训单位公章；<br />
                                            2.身份证：正反面上传；<br />
                                            3.初中及以上学历1份；<br />
                                            4.近3个月内二级乙等以上医院体检合格原件一份；<br />
                                            5.个人健康承若（下载模板：<a href="../Template/个人健康承诺.docx">【个人健康承诺模板.docx】</a>）；<br />
                                            6.考前安全作业培训承诺书。（与报名表一起下载）
                                        </p>
                                        <p runat="server" id="p_PostTyppe4" visible="false">
                                            北京市住房和城乡建设行业技能人员考核报考材料：<br />
                                            1.考试报名表：申请人签字并加盖聘用单位公章；<br />
                                        </p>
                                        <p runat="server" id="p_PostTyppe4000" visible="false">
                                            北京市住房和城乡建设行业技能人员考核报考材料：<br />
                                            1.考试报名表：申请人签字；<br />
                                        </p>
                                    </div>
                                    <div class="fujian">
                                        考试报名表扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.考试报名表扫描件%>','考试报名表扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi"><asp:Label ID="LabelSingupTableDesc" runat="server" Text="（要求：请在本页面导出打印报名表，签字、盖章后扫描上传,最大500K）"></asp:Label></span>
                                    </div>
                                    <div class="fujian" runat="server" id="div_ShenFenZheng">
                                        身份证扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.证件扫描件%>','证件扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传身份证正反面扫描件原件，jpg格式图片,最大500K）</span>
                                    </div>
                                    <div class="fujian" runat="server" id="div_LaoDongHeTong" visible="false">
                                        劳动合同主要页扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.劳动合同扫描件%>','劳动合同扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传劳动合同扫描件原件，只上传重要页,最多上传5页，jpg格式图片,每页最大500K）</span>
                                    </div>
                                    <div class="fujian" runat="server" id="div_SheBao" visible="false">
                                        社保权益记录及其它劳动关系证明材料扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.社保扫描件%>','社保扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：jpg格式图片,每页最大500K）</span>
                                    </div>
                                    <div runat="server" id="divSelectCheckType" style="line-height: 100%; margin: 20px 0; display: none">
                                        请选择学历和职称证明方式：<asp:RadioButtonList ID="RadioButtonListSignupPromise" runat="server" RepeatDirection="Horizontal" Style="display: inline" OnSelectedIndexChanged="RadioButtonListSignupPromise_SelectedIndexChanged" AutoPostBack="true">
                                            <asp:ListItem Text="上传学历和职称证明" Value="0" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="上传承诺书" Value="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="fujian" runat="server" id="div_bktjzm" style="display: none;">
                                        报考条件证明承诺书：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.报考条件证明承诺书%>','报考条件证明承诺书','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传签名、盖章的报考条件证明承诺书扫描件，jpg格式图片,最大500K.
                                            <asp:Button ID="ButtonOutPutSignupPromise" runat="server" Text="下载承诺书模板" CssClass="bt_large" OnClick="ButtonOutPutSignupPromise_Click" />）</span>
                                    </div>
                                    <div class="fujian" runat="server" id="div_XueLi" style="display: none;">
                                        学历证书扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.学历证书扫描件%>','学历证书扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传学历证书扫描件原件，jpg格式图片,最大500K）</span>
                                    </div>
                                    <div class="fujian" runat="server" id="div_JiShuZhiCheng" style="display: none;">
                                        职称证书扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.技术职称扫描件%>','技术职称扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传技术职称扫描件原件，jpg格式图片,最大500K）</span>
                                    </div>
                                    <div class="fujian" runat="server" id="div_TiJian" visible="false">
                                        体检证明扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.体检合格证明%>','体检合格证明','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：jpg格式图片,最大500K。<a></a>）</span>
                                    </div>
                                    <div class="fujian" runat="server" id="div_JingYeZhiZhao" visible="false">
                                        企业营业执照扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.企业营业执照扫描件%>','企业营业执照扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传企业营业执照扫描件原件，jpg格式图片,最大500K）</span>
                                    </div>
                                    <div class="fujian" runat="server" id="div_grjkcn" visible="false">
                                        个人健康承诺扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.个人健康承诺%>','个人健康承诺','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传填写好的个人健康承诺扫描件，jpg格式图片,最大500K）</span>
                                    </div>
                                     <div class="fujian" runat="server" id="div_examsafetrain" visible="false">
                                        考前安全作业培训承诺书：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.考前安全作业培训承诺书%>','考前安全作业培训承诺书','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传个人签名、单位盖章后的考前安全作业培训承诺书扫描件，jpg格式图片,最大500K）</span>
                                    </div>
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
                                                    <telerik:GridBoundColumn HeaderText="办理时间" UniqueName="ActionData" DataField="ActionData">
                                                        <ItemStyle Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="办理结果" UniqueName="ActionResult" DataField="ActionResult">
                                                        <ItemStyle Wrap="false" />
                                                    </telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn HeaderText="办理意见" UniqueName="ActionRemark" DataField="ActionRemark">
                                                    </telerik:GridBoundColumn>
                                                </Columns>
                                                <HeaderStyle Font-Bold="True" BorderColor="#cccccc" BorderStyle="Solid" BorderWidth="1" Wrap="false" />
                                                <ItemStyle CssClass="subtable" Font-Size="16px" />
                                                <AlternatingItemStyle CssClass="subtable" Font-Size="16px" />
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <hr />
                        <br />
                        <asp:HiddenField ID="HiddenFieldApplyMan" runat="server" />
                        <asp:HiddenField ID="HiddenFieldFirstCheck" runat="server" />
                        <div class="DivContent" >
                            <div id="divPostType1Tip" runat="server" visible="false" >
                                <p class="bold">重要提示：</p>
	                            <p style="padding-left:64px">本次业务申请最终审批结果以住房和城乡建设部生成电子证书为准，在未生成电子证书前，申请人应满足以下条件：<br />
	                            1.申请法人A本证书（职务为法定代表人）的，应为该单位营业执照上载明的法定代表人。<br />
                                2.不允许持有多本非法人A本（职务不是法定代表人）。<br />
	                            3.申请项目负责人B本证书的，其一、二级建造师注册单位应与其现聘用单位一致。<br />
	                            4.同时持有多本安管人员ABC证书的，其项目负责人B本、专职安管人员C本应与其一本法人A本证书工作单位一致。<br />
	                            5.同时持有安管人员BC本证书的，其项目负责人B本应与其专职安管人员C本证书工作单位一致。<br />
                                6.有C3不能再报考C1和C2，有C1或C2不能报考C3。<br />
                                7.本次业务申请最终审批结果以住房和城乡建设部生成电子证书为准，在未生成电子证书前，请不要办理营业执照法定代表人变更或一、二级建造师变更、重新、注销注册等业务，以免无法正常生成电子证书，影响后续业务办理。<br />
	                            </p>
                            </div>
                            <p class="bold">报名流程：</p>
                            <p style="padding-left:64px" id="divStepDesc" runat="server">个人填写保存报名信息 》 个人提交单位初审 》 企业初审提交建委审核  》 建委审核</p>

                        </div>
                        <div style="width: 90%; padding: 5px; margin: 0 auto; text-align: center;">
                            <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="button" OnClick="btnSave_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;                          
                            <asp:Button ID="ButtonSendUnit" runat="server" Text="提交单位确认" CssClass="bt_large" OnClick="ButtonSendUnit_Click" OnClientClick="javascript:if(this.value=='提交单位确认'){if(confirm('1、请考生确认上传照片是否为彩色一寸白底标准免冠证件照，附件是在正确位置上传。\r\n2、修改申请表填报内容后，请先保存再提交单位确认，否则无法记录修改内容。\r\n\r\n确定要提交单位审核吗？')==false) return false;} else if(this.value=='提交培训机构'){if(confirm('1、请考生确认上传照片是否为彩色一寸白底标准免冠证件照，附件是在正确位置上传。\r\n2、修改申请表填报内容后，请先保存再提交，否则无法记录修改内容。\r\n\r\n确定要提交审核吗？')==false) return false;} else {if(confirm('确认要取消申报吗？')==false) return false;}" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                         <asp:Button ID="ButtonExport" runat="server" Text="导出打印" CssClass="button" OnClick="ButtonExport_Click"
                             Enabled="False" />&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="ButtonDelete" runat="server" Text="删除报名" Enabled="false" CssClass="button"
                                OnClick="ButtonDelete_Click" OnClientClick="javascript:if(confirm('请您确认是否删除此次考试报名申请。删除数据不可恢复，确认要删除报名吗？')==false) return false;" />
                            &nbsp;&nbsp;&nbsp;&nbsp;
                             <input id="Button1" type="button" value="返 回" class="button" onclick="javascript: location.href = 'ExamSignList.aspx';" />
                        </div>
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
                                                                    <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="../images/Cancel.gif" CommandName="Delete" Visible='<%#(IfExistRoleID("0") == true && ((ViewState["ExamSignUpOB"] as Model.ExamSignUpOB).Status == Model.EnumManager.SignUpStatus.SaveSignUp||(ViewState["ExamSignUpOB"] as Model.ExamSignUpOB).Status == Model.EnumManager.SignUpStatus.ReturnEdit) && (DateTime.Compare((ViewState["ExamPlanOB"] as Model.ExamPlanOB).StartCheckDate.Value, Convert.ToDateTime(System.DateTime.Now.ToString("yyyy-MM-dd"))) > 0))?true:false%>' OnClientClick="javascript:if(confirm('您确定要删除么?')==false) return false" />
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
        <uc4:IframeView ID="IframeView" runat="server" />
        <div id="winpop">
        </div>
        <%--<div id="DivExamConfirm" runat="server" style="line-height: 30px; width: 580px; display: none; margin-top: 30px; position: absolute; top: 100px; left: 200px; background-color: #dedede; padding: 20px 50px 50px 50px; border-left: 4px solid #eee; border-top: 4px solid #eee; border-right: 4px solid #999; border-bottom: 4px solid #999; color: #000">
            <p style="font-size: 30px; font-weight: bold; text-align: center;">考试确认</p>
            <p style="text-indent: 32px">原2021年11月已通过安管人员报考资格审查的人员，请于2022年7月1日至4日重新确认能否按时参加此次考试。</p>
            <p style="text-indent: 32px">1.点击“确定参加本次考试”即本人确认2022年7月11日至15日能遵守考核机关相关要求如期参加考试。</p>
            <p style="text-indent: 32px">2.点击“顺延参加下次考试”即本人无法参加2022年7月的考试，服从考核机关安排，自动顺延参加下一次考试。</p>
            <p style="text-align: center;">
                <asp:Button ID="ButtonExamYes" runat="server" Text="确定参加本次考试" CssClass="bt_large" OnClick="ButtonExamYes_Click" CausesValidation="false" />
                &nbsp;&nbsp; 
                <asp:Button ID="ButtonExamNo" runat="server" Text="顺延参加下次考试" CssClass="bt_large" OnClick="ButtonExamNo_Click" CausesValidation="false" />
            </p>
        </div>--%>
    </form>
</body>
</html>
