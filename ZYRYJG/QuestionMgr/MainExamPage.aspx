<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="MainExamPage.aspx.cs" Inherits="ZYRYJG.QuestionMgr.MainExamPage" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
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
                题库管理 &gt;&gt; <strong>试卷管理</strong>
            </div>
        </div>
        <div id="DivMain" style="width: 100%; padding: 0; display: block;" runat="server">
            <div class="jbxxbt">
                试卷管理
            </div>

            <div class="DivContent" id="div_tip" runat="server" >
                <b>使用说明：</b>
                <br />
                1、请首先定义试卷参数，保存后再进行组卷，系统会随机出题。
                    <br />
                2、试卷难度根据组卷结果自动计算，难度范围1~3之间，保留两位小数。数值越大表示难度越高。
                    <br />
            </div>
            <div style="width: 98%; margin: 0 auto; font-size: 12px; padding-left: 20px; color: #DC2804; font-weight: bold; text-align: left; line-height: 40px;">
                <div style="width: 300px; text-align: left; float: left;">
                    <span style="color: Black">过滤条件</span> &nbsp; &nbsp; &nbsp; &nbsp; 年度：
                        <telerik:RadComboBox ID="RadComboBoxYear" runat="server" Skin="Office2007" CausesValidation="False"
                            Width="80px" ExpandAnimation-Duration="0" DataTextField="ExamYear" DataTextFormatString="{0}年"
                            DataValueField="ExamYear" AutoPostBack="True" OnSelectedIndexChanged="RadComboBoxYear_SelectedIndexChanged">
                        </telerik:RadComboBox>
                    &nbsp; &nbsp; &nbsp; &nbsp;岗位工种：
                </div>

                <div style="width: 350px; text-align: left; float: left;">
                    <uc1:PostSelect ID="PostSelect" runat="server" style="float: left;"
                        OnPostSelectChange="PostSelect_SelectChange" />
                </div>


            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGridPost" runat="server" Width="100%" AllowSorting="false"
                    AllowPaging="true" GridLines="None" AutoGenerateColumns="False" Skin="Blue"
                    EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False"
                    OnDeleteCommand="RadGridPost_DeleteCommand">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView CommandItemDisplay="Top" DataKeyNames="ExamPageID,POSTTYPEID,POSTID" NoMasterRecordsText="　没有可显示的记录" PagerStyle-AlwaysVisible="true">
                        <CommandItemTemplate>
                            <div class="grid_CommandBar">
                                <input type="button" value=" " class="rgAdd" onclick="javascript: SetIfrmSrc('ExamPageEdit.aspx');" />
                                <nobr onclick="javascript:SetIfrmSrc('ExamPageEdit.aspx');" class="grid_CmdButton">
                                        新建试卷</nobr>
                            </div>
                        </CommandItemTemplate>
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="ExamYear" DataField="ExamYear" HeaderText="年度">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="80px" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="POSTNAME" DataField="POSTNAME" HeaderText="岗位工种">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="SubjectName" DataField="SubjectName" HeaderText="科目">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="ExamPageTitle" DataField="ExamPageTitle" HeaderText="试卷名称">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Difficulty" DataField="Difficulty" HeaderText="难度系数">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                             <telerik:GridTemplateColumn UniqueName="Compare" HeaderText="比较">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("ExamPageCompare.aspx?o=<%# Eval("ExamPageID") %>&p=<%# Eval("POSTID") %>");'>比较</span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Print" HeaderText="打印预览">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("ExamPagePrint.aspx?o=<%# Eval("ExamPageID") %>");'>打印预览</span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" Width="92px" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="编辑">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("ExamPageEdit.aspx?o=<%# Eval("ExamPageID") %>&ptid=<%# Eval("POSTTYPEID") %>&pid=<%# Eval("POSTID") %>");'>编辑</span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridButtonColumn HeaderText="删除" UniqueName="Delete" CommandName="Delete" Text="&lt;span onclick=&quot;if(!confirm('您确定要删除么?'))return false; &quot; &gt;删除&lt;/span&gt;">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Width="50px" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridButtonColumn>
                            <%-- <telerik:GridTemplateColumn UniqueName="Flag" HeaderText="状态">
                                    <ItemTemplate>
                                        <%#Eval("Flag").ToString()=="0"?"未发布":"已发布"%></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>--%>
                            <%--  <telerik:GridBoundColumn UniqueName="Flag" DataField="Flag" HeaderText="状态">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" Width="100px" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>--%>
                        </Columns>
                        <HeaderStyle Font-Bold="True" />
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.ExamPageDAL"
                    DataObjectTypeName="Model.ExamPageOB" SelectMethod="GetListView" EnablePaging="true"
                    SelectCountMethod="SelectViewCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
    </div>

    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
