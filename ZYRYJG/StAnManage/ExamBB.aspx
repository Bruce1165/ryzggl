<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamBB.aspx.cs" Inherits="ZYRYJG.StAnManage.ExamBB" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
 <link href="../Css/styleRed.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="div_out">
        <div class="dqts">
            <img src="../Images/ts.gif" width="17" height="11" />当前位置 &gt;&gt; 统计分析
      &gt;&gt; 从业人员业务统计 &gt;&gt; <strong>证书管理报表</strong> 
        </div>
        <div class="table_border" style="width:92%;margin:5px auto;">
            <div class="content">
                <p class="jbxxbt">证书管理报表</p>
                <div>
                    <p class="table_cx"><img src="../Images/1034.gif" width="11" height="11" style="margin-bottom:-2px;padding-right:2px;"/> 查询条件</p>
                    <div class="blue_center" style="width:95%;margin:0 auto;">
                        <div><b class="subxtop"><b class="subxb1"></b><b class="subxb2"></b><b class="subxb3"></b><b class="subxb4"></b></b></div>
                        <div class="subxboxcontent">
                             <table width="95%" border="0" align="center" cellspacing="5">
                                  <tr>
                                    <td align="right" width="11%" nowrap="nowrap">岗位类别：</td>
                                    <td colspan="3" nowrap="nowrap" width="35%">
                                        <asp:TextBox ID="TextBox1" runat="server" Cssclass="texbox" style="width:90%"></asp:TextBox></td>
                                    <td width="11%" align="right" nowrap="nowrap">岗位工种：</td>
                                    <td nowrap="nowrap"><asp:TextBox ID="TextBox3" runat="server" Cssclass="texbox" style="width:90%"></asp:TextBox></td>
                                  </tr>
                                  <tr>
                                    <td align="right" nowrap="nowrap">日期： </td>
                                    <td nowrap="nowrap"><asp:TextBox ID="TextBox2" runat="server" Cssclass="texbox" style="width:80%"></asp:TextBox></td>
                                    <td width="2%" nowrap="nowrap">至</td>
                                    <td nowrap="nowrap"><asp:TextBox ID="TextBox4" runat="server" Cssclass="texbox" style="width:78%"></asp:TextBox></td>
                                    <td align="right" nowrap="nowrap">&nbsp;</td>
                                    <td  nowrap="nowrap">&nbsp;</td>
                                  </tr>                             
                                  <tr>
                                    <td colspan="6" align="center">
                                        <asp:Button ID="Button1" runat="server" Text="查询" Cssclass="button"/>
                                      &nbsp;&nbsp;
                                      <asp:Button ID="Button2" runat="server" Text="重置" Cssclass="button"/>                                      </td>
                                  </tr>
                            </table>
                        </div>
                        <div><b class="subxbottom"></b><b class="subxb4"></b><b class="subxb3"></b><b class="subxb2"></b><b class="subxb1"></b></div>
                    </div>
                </div>
                <div>
                    &nbsp;
                    <div align="center"><img src="../Images/images2/tb2.png" width="600" height="319" /></div>
                </div>            
            </div>
        </div>
    </div>
    </form>
</body>
</html>