-- RYZGGL Database Patch Script
-- Add missing columns to existing tables
-- Date: 2026-03-16

GO

-- ============================================
-- 1. ApplyContinue - Add missing columns
-- ============================================
IF COL_LENGTH('ApplyContinue', 'CREATE_TIME') IS NULL
BEGIN
    PRINT 'Adding CREATE_TIME to ApplyContinue';
    ALTER TABLE ApplyContinue ADD CREATE_TIME DATETIME NULL;
END
GO

IF COL_LENGTH('ApplyContinue', 'UPDATE_TIME') IS NULL
BEGIN
    PRINT 'Adding UPDATE_TIME to ApplyContinue';
    ALTER TABLE ApplyContinue ADD UPDATE_TIME DATETIME NULL;
END
GO

IF COL_LENGTH('ApplyContinue', 'CREATE_BY') IS NULL
BEGIN
    PRINT 'Adding CREATE_BY to ApplyContinue';
    ALTER TABLE ApplyContinue ADD CREATE_BY NVARCHAR(100) NULL;
END
GO

IF COL_LENGTH('ApplyContinue', 'UPDATE_BY') IS NULL
BEGIN
    PRINT 'Adding UPDATE_BY to ApplyContinue';
    ALTER TABLE ApplyContinue ADD UPDATE_BY NVARCHAR(100) NULL;
END
GO

IF COL_LENGTH('ApplyContinue', 'DELETED') IS NULL
BEGIN
    PRINT 'Adding DELETED to ApplyContinue';
    ALTER TABLE ApplyContinue ADD DELETED INT DEFAULT 0;
END
GO

-- ============================================
-- 2. ApplyRenew - Add missing columns
-- ============================================
IF COL_LENGTH('ApplyRenew', 'CREATE_TIME') IS NULL
BEGIN
    PRINT 'Adding CREATE_TIME to ApplyRenew';
    ALTER TABLE ApplyRenew ADD CREATE_TIME DATETIME NULL;
END
GO

IF COL_LENGTH('ApplyRenew', 'UPDATE_TIME') IS NULL
BEGIN
    PRINT 'Adding UPDATE_TIME to ApplyRenew';
    ALTER TABLE ApplyRenew ADD UPDATE_TIME DATETIME NULL;
END
GO

IF COL_LENGTH('ApplyRenew', 'CREATE_BY') IS NULL
BEGIN
    PRINT 'Adding CREATE_BY to ApplyRenew';
    ALTER TABLE ApplyRenew ADD CREATE_BY NVARCHAR(100) NULL;
END
GO

IF COL_LENGTH('ApplyRenew', 'UPDATE_BY') IS NULL
BEGIN
    PRINT 'Adding UPDATE_BY to ApplyRenew';
    ALTER TABLE ApplyRenew ADD UPDATE_BY NVARCHAR(100) NULL;
END
GO

IF COL_LENGTH('ApplyRenew', 'DELETED') IS NULL
BEGIN
    PRINT 'Adding DELETED to ApplyRenew';
    ALTER TABLE ApplyRenew ADD DELETED INT DEFAULT 0;
END
GO

-- ============================================
-- 3. CertificateChange - Add missing columns
-- ============================================
IF COL_LENGTH('CertificateChange', 'CHANGEDATE') IS NULL
BEGIN
    PRINT 'Adding CHANGEDATE to CertificateChange';
    ALTER TABLE CertificateChange ADD CHANGEDATE DATE NULL;
END
GO

IF COL_LENGTH('CertificateChange', 'CHANGEMAN') IS NULL
BEGIN
    PRINT 'Adding CHANGEMAN to CertificateChange';
    ALTER TABLE CertificateChange ADD CHANGEMAN NVARCHAR(100) NULL;
END
GO

IF COL_LENGTH('CertificateChange', 'CHANGEMANID') IS NULL
BEGIN
    PRINT 'Adding CHANGEMANID to CertificateChange';
    ALTER TABLE CertificateChange ADD CHANGEMANID BIGINT NULL;
END
GO

IF COL_LENGTH('CertificateChange', 'CHECKADVISE') IS NULL
BEGIN
    PRINT 'Adding CHECKADVISE to CertificateChange';
    ALTER TABLE CertificateChange ADD CHECKADVISE NVARCHAR(MAX) NULL;
END
GO

