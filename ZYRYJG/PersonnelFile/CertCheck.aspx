<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CertCheck.aspx.cs" Inherits="ZYRYJG.PersonnelFile.CertCheck" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Css/cerCheck.css?v=1.04" rel="stylesheet" type="text/css" />
    <meta name="viewport" content="width=device-width, initial-scale=0.6,  minimum-scale=0.6, maximum-scale=1.0" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="mDiv">
            <div class="title">
                北京市住房和城乡建设领域人员资格管理信息系统
                <br />
                证书信息验证
            </div>
            <table cellpadding="5" cellspacing="1" class="table" align="center">
                <tr>
                    <td width="9%" nowrap="nowrap" align="right">
                        <strong>姓 名</strong>
                    </td>
                    <td>
                        <asp:Label ID="LabelWorkerName" runat="server" Text=""></asp:Label>
                    </td>

                    <td rowspan="4" align="center" nowrap="nowrap" style="width: 6rem">
                        <img id="ImgCode" runat="server" src="~/Images/photo_ry.jpg"
                            alt="一寸照片" />
                    </td>
                </tr>
                <tr>
                    <td align="right" width="9%">
                        <strong>性 别</strong>
                    </td>
                    <td>
                        <asp:Label ID="LabelSex" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">
                        <strong>出生日期</strong>
                    </td>
                    <td>
                        <asp:Label ID="LabelBirthday" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">
                        <strong>证件号码</strong>
                    </td>
                    <td>
                        <asp:Label ID="LabelWorkerCertificateCode" runat="server" Text=""></asp:Label>
                    </td>

                </tr>
                <tr id="trUnit" runat="server">
                    <td align="right" nowrap="nowrap">
                        <strong>企业名称</strong>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="LabelUnitName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr id="trUnitCode" runat="server">
                    <td align="right" nowrap="nowrap">
                        <strong>组织机构代码</strong>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="LabelUnitCode" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                 <tr id="trTrainUnit" runat="server" visible="false">
                    <td align="right" nowrap="nowrap">
                        <strong>培训考核机构</strong>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="LabelTrainUnit" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">
                        <strong>岗位类别</strong>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="LabelPostTypeID" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">
                        <strong>岗位工种</strong>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="LabelPostID" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td nowrap="nowrap" align="right">
                        <strong>
                            <nobr>技术职称或等级</nobr>
                        </strong>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="LabelSKILLLEVEL" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">
                        <strong>证书编号</strong>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="LabelCertificateCode" runat="server" Text=""></asp:Label><asp:Label ID="LabelPrintCount" runat="server" Text="" Visible="false" Font-Bold="true" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr id="trValidate" runat="server">
                    <td align="right" nowrap="nowrap">
                        <strong>有效期至</strong>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="LabelValidate" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td nowrap="nowrap" align="right">
                        <strong>发证机关</strong>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="LabelConferUnit" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap">
                        <strong>发证日期</strong>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="LabelConferDate" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
               <%-- <tr>
                    <td align="right" nowrap="nowrap">
                        <strong>制证日期</strong>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="LabelZZRQ" runat="server" Text=""></asp:Label>
                    </td>
                </tr>--%>
                <tr>
                    <td nowrap="nowrap" align="right">
                        <strong>证书状态</strong>
                    </td>
                    <td colspan="2">
                        <asp:Label ID="LabelStatus" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:Label ID="LabelDesc" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" id="td_tip" runat="server" visible="false" style="color:red">
                        特别提示：此证书已经开始实施新版国标电子证书，过渡期间您可以继续使用旧版电子证书。过渡期截止后旧版电子证书将无法验证，请及时下载新版电子证书。
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
