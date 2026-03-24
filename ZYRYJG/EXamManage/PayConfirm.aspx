<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/RadControls.Master"
    CodeBehind="PayConfirm.aspx.cs" Inherits="ZYRYJG.EXamManage.PayConfirm" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../ExamPlanSelect.ascx" TagName="ExamPlanSelect" TagPrefix="uc4" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/timelimit.css?v=1.001" rel="stylesheet" type="text/css" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript" src="../Scripts/timelimit.js?v=1.003"></script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="ButtonComfirm">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableEmbeddedScripts="true" OnClientClose="OnClientClose"
        Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                报名管理 &gt;&gt; <strong>审核通过确认</strong>
            </div>
        </div>
        <div class="content">
            <table class="bar_cx">
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">考试计划：
                    </td>
                    <td align="left" colspan="3">
                        <uc4:ExamPlanSelect ID="ExamPlanSelect1" runat="server" />
                    </td>
                </tr>              
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">岗位工种：
                    </td>
                    <td align="left" width="38%">
                         <uc1:PostSelect ID="PostSelect2" runat="server" />
                    </td>
                    <td width="11%" align="right" nowrap="nowrap">考试时间：
                    </td>
                    <td align="left" width="38%">
                        <telerik:RadComboBox ID="RadComboBoxYear" runat="server" Skin="Office2007" CausesValidation="False"
                            Width="60px" ExpandAnimation-Duration="0">
                        </telerik:RadComboBox>
                        &nbsp;年&nbsp;
                        <telerik:RadComboBox ID="RadComboBoxMonth" runat="server" Skin="Office2007" CausesValidation="False"
                            Width="60px" ExpandAnimation-Duration="0">
                            <Items>
                                <telerik:RadComboBoxItem Text="全部" Value="" />
                                <telerik:RadComboBoxItem Text="1" Value="1" />
                                <telerik:RadComboBoxItem Text="2" Value="2" />
                                <telerik:RadComboBoxItem Text="3" Value="3" />
                                <telerik:RadComboBoxItem Text="4" Value="4" />
                                <telerik:RadComboBoxItem Text="5" Value="5" />
                                <telerik:RadComboBoxItem Text="6" Value="6" />
                                <telerik:RadComboBoxItem Text="7" Value="7" />
                                <telerik:RadComboBoxItem Text="8" Value="8" />
                                <telerik:RadComboBoxItem Text="9" Value="9" />
                                <telerik:RadComboBoxItem Text="10" Value="10" />
                                <telerik:RadComboBoxItem Text="11" Value="11" />
                                <telerik:RadComboBoxItem Text="12" Value="12" />
                            </Items>
                        </telerik:RadComboBox>
                        &nbsp;月
                    </td>
                </tr>
                 <tr>
                     <td  align="right" nowrap="nowrap">
                          自定义查询：
                        </td>
                        <td align="left" >
                              <telerik:RadComboBox ID="RadComboBoxItem" runat="server" Width="200px">
                                <Items>
                                    <telerik:RadComboBoxItem Text="姓名" Value="WorkerName" />
                                    <telerik:RadComboBoxItem Text="证件号码" Value="CertificateCode" />
                                    <telerik:RadComboBoxItem Text="单位名称" Value="UnitName" />
                                    <telerik:RadComboBoxItem Text="培训(报名)点" Value="TRAINUNITNAME" />
                                </Items>
                            </telerik:RadComboBox>
                            <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="200px" Skin="Default">
                            </telerik:RadTextBox>
                        </td>
                      <td align="right"  nowrap="nowrap">
                          考试方式：
                    </td>
                     <td align="left" >
                           <telerik:RadComboBox ID="RadComboBoxExamWay" runat="server" Skin="Office2007" CausesValidation="False" Width="70px"
                            ExpandAnimation-Duration="0">
                            <Items>
                                <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                <telerik:RadComboBoxItem Text="机考" Value="机考" />
                                <telerik:RadComboBoxItem Text="网考" Value="网考" />                               
                            </Items>
                        </telerik:RadComboBox>
                        </td>
                  </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                    </td>
                </tr>
            </table>
            <div class="table_cx">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                待审核确认报名列表（提示：生成准考证后，将无法对还未进行审核确认的数据进行操作。）<span style="color:#777">（图例说明：<img alt="" src="../Images/flag_red.png" style="padding-right:8px" />全国证书持证校验预警）</span>
               <%-- <span style="text-align:right; right:20px;cursor:pointer;color:blue;" onclick="showCheckListHelp()">《审核时限说明》</span>--%>
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGrid1" runat="server" Width="100%"
                    PageSize="10" AllowSorting="True" AllowPaging="True" GridLines="None" AutoGenerateColumns="False"
                    Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" OnDeleteCommand="RadGrid1_DeleteCommand"
                    OnPageIndexChanged="RadGridAccept_PageIndexChanged" OnDataBound="RadGridAccept_DataBound">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <MasterTableView DataKeyNames="ExamSignUpID,SignUpCode" NoMasterRecordsText="　没有可显示的记录">
                        <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                        <Columns>
                            <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="Highlight <br/> ship name">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" onclick='checkBoxClick(this.checked);' />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <uc3:CheckAll ID="CheckAll1" runat="server" />
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                              <telerik:GridTemplateColumn UniqueName="CheckImg" HeaderText="预警">
                                <ItemTemplate>                                  
                                    <%# (Eval("ZACheckResult")!= DBNull.Value  && Eval("ZACheckResult").ToString() == "0" ? string.Format("<div title=\"点击查看详细\" class=\"flag_red pointer\" ><img onclick='javascript:layer.alert(\"全国证书持证校验未通过，校验时间：{0}。<br />未通过原因：{1}\",{{area: [\"600px\", \"auto\"]}})' alt=\"\" src=\"../Images/flag_red.png\" style=\"cursor:pointer\" /></div>",Convert.ToDateTime(Eval("ZACheckTime")).ToString("yyyy-MM-dd"),Eval("ZACheckRemark")) : "")%>
                                 
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false"  />
                            </telerik:GridTemplateColumn>          
                            <telerik:GridTemplateColumn UniqueName="Detail" HeaderText="详细">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("ExamSignView.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("ExamSignUpID").ToString())%>");'>详细</span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                             <telerik:GridBoundColumn UniqueName="FIRSTTRIALTIME" DataField="FIRSTTRIALTIME" HtmlEncode="false"
                                DataFormatString="{0:yyyy-MM-dd}" HeaderText="申报日期">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                         <%--  <telerik:GridTemplateColumn UniqueName="CheckLimit" HeaderText="审核时限(工作日)" >
                                <ItemTemplate>
                                     <%# ZYRYJG.UIHelp.formatCheckListExam(Eval("STARTCHECKDATE"),Eval("Status"))%>                                
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>--%>
                           
                            <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="单位名称">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                             <telerik:GridTemplateColumn UniqueName="sb" HeaderText="社保">
                                <ItemTemplate>
                                    <%# (Convert.ToDateTime(Eval("SignUpDate")).CompareTo(DateTime.Parse("2014-07-01")) >= 0) ?
                                             string.Format("<span class=\"link_edit\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'><nobr>{3}</nobr></span>", Eval("CertificateCode").ToString(), Eval("UnitCode").ToString(), Convert.ToDateTime(Eval("SignUpDate")).ToString(),(Eval("SheBaoCheck")==DBNull.Value)?"查看": Eval("SheBaoCheck").ToString()=="1"?"符合":"不符合" ):""%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="比对结果">
                                <ItemTemplate>
                                    <%# FormatFirstCheckType(Eval("FirstCheckType").ToString()) %></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="PostName" HeaderText="申报岗位工种">
                                <ItemTemplate>
                                    <nobr><%# Eval("PostName")%><%# Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("暖通") ? "（暖通）":
                                                   Eval("PostID").ToString() == "12" && Eval("EXAMPLANNAME").ToString().Contains("电气") ? "（电气）" : ""%></nobr>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="考试时间">
                                <ItemTemplate>
                                    <nobr><%# Convert.ToDateTime(Eval("ExamStartDate")).CompareTo(Convert.ToDateTime(Eval("ExamEndDate")))==0?Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd"):Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy.MM.dd") + " - " + Convert.ToDateTime(Eval("ExamEndDate")).ToString("yyyy.MM.dd")%></nobr>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="Faceimage" HeaderText="照片">
                                <ItemTemplate>
                                    <div style="position: relative;">
                                        <asp:Image ID="Image1" runat="server" Width="14" Height="18" ImageUrl='<%#  ShowFaceimage(Eval("CertificateCode").ToString(),Eval("ExamPlanID").ToString())  %>'
                                            onmouseover="setImgSize(this,100,140,'absolute',event);" onmouseout="setImgSize(this,14,18,'relative',event);"></asp:Image>
                                    </div>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                           <%-- <telerik:GridButtonColumn UniqueName="Delete" HeaderText="删除" CommandName="Delete"
                                Text="&lt;span onclick=&quot;if(!confirm('您确定要删除么?'))return false; &quot; &gt;删除&lt;/span&gt;">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridButtonColumn>--%>
                        </Columns>
                        <HeaderStyle Font-Bold="True" />
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete"
                    InsertMethod="Insert" SelectMethod="GetList_New" TypeName="DataAccess.ExamSignUpDAL"
                    UpdateMethod="Update" SelectCountMethod="SelectCount_New" EnablePaging="true" MaximumRowsParameterName="maximumRows"
                    StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue=" and Status ='待缴费'" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <div style="width: 95%; margin: 0 auto; text-align: center; padding-top: 8px;">
                <asp:Button ID="ButtonComfirm" runat="server" Text="审核通过确认" CssClass="bt_large" OnClick="ButtonComfirm_Click" />&nbsp;
            </div>
        </div>
    </div>
    <uc5:IframeView ID="IframeView" runat="server" />
</asp:Content>
