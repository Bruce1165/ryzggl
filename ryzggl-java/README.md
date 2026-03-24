# RYZGGL Java - Migration Documentation

## Overview

This project is a Spring Boot migration of the RYZGGL-C# .NET personnel qualification management system (人员资格管理系统). The migration maintains full functional parity with the legacy system while modernizing the technology stack.

## Technology Stack

| Component | .NET Legacy | Spring Boot Migration |
|------------|--------------|------------------------|
| **Framework** | ASP.NET WebForms 3.5 | Spring Boot 3.2.0 |
| **Language** | C# | Java 17 |
| **Database** | SQL Server (ADO.NET) | SQL Server (MyBatis-Plus) |
| **UI Library** | Telerik RadControls | Vue.js 3 + Element Plus |
| **ORM** | Manual SQL in DAL | MyBatis-Plus (JPA) |
| **Security** | Session + Role-based | JWT + Role-based |
| **API Style** | Page postbacks | RESTful APIs |
| **Build Tool** | MSBuild | Maven/Gradle |

## Project Structure

```
ryzggl-java/
├── pom.xml                      # Maven configuration
├── src/main/
│   ├── java/com/ryzggl/
│   │   ├── common/           # Shared components
│   │   │   ├── BaseEntity.java      # Base entity with audit fields
│   │   │   ├── Result.java        # Unified API response wrapper
│   │   │   └── ...
│   │   ├── entity/           # JPA entities (maps to DB tables)
│   │   │   ├── Apply.java      # Core application table
│   │   │   ├── ApplyFirst.java # Initial registration
│   │   │   ├── ApplyChange.java # Change applications
│   │   │   ├── Certificate.java  # Certificate management
│   │   │   └── ... (see DATABASE_REFERENCE.md)
│   │   ├── repository/       # MyBatis mappers
│   │   │   ├── ApplyRepository.java
│   │   │   ├── CertificateRepository.java
│   │   │   └── ...
│   │   ├── service/          # Business logic layer
│   │   │   ├── ApplyService.java  # Application workflows
│   │   │   ├── CertificateService.java  # Certificate lifecycle
│   │   │   └── ...
│   │   ├── controller/       # REST API controllers
│   │   │   ├── ApplyController.java
│   │   │   └── ...
│   │   └── config/           # Configuration
│   │       ├── JwtAuthenticationFilter.java  # JWT filter
│   │       ├── JwtTokenProvider.java    # Token generation/validation
│   │       ├── SecurityConfig.java   # Spring Security config
│   │       └── ...
│   └── resources/
│       ├── application.yml        # Database & JWT configuration
│       ├── mapper/*.xml         # MyBatis mapper XMLs (if needed)
│       └── static/            # Vue.js frontend assets
└── DATABASE_REFERENCE.md      # Complete database schema documentation
```

## Key Architecture Decisions

### 1. Entity Mapping Strategy
**Choice**: Table-per-entity (150+ entities)

**Rationale**:
- Matches .NET pattern (1 entity per table)
- Simplifies MyBatis mapping
- Clear separation of concerns
- Easier to add database indexes per entity

**Discriminator Column Approach** (for subtypes):
For tables with multiple subtypes (Apply, Certificate):
- Use single entity with `@TableField` for specific columns
- Or use MyBatis-Plus `@TableNameCondition` for conditional queries
- Current implementation: Separate entities (Apply, ApplyFirst, ApplyChange)

### 2. Authentication & Authorization

**Legacy Pattern** (BasePage):
```csharp
- Session-based auth
- URL-based permission checking (ValidatePageRight)
- RoleIDs string in session
- Redirect on unauthorized access
```

**Spring Boot Implementation**:
```java
- JWT stateless authentication
- Role-based authorization via @PreAuthorize
- Custom UserDetailsService (LoginUser)
- Security context in request attributes
- Replaces: IsNeedLogin, ValidatePageRight, Session checks
```

**Key Classes**:
- `JwtAuthenticationFilter` - Intercepts all requests, validates JWT token
- `JwtTokenProvider` - Generates and validates JWT tokens
- `LoginUser` - Implements UserDetails with roles and unitCode
- `SecurityConfig` - Configures filter chain and CORS

