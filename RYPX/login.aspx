<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div style="width: 100%; text-align: left; padding: 100px 100px; line-height: 300%; ">
            <div style="line-height:400%;font-size:30px">个人登录采用北京集成用户登录，这里用于上线前个人模拟登录</div>
         <asp:Label ID="Labelsfzhm" runat="server" Text="用户名："></asp:Label><asp:TextBox ID="RadTextBoxCID" runat="server" Text="" Width="160px" Height="28px"></asp:TextBox>
            <asp:Button ID="ButtonWorker" runat="server" Text="个人登陆" OnClick="ButtonWorker_Click" Style="cursor: pointer;" class="button" onfocus="this.blur()" />
          
         
        </div>
 
    </div>
    </form>
</body>
</html>
