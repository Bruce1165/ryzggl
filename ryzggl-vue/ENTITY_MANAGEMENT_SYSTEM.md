# Generic Entity Management System

## Overview

The Generic Entity Management System is a comprehensive framework for managing CRUD operations across all entity types in the RYZGGL project. It provides a unified, type-safe, and maintainable approach to entity management with built-in validation, error handling, and accessibility features.

## Architecture

The system follows a three-tier architecture:

```
┌─────────────────────────────────────────────────────────┐
│                    UI Components                         │
│  (EntityForm.vue, EntitySearch.vue, EntityManagement.vue)│
└─────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────┐
│                 Composables Layer                        │
│           (useEntityManagement.js)                      │
└─────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────┐
│                  Configuration Layer                     │
│             (entityRegistry.js)                         │
└─────────────────────────────────────────────────────────┘
                            ↓
┌─────────────────────────────────────────────────────────┐
│                   API Gateway Layer                      │
│              (entityGateway.js)                          │
└─────────────────────────────────────────────────────────┘
```

## Core Components

### 1. Entity Configuration Registry (`src/utils/entityRegistry.js`)

Central registry for entity configurations providing schema structure, validation, and metadata.

**Key Features:**
- Centralized entity configuration management
- Type-safe field definitions
- Built-in validation rules
- Computed properties for search fields and display fields
- Support for 8+ field types

**Field Types:**
```javascript
const FIELD_TYPES = {
  TEXT: 'text',
  NUMBER: 'number',
  DATE: 'date',
  DATETIME: 'datetime',
  SELECT: 'select',
  TEXTAREA: 'textarea',
  BOOLEAN: 'boolean',
  PHONE: 'phone',
  EMAIL: 'email',
  ID_CARD: 'idcard',
  CERTIFICATE_CODE: 'certificatecode'
}
```

**Registering a New Entity:**
```javascript
registry.register('newEntity', {
  name: 'New Entity',
  title: 'New Entity Management',
  apiPath: '/api/new-entity',
  primaryKey: 'id',
  tableColumns: [
    { prop: 'id', label: 'ID', width: 80 },
    { prop: 'name', label: '名称', width: 200 }
  ],
  formFields: [
    { prop: 'name', label: '名称', type: FIELD_TYPES.TEXT, required: true }
  ]
})
```

### 2. Generic CRUD Composable (`src/composables/useEntityManagement.js`)

Reactive state management for entity operations with loading, error handling, and data caching.

**Usage:**
```javascript
import { useEntityManagement } from '@/composables/useEntityManagement'

const {
  loading,
  error,
  data,
  pagination,
  searchParams,
  formVisible,
  formData,
  entityConfig,

  // CRUD operations
  fetchList,
  createEntity,
  updateEntity,
  deleteEntity,
  performAction,

  // Form management
  openCreateForm,
  openEditForm,
  closeForm
} = useEntityManagement('worker', {
  autoLoad: true,
  pageSize: 20
})
```

**Methods:**
- `fetchList(params)` - Get paginated list with filters
- `fetchById(id)` - Get single entity
- `createEntity(data)` - Create new entity
- `updateEntity(id, data)` - Update existing entity
- `deleteEntity(id, confirmMessage)` - Delete with confirmation
- `batchDeleteEntities(ids)` - Batch delete
- `performAction(action, id, data)` - Custom actions

### 3. Dynamic API Gateway (`src/api/entityGateway.js`)

Generic API gateway implementing RESTful conventions for all entity types.

**Features:**
- Dynamic endpoint resolution based on entity configurations
- RESTful convention (GET, POST, PUT, DELETE)
- Support for custom endpoints and actions
- Response normalization
- Error handling and enrichment
- Basic caching for list operations

**Usage:**
```javascript
import { entityGateway } from '@/api/entityGateway'

// CRUD operations
const list = await entityGateway.list('worker', { page: 1, size: 10 })
const entity = await entityGateway.get('worker', id)
const created = await entityGateway.create('worker', data)
const updated = await entityGateway.update('worker', id, data)
await entityGateway.delete('worker', id)

// Custom actions
await entityGateway.action('apply', 'approve', id, { reason: 'Approved' })

// Batch operations
await entityGateway.batchCreate('worker', [item1, item2])
await entityGateway.batchDelete('worker', [id1, id2])
```

### 4. Form Schema Builder (`src/components/EntityForm.vue`)

Dynamic form rendering with automatic validation based on entity configuration.

**Features:**
- Automatic field rendering based on field type
- Built-in validation rules
- Support for custom slots
- Accessibility features (keyboard navigation, ARIA labels)
- Responsive layout
- Form state management

**Usage:**
```vue
<EntityForm
  ref="formRef"
  v-model="formData"
  :fields="formFields"
  :rules="formRules"
  :columns="2"
  @submit="handleSubmit"
/>
```

**Supported Field Types:**
- `text` - Standard text input
- `number` - Number input with precision control
- `date` / `datetime` - Date and datetime pickers
- `select` - Dropdown with options
- `textarea` - Multi-line text input
- `boolean` - Toggle switch
- `phone` - Phone number input (11 digits)
- `email` - Email input with validation
- `idcard` - ID card input (15/18 digits)
- `certificatecode` - Certificate code input (4-20 chars)
- `custom` - Custom slot

### 5. Enhanced Search Component (`src/components/EntitySearch.vue`)

Advanced search with field-specific filters and operators.