IF COL_LENGTH('CertificateChange', 'CHECKMAN') IS NULL
BEGIN
    PRINT 'Adding CHECKMAN to CertificateChange';
    ALTER TABLE CertificateChange ADD CHECKMAN NVARCHAR(100) NULL;
END
GO

IF COL_LENGTH('CertificateChange', 'CHECKDATE') IS NULL
BEGIN
    PRINT 'Adding CHECKDATE to CertificateChange';
    ALTER TABLE CertificateChange ADD CHECKDATE NVARCHAR(50) NULL;
END
GO

IF COL_LENGTH('CertificateChange', 'FILEPATH') IS NULL
BEGIN
    PRINT 'Adding FILEPATH to CertificateChange';
    ALTER TABLE CertificateChange ADD FILEPATH NVARCHAR(500) NULL;
END
GO

IF COL_LENGTH('CertificateChange', 'REMARK') IS NULL
BEGIN
    PRINT 'Adding REMARK to CertificateChange';
    ALTER TABLE CertificateChange ADD REMARK NVARCHAR(MAX) NULL;
END
GO

IF COL_LENGTH('CertificateChange', 'CREATE_TIME') IS NULL
BEGIN
    PRINT 'Adding CREATE_TIME to CertificateChange';
    ALTER TABLE CertificateChange ADD CREATE_TIME DATETIME NULL;
END
GO

IF COL_LENGTH('CertificateChange', 'UPDATE_TIME') IS NULL
BEGIN
    PRINT 'Adding UPDATE_TIME to CertificateChange';
    ALTER TABLE CertificateChange ADD UPDATE_TIME DATETIME NULL;
END
GO

IF COL_LENGTH('CertificateChange', 'CREATE_BY') IS NULL
BEGIN
    PRINT 'Adding CREATE_BY to CertificateChange';
    ALTER TABLE CertificateChange ADD CREATE_BY NVARCHAR(100) NULL;
END
GO

IF COL_LENGTH('CertificateChange', 'UPDATE_BY') IS NULL
BEGIN
    PRINT 'Adding UPDATE_BY to CertificateChange';
    ALTER TABLE CertificateChange ADD UPDATE_BY NVARCHAR(100) NULL;
END
GO

IF COL_LENGTH('CertificateChange', 'DELETED') IS NULL
BEGIN
    PRINT 'Adding DELETED to CertificateChange';
    ALTER TABLE CertificateChange ADD DELETED INT DEFAULT 0;
END
GO

-- Create indexes for CertificateChange
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'CertificateChange') AND name = 'IX_CertificateChange_CertificateId')
BEGIN
    PRINT 'Creating IX_CertificateChange_CertificateId index';
    CREATE NONCLUSTERED INDEX IX_CertificateChange_CertificateId ON CertificateChange(CERTIFICATEID);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'CertificateChange') AND name = 'IX_CertificateChange_CertificateCode')
BEGIN
    PRINT 'Creating IX_CertificateChange_CertificateCode index';
    CREATE NONCLUSTERED INDEX IX_CertificateChange_CertificateCode ON CertificateChange(CERTIFICATECODE);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'CertificateChange') AND name = 'IX_CertificateChange_ChangeDate')
BEGIN
    PRINT 'Creating IX_CertificateChange_ChangeDate index';
    CREATE NONCLUSTERED INDEX IX_CertificateChange_ChangeDate ON CertificateChange(CHANGEDATE);
END
GO

-- ============================================
-- 4. CertificateContinue - Add missing columns
-- ============================================
IF COL_LENGTH('CertificateContinue', 'CONTINUEDATE') IS NULL
BEGIN
    PRINT 'Adding CONTINUEDATE to CertificateContinue';
    ALTER TABLE CertificateContinue ADD CONTINUEDATE DATE NULL;
END
GO

IF COL_LENGTH('CertificateContinue', 'CONTINUEMAN') IS NULL
BEGIN
    PRINT 'Adding CONTINUEMAN to CertificateContinue';
    ALTER TABLE CertificateContinue ADD CONTINUEMAN NVARCHAR(100) NULL;
END
GO

IF COL_LENGTH('CertificateContinue', 'CONTINUEMANID') IS NULL
BEGIN
    PRINT 'Adding CONTINUEMANID to CertificateContinue';
    ALTER TABLE CertificateContinue ADD CONTINUEMANID BIGINT NULL;
