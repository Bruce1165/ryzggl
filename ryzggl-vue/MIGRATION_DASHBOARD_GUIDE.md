# RYZGGL Migration Dashboard - Quick Start Guide

## Overview

The Migration Dashboard provides real-time visibility into the progress of migrating the RYZGGL (人员资格管理系统) from .NET to Spring Boot + Vue.js.

## Accessing the Dashboard

### Option 1: Direct Access (No Authentication Required)

Since this is a project tracking dashboard, it doesn't require authentication:

```bash
# Start the Vue development server
cd ryzggl-vue
npm run dev

# Access the migration dashboard
http://localhost:5173/migration-dashboard
```

### Option 2: Through Application

1. Login to the main application: http://localhost:5173/
2. Navigate to: http://localhost:5173/migration-dashboard

## Dashboard Features

### 1. Overall Progress Section
- **Visual Progress Bar**: Shows total migration percentage
- **Phase Indicators**: 5-phase migration timeline
  - Phase 1: 基础架构 - 90% complete
  - Phase 2: 核心业务 - 70% complete (active)
  - Phase 3: 高级功能 - 20% complete
  - Phase 4: 测试优化 - 0% complete
  - Phase 5: 部署上线 - Pending

### 2. Key Metrics Cards
Four main progress indicators:

| Card | Shows | Current Status |
|------|-------|----------------|
| **实体迁移** | Database tables → Java entities | 10/150 (6.7%) |
| **业务服务** | Business logic implementation | 8/50 (16%) |
| **API控制器** | REST API endpoints | 6/40 (15%) |
| **前端页面** | Vue.js page components | 15/35 (42.9%) |

### 3. Entity Migration Status Table
Tracks migration status for each database table:

**Columns:**
- **业务域**: Business domain (申请管理, 证书管理, etc.)
- **数据库表名**: Original SQL Server table name
- **实体类名**: Java entity class name
- **迁移状态**: completed, inProgress, or pending
- **后端组件**: Entity ✓, Repository ✓, Service ✓, Controller ✓
- **前端组件**: Vue page implementation
- **优先级**: Priority level (high, medium, low)
- **备注**: Additional notes

**Filter Options:**
- All entities
- Completed only
- In progress only
- Pending only

### 4. Architecture Decisions Timeline
Key technical decisions with rationale:

1. **认证架构决策** (2024-03-14)
   - Migrated from Session to JWT stateless authentication
   - Improves scalability, supports distributed deployment

2. **ORM框架选择** (2024-03-13)
   - Chose MyBatis-Plus over JPA
   - More flexible SQL control, better for complex queries

3. **前端技术栈** (2024-03-13)
   - Vue 3 + Element Plus + Vite
   - Modern development experience, TypeScript support

4. **数据库策略** (2024-03-12)
   - Keep existing SQL Server, no schema changes
   - Lowers migration risk, enables gradual migration

### 5. Issues & Blockers Panel
Tracks known issues and blockers:

**Severity Levels:**
- 🔴 **High**: Critical issues requiring immediate attention
- 🟡 **Medium**: Important issues for next sprint
- 🟢 **Low**: Nice-to-have improvements

**Current Blockers:**
1. **证书有效期计算逻辑迁移** (High, Blocker)
   - Due: 2024-03-20
   - Complex business rules involving age, region, and position

2. **多步骤审批工作流** (High, Blocker)
   - Due: 2024-04-05
   - ApplyCheckTask multi-step approval workflow

### 6. Testing & Quality Metrics

**Test Coverage:**
- Unit Tests: 35%
- Integration Tests: 15%
- E2E Tests: 5%

**Code Quality:**
- Code Coverage: 45%
- Technical Debt: 25%
- Documentation: 70%

### 7. Recent Activity Log
Tracks recent development activities:
- Frontend page development
- API endpoint implementation
- Bug fixes
- Documentation updates
- Architecture design

### 8. Technology Comparison Matrix
Shows migration status for each technology component:

| Component | Legacy (.NET) | Modern (Spring/Vue) | Status |
|-----------|---------------|---------------------|--------|
| Frontend Framework | ASP.NET WebForms | Vue.js 3 | In Progress |
| UI Component Library | Telerik RadControls | Element Plus | In Progress |
| Backend Framework | ASP.NET 3.5/4.0 | Spring Boot 3.2 | In Progress |
| ORM/Data Access | ADO.NET + DAL | MyBatis-Plus | In Progress |
| Authentication | Session + Role | JWT + Spring Security | Completed |
| Database | SQL Server | SQL Server | Completed |
| Build Tool | MSBuild | Maven | Completed |
| API Documentation | None | Swagger/Knife4j | In Progress |

