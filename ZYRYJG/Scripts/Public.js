var ie = (navigator.appName.indexOf("Microsoft") != -1) ? true : false;

// 这一段使得FireFox的HTMLElement具有click方法（add click method to HTMLElement in Mozilla） 
try {
    // create span element so that HTMLElement is accessible 
    document.createElement("span");
    HTMLElement.prototype.click = function () {
        if (typeof this.onclick == "function")
            this.onclick({ type: "click" });
    };
}
catch (e) {
    // alert("click method for HTMLElement couldn\"t be added"); 
}
// 对HTMLAnchorElement 加入onclick事件 
try {
    // create a element so that HTMLAnchorElement is accessible 
    document.createElement("a");
    HTMLElement.prototype.click = function () {
        if (typeof this.onclick == "function") {
            if (this.onclick({ type: "click" }) && this.href)
                window.open(this.href, this.target ? this.target : "_self");
        }
        else if (this.href)
            window.open(this.href, this.target ? this.target : "_self");
    };
}
catch (e) {
    // alert("click method for HTMLAnchorElement couldn\"t be added"); 
}
//* text 模拟 link
function textMouseover(obj) {
    obj.textDecoration = "underline";
}
function textMouseout(obj) {
    obj.style.textDecoration = "none";
}

function getRootPath() {
    //获取当前网址
    var curWwwPath = window.document.location.href;
    //获取主机地址之后的目录
    var pathName = window.document.location.pathname;
    var pos = curWwwPath.indexOf(pathName);
    //获取主机地址，如： http://localhost:8083
    var localhostPaht = curWwwPath.substring(0, pos);
    return localhostPaht;
}

//获取InnerText或textContent
function getInnerText(obj) {
    return obj.innerText ? obj.innerText : obj.textContent;
}

//设置InnerText或textContent
function setInnerText(obj, value) {
    if (obj.innerText != undefined)
        obj.innerText = value;
    else
        obj.textContent = value;
}
//ifram自适应高度
function SetCwinHeight() {
    var ifrmView = document.getElementById("IfrmView"); //iframe id
    var divFram = document.getElementById("divFram");
    document.documentElement.scrollTop = 0;
    if (document.getElementById) {
        if (ifrmView && !window.opera) {
            if (ifrmView.contentDocument && ifrmView.contentDocument.body.offsetHeight) {

                ifrmView.height = ifrmView.contentDocument.body.offsetHeight + 60;
              

            } else if (ifrmView.Document && ifrmView.Document.body.scrollHeight) {

                ifrmView.height = ifrmView.Document.body.scrollHeight + 60;
              
            }
   
            
        }
        if (ifrmView != null && ifrmView.src != "about:blank") {
          
            divFram.style.top = "0px";
            divFram.style.left = "8px";
            divFram.style.zIndex = 99999;

            var top;
            if (ifrmView.contentDocument)
                top = ifrmView.contentDocument.getElementById("functionMenu");
            else
                top = ifrmView.Document.getElementById("functionMenu");
            if (top == null) {
                if (ifrmView.contentDocument)
                    top = ifrmView.contentDocument.getElementById("div_top");
                else
                    top = ifrmView.Document.getElementById("div_top");
                if (top != null) top.innerHTML += "<div id='functionMenu' class='right_t_menu' style='float: right; margin-right: 2%'>&nbsp;&nbsp;<a href='#' onclick='javascript:hideIfam();'><img src='" + getRootPath() + "/Images/close.gif' alt='关闭窗口' style='border:0px;' /></a></div>";
            }
            else if (top.innerHTML.indexOf("关闭窗口") == -1)
                top.innerHTML += "&nbsp;&nbsp;<a href='#' onclick='javascript:hideIfam();'><img src='" + getRootPath() + "/Images/close.gif' alt='关闭窗口' style='border:0px;' /></a>";
        }
    }
}

//ifram模拟弹出模式窗口
function SetIfrmSrc(url) {
    SetIfrmSrcMode(url, true);

    var divFram = document.getElementById("divFram");
    if (divFram != null) divFram.style.display = "block";
}

//ifram模拟弹出窗口,IsMode：true表示模式窗口、false表示非模式窗口

