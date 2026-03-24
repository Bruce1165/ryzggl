

var ie = (navigator.appName.indexOf("Microsoft") != -1) ? true : false;

// 这一段使得FireFox的HTMLElement具有click方法（add click method to HTMLElement in Mozilla） 
try {
    // create span element so that HTMLElement is accessible 
    document.createElement('span');
    HTMLElement.prototype.click = function () {
        if (typeof this.onclick == 'function')
            this.onclick({ type: 'click' });
    };
}
catch (e) {
    // alert('click method for HTMLElement couldn\'t be added'); 
}
// 对HTMLAnchorElement 加入onclick事件 
try {
    // create a element so that HTMLAnchorElement is accessible 
    document.createElement('a');
    HTMLElement.prototype.click = function () {
        if (typeof this.onclick == 'function') {
            if (this.onclick({ type: 'click' }) && this.href)
                window.open(this.href, this.target ? this.target : '_self');
        }
        else if (this.href)
            window.open(this.href, this.target ? this.target : '_self');
    };
}
catch (e) {
    // alert('click method for HTMLAnchorElement couldn\'t be added'); 
}

function _$(id) {
    return document.getElementById(id);
}

//* text 模拟 link
function textMouseover(obj) {
    obj.textDecoration = "underline";
}
function textMouseout(obj) {
    obj.style.textDecoration = "none";
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
    var IfrmView = document.getElementById("IfrmView"); //iframe id
    document.documentElement.scrollTop = 0;
    if (document.getElementById) {
        if (IfrmView && !window.opera) {
            if (IfrmView.contentDocument && IfrmView.contentDocument.body.offsetHeight) {
                IfrmView.height = IfrmView.contentDocument.body.offsetHeight + 20;

            } else if (IfrmView.Document && IfrmView.Document.body.scrollHeight) {
                IfrmView.height = IfrmView.Document.body.scrollHeight + 20;

            }
        }
        if (IfrmView != null && IfrmView.src != "about:blank") {
            var divFram = _$("divFram");
            divFram.style.top = "0px";
            divFram.style.left = "8px";
            divFram.style.zIndex = 99999;

            var top;
            if (IfrmView.contentDocument)
                top = IfrmView.contentDocument.getElementById("functionMenu");
            else
                top = IfrmView.Document.getElementById("functionMenu");
            if (top == null) {
                if (IfrmView.contentDocument)
                    top = IfrmView.contentDocument.getElementById("div_top");
                else
                    top = IfrmView.Document.getElementById("div_top");
                if (top != null) top.innerHTML += "<div id='functionMenu' class='right_t_menu' style='float: right; margin-right: 2%'>&nbsp;&nbsp;<a href='#' onclick='javascript:hideIfam();'><img src='../Img/close.png' alt='关闭窗口' style='border:0px;' /></a></div>";
            }
            else if (top.innerHTML.indexOf("关闭窗口") == -1)
                top.innerHTML += "&nbsp;&nbsp;<a href='#' onclick='javascript:hideIfam();'><img src='../Img/close.png' alt='关闭窗口' style='border:0px;' /></a>";
            //if (top != null) top.innerHTML += "<span onclick='hideIfam();' style=\"float: right; margin:0px 10px; background: transparent url(Img/gif/closewin.gif) no-repeat 0px 0px; width:18px; height:18px; vertical-align:middle; cursor:pointer\"><span>";
        }
    }
}

//ifram模拟弹出模式窗口
function SetIfrmSrc(url) {
    SetIfrmSrcMode(url, true);

    var divFram = _$("divFram");
    if (divFram != null) divFram.style.display = "block";
}

//ifram模拟弹出窗口,IsMode：true表示模式窗口、false表示非模式窗口
function SetIfrmSrcMode(url, IsMode) {
    var IfrmView = document.getElementById("IfrmView"); //iframe id
    if (IfrmView != null) {
        IfrmView.src = urlRandom(url);;
        IfrmView.style.borderLeft = "#D66340 4px solid";
        IfrmView.style.borderTop = "#D66340 30px solid";
        IfrmView.style.borderBottom = "#B9510D 6px solid";
        IfrmView.style.borderRight = "#B9510D 6px solid";
        IfrmView.style.display = "block";

        //遮挡层        
        _$("HiddenOpenMode").value = (IsMode == true) ? "true" : "false";
        if (_$("HiddenOpenMode").value == "true") {
            var iframeGray = _$("iframeGray");
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
    if (url.indexOf("?") != -1)
        return url + "&" + Math.random();
    else
        return url + "?" + Math.random();
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

        var IfrmView = document.getElementById("IfrmView");
        if (IfrmView != null) IfrmView.src = "about:blank";

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
//窗体全屏
function windowMax() {
    window.resizeTo(window.screen.availWidth, window.screen.availHeight);
    window.moveTo(0, 0);
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
        winMax(win);
    }
    else {
        win.close();
        win = window.open(Page, "",
                "top=0,left=0,toolbar=no,location=no,directories=no,fullscreen=no,status=no,menubar=no,scrollbars=yes,resizable=yes");
        winMax(win);
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
        IfrmView.style.width = document.documentElement.scrollWidth - 20 + "px";
        IfrmView.style.height = document.documentElement.scrollHeight - 20 + "px";

    }
}
function MM_goToURL() { //v3.0
    var i, args = MM_goToURL.arguments; document.MM_returnValue = false;
    for (i = 0; i < (args.length - 1) ; i += 2) eval(args[i] + ".location='" + args[i + 1] + "'");
}
function closeWindow() {
    var browserName = navigator.appName;
    if (browserName == "Netscape") {
        window.open('', '_parent', ''); window.close();
    } else if (browserName == "Microsoft Internet Explorer") {
        window.opener = "whocares"; window.close();
    }
}

function onfocus(obj) {
    if (obj.value == obj.defaultValue)
    { obj.value = ''; }
}

function onblur(obj) {
    if (obj.value == '')
    { obj.value = this.defaultValue; }
}
