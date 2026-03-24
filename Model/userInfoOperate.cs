using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Model
{
    /// <summary>
    /// 二级造价工程师信息（为建设部提供）
    /// </summary>
    [Serializable]
    public class userInfoOperate
    {
        // <summary>
        ///  秘钥："ek2Uf7cjFUrRhmeS"
        /// </summary>
        [JsonProperty("key")]
        public string key { set; get; }

        /// <summary>
        ///  证书信息
        /// </summary>
        [JsonProperty("userInfos")]
        public string userInfos { set; get; }
    }

    /// <summary>
    /// 二级造价工程师信息（为建设部提供）
    /// </summary>
    [Serializable]
    public class userInfos
    {
        /// <summary>
        /// 造价师信息主键
        /// </summary>
        [JsonProperty("outUserID")]
        public string outUserID { set; get; }
        /// <summary>
        /// 人员状态：1:正常 2：暂停 3：注销  4：失效 5：异常
        /// </summary>
        [JsonProperty("validFlag")]
        public int validFlag { set; get; }
        /// <summary>
        /// 姓名
        /// </summary>
        [JsonProperty("userName")]
        public string userName { set; get; }
        /// <summary>
        /// 性别：1：男  0：女
        /// </summary>
        [JsonProperty("sex")]
        public int sex { set; get; }
        /// <summary>
        /// 生日：YYYY-MM-dd
        /// </summary>
        [JsonProperty("birthday")]
        public string birthday { set; get; }
        /// <summary>
        /// 民族ID
        /// </summary>
        [JsonProperty("nationID")]
        public string nationID { set; get; }
        /// <summary>
        /// 职称ID
        /// </summary>
        [JsonProperty("titlesID")]
        public string titlesID { set; get; }
        /// <summary>
        /// 身份证号
        /// </summary>
        [JsonProperty("idCard")]
        public string idCard { set; get; }
        /// <summary>
        /// 军官号
        /// </summary>
        [JsonProperty("soldierNumber")]
        public string soldierNumber { set; get; }
        /// <summary>
        /// 学历ID
        /// </summary>
        [JsonProperty("eduLevelID")]
        public string eduLevelID { set; get; }
        /// <summary>
        /// 毕业时间：YYYY-MM-dd
        /// </summary>
        [JsonProperty("graduateDate")]
        public string graduateDate { set; get; }
        /// <summary>
        /// 毕业院校
        /// </summary>
        [JsonProperty("graduateSchool")]
        public string graduateSchool { set; get; }
        /// <summary>
        /// 所学专业
        /// </summary>
        [JsonProperty("majorName")]
        public string majorName { set; get; }
        /// <summary>
        /// 工作年限
        /// </summary>
        [JsonProperty("workYear")]
        public string workYear { set; get; }
        /// <summary>
        /// 聘用单位类别ID
        /// </summary>
        [JsonProperty("enterPriseTypeID")]
        public string enterPriseTypeID { set; get; }
        /// <summary>
        /// 单位所在省
        /// </summary>
        [JsonProperty("areaName")]
        public string areaName { set; get; }
        /// <summary>
        /// 单位所在市
        /// </summary>
        [JsonProperty("cityName")]
        public string cityName { set; get; }
        /// <summary>
        /// 单位所在区
        /// </summary>
        [JsonProperty("countyName")]
        public string countyName { set; get; }
        /// <summary>
        /// 注册管理机构
        /// </summary>
        [JsonProperty("instanceName")]
        public string instanceName { set; get; }
        /// <summary>
        /// 聘用单位
        /// </summary>
        [JsonProperty("workUnitName")]
        public string workUnitName { set; get; }
        /// <summary>
        /// 分公司
        /// </summary>
        [JsonProperty("branchUnit")]
        public string branchUnit { set; get; }
        /// <summary>
        /// 单位是否有造价资质
        /// </summary>
        [JsonProperty("empUnitTypeID")]
        public string empUnitTypeID { set; get; }
        /// <summary>
        /// 单位造价资质证书号
        /// </summary>
        [JsonProperty("workUnitNumber")]
        public string workUnitNumber { set; get; }
        /// <summary>
        /// 有无固定劳动期限  
        /// </summary>
        [JsonProperty("employDateStatus")]
        public string employDateStatus { set; get; }
        /// <summary>
        /// 聘用期限开始日期
        /// </summary>
        [JsonProperty("employDateBegin")]
        public string employDateBegin { set; get; }
        /// <summary>
        /// 聘用期限结束日期
        /// </summary>
        [JsonProperty("employDateEnd")]
        public string employDateEnd { set; get; }
        /// <summary>
        /// 单位代码
        /// </summary>
        [JsonProperty("workUnitCode")]
        public string workUnitCode { set; get; }
        /// <summary>
        /// 存档单位
        /// </summary>
        [JsonProperty("archiveUnitName")]
        public string archiveUnitName { set; get; }
        /// <summary>
        /// 建造号
        /// </summary>
        [JsonProperty("certificateNumber")]
        public string certificateNumber { set; get; }
        /// <summary>
        /// 执章编号
        /// </summary>
        [JsonProperty("signetNumber")]
        public string signetNumber { set; get; }
        /// <summary>
        /// 初始注册日期
        /// </summary>
        [JsonProperty("registerDate")]
        public string registerDate { set; get; }
        /// <summary>
        /// 有效期止
        /// </summary>
        [JsonProperty("invalidDate")]
        public string invalidDate { set; get; }
        /// <summary>
        /// 考试档案号
        /// </summary>
        [JsonProperty("examNumber")]
        public string examNumber { set; get; }
        /// <summary>
        /// 职业资格证书编号或管理号
        /// </summary>
        [JsonProperty("qualificationNumber")]
        public string qualificationNumber { set; get; }
        /// <summary>
        /// 批准日期
        /// </summary>
        [JsonProperty("agreeDate")]
        public string agreeDate { set; get; }
        /// <summary>
        /// 在岗状态
        /// </summary>
        [JsonProperty("isEmployed")]
        public string isEmployed { set; get; }
        /// <summary>
        /// 报考省市
        /// </summary>
        [JsonProperty("examAreaName")]
        public string examAreaName { set; get; }
        /// <summary>
        /// 报考专业
        /// </summary>
        [JsonProperty("examSpecialityID")]
        public string examSpecialityID { set; get; }
        /// <summary>
        /// 考试通过年度
        /// </summary>
        [JsonProperty("examYear")]
        public string examYear { set; get; }
        /// <summary>
        /// 单位地址
        /// </summary>
        [JsonProperty("address")]
        public string address { set; get; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        [JsonProperty("postNumber")]
        public string postNumber { set; get; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [JsonProperty("phone")]
        public string phone { set; get; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [JsonProperty("mobile")]
        public string mobile { set; get; }
        /// <summary>
        /// 单位主营业务ID
        /// </summary>
        [JsonProperty("unitMainBusinessID")]
        public string unitMainBusinessID { set; get; }
        /// <summary>
        /// 备注
        /// </summary>
        [JsonProperty("userRemark")]
        public string userRemark { set; get; }
        /// <summary>
        /// 继续教育学时
        /// </summary>
        [JsonProperty("allPeriod")]
        public string allPeriod { set; get; }

    }

    /// <summary>
    /// 调用数据上传结果接口返回结果
    /// </summary>
    [Serializable]
    public class zjgcsUpResultMDL
    {
        /// <summary>
        /// 调用返回编码：成 功 1 ，失败 -1。
        /// </summary>
        [JsonProperty("code")]
        public string code { set; get; }

        /// <summary>
        /// 调用返回描述：成 功返回“操作成功” ，失败返回失败具体原因描述。
        /// </summary>
        [JsonProperty("msg")]
        public string msg { set; get; }
    }

}
