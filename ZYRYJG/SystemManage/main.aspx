<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="main.aspx.cs" Inherits="ZYRYJG.SystemManage.main" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="div_out">
        <div class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 系统管理 &gt;&gt;
                岗位管理 &gt;&gt; <strong>岗位、工种、考试科目</strong>
            </div>
        </div>
        <div class="table_border" style="width: 98%; margin: 5px auto;">
            <div class="content">
                <p class="jbxxbt">
                    岗位、工种、考试科目</p>
                <div>
                    <div class="table_cx">
                        <img alt="" src="../Images/jglb.png" width="15" height="15" style="margin-bottom: -2px;
                            padding-right: 2px;" />
                        树型结构列表（点击行头小箭头可以展开）
                    </div>
                    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                        <AjaxSettings>
                            <telerik:AjaxSetting AjaxControlID="RadGrid1">
                                <UpdatedControls>
                                    <telerik:AjaxUpdatedControl ControlID="RadGrid1" LoadingPanelID="RadAjaxLoadingPanel1" />
                                </UpdatedControls>
                            </telerik:AjaxSetting>
                        </AjaxSettings>
                    </telerik:RadAjaxManager>
                    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
                    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
                        EnableShadow="true" EnableEmbeddedScripts="true">
                    </telerik:RadWindowManager>
                    <telerik:RadGrid ID="RadGrid1" runat="server" ShowStatusBar="True"
                        Width="95%" AutoGenerateColumns="False" PageSize="99999" AllowSorting="True"
                        AllowPaging="True" GridLines="None" OnItemUpdated="RadGrid1_ItemUpdated" OnItemDeleted="RadGrid1_ItemDeleted"
                        OnItemInserted="RadGrid1_ItemInserted" OnInsertCommand="RadGrid1_InsertCommand"
                        OnDetailTableDataBind="RadGrid1_DetailTableDataBind" ShowDesignTimeSmartTagMessage="False"
                        Skin="Blue" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false">
                        <ClientSettings EnableRowHoverStyle="true">
                        </ClientSettings>
                        <PagerStyle Mode="NumericPages"></PagerStyle>
                        <MasterTableView  DataKeyNames="PostID" AllowMultiColumnSorting="True"
                            Width="100%" CommandItemDisplay="None" Name="Customers">
                            <DetailTables>
                                <telerik:GridTableView DataKeyNames="PostID" Width="100%" runat="server" CommandItemDisplay="None"
                                    Name="Post">
                                    <DetailTables>
                                        <telerik:GridTableView DataKeyNames="PostID,UpPostID" Width="100%" runat="server"
                                            CommandItemDisplay="None" Name="Project">
                                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                            <Columns>                                        
                                                <telerik:GridBoundColumn SortExpression="PostName" HeaderText="科目名称" HeaderButtonType="TextButton"
                                                    DataField="PostName" UniqueName="PostName">
                                                </telerik:GridBoundColumn>                                               
                                            </Columns>                                          
                                        </telerik:GridTableView>
                                    </DetailTables>
                                    <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                                    <ExpandCollapseColumn Visible="True">
                                    </ExpandCollapseColumn>
                                    <Columns>                                       
                                        <telerik:GridBoundColumn SortExpression="PostName" HeaderText="工种名称" HeaderButtonType="TextButton"
                                            DataField="PostName" UniqueName="PostName">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="ExamFee" HeaderText="考试费用(元)" HeaderButtonType="TextButton"
                                            DataField="ExamFee" UniqueName="ExamFee">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="CodeFormat" HeaderText="放号规则" HeaderButtonType="TextButton"
                                            DataField="CodeFormat" UniqueName="CodeFormat">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn SortExpression="CurrentNumber" HeaderText="当前尾号" HeaderButtonType="TextButton"
                                            DataField="CurrentNumber" UniqueName="CurrentNumber">
                                        </telerik:GridBoundColumn>
                                    </Columns>                                  
                                </telerik:GridTableView>
                            </DetailTables>
                            <%--第一层--%>
                            <CommandItemSettings ExportToPdfText="Export to Pdf"></CommandItemSettings>
                            <ExpandCollapseColumn Visible="True">
                            </ExpandCollapseColumn>
                            <Columns>                          
                                <telerik:GridBoundColumn SortExpression="PostName" HeaderText="岗位名称" HeaderButtonType="TextButton"
                                    DataField="PostName" UniqueName="PostName">
                                    <HeaderStyle HorizontalAlign="left" />
                                    <ItemStyle HorizontalAlign="left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="CodeFormat" HeaderText="放号规则" HeaderButtonType="TextButton"
                                    DataField="CodeFormat" UniqueName="CodeFormat">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn SortExpression="CurrentNumber" HeaderText="当前尾号" HeaderButtonType="TextButton"
                                    DataField="CurrentNumber" UniqueName="CurrentNumber">
                                </telerik:GridBoundColumn>                                
                            </Columns>                          
                            <EditFormSettings>
                                <EditColumn UniqueName="EditCommandColumn1">
                                </EditColumn>
                            </EditFormSettings>
                        </MasterTableView>
                    </telerik:RadGrid>
                    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.PostInfoDAL"
                        DataObjectTypeName="Model.PostInfoOB" SelectMethod="GetList" InsertMethod="Insert"
                        EnablePaging="true" UpdateMethod="Update" DeleteMethod="Delete" SelectCountMethod="SelectCount"
                        MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                        SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="and PostType=1" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" TypeName="DataAccess.PostInfoDAL"
                        DataObjectTypeName="Model.PostInfoOB" SelectMethod="GetList" InsertMethod="Insert"
                        EnablePaging="true" UpdateMethod="Update" DeleteMethod="Delete" SelectCountMethod="SelectCount"
                        MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                        SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="and PostType=2 " ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" TypeName="DataAccess.PostInfoDAL"
                        DataObjectTypeName="Model.PostInfoOB" SelectMethod="GetList" InsertMethod="Insert"
                        EnablePaging="true" UpdateMethod="Update" DeleteMethod="Delete" SelectCountMethod="SelectCount"
                        MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                        SortParameterName="orderBy">
                        <SelectParameters>
                            <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                                DefaultValue="and PostType=3" ConvertEmptyStringToNull="false" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
