<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UnitMessage.aspx.cs" Inherits="ZYRYJG.County.UnitMessage" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />     
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
    <%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
    <style type="text/css">
        .link {
            border: none;
            color: blue;
            background-color: transparent;
            cursor: pointer;
            font-size: 12px;
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
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridZGGL" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridZGGL">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridZGGL" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        <img alt="" src="../Images/ts.gif" width="17" height="11" />当前位置 &gt;&gt;企业信息 &gt;&gt;<strong>信息查询</strong>
                    </div>
                </div>
                <div class="content">
                   <%-- <p class="jbxxbt">
                        资格校验
                    </p>--%>
                    <div style="width: 95%; height: 100%; margin: 10px auto; text-align: center;">
                        <table>
                            <tr>
                                <td style="width: 60%; text-align: left">查询条件：
                                    <telerik:RadComboBox ID="RadComboBoxItem" runat="server" Width="120">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="企业名称" Value="ENT_Name" />
                                        <telerik:RadComboBoxItem Text="组织机构代码" Value="ENT_OrganizationsCode" />
                                        <telerik:RadComboBoxItem Text="区县" Value="ENT_City" />
                                        <telerik:RadComboBoxItem Text="资质证书编号" Value="ENT_QualificationCertificateNo" />

                                    </Items>
                                    </telerik:RadComboBox>
&nbsp;<telerik:RadTextBox ID="RadTextBoxZJHM" runat="server" Width="150px" Text=""></telerik:RadTextBox>
                                    <asp:Button ID="ButtonQuery" runat="server" Text="查 询" CssClass="button" OnClick="ButtonQuery_Click" />
                                </td>
                                <td align="right" style="width: 34%;">
                                  
                                </td>
                                <td style="width: 6%;">
                                
                                </td>
                            </tr>
                        </table>
                        <div style="text-align: right" id="spanOutput" runat="server"></div>
                        <telerik:RadGrid ID="RadGridZGGL" runat="server"
                            GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                            SortingSettings-SortToolTip="单击进行排序" Width="98%" PagerStyle-AlwaysVisible="true" >
                            <ClientSettings EnableRowHoverStyle="true">
                            </ClientSettings>
                            <HeaderContextMenu EnableEmbeddedSkins="False">
                            </HeaderContextMenu>
                            <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                <Columns>
                                   <%-- <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </telerik:GridBoundColumn>--%>
                                    <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="160px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ENT_OrganizationsCode" DataField="ENT_OrganizationsCode" HeaderText="组织机构代码">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ENT_Economic_Nature" DataField="ENT_Economic_Nature" HeaderText="企业性质">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="ENT_City" DataField="ENT_City" HeaderText="区县">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="END_Addess" DataField="END_Addess" HeaderText="工商注册地址">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="70px" />
                                    </telerik:GridBoundColumn>
                                   <telerik:GridBoundColumn UniqueName="ENT_Corporate" DataField="ENT_Corporate" HeaderText="企业法人">
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ENT_Correspondence" HeaderText="企业通讯地址" UniqueName="ENT_Correspondence" Visible="false">
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle Width="60px" />
                                        
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ENT_Contact" HeaderText="联系人" UniqueName="ENT_Contact">
                                        <HeaderStyle Width="40px" />
                                        <ItemStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ENT_Telephone" HeaderText="联系电话" UniqueName="ENT_Telephone">
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ENT_MobilePhone" HeaderText="手机" UniqueName="ENT_MobilePhone">
                                        <HeaderStyle Width="60px" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ENT_Type" HeaderText="企业类型" UniqueName="ENT_Type"  Visible="false">
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="ENT_Sort" HeaderText="企业资质类别" UniqueName="ENT_Sort">
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn DataField="ENT_Grade" HeaderText="企业资质等级" UniqueName="ENT_Grade">
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle Width="80px" />
                                    </telerik:GridBoundColumn>

                                      <telerik:GridBoundColumn DataField="ENT_QualificationCertificateNo" HeaderText="企业资质证书编号" UniqueName="ENT_QualificationCertificateNo">
                                        <HeaderStyle Width="60px" />
                                        <ItemStyle Width="80px" />
                                    </telerik:GridBoundColumn>
                                </Columns>

<PagerStyle AlwaysVisible="True"></PagerStyle>

                                <HeaderStyle Font-Bold="True" />

<CommandItemStyle HorizontalAlign="Left"></CommandItemStyle>
                                <PagerTemplate>
                                    <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                </PagerTemplate>
                            </MasterTableView>

<PagerStyle AlwaysVisible="True"></PagerStyle>

                            <FilterMenu EnableEmbeddedSkins="False">
                            </FilterMenu>
                            <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                        </telerik:RadGrid>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.QualificationDAL"
                            SelectMethod="GetList2" TypeName="DataAccess.QualificationDAL"
                            SelectCountMethod="SelectCount2" EnablePaging="true"
                            MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                    DefaultValue="" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                   
                </div>
            </div>
        </div>
    </form>

</body>
</html>
