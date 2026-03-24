# Entity Management Frontend Guide

## Overview

The Entity Management module provides comprehensive CRUD (Create, Read, Update, Delete) interfaces for the 4 new entities:
- ApplyContinue (延续申请)
- ApplyRenew (续期申请)
- CertificateChange (证书变更)
- CertificateContinue (证书延续)

## Features

### 1. Unified Entity Management Page
A single, flexible Vue component that handles all 4 entity types with dynamic rendering.

### 2. Search & Filter Capabilities
- Keyword search across multiple fields
- Field-specific filtering
- Status-based filtering (for CertificateContinue)
- Pagination support

### 3. Data Table Display
- Responsive table layout
- Column sorting
- Action buttons for each row
- Status indicators

### 4. Form Dialog
- Create new records
- Edit existing records
- Form validation
- Dynamic field rendering based on entity type

### 5. Quick Actions
Dashboard shortcuts to quickly access each entity management page

## File Structure

```
ryzggl-vue/
├── src/
│   ├── views/
│   │   └── EntityManagement.vue          # Main unified entity management component
│   ├── api/
│   │   └── entityManagement.js          # API client for all 4 entities
│   ├── router/
│   │   └── index.js                     # Router configuration (updated)
│   └── views/
│       └── Dashboard.vue                 # Updated with quick action buttons
```

## API Endpoints

### ApplyContinue
- `GET /api/v1/apply-continue/list` - List with pagination
- `GET /api/v1/apply-continue/:id` - Get by ID
- `POST /api/v1/apply-continue` - Create new
- `PUT /api/v1/apply-continue/:id` - Update
- `DELETE /api/v1/apply-continue/:id` - Delete

### ApplyRenew
- `GET /api/v1/apply/renew/list` - List with pagination
- `GET /api/v1/apply/renew/:id` - Get by ID
- `POST /api/v1/apply/renew` - Create new
- `PUT /api/v1/apply/renew/:id` - Update
- `DELETE /api/v1/apply/renew/:id` - Delete

### CertificateChange
- `GET /api/v1/certificate/change/list` - List with pagination
- `GET /api/v1/certificate/change/:id` - Get by ID
- `POST /api/v1/certificate/change` - Create new
- `PUT /api/v1/certificate/change/:id` - Update
- `DELETE /api/v1/certificate/change/:id` - Delete
- `GET /api/v1/certificate/change/statistics` - Get statistics

### CertificateContinue
- `GET /api/v1/certificate/continue/list` - List with pagination
- `GET /api/v1/certificate/continue/:id` - Get by ID
- `POST /api/v1/certificate/continue` - Create new
- `PUT /api/v1/certificate/continue/:id` - Update
- `DELETE /api/v1/certificate/continue/:id` - Delete
- `GET /api/v1/certificate/continue/statistics` - Get statistics

## Access Routes

### Direct Routes
- `http://localhost:5173/entity-management/applyContinue` - ApplyContinue Management
- `http://localhost:5173/entity-management/applyRenew` - ApplyRenew Management
- `http://localhost:5173/entity-management/certificateChange` - CertificateChange Management
- `http://localhost:5173/entity-management/certificateContinue` - CertificateContinue Management

### Shortcut Routes (User-friendly)
- `http://localhost:5173/apply-continue` - ApplyContinue Management
- `http://localhost:5173/apply-renew` - ApplyRenew Management
- `http://localhost:5173/certificate-change` - CertificateChange Management
- `http://localhost:5173/certificate-continue` - CertificateContinue Management

