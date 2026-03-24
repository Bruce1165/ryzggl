<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReleaseNew.aspx.cs" validateRequest="false" Inherits="ZYRYJG.Register.ReleaseNew" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
        <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" /> 
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script src="../ueditor/ueditor.all.min.js" type="text/javascript"></script>

    <script src="../ueditor/lang/zh-cn/zh-cn.js" type="text/javascript"></script>
    <script type="text/javascript">
        function save()
        {
            document.getElementById("HiddenField1").value = ue.getContent();// ue.getPlainTxt();
        }
    </script>

 <%--  <script type="text/javascript">
       
       container.ready(function () {
           container.setContent(cont);
       })
   </script>--%>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        </telerik:RadStyleSheetManager>
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Skin="Windows7">
        </telerik:RadWindowManager>
        <div class="div_out">
            <div class="table_border" style="width: 99%; margin: 5px auto; padding-bottom: 30px;">
                <div id="div_top" class="dqts">
                    <div style="float: left;">
                        当前位置 &gt;&gt;业务受理 &gt;&gt;<strong>政策通知</strong>
                    </div>
                </div>
                <div class="content">
                    <div style="width: 95%; height: 100%; margin: 10px auto; text-align: center;">
                        <table class="table" width="98%">
                            <tr>
                                <td>标题：</td>
                                <td>
                                    <telerik:RadTextBox ID="RadTextBoxtitle" runat="server" Skin="Telerik" Width="98%"></telerik:RadTextBox></td>
                            </tr>
                            <tr>
                                <td>内容：</td>
                                <td>
                                    
                                    <script id="container" type="text/plain"  style="margin: 0 auto; width: 100%; height: 400px; overflow-y:hidden"></script>
                                    <script type="text/javascript">
                                        var ue = UE.getEditor('container', {
                                            //toolbars: [
                                            //    ['bold', 'italic', 'underline', 'forecolor', 'justifyleft', 'justifyright', 'justifycenter', 'justifyjustify', 'undo', 'redo', 'insertorderedlist', 'insertunorderedlist', 'directionalityltr', 'directionalityrtl', 'cleardoc', 'searchreplace', 'help', 'preview', 'print', 'fontsize', 'fontfamily']
                                            //],
                                            autoHeightEnabled: false,
                                            autoFloatEnabled: true
                                           
                                        });
                                    </script>
                                </td>
                            </tr>
                            <tr>
                                <td>附件：</td>
                                <td>
                                    <asp:FileUpload ID="FileUpload1" runat="server" Width="50%"  />

                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="FileUpload1" runat="server" ErrorMessage="上传附件格式不允许" Display="Dynamic" ValidationExpression="^.+(.doc|.docx|.xls|.xlsx|.pdf|.rar|.zip|.jpg|.png|.gif)$"></asp:RegularExpressionValidator>
                                    <asp:Button ID="Btn_Next" runat="server" Width="60px" Text="查看" OnClick="Btn_Next_Click" Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="ButtonSave" CssClass="bt_large" runat="server" Text="保 存" CausesValidation="true" OnClick="ButtonSave_Click" OnClientClick="return save()" />
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="HiddenField1" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
