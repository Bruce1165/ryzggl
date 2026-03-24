# Database Schema Setup Guide

## Overview

This guide provides step-by-step instructions for creating the 4 new database tables (ApplyContinue, ApplyRenew, CertificateChange, CertificateContinue).

## Prerequisites

- SQL Server access to database `RYZG` on server `101.200.193.13:1433`
- SQL Server Management Studio or similar tool
- Connection credentials (from application.yml):
  - Username: `synergydatauser`
  - Password: `synergy_p@ssword`
  - Database: `RYZG`

## Quick Execution (Method 1 - Recommended)

### Using SQL Server Management Studio

1. **Open SQL Server Management Studio**
   - Connect to `101.200.193.13,1433`
   - Use credentials above
   - Select database: `RYZG`

2. **Execute the SQL Script**
   - Open the file: `database-new-tables.sql` (located in project root)
   - Copy and paste the entire SQL script into a new query window
   - Execute by pressing `F5` (Run)

3. **Verify Tables Created**
   ```sql
   SELECT name FROM sys.tables WHERE name IN (
       'ApplyContinue',
       'ApplyRenew',
       'CertificateChange',
       'CertificateContinue'
   )
   ```

## Alternative Execution Methods

### Method 2: Using SQLCMD (Windows)

If you have `sqlcmd` installed:
```bash
sqlcmd -S 101.200.193.13,1433 -U synergydatauser -P "synergy_p@ssword" -d RYZG -i database-new-tables.sql
```

### Method 3: Using Java Application

If you want to use the Java application to execute the schema:

1. **Build the project**
   ```bash
   cd ryzggl-java
   mvn clean package -DskipTests
   ```

2. **Run the application**
   ```bash
   java -jar target/ryzggl-java-1.0.0.jar
   ```

   The `DatabaseSchemaUpdater` utility will automatically execute the SQL script on startup.

## Tables to Create

### 1. ApplyContinue (延续申请表)

| Column Name | Data Type | Description |
|-------------|-------------|-------------|
| ID | BIGINT IDENTITY(1,1) | Primary Key |
| APPLYNO | NVARCHAR(50) | 申请编号 |
| WORKERID | BIGINT | 申请人ID |
| APPLICANTNAME | NVARCHAR(100) | 申请人姓名 |
| IDCARD | NVARCHAR(50) | 身份证号 |
| PHONE | NVARCHAR(20) | 手机号 |
| UNITCODE | NVARCHAR(50) | 所属企业代码 |
| UNITNAME | NVARCHAR(200) | 企业名称 |
| CERTIFICATECODE | NVARCHAR(50) | 证书编号 |
| CONTINUETYPE | NVARCHAR(50) | 延续类型（个人、企业）|
| CONTINUESTARTDATE | DATE | 延续开始日期 |
| CONTINUEENDDATE | DATE | 延续结束日期 |
| APPLYSTATUS | NVARCHAR(50) | 申请状态 |
| CHECKADVISE | NVARCHAR(MAX) | 审批意见 |
| CHECKMAN | NVARCHAR(100) | 审批人 |
| CHECKDATE | NVARCHAR(50) | 审批时间 |
| FILEPATH | NVARCHAR(500) | 附件路径 |
| REMARK | NVARCHAR(MAX) | 备注 |
| CREATE_TIME | DATETIME | 创建时间 |
| UPDATE_TIME | DATETIME | 更新时间 |
| CREATE_BY | NVARCHAR(100) | 创建人 |
| UPDATE_BY | NVARCHAR(100) | 更新人 |
| DELETED | INT DEFAULT 0 | 删除标记 |

### 2. ApplyRenew (续期申请表)

