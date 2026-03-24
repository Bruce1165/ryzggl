<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitMgr.aspx.cs" Inherits="ZYRYJG.Unit.UnitMgr" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
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

        .table td {
            border: 1px solid #efefef;
        }
        .red{color:red;padding-right:5px;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1" EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
        </telerik:RadWindowManager>
        <div class="div_out">

            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;信息查看 &gt;&gt;<strong>企业信息</strong>
                </div>
            </div>
            <div class="content">
                <asp:Label ID="lblGSXX" runat="server" Font-Size="18px" />
             
            </div>
            <div class="content">
                <table id="TableEdit" runat="server" width="98%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                    <tr class="GridLightBK">
                        <td colspan="6" class="barTitle">组织机构代码信息</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="10%" align="right">企业名称：</td>
                        <td colspan="5" align="left">
                            <telerik:RadTextBox ID="RadTextBoxENT_Name" runat="server" Width="50%"></telerik:RadTextBox><asp:Label ID="LabelXSL" runat="server" Text="【新设立企业（无资质）】" Visible="false" ForeColor="Red" Font-Bold="true"></asp:Label>
                        </td>

                    </tr>
                    <tr class="GridLightBK">
                        <td width="10%" align="right">组织机构代码：</td>
                        <td width="23%" align="left">
                            <telerik:RadTextBox ID="RadTextBoxENT_OrganizationsCode" runat="server" Width="99%"></telerik:RadTextBox>
                        </td>
                        <td width="10%" align="right">企业性质：</td>
                        <td width="23%" align="left">
                            <telerik:RadTextBox ID="RadTextBoxENT_Economic_Nature" runat="server" Width="99%"></telerik:RadTextBox>
                            <telerik:RadComboBox ID="RadComboBoxENT_Economic_Nature" runat="server" Visible="false">
                                <Items>
                                    <telerik:RadComboBoxItem Text="请选择" Value="" />
                                    <telerik:RadComboBoxItem Text="有限责任公司" Value="有限责任公司" />
                                    <telerik:RadComboBoxItem Text="股份有限公司" Value="股份有限公司" />
                                    <telerik:RadComboBoxItem Text="股份合作企业" Value="股份合作企业" />
                                    <telerik:RadComboBoxItem Text="国有企业" Value="国有企业" />
                                    <telerik:RadComboBoxItem Text="集体企业" Value="集体企业" />
                                    <telerik:RadComboBoxItem Text="私营企业" Value="私营企业" />
                                    <telerik:RadComboBoxItem Text="联营企业" Value="联营企业" />
                                    <telerik:RadComboBoxItem Text="港、澳、台商投资企业" Value="港、澳、台商投资企业" />
                                    <telerik:RadComboBoxItem Text="外商投资企业" Value="外商投资企业" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td width="10%" align="right"><span class="red">*</span>区县：</td>
                        <td width="23%" align="left">
                            <telerik:RadTextBox ID="RadTextBoxENT_City" runat="server" Width="99%"></telerik:RadTextBox>
                            <telerik:RadComboBox ID="RadComboBoxENT_City" runat="server" >
                                <Items>
                                    <telerik:RadComboBoxItem Text="请选择" Value="" />
                                    <telerik:RadComboBoxItem Text="东城区" Value="东城区" />
                                    <telerik:RadComboBoxItem Text="西城区" Value="西城区" />
                                    <telerik:RadComboBoxItem Text="朝阳区" Value="朝阳区" />
                                    <telerik:RadComboBoxItem Text="海淀区" Value="海淀区" />
                                    <telerik:RadComboBoxItem Text="丰台区" Value="丰台区" />
                                    <telerik:RadComboBoxItem Text="石景山区" Value="石景山区" />
                                    <telerik:RadComboBoxItem Text="昌平区" Value="昌平区" />
                                    <telerik:RadComboBoxItem Text="通州区" Value="通州区" />
                                    <telerik:RadComboBoxItem Text="顺义区" Value="顺义区" />
                                    <telerik:RadComboBoxItem Text="门头沟区" Value="门头沟区" />
                                    <telerik:RadComboBoxItem Text="房山区" Value="房山区" />
                                    <telerik:RadComboBoxItem Text="大兴区" Value="大兴区" />
                                    <telerik:RadComboBoxItem Text="怀柔区" Value="怀柔区" />
                                    <telerik:RadComboBoxItem Text="平谷区" Value="平谷区" />
                                    <telerik:RadComboBoxItem Text="密云区" Value="密云区" />
                                    <telerik:RadComboBoxItem Text="延庆区" Value="延庆区" />
                                    <telerik:RadComboBoxItem Text="亦庄" Value="亦庄" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                    </tr>

                    <tr class="GridLightBK">
                        <td width="13%" align="right">统一社会信用代码：</td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxTYXYDM" runat="server" Width="95%" MaxLength="18"></telerik:RadTextBox>
                        </td>
                        <td colspan="4">
                            <%--<asp:Button Width="100px" ID="btnSubmit" runat="server" Text="验证工商信息" OnClick="btnSubmit_Click" CssClass="button" />
                            <div runat="server" id="div_Checking" visible="false" style="background:url(../layer/skin/default/loading-0.gif) 100px center no-repeat;">正在验证中</div>--%>
                        </td>                       
                    </tr>
                    <tr class="GridLightBK">
                        <td colspan="6" class="barTitle">企业联系信息</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="13%" align="right">工商注册地址：</td>
                        <td colspan="3" align="left">
                            <span class="red">*</span><telerik:RadTextBox ID="RadTextBoxEND_Addess" runat="server" Width="99%" MaxLength="100"></telerik:RadTextBox>
                        </td>
                        <td width="10%" align="right"><span class="red">*</span>企业法人：</td>
                        <td width="23%" align="left">
                            <telerik:RadTextBox ID="RadTextBoxENT_Corporate" runat="server" Width="99%"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="11%" align="right"><span class="red">*</span>通讯地址：</td>
                        <td colspan="3" align="left">
                            <telerik:RadTextBox ID="RadTextBoxENT_Correspondence" runat="server" Width="99%"></telerik:RadTextBox>
                        </td>

                        <td width="13%" align="right">邮政编码：</td>
                        <td align="left">
                            <telerik:RadTextBox ID="RadTextBoxENT_Postcode" runat="server" Width="99%" MaxLength="6"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="10%" align="right"><span class="red">*</span>联系人：</td>
                        <td width="23%" align="left">
                            <asp:TextBox ID="TextBoxENT_Contact" runat="server" Width="99%" MaxLength="25"></asp:TextBox>

                        </td>
                        <td width="10%" align="right"><span class="red">*</span>联系电话：</td>
                        <td width="23%" align="left">
                            <asp:TextBox ID="TextBoxENT_Telephone" runat="server" Width="99%" MaxLength="25"></asp:TextBox>
                        </td>

                        <td width="13%" align="right">手机：</td>
                        <td width="23%" align="left" colspan="5">
                            <asp:TextBox ID="TextBoxENT_MobilePhone" runat="server" Width="99%" MaxLength="25"></asp:TextBox>
                        </td>
                    </tr>

                    <tr class="GridLightBK">
                        <td colspan="6" class="barTitle">企业资质</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="13%" align="right">企业类型：</td>
                        <td colspan="5" align="left">
                            <telerik:RadTextBox ID="RadTextBoxENT_Type" runat="server" Width="99%"></telerik:RadTextBox>
                        </td>

                    </tr>
                    <tr class="GridLightBK">
                        <td width="13%" align="right">资质证书编号：</td>
                        <td width="23%" align="left">
                            <telerik:RadTextBox ID="RadTextBoxENT_QualificationCertificateNo" runat="server" Width="99%" MaxLength="50"></telerik:RadTextBox>
                        </td>
                        <td width="10%" align="right">资质类别：</td>
                        <td width="23%" align="left">
                            <telerik:RadTextBox ID="RadTextBoxENT_Sort" runat="server" Width="99%" MaxLength="500"></telerik:RadTextBox>
                        </td>
                        <td width="10%" align="right">资质等级：</td>
                        <td width="23%" align="left">
                            <telerik:RadTextBox ID="RadTextBoxENT_Grade" runat="server" Width="99%" MaxLength="50"></telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td colspan="6" class="barTitle">安全生产许可证信息</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="13%" align="right">安全生产许可证号：</td>
                        <td width="23%" align="left">
                            <telerik:RadTextBox ID="RadTextBoxXKZH" runat="server" Width="99%"></telerik:RadTextBox>
                        </td>
                        <td width="10%" align="right">核发时间：</td>
                        <td width="23%" align="left">
                            <telerik:RadTextBox ID="RadTextBoxFZRQ" runat="server" Width="99%"></telerik:RadTextBox>
                        </td>
                        <td width="10%" align="right">延续有效期：</td>
                        <td width="23%" align="left">
                            <telerik:RadTextBox ID="RadTextBoxKSYXQ" runat="server" Width="99%"></telerik:RadTextBox>
                        </td>
                    </tr>
                </table>

            </div>
            <div style="text-align: center">
                <asp:Button ID="ButtonSave" runat="server" Text="保存" OnClick="ButtonSave_Click" CssClass="button" />
            </div>
        </div>
    </form>
</body>
</html>
