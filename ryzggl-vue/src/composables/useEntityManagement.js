/**
 * useEntityManagement Composable
 *
 * Generic CRUD operations composable for entity management.
 * Provides reactive state management for entity operations with loading,
 * error handling, and data caching.
 *
 * Follows Vue 3 Composition API patterns with immutable state updates
 * and comprehensive error handling.
 */

import { ref, computed, reactive } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { entityGateway } from '../api/entityGateway'
import { getEntityConfig, validateFieldValue } from '../utils/entityRegistry'

/**
 * Generic entity management composable
 * @param {string} entityType - Entity type identifier (e.g., 'worker', 'certificate')
 * @param {Object} options - Configuration options
 * @returns {Object} Reactive state and methods for entity management
 */
export function useEntityManagement(entityType, options = {}) {
  // Configuration options with defaults
  const {
    autoLoad = true,
    pageSize = 10,
    autoRefresh = false,
    refreshInterval = 30000,
    onError = null,
    onSuccess = null
  } = options

  // Reactive state
  const loading = ref(false)
  const error = ref(null)
  const data = ref([])
  const selectedItems = ref([])

  // Pagination state
  const pagination = reactive({
    page: 1,
    size: pageSize,
    total: 0
  })

  // Search and filter state
  const searchParams = reactive({
    keyword: '',
    field: '',
    status: '',
    ...options.defaultSearchParams
  })

  // Form state
  const formVisible = ref(false)
  const formMode = ref('create') // 'create' | 'edit'
  const formData = reactive({})
  const formErrors = reactive({})

  // Cache for current data
  let refreshTimer = null

  /**
   * Get entity configuration
   */
  const entityConfig = computed(() => {
    try {
      return getEntityConfig(entityType)
    } catch (err) {
      console.error(`Failed to get entity config for ${entityType}:`, err)
      return null
    }
  })

  /**
   * Fetch entity list with pagination and filters
   * @param {Object} params - Additional query parameters
   * @returns {Promise<Object>} API response
   */
  async function fetchList(params = {}) {
    if (!entityConfig.value) {
      const err = new Error(`Entity type "${entityType}" is not configured`)
      handleError(err, 'fetchList')
      throw err
    }

    loading.value = true
    error.value = null

    try {
      const queryParams = {
        page: pagination.page,
        size: pagination.size,
        ...searchParams,
        ...params
      }

      const response = await entityGateway.list(entityType, queryParams)

      if (response.success) {
        // Update data with new array (immutable)
        data.value = response.data.records || response.data || []

        // Update pagination
        if (response.pagination) {
          pagination.total = response.pagination.total
          pagination.page = response.pagination.page
          pagination.size = response.pagination.size
        } else if (response.data?.total !== undefined) {
          pagination.total = response.data.total
        }

        // Call success callback if provided
        if (onSuccess) {
          onSuccess('fetchList', response)
        }

        return response
      } else {
        throw new Error(response.message || '获取数据失败')
      }
    } catch (err) {
      handleError(err, 'fetchList')
      throw err
    } finally {
      loading.value = false
    }
  }

  /**
   * Fetch single entity by ID
   * @param {number|string} id - Entity ID
   * @returns {Promise<Object>} Entity data
   */
  async function fetchById(id) {
    if (!entityConfig.value) {
      const err = new Error(`Entity type "${entityType}" is not configured`)
      handleError(err, 'fetchById')
      throw err
    }

    loading.value = true
    error.value = null

    try {
      const response = await entityGateway.get(entityType, id)

      if (response.success) {
        return response.data
      } else {
        throw new Error(response.message || '获取数据失败')
      }
    } catch (err) {
      handleError(err, 'fetchById')
      throw err
    } finally {
      loading.value = false
    }
  }

  /**
   * Create new entity
   * @param {Object} entityData - Entity data to create
   * @returns {Promise<Object>} Created entity
   */
  async function createEntity(entityData) {
    if (!entityConfig.value) {
      const err = new Error(`Entity type "${entityType}" is not configured`)
      handleError(err, 'createEntity')
      throw err
    }

    loading.value = true
    error.value = null

    try {
      // Validate form data
      const validationResult = validateFormData(entityData)
      if (!validationResult.valid) {
        throw new Error(validationResult.message)
      }

      const response = await entityGateway.create(entityType, entityData)

      if (response.success) {
        ElMessage.success('创建成功')

        // Refresh list to include new entity
        await fetchList()

        // Call success callback if provided
        if (onSuccess) {
          onSuccess('createEntity', response)
        }

        return response.data
      } else {
        throw new Error(response.message || '创建失败')
      }
    } catch (err) {
      handleError(err, 'createEntity')
      throw err
    } finally {
      loading.value = false
    }
  }

  /**
   * Update existing entity
   * @param {number|string} id - Entity ID
   * @param {Object} entityData - Updated entity data
   * @returns {Promise<Object>} Updated entity
   */
  async function updateEntity(id, entityData) {
    if (!entityConfig.value) {
      const err = new Error(`Entity type "${entityType}" is not configured`)
      handleError(err, 'updateEntity')
      throw err
    }

    loading.value = true
    error.value = null

    try {
      // Validate form data
      const validationResult = validateFormData(entityData)
      if (!validationResult.valid) {
        throw new Error(validationResult.message)
      }

      const response = await entityGateway.update(entityType, id, entityData)

      if (response.success) {
        ElMessage.success('更新成功')

        // Refresh list to show updated data
        await fetchList()

        // Call success callback if provided
        if (onSuccess) {
          onSuccess('updateEntity', response)
        }

        return response.data
      } else {
        throw new Error(response.message || '更新失败')
      }
    } catch (err) {
      handleError(err, 'updateEntity')
      throw err
    } finally {
      loading.value = false
    }
  }

  /**
   * Delete entity with confirmation
   * @param {number|string} id - Entity ID
   * @param {string} [confirmMessage] - Custom confirmation message
   * @returns {Promise<boolean>} True if deleted
   */
  async function deleteEntity(id, confirmMessage = '确定要删除这条记录吗？') {
    try {
      await ElMessageBox.confirm(confirmMessage, '提示', {
        type: 'warning',
        confirmButtonText: '确定',
        cancelButtonText: '取消'
      })

      loading.value = true
      error.value = null

      const response = await entityGateway.delete(entityType, id)

      if (response.success) {
        ElMessage.success('删除成功')

        // Refresh list to remove deleted entity
        await fetchList()

        // Call success callback if provided
        if (onSuccess) {
          onSuccess('deleteEntity', response)
        }

        return true
      } else {
        throw new Error(response.message || '删除失败')
      }
    } catch (err) {
      if (err === 'cancel') {
        return false
      }
      handleError(err, 'deleteEntity')
      throw err
    } finally {
      loading.value = false
    }
  }

  /**
   * Batch delete entities
   * @param {Array<number|string>} ids - Array of entity IDs
   * @param {string} [confirmMessage] - Custom confirmation message
   * @returns {Promise<boolean>} True if deleted
   */
  async function batchDeleteEntities(ids, confirmMessage = `确定要删除这 ${ids.length} 条记录吗？`) {
    if (!ids || ids.length === 0) {
      ElMessage.warning('请先选择要删除的记录')
      return false
    }

    try {
      await ElMessageBox.confirm(confirmMessage, '批量删除', {
        type: 'warning',
        confirmButtonText: '确定',
        cancelButtonText: '取消'
      })

      loading.value = true
      error.value = null

      const response = await entityGateway.batchDelete(entityType, ids)

      if (response.success) {
        ElMessage.success(`成功删除 ${ids.length} 条记录`)

        // Clear selection
        selectedItems.value = []

        // Refresh list
        await fetchList()

        // Call success callback if provided
        if (onSuccess) {
          onSuccess('batchDeleteEntities', response)
        }

        return true
      } else {
        throw new Error(response.message || '批量删除失败')
      }
    } catch (err) {
      if (err === 'cancel') {
        return false
      }
      handleError(err, 'batchDeleteEntities')
      throw err
    } finally {
      loading.value = false
    }
  }

  /**
   * Perform custom action on entity
   * @param {string} action - Action name (e.g., 'approve', 'reject')
   * @param {number|string} id - Entity ID
   * @param {Object} [actionData] - Additional data for action
   * @returns {Promise<Object>} Action response
   */
  async function performAction(action, id, actionData = {}) {
    if (!entityConfig.value) {
      const err = new Error(`Entity type "${entityType}" is not configured`)
      handleError(err, 'performAction')
      throw err
    }

    loading.value = true
    error.value = null

    try {
      const response = await entityGateway.action(entityType, action, id, actionData)

      if (response.success) {
        ElMessage.success('操作成功')

        // Refresh list to show updated status
        await fetchList()

        // Call success callback if provided
        if (onSuccess) {
          onSuccess('performAction', response)
        }

        return response.data
      } else {
        throw new Error(response.message || '操作失败')
      }
    } catch (err) {
      handleError(err, 'performAction')
      throw err
    } finally {
      loading.value = false
    }
  }

  /**
   * Open form for creating new entity
   */
  function openCreateForm() {
    formMode.value = 'create'

    // Reset form data with empty values
    Object.keys(formData).forEach(key => {
      formData[key] = ''
    })
    Object.keys(formErrors).forEach(key => {
      delete formErrors[key]
    })

    formVisible.value = true
  }

  /**
   * Open form for editing existing entity
   * @param {Object} entity - Entity data to edit
   */
  function openEditForm(entity) {
    formMode.value = 'edit'

    // Populate form data with entity values (immutable copy)
    Object.assign(formData, { ...entity })

    // Clear form errors
    Object.keys(formErrors).forEach(key => {
      delete formErrors[key]
    })

    formVisible.value = true
  }

  /**
   * Close form
   */
  function closeForm() {
    formVisible.value = false
    formMode.value = 'create'

    // Reset form data
    Object.keys(formData).forEach(key => {
      formData[key] = ''
    })
    Object.keys(formErrors).forEach(key => {
      delete formErrors[key]
    })
  }

  /**
   * Validate form data based on entity configuration
   * @param {Object} data - Form data to validate
   * @returns {Object} { valid: boolean, message: string }
   */
  function validateFormData(data) {
    if (!entityConfig.value) {
      return { valid: false, message: 'Entity configuration not found' }
    }

    const errors = []

    // Check required fields
    entityConfig.value.formFields.forEach(field => {
      if (field.required && (!data[field.prop] || data[field.prop] === '')) {
        errors.push(`${field.label} 是必填项`)
      }

      // Validate field value by type
      if (data[field.prop]) {
        const validation = validateFieldValue(field.type, data[field.prop])
        if (!validation.isValid) {
          errors.push(validation.message)
        }
      }
    })

    if (errors.length > 0) {
      return { valid: false, message: errors.join('; ') }
    }

    return { valid: true, message: '' }
  }

  /**
   * Handle search/filter change
   * @param {Object} newSearchParams - New search parameters
   */
  function handleSearchChange(newSearchParams) {
    Object.assign(searchParams, newSearchParams)
    pagination.page = 1 // Reset to first page
    fetchList()
  }

  /**
   * Reset search parameters and fetch list
   */
  function resetSearch() {
    Object.assign(searchParams, {
      keyword: '',
      field: '',
      status: '',
      ...options.defaultSearchParams
    })
    pagination.page = 1
    fetchList()
  }

  /**
   * Handle page size change
   * @param {number} newSize - New page size
   */
  function handleSizeChange(newSize) {
    pagination.size = newSize
    pagination.page = 1
    fetchList()
  }

  /**
   * Handle current page change
   * @param {number} newPage - New page number
   */
  function handlePageChange(newPage) {
    pagination.page = newPage
    fetchList()
  }

  /**
   * Handle selection change
   * @param {Array<Object>} selection - Selected items
   */
  function handleSelectionChange(selection) {
    selectedItems.value = selection
  }

  /**
   * Handle error consistently
   * @param {Error} err - Error object
   * @param {string} operation - Operation that failed
   * @private
   */
  function handleError(err, operation) {
    error.value = err

    const errorMessage = err.message || `${operation} failed`

    // Show user-friendly error message
    ElMessage.error(errorMessage)

    // Call error callback if provided
    if (onError) {
      onError(operation, err)
    }

    // Log for debugging
    console.error(`[useEntityManagement] ${operation} failed:`, err)
  }

  /**
   * Start auto-refresh timer
   * @private
   */
  function startAutoRefresh() {
    if (refreshTimer) {
      clearInterval(refreshTimer)
    }

    if (autoRefresh && refreshInterval > 0) {
      refreshTimer = setInterval(() => {
        fetchList()
      }, refreshInterval)
    }
  }

  /**
   * Stop auto-refresh timer
   * @private
   */
  function stopAutoRefresh() {
    if (refreshTimer) {
      clearInterval(refreshTimer)
      refreshTimer = null
    }
  }

  /**
   * Cleanup function to stop auto-refresh
   */
  function cleanup() {
    stopAutoRefresh()
  }

  // Auto-load data on mount if enabled
  if (autoLoad && entityConfig.value) {
    fetchList()
  }

  // Start auto-refresh if enabled
  if (autoRefresh) {
    startAutoRefresh()
  }

  // Return reactive state and methods
  return {
    // State
    loading,
    error,
    data,
    selectedItems,
    pagination,
    searchParams,
    formVisible,
    formMode,
    formData,
    formErrors,
    entityConfig,

    // Computed
    hasData: computed(() => data.value.length > 0),
    hasError: computed(() => error.value !== null),
    isLoading: computed(() => loading.value),
    selectedCount: computed(() => selectedItems.value.length),

    // CRUD Operations
    fetchList,
    fetchById,
    createEntity,
    updateEntity,
    deleteEntity,
    batchDeleteEntities,
    performAction,

    // Form Management
    openCreateForm,
    openEditForm,
    closeForm,
    validateFormData,

    // Search & Filter
    handleSearchChange,
    resetSearch,

    // Pagination
    handleSizeChange,
    handlePageChange,

    // Selection
    handleSelectionChange,

    // Utility
    cleanup
  }
}

export default useEntityManagement