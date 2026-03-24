<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyCancel.aspx.cs" Inherits="ZYRYJG.Unit.ApplyCancel" %>

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
    <%--<script type="text/javascript">
        $(function () {
            var clicktag = 0;
            $("#ButtonSave").click(function () {
                if (clicktag == 0) {
                    clicktag = 1;
                    setTimeout(function () { clicktag = 0 }, 5000);
                }
                else {
                    alert('请勿频繁点击！');
                }
            });
        });
 </script>--%>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;事项申报 &gt;&gt;<strong>注销注册</strong>
                </div>
            </div>
            <div class="content">
                <p class="jbxxbt">
                    注销注册
                </p>
                <div class="step">
                    <div class="stepLabel">办理进度：</div>
                    <div id="step_未申报" runat="server" class="stepItem lgray">个人填写></div>
                    <div id="step_待确认" runat="server" class="stepItem lgray">待单位审核></div>
                    <div id="step_已申报" runat="server" class="stepItem lgray">已申报></div>
                    <div id="step_已受理" runat="server" class="stepItem lgray">区级受理></div>
                    <div id="step_区县审查" runat="server" class="stepItem lgray">区级审核></div>
                    <div id="step_已上报" runat="server" class="stepItem lgray">汇总上报></div>
                    <div id="step_已审查" runat="server" class="stepItem lgray">市级审核></div>
                    <div id="step_已决定" runat="server" class="stepItem lgray">市级决定</div>

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
                                <td class="infoHead w20">企业名称：
                                </td>
                                <td colspan="3" class="formItem_1 w40">
                                    <telerik:RadTextBox ID="RadTextBoxENT_Name" runat="server" CssClass="textEdit" Width="98%" MaxLength="200"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">机构代码（或信用代码）：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxENT_OrganizationsCode" runat="server" CssClass="textEdit" Width="98%" MaxLength="50"></telerik:RadTextBox>
                                </td>
                                <td class="infoHead">所在区县：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxENT_City" runat="server" CssClass="textEdit" Width="98%" MaxLength="100"></telerik:RadTextBox>
                                </td>

                            </tr>
                            <tr>
                                <td class="infoHead">联系人：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxLinkMan" runat="server" Width="99%"></telerik:RadTextBox>
                                </td>
                                <td class="infoHead">联系电话：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxENT_Telephone" runat="server" CssClass="textEdit" Width="98%" MaxLength="100"></telerik:RadTextBox>
                                </td>

                            </tr>
                            <tr>
                                <td class="infoHead">姓名：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_Name" runat="server" CssClass="textEdit" Width="98%" MaxLength="50"></telerik:RadTextBox>
                                </td>
                                <td class="infoHead">性别：
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
                                <td class="infoHead">证件号码：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_CertificateNO" runat="server" CssClass="textEdit" Width="98%" MaxLength="30"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">手机：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_MobilePhone" runat="server" CssClass="textEdit" Width="98%" MaxLength="100"></telerik:RadTextBox>
                                </td>
                                <td class="infoHead">邮箱：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_Email" runat="server" CssClass="textEdit" Width="98%" MaxLength="200"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">注册号：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_RegisterNO" runat="server" CssClass="textEdit" Width="98%" MaxLength="50"></telerik:RadTextBox>
                                </td>
                                <td class="infoHead">注册证书编号：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxPSN_RegisterCertificateNo" runat="server" CssClass="textEdit" Width="98%" MaxLength="50"></telerik:RadTextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="infoHead">民族：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadComboBox ID="RadComboBoxNation" runat="server" Width="98%" NoWrap="true" DropDownCssClass="multipleRowsColumns" DropDownWidth="620px" Height="300px">
                                    </telerik:RadComboBox>
                                    <asp:CompareValidator ValueToCompare="请选择" Operator="NotEqual" ControlToValidate="RadComboBoxNation"
                                        ErrorMessage="必填" runat="server" ID="Comparevalidator4" ForeColor="Red" Display="Dynamic" />
                                </td>
                                <td class="infoHead">有效期截止日期：
                                </td>
                                <td class="formItem_1">
                                    <telerik:RadTextBox ID="RadTextBoxRegisterValidity" runat="server" CssClass="textEdit" Width="98%" MaxLength="50"></telerik:RadTextBox>
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
                                                <telerik:RadTextBox ID="RadTextBoxPSN_RegisteProfession1" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                            <td class="formItem_1" style="width: 40%">
                                                <telerik:RadTextBox ID="RadTextBoxPSN_CertificateValidity1" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBoxPSN_RegisteProfession2" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBoxPSN_CertificateValidity2" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBoxPSN_RegisteProfession3" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBoxPSN_CertificateValidity3" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBoxPSN_RegisteProfession4" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                            <td class="formItem_1">
                                                <telerik:RadTextBox ID="RadTextBoxPSN_CertificateValidity4" runat="server" CssClass="readonly" Width="98%" MaxLength="50" ReadOnly="true"></telerik:RadTextBox>
                                            </td>
                                        </tr>
                                    </table>

                                </td>

                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">注销信息</td>
                            </tr>
                            <tr>
                                <td class="infoHead">注销专业：
                                </td>
                                <td colspan="3" class="formItem_2 w80">
                                    <asp:CheckBoxList ID="CheckBoxListPSN_RegisteProfession" runat="server" RepeatDirection="Horizontal" RepeatLayout="Table"></asp:CheckBoxList>
                                </td>
                            </tr>
                            <tr style="display: none;">
                                <td class="infoHead">申请注销注册人或机构：
                                </td>
                                <td colspan="3" align="left" class="formItem_2 w80">
                                    <asp:RadioButtonList ID="RadioButtonListApplyManType" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                                        <asp:ListItem Text="注册建造师本人" Value="注册建造师本人" Selected="true"></asp:ListItem>
                                        <asp:ListItem Text="聘用企业" Value="聘用企业"></asp:ListItem>
                                        <asp:ListItem Text="省级建设主管部门" Value="省级建设主管部门"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr id="tr_enforce" runat="server" >
                                <td class="infoHead">是否申请强制注销：
                                </td>
                                <td  class="formItem_2">
                                    <asp:RadioButtonList ID="RadioButtonList_enforce" runat="server" RepeatDirection="Horizontal" Style="display: inline" OnSelectedIndexChanged="RadioButtonList_enforce_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Text="否" Value="" Selected="True"></asp:ListItem>
                                        <asp:ListItem Text="是" Value="申请强制执行"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </td>
                                <td colspan="2" style="text-align:left"><%--（特别提示：强制执行申请直接提交到住建委）--%></td>
                            </tr>
                            <tr>
                                <td class="infoHead">注销原因：
                                </td>
                                <td colspan="3" align="left" class="formItem_2 w80">
                                    <asp:RadioButtonList ID="RadioButtonListCancelReason" runat="server" RepeatDirection="Vertical" RepeatColumns="1" TextAlign="right">
                                        <asp:ListItem Text="聘用企业破产" Value="聘用企业破产"></asp:ListItem>
                                        <asp:ListItem Text="死亡或不具有完全民事行为能力" Value="死亡或不具有完全民事行为能力"></asp:ListItem>
                                        <asp:ListItem Text="聘用企业被吊销营业执照" Value="聘用企业被吊销营业执照"></asp:ListItem>
                                        <asp:ListItem Text="聘用企业被吊销或者撤回资质证书" Value="聘用企业被吊销或者撤回资质证书"></asp:ListItem>
                                        <asp:ListItem Text="其他导致注册失效的情形" Value="其他导致注册失效的情形"></asp:ListItem>
                                        <asp:ListItem Text="已与聘用企业解除聘用合同关系" Value="已与聘用企业解除聘用合同关系"></asp:ListItem>
                                        <asp:ListItem Text="依法被撤消注册的" Value="依法被撤消注册的"></asp:ListItem>
                                        <asp:ListItem Text="注册有效期满且未延续注册" Value="注册有效期满且未延续注册"></asp:ListItem>
                                        <asp:ListItem Text="依法被吊销注册证书" Value="依法被吊销注册证书"></asp:ListItem>
                                        <asp:ListItem Text="年龄超过65周岁" Value="年龄超过65周岁"></asp:ListItem>
                                        <asp:ListItem Text="法律、法规规定应当注销注册的其他情形" Value="法律、法规规定应当注销注册的其他情形"></asp:ListItem>

                                        <asp:ListItem Text="已与聘用单位解除劳动关系的" Value="已与聘用单位解除劳动关系的"></asp:ListItem>
                                        <asp:ListItem Text="距离注册专业有效期不足30天的" Value="距离注册专业有效期不足30天的"></asp:ListItem>
                                        <asp:ListItem Text="受到行政或刑事处罚且在处罚期内的" Value="受到行政或刑事处罚且在处罚期内的"></asp:ListItem>
                                        <asp:ListItem Text="法律、法规规定其他导致注册证书失效的" Value="法律、法规规定其他导致注册证书失效的"></asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:RadioButton ID="RadioButtonWorkerEnforceApply" runat="server" Visible="false" Checked="true" Text="申请强制注销（因企业不配合、企业营业执照注销等原因）" />
                                    <asp:RadioButton ID="RadioButtonUnitEnforceApply" runat="server" Visible="false"  Checked="true"   Text="申请强制注销（因二级建造师不配合等原因）" />
                                </td>
                            </tr>
                            <%-- <tr id="trJiePinShiJian" runat="server" visible="false">
                                <td class="infoHead">解聘时间：
                                </td>
                                <td colspan="3" align="left" class="formItem_2 w80">
                                    <telerik:RadDatePicker ID="RadDatePickerENT_ContractENDTime" runat="server" MinDate="1900-1-1" MaxDate="2090-1-1" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"></telerik:RadDatePicker>
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
                                        一寸免冠照片：<span class="link" onclick="javascript:tips_pop('<%=string.Format("{0}/{1}",Model.EnumManager.FileDataType.一寸免冠照片,RadTextBoxPSN_CertificateNO.Text.Trim().Substring(RadTextBoxPSN_CertificateNO.Text.Trim().Length - 3, 3))%>','一寸免冠照片','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传免冠照片扫描件原件、一寸jpg格式图片，名称不限，最大为50K，宽高110 X 140像素）</span>
                                    </div>
                                    <div class="fujian">
                                        符合注销注册情形的相关证明：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.符合注销注册情形的相关证明%>','符合注销注册情形的相关证明','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：上传符合注销注册情形的相关证明扫描件原件、jpg格式图片,最大500K）</span>
                                    </div>
                                    <div class="fujian">
                                        申请表扫描件：<span class="link" onclick="javascript:tips_pop('<%=Model.EnumManager.FileDataType.申请表扫描件%>','申请表扫描件','','<%=ApplyID%>')">选择文件</span><span class="tishi">（要求：请在本页面导出打印申请表，单位盖章签字后扫描上传）</span>
                                    </div>
                                </td>
                            </tr>
                        </table>

                        <div id="divGR" visible="false" runat="server" style="width: 100%; padding-top: 20px; text-align: center; clear: both;">
                            <div id="divStepDesc" runat="server" style="line-height: 300%;">操作流程：个人申请保存-->点击导出打印(导出申请表)-->上传相关附件-->提交单位审核</div>
                            <asp:Button ID="ButtonSave" runat="server" Text="保 存" CssClass="bt_large" UseSubmitBehavior="false" OnClientClick="var isValid = Page_ClientValidate();if(isValid==false) return false;this.value='正在提交';this.disabled=true;" OnClick="ButtonSave_Click" Enabled="false" />
                            &nbsp;&nbsp;<asp:Button ID="ButtonOutput" runat="server" Text="导出打印" CssClass="bt_large" OnClick="ButtonOutput_Click" Enabled="false" />
                            &nbsp;&nbsp;<asp:Button ID="ButtonUnit" runat="server" Text="提交单位确认" CssClass="bt_large" OnClick="ButtonUnit_Click" Enabled="false" OnClientClick="javascript:if(this.value=='提交建委审核'){if(confirm('修改申请表填报内容后，请先保存再提交建委审核，否则无法记录修改内容。\r\n\r\n确定要提交建委审核吗？')==false) return false;} else if(this.value=='提交单位确认'){if(confirm('修改申请表填报内容后，请先保存再提交单位确认，否则无法记录修改内容。\r\n\r\n确定要提交单位审核吗？')==false) return false;} else {if(confirm('确认要取消申报吗？')==false) return false;}" />
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
                                                <telerik:RadComboBoxItem Text="已决定" Value="已公告" />
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
                                                                    <asp:ImageButton runat="server" ID="ImageButton1" ImageUrl="../images/Cancel.gif" CommandName="Delete" Visible='<%#((IfExistRoleID("0") == true || (IfExistRoleID("2") && (ViewState["ApplyCancelMDL"] as Model.ApplyCancelMDL).CancelReason=="申请强制注销（因二级建造师不配合等原因）")) && ((ViewState["ApplyMDL"] as Model.ApplyMDL).ApplyStatus == Model.EnumManager.ApplyStatus.未申报 || (ViewState["ApplyMDL"] as Model.ApplyMDL).ApplyStatus == Model.EnumManager.ApplyStatus.已驳回))?true:false%>' OnClientClick="javascript:if(confirm('您确定要删除么?')==false) return false" />

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


                //单位审核结果
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
        </script>
    </form>
</body>
</html>
