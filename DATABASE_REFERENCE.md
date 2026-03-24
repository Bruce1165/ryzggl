# Database Reference - RYZGGL-C# System

## Overview

This document provides comprehensive database schema reference for the RYZGGL (人员资格管理) system, extracted from analysis of RYZG.sql and RYPX.sql database files.

---

## Database Structure

### Main Database: RYZG
- **Server**: 101.200.193.13
- **Database**: RYZG
- **User**: synergydatauser
- **Tables**: ~150+
- **Views**: 30+
- **Functions**: 3 user-defined functions

### Secondary Database: RYPX
- **Database**: RYPX
- **Connection**: Same server (101.200.193.13), same user
- **Purpose**: Training/continuation module
- **Tables**: ~25

---

## Core Entity Domains

### 1. Application Workflow (Apply* Tables)

#### Apply - Core Application Table
**File**: Apply.cs / ApplyMDL.cs / ApplyDAL.cs

**Key Fields**:
- `APPLYID` - Primary key (IDENTITY)
- `WORKERID` - Foreign key to Worker table
- `UNITCODE` - Department/Unit code
- `POSTTYPEID` - Position type
- `POSTID` - Position
- `APPLYSTATUS` - Application status
- `CHECKMAN`, `CHECKADVISE` - Approval workflow
- `CHECKDATE` - Approval timestamp
- `PRINTMAN`, `PRINTDATE` - Print workflow
- `CREATETIME`, `MODIFYTIME` - Audit trails

**Status Values**: 未填写, 待确认, 未申报, 已申报, 已受理, 已驳回

#### Apply-Related Tables
- `Apply_DelHis` - Historical (deleted) applications
- `ApplyFirst` - Initial registration applications
- `ApplyChange` - Change applications
- `ApplyContinue` - Continuation applications
- `ApplyRenew` - Renewal applications
- `ApplyCancel` - Cancellation applications
- `ApplyReplace` - Replacement applications
- `ApplyFile` - File attachments for applications
- `ApplyCheckTask`, `ApplyCheckTaskItem` - Multi-step approval tasks
- `ApplyAddItem` - Additional items in applications
- `ApplyNews` - Application news/announcements
- `ApplyForSheBaoCheck` - For Shenyang (沈阳) specific checks

---

### 2. Certificate Management (CERTIFICATE* Tables)

#### CERTIFICATE - Main Certificate Table
**File**: Certificate.cs / CertificateOB.cs / CertificateDAL.cs

**Key Fields**:
- `CERTIFICATEID` - Primary key (IDENTITY)
- `WORKERID` - Linked worker
- `CERTIFICATETYPE` - Certificate type
- `POSTTYPEID`, `POSTID` - Position information
- `CERTIFICATECODE` - Certificate number
- `WORKERNAME` - Worker name
- `SEX`, `BIRTHDAY` - Personal information
- `UNITNAME`, `CONFERUNIT` - Organization information
- `CONFERDATE` - Issuance date
- `VALIDSTARTDATE`, `VALIDENDDATE` - Certificate validity period
- `STATUS` - Current certificate status
- `WORKERCERTIFICATECODE` - Associated worker certificate
- `UNITCODE` - Department/unit code
- `CHECKMAN`, `CHECKADVISE` - Review/verification workflow
- `CHECKDATE`, `PRINTDATE` - Process timestamps
- `CREATEPERSONID`, `MODIFYPERSONID`, `MODIFYTIME` - Audit trails

#### Certificate Lifecycle Tables
- `CERTIFICATE_TZZY` - Specialty (特种作业) certificates
- `CERTIFICATEADDITEM` - Additional certificate items
- `CERTIFICATECHANGE`, `CERTIFICATECHANGE_DEL`, `CERTIFICATECHANGE_HIS` - Certificate changes with history
- `CERTIFICATECONTINUE`, `CERTIFICATECONTINUE_DEL`, `CERTIFICATECONTINUE_HIS` - Certificate renewals with history
- `CERTIFICATEENTERAPPLY`, `CERTIFICATEENTERAPPLY_DEL` - New certificate applications
- `CERTIFICATEHISTORY` - Historical certificate records
- `CERTIFICATELOCK`, `CERTIFICATELOCK_HIS`, `CertificateLock_JZS` - Certificate locking for administrative control

#### Certificate Operations
- `CertificateOut`, `CertificateOutApply` - Certificate issuance/print
- `CertificatePause` - Certificate suspension
- `CertificateMerge` - Certificate merging
- `CertificateMore`, `CertificateMore` - Additional certificate attributes