**Features:**
- Keyword search with field selection
- Multiple filter types (select, date range, number range)
- Advanced search panel (collapsible)
- Auto-reset functionality
- Debounced search
- Keyboard shortcuts

**Usage:**
```vue
<EntitySearch
  v-model="searchParams"
  :search-fields="searchFields"
  :filters="entityFilters"
  :disabled="isLoading"
  @search="handleSearch"
  @reset="handleReset"
/>
```

## Currently Supported Entity Types

1. **worker** - 人员管理
2. **certificate** - 证书管理
3. **exam** - 考试管理
4. **apply** - 申请管理
5. **applyContinue** - 延续申请管理
6. **applyRenew** - 续期申请管理
7. **certificateChange** - 证书变更管理
8. **certificateContinue** - 证书延续管理

## Implementation Guidelines

### Adding a New Entity Type

1. **Register the entity** in `src/utils/entityRegistry.js`:

```javascript
registry.register('newEntity', {
  name: 'New Entity',
  title: 'New Entity Management',
  apiPath: '/api/new-entity',
  primaryKey: 'id',
  tableColumns: [
    // Define table columns
  ],
  formFields: [
    // Define form fields
  ]
})
```

2. **Use in a component**:

```javascript
const { data, fetchList, createEntity } = useEntityManagement('newEntity')
```

3. **Route configuration**:

```javascript
{
  path: '/new-entity/:type?',
  name: 'NewEntity',
  component: () => import('@/views/EntityManagement.vue'),
  props: { type: 'newEntity' }
}
```

### Customizing Entity Behavior

**Custom Validation Rules:**
```javascript
const formRules = computed(() => {
  return {
    name: [
      { required: true, message: '名称不能为空', trigger: 'blur' },
      { min: 2, max: 50, message: '长度在 2 到 50 个字符', trigger: 'blur' }
    ]
  }
})
```

**Custom Filters:**
```javascript
const entityFilters = computed(() => {
  return [
    {
      prop: 'status',
      label: '状态',
      type: 'select',
      options: ['active', 'inactive']
    },
    {
      prop: 'dateRange',
      label: '日期范围',
      type: 'dateRange',
      advanced: true
    }
  ]
})
```

**Custom Actions:**
```javascript
async function handleCustomAction(id) {
  const result = await performAction('customAction', id, { param: 'value' })
}
```

## Technical Standards

### Immutable Data Patterns
- Never mutate existing objects
- Always create new copies when updating data
- Use spread operator: `{ ...obj }` and `[...arr]`

### Error Handling
- Handle errors at all levels
- Provide user-friendly messages
- Log detailed errors for debugging
- Never silently swallow errors

### Input Validation
- Validate at system boundaries (API calls, form submission)
- Use field-type specific validation
- Clear error messages for users
- Client-side and server-side validation

### Accessibility
- Semantic HTML structure
- ARIA labels for screen readers
- Keyboard navigation support
- Focus management
- Error announcement

### Performance
- Virtual scrolling for large datasets
- Debounced search (300ms default)
- Basic caching for list operations
- Lazy loading of components
- Optimized reactivity

## Testing

The system includes comprehensive error handling and can be tested with the following scenarios:

1. **Basic CRUD Operations**
   - Create entity
   - Read entity list and single entity
   - Update entity
   - Delete entity with confirmation

2. **Form Validation**
   - Required field validation
   - Type-specific validation (email, phone, ID card)
   - Custom validation rules

3. **Search and Filter**
   - Keyword search
   - Field-specific filters
   - Date range filters
   - Reset functionality

4. **Error Handling**
   - Network errors
   - Validation errors
   - Server errors
   - User cancellation

## Browser Support

- Modern browsers (Chrome, Firefox, Safari, Edge)
- IE11+ (with polyfills)
- Mobile browsers (iOS Safari, Chrome Mobile)

## Dependencies

- Vue 3 Composition API
- Element Plus UI library
- ES6+ JavaScript features

## Performance Metrics

Target metrics for optimal performance:
- Initial load: < 2 seconds
- API response time: < 500ms
- Form validation: < 100ms
- Search debounce: 300ms
- Pagination response: < 300ms

## Future Enhancements

1. **Advanced Features**
   - Virtual scrolling for large datasets
   - Excel import/export
   - Advanced search with query builder
   - Bulk edit operations
   - History tracking and audit logs

2. **Performance Optimizations**
   - Request batching
   - Advanced caching strategies
   - Web Workers for heavy operations
   - Service worker for offline support

3. **UI/UX Improvements**
   - Drag-and-drop field ordering
   - Custom field types
   - Form templates
   - Multi-step wizards
   - Data visualization integration

## Troubleshooting

### Common Issues

**Entity not found error:**
- Ensure entity is registered in `entityRegistry.js`
- Check entity type spelling in route params
- Verify configuration has required fields

**Form validation not working:**
- Check field type matches supported types
- Ensure required fields have values
- Verify validation rules are correct

**API calls failing:**
- Check API path in entity configuration
- Verify backend endpoint exists
- Check network connection
- Review error messages in console

## Contributing

When extending the system:

1. Follow existing patterns and conventions
2. Add comprehensive comments
3. Include error handling
4. Test with multiple entity types
5. Update documentation
6. Maintain accessibility standards

## License

This system is part of the RYZGGL project and follows the same licensing terms.

---

**Version:** 1.0.0
**Last Updated:** 2026-03-17
**Maintainer:** Frontend Development Team