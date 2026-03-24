<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Splitnew.aspx.cs" Inherits="ZYRYJG.EXamManage.Splitnew" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
 <link href="../Css/styleRed.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" charset="gb2312">
        function   hideshow()
        {   
           window.parent.document.getElementsByTagName('frameset')[1].cols= window.parent.document.getElementsByTagName('frameset')[1].cols=="265,12,*"?"0,12,*":"265,12,*";  
           document.getElementById("div1").className= parent.document.getElementById("frameset1").cols== "265,12,*"?"yc_left":"yc_right";
        }   
	    </script>
</head>
<body>
    <div id="div1" class="yc_left" style="MARGIN-TOP:110px; CURSOR: pointer; HEIGHT: 210px;" onclick="hideshow()"/>
</body>
</html>