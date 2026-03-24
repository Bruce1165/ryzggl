-- RYZGGL Database Migration Script
-- New tables for ApplyContinue, ApplyRenew, CertificateChange, CertificateContinue
-- Date: 2026-03-16
-- Database: RYZG (SQL Server)

GO

-- ============================================
-- 1. ApplyContinue (延续申请表)
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ApplyContinue') AND type = 'U')
BEGIN
    PRINT 'Creating table: ApplyContinue';
END
GO

CREATE TABLE ApplyContinue (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY,
    APPLYNO NVARCHAR(50) NULL,                        -- 申请编号
    WORKERID BIGINT NULL,                             -- 申请人ID
    APPLICANTNAME NVARCHAR(100) NULL,                   -- 申请人姓名
    IDCARD NVARCHAR(50) NULL,                         -- 身份证号
    PHONE NVARCHAR(20) NULL,                          -- 手机号
    UNITCODE NVARCHAR(50) NULL,                         -- 所属企业代码
    UNITNAME NVARCHAR(200) NULL,                        -- 企业名称
    CERTIFICATECODE NVARCHAR(50) NULL,                    -- 证书编号
    CONTINUETYPE NVARCHAR(50) NULL,                       -- 延续类型（个人、企业）
    CONTINUESTARTDATE DATE NULL,                         -- 延续开始日期
    CONTINUEENDDATE DATE NULL,                           -- 延续结束日期
    APPLYSTATUS NVARCHAR(50) NULL,                       -- 申请状态（未填写、待确认、已申报、已受理、已驳回）
    CHECKADVISE NVARCHAR(MAX) NULL,                       -- 审批意见
    CHECKMAN NVARCHAR(100) NULL,                          -- 审批人
    CHECKDATE NVARCHAR(50) NULL,                          -- 审批时间
    FILEPATH NVARCHAR(500) NULL,                          -- 附件路径
    REMARK NVARCHAR(MAX) NULL,                           -- 备注
    CREATE_TIME DATETIME NULL,                           -- 创建时间
    UPDATE_TIME DATETIME NULL,                           -- 更新时间
    CREATE_BY NVARCHAR(100) NULL,                         -- 创建人
    UPDATE_BY NVARCHAR(100) NULL,                         -- 更新人
    DELETED INT DEFAULT 0                                  -- 删除标记 (0-正常, 1-已删除)
);
GO

-- Create indexes for ApplyContinue
CREATE NONCLUSTERED INDEX IX_ApplyContinue_WorkerId ON ApplyContinue(WORKERID);
CREATE NONCLUSTERED INDEX IX_ApplyContinue_CertificateCode ON ApplyContinue(CERTIFICATECODE);
CREATE NONCLUSTERED INDEX IX_ApplyContinue_ApplyStatus ON ApplyContinue(APPLYSTATUS);
CREATE NONCLUSTERED INDEX IX_ApplyContinue_CreateTime ON ApplyContinue(CREATE_TIME);
GO

-- ============================================
-- 2. ApplyRenew (续期申请表)
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'ApplyRenew') AND type = 'U')
BEGIN
    PRINT 'Creating table: ApplyRenew';
END
GO

