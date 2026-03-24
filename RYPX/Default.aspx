<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>北京市建设行业从业人员公益培训平台</title>
    <link href="./Content/swiper.min.css" rel="stylesheet" type="text/css" />
    <link href="./Content/Default.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="./Scripts/jquery-3.4.1.min.js"></script>
    <script type="text/javascript" src="./Scripts/swiper.min.js"></script>   
    <script type="text/javascript" src="./Scripts/jquery-3.4.1.min.js"></script>
    <script type="text/javascript" src="./Scripts/Public.js?v=1.011"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="top">
            <div class="title">北京市建设行业从业人员公益培训平台</div>
            <div class="fun">
                <span style="font-family: 'Microsoft YaHei UI', 'Microsoft YaHei',STHupo,Arial; font-size: 20px; font-weight: bold;">
                    <img src="Img/mf.png" alt="" style="vertical-align: middle; padding-right: 6px; height: 46px" />公益培训&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span>
                <a class="cur"  href="#">首 页</a>
                <a class="not"  href="./Student/WebClass.aspx?t=工匠讲堂">工匠讲堂</a>
                <a class="not"  href="./Student/WebClass.aspx?t=首都建设云课堂">首都建设云课堂</a>
                <a class="not"  href="./Student/WebClass.aspx?t=行业推荐精品课程">行业推荐精品课程</a>
            </div>
        </div>
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
        <script>
            window.onload = function () {
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
        </script>
    </form>
</body>
</html>
