<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="InfoTagManage.aspx.cs" Inherits="ZYRYJG.QuestionMgr.InfoTagManage" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
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
                题库管理 &gt;&gt; <strong>知识大纲管理</strong>
            </div>
        </div>
        <div id="DivMain" style="width: 98%; padding: 0; margin: 8px 0px 8px 0px;
            display: block;" runat="server" onclick="javascript:hideTree();">
            <div id="img_find" style="background: url(../Images/selectkm.png) no-repeat left top;
                position: absolute; left: 0px; color: White; width: 50px; height: 150px; border: none;
                display: inline;" onmouseover="javascript:showTree();">
            </div>
            <div id="div_tree"  onmousemove="javascript:mousemove();" style="display: none;
                position: absolute; float: left; text-align: left; width: 445px; height:450px; overflow: hidden;
                background-color: White; left: 0px; padding: 18px 8px 50px 8px;">
                <div style="text-align: left; font-weight: bold; font-size: 12px; width: 100% ;line-height:30px;">
                    请选择一个考试科目</div>
                <div style=" width: 100%; height:450px;overflow: auto; margin-bottom:30px">
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
                知识大纲管理
            </div>
            <div id="div_grid" style="display: block; width: 95%; height: 100%;">
                <div class="DivContent" id="div_tip" runat="server" style="margin-top: 10px; padding: 20px 2%;
                    line-height: 18px; width: 92%">
                    <b>使用说明：</b>
                    <br />
                    1、序号使用数字表示，层级间用小数点表示，每层号码范围【1~99】，最大支持4层（例如：1.12.1.1）。
                    <br />
                    2、同一科目内大纲序号不允许重复。
                    <br />
                    3、单条大纲内容不得超过4000个字符（中文算2个）。
                    <br />
                </div>
                <div style="width: 98%; margin: 0 auto; font-size: 12px; padding-left: 20px; 
                    font-weight: bold; text-align: left; line-height: 30px;">
                    <asp:Label ID="LabelSelectPost" runat="server" Text="" ForeColor="#0033cc"></asp:Label>
                    <asp:Label ID="LabelWeightCount" runat="server" Text="" ForeColor="#ff0000" style="padding-left:20px"></asp:Label>
                </div>
                <div style="width: 98%; margin: 0 auto;">
                    <telerik:RadGrid ID="RadGridInfoTag" AutoGenerateColumns="False" runat="server" Visible="true"
                        AllowPaging="false" AllowSorting="false" Skin="Blue" EnableAjaxSkinRendering="false"
                        EnableEmbeddedSkins="false" Width="100%" GridLines="None" OnInsertCommand="RadGridInfoTag_InsertCommand"
                        OnNeedDataSource="RadGridInfoTag_NeedDataSource" OnDeleteCommand="RadGridInfoTag_DeleteCommand"
                        OnUpdateCommand="RadGridInfoTag_UpdateCommand" OnExcelExportCellFormatting="RadGridInfoTag_ExcelExportCellFormatting">
                        <ExportSettings FileName="考试大纲" OpenInNewWindow="true">
                        </ExportSettings>
                        <MasterTableView EditMode="EditForms" CommandItemDisplay="Top" DataKeyNames="TagID"
                            NoMasterRecordsText="　没有可显示的记录">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="ShowCode" DataField="ShowCode" HeaderText="序号">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="50px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Title" DataField="Title" HeaderText="大纲内容">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="70%" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Weight" DataField="Weight" HeaderText="权重">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridEditCommandColumn UniqueName="Edit" EditText="编辑">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="50px" />
                                </telerik:GridEditCommandColumn>
                                <telerik:GridButtonColumn UniqueName="Delete" CommandName="Delete" Text="&lt;span onclick=&quot;if(!confirm('您确定要删除么?'))return false; &quot; &gt;删除&lt;/span&gt;">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="50px" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                            <CommandItemSettings AddNewRecordText="添加知识大纲" ShowRefreshButton="false" />
                            <EditFormSettings InsertCaption="添加知识大纲" CaptionFormatString="编辑知识大纲" EditFormType="Template"
                                PopUpSettings-Modal="true" FormCaptionStyle-HorizontalAlign="Left">
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                                <FormCaptionStyle HorizontalAlign="Left"></FormCaptionStyle>
                                <FormTemplate>
                                    <table width="100%" border="0" align="center" cellspacing="8">
                                        <tr>
                                            <td align="left">
                                                <span style="color: #969696; font-weight: bold; padding-left: 50px; line-height: 30px;">
                                                    序号层级间用小数点表示，例如：1.1.1。权重范围0~100（即百分比）
                                                    <br />
                                                </span>序号：
                                                <telerik:RadTextBox ID="RadTextBoxShowCode" runat="server" Width="100px" Skin="Default"
                                                    Text='<%# Eval("ShowCode") %>' MaxLength="50">
                                                </telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="RadTextBoxShowCode"
                                                    Display="Dynamic" runat="server" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="格式不正确！"
                                                    Display="Dynamic" ValidationExpression="\d(.\d)*" ControlToValidate="RadTextBoxShowCode"></asp:RegularExpressionValidator>
                                                <asp:HiddenField ID="HiddenFieldTagID" runat="server" Value='<%# Eval("TagID")%>' />
                                                权重：
                                                <telerik:RadTextBox ID="RadTextBoxWeight" runat="server" Width="100px" Skin="Default"
                                                    Text='<%# Eval("Weight") %>' MaxLength="3">
                                                </telerik:RadTextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="请输入0~100数字！"
                                                    Display="Dynamic" ValidationExpression="\d{1,2}|100" ControlToValidate="RadTextBoxWeight"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="left" valign="top">
                                                内容：
                                                <telerik:RadTextBox ID="RadTextBoxTitle" runat="server" Width="80%" Skin="Default"
                                                    Text='<%# Eval("Title") %>' MaxLength="1000" TextMode="MultiLine" Rows="3">
                                                </telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="RadTextBoxTitle"
                                                    Display="Dynamic" runat="server" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="text-align: center; padding: 8px 8px;">
                                                <asp:Button ID="ButtonSave" CssClass="button" Text='<%# (Container is GridEditFormInsertItem) ? "保 存" : "更 新" %>'
                                                    runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'>
                                                </asp:Button>&nbsp;
                                                <asp:Button ID="Button2" CssClass="button" Text="取 消" runat="server" CausesValidation="False"
                                                    CommandName="Cancel"></asp:Button>
                                            </td>
                                        </tr>
                                    </table>
                                </FormTemplate>
                            </EditFormSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="width: 98%; margin: 15px auto; clear: both; line-height: 22px;">
                    <div style="float: left; text-align: left; font-size: 12px;">
                        导入大纲：</div>
                    <div style="float: left; text-align: left; vertical-align: middle;width:270px">
                        <telerik:RadUpload ID="RadUploadFile" runat="server" InitialFileInputsCount="1" AllowedFileExtensions="xls"
                            ControlObjectsVisibility="None" MaxFileInputsCount="1" MaxFileSize="1073741824"
                            Width="220px" Enabled="true" Skin="Hot" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false">
                            <Localization Select="选择文件" />
                        </telerik:RadUpload>
                        <asp:CustomValidator ID="Customvalidator1" runat="server" Display="Dynamic" ClientValidationFunction="validateRadUploadTaboe"
                            ErrorMessage="只能上传扩展名为xls的Excel文件！"> </asp:CustomValidator>
                    </div>
                    <div style="float: left; text-align: left;">
                        &nbsp;&nbsp;
                        <asp:Button ID="ButtonImport" runat="server" Text="导 入" CssClass="bt_large" OnClick="ButtonImport_Click"
                            OnClientClick="javascript:if(confirm('导入大纲将清除该科目下现有内容，是否确定要导入？')==false) return false;"
                            Enabled="true" />&nbsp; <a target="_blank" style="color: Blue; font-size: 12px;"
                                href="..\Template\知识大纲导入模版.xls">《下载大纲导入模版.xls》</a>
                    </div>
                    <div style="float: right; padding-right: 20px;">
                        <asp:Button ID="ButtonExportToExcel" CssClass="bt_large" Text="导出大纲列表" OnClick="ButtonExportToExcel_Click"
                            runat="server"></asp:Button>
                    </div>
                    <div style="clear:both;"></div>
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
