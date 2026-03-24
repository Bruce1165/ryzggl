<%@ Page Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="CertifReportUpload.aspx.cs" Inherits="ZYRYJG.RenewCertifates.CertifReportUpload" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager2" runat="server" EnableAJAX="true">
        <AjaxSettings>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Windows7" />
    <telerik:RadWindowManager runat="server" RestrictionZoneID="offsetElement" ID="RadWindowManager1"
        EnableShadow="true" EnableEmbeddedScripts="true" Skin="Windows7" VisibleStatusbar="false">
    </telerik:RadWindowManager>
    <div class="div_out">
        <div id="div_top" class="dqts">
            <div style="float: left;">
                当前位置 &gt;&gt; 证书续期 &gt;&gt;
                <strong>初审汇总上报</strong>
            </div>
        </div>
        <div class="table_border" style="width: 98%; margin: 5px auto;">
            <div class="content">
                <div class="jbxxbt">
                    续期初审汇总上报附件上传
                </div>

                <div class="DivContent" id="Td3">
                    <b style="color: Red">上传附件说明：</b><br />
                    1、上报需要上传“续期初审汇总签字表”（扫描件，jpg格式图片）<br />
                    2、上报需要上传“续期初审汇总明细”（Excel）<br />
                    3、上传扫描件图片大小不要过大<br />
                </div>
                <table border="0" cellpadding="5" cellspacing="1" class="table2" align="center" width="96%">
                    <tr class="GridLightBK">
                        <td width="15%" nowrap="nowrap" align="right">汇总批次号：
                        </td>
                        <td width="35%" align="left">
                            <asp:Label ID="LabelReportCode" runat="server" Text=""></asp:Label>
                        </td>

                        <td width="15%" nowrap="nowrap" align="right">汇总日期：
                        </td>
                        <td width="35%" align="left">
                            <asp:Label ID="LabelReportDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td nowrap="nowrap" align="right">岗位类别：
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelPostTypeName" runat="server" Text=""></asp:Label>
                        </td>

                        <td nowrap="nowrap" align="right">证书数量：
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelCertCount" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td nowrap="nowrap" align="right">初审单位名称：
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelChechUnit" runat="server" Text=""></asp:Label>
                        </td>

                        <td nowrap="nowrap" align="right">初审时间：
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelCheckDate" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>
                    <tr class="GridLightBK">
                        <td align="right" style="font-size: 13px; font-weight: bold;">上传扫描件：
                        </td>
                        <td align="left" ">
                            <telerik:RadUpload ID="RadUploadReportImg" runat="server" ControlObjectsVisibility="None"
                                Height="23px" MaxFileInputsCount="1" OverwriteExistingFiles="True" Skin="Hot"
                                EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" ReadOnlyFileInputs="False"
                                Width="300px">
                                <Localization Select="选 择" />
                            </telerik:RadUpload>
                            （jpg格式）
                        </td>
                        <td align="right" style="font-size: 13px; font-weight: bold;">上传Excel：
                        </td>
                        <td align="left" ">
                            <telerik:RadUpload ID="RadUploadExcel" runat="server" ControlObjectsVisibility="None"
                                Height="23px" MaxFileInputsCount="1" OverwriteExistingFiles="True" Skin="Hot"
                                EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" ReadOnlyFileInputs="False"
                                Width="300px">
                                <Localization Select="选 择" />
                            </telerik:RadUpload>（xls或xlsx格式）
                        </td>
                    </tr>
                </table>

                <div>

                    <asp:Button ID="ButtonUpload" runat="server" Text="开始上传" CssClass="bt_large" OnClick="ButtonUpload_Click" />
                    <input id="ButtonReturn" type="button" value="返 回" class="button" onclick="javascript: hideIfam(true);" />

                </div>
                <br />
            </div>
        </div>
    </div>

</asp:Content>
