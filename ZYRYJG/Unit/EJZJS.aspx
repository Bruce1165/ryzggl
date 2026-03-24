<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EJZJS.aspx.cs" Inherits="ZYRYJG.Unit.EJZJS" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
        <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>
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
                    当前位置 &gt;&gt;信息查看 &gt;&gt;<strong>二级建造师</strong>
                </div>
            </div>
            <div class="content">
                 <div class="DivContent" id="Td3">                  
                    业务说明：个人电子证书仅可由持证人本人下载使用。
                </div>
                <table class="cx" width="100%" border="0" align="center" cellspacing="1">
                    <tr id="TrPerson" runat="server">
                        <td width="100px" align="right" nowrap="nowrap">
                            <telerik:RadComboBox ID="RadComboBoxIten" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="姓名" Value="PSN_Name" />
                                    <telerik:RadComboBoxItem Text="证件号码" Value="PSN_CertificateNO" />
                                    <telerik:RadComboBoxItem Text="单位名称" Value="ENT_Name" />
                                    <telerik:RadComboBoxItem Text="机构代码" Value="ENT_OrganizationsCode" />
                                    <telerik:RadComboBoxItem Text="注册号" Value="PSN_RegisterNO" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="left" width="300px">
                            <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>

                        </td>
                        <td width="80px" align="right" nowrap="nowrap">注销状态：
                        </td>
                        <td align="left" width="80px">
                            <telerik:RadComboBox ID="RadComboBoxPSN_RegisteType" runat="server" Width="80px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" />
                                    <telerik:RadComboBoxItem Text="未注销" Value="未注销" Selected="true" />
                                    <telerik:RadComboBoxItem Text="已注销" Value="已注销" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                          <td width="90px" align="right" nowrap="nowrap">锁定状态：
                        </td>
                        <td align="left" width="80px">
                            <telerik:RadComboBox ID="RadComboBoxLockStatus" runat="server" Width="80px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" Selected="true"  />
                                    <telerik:RadComboBoxItem Text="锁定" Value="锁定" />
                                    <telerik:RadComboBoxItem Text="未锁定" Value="未锁定" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="left" style="padding-left: 40px">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                             &nbsp;
                            <asp:Button ID="ButtonExportToExcel" CssClass="bt_large" Text="导出查询结果" OnClick="ButtonExportToExcel_Click"
                                runat="server"></asp:Button>
                        </td>
                    </tr>
                </table>
                <div style="width: 98%; margin: 8px auto;">
                    <telerik:RadGrid ID="RadGridQY" runat="server"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                            DataKeyNames="PSN_ServerID">
                            <Columns>

                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="PSN_CertificateNO" HeaderText="证件号码">
                                    <ItemTemplate>
                                        <%# Eval("PSN_CertificateNO") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_RegisterCertificateNo" DataField="PSN_RegisterCertificateNo" HeaderText="证书编号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_RegisterNO" DataField="PSN_RegisterNO" HeaderText="注册号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ProfessionWithValid" DataField="ProfessionWithValid" HeaderText="注册专业及有效期">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>

                                <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="详细">
                                    <ItemTemplate>
                                        <span class="link_edit" onclick=' javascript:SetIfrmSrc("EJZJSDetail.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("PSN_ServerID").ToString()) %>"); '>详细</span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="EleCert" HeaderText="电子证书" >
                                <ItemTemplate>
                                    <%# IfExistRoleID("2") == true?"<apan style='color:#333'>持证人本人下载</apan>": (Convert.ToDateTime(Eval("PSN_CertificateValidity")) < DateTime.Now || Eval("PSN_RegisteType").ToString()=="07")?"":string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/CertificatePdf.aspx?c={0}&t=0\");'><nobr>电子证书</nobr></span>", Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("PSN_ServerID").ToString())))%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" ForeColor="Blue" />
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
                </div>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.COC_TOW_Person_BaseInfoMDL"
                    SelectMethod="GetListWithProfession" TypeName="DataAccess.COC_TOW_Person_BaseInfoDAL"
                    SelectCountMethod="SelectCountWithProfession" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
        <div id="winpop">
        </div>
    </form>
</body>
</html>
