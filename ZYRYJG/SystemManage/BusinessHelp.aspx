<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BusinessHelp.aspx.cs" Inherits="ZYRYJG.SystemManage.BusinessHelp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>业务规则</title>
    <link href="../Css/styleRed.css?v=1.0.12" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-3.4.1.min.js" type="text/javascript"></script>
    <script src="../Scripts/Public.js?v=1.011" type="text/javascript"></script>
    <script type="text/javascript">
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
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="div_out">
            <div class="dqts">
                <div style="float: left;">
                    当前位置 &gt;&gt; 系统管理 &gt;&gt;
                系统配置 &gt;&gt; <strong>业务规则查看</strong>
                </div>
            </div>
            <div style="width: 98%; margin: 5px auto; padding: 20px 0px;">
                <div class="content">
                    <div class="jbxxbt">
                        业务规则查看
                    </div>

                    <div class="DivTitleOn" onclick="DivOnOff(this,'Td1',event);" title="折叠">
                        一、从业人员考试报名
                    </div>
                    <div class="DivContent" id="Td1" style="padding: 20px 20px; line-height: 180%; font-size: 16px;">
                        1、报名限制：年龄未满18周岁。
            <br />
                        非法人A本、B本、C本 ，男60周岁、女55周岁。
            <br />
                        法人A本（报考人是企业法人），不设年龄上限。
            <br />
                        建筑施工特种作业,男60周岁女50周岁。
            <br />

                        <br />
                        2、报名限制检查（项目负责人、专职安全生产管理人员）：每人只能有一个"项目负责人"和"专职安全生产管理人员"证，但两本必须在同一单位。
                        专职安全员分为（机械类C1、土建类C2、综合类C3），其中有C1只能报考C2，有C2只能报考C1，有C3不能报考C1和C2，没有专业安全员可报考C1、C2、C3任意一个。
            <br />
                        3、报名限制检查:不能在同一家公司取得多个企业负责人证书。
            <br />
                        4、已有同类型证书，尚未过期，不能报名。（但满足第五条可重复，但必须不同等级；企业负责人可重复，但必须多加单位）。
            <br />
                        5、职业技能岗位证书（除“村镇建筑工匠”和“普工”）外，已取得同类型同“技术等级”证书，尚未过期，不能报名。
            <br />
                        6、报考“物业项目负责人”：
            <br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 6.1）比对《在岗无证物业项目负责人》库，一致允许报考。
            <br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 6.2）不在《在岗无证物业项目负责人》库，需要比对物业企业资质库，满足允许报考（组织机构代码）。
            <br />
                        7、在黑名单（替考、违纪）中人员限制一年内不得报考所有专业。
            <br />
                        8、系统中存在相同岗位工种证书，证书处于锁定中，解锁前不允许报名。
            <br />
                        9、人员（身份证）处在被锁定状态中，解锁前不允许报名。
            <br />
                        10、三类人员时，增加考生的企业名称（或组织机构代码）与本市管理的建筑企业资质库的比对功能，库外的不得报考。（其中起重机械租赁企业只允许报考专职安全生产管理人员（C1、C2、C3本），不能报考A本和B本）
            <br />
                        11、项目负责人增加考生的身份证号码与本市企业建造师注册证书库的比对功能，库外的不得报考。
            <br />
                        12、报考“房屋建筑设施设备安全管理员”和“房屋建筑结构安全管理员”，检查本外地物业企业资质库，一致允许报考。
            <br />
                        13、职业技能岗位考试计划有“技术等级”分类,报名时不允许手工填写，自动获取为与考试计划一致。（其中村镇建筑工匠or普工两个岗位无等级，系统自动填写为“无”）
            <br />
                        14、根据有关规定，停止造价员、拆迁员、安全监理员、物业项目负责人、房屋结构安全管理员和房屋设备安全管理员考核、变更和续期工作。
            <br />
                        15、相同岗位类型一个月只能参加一个工种考试报名。
            <br />
                        16、三类人和专业技术人员考试，存在下列情况之一免初审：1）报考B本；2）社保比对合格；3)企业法人报考A本。
            <br />
                        17、缺考次数限制：
            <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;17.1）报考三类人员和专业管理人员时，一年内累积三次考试缺考（0分）的（三类人员和专业管理人员都包含），锁定身份证号码一年，一年内不能报三类人员和专业管理人员考试。
            <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;17.2）报考三类人员和专业管理人员时，一年内上一次考试缺考（0分）的（三类人员和专业管理人员都包含），须现场审核报考材料（即使往届考生、建造师报B本或社保比对一致的也须现场审核报考材料）。
            <br />
                        18、对特种作业人员考核业务限定企业范围：本市注册的施工企业、来京施工备案企业和在京备案的起重租赁公司。
            <br />
                        19、企业审核时间为从报名开始日期+1日，至报名截至日期 +2日。
                    </div>
                    <br />
                    <div class="DivTitleOff" onclick="DivOnOff(this,'Div2',event);" title="展开">
                        二、从业人员证书变更
                    </div>
                    <div class="DivContent" id="Div2" style="padding: 20px 20px; line-height: 180%; font-size: 16px; display: none;">
                        1、一年内最多5次变更单位（项目负责人变更特殊：当大于5次变更时，如果变更前与二建所在单位不匹配，变更后匹配允许变更）；
                <br />
                        2、已提交变更申请未审批的不能重复提交；
                <br />
                        3、正在办理续期的，续期未结束不能办理变更；
                <br />
                        4、证书处于锁定中，不允许变更（内部锁）；
                <br />
                        <span style="text-decoration: line-through">5、证书为尚未发放状态（未打印），不允许变更电子版；</span>
                        <br />
                        6、检查拆迁企业拆迁员最低人数限制（离京变更、注销或变更单位时检查），规则：该企业有效拆迁员个数 - 当前申请个数1 >= 最低人数限制
                <br />
                        7、检查物业项目负责人是否被锁定（外部锁),（离京变更、注销或变更单位时检查），锁定时不能提交变更；
                <br />
                        8、只有“安全生产考核三类人员”和“造价员”才能申请证书离京，其它（特种作业、职业技能、专业技术人员）不提供离京变更功能。
                <br />
                        9、特殊：合同员不提供变更服务。
                <br />
                        10、三类人员证书变更规则（只涉及京内变更、补办。离京变更和注销无限制）：
	            <br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 10.1、组织机构代码和企业名称无变化，其它4项（姓名、证件号码、性别、出生日期）可以变更；
	            <br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 10.2、组织机构代码不变，企业名称变更，检查新企业名称与组织机构代码是否与本（外）地企业资质库一致，一致允许变更；（相当于名称修正）
	            <br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 10.3、组织机构代码变更，检查新企业名称与新组织机构代码是否与本地企业资质库一致，一致允许变更；（只检查本地资质库，即不允许变更到外地企业）
	            <br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 10.4、组织机构代码变更，证书类型为项目负责人（B本）证书，检查变更后企业及人员信息是否与本地建造师（含外地进京备案建造师）企业信息和人员信息是否一致，一致允许变更；（即有B本必须有建造师，且单位、人员信息一致）
                 <br />
                        11、根据有关规定，停止造价员、拆迁员、安全监理员、物业项目负责人、房屋结构安全管理员和房屋设备安全管理员考核、变更和续期工作。
                         <br />
                        12、法人增发A证：
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;12.1、企业法人名下有多家建筑企业，有一本企业主要负责人（A证，有效未过期），企业或个人须先网上申请增发另一本A证，再持材料到注册中心现场审核。<br />
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;12.2、请使用持有A证本人或单位登录系统发起申请。<br />
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;12.3、增发A证的发证日期与有效期将于原持股有A证保持一致，便于未来同时续期。
                        13、特种作业人员变更业务限定企业范围：本市注册的施工企业、来京施工备案企业和在京备案的起重租赁公司
                    </div>
                    <br />
                    <div class="DivTitleOff" onclick="DivOnOff(this,'Div3',event);" title="展开">
                        三、从业人员证书续期
                    </div>
                    <div class="DivContent" id="Div3" style="padding: 20px 20px; line-height: 180%; font-size: 16px; display: none;">
                        1、证书处于锁定中（内部锁），不允许续期。
                <br />
                        2、证书正在办理变更，未办结不能同时申请续期。
                <br />
                        3、三类人员续期条件：证书所在单位无建筑施工企业资质证书（强校验：不包含“外地进京”），不允许续期。
                <br />
                        4、项目负责人B证：无建本地造师注册证书（或中央在京备案、外地进京本案建造师证书）或注册证书单位与所在单位不一致，不允许续期。
                <br />
                        5、三类人续期按企业资质隶属管理自动选择初审机构（央企、集团、区县建委），无隶属关系不允许续期，请先完善资质中隶属关系。
                <br />
                        6、特种作业证书续期申请时允许同时变更单位。
                 <br />
                        7、根据有关规定，停止造价员、拆迁员、安全监理员、物业项目负责人、房屋结构安全管理员和房屋设备安全管理员考核、变更和续期工作
                  <br />
                        8、特种作业人员续期业务限定企业范围：本市注册的施工企业、来京施工备案企业和在京备案的起重租赁公司
                    </div>
                    <br />
                    <div class="DivTitleOff" onclick="DivOnOff(this,'Div4',event);" title="展开">
                        四、从业人员证书进京
                    </div>
                    <div class="DivContent" id="Div4" style="padding: 20px 20px; line-height: 180%; font-size: 16px; display: none;">
                        1、三类人允许办理进京申请，其它类型证书不提供进京功能。
                <br />
                        2、证书号码已经提交过进京申请了，不能重复使用。
                <br />
                        3、已经提交过相同的岗位工种的进京申请了，不能重复申请（造价员有相同专业增项也不能重复申请）。
                <br />
                        4、申请人员在京存在有效的同一岗位证书，不允许申请进京。（企业负责人可以重复申请，但必须保证单位不同）
                <br />
                        5、每人只能有一个"项目负责人"和"专职安全生产管理人员"证，且两本必须在同一单位。
                <br />
                        6、项目负责人（B本），身份证一致且新单位组织机构代码与建造师证书一致的允许进京。
                <br />
                        7、三类人员证书进京，现聘用单位必须在企业资质库中。（强校验：不包含“外地进京”）
                <br />
                        8、根据有关规定，停止造价员续期工作。
                    </div>
                    <br />
                    <div class="DivTitleOff" onclick="DivOnOff(this,'Div5',event);" title="展开">
                        五、企业资质比对顺序
                    </div>
                    <div class="DivContent" id="Div5" style="padding: 20px 20px; line-height: 180%; font-size: 16px; display: none;">
                        》企业存在多个资质时，且名称和组织机构代码不同，比对顺序如下，所有业务必须第一个匹配企业资质信息一致（企业名称、组织机构代码）。
                        <br />
                        1、建筑施工企业
                 <br />
                        2、中央在京
                 <br />
                        3、外地进京
                 <br />
                        4、起重机械租赁企业
                 <br />
                        5、设计施工一体化

 <br />
                        》根据组织机构代码获取第一个企业资质证书信息时的排序规则如下：
                    <br />
                        1、资质类型： 1）本地施工企业，2）本地监理企业，3）本地造价咨询企业，4）本地招标代理机构，5）设计施工一体化，6）工程设计，7）工程勘察，8）其他
                     <br />
                        2、资质序列： 1）施工总承包，2）专业承包，3）专业分包，4）其他
                      <br />
                        3、资质等级： 1）特级，2）壹级，3）贰级，4）叁级，5）甲级，6）乙级，7）其他
                    </div>
                    <br />
                    <div class="DivTitleOff" onclick="DivOnOff(this,'Div6',event);" title="展开">
                        六、企业资质符合人员要求统计
                    </div>
                    <div class="DivContent" id="Div6" style="padding: 20px 20px; line-height: 180%; font-size: 16px; display: none;">
                        1、统计企业资质范围：建筑施工企业、中央在京、设计施工一体化。
                <br />
                        2、企业存在多个资质的按：总包、专包、一体化、分包先后顺序取第一个最高等级资质作为该企业资质进行统计
                <br />
                        3、按资质序列统计：
	                <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;1）施工总承包：特级、一级、二级、三级
	                <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;2）专业承包：一级、二级、三级、不分等级
	                <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;3）施工一体化：一级、二级、三级
	                <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;4）劳务分包：一级、二级、不分等级
                <br />
                        4、统计人员分类：
	                <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;1）管理人员：专职安全生产管理人员、造价员、专业技能岗位
	                <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;2）技术工人：特种作业（都算初级）、职业技能岗位（初级、中级、高级、技师、高级技师）
	                <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;3）分包统计初级及以上人员数量；其他序列（总、专、一体化）统计中级及以上人员数量。
                    </div>
                    <br />
                    <div class="DivTitleOff" onclick="DivOnOff(this,'Div7',event);" title="展开">
                        七、社保比对规则
                    </div>
                    <div class="DivContent" id="Div7" style="padding: 20px 20px; line-height: 180%; font-size: 16px; display: none;">
                        目前按以下业务进行社保比对。业务申请当日夜间与社保局进行数据交换，第二日返回结果。系统不提供自动比对结果，只提供比对查看页面。
                <br />
                        1、从业人员考试报名、证书变更（京内变更和进京变更，不包含离京和注销）、证书续期、证书补登记进行比对。
                <br />
                        2、二级注册建造师初始注册、延续注册、执业企业变更、重新注册、进行比对（增项注册、注销注册、企业信息变更、个人信息变更不进行比对）。
                <br />
                        3、是否符合要求由系统自动判断，判断标准为：判断人员近一个月的社保缴费情况，五个险种（养老、工伤、失业、生育，医保）其中养老一项按时缴纳即为合格。
                <br />
                        4、近一个月的社保取值为申报月份的前一个月的社保（无法查询申报当月社保）。
                
                    </div>
                    <br />
                    <div class="DivTitleOff" onclick="DivOnOff(this,'Div8',event);" title="展开">
                        八、申请单有效期
                    </div>
                    <div class="DivContent" id="Div8" style="padding: 20px 20px; line-height: 180%; font-size: 16px; display: none;">
                        1、自动删除从业人员变更申请一个月后没有审核的申请
                <br />
                        2、自动删除从业人员续期申请后证书过期三个月后没有初审的申请
                <br />
                        3、自动删除从业人员进京申请一个月后不审核的申请
                <br />
                        4、自动删除从业人员考试后未审核通过的考试报名申请
                 <br />
                        5、自动删除二级注册建造师申报后90天区县未受理的申请单。
                    </div>
                    <br />
                    <div class="DivTitleOff" onclick="DivOnOff(this,'Div9',event);" title="展开">
                        九、超龄注销
                    </div>
                    <div class="DivContent" id="Div9" style="padding: 20px 20px; line-height: 180%; font-size: 16px; display: none;">
                        1、企业负责人（三类人员安全考核）不设年龄上限。
                 <br />
                        2、项目负责人（三类人员安全考核）年龄上限为65周岁。
                 <br />
                        3、专职安全生产管理人员（三类人员安全考核）、专业技术管理人员、造价员和建设职业技能人员年龄上限为60周岁。
                 <br />
                        4、建筑施工特种作业人员年龄上限为男60周岁女50周岁。
                 <br />
                        5、每日按照上述限制系统自动执行超龄注销操作，在证书备注中标明超龄注销日期。
                 <br />
                        6、报考、证书进京按上述限制比对，超龄不允许申请业务。<br />

                        7、二级建造师，年满65周岁前90天,不再允许其发起二级建造师初始、重新、延续、增项、执业企业变更注册申请
                    </div>
                    <br />
                    <div class="DivTitleOff" onclick="DivOnOff(this,'Div10',event);" title="展开">
                        十、电子证书生成规则
                    </div>
                    <div class="DivContent" id="Div10" style="padding: 20px 20px; line-height: 180%; font-size: 16px; display: none;">
                        1、电子证书应用范围：1）专业技术人员岗位；2）三类人；3）职业技能人员；4）特种作业。
                 <br />
                        2、电子证书创建时间：证书变化后（首次发证、变更、续期、进京），如果证书有效（未过期、未离京、未注销）创建电子证书去相关部门盖章，1或2日返回。
                     <br />
                        3、电子证书创建要求：<br />
                        <div style="padding-left: 40px">
                            1）证书必须已经上传了一寸电子照片；<br />
                            2）身份证格式错误无法进行电子签章；<br />
                            3）证书所在企业必须存在建委法人库中存在。<br />
                            4）造价员、物业项目负责人、拆迁员、安全监理员、房屋建筑结构安全管理员、房屋建筑设施设备安全管理员、监理员（房屋建筑）暂时不申请电子证书
                        </div>
                    </div>
                    <br />
                    <div class="DivTitleOn" onclick="DivOnOff(this,'TdErJianProgress',event);" title="折叠">
                        十一、二级注册建造师注册流程
                    </div>
                    <div class="DivContent" id="TdErJianProgress" style="padding: 20px 20px; line-height: 180%; font-size: 16px; display: none;">
                        1、初始注册：个人申报 >> 企业确认 >> 区县业务员受理 >> 区县领导审查 >> 区县业务员汇总 >> 区县业务员上报 >> 注册中心业务员审查 >> 注册中心领导决定 >> 公示 >> 公告 >> 发放编号 >> 办结<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 1.1、不允许注册二级临时证书
                        <br />
                        2、重新注册：个人申报 >> 企业确认 >> 区县业务员受理 >> 区县领导审查 >> 区县业务员汇总 >> 区县业务员上报 >> 注册中心业务员审查 >> 注册中心领导决定 >> 公示 >> 公告 >> 发放编号 >> 办结<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 2.1、只有证书注销了，证书才能重新注册。（过期证书必须先办理注销注册才能发起重新注册申请）<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 2.2、重新注册可以重资格库任选一个或多个专业注册，不局限于原来的专业。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 2.3、重新注册可以在新单位发起(按创建单位匹配)。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 2.4、不允许注册二级临时证书
                        <br />
                        3、增项注册：个人申报 >> 企业确认 >> 注册中心业务员审查 >> 注册中心领导决定 >> 公示 >> 公告 >> 办结<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 3.1、截止前30天关闭申请通道，进入有效期截止30天内只允许提交注销业务。<br />
                        4、延续注册：个人申报 >> 企业确认 >> 区县业务员受理 >> 区县领导审查 >> 区县业务员汇总 >> 区县业务员上报 >> 注册中心业务员审查 >> 注册中心领导决定 >> 公示 >> 公告 >> 办结<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 4.1、有效期截止时间前90天开放申请，截止前30天关闭申请通道。<br />
                        <span style="text-decoration: line-through">5、遗失补办（已作废）：企业申报 >> 区县业务员受理 >> 区县领导审查 >> 区县业务员汇总 >> 区县业务员上报 >> 注册中心业务员审查 >> 注册中心领导决定 >> 发放编号 >> 办结</span><br />
                        6、变更注册：<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;6.1、执业企业变更：个人申报 >> 调出企业同意 >> 调入企业确认 >> 调入企业区县业务员受理 >> 调入企业区县领导审查>> 调入企业区县业务员汇总>> 调入企业区县业务员上报 >> 办结<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;1）截止前30天关闭申请通道，进入有效期截止30天内只允许提交注销业务。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;6.2、企业名称变更：请先变更企业一级建造师企业名称 >> 企业申报 >> 区县业务员受理 >> 区县领导审查 >> 区县业务员汇总 >> 区县业务员上报 >> 办结<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;1）请先变更完一级(一级临时)证书才能变更二级证书！根据企业名称检验一级建造师企业如存在该企业则不允许变更企业。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;2）企业有未办结的注册事项，无法变更企业信息！<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;3）有过期未注销的二级建造师不允许变更<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;4）企业名称来源于工商注册信息，注册地址及隶属区县从企业资质中获取。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;6.3、个人信息变更：个人申报 >> 企业确认 >> 区县业务员受理 >> 区县领导审查 >> 区县业务员汇总 >> 区县业务员上报 >> 注册中心业务员审查 >> 注册中心领导决定 >> 办结<br />
                        7、注销注册：个人申报 >> 企业确认 >> 区县业务员受理 >> 区县领导审查 >> 区县业务员汇总 >> 区县业务员上报 >> 注册中心业务员审查 >> 注册中心领导决定 >> 办结<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;7.1、截止前30天关闭申请通道，进入有效期截止30天内只允许提交注销业务。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;7.2、证书锁定时（注册中心锁定），只能办理注销注册，其他注册不允许。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;7.3、被动注销：注册中心定期检测，证书过期后长期不办理注销，由建委强制注销。<br />
                    </div>
                    <br />
                    <div class="DivTitleOn" onclick="DivOnOff(this,'DivZJS',event);" title="折叠">
                        十二、二级造价工程师注册流程
                    </div>
                    <div class="DivContent" id="DivZJS" style="padding: 20px 20px; line-height: 180%; font-size: 16px; display: none;">
                        1、初始注册：个人申报 >> 企业确认 >> 市级受理 >> 市级审核 >> 市级复核 >> 市级决定 >> 公告 >> 发放编号 >> 办结<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 1.1、一次只能注册一个专业。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 1.2、已经存在一个专业证书，申请第二专业。两个专业必须在同一家单位，单独核发新一本造价工程师注册证书，两本证书有效期分别计算（两个注册编号、两个有效期）。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 1.3、同一个人二建、二造（多专业证书）必须注册在同一家单位。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 1.4、满70周岁前90天起,不再允许发起注册申请。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 1.5、注册所在企业必须通过本系统工商信息验证，不要求特殊资质。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 1.6、证书注销后，个人可重新注册，申请流程及申请表单使用初始注册内容。<br />
                        <br />
                        2、延续注册：个人申报 >> 企业确认 >> 市级受理 >> 市级审核 >> 市级复核 >> 市级决定 >> 公告 >> 办结<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 2.1、有效期截止时间前90天开放申请，截止前30天关闭申请通道。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 2.2、满70周岁前90天起,不再允许发起注册申请。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; 2.3、每4年申请一次续期，通过后注册证书有效期自动向后延续4年。多专业分别申请延续。<br />
                        3、变更注册：<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;3.1、执业企业变更：个人申报 >> 调出企业同意 >> 调入企业确认 >> 市级受理 >> 市级审核 >> 市级复核 >> 市级决定 >> 办结<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;1）截止前30天关闭申请通道，进入有效期截止30天内只允许提交注销业务。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;3.2、企业名称变更：请先变更企业一级造工程师价师企业名称 >> 企业申报 >>市级受理 >> 市级审核 >> 市级复核 >> 市级决定 >> 办结<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;1）请先变更完一级证书才能变更二级证书！根据企业名称检验一级造价工程师企业如存在该企业则不允许变更企业。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;2）企业有未办结的注册事项，无法变更企业信息！<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;3）有过期未注销的二级造价工程师不允许变更<br />
                        &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;4）初始注册、执业企业变更未满一年内不允许做执业企业变更。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;3.3、个人信息变更：个人申报 >> 企业确认 >>市级受理 >> 市级审核 >> 市级复核 >> 市级决定 >> 办结<br />
                        4、注销注册：个人申报 >> 企业确认 >> 市级受理 >> 市级审核 >> 市级复核 >> 市级决定 >> 办结<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;4.1、截止前30天关闭续期、变更申请通道，进入有效期截止30天内只允许提交注销业务。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;4.2、证书锁定时（注册中心锁定），只能办理注销注册，其他注册不允许。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;4.3、被动注销：注册中心定期检测，证书过期后长期不办理注销，由建委强制注销。<br />

                    </div>
                    <br />
                    <div class="DivTitleOn" onclick="DivOnOff(this,'DivShare',event);" title="折叠">
                        十三、数据共享规则
                    </div>
                    <div class="DivContent" id="DivShare" style="padding: 20px 20px; line-height: 180%; font-size: 16px; display: none;">
                        1、二级注册建造师：<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;1）建设部及委内其他业务系统：不共享注销、有效期过期数据。<br />
                        &nbsp;&nbsp;&nbsp;&nbsp;2）交易中心：全量共享。<br />
                        2、从业人员证书：不共享注销、离京变更、有效期过期数据。<br />

                    </div>
                    <br />
                    <div class="DivTitleOn" onclick="DivOnOff(this,'DivGongShang',event);" title="折叠">
                        十四、工商信息验证
                    </div>
                    <div class="DivContent" id="DivGongShang" style="padding: 20px 20px; line-height: 180%; font-size: 16px; display: none;">
                        1、二级建造师、二级造价师、从业人员在北京本地企业发起除注销以外的业务申请时，首先验证企业工商经营状态是否为“开业”，不是不允许其发起业务申请。<br />

                        2、外地企业不验证工商信息，但必须在京办理企业进京备案。<br />
                    </div>
                    <div class="DivTitleOn" onclick="DivOnOff(this,'DivGongYiPeiXun',event);" title="折叠">
                        十五、公益教育培训
                    </div>
                    <div class="DivContent" id="DivGongYiPeiXun" style="padding: 20px 20px; line-height: 180%; font-size: 16px; display: none;">
                        1、安全生产三类人员：每年对本企业安管人员开展安全生产教育培训。（目前学时要求没有文件支持）<br />
                        2、特种作业：每年不少于8学时的继续教育学习。<br />
                        3、造价工程师：<br />
                        1）、自职业资格证书批准之日起18个月后首次申请初始注册的，应提供自申请注册之日起算，近1年不少于30学时的继续教育学习证明，其中必修课和选修课各15学时。<br />
                        2）、注销注册或注册证书失效后重新申请初始注册的，自重新申请初始注册之日起算，近4年继续教育学时应不少于120学时，其中必修课（每年度15学时，合计60学时）、选修课（每年度15学时，合计60学时）；<br />
                        自职业资格证书批准之日起至重新申请初始注册之日止不足4年的，应提供每满1个年度不少于30学时的继续教育学习证明，其中必修课和选修课各15学时。同时注册两个专业的，继续教育学时可重复计算。<br />
                        4、二建：尚未应用。
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