function SetIfrmSrcMode(url, isMode) {
    var ifrmView = document.getElementById("IfrmView"); //iframe id
    if (ifrmView != null) {
        ifrmView.src = urlRandom(url);

        //ifrmView.style.borderLeft = "#5C9DD3 4px solid";
        //ifrmView.style.borderTop = "#5C9DD3 24px solid";
        //ifrmView.style.borderBottom = "#3E7FB5 6px solid";
        //ifrmView.style.borderRight = "#3E7FB5 6px solid";

        ifrmView.style.display = "block";

        //遮挡层        
        document.getElementById("HiddenOpenMode").value = (isMode == true) ? "true" : "false";
        if (document.getElementById("HiddenOpenMode").value == "true") {
            var iframeGray = document.getElementById("iframeGray");
            iframeGray.style.width = document.documentElement.scrollWidth + "px";
            iframeGray.style.height = document.documentElement.scrollHeight + "px";
            iframeGray.style.top = "0px";
            iframeGray.style.left = "0px";
            iframeGray.style.zIndex = 0;
            iframeGray.style.display = "block";
        }
    }
}
//为链接添加随机参数，迫使ifram每次刷新
function urlRandom(url) {
    return url;
    if (url.indexOf("?") != -1)
        return url + "&" + Math.random();
    else
        return url + "?" + Math.random();
}
//刷新GridView当前页数据
var PageBarGo = null;
function bindGridView() {
    if (document.getElementById("PageBarGo") != null)
        document.getElementById("PageBarGo").onclick();
    else
        location.href = location.href;
}
//隐藏Ifram中的页面
var isfresh = false; //是否刷新父页面，用于特殊情况返回按钮需要刷新WriteScriptStart("isfresh", "var isfresh=true;");
function hideIfam() { 
    if (parent.document.getElementById("divFram") != null) {

        if (isfresh == true) {
            parent.refreshGrid();
        }

        parent.document.getElementById("divFram").style.top = "-99999px";
        parent.document.getElementById("divFram").style.display = "none";

        var ifrmView = document.getElementById("IfrmView");
        if (ifrmView != null) ifrmView.src = "about:blank";

        if (parent.document.getElementById("iframeGray") != null) {
            parent.document.getElementById("iframeGray").style.display = "none";
        }
        parent.document.body.style.backgroundColor = "#FFFFFF";
    }
    else
        history.go(-1);
}
function hideIfam(bool_fresh) {
    if (parent.document.getElementById("divFram") != null) {
        if (bool_fresh == true || (bool_fresh == undefined && isfresh==true)) {
            parent.refreshGrid();
        }

        parent.document.getElementById("divFram").style.top = "-99999px";
        parent.document.getElementById("divFram").style.display = "none";

        var ifrmView = document.getElementById("IfrmView");
        if (ifrmView != null) ifrmView.src = "about:blank";

        if (parent.document.getElementById("iframeGray") != null) {
            parent.document.getElementById("iframeGray").style.display = "none";
        }
        parent.document.body.style.backgroundColor = "#FFFFFF";
    }
    else
        history.go(-1);
}
function hideIfam1() {
    if (parent.document.getElementById("divFram") != null) {
        //alert("hide");
        parent.document.getElementById("divFram").style.top = "-99999px";
        parent.document.getElementById("divFram").style.display = "none";

        var IfrmView = document.getElementById("IfrmView");
        if (IfrmView != null) IfrmView.src = "about:blank";

        if (parent.document.getElementById("iframeGray") != null) {
            parent.document.getElementById("iframeGray").style.display = "none";
        }
        parent.document.body.style.backgroundColor = "#FFFFFF";
        //
        var IfrmCalendar = parent.document.getElementById("IfrmCalendar"); //iframe id
        if (IfrmCalendar == null) {//非首页才刷新
            //alert("fresh");
            if (parent != null) parent.location = parent.location; //.reload();
        }
        else {
            //alert("nofresh");
        }
    }
    else
        history.go(-1);
}
//刷新父页面

function freshPage(url) {
    var IfrmCalendar = parent.document.getElementById("IfrmCalendar"); //iframe id
    if (IfrmCalendar != null) {
        var d = new Date();
        if (url.length > 30) IfrmCalendar.src = url + "&s=" + d.getSeconds();
    }
    else {
        //alert(url);
        //if (parent != null) parent.location = url; // parent.location.reload();
    }
}
//获取ifram的document
function getDocument(ifram) {
    if (ifram.contentDocument)
        return ifram.contentDocument;
    else
        return ifram.document;
}

function HTMLEncode(input) {
    var converter = document.createElement("DIV");
    setInnerText(converter, input);
    var output = converter.innerHTML;
    converter = null;
    return output.replace(/</g, "&lt;").replace(/>/g, "&gt;");
    s = s.replace(/</g, "&lt;");
    s = s.replace(/>/g, "&gt;");
}

