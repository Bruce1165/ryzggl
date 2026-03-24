var timer;
var timer1;
$(document).ready(

   function () {

       setURL('Main.aspx');

       setPageSize();
       $(window).resize(function () {
           setPageSize();
       });

       //$('#menu_sub div').hover(function () { clearTimeout(timer1); },
       $('#menu_sub').hover(function () { clearTimeout(timer); },
       function () {
           clearTimeout(timer);
          
           $("#" + curPan.attr("id") + " .active:first").parent().children("ul:first").css("display", "none");
           $("#" + curPan.attr("id") + " .active").addClass("inactive");
           $("#" + curPan.attr("id") + " .active").removeClass("active");
           $("#" + curPan.attr("id") + " div:first").addClass("menuoff");
           $("#" + curPan.attr("id")).css("display", "none");
       }
       );

       $('#left_side div').mouseenter(function () {
           $("#" + this.id).css("backgroundColor", "#ddd");
           var myid = this.id;
           clearTimeout(timer);
           timer = window.setTimeout(function () {

               if (curPan) {
                   $("#" + curPan.attr("id") + " .active:first").parent().children("ul:first").css("display", "none");
                   $("#" + curPan.attr("id") + " .active").addClass("inactive");
                   $("#" + curPan.attr("id") + " .active").removeClass("active");
                   $("#" + curPan.attr("id") + " div:first").addClass("menuoff");
               
                   $("#RadPanelBar"  +curMenu.attr("id")).css("display", "none");
                   
               }
               $("#RadPanelBar" + myid).css("display", "block");

               $("#RadPanelBar" + myid + " div:first").removeClass("menuoff");
               curPan = $("#RadPanelBar" + myid);
               curMenu = $("#" + myid);

               $("#RadPanelBar" + myid + " .menu ul li a:first").removeAttr("class");
               $("#RadPanelBar" + myid + " .menu ul li a:first").addClass("active");
               $("#RadPanelBar" + myid + " .menu ul li ul:first").css("display", "block");
          
           }, 500);
       });
       $('#left_side div').mouseleave(
          function () {
              $("#" + this.id).css("backgroundColor", "#F6F6F4");

              //timer1 = window.setTimeout(function () {
              //    $("#RadPanelBar" + this.id).css("display", "none");
                 
              //},500);
              //clearTimeout(timer);
            
          }
       );

       //menu_sub
       $('#menu_sub .div_menu ul li ul li a').mouseenter(function () {
           $(this).css("color", "red");          
       });
       $('#menu_sub .div_menu ul li ul li a').mouseleave(
        function () {
            $(this).css("color", "#1874CD");
        }
     );
   }
);

function setPageSize() {
    var wh = $(window).height() - 70;
    $(".middle").height(wh);
    $("#right_side").height(wh);
    $("#mainFrame").height(wh);
    $("#left_side").height(wh);
    $(".split").height(wh);

    //获取左边宽度
    var leftwidth = $("#left_side").width();
    //alert(leftwidth);
    //可见宽度
    var xy = $(window).width();
    //右边宽度
    var rightwidth = 0;
    //if (leftwidth == 0) {
    //    rightwidth = xy - $("#left_side").outerWidth() - 18;

    //}
    //else {
    //    rightwidth = xy - $("#left_side").outerWidth() - 18;

    //}

    rightwidth = xy - $("#left_side").outerWidth() - 18;
    $("#right_side").width(rightwidth + "px");

}

// 设置PanelBar的链接
function setURL(url) {
    $('.cd-nav-container, .cd-overlay').toggleClass('is-visible', false);
    $("#divMenu").width(0);
    $("#divalpha").width(0);
    if (url.indexOf("?") != -1)
        $("#mainFrame").attr('src', url + "&" + Math.random());
    else
        $("#mainFrame").attr('src', url + "?invoke=" + Math.random());


}

function SetCwinHeight() {
    var ifrmView = document.getElementById("mainFrame"); //iframe id
    document.documentElement.scrollTop = 0;
    if (document.getElementById) {
        if (ifrmView && !window.opera) {
            if (ifrmView.contentDocument && ifrmView.contentDocument.body.offsetHeight) {
                ifrmView.height = ifrmView.contentDocument.body.offsetHeight;

            } else if (ifrmView.Document && ifrmView.Document.body.scrollHeight) {
                ifrmView.height = ifrmView.Document.body.scrollHeight;

            }
        }
    }
}
function hideshow() {

    if ($(".left").width() == 0) {
        $(".left").outerWidth("77px");
        $(".split").css("left", "83px");
        $(".right").css("left", "89px");
        $(".split").addClass("split_l");
        setPageSize();
    }
    else {
        $(".left").width("0px");
        $(".split").css("left", "6px");
        $(".right").css("left", "12px");
        $(".split").addClass("split_r");
        setPageSize();
    }
}
var x = 0;
function menu_show(e, code) {
    x = e.clientX;

    if (code == 0) {
        if ($(".menu_sub").width() > 0) {
            $(".menu_sub").width("0px");

            alert(x);
        }
    }
    else if (code == 1) {
        if ($(".menu_sub").width() == 0) {
            $(".menu_sub").width("77px");
        }
    }
}
var curPan
//var curBar;
var curMenu;
function bar_show(send, e) {

    if (curPan) {
        $("#" + curPan.attr("id") + " .active:first").parent().children("ul:first").css("display", "none");
        $("#" + curPan.attr("id") + " .active").addClass("inactive");
        $("#" + curPan.attr("id") + " .active").removeClass("active");
        $("#" + curPan.attr("id") + " div:first").addClass("menuoff");
    }

    $("#RadPanelBar" + send.id + " div:first").removeClass("menuoff");
    curPan = $("#RadPanelBar" + send.id);

    $("#RadPanelBar" + send.id + " .menu ul li a:first").removeAttr("class");
    $("#RadPanelBar" + send.id + " .menu ul li a:first").addClass("active");
    $("#RadPanelBar" + send.id + " .menu ul li ul:first").css("display", "block");

}

function hidemenu() {
    if (curPan) {
        $("#" + curPan.attr("id") + " .active:first").parent().children("ul:first").css("display", "none");
        $("#" + curPan.attr("id") + " .active").addClass("inactive");
        $("#" + curPan.attr("id") + " .active").removeClass("active");
        $("#" + curPan.attr("id") + " div:first").addClass("menuoff");
    }
}
