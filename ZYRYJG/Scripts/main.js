function tips_pop() {
    var MsgPop = document.getElementById("winpop");//获取窗口这个对象,即ID为winpop的对象
    var popH = parseInt(MsgPop.style.height);//用parseInt将对象的高度转化为数字,以方便下面比较
    if (popH == 0) { //如果窗口的高度是0
        MsgPop.style.display = "block";//那么将隐藏的窗口显示出来
        show = setInterval("changeH('up')", 2);//开始以每0.002秒调用函数changeH("up"),即每0.002秒向上移动一次
    }
    else { //否则
        hide = setInterval("changeH('down')", 2);//开始以每0.002秒调用函数changeH("down"),即每0.002秒向下移动一次
    }
}

function changeH(str) {
    var MsgPop = document.getElementById("winpop");
    var popH = parseInt(MsgPop.style.height);
    if (str == "up") { //如果这个参数是UP
        if (popH <= 100) { //如果转化为数值的高度小于等于100
            MsgPop.style.height = (popH + 4).toString() + "px";//高度增加4个象素
        }
        else {
            clearInterval(show);//否则就取消这个函数调用,意思就是如果高度超过100象度了,就不再增长了
        }
    }
    if (str == "down") {
        if (popH >= 4) { //如果这个参数是down
            MsgPop.style.height = (popH - 4).toString() + "px";//那么窗口的高度减少4个象素
        }
        else { //否则
            clearInterval(hide); //否则就取消这个函数调用,意思就是如果高度小于4个象度的时候,就不再减了
            MsgPop.style.display = "none"; //因为窗口有边框,所以还是可以看见1~2象素没缩进去,这时候就把DIV隐藏掉
        }
    }
}

$(document).ready(function () {
    //先查询有没有企业消息，有消息提示，用户点击查看0.8秒消息提示关闭，用户没点击查看3分钟之后自己关机，下次进来继续提示，如果已经查看则不再提示
    $.ajax({
        type: "post",
        url: "Ajax/MainNews.ashx",
        async: true,
        dataType: "json",
        success: function (obj) {
            if (obj != "False") {
                document.getElementById('winpop').style.height = '0px';//我不知道为什么要初始化这个高度,CSS里不是已经初始化了吗,知道的告诉我一下
                setTimeout("tips_pop()", 800); //0.8秒后调用tips_pop()这个函数弹出消息提示
                // setTimeout("tips_pop()", 3000);//3分钟后调用tips_pop()这个函数一分钟后自己自动关闭
            }
        }
    })//
    $("#con").click(function () {
        layer.open({
            type: 1,
            title: ['申请完成通知', 'font-weight:bold;background: #5DA2EF;'],//标题
            maxmin: true, //开启最大化最小化按钮,
            area: ['600px', '360px'],
            shadeClose: false, //点击遮罩关闭
            content: '\<\div style="padding:20px;"><table id="news" width="98%" class="table"><tr><th>序号</th><th>姓名</th><th>证件号码<th>注册编号</th><th>申请类型</th></tr> </table>\<\/div>'
        });//
        setTimeout("tips_pop()", 800); //点击查看消息之后0.8秒内关闭消息提示
        $.ajax({
            type: "post",
            url: "Ajax/MainNews.ashx",
            async: false,
            dataType: "json",
            success: function (result) {
                if (result != "False") {
                    $.each(result, function (i, ss) {
                        $("#news").append("<tr><td>" + (i + 1) + "</td><td>" + ss.PSN_Name + "</td><td>" + ss.PSN_CertificateNO + "</td><td>" + ss.PSN_RegisterNo + "</td><td>" + ss.ApplyType + "</td></tr>");
                    })
                }
            }
        });//消息查看完成后制为无效
        $.ajax({
            type: "post",
            url: "Ajax/NewsFalse.ashx",
            async: true,
        });//


    })

    //var ietype = browerType();
    //$("#divBrower").text(ietype);

})
