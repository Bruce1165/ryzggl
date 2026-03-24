<template>
  <div class="action-buttons">
    <slot></slot>

    <!-- 默认操作按钮 -->
    <template v-if="showDefault">
      <el-button
        v-if="showView"
        type="primary"
        size="small"
        link
        @click="handleView"
      >
        查看
      </el-button>

      <el-button
        v-if="showEdit"
        type="primary"
        size="small"
        link
        @click="handleEdit"
      >
        编辑
      </el-button>

      <el-button
        v-if="showDelete"
        type="danger"
        size="small"
        link
        @click="handleDelete"
      >
        删除
      </el-button>

      <el-dropdown v-if="hasMore" @command="handleCommand" trigger="click">
        <el-button type="primary" size="small" link>
          更多
          <el-icon class="el-icon--right"><arrow-down /></el-icon>
        </el-button>
        <template #dropdown>
          <el-dropdown-menu>
            <slot name="more-actions">
              <el-dropdown-item command="export">导出</el-dropdown-item>
              <el-dropdown-item command="print">打印</el-dropdown-item>
            </slot>
          </el-dropdown-menu>
        </template>
      </el-dropdown>
    </template>
  </div>
</template>

<script>
import { ArrowDown } from '@element-plus/icons-vue'

export default {
  name: 'ActionButtons',

  components: {
    ArrowDown
  },

  props: {
    showDefault: {
      type: Boolean,
      default: true
    },
    showView: {
      type: Boolean,
      default: true
    },
    showEdit: {
      type: Boolean,
      default: true
    },
    showDelete: {
      type: Boolean,
      default: true
    },
    hasMore: {
      type: Boolean,
      default: true
    }
  },

  emits: [
    'view',
    'edit',
    'delete',
    'command'
  ],

  setup(props, { emit }) {
    /**
     * Handle view button click
     */
    const handleView = () => {
      emit('view')
    }

    /**
     * Handle edit button click
     */
    const handleEdit = () => {
      emit('edit')
    }

    /**
     * Handle delete button click
     */
    const handleDelete = () => {
      emit('delete')
    }

    /**
     * Handle dropdown command
     */
    const handleCommand = (command) => {
      emit('command', command)
    }

    return {
      handleView,
      handleEdit,
      handleDelete,
      handleCommand
    }
  }
}
</script>

<style scoped>
.action-buttons {
  display: flex;
  gap: 8px;
  flex-wrap: wrap;
}

.el-icon--right {
  margin-left: 5px;
}
</style>