function HTMLDecode(input) {
    var converter = document.createElement("DIV");
    converter.innerHTML = input;
    var output = getInnerText(converter);
    converter = null;
    return output.replace(/&lt;/g, "<").replace(/&gt;/g, ">");
}

//设置页面url
function postToUrl(obj) {
    location.href = obj.getAttribute("url");
}

//grid行选择
function checkBoxClick(checked) {
    if (checked == true) return;
    var ckall = document.getElementById(CheckBoxAllClientID);
    if (ckall == null) return;
    if (ckall.checked == true) ckall.checked = false;
}
//
function checkAllTip(checkBoxAllClientId) {
    var ckall = document.getElementById(checkBoxAllClientId);
    if (ckall == null) return;
    if (ckall.checked == true) {
        alert("注意：您全选了所有页数据（不仅仅是当前页），您接下来的操作将对所有数据进行处理！");
    }
}

//grid全选
function checkBoxAllClick(checkBoxAllClientId) {
    if (checkBoxAllClientId == undefined) return;
    var hideSelectAllRefreshCount = document.getElementById(checkBoxAllClientId.replace("CheckBoxAll", "HiddenFieldSelectAllRefreshCount"));
    var hideSelectAll = document.getElementById(checkBoxAllClientId.replace("CheckBoxAll", "HiddenFieldSelectAll"));
    var ckall = document.getElementById(checkBoxAllClientId);
    if (ckall == null) return;
    hideSelectAll.value = ckall.checked;
    hideSelectAllRefreshCount.value = "0";
    var grid = ckall.parentNode;
    while (grid != null && grid != undefined && grid.nodeName != "TABLE") {
        grid = grid.parentNode;
    }
    var ifSelect = ckall.checked;
    var chks;
    if (grid == undefined)
        chks = document.getElementsByTagName("input");
    else
        chks = grid.getElementsByTagName("input");

    if (chks.length) {
        for (var i = 0; i < chks.length; i++) {
            if (chks[i].type == "checkbox") {
                chks[i].checked = ifSelect;
            }
        }
    }
    else if (chks) {
        if (chks.type == "checkbox") {
            chks.checked = ifSelect;
        }
    }
}

//客户端提示
function OpenAlert(tip) {
    radalert("<table width='200px' style='table-layout:fixed; '><tr><td style='word-break:break-all;padding10px 0px;'>" + tip + "</td></tr></table>", 200, 185, '提示信息');
    return false;

    //alert(tip);
}

//** Div折叠效果 需要样式表DIV.DivTitleOn支持*********
//<div class="PageRoad" style="padding: 0 0 50px 0">
//    当前位置：一般列表查询模板
//</div>
//<div class="DivTitleOn" onclick="DivOnOff('Td3',event);" >种植信息</div>
//<div class="DivContent" id="Td3">
// 内容......
//</div>
//*****************************************************
function DivOnOff(send, objId, eventObj) {
    var e = eventObj || window.event;
    var srcElement = e.srcElement || e.target;
    var objDesc = document.getElementById(objId);
    if (objDesc.style.display == "none") {
        srcElement.className = "DivTitleOn";
        objDesc.style.display = "";
        send.setAttribute("title", "折叠");
    }
    else {
        srcElement.className = "DivTitleOff";
        objDesc.style.display = "none";
        send.setAttribute("title", "展开");
    }
}

//判断GridView是否选择一个checkBox，适用于Button控件
//参数：【alt：提示信息，如请勾选一条记录，可选项，默认为“请勾选需要操作的记录”】【divID：控件容器ID，可选项，默认为document】
function isSelectRow(alt, divId) {
    var chks;
    if (divId == undefined)
        chks = document.getElementsByTagName("input");
    else
        chks = document.getElementById(divId).getElementsByTagName("input");

    if (chks == null) return false;

    if (chks.length) {
        for (var i = 0; i < chks.length; i++) {
            if (chks[i].type == "checkbox") {
                if (chks[i].checked == true) {
                    return true;
                }
            }
        }
    }
    if (alt != undefined)
        alert(alt);
    else
        alert("至少选择一条记录");
    return false;
}

