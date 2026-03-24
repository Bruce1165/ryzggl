<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NumberChange.aspx.cs" Inherits="ZYRYJG.Register.NumberChange" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <style type="text/css">
        .floatL {
            float: left;
            padding: 4px 0 4px 4px;
            line-height: 26px;
        }

        .clearL {
            clear: left;
            padding-left: 12px;
        }

        .clearR {
            clear: right;
        }

        .hide {
            display: none;
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
                    当前位置 &gt;&gt;编号管理 &gt;&gt;<strong>更换作废</strong>
                </div>
            </div>
            <div class="content">
                <div class="floatL" style="width: 98%; text-align: left; padding-left: 8px">
                    <div class="floatL">
                        <telerik:RadComboBox ID="RadComboBoxPersonItem" runat="server" Width="100px">
                            <Items>
                                <telerik:RadComboBoxItem Text="姓名" Value="PSN_Name" />
                                <telerik:RadComboBoxItem Text="证件号码" Value="PSN_CertificateNO" />
                                <telerik:RadComboBoxItem Text="注册编号" Value="PSN_RegisterNO" />
                                 <telerik:RadComboBoxItem Text="证书编号" Value="PSN_RegisterCertificateNo" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>
                    <div class="floatL">
                        <telerik:RadTextBox ID="RadTextBoxPersonValue" runat="server" Width="150px"></telerik:RadTextBox>
                    </div>
                    <div class="floatL">
                        <telerik:RadComboBox ID="RadComboBoxUnitItem" runat="server" Width="100px">
                            <Items>
                                <telerik:RadComboBoxItem Text="企业名称" Value="ENT_Name" />
                                <telerik:RadComboBoxItem Text="组织机构代码" Value="ENT_OrganizationsCode" />
                            </Items>
                        </telerik:RadComboBox>
                    </div>
                    <div class="floatL">
                        <telerik:RadTextBox ID="RadTextBoxUnitValue" runat="server" Width="150px"></telerik:RadTextBox>
                    </div>
                    <div class="floatL">
                     <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                         </div>
                </div>

                <div class="floatL" style="width: 98%; text-align: left; padding-left: 8px">
                    <telerik:RadGrid ID="RadGridQY" runat="server"
                    GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                    SortingSettings-SortToolTip="单击进行排序" Width="98%" Skin="Default" EnableAjaxSkinRendering="true"
                    EnableEmbeddedSkins="true" PagerStyle-AlwaysVisible="true">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                        DataKeyNames="PSN_ServerID">
                        <Columns>

                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn UniqueName="PSN_Level" DataField="PSN_Level" HeaderText="等级">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>     
                            <telerik:GridBoundColumn UniqueName="PSN_CertificateNO" DataField="PSN_CertificateNO" HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>        
                           
                            <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PSN_RegisteProfession" DataField="PSN_RegisteProfession" HeaderText="注册专业">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PSN_RegisterNO" DataField="PSN_RegisterNO" HeaderText="注册编号">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn UniqueName="PSN_RegisterCertificateNo" DataField="PSN_RegisterCertificateNo" HeaderText="证书编号">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>                        
                              
                          
                            <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="操作">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("CertificateNoChange.aspx?o=<%# Eval("PSN_ServerID") %>")'>更换</span>
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
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.COC_TOW_Person_BaseInfMDL"
                    SelectMethod="GetList" TypeName="DataAccess.COC_TOW_Person_BaseInfoDAL"
                    SelectCountMethod="SelectCount" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                </div>
                 <uc2:IframeView ID="IframeView" runat="server" />
            </div>
        </div>
    </form>
</body>
</html>