### 3. Business Logic Migration

**DAL → Service Pattern**:
- .NET: `ApplyDAL.AddApply()` → Java: `ApplyService.createApply()`
- .NET: Validation in DAL → Java: Service layer with `@Validated`
- .NET: Transaction management → Spring `@Transactional`
- Business rules from SQL functions → Java service methods

**Example** - Certificate Validity Calculation:
```sql
-- Legacy: GET_CertificateContinueValidEndDate stored function
-- Java: CertificateService.calculateValidEndDate() method
```

### 4. API Design

**RESTful Endpoints**:
```
GET    /api/apply/list              → List applications with pagination
POST   /api/apply                   → Create new application
PUT    /api/apply/{id}/submit       → Submit for review
PUT    /api/apply/{id}/approve      → Approve application
PUT    /api/apply/{id}/reject       → Reject application
GET    /api/apply/{id}               → Get application details
GET    /api/apply/status/{status}      → Filter by status
GET    /api/apply/pending             → Get pending applications
```

**Response Format** (Unified via `Result<T>`):
```json
{
  "code": 0,           // 0=success, 1=error, 2=unauthorized, 3=forbidden
  "message": "success",
  "data": { ... },
  "timestamp": 1234567890
}
```

**API Versioning**: Base URL `/api/v1/` for future compatibility

### 5. Database Integration

**Configuration** (application.yml):
```yaml
spring:
  datasource:
    driver-class: com.microsoft.sqlserver.jdbc.SQLServerDriver
    url: jdbc:sqlserver://101.200.193.13:1433;databaseName=RYZG
    username: synergydatauser
    password: synergy_password
```

**MyBatis-Plus Configuration**:
```xml
mybatis-plus:
  mapper-locations: classpath:mapper/*.xml
  type-aliases-package: com.ryzggl.entity
```

**Migration Considerations**:
- Preserve all existing data (no schema changes)
- Keep stored functions (CheckIDCard, certificate validity) callable via MyBatis `@Select`
- Use `ddl-auto: none` to protect existing schema
- Add indexes in application.yml for performance

### 6. Frontend Architecture

**Technology**: Vue.js 3 + Element Plus + Vite

**Component Mapping** (Telerik → Vue):
| .NET Telerik | Vue.js + Element Plus |
|----------------|------------------------|
| RadGrid | el-table (data table) |
| RadComboBox | el-select (dropdown) |
| RadDatePicker | el-date-picker |
| RadTextBox | el-input (text field) |
| RadWindow | el-dialog (modal) |
| RadAjaxPanel | v-loading (loading) |
| RadStyleSheetManager | CSS variables/themes |

**Page Mapping**:
| .NET Page | Vue Route |
|------------|-----------|
| zjsApplyList.aspx | /api/apply/list (应用列表) |
| zjsApplyFirst.aspx | /api/apply/create (首次申请) |
| zjsApplyChange.aspx | /api/apply/{id}/change (变更申请) |
| CertifManage/Application.aspx | /api/certificates/list (证书管理) |
| CertifManage/CertifList.aspx | /api/certificates/{id}/change (证书变更) |

**Authentication Flow**:
1. Login → Store JWT token in localStorage/cookie
2. Include token in Axios interceptor headers
3. Redirect to login on 401/403 responses
4. Token refresh on 401 errors

### 7. Status Code Mapping

**Application Status** (Apply.APPLYSTATUS):
| .NET Value | Java Value | Meaning |
|-------------|-----------|---------|
| 未填写 | "UNFILLED" | Initial application state |
| 待确认 | "PENDING_REVIEW" | Submitted for approval |
| 已受理 | "UNDER_REVIEW" | Being processed |
| 已申报 | "SUBMITTED" | Submitted to authority |
| 已驳回 | "REJECTED" | Approval denied |

**Certificate Status** (Certificate.STATUS):
| .NET Value | Java Value | Meaning |
|-------------|-----------|---------|
| 有效 | "VALID" | Active certificate |
| 暂停 | "SUSPENDED" | Temporarily suspended |
| 注销 | "REVOKED" | Cancelled |

## Getting Started

### Prerequisites

