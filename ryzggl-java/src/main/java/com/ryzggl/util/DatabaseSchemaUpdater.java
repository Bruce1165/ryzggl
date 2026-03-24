package com.ryzggl.util;

import org.springframework.boot.CommandLineRunner;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Component;

/**
 * Database Schema Updater - Utility to execute SQL migration scripts
 */
@Component
public class DatabaseSchemaUpdater implements CommandLineRunner {

    private final JdbcTemplate jdbcTemplate;
    private final DatabaseColumnFixer columnFixer;

    public DatabaseSchemaUpdater(JdbcTemplate jdbcTemplate, DatabaseColumnFixer columnFixer) {
        this.jdbcTemplate = jdbcTemplate;
        this.columnFixer = columnFixer;
    }

    @Override
    public void run(String... args) {
        System.out.println("========================================");
        System.out.println("Database Schema Updater");
        System.out.println("========================================");
        System.out.println();

        try {
            // Fix missing columns in existing tables
            columnFixer.fixMissingColumns();

            System.out.println();
            System.out.println("========================================");
            System.out.println("Database schema update completed!");
            System.out.println("========================================");

        } catch (Exception e) {
            System.err.println("========================================");
            System.err.println("Database schema update failed!");
            System.err.println("Error: " + e.getMessage());
            System.err.println("========================================");
            e.printStackTrace();
        }
    }
}
