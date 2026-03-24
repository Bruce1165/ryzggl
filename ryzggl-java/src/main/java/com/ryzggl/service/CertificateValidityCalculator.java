package com.ryzggl.service;

import org.springframework.stereotype.Service;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.time.LocalDate;
import java.time.LocalDateTime;
import java.time.ZoneId;
import java.time.ZonedDateTime;
import java.time.temporal.ChronoUnit;
import java.util.Date;

/**
 * Certificate Validity Calculator Service
 * Migrated from database function GET_CertificateContinueValidEndDate
 *
 * Business Rules:
 * - PostTypeID 1 (安管人员):
 *   - PostID 148 (B类): 3 years extension
 *   - PostID 147 (A类):
 *     - FR region: 3 years extension
 *     - Non-FR region: Age limits apply
 * - PostTypeID 2 (特种作业):
 *   - Male: 2 years extension
 *   - Female: 50 years extension (with age limit)
 *
 * Age Limits:
 * - 法人A (PostID 148): No age limit
 * - 非法人A本、C本: Male 60, Female 55
 * - B本: 65 years (for cancellation age limit)
 * - 特种作业: Male 60, Female 50
 */
@Service
public class CertificateValidityCalculator {

    private static final Logger logger = LoggerFactory.getLogger(CertificateValidityCalculator.class);

    // Post Type Constants
    private static final int POST_TYPE_SAFETY_PERSONNEL = 1; // 安管人员
    private static final int POST_TYPE_SPECIAL_WORK = 2;       // 特种作业

    // Post ID Constants (Safety Personnel)
    private static final int POST_ID_LEGAL_PERSON = 148;    // 法人A (B类)
    private static final int POST_ID_REGION_A = 147;          // A类

    // Age Limits (Safety Personnel)
    private static final int AGE_LIMIT_MALE_LEGAL_A = Integer.MAX_VALUE; // 法人A 不限年龄
    private static final int AGE_LIMIT_FEMALE_LEGAL_A = Integer.MAX_VALUE;
    private static final int AGE_LIMIT_MALE_ILLEGAL_NON_FR = 60; // 非法人A本、C本 男60周岁
    private static final int AGE_LIMIT_FEMALE_ILLEGAL_NON_FR = 55; // 非法人A本、C本 女55周岁
    private static final int AGE_LIMIT_ILLEGAL_B = 65; // B本 65周岁（注销年龄上限）

    // Age Limits (Special Work)
    private static final int AGE_LIMIT_MALE_SPECIAL = 60; // 特种作业 男60周岁
    private static final int AGE_LIMIT_FEMALE_SPECIAL = 50; // 特种作业 女50周岁

    // Extension Years
    private static final int EXTENSION_YEARS_LEGAL_A = 3; // 法人A：3年
    private static final int EXTENSION_YEARS_REGION_A = 3; // A类：3年
    private static final int EXTENSION_YEARS_ILLEGAL_NON_FR_MALE = 3; // 非法人A本、C本 男：3年
    private static final int EXTENSION_YEARS_ILLEGAL_NON_FR_FEMALE = 3; // 非法人A本、C本 女：3年
    private static final int EXTENSION_YEARS_SPECIAL_MALE = 2; // 特种作业 男：2年
    private static final int EXTENSION_YEARS_SPECIAL_FEMALE = 2; // 特种作业 女：2年
    private static final int EXTENSION_YEARS_SPECIAL_AGE_LIMIT_MALE = 50; // 特种作业 男：50年（超龄续期）
    private static final int EXTENSION_YEARS_SPECIAL_AGE_LIMIT_FEMALE = 55; // 特种作业 女：55年（超龄续期）

    /**
     * Calculate certificate validity end date for continuation
     *
     * @param postTypeID Post type (1=安管人员, 2=特种作业)
     * @param postID Post ID
     * @param sex Gender (男/女)
     * @param birthday Worker's birthday
     * @param validEndDate Current certificate valid end date
     * @param unitCode Unit code
     * @param workerName Worker name (for FR region check)
     * @return Calculated new valid end date
     */
    public Date calculateContinueValidEndDate(Integer postTypeID, Integer postID, String sex,
                                          Date birthday, Date validEndDate,
                                          String unitCode, String workerName) {
        try {
            logger.debug("Calculating certificate continue valid end date: postType={}, postID={}, sex={}, birthday={}, validEndDate={}",
                    postTypeID, postID, sex, birthday, validEndDate);

            LocalDate birthDate = convertToLocalDate(birthday);
            LocalDate currentValidEndDate = convertToLocalDate(validEndDate);

            if (postTypeID == null) {
                logger.warn("PostTypeID is null, returning current valid date");
                return validEndDate;
            }

            LocalDate newValidEndDate;

            if (postTypeID == POST_TYPE_SAFETY_PERSONNEL) {
                // 安管人员规则
                newValidEndDate = calculateSafetyPersonnelRule(postID, sex, birthDate,
                                                            currentValidEndDate, unitCode, workerName);
            } else if (postTypeID == POST_TYPE_SPECIAL_WORK) {
                // 特种作业规则
                newValidEndDate = calculateSpecialWorkRule(sex, birthDate, currentValidEndDate);
            } else {
                logger.warn("Unknown PostTypeID: {}, returning current valid date", postTypeID);
                return validEndDate;
            }

            logger.info("Calculated certificate valid end date: {} (from: {})", newValidEndDate, currentValidEndDate);
            return convertToDate(newValidEndDate);

        } catch (Exception e) {
            logger.error("Error calculating certificate validity end date", e);
            return validEndDate; // Return original date on error
        }
    }

