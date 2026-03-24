<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="MainExamOutline.aspx.cs" Inherits="ZYRYJG.QuestionMgr.MainExamOutline" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
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
                题库管理 &gt;&gt; <strong>考试大纲发布</strong>
            </div>
        </div>
        <div id="DivMain" style="width: 100%; padding: 0; display: block;" runat="server">
            <div class="jbxxbt">
                考试大纲发布
            </div>
            <div id="div_grid" style="display: block; width: 95%; height: 100%;">
                <div class="DivContent" id="div_tip" runat="server" style="margin-top: 10px; padding: 20px 20px 20px 20px; line-height: 18px; width: 90%">
                    <b>使用说明：</b>
                    <br />
                    1、每年从知识大纲中确定考试范围、出题权重，发布后系统按考试大纲出题。
                    <br />
                    2、修改的知识大纲不影响已发布的考试大纲，如果希望将修改应用到当年考试大纲中，需要重新生成考试大纲。
                    <br />
                    3、删除考试大纲条目不影响知识大纲条目的存在。
                    <br />
                </div>
                <div style="width: 98%; margin: 0 auto; font-size: 12px; padding-left: 20px; color: #DC2804; font-weight: bold; text-align: left; line-height: 40px;">
                    <div style="width: 300px; text-align: left; float: left;">
                        <span style="color: Black">过滤条件</span> &nbsp; &nbsp; &nbsp; &nbsp; 年度：
                        <telerik:RadComboBox ID="RadComboBoxYear" runat="server" Skin="Office2007" CausesValidation="False"
                            Width="70px" ExpandAnimation-Duration="0" DataTextField="ExamYear" DataTextFormatString="{0}年"
                            DataValueField="ExamYear" AutoPostBack="True" OnSelectedIndexChanged="RadComboBoxYear_SelectedIndexChanged">
                        </telerik:RadComboBox>
                        &nbsp; &nbsp; &nbsp; &nbsp;岗位工种：
                    </div>

                    <div style="width: 370px; text-align: left; float: left;">
                        <uc1:PostSelect ID="PostSelect" runat="server" style="float: left;" OnPostTypeSelectChange="PostTypeSelect_SelectChange"
                            OnPostSelectChange="PostSelect_SelectChange" />
                    </div>
                    <div style="width: 100px; text-align: left; float: left; padding-top:8px; ">
                        <asp:Button ID="ButtonNew" runat="server" Text="新建考试大纲" CssClass="bt_large" OnClick="ButtonNew_Click" />
                    </div>

                </div>
                <div style="width: 98%; margin: 0 auto;">
                    <telerik:RadGrid ID="RadGridPost" runat="server" Width="100%" AllowSorting="false"
                        AllowPaging="false" GridLines="None" AutoGenerateColumns="False" Skin="Blue"
                        EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False" OnDetailTableDataBind="RadGridPost_DetailTableDataBind">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="SubjectID" NoMasterRecordsText="　没有可显示的记录">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="ExamYear" DataField="ExamYear" HeaderText="年度">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="80px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="250px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="POSTNAME" DataField="POSTNAME" HeaderText="岗位工种">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="250px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="SubjectName" DataField="SubjectName" HeaderText="科目">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="150px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="Flag" HeaderText="状态">
                                    <ItemTemplate>
                                        <%#Eval("Flag").ToString()=="0"?"未发布":"已发布"%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <%--  <telerik:GridBoundColumn UniqueName="Flag" DataField="Flag" HeaderText="状态">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>--%>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                            <DetailTables>
                                <telerik:GridTableView Name="RadGridInfoTag" DataKeyNames="TagID" AutoGenerateColumns="False"
                                    runat="server" AllowPaging="false" AllowSorting="false" EnableAjaxSkinRendering="false"
                                    EnableEmbeddedSkins="false" Width="100%" GridLines="None" EditMode="EditForms"
                                    CommandItemDisplay="none" NoMasterRecordsText="　没有可显示的记录">
                                    <ParentTableRelation>
                                        <telerik:GridRelationFields DetailKeyField="SubjectID" MasterKeyField="SubjectID" />
                                    </ParentTableRelation>
                                    <Columns>
                                        <telerik:GridBoundColumn UniqueName="ShowCode" DataField="ShowCode" HeaderText="序号">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="Title" DataField="Title" HeaderText="大纲内容">
                                            <HeaderStyle HorizontalAlign="Left" Wrap="false" Font-Bold="true" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn UniqueName="Weight" DataField="Weight" HeaderText="权重">
                                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                        </telerik:GridBoundColumn>
                                    </Columns>
                                    <EditFormSettings CaptionFormatString="编辑知识大纲" EditFormType="Template" PopUpSettings-Modal="true"
                                        FormCaptionStyle-HorizontalAlign="Left">
                                        <EditColumn UniqueName="EditCommandColumn1">
                                        </EditColumn>
                                        <FormCaptionStyle HorizontalAlign="Left"></FormCaptionStyle>
                                        <FormTemplate>
                                            <table width="100%" border="0" align="center" cellspacing="8">
                                                <tr>
                                                    <td align="left">
                                                        <span style="color: #969696; font-weight: bold; padding-left: 50px; line-height: 30px;">层级间用小数点表示，例如：1.1.1
                                                            <br />
                                                        </span>序号：
                                                        <telerik:RadTextBox ID="RadTextBoxShowCode" runat="server" Width="80%" Skin="Default"
                                                            Text='<%# Eval("ShowCode") %>' MaxLength="50">
                                                        </telerik:RadTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="RadTextBoxShowCode"
                                                            Display="Dynamic" runat="server" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="格式不正确！"
                                                            Display="Dynamic" ValidationExpression="\d(.\d)*" ControlToValidate="RadTextBoxShowCode"></asp:RegularExpressionValidator>
                                                        <asp:HiddenField ID="HiddenFieldTagID" runat="server" Value='<%# Eval("TagID")%>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="top">内容：
                                                        <telerik:RadTextBox ID="RadTextBoxTitle" runat="server" Width="80%" Skin="Default"
                                                            Text='<%# Eval("Title") %>' MaxLength="1000" TextMode="MultiLine" Rows="3">
                                                        </telerik:RadTextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="RadTextBoxTitle"
                                                            Display="Dynamic" runat="server" ErrorMessage="必填"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center; padding: 8px 8px;">
                                                        <asp:Button ID="ButtonSave" CssClass="button" Text='<%# (Container is GridEditFormInsertItem) ? "保 存" : "更 新" %>'
                                                            runat="server" CommandName='<%# (Container is GridEditFormInsertItem) ? "PerformInsert" : "Update" %>'></asp:Button>&nbsp;
                                                        <asp:Button ID="Button2" CssClass="button" Text="取 消" runat="server" CausesValidation="False"
                                                            CommandName="Cancel"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </FormTemplate>
                                    </EditFormSettings>
                                </telerik:GridTableView>
                            </DetailTables>
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="width: 95%; margin: 15px auto; clear: both; line-height: 22px;">
                    <%--  <div style="float: right; padding-right: 20px;">
                        <asp:Button ID="ButtonExportToExcel" CssClass="bt_large" Text="导出大纲列表" OnClick="ButtonExportToExcel_Click"
                            runat="server"></asp:Button>
                    </div>--%>
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

        </script>
    </telerik:RadCodeBlock>
</asp:Content>