END
GO

IF COL_LENGTH('CertificateContinue', 'CHECKADVISE') IS NULL
BEGIN
    PRINT 'Adding CHECKADVISE to CertificateContinue';
    ALTER TABLE CertificateContinue ADD CHECKADVISE NVARCHAR(MAX) NULL;
END
GO

IF COL_LENGTH('CertificateContinue', 'CHECKMAN') IS NULL
BEGIN
    PRINT 'Adding CHECKMAN to CertificateContinue';
    ALTER TABLE CertificateContinue ADD CHECKMAN NVARCHAR(100) NULL;
END
GO

IF COL_LENGTH('CertificateContinue', 'CHECKDATE') IS NULL
BEGIN
    PRINT 'Adding CHECKDATE to CertificateContinue';
    ALTER TABLE CertificateContinue ADD CHECKDATE NVARCHAR(50) NULL;
END
GO

IF COL_LENGTH('CertificateContinue', 'FILEPATH') IS NULL
BEGIN
    PRINT 'Adding FILEPATH to CertificateContinue';
    ALTER TABLE CertificateContinue ADD FILEPATH NVARCHAR(500) NULL;
END
GO

IF COL_LENGTH('CertificateContinue', 'REMARK') IS NULL
BEGIN
    PRINT 'Adding REMARK to CertificateContinue';
    ALTER TABLE CertificateContinue ADD REMARK NVARCHAR(MAX) NULL;
END
GO

IF COL_LENGTH('CertificateContinue', 'CREATE_TIME') IS NULL
BEGIN
    PRINT 'Adding CREATE_TIME to CertificateContinue';
    ALTER TABLE CertificateContinue ADD CREATE_TIME DATETIME NULL;
END
GO

IF COL_LENGTH('CertificateContinue', 'UPDATE_TIME') IS NULL
BEGIN
    PRINT 'Adding UPDATE_TIME to CertificateContinue';
    ALTER TABLE CertificateContinue ADD UPDATE_TIME DATETIME NULL;
END
GO

IF COL_LENGTH('CertificateContinue', 'CREATE_BY') IS NULL
BEGIN
    PRINT 'Adding CREATE_BY to CertificateContinue';
    ALTER TABLE CertificateContinue ADD CREATE_BY NVARCHAR(100) NULL;
END
GO

IF COL_LENGTH('CertificateContinue', 'UPDATE_BY') IS NULL
BEGIN
    PRINT 'Adding UPDATE_BY to CertificateContinue';
    ALTER TABLE CertificateContinue ADD UPDATE_BY NVARCHAR(100) NULL;
END
GO

IF COL_LENGTH('CertificateContinue', 'DELETED') IS NULL
BEGIN
    PRINT 'Adding DELETED to CertificateContinue';
    ALTER TABLE CertificateContinue ADD DELETED INT DEFAULT 0;
END
GO

-- Create indexes for CertificateContinue
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'CertificateContinue') AND name = 'IX_CertificateContinue_CertificateId')
BEGIN
    PRINT 'Creating IX_CertificateContinue_CertificateId index';
    CREATE NONCLUSTERED INDEX IX_CertificateContinue_CertificateId ON CertificateContinue(CERTIFICATEID);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'CertificateContinue') AND name = 'IX_CertificateContinue_CertificateCode')
BEGIN
    PRINT 'Creating IX_CertificateContinue_CertificateCode index';
    CREATE NONCLUSTERED INDEX IX_CertificateContinue_CertificateCode ON CertificateContinue(CERTIFICATECODE);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'CertificateContinue') AND name = 'IX_CertificateContinue_ContinueStatus')
BEGIN
    PRINT 'Creating IX_CertificateContinue_ContinueStatus index';
    CREATE NONCLUSTERED INDEX IX_CertificateContinue_ContinueStatus ON CertificateContinue(CONTINUESTATUS);
END
GO

IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'CertificateContinue') AND name = 'IX_CertificateContinue_ContinueDate')
BEGIN
    PRINT 'Creating IX_CertificateContinue_ContinueDate index';
    CREATE NONCLUSTERED INDEX IX_CertificateContinue_ContinueDate ON CertificateContinue(CONTINUEDATE);
END
GO

PRINT '========================================';
PRINT 'Database patch completed successfully!';
PRINT '========================================';
GO