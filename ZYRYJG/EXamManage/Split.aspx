<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Split.aspx.cs" Inherits="PSMSweb.EXamManage.Split" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
   <link href="../Css/styleRed.css" rel="stylesheet" type="text/css" />
 <script type="text/javascript">
         function   hideshow()
        {   
           parent.document.all.frameset1.cols= parent.document.all.frameset1.cols=="160,6,*"?"0,6,*":"160,6,*";  
           document.getElementById("div1").className= parent.document.getElementById("frameset1").cols== "160,6,*"?"yckj_left":"yckj_right";
        }   
	    </script>
</head>
<body class="yckj_bg">
    <div id="div1" class="yckj_left" style="MARGIN-TOP:200px; CURSOR: pointer; HEIGHT: 43px" onclick="hideshow()"/>
</body>
</html>