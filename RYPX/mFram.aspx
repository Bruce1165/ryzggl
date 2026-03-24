<%@ Page Language="C#" AutoEventWireup="true" CodeFile="mFram.aspx.cs" Inherits="mFram" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>北京市建设行业从业人员公益培训平台 1.10</title>
    <script type="text/javascript" src="./Scripts/jquery-3.4.1.min.js"></script>
    <script type="text/javascript" src="./layer/layer.js"></script>
    <link type="text/css" rel="stylesheet" href="./layer/skin/layer.css" />
</head>
<frameset rows="150,*" frameborder="no" border="0" framespacing="0">
    <frame src="Head.aspx" name="topFrame" scrolling="no" noresize="noresize" id="topFrame" />
    <frame src="./Student/TopClass.aspx" id="sFram" name="sFram"  runat="server"></frame>
</frameset>
<body style="background: #FFF0E1;">
</body>
</html>
