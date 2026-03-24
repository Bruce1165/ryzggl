<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessAccomplishDetail.aspx.cs" Inherits="ZYRYJG.County.BusinessAccomplishDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/Style3.css" rel="stylesheet" type="text/css" />
    <link href="../css/Grid.Blue.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.9.1.min.js"></script>
    <script src="../Scripts/Public.js" type="text/javascript"></script>
    <%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
    <%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
    <style type="text/css">
        .barTitle {
            color: #434343;
            background-color: #E4E4E4;
            font-weight: bold;
            border-left: 5px solid #ff6a00;
            text-align: left;
        }

        .img {
            border: none;
            width: 0px;
        }

        .img200 {
            border: none;
            width: 200px;
        }

        .subtable td {
            border: 1px solid #cccccc;
            border-collapse: collapse;
        }

        .addrow {
            float: right;
            background: url(../images/jiah.gif) no-repeat center center;
            width: 15px;
            height: 15px;
            padding-right: 20px;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            var imgWid = 0;
            var imgHei = 0; //变量初始化
            var big = 2.5;//放大倍数
            $(".img200").hover(function () {
                $(this).stop(true, true);
                var imgWid2 = 0; var imgHei2 = 0;//局部变量
                imgWid = $(this).width();
                imgHei = $(this).height();
                imgWid2 = imgWid * big;
                imgHei2 = imgHei * big;

                $("#divImg").css({ "float": "right", "overflow": "visible" });
                $(this).animate({ "width": imgWid2, "height": imgHei2, "margin-left": -imgWid * (big - 1), "position": "absolute", "z-index": 999 });
            }, function () {
                $("#divImg").css({ "float": "right", "overflow": "auto" });
                $(this).stop().animate({ "width": imgWid, "height": imgHei, "margin-left": 0, "position": "relative", "float": "none" });
            });
        })
    </script>
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
                        <img alt="" src="../Images/ts.gif" width="17" height="11" />当前位置 &gt;&gt;业务管理 &gt;&gt;已办业务 &gt;&gt;<strong>详细预览</strong>
                    </div>
                </div>
                <div class="content">
                    <p class="jbxxbt">
                        详细预览
                    </p>
                    <div style="width: 100%; margin: 0; text-align: center; overflow: hidden;">
                        <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1" SelectedIndex="0" Skin="Sitefinity" Style="text-align: center; font-weight: 700">
                            <Tabs>
                                <telerik:RadTab runat="server" Text="人员信息" PageViewID="RadPageView1" Selected="True" Font-Size="Large">
                                </telerik:RadTab>
                                <telerik:RadTab runat="server" Text="审办记录" PageViewID="RadPageView2" Font-Size="Large">
                                </telerik:RadTab>
                            </Tabs>
                        </telerik:RadTabStrip>
                        <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="0">
                            <telerik:RadPageView ID="RadPageView1" runat="server">
                                <div style="width: 65%; float: left; clear: left">
                                    <table runat="server" id="GetTable" width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center;">
                                        <tr class="GridLightBK">
                                            <td colspan="2" class="barTitle">所属企业信息</td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">组织机构代码</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelENT_OrganizationsCode" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">企业名称</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelENT_Name" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td colspan="2" class="barTitle">人员基本信息</td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">人员类型</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="Label3" runat="server" Text="二级建筑工程师"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">姓名</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_Name" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">性别</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_Sex" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">名族</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_National" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">出生年月</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_BirthDate" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td colspan="2" class="barTitle">人员证件照信息</td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">证件类型</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_CertificateType" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">证件号码</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_CertificateNO" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td colspan="2" class="barTitle">学历信息</td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">毕业学校</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_GraduationSchool" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">所学专业</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_Specialty" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">毕业（肄业）时间</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_GraduationTime" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">最高学历</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_Qualification" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">学位</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_Degree" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td colspan="2" class="barTitle">联系信息</td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">手机</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_MobilePhone" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">联系电话</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_Telephone" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">电子邮箱</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_Email" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <%-- <tr class="GridLightBK">
                                            <td colspan="2" class="barTitle">社会保险</td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">是否离退休</td>
                                            <td width="72%" align="left">
                                                 <asp:Label ID="Label18" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td colspan="2" class="barTitle">劳动合同信息</td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">劳动合同开始时间</td>
                                            <td width="72%" align="left">
                                                  <asp:Label ID="Label19" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">劳动合同结束时间</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="Label20" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>--%>
                                        <tr class="GridLightBK">
                                            <td colspan="2" class="barTitle">执业资格证书信息</td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">执业资格证书专业类别</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_RegisteProfession" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">执业资格证书编号</td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_RegisterCertificateNo" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <%--  <tr class="GridLightBK">
                                            <td width="28%" align="right">执业资格证书级别</td>
                                            <td width="72%" align="left">
                                                  <asp:Label ID="Label23" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>--%>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">签发日期 </td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_CertificationDate" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="GridLightBK">
                                            <td width="28%" align="right">注册有效期 </td>
                                            <td width="72%" align="left">
                                                <asp:Label ID="LabelPSN_CertificateValidity" runat="server" Text=""></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                   <div id="divImg" style="width: 32%; float: left; clear: right; margin-left: 1%; overflow: auto; border: 1px solid #cccccc; margin-bottom: 200px">
                               <%-- <div id="divImg" style="width: 32%; float: right; clear: right; margin-left: 1%; height: 835px; overflow: scroll; border: 1px solid #cccccc">--%>
                                    <telerik:RadGrid ID="RadGridFile" runat="server"
                                        GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                                        Width="100%" Skin="Default" EnableAjaxSkinRendering="false"
                                        EnableEmbeddedSkins="false">
                                        <ClientSettings EnableRowHoverStyle="false">
                                        </ClientSettings>
                                        <MasterTableView NoMasterRecordsText=" 没有相关附件" GridLines="None" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                                            DataKeyNames="ApplyID,FileID">
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="ApplyFile" HeaderText="附件">
                                                    <ItemTemplate>
                                                        <div class="DivTitleOn" onclick="DivOnOff(this,'Div<%# Eval("FileID") %>',event);" title="折叠">
                                                            <%# Eval("FileName") %>
                                                        </div>
                                                        <div class="DivContent" id="Div<%# Eval("FileID") %>">
                                                            <img class="img200" alt="图片" src="<%# Eval("FileUrl").ToString().Replace("~","..") %>" />
                                                        </div>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" Height="30px" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </telerik:GridTemplateColumn>
                                                 <telerik:GridButtonColumn UniqueName="Delete" HeaderText="" CommandName="Delete" ImageUrl="../images/Cancel.gif"
                                                    ButtonType="ImageButton" ConfirmText="您确定要删除么?"
                                                    Text="删除">
                                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="40px" />
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </telerik:GridButtonColumn>
                                            </Columns>
                                            <HeaderStyle BackColor="#E4E4E4" Height="22px" Font-Bold="true" />
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </div>
                            </telerik:RadPageView>
                            <telerik:RadPageView ID="RadPageView2" runat="server">

                                <telerik:RadGrid ID="RadGridSBJL" runat="server"
                                    GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                    SortingSettings-SortToolTip="单击进行排序" Width="98%" Skin="Default" EnableAjaxSkinRendering="true"
                                    EnableEmbeddedSkins="true" PagerStyle-AlwaysVisible="true">
                                    <ClientSettings EnableRowHoverStyle="true">
                                    </ClientSettings>
                                    <HeaderContextMenu EnableEmbeddedSkins="False">
                                    </HeaderContextMenu>
                                    <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                        <Columns>
                                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                                <HeaderStyle HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="AA" DataField="AA" HeaderText="流程">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="BB" DataField="BB" HeaderText="办理人">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="CC" DataField="CC" HeaderText="办理时间">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="DD" DataField="DD" HeaderText="办理结果">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                            <telerik:GridBoundColumn UniqueName="EE" DataField="EE" HeaderText="办理意见">
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </telerik:GridBoundColumn>
                                        </Columns>
                                        <HeaderStyle Font-Bold="True" />
                                        <PagerTemplate>
                                            <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                        </PagerTemplate>
                                    </MasterTableView>
                                    <FilterMenu EnableEmbeddedSkins="False">
                                    </FilterMenu>
                                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                                </telerik:RadGrid>
                            </telerik:RadPageView>
                        </telerik:RadMultiPage>
                    </div>

                </div>
            </div>
        </div>

    </form>
</body>
</html>
