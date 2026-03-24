using System;
using System.Reflection;

namespace Model
{
    /// <summary>
    /// 枚举通用类
    /// </summary>
    public class EnumManager
    {
        /// <summary>
        /// 得到枚举项对应的属性文字说明
        /// </summary>
        /// <typeparam name="T">枚举类型</typeparam>
        /// <param name="enumItem">枚举值</param>
        /// <returns>枚举值的显示名称</returns>
        public static string GetEnumShowName<T>(T enumItem)
        {
            Type objType = enumItem.GetType();
            string s = enumItem.ToString();
            EnumShowName[] nameList = (EnumShowName[])objType.GetField(s).GetCustomAttributes(typeof(EnumShowName), false);
            if (nameList != null && nameList.Length == 1)
            {
                return nameList[0].StringName;
            }
            return s;
        }

        #region 二级造价工程师

        /// <summary>
        ///  申报事项编号
        ///  初始注册 = "01";
        ///  变更注册 = "02";
        ///  延续注册 = "03";
        ///  注销 = "07";
        /// </summary>       
        public static class ZJSApplyTypeCode
        {
            public const string 初始注册 = "01";
            public const string 变更注册 = "02";
            public const string 延续注册 = "03";
            public const string 注销 = "07";
        }

        /// <summary>
        ///  申报事项大类
        /// </summary>       
        public static class ZJSApplyType
        {
            public const string 初始注册 = "初始注册";
            public const string 变更注册 = "变更注册";
            public const string 个人信息变更 = "个人信息变更";
            public const string 执业企业变更 = "执业企业变更";
            public const string 企业信息变更 = "企业信息变更";
            public const string 延续注册 = "延续注册";
            public const string 注销 = "注销";
        }

        /// <summary>
        ///  造价师注册申请状态
        /// </summary>       
        public static class ZJSApplyStatus
        {
            public const string 未申报 = "未申报";
            public const string 待确认 = "待确认";//待企业审核
            public const string 已申报 = "已申报";//企业已审核
            public const string 已驳回 = "已驳回";
            public const string 已受理 = "已受理";//市级受理
            public const string 已审核 = "已审核";//市级审核
            public const string 已复核 = "已复核";//市级复核(已经弃用)
            public const string 已决定 = "已决定";
            //public const string 已批准 = "已批准";//市级批准
            public const string 已公告 = "已公告";
        }


        #endregion

        #region 二级建造师

        /// <summary>
        /// 续期申报开放时间段（天），默认有效期截止前90天开启，前30天关闭。
        /// </summary>
        public static class ContinueTime
        {
            public const int 开始时间 = 90;
            public const int 结束时间 = 30;
        }

        /// <summary>
        /// 网上注册申请，区县未受理情况处理时间段（天）
        /// </summary>
        public static class ApplyDeleteTime
        {
            public const int 时间间隔 = 90;
        }

        /// <summary>
        ///  注册申请状态
        /// </summary>       
        public static class ApplyStatus
        {
            public const string 未申报 = "未申报";
            public const string 待确认 = "待确认";//代企业审核
            public const string 已申报 = "已申报";
            public const string 已驳回 = "已驳回";
            public const string 已受理 = "已受理";
            public const string 区县审查 = "区县审查";
            public const string 已上报 = "已上报";
            public const string 已收件 = "已收件";//已作废
            public const string 已审查 = "已审查";
            public const string 已决定 = "已决定";
            public const string 已公示 = "已公示";
            public const string 已公告 = "已公告";
        }

        /// <summary>
        ///  申报事项大类
        /// </summary>       
        public static class ApplyType
        {
            public const string 初始注册 = "初始注册";
            public const string 变更注册 = "变更注册";
            public const string 个人信息变更 = "个人信息变更";
            public const string 执业企业变更 = "执业企业变更";
            public const string 企业信息变更 = "企业信息变更";
            public const string 延期注册 = "延期注册";
            public const string 增项注册 = "增项注册";
            public const string 重新注册 = "重新注册";
            public const string 遗失补办 = "遗失补办";
            public const string 注销 = "注销";
        }

        /// <summary>
        ///  申报事项编号
        ///  初始注册 = "01";
        ///  变更注册 = "02";
        ///  延期注册 = "03";
        ///  增项注册 = "04";
        ///  重新注册 = "05";
        ///  遗失补办 = "06";
        ///  注销 = "07";
        /// </summary>       
        public static class ApplyTypeCode
        {
            public const string 初始注册 = "01";
            public const string 变更注册 = "02";
            public const string 延期注册 = "03";
            public const string 增项注册 = "04";
            public const string 重新注册 = "05";
            public const string 遗失补办 = "06";
            public const string 注销 = "07";
        }

        /// <summary>
        ///  变更原因编码
        ///  个人信息变更 = "001";
        ///  企业信息变更 = "010";
        ///  执业企业变更 = "101";
        /// </summary>       
        public static class ChangeReasonCode
        {
            public const string 个人信息变更 = "001";
            public const string 企业信息变更 = "010";
            public const string 执业企业变更 = "101";
        }

        /// <summary>
        /// 注册专业
        /// </summary>
        public static class RegisteProfession
        {
            public const string 建筑 = "建筑";
            public const string 市政 = "市政";
            public const string 机电 = "机电";
            public const string 公路 = "公路";
            public const string 水利 = "水利";
            public const string 矿业 = "矿业";
        }

        /// <summary>
        /// 附件数据存放目录
        /// </summary>
        public static class FileDataType
        {
            public const string 证件扫描件 = "zj";
            public const string 学历证书扫描件 = "xuzs";
            public const string 执业资格证书扫描件 = "zyzgzs";//二建执业资格证书
            public const string 造价工程师执业资格证书扫描件 = "zjs_zyzgzs";
            public const string 劳动合同扫描件 = "ldht";
            public const string 申请表扫描件 = "sqb";
            public const string 增项注册告知承诺书 = "zxgzcns";

            public const string 继续教育承诺书扫描件 = "jxjy";
            public const string 继续教育证明扫描件 = "jxjyzm";
            public const string 社保扫描件 = "sb";
            public const string 一寸免冠照片 = "WorkerPhoto";
            public const string 手写签名照 = "SignImg";
            public const string 个人信息变更证明 = "grxxbgzm";
            public const string 解除劳动合同证明 = "jcldht";

            public const string 符合注销注册情形的相关证明 = "zxzm";
            public const string 企业信息变更证明 = "qyxxbgzm";
            public const string 遗失声明扫描件 = "yszm";
            public const string 新设立企业建造师注册承诺书 = "jzscns";
            public const string 申述扫描件 = "shenshu";

            //考务
            public const string 考试报名表扫描件 = "ksbmb";
            public const string 企业营业执照扫描件 = "yyzz";
            public const string 技术职称扫描件 = "jszc";
            public const string 体检合格证明 = "tj";
            public const string 安全教育培训和无违章及不良作业记录证明 = "aqjy";
            public const string 个人健康承诺 = "grjkcn";

            public const string 变更申请表扫描件 = "bgsqb";
            public const string 续期申请表扫描件 = "xqsqb";
            public const string 报考条件证明承诺书 = "bktjzm";

            public const string 安全生产考核证书扫描件 = "aqsckhzs";
            public const string 强制执行申请表 = "enforce";
            public const string 年度安全教育培训记录 = "safetrain";

            public const string 考前安全作业培训承诺书 = "examsafetrain";

            public const string 当前证书注册状态截图 = "certshot";
            public const string 个人承诺 = "userpromise";
            public const string 符合规定情形的相关证明 = "fhgdam";

        }

