<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BatUploadImg.aspx.cs"
    Inherits="ZYRYJG.EXamManage.BatUploadImg" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <center>
        <div>
            <table width="600px" >
                <tr>
                    <td valign="bottom">
                        上传照片：
                    </td>
                    <td>
                        <telerik:RadAsyncUpload runat="server" ID="RadAsyncUploadFacePhoto" MultipleFileSelection="Automatic"
                            AllowedFileExtensions="jpg" MaxFileInputsCount="1000" Culture="(Default)" Width="270px"  MaxFileSize="51200"
                            Height="22px" ControlObjectsVisibility="None">
                            <Localization Delete="删除" Remove="删除" Select="选择文件" />
                        </telerik:RadAsyncUpload>
                    </td>
                    <td valign="bottom">
                        <asp:Button ID="ButtonUploadImg" runat="server" Text="上传" CssClass="button" OnClick="ButtonUploadImg_Click" />
                        （一次可上传多个照片）
                    </td>
                </tr>
            </table>
        </div>
        </center>
    </form>
</body>
</html>
