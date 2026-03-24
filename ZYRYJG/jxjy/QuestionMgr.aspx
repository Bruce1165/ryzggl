<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeBehind="QuestionMgr.aspx.cs" Inherits="ZYRYJG.jxjy.QuestionMgr" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="~/SelectSource.ascx" TagPrefix="uc4" TagName="SelectSource" %>

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
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
            <script type="text/javascript">
                function validateRadUploadTaboe(source, arguments) {
                    arguments.IsValid = getRadUpload('<%= RadUploadSignUpTable.ClientID %>').validateExtensions();
            }

            function getEventObject(W3CEvent) {   //事件标准化函数
                return W3CEvent || window.event;
            }
            function getPointerPosition(e) {   //兼容浏览器的鼠标x,y获得函数
                e = e || getEventObject(e);
                var x = e.pageX || (e.clientX + (document.documentElement.scrollLeft || document.body.scrollLeft));
                var y = e.pageY || (e.clientY + (document.documentElement.scrollTop || document.body.scrollTop));

                return { 'x': x, 'y': y };
            }

            function setImgSize(img, imgWidth, timgHeight, position, e) {
                img.style.width = imgWidth + "px";
                img.style.height = timgHeight + "px";

                var pos = getPointerPosition(e);

                img.style.position = position;
                if (position == "absolute") {
                    img.style.top = pos.y - timgHeight + 10;
                    img.style.left = pos.x - imgWidth + 10;
                }
                else {
                    img.style.top = 0;
                    img.style.left = 0;
                }
            }
            </script>
        </telerik:RadCodeBlock>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQuestion" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridQuestion">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQuestion" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ButtonUsing">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQuestion" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="ButtonStop">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQuestion" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadWindowManager ID="Singleton" Skin="Sunset" Width="500" Height="400" VisibleStatusbar="false"  OnClientClose="OnClientClose"
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
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Sunset">
        </telerik:RadAjaxLoadingPanel>
        <div class="div_out">
            <div id="div_top" class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;在线公益教育培训 &gt;&gt;<strong>培训试题管理</strong>
                </div>
            </div>
            <div class="content" style="min-height:600px">
                <table id="tableSearch" runat="server" class="cx" width="98%" border="0" align="center" cellspacing="5">
                    <tr>
                        <td align="right" nowrap="nowrap" width="15%">问题(关键字)：
                        </td>
                        <td align="left" width="35%">
                            <telerik:RadTextBox ID="RadTextBoxTitle" runat="server" Width="97%" Skin="Default"
                                onkeydown="ButtonSearchClick(event);">
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" width="15%" nowrap="nowrap">编辑日期：
                        </td>
                        <td align="left" width="35%">
                            <telerik:RadDatePicker ID="RadDatePicker_TimeStart" MinDate="01/01/1900" runat="server"
                                Width="46%" />
                                <span style="float: left; padding: 0 4px; line-height: 22px;">至</span>
                                        <telerik:RadDatePicker ID="RadDatePicker_TimeEnd" MinDate="01/01/1900" runat="server"
                                            Width="46%" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="15%" nowrap="nowrap">试题类型：
                        </td>
                        <td align="left" width="35%">
                            <span style="float: left; padding: 0 4px; line-height: 22px;">
                            <telerik:RadComboBox ID="RadComboBoxQuestionType" runat="server">
                                <Items>
                                    <telerik:RadComboBoxItem Text="全部" Value="" />
                                     <telerik:RadComboBoxItem Text="判断题" Value="判断题" />
                                     <telerik:RadComboBoxItem Text="单选题" Value="单选题" />
                                    <telerik:RadComboBoxItem Text="多选题" Value="多选题" />
                                </Items>
                            </telerik:RadComboBox>
                                </span>
                            <span style="float: left; padding: 0 4px; line-height: 22px;">试题编号：</span>
                            <telerik:RadTextBox ID="RadTextBoxQuestionNo" runat="server" Width="200px" Skin="Default"
                               >
                            </telerik:RadTextBox>
                        </td>
                        <td align="right" width="15%" nowrap="nowrap">状态：
                        </td>
                        <td align="left" width="35%">
                            <asp:RadioButtonList ID="RadioButtonListFlag" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="false">
                                <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                                <asp:ListItem Value="启用">启用</asp:ListItem>
                                <asp:ListItem Value="停用">停用</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>

                    <tr>
                         <td align="right" nowrap="nowrap" width="15%">隶属课程：
                        </td>
                        <td align="left" width="35%">
                            <uc4:SelectSource runat="server" ID="SelectSource" />
                        </td>
                        <td colspan="2" align="center">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </table>
               <div style="width: 95%; margin: 15px auto; clear: both;">
                    <div style="float: left; text-align: left;">
                        导入试题：
                    </div>
                    <div style="float: left; text-align: left;">
                        <telerik:RadUpload ID="RadUploadSignUpTable" runat="server" InitialFileInputsCount="1"
                            AllowedFileExtensions="xls" ControlObjectsVisibility="None" MaxFileInputsCount="1"
                            MaxFileSize="1073741824"  Enabled="true" Skin="Hot" EnableAjaxSkinRendering="false" Width="300px"
                            EnableEmbeddedSkins="false">
                            <Localization Select="选择文件" />                            
                        </telerik:RadUpload>
                        <asp:CustomValidator ID="Customvalidator1" runat="server" Display="Dynamic" ClientValidationFunction="validateRadUploadTaboe"
                            ErrorMessage="只能上传扩展名为xls的Excel文件！"> </asp:CustomValidator>
                    </div>
                    <div style="float: left; padding-left: 8px;">
                        <asp:Button ID="ButtonImportScore" runat="server" Text="导 入" CssClass="button" OnClick="ButtonImportScore_Click"
                            Enabled="true" />&nbsp; <a target="_blank" style="color: Blue" href="../Template/公益教育试题导入模版.xls">【鼠标右键另存，下载试题导入模版.xls】</a>
                    </div>
                    <div style="clear:both"></div>
                </div>
                <div class="table_cx" style="clear: both;">
                    <img src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    试题列表
                </div>
                <div class="div_grid">
                    <telerik:RadGrid ID="RadGridQuestion" runat="server" AutoGenerateColumns="False"
                        GridLines="None" AllowPaging="True" PageSize="40" OnDeleteCommand="RadGridQuestion_DeleteCommand"
                        OnPageIndexChanged="RadGridQuestion_PageIndexChanged" OnDataBound="RadGridQuestion_DataBound"
                        EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Skin="Blue" Width="100%">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="QuestionID" NoMasterRecordsText="　尚未添加">
                            <CommandItemTemplate>
                                <div class="grid_CommandBar">
                                    <input type="button" value=" " class="rgAdd" onclick="javascript:SetIfrmSrc('QuestionEdit.aspx');" />
                                    <nobr onclick="javascript:SetIfrmSrc('QuestionEdit.aspx');" class="grid_CmdButton">
                                        添加试题</nobr>
                                </div>
                            </CommandItemTemplate>
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="SelectAllColumn" HeaderText="Highlight <br/> ship name">
                                    <HeaderTemplate>
                                        <uc3:CheckAll ID="CheckAll1" runat="server" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn HeaderText="行号" UniqueName="RowNum" DataField="RowNum">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="试题编号" UniqueName="QuestionNo" DataField="QuestionNo">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>                                                              
                                <telerik:GridBoundColumn HeaderText="类型" UniqueName="QuestionType" DataField="QuestionType">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="问题" UniqueName="Title" DataField="Title">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="选项" UniqueName="Option" DataField="Option">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>                             
                                <telerik:GridBoundColumn HeaderText="答案" UniqueName="Answer" DataField="Answer">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="编辑日期" UniqueName="LastModifyTime" DataField="LastModifyTime"
                                    HtmlEncode="false" DataFormatString="{0:yy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn HeaderText="状态" UniqueName="Flag" DataField="Flag">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                 <telerik:GridTemplateColumn>
                                    <ItemTemplate>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("QuestionView.aspx?o=<%# Utility.Cryptography.Encrypt(string.Format("{0},{1},{2}",DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),Eval("QuestionType"),Eval("QuestionID")))%>");'>预览</span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn>
                                    <ItemTemplate>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("QuestionEdit.aspx?o=<%# Eval("QuestionID")%>");'>编辑</span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>

                                <telerik:GridButtonColumn UniqueName="Delete"  CommandName="Delete"
                                      Text="&lt;span onclick=&quot;if(!confirm('确定要删除吗?'))return false; &quot; &gt;删除&lt;/span&gt;">                           
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="true" />
                            <PagerStyle AlwaysVisible="true" />
                            <PagerTemplate>
                                <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.TrainQuestionDAL"
                        DataObjectTypeName="Model.TrainQuestionMDL" SelectMethod="GetListView" EnablePaging="true"
                        SelectCountMethod="SelectCountView" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                        SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
                <div style="padding-top: 10px; vertical-align: middle; line-height:400%; text-align:center;">
                    <asp:Button ID="ButtonUsing" runat="server" Text="启 用" CssClass="bt_large" OnClick="ButtonUsing_Click" />&nbsp;
                    <asp:Button ID="ButtonStop" runat="server" Text="停 用" CssClass="bt_large" OnClick="ButtonStop_Click" />
                </div>
                 
            </div>
        </div>
        <div id="winpop">
        </div>
        <uc1:IframeView ID="IframeView" runat="server" />
    </form>
</body>
</html>
