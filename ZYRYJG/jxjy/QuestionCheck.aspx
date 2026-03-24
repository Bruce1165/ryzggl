<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="QuestionCheck.aspx.cs" Inherits="ZYRYJG.jxjy.QuestionCheck" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/SelectSource.ascx" TagPrefix="uc1" TagName="SelectSource" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Hot/Upload.hot.css?v=1.001" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.007" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false" EnableEmbeddedScripts="true" OnClientClose="OnClientClose"
            ReloadOnShow="true" runat="server" Skin="Sunset" EnableShadow="true">
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
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        </telerik:RadCodeBlock>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
            Skin="Sunset" />
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;在线公益教育培训 &gt;&gt;培训试题管理&gt;&gt;<strong>试题自检扫描</strong>
                </div>
            </div>
            <div class="content">
                <table id="tableSearch" runat="server" class="cx" width="98%" border="0" align="center" cellspacing="5">
                    <tr>
                        <td align="right" width="15%" nowrap="nowrap">试题类型：
                        </td>
                        <td align="left" width="25%">

                            <telerik:RadComboBox ID="RadComboBoxQuestionType" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" />
                                    <telerik:RadComboBoxItem Text="判断题" Value="判断题" />
                                    <telerik:RadComboBoxItem Text="单选题" Value="单选题" />
                                    <telerik:RadComboBoxItem Text="多选题" Value="多选题" />
                                </Items>
                            </telerik:RadComboBox>
                        </td>
                        <td align="right" nowrap="nowrap" width="15%">隶属课程：
                        </td>
                        <td align="left" width="35%">
                            <uc1:SelectSource runat="server" ID="SelectSource" />
                        </td>
                        <td align="center">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </table>
                <div runat="server" style="margin: 5px 10px; padding: 5px 20px;">
                    <div style="padding-top: 10px; padding-bottom: 20px; vertical-align: middle; width: 95%; margin: 10px 0; text-align: center">
                         <asp:Button ID="ButtonCheck" runat="server" Text="扫描错误" CssClass="button" OnClick="ButtonCheck_Click" />&nbsp;&nbsp;
                        <asp:Button ID="ButtonPrev" runat="server" Text="上一题" CssClass="button" OnClick="ButtonPrev_Click" />&nbsp;&nbsp;
                        <asp:Button ID="ButtonNext" runat="server" Text="下一题" CssClass="button" OnClick="ButtonNext_Click" />&nbsp;&nbsp;
                        <asp:Button ID="ButtonEdit" runat="server" Text="编 辑" CssClass="button" OnClick="ButtonEdit_Click" />&nbsp;&nbsp;
                        <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript: hideIfam();" />
                    </div>
                    <div style="width: 95%; margin: 10px auto; text-align: center;">
                        <asp:Label ID="LabelPage" runat="server" Text="" Font-Bold="true" Visible="false"></asp:Label>
                    </div>
                    <div id="DivQuestion" runat="server" style="text-align: left; padding-left: 10px; font-size: 18px; line-height: 150%;">
                    </div>
                    <telerik:RadGrid ID="RadGridQuestOption" runat="server" AutoGenerateColumns="False"
                        GridLines="None" AllowPaging="false" PageSize="10" EnableAjaxSkinRendering="true" OnItemDataBound="RadGridQuestOption_ItemDataBound"
                        EnableEmbeddedSkins="false" Width="100%" ShowHeader="false">
                        <ClientSettings EnableRowHoverStyle="false">
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="QuestOptionID,OptionNo" NoMasterRecordsText="　没有可显示的记录">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="Highlight <br/> ship name">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" Width="20" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="Highlight <br/> ship name">
                                    <ItemTemplate>
                                        <%# Eval("OptionNo")%>、<%# Eval("OptionContent")%>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" Font-Size="18px" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <div id="DivQuestionAnswerTip" runat="server" style="margin: 30px 30px; text-align: center; line-height: 200%; color: red; font-size: 16px; font-weight: bold;">
                    </div>
                </div>

            </div>

        </div>
        <div id="winpop">
        </div>
    </form>
</body>
</html>

