<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Upload.aspx.cs" Inherits="ZYRYJG.uploader.Upload" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <script src="../Scripts/jquery-3.4.1.min.js"></script>
    <script src="./webuploader.js?v=1.009992"></script>

    <link href="./style.css" rel="stylesheet" />
    <link href="./webuploader.css" rel="stylesheet" />
        <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
</head>
<body>

    <div id="uploader">
        <div class="queueList">
            <div id="divUploaded" runat="server" class="uploaded">
            </div>
            <div style="clear: both;">
            </div>
            <div id="dndArea" class="placeholder">
                <div id="filePicker"></div>

            </div>
        </div>
        <div class="statusBar" style="display: none;">
            <div class="progress">
                <span class="text">0%</span>
                <span class="percentage"></span>
            </div>
            <div class="info"></div>
            <div class="btns">
                <div id="filePicker2"></div>
                <div class="uploadBtn">开始上传</div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        var uploadDir = '<%=UploadDir%>';
        var param_a = '<%=ApplyID%>';
        var param_t = '<%=FileBusType%>';
        var param_s = '<%=FileShowName%>';
    </script>
    <script src="./upload.js?v=1.0934"></script>
    <script>
    
        $divUploaded = $("#divUploaded");

        $("#divUploaded li").on('mouseenter', function () {
            $("#" + this.id + " .file-panel").animate({ height: 30 });
        });
        $("#divUploaded li").on('mouseleave', function () {
            $("#" + this.id + " .file-panel").animate({ height: 0 });
        });

        
        function delfile(li,url) {
            $.ajax({
                type: "post",
                url: "UploadHandler.ashx?a=0&o=" + encodeURIComponent(url),
                async: true,
                dataType: "json",
                success: function (result) {
          
                    if (result == true) {
                        $("#" + li).remove();
                    }
                    else {
                        alert("删除失败：" + result);
                    }
                }
            });
        };
        function lookimg(str) {
            var newwin = window.open();
      
          
            newwin.document.write('<img src=' + str + " width='200' height='200' />");
        }
    </script>


</body>
</html>