CREATE TABLE ApplyRenew (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY,
    APPLYNO NVARCHAR(50) NULL,                        -- 申请编号
    WORKERID BIGINT NULL,                             -- 申请人ID
    APPLICANTNAME NVARCHAR(100) NULL,                   -- 申请人姓名
    IDCARD NVARCHAR(50) NULL,                         -- 身份证号
    PHONE NVARCHAR(20) NULL,                          -- 手机号
    UNITCODE NVARCHAR(50) NULL,                         -- 所属企业代码
    UNITNAME NVARCHAR(200) NULL,                        -- 企业名称
    CERTIFICATECODE NVARCHAR(50) NULL,                    -- 证书编号
    RENEWREASON NVARCHAR(MAX) NULL,                      -- 续期原因
    RENEWTYPE NVARCHAR(50) NULL,                        -- 续期类型
    RENEWSTARTDATE DATE NULL,                             -- 续期开始日期
    RENEWENDDATE DATE NULL,                               -- 续期结束日期
    APPLYSTATUS NVARCHAR(50) NULL,                       -- 申请状态（未填写、待确认、已申报、已受理、已驳回）
    CHECKADVISE NVARCHAR(MAX) NULL,                       -- 审批意见
    CHECKMAN NVARCHAR(100) NULL,                          -- 审批人
    CHECKDATE NVARCHAR(50) NULL,                          -- 审批时间
    FILEPATH NVARCHAR(500) NULL,                          -- 附件路径
    REMARK NVARCHAR(MAX) NULL,                           -- 备注
    CREATE_TIME DATETIME NULL,                           -- 创建时间
    UPDATE_TIME DATETIME NULL,                           -- 更新时间
    CREATE_BY NVARCHAR(100) NULL,                         -- 创建人
    UPDATE_BY NVARCHAR(100) NULL,                         -- 更新人
    DELETED INT DEFAULT 0                                  -- 删除标记 (0-正常, 1-已删除)
);
GO

-- Create indexes for ApplyRenew
CREATE NONCLUSTERED INDEX IX_ApplyRenew_WorkerId ON ApplyRenew(WORKERID);
CREATE NONCLUSTERED INDEX IX_ApplyRenew_CertificateCode ON ApplyRenew(CERTIFICATECODE);
CREATE NONCLUSTERED INDEX IX_ApplyRenew_ApplyStatus ON ApplyRenew(APPLYSTATUS);
CREATE NONCLUSTERED INDEX IX_ApplyRenew_CreateTime ON ApplyRenew(CREATE_TIME);
GO

-- ============================================
-- 3. CertificateChange (证书变更表)
-- ============================================
-- Note: This table already exists based on API test results
-- This script adds missing columns if needed
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'CertificateChange') AND type = 'U')
BEGIN
    PRINT 'Table CertificateChange already exists, checking columns...';
END
GO

-- Check and add missing columns if table exists
IF COL_LENGTH('CertificateChange', 'CHANGETYPE') IS NULL
BEGIN
    ALTER TABLE CertificateChange ADD CHANGETYPE NVARCHAR(50) NULL;
END
GO

-- Create indexes if they don't exist
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'CertificateChange') AND name = 'IX_CertificateChange_CertificateId')
BEGIN
    CREATE NONCLUSTERED INDEX IX_CertificateChange_CertificateId ON CertificateChange(CERTIFICATEID);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'CertificateChange') AND name = 'IX_CertificateChange_CertificateCode')
BEGIN
    CREATE NONCLUSTERED INDEX IX_CertificateChange_CertificateCode ON CertificateChange(CERTIFICATECODE);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'CertificateChange') AND name = 'IX_CertificateChange_ChangeDate')
BEGIN
    CREATE NONCLUSTERED INDEX IX_CertificateChange_ChangeDate ON CertificateChange(CHANGEDATE);
END
GO

-- ============================================
-- 4. CertificateContinue (证书延续表)
-- ============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'CertificateContinue') AND type = 'U')
BEGIN
    PRINT 'Creating table: CertificateContinue';
END
GO

