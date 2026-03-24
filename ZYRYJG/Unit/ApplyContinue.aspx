<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyContinue.aspx.cs" Inherits="ZYRYJG.Unit.ApplyContinue" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style type="text/css">
        td {
            line-height: 200%;
        }

        .detailTable {
            width: 98%;
        }

        .infoHead {
            text-align: right;
            vertical-align: top;
            font-weight: bold;
            line-height: 200%;
        }

        .formItem_1 {
            text-align: left;
            vertical-align: top;
            line-height: 200%;
        }

            .formItem_1 input {
                border: none;
                line-height: 200%;
                width: 100%;
            }

            .formItem_1 td {
            }

        .la {
            font-weight: bold;
        }

        .readonly {
            border: none !important;
        }

        .tbzy td {
            border: 1px solid #efefef;
            line-height: 200%;
        }

        .barTitle {
            color: #434343;
            background-color: #E4E4E4;
            font-weight: bold;
            border-left: 5px solid #ff6a00;
            text-align: left;
        }

        .img {
            border: none;
            width: 0px;
        }

        .img200 {
            border: none;
            width: 200px;
        }

        .subtable td {
            border: 1px solid #cccccc;
            border-collapse: collapse;
        }

        .addrow {
            float: right;
            background: url(../images/jiah.gif) no-repeat center center;
            width: 15px;
            height: 15px;
            padding-right: 20px;
        }

        .auto-style1 {
            text-align: right;
            vertical-align: top;
            font-weight: bold;
            line-height: 200%;
            width: 15%;
            border: 1px solid #efefef;
        }

        .main {
            color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server"
            EnableAJAX="true">
            <AjaxSettings>                
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;申报事项 &gt;&gt;<strong>延续注册</strong>
                </div>
            </div>
            <div class="content">
                <p class="jbxxbt">
                    延续注册
                </p>
                <div class="step">
                    <div class="stepLabel">办理进度：</div>
                    <div id="step_未申报" runat="server" class="stepItem lgray">个人填写></div>
                    <div id="step_待确认" runat="server" class="stepItem lgray">待单位审核></div>
                    <div id="step_已申报" runat="server" class="stepItem lgray">单位申报></div>
                    <div id="step_已受理" runat="server" class="stepItem lgray">区级受理></div>
                    <div id="step_区县审查" runat="server" class="stepItem lgray">区级审核></div>
                    <div id="step_已上报" runat="server" class="stepItem lgray">汇总上报></div>
                    <div id="step_已审查" runat="server" class="stepItem lgray">市级审核></div>
                    <div id="step_已决定" runat="server" class="stepItem lgray">市级决定></div>
                    <div id="step_已公示" runat="server" class="stepItem lgray">公示></div>
                    <div id="step_已公告" runat="server" class="stepItem lgray">公告（办结）</div>
                    <div class="stepArrow">▶</div>
                </div>
                <div style="width: 100%; margin: 10px auto; text-align: center; overflow: hidden;">
                    <div class="code">申请编号：<asp:Label ID="LabelApplyCode" runat="server" Text=""></asp:Label></div>
                    <div style="width: 66%; float: left; clear: left">
                        <table runat="server" id="EditTable" class="detailTable">
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">基本信息</td>
                            </tr>
                            <tr>
                                <td class="infoHead" style="width: 25%">企业名称：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxENT_Name" runat="server" CssClass="textEdit" Width="98%" MaxLength="200"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorENT_Name" runat="server" ControlToValidate="RadTextBoxENT_Name"
                                        ErrorMessage="请输入企业名称" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td class="infoHead" style="width: 25%">机构代码（或信用代码）：
                                </td>
                                <td class="formItem_1" style="width: 30%">
                                    <telerik:RadTextBox ID="RadTextBoxENT_OrganizationsCode" runat="server" CssClass="textEdit" Width="98%" MaxLength="50"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">所在区县：
                                </td>
                                <td class="formItem_1" style="width: 30%">
                                    <telerik:RadTextBox ID="RadTextBoxENT_City" runat="server" CssClass="textEdit" Width="98%" MaxLength="100"></telerik:RadTextBox>
                                </td>
                                <td class="infoHead">民族：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadComboBox ID="RadComboBoxNation" runat="server" Width="98%" NoWrap="true" DropDownCssClass="multipleRowsColumns" DropDownWidth="620px" Height="300px">
                                    </telerik:RadComboBox>
                                    <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxNation"
                                        ErrorMessage="必填" runat="server" ID="Comparevalidator4" ForeColor="Red" Display="Dynamic" />
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">姓名：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_Name" runat="server" CssClass="textEdit" Width="98%" MaxLength="50"></telerik:RadTextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidatorPSN_Name" runat="server" ControlToValidate="RadTextBoxPSN_Name"
                                        ErrorMessage="请输入####" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                                <td class="auto-style1">性别：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_Sex" runat="server" CssClass="textEdit" Width="98%" MaxLength="2"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">证件类型：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_CertificateType" runat="server" CssClass="textEdit" Width="98%" MaxLength="20"></telerik:RadTextBox>
                                </td>
                                <td class="auto-style1">证件号码：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_CertificateNO" runat="server" CssClass="textEdit" Width="98%" MaxLength="30"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">个人电话：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_MobilePhone" runat="server" CssClass="textEdit" Width="98%" MaxLength="100"></telerik:RadTextBox>
                                </td>
                                <td class="auto-style1">企业电话：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxENT_Telephone" runat="server" CssClass="textEdit" Width="98%" MaxLength="100"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">注册号：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_RegisterNo" runat="server" CssClass="textEdit" Width="98%" MaxLength="50"></telerik:RadTextBox>
                                </td>
                                <td class="auto-style1">邮箱：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_Email" runat="server" CssClass="textEdit" Width="98%" MaxLength="200"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">已注册专业：
                                </td>
                                <td colspan="3" class="formItem_1" style="padding: 0 0 0 0!important;">
                                    <table width="100%" cellpadding="0" cellspacing="0" style="border-collapse: collapse;">
                                        <tr>
                                            <td class="formItem_1 la" style="width: 40%;">专业
                                            </td>
                                            <td class="formItem_1 la" style="width: 60%;">有效期</td>
                                        </tr>
                                        <tr>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBox_PSN_RegisteProfession1" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                            <td class="formItem_1" style="width: 40%">
                                                <telerik:RadTextBox ID="RadTextBox_PSN_CertificateValidity1" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBox_PSN_RegisteProfession2" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBox_PSN_CertificateValidity2" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBox_PSN_RegisteProfession3" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBox_PSN_CertificateValidity3" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBox_PSN_RegisteProfession4" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBox_PSN_CertificateValidity4" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                           <%-- <tr>
                                <td align="right" class="infoHead">合同类型：</td>
                                <td align="left" colspan="3">
                                    <asp:RadioButtonList ID="RadioButtonListENT_ContractType" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListENT_ContractType_SelectedIndexChanged" Width="100%">
                                        <asp:ListItem Text="固定期限" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="无固定期限" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="以完成一定工作任务为期限" Value="3"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="infoHead">劳动合同开始时间：</td>
                                <td align="left" class="formItem_1">
                                    <telerik:RadDatePicker ID="RadDatePickerENT_ContractStartTime" runat="server" MinDate="1900-1-1" MaxDate="2090-1-1" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="ValidatorENT_ContractStartTime" runat="Server" ControlToValidate="RadDatePickerENT_ContractStartTime"
                                        ErrorMessage="请输入劳动合同开始时间" Display="Dynamic">*请输入劳动合同开始时间</asp:RequiredFieldValidator>
                                </td>
                                <td align="right" class="infoHead">
                                    <asp:Label ID="LabelJZSJ" runat="server" Text="劳动合同结束时间："></asp:Label></td>
                                <td align="left" class="formItem_1">
                                    <telerik:RadDatePicker ID="RadDatePickerENT_ContractENDTime" runat="server" MinDate="1900-1-1" MaxDate="2090-1-1" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
                                    <asp:RequiredFieldValidator ID="ValidatorENT_ContractENDTime" runat="Server" ControlToValidate="RadDatePickerENT_ContractENDTime"
                                        ErrorMessage="请输入劳动合同结束时间" Display="Dynamic">*请输入劳动合同结束时间</asp:RequiredFieldValidator>
                                </td>
                            </tr>--%>
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">续期信息</td>
                            </tr>
                            <tr>
                                <td class="infoHead">申请延续注册专业：
                                </td>
                                <td colspan="3" style="padding: 0 0 0 0!important;">
                                    <table width="100%" class="tbzy" cellpadding="0" cellspacing="0" style="border-collapse: collapse; width: 100%!important;">
                                        <tr>
                                            <td rowspan="2" class="la" width="15%">延续专业</td>
                                            <td rowspan="2" class="la" width="15%">是否延续</td>
                                            <td colspan="2" class="la" width="45%">继续教育情况（逾期注册）</td>
                                            <td rowspan="2" class="la" width="25%">备注</td>
                                        </tr>
                                        <tr>
                                            <td class="la">必修课（学时）</td>
                                            <td class="la">选修课（学时）</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelPSN_RegisteProfession1" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="RadComboBoxIfContinue1" runat="server" Width="60px">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="请选择" Value="" />
                                                        <telerik:RadComboBoxItem Text="是" Value="True" />
                                                        <telerik:RadComboBoxItem Text="否" Value="False" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelBiXiu1" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelXuanXiu1" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="RadTextBoxRemark1" runat="server" CssClass="textEdit" Width="98%" MaxLength="64"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelPSN_RegisteProfession2" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="RadComboBoxIfContinue2" runat="server" Width="60px">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="请选择" Value="" />
                                                        <telerik:RadComboBoxItem Text="是" Value="True" />
                                                        <telerik:RadComboBoxItem Text="否" Value="False" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelBiXiu2" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelXuanXiu2" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="RadTextBoxRemark2" runat="server" CssClass="textEdit" Width="98%" MaxLength="64"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelPSN_RegisteProfession3" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="RadComboBoxIfContinue3" runat="server" Width="60px">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="请选择" Value="" />
                                                        <telerik:RadComboBoxItem Text="是" Value="True" />
                                                        <telerik:RadComboBoxItem Text="否" Value="False" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelBiXiu3" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelXuanXiu3" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="RadTextBoxRemark3" runat="server" CssClass="textEdit" Width="98%" MaxLength="64"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LabelPSN_RegisteProfession4" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="RadComboBoxIfContinue4" runat="server" Width="60px">
                                                    <Items>
                                                        <telerik:RadComboBoxItem Text="请选择" Value="" />
                                                        <telerik:RadComboBoxItem Text="是" Value="True" />
                                                        <telerik:RadComboBoxItem Text="否" Value="False" />
                                                    </Items>
                                                </telerik:RadComboBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelBiXiu4" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="LabelXuanXiu4" runat="server" Text=""></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="RadTextBoxRemark4" runat="server" CssClass="textEdit" Width="98%" MaxLength="64"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr class="GridLightBK" id="trFuJanTitel" runat="server" visible="false">
                                <td colspan="4" class="barTitle">附件上传<span style="color: red">(所有电子证书扫描件要求与原件1:1比例正向扫描上传,信息清晰完整) </span>
                                </td>
                            </tr>
                            <tr class="GridLightBK" id="trFuJan" runat="server" visible="false">
                                <td colspan="4">
                                    <div style="text-align: right; padding-right: 40px;">
                                        <span class="tishi">附件辅助工具下载：</span><a
                                            href="../Images/1寸照片生成器.exe" style="text-decoration: underline; color: Blue;"><img
                                                alt="" src="../Images/Soft_common.gif" style="border-width: 0;" />
                                            1寸照片生成器.exe</a>
                                    </div>
                                    <div class="fujian">
                                        <span style="color: Red">* </span>一寸免冠照片：<span class="link" onclick="javascript:tips_pop('<%=string.Format("{0}/{1}",Model.EnumManager.FileDataType.一寸免冠照片,RadTextBoxPSN_CertificateNO.Text.Trim().Substring(RadTextBoxPSN_CertificateNO.Text.Trim().Length - 3, 3))%>','一寸免冠照片','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传免冠照片扫描件原件、一寸jpg格式图片，名称不限，最大为50K，宽高110 X 140像素）</span>
                                    </div>

                                    <div class="fujian">
                                        <span style="color: Red">* </span>证件扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.证件扫描件%>','证件扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传身份证扫描件原件、jpg格式图片,最大500K）</span>
                                    </div>
                                    <div class="fujian">
                                        学历证书扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.学历证书扫描件%>','学历证书扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传学历证书扫描件原件、jpg格式图片,最大500K）</span>
                                    </div>
                                    <div class="fujian">
                                        劳动合同扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.劳动合同扫描件%>','劳动合同扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（<span style="color: Red">已退休人员需提供劳动合同或相关劳动关系证明。</span>要求：上传劳动合同扫描件原件、只上传重要页,最多上传5页、jpg格式图片,每页最大500K）</span>
                                    </div>
                                    <div class="fujian">
                                        执业资格证书或资格考试合格通知书扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.执业资格证书扫描件%>','执业资格证书或资格考试合格通知书扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传执业资格证书扫描件原件、jpg格式图片,最大500K）</span>
                                    </div>
                                    <div class="fujian">
                                        <span style="color: Red">* </span>继续教育承诺书扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.继续教育承诺书扫描件%>','继续教育承诺书扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传继续教育承诺书扫描件原件、jpg格式图片,最大500K）</span><a target="_blank" href="../Template/二建继续教育承诺书.docx">《二建继续教育承诺书模板》</a>
                                    </div>
                                    <div class="fujian">
                                        <span style="color: Red">* </span>申请表扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.申请表扫描件%>','申请表扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：请在本页面导出打印申请表，单位盖章签字后扫描上传）</span>
                                    </div>
                                    <div class="fujian">
                                        社保权益记录及其他劳动关系证明材料：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.社保扫描件%>','社保扫描件','','<%=ApplyID%>')">选择文件</span>
                                        <span class="tishi">（社保校验结果为符合的，不用上传；不符合的，需上传相关社保权益记录或其他证明材料扫描件，jpg格式图片,最大500K。）<br />
                                            社保附件要求说明：<br />
                                            1、已在申请单位缴纳申请之日上一个月社会保险的，须提交《社会保险个人权益记录》；<br />
                                            2、已在申请单位本市或外省市分支机构缴纳申请之日上一个月社会保险的，须提交该单位分支机构的《营业执照》和《社会保险个人权益记录》；<br />
                                            3、 已办理正式退休手续的，须提交退休所在单位人事部门或社会保障部门颁发的《退休证》；<br />
                                            4、已办理正式退休手续，但男未满60周岁、女未满55周岁的，须提交退休所在单位人事部门或社会保障部门颁发的《退休证》和《城乡居民养老保险待遇核定表》。                                         
                                        </span>

                                    </div>
                                </td>
                            </tr>
                        </table>
                        <div id="divGR" visible="false" runat="server" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                            <div id="divStepDesc" runat="server" style="line-height: 300%;">操作流程：个人申请保存-->点击导出打印(导出申请表)-->上传相关附件-->提交单位审核</div>
                            <asp:Button ID="ButtonSave" runat="server" Text="保 存" CssClass="bt_large" UseSubmitBehavior="false" OnClientClick="if(CheckClientValidate()==true){this.value='正在提交';this.disabled=true;}" OnClick="ButtonSave_Click" Enabled="false" />
                            &nbsp;&nbsp;<asp:Button ID="ButtonOutput" runat="server" Text="导出打印" CssClass="bt_large" OnClick="ButtonOutput_Click" Enabled="false" />
                            &nbsp;&nbsp;<asp:Button ID="ButtonUnit" runat="server" type="button" Text="提交单位确认" CssClass="bt_large" OnClick="ButtonUnit_Click" Enabled="false" OnClientClick="javascript:if(this.value=='提交单位确认'){if(confirm('修改申请表填报内容后，请先保存再提交单位确认，否则无法记录修改内容。\r\n\r\n确定要提交单位审核吗？')==false) return false;} else {if(confirm('确认要取消申报吗？')==false) return false;}" />
                            &nbsp;&nbsp;<asp:Button ID="ButtonDelete" runat="server" Text="删 除" CssClass="bt_large" OnClick="ButtonDelete_Click" Enabled="false" OnClientClick="javascript:if(!confirm('您确定要删除么?'))return false;" CausesValidation="false" />
                        </div>
                        <div id="divCheckHistory" visible="true" runat="server" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
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
                                                <ItemStyle CssClass="subtable" />
                                                <AlternatingItemStyle CssClass="subtable" />
                                            </MasterTableView>
                                        </telerik:RadGrid>
                                    </td>
                                </tr>
                            </table>
                            <div style="font-size: 20px">
                                <a href="https://zjw.beijing.gov.cn/bjjs/gcjs/kszczn/tg/ejjzs/index.shtml" target="_blank" style="color: red"><span id="blink">【最新】</span>北京市二级建造师注册审查公告文件</a>
                                <%--  <span style="padding-left: 50px"><a href="http://120.52.185.14/Upload/PolicyNews/北京市二级建造师注册申诉表（2021版）.doc" target="_blank">【北京市二级建造师注册申诉表（2021版）】</a></span>--%>
                            </div>
                            <div style="line-height: 180%; text-align: left; background-color: #efefef; border: 1px solid #e9e9e9; border-radius: 12px 12px; padding: 12px 20px; color: red; margin: 16px 16px">
                                <b>提示：</b>申报事项公告通过后，申请人可在 电子证书 下载栏目中生成该电子证书使用有效期范围，公告后24小时内未自行填写，系统将会自动生成最大时间范围的使用有效期。新电子证书生成后，申请人也可以根据自身需求再次调整证书使用有效期时间范围。
                            </div>
                        </div>
                        <div id="divUnit" runat="server" visible="false" style="padding-top: 20px; text-align: center; clear: both;" class="auto-style3">
                            <table id="Table6" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">单位确认</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">处理结果：</td>
                                    <td width="80%" align="left">
                                        <asp:RadioButtonList ID="RadioButtonListOldUnitCheckResult" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                            <asp:ListItem Text="同意" Value="同意" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不同意" Value="不同意"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">处理意见：</td>
                                    <td width="80%" align="left">
                                        <asp:TextBox ID="TextBoxOldUnitCheckRemark" runat="server" TextMode="MultiLine" Rows="3" Width="99%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="ButtonApply" runat="server" CssClass="bt_large" Text="确定" OnClick="ButtonApply_Click" />&nbsp;&nbsp;                                         
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divQX" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
                            <table id="TableEdit" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">受理操作</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">受理结果：</td>
                                    <td width="80%" align="left">
                                        <asp:RadioButtonList ID="RadioButtonListApplyStatus" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                            <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">受理意见：</td>
                                    <td width="80%" align="left">
                                        <asp:TextBox ID="TextBoxApplyGetResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="予以受理"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="BtnSave" runat="server" CssClass="bt_large" Text="确认提交" OnClick="BtnSave_Click" />&nbsp;&nbsp;
                                            <input id="BtnReturn" type="button" class="bt_large" value="取消提交" onclick='javascript: hideIfam()' />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divQXCK" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
                            <table id="Table4" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">区县审查操作</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">审查结果：</td>
                                    <td width="80%" align="left">
                                        <asp:RadioButtonList ID="RadioButtonListExamineResult" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                            <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">审查意见：</td>
                                    <td width="80%" align="left">
                                        <asp:TextBox ID="TextBoxExamineRemark1" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="允许通过"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="BttSave" runat="server" CssClass="bt_large" Text="确认提交" OnClick="BttSave_Click" />&nbsp;&nbsp;
                                            <input id="BtnReturnck" type="button" class="bt_large" value="取消提交" onclick='javascript: hideIfam()' />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divCheck" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
                            <table id="Table2" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">审查操作</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">审查结果：</td>
                                    <td width="80%" align="left">
                                        <asp:RadioButtonList ID="RadioButtonListCheckResult" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                            <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">审查意见：</td>
                                    <td width="80%" align="left">
                                        <asp:TextBox ID="TextBoxApplyCheckRemark" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="允许通过"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="ButtonCheck" runat="server" CssClass="bt_large" Text="确认提交" OnClick="ButtonCheck_Click" />&nbsp;&nbsp;
                                            <input id="BtnReturn3" type="button" class="bt_large" value="取消提交" onclick='javascript: hideIfam()' />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divDecide" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
                            <table id="Table3" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">决定操作</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">决定结果：</td>
                                    <td width="80%" align="left">
                                        <asp:RadioButtonList ID="RadioButtonListDecide" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                            <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                            <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="ButtonDecide" runat="server" CssClass="bt_large" Text="确认提交" OnClick="ButtonDecide_Click" />&nbsp;&nbsp;
                                            <input id="BtnReturn4" type="button" class="bt_large" value="取消提交" onclick='javascript: hideIfam()' />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divSendBack" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
                            <table width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                <tr class="GridLightBK">
                                    <td colspan="2" class="barTitle">审核流程后退操作</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td width="20%" align="right">请选择要后退到的节点：</td>
                                    <td width="80%" align="left">
                                        <telerik:RadComboBox ID="RadComboBoxReturnApplyStatus" runat="server" Width="80">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="已申报" Value="已申报" />
                                                <telerik:RadComboBoxItem Text="已受理" Value="已受理" />
                                                <telerik:RadComboBoxItem Text="区县审查" Value="区县审查" />
                                                <telerik:RadComboBoxItem Text="已上报" Value="已上报" />
                                                <telerik:RadComboBoxItem Text="已审查" Value="已审查" />
                                                <telerik:RadComboBoxItem Text="已决定" Value="已决定" />
                                                <telerik:RadComboBoxItem Text="已公告" Value="已公告" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="2" align="center">
                                        <asp:Button ID="ButtonSendBack" runat="server" CssClass="bt_large" Text="执行后退" OnClick="ButtonSendBack_Click" OnClientClick="javascript:if(!confirm('您确定要后退么?')) return false;" CausesValidation="false" />&nbsp;&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divViewAction" runat="server" visible="false" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                            <input id="BtnReturn2" type="button" class="bt_large" value="返 回" onclick='javascript: hideIfam()' />
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
                                                <%# Eval("DataType").ToString().Replace("证件扫描件","身份证") %>
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
                                                                    <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="../images/Cancel.gif" CommandName="Delete" Visible='<%#(IfExistRoleID("0") == true && ((ViewState["ApplyMDL"] as Model.ApplyMDL).ApplyStatus == Model.EnumManager.ApplyStatus.未申报 || (ViewState["ApplyMDL"] as Model.ApplyMDL).ApplyStatus == Model.EnumManager.ApplyStatus.已驳回))?true:false%>' OnClientClick="javascript:if(confirm('您确定要删除么?')==false) return false" />
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
        <div id="winpop">
        </div>
    </form>
    <script type="text/javascript">
        var colornum = 0;
        function changeColor() {
            var color = "#fff|red";
            color = color.split("|");
            if (document.getElementById("blink")) document.getElementById("blink").style.color = color[colornum];
            if (colornum == 0) {
                colornum = 1
            } else {
                colornum = 0
            }
        }
        if (document.getElementById("blink")) {
            setInterval("changeColor()", 500);
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
            //变换审核结果
            $("#<%= RadioButtonListApplyStatus.ClientID%> input").each(function () {
                $(this).click(function () {
                    var TextBoxApplyGetResult = $("#<%= TextBoxApplyGetResult.ClientID%>");
                    if ($(this).val() == "通过") {
                        TextBoxApplyGetResult.val("予以受理");
                    }
                    else {
                        TextBoxApplyGetResult.val("不予受理");
                    }
                });
            });
            //变换区县审核
            $("#<%= RadioButtonListExamineResult.ClientID%> input").each(function () {
                $(this).click(function () {
                    var TextBoxApplyCheckRemark = $("#<%= TextBoxExamineRemark1.ClientID%>");
                    if ($(this).val() == "通过") {
                        TextBoxApplyCheckRemark.val("允许通过");
                    }
                    else {
                        TextBoxApplyCheckRemark.val("审核未通过");
                    }
                });
            });
            //变换审核结果
            $("#<%= RadioButtonListCheckResult.ClientID%> input").each(function () {
                $(this).click(function () {
                    var TextBoxApplyCheckRemark = $("#<%= TextBoxApplyCheckRemark.ClientID%>");
                    if ($(this).val() == "通过") {
                        TextBoxApplyCheckRemark.val("允许通过");
                    }
                    else {
                        TextBoxApplyCheckRemark.val("审核未通过");
                    }
                });
            });
        })
        function tips_pop(code, ftype, fsname, pid) {
            layer.open({
                type: 2,
                title: ['资料上传 - ' + ftype, 'font-weight:bold;background: #5DA2EF;'],//标题
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
    </script>
</body>
</html>
