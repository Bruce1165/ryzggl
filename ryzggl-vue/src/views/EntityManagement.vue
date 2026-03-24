<template>
  <div class="entity-management">
    <el-card class="box-card">
      <template #header>
        <div class="card-header">
          <span>{{ currentEntityTitle }}管理</span>
          <el-button
            type="primary"
            @click="handleCreate"
            :icon="Plus"
            :disabled="isLoading"
          >
            新增{{ currentEntityName }}
          </el-button>
        </div>
      </template>

      <!-- Entity Search Component -->
      <EntitySearch
        v-model="searchParams"
        :search-fields="searchFields"
        :filters="entityFilters"
        :disabled="isLoading"
        @search="handleSearch"
        @reset="handleReset"
        @field-change="handleFieldChange"
      />

      <!-- Data Table -->
      <el-table
        :data="data"
        stripe
        style="width: 100%"
        v-loading="isLoading"
        :empty-text="hasError ? '数据加载失败' : '暂无数据'"
        @selection-change="handleSelectionChange"
      >
        <el-table-column
          v-if="showSelection"
          type="selection"
          width="55"
        />

        <el-table-column
          v-for="column in tableColumns"
          :key="column.prop"
          :prop="column.prop"
          :label="column.label"
          :width="column.width"
          :min-width="column.minWidth"
          :fixed="column.fixed"
        />

        <el-table-column
          v-if="showActions"
          label="操作"
          width="200"
          fixed="right"
        >
          <template #default="{ row }">
            <el-button
              type="primary"
              size="small"
              @click="handleEdit(row)"
              :icon="Edit"
              :disabled="isLoading"
            >
              编辑
            </el-button>
            <el-button
              type="danger"
              size="small"
              @click="handleDelete(row)"
              :icon="Delete"
              :disabled="isLoading"
            >
              删除
            </el-button>
          </template>
        </el-table-column>
      </el-table>

      <!-- Pagination -->
      <div class="pagination-container">
        <el-pagination
          v-model:current-page="pagination.page"
          v-model:page-size="pagination.size"
          :page-sizes="[10, 20, 50, 100]"
          :total="pagination.total"
          layout="total, sizes, prev, pager, next, jumper"
          :disabled="isLoading"
          @size-change="handleSizeChange"
          @current-change="handlePageChange"
        />
      </div>
    </el-card>

    <!-- Form Dialog -->
    <el-dialog
      v-model="formVisible"
      :title="dialogTitle"
      width="800px"
      :close-on-click-modal="false"
      :close-on-press-escape="false"
    >
      <EntityForm
        ref="formRef"
        v-model="formData"
        :fields="formFields"
        :rules="formRules"
        :disabled="isSubmitting"
        :columns="2"
        @submit="handleSubmit"
      />

      <template #footer>
        <div class="dialog-footer">
          <el-button
            @click="handleFormCancel"
            :disabled="isSubmitting"
          >
            取消
          </el-button>
          <el-button
            type="primary"
            @click="handleSubmit"
            :loading="isSubmitting"
          >
            确定
          </el-button>
        </div>
      </template>
    </el-dialog>

    <!-- Error Alert -->
    <el-alert
      v-if="hasError"
      :title="error.message || '操作失败'"
      type="error"
      :closable="true"
      @close="handleErrorClear"
      style="margin-top: 20px"
    />
  </div>
</template>

<script setup>
import { ref, computed, onMounted, onUnmounted, watch } from 'vue'
import { useRoute } from 'vue-router'
import { Plus, Edit, Delete } from '@element-plus/icons-vue'
import { ElMessage, ElMessageBox } from 'element-plus'

// Import new components and composables
import EntitySearch from '../components/EntitySearch.vue'
import EntityForm from '../components/EntityForm.vue'
import useEntityManagement from '../composables/useEntityManagement'
import { getEntityConfig, getEntityOptions } from '../utils/entityRegistry'

const route = useRoute()

/**
 * Get entity type from route params or default
 */
const entityType = computed(() => route.params.type || 'worker')

/**
 * Use the entity management composable
 */
const {
  loading: isLoading,
  error,
  data,
  selectedItems,
  pagination,
  searchParams,
  formVisible,
  formMode,
  formData,
  entityConfig,
  hasData,
  hasError,
  selectedCount,

  // CRUD operations
  fetchList,
  fetchById,
  createEntity,
  updateEntity,
  deleteEntity,
  batchDeleteEntities,

  // Form management
  openCreateForm,
  openEditForm,
  closeForm,

  // Search & pagination
  handleSearchChange,
  resetSearch,
  handleSizeChange,
  handlePageChange,
  handleSelectionChange,

  // Cleanup
  cleanup
} = useEntityManagement(entityType.value, {
  autoLoad: false, // We'll handle loading manually
  pageSize: 10,
  onSuccess: (operation, response) => {
    console.log(`Operation ${operation} succeeded:`, response)
  },
  onError: (operation, err) => {
    console.error(`Operation ${operation} failed:`, err)
  }
})

/**
 * Form submission state
 */
const isSubmitting = ref(false)
const formRef = ref(null)

/**
 * Computed properties from entity configuration
 */
