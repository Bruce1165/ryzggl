# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

RYZGGL-C# is a Chinese government personnel qualification management system (人员资格管理系统) for managing professional certifications, examinations, and worker information. Built with .NET Framework 3.5/4.0 using ASP.NET WebForms.

## Solution Structure

The solution contains 7 projects:

- **ZYRYJG/** - Main ASP.NET WebForms application (primary web UI)
- **DataAccess/** - Data Access Layer (DAL pattern, ~150 classes)
- **Model/** - Data Models (MDL pattern, ~150 classes)
- **Utility/** - Shared utilities (HTTP, logging, encryption, Excel, etc.)
- **WS_GetData/** - Windows Service for external API data synchronization
- **WS_SynUserInfo/** - Windows Service for user info synchronization
- **RYPX/** - Secondary web module
- **Test/** - Test project

## Key Architecture Patterns

### DAL/MDL Pattern
Each entity follows the pattern:
- `[Entity]DAL.cs` - Data Access Layer class in `DataAccess/`
- `[Entity]MDL.cs` - Model class in `Model/`
- DAL uses `DBHelper.cs` for SQL operations
- Example: `ApplyDAL.cs` ↔ `ApplyMDL.cs`

### Three-Tier Architecture
```
Web UI (ZYRYJG/) → Business Logic (in DAL) → Data Access (DAL) → SQL Server
```

### External API Integration (WS_GetData)
Located in `WS_GetData/gb/` with standardized structures:
- **Request/** - API request models
- **Core.cs** - Business logic per operation
- **ResponseResult.cs** - Standard response wrapper
- **SuccessData.cs** / **ErrorData.cs** - Response payloads
- **Tzzy/** / **Aqscglry/** - Platform-specific data adapters

API operations include: Login, Update, Change, Verification, InCheck, OutCheck, Upcert, Imputation

## Build Commands

```bash
# Build entire solution (requires MSBuild on Windows)
msbuild RYZG解决方案.sln /p:Configuration=Release

# Build specific project
msbuild ZYRYJG/ZYRYJG.csproj /p:Configuration=Release

# Install Windows Services (WS_GetData, WS_SynUserInfo)
cd WS_GetData
install.bat

# Uninstall Windows Services
uninstall.bat
```

## Key Business Domains

### Apply (申请) - Application Workflows
- `ApplyDAL.cs`, `ApplyMDL.cs` - Core application entity
- `ApplyFirstDAL.cs` - Initial applications
- `ApplyChangeDAL.cs` - Change applications
- `ApplyRenewDAL.cs` - Renewal applications
- `ApplyCancelDAL.cs` - Cancellation applications
- `ApplyCheckTaskDAL.cs` - Approval tasks

### Certificate (证书) - Certificate Management
- `CertificateDAL.cs`, `CertificateMDL.cs` - Core certificate entity
- Certificate lifecycle: Enter, Change, Continue, Pause, Merge, Lock
- `CertificateHistoryDAL.cs` - Historical tracking

### Exam (考试) - Examination System
- `ExamSignUpDAL.cs` - Exam registration
- `ExamResultDAL.cs` - Results
- `ExamPlaceDAL.cs` - Exam venues
- `ExamPlanDAL.cs` - Exam scheduling

### Worker (人员/工人) - Personnel Management
- `WorkerDAL.cs`, `WorkerOB.cs` - Worker entity (OB = older model pattern)
- `DepartmentDAL.cs` - Department hierarchy
- `OrganizationDAL.cs` - Organization structure

## Database

- **RYZG.sql** - Main database schema (2.2MB)
- **RYPX.sql** - Secondary module schema
- Connection string in `DataAccess/MyWebConfig.cs`
- User-defined functions in SQL: `CheckIDCard`, `GET_CertificateContinueValidEndData`

## Common Utilities

**File:** `Utility/`
- `HttpHelp.cs` - HTTP requests for external APIs
- `FileLog.cs` - File logging
- `Cryptography.cs` - Encryption/decryption
- `ExcelDealHelp.cs` - Excel import/export
- `Check.cs` - Validation utilities (60KB)
- `JSONHelp.cs` - JSON serialization (Newtonsoft.Json wrapper)

## Configuration Files

- **app.config** - WS_GetData Windows Service configuration
- Connection strings typically in web.config (ZYRYJG) or MyWebConfig.cs

## WCF Service References (WS_GetData)

External services integrated via WCF:
- **JCSJKService** - Basic construction certification API
- **BDYService** - Construction industry API
- **FileService** - File upload/download service
- **zxjaUpdater** - Update service

## Development Notes

- Target Framework: .NET 3.5 (Model, DataAccess, Utility), .NET 4.0 (WS_GetData)
- Platform: x86 (WS_GetData), AnyCPU (most projects)
- Uses Source Code Control (SAK entries indicate VSS integration)
- Legacy codebase with established patterns - follow existing conventions

## UI (User Interface)

### Web Technologies
- **ASP.NET WebForms** - Primary UI framework
- **Telerik RadControls** - Third-party UI component suite (RadGrid, RadComboBox, RadDatePicker, RadWindowManager, RadAjaxManager)
- **jQuery 1.8.3** - Client-side scripting
- **Layer.js** - Popup/dialog library
- **ECharts** - Data visualization (echarts.min.js, echarts-all.js)
- **Highcharts** - Alternative charting library (highcharts.js, highstock.js)

### Page Structure (ZYRYJG/)

**Main directories by functionality:**
- **zjs/** - Technicians (技师) pages - 40+ ASPX files for construction workers
  - `zjsApplyList.aspx` - Application list and search
  - `zjsApplyChange.aspx` - Change applications
  - `zjsApplyContinue.aspx` - Continuation applications
  - `zjsApplyCancel.aspx` - Cancel applications
  - `zjsQualification.aspx` - Qualification management
  - `zjsCertMgr.aspx` - Certificate management
  - `zjsExamine.aspx` - Examination management
  - `zjsAgency.aspx` - Agency management
  - `zjsApplyFirst.aspx` - Initial registration
  - `zjsReportList.aspx` - Reports
- **CertifManage/** - Certificate management pages
  - `Application.aspx` - Certificate application workflow
  - `CertifChange.aspx` - Certificate change
  - `CertifChangeCheckConfirm.aspx` - Change confirmation
  - `CertifChangeCheckPatch.aspx` - Change with patch
- **Ajax/** - AJAX handlers (.ashx) for dynamic content
  - `Common.ashx` - Common AJAX operations
  - `Qualification.ashx` - Qualification dropdowns
  - `MainNews.ashx` - News feed
- **uploader/** - File upload components
  - `Upload.aspx` - Generic file upload handler

### User Controls (ASCX)
Located in root `ZYRYJG/`:
- `GridPagerTemple.ascx` - Standardized grid pagination (First/Prev/Next/Last + page number input)
- `CheckAll.ascx` - Checkbox selection control
- `SelectSource.ascx`, `PostAllSelect.ascx`, `PostSelect.ascx`, `ExamPlanSelect.ascx` - Dropdown selectors
- `ApplyNotice.ascx`, `ExamNotice.ascx` - Notice components
- `ReportSelect.ascx` - Report filtering controls
- `IframeView.ascx` - Iframe viewer for embedded content
- `myhelp.ascx` - Help/documentation popup

### BasePage - Page Lifecycle & Security
File: `ZYRYJG/BasePage.cs` (34KB)
All pages inherit from `BasePage` which handles:
- **Authentication check** - Redirects to login if not authenticated
- **Permission validation** - `ValidatePageRight()` checks URL-based access control
- **Session management** - User session and authorization tracking
- **Role-based access** - `RoleIDs` property for permission evaluation
- **Security attributes** - `CheckVisiteRgihtUrl` for authorized URL patterns

### CSS & Skins
- **css/** - Multiple CSS files (styleRed.css, style2.css, Style3.css, defaultRed.css, Grid.Default.css, gbCertView.css)
- **Skins/Blue/** - Telerik Blue theme skin
- **layer/** - Layer.js dialog skin

### Scripts (JavaScript)
Located in `ZYRYJG/Scripts/`:
- `Public.js` - Common utility functions
- `default.js` - Default behavior
- `Barrett.js`, `BigInt.js` - Number utilities
- `CodeManage.js` - Code management
- `checkBrower.js` - Browser detection
- `FloatMessage.js` - Floating notification messages

## Third-Party Dependencies

Located in `ZYRYJG/dll/`:
- `Newtonsoft.Json.dll` - JSON serialization
- `log4net.dll` - Logging
- `DocX.dll` - Word document generation
- `iTextSharp.dll` - PDF generation
- `Synergy.Common.dll` - Shared utilities
