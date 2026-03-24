<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CheckSign.aspx.cs" Inherits="ZYRYJG.EXamManage.CheckSign" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../ExamPlanSelect.ascx" TagName="ExamPlanSelect" TagPrefix="uc4" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/timelimit.css?v=1.001" rel="stylesheet" type="text/css" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function getEventObject(W3CEvent) {   //事件标准化函数
                return W3CEvent || window.event;
            }
            function getPointerPosition(e) {   //兼容浏览器的鼠标x,y获得函数
                e = e || getEventObject(e);
                var x = e.pageX || (e.clientX + (document.documentElement.scrollLeft || document.body.scrollLeft));
                var y = e.pageY || (e.clientY + (document.documentElement.scrollTop || document.body.scrollTop));

                return { 'x': x, 'y': y };
            }

            function setImgSize(img, imgWidth, timgHeight, position, e) {
                img.style.width = imgWidth + "px";
                img.style.height = timgHeight + "px";

                var pos = getPointerPosition(e);

                img.style.position = position;
                if (position == "absolute") {
                    img.style.top = -timgHeight + 20 + "px";
                    img.style.left = -imgWidth + 40 + "px";
                }
                else {
                    img.style.top = 0;
                    img.style.left = 0;
                }
            }

            function changeCheckResult(sender) {

                var TextBoxCheckResult = $("#<%= TextBoxCheckResult.ClientID%>");

                if ($(sender).val() == "通过") {

                    TextBoxCheckResult.val("通过");
                }
                else {
                    TextBoxCheckResult.val("退回个人");
                }
            }

        </script>
         <script type="text/javascript" src="../Scripts/timelimit.js?v=1.003"></script>
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="content">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="content" LoadingPanelID="RadAjaxLoadingPanel1" />
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
                报名管理 &gt;&gt; <strong>报名审核</strong>
            </div>
        </div>
        <div class="content" id="content" runat="server">
            <table class="bar_cx">
                <tr>
                    <td align="right" width="11%" nowrap="nowrap"><span style="color: Red">* </span>考试计划(必选)：
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
                         &nbsp;&nbsp;&nbsp;&nbsp;考试方式：
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
                    <td align="right" width="11%" nowrap="nowrap">审核进度：
                    </td>
                    <td align="left" width="38%">
                        <telerik:RadComboBox ID="RadComboBoxUnitCheckType" runat="server" Skin="Office2007" CausesValidation="False"
                            ExpandAnimation-Duration="0">
                            <Items>
                                <telerik:RadComboBoxItem Text="全部" Value=""  />
                                <telerik:RadComboBoxItem Text="已受理" Value="已受理" Selected="true"  />
                                <telerik:RadComboBoxItem Text="审核未通过" Value="退回修改" />     
                            </Items>
                        </telerik:RadComboBox>&nbsp;&nbsp;比对结果：
                   
                        <telerik:RadComboBox ID="RadComboBoxFirstCheckType" runat="server" Skin="Office2007" CausesValidation="False"
                            ExpandAnimation-Duration="0">
                            <Items>
                                <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                <telerik:RadComboBoxItem Text="人工审核" Value="<2" />
                                 <telerik:RadComboBoxItem Text="社保符合(非A证)" Value="3.1" />
                                <telerik:RadComboBoxItem Text="社保符合(A证非法人)" Value="3.2" />
                                <telerik:RadComboBoxItem Text="法人符合" Value="4" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td align="right" width="11%" nowrap="nowrap">缴费月份：
                    </td>
                      <td   align="left" >
                           <telerik:RadComboBox ID="RadComboBoxJFCount" runat="server" Skin="Office2007" CausesValidation="False" Width="70px"
                            ExpandAnimation-Duration="0">
                            <Items>
                                <telerik:RadComboBoxItem Text="全部" Value="" Selected="true" />
                                <telerik:RadComboBoxItem Text="上上" Value="2" />
                                <telerik:RadComboBoxItem Text="上" Value="1" />
                                <telerik:RadComboBoxItem Text="空" Value="0" />
                            </Items>
                        </telerik:RadComboBox>
                          </td>
                </tr>
             <tr>
                     <td align="right" width="11%" nowrap="nowrap">
                        
                        自定义查询：
                    </td>
                    <td align="left" width="38%">
                         <telerik:RadComboBox ID="RadComboBoxIten" runat="server" Width="150px">
                            <Items>
                                <telerik:RadComboBoxItem Text="姓 名" Value="WorkerName" />
                                <telerik:RadComboBoxItem Text="证件号码" Value="CertificateCode" />
                                <telerik:RadComboBoxItem Text="单位名称" Value="UnitName" />
                               
                            </Items>
                        </telerik:RadComboBox>
                       <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Skin="Default" Width="300px" MaxLength="50">
                        </telerik:RadTextBox>
                    </td>
                   <td align="left" colspan="2">
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="bt_large" OnClick="ButtonSearch_Click" style="margin-left:40px" />
                   </td>
                </tr>
            </table>

            <div class="table_cx">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                待审核的考试报名列表 <span style="color:#777">（图例说明：<img alt="" src="../Images/s_lock.png"  />违规申报锁定）</span><span style="text-align:right; right:20px;cursor:pointer;color:blue;" onclick="showCheckListHelp()">《审核时限说明》</span>
            </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGrid1" runat="server" Width="100%" PageSize="10" AllowSorting="True" PagerStyle-AlwaysVisible="true"
                    AllowPaging="True" GridLines="None" AutoGenerateColumns="False" Skin="Blue" EnableAjaxSkinRendering="false"
                    EnableEmbeddedSkins="false" OnDeleteCommand="RadGrid1_DeleteCommand" OnPageIndexChanged="RadGridAccept_PageIndexChanged"
                    OnDataBound="RadGridAccept_DataBound">
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="True" />
                    </ClientSettings>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView DataKeyNames="ExamSignUpID,ExamPlanID,SignUpCode"
                        NoMasterRecordsText="　没有可显示的记录">
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
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn HeaderText="序号" UniqueName="RowNum" DataField="RowNum" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                             <telerik:GridTemplateColumn UniqueName="Detail" HeaderText="详细">
                                <ItemTemplate>
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("ExamSignView.aspx?o=<%#  Page.Server.UrlEncode(Utility.Cryptography.Encrypt(Eval("ExamSignUpID").ToString()))%>");'>详细</span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                             <telerik:GridBoundColumn UniqueName="FIRSTTRIALTIME" DataField="FIRSTTRIALTIME" HtmlEncode="false"
                                DataFormatString="{0:yyyy-MM-dd}" HeaderText="申报日期">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                              <%-- <telerik:GridTemplateColumn UniqueName="CheckLimit" HeaderText="审核时限(工作日)" >
                                <ItemTemplate>
                                     <%# ZYRYJG.UIHelp.formatCheckListExam(Eval("STARTCHECKDATE"),Eval("Status"))%>                                
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>--%>
                              <telerik:GridBoundColumn UniqueName="AcceptTime" DataField="AcceptTime" HtmlEncode="false"
                                DataFormatString="{0:yyyy.MM.dd HH:mm:ss}" HeaderText="受理时间">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="Status" DataField="Status" HeaderText="审核进度">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            
                            <telerik:GridTemplateColumn UniqueName="WorkerName" HeaderText="姓名" SortExpression="WorkerName" >
                                  <ItemTemplate>                                   
                                   <%# Eval("LockEndTime") != DBNull.Value && Convert.ToDateTime(Eval("LockEndTime")).AddDays(1)> DateTime.Now?string.Format("<img alt=\"\" src=\"../Images/s_lock.png\" title=\"违规申报锁定，锁定时间：{0} - {1}，\r\n锁定原因：{2}\" style=\"cursor:pointer\" />",Convert.ToDateTime(Eval("LockTime")).ToString("yyyy.MM.dd"),Convert.ToDateTime(Eval("LockEndTime")).ToString("yyyy.MM.dd"),Eval("LockReason")):""%> <%# Eval("WorkerName") %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
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
                                    <%# DataAccess.ExamSignUpDAL.FormatFirstCheckType(Eval("FirstCheckType").ToString()) %></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn HeaderText="缴费月份">
                                <ItemTemplate>
                                    <%# DataAccess.ExamSignUpDAL.FormatJFCount(Eval("JFCount")) %></ItemTemplate>
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
                                    <nobr><%# Convert.ToDateTime(Eval("ExamStartDate")).CompareTo(Convert.ToDateTime(Eval("ExamEndDate")))==0?Convert.ToDateTime(Eval("ExamStartDate")).ToString("yyyy-MM-dd"):Convert.ToDateTime(Eval("ExamStartDate")).ToString("yy.MM.dd") + " - " + Convert.ToDateTime(Eval("ExamEndDate")).ToString("yy.MM.dd")%></nobr>
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
                           
                        </Columns>
                        <EditFormSettings>
                            <EditColumn InsertImageUrl="Update.gif" UpdateImageUrl="Update.gif" EditImageUrl="Edit.gif"
                                CancelImageUrl="Cancel.gif">
                            </EditColumn>
                        </EditFormSettings>
                        <HeaderStyle Font-Bold="True" />
                        <PagerTemplate>
                            <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <ClientSettings>
                        <Selecting AllowRowSelect="true" />
                    </ClientSettings>
                    <FilterMenu EnableEmbeddedSkins="False">
                    </FilterMenu>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    <StatusBarSettings LoadingText="正在读取数据" ReadyText="完成" />
                </telerik:RadGrid>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DeleteMethod="Delete"
                    InsertMethod="Insert" SelectMethod="GetList_New" TypeName="DataAccess.ExamSignUpDAL"
                    UpdateMethod="Update" SelectCountMethod="SelectCount_New" EnablePaging="true" MaximumRowsParameterName="maximumRows"
                    StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue=" and Status ='已初审'" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <div class="table_cx" style="padding-top: 8px;">
                <img alt="" src="../Images/1034.gif" width="11" height="11" style="margin-bottom: -2px; padding-right: 2px;" />
                在查询结果中定位数据所在页 <span style="color: Blue;">（提示：如果定位条件查到多条记录，只能定位到第一条。）</span>
            </div>
            <table class="bar_cx">
                <tr>
                    <td>证件号码：
                     
                            <telerik:RadTextBox ID="RadTxtCertificateCode_Find" runat="server" Width="200px" Skin="Default">
                            </telerik:RadTextBox>
                        姓名：
                       
                            <telerik:RadTextBox ID="RadTextBoxWorkerName_Find" runat="server" Width="150px" Skin="Default">
                            </telerik:RadTextBox>

                        <asp:Button ID="ButtonPosition" runat="server" Text="定位到所在页" CssClass="bt_large"
                            OnClick="ButtonPosition_Click" />
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; width: 98%; margin: 10px auto">
                <tr class="GridLightBK">
                    <td colspan="2" class="barTitle">报名批量审核</td>
                </tr>
                <tr class="GridLightBK">
                    <td colspan="2">请勾选您要审批的数据，填写审批意见。</td>
                </tr>
                <tr class="GridLightBK">
                    <td width="20%" align="right">处理结果：</td>
                    <td width="80%" align="left">
                        <asp:RadioButtonList ID="RadioButtonListCheckResult" runat="server" RepeatDirection="Horizontal" TextAlign="right">
                            <asp:ListItem Text="通过" Value="通过" Selected="True" onclick="changeCheckResult(this)"></asp:ListItem>
                            <asp:ListItem Text="不通过" Value="不通过" onclick="changeCheckResult(this)"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr class="GridLightBK">
                    <td width="20%" align="right">处理意见：</td>
                    <td width="80%" align="left">

                        <asp:TextBox ID="TextBoxCheckResult" runat="server" TextMode="MultiLine" Rows="3" Width="99%" MaxLength="100" Text="通过"></asp:TextBox>
                    </td>
                </tr>
                <tr class="GridLightBK">
                    <td colspan="2" align="center">
                        <asp:Button ID="ButtonCheck" runat="server" Text="确 定" CssClass="bt_large" OnClick="ButtonCheck_Click" />

                    </td>
                </tr>
            </table>

        </div>
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
