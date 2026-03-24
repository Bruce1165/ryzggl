package com.ryzggl.service;

import org.springframework.stereotype.Service;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.time.LocalDate;
import java.time.format.DateTimeFormatter;
import java.time.format.DateTimeParseException;

/**
 * ID Card Validator Service
 * Migrated from database function [dbo].[CheckIDCard]
 *
 * Chinese ID Card Validation Rules:
 * - Supports both 15-digit and 18-digit ID cards
 * - 15-digit: Validates date (1970-1999 prefix) and numeric format
 * - 18-digit: Validates date, numeric format, and checksum digit
 *
 * Checksum Algorithm (for 18-digit cards):
 * - Weighted factors: 7,9,10,5,8,4,2,1,6,3,7,9,10,5,8,4,2
 * - Valid codes: 1,0,X,9,8,7,6,5,4,3,2
 * - Checksum = sum(digit[i] * factor[i]) % 11
 */
@Service
public class IDCardValidator {

    private static final Logger logger = LoggerFactory.getLogger(IDCardValidator.class);

    // Weighted factors for 18-digit ID card checksum calculation
    private static final int[] WEIGHTED_FACTORS = {7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 8, 4, 2};

    // Valid checksum codes (indexed by remainder value 0-10)
    private static final char[] VALID_CHECKSUM_CODES = {'1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2'};

    private static final DateTimeFormatter DATE_FORMATTER = DateTimeFormatter.ofPattern("yyyyMMdd");

    /**
     * Validate Chinese ID Card
     *
     * @param idCard ID Card number (15 or 18 digits)
     * @return true if valid, false otherwise
     */
    public boolean validate(String idCard) {
        if (idCard == null || idCard.isEmpty()) {
            logger.debug("ID Card is null or empty");
            return false;
        }

        // Convert to uppercase for consistency
        idCard = idCard.trim().toUpperCase();

        logger.debug("Validating ID Card: {}", idCard);

        // Validate length
        if (idCard.length() != 15 && idCard.length() != 18) {
            logger.debug("ID Card length invalid: {} (expected 15 or 18)", idCard.length());
            return false;
        }

        // 15-digit ID Card validation
        if (idCard.length() == 15) {
            return validate15DigitID(idCard);
        }

        // 18-digit ID Card validation
        return validate18DigitID(idCard);
    }

    /**
     * Validate 15-digit ID Card
     * Rules:
     * - All characters must be digits
     * - Date part (positions 7-12) must be valid (prefix with '19')
     */
    private boolean validate15DigitID(String idCard) {
        // Check if all characters are digits
        if (!idCard.matches("\\d{15}")) {
            logger.debug("15-digit ID Card contains non-digit characters");
            return false;
        }

        // Validate date part (6 digits: YYMMDD)
        String datePart = idCard.substring(6, 12);
        String fullDate = "19" + datePart; // 1970-1999

        try {
            LocalDate.parse(fullDate, DATE_FORMATTER);
            logger.debug("15-digit ID Card date validation passed");
            return true;
        } catch (DateTimeParseException e) {
            logger.debug("15-digit ID Card date validation failed: {}", fullDate);
            return false;
        }
    }

    /**
     * Validate 18-digit ID Card
     * Rules:
     * - First 17 characters must be digits
     * - Date part (positions 7-14) must be valid
     * - Last character must match checksum calculation
     */
    private boolean validate18DigitID(String idCard) {
        // Check if first 17 characters are digits
        String first17 = idCard.substring(0, 17);
        if (!first17.matches("\\d{17}")) {
            logger.debug("18-digit ID Card first 17 digits contain non-digit characters");
            return false;
        }

        // Validate date part (8 digits: YYYYMMDD)
        String datePart = idCard.substring(6, 14);
        try {
            LocalDate.parse(datePart, DATE_FORMATTER);
        } catch (DateTimeParseException e) {
            logger.debug("18-digit ID Card date validation failed: {}", datePart);
            return false;
        }

        // Validate checksum digit
        char actualChecksum = idCard.charAt(17);
        char calculatedChecksum = calculateChecksum(first17);

        if (actualChecksum == calculatedChecksum) {
            logger.debug("18-digit ID Card checksum validation passed");
            return true;
        } else {
            logger.debug("18-digit ID Card checksum validation failed: expected {}, actual {}",
                    calculatedChecksum, actualChecksum);
            return false;
        }
    }

    /**
     * Calculate checksum digit for 18-digit ID Card
     *
     * @param first17 First 17 digits of ID Card
     * @return Calculated checksum character
     */
    private char calculateChecksum(String first17) {
        int sum = 0;

        for (int i = 0; i < 17; i++) {
            int digit = Character.getNumericValue(first17.charAt(i));
            sum += digit * WEIGHTED_FACTORS[i];
        }

        int remainder = sum % 11;
        return VALID_CHECKSUM_CODES[remainder];
    }

    /**
     * Extract birth date from ID Card
     *
     * @param idCard ID Card number
     * @return Birth date (LocalDate) or null if invalid
     */
    public LocalDate extractBirthDate(String idCard) {
        if (idCard == null || idCard.isEmpty()) {
            return null;
        }

        idCard = idCard.trim().toUpperCase();

        try {
            if (idCard.length() == 15) {
                // 15-digit: positions 7-12 (YYMMDD), prefix with '19'
                String datePart = "19" + idCard.substring(6, 12);
                return LocalDate.parse(datePart, DATE_FORMATTER);
            } else if (idCard.length() == 18) {
                // 18-digit: positions 7-14 (YYYYMMDD)
                String datePart = idCard.substring(6, 14);
                return LocalDate.parse(datePart, DATE_FORMATTER);
            }
        } catch (DateTimeParseException e) {
            logger.warn("Failed to extract birth date from ID Card: {}", idCard, e);
        }

        return null;
    }

    /**
     * Extract gender from ID Card
     *
     * @param idCard ID Card number
     * @return Gender ("男" for odd last digit, "女" for even last digit) or null if invalid
     */
    public String extractGender(String idCard) {
        if (idCard == null || idCard.isEmpty()) {
            return null;
        }

        idCard = idCard.trim().toUpperCase();

        int genderDigitPosition;
        if (idCard.length() == 15) {
            // 15-digit: last digit indicates gender
            genderDigitPosition = 14;
        } else if (idCard.length() == 18) {
            // 18-digit: second-to-last digit indicates gender
            genderDigitPosition = 16;
        } else {
            return null;
        }

        try {
            char genderChar = idCard.charAt(genderDigitPosition);
            int genderDigit = Character.getNumericValue(genderChar);
            return (genderDigit % 2 == 1) ? "男" : "女";
        } catch (NumberFormatException e) {
            logger.warn("Failed to extract gender from ID Card: {}", idCard, e);
            return null;
        }
    }

    /**
     * Calculate age based on ID Card
     *
     * @param idCard ID Card number
     * @return Age in years or -1 if invalid
     */
    public int calculateAge(String idCard) {
        LocalDate birthDate = extractBirthDate(idCard);
        if (birthDate == null) {
            return -1;
        }

        return (int) java.time.temporal.ChronoUnit.YEARS.between(birthDate, LocalDate.now());
    }
}
