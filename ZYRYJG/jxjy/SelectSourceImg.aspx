<%@ Page Title="考试课程北京图片" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="SelectSourceImg.aspx.cs" Inherits="ZYRYJG.jxjy.SelectSourceImg" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <style type="text/css">
          .simg {
              width: 400px;
              height: 150px;
              background-size: cover;
              background-position: left top;
              background-repeat: no-repeat;
              margin: 20px 20px;
              float: left;
          }
      </style>
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function returnToParent(img) {
                var oArg = new Object();
                oArg.SourceImg = img;
                  var oWnd = GetRadWindow();
                oWnd.close(oArg);
            }
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManagerExamPlanSearch" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanelExamPlanSearch"
        EnableAJAX="true">
        <AjaxSettings>
          <%--  <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridSource" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="RadGridSource">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridSource" />
                </UpdatedControls>
            </telerik:AjaxSetting>    --%>       
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanelExamPlanSearch" runat="server"
        Visible="true" Skin="Windows7" />
    <div class="content">
        <%--<table class="cx" width="98%" border="0" align="center" cellspacing="5">
            <tr>
                <td align="right" nowrap="nowrap" width="11%">课程名称：
                </td>
                <td align="left" width="39%">
                    <telerik:RadTextBox ID="RadTextBoxSourceName" runat="server" Width="90%" Skin="Default"
                        onkeydown="ButtonSearchClick(event);">
                    </telerik:RadTextBox>
                </td>
                <td align="right" width="11%" nowrap="nowrap" valign="middle">授课教师：
                </td>
                <td align="left" width="39%">
                    <telerik:RadTextBox ID="RadTextBoxTeacher" runat="server" Width="90%" Skin="Default"
                        onkeydown="ButtonSearchClick(event);">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td align="right" width="11%" nowrap="nowrap" valign="middle">年 度：
                </td>
                <td align="left" width="39%">
                    <telerik:RadComboBox ID="RadComboBoxSourceYear" runat="server">
                    </telerik:RadComboBox>
                </td>
                <td align="right" width="11%" nowrap="nowrap">类型：
                </td>
                <td align="left" width="39%">
                    <asp:RadioButtonList ID="RadioButtonListSourceType" runat="server" RepeatDirection="Horizontal"
                        AutoPostBack="false">
                        <asp:ListItem Value="" Selected="True">全部</asp:ListItem>
                        <asp:ListItem Value="必修">必修</asp:ListItem>
                        <asp:ListItem Value="选修">选修</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                </td>
            </tr>
        </table>--%>
        <div class="table_cx" style="padding-top: 2px;">
            提示：单击选择一个图片。<input id="Button1" type="button" value="让系统随即选择图片" onclick="returnToParent('')" class="bt_maxlarge" />
        </div>
        <div id="divImgSet" runat="server" style="min-height:500px;">

        </div>
       <%-- <div style="width: 98%; margin: 0 auto;">
            <telerik:RadGrid ID="RadGridSource" AutoGenerateColumns="False"
                runat="server" AllowPaging="True" PageSize="10" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序"
                Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="100%" PagerStyle-AlwaysVisible="true"
                GridLines="None" OnItemDataBound="RadGridSource_ItemDataBound">
                <ClientSettings EnableRowHoverStyle="true">
                </ClientSettings>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="SourceID,SourceName"
                    NoMasterRecordsText="　没有可显示的记录">
                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                    <Columns>
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
                        <telerik:GridTemplateColumn HeaderText="课件总时长" UniqueName="Period">
                            <ItemTemplate>
                                <%# Convert.ToInt32(Eval("Period")) / 60 == 0 ? "" : string.Format("{0}小时", Convert.ToString(Convert.ToInt32(Eval("Period")) / 60))%><%# string.Format("{0}分", Convert.ToString(Convert.ToInt32(Eval("Period")) % 60))%>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Right" Wrap="false" />
                            <ItemStyle HorizontalAlign="Right" Wrap="false" />
                        </telerik:GridTemplateColumn>
                        <telerik:GridBoundColumn HeaderText="课件数" UniqueName="SourceWareCount" DataField="SourceWareCount">
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridBoundColumn>
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
                    </Columns>
                    <HeaderStyle Font-Bold="True" />
                    <PagerTemplate>
                        <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                    </PagerTemplate>
                    <ItemStyle Height="16px" />
                    <AlternatingItemStyle Height="16px" />
                </MasterTableView>
            </telerik:RadGrid>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.SourceDAL"
                DataObjectTypeName="Model.SourceOB" SelectMethod="GetList" EnablePaging="true"
                SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                SortParameterName="orderBy">
                <SelectParameters>
                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                        DefaultValue="" ConvertEmptyStringToNull="false" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>--%>
    </div>
    <br />
</asp:Content>