    /**
     * Calculate rule for safety personnel (安管人员)
     */
    private LocalDate calculateSafetyPersonnelRule(Integer postID, String sex, LocalDate birthDate,
                                           LocalDate currentValidEndDate, String unitCode, String workerName) {
        if (postID == POST_ID_LEGAL_PERSON) {
            // 法人A（B类）：续期3年
            logger.debug("Legal Person A rule: Add 3 years");
            return currentValidEndDate.plusYears(EXTENSION_YEARS_LEGAL_A);
        } else if (postID == POST_ID_REGION_A) {
            // A类：检查是否为FR地区
            if (isFRRegion(unitCode, workerName)) {
                // FR地区：续期3年
                logger.debug("A class FR region: Add 3 years");
                return currentValidEndDate.plusYears(EXTENSION_YEARS_REGION_A);
            } else {
                // 非FR地区：应用年龄限制
                return calculateAgeLimitRule(sex, birthDate, currentValidEndDate,
                                              AGE_LIMIT_MALE_ILLEGAL_NON_FR,
                                              AGE_LIMIT_FEMALE_ILLEGAL_NON_FR,
                                              EXTENSION_YEARS_ILLEGAL_NON_FR_MALE,
                                              EXTENSION_YEARS_ILLEGAL_NON_FR_FEMALE);
            }
        } else {
            logger.warn("Unknown safety personnel PostID: {}", postID);
            return currentValidEndDate;
        }
    }

    /**
     * Calculate rule for special work (特种作业)
     */
    private LocalDate calculateSpecialWorkRule(String sex, LocalDate birthDate, LocalDate currentValidEndDate) {
        int currentAge = calculateAge(birthDate);

        if ("男".equals(sex)) {
            // 特种作业男性：2年续期，年龄60岁限制
            if (currentAge >= AGE_LIMIT_MALE_SPECIAL) {
                // 超龄：50年续期
                logger.debug("Special work male over age: Add 50 years");
                return currentValidEndDate.plusYears(EXTENSION_YEARS_SPECIAL_AGE_LIMIT_MALE);
            } else {
                // 正常：2年续期
                logger.debug("Special work male normal: Add 2 years");
                return currentValidEndDate.plusYears(EXTENSION_YEARS_SPECIAL_MALE);
            }
        } else if ("女".equals(sex)) {
            // 特种作业女性：2年续期，年龄50岁限制
            if (currentAge >= AGE_LIMIT_FEMALE_SPECIAL) {
                // 超龄：55年续期
                logger.debug("Special work female over age: Add 55 years");
                return currentValidEndDate.plusYears(EXTENSION_YEARS_SPECIAL_AGE_LIMIT_FEMALE);
            } else {
                // 正常：2年续期
                logger.debug("Special work female normal: Add 2 years");
                return currentValidEndDate.plusYears(EXTENSION_YEARS_SPECIAL_FEMALE);
            }
        } else {
            logger.warn("Unknown sex: {}, returning current valid date", sex);
            return currentValidEndDate;
        }
    }

    /**
     * Calculate age limit rule for non-FR regions
     */
    private LocalDate calculateAgeLimitRule(String sex, LocalDate birthDate, LocalDate currentValidEndDate,
                                          int maleAgeLimit, int femaleAgeLimit,
                                          int maleExtensionYears, int femaleExtensionYears) {
        int currentAge = calculateAge(birthDate);

        if ("男".equals(sex)) {
            if (currentAge >= maleAgeLimit) {
                // 超龄：60年续期
                logger.debug("Male over age: Add {} years", maleExtensionYears);
                return currentValidEndDate.plusYears(maleExtensionYears);
            } else {
                // 正常：3年续期
                logger.debug("Male normal: Add 3 years");
                return currentValidEndDate.plusYears(3);
            }
        } else if ("女".equals(sex)) {
            if (currentAge >= femaleAgeLimit) {
                // 超龄：60年续期
                logger.debug("Female over age: Add {} years", femaleExtensionYears);
                return currentValidEndDate.plusYears(femaleExtensionYears);
            } else {
                // 正常：3年续期
                logger.debug("Female normal: Add 3 years");
                return currentValidEndDate.plusYears(3);
            }
        } else {
            logger.warn("Unknown sex: {}, returning current valid date", sex);
            return currentValidEndDate;
        }
    }

    /**
     * Check if unit is FR region (Foreign)
     * Note: In original SQL, this checks QY_GSDJXX table with pattern matching
     * This is a simplified version - production should have proper region configuration
     */
    private boolean isFRRegion(String unitCode, String workerName) {
        // This is a simplified check. In production, this should query a proper regions table
        // or use configuration to define which unit codes belong to FR regions
        // For now, we'll use a simple heuristic

        if (unitCode == null || unitCode.isEmpty()) {
            return false;
        }

        // Check for common FR region patterns (simplified)
        // In production, this should be replaced with proper region data
        return unitCode.startsWith("FR") || unitCode.contains("法国") || unitCode.contains("法");
    }

    /**
     * Calculate age based on birthday
     */
    private int calculateAge(LocalDate birthDate) {
        if (birthDate == null) {
            return 0;
        }
        return (int) ChronoUnit.YEARS.between(birthDate, LocalDate.now());
    }

    /**
     * Convert java.util.Date to LocalDate
     */
    private LocalDate convertToLocalDate(Date date) {
        if (date == null) {
            return null;
        }
        return new java.sql.Date(date.getTime()).toLocalDate();
    }

    /**
     * Convert LocalDate to java.util.Date
     */
    private Date convertToDate(LocalDate localDate) {
        if (localDate == null) {
            return null;
        }
        return java.util.Date.from(localDate.atStartOfDay(java.time.ZoneId.systemDefault()).toInstant());
    }
}