        /// <summary>
        /// 附件数据存放目录
        /// </summary>
        public static class FileDataTypeName
        {
            public const string 证件扫描件 = "证件扫描件";
            public const string 学历证书扫描件 = "学历证书扫描件";
            //public const string 执业资格证书扫描件 = "执业资格证书扫描件";
            public const string 执业资格证书扫描件 = "执业资格证书或资格考试合格通知书扫描件";//二建执业资格证书
            public const string 造价工程师执业资格证书扫描件 = "执业资格证书或资格考试合格通知书扫描件";
            public const string 劳动合同扫描件 = "劳动合同扫描件";
            public const string 申请表扫描件 = "申请表扫描件";
            public const string 增项注册告知承诺书 = "增项注册告知承诺书";

            public const string 继续教育承诺书扫描件 = "继续教育承诺书扫描件";
            public const string 继续教育证明扫描件 = "继续教育证明扫描件";
            public const string 社保扫描件 = "社保扫描件";
            public const string 一寸免冠照片 = "一寸免冠照片";
            public const string 手写签名照 = "手写签名照";
            public const string 个人信息变更证明 = "个人信息变更证明";
            public const string 解除劳动合同证明 = "解除劳动合同证明";

            public const string 符合注销注册情形的相关证明 = "符合注销注册情形的相关证明";
            public const string 企业信息变更证明 = "企业信息变更证明";
            public const string 遗失声明扫描件 = "遗失声明扫描件";
            public const string 新设立企业建造师注册承诺书 = "新设立企业建造师注册承诺书";
            public const string 申述扫描件 = "申述扫描件";

            //考务
            public const string 考试报名表扫描件 = "考试报名表扫描件";
            public const string 企业营业执照扫描件 = "企业营业执照扫描件";
            public const string 技术职称扫描件 = "技术职称扫描件";
            public const string 体检合格证明 = "体检合格证明";
            public const string 安全教育培训和无违章及不良作业记录证明 = "安全教育培训和无违章及不良作业记录证明";
            public const string 个人健康承诺 = "个人健康承诺";

            public const string 变更申请表扫描件 = "变更申请表扫描件";
            public const string 续期申请表扫描件 = "续期申请表扫描件";
            public const string 报考条件证明承诺书 = "报考条件证明承诺书";

            public const string 安全生产考核证书扫描件 = "安全生产考核证书扫描件";
            public const string 强制执行申请表 = "强制执行申请表";
            public const string 年度安全教育培训记录 = "年度安全教育培训记录";
            public const string 考前安全作业培训承诺书 = "考前安全作业培训承诺书";

            public const string 当前证书注册状态截图 = "当前证书注册状态截图";
            public const string 个人承诺 = "个人承诺";
            public const string 符合规定情形的相关证明 = "符合规定情形的相关证明";

        }


        /// <summary>
        /// 获取枚举类属性的属性名称
        /// </summary>
        /// <param name="typeCls">枚举类型（例：typeof("枚举名称")）</param>
        /// <param name="value">枚举值</param>
        /// <returns>枚举属性名称</returns>
        public static string GetShowName(Type typeCls, object value)
        {

            FieldInfo[] fdInfo = typeCls.GetFields(); //反射类的属性

            foreach (FieldInfo pi in fdInfo)
            {
                object value1 = pi.GetValue(null);//用pi.GetValue获得值  
                string name = pi.Name;//获得属性的名字,后面就可以根据名字判断来进行些自己想要的操作 
                if (value1.Equals(value))
                {
                    return name;
                }
            }

            return "";
        }
        /// <summary>
        /// 上报批次号枚举
        /// </summary>
        public static class CityEnum
        {
            public const string 东城区 = "01";
            public const string 西城区 = "02";
            public const string 崇文区 = "03";
            public const string 宣武区 = "04";
            public const string 朝阳区 = "05";
            public const string 丰台区 = "06";
            public const string 石景山区 = "07";
            public const string 海淀区 = "08";
            public const string 门头沟区 = "09";
            public const string 房山区 = "11";
            public const string 通州区 = "12";
            public const string 顺义区 = "13";
            public const string 昌平区 = "14";
            public const string 大兴区 = "15";
            public const string 怀柔区 = "16";
            public const string 平谷区 = "17";
            public const string 亦庄 = "18";
            public const string 密云区 = "28";
            public const string 延庆区 = "29";
        }

        #endregion 执业人员管理

        #region 人员考务系统

        /// <summary>
        /// 从业电子证书创建进程环节
        /// </summary>
        public static class EleCertDoStep
        {
            /// <summary>
            /// 校验
            /// </summary>
            public const string CertCheck = "校验";
            /// <summary>
            /// 赋码
            /// </summary>
            public const string GetCode = "赋码";
            /// <summary>
            /// 归集
            /// </summary>
            public const string UpCertUrl = "归集";
            /// <summary>
            /// 创建
            /// </summary>
            public const string CreateCert = "创建";
            /// <summary>
            /// 签章
            /// </summary>
            public const string IssueCA = "签章";
            /// <summary>
            /// 下载
            /// </summary>
            public const string Download = "下载";
            /// <summary>
            /// 取回
            /// </summary>
            public const string ReturnCert = "取回";
            /// <summary>
            /// 挂起
            /// </summary>
            public const string SetTemp = "挂起";


        }

        /// <summary>
        /// 证书变更状态
        /// </summary>
        public static class CertificateChangeStatus
        {
            /// <summary>
            /// 填报中
            /// </summary>
            public const string NewSave = "填报中";
            /// <summary>
            /// 待单位确认(现单位确认)
            /// </summary>
            public const string WaitUnitCheck = "待单位确认";
            /// <summary>
            /// 已申请
            /// </summary>
            public const string Applyed = "已申请";
            /// <summary>
            /// 已受理
            /// </summary>
            public const string Accepted = "已受理";
            /// <summary>
            /// 已审核
            /// </summary>
            public const string Checked = "已审核";
            /// <summary>
            /// 已决定
            /// </summary>
            public const string Decided = "已决定";
            /// <summary>
            /// 已告知
            /// </summary>
            public const string Noticed = "已告知";
            /// <summary>
            /// 退回修改
            /// </summary>
            public const string SendBack = "退回修改";
        }

        /// <summary>
        /// 证书续期状态
        /// </summary>
        public static class CertificateContinueStatus
        {
            /// <summary>
            /// 填报中
            /// </summary>
            public const string NewSave = "填报中";
            /// <summary>
            /// 待单位确认(现单位确认)
            /// </summary>
            public const string WaitUnitCheck = "待单位确认";
            /// <summary>
            /// 已申请
            /// </summary>
            public const string Applyed = "已申请";
            /// <summary>
            /// 已初审
            /// </summary>
            public const string Accepted = "已初审";
            /// <summary>
            /// 已审核
            /// </summary>
            public const string Checked = "已审核";
            /// <summary>
            /// 已决定
            /// </summary>
            public const string Decided = "已决定";
            /// <summary>
            /// 退回修改
            /// </summary>
            public const string SendBack = "退回修改";
        }

        /// <summary>
        /// 证书合并申请状态
        /// </summary>
        public static class CertificateMergeStatus
        {

            /// <summary>
            /// 待单位确认
            /// </summary>
            public const string WaitUnitCheck = "待单位确认";
            /// <summary>
            /// 已申请
            /// </summary>
            public const string Applyed = "已申请";
            /// <summary>
            /// 已决定
            /// </summary>
            public const string Decided = "已决定";
            /// <summary>
            /// 退回修改
            /// </summary>
            public const string SendBack = "退回修改";
        }

