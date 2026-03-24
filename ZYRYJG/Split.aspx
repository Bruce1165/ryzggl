<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Split.aspx.cs" Inherits="ZYRYJG.Split" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function hideshow() {
            parent.document.all.frameset1.cols = parent.document.all.frameset1.cols == "260,12,*" ? "0,12,*" : "260,12,*";
            document.getElementById("div1").className = parent.document.getElementById("frameset1").cols == "260,12,*" ? "yckj_left" : "yckj_right";
        }
    </script>
    <style type="text/css">
        
.yckj_bg {
    background-color:  transparent;
    width: 12px;
    margin:0;
    /*background-repeat: repeat-y;*/
}
.yckj_left {
    background: url(./images/yckj_center_left.gif);
  width:12px;
		  height:210px;
    background-repeat: no-repeat;
}

.yckj_right {
    background: url(./images/yckj_center_right.gif);
   width:12px;
		  height:210px;
    background-repeat: no-repeat;
}
    </style>
</head>
<body class="yckj_bg">
    <div id="div1" class="yckj_left" style="margin-top: 110px; cursor: pointer; height: 210px;"
        onclick="hideshow()" />
</body>
</html>