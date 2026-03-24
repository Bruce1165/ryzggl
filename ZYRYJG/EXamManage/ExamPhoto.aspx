<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="ExamPhoto.aspx.cs" Inherits="ZYRYJG.EXamManage.ExamPhoto" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">

        <script type="text/javascript">
            function GetRadWindow() {
                var oWindow = null;
                if (window.radWindow) oWindow = window.radWindow;
                else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
                return oWindow;
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadWindowManager ID="RadWindowManager1" ShowContentDuringLoad="false" VisibleStatusbar="false"
        ReloadOnShow="true" runat="server" Skin="Windows7" EnableShadow="true">
    </telerik:RadWindowManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" />
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 考务管理 &gt;&gt;考场管理
                &gt;&gt; <strong>照片阵列</strong>
            </div>
        </div>
        <div class="table_border" style="width: 750px; margin: 3px auto;">
            <div class="content">
                <div id="DivContent">
                    <p class="jbxxbt" style="text-align: center">
                        考生照片一览表</p>
                    <div>
                        <table cellpadding="5" cellspacing="1" border="0" width="660px" class="table" align="center">
                            <tr class="GridLightBK">
                                <td width="15%" align="right" nowrap="nowrap">
                                    <strong>考试名称：</strong>
                                </td>
                                <td colspan="3">
                                    <asp:Label ID="LabelExamPlanName" ReadOnly="True" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td width="15%" align="right" nowrap="nowrap">
                                    <strong>考点名称：</strong>
                                </td>
                                <td width="40%">
                                    <asp:Label ID="RadTextBoxExamPlaceName" ReadOnly="True" runat="server" Text=""></asp:Label>
                                </td>
                                <td width="15%" align="right" nowrap="nowrap">
                                    <strong>考场号：</strong>
                                </td>
                                <td width="40%">
                                    <asp:Label ID="lblExamRoomCode" ReadOnly="True" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                            <tr class="GridLightBK">
                                <td align="right" nowrap="nowrap">
                                    <strong>考试日期：</strong>
                                </td>
                                <td width="119">
                                    <asp:Label ID="RadTextBoxExamDate" ReadOnly="True" runat="server" Text=""></asp:Label>
                                </td>
                                <asp:PlaceHolder ID="PlaceHolder2" runat="server"></asp:PlaceHolder>
                                <td nowrap="nowrap" align="right">
                                    <strong>考场人数：</strong>
                                </td>
                                <td>
                                    <asp:Label ID="RadTextBoxExamPERSONNUMBER" ReadOnly="True" runat="server" Text=""></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div style="margin: 0px auto;">
                        <table cellpadding="0" cellspacing="1" border="0" width="90%" align="center">
                            <tr>
                                <td align="center">
                                    <telerik:RadListView ID="RadListViewPhoto"  runat="server"
                                        Width="750px" AllowPaging="true" PageSize="25" ItemPlaceholderID="ProductsHolder">
                                        <ItemTemplate>
                                            <div style='float: left; '>
                                                <table cellpadding="0" cellspacing="0" align="center" width="130" >
                                                    <tr>
                                                        <td style="padding-left: 10px;" align="center">
                                                        <div style="border:solid 1px #cccccc; padding:1px 1px; height:136px; width:108px;" >
                                                            <%--<img alt="" style="border: none;" height="130px" width="102px" src='ExamSignimage.aspx?o=../UpLoad/SignUpPhoto/<%# Eval("ExamPlanID") %>/<%# Eval("CertificateCode") %>.jpg' />--%>
                                                            <img alt="" style="border: none;" height="130px" width="102px" src='ExamSignimage.aspx?o=<%# Utility.Cryptography.Encrypt(GetFacePhotoPath( Convert.ToString(Eval("ExamPlanID")) ,Convert.ToString(Eval("CertificateCode")))) %>' />
                                                            
                                                            </div>
                                                            
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 10px;" align="center">
                                                            <%# Eval("WorkerName") %>&nbsp;<%# Eval("ExamCardID")%>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 10px;" align="center">
                                                            <%# Eval("CertificateCode")%>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div><%# (Convert.ToInt16(Eval("RowNum")) +1) % 5 ==1?"<br />":""  %> 
                                        </ItemTemplate>
                                        <LayoutTemplate>
                                            <fieldset style="width: 660px;" id="RadListView1">
                                                <asp:Panel ID="ProductsHolder" runat="server" />
                                                <table cellpadding="0" cellspacing="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="RadListViewPhoto"
                                                                PageSize="25">
                                                                <Fields>
                                                                    <telerik:RadDataPagerTemplatePageField>
                                                                        <PagerTemplate>
                                                                            <table width="100%">
                                                                                <tr>
                                                                                    <td align="left">
                                                                                        <b>第&nbsp;<asp:Label runat="server" ID="CurrentPageLabel" Text="<%# Container.Owner.StartRowIndex /25 +1%>" />&nbsp;页&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                            共&nbsp;<asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.Owner.PageCount%>" />&nbsp;页&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                            每页&nbsp;<asp:Label runat="server" ID="TotalPagesLabel" Text="25" />&nbsp;条
                                                                                            <br />
                                                                                        </b>
                                                                                    </td>
                                                                                    <td align="right">
                                                                                        <asp:LinkButton ID="LinkButtonFirst" CommandName="Page" CausesValidation="false"
                                                                                            CommandArgument="First" runat="server">头页</asp:LinkButton>
                                                                                        <asp:LinkButton ID="LinkButtonPrev" CommandName="Page" CausesValidation="false" CommandArgument="Prev"
                                                                                            runat="server">上一页</asp:LinkButton>
                                                                                        <asp:LinkButton ID="LinkButtonNext" CommandName="Page" CausesValidation="false" CommandArgument="Next"
                                                                                            runat="server">下一页</asp:LinkButton>
                                                                                        <asp:LinkButton ID="LinkButtonLast" CommandName="Page" CausesValidation="false" CommandArgument="Last"
                                                                                            runat="server">尾页</asp:LinkButton>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </PagerTemplate>
                                                                    </telerik:RadDataPagerTemplatePageField>
                                                                </Fields>
                                                            </telerik:RadDataPager>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>
                                        </LayoutTemplate>
                                    </telerik:RadListView>
                                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.ExamResultDAL"
                                        DataObjectTypeName="Model.ExamResultOB" SelectMethod="GetListView" EnablePaging="true"
                                        SelectCountMethod="SelectCountView" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                                        SortParameterName="orderBy">
                                        <SelectParameters>
                                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                                DefaultValue="" ConvertEmptyStringToNull="false" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                            <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                        </table>
                    </div>
                </div>
                <div style="width: 95%; margin: 10px auto; text-align: center; clear: left;">
               
                    <asp:Button ID="ButtonExport" runat="server" Text="导出打印" CssClass="button" OnClick="ButtonExport_Click" />&nbsp;&nbsp;
                    <input id="Button1" type="button" value="返 回" class="button" onclick="javascript:GetRadWindow().close();" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