        /// <summary>
        /// 证书续期审核结果
        /// </summary>
        public static class CertificateContinueCheckResult
        {
            /// <summary>
            /// 退回修改
            /// </summary>
            public const string SendBack = "退回修改";
            /// <summary>
            /// 不予受理
            /// </summary>
            public const string NoGet = "不予受理";
            /// <summary>
            /// 初审通过
            /// </summary>
            public const string GetPass = "初审通过";
            /// <summary>
            /// 审核通过
            /// </summary>
            public const string CheckPass = "审核通过";
            /// <summary>
            /// 审核不通过
            /// </summary>
            public const string CheckNoPass = "审核不通过";
            /// <summary>
            /// 决定通过
            /// </summary>
            public const string DecidPass = "决定通过";
            /// <summary>
            /// 决定不通过
            /// </summary>
            public const string DecidNoPass = "决定不通过";
        }

        /// <summary>
        /// 证书进京申请状态
        /// </summary>
        public static class CertificateEnterStatus
        {
            /// <summary>
            /// 填报中
            /// </summary>
            public const string NewSave = "填报中";
            /// <summary>
            /// 待单位确认(现单位确认)
            /// </summary>
            public const string WaitUnitCheck = "待单位确认";
            /// <summary>
            /// 已申请
            /// </summary>
            public const string Applyed = "已申请";
            /// <summary>
            /// 已受理
            /// </summary>
            public const string Accepted = "已受理";
            /// <summary>
            /// 已审核
            /// </summary>
            public const string Checked = "已审核";
            /// <summary>
            /// 已编号
            /// </summary>
            public const string Decided = "已编号";
            ///// <summary>
            ///// 已决定
            ///// </summary>
            //public const string Decided = "已决定";
            /// <summary>
            /// 退回修改
            /// </summary>
            public const string SendBack = "退回修改";
        }

        /// <summary>
        /// 外阜证书申报状态
        /// </summary>
        public static class ForeignCertificateApplyStatus
        {
            /// <summary>
            /// 未申报
            /// </summary>
            public const string UnApply = "未申报";
            /// <summary>
            /// 已申报
            /// </summary>
            public const string Applyed = "已申报";
            /// <summary>
            /// 已审核
            /// </summary>
            public const string Checked = "已审核";
            /// <summary>
            /// 退回修改
            /// </summary>
            public const string ReturnEdit = "退回修改";
        }

        /// <summary>
        /// 证书更新状态
        /// </summary>
        public static class CertificateUpdateType
        {
            /// <summary>
            /// 待审批
            /// </summary>
            public const string WaitCheck = "待审批";
            /// <summary>
            /// 进京待审批
            /// </summary>
            public const string EnterWaitCheck = "进京待审批";
            /// <summary>
            /// 首次
            /// </summary>
            public const string first = "首次";
            /// <summary>
            /// 续期
            /// </summary>
            public const string Continue = "续期";
            /// <summary>
            /// 进京变更
            /// </summary>
            public const string InBeiJing = "进京变更";
            /// <summary>
            /// 离京变更
            /// </summary>
            public const string OutBeiJing = "离京变更";
            /// <summary>
            /// 京内变更
            /// </summary>
            public const string ChangeInBeiJing = "京内变更";
            /// <summary>
            /// 注销
            /// </summary>
            public const string Logout = "注销";
            /// <summary>
            /// 补办
            /// </summary>
            public const string Patch = "补办";

        }

        /// <summary>
        /// 证书暂扣申报状态
        /// </summary>
        public static class CertificatePauseStatus
        {
            /// <summary>
            /// 申请暂扣
            /// </summary>
            public const string ApplyPause = "申请暂扣";
            /// <summary>
            /// 已暂扣
            /// </summary>
            public const string Pauseing = "已暂扣";
            /// <summary>
            /// 申请返还
            /// </summary>
            public const string ApplyReturn = "申请返还";
            /// <summary>
            /// 已返还
            /// </summary>
            public const string Returned = "已返还";

        }

        /// <summary>
        /// 机构类型
        /// </summary>
        public static class UnitType
        {
            /// <summary>
            /// 企业
            /// </summary>
            public const string qy = "企业";
            /// <summary>
            /// 培训点
            /// </summary>
            public const string pxd = "培训点";
            /// <summary>
            /// 区县建委
            /// </summary>
            public const string qxjw = "区县建委";
            /// <summary>
            /// 服务大厅
            /// </summary>
            public const string fwdt = "服务大厅";
            /// <summary>
            /// 注册中心
            /// </summary>
            public const string zczx = "注册中心";
            /// <summary>
            /// 工作站
            /// </summary>
            public const string gzz = "工作站";
        }

        /// <summary>
        /// 考试报名状态
        /// </summary>
        public static class SignUpStatus
        {
            /// <summary>
            /// 未提交
            /// </summary>
            public const string SaveSignUp = "未提交";//个人未提交
            /// <summary>
            /// 待初审
            /// </summary>
            public const string NewSignUp = "待初审";//待单位确认
            /// <summary>
            /// 已初审(单位审核)
            /// </summary>
            public const string FirstChecked = "已初审";//即单位已确认
            /// <summary>
            /// 已受理
            /// </summary>
            public const string Accept = "已受理";//建委已受理
            /// <summary>
            /// 已审核
            /// </summary>
            public const string Checked = "已审核";//建委审核
            /// <summary>
            /// 待缴费
            /// </summary>
            public const string PayNoticed = "待缴费";//已作废，忽略
            /// <summary>
            /// 已缴费
            /// </summary>
            public const string PayConfirmed = "已缴费";//意义已经改变，相当于审核确认(即决定)
            /// <summary>
            /// 退回修改
            /// </summary>
            public const string ReturnEdit = "退回修改";
        }

        /// <summary>
        /// 考点分配状态
        /// </summary>
        public static class ExamPlaceAllotStatus
        {
            /// <summary>
            /// 已分配考点
            /// </summary>
            public const string AllotPlaced = "已分配考点";
            /// <summary>
            /// 已分配考生
            /// </summary>
            public const string AllotExamered = "已分配考生";
        }

        /// <summary>
        /// 考场分配状态
        /// </summary>
        public static class ExamRoomAllotStatus
        {
            /// <summary>
            /// 已分配考点
            /// </summary>
            public const string AllotRoomed = "已分配考点";
            /// <summary>
            /// 已分配考生
            /// </summary>
            public const string AllotExamered = "已分配考生";
        }

        /// <summary>
        /// 考点状态
        /// </summary>
        public static class ExamPlaceStatus
        {
            /// <summary>
            /// 未删除
            /// </summary>
            public const string UnDelete = "未删除";
            /// <summary>
            /// 已删除
            /// </summary>
            public const string Deleted = "已删除";
        }

        /// <summary>
        /// 考试成绩状态
        /// </summary>
        public static class ExamResultStatus
        {
            /// <summary>
            /// 成绩未判定
            /// </summary>
            public const string BeforeResult = "成绩未判定";
            /// <summary>
            /// 成绩未公告
            /// </summary>
            public const string UnPublished = "成绩未公告";
            /// <summary>
            /// 成绩已公告
            /// </summary>
            public const string Published = "成绩已公告";
        }

        /// <summary>
        /// 考试成绩结果
        /// </summary>
        public static class ExamResult
        {
            /// <summary>
            /// 不合格
            /// </summary>
            public const string UnPass = "不合格";
            /// <summary>
            /// 合格
            /// </summary>
            public const string Pass = "合格";
        }

