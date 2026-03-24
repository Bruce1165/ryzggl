<%@ Page Title="" Language="C#" MasterPageFile="~/RadControls.Master" AutoEventWireup="true"
    CodeBehind="BatUploadPhoto.aspx.cs" Inherits="ZYRYJG.EXamManage.BatUploadPhoto" %>

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
            function rtn() {
                var oArg = new Object();
                var oWnd = GetRadWindow();
                oWnd.close(oArg);
            }
        </script>

    </telerik:RadCodeBlock>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server" DefaultLoadingPanelID="RadAjaxLoadingPanel1"
        EnableAJAX="true">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="ButtonUploadImg">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="ButtonUploadImg" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Visible="true" />
    <div class="jbxxbt">
        批量上传照片
    </div>
    <div class="div_out">
        <div class="content">
            <div id="DivApplyBat" runat="server" style="width: 100%; margin: 5px auto; text-align: center; padding-top: 8px;">
                <table width="100%">
                    <tr>
                        <td align="left" style="padding-left: 9px; padding-top: 10px;">
                            <span id="SpanTip" runat="server" style="font-size: 12px;">批量上传照片：（格式要求：一寸jpg格式图片，最大为50K，宽高102
                                    X 140像素）</span> &nbsp;&nbsp;&nbsp;&nbsp;辅助工具下载：<a href="../Images/1寸照片生成器.exe" style="text-decoration: underline; color: Blue;"><img alt="" src="../Images/Soft_common.gif" style="border-width: 0;" />
                                        1寸照片生成器.exe</a>
                            <div style="color: Red; padding-left: 85px">
                                （注意：图片名称必须使用证书上的证件号码，如身份证“210504198805200015”。
                                    如果您的电脑设置了隐藏文件的扩展名，请百度搜索“怎么显示文件的扩展名”，避免上传图片名称出现两个“jpg”（如：210504198805200015.jpg.jpg）造成无法显示已上传照片。）
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <div style="float: left; clear: left; line-height: 28px; width: 68px; text-align: left;">
                            </div>
                            <div id="DivvUpload" runat="server" style="float: left;">
                                <telerik:RadAsyncUpload runat="server" ID="RadAsyncUploadFacePhoto" MultipleFileSelection="Automatic"
                                    AutoAddFileInputs="true" OverwriteExistingFiles="true" AllowedFileExtensions="jpg" MaxFileSize="51200"
                                    MaxFileInputsCount="1" Culture="(Default)" Width="215px"
                                    Skin="Hot" EnableAjaxSkinRendering="false" EnableEmbeddedSkins="false" ControlObjectsVisibility="None"
                                    InitialFileInputsCount="1" TemporaryFileExpiration="04:00:00" OnFileUploaded="RadAsyncUploadFacePhoto_FileUploaded"
                                    HttpHandlerUrl="~/EXamManage/CustomHandler.ashx" Enabled="true" EnableFileInputSkinning="false">
                                    <Localization Delete="" Remove="" Select="选择照片" />
                                </telerik:RadAsyncUpload>
                            </div>
                            <div style="float: left; padding-left: 13px; z-index: 9999;">
                                <asp:Button ID="ButtonUploadImg" runat="server" Text="上 传" CssClass="button" OnClick="ButtonUploadImg_Click" />&nbsp;&nbsp;
                            </div>

                        </td>
                    </tr>
                </table>
            </div>
            <br />
        </div>
    </div>
</asp:Content>
