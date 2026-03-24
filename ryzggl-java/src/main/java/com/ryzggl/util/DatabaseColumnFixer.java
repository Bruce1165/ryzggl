package com.ryzggl.util;

import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Component;

import javax.sql.DataSource;
import java.sql.Connection;
import java.sql.DatabaseMetaData;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

/**
 * Database Column Fixer - Add missing columns to existing tables
 */
@Component
public class DatabaseColumnFixer {

    private final JdbcTemplate jdbcTemplate;
    private final DataSource dataSource;

    public DatabaseColumnFixer(JdbcTemplate jdbcTemplate, DataSource dataSource) {
        this.jdbcTemplate = jdbcTemplate;
        this.dataSource = dataSource;
    }

    public void fixMissingColumns() {
        System.out.println("========================================");
        System.out.println("Database Column Fixer");
        System.out.println("========================================");
        System.out.println();

        try {
            // Fix ApplyContinue table
            fixApplyContinueTable();

            // Fix ApplyRenew table
            fixApplyRenewTable();

            // Fix CertificateChange table
            fixCertificateChangeTable();

            // Fix CertificateContinue table
            fixCertificateContinueTable();

            System.out.println();
            System.out.println("========================================");
            System.out.println("Column fix completed!");
            System.out.println("========================================");

        } catch (Exception e) {
            System.err.println("========================================");
            System.err.println("Column fix failed!");
            System.err.println("Error: " + e.getMessage());
            System.err.println("========================================");
            e.printStackTrace();
        }
    }

    private void fixApplyContinueTable() throws SQLException {
        System.out.println("Fixing ApplyContinue table...");
        String tableName = "ApplyContinue";

        // Add missing columns
        addColumnIfNotExists(tableName, "CREATE_TIME", "DATETIME NULL");
        addColumnIfNotExists(tableName, "UPDATE_TIME", "DATETIME NULL");
        addColumnIfNotExists(tableName, "CREATE_BY", "NVARCHAR(100) NULL");
        addColumnIfNotExists(tableName, "UPDATE_BY", "NVARCHAR(100) NULL");
        addColumnIfNotExists(tableName, "DELETED", "INT DEFAULT 0");

        // Add missing indexes
        addIndexIfNotExists(tableName, "IX_ApplyContinue_WorkerId", "WORKERID");
        addIndexIfNotExists(tableName, "IX_ApplyContinue_CertificateCode", "CERTIFICATECODE");
        addIndexIfNotExists(tableName, "IX_ApplyContinue_ApplyStatus", "APPLYSTATUS");
        addIndexIfNotExists(tableName, "IX_ApplyContinue_CreateTime", "CREATE_TIME");

        System.out.println("ApplyContinue table fixed!");
    }

    private void fixApplyRenewTable() throws SQLException {
        System.out.println("Fixing ApplyRenew table...");
        String tableName = "ApplyRenew";

        // Add missing columns
        addColumnIfNotExists(tableName, "CREATE_TIME", "DATETIME NULL");
        addColumnIfNotExists(tableName, "UPDATE_TIME", "DATETIME NULL");
        addColumnIfNotExists(tableName, "CREATE_BY", "NVARCHAR(100) NULL");
        addColumnIfNotExists(tableName, "UPDATE_BY", "NVARCHAR(100) NULL");
        addColumnIfNotExists(tableName, "DELETED", "INT DEFAULT 0");

        // Add missing indexes
        addIndexIfNotExists(tableName, "IX_ApplyRenew_WorkerId", "WORKERID");
        addIndexIfNotExists(tableName, "IX_ApplyRenew_CertificateCode", "CERTIFICATECODE");
        addIndexIfNotExists(tableName, "IX_ApplyRenew_ApplyStatus", "APPLYSTATUS");
        addIndexIfNotExists(tableName, "IX_ApplyRenew_CreateTime", "CREATE_TIME");

        System.out.println("ApplyRenew table fixed!");
    }

    private void fixCertificateChangeTable() throws SQLException {
        System.out.println("Fixing CertificateChange table...");
        String tableName = "CertificateChange";

        // Add missing columns
        addColumnIfNotExists(tableName, "CHANGEDATE", "DATE NULL");
        addColumnIfNotExists(tableName, "CHANGEMAN", "NVARCHAR(100) NULL");
        addColumnIfNotExists(tableName, "CHANGEMANID", "BIGINT NULL");
        addColumnIfNotExists(tableName, "CHECKADVISE", "NVARCHAR(MAX) NULL");
        addColumnIfNotExists(tableName, "CHECKMAN", "NVARCHAR(100) NULL");
        addColumnIfNotExists(tableName, "CHECKDATE", "NVARCHAR(50) NULL");
        addColumnIfNotExists(tableName, "FILEPATH", "NVARCHAR(500) NULL");
        addColumnIfNotExists(tableName, "REMARK", "NVARCHAR(MAX) NULL");
        addColumnIfNotExists(tableName, "CREATE_TIME", "DATETIME NULL");
        addColumnIfNotExists(tableName, "UPDATE_TIME", "DATETIME NULL");
        addColumnIfNotExists(tableName, "CREATE_BY", "NVARCHAR(100) NULL");
        addColumnIfNotExists(tableName, "UPDATE_BY", "NVARCHAR(100) NULL");
        addColumnIfNotExists(tableName, "DELETED", "INT DEFAULT 0");

        // Add missing indexes
        addIndexIfNotExists(tableName, "IX_CertificateChange_CertificateId", "CERTIFICATEID");
        addIndexIfNotExists(tableName, "IX_CertificateChange_CertificateCode", "CERTIFICATECODE");
        addIndexIfNotExists(tableName, "IX_CertificateChange_ChangeDate", "CHANGEDATE");

        System.out.println("CertificateChange table fixed!");
    }