        /// <summary>
        /// 考试计划状态
        /// </summary>
        public static class ExamPlanStatus
        {
            /// <summary>
            /// 正常
            /// </summary>
            public const string BeginSignUp = "正常";
        }

        /// <summary>
        /// 考试计划公开类型
        /// </summary>
        public static class ExamPlanPublishType
        {
            /// <summary>
            /// 完全公开
            /// </summary>
            public const string Publish = "完全公开";

            /// <summary>
            /// 完全公开培训点受限
            /// </summary>
            public const string PublishAndTrainUnitLimit = "完全公开培训点受限";

            /// <summary>
            /// 部分公开
            /// </summary>
            public const string UnPublish = "部分公开";
        }

        /// <summary>
        /// 特殊的考试计划ID
        /// </summary>
        public static class CertificateExamPlanID
        {
            /// <summary>
            /// 历史证书数据：-100
            /// </summary>
            public const int CertificateHistory = -100;

            /// <summary>
            /// 证书进京：-200
            /// </summary>
            public const int CertificateEnter = -200;

            /// <summary>
            /// 证书补登记：-300
            /// </summary>
            public const int CertificateAdd = -300;

            /// <summary>
            /// A本增发：-400
            /// </summary>
            public const int CertificateMore = -400;

            /// <summary>
            /// 合并生成：-500
            /// </summary>
            public const int CertificateMerge = -500;
        }

        /// <summary>
        /// 社保比对类型
        /// </summary>
        public static class ShebaoCheckType
        {
            /// <summary>
            /// 考试报名
            /// </summary>
            public const string Exam = "考试报名";

            /// <summary>
            /// 证书变更
            /// </summary>
            public const string Change = "证书变更";

            /// <summary>
            /// 证书续期
            /// </summary>
            public const string Continue = "证书续期";

            /// <summary>
            /// 证书进京
            /// </summary>
            public const string Enter = "证书进京";
            /// <summary>
            /// 证书补登记
            /// </summary>
            public const string Additional = "证书补登记";
        }

        /// <summary>
        /// 企业资质类别
        /// </summary>
        public static class UnitZZLB
        {
            /// <summary>
            /// 建筑施工企业（含拆装企业）
            /// </summary>
            public const string qy_jzsg = "建筑施工企业";
            /// <summary>
            /// 中央在京
            /// </summary>
            public const string qy_zyzj = "中央在京";
            /// <summary>
            /// 外地进京
            /// </summary>
            public const string qy_wdjj = "外地进京";
            /// <summary>
            /// 起重机械租赁企业
            /// </summary>
            public const string qy_qzjx = "起重机械租赁企业";
            /// <summary>
            /// 设计施工一体化
            /// </summary>
            public const string qy_sjsg = "设计施工一体化";
        }

        /// <summary>
        /// 附件类型编号
        /// </summary>
        public static class FileTypeCode
        {
            /// <summary>
            /// 电子证书：1
            /// </summary>
            public const int CA = 1;

            /// <summary>
            /// 一寸照片：2
            /// </summary>
            public const int FacePhoto = 2;
        }

        /// <summary>
        /// A本增发申请状态
        /// </summary>
        public static class CertificateMore
        {
            /// <summary>
            /// 填报中
            /// </summary>
            public const string NewSave = "填报中";
            /// <summary>
            /// 待单位确认(原、现单位确认)
            /// </summary>
            public const string WaitUnitCheck = "待单位确认";
            /// <summary>
            /// 已申请
            /// </summary>
            public const string Applyed = "已申请";
            /// <summary>
            /// 已审核
            /// </summary>
            public const string Checked = "已审核";
            /// <summary>
            /// 已决定
            /// </summary>
            public const string Decided = "已决定";
            /// <summary>
            /// 退回修改
            /// </summary>
            public const string SendBack = "退回修改";
        }

        /// <summary>
        /// A本增发申请审查结果
        /// </summary>
        public static class CertificateMoreCheckResult
        {
            /// <summary>
            /// 通过
            /// </summary>
            public const string CheckPass = "通过";

            /// <summary>
            /// 不通过
            /// </summary>
            public const string CheckNoPass = "不通过";
        }

        /// <summary>
        /// 系统固定提示
        /// </summary>
        public static class Message
        {
            ///// <summary>
            ///// 二建证书锁定提示(移动到UIHelp.aspx)
            ///// </summary>
            //public const string ErJianCertLockMessage = string.Format("您的证书注册状态异常，已被锁定，请联系注册中心核实相关情况！<a target=\"_blank\" href=\"http://120.52.185.14/Register/NewsView.aspx?o={0}\">（关于注册状态异常标记的解释及处理流程）</a>", Utility.Cryptography.Encrypt("1cf44934-db2b-4de2-9ab5-a6650417824a"));
        }

        /// <summary>
        /// 培训计划发布状态
        /// </summary>
        public static class PackageStatus
        {
            /// <summary>
            /// 已发布
            /// </summary>
            public const string Published = "已发布";
            /// <summary>
            /// 未发布
            /// </summary>
            public const string UnPublish = "未发布";
        }

        /// <summary>
        /// 学习计划加入方式
        /// </summary>
        public static class StudyPlanAddType
        {
            /// <summary>
            /// 个人添加
            /// </summary>
            public const string user = "个人添加";
            /// <summary>
            /// 系统指派
            /// </summary>
            public const string system = "系统指派";
        }
        /// <summary>
        /// 公益培训测试状态
        /// </summary>
        public static class StudyTestStatus
        {
            /// <summary>
            /// 未达标
            /// </summary>
            public const int NoPass = 0;
            /// <summary>
            /// 已达标
            /// </summary>
            public const int Passed = 1;
        }

        #endregion

        /// <summary>
        ///  监管反馈状态名称
        /// </summary>       
        public static class CheckFeedStatus
        {
            public const string 未发布 = "未发布";
            public const string 待反馈 = "待反馈";
            public const string 已驳回 = "已驳回";
            public const string 待审查 = "待审查";
            public const string 待复审 = "待复审";
            public const string 待决定 = "待决定";
            public const string 已决定 = "已决定";
        }

        /// <summary>
        ///  监管反馈状态编码
        /// </summary>       
        public static class CheckFeedStatusCode
        {
            public const int 未发布 = 0;
            public const int 待反馈 = 1;
            public const int 已驳回 = 2;
            public const int 待审查 = 3;
            public const int 待复审 = 4;
            public const int 待决定 = 6;
            public const int 已决定 = 7;
        }

