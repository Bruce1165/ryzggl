<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApplyCheckDetail.aspx.cs" Inherits="ZYRYJG.CheckMgr.ApplyCheckDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<!DOCTYPE html>

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
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;综合查询 &gt;&gt;业务抽查&gt;&gt;<strong>业务抽查详细列表</strong>
                </div>
            </div>
            <div class="content">
                <table class="cx" width="98%" border="0" align="center" cellspacing="5">
                     <tr>
                         <td style="text-align: right;width:200px">抽查创建日期：
                        </td>
                         <td  style="text-align:left" colspan="2">
                             <asp:Label ID="Labelcjsj" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr>
                         <td style="text-align: right;width:200px">抽查业务范围：
                        </td>
                         <td  style="text-align:left" colspan="2">
                            <asp:CheckBoxList ID="CheckBoxListBusRange" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="CheckBoxListBusRange_SelectedIndexChanged">
                                <asp:ListItem Text="二建注册建造师" Value="1" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="二级注册造价工程师" Value="2" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="安全生产管理人员" Value="3" Selected="true"></asp:ListItem>
                                <asp:ListItem Text="特种作业人员" Value="4" Selected="true"></asp:ListItem>
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                     <tr>
                         <td style="text-align: right;width:200px">业务办结日期：
                        </td>
                         <td  style="text-align:left" colspan="2">
                             <asp:Label ID="LabelTimeSpan" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    
                    <tr >
                        <td style="text-align: right;width:200px">审查状态：
                        </td>
                        <td width="100px" style="text-align: left" nowrap="nowrap">
                            <asp:RadioButtonList ID="RadioButtonListStatus" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="RadioButtonListStatus_SelectedIndexChanged"
                                        AutoPostBack="true" Style="float: left;" Width="150px">
                                        <asp:ListItem Value="" Selected="true">全部</asp:ListItem>
                                        <asp:ListItem Value="未审查">未审查</asp:ListItem>
                                        <asp:ListItem Value="已审查">已审查</asp:ListItem>
                                    </asp:RadioButtonList>
                        </td>
                        <td align="left" style="padding-left:100px">
                            <asp:Button ID="ButtonOut" runat="server" Text="导出审查结果" CssClass="bt_large" OnClick="ButtonOut_Click" />&nbsp;&nbsp;
                           <asp:Button ID="ButtonRtn" runat="server" Text="返回" CssClass="bt_large" OnClick="ButtonRtn_Click" />
                        </td>
                    </tr>
                </table>
                <div style="width: 99%; margin: 8px auto;">
                    <telerik:RadGrid ID="RadGridApplyCheckItem" runat="server"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="10" OnDataBound="RadGridApplyCheckItem_DataBound" OnPageIndexChanged="RadGridApplyCheckItem_PageIndexChanged"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Center"
                            DataKeyNames="TaskItemID">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="Highlight <br/> ship name">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <uc3:CheckAll ID="CheckAll1" runat="server" />
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>                               
                                <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="人员姓名">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>                               
                                <telerik:GridBoundColumn UniqueName="IDCard" DataField="IDCard" HeaderText="证件号码">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridTemplateColumn UniqueName="CertificateCode" HeaderText="证书编号">
                                    <ItemTemplate>
                                       <%# GetUrl(Eval("ApplyType").ToString(),Eval("BusTypeID").ToString(),Eval("DataID").ToString(),Eval("CertificateCode").ToString())%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>                                                            
                                 <telerik:GridTemplateColumn UniqueName="BusTypeID" HeaderText="证书类别">
                                    <ItemTemplate>
                                        <%# GetBusTypeName(Convert.ToInt32(Eval("BusTypeID")))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false"  />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="ApplyType" DataField="ApplyType" HeaderText="业务类别">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ApplyFinishTime" DataField="ApplyFinishTime" HeaderText="业务办结日期" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>   
                                 <telerik:GridBoundColumn UniqueName="CheckTime" DataField="CheckTime" HeaderText="抽查审查时间" HtmlEncode="false" DataFormatString="{0:yyyy-MM-dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn> 
                                <telerik:GridBoundColumn UniqueName="CheckResult" DataField="CheckResult" HeaderText="审查结果">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>  
                                 <telerik:GridBoundColumn UniqueName="CheckResultDesc" DataField="CheckResultDesc" HeaderText="审查结果说明">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
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
  <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.ApplyCheckTaskItemMDL"
                    SelectMethod="GetList" TypeName="DataAccess.ApplyCheckTaskItemDAL"
                    SelectCountMethod="SelectCount" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                </div>
              <table id="TableEdit" runat="server" visible="false"  width="100%" border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; margin: 10px auto">
                    <tr class="GridLightBK">
                        <td colspan="2" class="barTitle">批量审核（请先勾选要审核操的记录）</td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="20%" align="right">审查结果说明：</td>
                        <td width="80%" align="left">
                            <asp:RadioButtonList ID="RadioButtonListCheckResult" runat="server" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonListCheckResult_SelectedIndexChanged">
                                 <asp:ListItem Text="合格" Value="合格" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="不合格" Value="不合格"></asp:ListItem>
                            </asp:RadioButtonList>
                        
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td width="20%" align="right">处理意见：</td>
                        <td width="80%" align="left">

                            <asp:TextBox ID="TextBoxCheckResultDesc" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="100" Text="符合要求。"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td colspan="2" align="center">
                            <asp:Button ID="ButtonCheck" runat="server" CssClass="bt_large" Text="确 定" OnClick="ButtonCheck_Click" />&nbsp;&nbsp;
                                         
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
        <div id="winpop"></div>
    </form>
</body>
</html>
