<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WordView.aspx.cs" Inherits="ZYRYJG.QuestionMgr.WordView" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
      <script type="text/javascript" language="javascript" src="script.js"></script>
</head>
<body onload="load();">
    <form id="form1" runat="server" method="post" enctype="multipart/form-data"  >
        <div>
            <input id="Button2" type="button" value="保 存"  onclick="return saveword('<%=SaveUrl%>')" />
        </div>
    <div>
        <object classid="clsid:00460182-9E5E-11d5-B7C8-B8269041DD57" codebase="dsoframer.CAB#Version=2.3.0.0" id="oframe" width="100%" height="100%">  
             <param name="BorderStyle" value="1">  
             <param name="TitlebarColor" value="52479">  
             <param name="TitlebarTextColor" value="0">  
            <PARAM NAME="Titlebar" VALUE="0">
	<PARAM NAME="Toolbars" VALUE="1">
	<PARAM NAME="Menubar" VALUE="0">
           
       </object>
   <input type="file" name="File" id = "File" value="" style="display:none"/>

    </div>
        <script type="text/javascript">


            function load()
            {
                var url = location.search;
                if (url.indexOf("?") != -1) {
                    varstr = url.substr(1)　//去掉?号
                    loadword(varstr);
                }
            }

            function loadword(url) {
              
                oframe = document.getElementById("oframe");
                var readurl = location.href.split('?')[0].replace("WordView.aspx", "WordRead.aspx");
                var ww = readurl + "?" + url + "&" + Math.random();
           
                //var ww = "http://localhost:55886/QuestionMgr/WordRead.aspx?u=" + url + "&" + Math.random();
                oframe.Open(ww, true, "Word.Document");               
            }

            function saveword(url) {
                oframe = document.getElementById("oframe");
                oframe.HttpInit();             
                oframe.HttpAddPostCurrFile('File', '');
                var url = url + "?o=" + Math.random();
                //var url = "http://localhost:55886/QuestionMgr/WordWrite.aspx?o=" + Math.random();
                oframe.HttpPost(url);
        }
    </script>
    </form>    
</body>
</html>
