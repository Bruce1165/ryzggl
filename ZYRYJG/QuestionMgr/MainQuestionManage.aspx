<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="MainQuestionManage.aspx.cs" Inherits="ZYRYJG.QuestionMgr.MainQuestionManage" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="DivMain">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DivMain" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true"
        Skin="Windows7" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                题库管理 &gt;&gt; <strong>试题管理</strong>
            </div>
        </div>
        <div id="DivMain"  style="width: 98%; padding: 0; margin: 8px 0px 8px 0px; display: block;"
            runat="server" onclick="javascript:hideTree();">
            <div id="img_find" style="background: url(../Images/selectkm.png) no-repeat left top; position: absolute; left: 0px; color: White; width: 50px; height: 150px; border: none; display: inline;"
                onmouseover="javascript:showTree();">
            </div>
            <div id="div_tree" class="table_border" onmousemove="javascript:mousemove();" style="display: none; z-index: 99999; position: absolute; float: left; text-align: left; width: 445px; height: 450px; overflow: hidden; background-color: White; left: 0px; padding: 20px 8px 50px 8px;">
                <div style="text-align: left; font-weight: bold; font-size: 12px; width: 100%; line-height: 30px;">
                    请选择一个考试科目
                </div>
                <div style="width: 100%; height: 450px; overflow: auto; margin-bottom: 30px">
                    <telerik:RadGrid ID="RadGridPost" runat="server" Width="98%" AllowSorting="false"
                        AllowPaging="false" GridLines="None" AutoGenerateColumns="False" Skin="Blue"
                        EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" OnSelectedIndexChanged="RadGridPost_SelectedIndexChanged">
                        <ClientSettings EnableRowHoverStyle="true" EnablePostBackOnRowClick="true">
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="SubjectID">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="200px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="POSTNAME" DataField="POSTNAME" HeaderText="岗位工种">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="200px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="SubjectName" DataField="SubjectName" HeaderText="科目">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="75px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <ItemStyle Height="20px" />
                            <HeaderStyle Font-Bold="True" />
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
            </div>
            <div class="jbxxbt">
                试题管理
            </div>
            <div id="div_grid" style="display: block; width: 98%; height: 100%;">
                <div class="DivContent" id="div_tip" runat="server" style="margin-top: 10px; padding: 20px 20px 20px 20px; line-height: 18px; width: 90%">
                    <b>使用说明：</b>
                    <br />
                    1、请参考页面上《试题导入模版.doc》格式编写试题。
                    <br />
                    2、在左侧“考试科目列表”中选择要导入试题的科目。
                    <br />
                    3、在页面上点击“选择文件”按钮，选择已录入好试题的模板，点击“导入”按钮进行试题导入。导入后可进行单条预览和修改。
                    <br />
                </div>
                <div style="width: 98%; margin: 15px auto; clear: both; line-height: 22px;">
                    <div style="float: left; text-align: left; font-size: 12px;">
                        导入试题：
                    </div>
                    <div style="float: left; text-align: left; vertical-align: bottom;Width:270px">
                        <telerik:RadUpload ID="RadUploadFile" runat="server" InitialFileInputsCount="1" AllowedFileExtensions="doc"
                            ControlObjectsVisibility="None" MaxFileInputsCount="1" MaxFileSize="1073741824"
                            Height="20px" Width="220px" Enabled="true" Skin="Hot" EnableAjaxSkinRendering="false"
                            EnableEmbeddedSkins="false">
                            <Localization Select="选择文件" />
                        </telerik:RadUpload>
                        <asp:CustomValidator ID="Customvalidator1" runat="server" Display="Dynamic" ClientValidationFunction="validateRadUploadTaboe"
                            ErrorMessage="只能上传扩展名为xls的文件！"> </asp:CustomValidator>
                    </div>
                    <div style="float: left; text-align: left; margin-left:20px">
                        &nbsp;&nbsp;
                        <asp:Button ID="ButtonImport" runat="server" Text="导 入" CssClass="bt_large" OnClick="ButtonImport_Click"
                            OnClientClick="javascript:if(confirm('确定要导入吗？')==false) return false;" Enabled="true" />&nbsp;
                        <a target="_blank" style="color: Blue; font-size: 12px;" href="..\Template\试题导入模版.doc">下载《试题导入模版.doc》</a>
                    </div>
                    <div style="clear:both"></div>
                </div>
                <div style="width: 98%; margin: 0 auto; font-size: 12px; padding-left: 20px; color: #DC2804; font-weight: bold; text-align: left; line-height: 30px;">
                    <asp:Label ID="LabelSelectPost" runat="server" Text=""></asp:Label>
                </div>
                <div style="width: 98%; margin: 0 auto; font-size: 12px; padding-left: 20px; text-align: left; line-height: 30px;">
                  
                      <div style="float:left; padding-left:20px;">状态：</div>
                      <div style="float:left">  <asp:RadioButtonList ID="RadioButtonListStatus" runat="server" RepeatDirection="Horizontal"
                         AutoPostBack="false">
                         <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                         <asp:ListItem Value="0" >未发布</asp:ListItem>
                         <asp:ListItem Value="1">已发布</asp:ListItem>
                     </asp:RadioButtonList></div>
                      <div style="float:left; padding-left:20px;">
                    关键字：                             
                                    <telerik:RadTextBox ID="RadTextBoxKey" runat="server" Width="300px" Skin="Default"
                                        MaxLength="100">
                                    </telerik:RadTextBox>
                    </div>
                    <div style="float:left; padding-left:20px; padding-top:5px">
                    <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                        </div>
                     <div style="clear:both;"></div>
                </div>
                <div style="width: 98%; margin: 0 auto;">
                    <telerik:RadGrid ID="RadGridQuestion" runat="server" Width="100%" AllowSorting="false" AllowPaging="true" AllowCustomPaging="true"
                        PageSize="10" GridLines="None" AutoGenerateColumns="false" Skin="Blue"
                        EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" OnNeedDataSource="RadGridQuestion_NeedDataSource"
                        OnItemCommand="RadGridQuestion_ItemCommand" OnItemDataBound="RadGridQuestion_ItemDataBound">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="Top" EditMode="EditForms" DataKeyNames="QuestionID"
                            NoMasterRecordsText="　没有可显示的记录">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="行号">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="QuestionType" DataField="QuestionType" HeaderText="试题类型">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Title" DataField="Title" HeaderText="题目">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <%--<telerik:GridBoundColumn UniqueName="Answer" DataField="Answer" HeaderText="标准答案">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="90px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridTemplateColumn UniqueName="Answer" HeaderText="答案">
                                    <ItemTemplate>
                                        <%#Eval("Answer").ToString().Length>6?Eval("Answer").ToString().Substring(0,6) + "...":Eval("Answer")%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="ShowCode" DataField="ShowCode" HeaderText="大纲">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Difficulty" DataField="Difficulty" HeaderText="难度">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="Flag" HeaderText="状态">
                                    <ItemTemplate>
                                        <%#Eval("Flag").ToString()=="0"?"未发布":"已发布"%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridEditCommandColumn UniqueName="Edit" EditText="编辑">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridButtonColumn UniqueName="Delete" CommandName="Delete" Text="&lt;span onclick=&quot;if(!confirm('您确定要删除么?'))return false; &quot; &gt;删除&lt;/span&gt;">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                            <PagerTemplate>
                                <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                            <CommandItemSettings AddNewRecordText="添加试题" ShowRefreshButton="false" />
                            <EditFormSettings InsertCaption="添加试题" CaptionFormatString="编辑试题" EditFormType="Template"
                                PopUpSettings-Modal="true" FormCaptionStyle-HorizontalAlign="Left">
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                                <FormCaptionStyle HorizontalAlign="Left"></FormCaptionStyle>
                                <FormTemplate>
                                    <table width="100%" border="0" align="center" cellspacing="8" style="line-height: 18px; background-color: #DAF6E5; border: 2px solid red">
                                        <tr>
                                            <td align="right" style="width: 12%;">大纲编号：
                                            </td>
                                            <td align="left" style="width: 38%;">
                                                <telerik:RadTextBox ID="RadTextBoxShowCode" runat="server" Width="100px" Skin="Default"
                                                    ToolTip="序号层级间用小数点表示，例如：1.1.1" Text='<%# Eval("ShowCode") %>' MaxLength="50">
                                                </telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="RadTextBoxShowCode"
                                                    Display="Dynamic" runat="server" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="格式不正确！"
                                                    Display="Dynamic" ValidationExpression="\d(.\d{1,4})*" ControlToValidate="RadTextBoxShowCode"></asp:RegularExpressionValidator>
                                                <asp:HiddenField ID="HiddenFieldQuestionID" runat="server" Value='<%# Eval("QuestionID")%>' />
                                            </td>
                                            <td align="right" style="width: 12%;">试题类型：
                                            </td>
                                            <td align="left" style="width: 38%;">
                                                <asp:RadioButtonList ID="RadioButtonListQuestionType" runat="server" RepeatDirection="Horizontal"
                                                    SelectedValue='<%# RadGridQuestion.MasterTableView.IsItemInserted ==true?"单选题": Eval("QuestionType") %>'
                                                    RepeatColumns="2">
                                                    <asp:ListItem Text="判断题" Value="判断题"></asp:ListItem>
                                                    <asp:ListItem Text="单选题" Value="单选题" Selected="True"></asp:ListItem>
                                                    <asp:ListItem Text="多选题" Value="多选题"></asp:ListItem>
                                                    <asp:ListItem Text="简答题" Value="简答题"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">题目：
                                            </td>
                                            <td align="left" valign="top" colspan="3">

                                                <telerik:RadEditor ID="RadEditorTitle" runat="server" ContentFilters="FixUlBoldItalic"
                                                    Content='<%# Eval("Title").ToString().Replace("\r\n","<br>") %>'>
                                                </telerik:RadEditor>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="RadEditorTitle"
                                                    Display="Dynamic" runat="server" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">标准答案：
                                            </td>
                                            <td align="left" valign="top" colspan="3">
                                                <telerik:RadTextBox ID="RadTextBoxAnswer" runat="server" Width="80%" Skin="Default"
                                                    Text='<%# Eval("Answer") %>' MaxLength="4000" TextMode="MultiLine" Rows="5">
                                                </telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="RadTextBoxAnswer"
                                                    Display="Dynamic" runat="server" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">难度：
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioButtonListDifficulty" runat="server" RepeatDirection="Horizontal"
                                                    SelectedValue='<%# RadGridQuestion.MasterTableView.IsItemInserted ==true?"1": Eval("Difficulty") %>'>
                                                    <asp:ListItem Text="易" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="中" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="难" Value="3"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                            <td align="right">状态：
                                            </td>
                                            <td align="left">
                                                <asp:RadioButtonList ID="RadioButtonListFlag" runat="server" RepeatDirection="Horizontal"
                                                    SelectedValue='<%# RadGridQuestion.MasterTableView.IsItemInserted ==true?"0": Eval("Flag") %>'>
                                                    <asp:ListItem Text="未发布" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="已发布" Value="1"></asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; padding: 8px 8px;" colspan="4">
                                                <asp:Button ID="ButtonSave" CssClass="button" Text='<%# (Container is GridEditFormInsertItem) ? "保 存" : "更 新" %>'
                                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                                <asp:Button ID="Button2" CssClass="button" Text="取 消" runat="server" CausesValidation="False"
                                                    CommandName="Cancel"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </FormTemplate>
                            </EditFormSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.QuestionOB"
                        SelectMethod="GetList" TypeName="DataAccess.QuestionDAL"
                        UpdateMethod="Update" SelectCountMethod="SelectCount" EnablePaging="true" MaximumRowsParameterName="maximumRows"
                        StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
                <div id="DivHtmlSource" runat="server" style="width: 95%; margin: 15px auto; clear: both; line-height: 22px;">
                    <asp:Button ID="ButtonCheck" runat="server" Text="正确性检查" CssClass="bt_large" OnClick="ButtonCheck_Click"
                        Enabled="true" />&nbsp;
                 <asp:Button ID="ButtonPublsh" runat="server" Text="批量发布" CssClass="bt_large" OnClick="ButtonPublsh_Click"
                     OnClientClick="javascript:if(confirm('确定要发布吗？')==false) return false;" Enabled="true" />&nbsp;
                     <asp:Button ID="ButtonDelete" runat="server" Text="批量删除" CssClass="bt_large" OnClick="ButtonDelete_Click"
                         OnClientClick="javascript:if(confirm('警告：该操作将删除该科目所有试题，确定要删除吗？')==false) return false;" Enabled="true" />
                </div>
            </div>
        </div>
    </div>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {

                if (args.get_eventTarget().indexOf("ButtonExportToExcel") >= 0
                || args.get_eventTarget().indexOf("ButtonImport") >= 0
                ) {
                    args.set_enableAjax(false);
                }
            }
            function mousePosition(ev) {
                if (ev.pageX || ev.pageY) {
                    return { x: ev.pageX, y: ev.pageY };
                }
                return {
                    x: ev.clientX + document.body.scrollLeft - document.body.clientLeft,
                    y: ev.clientY + document.body.scrollTop - document.body.clientTop
                };
            }

            function showTree() {
                document.getElementById('div_tree').style.display = 'inline';
                document.getElementById('img_find').style.display = 'none';
            }

            function mousemove() {
                if (mousePosition(window.event).x > 560) {
                    hideTree();
                };
            }

            function hideTree() {
                var tree = document.getElementById('div_tree');
                var img = document.getElementById('img_find');
                if (tree.style) {
                    tree.style.display = 'none';
                    img.style.display = 'inline';
                }
            }
            function validateRadUploadTaboe(source, arguments) {
                arguments.IsValid = getRadUpload('<%= RadUploadFile.ClientID %>').validateExtensions();
            }
        </script>
    </telerik:RadCodeBlock>
</asp:Content>
