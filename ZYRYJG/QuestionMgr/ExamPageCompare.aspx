<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ExamPageCompare.aspx.cs" Inherits="ZYRYJG.QuestionMgr.ExamPageCompare" %>

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
        <div id="div_top" class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                题库管理 &gt;&gt; <strong>试卷管理</strong>
            </div>
        </div>
        <div id="DivMain" runat="server" class="table_border" style="width: 98%; margin: 5px auto;padding:20px 0 40px 0;">
            <div class="jbxxbt">
                试卷对比
            </div>
            <div id="div_grid" style="display: block; width: 95%; height: 100%;">
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    1、请选择一个要比对的试卷
                </div>
                <div style="width: 98%; margin: 0 auto; font-size: 12px; padding-left: 20px; color: #DC2804; font-weight: bold; text-align: left; line-height: 40px;">
                    <div style="width: 300px; text-align: left; float: left;">
                        <span style="color: Black">过滤条件</span> &nbsp; &nbsp; &nbsp; &nbsp; 年度：
                        <telerik:RadComboBox ID="RadComboBoxYear" runat="server" Skin="Office2007" CausesValidation="False"
                            Width="90px" ExpandAnimation-Duration="0" DataTextField="ExamYear" DataTextFormatString="{0}年"
                            DataValueField="ExamYear" AutoPostBack="True" OnSelectedIndexChanged="RadComboBoxYear_SelectedIndexChanged">
                        </telerik:RadComboBox>                    
                    </div>
                </div>
                <div style="width: 98%; margin: 0 auto; padding-bottom: 10px;">
                    <telerik:RadGrid ID="RadGridPost" runat="server" Width="100%" AllowSorting="false"
                        AllowPaging="true" GridLines="None" AutoGenerateColumns="False" Skin="Blue" PageSize="5"
                        EnableAjaxSkinRendering="False" EnableEmbeddedSkins="False">
                        <ClientSettings EnableRowHoverStyle="false" Selecting-AllowRowSelect="true">
                        </ClientSettings>
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="ExamPageID" NoMasterRecordsText="　没有可显示的记录" PagerStyle-AlwaysVisible="true">
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
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    2、开始比对 
                        <asp:Button ID="ButtonCompare" CssClass="bt_large" Text="开始比对" OnClick="ButtonCompare_Click"
                            runat="server"></asp:Button>
                </div>
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    3、比对结果 <asp:Label runat="server" ID="LabelResult" ForeColor="#333" Font-Bold="true" Font-Size="14px" style="padding-left:8px;"></asp:Label>
                </div>
              
            </div>
        </div>
    </div>

    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
