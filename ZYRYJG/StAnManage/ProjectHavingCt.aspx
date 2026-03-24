<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectHavingCt.aspx.cs" Inherits="ZYRYJG.StAnManage.ProjectHavingCt" %>

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
      &gt;&gt; 综合查询 &gt;&gt; <strong>工程持证人员</strong> 
        </div>
        <div class="table_border" style="width:92%;margin:5px auto;">
            <div class="content">
                <p class="jbxxbt">工程持证人员查询</p>
                <div>
                    <p class="table_cx"><img src="../Images/1034.gif" width="11" height="11" style="margin-bottom:-2px;padding-right:2px;"/> 查询条件</p>
                    <div class="blue_center" style="width:95%;margin:0 auto;">
                        <div><b class="subxtop"><b class="subxb1"></b><b class="subxb2"></b><b class="subxb3"></b><b class="subxb4"></b></b></div>
                        <div class="subxboxcontent">
                             <table width="95%" border="0" align="center" cellspacing="5">
                                  <tr>
                                    <td align="right" width="11%" nowrap="nowrap">工程名称：</td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="TextBox1" runat="server" Cssclass="texbox" style="width:90%"></asp:TextBox></td>
                                    <td width="11%" align="right" nowrap="nowrap">企业名称：</td>
                                    <td nowrap="nowrap"><asp:TextBox ID="TextBox3" runat="server" Cssclass="texbox" style="width:90%"></asp:TextBox></td>
                                  </tr>
                                  <tr>
                                    <td align="right" nowrap="nowrap">岗位类别： </td>
                                    <td nowrap="nowrap">
									<asp:TextBox ID="TextBox2" runat="server" Cssclass="texbox" style="width:90%"></asp:TextBox>                                    </td>
                                    <td align="right" nowrap="nowrap">岗位工种：</td>
                                    <td  nowrap="nowrap"><asp:TextBox ID="TextBox4" runat="server" Cssclass="texbox" style="width:90%"></asp:TextBox>                                    </td>
                                  </tr>                             
                                  <tr>
                                    <td colspan="4" align="center">
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
                    <p class="table_cx"><img src="../Images/jglb.png" width="15" height="15" style="margin-bottom:-2px;padding-right:2px;"/> 工程持证人员列表</p>
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
                            
                            <asp:BoundColumn HeaderText="企业名称" DataField="QYMC" 
                                 HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" >
                            </asp:BoundColumn>
                             
                            <asp:BoundColumn HeaderText="姓名" DataField="NAME" 
                                 HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" >
                            </asp:BoundColumn>
							
                            <asp:BoundColumn HeaderText="证件号码" DataField="IdCard" 
                                 HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" >
                            </asp:BoundColumn>
                            
                            <asp:BoundColumn HeaderText="岗位工种" DataField="GwGz" 
                                 HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" >
                            </asp:BoundColumn>
                            
                            <asp:BoundColumn HeaderText="证书编号" DataField="ZSBH" 
                                 HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" >
                            </asp:BoundColumn> 
                            
                            <asp:BoundColumn HeaderText="证书有效期" DataField="DateCt" 
                                 HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Middle" >
                            </asp:BoundColumn> 
							
                        </Columns>
                    </asp:DataGrid>--%>
                    <table cellpadding="3" cellspacing="1" border="0" width="95%" class="table" align="center">
                        <tr class="GridHead">
                          <td nowrap="nowrap" align="center">序号</td>
                          <td nowrap="nowrap" align="center">企业名称</td>
                          <td nowrap="nowrap" align="center">姓名</td>
                          <td nowrap="nowrap" align="center">证件号码</td>
                          <td nowrap="nowrap"align="center">岗位工种</td>				  
                          <td nowrap="nowrap"  align="center">证书编号</td>
                          <td  align="center" nowrap="nowrap">证书有效期</td>
                      </tr>
                        <tr class="GridLightBK">
                          <td nowrap="nowrap" align="center">1</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td width="147">&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                        </tr>
                        <tr class="GridDarkBK">
                          <td nowrap="nowrap" align="center">2</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                        </tr>
                        <tr class="GridLightBK">
                          <td nowrap="nowrap" align="center">3</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                        </tr>
                        <tr class="GridDarkBK">
                          <td align="center"  nowrap="nowrap">4</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                        </tr>
                        <tr class="GridLightBK">
                          <td nowrap="nowrap" align="center">5</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                        </tr>
                        <tr class="GridDarkBK">
                          <td nowrap="nowrap" align="center">6</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td align="center">&nbsp;</td>
                          <td align="center">&nbsp;</td>
                        </tr>
                        <tr class="GridLightBK">
                          <td nowrap="nowrap" align="center">7</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td align="center">&nbsp;</td>
                          <td align="center">&nbsp;</td>
                        </tr>
                        <tr class="GridDarkBK">
                          <td nowrap="nowrap" align="center">8</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td align="center">&nbsp;</td>
                          <td align="center">&nbsp;</td>
                        </tr>
                        <tr class="GridLightBK">
                          <td nowrap="nowrap" align="center">9</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td align="center">&nbsp;</td>
                          <td align="center">&nbsp;</td>
                        </tr>
                        <tr class="GridDarkBK">
                          <td nowrap="nowrap" align="center">10</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td>&nbsp;</td>
                          <td align="center">&nbsp;</td>
                          <td align="center">&nbsp;</td>
                        </tr>
                      </table>
                        <table cellpadding="0" cellspacing="0" border="0" width="95%" align="center">
                          <tr>
                            <td style="width: 50%;" align="left"><span  class="Label">共</span> <span  class="Label">1</span> <span  class="Label">页</span>&nbsp; <span  class="Label">第</span> <span  class="Label">1</span> <span  class="Label">页</span> &nbsp; <span  class="Label">10条/页</span> </td>
                            <td  align="right"><a  class="LinkButton" href="">头页</a> <a  class="LinkButton" href="">上一页</a> <a  class="LinkButton" href="">下一页</a> <a  class="LinkButton" href="">尾页</a>&nbsp;
                                <input name="Input" type="text" style="width:30px;height:15px; border:1px solid #ccc;">
                                <img src="../Images/go_little.gif" style="margin-bottom:-5px;" /></td>
                          </tr>
                        </table>
						<div style="width:95%;margin:0 auto;text-align:center;">
                            <asp:Button ID="Button3" runat="server" Text="导出" CssClass="button" />
                            &nbsp;
                            <asp:Button ID="Button4" runat="server" Text="返回" CssClass="button" />
						</div>
                    </div>
                </div>
             <!--   <div>
                    <p class="table_cx"><img src="../Images/Soft_common.gif" style="margin-bottom:-2px;padding-right:2px;"/> 说明</p>
                    <div style="width:95%;margin:0 auto;padding:10px;font-size:12px;color:#5b7ba2;line-height:18px;border:1px solid #ccc;">
                    
                        1、东考试报名点地址：北京朝阳水碓东路18号院（北京城建学校院内） 电话：010-85982272 可乘公交31路、117路、419路、710路、729路、752路、754路、815路、847路、852路、988路甜水园北里下车或乘9路金台路下车或乘115路团结湖公园下车。

2、西考试报名点地址：北京海淀西四环北路137号院（田村社区服务中心院内） 电话：010-88111278 可乘公交61路、121路、334路、603路、996路、运通110路、运通113路、运通118路五路桥下车（西四环路西侧）即到。

3、最终的考试时间与考试地点以准考证为准。
                    </div>
                    <br />
                </div>-->
            </div>
        </div>
    </div>
    </form>
</body>
</html>