| Column Name | Data Type | Description |
|-------------|-------------|-------------|
| ID | BIGINT IDENTITY(1,1) | Primary Key |
| APPLYNO | NVARCHAR(50) | 申请编号 |
| WORKERID | BIGINT | 申请人ID |
| APPLICANTNAME | NVARCHAR(100) | 申请人姓名 |
| IDCARD | NVARCHAR(50) | 身份证号 |
| PHONE | NVARCHAR(20) | 手机号 |
| UNITCODE | NVARCHAR(50) | 所属企业代码 |
| UNITNAME | NVARCHAR(200) | 企业名称 |
| CERTIFICATECODE | NVARCHAR(50) | 证书编号 |
| RENEWREASON | NVARCHAR(MAX) | 续期原因 |
| RENEWTYPE | NVARCHAR(50) | 续期类型 |
| RENEWSTARTDATE | DATE | 续期开始日期 |
| RENEWENDDATE | DATE | 续期结束日期 |
| APPLYSTATUS | NVARCHAR(50) | 申请状态 |
| CHECKADVISE | NVARCHAR(MAX) | 审批意见 |
| CHECKMAN | NVARCHAR(100) | 审批人 |
| CHECKDATE | NVARCHAR(50) | 审批时间 |
| FILEPATH | NVARCHAR(500) | 附件路径 |
| REMARK | NVARCHAR(MAX) | 备注 |
| CREATE_TIME | DATETIME | 创建时间 |
| UPDATE_TIME | DATETIME | 更新时间 |
| CREATE_BY | NVARCHAR(100) | 创建人 |
| UPDATE_BY | NVARCHAR(100) | 更新人 |
| DELETED | INT DEFAULT 0 | 删除标记 |

### 3. CertificateChange (证书变更表)

> **Note**: This table already exists based on API tests. The script will only add missing columns and indexes.

| Column Name | Data Type | Description |
|-------------|-------------|-------------|
| ID | BIGINT IDENTITY(1,1) | Primary Key |
| CERTIFICATEID | BIGINT | 证书ID |
| CERTIFICATECODE | NVARCHAR(50) | 证书编号 |
| CHANGETYPE | NVARCHAR(50) | 变更类型 |
| CHANGEREASON | NVARCHAR(MAX) | 变更原因 |
| OLDVALUE | NVARCHAR(MAX) | 变更前值 |
| NEWVALUE | NVARCHAR(MAX) | 变更后值 |
| CHANGEDATE | DATE | 变更日期 |
| CHANGEMAN | NVARCHAR(100) | 变更人 |
| CHANGEMANID | BIGINT | 变更人ID |
| CHECKADVISE | NVARCHAR(MAX) | 审批意见 |
| CHECKMAN | NVARCHAR(100) | 审批人 |
| CHECKDATE | NVARCHAR(50) | 审批日期 |
| FILEPATH | NVARCHAR(500) | 附件路径 |
| REMARK | NVARCHAR(MAX) | 备注 |
| CREATE_TIME | DATETIME | 创建时间 |
| UPDATE_TIME | DATETIME | 更新时间 |
| CREATE_BY | NVARCHAR(100) | 创建人 |
| UPDATE_BY | NVARCHAR(100) | 更新人 |
| DELETED | INT DEFAULT 0 | 删除标记 |

### 4. CertificateContinue (证书延续表)

