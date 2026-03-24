<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RandomCheckBase.aspx.cs" Inherits="ZYRYJG.CheckMgr.RandomCheckBase" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <style>
        .divLeft {
            width: 15%;
            float: left;
            margin-left: 1%;
            overflow: auto;
            border: 1px solid #e1e1e1;
            margin-bottom: 40px;
        }

            .divLeft option {
                padding: 4px 0;
            }

        .divRight {
            width: 80%;
            float: left;
            clear: right;
            margin-left: 1%;
            overflow: auto;
            border: none;
            margin-bottom: 40px;
        }

        .divTop {
            width: 97%;
            margin: 0 auto 16px auto;
            clear: both;
            line-height: 150%;
            padding:12px 16px;
            background-color:#efefef;
        }

        .grid {
            width: 99%;
            margin: 0 auto;
            clear: both;
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
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;综合监管 &gt;&gt;双随机检查&gt;&gt;<strong>检查对象人员筛查</strong>
                </div>
            </div>
             <div class="content" id="divContent" runat="server">
                <div class="DivTitleOn" onclick="DivOnOff(this,'Td3',event);" title="折叠">
                    统计说明
                </div>  
                <table class="cx" width="100%" border="0" align="center" cellspacing="1">
                    <tr>
                        <td align="left" nowrap="nowrap">
                           筛查年度：
                             <telerik:RadNumericTextBox ID="RadNumericTextBoxSourceYear" runat="server" MaxLength="4"
                                     Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" Width="80px"
                                     MinValue="2026" MaxValue="2050">
                                     <NumberFormat DecimalDigits="0"></NumberFormat>
                                 </telerik:RadNumericTextBox> &nbsp;&nbsp;
                      人员类型：                       
                            <telerik:RadComboBox ID="RadComboBoxPersonType" runat="server" Width="150px" OnSelectedIndexChanged="RadComboBoxPersonType_SelectedIndexChanged" AutoPostBack="true">
                                <Items>
                                      <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                    <telerik:RadComboBoxItem Text="二级注册建造师" Value="二级注册建造师" />
                                     <telerik:RadComboBoxItem Text="二级造价工程师" Value="二级造价工程师" />
                                    <telerik:RadComboBoxItem Text="安全生产考核三类人员" Value="安全生产考核三类人员" />
                                    <telerik:RadComboBoxItem Text="建筑施工特种作业" Value="建筑施工特种作业" />
                                </Items>
                            </telerik:RadComboBox>
                            &nbsp;&nbsp;
                            申报事项：
                            <telerik:RadComboBox ID="RadComboBoxPSN_RegisteType" runat="server" Width="80px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                    <telerik:RadComboBoxItem Text="初始注册" Value="初始注册" />
                                    <telerik:RadComboBoxItem Text="重新注册" Value="重新注册" />
                                    <telerik:RadComboBoxItem Text="变更注册" Value="变更注册" />
                                    <telerik:RadComboBoxItem Text="增项注册" Value="增项注册" />
                                    <telerik:RadComboBoxItem Text="延续注册" Value="延期注册" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadComboBox ID="RadComboBox_CongYeApplyType" runat="server" Width="80px" Visible="false">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                    <telerik:RadComboBoxItem Text="考试发证" Value="考试发证" />
                                    <telerik:RadComboBoxItem Text="企业变更" Value="企业变更" />
                                    <telerik:RadComboBoxItem Text="证书续期" Value="证书续期" />
                                    <telerik:RadComboBoxItem Text="证书进京" Value="证书进京" />
                                </Items>
                            </telerik:RadComboBox>
                             <telerik:RadComboBox ID="RadComboBoxEJZJGCS" runat="server" Width="80px" Visible="false">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                    <telerik:RadComboBoxItem Text="初始注册" Value="初始注册" />
                                    <telerik:RadComboBoxItem Text="变更注册" Value="变更注册" />
                                    <telerik:RadComboBoxItem Text="延续注册" Value="延续注册" />
                                </Items>
                            </telerik:RadComboBox>
                       
                            &nbsp; &nbsp;
                              <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="bt_large" OnClick="ButtonSearch_Click" />
                            &nbsp;&nbsp;
                            <asp:Button ID="ButtonExportToExcel" CssClass="bt_large" Text="导出查询结果" OnClick="ButtonExportToExcel_Click"
                                runat="server"></asp:Button>
                              &nbsp;&nbsp;                        
                             <input id="ButtonNew"  runat="server" type="button" value="新建筛查" class="bt_large" onclick='javascript: SetIfrmSrc("RandomCheckAdd.aspx"); ' />
                        </td>
                    </tr>

                </table>
                 <div class="table_cx">
                        <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                        筛查人员历史记录
                    </div>
                <div style="width: 98%; margin: 8px auto;">
                    <telerik:RadGrid ID="RadGridQY" runat="server"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="人员类型">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridBoundColumn UniqueName="CheckYear" DataField="CheckYear" HeaderText="抽查年度">
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
                                <telerik:GridBoundColumn UniqueName="PSN_RegisterNO" DataField="PSN_RegisterNO" HeaderText="证书编号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ProfessionWithValid" DataField="ProfessionWithValid" HeaderText="专业及有效期">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn UniqueName="ApplyType" DataField="ApplyType" HeaderText="申报事项">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ApplyTime" DataField="ApplyTime" HeaderText="申报日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NoticeDate" DataField="NoticeDate" HeaderText="办结日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
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
                </div>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.CheckObjectMDL"
                    SelectMethod="GetList" TypeName="DataAccess.CheckObjectDAL"
                    SelectCountMethod="SelectCount" EnablePaging="true"
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
