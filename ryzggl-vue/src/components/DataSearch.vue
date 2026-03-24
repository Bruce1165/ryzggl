<template>
  <div class="data-search">
    <el-form
      ref="formRef"
      :model="formData"
      :inline="inline"
      :size="size"
      class="search-form"
    >
      <slot :form="formData"></slot>

      <!-- 默认搜索和重置按钮 -->
      <template v-if="showActions">
        <el-form-item>
          <slot name="actions">
            <el-button
              type="primary"
              :icon="Search"
              :loading="loading"
              @click="handleSearch"
            >
              {{ searchText }}
            </el-button>
            <el-button
              :icon="Refresh"
              @click="handleReset"
            >
              {{ resetText }}
            </el-button>
          </slot>
        </el-form-item>
      </template>
    </el-form>
  </div>
</template>

<script>
import { ref, computed } from 'vue'
import { Search, Refresh } from '@element-plus/icons-vue'

export default {
  name: 'DataSearch',

  components: {
    Search,
    Refresh
  },

  props: {
    modelValue: {
      type: Object,
      default: () => ({})
    },
    inline: {
      type: Boolean,
      default: true
    },
    size: {
      type: String,
      default: 'default'
    },
    showActions: {
      type: Boolean,
      default: true
    },
    loading: {
      type: Boolean,
      default: false
    },
    searchText: {
      type: String,
      default: '查询'
    },
    resetText: {
      type: String,
      default: '重置'
    }
  },

  emits: [
    'update:modelValue',
    'search',
    'reset'
  ],

  setup(props, { emit }) {
    const formRef = ref(null)

    /**
     * Computed form data with two-way binding
     */
    const formData = computed({
      get: () => props.modelValue,
      set: (value) => emit('update:modelValue', value)
    })

    /**
     * Handle search button click
     */
    const handleSearch = () => {
      emit('search', props.modelValue)
    }

    /**
     * Handle reset button click
     */
    const handleReset = () => {
      if (formRef.value) {
        formRef.value.resetFields()
      }
      emit('reset')
    }

    return {
      formRef,
      formData,
      handleSearch,
      handleReset
    }
  }
}
</script>

<style scoped>
.data-search {
  margin-bottom: 20px;
}

.search-form {
  background: #f5f7fa;
  padding: 20px;
  border-radius: 4px;
}
</style>
