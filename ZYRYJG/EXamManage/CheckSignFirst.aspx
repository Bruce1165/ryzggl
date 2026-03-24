<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CheckSignFirst.aspx.cs" Inherits="ZYRYJG.EXamManage.CheckSignFirst" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../ExamPlanSelect.ascx" TagName="ExamPlanSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="../css/timelimit.css?v=1.001" rel="stylesheet" type="text/css" />
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript" src="../Scripts/timelimit.js?v=1.003"></script>
        <script type="text/javascript">
            function onRequestStart(sender, args) {
                if (args.get_eventTarget().indexOf("ButtonExportToExcel") >= 0) {
                    args.set_enableAjax(false);
                }
            }

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

            //回车执行查询
            function ShaoQiangSearch(e) {
                e = e || getEventObject(e);
                if (e.keyCode == 13) {
                    e.keyCode = 9;
                    e.returnValue = false;
                    var btn = document.getElementById("ctl00_ContentPlaceHolder1_ButtonSearch");
                    if (btn != null) btn.click();
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

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="onRequestStart" />
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
     <style type="text/css">
        .tl, .flag_blue, .flag_lock, .flag_black {
            color: #444;
            float: left;
            margin: 0 2px;
        }

        .pl20 {
            padding-left: 20px;
        }

        .flag_blue {
            background: url(../Images/flag_blue.png) no-repeat left center transparent;
        }


        .flag_black {
            background: url(../Images/flag_black.png) no-repeat left center transparent;
        }


         .flag_lock {
            background: url(../Images/s_lock.png) no-repeat left center transparent;
        }

        .pointer {
            cursor: pointer;
            width: 16px;
            height: 16px;
        }

        .RadPicker2 {
            float: left;
            line-height: 27px;
            padding: 0px 1px;
            background-repeat: no-repeat;
        }

        .float {
            float: left;
            margin: 4px 12px;
            position: relative;
        }

        .absolute {
            position: relative;
            clear: both;
        }
    </style>
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;
                报名管理 &gt;&gt; <strong>报名受理</strong>
            </div>
        </div>
        <div class="content" id="content" runat="server">
            <table class="bar_cx">
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">考试计划：
                    </td>
                    <td align="left" colspan="3">
                        <uc1:ExamPlanSelect ID="ExamPlanSelect1" runat="server" />
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
                    <td width="11%" align="right" nowrap="nowrap">学历职称证明方式：
                    </td>
                    <td align="left" width="38%">
                         <telerik:RadComboBox ID="RadComboBoxSignupPromise" runat="server" Width="200px">
                            <Items>
                                <telerik:RadComboBoxItem Text="全部" Value="" />
                                <telerik:RadComboBoxItem Text="上传承诺书" Value="1" />
                                <telerik:RadComboBoxItem Text="上传学历职称证明" Value="0" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" width="11%" nowrap="nowrap">岗位工种：
                    </td>
                    <td align="left" width="38%">
                        <uc5:PostSelect ID="PostSelect2" runat="server" />
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
                                <telerik:RadComboBoxItem Text="待初审" Value="待初审" />
                                <telerik:RadComboBoxItem Text="已初审" Value="已初审"  Selected="true" /> 
                                <telerik:RadComboBoxItem Text="已受理" Value="已受理" />
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
                       
                        <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" style="margin-left:40px" />
                    </td>
                </tr>
            </table>
            <%--<div class="table_cx">
                <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                待受理报名列表<span style="color:#777">（图例说明：<img alt="" src="../Images/flag_black.png"  />社保缺失，<img alt="" src="../Images/s_lock.png"  />违规申报锁定，<img alt="" src="../Images/flag_black.png"  />异地持证）</span><span style="text-align:right; right:20px;cursor:pointer;color:blue;" onclick="showCheckListHelp()">《审核时限说明》</span>
               
            </div>--%>
             <div style="width: 98%; line-height: 20px; height: 20px; vertical-align: middle; padding: 0; margin:4px 0;">
                    <div class="tl pl20">待受理报名列表（只显示审核截止前数据），图例说明：</div>
              
                    <div class="flag_blue pl20">社保缺失，</div>
                    <div class="flag_lock pl20">违规申报锁定，</div>
                    <div class="flag_black pl20">异地持证</div>                   
                    <div id="divCheckLimit" runat="server" style="text-align:right; right:20px;cursor:pointer;color:blue;" onclick="showCheckListHelp()">《审核时限说明》</div>
                </div>
            <div style="width: 98%; margin: 0 auto;">
                <telerik:RadGrid ID="RadGrid1" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
                    GridLines="None" AutoGenerateColumns="False" Skin="Blue" EnableAjaxSkinRendering="False" PagerStyle-AlwaysVisible="true"
                    EnableEmbeddedSkins="False" OnPageIndexChanged="RadGridAccept_PageIndexChanged" OnDataBound="RadGridAccept_DataBound">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView DataKeyNames="ExamSignUpID,ExamPlanID,CertificateCode,WorkerName"
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
                                    <span class="link_edit" onclick='javascript:SetIfrmSrc("ExamSignView.aspx?o=<%# Utility.Cryptography.Encrypt(Eval("ExamSignUpID").ToString())%>");'>详细</span>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>
                             <telerik:GridBoundColumn UniqueName="FIRSTTRIALTIME" DataField="FIRSTTRIALTIME" HtmlEncode="false"
                                DataFormatString="{0:yyyy.MM.dd}" HeaderText="申报日期">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                             <telerik:GridBoundColumn UniqueName="AcceptTime" DataField="AcceptTime" HtmlEncode="false"
                                DataFormatString="{0:yyyy.MM.dd HH:mm:ss}" HeaderText="受理时间">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                             <%-- <telerik:GridTemplateColumn UniqueName="CheckLimit" HeaderText="受理时限(工作日)" SortExpression="AcceptTime" >
                                <ItemTemplate>
                                     <%# Eval("AcceptTime")!=DBNull.Value?Convert.ToDateTime(Eval("AcceptTime")).ToString("yyyy.MM.dd HH:mm"): ZYRYJG.UIHelp.formatCheckListExam(Eval("STARTCHECKDATE"),Eval("Status"))%>                                
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>--%>
                        <%--    <telerik:GridBoundColumn UniqueName="Status" DataField="Status" HeaderText="审核进度">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>--%>
                              <telerik:GridTemplateColumn UniqueName="Status" HeaderText="审核进度" SortExpression="Status" >
                                   <ItemTemplate>                                     
                                 <%# ZYRYJG.EXamManage.CheckSignFirst.fmtStatus(Eval("Status").ToString()) %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                                  </telerik:GridTemplateColumn>
                            <telerik:GridTemplateColumn UniqueName="WorkerName" HeaderText="姓名" SortExpression="WorkerName" >
                                  <ItemTemplate>      
                                       <%-- <%# Eval("LockEndTime") != DBNull.Value && Convert.ToDateTime(Eval("LockEndTime")).AddDays(1)> DateTime.Now?string.Format("<img alt=\"\" src=\"../Images/s_lock.png\" title=\"违规申报锁定，锁定时间：{0} - {1}，\r\n锁定原因：{2}\" style=\"cursor:pointer\" />",Convert.ToDateTime(Eval("LockTime")).ToString("yyyy.MM.dd"),Convert.ToDateTime(Eval("LockEndTime")).ToString("yyyy.MM.dd"),Eval("LockReason")):""%>  --%>                           
                                   <%# Eval("WorkerName") %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                             <telerik:GridTemplateColumn UniqueName="SignupPromise" HeaderText="学历职称证明方式" >
                                <ItemTemplate>
                                     <%# (Eval("SignupPromise") !=DBNull.Value && Eval("SignupPromise").ToString()=="1")?"上传承诺书":"上传学历职称证明"%>                                
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>
                            
                            <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="单位名称">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="sb" HeaderText="校验图例">
                                <ItemTemplate>                                
                                      <%# Eval("SheBaoCheck") ==DBNull.Value?"<div class=\"flag_blue pointer\" onclick='javascript:alert(\"尚未比对!（当日提交数据后,次日进行比对）\")'>&nbsp;</div>": Eval("SheBaoCheck").ToString()=="1"?"":string.Format("<div class=\"flag_blue pointer\" onclick='javascript:SetIfrmSrc(\"../PersonnelFile/RYSBView.aspx?o1={0}&o2={1}&o3={2}\");'>&nbsp;</div>", Eval("CertificateCode"), Eval("UnitCode"), Eval("SignUpDate"))%>
                                     <%# Eval("LockEndTime") != DBNull.Value && Convert.ToDateTime(Eval("LockEndTime")).AddDays(1)> DateTime.Now?string.Format("<div class=\"flag_lock pointer\" onclick='javascript:alert(\"违规申报锁定，锁定时间：{0} - {1}，\r\n锁定原因：{2}\")'>&nbsp;</div>",Convert.ToDateTime(Eval("LockTime")).ToString("yyyy.MM.dd"),Convert.ToDateTime(Eval("LockEndTime")).ToString("yyyy.MM.dd"),Eval("LockReason")):""%>
                                    <%-- <%# Eval("ZACheckResult") ==DBNull.Value?"<div class=\"flag_black pointer\" onclick='javascript:alert(\"尚未比对!\")'>&nbsp;</div>": Eval("ZACheckResult").ToString()=="1"?"": string.Format("<div class=\"flag_black pointer\" onclick='javascript:alert(\"{0}\")'>&nbsp;</div>",Eval("ZACheckRemark"))%>--%>
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
                            <telerik:GridTemplateColumn UniqueName="ExamDate" DataField="ExamDate" HeaderText="考试时间">
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
                        </Columns>
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
                    UpdateMethod="Update" SelectCountMethod="SelectCount_New" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                    SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue=" and Status ='已初审'" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
             <table border="0" cellpadding="5" cellspacing="1" class="table" style="text-align: center; width: 98%; margin: 10px auto">
                <tr class="GridLightBK">
                    <td colspan="2" class="barTitle">报名批量受理</td>
                </tr>
                <tr class="GridLightBK">
                    <td colspan="2">请勾选您要受理的数据，填写受理意见。</td>
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
                        <asp:Button ID="ButtonAccept" runat="server" Text="确 定" CssClass="bt_large" OnClick="ButtonAccept_Click" />

                    </td>
                </tr>
            </table>
           <%-- <div>
                <div style="width: 95%; margin: 0 auto; text-align: center; padding-top: 8px;">
                    <asp:Button ID="ButtonAccept" runat="server" Text="批量受理通过" CssClass="bt_large" OnClick="ButtonAccept_Click" />&nbsp;&nbsp;
                            <asp:Button ID="ButtonExportToExcel" CssClass="bt_large" Text="导出查询结果" OnClick="ButtonExportToExcel_Click"
                                runat="server"></asp:Button>
                </div>
            </div>--%>
        </div>
    </div>
    <uc4:IframeView ID="IframeView" runat="server" />
</asp:Content>
