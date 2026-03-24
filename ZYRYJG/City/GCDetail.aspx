<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GCDetail.aspx.cs" Inherits="ZYRYJG.GCDetail" %>

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
                    当前位置 &gt;&gt;人员全执业生命周期 &gt;&gt;<strong>项目监控</strong>
                </div>
            </div>
            <div class="content">
                <p class="jbxxbt">
                    工程详细
                </p>
                <div style="width: 100%; height: 100%; margin: 10px auto; text-align: center; border-collapse: collapse;">
                    <div style="border: 1px solid #999; width: 100%; border-collapse: collapse;">
                        <div style="text-align: left; background-color: #5C9DD3; font-weight: bold; color: white; width: 100%; clear: both; line-height: 40px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;工程基本信息 
                        </div>
                        <table width="100%" style="margin:12px 0px 12px 0px">
                            <tr>
                                <td class="r">工程名称：</td>
                                <td class="l" colspan="3">
                                    <asp:Label ID="LabelGCMC" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="r">工程编码：</td>
                                <td class="l">
                                    <asp:Label ID="LabelGCBM" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="r">工程规模(平方米)：</td>
                                <td class="l">
                                    <asp:Label ID="LabelGCGM" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="r">工程性质：</td>
                                <td class="l">
                                    <asp:Label ID="LabelGCXZ" runat="server" Text=""></asp:Label>

                                </td>
                                <td class="r">工程类别：</td>
                                <td class="l">
                                    <asp:Label ID="LabelGCLB" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="r">结构类型：</td>
                                <td class="l">
                                    <asp:Label ID="LabelJGLX" runat="server" Text=""></asp:Label>

                                </td>
                                <td class="r">建设工程规划许可证号：</td>
                                <td class="l">
                                    <asp:Label ID="LabelGHXKZH" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="r">工程建设地点：</td>
                                <td class="l" colspan="3">
                                    <asp:Label ID="LabelGCJSDD" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="r">合同编号：</td>
                                <td class="l">
                                    <asp:Label ID="LabelHTBH" runat="server" Text=""></asp:Label>
                                </td>
                                <td class="r">合同价格(万元)：</td>
                                <td class="l">
                                    <asp:Label ID="LabelHTJG" runat="server" Text=""></asp:Label>

                                </td>
                            </tr>
                            <tr>
                                <td class="r">建设单位：</td>
                                <td class="l">
                                    <asp:Label ID="LabelJSDWMC" runat="server" Text=""></asp:Label>

                                </td>
                                <td class="r">所在区县：</td>
                                <td class="l">
                                    <asp:Label ID="LabelGCSZQX" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="r">单位法定代表人：</td>
                                <td class="l">
                                    <asp:Label ID="LabelJSDWFDDBR" runat="server" Text=""></asp:Label>

                                </td>
                                <td class="r">法定代表人电话：</td>
                                <td class="l">
                                    <asp:Label ID="LabelJSDWFDDBRDH" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td class="r">单位项目负责人：</td>
                                <td class="l">
                                    <asp:Label ID="LabelJSDWXMFZR" runat="server" Text=""></asp:Label>

                                </td>
                                <td class="r">项目负责人电话：</td>
                                <td class="l">
                                    <asp:Label ID="LabelJSDWXMFZRDH" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="border: 1px solid #999; width: 100%; border-collapse: collapse;">
                        <div style="text-align: left; background-color: #5C9DD3; font-weight: bold; color: white; width: 100%; clear: both; line-height: 40px;">
                            &nbsp;&nbsp;&nbsp;&nbsp;项目经理信息 
                        </div>
                         <telerik:RadGrid ID="RadGridQY" runat="server" BorderWidth="0"
                        GridLines="None" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                         Width="100%" Skin="Default" EnableAjaxSkinRendering="true"
                        EnableEmbeddedSkins="true"                        >
                        <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"                           >
                            <Columns>
                               
                                <telerik:GridBoundColumn UniqueName="step" DataField="step" HeaderText="工程阶段">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                  <telerik:GridBoundColumn UniqueName="xm" DataField="xm" HeaderText="项目经理">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="recordTime" DataField="recordTime" HeaderText="备案/变更时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                  <telerik:GridBoundColumn UniqueName="remark" DataField="remark" HeaderText="备注">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                               
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
