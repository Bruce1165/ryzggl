<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyChangePerson.aspx.cs" Inherits="ZYRYJG.Unit.ApplyChangePerson" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>

    <style type="text/css">
        .detailTable {
            width: 98%;
            height: 14px;
        }

        .infoHead {
            width: 25%;
            text-align: right;
            vertical-align: top;
            font-weight: bold;
            line-height: 150%;
        }

        .formItem_1 {
            width: 40%;
            text-align: left;
            vertical-align: top;
        }

            .formItem_1 input {
                border: none;
                line-height: 150%;
                width: 100%;
            }

            .formItem_1 td {
                width: 33%;
            }

        .formItem_2 {
            width: 35%;
            text-align: left;
            vertical-align: top;
        }

        .la {
            font-weight: bold;
        }

        .readonly {
            border: none !important;
        }
        /*///*/
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

        .link {
            border: none;
            color: blue;
            background-color: transparent;
            cursor: pointer;
            font-size: 12px;
        }

        .disable {
            border: none;
            color: #999;
            background-color: transparent;
            cursor: not-allowed;
            font-size: 12px;
        }

        .notopborder {
            border: 1px solid #999;
            border-top: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <div class="div_out">

            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置&gt;&gt;事项申报 &gt;&gt;变更注册 &gt;&gt;<strong>执业企业变更</strong>
                </div>
            </div>
            <div class="content">
                <p class="jbxxbt">
                    执业企业变更
                </p>
                <div id="div30DayTip"  style="width: 99%; margin: 8px auto; color:red;">过期前30天内只能办理注销，不受理其他注册业务，请注意证书有效期截止日期，提前进行申请！</div>
                <div id="div_applyDeleteTime" runat="server" visible="true"  style="width: 99%; margin: 8px auto; color:red;"><%= string.Format("请在提交网上申请后，联系聘用企业进行审核上报。{0}天内企业未在网上进行审核上报的，系统将自动删除此次业务申请。", Model.EnumManager.ApplyDeleteTime.时间间隔) %></div>
                <div style="width: 99%; margin: 10px auto; text-align: center;">

                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" MultiPageID="RadMultiPage1" SelectedIndex="1" Skin="Outlook">
                        <Tabs>
                            <%--南静注释  2019-10-29--%>
                            <%--<telerik:RadTab runat="server" Text="收到调出申请" PageViewID="RadPageView1">
                            </telerik:RadTab>--%>
                            <telerik:RadTab runat="server" Text="我的调入申请" PageViewID="RadPageView2" Selected="True">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                    <telerik:RadMultiPage ID="RadMultiPage1" runat="server" SelectedIndex="1">
                       <%-- 调出申请代码开始(项目合并后这段代码用不上)   南静备注   2019-10-29--%>
                        <telerik:RadPageView ID="RadPageView1" runat="server" BackColor="#cccccc" CssClass="notopborder">
                            <table style="width: 100%; background-color: #efefef;" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td style="width: 80px">
                                        <telerik:RadComboBox ID="RadComboBoxItem" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="姓名" Value="PSN_Name" />
                                                <telerik:RadComboBoxItem Text="证件号码" Value="PSN_CertificateNO" />
                                                <telerik:RadComboBoxItem Text="注册号" Value="PSN_RegisterNO" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="width: 120px">
                                        <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Skin="Default">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="ButtonQuery" runat="server" Text="查 询" CssClass="button" OnClick="ButtonQuery_Click" />
                                    </td>
                                </tr>
                            </table>
                            <telerik:RadGrid ID="RadGridQY" runat="server" BorderWidth="0"
                                GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                                SortingSettings-SortToolTip="单击进行排序" Width="100%"  Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true" OnItemCommand="RadGridQY_ItemCommand">
                                <ClientSettings EnableRowHoverStyle="true">
                                </ClientSettings>
                                <HeaderContextMenu EnableEmbeddedSkins="False">
                                </HeaderContextMenu>
                                <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                                    DataKeyNames="ApplyID">
                                    <Columns>
                                        <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="cjsj" DataField="cjsj" HeaderText="申请日期" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}" AllowSorting="false">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_Sex" DataField="PSN_Sex" HeaderText="性别">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_CertificateNO" DataField="PSN_CertificateNO" HeaderText="证件号码">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="PSN_RegisterNO" HeaderText="注册号">
                                            <ItemTemplate>
                                                <span class="link_edit" onclick='javascript:SetIfrmSrc("EJZJSDetail.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("PSN_ServerID").ToString()) %>");'><%# Eval("PSN_RegisterNO") %></span>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Left" Wrap="false" ForeColor="Blue" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="ENT_City" DataField="ENT_City" HeaderText="所属区县">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="OldUnitCheckRemark" DataField="OldUnitCheckRemark" HeaderText="确认结果">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <%-- 新加调出申请里的状态列 --%>
                                        <telerik:GridBoundColumn UniqueName="ApplyStatus" DataField="ApplyStatus" HeaderText="状态">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>

                                        <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="操作">
                                            <ItemTemplate>
                                                <asp:Button ID="Button3" runat="server" Text="同意" CssClass="link" CommandName="report" OnClientClick="javascript:if(confirm('您确认要同意吗？')==false) return false;" Visible='<%# Eval("OldUnitCheckTime")== DBNull.Value%>' />
                                                &nbsp;
                                            <asp:Button ID="Button5" runat="server" Text="拒绝" CssClass="link" CommandName="Cancelreport" OnClientClick="javascript:if(confirm('您确认要拒绝吗？')==false) return false;" Visible='<%# Eval("OldUnitCheckTime")== DBNull.Value%>' />
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridTemplateColumn>
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
                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.ApplyMDL"
                                SelectMethod="GetApplyChangeList" TypeName="DataAccess.ApplyDAL"
                                SelectCountMethod="SelectApplyChangeCount" EnablePaging="true"
                                MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                        DefaultValue="" ConvertEmptyStringToNull="false" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </telerik:RadPageView>
                        <%-- 调出申请代码结束(项目合并后这段代码用不上)   南静备注   2019-10-29--%>
                        <telerik:RadPageView ID="RadPageView2" runat="server" BackColor="#cccccc" CssClass="notopborder">
                            <table style="width: 100%; background-color: #efefef;" cellpadding="4" cellspacing="4">
                                <tr>
                                    <td style="width: 80px">
                                        <telerik:RadComboBox ID="RadComboBoxQYDR" runat="server">
                                            <Items>
                                                <telerik:RadComboBoxItem Text="姓名" Value="PSN_Name" />
                                                <telerik:RadComboBoxItem Text="证件号码" Value="PSN_CertificateNO" />
                                                <%--    <telerik:RadComboBoxItem Text="注册号" Value="PSN_RegisterNO" />--%>
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td style="width: 120px">
                                        <telerik:RadTextBox ID="RadTextBoxQYDR" runat="server" Skin="Default">
                                        </telerik:RadTextBox>
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="ButtonQYDR" runat="server" Text="查 询" CssClass="button" OnClick="ButtonQYDR_Click" />
                                    </td>
                                </tr>
                            </table>
                            <telerik:RadGrid ID="RadGridQYDR" runat="server" BorderWidth="0"
                                GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                                SortingSettings-SortToolTip="单击进行排序" Width="100%"  Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
                                <ClientSettings EnableRowHoverStyle="true">
                                </ClientSettings>
                                <HeaderContextMenu EnableEmbeddedSkins="False">
                                </HeaderContextMenu>
                                <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="Top" CommandItemStyle-HorizontalAlign="Left"
                                    DataKeyNames="ApplyID">
                                    <CommandItemTemplate>
                                        <span style="cursor: pointer; color: blue; line-height: 26px; padding-left: 40px; background: url(../images/jiah.gif) no-repeat 20px center;" onclick='javascript:SetIfrmSrc("ApplyChangePersonnel.aspx");'>添加调入申请</span>
                                    </CommandItemTemplate>
                                    <Columns>
                                        <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn UniqueName="cjsj" DataField="cjsj" HeaderText="创建日期" HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}" AllowSorting="false">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_Sex" DataField="PSN_Sex" HeaderText="性别">
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_CertificateNO" DataField="PSN_CertificateNO" HeaderText="身份证号">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="PSN_RegisterNO" DataField="PSN_RegisterNO" HeaderText="注册编号">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="OldENT_Name" DataField="OldENT_Name" HeaderText="原企业名称">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn UniqueName="ApplyStatus" HeaderText="原单位意见">
                                            <ItemTemplate>
                                                <%# Eval("OldUnitCheckRemark")==DBNull.Value?"待确认":Eval("OldUnitCheckRemark") %>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridBoundColumn UniqueName="ApplyStatus" DataField="ApplyStatus" HeaderText="状态">
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>

                                        <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="操作">
                                            <ItemTemplate>
                                                <input id="Button1" runat="server" type="button" value="详细" class="link" onclick='<%# string.Format("javascript:SetIfrmSrc(\"ApplyChangePersonnel.aspx?a={0}\");return false;",Utility.Cryptography.Encrypt(Eval("ApplyID").ToString()))%>' />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                        </telerik:GridTemplateColumn>
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
                            <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" DataObjectTypeName="Model.ApplyMDL"
                                SelectMethod="GetApplyChangeList" TypeName="DataAccess.ApplyDAL"
                                SelectCountMethod="SelectApplyChangeCount" EnablePaging="true"
                                MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                                <SelectParameters>
                                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                        DefaultValue="" ConvertEmptyStringToNull="false" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </telerik:RadPageView>
                    </telerik:RadMultiPage>
                </div>
            </div>

        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