const currentEntityTitle = computed(() => entityConfig.value?.title || '实体')
const currentEntityName = computed(() => entityConfig.value?.name || '实体')
const tableColumns = computed(() => entityConfig.value?.tableColumns || [])
const formFields = computed(() => entityConfig.value?.formFields || [])
const searchFields = computed(() => entityConfig.value?.searchFields || [])

/**
 * Form rules (can be customized per entity)
 */
const formRules = computed(() => {
  // Basic rules - can be extended per entity type
  return {}
})

/**
 * Entity-specific filters
 */
const entityFilters = computed(() => {
  const filters = []

  // Add certificate status filter for certificate entities
  if (entityType.value === 'certificate') {
    filters.push({
      prop: 'status',
      label: '证书状态',
      type: 'select',
      options: ['有效', '过期', '暂停', '锁定']
    })
  }

  // Add apply status filter for apply entities
  if (entityType.value === 'apply') {
    filters.push({
      prop: 'applyStatus',
      label: '申请状态',
      type: 'select',
      options: ['待提交', '待审核', '已通过', '已拒绝']
    })
  }

  // Add continue status filter for certificate continue
  if (entityType.value === 'certificateContinue') {
    filters.push({
      prop: 'continueStatus',
      label: '延续状态',
      type: 'select',
      options: ['未延续', '已延续', '已过期']
    })
  }

  return filters
})

/**
 * Dialog title
 */
const dialogTitle = computed(() => {
  return formMode.value === 'create'
    ? `新增${currentEntityName.value}`
    : `编辑${currentEntityName.value}`
})

/**
 * UI options
 */
const showSelection = computed(() => false) // Can be enabled for batch operations
const showActions = computed(() => true)

/**
 * Handle create action
 */
function handleCreate() {
  openCreateForm()
}

/**
 * Handle edit action
 */
function handleEdit(row) {
  openEditForm(row)
}

/**
 * Handle delete action
 */
async function handleDelete(row) {
  try {
    const confirmMessage = `确定要删除这条${currentEntityName.value}记录吗？`
    await deleteEntity(row.id, confirmMessage)
  } catch (error) {
    if (error !== 'cancel') {
      console.error('Delete failed:', error)
    }
  }
}

/**
 * Handle form submission
 */
async function handleSubmit() {
  try {
    isSubmitting.value = true

    // Validate form
    const isValid = await formRef.value?.validate()
    if (!isValid) {
      ElMessage.warning('请检查表单填写是否正确')
      return
    }

    // Create or update based on mode
    if (formMode.value === 'create') {
      await createEntity(formData)
    } else {
      await updateEntity(formData.id, formData)
    }

    // Close form and refresh
    closeForm()
    await fetchList()
  } catch (error) {
    console.error('Form submission failed:', error)
    ElMessage.error(error.message || '操作失败')
  } finally {
    isSubmitting.value = false
  }
}

/**
 * Handle form cancel
 */
function handleFormCancel() {
  closeForm()
}

/**
 * Handle search action
 */
function handleSearch() {
  handleSearchChange(searchParams.value)
}

/**
 * Handle reset action
 */
function handleReset() {
  resetSearch()
}

/**
 * Handle field change
 */
function handleFieldChange(field) {
  console.log('Search field changed to:', field)
}

/**
 * Handle error clear
 */
function handleErrorClear() {
  // Error is cleared automatically when new operations occur
}

/**
 * Watch for route parameter changes
 */
watch(() => route.params.type, (newType) => {
  if (newType) {
    // Entity type changed, reset and reload
    resetSearch()
    fetchList()
  }
}, { immediate: true })

/**
 * Lifecycle hooks
 */
onMounted(async () => {
  // Load initial data
  try {
    await fetchList()
  } catch (error) {
    console.error('Failed to load initial data:', error)
  }
})

// Cleanup on unmount
onUnmounted(() => {
  cleanup()
})
</script>

<style scoped>
.entity-management {
  padding: 20px;
  min-height: calc(100vh - 120px);
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.pagination-container {
  margin-top: 20px;
  display: flex;
  justify-content: flex-end;
  align-items: center;
  gap: 10px;
}

.dialog-footer {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
}

/* Responsive adjustments */
@media (max-width: 768px) {
  .entity-management {
    padding: 10px;
  }

  .pagination-container {
    flex-direction: column;
    align-items: stretch;
  }
}

/* Loading state */
.el-table {
  min-height: 400px;
}

/* Empty state improvements */
.el-table :deep(.el-table__empty-text) {
  color: var(--el-text-color-secondary);
}

/* Error state */
.el-alert {
  animation: fadeIn 0.3s ease-in-out;
}

@keyframes fadeIn {
  from {
    opacity: 0;
    transform: translateY(-10px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

/* Accessibility improvements */
.el-button:focus-visible {
  outline: 2px solid var(--el-color-primary);
  outline-offset: 2px;
}

/* Table row hover effect */
.el-table :deep(.el-table__row:hover) {
  background-color: var(--el-table-row-hover-bg-color);
}

/* Loading overlay */
.el-loading-mask {
  background-color: rgba(255, 255, 255, 0.7);
}

/* Dialog accessibility */
.el-dialog {
  border-radius: 8px;
}

.el-dialog :deep(.el-dialog__header) {
  padding: 20px 20px 10px;
}

.el-dialog :deep(.el-dialog__body) {
  padding: 20px;
  max-height: 70vh;
  overflow-y: auto;
}
</style>