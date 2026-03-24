<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sFram.aspx.cs" Inherits="sFram" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<frameset cols="250,*" frameborder="no" border="0" framespacing="0" style="background: #FFF4F4;">
        <frame src="menu.aspx" id="menuFram" name="menuFram"></frame>
        <frame src="./Student/BaseInfoEdit.aspx" name="mainFrame" id="mainFrame"  style="background: #FFF4F4; margin:20px 20px"  />
</frameset>
<body style="background: #FFF0E1;">
</body>
</html>