        /// <summary>
        /// 政务行政事项申请服务ID
        /// </summary>
        public static class ServiceCodeId
        {
            public const string 北京市_二级建造师执业资格认定_公路工程专业_初始注册 = "07a04ac2-7967-4808-9665-9892a5d7b01c";
            public const string 北京市_二级建造师执业资格认定_公路工程专业_延续注册 = "587381e1-c5c6-495c-b396-f49f7f5ddacb";
            public const string 北京市_二级建造师执业资格认定_公路工程专业_增项注册 = "dcb79fe5-8d11-49af-97c4-b1553fac7448";
            public const string 北京市_二级建造师执业资格认定_公路工程专业_重新注册 = "e3ebef86-46c0-4788-9b57-0440180ab1b7";
            public const string 北京市_二级建造师执业资格认定_公路工程专业_注销注册 = "e1f61b22-51cc-4b70-a11a-cb484760fcee";
            public const string 北京市_二级建造师执业资格认定_机电工程专业_初始注册 = "40fcee1d-262c-4ca5-93e8-ec5a58aa0848";
            public const string 北京市_二级建造师执业资格认定_机电工程专业_延续注册 = "5ca5afc8-858b-46c3-8baf-7641c49c2b8e";
            public const string 北京市_二级建造师执业资格认定_机电工程专业_增项注册 = "cd0a9a69-f125-437f-9060-aea1d6017733";
            public const string 北京市_二级建造师执业资格认定_机电工程专业_重新注册 = "89558501-4758-4973-b08c-df20d08c46c0";
            public const string 北京市_二级建造师执业资格认定_机电工程专业_注销注册 = "42104366-07fb-4ace-98cd-6856555d1b55";
            public const string 北京市_二级建造师执业资格认定_建筑工程专业_初始注册 = "ef63e0ab-8db9-4d0e-9e18-a7267ca61052";
            public const string 北京市_二级建造师执业资格认定_建筑工程专业_延续注册 = "382499d1-9024-4906-8b47-11720d9bc067";
            public const string 北京市_二级建造师执业资格认定_建筑工程专业_增项注册 = "16253e67-3a94-4795-93ba-325af56c1940";
            public const string 北京市_二级建造师执业资格认定_建筑工程专业_重新注册 = "341af6bf-c6a5-430e-b9bf-d65ee49ae20e";
            public const string 北京市_二级建造师执业资格认定_建筑工程专业_注销注册 = "1d5f94c0-8972-4c95-8215-a7da9ef3d9b3";
            public const string 北京市_二级建造师执业资格认定_矿业工程专业_初始注册 = "ee2d8275-18e0-4d6b-937a-75e50b6256bf";
            public const string 北京市_二级建造师执业资格认定_矿业工程专业_延续注册 = "688f181f-1543-41cf-9b46-430f6bde13be";
            public const string 北京市_二级建造师执业资格认定_矿业工程专业_增项注册 = "99e6a5e1-02e3-400a-8bd4-5f405ed9d7d8";
            public const string 北京市_二级建造师执业资格认定_矿业工程专业_重新注册 = "6b547c76-814e-4779-947a-a3502ef1e041";
            public const string 北京市_二级建造师执业资格认定_矿业工程专业_注销注册 = "b93f9a21-9c25-41eb-ab82-8027ada99b9e";
            public const string 北京市_二级建造师执业资格认定_市政公用工程专业_初始注册 = "8f7e77f0-e1d6-4f9c-8da8-992ac9b620a8";
            public const string 北京市_二级建造师执业资格认定_市政公用工程专业_延续注册 = "e78b1963-74f2-4e55-9aed-4ebc54213a57";
            public const string 北京市_二级建造师执业资格认定_市政公用工程专业_增项注册 = "fad689f1-7c4f-4da9-a985-36787ccef4ec";
            public const string 北京市_二级建造师执业资格认定_市政公用工程专业_重新注册 = "8800efcd-5c06-439a-970e-ed59093d9a07";
            public const string 北京市_二级建造师执业资格认定_市政公用工程专业_注销注册 = "8e0b705f-b92c-478b-a871-4cfe9afe17df";
            public const string 北京市_二级建造师执业资格认定_水利水电工程专业_初始注册 = "50f01834-5367-454d-8b8f-09d284651d05";
            public const string 北京市_二级建造师执业资格认定_水利水电工程专业_延续注册 = "a678685a-76ba-44bb-b483-4eb6b05cf6e8";
            public const string 北京市_二级建造师执业资格认定_水利水电工程专业_增项注册 = "0f857f34-647d-403e-be3d-46e4afc0d3ec";
            public const string 北京市_二级建造师执业资格认定_水利水电工程专业_重新注册 = "419f326b-1a3e-4f24-8a36-3c29cb9700bb";
            public const string 北京市_二级建造师执业资格认定_水利水电工程专业_注销注册 = "5a1b531f-0894-4be3-a96f-b2c6d5ff06e1";
            public const string 朝阳区_二级建造师执业资格认定_公路工程专业_变更注册 = "70a27293-4dfb-43bc-be9d-0590b467fa6f";
            public const string 大兴区_二级建造师执业资格认定_公路工程专业_变更注册 = "f616cdf1-3b91-435b-806f-8af2a6be5fb5";
            public const string 昌平区_二级建造师执业资格认定_公路工程专业_变更注册 = "911f800b-4020-4556-a5c0-5c1d05d8333a";
            public const string 北京市_二级建造师执业资格认定_公路工程专业_变更注册 = "0eca6af0-229f-46e7-8267-72a46eae2cdd";
            public const string 平谷区_二级建造师执业资格认定_公路工程专业_变更注册 = "c8e81a4d-64ae-49b0-8723-5f7d13d8f632";
            public const string 东城区_二级建造师执业资格认定_公路工程专业_变更注册 = "18936853-cf5c-4e22-8433-10f1b8c3db06";
            public const string 通州区_二级建造师执业资格认定_公路工程专业_变更注册 = "5ff27b41-9427-46c0-a45f-35f9a0d4f3df";
            public const string 经济技术开发区_二级建造师执业资格认定_公路工程专业_变更注册 = "1be6f308-dd5e-4f1c-a8b0-5e90c3439247";
            public const string 顺义区_二级建造师执业资格认定_公路工程专业_变更注册 = "2ec3db85-e182-4dd0-863f-11beab1e4288";
            public const string 石景山区_二级建造师执业资格认定_公路工程专业_变更注册 = "7d1fd0af-2955-4e68-8858-234748ca4faf";
            public const string 海淀区_二级建造师执业资格认定_公路工程专业_变更注册 = "96589936-fda1-4cda-9cfa-ae3d6cf4b44d";
            public const string 延庆区_二级建造师执业资格认定_公路工程专业_变更注册 = "48fdcf96-ee1b-43bb-8800-0d2615de2379";
            public const string 怀柔区_二级建造师执业资格认定_公路工程专业_变更注册 = "6644b043-d1a5-41d0-a8a3-b89fe4bbf0dd";
            public const string 西城区_二级建造师执业资格认定_公路工程专业_变更注册 = "a07dc1e9-8b24-4ad0-a140-8039d9132c0d";
            public const string 房山区_二级建造师执业资格认定_公路工程专业_变更注册 = "a2785eb3-2947-4f92-bab7-67f9cf8ef286";
            public const string 密云区_二级建造师执业资格认定_公路工程专业_变更注册 = "869dad3c-7322-4c8e-8eeb-df1a91e3f06a";
            public const string 丰台区_二级建造师执业资格认定_公路工程专业_变更注册 = "b9b7f39c-d02a-467d-8765-b30bcf2ddfff";
            public const string 门头沟区_二级建造师执业资格认定_公路工程专业_变更注册 = "3536508f-2e5a-4207-8c47-093279d4f282";
            public const string 顺义区_二级建造师执业资格认定_机电工程专业_变更注册 = "b8402977-136a-4b79-829c-ffe09cf307e8";
            public const string 平谷区_二级建造师执业资格认定_机电工程专业_变更注册 = "baedd1c7-1ae0-4143-b54e-6df4ff186ad0";
            public const string 密云区_二级建造师执业资格认定_机电工程专业_变更注册 = "bddd52b1-6a21-4910-ac22-d9580a55499f";
            public const string 通州区_二级建造师执业资格认定_机电工程专业_变更注册 = "fdac9f8f-0934-4666-b1c0-da8255aa7af4";
            public const string 海淀区_二级建造师执业资格认定_机电工程专业_变更注册 = "46099278-fd39-4404-8a73-471e07794009";
            public const string 门头沟区_二级建造师执业资格认定_机电工程专业_变更注册 = "eb960cd7-6230-4a82-adb6-a31df872c222";
            public const string 西城区_二级建造师执业资格认定_机电工程专业_变更注册 = "b0ae7513-522f-413c-ac7a-b22a339679c2";
            public const string 延庆区_二级建造师执业资格认定_机电工程专业_变更注册 = "c8d7eb43-69f9-4c18-8492-7492a4019dc6";
            public const string 房山区_二级建造师执业资格认定_机电工程专业_变更注册 = "ad0b7b42-a108-43d7-8ddf-4be7cf3a7f1c";
            public const string 石景山区_二级建造师执业资格认定_机电工程专业_变更注册 = "76e620fc-5b21-4011-9f16-52277ed1db2a";
            public const string 丰台区_二级建造师执业资格认定_机电工程专业_变更注册 = "3bad603d-f8f6-40c1-ba2c-ef2d92b821a3";
            public const string 大兴区_二级建造师执业资格认定_机电工程专业_变更注册 = "f6ddde59-5afd-41d0-83a0-d7b96d7d4b3f";
            public const string 经济技术开发区_二级建造师执业资格认定_机电工程专业_变更注册 = "090f54ca-655b-482e-a670-df5741961dd0";
            public const string 怀柔区_二级建造师执业资格认定_机电工程专业_变更注册 = "8d8c0b6e-1bb5-4a2e-a378-77ceb67d2524";
            public const string 北京市_二级建造师执业资格认定_机电工程专业_变更注册 = "6f762f94-d66e-479b-b890-da8156ab8bdf";
            public const string 东城区_二级建造师执业资格认定_机电工程专业_变更注册 = "8b336b4f-a5e4-47bf-ac06-9d7e1781a167";
            public const string 朝阳区_二级建造师执业资格认定_机电工程专业_变更注册 = "d86b18a3-bc6e-4944-934f-b420027e6983";
            public const string 昌平区_二级建造师执业资格认定_机电工程专业_变更注册 = "d8bc7d2f-9463-40d0-8299-f03127b7f90c";
            public const string 西城区_二级建造师执业资格认定_建筑工程专业_变更注册 = "da4aaae3-4061-43dc-9b03-8587aebaaace";
            public const string 经济技术开发区_二级建造师执业资格认定_建筑工程专业_变更注册 = "57465f6a-58b0-489f-bde1-ccc8bbb93923";
            public const string 昌平区_二级建造师执业资格认定_建筑工程专业_变更注册 = "79c9aabb-0739-4893-b309-c8bb98da5920";
            public const string 朝阳区_二级建造师执业资格认定_建筑工程专业_变更注册 = "acd25f31-6ff4-4440-9a8f-80d68c9b8d93";
            public const string 平谷区_二级建造师执业资格认定_建筑工程专业_变更注册 = "192a196d-4cd7-4296-8f1c-47c504ff4118";
            public const string 北京市_二级建造师执业资格认定_建筑工程专业_变更注册 = "2f31a4d7-030b-44c3-a3e8-a1dc842840fd";
            public const string 石景山区_二级建造师执业资格认定_建筑工程专业_变更注册 = "488857b4-ecd1-4814-86e9-e7882c4f5896";
            public const string 东城区_二级建造师执业资格认定_建筑工程专业_变更注册 = "b14fd642-f228-4bc0-8436-385c7c5511ee";
            public const string 大兴区_二级建造师执业资格认定_建筑工程专业_变更注册 = "e3e75b5e-a148-485a-a9af-406e53a8aeec";
            public const string 丰台区_二级建造师执业资格认定_建筑工程专业_变更注册 = "454715bd-a4a4-483d-9de7-75c31a8bdde4";
            public const string 延庆区_二级建造师执业资格认定_建筑工程专业_变更注册 = "ec4298cf-c5af-4530-9c7d-80c45bb58221";
            public const string 门头沟区_二级建造师执业资格认定_建筑工程专业_变更注册 = "99d5ff7f-8054-4eda-9d07-1627dedd4c24";
            public const string 怀柔区_二级建造师执业资格认定_建筑工程专业_变更注册 = "22980b2b-649a-4689-ac61-fb1aceab0827";
            public const string 房山区_二级建造师执业资格认定_建筑工程专业_变更注册 = "861db9e8-8423-4e5d-aca1-f5cf98bf7f9b";
            public const string 海淀区_二级建造师执业资格认定_建筑工程专业_变更注册 = "6e81811c-3bec-4fc3-96b7-a6d91cac5327";
            public const string 通州区_二级建造师执业资格认定_建筑工程专业_变更注册 = "24f7d6cc-34ea-4478-9df6-0ffc4f6f6e6c";
            public const string 密云区_二级建造师执业资格认定_建筑工程专业_变更注册 = "4db0fe18-27ef-4411-911a-ca7f81783086";
            public const string 顺义区_二级建造师执业资格认定_建筑工程专业_变更注册 = "26a129e0-12da-4bc5-8dd4-f45253874c4e";
            public const string 平谷区_二级建造师执业资格认定_矿业工程专业_变更注册 = "022d3dff-00b0-41a7-9ef1-123b3875aca4";
            public const string 密云区_二级建造师执业资格认定_矿业工程专业_变更注册 = "07a4b2e3-c760-43d4-b985-6d616ca66321";
            public const string 通州区_二级建造师执业资格认定_矿业工程专业_变更注册 = "6cfe2004-9cca-4e87-b8ac-8589967461c5";
            public const string 房山区_二级建造师执业资格认定_矿业工程专业_变更注册 = "4a579b43-0512-4c0d-bb1c-0cb4fab81411";
            public const string 昌平区_二级建造师执业资格认定_矿业工程专业_变更注册 = "80a119ee-461a-4a57-a3eb-58cb408dde72";
            public const string 海淀区_二级建造师执业资格认定_矿业工程专业_变更注册 = "e90e1ca4-5e9c-4ee8-ba99-6b83f712ddae";
            public const string 延庆区_二级建造师执业资格认定_矿业工程专业_变更注册 = "68a9bfc3-e14d-4e8c-8c09-86d75e54c0e3";
            public const string 石景山区_二级建造师执业资格认定_矿业工程专业_变更注册 = "fa1b4ea6-7a67-436b-bda4-4c46bf0b1b5f";
            public const string 经济技术开发区_二级建造师执业资格认定_矿业工程专业_变更注册 = "1c82c527-817a-4567-9dcf-72b574b801e4";
            public const string 丰台区_二级建造师执业资格认定_矿业工程专业_变更注册 = "f15c3811-8f11-4e11-9d29-8070e613dac4";
            public const string 顺义区_二级建造师执业资格认定_矿业工程专业_变更注册 = "5aceb126-1c90-4af5-9ff5-2dbe8873df8e";
            public const string 大兴区_二级建造师执业资格认定_矿业工程专业_变更注册 = "5c3cbdab-a73b-4ed9-b6c3-36379693a55e";
            public const string 东城区_二级建造师执业资格认定_矿业工程专业_变更注册 = "0a59686c-d732-4d3f-a98d-76accbe1f020";
            public const string 西城区_二级建造师执业资格认定_矿业工程专业_变更注册 = "c05ba813-cda0-4bdb-8236-40656df0e63c";
            public const string 北京市_二级建造师执业资格认定_矿业工程专业_变更注册 = "d8fc1705-5a99-40ed-a9e7-e2428f02846a";
            public const string 门头沟区_二级建造师执业资格认定_矿业工程专业_变更注册 = "bf24f6be-ebdb-465a-b7f2-97020028186a";
            public const string 怀柔区_二级建造师执业资格认定_矿业工程专业_变更注册 = "f0bba328-b313-4f38-8503-93ec7418d1f7";
            public const string 朝阳区_二级建造师执业资格认定_矿业工程专业_变更注册 = "8e0a52c6-03d0-4c7b-bfb5-08c4110daf5a";
            public const string 经济技术开发区_二级建造师执业资格认定_市政公用工程专业_变更注册 = "c68c24d3-f1ea-4272-8300-c829e87085ee";
            public const string 平谷区_二级建造师执业资格认定_市政公用工程专业_变更注册 = "917f6c5e-b3aa-45a2-b7ab-e8a2b66b9ce9";
            public const string 石景山区_二级建造师执业资格认定_市政公用工程专业_变更注册 = "0c4679f9-36a0-41b3-ab42-b8942754bfa5";
            public const string 密云区_二级建造师执业资格认定_市政公用工程专业_变更注册 = "e017e231-1d1f-4a0d-bbd9-7602b3053604";
            public const string 海淀区_二级建造师执业资格认定_市政公用工程专业_变更注册 = "ad58e59d-c9a0-4949-8cad-2e545e6f9742";
            public const string 门头沟区_二级建造师执业资格认定_市政公用工程专业_变更注册 = "2a68d694-ad03-4a3c-a7fe-7705c397d17a";
            public const string 顺义区_二级建造师执业资格认定_市政公用工程专业_变更注册 = "7c521f70-3872-4a90-9dcc-f128a6e42713";
            public const string 北京市_二级建造师执业资格认定_市政公用工程专业_变更注册 = "2e6b8234-e85c-41b1-903d-9244db1c05df";
            public const string 怀柔区_二级建造师执业资格认定_市政公用工程专业_变更注册 = "4437128a-7511-435b-936e-02a9a6fe23fa";
            public const string 昌平区_二级建造师执业资格认定_市政公用工程专业_变更注册 = "e3d20137-095a-4e98-a431-cda14e44b9d8";
            public const string 大兴区_二级建造师执业资格认定_市政公用工程专业_变更注册 = "cd6118f9-978d-47ff-bd8c-93163fd0c9ba";
            public const string 房山区_二级建造师执业资格认定_市政公用工程专业_变更注册 = "636082c0-bd96-41b0-9fbf-4f45d7feabc9";
            public const string 丰台区_二级建造师执业资格认定_市政公用工程专业_变更注册 = "82c87482-1d84-4794-a295-e59f510ee78d";
            public const string 通州区_二级建造师执业资格认定_市政公用工程专业_变更注册 = "8aada651-d4ab-46bf-8f80-77ea29fff730";
            public const string 延庆区_二级建造师执业资格认定_市政公用工程专业_变更注册 = "cd7c6059-1cef-4077-aba6-f448ee9ac6a4";
            public const string 朝阳区_二级建造师执业资格认定_市政公用工程专业_变更注册 = "2284038d-06c1-464b-b940-f44ee6d8b492";
            public const string 东城区_二级建造师执业资格认定_市政公用工程专业_变更注册 = "6c3fcf37-1705-4b36-a9f8-283981b175ba";
            public const string 西城区_二级建造师执业资格认定_市政公用工程专业_变更注册 = "060168e0-26ac-494b-b483-294b4229fece";
            public const string 海淀区_二级建造师执业资格认定_水利水电工程专业_变更注册 = "346280e9-b892-403c-8f8b-fdf51b6dabc0";
            public const string 门头沟区_二级建造师执业资格认定_水利水电工程专业_变更注册 = "13be39b1-ad25-4d51-a5f3-d24bbcd5ad07";
            public const string 房山区_二级建造师执业资格认定_水利水电工程专业_变更注册 = "4cbf01f2-1d06-4b47-a9a8-e259d8d8c467";
            public const string 经济技术开发区_二级建造师执业资格认定_水利水电工程专业_变更注册 = "7f4459a3-493a-4a3d-b3e7-2c14329ba301";
            public const string 昌平区_二级建造师执业资格认定_水利水电工程专业_变更注册 = "618c25b2-8fe5-463a-84b1-989a40aeb500";
            public const string 朝阳区_二级建造师执业资格认定_水利水电工程专业_变更注册 = "eb24184c-99e8-4e24-9018-4fc3c3a1488e";
            public const string 平谷区_二级建造师执业资格认定_水利水电工程专业_变更注册 = "60cb24bf-8e8f-4f47-b715-ea42159154d3";
            public const string 西城区_二级建造师执业资格认定_水利水电工程专业_变更注册 = "e3f34341-64a2-4690-9a83-01cd10a343c2";
            public const string 怀柔区_二级建造师执业资格认定_水利水电工程专业_变更注册 = "f9454691-40f2-49c7-8467-8bdd429ca594";
            public const string 通州区_二级建造师执业资格认定_水利水电工程专业_变更注册 = "443be29e-c19a-4f3a-acd8-8529a78d38c3";
            public const string 延庆区_二级建造师执业资格认定_水利水电工程专业_变更注册 = "450218a2-9770-4a8a-9aef-6650b37ab409";
            public const string 大兴区_二级建造师执业资格认定_水利水电工程专业_变更注册 = "1d2d85db-e9a0-4f87-a505-3cb88a5b5f18";
            public const string 石景山区_二级建造师执业资格认定_水利水电工程专业_变更注册 = "7c29429b-988c-4fcf-bc79-81dce54a69a2";
            public const string 顺义区_二级建造师执业资格认定_水利水电工程专业_变更注册 = "415d8ffa-4fe9-4b83-a465-c8e1a86fc0d0";
            public const string 东城区_二级建造师执业资格认定_水利水电工程专业_变更注册 = "2cd3a6c8-dcb3-4674-a428-fb9610c51a96";
            public const string 北京市_二级建造师执业资格认定_水利水电工程专业_变更注册 = "e089c589-530c-4fde-9bf1-596cedbebb83";
            public const string 密云区_二级建造师执业资格认定_水利水电工程专业_变更注册 = "5d778446-7292-4af6-b47a-6c5d9a513078";
            public const string 丰台区_二级建造师执业资格认定_水利水电工程专业_变更注册 = "c3861a00-18f0-4d10-83af-bef19882b1da";

