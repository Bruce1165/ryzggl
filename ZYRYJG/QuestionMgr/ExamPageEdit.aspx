<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ExamPageEdit.aspx.cs" Inherits="ZYRYJG.QuestionMgr.ExamPageEdit" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" />
    <div class="div_out">
        <div id="div_top" class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                题库管理 &gt;&gt; 试卷管理 &gt;&gt; <strong>试卷编辑</strong>
            </div>
        </div>
        <div class="table_border" style="width: 98%; margin: 5px auto;">
            <div class="content">
                <%-- <p class="jbxxbt">
                    试卷编辑
                </p>--%>
                <div style="text-align: left; padding-bottom: 8px;">
                    <telerik:RadTabStrip ID="RadTabStrip1" runat="server" OnTabClick="RadTabStrip1_TabClick"
                        ScrollChildren="true" ShowBaseLine="true" PerTabScrolling="true" Align="Left"
                        CausesValidation="false" Width="98%" Skin="Default">
                        <Tabs>
                            <telerik:RadTab Text="试卷定义" Value="试卷定义" Selected="true">
                            </telerik:RadTab>
                            <telerik:RadTab Text="随机组卷" Value="随机组卷">
                            </telerik:RadTab>
                        </Tabs>
                    </telerik:RadTabStrip>
                </div>

                <div class="DivContent" style="width: 98%; margin: 0 auto;" id="divExamPage" runat="server">
                    <div style="width: 98%; margin: 0 auto; font-size: 12px; padding-left: 20px; font-weight: bold; text-align: left; line-height: 30px;">
                        <div style="width: 70px; text-align: left; float: left;">
                            岗位工种：
                        </div>
                        <div style="width: 360px; text-align: left; float: left;">
                            <uc1:PostSelect ID="PostSelect" runat="server" OnPostSelectChange="PostSelect_OnPostSelectChange" />

                        </div>
                        <div style="width: 46px; text-align: left; float: left;">
                            科目：
                        </div>
                        <div style="text-align: left; float: left; line-height: 22px;">
                            <asp:RadioButtonList ID="RadioButtonListKeMuID" runat="server" DataTextField="SUBJECTNAME"
                                DataValueField="SUBJECTID" RepeatDirection="Horizontal">
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadioButtonListKeMuID" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div style="width: 98%; margin: 0 auto; font-size: 12px; padding-left: 20px; font-weight: bold; text-align: left; line-height: 30px;">
                        <div style="width: 150px; text-align: left; float: left;">
                            考试年度：
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxYear" runat="server" MaxLength="4"
                                MinValue="2014" MaxValue="2030" Type="Number" NumberFormat-DecimalDigits="0"
                                ShowSpinButtons="true" Width="70px">
                                <NumberFormat DecimalDigits="0" GroupSeparator=""></NumberFormat>
                            </telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadNumericTextBoxYear" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div style="width: 500px; text-align: left; float: left;">
                            试卷名称：
                            <telerik:RadTextBox ID="RadTextBoxExamPageTitle" runat="server" MaxLength="100" Width="350px">
                            </telerik:RadTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadTextBoxExamPageTitle" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div style="width: 98%; margin: 0 auto; font-size: 12px; padding-left: 20px; font-weight: bold; text-align: left; line-height: 30px;">
                        <div style="width: 180px; text-align: left; float: left;">
                            考试时长：
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxTimeLimit" runat="server" MaxLength="3"
                                Value="120" Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true"
                                Width="70px">
                                <NumberFormat DecimalDigits="0"></NumberFormat>
                            </telerik:RadNumericTextBox>(分钟)
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadNumericTextBoxTimeLimit" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div style="width: 150px; text-align: left; float: left; padding-left: 40px;">
                            试卷总分：
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxScore" runat="server" MaxLength="3"
                                Value="100" Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true"
                                Width="70px">
                                <NumberFormat DecimalDigits="0"></NumberFormat>
                            </telerik:RadNumericTextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidatorScore" runat="server" ErrorMessage="必填"
                                ControlToValidate="RadNumericTextBoxScore" Display="Dynamic"></asp:RequiredFieldValidator>
                        </div>
                        <div style="width: 240px; text-align: left; float: left;">
                            试卷难度：
                            <telerik:RadNumericTextBox ID="RadNumericTextBoxDifficulty" runat="server" MaxLength="3"
                                Enabled="false" Type="Number" NumberFormat-DecimalDigits="2" ShowSpinButtons="true"
                                Width="70px" BackColor="#F3F3F3">
                            </telerik:RadNumericTextBox>(系统自动计算)
                        </div>
                    </div>
                    <div style="width: 98%; margin: 0 auto; font-size: 12px; padding-left: 20px; padding-bottom: 8px; font-weight: bold; text-align: left; line-height: 30px;">
                        <%-- <div style="width: 600px; text-align: left; float: left;">--%>
                            答题说明：
                            <telerik:RadTextBox ID="RadTextBoxRemark" runat="server" MaxLength="500" Width="90%"
                                TextMode="MultiLine" Rows="4">
                            </telerik:RadTextBox>
                        <%-- </div>--%>
                    </div>

                    <telerik:RadGrid ID="RadGridQuestionType" runat="server" AutoGenerateColumns="false"
                        Width="100%">
                        <ClientSettings EnableRowHoverStyle="false">
                            <Selecting AllowRowSelect="false" />
                        </ClientSettings>
                        <MasterTableView NoMasterRecordsText="　没有可显示的记录" DataKeyNames="ExamPageID,TYPENAME,ShowOrder,QuestionCount,Score,Remark">
                            <Columns>
                                <telerik:GridTemplateColumn UniqueName="Select" HeaderText="是否出题">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="65" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="TYPENAME" DataField="TYPENAME" HeaderText="试题类型">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="65" />
                                    <ItemStyle HorizontalAlign="Center" Font-Bold="true" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="ShowOrder" HeaderText="出题排序">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxShowOrder" runat="server" MaxLength="2"
                                            Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" MinValue="1"
                                            MaxValue="10" Width="70px">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="65" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="QuestionCount" HeaderText="试题数量">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxQuestionCount" runat="server" MaxLength="3"
                                            Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" MinValue="1"
                                            MaxValue="200" Width="70px">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="65" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="Score" HeaderText="每题分值">
                                    <ItemTemplate>
                                        <telerik:RadNumericTextBox ID="RadNumericTextBoxScore" runat="server" MaxLength="2"
                                            Type="Number" NumberFormat-DecimalDigits="0" ShowSpinButtons="true" MinValue="1"
                                            MaxValue="100" Width="70px">
                                        </telerik:RadNumericTextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Width="80" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="Remark" HeaderText="答题说明">
                                    <ItemTemplate>
                                        <telerik:RadTextBox ID="RadTextBoxRemark" runat="server" MaxLength="500" Width="98%" Text='<%# Eval("Remark") == DBNull.Value?Eval("TYPEVALUE"):Eval("Remark") %>'
                                           >
                                        </telerik:RadTextBox>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="true" />
                        </MasterTableView>
                    </telerik:RadGrid>
                    <div style="width: 98%; margin: 0 auto; font-size: 12px; padding-left: 20px; font-weight: bold; text-align: center; line-height: 40px;">
                        <asp:Button ID="btnSave" runat="server" Text="保 存" CssClass="bt_large" OnClick="btnSave_Click" />&nbsp;&nbsp;
                        <input id="ButtonReturn" type="button" value="返 回" class="bt_large" onclick="javascript: hideIfam();" />
                    </div>
                </div>
                <div class="DivContent" style="width: 98%; margin: 0 auto;" id="divQuestion" runat="server"
                    visible="false">

                    <div style="width: 98%; margin: 0 auto; font-size: 12px; padding-left: 20px; font-weight: bold; text-align: left; line-height: 30px;">
                        <asp:Button ID="ButtonCreateQuestion" runat="server" Text="随机出题" CssClass="bt_large"
                            OnClick="ButtonCreateQuestion_Click" />
                    </div>
                    <telerik:RadGrid ID="RadGridQuestion" runat="server" Width="100%" AllowSorting="false"
                        AllowPaging="false" GridLines="None" AutoGenerateColumns="false" Skin="Blue"
                        EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False">
                        <ClientSettings EnableRowHoverStyle="false">
                            <Selecting AllowRowSelect="false" />
                            <Scrolling AllowScroll="true" ScrollHeight="360" />
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="None" EditMode="EditForms" DataKeyNames="QuestionID"
                            NoMasterRecordsText="　没有可显示的记录">
                            <Columns>
                                <telerik:GridBoundColumn UniqueName="QuestionNo" DataField="QuestionNo" HeaderText="题号">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="QuestionType" DataField="QuestionType" HeaderText="试题类型">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="Title" DataField="Title" HeaderText="题目">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>

                                <%-- <telerik:GridBoundColumn UniqueName="TagCode" DataField="TagCode" HeaderText="大纲编号">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="90px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>--%>
                                <telerik:GridTemplateColumn UniqueName="Difficulty" HeaderText="难度">
                                    <ItemTemplate>
                                        <%# Eval("Difficulty").ToString()=="1"?"易":Eval("Difficulty").ToString()=="2"?"中":Eval("Difficulty").ToString()=="3"?"难":""%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="50px" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                        </MasterTableView>
                    </telerik:RadGrid>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
