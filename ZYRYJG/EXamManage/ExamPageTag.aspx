<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ExamPageTag.aspx.cs" Inherits="ZYRYJG.EXamManage.ExamPageTag" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">

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
                if (mousePosition(window.event).x > 360) {
                    hideTree();
                };
            }
            function hideTree() {
                var tree = document.getElementById('div_tree');
                var img = document.getElementById('img_find');
                tree.style.display = 'none';
                img.style.display = 'inline';
            }
        </script>
    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
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
                考场管理 &gt;&gt; <strong>考试卷标</strong>
            </div>
        </div>
        <div id="DivMain" style="width: 100%; padding: 0; display: block;" runat="server">
            <div id="div_tree" class="table_border" onmousemove="javascript:mousemove();" style="display: inline;
                position: absolute; float: left; text-align: left; width: 345px; overflow: hidden;
                background-color: White; left: 0px; padding: 8px 8px 50px 8px;">
                <div style="text-align: left; font-weight: bold; font-size: 12px; width: 100%">
                    请选择考试计划（或日期）</div>
                <telerik:RadTreeView ID="RadTreeViewExamPlan" runat="server" Skin="Windows7" OnNodeExpand="RadTreeViewExamPlan_NodeExpand"
                    OnNodeClick="RadTreeViewExamPlan_NodeClick">
                </telerik:RadTreeView>
            </div>
            <div id="img_find" style="background: url(../Images/bar1.png) no-repeat left top;
                position: absolute; left: 0px; color: White; width: 50px; height: 150px; border: none;
                display: none;" onmouseover="javascript:showTree();">
            </div>
            <div class="jbxxbt">
                考试卷标</div>
            <div id="div_grid" style="display: block; padding-left: 66px; height: 100%" onmousemove="javascript:hideTree();">
                <div class="DivContent" id="div_tip" runat="server" style="background-image: url(../Images/mf.png);
                    background-position: 20px center; background-repeat: no-repeat; padding: 20px 20px 20px 170px;
                    line-height: 22px; width: 60%">
                    <br />
                    <b>使用说明：</b>
                    <br />
                    卷标用途：用于在每个考场试卷档案袋上标注本场考试信息及试卷数量。<br />
                    可以按照“考试日期”或“考试计划”生成卷标导出打印。<br />
                    从左侧考试计划列表中选择一个考试日期或考试计划查询出数据，点击“导出打印”，保存word文档到本地进行打印；<br />
                </div>
                <div style="padding: 10px 20px; text-align: right; font-size: 12px;">
                    <asp:Button ID="ButtonPrint" runat="server" Text="导出打印" CssClass="bt_large" OnClick="ButtonExport_Click"
                        Visible="false" />
                </div>
                <div style="width: 98%; margin: 0 auto;">
                    <telerik:RadGrid ID="RadGridExamResult" AutoGenerateColumns="False" runat="server"
                        Visible="false" AllowPaging="false" AllowSorting="false" Skin="Blue" EnableAjaxSkinRendering="false"
                        EnableEmbeddedSkins="false" Width="100%" GridLines="None" OnDataBound="RadGridExamResult_DataBound">
                        <MasterTableView CommandItemDisplay="None" DataKeyNames="" NoMasterRecordsText="　没有可显示的记录">
                            <Columns>
                                 <telerik:GridTemplateColumn UniqueName="PostName" HeaderText="岗位工种">
                                        <ItemTemplate>
                                            <%# Eval("PostName")%><%# Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("暖通") ? "（暖通）":
                                                   Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("电气") ? "（电气）" : ""%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="Subject" DataField="Subject" HeaderText="科目">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="考试时间">
                                    <ItemTemplate>
                                        <nobr><asp:Literal runat="server" ID="ExamTime" Text='<%# Convert.ToDateTime(Eval("ExamStartDate")).ToString()==Convert.ToDateTime(Eval("RoomExamStartTime")).ToString()?Convert.ToDateTime(Eval("ExamStartTime")).ToString("yyyy年MM月dd日 HH:mm -")+ Convert.ToDateTime(Eval("ExamEndTime")).ToString("HH:mm"):Convert.ToDateTime(Eval("RoomExamStartTime")).ToString("yyyy年MM月dd日 HH:mm -")+ Convert.ToDateTime(Eval("RoomExamEndTime")).ToString("HH:mm")%>' />
                                       </nobr>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="ExamPlaceName" DataField="ExamPlaceName" HeaderText="考点名称">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ExamRoomCode" DataField="ExamRoomCode" HeaderText="考场">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PersonNumber" DataField="PersonNumber" HeaderText="人数">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle Font-Bold="True" />
                        </MasterTableView>
                    </telerik:RadGrid>
                </div>
                <div style="padding: 10px 0px; text-align: center; font-size: 12px;">
                    <asp:Button ID="ButtonPrint1" runat="server" Text="导出打印" CssClass="bt_large" OnClick="ButtonExport_Click"
                        Visible="false" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