---

### 3. Examination System (EXAM* Tables)

#### Core Exam Tables
- `EXAMPAGE` - Exam plans/periods
- `EXAMPAGEQUESTIONTYPE` - Question types for exams
- `EXAMPLACE` - Exam venues (location, capacity, contacts)
- `EXAMPAGETITLE` - Exam titles/names
- `EXAMYEAR`, `SUBJECTID` - Scheduling
- `EXAMSIGNUP` - Exam registration
- `EXAMRESULT` - Exam results and scoring
- `EXAMRANGE`, `EXAMRANGESUB` - Exam scope/coverage
- `EXAMPLAN` - Exam plans and schedules
- `EXAMPLANSUBJECT` - Subject assignments to exam plans
- `EXAMPAGEDETAIL` - Exam details

#### Exam Support Tables
- `EXAMROOM` - Room allocation
- `EXAMROOMALLOT` - Room assignment operations
- `EXAMPAGEQUESTION` - Question bank
- `EXAMPAGESUBJECT` - Subject mapping
- `EXAMPAGETEMPLATE` - Exam templates
- `EXAMOUTLINE` - Exam syllabus/outlines
- `EXAMPAGE` - Exam page management
- `EXAMPAGEQUESTIONTYPE` - Question categorization

---

### 4. Worker/Personnel Management

#### Worker Information
- `Worker` (or `WorkerOB`) - Core worker entity
- `WorkerLock` - Locked/administrative control
- `WorkerSet` - Worker settings/configuration
- `Department` - Organizational departments
- `Organization` - Organizations/enterprises
- `PostInfo` - Position/role definitions
- `Qualification` - Qualification records
- `ForeignCertificate` - External certifications
- `ForeignCertificateHistory` - History of external certs
- `ForeignPostInfo` - Position information

#### Personnel Reference Tables
- `RY_ZCJZSDAL` - ZC (造材) personnel
- `zjs_*` - Technicians (技师) related tables (separate data structure)
- `GYPXWhite` - White-list management

---

### 5. Enterprise/Organization Data (COC_ONE_* Tables)

#### Enterprise Information System
- `COC_ONE_ENT_BaseInfo` - Enterprise base information
  - Fields: ENT_Name, ENT_OrganizationsCode, ENT_Economic_Nature, ENT_Province/City, ENT_Address, ENT_Contact, ENT_Telephone, ENT_MobilePhone, ENT_Type, ENT_Sort, ENT_Grade
- `COC_ONE_Person_BaseInfo` - Personnel within enterprise
  - Fields: PSN_Name, PSN_Sex, PSN_BirthDate, PSN_National, PSN_CertificateType, PSN_CertificateNO, PSN_GraduationSchool, PSN_Specialty, PSN_GraduationTime
- `COC_ONE_Register_Profession` - Registered professions
- `COC_TOW_Person_BaseInfo`, `COC_TOW_Register_Profession_His` - Temporary/History personnel data
- `COC_ONE_Person_File` - Attached documents
- `COC_TOW_Person_BaseInfo_Declare` - Personnel declarations
- `COC_ONE_Temporary_Person_BaseInfo` - Temporary worker data
- `QY_HYLSGX` - Industry relation data

#### Foreign Reference
- `COC_TOW` - Two-way cooperation (tow=对外的缩写) enterprises
- `COC_TOW_Person_BaseInfo_His` - Historical personnel data

---

### 6. Training & Learning (Finish* Tables in RYPX)

#### Training Records
- `FinishCert` - Certificate training completion
- `FinishSourceWare` - Training source/course records
- `FinishSourceWareHis` - Historical training sources

**Key Fields**:
- `WorkerCertificateCode` - Link to certificate
- `SourceID` - Training source/course ID
- `Period` - Training duration
- `FinishPeriod` - Completion period
- `StudyStatus` - Learning status (0=pending, 1=completed)
- `PlayAction` - Training actions

---

### 7. Support & Configuration Tables

#### System Configuration
- `Types` - System types/enums
- `UnitCodeSet` - Unit code configurations
- `Dictionary` - System dictionary values
- `NewSetUp` - New configuration settings
- `Package` - Configuration packages
- `PackageSource` - Configuration sources
- `BATCHNUMBER` - Batch number management

