<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessQuery.aspx.cs" Inherits="ZYRYJG.County.BusinessQuery" %>


<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<%@ Register Src="../CheckAll.ascx" TagName="CheckAll" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <%--<meta http-equiv="X-UA-Compatible" content="IE=7">--%>  
    <meta name="renderer" content="webkit|ie-comp|ie-stand" />
    <title></title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
     <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
      <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function onRequestStart(sender, args) {

                if (args.get_eventTarget().indexOf("ButtonPrint") >= 0) {
                    args.set_enableAjax(false);

                }
            }
      
        </script>
    </telerik:RadCodeBlock>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true">
            <ClientEvents OnRequestStart="onRequestStart" />
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQY" LoadingPanelID="RadAjaxLoadingPanel1" />
                        <telerik:AjaxUpdatedControl ControlID="spanOutput"  UpdatePanelRenderMode="Inline" />
                        <telerik:AjaxUpdatedControl ControlID="ButtonPrint" UpdatePanelRenderMode="Inline" />
                       <telerik:AjaxUpdatedControl ControlID="BtnReturn2" UpdatePanelRenderMode="Inline" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridQY">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQY" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>

                  <telerik:AjaxSetting AjaxControlID="ButtonPrint">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="ButtonPrint" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                 
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt;综合查询 &gt;&gt;<strong>建造师注册业务查询</strong>
                </div>
            </div>
            <div class="content">
                <div style="width: 99%; margin: 2px auto; text-align: center;">
                    <table class="bar_cx" width="98%" border="0" align="center" cellspacing="1" style="padding-bottom: 8px">
                        <tr id="TrPerson" runat="server">
                            <td width="70px" align="right" nowrap="nowrap">
                                <telerik:RadComboBox ID="RadComboBoxItem" runat="server" Width="100%">
                                    <Items>                                        
                                        <telerik:RadComboBoxItem Text="姓名" Value="PSN_Name" />
                                        <telerik:RadComboBoxItem Text="证件号码" Value="PSN_CertificateNO" />
                                        <telerik:RadComboBoxItem Text="企业名称" Value="ENT_Name" />
                                        <telerik:RadComboBoxItem Text="注册号" Value="PSN_RegisterNO" />
                                        <telerik:RadComboBoxItem Text="受理人" Value="GetMan" />
                                        <telerik:RadComboBoxItem Text="上报批次" Value="ReportCode" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td align="left" width="150px">
                                <telerik:RadTextBox ID="RadTextBoxValue" runat="server" Width="97%" Skin="Default">
                                </telerik:RadTextBox>
                            </td>
                            <td align="left" width="340px">
                                <div class="RadPicker md">
                                    &nbsp;
                                    <telerik:RadComboBox ID="RadComboBoxRQItem" runat="server" Width="80">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="申报日期" Value="OldUnitCheckTime" />
                                            <telerik:RadComboBoxItem Text="受理日期" Value="GetDateTime" />
                                            <telerik:RadComboBoxItem Text="上报日期" Value="ReportDate" />
                                        </Items>
                                    </telerik:RadComboBox>
                                    <telerik:RadComboBox ID="RadComboBoxRQItem_City" runat="server" Width="80" Visible="false">
                                        <Items>
                                            <telerik:RadComboBoxItem Text="上报日期" Value="ReportDate" />
                                            <telerik:RadComboBoxItem Text="审查日期" Value="CheckDate" />
                                            <telerik:RadComboBoxItem Text="决定日期" Value="ConfirmDate" />
                                            <telerik:RadComboBoxItem Text="公示日期" Value="PublicDate" />
                                            <telerik:RadComboBoxItem Text="公告日期" Value="NoticeDate" />
                                        </Items>
                                    </telerik:RadComboBox>
                                </div>
                                <telerik:RadDatePicker ID="RadDatePickerGetDateStart" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="100px" />
                                <div class="RadPicker md">至</div>
                                <telerik:RadDatePicker ID="RadDatePickerGetDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="100px" />
                            </td>

                            <td align="left">&nbsp;申报事项：
                                 <telerik:RadComboBox ID="RadComboBoxPSN_RegisteType" runat="server" Width="100">
                                     <Items>
                                         <telerik:RadComboBoxItem Text="全部" Value="全部" />
                                         <telerik:RadComboBoxItem Text="初始注册" Value="初始注册" />
                                         <telerik:RadComboBoxItem Text="重新注册" Value="重新注册" />
                                         <telerik:RadComboBoxItem Text="增项注册" Value="增项注册" />
                                         <telerik:RadComboBoxItem Text="延续注册" Value="延期注册" />
                                         <telerik:RadComboBoxItem Text="执业企业变更" Value="执业企业变更" />
                                         <telerik:RadComboBoxItem Text="企业信息变更" Value="企业信息变更" />
                                         <telerik:RadComboBoxItem Text="个人信息变更" Value="个人信息变更" />
                                         <telerik:RadComboBoxItem Text="遗失补办" Value="遗失补办" />
                                         <telerik:RadComboBoxItem Text="注销" Value="注销" />
                                     </Items>
                                 </telerik:RadComboBox>
                                &nbsp;&nbsp;申报进度：
                                 <telerik:RadComboBox ID="RadComboBoxApplyStatus" runat="server" Width="80">
                                     <Items>
                                         <telerik:RadComboBoxItem Text="全部" Value="全部" />
                                         <telerik:RadComboBoxItem Text="未受理" Value="未受理"  />
                                         <telerik:RadComboBoxItem Text="已受理" Value="已受理" />
                                         <telerik:RadComboBoxItem Text="区县审查" Value="区县审查" />
                                         <telerik:RadComboBoxItem Text="已驳回" Value="已驳回" />
                                         <telerik:RadComboBoxItem Text="已上报" Value="已上报" />
                                         <telerik:RadComboBoxItem Text="已办结" Value="已办结" />

                                     </Items>
                                 </telerik:RadComboBox>&nbsp;&nbsp;
                                <telerik:RadComboBox ID="RadComboBoxApplyStatus_City" runat="server" Width="80" Visible="false">
                                    <Items>
                                        <telerik:RadComboBoxItem Text="全部" Value="全部" />
                                        <telerik:RadComboBoxItem Text="未申报" Value="未申报"  />
                                        <telerik:RadComboBoxItem Text="待确认" Value="待确认"  />
                                         <telerik:RadComboBoxItem Text="已申报" Value="已申报"  />
                                         <telerik:RadComboBoxItem Text="已受理" Value="已受理" />
                                         <telerik:RadComboBoxItem Text="区县审查" Value="区县审查" />
                                        <telerik:RadComboBoxItem Text="已上报" Value="已上报" />
                                        <telerik:RadComboBoxItem Text="已审查" Value="已审查" />
                                        <telerik:RadComboBoxItem Text="已决定" Value="已决定" />
                                        <telerik:RadComboBoxItem Text="已公示" Value="已公示" />
                                        <telerik:RadComboBoxItem Text="已公告" Value="已公告" />
                                        <telerik:RadComboBoxItem Text="已驳回" Value="已驳回" />
                                         <telerik:RadComboBoxItem Text="已放号" Value="已放号" />

                                    </Items>
                                </telerik:RadComboBox>
                                  &nbsp;&nbsp;审批结果：                           
                                <telerik:RadComboBox ID="RadComboBoxStatus_Result" runat="server" Width="100" >
                                    <Items>
                                        <telerik:RadComboBoxItem Text="全部" Value="全部" />
                                        <telerik:RadComboBoxItem Text="受理通过" Value="受理通过" />
                                        <telerik:RadComboBoxItem Text="受理驳回" Value="受理驳回" />
                                        <telerik:RadComboBoxItem Text="区县审查通过" Value="区县审查通过" />
                                        <telerik:RadComboBoxItem Text="区县审查不通过" Value="区县审查不通过" />
                                        <telerik:RadComboBoxItem Text="审查通过" Value="审查通过" />
                                        <telerik:RadComboBoxItem Text="审查不通过" Value="审查不通过" />
                                        <telerik:RadComboBoxItem Text="决定通过" Value="决定通过" />                                       
                                        <telerik:RadComboBoxItem Text="决定不通过" Value="决定不通过" />                                
                                    </Items>
                                </telerik:RadComboBox>
                                &nbsp;<asp:Button ID="ButtonSearch" runat="server" Text="查 询" CssClass="button" OnClick="ButtonSearch_Click" />
                            </td>
                            <td></td>
                        </tr>
                      <%--  <tr id="TrOtherCheck" runat="server" visible="false">
                            <td  align="left" nowrap="nowrap" colspan="3">
                                专业委办局已核查：<asp:CheckBox ID="CheckBoxOtherCheck" runat="server" />
                            </td>

                        </tr>--%>
                    </table>
                    <div style="width: 98%; margin: 8px auto;">
                    <telerik:RadGrid ID="RadGridQY" runat="server"
                        GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" Skin="Blue" EnableAjaxSkinRendering="False"
                        EnableEmbeddedSkins="False"
                        SortingSettings-SortToolTip="单击进行排序" Width="100%" PagerStyle-AlwaysVisible="true">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <HeaderContextMenu EnableEmbeddedSkins="False">
                        </HeaderContextMenu>
                        <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                            DataKeyNames="ApplyID,PSN_ServerID">
<CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <Columns>

                                <telerik:GridTemplateColumn   UniqueName="SelectAllColumn">
                                <HeaderTemplate>
                                    <uc3:CheckAll ID="CheckAll1" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" CssClass="ck" runat="server" onclick='checkBoxClick(this.checked);' />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="36" />
                            </telerik:GridTemplateColumn>

                                <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_Sex" DataField="PSN_Sex" HeaderText="性别">
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="PSN_CertificateNO" HeaderText="证件号码">
                                    <ItemTemplate>
                                        <%# Eval("PSN_CertificateNO") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_RegisteProfession" DataField="PSN_RegisteProfession" HeaderText="注册专业">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="PSN_RegisterNo" DataField="PSN_RegisterNo" HeaderText="注册编号">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn UniqueName="ENT_Name" DataField="ENT_Name" HeaderText="企业名称">
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>                               
                                <telerik:GridTemplateColumn UniqueName="OldUnitCheckTime" HeaderText="申报日期" SortExpression="OldUnitCheckTime" >
                                <ItemTemplate>
                                    <%# (Eval("newUnitCheckTime")!=DBNull.Value &&  Eval("OldUnitCheckTime")!=DBNull.Value && Convert.ToDateTime(Eval("newUnitCheckTime")) >Convert.ToDateTime(Eval("OldUnitCheckTime")) ) 
                                            ?Convert.ToDateTime(Eval("newUnitCheckTime")).ToString("yyyy.MM.dd")
                                            :(Eval("OldUnitCheckTime")!=DBNull.Value
                                            ?Convert.ToDateTime(Eval("OldUnitCheckTime")).ToString("yyyy.MM.dd")
                                            :(Eval("Memo")!=DBNull.Value && Eval("Memo").ToString()=="申请强制注销" && Eval("ApplyTime")!=DBNull.Value)
                                                ?Convert.ToDateTime(Eval("ApplyTime")).ToString("yyyy.MM.dd")
                                                :"")%>    
                                </ItemTemplate> 
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridTemplateColumn>
                                   <telerik:GridTemplateColumn UniqueName="ApplyType" HeaderText="申报事项">
                                        <ItemTemplate>
                                            <%# Eval("ApplyType").ToString()=="变更注册"?Eval("ApplyTypeSub"):Eval("ApplyType").ToString()=="延期注册"?"延续注册":Eval("ApplyType") %>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="ConfirmResult" HeaderText="最终结果" UniqueName="ConfirmResult" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn UniqueName="progress" HeaderText="进度">
                                    <ItemTemplate>
                                       <%# BindApplyStatus(Eval("ApplyStatus").ToString())%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                 <telerik:GridTemplateColumn UniqueName="progressDate" HeaderText="进度日期">
                                    <ItemTemplate>
                                      <%# Eval("ApplyStatus").ToString() =="未申报"?(Eval("CJSJ")==DBNull.Value?"":Convert.ToDateTime(Eval("CJSJ")).ToString("yyyy-MM-dd")):
                                        Eval("ApplyStatus").ToString() =="待确认"?(Eval("ApplyTime")==DBNull.Value?"":Convert.ToDateTime(Eval("ApplyTime")).ToString("yyyy-MM-dd")):
                                        Eval("ApplyStatus").ToString() =="已申报"?((Eval("Memo")!=DBNull.Value && Eval("Memo").ToString()=="申请强制注销" && Eval("ApplyTime")!=DBNull.Value)?Convert.ToDateTime(Eval("ApplyTime")).ToString("yyyy.MM.dd")
                                                                                  :Eval("OldUnitCheckTime")==DBNull.Value?"":Convert.ToDateTime(Eval("OldUnitCheckTime")).ToString("yyyy-MM-dd")):
                                        Eval("ApplyStatus").ToString() =="已受理"?(Eval("GetDateTime")==DBNull.Value?"":Convert.ToDateTime(Eval("GetDateTime")).ToString("yyyy-MM-dd")):
                                        Eval("ApplyStatus").ToString() =="区县审查"?(Eval("ExamineDatetime")==DBNull.Value?"":Convert.ToDateTime(Eval("ExamineDatetime")).ToString("yyyy-MM-dd")):
                                        Eval("ApplyStatus").ToString() =="已上报"?(Eval("ReportDate")==DBNull.Value?"":Convert.ToDateTime(Eval("ReportDate")).ToString("yyyy-MM-dd")):
                                        Eval("ApplyStatus").ToString() =="已审查"?(Eval("CheckDate")==DBNull.Value?"":Convert.ToDateTime(Eval("CheckDate")).ToString("yyyy-MM-dd")):
                                        Eval("ApplyStatus").ToString() =="已决定"?(Eval("ConfirmDate")==DBNull.Value?"":Convert.ToDateTime(Eval("ConfirmDate")).ToString("yyyy-MM-dd")):
                                        Eval("ApplyStatus").ToString() =="已公示"?(Eval("PublicDate")==DBNull.Value?"":Convert.ToDateTime(Eval("PublicDate")).ToString("yyyy-MM-dd")):
                                        Eval("ApplyStatus").ToString() =="已公告"?(Eval("NoticeDate")==DBNull.Value?"":Convert.ToDateTime(Eval("NoticeDate")).ToString("yyyy-MM-dd")):""%>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                </telerik:GridTemplateColumn>
                                 <telerik:GridTemplateColumn UniqueName="progressResult" HeaderText="审批结果">
                                        <ItemTemplate>
                                            <%# Eval("ApplyStatus").ToString() =="已驳回"?(Eval("GetDateTime")==DBNull.Value?"":Eval("GetResult")):
                                                Eval("ApplyStatus").ToString() =="待确认"?"等待企业确认":
                                                Eval("ApplyStatus").ToString() =="已申报"?"提交审核":
                                                Eval("ApplyStatus").ToString() =="已受理"?Eval("GetResult"):
                                                Eval("ApplyStatus").ToString() =="区县审查"?Eval("ExamineResult"):  
                                                Eval("ApplyStatus").ToString() =="已审查"?Eval("CheckResult"):                                             
                                                Eval("ApplyStatus").ToString() =="已决定"?Eval("ConfirmResult"):
                                                Eval("ApplyStatus").ToString() =="已公告"?Eval("ConfirmResult"):""%>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridTemplateColumn>
                                <telerik:GridTemplateColumn UniqueName="Edit" HeaderText="详细">
                                    <ItemTemplate>
                                         <%--2020-02-12 南静修改参数传参加密,源代码为:o=<%# Eval("PSN_ServerID").ToString() %>&a=<%# Eval("ApplyID") %>&v=1--%>
                                        <%--<span class="link_edit" onclick='javascript:SetIfrmSrc("../Unit/<%# GetApplyTypeUrl( Eval("ApplyID")==DBNull.Value?ApplyType:Eval("ApplyType").ToString()) %>?o=<%# Eval("PSN_ServerID") %>&a=<%# Eval("ApplyID") %>&v=1")'>详细</span>--%>
                                        <span class="link_edit" onclick='javascript:SetIfrmSrc("<%# GetApplyTypeUrl(Eval("ApplyType").ToString(),Eval("ApplyTypeSub").ToString()) %>?o=<%# Utility.Cryptography.Encrypt(Eval("PSN_ServerID").ToString()) %>&a=<%# Utility.Cryptography.Encrypt(Eval("ApplyID").ToString()) %>&v=1")'>详细</span>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                    <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                                </telerik:GridTemplateColumn>
                            </Columns>