### Dashboard Quick Access
Navigate to Dashboard (http://localhost:5173/) and click on the entity management shortcut buttons.

## User Guide

### 1. Accessing Entity Management

**From Dashboard:**
1. Log in to the system
2. Navigate to Dashboard
3. Scroll to "快捷操作" section
4. Click on any "实体管理快捷入口" button

**Direct URL Access:**
- Type the entity management URL directly in browser address bar

### 2. Viewing Entity Data

1. **Browse Records**: The page loads with paginated data (10 records per page by default)
2. **Pagination Controls**: Use pagination controls at the bottom to navigate through pages
3. **Table Columns**: Review the displayed columns for each entity type

### 3. Searching and Filtering

**Basic Search:**
1. Enter keywords in the search input field
2. Select the specific field to search (or leave as "全部" for global search)
3. Click "查询" button to execute search

**Advanced Filtering (CertificateContinue):**
1. Select "延续状态" from dropdown (未延续, 已延续, 已过期)
2. Combine with keyword search for refined results

**Reset:**
- Click "重置" button to clear all filters and return to default view

### 4. Creating New Records

1. **Open Create Form**: Click "新增[Entity Name]" button at top right
2. **Fill Required Fields**:
   - ApplyContinue: 申请编号, 申请人ID, 申请人姓名, 身份证号, 手机号, 企业代码, 企业名称, 证书编号, 延续类型, 申请状态
   - ApplyRenew: 申请编号, 申请人ID, 申请人姓名, 身份证号, 手机号, 企业代码, 企业名称, 证书编号, 续期原因, 申请状态
   - CertificateChange: 证书ID, 证书编号, 变更类型, 变更原因, 变更前值, 变更后值, 变更日期, 变更人
   - CertificateContinue: 证书ID, 证书编号, 延续类型, 延续开始日期, 延续结束日期, 延续年数, 延续费用, 延续状态, 延续人

3. **Save**: Click "确定" button to create the record
4. **Success Message**: System will show success message and refresh the table

### 5. Editing Records

1. **Open Edit Form**: Click "编辑" button in the action column for the desired row
2. **Modify Fields**: Update the required fields
3. **Save**: Click "确定" button to save changes
4. **Success Message**: System will show success message and refresh the table

### 6. Deleting Records

1. **Confirm Deletion**: Click "删除" button in the action column
2. **Confirm Dialog**: System shows confirmation dialog - click "确定" to confirm
3. **Success Message**: System will show success message and refresh the table
4. **Cancel**: Click "取消" in the confirmation dialog to abort deletion

## Technical Details

### Component Architecture

**EntityManagement.vue** uses Vue 3 Composition API with the following features:

- **Dynamic Rendering**: Single component renders different entities based on route parameter
- **Entity Configuration**: `entityConfigs` object defines columns, form fields, and labels for each entity type
- **Composable Logic**: Search, pagination, form handling are separated into reusable functions

### State Management

- **Local State**: Uses Vue's reactive `ref` and `computed` for component state
- **Route Parameter**: Watches `route.params.type` to switch between entity types
- **API Integration**: Calls backend API endpoints through centralized `entityManagement.js` client

### Form Field Types

The component supports multiple field types:
- `text`: Text input fields
- `number`: Numeric input fields
- `date`: Date picker fields
- `select`: Dropdown selection with predefined options
- `textarea`: Multi-line text input

## Database Schema Requirements

The frontend expects the following database tables to exist:

### ApplyContinue Table
- `ID`, `APPLYNO`, `WORKERID`, `APPLICANTNAME`, `IDCARD`, `PHONE`, `UNITCODE`, `UNITNAME`, `CERTIFICATECODE`, `CONTINUETYPE`, `CONTINUESTARTDATE`, `CONTINUEENDDATE`, `APPLYSTATUS`, `CHECKADVISE`, `CHECKMAN`, `CHECKDATE`, `FILEPATH`, `REMARK`, `CREATE_TIME`, `UPDATE_TIME`, `CREATE_BY`, `UPDATE_BY`, `DELETED`

### ApplyRenew Table
- `ID`, `APPLYNO`, `WORKERID`, `APPLICANTNAME`, `IDCARD`, `PHONE`, `UNITCODE`, `UNITNAME`, `CERTIFICATECODE`, `RENEWREASON`, `RENEWTYPE`, `RENEWSTARTDATE`, `RENEWENDDATE`, `APPLYSTATUS`, `CHECKADVISE`, `CHECKMAN`, `CHECKDATE`, `FILEPATH`, `REMARK`, `CREATE_TIME`, `UPDATE_TIME`, `CREATE_BY`, `UPDATE_BY`, `DELETED`

### CertificateChange Table
- `ID`, `CERTIFICATEID`, `CERTIFICATECODE`, `CHANGETYPE`, `CHANGEREASON`, `OLDVALUE`, `NEWVALUE`, `CHANGEDATE`, `CHANGEMAN`, `CHANGEMANID`, `CHECKADVISE`, `CHECKMAN`, `CHECKDATE`, `FILEPATH`, `REMARK`, `CREATE_TIME`, `UPDATE_TIME`, `CREATE_BY`, `UPDATE_BY`, `DELETED`

### CertificateContinue Table
- `ID`, `CERTIFICATEID`, `CERTIFICATECODE`, `CONTINUETYPE`, `CONTINUESTARTDATE`, `CONTINUEENDDATE`, `CONTINUEYEARS`, `CONTINUEFEE`, `CONTINUEREASON`, `CONTINUESTATUS`, `CONTINUEMAN`, `CONTINUEMANID`, `CONTINUEDATE`, `CHECKADVISE`, `CHECKMAN`, `CHECKDATE`, `FILEPATH`, `REMARK`, `CREATE_TIME`, `UPDATE_TIME`, `CREATE_BY`, `UPDATE_BY`, `DELETED`

## Troubleshooting

### Issue: Data not loading
**Symptoms**: Table shows no data or loading state persists
**Solutions**:
1. Check backend API is running (http://localhost:8080)
2. Verify database tables exist and contain data
3. Check browser console for API errors
4. Verify API endpoint URLs are correct

### Issue: Cannot create/edit records
**Symptoms**: Form validation errors or API 400/500 errors
**Solutions**:
1. Check all required fields are filled
2. Verify data formats (dates, numbers, etc.)
3. Check backend API logs for specific error messages
4. Ensure database constraints are satisfied

### Issue: Route not found
**Symptoms**: 404 error when accessing entity management pages
**Solutions**:
1. Verify routes are correctly configured in `src/router/index.js`
2. Check Vue dev server is running (http://localhost:5173)
3. Clear browser cache and restart Vue dev server

### Issue: Form submission fails
**Symptoms**: Click "确定" button but nothing happens
**Solutions**:
1. Check browser console for JavaScript errors
2. Verify API client configuration in `src/api/request.js`
3. Check network requests in browser DevTools
4. Verify backend authentication is working

## Testing Checklist

### Basic Functionality
- [ ] Access each entity management page
- [ ] View list of records
- [ ] Test pagination (10, 20, 50, 100 records per page)
- [ ] Test keyword search
- [ ] Test field-specific search
- [ ] Test status filtering (CertificateContinue)
- [ ] Test reset functionality

### CRUD Operations
- [ ] Create new record for each entity type
- [ ] Edit existing record for each entity type
- [ ] Delete record for each entity type
- [ ] Verify form validation works
- [ ] Verify success/error messages display correctly

### UI/UX
- [ ] Table columns display correctly for each entity
- [ ] Form fields display correctly for each entity
- [ ] Action buttons are visible and functional
- [ ] Loading states work properly
- [ ] Error messages are user-friendly

### Integration
- [ ] Dashboard quick access buttons work
- [ ] Direct URL access works
- [ ] Browser navigation (back/forward) works
- [ ] Page refresh maintains state (if applicable)

## Next Steps

1. **Database Setup**: Execute `database-new-tables.sql` to create tables in the database
2. **Backend Testing**: Verify all API endpoints are working correctly
3. **Frontend Testing**: Test all CRUD operations through the UI
4. **User Training**: Train users on the new entity management interface
5. **Documentation**: Create user manuals and help documentation

## Support

For issues or questions:
1. Check this guide for troubleshooting steps
2. Review backend API documentation
3. Check browser console for error messages
4. Contact development team for technical support

---

**Created**: 2026-03-16
**Version**: 1.0
**Components**: EntityManagement.vue, entityManagement.js
**Backend**: Spring Boot 3.1.5 with MyBatis-Plus
**Frontend**: Vue 3 + Element Plus