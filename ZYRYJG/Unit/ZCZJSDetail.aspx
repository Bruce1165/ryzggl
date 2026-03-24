<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ZCZJSDetail.aspx.cs" Inherits="ZYRYJG.Unit.ZCZJSDetail" %>

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
            text-align: left;
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
                         当前位置 &gt;&gt;综合查询 &gt;&gt;人员证书查询 &gt;&gt;<strong>一级造价师工程师</strong>
                    </div>
                </div>
                <div class="content">
                    <div style="width: 95%; margin: 10px auto; text-align: center;">
                        <table runat="server" id="EditTable" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center;">
                            <tr class="GridLightBK">
                                 <td colspan="5" class="barTitle">注册证书信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td class="infoHead">姓名：</td>
                                <td class="formItem_1" >
                                    <asp:TextBox ID="TextBoxPSN_Name" runat="server" CssClass="textEdit" MaxLength="100" ReadOnly="true"></asp:TextBox>
                                </td>
                             <td class="infoHead">证件号码：
                                </td>
                                <td class="formItem_1" >
                                    <asp:TextBox ID="TextBoxPSN_CertificateNO" runat="server" CssClass="textEdit" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                </td>
                                <td class="formItem_3"  rowspan="3" >
                                    <img id="ImgCode" runat="server" height="140" width="110" src="~/Images/tup.gif"
                                        alt="一寸照片" />
                                </td>
                            </tr>   
                            <tr class="GridLightBK">                              
                                <td class="infoHead">注册证号：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxPSN_RegisterNO" runat="server" CssClass="textEdit" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                </td>                           

                               <td class="infoHead">注册专业：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxZY" runat="server" CssClass="textEdit" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                </td>                           

                            </tr>                          
                             <tr class="GridLightBK">                              
                                
                                <td class="infoHead">发证日期：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxFZRQ" runat="server" CssClass="textEdit" MaxLength="10" ReadOnly="true"></asp:TextBox>
                                </td>
                                  <td class="infoHead">有效期：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxPSN_CertificateValidity" runat="server" CssClass="textEdit" MaxLength="10" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>                          
                             <tr class="GridLightBK">
                                <td class="infoHead">单位名称：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxENT_Name" runat="server" CssClass="textEdit" MaxLength="200" ReadOnly="true"></asp:TextBox>
                                </td>
                            
                                <td class="infoHead">所在区县：
                                </td>
                                   <td class="formItem_1">
                                    <asp:TextBox ID="TextBox所在区县" runat="server" CssClass="textEdit" MaxLength="50" ReadOnly="true"></asp:TextBox>
                                </td>
                                 </tr>
                          
                            <tr class="GridLightBK">
                                <td colspan="5" class="barTitle">证书注册历史</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="5" class="formItem_1">
                                   没有可显示的记录
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="5" class="barTitle">参与项目记录</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="5" class="formItem_1">
                                    <telerik:RadGrid ID="RadGridGC" runat="server"
                                        GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" PageSize="15"
                                        Width="100%" Skin="Default" EnableAjaxSkinRendering="true"
                                        EnableEmbeddedSkins="true" PagerStyle-AlwaysVisible="true" BorderColor="#dddddd">
                                        <ClientSettings EnableRowHoverStyle="true">
                                        </ClientSettings>
                                        <HeaderContextMenu EnableEmbeddedSkins="False">
                                        </HeaderContextMenu>
                                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                                            DataKeyNames="GCBM">
                                            <Columns>
                                                <telerik:GridBoundColumn UniqueName="SGDW" DataField="SGDW" HeaderText="单位名称">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                               
                                                <telerik:GridBoundColumn UniqueName="GCJSDD" DataField="GCJSDD" HeaderText="建设地点">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="GCMC" DataField="GCMC" HeaderText="工程名称">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="HTKGRQ" DataField="HTKGRQ" HeaderText="工程开始时间" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="HTJGRQ" DataField="HTJGRQ" HeaderText="工程结束时间" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                            </Columns>
                                            <HeaderStyle Font-Bold="True" />

                                        </MasterTableView>
                                        <FilterMenu EnableEmbeddedSkins="False">
                                        </FilterMenu>

                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="5" class="barTitle">继续教育记录</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="5" class="formItem_1">没有可显示的记录
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