            public const string 二级注册造价工程师注册_变更注册 = "08f26b21-62b4-445d-aa60-7743baa5aad9";
            public const string 二级注册造价工程师注册_延续注册 = "8e38e002-9ae9-4001-a36d-1b3f57edfb06";
            public const string 二级注册造价工程师注册_注销注册 = "67bcb8e4-268a-4fac-9045-9d4ed44d6639";
            public const string 二级注册造价工程师注册_初始注册 = "9e2a5380-ffa5-49e9-b9f1-2b98ebb3fed9";

            public const string 建筑施工企业主要负责人安全生产考核_证书延期 = "927669c2-0201-4882-aa21-1cf16e2c263f";
            public const string 建筑施工企业项目负责人安全生产考核_证书延期 = "3c4320cb-f33b-4a89-9417-9d6d509bb7cf";
            public const string 建筑施工企业专职安全生产管理人员安全生产考核_证书延期 = "8a3b79bf-6805-48eb-9541-4b88b139694c";

            public const string 建筑施工企业主要负责人安全生产考核_跨省变更受聘企业 = "167345f5-8c73-46df-8169-d4d0bef39cc5";
            public const string 建筑施工企业项目负责人安全生产考核_跨省变更受聘企业 = "14f4a15c-3dea-46b4-8feb-b5e90742009c";
            public const string 建筑施工企业专职安全生产管理人员安全生产考核_跨省变更受聘企业 = "99a1353b-5725-47d4-b515-b5f83d42d343";

