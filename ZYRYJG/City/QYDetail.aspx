<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QYDetail.aspx.cs" Inherits="ZYRYJG.QYDetai" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script type="text/javascript" src="../Scripts/echarts-all.js"></script>
    <style type="text/css">
        .l {
            text-align: left;
            line-height: 24px;
            padding-left: 2px;
            width: 35%;
        }

        .r {
            text-align: right;
            line-height: 24px;
            font-weight: bold;
            width: 15%;
            color: #444;
        }
    </style>
</head>
<body style="padding-bottom: 20px">
    <form id="form1" runat="server">

        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQY" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridQY">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQY" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;人员全执业生命周期 &gt;&gt;<strong>企业监控</strong>
                </div>
            </div>
            <div class="content">
                <p class="jbxxbt">
                    企业详细
                </p>
                <div style="width: 100%; height: 100%; margin: 10px auto; text-align: center; border-collapse: collapse;">
                    <div style="border: 1px solid #999; width: 100%; border-collapse: collapse;">
                        <div style="text-align: left; background-color: #5C9DD3; font-weight: bold; color: white; width: 100%; clear: both; line-height: 40px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;企业基本信息 
                        </div>
                        <table width="100%" style="margin: 12px 0px 12px 0px">
                            <tr>
                                <td class="r">企业名称：</td>
                                <td class="l" colspan="3">
                                    <asp:Label ID="LabelQYMC" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="r">机构代码：</td>
                                <td class="l">
                                    <asp:Label ID="LabelZZJGDM" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="r">注册资本金：</td>
                                <td class="l">
                                    <asp:Label ID="LabelZCZJ" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="LabelZCBZ" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="r">所在区县：</td>
                                <td class="l">
                                    <asp:Label ID="LabelRegionName" runat="server" Text=""></asp:Label>

                                </td>
                                <td class="r">注册地址：</td>
                                <td class="l">
                                    <asp:Label ID="LabelZCDZ" runat="server" Text=""></asp:Label>
                                </td>

                            </tr>
                            <tr>
                                <td class="r">法定代表人：</td>
                                <td class="l">
                                    <asp:Label ID="LabelFDDBR" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="r">联系电话：</td>
                                <td class="l">
                                    <asp:Label ID="LabelLXDH" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>

                        </table>
                    </div>
                    <div style="border: 1px solid #999; width: 100%; border-collapse: collapse;">
                        <div style="text-align: left; background-color: #5C9DD3; font-weight: bold; color: white; width: 100%; clear: both; line-height: 40px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;资质达标情况 
                        </div>

                        <telerik:RadGrid ID="RadGridQY" runat="server" BorderWidth="0"
                            GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                            Width="100%" Skin="Default" EnableAjaxSkinRendering="true"
                            EnableEmbeddedSkins="true">
                            <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true">
                            </ClientSettings>
                            <HeaderContextMenu EnableEmbeddedSkins="False">
                            </HeaderContextMenu>
                            <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="ZZ" HeaderText="资质类型">
                                        <ItemTemplate>
                                            <%# Eval("ZZLB") %><%# Eval("ZZXL") %><%# Eval("ZZDJ") %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="20%" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="ZZ" HeaderText="资质标准要求">
                                        <ItemTemplate>
                                            <div style="width: 100%; line-height: 30px;">
                                                <%# Eval("BZ") %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="40%" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridTemplateColumn UniqueName="ZZ" HeaderText="实际情况">
                                        <ItemTemplate>

                                            <div style="width: 100%; line-height: 30px;">
                                                <%# Eval("Detail") %>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="40%" />
                                    </telerik:GridTemplateColumn>

                                </Columns>
                                <HeaderStyle Font-Bold="True" Wrap="false" />
                            </MasterTableView>
                            <HeaderStyle Wrap="false" />
                        </telerik:RadGrid>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
