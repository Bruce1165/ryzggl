<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PeopleList.aspx.cs" Inherits="ZYRYJG.Unit.PeopleList" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
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
            <div  id="div_top" class="dqts">
                <div style="float: left; ">
                    当前位置 &gt;&gt;企业信息 &gt;&gt;<strong>人员信息</strong>
                </div>
            </div>
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
            <div class="content">
                  <table  class="cx" width="100%" border="0" align="center" cellspacing="1">
                    <tr id="TrPerson" runat="server">
                        <td width="12%" align="right" nowrap="nowrap">
                            <telerik:RadComboBox ID="RadComboBoxIten" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="姓名" Value="PSN_Name" />
                                    <telerik:RadComboBoxItem Text="证件号码" Value="PSN_CertificateNO" />
                                    <telerik:RadComboBoxItem Text="企业名称" Value="ENT_Name" />
                                    <telerik:RadComboBoxItem Text="证书编号" Value="PSN_RegisterCertificateNo" />
                               <%--     <telerik:RadComboBoxItem Text="注册号" Value="PSN_RegisterNO" />--%>
                                    <telerik:RadComboBoxItem Text="类型" Value="LX" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="left" width="20%">
                            <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="97%" Skin="Default">
                            </telerik:RadTextBox>

                        </td>                       
                        <td width="60px" align="right" nowrap="nowrap">
                            有效期：
                        </td>
                        <td align="left" width="150px">
                             <telerik:RadDatePicker ID="RadDatePickerGetDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="130px" />
                        </td>

                         <td width="20px" align="right" nowrap="nowrap">
                            至
                        </td>
                         <td align="left" width="150px">
                             <telerik:RadDatePicker ID="RadDatePickerGetDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="130px" />
                        </td>
                        <td align="left" style="padding-left:40px">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                           <%--  &nbsp 
                            <asp:Button ID="ButtonReturn" runat="server" Text="返 回" CssClass="button" OnClick="ButtonReturn_Click" />--%>
                        </td>
                    </tr>
                </table>
                <telerik:RadGrid ID="RadGridQY" runat="server"
                    GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%" PagerStyle-AlwaysVisible="true">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                        DataKeyNames="Fid">
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
                        <%--     <telerik:GridBoundColumn UniqueName="PSN_RegisterNO" DataField="PSN_RegisterNO" HeaderText="注册号">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>--%>
                            <telerik:GridBoundColumn UniqueName="PSN_RegisteProfession" DataField="PSN_RegisteProfession" HeaderText="注册专业">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>     
                            <telerik:GridBoundColumn UniqueName="PSN_CertificateValidity" DataField="PSN_CertificateValidity" HeaderText="有效期" DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>  
                             <telerik:GridBoundColumn UniqueName="LX" DataField="LX" HeaderText="类型">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>                     
                           
                            <telerik:GridTemplateColumn UniqueName="Edit">
                                <ItemTemplate>
                                    <%--<span class="link_edit" onclick=' javascript:SetIfrmSrc("EJZJSDetail.aspx?p=<%# Eval("Fid")%>"); '>详细</span>--%>
                                    <span class="link_edit" onclick='<%# Eval("LX").ToString()=="一级建造师"?string.Format("javascript:SetIfrmSrc(\"YJZJSDetail.aspx?o={0}\");",Utility.Cryptography.Encrypt(Eval("Fid").ToString())):Eval("LX").ToString()=="一级临时建造师"?string.Format("javascript:SetIfrmSrc(\"YJZJS_LSDetail.aspx?o={0}\");",Utility.Cryptography.Encrypt(Eval("Fid").ToString())):Eval("LX").ToString()=="二级建造师"?string.Format("javascript:SetIfrmSrc(\"EJZJSDetail.aspx?o={0}\");",Utility.Cryptography.Encrypt(Eval("Fid").ToString())):Eval("LX").ToString()=="二级临时建造师"?string.Format("javascript:SetIfrmSrc(\"EJZJSDetail.aspx.aspx?o={0}\");",Utility.Cryptography.Encrypt(Eval("Fid").ToString()) ):Eval("LX").ToString()=="监理师"?string.Format("javascript:SetIfrmSrc(\"EJZJSDetail.aspx?o={0}\");",Utility.Cryptography.Encrypt(Eval("Fid").ToString())):Eval("LX").ToString()=="造价师"?string.Format("javascript:SetIfrmSrc(\"ZCZJSDetail.aspx?o={0}\");",Utility.Cryptography.Encrypt(Eval("Fid").ToString())):""%>'/>详细</span>
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

                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.COC_TOW_Person_BaseInfoMDL"
                    SelectMethod="GetPeoPleList" TypeName="DataAccess.COC_TOW_Person_BaseInfoDAL"
                    SelectCountMethod="SelectCountPeoPleList" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                
                            
                <div  style="text-align:center;padding-top:10px"><label runat="server" id="Unit_Name"></label><label runat="server" id="PeopleCount" style="color:red"></label>&nbsp&nbsp&nbsp</div>
            </div>
             </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
