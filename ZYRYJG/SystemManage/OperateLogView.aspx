<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="OperateLogView.aspx.cs" Inherits="ZYRYJG.SystemManage.OperateLogView" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        </telerik:RadCodeBlock>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>

        <div class="div_out">
              <div class="dqts">
                <div style="float: left;">
                    <img alt="" src="../Images/ts.gif" width="17" height="11" />当前位置 &gt;&gt; 系统管理 &gt;&gt; 操作日志 &gt;&gt;
                <strong>操作日志详细</strong>
                </div>
            </div>
        
            <div class="table_border" style="width: 98%; margin: 1% auto;">
                
                     
                        <table cellpadding="5" cellspacing="1" border="0" width="95%" class="table" align="center">
                            <tr class="GridLightBK">
                                <td width="20%" nowrap="nowrap" align="right">
                                    <strong>操作者：</strong>
                                </td>
                                <td width="80%" align="left">
                                    <asp:Label ID="LabelPersonName" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="20%" nowrap="nowrap" align="right">
                                    <strong>操作时间：</strong>
                                </td>
                                <td width="80%" align="left">
                                    <asp:Label ID="LabelLogTime" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="20%" nowrap="nowrap" align="right">
                                    <strong>操作名称：</strong>
                                </td>
                                <td width="80%" align="left">
                                    <asp:Label ID="LabelOperateName" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right" nowrap="nowrap" valign="top">
                                    <strong>详细说明：</strong>
                                </td>
                                <td width="80%" align="left" valign="top">
                                    <span id="P_OperateName" runat="server" style="line-height: 20px;"></span>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <div style="width: 95%; margin: 10px auto; text-align: center;">
                        <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript: hideIfam();" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
