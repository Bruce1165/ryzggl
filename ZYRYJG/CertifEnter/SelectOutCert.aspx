<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SelectOutCert.aspx.cs" Inherits="ZYRYJG.CertifEnter.SelectOutCert" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="../GridPagerTemple.ascx" TagName="GridPagerTemple" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../css/StyleRed.css?v=1.0.1" rel="stylesheet" type="text/css" />
    <link href="../Skins/Blue/Grid.Blue.css?v=1.008" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery-3.4.1.min.js"></script>
        <link href="../layer/skin/layer.css" rel="stylesheet" />
    <style>
        @keyframes blink {
          0% { padding-left:0; }
          40% { padding-left:2px; }
          100% { padding-left:0;}
        } 
        .blinking-text {
          animation: blink 2s infinite;
        }
    </style>
    <script src="../layer/layer.js" type="text/javascript"></script>
    <script>
        function setdata(ENT_Name, ENT_OrganizationsCode, CreditCode) {
            document.getElementById("HiddenCertCode").value = ENT_Name;
        }

        function callbackdata() {
            var data = {
                CertCode: document.getElementById("HiddenCertCode").value
            };
            return data;
        }

        function waitDown() {
                  var num = 61;
               var myVar = setInterval(function () {
                   num--;
                   if (num == 0) {
                       clearInterval(myVar);
                       __doPostBack('DownCert', '');
                   }
                   $("#SpanWait").text("同步中，请耐心等待：" + num + " 秒。");
                }, 1000);
            }
    </script>
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
                <%--<telerik:AjaxSetting AjaxControlID="ButtonSearch">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridCert" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>--%>
                <telerik:AjaxSetting AjaxControlID="RadGridCert">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="RadGridCert" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" Skin="Default" />
        <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">          
        </telerik:RadCodeBlock>
        <div class="content">
            <div class="DivContent">
                <p style="color:red;text-indent: 32px;" class="blinking-text">特别提醒：请申请人登录“全国工程质量安全监管信息平台公共服务门户”-安全生产管理人员考核合格证书信息栏目查询原证书信息，应为“办理转出”状态方可同步获取到转出证书信息，提交进京申请。</p>
                <p>
                （同步数据来源：全国工程质量安全监管信息平台公共服务门户）  <asp:Button ID="ButtonSearch" runat="server" Text="获取跨省转入证书信息" CssClass="bt_large" OnClick="ButtonSearch_Click" style="width:240px" /><span id="SpanWait" style="color:red;font-weight:bold;padding-left:40px;font-size:20px;"></span></p>
            </div>
            <div style="width: 99%; margin: 10px auto; text-align: center;">
               
                <telerik:RadGrid ID="RadGridCert" runat="server"
                    GridLines="None" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" OnItemDataBound="RadGridCert_ItemDataBound"
                    SortingSettings-SortToolTip="单击进行排序" Width="100%"  EnableAjaxSkinRendering="false" Skin="Blue" 
                    EnableEmbeddedSkins="false" PagerStyle-AlwaysVisible="true" >
                    <ClientSettings EnableRowHoverStyle="true" Selecting-AllowRowSelect="true">
                    </ClientSettings>
                    <HeaderContextMenu EnableEmbeddedSkins="False">
                    </HeaderContextMenu>
                    <MasterTableView NoMasterRecordsText=" <span style='color:red'>没有查到可用的证书转出信息,请登录“全国工程质量安全监管信息平台公共服务门户”查询证书为“办理转出”状态后再同步证书。</span>" 
                        CommandItemDisplay="None" DataKeyNames="out_certNum" Caption="同步证书结果列表（提示：点击一行选择数据，单击“确定”按钮返回申请单。）">
                        <Columns>
                            <telerik:GridBoundColumn UniqueName="RowNum" DataField="RowNum" HeaderText="序号" AllowSorting="false">
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </telerik:GridBoundColumn>
                             <telerik:GridTemplateColumn HeaderText="岗位工种" UniqueName="out_categoryCode">
                                <ItemTemplate>
                                    <%# DataAccess.CertificateDAL.GetSLRPostNameByCode(Eval("out_categoryCode").ToString()) %>                                  
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false"  />
                            </telerik:GridTemplateColumn>
                           
                            <telerik:GridBoundColumn UniqueName="out_certNum" DataField="out_certNum"
                                HeaderText="证书编号">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="有效期起始" UniqueName="out_effectiveDate" DataField="out_effectiveDate"
                                DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="有效期截止" UniqueName="out_expiringDate" DataField="out_expiringDate"
                                DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                              <%--<telerik:GridTemplateColumn HeaderText="证书状态" UniqueName="out_certState">
                                <ItemTemplate>
                                    <%# DataAccess.CertificateDAL.Get_GB_Aqscglry_CertStateNameByCode(Eval("out_certState").ToString()) %>                                  
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" ForeColor="Blue" />
                            </telerik:GridTemplateColumn>--%>
                          <%--  <telerik:GridBoundColumn UniqueName="out_name" DataField="out_name" HeaderText="姓名">
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" Wrap="false" />
                            </telerik:GridBoundColumn>
                              <telerik:GridBoundColumn UniqueName="out_identityCard" DataField="out_identityCard" HeaderText="证件号码">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>--%>
                               
                            <telerik:GridBoundColumn UniqueName="out_companyName" DataField="out_companyName" HeaderText="聘用单位名称">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn UniqueName="out_issuAuth" DataField="out_issuAuth" HeaderText="发证单位">
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn HeaderText="发证日期" UniqueName="out_issuedDate" DataField="out_issuedDate"
                                DataFormatString="{0:yyyy-MM-dd}">
                                <HeaderStyle HorizontalAlign="Left" Wrap="false" />
                                <ItemStyle HorizontalAlign="Left" Wrap="false" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <HeaderStyle Font-Bold="True" />
                        <PagerTemplate>
                            <uc1:GridPagerTemple ID="GridPagerTemple1" runat="server" />
                        </PagerTemplate>
                    </MasterTableView>
                    <FilterMenu EnableEmbeddedSkins="False">
                    </FilterMenu>
                </telerik:RadGrid>

                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" DataObjectTypeName="Model.CertificateOutMDL"
                    SelectMethod="GetList" TypeName="DataAccess.CertificateOutDAL"
                    SelectCountMethod="SelectCount" EnablePaging="true"
                    MaximumRowsParameterName="maximumRows" StartRowIndexParameterName="startRowIndex" SortParameterName="orderBy">
                    <SelectParameters>
                        <asp:QueryStringParameter Name="filterWhereString" QueryStringField="filterWhereString"
                            DefaultValue="" ConvertEmptyStringToNull="false" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </div>
        <input id="HiddenCertCode" type="hidden" />
    </form>
</body>
</html>