## Updating Dashboard Data

### Backend API Endpoints

The dashboard is powered by the following backend APIs (auto-updates available):

```bash
# Get overall progress
GET /api/v1/migration-dashboard/progress

# Get entity status
GET /api/v1/migration-dashboard/entities

# Get issues and blockers
GET /api/v1/migration-dashboard/issues

# Get quality metrics
GET /api/v1/migration-dashboard/quality

# Get recent activities
GET /api/v1/migration-dashboard/activities

# Get migration phases
GET /api/v1/migration-dashboard/phases
```

### Manual Updates

To manually update dashboard data, edit the `MigrationDashboardController.java` file:

```bash
# Backend controller location
ryzggl-java/src/main/java/com/ryzggl/controller/MigrationDashboardController.java
```

Key methods to update:
- `getOverallProgress()` - Update progress percentages
- `getEntityStatus()` - Add/update entity migration status
- `getIssues()` - Add new issues or update existing ones
- `getQualityMetrics()` - Update test coverage and quality metrics
- `getRecentActivities()` - Log new activities

## Customization

### Adding New Metrics

1. **Backend**: Add new method in `MigrationDashboardController.java`
2. **Frontend**: Add corresponding API call in `api/migrationDashboard.js`
3. **UI**: Display data in `MigrationDashboard.vue`

### Styling

The dashboard uses Element Plus components with custom CSS. Modify styles in the `<style scoped>` section of `MigrationDashboard.vue`.

### Colors and Themes

Edit color variables in the component's computed properties:
- Progress colors
- Status badge colors
- Card hover effects

## Integration with Workflow

### Daily Standup Updates
Update the "Recent Activity Log" after each significant task completion.

### Sprint Planning
Review "Issues & Blockers" to identify tasks for the next sprint.

### Progress Reviews
Use the "Overall Progress" section to track milestone completion.

### Architecture Decisions
Document new technical decisions in the "Architecture Decisions" timeline.

## Troubleshooting

### Dashboard Not Loading
1. Check if Vue dev server is running: `npm run dev`
2. Verify backend is accessible: http://localhost:8080/api/v1/migration-dashboard/progress
3. Check browser console for errors

### Data Not Updating
1. Verify backend controller is returning correct data
2. Check API endpoints are accessible
3. Clear browser cache and refresh

### Styling Issues
1. Verify Element Plus is properly installed
2. Check that icons are imported correctly
3. Review CSS for conflicts

## Best Practices

1. **Keep it Current**: Update dashboard data after each significant development task
2. **Be Honest**: Accurately reflect migration status, don't inflate progress
3. **Track Blockers**: Clearly document issues that are blocking progress
4. **Celebrate Wins**: Log completed features in the activity log
5. **Review Regularly**: Use the dashboard in sprint planning and retrospectives

## API Response Format

All dashboard APIs follow the standard Result<T> wrapper:

```json
{
  "code": 0,
  "message": "success",
  "data": { ... },
  "timestamp": 1678901234567
}
```

## Security Considerations

- The migration dashboard does not require authentication (for project visibility)
- In production, consider adding password protection if sharing externally
- API endpoints use standard Spring Security configuration

## Future Enhancements

Potential improvements to consider:
1. **Real-time Updates**: WebSocket integration for live progress updates
2. **Team Member Assignment**: Track who is working on each task
3. **Burndown Charts**: Visual progress tracking over time
4. **Export Functionality**: Generate PDF reports for stakeholders
5. **Notification System**: Alerts for critical blockers or milestones
6. **GitHub Integration**: Sync with commit history and pull requests

## Support

For questions or issues with the migration dashboard:
1. Check the API logs in the backend console
2. Review Vue.js browser console for frontend errors
3. Refer to project documentation in `CLAUDE.md` and `DATABASE_REFERENCE.md`

---

**Last Updated**: 2026-03-15
**Dashboard Version**: 1.0
**Project**: RYZGGL Migration (.NET to Spring Boot + Vue.js)