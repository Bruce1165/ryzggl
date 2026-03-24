<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SourceWarePaly.aspx.cs" Inherits="Student_SourceWarePaly" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript" src="../Scripts/jquery-3.4.1.min.js"></script>
     <script src="../Scripts/Public.js?v=1.012" type="text/javascript"></script>
        <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <script>
        var kjid = "";
        function pause() {
            IfrmView.src = "about:blank";
            layer.alert('认真听讲，课后答题！继续加油。', { icon: 6, time: 0, shade: [0.9, '#000', true] }, function () { play(); });
        }
        function play() {
            location.replace("SourceWarePaly.aspx?o=" + kjid + "&" + Math.random())
        }
        window.setInterval(function () { pause(); }, 600000);
    </script>
</head>
<body>
    <form id="form1" runat="server">
     <iframe id="IfrmView"  
          frameborder="0" src="about:blank" scrolling="no" onload="SetCwinHeight2()"
        style="overflow: visible; text-align: center;   background-color:transparent; width:100%; height:100%" ></iframe>
    </form>
</body>
</html>