CREATE TABLE CertificateContinue (
    ID BIGINT IDENTITY(1,1) PRIMARY KEY,
    CERTIFICATEID BIGINT NULL,                           -- 证书ID
    CERTIFICATECODE NVARCHAR(50) NULL,                    -- 证书编号
    CONTINUETYPE NVARCHAR(50) NULL,                       -- 延续类型（年度延续、专项延续）
    CONTINUESTARTDATE DATE NULL,                         -- 延续开始日期
    CONTINUEENDDATE DATE NULL,                           -- 延续结束日期
    CONTINUEYEARS INT NULL,                                -- 延续年数
    CONTINUEFEE DECIMAL(10,2) NULL,                      -- 延续费用
    CONTINUEREASON NVARCHAR(MAX) NULL,                     -- 延续原因
    CONTINUESTATUS NVARCHAR(50) NULL,                       -- 延续状态（未延续、已延续、已驳回）
    CONTINUEMAN NVARCHAR(100) NULL,                        -- 延续人
    CONTINUEMANID BIGINT NULL,                            -- 延续人ID
    CONTINUEDATE DATE NULL,                               -- 延续日期
    CHECKADVISE NVARCHAR(MAX) NULL,                       -- 审批意见
    CHECKMAN NVARCHAR(100) NULL,                          -- 审批人
    CHECKDATE NVARCHAR(50) NULL,                          -- 审批日期
    FILEPATH NVARCHAR(500) NULL,                          -- 附件路径
    REMARK NVARCHAR(MAX) NULL,                           -- 备注
    CREATE_TIME DATETIME NULL,                           -- 创建时间
    UPDATE_TIME DATETIME NULL,                           -- 更新时间
    CREATE_BY NVARCHAR(100) NULL,                         -- 创建人
    UPDATE_BY NVARCHAR(100) NULL,                         -- 更新人
    DELETED INT DEFAULT 0                                  -- 删除标记 (0-正常, 1-已删除)
);
GO

-- Create indexes for CertificateContinue
CREATE NONCLUSTERED INDEX IX_CertificateContinue_CertificateId ON CertificateContinue(CERTIFICATEID);
CREATE NONCLUSTERED INDEX IX_CertificateContinue_CertificateCode ON CertificateContinue(CERTIFICATECODE);
CREATE NONCLUSTERED INDEX IX_CertificateContinue_ContinueStatus ON CertificateContinue(CONTINUESTATUS);
CREATE NONCLUSTERED INDEX IX_CertificateContinue_ContinueDate ON CertificateContinue(CONTINUEDATE);
GO

-- ============================================
-- Insert sample test data (optional)
-- ============================================

-- Sample ApplyContinue record
INSERT INTO ApplyContinue (APPLYNO, WORKERID, APPLICANTNAME, CERTIFICATECODE, CONTINUETYPE,
                              CONTINUESTARTDATE, CONTINUEENDDATE, APPLYSTATUS, CREATE_TIME)
VALUES ('TC202403160001', 1, '张三', 'CERT001', '个人', '2024-03-01', '2026-03-01', '待确认', GETDATE());
GO

-- Sample ApplyRenew record
INSERT INTO ApplyRenew (APPLYNO, WORKERID, APPLICANTNAME, CERTIFICATECODE, RENEWREASON,
                           RENEWTYPE, RENEWSTARTDATE, RENEWENDDATE, APPLYSTATUS, CREATE_TIME)
VALUES ('RN202403160001', 1, '李四', 'CERT002', '证书即将到期', '续期', '2024-03-01', '2025-03-01', '待确认', GETDATE());
GO

-- Sample CertificateContinue record
INSERT INTO CertificateContinue (CERTIFICATEID, CERTIFICATECODE, CONTINUETYPE, CONTINUEYEARS,
                                 CONTINUEFEE, CONTINUESTATUS, CONTINUEMAN, CONTINUEDATE, CREATE_TIME)
VALUES (1, 'CERT003', '年度延续', 3, 100.00, '未延续', '管理员', GETDATE(), GETDATE());
GO

PRINT '========================================';
PRINT 'New tables created successfully!';
PRINT '========================================';
PRINT '';
PRINT 'Tables created:';
PRINT '  - ApplyContinue (延续申请表)';
PRINT '  - ApplyRenew (续期申请表)';
PRINT '  - CertificateChange (证书变更表) - checked and enhanced';
PRINT '  - CertificateContinue (证书延续表)';
PRINT '';
PRINT 'Next steps:';
PRINT '1. Test the API endpoints for each table';
PRINT '2. Verify data insertion and retrieval';
PRINT '3. Create frontend pages for these entities';
PRINT '========================================';
GO
