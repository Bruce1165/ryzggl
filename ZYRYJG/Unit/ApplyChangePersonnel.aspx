<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyChangePersonnel.aspx.cs" Inherits="ZYRYJG.Unit.ApplyChangePersonnel" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.3" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>

    <style type="text/css">
        .detailTable {
            width: 98%;
            height: 14px;
        }

        .infoHead {
            width: 20%;
            text-align: right;
            vertical-align: top;
            font-weight: bold;
            line-height: 150%;
        }

        .formItem_1 {
            width: 45%;
            text-align: left;
            vertical-align: top;
        }

            .formItem_1 input {
                border: none;
                line-height: 150%;
                width: 100%;
            }

            .formItem_1 td {
                width: 33%;
            }

        .formItem_2 {
            width: 35%;
            text-align: left;
            vertical-align: top;
        }

        .la {
            font-weight: bold;
        }

        .readonly {
            border: none !important;
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
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
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        当前位置&gt;&gt;事项申报 &gt;&gt;变更注册 &gt;&gt;<strong>执业企业变更</strong>
                    </div>
                </div>
                <div id="main" runat="server" class="content">
                    <p class="jbxxbt">
                        执业企业变更
                    </p>
                    <div class="step">
                        <div class="stepLabel">办理进度：</div>
                        <div id="step_未申报" runat="server" class="stepItem lgray">个人填写></div>
                        <div id="step_待确认" runat="server" class="stepItem lgray">待单位审核（原、新单位）></div>
                        <div id="step_已申报" runat="server" class="stepItem lgray">已申报></div>
                        <div id="step_已受理" runat="server" class="stepItem lgray">区级受理></div>
                        <div id="step_区县审查" runat="server" class="stepItem lgray">区级审核></div>
                        <div id="step_已上报" runat="server" class="stepItem lgray">汇总上报（办结）</div>
                        <div class="stepArrow">▶</div>
                    </div>
                    <div style="width: 100%; margin: 10px auto; text-align: center; overflow: hidden;">
                        <div class="code">申请编号：<asp:Label ID="LabelApplyCode" runat="server" Text=""></asp:Label></div>
                        <div style="width: 66%; float: left; clear: left">
                            <table runat="server" id="EditTable" class="detailTable" cellpadding="5" cellspacing="1">
                                <tr class="GridLightBK">
                                    <td colspan="4" class="barTitle">人员信息</td>
                                </tr>
                                <tr class="GridLightBK" id="trButtonQuery" runat="server" visible="false" style="display: none;">
                                    <td class="infoHead">输入要调入人员证书注册号和身份证号：
                                    </td>
                                    <td class="formItem_1" colspan="3">
                                        <telerik:RadTextBox ID="RadTextBoxPSN_RegisterNO_Find" runat="server" CssClass="textEdit" Width="150px" MaxLength="50" placeholder="输入调入人员注册编号">
                                        </telerik:RadTextBox>
                                        <telerik:RadTextBox ID="RadTextBoxPSN_CertificateNO_Find" runat="server" CssClass="textEdit" Width="150px" MaxLength="50" placeholder="输入调入人员身份证号">
                                        </telerik:RadTextBox>
                                        <asp:Button ID="ButtonQuery" runat="server" Text="检 索" CssClass="bt_large" Width="100" OnClick="ButtonQuery_Click" CausesValidation="false" />
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">注册号：
                                    </td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelPSN_RegisterNO" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">证件号码：                                 
                                    </td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelPSN_CertificateNO" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">姓名：
                                    </td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelPSN_Name" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">性别：
                                    </td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelPSN_Sex" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">民族：
                                    </td>
                                    <td class="formItem_1" colspan="3">
                                        <telerik:RadComboBox ID="RadComboBoxNation" runat="server" Width="98%" NoWrap="true" DropDownCssClass="multipleRowsColumns" DropDownWidth="620px" Height="300px">
                                        </telerik:RadComboBox>
                                        <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxNation"
                                            ErrorMessage="必填" runat="server" ID="Comparevalidator4" ForeColor="Red" Display="Dynamic" />
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">手机：
                                    </td>
                                    <td class="formItem_1" colspan="3">
                                        <telerik:RadTextBox ID="RadTextBoxPSN_MobilePhone" Enabled="false" runat="server" CssClass="textEdit" Width="150px" MaxLength="13">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">电子邮箱：
                                    </td>
                                    <td class="formItem_1" colspan="3">
                                        <telerik:RadTextBox ID="RadTextBoxPSN_Email" Enabled="false" runat="server" CssClass="textEdit" Width="300px" MaxLength="100">
                                        </telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="4" class="barTitle">原聘用企业信息</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">企业名称：</td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelOldENT_Name" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">企业工商注册地址：</td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelOldENT_Correspondence" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK" style="display: none">
                                    <td class="infoHead">邮政编码：</td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelOldENT_Postcode" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">联系人：</td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelOldLinkMan" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">联系电话：</td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelOldENT_Telephone" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="4" class="barTitle">现聘用企业信息</td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead" colspan="4" style="text-align: center">统一社会信用代码/机构代码：
                                         <telerik:RadTextBox ID="ZZJGDM_check" runat="server" CssClass="textEdit" Width="150px" MaxLength="50" AutoPostBack="true" placeholder="输入企业组织机构代码">
                                         </telerik:RadTextBox>

                                        <telerik:RadTextBox ID="ZZJGDM_old" runat="server" CssClass="textEdit" Width="150px" MaxLength="50" Style="display: none;">
                                        </telerik:RadTextBox>
                                        <telerik:RadTextBox ID="ZZJGDM_new" runat="server" CssClass="textEdit" Width="150px" MaxLength="50" Style="display: none;">
                                        </telerik:RadTextBox>
                                        <div style="display: none">
                                            企业名称：
                                       <telerik:RadTextBox ID="RadTextBoxUnitName" Width="250px" runat="server" AutoPostBack="true"></telerik:RadTextBox>
                                        </div>
                                        <input id="ButtonSelectQY" type="button" value="请选择一个单位" class="bt_maxlarge" onclick="javascript: selectQY();" />
                                        <asp:Button ID="ZZJGDMbtn" runat="server" Text="检 索" CssClass="bt_large" Width="100" OnClick="ZZJGDMbtn_Click" Enabled="false" CausesValidation="false" Visible="false" />
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">企业名称：</td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelENT_Name" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">企业工商注册地址：</td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelEND_Addess" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK" style="display: none">
                                    <td class="infoHead">邮政编码：</td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelENT_Postcode" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">联系人：</td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelLinkMan" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">联系电话：</td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelENT_Telephone" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">企业法人：</td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelFR" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">企业类型：</td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelENT_Type" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">企业资质类别：</td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelENT_Sort" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">企业资质等级：</td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelENT_Grade" runat="server" Text=""></asp:Label>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td class="infoHead">企业资质证书编号：</td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelENT_QualificationCertificateNo" runat="server" Text=""></asp:Label>
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
                                            <span style="color: Red">* </span>一寸免冠照片：<span class="link" onclick="javascript:tips_pop('<%=string.Format("{0}/{1}",Model.EnumManager.FileDataType.一寸免冠照片,LabelPSN_CertificateNO.Text.Trim().Substring(LabelPSN_CertificateNO.Text.Trim().Length - 3, 3))%>','一寸免冠照片','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传免冠照片扫描件原件、一寸jpg格式图片，名称不限，最大为50K，宽高110 X 140像素）</span>
                                        </div>
                                        <div class="fujian">
                                            <span style="color: Red">* </span>证件扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.证件扫描件%>','证件扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传身份证扫描件原件、jpg格式图片,最大500K）</span>
                                        </div>
                                        <div class="fujian">
                                            学历证书扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.学历证书扫描件%>','学历证书扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传学历证书扫描件原件、jpg格式图片,最大500K）</span>
                                        </div>
                                        <div class="fujian">
                                            <span style="color: Red">* </span>解除劳动合同证明：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.解除劳动合同证明%>','解除劳动合同证明','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传解除劳动合同证明扫描件原件、jpg格式图片,最大500K）</span>
                                        </div>
                                        <div class="fujian">
                                           新劳动合同扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.劳动合同扫描件%>','劳动合同扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（<span style="color: Red">已退休人员需提供劳动合同或相关劳动关系证明。</span>要求：上传新劳动合同扫描件原件、只上传重要页,最多上传5页、jpg格式图片,每页最大500K）</span>
                                        </div>
                                        <div class="fujian">
                                            执业资格证书或资格考试合格通知书扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.执业资格证书扫描件%>','执业资格证书或资格考试合格通知书扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传执业资格证书扫描件原件、jpg格式图片,最大500K）</span>
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
                                        <div id="cns" class="fujian" visible="false" runat="server">
                                            新设立企业建造师注册承诺书：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.新设立企业建造师注册承诺书%>','新设立企业建造师注册承诺书','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：请先下载新设立企业建造师注册承诺书，单位盖章签字后扫描上传）</span> <a href="../Template/新设立企业建造师注册承诺书.docx">《新设立企业建造师注册承诺书》</a>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div id="divGR" visible="false" runat="server" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                                <div id="divStepDesc" runat="server" style="line-height: 300%;">操作流程：个人申请保存-->点击导出打印(导出申请表)-->上传相关附件-->提交单位审核</div>
                                <asp:Button ID="ButtonSave" runat="server" Text="保 存" CssClass="bt_large" UseSubmitBehavior="false" OnClientClick="if(CheckClientValidate()==true){this.value='正在提交';this.disabled=true;}" OnClick="ButtonSave_Click" Enabled="false" />
                                &nbsp;&nbsp;<asp:Button ID="ButtonOutput" runat="server" Text="导出打印" CssClass="bt_large" OnClick="ButtonOutput_Click" Enabled="false" />
                                &nbsp;&nbsp;<asp:Button ID="ButtonUnit" runat="server" Text="提交单位确认" CssClass="bt_large" OnClick="ButtonUnit_Click" OnClientClick="javascript:if(this.value=='提交单位确认'&&!confirm('1、请先确认申请表原、现聘用单位已加盖公章并上传。\r\n2、修改申请表填报内容后，请先保存再提交单位确认，否则无法记录修改内容。\r\n\r\n确定要提交单位审核吗？'))return false;" Enabled="false" CausesValidation="false" />
                                &nbsp;&nbsp;<asp:Button ID="ButtonDelete" runat="server" Text="删 除" CssClass="bt_large" OnClick="ButtonDelete_Click" Enabled="false" OnClientClick="javascript:if(!confirm('您确定要删除么?'))return false;" CausesValidation="false" />
                            </div>
                            <div style="color: red; line-height: 180%; text-align: left">特别提示：根据相关规定，同时持有二级建造师注册证书、安管人员考核证书(B或C1、C2、C3)的人员只能受聘于同一家企业，自2021年2月25日起，申请人办理二级建造师执业企业变更至已取得建筑业企业资质的企业时，其持有的安管人员考核证书自动同步变更到二级建造师现聘用企业。</div>
                            <div id="divCheckHistory" visible="true" runat="server" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                                <table id="Table1" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                    <tr class="GridLightBK">
                                        <td class="barTitle">审办记录</td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td align="left" style="border-collapse: collapse;">
                                            <telerik:RadGrid ID="RadGridCheckHistory" runat="server" CellPadding="0"
                                                GridLines="None" AutoGenerateColumns="False"
                                                Width="100%" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="False">
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
                            </div>
                            <div id="divOldQY" runat="server" visible="false" style="padding-top: 20px; text-align: center; clear: both;" class="auto-style3">
                                <table id="Table7" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                    <tr class="GridLightBK">
                                        <td colspan="2" class="barTitle">原单位确认</td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td width="20%" align="right">处理结果：</td>
                                        <td width="80%" align="left">
                                            <asp:RadioButtonList ID="RadioButtonListOldQYCheckResult" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                                <asp:ListItem Text="同意" Value="同意" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="不同意" Value="不同意"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td width="20%" align="right">处理意见：</td>
                                        <td width="80%" align="left">

                                            <asp:TextBox ID="TextBoxOldQYCheckRemark" runat="server" TextMode="MultiLine" Rows="3" Width="99%"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td colspan="2" align="center">
                                            <asp:Button ID="OldQYButtonApply" runat="server" CssClass="bt_large" Text="确定" OnClick="OldQYButtonApply_Click" />&nbsp;&nbsp;
                                         
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divUnit" runat="server" visible="false" style="padding-top: 20px; text-align: center; clear: both;" class="auto-style3">
                                <table id="Table6" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                    <tr class="GridLightBK">
                                        <td colspan="2" class="barTitle">现单位确认</td>
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
                            <div id="divQX" runat="server" visible="false" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
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
        </div>
        <div id="winpop">
        </div>
        <script type="text/javascript">
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
            })
            //原单位审核结果
            $("#<%= RadioButtonListOldQYCheckResult.ClientID%> input").each(function () {
                $(this).click(function () {
                    var TextBoxOldQYCheckRemark = $("#<%= TextBoxOldQYCheckRemark.ClientID%>");
                    if ($(this).val() == "同意") {
                        TextBoxOldQYCheckRemark.val("提交区县审核");
                    }
                    else {
                        TextBoxOldQYCheckRemark.val("退回个人");
                    }
                });
            });
            //现单位审核结果
            $("#<%= RadioButtonListOldUnitCheckResult.ClientID%> input").each(function () {
                $(this).click(function () {
                    var TextBoxOldUnitCheckRemark = $("#<%= TextBoxOldUnitCheckRemark.ClientID%>");
                    if ($(this).val() == "同意") {
                        TextBoxOldUnitCheckRemark.val("提交区县审核");
                    }
                    else {
                        TextBoxOldUnitCheckRemark.val("退回个人");
                    }
                });
            });
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

                            var RadTextBoxCreditCode = $find("<%= ZZJGDM_check.ClientID%>");
                            var RadTextBoxUnitName = $find("<%= RadTextBoxUnitName.ClientID%>");
                            RadTextBoxCreditCode.set_value(data.ENT_OrganizationsCode);
                            RadTextBoxUnitName.set_value(data.ENT_Name);
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
            function alertinfo() {
                var info = $("#ButtonUnit").val();
                if (info == "提交单位确认") {
                    var result = confirm('您确定要提交吗?');
                    if (!result) { return false }
                }
            }
        </script>
    </form>
</body>
</html>

