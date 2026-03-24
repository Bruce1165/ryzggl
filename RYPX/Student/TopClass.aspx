<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TopClass.aspx.cs" Inherits="Student_TopClass" %>

<%@ Register Src="../IframeView.ascx" TagName="IframeView" TagPrefix="uc2" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Content/swiper.min.css" rel="stylesheet" type="text/css" />
    <link href="../Content/TopClass.css?v=1.001" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="../Scripts/jquery-3.4.1.min.js"></script>
    <script type="text/javascript" src="../Scripts/swiper.min.js"></script>  
    <link href="../layer/skin/layer.css" rel="stylesheet" />
    <script src="../layer/layer.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <style type="text/css">
        .floadAd {
    position: absolute;
    z-index: 999900;
    display: none;
    background-color: #80CFFD;
    padding: 25px 10px 28px 30px;
    border-radius: 6px 6px;
    font-size: 20px;
    width: 600px;
    font-weight:bold;
    color:blue;
    opacity: 0.8; 
    
}
.floadAd .item {
    display: block;
    padding-right: 40px;
}
.floadAd .item img {
    vertical-align: bottom;
}
.floadWJ{
     position: fixed;
    z-index: 999900;
    /*display: none;*/
    background-color:#E6E6FA;
    padding: 12px 12px 12px 12px;
    border-radius: 6px 6px;
    font-size: 22px;
    width: 800px;
    font-weight:bold;
    color:blue;
    opacity: 0.9; 
    right:4px;
    top:4px;
    text-align:center;
    animation: blink 2s infinite;
}

  @keyframes blink {         
     70% { margin-right:40px; } 
  } 

    </style>  
</head>
<body>
    <section class="pc-banner">
        <div class="swiper-container">
            <div class="swiper-wrapper">
                <div class="swiper-slide swiper-slide-center none-effect">
                    <a href="../Student/WebClass.aspx?t=工匠讲堂">
                        <img src="../Img/top/04.jpg" /><div>
                            <p class="bar-head">工匠讲堂</p>
                            <p class="bar-js">《工匠讲堂》是为建筑工人量身定制的线上职业培训教学资源。视频全部拍摄场景在施工现场录制完成，严格遵循相关职业工种技术规范和标准，通过经验丰富和技能娴熟的技术人员讲解操作，充分展示相关技术工种工艺要点及操作技能规范要求。</p>
                        </div>
                    </a>
                </div>
                <div class="swiper-slide">
                    <a href="../Student/WebClass.aspx?t=首都建设云课堂">
                        <img src="../Img/top/03.jpg" />
                        <div>
                            <p class="bar-head">首都建设云课堂</p>
                            <p class="bar-js">《首都建设云课堂》是基于信息化技术，服务从业人员职业培训资源共享的在线开放课程，利用网络模式环境推动从业人员终身教育职业学习新方式。课程制作充分发挥各企业集团专家优势，同时邀请重点领域知名专家、学者参与讲授，突出内容的针对性、实用性和前瞻性。</p>
                        </div>
                    </a>
                </div>
                <div class="swiper-slide">
                    <a href="../Student/WebClass.aspx?t=行业推荐精品课程">
                        <img src="../Img/top/05.jpg" />
                        <div>
                            <p class="bar-head">行业推荐精品课程</p>
                            <p class="bar-js">
                                《行业推荐精品课程》是由行业推荐，具有前沿性与代表性的优秀课程。　　　　　　　　　　　　　　　　
                            </p>
                        </div>
                    </a>
                </div>
            </div>
        </div>
        <div class="swiper-pagination"></div>
        <div class="button">
            <div class="swiper-button-prev"></div>
            <div class="swiper-button-next"></div>
        </div>
    </section>
    <div id="divKe" runat="server" style="width: 99%; text-align: center"></div>
    <div class="bq">主办单位：北京市住房和城乡建设委员会</div>
        <%--<div id="floadAD" class="floadAd">
            <a class="close" href="javascript:void();" style="color: #000000; text-align: right; float: right; clear: both; padding-bottom: 4px; font-size: 28px">×</a>
            <div class="item" style="line-height: 150%">
                <b>系统通知：</b>
                <p style="text-indent: 40px; line-height: 150%">
                    由于工地施工，运营商光缆受损导致视频播播放中断，运营商正在抢修，请稍后再试，给你带来的不便敬请谅解！                    
                </p>
            </div>
        </div>
     <script src="../Scripts/FloatMessage.js" type="text/javascript"></script>--%>
    <div id="floadWJ" runat="server" class="floadWJ blink" visible="false" >
            <a class="close" href="javascript:void();" onclick="$(this).parent().hide();" style="color: #000000; text-align: right; float: right; clear: both; padding-bottom: 4px; font-size: 28px">×</a>
            <div class="item1" style="line-height: 180%">
                 <p style="cursor:pointer" onclick="$(this).parent().parent().hide();openQuest();">诚邀您参加【北京市建设行业从业人员公益培训平台】年度问卷调查</p>
            </div>
    </div>
     <div id="winpop">
        </div>
    <script>
        window.onload = function () {
            //FloatAd("#floadAD");

            var swiper = new Swiper('.swiper-container', {
                autoplay: 3000,
                speed: 1000,
                autoplayDisableOnInteraction: false,
                loop: true,
                centeredSlides: true,
                slidesPerView: 2,
                pagination: '.swiper-pagination',
                paginationClickable: true,
                prevButton: '.swiper-button-prev',
                nextButton: '.swiper-button-next',
                onInit: function (swiper) {
                    swiper.slides[2].className = "swiper-slide swiper-slide-active";//第一次打开不要动画
                },
                breakpoints: {
                    368: {
                        slidesPerView: 1,
                    }
                }
            });
        }

        function openQuest() {
            layer.open({
                type: 2,
                title: ['问卷调查', 'font-weight:bold;background: #5DA2EF;'],//标题
                maxmin: true, //开启最大化最小化按钮,
                offset: $(parent.document).scrollTop() + 20 + 'px',
                area: ['1000px', '700px'],
                shadeClose: false, //点击遮罩关闭
                content: './QuestionGet.aspx',
                cancel: function (index, layero) {
                    layer.close(index);
                    return false;
                }
            });
            var MsgPop = document.getElementById("winpop");//获取窗口这个对象,即ID为winpop的对象
            MsgPop.style.display = "block";
            MsgPop.style.height = "400px";//高度增加4个象素
        }
    </script>
     <uc2:IframeView ID="IframeView" runat="server" />
</body>
</html>
