function browerType() {
    var browerName = '其他浏览器';
    var ua = navigator.userAgent.toLocaleLowerCase();
    // 判断是否为IE(第一个是正常的IE，第二个是Edge，第三个是IE11)
    var isIE = (ua.indexOf("compatible") > -1 && ua.indexOf("msie") > -1) || (ua.indexOf("edge") > -1) || (ua.indexOf('trident') > -1 && ua.indexOf("rv:11.0") > -1);
    // 判断是否为IE5678，!+[1,] 在IE5678返回true，在IE9、IE10、IE11返回false
    var isLteIE8 = isIE && !+[1, ];
    // 用于防止因通过IE8+的文档兼容性模式设置文档模式，导致版本判断失效
    var dm = document.documentMode,
                isIE5,
                isIE6,
                isIE7,
                isIE8,
                isIE9,
                isIE10,
                isIE11;
    if (dm) {
        isIE5 = dm === 5;
        isIE6 = dm === 6;
        isIE7 = dm === 7;
        isIE8 = dm === 8;
        isIE9 = dm === 9;
        isIE10 = dm === 10;
        isIE11 = dm === 11;
    } else {
        // 判断是否为IE5，IE5的文本模式为怪异模式(quirks),真实的IE5.5浏览器中没有document.compatMode属性  
        isIE5 = (isLteIE8 && (!document.compatMode || document.compatMode === 'BackCompat'));

        // 判断是否为IE6，IE7开始有XMLHttpRequest对象  
        isIE6 = isLteIE8 && !isIE5 && !XMLHttpRequest;

        // 判断是否为IE7，IE8开始有document.documentMode属性  
        isIE7 = isLteIE8 && !isIE6 && !document.documentMode;

        // 判断是否IE8  
        isIE8 = isLteIE8 && document.documentMode;

        // 判断IE9，IE9严格模式中函数内部this不为undefined  
        isIE9 = !isLteIE8 && (function () {
            "use strict";
            return !!this;
        }());

        // 判断IE10，IE10开始支持严格模式，严格模式中函数内部this为undefined   
        isIE10 = isIE && !!document.attachEvent && (function () {
            "use strict";
            return !this;
        }());

        // 判断IE11，IE11开始移除了attachEvent属性  
        isIE11 = isIE && !document.attachEvent;
    };
    // IE浏览器的判断
    browerName = isIE5 ? 'IE5' :
                 isIE6 ? 'IE6' :
                 isIE7 ? 'IE7' :
                 isIE8 ? 'IE8' :
                 isIE9 ? 'IE9' :
                 isIE10 ? 'IE10' :
                 isIE11 ? 'IE11' :
                 // 国内套壳浏览器判断
                 (ua.indexOf('qqbrowser') > -1) ? 'QQ浏览器' :
                 // 先判断QQ浏览器再判断搜狗浏览器，因为QQ浏览器也有'se'
                 (ua.indexOf('se') > -1) ? '搜狗浏览器' :
                 (ua.indexOf('aoyou') > -1) ? '遨游浏览器' :
                 (ua.indexOf('theworld') > -1) ? '世界之窗浏览器' :
                 (ua.indexOf('worldchrome') > -1) ? '世界之窗极速浏览器' :
                 (ua.indexOf('greenbrowser') > -1) ? '绿色浏览器' :
                 (ua.indexOf('baidu') > -1) ? '百度浏览器' :
                 // 360浏览器判断的特殊方法，打开新页原来的'360'字符串代理已经不显示，方法失效，同时也要晚于搜狗浏览器判断，不然搜狗会被认为360浏览器
                 (window.navigator.mimeTypes[40] || !window.navigator.mimeTypes.length) ? '360浏览器' : '其他浏览器';
    return browerName;
}