#### Data Sync & Reference
- `beijing` - Beijing-specific data reference
- `CertBScanJZS`, `CertBScanJZSHis` - Certificate scan records for JZS (construction技师)
- `jcsjk_*` - Construction data integration tables
  - `jcsjk_QY_ZHXX` - Enterprise registration info
  - `jcsjk_RY_JZS_ZSSD` - Personnel position data
  - `jcsjk_jzs` - Certificate records
  - `jcsjk_tj_qy_jzs` - Statistics/aggregation
  - `jcsjk_tj_qy_aqxkwdb` - Dangerous operation records

#### Audit & Logging
- `OperateLog`, `OperateLogOB` - User operation logs
- `InterFaceLog` - API/interface call logs
- `FileDownLog` - File download logs
- `TJ_Visit` - Visit tracking statistics
- `TJ_MissExamLock` - Exam lock tracking
- `InfoTag` - Information tagging system
- `FileInfo` - File information records

#### Quality Control
- `CheckFeedBack` - Feedback collection
- `CheckFeedBack_ing` - In-progress feedback
- `CheckObject` - Review object assignments
- `BlackList` - Blacklist management

---

### 8. User Management

#### User Tables
- `User` - System users
- `UserOB` - User information (older pattern)
- `UserRole` - User role assignments
- `Role` - System roles
- `RoleResource` - Role-to-resource mapping
- `ZYPX_Sys_User` - User info sync (in RYPX)
- `Gx_Task_ToZczxDAL` - Task to center data transfer

---

## User-Defined Database Functions

### 1. CheckIDCard - ID Card Validation
**Location**: RYZG database

**Purpose**: Validates Chinese resident ID cards (15 or 18 digits)

**Logic**:
```sql
- 15 digits: Validates birth date (YYYYMMDD format) and numeric characters
- 18 digits: Validates checksum using factors "79A584216379A5842" and modulus 11
  - Uses weighted multiplication (2, 4, 8, 5, 10, 9, 2, 6, 8)
  - Returns 1 if valid, 0 if invalid
```

**Use**: Validate worker, personnel, and certificate applicant ID cards throughout the system.

### 2. GET_CertificateContinueValidEndDate - Certificate Validity Calculation

**Location**: RYZG database

**Parameters**:
- `PostTypeID` - Position type
- `PostID` - Position
- `Sex` - Gender
- `BIRTHDAY` - Date of birth
- `VALIDENDDATE` - Current validity end date
- `UnitCode` - Organization code
- `WorkerName` - Worker name

**Purpose**: Calculate certificate renewal validity end date based on age and post type.

**Business Rules**:
```
安管人员
├── 法人A (PostID=148): Certificate valid + 3 years (no age limit)
├── 法人A (Other): Certificate valid + 3 years
└── 法人B (Beijing, Tianjin, etc.):
    ├── Female: Age <= 55 → Valid + 3 years
    └── Male: Age <= 60 → Valid + 3 years
    ├── Age > limit → Certificate valid until age limit date

特种作业
├── Female: Age <= 50 → Valid + 2 years
└── Male: Age <= 60 → Valid + 2 years
    ├── Age > limit → Certificate valid until age limit date
```

**Use**: Automatic certificate renewal validity calculation and age validation for certificate applications.

### 3. ID15TO18 - ID Card Format Conversion

**Location**: RYZG database

**Purpose**: Converts 15-digit ID card (old format) to 18-digit format.

**Logic**: Inserts province code at position 6 based on birth date region.

---

## Database Views (30+)

### Application Views
- `View_ApplyChange` - Application change history
- `View_CongYe_Check` - Application review status
- `View_CheckFeedBack` - Feedback aggregation
- `View_CheckTaskTj` - Check task tracking by technician
- `View_CheckTaskTjByCountry` - Check tasks by region

### Certificate Views
- `VIEW_CERTIFICATECHANGE` - Certificate change history view
- `VIEW_CERTIFICATECHANGE_DEL` - Deleted certificate changes
- `VIEW_CERTIFICATECONTINUE_DEL` - Deleted certificate renewals

### Exam Views
- `VIEW_EXAMSCORE` - Exam scores and results
- `VIEW_EXAMRESULT` - Exam results with details
- `VIEW_EXAMRESULT_Operation` - Exam result operations
- `VIEW_EXAMSCORE_Cert` - Certificate-related exam scores
- `VIEW_EXAMSCORE_WithFR` - Exam scores with French (法资) context
- `VIEW_EXAMSIGNUP_NEW` - New exam registrations
- `VIEW_QY_MAN_CHECK` - Quality management check view
- `VIEW_QY_MAN_CHECK_RESULT` - Quality management results

