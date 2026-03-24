<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZSSDDetail.aspx.cs" Inherits="ZYRYJG.County.ZSSDDetail" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />

    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <style type="text/css">
        .detailTable {
            width: 98%;
        }

        .infoHead {
            width: 20%;
            text-align: right;
            vertical-align: top;
            font-weight: bold;
            line-height: 150%;
        }

        .formItem_1 {
            width: 30%;
            text-align: left;
            vertical-align: top;
        }

        .formItem_2 {
            text-align: left;
            vertical-align: top;
        }

        .formItem_1 input {
            border: none;
            line-height: 150%;
        }

        .barTitle {
            color: #434343;
            background-color: #E4E4E4;
            font-weight: bold;
            border-left: 5px solid #ff6a00;
            text-align: center;
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
                        当前位置 &gt;&gt;信息查询 &gt;&gt;<strong>在施锁定记录详细</strong>
                    </div>
                </div>
                <div class="content">
                    <div style="width: 95%; margin: 10px auto; text-align: center;">
                        <table runat="server" id="EditTable" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center;">
                            <tr class="GridLightBK">
                                <td colspan="2" class="barTitle">在施锁定记录详细</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td class="infoHead">建造师姓名：</td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxXM" runat="server" CssClass="textEdit" MaxLength="100" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td class="infoHead">证件号码：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxZJHM" runat="server" CssClass="textEdit" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td class="infoHead">注册证号：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxZCH" runat="server" CssClass="textEdit" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td class="infoHead">合同编号：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxHTBH" runat="server" CssClass="textEdit" MaxLength="80" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td class="infoHead">项目名称：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxXMMC" runat="server" CssClass="textEdit" MaxLength="200" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td class="infoHead">中标企业：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxZBQY" runat="server" CssClass="textEdit" MaxLength="500" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                              <tr class="GridLightBK">
                                <td class="infoHead">操作时间：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxSDSJ" runat="server" CssClass="textEdit" MaxLength="500" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                              <tr class="GridLightBK">
                                <td class="infoHead">操作动作：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxSDZT" runat="server" CssClass="textEdit" MaxLength="500" ReadOnly="true"></asp:TextBox>
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
