<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="SourceMgr.aspx.cs" Inherits="ZYRYJG.jxjy.SourceMgr" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Hot/Upload.hot.css?v=1.001" rel="stylesheet" type="text/css" />
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
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="RadGridSource">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridSource" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadWindowManager ID="Singleton" Skin="Windows7" Width="500" Height="400" VisibleStatusbar="false"
            runat="server">
            <AlertTemplate>
                <div class="alertText">
                    {1}
                </div>
                <div class="confrimButton">
                    <input onclick="$find('{0}').close();" class="button" id="ButtonOK" type="button"
                        value="确 定" />
                </div>
            </AlertTemplate>
            <ConfirmTemplate>
                <div class="confrimText">
                    {1}
                </div>
                <div class="confrimButton">
                    <input onclick="$find('{0}').close(true);" class="button" id="ButtonOK" type="button"
                        value="确 定" />&nbsp;&nbsp;
                <input onclick="$find('{0}').close(false);" class="button" id="ButtonCancel" type="button"
                    value="取 消" />
                </div>
            </ConfirmTemplate>
        </telerik:RadWindowManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                function validateRadUploadTaboe(source, arguments) {
                    arguments.IsValid = getRadUpload('<%= RadUploadSignUpTable.ClientID%>').validateExtensions();
                }
            </script>
        </telerik:RadCodeBlock>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;在线公益教育培训 &gt;&gt;<strong>培训课程管理</strong>
                </div>
            </div>

            <div class="content">
                <table class="cx" width="98%" border="0" align="center" cellspacing="5">                   
                    <tr>
                        <td align="right"  nowrap="nowrap" valign="middle">年 度：
                        </td>
                        <td align="left" >
                            <telerik:RadComboBox ID="RadComboBoxSourceYear" runat="server" >
                            </telerik:RadComboBox>
                        </td>                        
                        <td align="right"  nowrap="nowrap" valign="middle">所属栏目：
                        </td>
                        <td align="left">
                            <telerik:RadComboBox ID="RadComboBoxBarType" runat="server" >
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                    <telerik:RadComboBoxItem Text="工匠讲堂" Value="工匠讲堂" />
                                    <telerik:RadComboBoxItem Text="首都建设云课堂" Value="首都建设云课堂" />
                                    <telerik:RadComboBoxItem Text="行业推荐精品课程" Value="行业推荐精品课程" />
                                    <telerik:RadComboBoxItem Text="无" Value="无" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="right" nowrap="nowrap">类型：
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="RadioButtonListSourceType" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="false">
                                <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Value="必修">必修</asp:ListItem>
                                <asp:ListItem Value="选修">选修</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                   
                         <td align="right" nowrap="nowrap" >
                            <telerik:RadComboBox ID="RadComboBoxIten" runat="server" Width="100px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="课程名称" Value="SourceName" Selected="true" />
                                    <telerik:RadComboBoxItem Text="授课教师" Value="Teacher" />
                                    <telerik:RadComboBoxItem Text="工作单位" Value="WorkUnit" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="left" >
                            <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Skin="Default" Width="250px">
                            </telerik:RadTextBox>
                        </td>
                        <td  align="left">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </table>
                <%--  <div style="width: 98%; margin: 15px auto; padding-bottom: 20px; ">
                    
                </div>--%>
                <div class="table_cx" style="margin: 15px auto; padding-bottom: 20px;">
                    <div style="float: left; text-align: left;">
                        <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                        课程、课件列表（展开课程即可看到包含的课件列表）
                    </div>
                    <div style="float: right; text-align: left;">
                        <div style="float: left; text-align: left;">
                            课程课件导入：
                        </div>
                        <div style="float: left; text-align: left;">
                            <telerik:RadUpload ID="RadUploadSignUpTable" runat="server" InitialFileInputsCount="1"
                                AllowedFileExtensions="xls" ControlObjectsVisibility="None" MaxFileInputsCount="1"
                                MaxFileSize="1073741824" Width="280px" Enabled="true" Skin="Hot" EnableAjaxSkinRendering="false"
                                EnableEmbeddedSkins="false">
                                <Localization Select="选择文件" />
                            </telerik:RadUpload>
                            <asp:CustomValidator ID="Customvalidator1" runat="server" Display="Dynamic" ClientValidationFunction="validateRadUploadTaboe"
                                ErrorMessage="只能上传扩展名为xls的Excel文件！"> </asp:CustomValidator>
                        </div>
                        <div style="float: left; padding-left: 8px;">
                            <asp:Button ID="ButtonImport" runat="server" Text="导 入" CssClass="button" OnClick="ButtonImport_Click"
                                Enabled="true" />
                        </div>
                        <div style="float: left; padding-left: 18px;">
                            <a href="../Template/课程导入模版.xls" target="_blank">《下载导入模板》</a>
                        </div>
                    </div>
                </div>
                <div style="width: 99%; margin: 8px auto;">
                    <telerik:RadGrid ID="RadGridSource" runat="server"
                        AutoGenerateColumns="False" AllowPaging="True" PageSize="15"
                        OnDeleteCommand="RadGridSource_DeleteCommand" EnableAjaxSkinRendering="false"
                        EnableEmbeddedSkins="false" Skin="Blue" Width="100%" OnDetailTableDataBind="RadGridSource_DetailTableDataBind">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="SourceID,ParentSourceID,SourceName"
                            NoMasterRecordsText="　尚未添加课程">
                            <DetailTables>
                                <telerik:GridTableView DataKeyNames="SourceID,ParentSourceID,SourceName" Width="95%"
                                    runat="server" CommandItemDisplay="None" CssClass="subGrid" Summary="" Name="subSource"
                                    NoDetailRecordsText="　尚未添加课件" ShowHeadersWhenNoRecords="true">
                                    <Columns>
                                        <telerik:GridBoundColumn HeaderText="排序" UniqueName="SortID" DataField="SortID">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="类型" UniqueName="SourceType" DataField="SourceType">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="年度" UniqueName="SourceYear" DataField="SourceYear">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="课件名称" UniqueName="SourceName" DataField="SourceName">
                                            <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                         <telerik:GridBoundColumn HeaderText="学时" UniqueName="ShowPeriod" DataField="ShowPeriod">
                                            <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                       <%-- <telerik:GridTemplateColumn HeaderText="课时" UniqueName="Period">
                                            <ItemTemplate>
                                                <%# Convert.ToInt32(Eval("Period")) / 60 == 0 ? "" : string.Format("{0}小时", Convert.ToString(Convert.ToInt32(Eval("Period")) / 60))%><%# string.Format("{0}分", Convert.ToString(Convert.ToInt32(Eval("Period")) % 60))%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Right" Wrap="false" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                        </telerik:GridTemplateColumn>--%>
                                        <telerik:GridBoundColumn HeaderText="授课教师" UniqueName="Teacher" DataField="Teacher">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="工作单位" UniqueName="WorkUnit" DataField="WorkUnit">
                                            <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn HeaderText="状态" UniqueName="Status" DataField="Status">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridTemplateColumn HeaderText="播放" UniqueName="SourceWareUrl">
                                            <ItemTemplate>
                                                <%# string.Format("<span style=\"cursor:pointer;\" onclick='OpenSameWindow(\"{0}?nickname={1}&uid={2}&k={3}\");'><font style='color:Blue;'>播 放</font></span>&nbsp;", Eval("SourceWareUrl"), Server.UrlEncode(PersonName), uid(), getPlayKey())%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridTemplateColumn>
                                         <%-- <telerik:GridTemplateColumn HeaderText="测试播放" UniqueName="test">
                                            <ItemTemplate>
                                                <%# string.Format("<span style=\"cursor:pointer;\" onclick='OpenSameWindowFull(\"SourceWarePaly.aspx?o={0}\");'><font style='color:Blue;'>播 放</font></span>&nbsp;",Utility.Cryptography.Encrypt(Eval("SourceID").ToString()))%>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridTemplateColumn>--%>
                                        <telerik:GridTemplateColumn>
                                            <ItemTemplate>
                                                <span class="link_edit" onclick='javascript:SetIfrmSrc("SourceEdit.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("SourceID").ToString())%>");'>编辑</span>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridTemplateColumn>
                                        <telerik:GridButtonColumn UniqueName="Delete" ButtonType="LinkButton" CommandName="Delete"
                                            ConfirmText="确定要删除吗？" ConfirmDialogType="RadWindow" Text="删除">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                </telerik:GridTableView>
                            </DetailTables>
                            <CommandItemTemplate>
                                <div class="grid_CommandBar">
                                    <input type="button" value=" " class="rgAdd" onclick="javascript: SetIfrmSrc('SourceEdit.aspx');" />
                                    <nobr onclick="javascript:SetIfrmSrc('SourceEdit.aspx');" class="grid_CmdButton">
                                        添加课程</nobr>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="排序" UniqueName="SortID" DataField="SortID">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="所属栏目" UniqueName="BarType" DataField="BarType">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="类型" UniqueName="SourceType" DataField="SourceType">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="年度" UniqueName="SourceYear" DataField="SourceYear">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="课程名称" UniqueName="SourceName" DataField="SourceName">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                             <%--   <telerik:GridTemplateColumn HeaderText="课件总时长" UniqueName="Period">
                                    <ItemTemplate>
                                        <%# Convert.ToInt32(Eval("Period")) / 60 == 0 ? "" : string.Format("{0}小时", Convert.ToString(Convert.ToInt32(Eval("Period")) / 60))%><%# string.Format("{0}分", Convert.ToString(Convert.ToInt32(Eval("Period")) % 60))%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Right" Wrap="false" />
                                </telerik:GridTemplateColumn>--%>
                                   <telerik:GridBoundColumn HeaderText="学时" UniqueName="ShowPeriod" DataField="ShowPeriod">
                                            <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="课件数" UniqueName="SourceWareCount" DataField="SourceWareCount">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                         
                                <telerik:GridTemplateColumn HeaderText="配套试题数" UniqueName="QuestionCount" SortExpression="QuestionCount">
                                    <ItemTemplate>
                                        <a href="QuestionMgr.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("SourceID").ToString())%>"><%#Eval("QuestionCount")%></a>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="授课教师" UniqueName="Teacher" DataField="Teacher">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>

                                <telerik:GridBoundColumn HeaderText="工作单位" UniqueName="WorkUnit" DataField="WorkUnit">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="状态" UniqueName="Status" DataField="Status">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn>
                                    <ItemTemplate>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("SourceEdit.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("SourceID").ToString())%>");'>编辑</span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridButtonColumn UniqueName="Delete" ButtonType="LinkButton" CommandName="Delete"
                                    ConfirmText="确定要删除吗？" ConfirmDialogType="RadWindow" Text="删除">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridButtonColumn>
                                <telerik:GridTemplateColumn>
                                    <ItemTemplate>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("SourceEdit.aspx?u=<%# Utility.Cryptography.Encrypt(Eval("SourceID").ToString())%>");'>添加课件</span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn>
                                    <ItemTemplate>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("QuestionMgr.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("SourceID").ToString())%>&t=add");'>添加试题</span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="true" />
                            <PagerStyle AlwaysVisible="true" />
                            <PagerTemplate>
                                <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.SourceDAL"
                        DataObjectTypeName="Model.SourceOB" SelectMethod="GetListWithQuestionCount" EnablePaging="true"
                        SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                        SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>

            </div>
        </div>
        <div id="winpop">
        </div>
        <uc1:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
