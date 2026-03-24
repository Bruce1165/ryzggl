<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyQueryTZ.aspx.cs" Inherits="ZYRYJG.CertifManage.ApplyQueryTZ" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <%--<meta http-equiv="X-UA-Compatible" content="IE=7">--%>
    <meta name="renderer" content="webkit|ie-comp|ie-stand" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.11" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.009" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style type="text/css">
        .end {
            display: block;
            margin: 0;
            background: url(../images/yx.png) no-repeat right center;
            padding-right: 18px;
        }

        .ing {
            padding-right: 18px;
            display: block;
            margin: 0;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                function onRequestStart(sender, args) {
                    if (args.get_eventTarget().indexOf("spanOutput") >= 0) {
                        args.set_enableAjax(false);
                    }
                }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true">
            <ClientEvents OnRequestStart="onRequestStart" />
            <AjaxSettings>             
                <telerik:AjaxSetting AjaxControlID="divmain">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divmain" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;从业人员证书管理 &gt;&gt;建筑施工特种作业&gt;&gt;<strong>申办业务查询</strong>
                </div>
            </div>
            <div class="content">
                <div id="divmain" runat="server" style="width: 99%; margin: 2px auto; text-align: center;">
                    <table class="bar_cx" width="98%" border="0" align="center" cellspacing="1" style="padding-bottom: 8px">
                        <tr id="TrPerson" runat="server">
                            <td width="90px" align="right" nowrap="nowrap">
                                <telerik:RadComboBox ID="RadComboBoxTypeItem" runat="server" Width="100%">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="姓名" Value="WORKERNAME" />
                                        <telerik:RadComboBoxItem Text="证件号码" Value="WORKERCERTIFICATECODE" />
                                        <telerik:RadComboBoxItem Text="企业名称" Value="UNITNAME" />
                                        <telerik:RadComboBoxItem Text="证书编号" Value="CERTIFICATECODE" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td align="left" width="200px">
                                <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="97%" Skin="Default">
                                </telerik:RadTextBox>
                            </td>
                            <td align="left" width="400px">
                                <div class="RadPicker md">
                                    &nbsp;
                                    <telerik:RadComboBox ID="RadComboBoxRQItem" runat="server" Width="80">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="申报日期" Value="APPLYDATE" />
                                            <telerik:RadComboBoxItem Text="审核日期" Value="progressDate" />

                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <telerik:RadDatePicker ID="RadDatePickerGetDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="130px" />
                                <div class="RadPicker md">至</div>
                                <telerik:RadDatePicker ID="RadDatePickerGetDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="130px" />
                            </td>
                            <td align="left">&nbsp;申报事项：
                                 <telerik:RadComboBox ID="RadComboBoxPSN_RegisteType" runat="server" Width="100" AutoPostBack="true" OnSelectedIndexChanged="RadComboBoxPSN_RegisteType_SelectedIndexChanged">
                                     <Items>
                                         <telerik:RadComboBoxItem Text="全部" Value="" />
                                         <telerik:RadComboBoxItem Text="京内变更" Value="京内变更" />
                                         <telerik:RadComboBoxItem Text="注销" Value="注销" />
                                         <telerik:RadComboBoxItem Text="证书续期" Value="证书续期" />
                                     </Items>
                                 </telerik:RadComboBox>
                                &nbsp;&nbsp;申报进度：
                                 <telerik:RadComboBox ID="RadComboBoxApplyStatus" runat="server" Width="120">
                                     <Items>
                                         <telerik:RadComboBoxItem Text="全部" Value="" />
                                         <telerik:RadComboBoxItem Text="填报中" Value="填报中" />
                                         <telerik:RadComboBoxItem Text="待单位确认" Value="待单位确认" />
                                         <telerik:RadComboBoxItem Text="已申请" Value="已申请" />
                                         <telerik:RadComboBoxItem Text="退回修改" Value="退回修改" />
                                         <telerik:RadComboBoxItem Text="已初审" Value="已初审" />
                                         <telerik:RadComboBoxItem Text="已受理" Value="已受理" />
                                         <telerik:RadComboBoxItem Text="已审核" Value="已审核" />
                                         <telerik:RadComboBoxItem Text="已决定" Value="已决定" />
                                     </Items>
                                 </telerik:RadComboBox>
                                &nbsp;&nbsp;
                                  <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                            </td>
                            <td></td>
                        </tr>
                    </table>
                    <div style="width: 98%; margin: 8px auto;">
                        <div style="width: 98%; line-height: 20px; height: 20px; vertical-align: middle; padding: 0; margin: 4px 0;text-align:right">
                            <div >图例说明：<img alt="" src="../images/yx.png" /> 表示业务已办结</div>                          
                        </div>
                        <telerik:RadGrid ID="RadGridProcess" runat="server" GridLines="None"
                            AllowPaging="true" AllowSorting="false" AutoGenerateColumns="False" AllowCustomPaging="true"
                            SortingSettings-SortToolTip="单击进行排序" Width="100%" OnPageIndexChanged="RadGridProcess_PageIndexChanged"
                            Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false">
                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <MasterTableView DataKeyNames="DataID" NoMasterRecordsText="　没有可显示的记录">
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="WORKERNAME" DataField="WORKERNAME" HeaderText="姓名">
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="WORKERCERTIFICATECODE" DataField="WORKERCERTIFICATECODE" HeaderText="证件号码">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="UNITNAME" DataField="UNITNAME" HeaderText="单位名称">
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种">
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridTemplateColumn UniqueName="CERTIFICATECODE" HeaderText="证书编号">
                                        <ItemTemplate>
                                             <span class="link_edit" onclick='javascript:SetIfrmSrc("<%# GetApplyTypeUrl(Eval("ItemType").ToString(),Eval("DataID").ToString(),Eval("CERTIFICATEID").ToString(),Eval("CERTIFICATEID").ToString()) %>")'><%# Eval("CERTIFICATECODE")%></span>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn UniqueName="ApplyDate" DataField="ApplyDate" HeaderText="申请日期"
                                        HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ItemType" DataField="ItemType" HeaderText="申报事项">
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="progressDate" DataField="progressDate" HeaderText="审批日期"
                                        HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>                                   
                                    <telerik:GridTemplateColumn UniqueName="ApplyStatus" HeaderText="审批进度">
                                        <ItemTemplate>
                                            <div class='<%# Convert.ToInt32(Eval("IfEnd"))==1?"end":"ing"%>'><%# Eval("ApplyStatus")%></div>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn UniqueName="progressResult" DataField="progressResult" HeaderText="审批结果">
                                        <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <PagerTemplate>
                                    <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                </PagerTemplate>
                            </MasterTableView>
                            <ClientSettings>
                            </ClientSettings>
                            <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                        </telerik:RadGrid>
                    </div>
                </div>
                <%--<div style="width: 98%; text-align: center; margin: 8px 0; text-align: right; padding-right: 40px">
                    <span id="spanOutput" runat="server" class="excel" style="padding-right: 40px; font-weight: bold"></span>
                    <asp:Button ID="ButtonOutput" runat="server" Text="导出查询结果" CssClass="bt_large" OnClick="ButtonOutput_Click" />
                </div>--%>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
