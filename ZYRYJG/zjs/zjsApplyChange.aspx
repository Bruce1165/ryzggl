<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="zjsApplyChange.aspx.cs" Inherits="ZYRYJG.zjs.zjsApplyChange" %>

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

        td {
            line-height: 200%;
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

        .ts {
            line-height: 200%;
            color: #999;
            text-align: center;
        }

        .auto-style1 {
            color: #434343;
            font-weight: bold;
            text-align: left;
            height: 33px;
            border-left: 5px solid #ff6a00;
            background-color: #E4E4E4;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;二级造价工程师注册 &gt;&gt;<strong>个人信息变更</strong>
                    </div>
                </div>
                <div class="content">
                    <div class="step">
                        <div class="stepLabel">办理进度：</div>
                        <div id="step_未申报" runat="server" class="stepItem lgray">个人填写></div>
                        <div id="step_待确认" runat="server" class="stepItem lgray">待单位审核></div>
                        <div id="step_已申报" runat="server" class="stepItem lgray">单位申报></div>
                        <div id="step_已受理" runat="server" class="stepItem lgray">市级受理></div>
                        <div id="step_已审核" runat="server" class="stepItem lgray">市级审核></div>
                        <div id="step_已决定" runat="server" class="stepItem lgray">市级决定（办结）</div>
                        <div class="stepArrow">▶</div>
                    </div>
                    <div style="width: 100%; margin: 10px auto; text-align: center; overflow: hidden;">
                        <div class="code">申请编号：<asp:Label ID="LabelApplyCode" runat="server" Text=""></asp:Label></div>
                        <div style="width: 66%; float: left; clear: left">
                            <table runat="server" id="EditTable" class="detailTable">
                                <%--个人信息变更--%>
                                <tr class="GridLightBK">
                                    <td colspan="4" class="auto-style1">变更选项</td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="infoHead">变更项目：</td>
                                    <td align="center" style="font-weight: 700">变更前</td>
                                    <td align="center"><strong>变更后</strong></td>
                                </tr>
                                <tr>
                                    <td colspan="4" class="ts">提示：变更一寸免冠照片或手写签名照，需要要首先点击保存按钮后，系统才会提供上传照片、签名图片位置。</td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="infoHead">一寸免冠照片：</td>
                                    <td class="formItem_1" style="text-align: center">
                                        <img id="ImgOldPhoto" runat="server" style="border: none" height="140" width="110" src="~/Images/photo_ry.jpg"
                                            alt="一寸照片" />
                                    </td>
                                    <td class="formItem_2" style="text-align: center">
                                        <img id="ImgUpdatePhoto" runat="server" style="border: none;" height="140" width="110" src="~/Images/photo_ry.jpg"
                                            alt="一寸照片" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="infoHead">手写签名照：</td>
                                    <td class="formItem_1" style="text-align: center">
                                        <img id="ImgOldSign" runat="server" height="43" width="99" alt="手写签名照" style="border: 1px solid #dddddd;" src="~/Images/SignImg.jpg" />
                                    </td>
                                    <td class="formItem_2" style="text-align: center">
                                        <img id="ImgUpdateSign" runat="server" height="43" width="99" alt="手写签名照" style="border: 1px solid #dddddd;" src="~/Images/SignImg.jpg" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="infoHead">姓名：</td>
                                    <td class="formItem_1">
                                        <asp:Label ID="LabelPSN_NameFrom" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="formItem_2">
                                        <telerik:RadTextBox ID="RadTextBoxPSN_NameTo" runat="server"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="ValidatorPSN_NameTo" runat="Server" ControlToValidate="RadTextBoxPSN_NameTo"
                                            ErrorMessage="请输入姓名" Display="Dynamic">*请输入姓名 </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="infoHead">性别：</td>
                                    <td class="formItem_1">
                                        <asp:Label ID="LabelFromPSN_Sex" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="formItem_2">
                                        <telerik:RadComboBox ID="RadComboBoxToPSN_Sex" runat="server" Width="125px">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Text="男" Value="男" />
                                                <telerik:RadComboBoxItem runat="server" Text="女" Value="女" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="infoHead">出生日期：</td>
                                    <td class="formItem_1">
                                        <asp:Label ID="LabelFromPSN_BirthDate" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="formItem_2">
                                        <telerik:RadDatePicker ID="RadDatePickerToPSN_BirthDate" runat="server" MinDate="1900-1-1" MaxDate="2050-1-1" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                            Style="display: inline-table">
                                        </telerik:RadDatePicker>
                                        <asp:RequiredFieldValidator ID="ValidatorToPSN_BirthDate" runat="Server" ControlToValidate="RadDatePickerToPSN_BirthDate"
                                            ErrorMessage="请填写出生日期" Display="Dynamic">*请填写出生日期 </asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="infoHead">证件号码：</td>
                                    <td class="formItem_1">
                                        <asp:Label ID="LabelFromPSN_CertificateNO" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="formItem_2">
                                        <telerik:RadTextBox ID="RadTextBoxToPSN_CertificateNO" runat="server" Width="180px"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="ValidatorToPSN_CertificateNO" runat="Server" ControlToValidate="RadTextBoxToPSN_CertificateNO"
                                            ErrorMessage="请输入身份证号码" Display="Dynamic">*请输入身份证号码</asp:RequiredFieldValidator>
                                        <asp:RegularExpressionValidator ID="ZJH" runat="Server" ControlToValidate="RadTextBoxToPSN_CertificateNO" Display="Dynamic" ValidationExpression="\d{17}[\d|X]|\d{15}" ErrorMessage="*身份证号码错误">
                                        </asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" class="infoHead">资格证书编号/管理号：</td>
                                    <td class="formItem_1">
                                        <asp:Label ID="LabelZGZSBH" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td class="formItem_2">
                                        <telerik:RadTextBox ID="RadTextBoxTo_ZGZSBH" runat="server" Width="180px"></telerik:RadTextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="Server" ControlToValidate="RadTextBoxTo_ZGZSBH"
                                            ErrorMessage="请输入资格证书编号" Display="Dynamic">*请输入资格证书编号</asp:RequiredFieldValidator>
                                    </td>
                                </tr>
                                <tr class="GridLightBK">
                                    <td colspan="4" class="barTitle">其他信息</td>
                                </tr>
                                <tr>
                                    <td class="infoHead">邮箱：
                                    </td>
                                    <td class="formItem_1">
                                        <telerik:RadTextBox ID="RadTextBoxPSN_Email" runat="server" CssClass="textEdit" Width="98%" MaxLength="200"></telerik:RadTextBox>
                                    </td>
                                    <td class="infoHead">电话：
                                    </td>
                                    <td class="formItem_1">
                                        <asp:Label ID="LabelPSN_MobilePhone" runat="server" CssClass="textEdit" Width="98%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="infoHead">注册号：
                                    </td>
                                    <td class="formItem_1">
                                        <asp:Label ID="LabelPSN_RegisterNO" runat="server" CssClass="textEdit" Width="98%"></asp:Label>
                                    </td>
                                    <td class="infoHead">注册专业：
                                    </td>
                                    <td class="formItem_1">
                                        <asp:Label ID="LabelPSN_RegisteProfession" runat="server" CssClass="textEdit" Width="98%"></asp:Label>
                                        <asp:Label ID="LabelPSN_CertificateValidity" runat="server" CssClass="textEdit" Width="98%" Visible="false"></asp:Label>
                                        <telerik:RadTextBox ID="RadTextBoxPSN_CertificateType" runat="server" CssClass="textEdit" Width="98%" Visible="false"></telerik:RadTextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="infoHead">企业名称：
                                    </td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelENT_Name" runat="server" CssClass="textEdit" Width="98%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="infoHead">工商注册地：
                                    </td>
                                    <td class="formItem_1" colspan="3">
                                        <asp:Label ID="LabelEND_Addess" runat="server" CssClass="textEdit" Width="98%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="infoHead">机构代码：
                                    </td>
                                    <td class="formItem_1">
                                        <asp:Label ID="LabelENT_OrganizationsCode" runat="server" CssClass="textEdit" Width="98%"></asp:Label>
                                    </td>
                                    <td class="infoHead">所在区县：
                                    </td>
                                    <td class="formItem_1">
                                        <asp:Label ID="LabelENT_City" runat="server" CssClass="textEdit" Width="98%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="infoHead">企业类型：
                                    </td>
                                    <td class="formItem_1">
                                        <asp:Label ID="LabelENT_Type" runat="server" CssClass="textEdit" Width="98%"></asp:Label>
                                    </td>
                                    <td class="infoHead">法定代表人：
                                    </td>
                                    <td class="formItem_1">
                                        <asp:Label ID="LabelFR" runat="server" CssClass="textEdit" Width="98%"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="infoHead">联系人：
                                    </td>
                                    <td class="formItem_1">
                                        <asp:Label ID="LabelLinkMan" runat="server" CssClass="textEdit" Width="98%"></asp:Label>
                                    </td>
                                    <td class="infoHead">联系电话：
                                    </td>
                                    <td class="formItem_1">
                                        <asp:Label ID="LabelENT_Telephone" runat="server" CssClass="textEdit" Width="98%"></asp:Label>
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
                                            一寸免冠照片：<span class="link" onclick="javascript:tips_pop('<%=string.Format("{0}/{1}",Model.EnumManager.FileDataType.一寸免冠照片,RadTextBoxToPSN_CertificateNO.Text.Trim().Substring(RadTextBoxToPSN_CertificateNO.Text.Trim().Length - 3, 3))%>','一寸免冠照片','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：变更一寸照片的，需要上传一寸白底免冠彩色照片，最大为50K，宽高110 X 140像素）</span>
                                        </div>
                                        <div class="fujian">
                                            手写签名照：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.手写签名照%>','手写签名照','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：变更手写签名照的，上传手写签名照扫描件、jpg格式图片,最大500K）</span>
                                        </div>
                                        <div class="fujian">
                                            证件扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.证件扫描件%>','证件扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传身份证扫描件、jpg格式图片,最大500K）</span>
                                        </div>
                                        <%--<div class="fujian">
                                            职业资格证书（或二级造价师专业类别考试合格证书）扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.造价工程师执业资格证书扫描件%>','执业资格证书或资格考试合格通知书扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传执业资格证书扫描件、jpg格式图片,最大500K）</span>
                                        </div>--%>
                                        <div class="fujian">
                                            申请表扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.申请表扫描件%>','申请表扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：请在本页面导出打印申请表，单位盖章签字后扫描上传）</span>
                                        </div>
                                        <div class="fujian">
                                            个人信息变更证明：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.个人信息变更证明%>','个人信息变更证明','','<%=ApplyID%>')">选择文件</span>
                                            <span class="tishi">（要求：上传个人信息变更证明扫描件、jpg格式图片,最大500K）<br />
                                                ① 更换姓名的，还需居民户口簿扫描件。<br />
                                                ② 属于错号、重号等原因导致的身份证号码非正常升位的，还需公安机关户籍管理部门出具的《 公民身份号码更正证明》原件扫描件。<br />
                                                ③中国人民武装警察警官、解放军军官因转业、退休、自主择业等原因需变更身份信息的，还需提交警（军））官证和转业证书或退休证扫描件。
                                            </span>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div id="divGR" visible="false" runat="server" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                                <div id="divStepDesc" runat="server" style="line-height: 300%;">操作流程：个人申请保存-->点击导出打印(导出申请表)-->上传相关附件-->提交单位审核</div>
                                <asp:Button ID="ButtonSave" runat="server" Text="保 存" CssClass="bt_large" UseSubmitBehavior="false" OnClientClick="this.value='正在提交';this.disabled=true;" OnClick="ButtonSave_Click" Enabled="false" />
                                &nbsp;&nbsp;<asp:Button ID="ButtonOutput" runat="server" Text="导出打印" CssClass="bt_large" OnClick="ButtonOutput_Click" Enabled="false" />
                                &nbsp;&nbsp;<asp:Button ID="ButtonUnit" runat="server" Text="提交单位确认" CssClass="bt_large" OnClick="ButtonUnit_Click" Enabled="false" OnClientClick="javascript:if(this.value=='提交单位确认'){if(confirm('修改申请表填报内容后，请先保存再提交单位确认，否则无法记录修改内容。\r\n\r\n确定要提交单位审核吗？')==false) return false;} else {if(confirm('确认要取消申报吗？')==false) return false;}" />
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
                                            <asp:Button ID="ButtonApply" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonApply_Click" />&nbsp;&nbsp;
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
                                            <input id="BtnReturn" type="button" class="bt_large" value="返 回" onclick='javascript: hideIfam()' />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divQXCK" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
                                <table id="Table4" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                                    <tr class="GridLightBK">
                                        <td colspan="2" class="barTitle">审核操作</td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td width="20%" align="right">审核结果：</td>
                                        <td width="80%" align="left">
                                            <asp:RadioButtonList ID="RadioButtonListExamineResult" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                                <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td width="20%" align="right">审核意见：</td>
                                        <td width="80%" align="left">
                                            <asp:TextBox ID="TextBoxExamineRemark1" runat="server" TextMode="MultiLine" Rows="3" Width="99%" Text="允许通过"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td colspan="2" align="center">
                                            <asp:Button ID="BttSave" runat="server" CssClass="bt_large" Text="确认提交" OnClick="BttSave_Click" />&nbsp;&nbsp;
                                            <input id="BtnReturnck" type="button" class="bt_large" value="返 回" onclick='javascript: hideIfam()' />
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
                                            <input id="BtnReturn4" type="button" class="bt_large" value="返 回" onclick='javascript: hideIfam()' />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divSendBack" runat="server" visible="false" style="width: 98%; padding-top: 20px; text-align: center; clear: both;">
                                <table id="Table2" runat="server" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
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
                                                    <telerik:RadComboBoxItem Text="已审核" Value="已审核" />
                                                    <telerik:RadComboBoxItem Text="已决定" Value="已决定" />
                                                    <telerik:RadComboBoxItem Text="已公告" Value="已公告" />
                                                </Items>
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr class="GridLightBK">
                                        <td colspan="2" align="center">
                                            <asp:Button ID="ButtonSendBack" runat="server" CssClass="bt_large" Text="执行后退" OnClick="ButtonSendBack_Click" OnClientClick="javascript:if(!confirm('您确定要后退么?')) return false;" CausesValidation="false" />&nbsp;&nbsp;
                                            <input id="BtnReturn5" type="button" class="bt_large" value="返 回" onclick='javascript: hideIfam()' />
                                        </td>
                                    </tr>
                                </table>
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
                                                                        <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="../images/Cancel.gif" CommandName="Delete" Visible='<%#(IfExistRoleID("0") == true && ((ViewState["zjs_ApplyMDL"] as Model.zjs_ApplyMDL).ApplyStatus == Model.EnumManager.ApplyStatus.未申报 || (ViewState["zjs_ApplyMDL"] as Model.zjs_ApplyMDL).ApplyStatus == Model.EnumManager.ApplyStatus.已驳回))?true:false%>' OnClientClick="javascript:if(confirm('您确定要删除么?')==false) return false" />

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

                //变换单位审核结果
                $("#<%= RadioButtonListOldUnitCheckResult.ClientID%> input").each(function () {
                    $(this).click(function () {
                        var TextBoxOldUnitCheckRemark = $("#<%= TextBoxOldUnitCheckRemark.ClientID%>");

                        if ($(this).val() == "同意") {

                            TextBoxOldUnitCheckRemark.val("提交审核");
                        }
                        else {

                            TextBoxOldUnitCheckRemark.val("退回个人");

                        }
                    });

                });

                //变换受理结果
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
                //变换审核结果
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
    </form>
</body>
</html>
