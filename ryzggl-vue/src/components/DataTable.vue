<template>
  <el-table
    v-loading="loading"
    :data="data"
    :stripe="stripe"
    :border="border"
    :height="height"
    :max-height="maxHeight"
    :size="size"
    :empty-text="emptyText"
    @sort-change="handleSortChange"
    @selection-change="handleSelectionChange"
    @row-click="handleRowClick"
  >
    <slot></slot>

    <!-- 默认操作列 -->
    <el-table-column
      v-if="showAction"
      label="操作"
      :width="actionWidth"
      :fixed="actionFixed"
      align="center"
    >
      <template #default="{ row, $index }">
        <slot name="actions" :row="row" :index="$index"></slot>
      </template>
    </el-table-column>
  </el-table>

  <!-- 分页 -->
  <div v-if="showPagination" class="table-pagination">
    <el-pagination
      v-model:current-page="pagination.current"
      v-model:page-size="pagination.size"
      :total="pagination.total"
      :page-sizes="pageSizes"
      :layout="paginationLayout"
      :background="background"
      @size-change="handleSizeChange"
      @current-change="handleCurrentChange"
    />
  </div>
</template>

<script>
import { computed } from 'vue'

export default {
  name: 'DataTable',

  props: {
    data: {
      type: Array,
      default: () => []
    },
    loading: {
      type: Boolean,
      default: false
    },
    stripe: {
      type: Boolean,
      default: true
    },
    border: {
      type: Boolean,
      default: true
    },
    height: [String, Number],
    maxHeight: [String, Number],
    size: {
      type: String,
      default: 'default'
    },
    emptyText: {
      type: String,
      default: '暂无数据'
    },
    // 操作列配置
    showAction: {
      type: Boolean,
      default: true
    },
    actionWidth: {
      type: Number,
      default: 200
    },
    actionFixed: {
      type: String,
      default: 'right'
    },
    // 分页配置
    showPagination: {
      type: Boolean,
      default: true
    },
    pagination: {
      type: Object,
      default: () => ({
        current: 1,
        size: 10,
        total: 0
      })
    },
    pageSizes: {
      type: Array,
      default: () => [10, 20, 50, 100]
    },
    paginationLayout: {
      type: String,
      default: 'total, sizes, prev, pager, next, jumper'
    },
    background: {
      type: Boolean,
      default: true
    }
  },

  emits: [
    'sort-change',
    'selection-change',
    'row-click',
    'size-change',
    'current-change'
  ],

  setup(props, { emit }) {
    /**
     * Handle sort change
     */
    const handleSortChange = (...args) => {
      emit('sort-change', ...args)
    }

    /**
     * Handle selection change
     */
    const handleSelectionChange = (...args) => {
      emit('selection-change', ...args)
    }

    /**
     * Handle row click
     */
    const handleRowClick = (...args) => {
      emit('row-click', ...args)
    }

    /**
     * Handle page size change
     */
    const handleSizeChange = (...args) => {
      emit('size-change', ...args)
    }

    /**
     * Handle current page change
     */
    const handleCurrentChange = (...args) => {
      emit('current-change', ...args)
    }

    return {
      handleSortChange,
      handleSelectionChange,
      handleRowClick,
      handleSizeChange,
      handleCurrentChange
    }
  }
}
</script>

<style scoped>
.table-pagination {
  margin-top: 20px;
  display: flex;
  justify-content: center;
}
</style>