```bash
# Java 17+
java --version

# Maven 3.6+
mvn --version

# SQL Server connectivity
# Ensure port 1433 is accessible
```

### Build & Run

```bash
# Build project
mvn clean package

# Run application
java -jar target/ryzggl-java-1.0.0.jar

# With specific profile
java -jar target/ryzggl-java-1.0.0.jar --spring.profiles.active=dev
```

### Access Application

- **Frontend**: http://localhost:5173/ (Vue dev server)
- **Backend API**: http://localhost:8080/api/v1/
- **API Documentation**: http://localhost:8080/swagger-ui.html (via Knife4j)

## Implementation Status

### ✅ Completed
1. **Database Reference** - Complete schema documentation (DATABASE_REFERENCE.md)
2. **Project Structure** - Maven multi-module setup (pom.xml)
3. **Configuration** - application.yml with SQL Server & JWT
4. **Security Framework** - JWT authentication filter and token provider
5. **Core Entities** - Apply, ApplyFirst, ApplyChange, Certificate, FileInfo, ExamResult
6. **Repositories** - ApplyRepository, CertificateRepository, FileInfoRepository (MyBatis-Plus)
7. **Services** - ApplyService, CertificateService, FileService, ExamService with full business logic
8. **REST API** - ApplyController, CertificateController, FileController, ExamController with complete CRUD endpoints
9. **Response Wrapper** - Unified Result<T> class
10. **Certificate Validity Service** - Port validity calculation function (age + post type + region rules)
11. **Worker Management** - Implement worker CRUD
12. **Examination System** - Implement exam modules with scoring and statistics
13. **File Upload/Download** - Implement file handling with validation and security

### 🚧 In Progress
1. **Vue.js Frontend** - Basic project structure (TODO: implement pages)
2. **Integrate with SQL Server database** - Verify configuration and connections
3. **Additional Entities** - Migrate remaining 140+ tables

### ⏳ TODO
1. **ID Card Validation Service** - Port CheckIDCard function
2. **Integration Testing** - End-to-end testing with .NET system
3. **Data Migration** - Strategy for migrating existing data
4. **Performance Optimization** - Add database indexes and query optimization

## Migration Notes

### Key Challenges Addressed

1. **Business Logic Complexity**
   - Certificate validity calculation involves age + post type + region rules
   - Multi-step approval workflows with status transitions
   - Solution: Ported SQL functions to service methods, documented in DATABASE_REFERENCE.md

2. **Authentication Model Change**
   - .NET: Session-based with page-level permissions
   - Spring Boot: JWT stateless with role-based auth
   - Solution: JwtAuthenticationFilter recreates BasePage logic with roles

3. **Entity Relationship Mapping**
   - .NET: Foreign keys often implicit (WORKERID, UNITCODE)
   - Spring Boot: Explicit entity references (ManyToOne, etc.)
   - Solution: Documented all relationships in DATABASE_REFERENCE.md

4. **Data Types**
   - .NET: varchar(100), datetime, bigint
   - Java: String, LocalDateTime, Long (with @Size annotations)
   - Solution: Added @TableField type specifications

### Testing Strategy

```bash
# Unit tests for each service
mvn test

# Integration test with SQL Server
# Ensure all CRUD operations work correctly

# API testing with Swagger UI
# Use Knife4j Swagger for endpoint testing
```

## References

- **Database Schema**: See `DATABASE_REFERENCE.md` for complete table documentation
- **Original System**: RYZGGL-C#/.NET solution
- **Spring Boot Docs**: https://spring.io/projects/spring-boot
- **MyBatis-Plus Docs**: https://baomidou.com/
- **Vue.js Docs**: https://vuejs.org/
- **Element Plus**: https://element-plus.org/

## Contact & Support

For questions about this migration, please refer to:
1. DATABASE_REFERENCE.md - Complete entity and business rule documentation
2. Application module (ApplyService.java, ApplyController.java) - Example implementation
3. Security architecture (JwtAuthenticationFilter, JwtTokenProvider) - Authentication pattern

---

**Last Updated**: 2024-03-13
**Migration Progress**: Phase 1 (Foundation) - 90% Complete