### Personnel Views
- `View_JZS_OneLevel` - Technician (技师) one-level hierarchy
- `View_JZS_OneTempLevel` - Temporary technician records
- `View_JZS_TOW_WithProfession` - Technicians with profession
- `View_JZS_TOW_ApplyFirst` - Technician first applications
- `View_JZS_TOW_Applying` - Technician applications in progress
- `View_JZS_TOW_Check` - Technician review status
- `View_JZS_TOW_Print` - Print records
- `View_JZS_Use` - Technician usage records
- `View_JZS_ONE` - Enterprise personnel
- `View_JZS_TOW` - Technician enterprise data

### Organization Views
- `View_FR` - French (法资) enterprise reference
- `View_SixPeople` - "Six people" (六类人员) data
- `View_QYLSGX` - Industry relation data view

---

## Key Relationships

### Foreign Key Patterns

**Application → Worker**:
- `Apply.WORKERID → Worker.WORKERID`
- Many applications per worker over time

**Certificate → Worker**:
- `CERTIFICATE.WORKERID → Worker.WORKERID`
- One certificate per worker (active)

**Exam → Worker**:
- `EXAMSIGNUP.WORKERID → Worker.WORKERID`
- Multiple exam records per worker

**Certificate History**:
- `CERTIFICATECHANGE.CERTIFICATEID → CERTIFICATE.CERTIFICATEID`
- `CERTIFICATECONTINUE.CERTIFICATEID → CERTIFICATE.CERTIFICATEID`
- `CERTIFICATEHISTORY.CERTIFICATEID → CERTIFICATE.CERTIFICATEID`

**Personnel → Organization**:
- `Worker.UNITCODE → Department.UNITCODE`
- `Department.ORGANIZATIONID → Organization.ORGANIZATIONID`

---

## Data Types and Patterns

### Primary Keys
Most tables use `IDENTITY(1,1)` for auto-incrementing bigint primary keys.

### Common Fields
- `CREATEPERSONID`, `CREATETIME` - Creation tracking
- `MODIFYPERSONID`, `MODIFYTIME` - Last modification audit
- `UNITCODE` - Department/unit reference (varchar(100))
- `POSTTYPEID`, `POSTID` - Position reference

### Status Columns
- `STATUS` - varchar(100) for flexible status values
- `FLAG` - varchar(100) for boolean/flag values
- `STUDYSTATUS` - int for study/learning states

### Naming Conventions
- Tables: PascalCase (Apply, Certificate, Exam, Worker)
- Columns: PascalCase or snake_case mixed (CERTIFICATEID, ValidEndDate, WorkerName)
- No consistent pattern - indicates legacy/evolved codebase

---

## Migration Notes for Spring Boot

### JPA Entity Mapping Priority

**Phase 1 - Core Entities** (Start with these):
1. `Apply` → `ApplyEntity`
2. `Certificate` → `CertificateEntity`
3. `Worker` → `WorkerEntity`
4. `Department` → `DepartmentEntity`
5. `Organization` → `OrganizationEntity`

**Phase 2 - Application Subtypes**:
6. All `Apply*` tables → separate entities or `@DiscriminatorColumn` pattern
7. All `CERTIFICATE*` tables → lifecycle entities

**Phase 3 - Examination**:
8. `EXAM*` tables → exam domain entities

**Phase 4 - Enterprise**:
9. `COC_ONE_*` tables → enterprise domain entities

### Column Type Mapping

| SQL Server Type | JPA Type | Notes |
|---------------|-----------|-------|
| bigint | Long/BigInteger | Auto-generated IDs |
| int | Integer | Status codes, counts |
| varchar(n) | String | Length as needed, @Size(n) |
| nvarchar(n) | String | Unicode support (names, addresses) |
| datetime | LocalDateTime/Timestamp | Use @Temporal annotations |
| bit | Boolean | 0=false, 1=true |

### Index Recommendations
Add `@Index` annotations for frequently queried columns:
- `Apply` table: `UNITCODE`, `WORKERID`, `POSTTYPEID`, `APPLYSTATUS`
- `Certificate` table: `WORKERID`, `CERTIFICATECODE`, `UNITCODE`, `VALIDENDDATE`
- `Worker` table: `UNITCODE`, `WORKERNAME`
- `EXAMSIGNUP` table: `WORKERID`, `EXAMYEAR`

### Validation Strategy
Port validation logic from database functions:
1. Create `@PrePersist` and `@PreUpdate` methods in entities
2. Call `CheckIDCard` equivalent service for ID validation
3. Implement certificate validity calculation service
4. Add `@NotNull` and `@Size` constraints matching DB schema
