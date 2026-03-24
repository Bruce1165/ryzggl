<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessAccepted.aspx.cs" Inherits="ZYRYJG.County.BusinessAccepted" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link href="../css/Style3.css" rel="stylesheet" type="text/css" />
    <link href="../css/Grid.Blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js" type="text/javascript"></script>
    <%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
    <%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
    
</head>
<body>
   <form id="form1" runat="server">
         <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Sunset">
        </telerik:RadWindowManager>
      <div class="div_out">
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        <img alt="" src="../Images/ts.gif" width="17" height="11" />当前位置 &gt;&gt;业务管理 &gt;&gt;<strong>业务受理</strong>
                    </div>
                </div>
                <div class="content">
                    <p class="jbxxbt">
                        业务受理
                    </p>
                    <div style="width: 95%; height:100%; margin: 10px auto; text-align:center;">
                         <table id="TableEdit" runat="server" width="98%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                               <tr class="GridLightBK">
                                  <td colspan="2" align="center">
                                      受理意思
                                  </td>
                               </tr>
                              <tr class="GridLightBK">
                                <td width="20%" align="right">受理结果：</td>
                                <td width="80%" align="left">
                                   <asp:RadioButtonList ID="RadioButtonListApplyStatus" runat="server" RepeatDirection="Horizontal"  TextAlign="right">
                                    <asp:ListItem Text="通过" Value="通过" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="不通过" Value="不通过"></asp:ListItem>
                                </asp:RadioButtonList>
                                </td>
                            </tr>
                              <tr class="GridLightBK">
                                <td width="20%" align="right">受理意见：</td>
                                <td width="80%" align="left">
                                    <telerik:RadTextBox ID="RadTextBoxGetResult" runat="server" Width="99%" Height="50px" TextMode="MultiLine"></telerik:RadTextBox>
                                </td>
                            </tr>
                             <tr class="GridLightBK">
                                <td colspan="2" style="height:200px"></td>
                            </tr>
                              <tr class="GridLightBK">
                                <td colspan="2" align="center">
                                    <asp:Button ID="BtnSave" runat="server" CssClass="button" Text="确认提交" OnClick="BtnSave_Click" />
                                    <input id="BtnReturn" type="button" class="button" value="取消提交" onclick='javascript: hideIfam()' />
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
