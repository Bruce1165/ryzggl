<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainCheckSign.aspx.cs" Inherits="ZYRYJG.Train.TrainCheckSign" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../PostSelect.ascx" TagName="PostSelect" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc4" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">            
        </telerik:RadScriptManager>
        <telerik:RadAjaxManager runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1" RequestQueueSize="1">
             <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="content">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="content" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7"
            Height="1000px" />
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
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
                //grid行选择
                function checkBoxClick(checked) {
                    if (checked == true) return;
                    var ckall = document.getElementById(CheckBoxAllClientID);
                    if (ckall == null) return;
                    if (ckall.checked == true) ckall.checked = false;
                }

                //
                function checkAllTip(checkBoxAllClientID) {
                    var ckall = document.getElementById(checkBoxAllClientID);
                    if (ckall == null) return;
                    if (ckall.checked == true) {
                        alert("注意：您全选了所有页数据（不仅仅是当前页），您接下来的操作将对所有数据进行处理！");
                    }
                }

                //grid全选
                function checkBoxAllClick(checkBoxAllClientID) {
                    if (checkBoxAllClientID == undefined) return;
                    var hideSelectAllRefreshCount = document.getElementById(checkBoxAllClientID.replace("CheckBoxAll", "HiddenFieldSelectAllRefreshCount"));
                    var hideSelectAll = document.getElementById(checkBoxAllClientID.replace("CheckBoxAll", "HiddenFieldSelectAll"));
                    var ckall = document.getElementById(checkBoxAllClientID);
                    if (ckall == null) return;
                    hideSelectAll.value = ckall.checked;
                    hideSelectAllRefreshCount.value = "0";
                    var grid = ckall.parentNode;
                    while (grid != null && grid != undefined && grid.nodeName != "TABLE") {
                        grid = grid.parentNode;
                    }
                    var ifSelect = ckall.checked;
                    var Chks;
                    if (grid == undefined)
                        Chks = document.getElementsByTagName("input");
                    else
                        Chks = grid.getElementsByTagName("input");

                    if (Chks.length) {
                        for (i = 0; i < Chks.length; i++) {
                            if (Chks[i].type == "checkbox") {
                                Chks[i].checked = ifSelect;
                            }
                        }
                    }
                    else if (Chks) {
                        if (Chks.type == "checkbox") {
                            Chks.checked = ifSelect;
                        }
                    }
                }              
                //判断GridView是否选择一个checkBox，适用于Button控件
                //参数：【alt：提示信息，如请勾选一条记录，可选项，默认为“请勾选需要操作的记录”】【divID：控件容器ID，可选项，默认为document】
                function isSelectRow(alt, divID) {
                    var chks;
                    if (divID == undefined)
                        chks = document.getElementsByTagName("input");
                    else
                        chks = document.getElementById(divID).getElementsByTagName("input");

                    if (chks == null) return false;

                    if (chks.length) {
                        for (i = 0; i < chks.length; i++) {
                            if (chks[i].type == "checkbox") {
                                if (chks[i].checked == true) {
                                    //alert(chks[i].value);
                                    return true;
                                }
                            }
                        }
                    }
                    if (alt != undefined)
                        alert(alt);
                    else
                        alert("至少选择一条记录");
                    return false;
                }
            </script>
        </telerik:RadCodeBlock>
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt; 培训点业务 &gt;&gt;<strong>报名审核</strong>
                </div>
            </div>
            <div class="content" id="content" runat="server">
                <table class="bar_cx">
                    <tr>
                        <td align="right" width="11%" nowrap="nowrap">岗位工种：
                        </td>
                        <td align="left" width="38%">
                            <uc1:PostSelect ID="PostSelect2" runat="server" />
                        </td>

                        <td align="right" width="11%" nowrap="nowrap">姓名：
                        </td>
                        <td align="left" width="38%">
                            <telerik:RadTextBox ID="RadTxtWorkerName" runat="server" Width="100px" Skin="Default">
                            </telerik:RadTextBox>&nbsp;&nbsp;证件号码：                  
                        <telerik:RadTextBox ID="RadTxtCertificateCode" runat="server" Width="200px" Skin="Default">
                        </telerik:RadTextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" width="11%" nowrap="nowrap">审核进度：
                        </td>
                        <td align="left" width="38%">
                            <asp:RadioButtonList ID="RadioButtonListCheckStatus" runat="server" RepeatDirection="Horizontal" TextAlign="right" Width="200px">
                                <asp:ListItem Text="未审核" Value="未审核" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="已审核" Value="已审核"></asp:ListItem>
                            </asp:RadioButtonList>
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
                        <td colspan="4" align="center">
                            <asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="bt_large" OnClick="ButtonSearch_Click" />
                        </td>
                    </tr>
                </table>
                  <div class="table_cx">
                    <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px; padding-right: 2px;" />
                    待审核的考试报名列表 
                </div>
                <div style="width: 98%; margin: 0 auto;">
                    <telerik:RadGrid ID="RadGrid1" runat="server" Width="100%" PageSize="10" AllowSorting="True" PagerStyle-AlwaysVisible="true"
                        AllowPaging="True" GridLines="None" AutoGenerateColumns="False" Skin="Blue" EnableAjaxSkinRendering="false"
                        EnableEmbeddedSkins="false" OnPageIndexChanged="RadGridAccept_PageIndexChanged"
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
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("../EXamManage/ExamSignView.aspx?o=<%#  Utility.Cryptography.Encrypt(Eval("ExamSignUpID").ToString())%>");'>详细</span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="SignUpDate" DataField="SignUpDate" HtmlEncode="false"
                                    DataFormatString="{0:yyyy-MM-dd}" HeaderText="申报日期">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridBoundColumn>                                
                                <telerik:GridTemplateColumn UniqueName="Status" HeaderText="审核进度">
                                    <ItemTemplate>
                                        <nobr><%# Eval("Status").ToString()=="待初审" ? "待审核":Eval("Status").ToString()=="已缴费" ? "已审核":Eval("Status")%></nobr>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                 <telerik:GridTemplateColumn UniqueName="CHECKRESULT" HeaderText="审核结果">
                                    <ItemTemplate>
                                        <nobr><%# Eval("CHECKRESULT")==DBNull.Value ? "":Eval("CHECKRESULT")%></nobr>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="WorkerName" HeaderText="姓名" SortExpression="WorkerName">
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
                                <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName"
                                    HeaderText="申报岗位工种">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>

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
    </form>
</body>
</html>
