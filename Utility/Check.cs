using System;
using System.Data;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Utility
{
    public class Check
    {

        /// <summary>
        /// 获取证书表示校验位（1位），校验位按照GB/T 17710—2008定义的"ISO/IEC 7064 MOD37 ,36" 规则计算
        /// </summary>
        /// <param name="ZZBS">证书标识，不带根码，不带分隔符</param>
        /// <returns>校验码</returns>
        public static string GetZZBS_CheckCode(string ZZBS)
        {
            List<string> Char36 = new List<string> { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            char[] list = ZZBS.ToCharArray();
            int index = 0;
            int p = 36;
            foreach (char c in list)
            {
                index = Char36.IndexOf(c.ToString());
                p = p + index;
                p = (p % 36);
                if (p == 0) p = 36;
                p = p * 2;
                p = p % 37;
            }
            p = 37 - p;
            if (p == 36) p = 0;
            return Char36[p];
        }

        #region Check 方法

        /// <summary>
        /// 检查企业是否为分公司（分部，分公司，分厂，分店，分院，分社，分中心）
        /// </summary>
        /// <param name="UnitName"></param>
        /// <returns>是分公司返回true，否则返回false</returns>
        public static bool CheckIfSubUnit(string UnitName)
        {
            //分校,分局,分处,分行,分会,分所,
            if(UnitName.Contains("分部")
                ||UnitName.Contains("分公司")
                ||UnitName.Contains("分厂")
                ||UnitName.Contains("分店")
                ||UnitName.Contains("分院")
                ||UnitName.Contains("分社")
                ||UnitName.Contains("分中心")
                )
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 清除Html不必要的标签和样式
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string CleanHtml(string html)
        {
            html = Regex.Replace(html, @"((.*\r\n)*)(<body.*>)((.*\r\n)*)</body>((.*\r\n)*)</html>", "$4", RegexOptions.IgnoreCase);

            // start by completely removing all unwanted tags    
            html = Regex.Replace(html, @"<[/]?(div|font|span|xml|del|ins|b|[ovwxp]:\w+)[^>]*?>", "", RegexOptions.IgnoreCase);
            // then run another pass over the html (twice), removing unwanted attributes  
            //html = Regex.Replace(html, @"<([^>]*)(?:class|lang|style|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>","<$1$2>", RegexOptions.IgnoreCase);

            html = Regex.Replace(html, @"<p\s[^>]*>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"</p>", "", RegexOptions.IgnoreCase);
            html = Regex.Replace(html, @"\r\n\r\n", "\r\n", RegexOptions.IgnoreCase);

            //html = Regex.Replace(html, @"(<IMG[^>]*src="")([^>]*>)", "$1../UpLoad/ExamPage/$2", RegexOptions.IgnoreCase);


            return html;
        }

        /// <summary>
        /// 超龄检查：
        /// 二级建造师，年满65周岁前90天,不再允许其发起二级建造师初始、重新、延续、增项、执业企业变更注册申请
        /// 二级造价工程师，年满70周岁前90天,不再允许其发起二级建造师初始、重新、延续、执业企业变更注册申请
        /// 安管人员，法人A不限；
        /// 安管人员（非法人A本、B本、C本）男60周岁、女55周岁；
        /// 安管人员，B本65周岁作为超龄注销年龄上限；
        /// 建筑施工特种作业,男60周岁女50周岁；
        /// 其他60周岁
        /// 
        ///《国务院关于渐进式延迟法定退休年龄的办法》自2025年1月1日起对现有安管人员、特种作业人员证书有效期进行相应调整:
        /// 1、60周岁男职工（安管人员、特种作业人员）延迟法定退休年龄按每四个月延长1月进行调整，最长由60周岁退休改为63周岁退休
        /// 2、55周岁女职工（安管人员）延迟法定退休年龄按每四个月延长1月进行调整，最长由55周岁退休改为58周岁退休
        /// 3、50周岁女职工（特种作业人员）延迟法定退休年龄按每两个月延长1月进行调整，最长由50周岁退休改为55周岁退休
        /// </summary>
        /// <param name="PostTypeID">证书岗位类别ID（特殊：三类人传递岗位工种ID:PostID，二级建造师：传递0，二级造价工程师：-1）</param>
        /// <param name="cardId">身份证号</param>
        /// <returns>超龄返回true，否则返回false</returns>
        public static bool CheckBirthdayLimit(int PostTypeID, string cardId, DateTime Birthday, string sex)
        {
            //DateTime myBirthday = getBirthday(cardId);//出生日期
            //string sex = "";//性别
            //sex = getSex(cardId);
            //if (sex == "") return false;//无法判断男女


            DateTime myBirthday = (isChinaIDCard(cardId) == true ? getBirthday(cardId) : Birthday);//出生日期
            string mySex = (isChinaIDCard(cardId) == true ? getSex(cardId) : sex);//性别
            if (mySex == "") return false;//无法判断男女

            int Spanmonth = 0;//按老规则退休超期月份数量。
            int Addmonth = 0;//延长退休月份数量。


            switch (PostTypeID)
            {
                case 2://建筑施工特种作业,男60周岁女50周岁
                    if (mySex == "男")
                    {
                        #region 男

                        if (myBirthday.AddYears(60) < Convert.ToDateTime("2025-01-01"))//老规则
                        {
                            if (myBirthday.AddYears(60).AddDays(1) < DateTime.Now)
                            {
                                return true;
                            }
                        }
                        else if (myBirthday.AddYears(60) >= Convert.ToDateTime("2025-01-01") && myBirthday.AddYears(60) < Convert.ToDateTime("2040-01-01"))//过渡期
                        {
                            Spanmonth = (myBirthday.AddYears(60).Year - 2025) * 12 + myBirthday.AddYears(60).Month;
                            Addmonth = (Spanmonth - 1) / 4 + 1;
                            if (myBirthday.AddYears(60).AddMonths(Addmonth).AddDays(1) < DateTime.Now )
                            {
                                return true;
                            }
                        }
                        else 
                        {
                            if (myBirthday.AddYears(63).AddDays(1) < DateTime.Now)//新规则
                            {
                                return true;
                            }
                        }

                        #endregion 男
                    }
                    else//女
                    {
                        #region 女

                        if (myBirthday.AddYears(50) < Convert.ToDateTime("2025-01-01"))//老规则
                        {
                            if (myBirthday.AddYears(50).AddDays(1) < DateTime.Now)
                            {
                                return true;
                            }
                        }
                        else if (myBirthday.AddYears(50) >= Convert.ToDateTime("2025-01-01") && myBirthday.AddYears(50) < Convert.ToDateTime("2040-01-01"))//过渡期
                        {
                            Spanmonth = (myBirthday.AddYears(50).Year - 2025) * 12 + myBirthday.AddYears(50).Month;
                            Addmonth = (Spanmonth - 1) / 2 + 1;
                            if (myBirthday.AddYears(50).AddMonths(Addmonth).AddDays(1) < DateTime.Now)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (myBirthday.AddYears(55).AddDays(1) < DateTime.Now)//新规则
                            {
                                return true;
                            }
                        }

                        #endregion 女
                    }
                    break;
                case 3://造价员，60周岁
                case 4://建设职业技能岗位，60周岁
                case 4000://新版建设职业技能岗位，60周岁
                case 5://关键岗位专业技术管理人员，60周岁
                    #region posttypeid=3 or 4 or 5

                    if (myBirthday.AddYears(60) < Convert.ToDateTime("2025-01-01"))//老规则
                    {
                        if (myBirthday.AddYears(60).AddDays(1) < DateTime.Now)
                        {
                            return true;
                        }
                    }
                    else if (myBirthday.AddYears(60) >= Convert.ToDateTime("2025-01-01") && myBirthday.AddYears(60) < Convert.ToDateTime("2040-01-01"))//过渡期
                    {
                        Spanmonth = (myBirthday.AddYears(60).Year - 2025) * 12 + myBirthday.AddYears(60).Month;
                        Addmonth = (Spanmonth - 1) / 4 + 1;
                        if (myBirthday.AddYears(60).AddMonths(Addmonth).AddDays(1) < DateTime.Now)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (myBirthday.AddYears(63).AddDays(1) < DateTime.Now)//新规则
                        {
                            return true;
                        }
                    }
                    break;

                #endregion posttypeid=3 or 4 or 5
                case 147://企业主要负责人，需要判断是否为法人，非法人（男60，女55）；法人不限，不能使用该公式判断；
                case 148://项目负责人，（男60，女55）不能办理业务，65周岁作为超龄注销年龄上限
                case 6://土建类专职安全生产管理人员
                case 1123://机械类专职安全生产管理人员
                case 1125://综合类专职安全生产管理人员   
                    #region posttypeid=1

                    if (mySex == "男")//60
                    {
                        if (myBirthday.AddYears(60) < Convert.ToDateTime("2025-01-01"))//老规则
                        {
                            if (myBirthday.AddYears(60).AddDays(1) < DateTime.Now )
                            {
                                return true;
                            }
                        }
                        else if (myBirthday.AddYears(60) >= Convert.ToDateTime("2025-01-01") && myBirthday.AddYears(60) < Convert.ToDateTime("2040-01-01"))//过渡期
                        {
                            Spanmonth = (myBirthday.AddYears(60).Year - 2025) * 12 + myBirthday.AddYears(60).Month;
                            Addmonth = (Spanmonth - 1) / 4 + 1;
                            if (myBirthday.AddYears(60).AddMonths(Addmonth).AddDays(1) < DateTime.Now)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (myBirthday.AddYears(63).AddDays(1) < DateTime.Now)//新规则
                            {
                                return true;
                            }
                        }
                    }
                    else//女55
                    {
                        if (myBirthday.AddYears(55) < Convert.ToDateTime("2025-01-01"))//老规则
                        {
                            if (myBirthday.AddYears(55).AddDays(1) < DateTime.Now)
                            {
                                return true;
                            }
                        }
                        else if (myBirthday.AddYears(55) >= Convert.ToDateTime("2025-01-01") && myBirthday.AddYears(55) < Convert.ToDateTime("2040-01-01"))//过渡期
                        {
                            Spanmonth = (myBirthday.AddYears(55).Year - 2025) * 12 + myBirthday.AddYears(55).Month;
                            Addmonth = (Spanmonth - 1) / 4 + 1;
                            if (myBirthday.AddYears(55).AddMonths(Addmonth).AddDays(1) < DateTime.Now )
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (myBirthday.AddYears(58).AddDays(1) < DateTime.Now)//新规则
                            {
                                return true;
                            }
                        }
                    }

                     #endregion posttypeid=1
                    break;
                case 0://二级建造师，年满65周岁前90天,不再允许其发起二级建造师初始、重新、延续、增项、执业企业变更注册申请
                    if (myBirthday.AddYears(65).AddDays(-90) <= DateTime.Now)
                    {
                        return true;
                    }
                    break;
                case -1://二级造价工程师，年满70周岁前90天,不再允许其发起二级建造师初始、重新、延续、增项、执业企业变更注册申请
                    if (myBirthday.AddYears(70).AddDays(-90) <= DateTime.Now)
                    {
                        return true;
                    }
                    break;
                default:
                    return false;

            }
            return false;
        }

        /// <summary>
        /// 三类人、安管人员证书续期申请超龄检查：     
        /// 安管人员，法人A不限；
        /// 安管人员（非法人A本、B本、C本）男60周岁、女55周岁；
        /// 建筑施工特种作业,男60周岁女50周岁；
        ///《国务院关于渐进式延迟法定退休年龄的办法》自2025年1月1日起对现有安管人员、特种作业人员证书有效期进行相应调整:
        /// 1、60周岁男职工（安管人员、特种作业人员）延迟法定退休年龄按每四个月延长1月进行调整，最长由60周岁退休改为63周岁退休
        /// 2、55周岁女职工（安管人员）延迟法定退休年龄按每四个月延长1月进行调整，最长由55周岁退休改为58周岁退休
        /// 3、50周岁女职工（特种作业人员）延迟法定退休年龄按每两个月延长1月进行调整，最长由50周岁退休改为55周岁退休
        /// </summary>
        /// <param name="PostTypeID">证书岗位类别ID</param>
        /// <param name="cardId">身份证号</param>        
        /// <param name="Birthday">出生日期</param>
        /// <param name="sex">性别</param>
        /// <param name="validEndDate">证书有效期截至日期</param>
        /// <returns>超龄返回true，否则返回false</returns>
        public static bool CheckContinueBirthdayLimit(int PostTypeID, string cardId, DateTime Birthday, string sex, DateTime validEndDate)
        {
            DateTime myBirthday = (isChinaIDCard(cardId) == true ? getBirthday(cardId) : Birthday);//出生日期
            string mySex =(isChinaIDCard(cardId) == true ?  getSex(cardId):sex);//性别
            if (mySex == "") return false;//无法判断男女

            int Spanmonth = 0;//按老规则退休超期月份数量。
            int Addmonth = 0;//延长退休月份数量。

            switch (PostTypeID)
            {
                case 2://建筑施工特种作业,男60周岁女50周岁

                    if (mySex == "男")
                    {
                        if (myBirthday.AddYears(60) < Convert.ToDateTime("2025-01-01"))//老规则
                        {
                            if (myBirthday.AddYears(60).AddDays(1) < DateTime.Now || myBirthday.AddYears(60) <= validEndDate)
                            {
                                return true;
                            }
                        }
                        else if (myBirthday.AddYears(60) >= Convert.ToDateTime("2025-01-01") && myBirthday.AddYears(60) < Convert.ToDateTime("2040-01-01"))//过渡期
                        {
                            Spanmonth = (myBirthday.AddYears(60).Year - 2025) * 12 + myBirthday.AddYears(60).Month;
                            Addmonth = (Spanmonth - 1) / 4 + 1;
                            if (myBirthday.AddYears(60).AddMonths(Addmonth).AddDays(1) < DateTime.Now || myBirthday.AddYears(60).AddMonths(Addmonth) <= validEndDate)
                            {
                                return true;
                            }
                        }
                        else 
                        {
                            if (myBirthday.AddYears(63).AddDays(1) < DateTime.Now || myBirthday.AddYears(63) <= validEndDate)//新规则
                            {
                                return true;
                            }
                        }
                    }
                    else//女
                    {
                        if (myBirthday.AddYears(50) < Convert.ToDateTime("2025-01-01"))//老规则
                        {
                            if (myBirthday.AddYears(50).AddDays(1) < DateTime.Now || myBirthday.AddYears(50) <= validEndDate)
                            {
                                return true;
                            }
                        }
                        else if (myBirthday.AddYears(50) >= Convert.ToDateTime("2025-01-01") && myBirthday.AddYears(50) < Convert.ToDateTime("2040-01-01"))//过渡期
                        {
                            Spanmonth = (myBirthday.AddYears(50).Year - 2025) * 12 + myBirthday.AddYears(50).Month;
                            Addmonth = (Spanmonth - 1) / 2 + 1;
                            if (myBirthday.AddYears(50).AddMonths(Addmonth).AddDays(1) < DateTime.Now || myBirthday.AddYears(50).AddMonths(Addmonth) <= validEndDate)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (myBirthday.AddYears(55).AddDays(1) < DateTime.Now || myBirthday.AddYears(55) <= validEndDate)//新规则
                            {
                                return true;
                            }
                        }
                    }
                    break;
              
                case 147://企业主要负责人，需要判断是否为法人，非法人（男60，女55）；法人不限，不能使用该公式判断；
                case 148://项目负责人，（男60，女55）不能办理业务，65周岁作为超龄注销年龄上限
                case 6://土建类专职安全生产管理人员
                case 1123://机械类专职安全生产管理人员
                case 1125://综合类专职安全生产管理人员
                    if (mySex == "男")//60
                    {
                        if (myBirthday.AddYears(60) < Convert.ToDateTime("2025-01-01"))//老规则
                        {
                            if (myBirthday.AddYears(60).AddDays(1)< DateTime.Now || myBirthday.AddYears(60) <= validEndDate)
                            {
                                return true;
                            }
                        }
                        else if (myBirthday.AddYears(60) >= Convert.ToDateTime("2025-01-01") && myBirthday.AddYears(60) < Convert.ToDateTime("2040-01-01"))//过渡期
                        {
                            Spanmonth = (myBirthday.AddYears(60).Year - 2025) * 12 + myBirthday.AddYears(60).Month;
                            Addmonth = (Spanmonth - 1) / 4 + 1;
                            if (myBirthday.AddYears(60).AddMonths(Addmonth).AddDays(1) < DateTime.Now || myBirthday.AddYears(60).AddMonths(Addmonth) <= validEndDate)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (myBirthday.AddYears(63).AddDays(1)< DateTime.Now || myBirthday.AddYears(63) <= validEndDate)//新规则
                            {
                                return true;
                            }
                        }
                    }
                    else//女55
                    {
                        if (myBirthday.AddYears(55) < Convert.ToDateTime("2025-01-01"))//老规则
                        {
                            if (myBirthday.AddYears(55).AddDays(1)< DateTime.Now || myBirthday.AddYears(55) <= validEndDate)
                            {
                                return true;
                            }
                        }
                        else if (myBirthday.AddYears(55) >= Convert.ToDateTime("2025-01-01") && myBirthday.AddYears(55) < Convert.ToDateTime("2040-01-01"))//过渡期
                        {
                            Spanmonth = (myBirthday.AddYears(55).Year - 2025) * 12 + myBirthday.AddYears(55).Month;
                            Addmonth = (Spanmonth - 1) / 4 + 1;
                            if (myBirthday.AddYears(55).AddMonths(Addmonth).AddDays(1) < DateTime.Now || myBirthday.AddYears(55).AddMonths(Addmonth) <= validEndDate)
                            {
                                return true;
                            }
                        }
                        else
                        {
                            if (myBirthday.AddYears(58).AddDays(1) < DateTime.Now || myBirthday.AddYears(58) <= validEndDate)//新规则
                            {
                                return true;
                            }
                        }
                    }
                    break;

                default:
                    return false;

            }
            return false;
        }

        public static string Decode(string str)
        {
            str = str.Replace("<br>", "\n");
            str = str.Replace("&gt;", ">");
            str = str.Replace("&lt;", "<");
            str = str.Replace("&nbsp;", " ");
            str = str.Replace("&quot;", "\"");
            return str;
        }

        public static string Encode(string str)
        {
            str = str.Replace("&", "&amp;");
            str = str.Replace("'", "''");
            str = str.Replace("\"", "&quot;");
            str = str.Replace(" ", "&nbsp;");
            str = str.Replace("<", "&lt;");
            str = str.Replace(">", "&gt;");
            str = str.Replace("\n", "<br>");
            return str;
        }

        /// <summary>
        /// 替换字符串中需转义的字符：',;"\
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string replaceChares(string str)
        {
            str = str.Replace("'", "’");
            str = str.Replace("\"", "”");
            str = str.Replace(",", "，");
            str = str.Replace(";", "；");
            str = str.Replace("\\", "/");
            return str;
        }

        /// <summary>
        /// 替换文本中的“中英文空格”、“回车与换行符”、“制表符”
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string removeInputErrorChares(string str)
        {
            return str.Replace(" ", "").Replace("　", "").Replace("\r", "").Replace("\n", "").Replace("\v", "");
        }

        public static string FormatSqlConditionExpression(string sql)
        {
            sql = sql.Replace("[", "[[]");
            return sql.Replace("'", "''");
        }

        public static string GetUrlParamValue(string url, string param)
        {
            string strParamValue = string.Empty;
            url = url.ToLower();
            param = param.ToLower();
            if (url.IndexOf(param) > 0)
            {
                strParamValue = url.Substring(url.IndexOf("=") + 1);
            }
            return strParamValue;
        }

        public static bool IfChinaChar(string InputText)
        {
            string strRegex = @"[\u4e00-\u9fa5]{0,}$";
            Regex re = new Regex(strRegex);
            return re.IsMatch(InputText);
        }

        //
        public static bool IfDateOrDateTimeFormat(string DateText)
        {
            string strRegex = @"(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(10|12|0?[13578])([-\/\._])(3[01]|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(11|0?[469])([-\/\._])(30|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(0?2)([-\/\._])(2[0-8]|1[0-9]|0?[1-9])$)|(^([2468][048]00)([-\/\._])(0?2)([-\/\._])(29)$)|(^([3579][26]00)([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][0][48])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][0][48])([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][2468][048])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][2468][048])([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][13579][26])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][13579][26])([-\/\._])(0?2)([-\/\._])(29)$)";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(DateText) == true) return true;

            string strRegex2 = @"((^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(10|12|0?[13578])([-\/\._])(3[01]|[12][0-9]|0?[1-9])( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(11|0?[469])([-\/\._])(30|[12][0-9]|0?[1-9])( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(0?2)([-\/\._])(2[0-8]|1[0-9]|0?[1-9])( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^([2468][048]00)([-\/\._])(0?2)([-\/\._])(29)( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^([3579][26]00)([-\/\._])(0?2)([-\/\._])(29)( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^([1][89][0][48])([-\/\._])(0?2)([-\/\._])(29)( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^([2-9][0-9][0][48])([-\/\._])(0?2)([-\/\._])(29)( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^([1][89][2468][048])([-\/\._])(0?2)([-\/\._])(29)( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^([2-9][0-9][2468][048])([-\/\._])(0?2)([-\/\._])(29)( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^([1][89][13579][26])([-\/\._])(0?2)([-\/\._])(29)( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^([2-9][0-9][13579][26])([-\/\._])(0?2)([-\/\._])(29)( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$))";
            Regex re2 = new Regex(strRegex2);
            if (re2.IsMatch(DateText) == true) return true;

            return false;
        }

        public static bool IfDateFormat(string DateText)
        {
            string strRegex = @"(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(10|12|0?[13578])([-\/\._])(3[01]|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(11|0?[469])([-\/\._])(30|[12][0-9]|0?[1-9])$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(0?2)([-\/\._])(2[0-8]|1[0-9]|0?[1-9])$)|(^([2468][048]00)([-\/\._])(0?2)([-\/\._])(29)$)|(^([3579][26]00)([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][0][48])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][0][48])([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][2468][048])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][2468][048])([-\/\._])(0?2)([-\/\._])(29)$)|(^([1][89][13579][26])([-\/\._])(0?2)([-\/\._])(29)$)|(^([2-9][0-9][13579][26])([-\/\._])(0?2)([-\/\._])(29)$)";
            Regex re = new Regex(strRegex);
            return re.IsMatch(DateText);
        }

        public static bool IfDateTimeFormat(string DateTimeText)
        {
            string strRegex = @"((^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(10|12|0?[13578])([-\/\._])(3[01]|[12][0-9]|0?[1-9])( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(11|0?[469])([-\/\._])(30|[12][0-9]|0?[1-9])( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^((1[8-9]\d{2})|([2-9]\d{3}))([-\/\._])(0?2)([-\/\._])(2[0-8]|1[0-9]|0?[1-9])( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^([2468][048]00)([-\/\._])(0?2)([-\/\._])(29)( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^([3579][26]00)([-\/\._])(0?2)([-\/\._])(29)( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^([1][89][0][48])([-\/\._])(0?2)([-\/\._])(29)( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^([2-9][0-9][0][48])([-\/\._])(0?2)([-\/\._])(29)( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^([1][89][2468][048])([-\/\._])(0?2)([-\/\._])(29)( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^([2-9][0-9][2468][048])([-\/\._])(0?2)([-\/\._])(29)( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^([1][89][13579][26])([-\/\._])(0?2)([-\/\._])(29)( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$)|(^([2-9][0-9][13579][26])([-\/\._])(0?2)([-\/\._])(29)( (([0,1]?[0-9])|[2][0-3]):([0-5]?[0-9]):([0-5]?[0-9]))?$))";
            Regex re = new Regex(strRegex);
            return re.IsMatch(DateTimeText);
        }

        public static bool IfMailFormat(string EmailText)
        {
            string strRegex = @"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$";
            Regex re = new Regex(strRegex);
            return re.IsMatch(EmailText);
        }

        public static bool IfPostFormat(string PostText)
        {
            string strRegex = @"^\d{6}$";
            Regex re = new Regex(strRegex);
            return re.IsMatch(PostText);
        }

        /// <summary>
        /// 电话格式验证
        /// </summary>
        /// <param name="PhoneText">电话号码</param>
        /// <returns></returns>
        public static bool IfTelPhoneFormat(string PhoneText)
        {
            string strRegex = @"^(^[0-9]{3,4}\-[0-9]{3,8}$)|(^[0-9]{3,8}$)|(^\([0-9]{3,4}\)[0-9]{3,8}$)|(^0{0,1}1[0-9][0-9]{9}$)";
            Regex re = new Regex(strRegex);
            return re.IsMatch(PhoneText);
        }

        /// <summary>
        /// 手机号码格式验证
        /// </summary>
        /// <param name="PhoneText">手机号码</param>
        /// <returns></returns>
        public static bool IfPhoneFormat(string PhoneText)
        {
            string strRegex = @"^[1][3,4,5,6,7,8,9][0-9]{9}$";
            Regex re = new Regex(strRegex);
            return re.IsMatch(PhoneText);
        }

        public static bool IfUrlFormat(string UrlText)
        {
            string strRegex = @"^http://([\w-]+\.)+[\w-]+(/[\w-./?%&=]*)?$";
            Regex re = new Regex(strRegex);
            return re.IsMatch(UrlText);
        }

        public static bool IsNumber(string NumberValue)
        {
            if (NumberValue.Trim().Equals(string.Empty))
            {
                return false;
            }
            string tempValue = "0123456789";
            for (int i = 0; i < NumberValue.Length; i++)
            {
                if (tempValue.IndexOf(NumberValue[i]) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        public static bool IsDouble(string NumberValue)
        {
            if (NumberValue.Trim().Equals(string.Empty))
            {
                return false;
            }
            if (NumberValue.Trim().Substring(0, 1).Trim() == ".")
            {
                return false;
            }
            string tempValue = "0123456789.";
            for (int i = 0; i < NumberValue.Length; i++)
            {
                if (tempValue.IndexOf(NumberValue[i]) < 0)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 组织机构代码加权因子
        /// </summary>
        public static int[] ww = { 3, 7, 9, 10, 5, 8, 4, 2 };

         /// <summary>
        ///  验证机构组织代码
        /// </summary>
        /// <param name="unitCode">组织机构代码</param>
        /// <returns>是否有效</returns>
        public static bool CheckUnitCode(string unitCode)
        {
            return isUnitCode(unitCode);
        }

        /// <summary>
        ///  验证输入是否为机构组织代码或社会统一信用代码
        /// </summary>
        /// <param name="unitCode">机构组织代码或社会统一信用代码</param>
        /// <returns>是否有效</returns>
        public static bool CheckUnitCodeOrCreditCode(string unitCode)
        {
            bool b1= isUnitCode(unitCode);
            bool b2 = isSocialCreditCode(unitCode);
            if (b1 == true || b2 == true)
                return true;
            else
                return false;
        }

        /// <summary>
        ///  验证机构组织代码
        /// </summary>
        /// <param name="unitCode">组织机构代码</param>
        /// <returns>是否有效</returns>
        public static bool isUnitCode(string unitCode)
        {
            //本标准根据国家技术监督局 １９９７年国家标准制修订项目补充计划，对 ＧＢ／Ｔ １１７１４—１９９５《全国组织机构代码编制规则》进行修订
            string strRegex = @"^[A-Z0-9]{8}([0-9]{1}|X)$";
            Regex re = new Regex(strRegex);
            if (re.IsMatch(unitCode) == false)
            {
                return false;
            }

            char[] _char = unitCode.ToCharArray();
            int[] cc = new int[8];
            int DD = 0;
            int C9 = 0;
            for (int i = 0; i < 8; i++)//将字符转换成ASCILL码
            {
                cc[i] = (int)_char[i];

                //将ASCILL码转换成对应数值
                if (47 < cc[i] && cc[i] < 58)//0-9
                    cc[i] = cc[i] - 48;
                else
                    cc[i] = cc[i] - 65 + 10;//A-Z
            }
            for (int i = 0; i < 8; i++)
            {
                DD += cc[i] * ww[i];
            }
            C9 = 11 - DD % 11; //计算校验码
            if (C9 == 10)
            {
                if (_char[8] == 'X')
                    return true;
                else
                    return false;
            }
            else if (C9 == 11)
            {
                if (_char[8] == '0')
                    return true;
                else
                    return false;
            }
            else
            {
                if (_char[8] == (char)(C9 + 48))
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 验证社会统一信用代码
        /// </summary>
        /// <param name="code">社会统一信用代码</param>
        /// <returns>true or false</returns>
        public static bool isSocialCreditCode(string code)
        {
            //if (code == null || code.Length != 18) return false;
            //code = code.ToUpper();
            //int[] factor = { 1, 3, 9, 27, 19, 26, 16, 17, 20, 29, 25, 13, 8, 24, 10, 30, 28 };
            //string str = "0123456789ABCDEFGHJKLMNPQRTUWXY";
            //int total = factor.Select((p, i) => p * str.IndexOf(code[i])).Sum();
            //int index = total % 31 == 0 ? 0 : (31 - total % 31);
            //return str[index] == code.Last();


            string strRegex = @"[^_IOZSVa-z\W]{2}\d{6}[^_IOZSVa-z\W]{10}";
            Regex re = new Regex(strRegex);
            return re.IsMatch(code);
        }

        /// <summary>
        /// 从社会统一代码中获取组织机构代码
        /// </summary>
        /// <param name="code">社会统一代码</param>
        /// <returns>组织机构代码</returns>
         public static string GetZZJGDMFromCreditCode(string code)
        {
            if (code.Length == 18)
            {
                return code.Substring(8, 9);
            }
            else return code;
        }

        /// <summary>
        /// 比较两个单位代码（组织机构代码或社会统一信用代码）是否位同一家单位
        /// </summary>
        /// <param name="code1">机构代码1</param>
         /// <param name="code2">机构代码2</param>
        /// <returns>true：同一家单位；false：不是一家单位</returns>
         public static bool CompareUnitCode(string code1, string code2)
         {
            int lenCode1= code1.Length;
            int lenCode2= code2.Length;
            if((lenCode1!=18 && lenCode1 !=9)||(lenCode2!=18 && lenCode2 !=9))
            {
                return false;
            }
            if (lenCode1 == lenCode2)
            {
                return (code1 == code2);
            }
            else
            {
                return ((lenCode1 == 18 ? code1.Substring(8, 9) : code1) == (lenCode2 == 18 ? code2.Substring(8, 9) : code2));
            }
         }

        /// <summary>
        /// 验证身份证号码（15/18位,省市，验证校验位）
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static bool isChinaIDCard(string Id)
        {
            if (Id.Length == 18)
            {
                bool check = isChinaIDCard18(Id);
                return check;
            }
            else if (Id.Length == 15)
            {
                bool check = isChinaIDCard15(Id);
                return check;
            }
            else
            { return false; }
        }

        /// <summary>
        /// 是否为港澳台居住证，810000：香港；820000：澳门；830000：台湾
        /// </summary>
        /// <param name="Id">证件号码</param>
        /// <returns></returns>
        public static bool isGangAoTai(string Id)
        {
            if (Id.Substring(0, 6) == "810000" || Id.Substring(0, 6) == "820000" || Id.Substring(0, 6) == "830000")
                return true;
            else 
                return false;
        }

        protected static bool isChinaIDCard18(string Id)
        {
            if(isGangAoTai(Id)==true)
            {
                return true;
            }
            long n = 0;
            if (long.TryParse(Id.Remove(17), out n) == false || n < Math.Pow(10, 16) || long.TryParse(Id.Replace('x', '0').Replace('X', '0'), out n) == false)
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 8).Insert(6, "-").Insert(4, "-");
            DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            string[] arrVarifyCode = ("1,0,x,9,8,7,6,5,4,3,2").Split(',');
            string[] Wi = ("7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2").Split(',');
            char[] Ai = Id.Remove(17).ToCharArray();
            int sum = 0;
            for (int i = 0; i < 17; i++)
            {
                sum += int.Parse(Wi[i]) * int.Parse(Ai[i].ToString());
            }
            int y = -1;
            Math.DivRem(sum, 11, out y);
            if (arrVarifyCode[y] != Id.Substring(17, 1).ToLower())
            {
                return false;//校验码验证
            }
            return true;//符合GB11643-1999标准
        }

        protected static bool isChinaIDCard15(string Id)
        {
            long n = 0; if (long.TryParse(Id, out n) == false || n < Math.Pow(10, 14))
            {
                return false;//数字验证
            }
            string address = "11x22x35x44x53x12x23x36x45x54x13x31x37x46x61x14x32x41x50x62x15x33x42x51x63x21x34x43x52x64x65x71x81x82x91";
            if (address.IndexOf(Id.Remove(2)) == -1)
            {
                return false;//省份验证
            }
            string birth = Id.Substring(6, 6).Insert(4, "-").Insert(2, "-"); DateTime time = new DateTime();
            if (DateTime.TryParse(birth, out time) == false)
            {
                return false;//生日验证
            }
            return true;//符合15位身份证标准
        }

        /// <summary>
        /// 15位身份证转换为18位
        /// </summary>
        /// <param name="perIDSrc">15位身份证</param>
        /// <returns>18位身份证</returns>
        public static string ConvertoIDCard15To18(string perIDSrc)
        {
            int iS = 0;

            //加权因子常数
            int[] iW = new int[] { 7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2 };
            //校验码常数
            string LastCode = "10X98765432";
            //新身份证号
            string perIDNew;

            perIDNew = perIDSrc.Substring(0, 6);
            //填在第6位及第7位上填上‘1’，‘9’两个数字
            perIDNew += "19";
            perIDNew += perIDSrc.Substring(6, 9);
            //进行加权求和
            for (int i = 0; i < 17; i++)
            {
                iS += int.Parse(perIDNew.Substring(i, 1)) * iW[i];
            }

            //取模运算，得到模值
            int iY = iS % 11;
            //从LastCode中取得以模为索引号的值，加到身份证的最后一位，即为新身份证号。
            perIDNew += LastCode.Substring(iY, 1);

            return perIDNew;
        }

        /// <summary>
        /// 15位身份证转换为18位
        /// </summary>
        /// <param name="perIDSrc">15位身份证</param>
        /// <returns>18位身份证</returns>
        public static string ConvertoIDCard18To15(string perIDSrc)
        {
            try
            {
                return perIDSrc.Remove(17, 1).Remove(6, 2);
            }
            catch
            {
                return perIDSrc;
            }
        }


        /// <summary>
        /// 从身份证号中提取生日，空或不合法身份证号返回当前日期
        /// </summary>
        /// <param name="cardId">身份证号码</param>
        /// <returns></returns>
        public static DateTime getBirthday(string cardId)
        {
            if (!isChinaIDCard(cardId))
            {
                return DateTime.Now;
            }
            int year, month, day;
            if (cardId.Length == 18)
            {
                year = int.Parse(cardId.Substring(6, 4));
                month = int.Parse(cardId.Substring(10, 2));
                day = int.Parse(cardId.Substring(12, 2));
                return new DateTime(year, month, day);
            }
            if (cardId.Length == 15)
            {
                year = int.Parse("19" + cardId.Substring(6, 2));
                month = int.Parse(cardId.Substring(8, 2));
                day = int.Parse(cardId.Substring(10, 2));
                return new DateTime(year, month, day);
            }
            return DateTime.Now;
        }

        /// <summary>
        /// 根据身份证号判断性别
        /// </summary>
        /// <param name="cardId">身份证号</param>
        /// <returns>“男”/“女”/“”(非法身份证号)</returns>
        public static string getSex(string cardId)
        {
            if (!isChinaIDCard(cardId))
            {
                return "";
            }
            int index = 0;
            //string subStr = cardId.Length == 18 ? int.Parse(cardId.Substring(14, 3)) : int.Parse(cardId.Substring(12, 3));
            index = cardId.Length == 18 ? int.Parse(cardId.Substring(14, 3)) : int.Parse(cardId.Substring(12, 3));
            if (index % 2 == 1)
                return "男";
            else
                return "女";
        }

        #endregion Check 方法

        #region 全角半角检查转换

        /// <summary>
        /// 判断字符是否英文半角字符或标点

        /// </summary>
        /// <remarks>
        /// 32    空格
        /// 33-47    标点
        /// 48-57    0~9
        /// 58-64    标点
        /// 65-90    A~Z
        /// 91-96    标点
        /// 97-122    a~z
        /// 123-126  标点
        /// </remarks>
        public static bool IsBjChar(char c)
        {
            int i = (int)c;
            return i >= 32 && i <= 126;
        }

        /// <summary>
        /// 判断字符是否全角字符或标点

        /// </summary>
        /// <remarks>
        /// <para>全角字符 - 65248 = 半角字符</para>
        /// <para>全角空格例外</para>
        /// </remarks>
        public static bool IsQjChar(char c)
        {
            if (c == '\u3000') return true;

            int i = (int)c - 65248;
            if (i < 32) return false;
            return IsBjChar((char)i);
        }

        /// <summary>
        /// 将字符串中的全角字符转换为半角
        /// </summary>
        public static string ToBj(string s)
        {
            if (s == null || s.Trim() == string.Empty) return s;

            StringBuilder sb = new StringBuilder(s.Length);
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '\u3000')
                    sb.Append('\u0020');
                else if (IsQjChar(s[i]))
                    sb.Append((char)((int)s[i] - 65248));
                else
                    sb.Append(s[i]);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 计算文本长度，区分中英文字符，中文算两个长度，英文算一个长度
        /// <seealso cref="Common_Function.Text_Length"/>
        /// </summary>
        /// <param name="Text">需计算长度的字符串</param>
        /// <returns>int</returns>
        public static int Text_Length(string Text)
        {
            return Encoding.Default.GetByteCount(Text);
        }

        #endregion 全角半角检查转换

        /// <summary>
        /// 数据库字符串转换
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="dataType"></param>
        /// <returns></returns>
        public static object ConvertDBNull(object obj, SqlDbType dataType)
        {
            switch (dataType)
            {
                case SqlDbType.Bit:
                    if (System.Convert.IsDBNull(obj))
                        return false;
                    else
                        return System.Convert.ToBoolean(obj);

                case SqlDbType.Int:
                    if (System.Convert.IsDBNull(obj))
                        return 0;
                    else
                        return System.Convert.ToInt32(obj);

                case SqlDbType.Float:
                    if (System.Convert.IsDBNull(obj))
                        return 0.0;
                    else
                        return System.Convert.ToDouble(obj);

                case SqlDbType.Money:
                    if (System.Convert.IsDBNull(obj))
                        return 0;
                    else
                        return System.Convert.ToDecimal(obj);

                case SqlDbType.DateTime:
                    if (System.Convert.IsDBNull(obj))
                        return new DateTime(1900, 1, 1);
                    else
                        return System.Convert.ToDateTime(obj);

                case SqlDbType.NVarChar:
                    if (System.Convert.IsDBNull(obj))
                        return "";
                    else
                        return System.Convert.ToString(obj);

                case SqlDbType.Char:
                    if (System.Convert.IsDBNull(obj))
                        return "";
                    else
                        return System.Convert.ToString(obj);

                case SqlDbType.NChar:
                    if (System.Convert.IsDBNull(obj))
                        return "";
                    else
                        return System.Convert.ToString(obj);

                case SqlDbType.VarChar:
                    if (System.Convert.IsDBNull(obj))
                        return "";
                    else
                        return System.Convert.ToString(obj);

                case SqlDbType.Binary:
                    return obj;

                default:
                    if (System.Convert.IsDBNull(obj))
                        return null;
                    else
                        return obj;
            }
        }

        /// <summary>
        /// 转化Base64编码 到 字符串
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetBase64Code(string str)
        {
            byte[] bytes = Convert.FromBase64String(str);
            return Encoding.UTF8.GetString(bytes); ;
        }

        /// <summary>
        /// 将字符串转化 为 Base64编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Base64Code(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            string encode = Convert.ToBase64String(bytes);
            return encode;
        }

        #region 建设部字典

        /// <summary>
        /// 获取民族字典编码
        /// </summary>
        /// <param name="NationName">民族名称</param>
        /// <returns>民族编码</returns>
        public static string GetNationCode(string NationName)
        {
            switch (NationName)
            {
                case "汉族":
                    return "1";
                case "蒙古族":
                    return "2";
                case "回族":
                    return "3";
                case "藏族":
                    return "4";
                case "维吾尔族":
                    return "5";
                case "苗族":
                    return "6";
                case "彝族":
                    return "7";
                case "壮族":
                    return "8";
                case "布依族":
                    return "9";
                case "朝鲜族":
                    return "10";
                case "满族":
                    return "11";
                case "侗族":
                    return "12";
                case "瑶族":
                    return "13";
                case "白族":
                    return "14";
                case "土家族":
                    return "15";
                case "哈尼族":
                    return "16";
                case "哈萨克族":
                    return "17";
                case "傣族":
                    return "18";
                case "黎族":
                    return "19";
                case "傈僳族":
                    return "20";
                case "佤族":
                    return "21";
                case "畲族":
                    return "22";
                case "高山族":
                    return "23";
                case "拉祜族":
                    return "24";
                case "水族":
                    return "25";
                case "东乡族":
                    return "26";
                case "纳西族":
                    return "27";
                case "景颇族":
                    return "28";
                case "柯尔克孜族":
                    return "29";
                case "土族":
                    return "30";
                case "达斡尔族":
                    return "31";
                case "仫佬族":
                    return "32";
                case "羌族":
                    return "33";
                case "布朗族":
                    return "34";
                case "撒拉族":
                    return "35";
                case "毛南族":
                    return "36";
                case "仡佬族":
                    return "37";
                case "锡伯族":
                    return "38";
                case "阿昌族":
                    return "39";
                case "普米族":
                    return "40";
                case "塔吉克族":
                    return "41";
                case "怒族":
                    return "42";
                case "乌孜别克族":
                    return "43";
                case "俄罗斯族":
                    return "44";
                case "鄂温克族":
                    return "45";
                case "德昴族":
                    return "46";
                case "保安族":
                    return "47";
                case "裕固族":
                    return "48";
                case "京族":
                    return "49";
                case "塔塔尔族":
                    return "50";
                case "独龙族":
                    return "51";
                case "鄂伦春族":
                    return "52";
                case "赫哲族":
                    return "53";
                case "门巴族":
                    return "54";
                case "珞巴族":
                    return "55";
                case "基诺族":
                    return "56";

                default:
                    return "";
            }
        }

        /// <summary>
        /// 获取职称字典编码
        /// </summary>
        /// <param name="EduLevelName">职称名称</param>
        /// <returns>职称编码</returns>
        public static string GetEduLevelCode(string EduLevelName)
        {
            switch (EduLevelName)
            {
                case "助理工程师":
                    return "1";
                case "工程师":
                    return "2";
                case "高级工程师":
                    return "3";
                case "助理会计师":
                    return "4";
                case "会计师":
                    return "5";
                case "高级会计师":
                    return "6";
                case "助理建筑师":
                    return "7";
                case "建筑师":
                    return "8";
                case "高级建筑师":
                    return "9";
                case "助理经济师":
                    return "10";
                case "经济师":
                    return "11";
                case "高级经济师":
                    return "12";
                case "助理商务师":
                    return "13";
                case "商务师":
                    return "14";
                case "高级商务师":
                    return "15";
                case "助理审计师":
                    return "16";
                case "审计师":
                    return "17";
                case "高级审计师":
                    return "18";
                case "助理统计师":
                    return "19";
                case "统计师":
                    return "20";
                case "高级统计师":
                    return "21";
                case "助理研究员":
                    return "22";
                case "研究员":
                    return "23";
                case "讲师":
                    return "24";
                case "高级讲师":
                    return "25";
                case "教授级高工":
                    return "26";
                case "副研究员":
                    return "27";
                case "无":
                    return "28";

                default:
                    return "";
            }
        }


        /// <summary>
        /// 获取学历编码
        /// </summary>
        /// <param name="titlesName">学历名称</param>
        /// <returns></returns>
        public static string GetTitlesID(string titlesName)
        {
            switch (titlesName)
            {
                case "博士":
                    return "1";
                case "博士后":
                    return "2";
                case "高中":
                    return "3";
                case "大学":
                    return "4";
                case "中专以下":
                    return "5";
                case "硕士":
                    return "6";
                case "中专":
                    return "7";
                case "本科":
                    return "8";
                case "大专":
                    return "9";
                default:
                    return "";
            }
        }

        
       /// <summary>
        /// 获取单位主营业务ID
        /// </summary>
        /// <param name="UnitMainBusiness">主营业务名称</param>
        /// <returns></returns>
        public static string GetUnitMainBusinessID(string UnitMainBusiness)
        {
            if (UnitMainBusiness.Contains("造价咨询"))
                return "1";
            else if (UnitMainBusiness.Contains("招标") || UnitMainBusiness.Contains("监理"))
                return "2";
            else if (UnitMainBusiness.Contains("房地产"))
                return "3";
            else if (UnitMainBusiness.Contains("设计研究"))
                return "4";
            else if (UnitMainBusiness.Contains("施工"))
                return "5";
            else if (UnitMainBusiness.Contains("事业单位"))
                return "6";
            else if (UnitMainBusiness.Contains("其它类型"))
                return "7";
            else
                return "7";
        }

          /// <summary>
        /// 获取聘用单位类别编码
        /// </summary>
        /// <param name="enterPriseTypeName">聘用单位类别</param>
        /// <returns></returns>
        public static string GetEnterPriseTypeID(string enterPriseTypeName)
        {
            if (enterPriseTypeName.Contains("有限责任公司"))
                return "1";
            else if (enterPriseTypeName.Contains("合伙企业"))
                return "2";
            else if (enterPriseTypeName.Contains("港、澳、台商投资企业"))
                return "3";
            else if (enterPriseTypeName.Contains("集体企业"))
                return "4";
            else if (enterPriseTypeName.Contains("股份合作企业"))
                return "5";
            else if (enterPriseTypeName.Contains("外商投资企业"))
                return "6";
            else if (enterPriseTypeName.Contains("国有企业"))
                return "7";
            else if (enterPriseTypeName.Contains("股份有限企业"))
                return "8";
            else if (enterPriseTypeName.Contains("其他企业"))
                return "9";
            else
                return "9";
        }
        
          /// <summary>
        /// 获取业绩项目类型编码
        /// </summary>
        /// <param name="projectsTypeName">业绩项目类型</param>
        /// <returns></returns>
        public static string GetProjectsTypeID(string projectsTypeName)
        {
            switch (projectsTypeName)
            {
                case "（一）项目经济评估":
                    return "1";
                case "（二）建设项目投资估算的编制、审核":
                    return "2";
                case "（三）概算、预算、结算、竣工决算编制、审核":
                    return "3";
                case "（四）工程招标标底价、投标报价的编制、审核":
                    return "4";
                case "（五）其他":
                    return "5";
                default:
                    return "";
            }
        }
     
        #endregion
      
    }
}