//js本地图片预览，兼容ie[6-9]、火狐、Chrome17+、Opera11+、Maxthon3
//<div id="divPreview" style="position: absolute; top: 60px; left: 0;width:95%;text-align:center;">
//        <img id="ImgReportList" width="300px"  src="../Images/null.gif" alt="汇总扫描件" />
//</div>
function PreviewImage(fileObj, imgPreviewId, divPreviewId) {
    var allowExtention = ".jpg,.bmp,.gif,.png"; //允许上传文件的后缀名document.getElementById("hfAllowPicSuffix").value;
    var extention = fileObj.value.substring(fileObj.value.lastIndexOf(".") + 1).toLowerCase();
    var browserVersion = window.navigator.userAgent.toUpperCase();
    if (allowExtention.indexOf(extention) > -1) {
        if (fileObj.files) {//HTML5实现预览，兼容chrome、火狐7+等
            if (window.FileReader) {
                var reader = new FileReader();
                reader.onload = function (e) {
                    document.getElementById(imgPreviewId).setAttribute("src", e.target.result);
                }
                reader.readAsDataURL(fileObj.files[0]);
            } else if (browserVersion.indexOf("SAFARI") > -1) {
                alert("不支持Safari6.0以下浏览器的图片预览!");
            }
        } else if (browserVersion.indexOf("MSIE") > -1) {
            if (browserVersion.indexOf("MSIE 6") > -1) {//ie6
                document.getElementById(imgPreviewId).setAttribute("src", fileObj.value);
            } else {//ie[7-9]
                fileObj.select();
                if (browserVersion.indexOf("MSIE 9") > -1)
                    fileObj.blur(); //不加上document.selection.createRange().text在ie9会拒绝访问
                var newPreview = document.getElementById(divPreviewId + "New");
                if (newPreview == null) {
                    newPreview = document.createElement("div");
                    newPreview.setAttribute("id", divPreviewId + "New");
                    newPreview.style.width = document.getElementById(imgPreviewId).width + "px";
                    newPreview.style.height = document.getElementById(imgPreviewId).height + "px";
                    newPreview.style.border = "solid 1px #d2e2e2";
                }
                newPreview.style.filter = "progid:DXImageTransform.Microsoft.AlphaImageLoader(sizingMethod='scale',src='" + document.selection.createRange().text + "')";
                var tempDivPreview = document.getElementById(divPreviewId);
                tempDivPreview.parentNode.insertBefore(newPreview, tempDivPreview);
                tempDivPreview.style.display = "none";
            }
        } else if (browserVersion.indexOf("FIREFOX") > -1) {//firefox
            var firefoxVersion = parseFloat(browserVersion.toLowerCase().match(/firefox\/([\d.]+)/)[1]);
            if (firefoxVersion < 7) {//firefox7以下版本
                document.getElementById(imgPreviewId).setAttribute("src", fileObj.files[0].getAsDataURL());
            } else {//firefox7.0+ 
                document.getElementById(imgPreviewId).setAttribute("src", window.URL.createObjectURL(fileObj.files[0]));
            }
        } else {
            document.getElementById(imgPreviewId).setAttribute("src", fileObj.value);
        }
    } else {
        alert("仅支持" + allowExtention + "为后缀名的文件!");
        fileObj.value = ""; //清空选中文件
        if (browserVersion.indexOf("MSIE") > -1) {
            fileObj.select();
            document.selection.clear();
        }
        fileObj.outerHTML = fileObj.outerHTML;
    }
    return fileObj.value; //返回路径
}
function winMax(win) {
    win.resizeTo(window.screen.availWidth, window.screen.availHeight);
    win.moveTo(0, 0);
}
function OpenSameWindow(Page, scrl) {
    if (false) {
        window.close();
    }
    if (!window.win || win.closed) {
        win = window.open(Page, "",
                "top=0,left=0,toolbar=no,location=no,directories=no,fullscreen=no,status=no,menubar=no,scrollbars=yes,resizable=yes");
    }
    else {
        win.close();
        win = window.open(Page, "",
                "top=0,left=0,toolbar=no,location=no,directories=no,fullscreen=no,status=no,menubar=no,scrollbars=yes,resizable=yes");
    }
}
function OpenSameWindowFull(Page, scrl) {
    if (false) {
        window.close();
    }   
    if (!window.win || win.closed) {
       
        win = window.open(Page, "",
                "top=0,left=0,toolbar=no,location=no,directories=no,fullscreen=yes,status=no,menubar=no,scrollbars=no,resizable=yes");
        winMax(win);
    }
    else {
        win.close();      
        win = window.open(Page, "",
                "top=0,left=0,toolbar=no,location=no,directories=no,fullscreen=yes,status=no,menubar=no,scrollbars=no,resizable=yes");
        winMax(win);  

    }
}
function SetCwinHeight2() {

    var IfrmView = document.getElementById("IfrmView"); //iframe id
    document.documentElement.scrollTop = 0;
    if (document.getElementById) {
        IfrmView.style.width = document.documentElement.scrollWidth -20 + "px";
        IfrmView.style.height = document.documentElement.scrollHeight -20 + "px";

    }
}