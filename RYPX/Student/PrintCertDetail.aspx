<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.master" AutoEventWireup="true" CodeFile="PrintCertDetail.aspx.cs" Inherits="Student_PrintCertDetail" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc1" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .ke {
           background:url(../img/xz.jpg) no-repeat #fcfcfc left center;
           padding:20px 20px 30px 250px;
           margin:20px 20px;
           border:1px solid #f6f6f6;
            border-right:3px solid #e9e9e9;
             border-bottom:3px solid #e9e9e9;
           border-radius:20px 20px;           
        }
        .ke_t {
            font-size: 24px;
            font-weight: bolder;
            line-height: 300%;
        }
        .ke_r {
            clear: both;
            line-height: 300%;
        }
        .ke_td {
            width: 49%;
            float: left;
            position: relative;
            line-height: 200%;
        }
        .clear {
            clear: both;
        }
    </style>
    <div class="div_main" style="padding: 8px 8px;">
        <div id="div_top" class="div_mainTop">
            <div class="div_road">
            </div>
        </div>
        <div class="div_fun">
            学习成果
        </div>
        <div class="content" style="text-align:center;padding-left:10%">
            <telerik:RadGrid ID="RadGridMyPackage" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="10" ShowHeader="false"
                EnableAjaxSkinRendering="false" BorderStyle="None"
                EnableEmbeddedSkins="false" Skin="Default" Width="80%">
                <ClientSettings EnableRowHoverStyle="false">
                </ClientSettings>
                <MasterTableView CommandItemDisplay="None" DataKeyNames="CertificateCode,ValidEndDate" NoMasterRecordsText="　没有可展示的学习成果">
                    <Columns>
                        <telerik:GridTemplateColumn HeaderText="" UniqueName="ValidEndDate" SortExpression="ValidEndDate desc">
                            <ItemTemplate>
                                <div class="ke">
                                    <div class="ke_td">
                                        岗位类别：<%# Eval("PostTypeName")%>
                                    </div>  
                                    <div class="ke_td">
                                        专业：<%# Eval("PostName")%>
                                    </div>  
                                     <div class="clear"></div>   
                                    <div class="ke_td">
                                        证书编号：<%# Eval("CertificateCode")%>
                                    </div>  
                                     <div class="ke_td">
                                        有效期至：<%# Eval("ValidEndDate")%>
                                    </div>  
                                      <div class="clear"></div>                                  
                                
                                    <div class="ke_td">
                                        课时:  <%# Convert.ToInt32(Eval("Period"))/45==0?"":string.Format("{0}课时",Convert.ToString(Convert.ToInt32(Eval("Period"))/45))%> <%# Convert.ToInt32(Eval("Period"))/45==0?"":string.Format("{0}课时",Convert.ToString(Convert.ToInt32(Eval("Period"))/45))%>
                                    </div>                                   
                                    <div class="ke_td">
                                        达标时间: <%# Convert.ToDateTime(Eval("FinishDate")).ToString("yyyy年MM月dd日")%>
                                    </div>
                                    <div class="clear"></div>                              
                                </div>
                                <div class="clear"></div>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" Wrap="false" Font-Bold="true" />
                            <ItemStyle HorizontalAlign="Center" Wrap="false" />
                        </telerik:GridTemplateColumn>                       
                    </Columns>
                    <HeaderStyle Font-Bold="true" />
                    <PagerStyle AlwaysVisible="false" />
                    <PagerTemplate>                        
                        <uc2:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                    </PagerTemplate>
                </MasterTableView>
            </telerik:RadGrid>
            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" TypeName="DataAccess.FinishCertDAL"
                DataObjectTypeName="Model.FinishCertMDL" SelectMethod="GetList" EnablePaging="true"
                SelectCountMethod="SelectCount" MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex"
                SortParameterName="orderBy">
                <SelectParameters>
                    <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                        DefaultValue="" ConvertEmptyStringToNull="false" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>

