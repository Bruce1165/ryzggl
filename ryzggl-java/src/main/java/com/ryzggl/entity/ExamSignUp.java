package com.ryzggl.entity;

import com.baomidou.mybatisplus.annotation.IdType;
import com.baomidou.mybatisplus.annotation.TableId;
import com.baomidou.mybatisplus.annotation.TableName;
import lombok.Data;
import lombok.EqualsAndHashCode;

import java.math.BigDecimal;
import java.util.Date;

/**
 * ExamSignUp Entity - Exam Registration (考试报名)
 * Represents a student's registration for an exam
 */
@Data
@EqualsAndHashCode(callSuper = false)
@TableName("ExamSignUp")
public class ExamSignUp {

    /**
     * Exam Sign Up ID - Primary Key
     */
    @TableId(type = IdType.AUTO)
    private Long ExamSignUpID;

    /**
     * Sign Up Code - Registration code
     */
    private String SignUpCode;

    /**
     * Sign Up Date - Registration date
     */
    private Date SignUpDate;

    /**
     * Worker ID - Foreign key to Worker table
     */
    private Long WorkerID;

    /**
     * Unit ID - Department/Company ID
     */
    private Long UnitID;

    /**
     * Training Unit ID - Training organization ID
     */
    private Long TrainUnitID;

    /**
     * Exam Plan ID - Foreign key to ExamPlan
     */
    private Long ExamPlanID;

    /**
     * Work Start Date - Start date of employment
     */
    private Date WorkStartDate;

    /**
     * Work Year Number - Years of work experience
     */
    private Integer WorkYearNumer;

    /**
     * Person Detail - Personal details
     */
    private String PersonDetail;

    /**
     * Hire Unit Advise - Hiring unit comments
     */
    private String HireUnitAdvise;

    /**
     * Admin Unit Advise - Administration unit comments
     */
    private String AdminUnitAdvise;

    /**
     * Check Code - Verification code
     */
    private String CheckCode;

    /**
     * Check Result - Verification result
     */
    private String CheckResult;

    /**
     * Check Man - Verifier name
     */
    private String CheckMan;

    /**
     * Check Date - Verification timestamp
     */
    private Date CheckDate;

    /**
     * Pay Notice Code - Payment notification code
     */
    private String PayNoticeCode;

    /**
     * Pay Notice Result - Payment notification result
     */
    private String PayNoticeResult;

    /**
     * Pay Notice Man - Payment notifier
     */
    private String PayNoticeMan;

    /**
     * Pay Notice Date - Payment notification date
     */
    private Date PayNoticeDate;

    /**
     * Pay Money - Payment amount
     */
    private BigDecimal PayMoney;

    /**
     * Pay Confirm Code - Payment confirmation code
     */
    private String PayConfirmCode;

    /**
     * Pay Confirm Result - Payment confirmation result
     */
    private String PayConfirmRult;

    /**
     * Pay Confirm Man - Payment confirmer
     */
    private String PayConfirmMan;

    /**
     * Pay Confirm Date - Payment confirmation date
     */
    private Date PayConfirmDate;

    /**
     * Face Photo - Photo path
     */
    private String FacePhoto;

    /**
     * Status - Registration status
     * e.g., "待审核", "已通过", "已驳回"
     */
    private String Status;

    /**
     * Worker Name - Worker's name
     */
    private String WorkerName;

    /**
     * Certificate Type - Certificate type
     */
    private String CertificateType;

    /**
     * Certificate Code - Certificate/ID Card number
     */
    private String CertificateCode;

    /**
     * Unit Name - Company/department name
     */
    private String UnitName;

    /**
     * Unit Code - Company/department code
     */
    private String UnitCode;

    /**
     * Result Certificate Code - Result certificate code (after exam)
     */
    private String ResultCertificateCode;

    /**
     * Create Person ID - Creator user ID
     */
    private Long CreatePersonID;

    /**
     * Create Time - Creation timestamp
     */
    private Date CreateTime;

    /**
     * Modify Person ID - Modifier user ID
     */
    private Long ModifyPersonID;

    /**
     * Modify Time - Modification timestamp
     */
    private Date ModifyTime;

    /**
     * Skill Level - Skill level
     */
    private String SKILLLEVEL;

    /**
     * Sex - Gender
     */
    private String S_SEX;

    /**
     * Cultural Level - Education level
     */
    private String S_CULTURALLEVEL;

    /**
     * Phone - Phone number
     */
    private String S_PHONE;

    /**
     * Birthday - Date of birth
     */
    private Date S_BIRTHDAY;

    /**
     * Is Conditions - Eligibility status
     */
    private String IsConditions;

    /**
     * First Trial Time - First trial date
     */
    private Date FIRSTTRIALTIME;

    /**
     * Training Unit Name - Training unit name
     */
    private String S_TRAINUNITNAME;

    /**
     * Sign Up Man - Registrant name
     */
    private String SignUpMan;

    /**
     * Promise - Commitment indicator
     */
    private Integer Promise;

    /**
     * Sign Up Place ID - Registration place ID
     */
    private Long SignUpPlaceID;

    /**
     * Place Name - Registration place name
     */
    private String PlaceName;

    /**
     * Check Date Plan - Planned check date
     */
    private Date CheckDatePlan;

    /**
     * First Check Type - First check type
     */
    private Integer FirstCheckType;

    /**
     * Accept Man - Acceptance person
     */
    private String AcceptMan;

    /**
     * Accept Time - Acceptance timestamp
     */
    private Date AcceptTime;

    /**
     * Accept Result - Acceptance result
     */
    private String AcceptResult;

    /**
     * Contract Type - Employment contract type
     */
    private Integer ENT_ContractType;

    /**
     * Contract Start Time - Contract start date
     */
    private Date ENT_ContractStartTime;

    /**
     * Contract End Time - Contract end date
     */
    private Date ENT_ContractENDTime;

    /**
     * Sign Up Promise - Signup commitment
     */
    private Integer SignupPromise;

    /**
     * ZA Check Time - ZA check timestamp
     */
    private Date ZACheckTime;

    /**
     * ZA Check Result - ZA check result
     */
    private Integer ZACheckResult;

    /**
     * ZA Check Remark - ZA check remarks
     */
    private String ZACheckRemark;

    /**
     * Job - Job title
     */
    private String Job;

    /**
     * Safe Train Type - Safety training type
     */
    private String SafeTrainType;

    /**
     * Safe Train Unit - Safety training unit
     */
    private String SafeTrainUnit;

    /**
     * Safe Train Unit Code - Safety training unit code
     */
    private String SafeTrainUnitCode;

    /**
     * Safe Train Unit Valid End Date - Training unit validity end date
     */
    private Date SafeTrainUnitValidEndDate;

    /**
     * Safe Train Unit Of Dept - Training unit department
     */
    private String SafeTrainUnitOfDept;

    /**
     * Lock Time - Registration lock time
     */
    private Date LockTime;

    /**
     * Lock End Time - Registration lock end time
     */
    private Date LockEndTime;

    /**
     * Lock Reason - Lock reason
     */
    private String LockReason;

    /**
     * Lock Man - Person who locked
     */
    private String LockMan;
}
