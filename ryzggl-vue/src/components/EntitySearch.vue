<template>
  <el-form
    :inline="inline"
    :model="searchParams"
    :label-width="labelWidth"
    class="entity-search"
    @submit.prevent="handleSearch"
  >
    <el-row :gutter="gutter">
      <!-- Keyword Search -->
      <el-col :span="keywordSpan">
        <el-form-item label="关键词">
          <el-input
            v-model="searchParams.keyword"
            :placeholder="placeholder || '输入关键字搜索'"
            :clearable="true"
            :disabled="disabled"
            @clear="handleClear"
            @keyup.enter="handleSearch"
          >
            <template #prepend>
              <el-select
                v-model="searchParams.field"
                style="width: 120px"
                placeholder="字段"
                :disabled="disabled"
                @change="handleFieldChange"
              >
                <el-option label="全部" value="" />
                <el-option
                  v-for="field in searchFields"
                  :key="field.value"
                  :label="field.label"
                  :value="field.value"
                />
              </el-select>
            </template>
            <template #append>
              <el-button
                :icon="Search"
                :disabled="disabled"
                @click="handleSearch"
              />
            </template>
          </el-input>
        </el-form-item>
      </el-col>

      <!-- Advanced Filters -->
      <el-col
        v-for="filter in visibleFilters"
        :key="filter.prop"
        :span="filter.span || 6"
      >
        <el-form-item :label="filter.label">
          <!-- Select Filter -->
          <el-select
            v-if="filter.type === 'select'"
            v-model="searchParams[filter.prop]"
            :placeholder="`选择${filter.label}`"
            :clearable="true"
            :disabled="disabled"
            :multiple="filter.multiple"
            style="width: 100%"
            @change="handleFilterChange"
          >
            <el-option
              v-for="option in filter.options"
              :key="option.value !== undefined ? option.value : option"
              :label="option.label !== undefined ? option.label : option"
              :value="option.value !== undefined ? option.value : option"
            />
          </el-select>

          <!-- Date Range Filter -->
          <el-date-picker
            v-else-if="filter.type === 'dateRange'"
            v-model="searchParams[filter.prop]"
            type="daterange"
            :range-separator="'至'"
            :start-placeholder="`开始${filter.label}`"
            :end-placeholder="`结束${filter.label}`"
            :disabled="disabled"
            :clearable="true"
            style="width: 100%"
            @change="handleFilterChange"
          />

          <!-- Date Filter -->
          <el-date-picker
            v-else-if="filter.type === 'date'"
            v-model="searchParams[filter.prop]"
            type="date"
            :placeholder="`选择${filter.label}`"
            :disabled="disabled"
            :clearable="true"
            style="width: 100%"
            @change="handleFilterChange"
          />

          <!-- Number Range Filter -->
          <el-input-number
            v-else-if="filter.type === 'numberRange'"
            v-model="searchParams[filter.prop]"
            :placeholder="`输入${filter.label}`"
            :disabled="disabled"
            :controls="false"
            style="width: 100%"
            @change="handleFilterChange"
          />

          <!-- Text Filter -->
          <el-input
            v-else
            v-model="searchParams[filter.prop]"
            :placeholder="`输入${filter.label}`"
            :clearable="true"
            :disabled="disabled"
            @keyup.enter="handleSearch"
            @change="handleFilterChange"
          />
        </el-form-item>
      </el-col>

      <!-- Action Buttons -->
      <el-col :span="buttonSpan" class="action-buttons">
        <el-form-item>
          <el-button
            type="primary"
            :icon="Search"
            :disabled="disabled"
            @click="handleSearch"
          >
            查询
          </el-button>
          <el-button
            :icon="RefreshLeft"
            :disabled="disabled"
            @click="handleReset"
          >
            重置
          </el-button>
          <el-button
            v-if="showAdvancedToggle"
            :icon="isAdvancedVisible ? ArrowUp : ArrowDown"
            text
            @click="toggleAdvanced"
          >
            {{ isAdvancedVisible ? '收起' : '高级搜索' }}
          </el-button>
        </el-form-item>
      </el-col>
    </el-row>

    <!-- Advanced Search Panel -->
    <el-collapse-transition>
      <div v-show="isAdvancedVisible" class="advanced-search">
        <el-row :gutter="gutter">
          <el-col
            v-for="filter in advancedFilters"
            :key="filter.prop"
            :span="filter.span || 6"
          >
            <el-form-item :label="filter.label">
              <!-- Reuse same filter logic -->
              <el-select
                v-if="filter.type === 'select'"
                v-model="searchParams[filter.prop]"
                :placeholder="`选择${filter.label}`"
                :clearable="true"
                :disabled="disabled"
                :multiple="filter.multiple"
                style="width: 100%"
                @change="handleFilterChange"
              >
                <el-option
                  v-for="option in filter.options"
                  :key="option.value !== undefined ? option.value : option"
                  :label="option.label !== undefined ? option.label : option"
                  :value="option.value !== undefined ? option.value : option"
                />
              </el-select>

              <el-date-picker
                v-else-if="filter.type === 'dateRange'"
                v-model="searchParams[filter.prop]"
                type="daterange"
                :range-separator="'至'"
                :start-placeholder="`开始${filter.label}`"
                :end-placeholder="`结束${filter.label}`"
                :disabled="disabled"
                :clearable="true"
                style="width: 100%"
                @change="handleFilterChange"
              />

              <el-input
                v-else
                v-model="searchParams[filter.prop]"
                :placeholder="`输入${filter.label}`"
                :clearable="true"
                :disabled="disabled"
                @keyup.enter="handleSearch"
                @change="handleFilterChange"
              />
            </el-form-item>
          </el-col>
        </el-row>
      </div>
    </el-collapse-transition>
  </el-form>
