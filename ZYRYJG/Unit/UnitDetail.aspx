<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitDetail.aspx.cs" Inherits="ZYRYJG.Unit.UnitDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <style>
        .detailTable{
            width:90%;
        }
        .infoHead{
            width:30%;
            text-align:right;
            vertical-align:top;
            font-weight:bold;
            line-height:150%;
        }
        .formItem_1{
            width:70%;
            text-align:left;
            vertical-align:top;
        }

        .formItem_1 input {
            border:none;
            line-height:150%;
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
                <div  id="div_top" class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;信息查询 &gt;&gt;<strong>企业信息</strong>
                    </div>
                </div>
                <div class="content">
                    <p class="jbxxbt">
                        企业信息详细
                    </p>
                    <div style="width: 95%; margin: 10px auto; text-align: center;">
                        <table runat="server" id="EditTable" class="detailTable">                            
                            <tr>
                                <td class="infoHead">企业名称：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxENT_Name" runat="server" CssClass="textEdit" MaxLength="200"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="infoHead">社会统一信用代码：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxCreditCode" runat="server" CssClass="textEdit" MaxLength="18"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="infoHead">组织机构代码：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxENT_OrganizationsCode" runat="server" CssClass="textEdit" MaxLength="18"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="infoHead">企业性质：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxENT_Economic_Nature" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                                
                            </tr>
                            
                            <tr>
                                <td class="infoHead">所属区县：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxENT_City" runat="server" CssClass="textEdit" MaxLength="100"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="infoHead">工商注册地址：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxEND_Addess" runat="server" CssClass="textEdit" MaxLength="200"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="infoHead">企业法人：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxENT_Corporate" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="infoHead">通讯地址：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxENT_Correspondence" runat="server" CssClass="textEdit" MaxLength="200"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="infoHead">邮政编码：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxENT_Postcode" runat="server" CssClass="textEdit" MaxLength="6"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="infoHead">联系人：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxENT_Contact" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="infoHead">联系电话：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxENT_Telephone" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="infoHead">手机：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxENT_MobilePhone" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="infoHead">企业类型：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxENT_Type" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="infoHead">资质类别：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxENT_Sort" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="infoHead">资质等级：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxENT_Grade" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td class="infoHead">资质证书编号：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxENT_QualificationCertificateNo" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                                
                            </tr>
                          
                            <tr>
                                <td class="infoHead">备注：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxMemo" runat="server" CssClass="textEdit" MaxLength="1000"></asp:TextBox>
                                </td>
                                
                            </tr>
                            <tr>
                                <td colspan="3" align="center" valign="middle" style="padding-top: 10px; padding-bottom: 20px">
                                    <div id="DivButtons" runat="server">
                                       <%-- <asp:Button ID="ButtonSubmit" runat="server" Text="保 存" CssClass="smlbtn" OnClick="ButtonSubmit_Click" />&nbsp;&nbsp;<input
                                            class="smlbtn" type="reset" value="重 置" />--%>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>

                </div>
            </div>
        </div>
    </form>
</body>
</html>