| Column Name | Data Type | Description |
|-------------|-------------|-------------|
| ID | BIGINT IDENTITY(1,1) | Primary Key |
| CERTIFICATEID | BIGINT | 证书ID |
| CERTIFICATECODE | NVARCHAR(50) | 证书编号 |
| CONTINUETYPE | NVARCHAR(50) | 延续类型（年度延续、专项延续）|
| CONTINUESTARTDATE | DATE | 延续开始日期 |
| CONTINUEENDDATE | DATE | 延续结束日期 |
| CONTINUEYEARS | INT | 延续年数 |
| CONTINUEFEE | DECIMAL(10,2) | 延续费用 |
| CONTINUEREASON | NVARCHAR(MAX) | 延续原因 |
| CONTINUESTATUS | NVARCHAR(50) | 延续状态 |
| CONTINUEMAN | NVARCHAR(100) | 延续人 |
| CONTINUEMANID | BIGINT | 延续人ID |
| CONTINUEDATE | DATE | 延续日期 |
| CHECKADVISE | NVARCHAR(MAX) | 审批意见 |
| CHECKMAN | NVARCHAR(100) | 审批人 |
| CHECKDATE | NVARCHAR(50) | 审批日期 |
| FILEPATH | NVARCHAR(500) | 附件路径 |
| REMARK | NVARCHAR(MAX) | 备注 |
| CREATE_TIME | DATETIME | 创建时间 |
| UPDATE_TIME | DATETIME | 更新时间 |
| CREATE_BY | NVARCHAR(100) | 创建人 |
| UPDATE_BY | NVARCHAR(100) | 更新人 |
| DELETED | INT DEFAULT 0 | 删除标记 |

## Sample Test Data

The SQL script includes sample records for testing:

1. **ApplyContinue**: One test record with status '待确认'
2. **ApplyRenew**: One test record with status '待确认'
3. **CertificateContinue**: One test record with status '未延续'

## Verification Steps

After executing the SQL script, verify the setup:

### 1. Check Tables Exist
```sql
SELECT name FROM sys.tables
WHERE name IN ('ApplyContinue', 'ApplyRenew', 'CertificateChange', 'CertificateContinue')
```

### 2. Test API Endpoints

After tables are created, test the API endpoints:

```bash
# Test ApplyContinue
curl http://localhost:8080/api/v1/apply-continue/list

# Test ApplyRenew
curl http://localhost:8080/api/v1/apply/renew/list

# Test CertificateContinue
curl http://localhost:8080/api/v1/certificate/continue/list

# Test CertificateChange
curl http://localhost:8080/api/v1/certificate/change/statistics
```

### 3. Verify Sample Data

```sql
-- Check sample data in ApplyContinue
SELECT * FROM ApplyContinue WHERE APPLYNO = 'TC202403160001';

-- Check sample data in ApplyRenew
SELECT * FROM ApplyRenew WHERE APPLYNO = 'RN202403160001';

-- Check sample data in CertificateContinue
SELECT * FROM CertificateContinue WHERE CERTIFICATECODE = 'CERT003';
```

## Troubleshooting

### Issue: Tables not created

**Symptoms**:
- API returns error: "列名 'CREATE_TIME' 无效"
- API returns error: "列名 'CONTINUEDATE' 无效"

**Solutions**:
1. Verify SQL script executed successfully
2. Check if tables exist with correct names
3. Verify all columns are created correctly
4. Check for any error messages in SQL execution

### Issue: Maven build fails with "No plugin found for prefix 'spring-boot'"

**Solution**:
```bash
# Clear Maven cache
rm -rf ~/.m2/repository/org/springframework
# Try again
mvn clean install
```

## Next Steps After Database Setup

1. **Create Frontend Pages**
   - ApplyContinue management page
   - ApplyRenew management page
   - CertificateChange management page
   - CertificateContinue management page

2. **Implement Form Validation**
   - Client-side validation for required fields
   - Server-side validation rules

3. **Add Test Coverage**
   - Unit tests for CRUD operations
   - Integration tests for API endpoints
   - E2E tests for complete workflows

## Files Reference

- SQL Script: `database-new-tables.sql`
- Java Utility: `ryzggl-java/src/main/java/com/ryzggl/util/DatabaseSchemaUpdater.java`
- SQL Resources: `ryzggl-java/src/main/resources/sql/migration-new-tables.sql`
- Connection Config: `ryzggl-java/src/main/resources/application.yml`

## Contact

If you encounter issues:
1. Check application.yml for correct database credentials
2. Verify SQL Server connectivity
3. Review SQL Server error logs
4. Check this guide for common solutions

---

**Date**: 2026-03-16
**Version**: 1.0
