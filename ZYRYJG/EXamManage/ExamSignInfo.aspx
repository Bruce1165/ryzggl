<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/RadControls.Master"
    CodeBehind="ExamSignInfo.aspx.cs" Inherits="ZYRYJG.EXamManage.ExamSignInfo" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadAjaxManager ID="RadAjaxManager2" runat="server" EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridExamSignUp">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridExamSignUp" LoadingPanelID="RadAjaxLoadingPanel2" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel2" runat="server" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div id="div_top" class="dqts">
            <div id="divRoad" runat="server" style="float: left;">
                当前位置 &gt;&gt; 综合查询 &gt;&gt;注册中心人员报名查询
                &gt;&gt; 报名情况详细信息
            </div>
        </div>
        <div class="table_border" style="width: 98%; margin: 5px auto;">
            <div class="content">
                <div class="jbxxbt">
                    报名情况详细信息</div>
                <div>
                    <div style="width: 98%; margin: 0 auto;">
                        <telerik:RadGrid ID="RadGridExamSignUp" AutoGenerateColumns="False" runat="server"
                            AllowPaging="True" PageSize="10" AllowSorting="True" SortingSettings-SortToolTip="单击进行排序"
                            Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" Width="100%"
                            GridLines="None">
                            <ClientSettings Selecting-AllowRowSelect="false" EnableRowHoverStyle="false">
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView EditMode="PopUp" CommandItemDisplay="None" DataKeyNames="ExamSignUpID,CertificateCode,SignUpCode"
                                NoMasterRecordsText="　没有可显示的记录">
                                <Columns>
                                    <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="PostTypeName" DataField="PostTypeName" HeaderText="岗位类别"
                                        Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="PostName" DataField="PostName" HeaderText="岗位工种"
                                        Visible="false">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="WorkerName" DataField="WorkerName" HeaderText="姓名">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="CertificateType" DataField="CertificateType"
                                        HeaderText="证件类型">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="CertificateCode" DataField="CertificateCode"
                                        HeaderText="证件号码">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="UnitName" DataField="UnitName" HeaderText="单位全称">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="UnitCode" DataField="UnitCode" HeaderText="组织机构代码">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="Status" DataField="Status" HeaderText="状态">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn UniqueName="IsConditions" DataField="IsConditions" HeaderText="是否符合条件">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridBoundColumn>
                                    <%--                                    <telerik:GridTemplateColumn UniqueName="Faceimage" HeaderText="照片">
                                        <ItemTemplate>
                                            <asp:Image ID="Image1" runat="server" Width="14" Height="18" ImageUrl='<%#  ShowFaceimage(Eval("CertificateCode").ToString())  %>'
                                                onmouseover="setImgSize(this,100,140,'absolute',event);" onmouseout="setImgSize(this,14,18,'relative',event);">
                                            </asp:Image>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridTemplateColumn>--%>
                                </Columns>
                                <PagerTemplate>
                                    <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                                </PagerTemplate>
                            </MasterTableView>
                        </telerik:RadGrid>
                        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.ExamSignUpDAL"
                            DataObjectTypeName="Model.ExamSignUpOB" SelectMethod="GetList_New" InsertMethod="Insert"
                            EnablePaging="true" UpdateMethod="Update" DeleteMethod="Delete" SelectCountMethod="SelectCount_New"
                            MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                            SortParameterName="orderBy">
                            <SelectParameters>
                                <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                    DefaultValue="" ConvertEmptyStringToNull="false" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </div>
                    <div style="width: 90%; margin: 5px auto; text-align: center;">
                        <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript:hideIfam();" />
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
