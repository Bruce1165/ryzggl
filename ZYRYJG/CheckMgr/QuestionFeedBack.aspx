<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuestionFeedBack.aspx.cs" Inherits="ZYRYJG.CheckMgr.QuestionFeedBack" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
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
                <telerik:AjaxSetting AjaxControlID="divMain">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="divMain" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;综合监管 &gt;&gt;<strong>监管问题反馈</strong>
                </div>
            </div>
            <div class="content" id="divMain" runat="server">
                <div class="DivContent">
                    <a href="../Template/反馈填报须知.doc" target="_blank"><font style="color: blue; font-size: 18px; font-weight: bold; text-decoration: none; margin-left: 10px;">【反馈填报须知】</font></a>
                    <a href="../Template/常见问题及统一解释.doc" target="_blank"><font style="color: blue; font-size: 18px; font-weight: bold; text-decoration: none; margin-left: 10px;">【常见问题及统一解释】</font></a>
                </div>
                <telerik:RadGrid ID="RadGridCheckFeedBack" runat="server"
                    GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%" Skin="Blue" EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" PagerStyle-AlwaysVisible="true">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Center"
                        DataKeyNames="DataID">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="PostTypeName" HeaderText="注册证书">
                                <ItemTemplate>
                                    <p>
                                        <b><%#Eval("PostTypeName") %>：</b><%#Eval("CertificateCode") %><br />
                                        <b>注册单位：</b><%#Eval("Unit") %><br />
                                        <b>所属区：</b><%#Eval("Country") %>
                                    </p>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="question" HeaderText="存在问题">
                                <ItemTemplate>
                                    <p class="link_edit" onclick='javascript:SetIfrmSrc("QuestionFeedBackDetail.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("DataID").ToString())%>");'>
                                        <b>数据来源：</b>住房和城乡建设部（<%#Convert.ToDateTime(Eval("SourceTime")).ToString("yyyy年M月d日") %>）<br />
                                        <b>社保情况：</b><%#Eval("SheBaoCase") %><br />
                                        <b>公积金情况：</b><%#Eval("GongjijinCase") %><br />
                                        <b>社保单位：</b><%#Eval("ShebaoUnit") %>
                                    </p>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="LastReportTime" HeaderText="反馈要求与结果">
                                <ItemTemplate>
                                    <p>
                                        <b>反馈截止时间：</b><%# Convert.ToDateTime(Eval("LastReportTime")).ToString("yyyy年M月d日") %><br />
                                        <b>个人反馈时间：</b><%#Eval("WorkerRerpotTime") == DBNull.Value?(Eval("DataStatusCode").ToString()=="7"?"":"尚未反馈"):Convert.ToDateTime(Eval("WorkerRerpotTime")).ToString("yyyy年M月d日") %><br />
                                        <b>反馈审批结果：</b><%#Eval("DataStatusCode").ToString()=="7"?string.Format("审批通过（办结：{0}）",Eval("PassType")):
                                                                Eval("DataStatusCode").ToString()=="2"?string.Format("{0}<apan style='color:red'>审批不通过。{1}</apan>",Eval("BackUnit"),Eval("BackReason")):
                                                                Eval("DataStatusCode").ToString()=="3"?"待审批":
                                                                Eval("DataStatusCode").ToString()=="1"?"":"审核中"%>
                                    </p>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="question">
                                <ItemTemplate>
                                    <span class="link_edit" style="display: block; line-height: 400%" onclick='javascript:SetIfrmSrc("QuestionFeedBackDetail.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("DataID").ToString())%>");'>办 理
                                    </span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
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
                <asp:ObjectDataSource ID="ObjectDataSourceCheckFeedBack" runat="server" DataObjectTypeName="Model.CheckFeedBackMDL"
                    SelectMethod="GetList" TypeName="DataAccess.CheckFeedBackDAL"
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
        <div id="winpop"></div>
    </form>
</body>
</html>
