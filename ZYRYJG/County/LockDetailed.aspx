<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LockDetailed.aspx.cs" Inherits="ZYRYJG.County.LockDetailed" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <style type="text/css">
        .auto-style1 {
            height: 30px;
        }
        .auto-style2 {
            height: 30px;
            width: 130px;
        }
    </style>
    </head>
<body>
    <form id="form1" runat="server">
          <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
            EnableAJAX="true">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="ButtonSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQY" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
                <telerik:AjaxSetting AjaxControlID="RadGridQY">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridQY" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
    <div class="div_out">
        <div id="div_top" class="dqts">
              <div style="float: left; width: 244px;">
                    当前位置 &gt;&gt;证书锁定 &gt;&gt;<strong>证书加锁</strong>
                </div>
        </div>
       <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
            <div class="content">
                <div style="background: url(../Images/Lock.png) left center no-repeat; background-size:12%; ">
                <%--<h1 style="text-align:center;color:red">证书加锁</h1>--%>
         <table  border="1"  style="text-align:center;width:100%; border-collapse:collapse;border:1px solid  #999"  >
                    <tr id="TrPerson" runat="server">
                        <td style="width:140px">  <asp:Label ID="Lbl_SFZH" runat="server" Text="Label" Visible="false"></asp:Label></td>
                        <td   style="width:12%;height:30px;text-align:right" >
                           证书编号：
                        </td>
                        <td  style="width:25%;height:20px;text-align:left;">
                            <asp:Label ID="Lbl_PSN_RegisterCertificateNo" runat="server" Text="Label"></asp:Label>

                        </td>                       
                        <td style="text-align:right" class="auto-style2">
                           姓名：
                        </td>
                        <td style=" text-align:left;" >
                         <asp:Label ID="Lbl_PSN_Name" runat="server" Text="Label" ></asp:Label>
                        </td>
                      <%--  <td align="left" style="padding-left:40px">
                            </td>--%>
                    </tr>
                    <tr>
                        <td></td>
                        <td style="width:80px;height:20px;text-align:right">
                         加锁人：
                        </td> 
                        <td  style="text-align:left;" >
                        <asp:Label ID="Lbl_LockPeople" runat="server" Text="Label"></asp:Label>
                        </td>
                        <td style="text-align:right" class="auto-style2">
                            加锁日期：
                        </td>
                        <td style="text-align:left;">
                             <asp:Label ID="Lbl_LockStartTime" runat="server" Text="Label"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td></td>
                        <td style="text-align:right" class="auto-style1" >
                        锁定截止日期：
                        </td> 
                        <td   style="text-align:left;color:#999">
                           <telerik:RadDatePicker ID="RadDatePickerGetDateEnd" MinDate="01/01/1900" runat="server" Calendar-DayCellToolTipFormat="yyyy年MM月dd日"
                                    Width="120px" />(到期后锁定无效)
                        </td>
                        <td style="text-align:right" class="auto-style2">类型：</td>
                             
                        <td style="text-align:left;"> <asp:Label ID="Lbl_lx" runat="server" Text="Label"></asp:Label></td>
                    </tr>

                    <tr><td></td>
                    <td style="text-align:right;height:50px" class="auto-style1" >锁定原因说明：</td> 
                    <td colspan="3" class="auto-style1" style="text-align:left;">
                        <asp:TextBox ID="TextBox1" runat="server" TextMode="MultiLine" Width="90%" Height="40"></asp:TextBox>
                        </td>
                           
                    </tr>
                </table>
                    </div>
                 <div style="text-align:center;padding-top:10px" >
                         <asp:Button ID="ButtonOK" runat="server" CssClass="bt_large" Text="确认" OnClick="ButtonOK_Click" />&nbsp;&nbsp;   
                        &nbsp;&nbsp;<input id="BtnReturn" type="button" class="bt_large" value="取消" onclick='javascript: hideIfam()' />
                 </div>
                 <div style="color:red;text-align:center;"><h2>证书锁定记录</h2></div>
                  <div class="content">
                      
                       <telerik:RadGrid ID="RadGridQY" runat="server"
                    GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" PageSize="15"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%" PagerStyle-AlwaysVisible="true">
                    <ClientSettings EnableRowHoverStyle="true">
                    </ClientSettings>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView NoMasterRecordsText=" 没有可显示的记录" CommandItemDisplay="None" CommandItemStyle-HorizontalAlign="Left"
                        DataKeyNames="Fid">
                        <Columns>

                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="PSN_Name" DataField="PSN_Name" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn UniqueName="PSN_RegisterCertificateNo" HeaderText="证件编号">
                                <ItemTemplate>
                                    <%# Eval("PSN_RegisterCertificateNo") %>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn>
                             <telerik:GridBoundColumn UniqueName="LockStartTime" DataField="LockStartTime" HeaderText="锁定时间"  DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="LockEndTime" DataField="LockEndTime" HeaderText="锁定截止时间"  DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
     
                            <telerik:GridBoundColumn UniqueName="LockPeople" DataField="LockPeople" HeaderText="锁定人">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>    
                            <telerik:GridBoundColumn UniqueName="UnlockTime" DataField="UnlockTime" HeaderText="解锁时间"  DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn> 
                            <telerik:GridBoundColumn UniqueName="UnlockPeople" DataField="UnlockPeople" HeaderText="解锁人">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>  
                             <telerik:GridBoundColumn UniqueName="LX" DataField="LX" HeaderText="类型">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>                     
                           
                              <telerik:GridBoundColumn UniqueName="LockStates" DataField="LockStates" HeaderText="状态">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn> 
                                
                              <telerik:GridTemplateColumn UniqueName="LockContent" HeaderText="加锁原因">
                                <ItemTemplate>
                                      <asp:Label  id="txt_Start" runat="server"  Text='<%#Eval("LockContent").ToString().Length>2 ? Eval("LockContent").ToString().Substring(0,2)+"..." :Eval("LockContent").ToString() %>' ToolTip='<%#Eval("LockContent").ToString()%>'>
                                    </asp:Label>
                              
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn>

                              <telerik:GridTemplateColumn UniqueName="UnlockContent" HeaderText="解锁原因">
                                <ItemTemplate>
                                   <asp:Label  id="txt_End" runat="server"  Text='<%#Eval("UnlockContent").ToString().Length>2 ? Eval("UnlockContent").ToString().Substring(0,2)+"..." :Eval("UnlockContent").ToString() %>' ToolTip='<%#Eval("UnlockContent").ToString()%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridTemplateColumn>

                               
                        </Columns>
                        <HeaderStyle Font-Bold="True" />
                        <PagerTemplate>
                            <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <FilterMenu EnableEmbeddedSkins="False">
                    </FilterMenu>
                    <SortingSettings SortToolTip="单击进行排序"></SortingSettings>
                </telerik:RadGrid>

                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.COC_TOW_Person_BaseInfoMDL"
                    SelectMethod="GetLockList" TypeName="DataAccess.Certificate_LockDAL"
                    SelectCountMethod="GetCountLockList" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>

                  </div>
              </div>
        </div>
         <uc2:IframeView ID="IframeView" runat="server" />
    </div>
    </form>
</body>
</html>
