<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YJZJSDetail.aspx.cs" Inherits="ZYRYJG.Unit.YJZJSDetail" %>

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
                        当前位置 &gt;&gt;综合查询 &gt;&gt;人员证书查询 &gt;&gt;<strong>一级建造师</strong>
                    </div>
                </div>
                <div class="content">
                    <p class="jbxxbt">
                        一级建造师
                    </p>
                    <div style="width: 95%; margin: 10px auto; text-align: center;">
                        <%-- <table runat="server" id="EditTable" class="detailTable">--%>
                        <table runat="server" id="EditTable" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center;">
                            <tr class="GridLightBK">
                                <td colspan="5" class="barTitle">人员基本信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td class="infoHead">姓名：</td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxPSN_Name" runat="server" CssClass="textEdit" MaxLength="100"></asp:TextBox>
                                </td>
                                <td class="infoHead">性别：</td>
                                <td class="formItem_2">
                                    <asp:TextBox ID="TextBoxPSN_Sex" runat="server" CssClass="textEdit" MaxLength="2"></asp:TextBox>
                                </td>
                                <td class="formItem_3" rowspan="5">
                                    <img id="ImgCode" runat="server" height="140" width="110" src="~/Images/tup.gif"
                                        alt="一寸照片" />
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td class="infoHead">出生日期：</td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxPSN_BirthDate" runat="server" CssClass="textEdit" MaxLength="8"></asp:TextBox>
                                </td>
                                <td class="infoHead">民族：</td>
                                <td class="formItem_2">
                                    <asp:TextBox ID="TextBoxPSN_National" runat="server" CssClass="textEdit" MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td class="infoHead">证件类别：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxPSN_CertificateType" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                                <td class="infoHead">证件号码：
                                </td>
                                <td class="formItem_2">
                                    <asp:TextBox ID="TextBoxPSN_CertificateNO" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td class="infoHead">毕业院校：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxPSN_GraduationSchool" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                                <td class="infoHead">所学专业：
                                </td>
                                <td class="formItem_2">
                                    <asp:TextBox ID="TextBoxPSN_Specialty" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td class="infoHead">学历：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxPSN_Qualification" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                                <td class="infoHead">学位：
                                </td>
                                <td class="formItem_2">
                                    <asp:TextBox ID="TextBoxPSN_Degree" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td class="infoHead">毕业时间：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxPSN_GraduationTime" runat="server" CssClass="textEdit" MaxLength="8"></asp:TextBox>
                                </td>
                                <td class="infoHead">电子邮件：
                                </td>
                                <td class="formItem_1" colspan="2">
                                    <asp:TextBox ID="TextBoxPSN_Email" runat="server" CssClass="textEdit" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td class="infoHead">手机：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxPSN_MobilePhone" runat="server" CssClass="textEdit" MaxLength="100"></asp:TextBox>
                                </td>
                                <td class="infoHead">联系电话：
                                </td>
                                <td class="formItem_1" colspan="2">
                                    <asp:TextBox ID="TextBoxPSN_Telephone" runat="server" CssClass="textEdit" MaxLength="100"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <table runat="server" id="Table1" width="100%" border="0" cellpadding="5" cellspacing="0" class="table" style="text-align: center;">

                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">注册证书信息</td>
                            </tr>
                            <%--   <tr  class="GridLightBK">
                                <td class="infoHead">项目经理级别：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxPSN_PMGrade" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                                <td class="infoHead">项目经理证书编号：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxPSN_PMCertificateNo" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>--%>
                            <tr class="GridLightBK">

                                <td class="infoHead">注册证书编号：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxPSN_RegisterCertificateNo" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                                <td class="infoHead">注册号：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxPSN_RegisterNO" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                            </tr>

                            <tr class="GridLightBK">
                                <td class="infoHead">发证日期：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxPSN_CertificationDate" runat="server" CssClass="textEdit" MaxLength="8"></asp:TextBox>
                                </td>
                                <td class="infoHead">证书有效期：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxPSN_CertificateValidity" runat="server" CssClass="textEdit" MaxLength="8"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">

                                <td class="infoHead">注册专业：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxPSN_RegisteProfession" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                                <td class="infoHead">单位名称：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxENT_Name" runat="server" CssClass="textEdit" MaxLength="200"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td class="infoHead">注册类别：
                                </td>
                                <td class="formItem_1">
                                    <asp:TextBox ID="TextBoxPSN_RegisteType" runat="server" CssClass="textEdit" MaxLength="50"></asp:TextBox>
                                </td>
                                <td class="infoHead">注册审批日期
                                </td>
                                <td class="formItem_1" colspan="3">
                                    <asp:TextBox ID="TextBoxPSN_RegistePermissionDate" runat="server" CssClass="textEdit" MaxLength="8"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">证书注册历史</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" class="formItem_1">
                                    <telerik:RadGrid ID="RadGridHis" runat="server"
                                        GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" PageSize="15"
                                        Width="100%" Skin="Default" EnableAjaxSkinRendering="true"
                                        EnableEmbeddedSkins="true" PagerStyle-AlwaysVisible="true" BorderColor="#dddddd">
                                        <ClientSettings EnableRowHoverStyle="true">
                                        </ClientSettings>
                                        <HeaderContextMenu EnableEmbeddedSkins="False">
                                        </HeaderContextMenu>
                                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                                            DataKeyNames="">
                                            <Columns>
                                              
                                                <telerik:GridBoundColumn UniqueName="PSN_RegistePermissionDate" DataField="PSN_RegistePermissionDate" HeaderText="时间" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn UniqueName="PSN_RegisteTypeName" DataField="PSN_RegisteTypeName" HeaderText="注册类型">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="PSN_CertificateNO" DataField="PSN_CertificateNO" HeaderText="证件号码">
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn UniqueName="PSN_RegisteProfession" DataField="PSN_RegisteProfession" HeaderText="注册专业">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="PSN_RegisterNO" DataField="PSN_RegisterNO" HeaderText="注册编号">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称">
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
                                <td colspan="4" class="barTitle">参与项目记录</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" class="formItem_1">
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
                                                <%-- <telerik:GridBoundColumn UniqueName="GCBM" DataField="GCBM" HeaderText="工程编码">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                                                --%>
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
                                <td colspan="4" class="barTitle">继续教育记录</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" class="formItem_1">没有可显示的记录
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">获奖信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" class="formItem_1">没有可显示的记录
                                </td>
                            </tr>
                             <tr class="GridLightBK">
                                <td colspan="4" class="barTitle">处罚信息</td>
                            </tr>
                            <tr class="GridLightBK">
                                <td colspan="4" class="formItem_1">没有可显示的记录
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
