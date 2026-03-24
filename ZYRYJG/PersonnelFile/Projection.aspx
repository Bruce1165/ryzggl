<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Projection.aspx.cs" Inherits="ZYRYJG.PersonnelFile.Projection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../Css/styleRed.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="div_out">
        <div class="dqts">
            <img src="../Images/ts.gif" width="17" height="11" />当前位置 &gt;&gt; 人员档案 &gt;&gt;
            <strong>人员参与工程信息</strong>
        </div>
        <div class="table_border" style="width: 98%; margin: 5px auto;">
            <div class="content">
                <p class="jbxxbt">
                    人员参与工程信息</p>
                <div>
                    <p class="table_cx">
                        <img src="../Images/1034.gif" width="11" height="11" style="margin-bottom: -2px;
                            padding-right: 2px;" />
                        查询条件</p>
                    <div class="blue_center" style="width: 98%; margin: 0 auto;">
                        <div>
                            <b class="subxtop"><b class="subxb1"></b><b class="subxb2"></b><b class="subxb3"></b>
                                <b class="subxb4"></b></b>
                        </div>
                        <div class="subxboxcontent">
                            <table width="95%" border="0" align="center" cellspacing="5">
                                <tr>
                                    <td width="12%" align="right" nowrap="nowrap">
                                        姓名：
                                    </td>
                                    <td width="38%" nowrap="nowrap">
                                        <asp:TextBox ID="Name" runat="server" CssClass="texbox" Style="width: 90%"></asp:TextBox>
                                    </td>
                                    <td width="12%" align="right" nowrap="nowrap">
                                        证件号码：
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="texbox" Style="width: 90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap="nowrap">
                                        工程名称：
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="TextBox6" runat="server" CssClass="texbox" Style="width: 90%"></asp:TextBox>
                                    </td>
                                    <td align="right" nowrap="nowrap">
                                        企业名称：
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="TextBox2" runat="server" CssClass="texbox" Style="width: 90%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap="nowrap">
                                        所在岗位：
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="TextBox4" runat="server" CssClass="texbox" Style="width: 90%"></asp:TextBox>
                                    </td>
                                    <td align="right" nowrap="nowrap">
                                        &nbsp;
                                    </td>
                                    <td nowrap="nowrap">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:Button ID="Button1" runat="server" Text="查 询" CssClass="button" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <b class="subxbottom"></b><b class="subxb4"></b><b class="subxb3"></b><b class="subxb2">
                            </b><b class="subxb1"></b>
                        </div>
                    </div>
                </div>
                <div>
                    <p class="table_cx">
                        <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px;
                            padding-right: 2px;" />
                        人员参与工程信息列表</p>
                    <div>
                        <%--<asp:DataGrid ID="grid" runat="server" CssClass="table" AutoMouseStyle="True"
                        CellSpacing="1" AllowSorting="True" CellPadding="0" 
                        AllowPaging="True" Width="98%" DataKeyField="ID" AllowCustomPaging="True"
                        ShowHeader="True" GridLines="Both">
                        <EditItemStyle CssClass="font2"></EditItemStyle>
                        <AlternatingItemStyle CssClass="GridLightBK"></AlternatingItemStyle>
                        <ItemStyle CssClass="GridDarkBK"></ItemStyle>
                        <HeaderStyle CssClass="GridHead"></HeaderStyle>
						<FooterStyle CssClass="GridFooter"></FooterStyle>
                        <Columns>
                            <asp:BoundColumn HeaderText="序号" DataField="ID" 
                                 HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" >
                            </asp:BoundColumn>
                            
                            <asp:BoundColumn HeaderText="姓名" DataField="Name" 
                                 HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" >
                            </asp:BoundColumn>
                            
                              <asp:BoundColumn HeaderText="证件号码" DataField="CertifNumb" 
                                 HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" >
                            </asp:BoundColumn>
                             
                            <asp:BoundColumn HeaderText="工程名称" DataField="GCMC" 
                                 HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" >
                            </asp:BoundColumn> 
                            
                            <asp:BoundColumn HeaderText="企业名称" DataField="QYMC" 
                                 HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" >
                            </asp:BoundColumn>
                            
                            <asp:BoundColumn HeaderText="所在岗位" DataField="Post" 
                                 HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" >
                            </asp:BoundColumn> 
                        </Columns>
                    </asp:DataGrid>--%>
                        <table cellpadding="3" cellspacing="1" border="0" width="95%" class="table" align="center">
                            <tr class="GridHead">
                                <td nowrap="nowrap" align="center">
                                    序号
                                </td>
                                <td nowrap="nowrap" align="center">
                                    姓名
                                </td>
                                <td nowrap="nowrap" align="center">
                                    证件号码
                                </td>
                                <td nowrap="nowrap" align="center">
                                    工程名称
                                </td>
                                <td nowrap="nowrap" align="center">
                                    企业名称
                                </td>
                                <td align="center" nowrap="nowrap">
                                    所在岗位
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="center">
                                    1
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td width="147">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr class="GridDarkBK">
                                <td nowrap="nowrap" align="center">
                                    2
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="center">
                                    3
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr class="GridDarkBK">
                                <td align="center" nowrap="nowrap">
                                    4
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="center">
                                    5
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr class="GridDarkBK">
                                <td nowrap="nowrap" align="center">
                                    6
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="center">
                                    7
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr class="GridDarkBK">
                                <td nowrap="nowrap" align="center">
                                    8
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td nowrap="nowrap" align="center">
                                    9
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr class="GridDarkBK">
                                <td nowrap="nowrap" align="center">
                                    10
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        <table cellpadding="0" cellspacing="0" border="0" width="95%" align="center">
                            <tr>
                                <td style="width: 50%;" align="left">
                                    <span class="Label">共</span> <span class="Label">1</span> <span class="Label">页</span>&nbsp;
                                    <span class="Label">第</span> <span class="Label">1</span> <span class="Label">页</span>
                                    &nbsp; <span class="Label">10条/页</span>
                                </td>
                                <td align="right">
                                    <a class="LinkButton" href="">头页</a> <a class="LinkButton" href="">上一页</a> <a class="LinkButton"
                                        href="">下一页</a> <a class="LinkButton" href="">尾页</a>&nbsp;
                                    <input name="Input" type="text" style="width: 30px; height: 15px; border: 1px solid #ccc;">
                                    <img src="../Images/go_little.gif" style="margin-bottom: -5px;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!--	<div style="width:90%;margin:5px auto;text-align:center;">
                          <asp:Button ID="Button3" runat="server" Text="申请" Cssclass="button"/>                         
                           &nbsp;&nbsp;
                          <asp:Button ID="Button5" runat="server" Text="返回" Cssclass="button"/>
                    </div>-->
                </div>
                <!-- <div>
                    <div style="width:95%;margin:0 auto;padding:10px;font-size:12px;color:#5b7ba2;line-height:18px;">
                    <strong>说明：</strong>
                       点击申请后，系统将自动产生一个申请编号，便于下一步工作查找数据
                    </div>
                    <br />
                </div>-->
            </div>
        </div>
    </div>
    </form>
</body>
</html>
