using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Model
{
    public class SendDate
    {
       // 用户名：1E6843C609C1CECCA3E00831C45CA7C4 
        //密码：18FFD652AC042EC878C9E0E7F408ED85
        //证照类型代码：11100000000013338W007
        public string appKey { get; set; }
        public string appSecret { get; set; }
        public string certificateTypeCode { get; set; }
        public string data { get; set; }
        public object identifierCodeList { get; set; }
        public string certificateIdentifierCode { get; set; }
        public string logoutReason { get; set; }
        public string timeStamp { get; set; }
        public string sign { get; set; }
        
    }
}