</template>

<script setup>
import { ref, reactive, computed, watch } from 'vue'
import { Search, RefreshLeft, ArrowUp, ArrowDown } from '@element-plus/icons-vue'

/**
 * Props
 */
const props = defineProps({
  // Search fields for dropdown
  searchFields: {
    type: Array,
    default: () => [],
    validator: (value) => {
      return value.every(field => field.value && field.label)
    }
  },

  // Additional filters
  filters: {
    type: Array,
    default: () => []
  },

  // Current search parameters (v-model)
  modelValue: {
    type: Object,
    default: () => ({})
  },

  // Layout options
  inline: {
    type: Boolean,
    default: true
  },

  gutter: {
    type: Number,
    default: 20
  },

  labelWidth: {
    type: [String, Number],
    default: '80px'
  },

  keywordSpan: {
    type: Number,
    default: 8
  },

  buttonSpan: {
    type: Number,
    default: 8
  },

  // UI options
  placeholder: {
    type: String,
    default: ''
  },

  disabled: {
    type: Boolean,
    default: false
  },

  showAdvancedToggle: {
    type: Boolean,
    default: true
  },

  // Debounce delay for search (ms)
  debounceDelay: {
    type: Number,
    default: 300
  }
})

/**
 * Emits
 */
const emit = defineEmits([
  'update:modelValue',
  'search',
  'reset',
  'field-change',
  'filter-change',
  'clear'
])

/**
 * Reactive state
 */
const searchParams = reactive({
  keyword: '',
  field: '',
  ...props.modelValue
})

const isAdvancedVisible = ref(false)

/**
 * Separate basic and advanced filters
 */
const visibleFilters = computed(() => {
  return props.filters.filter(filter => !filter.advanced)
})

const advancedFilters = computed(() => {
  return props.filters.filter(filter => filter.advanced)
})

/**
 * Toggle advanced search panel
 */
function toggleAdvanced() {
  isAdvancedVisible.value = !isAdvancedVisible.value
}

/**
 * Handle keyword field change
 */
function handleFieldChange() {
  emit('field-change', searchParams.field)
  emit('update:modelValue', { ...searchParams })
}

/**
 * Handle filter change
 */
function handleFilterChange() {
  emit('filter-change', searchParams)
  emit('update:modelValue', { ...searchParams })
}

/**
 * Handle search action
 */
function handleSearch() {
  emit('update:modelValue', { ...searchParams })
  emit('search', { ...searchParams })
}

/**
 * Handle reset action
 */
function handleReset() {
  // Reset search parameters
  Object.keys(searchParams).forEach(key => {
    if (key === 'keyword' || key === 'field') {
      searchParams[key] = ''
    } else {
      delete searchParams[key]
    }
  })

  // Hide advanced panel
  isAdvancedVisible.value = false

  // Emit events
  emit('update:modelValue', { ...searchParams })
  emit('reset')
}

/**
 * Handle clear action
 */
function handleClear() {
  searchParams.keyword = ''
  emit('clear')
  emit('update:modelValue', { ...searchParams })
}

/**
 * Expose methods for programmatic control
 */
defineExpose({
  search: handleSearch,
  reset: handleReset,
  getSearchParams: () => ({ ...searchParams }),
  setSearchParams: (params) => {
    Object.assign(searchParams, params)
    emit('update:modelValue', { ...searchParams })
  }
})

/**
 * Watch for model value changes
 */
watch(() => props.modelValue, (newValue) => {
  if (newValue && typeof newValue === 'object') {
    Object.assign(searchParams, newValue)
  }
}, { deep: true })

/**
 * Watch for search field changes
 */
watch(() => props.searchFields, () => {
  // Reset field selection if current field is no longer available
  const fieldExists = props.searchFields.some(field => field.value === searchParams.field)
  if (!fieldExists) {
    searchParams.field = ''
  }
}, { deep: true })
</script>

<style scoped>
.entity-search {
  margin-bottom: 20px;
  padding: 20px;
  background: var(--el-bg-color-page);
  border-radius: 4px;
}

.advanced-search {
  margin-top: 20px;
  padding-top: 20px;
  border-top: 1px solid var(--el-border-color);
}

.action-buttons {
  display: flex;
  align-items: flex-end;
  justify-content: flex-end;
}

.action-buttons .el-form-item {
  margin-bottom: 0;
}

/* Responsive adjustments */
@media (max-width: 768px) {
  .entity-search {
    padding: 10px;
  }

  .el-col {
    margin-bottom: 10px;
  }
}

/* Accessibility improvements */
.el-form-item :deep(.el-input__wrapper),
.el-form-item :deep(.el-select .el-input__wrapper) {
  transition: box-shadow 0.2s;
}

.el-form-item :deep(.el-input__wrapper:focus),
.el-form-item :deep(.el-select:focus .el-input__wrapper) {
  box-shadow: 0 0 0 2px var(--el-color-primary) inset;
}

/* Button group spacing */
.action-buttons .el-button + .el-button {
  margin-left: 10px;
}
</style>