            public const string 建筑施工企业主要负责人安全生产考核_省内变更受聘企业 = "cdbaf0f7-4962-450b-b9c2-0882c2fb5c26";
            public const string 建筑施工企业项目负责人安全生产考核_省内变更受聘企业 = "e64cb614-cf97-40a9-a909-658f0d44c15f";
            public const string 建筑施工企业专职安全生产管理人员安全生产考核_省内变更受聘企业 = "b723e0ed-7d46-4af6-878e-04c8dd95637e";

            public const string 建筑施工企业项目负责人安全生产考核_证书注销 = "351e17f4-becd-4dfc-bbac-37c1d2fc746b";
            public const string 建筑施工企业主要负责人安全生产考核_证书注销 = "9909f2f4-f38c-4021-82a3-46782a478b51";
            public const string 建筑施工企业专职安全生产管理人员安全生产考核_证书注销 = "e2a85252-bebe-4eb2-90bb-1c1fb1133320";

            public const string 建筑施工企业主要负责人安全生产考核_考核发证 = "6dc09824-1052-4908-ada5-a49d0e7351a2";
            public const string 建筑施工企业项目负责人安全生产考核_考核发证 = "9e33c147-5bde-4670-a4b2-f8195f99bee8";
            public const string 建筑施工企业专职安全生产管理人员安全生产考核_考核发证 = "7bc684fa-6625-42af-a159-9cbcb533ebb4";

