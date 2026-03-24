package com.ryzggl;

import org.junit.jupiter.api.Test;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.boot.test.context.SpringBootTest;
import org.springframework.jdbc.core.JdbcTemplate;

import javax.sql.DataSource;
import java.sql.Connection;
import java.sql.DatabaseMetaData;

import static org.junit.jupiter.api.Assertions.*;

/**
 * 数据库连接验证测试
 */
@SpringBootTest(classes = RyzgglJavaApplication.class)
public class DatabaseConnectionTest {

    @Autowired
    private DataSource dataSource;

    @Autowired
    private JdbcTemplate jdbcTemplate;

    @Test
    public void testDatabaseConnection() {
        try {
            Connection connection = dataSource.getConnection();
            assertNotNull(connection, "数据库连接失败");

            DatabaseMetaData metaData = connection.getMetaData();
            System.out.println("========================================");
            System.out.println("数据库连接验证成功!");
            System.out.println("数据库产品: " + metaData.getDatabaseProductName());
            System.out.println("数据库版本: " + metaData.getDatabaseProductVersion());
            System.out.println("驱动名称: " + metaData.getDriverName());
            System.out.println("驱动版本: " + metaData.getDriverVersion());
            System.out.println("数据库URL: " + metaData.getURL());
            System.out.println("数据库用户: " + metaData.getUserName());
            System.out.println("========================================");

            connection.close();
            assertTrue(true, "数据库连接验证通过");
        } catch (Exception e) {
            fail("数据库连接失败: " + e.getMessage());
        }
    }

    @Test
    public void testDatabaseQuery() {
        try {
            // 测试简单查询
            String query = "SELECT COUNT(*) FROM information_schema.tables WHERE table_type = 'BASE TABLE'";
            Integer tableCount = jdbcTemplate.queryForObject(query, Integer.class);

            System.out.println("========================================");
            System.out.println("数据库查询测试成功!");
            System.out.println("当前数据库表数量: " + tableCount);
            System.out.println("========================================");

            assertNotNull(tableCount, "查询结果为空");
            assertTrue(tableCount > 0, "数据库中没有表");
        } catch (Exception e) {
            fail("数据库查询失败: " + e.getMessage());
        }
    }

    @Test
    public void testCoreTablesExist() {
        try {
            // 检查核心表是否存在
            String[] coreTables = {"Apply", "Certificate", "Worker", "User", "Exam"};

            System.out.println("========================================");
            System.out.println("检查核心表是否存在...");
            System.out.println("========================================");

            for (String tableName : coreTables) {
                String query = "SELECT COUNT(*) FROM information_schema.tables WHERE table_name = ?";
                Integer count = jdbcTemplate.queryForObject(query, Integer.class, tableName);

                if (count != null && count > 0) {
                    System.out.println("✅ 表 [" + tableName + "] 存在");
                } else {
                    System.out.println("❌ 表 [" + tableName + "] 不存在");
                }
            }

            System.out.println("========================================");
        } catch (Exception e) {
            fail("检查表结构失败: " + e.getMessage());
        }
    }
}
