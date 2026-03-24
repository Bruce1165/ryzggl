<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginJava.aspx.cs" Inherits="ZYRYJG.LoginJava" EnableViewState="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link href="./Css/styleRed.css?v=1.01" rel="stylesheet" type="text/css" />
     <script src="./Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="Scripts/BigInt.js"></script>
    <script type="text/javascript" src="Scripts/Barrett.js"></script>
    <script type="text/javascript" src="Scripts/RSA.js"></script>
    <script type="text/javascript" src="Scripts/CodeManage.js"></script>
    <link href="layer/skin/layer.css" rel="stylesheet" />
    <script src="layer/layer.js" type="text/javascript"></script>
          
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align:center;line-height:100px;font-size:x-large;font-weight:bolder">测试专用</div>
    <div style="text-align:center;line-height:100px;">
    
        用户名：<asp:TextBox ID="RadTextBoxCID" runat="server" Text="" Width="160px" Height="28px" MaxLength="50"></asp:TextBox>
          <asp:Button ID="ButtonWorker" runat="server" Text="登 陆" OnClick="ButtonWorker_Click" Style="cursor: pointer;" class="button" onfocus="this.blur()" />
    </div>
    </form>
</body>
</html>
