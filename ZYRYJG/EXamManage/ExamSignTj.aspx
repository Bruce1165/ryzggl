<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ExamSignTj.aspx.cs"
    MasterPageFile="~/RadControls.Master" Inherits="ZYRYJG.EXamManage.ExamSignTj" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../ExamPlanSelect.ascx" TagName="ExamPlanSelect" TagPrefix="uc1" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {

                if (args.get_eventTarget().indexOf("ButtonExport") >= 0) {
                    args.set_enableAjax(false);
                }
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true" DefaultLoadingPanelID="RadAjaxLoadingPanel1">
        <ClientEvents OnRequestStart="onRequestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DivList" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonExport">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ButtonExport" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" OnClientClose="OnClientClose"
        Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                报名管理 &gt;&gt; <strong>报名审核统计</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx">
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">考试计划：
                    </td>
                    <td align="left">
                        <uc1:ExamPlanSelect ID="ExamPlanSelect1" runat="server" />
                    </td>
                    <td width="11%" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>
            <div id="DivList" runat="server">
                <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    考试报名列表
                </div>
                <div style="width: 98%; margin: 0 auto; padding-bottom: 10px;">
                    <telerik:RadGrid ID="RadGrid1" runat="server" Width="100%"
                        AllowSorting="False" AllowPaging="False" GridLines="None" AutoGenerateColumns="False"
                        Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" OnExcelExportCellFormatting="RadGrid1_ExcelExportCellFormatting">

                        <MasterTableView NoMasterRecordsText="　没有可显示的记录">
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>

                                <telerik:GridBoundColumn UniqueName="S_TRAINUNITNAME" DataField="S_TRAINUNITNAME" HeaderText="报名点">
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="CHECKDATEPLAN" DataField="CHECKDATEPLAN" HeaderText="初审日期"
                                    HtmlEncode="false" DataFormatString="{0:yyyy.MM.dd}">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ManCount" DataField="ManCount" HeaderText="报名<br/>人数">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="NeedManCheckCount" DataField="NeedManCheckCount"
                                    HeaderText="需现场<br/>初审人数">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="已初审人数">
                                    <ItemTemplate>

                                        <%#  string.Format("已初审{0}人，其中现场审核{1}人，自动审核人{2}（{3}{4}{5}{6}）",Eval("CheckedCount"),Eval("ManCheckCount"),Eval("SystemCheckCount")
                                            ,Convert.ToInt32(Eval("ZJSCheckCount")) >0?string.Format("建造师：{0}人，",Eval("ZJSCheckCount")):""
                                             ,Convert.ToInt32(Eval("ExamCheckCount")) >0?string.Format("最近参考：{0}人，",Eval("ExamCheckCount")):""
                                              ,Convert.ToInt32(Eval("SheBaoCheckCount")) >0?string.Format("社保符合：{0}人",Eval("SheBaoCheckCount")):""
                                               ,Convert.ToInt32(Eval("FRCheckCount")) >0?string.Format("法人符合：{0}人",Eval("FRCheckCount")):""
                                            ) %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="JWCheckCount" DataField="JWCheckCount" HeaderText="建委审<br/>核人数">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PayCount" DataField="PayCount" HeaderText="缴费确<br/>认人数">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>


                            </Columns>
                            <HeaderStyle Font-Bold="True" />

                        </MasterTableView>
                        <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                        <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                    </telerik:RadGrid>

                </div>
                <div style="width: 95%; margin: 0 auto; text-align: center; padding-bottom: 20px;">
                    <asp:Button ID="ButtonExport" runat="server" Text="导出结果" CssClass="bt_large" OnClick="ButtonExport_Click" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