            public const string 建筑施工特种作业人员职业资格认定_建筑架子工_普通脚手架_考核 = "64bb6183-2387-4499-96e9-acb64a6dfbc4";
            public const string 建筑施工特种作业人员职业资格认定_建筑架子工_普通脚手架_延期复核 = "52e49241-3b62-4d51-8dd7-35de245d5d5c";
            public const string 建筑施工特种作业人员职业资格认定_建筑架子工_普通脚手架_注销 = "45d10245-b061-4377-8983-3c1b16265fbe";

            public const string 建筑施工特种作业人员职业资格认定_建筑起重司索信号工_考核 = "bf8c6b8c-3322-4c2c-8902-2628168e44ed";
            public const string 建筑施工特种作业人员职业资格认定_建筑起重司索信号工_延期复核 = "3896371c-e03e-4f42-9e27-0caa4e1a3102";
            public const string 建筑施工特种作业人员职业资格认定_建筑起重司索信号工_注销 = "8e05b23b-7032-491c-9437-934ee927fba7";

            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械安装拆卸工_物料提升机_考核 = "a867c4c0-38e6-42bd-b242-b5624f883b7c";
            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械安装拆卸工_物料提升机_延期复核 = "ad72391d-5673-4b3c-8087-34e41c7053b7";
            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械安装拆卸工_物料提升机_注销 = "71474453-9f53-4ec7-8fc6-e6dfdae35202";

            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械安全拆卸工_塔式起重机_考核 = "758a2d69-3cc7-451e-b09b-6e4a44928a5b";
            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械安全拆卸工_塔式起重机_延期复核 = "9509a869-f473-4e93-b36f-5c5c698cf13c";
            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械安全拆卸工_塔式起重机_注销 = "f5bff485-ee1e-4cc8-aef7-7de1aad697f1";

            public const string 建筑施工特种作业人员职业资格认定_高处作业吊篮安装拆卸工_考核 = "00915139-4856-48a8-bf64-6ecea3aeb82f";
            public const string 建筑施工特种作业人员职业资格认定_高处作业吊篮安装拆卸工_延期复核 = "c75500ad-9604-4c97-8f99-82c891f6efa0";
            public const string 建筑施工特种作业人员职业资格认定_高处作业吊篮安装拆卸工_注销 = "e7a57d74-e24e-4a19-960d-d69ef7a1b413";

            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械司机_塔式起重机_考核 = "75df3e52-6623-4248-8c83-cd587cf38e31";
            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械司机_塔式起重机_延期复核 = "1449f41f-3d49-45dc-ba7a-50f812e34996";
            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械司机_塔式起重机_注销 = "75bd66aa-de4c-4305-a5af-e5a663a436ac";

            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械安装拆卸工_施工升降机_考核 = "b3887b27-769b-41a8-a768-37d5dd7f0e5e";
            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械安装拆卸工_施工升降机_延期复核 = "fe9de1cc-9048-452e-8a33-b95d405e8ee6";
            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械安装拆卸工_施工升降机_注销 = "93fef00a-bbf8-4a9a-8e9b-92da422484b7";

            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械司机_施工升降机_考核 = "7b1a3712-7b94-41fa-b705-46148cfa1edc";
            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械司机_施工升降机_延期复核 = "7e942cc0-14a8-48c2-a8ad-b4783c33d277";
            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械司机_施工升降机_注销 = "14277492-4e5a-4b8e-8ff8-a80a3c97cad9";

            public const string 建筑施工特种作业人员职业资格认定_建筑电工_考核 = "ee8ad88a-1b47-4a07-b000-8068d0794fb6";
            public const string 建筑施工特种作业人员职业资格认定_建筑电工_延期复核 = "6bcd9edf-1756-46a8-9ba4-85ddd398251f";
            public const string 建筑施工特种作业人员职业资格认定_建筑电工_注销 = "6354731e-82a0-4f2d-9a27-2183e28f6cec";

            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械司机_物料提升机_考核 = "5251fb90-ad55-4a36-9563-80fea9615975";
            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械司机_物料提升机_延期复核 = "c8b82a14-dcb1-495e-a84f-70cdce36195b";
            public const string 建筑施工特种作业人员职业资格认定_建筑起重机械司机_物料提升机_注销 = "6dcb8632-1add-4e09-a851-88abf0104b1e";

            public const string 建筑施工特种作业人员职业资格认定_建筑架子工_附着升降脚手架_考核 = "ba0b3617-9410-4cfc-b20f-360d4ddbe371";
            public const string 建筑施工特种作业人员职业资格认定_建筑架子工_附着升降脚手架_延期复核 = "ae068505-8e2a-4f21-95a9-f2aaa2845288";
            public const string 建筑施工特种作业人员职业资格认定_建筑架子工_附着升降脚手架_注销 = "4ed701c5-7973-4bde-ba44-53342aa185ff";

            public const string 职业技能鉴定证书变更 = "32bcf506-7920-4647-81a5-ada4891c744f";
            public const string 职业技能鉴定证书注销 = "fba905eb-a44d-4ee9-a9cc-15aca9cf41a9";

        }

        /// <summary>
        /// 政务行政事项大类（证书类型）
        /// </summary>
        public static class TaskName_Master
        {
            public const string 建造师执业资格认定 = "建造师执业资格认定";
            public const string 建筑施工企业主要负责人_项目负责人和专职安全生产管理人员安全生产考核 = "建筑施工企业主要负责人、项目负责人和专职安全生产管理人员安全生产考核";
            public const string 建筑施工特种作业人员职业资格认定 = "建筑施工特种作业人员职业资格认定";
            public const string 注册造价工程师注册 = "注册造价工程师注册";
            public const string 职业技能鉴定="职业（工种）技能鉴定";
        }
    }
}