    private void fixCertificateContinueTable() throws SQLException {
        System.out.println("Fixing CertificateContinue table...");
        String tableName = "CertificateContinue";

        // Add missing columns
        addColumnIfNotExists(tableName, "CONTINUEDATE", "DATE NULL");
        addColumnIfNotExists(tableName, "CONTINUEMAN", "NVARCHAR(100) NULL");
        addColumnIfNotExists(tableName, "CONTINUEMANID", "BIGINT NULL");
        addColumnIfNotExists(tableName, "CHECKADVISE", "NVARCHAR(MAX) NULL");
        addColumnIfNotExists(tableName, "CHECKMAN", "NVARCHAR(100) NULL");
        addColumnIfNotExists(tableName, "CHECKDATE", "NVARCHAR(50) NULL");
        addColumnIfNotExists(tableName, "FILEPATH", "NVARCHAR(500) NULL");
        addColumnIfNotExists(tableName, "REMARK", "NVARCHAR(MAX) NULL");
        addColumnIfNotExists(tableName, "CREATE_TIME", "DATETIME NULL");
        addColumnIfNotExists(tableName, "UPDATE_TIME", "DATETIME NULL");
        addColumnIfNotExists(tableName, "CREATE_BY", "NVARCHAR(100) NULL");
        addColumnIfNotExists(tableName, "UPDATE_BY", "NVARCHAR(100) NULL");
        addColumnIfNotExists(tableName, "DELETED", "INT DEFAULT 0");

        // Add missing indexes
        addIndexIfNotExists(tableName, "IX_CertificateContinue_CertificateId", "CERTIFICATEID");
        addIndexIfNotExists(tableName, "IX_CertificateContinue_CertificateCode", "CERTIFICATECODE");
        addIndexIfNotExists(tableName, "IX_CertificateContinue_ContinueStatus", "CONTINUESTATUS");
        addIndexIfNotExists(tableName, "IX_CertificateContinue_ContinueDate", "CONTINUEDATE");

        System.out.println("CertificateContinue table fixed!");
    }

    private void addColumnIfNotExists(String tableName, String columnName, String columnDefinition) throws SQLException {
        if (columnExists(tableName, columnName)) {
            System.out.println("  Column " + columnName + " already exists in " + tableName);
            return;
        }

        try {
            String sql = String.format("ALTER TABLE %s ADD %s %s", tableName, columnName, columnDefinition);
            jdbcTemplate.execute(sql);
            System.out.println("  ✓ Added column " + columnName + " to " + tableName);
        } catch (Exception e) {
            System.err.println("  ✗ Failed to add column " + columnName + " to " + tableName);
            System.err.println("    Error: " + e.getMessage());
        }
    }

    private void addIndexIfNotExists(String tableName, String indexName, String columnName) throws SQLException {
        if (indexExists(tableName, indexName)) {
            System.out.println("  Index " + indexName + " already exists in " + tableName);
            return;
        }

        try {
            String sql = String.format("CREATE NONCLUSTERED INDEX %s ON %s(%s)", indexName, tableName, columnName);
            jdbcTemplate.execute(sql);
            System.out.println("  ✓ Added index " + indexName + " to " + tableName);
        } catch (Exception e) {
            System.err.println("  ✗ Failed to add index " + indexName + " to " + tableName);
            System.err.println("    Error: " + e.getMessage());
        }
    }

    private boolean columnExists(String tableName, String columnName) throws SQLException {
        try (Connection connection = dataSource.getConnection()) {
            DatabaseMetaData metaData = connection.getMetaData();
            try (ResultSet columns = metaData.getColumns(null, null, tableName, columnName)) {
                return columns.next();
            }
        }
    }

    private boolean indexExists(String tableName, String indexName) throws SQLException {
        try (Connection connection = dataSource.getConnection()) {
            DatabaseMetaData metaData = connection.getMetaData();
            try (ResultSet indexes = metaData.getIndexInfo(null, null, tableName, false, false)) {
                while (indexes.next()) {
                    String currentIndexName = indexes.getString("INDEX_NAME");
                    if (indexName.equalsIgnoreCase(currentIndexName)) {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}