<PagerStyle AlwaysVisible="True"></PagerStyle>
                            <HeaderStyle Font-Bold="True" />
<CommandItemStyle HorizontalAlign="Left"></CommandItemStyle>
                            <PagerTemplate>
                                <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                            </PagerTemplate>
                        </MasterTableView>
<PagerStyle AlwaysVisible="True"></PagerStyle>
                        <FilterMenu EnableEmbeddedSkins="False">
                        </FilterMenu>
                        <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.ApplyMDL"
                        SelectMethod="GetList" TypeName="DataAccess.ApplyDAL"
                        SelectCountMethod="SelectCount" EnablePaging="true"
                        MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                        </div>
                </div>
                <div  style="width:98%; text-align:center;margin:8px 0;text-align:right;padding-right:40px" >
                    <asp:Button ID="ButtonPrint" runat="server" Text="打印受理通知单" CssClass="bt_maxlarge" OnClick="ButtonPrint_Click" Visible="False"  />  
                            &nbsp; &nbsp;&nbsp;<asp:Button ID="BtnReturn2" runat="server" Text="返回" CssClass="bt_large"  Visible="False" OnClick="BtnReturn2_Click"  />        
                    <span id="spanOutput" runat="server" class="excel" style="padding-right:40px; font-weight:bold"></span><asp:Button ID="ButtonOutput" runat="server" Text="导出查询结果" CssClass="bt_large" OnClick="ButtonOutput_Click" />
                </div>
            </div>
        </div>
        <uc2:IframeView ID="IframeView" runat="server" />
    </form>
     <script type="text/javascript">
        function checkBoxAllClick(checkBoxAllClientID) {
            if (checkBoxAllClientID == undefined) return;
            var ckall = document.getElementById(checkBoxAllClientID);
            if (ckall == null) return;
            var grid = ckall.parentNode;
            while (grid != null && grid != undefined && grid.nodeName != "div") {
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
       </script>
</body>
</html>
