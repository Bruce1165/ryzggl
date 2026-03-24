<template>
  <div class="check-task-detail-container">
    <!-- App Header -->
    <AppHeader />

    <!-- Page Header -->
    <div class="page-header">
      <h2>业务申请单抽任务详情</h2>
      <div class="breadcrumb">
        当前位置: 任务管理 &gt; <span v-if="taskInfo">业务申请单抽任务</span>
      </div>
    </div>

    <div v-loading="loading">
      <!-- Task Info Card -->
      <el-card v-if="taskInfo" class="info-card">
        <template #header>
          <div class="card-header">
            <span>任务信息</span>
            <div class="header-actions">
              <el-button size="small" icon="Back" @click="handleBack">返回</el-button>
              <el-button
                type="primary"
                size="small"
                icon="Refresh"
                @click="handleGenerateItems"
                :disabled="!canGenerateItems"
              >
                重新生成抽查项
              </el-button>
            </div>
          </div>
        </template>

        <el-descriptions :column="3" border>
          <el-descriptions-item label="任务ID">{{ taskInfo.taskID }}</el-descriptions-item>
          <el-descriptions-item label="任务名称">{{ taskInfo.taskName || '-' }}</el-descriptions-item>
          <el-descriptions-item label="业务范围">
            {{ getBusinessRangeName(taskInfo.busRangeCode) }}
          </el-descriptions-item>
          <el-descriptions-item label="抽查比例">
            {{ taskInfo.checkPer / 10 }}%
          </el-descriptions-item>
          <el-descriptions-item label="业务开始日期">{{ taskInfo.busStartDate }}</el-descriptions-item>
          <el-descriptions-item label="业务结束日期">{{ taskInfo.busEndDate }}</el-descriptions-item>
          <el-descriptions-item label="抽查总数">{{ taskInfo.itemCount || 0 }}</el-descriptions-item>
          <el-descriptions-item label="已审核数">{{ taskInfo.checkedCount || 0 }}</el-descriptions-item>
          <el-descriptions-item label="待审核数">{{ uncheckedCount }}</el-descriptions-item>
          <el-descriptions-item label="任务状态">
            <el-tag :type="getStatusType(taskInfo.taskStatus)">
              {{ taskInfo.taskStatus }}
            </el-tag>
          </el-descriptions-item>
          <el-descriptions-item label="创建时间">{{ taskInfo.cjsj }}</el-descriptions-item>
          <el-descriptions-item label="备注" :span="2">
            {{ taskInfo.remark || '-' }}
          </el-descriptions-item>
        </el-descriptions>
      </el-card>

      <!-- Task Progress -->
      <el-card v-if="taskInfo" class="progress-card">
        <template #header>
          <span>任务进度</span>
        </template>
        <div class="progress-container">
          <el-progress
            :percentage="progressPercentage"
            :status="progressStatus"
            :stroke-width="20"
          />
          <div class="progress-text">
            {{ taskInfo.checkedCount || 0 }} / {{ taskInfo.itemCount || 0 }}
          </div>
        </div>
      </el-card>

      <!-- Filter for Task Items -->
      <el-card class="filter-card">
        <el-form :inline="true" :model="filterForm">
          <el-form-item label="审核状态">
            <el-radio-group v-model="filterForm.status" @change="handleFilterChange">
              <el-radio label="">全部</el-radio>
              <el-radio label="unchecked">待审核</el-radio>
              <el-radio label="checked">已审核</el-radio>
            </el-radio-group>
          </el-form-item>
        </el-form>
      </el-card>

      <!-- Task Items Table -->
      <el-card class="items-card">
        <template #header>
          <div class="card-header">
            <span>抽查项列表 ({{ filteredItems.length }}条)</span>
            <div class="header-actions">
              <el-checkbox v-model="selectAll" @change="handleSelectAll">
                全选
              </el-checkbox>
              <el-button
                type="success"
                size="small"
                @click="handleBatchApprove"
                :disabled="!hasCheckedItem"
              >
                批量通过
              </el-button>
              <el-button
                type="danger"
                size="small"
                @click="handleBatchReject"
                :disabled="!hasCheckedItem"
              >
                批量不通过
              </el-button>
            </div>
          </div>
        </template>

        <el-table
          :data="filteredItems"
          v-loading="itemsLoading"
          stripe
          @selection-change="handleSelectionChange"
          ref="tableRef"
          style="width: 100%"
        >
          <el-table-column type="selection" width="55" />
          <el-table-column type="index" label="序号" width="60" />
          <el-table-column prop="workerName" label="人员姓名" width="100" />
          <el-table-column prop="idCard" label="身份证号" width="150" />
          <el-table-column prop="dataID" label="数据ID" width="120" />
          <el-table-column prop="applyTableName" label="来源表" width="120">
            <template #default="{ row }">
              {{ getSourceTableName(row.applyTableName) }}
            </template>
          </el-table-column>
          <el-table-column prop="applyType" label="申请类型" width="100" />
          <el-table-column prop="certificateCode" label="证书编号" width="150" />
          <el-table-column prop="applyFinishTime" label="完成时间" width="120" />
          <el-table-column prop="checkMan" label="审核人" width="100" />
          <el-table-column prop="checkTime" label="审核时间" width="120" />
          <el-table-column prop="checkResult" label="审核结果" width="100">
            <template #default="{ row }">
              <el-tag v-if="row.checkResult" :type="getCheckResultType(row.checkResult)">
                {{ row.checkResult }}
              </el-tag>
              <span v-else>-</span>
            </template>
          </el-table-column>
          <el-table-column prop="checkResultDesc" label="审核意见" min-width="150" show-overflow-tooltip />
          <el-table-column label="操作" width="150" fixed="right">
            <template #default="{ row }">
              <el-button
                v-if="!row.checkTime"
                type="primary"
                size="small"
                @click="handleApproveItem(row)"
              >
                审核
              </el-button>
              <el-button
                v-if="!row.checkTime"
                type="warning"
                size="small"
                @click="handleRejectItem(row)"
              >
                不通过
              </el-button>
              <el-button
                v-if="row.checkTime"
                type="info"
                size="small"
                @click="handleViewDetail(row)"
              >
                查看
              </el-button>
            </template>
          </el-table-column>
        </el-table>
      </el-card>
    </div>

    <!-- Approve Item Dialog -->
    <el-dialog
      v-model="approveDialogVisible"
      :title="approveDialogTitle"
      width="600px"
    >
      <el-form
        ref="approveFormRef"
        :model="approveForm"
        :rules="approveRules"
        label-width="100px"
      >
        <el-form-item label="审核人" prop="checkMan">
          <el-input v-model="approveForm.checkMan" placeholder="输入审核人姓名" />
        </el-form-item>

        <el-form-item label="审核结果" prop="checkResult" v-if="isApproveMode">
          <el-radio-group v-model="approveForm.checkResult">
            <el-radio label="通过">通过</el-radio>
            <el-radio label="不通过">不通过</el-radio>
          </el-radio-group>
        </el-form-item>

        <el-form-item label="审核意见" prop="checkResultDesc">
          <el-input
            v-model="approveForm.checkResultDesc"
            type="textarea"
            :rows="4"
            :placeholder="isApproveMode ? '请输入审核意见（可选）' : '请输入不通过原因'"
          />
        </el-form-item>
      </el-form>

      <template #footer>
        <el-button @click="approveDialogVisible = false">取消</el-button>
        <el-button type="primary" :loading="approving" @click="handleApproveSubmit">
          {{ approving ? '提交中...' : '确定' }}
        </el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script>
import { ref, reactive, computed, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { ElMessage } from 'element-plus'
import {
  getCheckTaskById,
  getTaskItems,
  approveTaskItem,
  rejectTaskItem,
  batchApproveItems,
  generateCheckItems
} from '@/api/checkTask'
import AppHeader from '@/components/AppHeader.vue'

export default {
  name: 'CheckTaskDetail',

  components: {
    AppHeader
  },

  setup() {
    const router = useRouter()
    const route = useRoute()
    const tableRef = ref(null)
    const approveFormRef = ref(null)
    const loading = ref(false)
    const itemsLoading = ref(false)
    const approveDialogVisible = ref(false)
    const approving = ref(false)
    const isApproveMode = ref(true)
    const selectAll = ref(false)

    // Task info
    const taskInfo = ref(null)

    // Filter form
    const filterForm = reactive({
      status: ''
    })

    // Selected items for batch operation
    const selectedItems = ref([])

    // Current item being edited
    const currentItem = ref(null)

    // Approve form
    const approveForm = reactive({
      checkMan: '',
      checkResult: '通过',
      checkResultDesc: ''
    })

    // Approve form validation rules
    const approveRules = {
      checkMan: [{ required: true, message: '请输入审核人姓名', trigger: 'blur' }],
      checkResultDesc: [{ required: true, message: '请输入审核意见', trigger: 'blur' }]
    }

    /**
     * Can generate items?
     */
    const canGenerateItems = computed(() => {
      return taskInfo.value &&
        (taskInfo.value.taskStatus === '进行中' || !taskInfo.value.taskStatus)
    })

    /**
     * Unchecked count
     */
    const uncheckedCount = computed(() => {
      return taskInfo.value && taskInfo.value.itemCount && taskInfo.value.checkedCount
        ? taskInfo.value.itemCount - taskInfo.value.checkedCount
        : 0
    })

    /**
     * Progress percentage
     */
    const progressPercentage = computed(() => {
      if (!taskInfo.value || !taskInfo.value.itemCount || taskInfo.value.itemCount === 0) {
        return 0
      }
      return Math.round((taskInfo.value.checkedCount / taskInfo.value.itemCount) * 100)
    })

    /**
     * Progress status
     */
    const progressStatus = computed(() => {
      if (progressPercentage.value >= 100) return 'success'
      if (progressPercentage.value >= 50) return 'warning'
      return 'exception'
    })

    /**
     * Filtered items
     */
    const filteredItems = computed(() => {
      if (!taskInfo.value || !taskInfo.value.items) return []

      if (filterForm.status === '') {
        return taskInfo.value.items
      } else if (filterForm.status === 'unchecked') {
        return taskInfo.value.items.filter(item => !item.checkTime)
      } else if (filterForm.status === 'checked') {
        return taskInfo.value.items.filter(item => item.checkTime)
      }
      return taskInfo.value.items
    })

    /**
     * Has checked item in selection?
     */
    const hasCheckedItem = computed(() => {
      return selectedItems.value.length > 0
    })

    /**
     * Approve dialog title
     */
    const approveDialogTitle = computed(() => {
      if (isApproveMode.value) {
        return currentItem.value ? `审核抽查项 - ${currentItem.value.workerName}` : '批量审核'
      }
      return currentItem.value ? `不通过抽查项 - ${currentItem.value.workerName}` : '批量不通过'
    })

    /**
     * Get business range name
     */
    const getBusinessRangeName = (code) => {
      if (!code) return ''
      const types = code.split(',')
      const names = {
        '1': '二建',
        '2': '二造',
        '3': '安管人员',
        '4': '特种作业'
      }
      return types.map(t => names[t]).join(', ')
    }

    /**
     * Get source table name
     */
    const getSourceTableName = (tableName) => {
      const names = {
        'Apply': '申请表',
        'zjs_Apply': '二造申请',
        'EXAMSIGNUP': '考试报名',
        'CERTIFICATE_ENTER': '证书进京',
        'CERTIFICATECONTINUE': '证书续期'
      }
      return names[tableName] || tableName
    }

    /**
     * Get status tag type
     */
    const getStatusType = (status) => {
      const typeMap = {
        '进行中': 'warning',
        '已完成': 'success',
        '已关闭': 'info'
      }
      return typeMap[status] || 'info'
    }

    /**
     * Get check result tag type
     */
    const getCheckResultType = (result) => {
      return result === '通过' ? 'success' : 'danger'
    }

    /**
     * Load task detail
     */
    const loadTaskDetail = async () => {
      loading.value = true
      try {
        const taskId = route.query.id
        if (!taskId) {
          ElMessage.error('任务ID参数缺失')
          router.push('/check-task/list')
          return
        }

        const res = await getCheckTaskById(taskId)

        if (res.code === 0) {
          taskInfo.value = res.data
        } else {
          ElMessage.error(res.message || '加载任务详情失败')
          router.push('/check-task/list')
        }
      } catch (error) {
        ElMessage.error('加载任务详情失败: ' + error.message)
        router.push('/check-task/list')
      } finally {
        loading.value = false
      }
    }

    /**
     * Load task items
     */
    const loadTaskItems = async () => {
      itemsLoading.value = true
      try {
        const res = await getTaskItems(taskInfo.value.taskID)
        if (res.code === 0) {
          taskInfo.value.items = res.data || []
        } else {
          ElMessage.error(res.message || '加载抽查项失败')
        }
      } catch (error) {
        ElMessage.error('加载抽查项失败: ' + error.message)
      } finally {
        itemsLoading.value = false
      }
    }

    /**
     * Handle filter change
     */
    const handleFilterChange = () => {
      // Reset selection when filter changes
      selectedItems.value = []
      selectAll.value = false
    }

    /**
     * Handle selection change
     */
    const handleSelectionChange = (selection) => {
      selectedItems.value = selection
    }

    /**
     * Handle select all
     */
    const handleSelectAll = (checked) => {
      selectAll.value = checked
      if (tableRef.value) {
        filteredItems.value.forEach(row => {
          tableRef.value.toggleRowSelection(row, checked)
        })
      }
    }

    /**
     * Handle approve item
     */
    const handleApproveItem = (row) => {
      currentItem.value = row
      isApproveMode.value = true
      approveForm.checkResult = '通过'
      approveForm.checkResultDesc = ''
      approveDialogVisible.value = true
    }

    /**
     * Handle reject item
     */
    const handleRejectItem = (row) => {
      currentItem.value = row
      isApproveMode.value = false
      approveForm.checkResult = '不通过'
      approveForm.checkResultDesc = ''
      approveDialogVisible.value = true
    }

    /**
     * Handle approve submit
     */
    const handleApproveSubmit = async () => {
      if (!approveFormRef.value) return

      try {
        const valid = await approveFormRef.value.validate()
        if (!valid) return

        approving.value = true

        if (isApproveMode.value && currentItem.value) {
          // Single item approve
          const res = await approveTaskItem(
            currentItem.value.taskItemID,
            approveForm.checkMan,
            approveForm.checkResult,
            approveForm.checkResultDesc
          )

          if (res.code === 0) {
            ElMessage.success('审核成功')
            approveDialogVisible.value = false
            loadTaskItems()
          } else {
            ElMessage.error(res.message || '审核失败')
          }
        } else if (!isApproveMode.value && currentItem.value) {
          // Single item reject
          const res = await rejectTaskItem(
            currentItem.value.taskItemID,
            approveForm.checkMan,
            approveForm.checkResultDesc
          )

          if (res.code === 0) {
            ElMessage.success('操作成功')
            approveDialogVisible.value = false
            loadTaskItems()
          } else {
            ElMessage.error(res.message || '操作失败')
          }
        } else {
          // Batch operation
          const res = await batchApproveItems(
            approveForm.checkMan,
            isApproveMode.value ? '通过' : '不通过',
            approveForm.checkResultDesc,
            null,
            taskInfo.value.taskID
          )

          if (res.code === 0) {
            ElMessage.success('批量操作成功')
            approveDialogVisible.value = false
            loadTaskItems()
          } else {
            ElMessage.error(res.message || '批量操作失败')
          }
        }
      } catch (error) {
        ElMessage.error('操作失败: ' + error.message)
      } finally {
        approving.value = false
      }
    }

    /**
     * Handle batch approve
     */
    const handleBatchApprove = () => {
      isApproveMode.value = true
      approveForm.checkResult = '通过'
      approveForm.checkResultDesc = ''
      approveDialogVisible.value = true
    }

    /**
     * Handle batch reject
     */
    const handleBatchReject = () => {
      isApproveMode.value = false
      approveForm.checkResult = '不通过'
      approveForm.checkResultDesc = ''
      approveDialogVisible.value = true
    }

    /**
     * Handle generate items
     */
    const handleGenerateItems = async () => {
      try {
        await ElMessage.confirm(
          `确定要为任务 ${taskInfo.value.taskName} 重新生成抽查项吗？原有的抽查项将被清除。`,
          '确认操作',
          {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning'
          }
        )

        const res = await generateCheckItems(taskInfo.value.taskID)

        if (res.code === 0) {
          ElMessage.success(`已重新生成 ${res.data} 个抽查项`)
          loadTaskDetail()
          loadTaskItems()
        } else {
          ElMessage.error(res.message || '生成抽查项失败')
        }
      } catch (error) {
        if (error !== 'cancel') {
          ElMessage.error('生成抽查项失败: ' + error.message)
        }
      }
    }

    /**
     * Handle view detail
     */
    const handleViewDetail = (row) => {
      // Could open a detail dialog showing more information
      ElMessage.info('查看详情功能待实现')
    }

    /**
     * Handle back
     */
    const handleBack = () => {
      router.push('/check-task/list')
    }

    onMounted(() => {
      loadTaskDetail()
      loadTaskItems()
    })

    return {
      tableRef,
      approveFormRef,
      loading,
      itemsLoading,
      approveDialogVisible,
      approving,
      isApproveMode,
      selectAll,
      taskInfo,
      filterForm,
      selectedItems,
      currentItem,
      approveForm,
      approveRules,
      canGenerateItems,
      uncheckedCount,
      progressPercentage,
      progressStatus,
      filteredItems,
      hasCheckedItem,
      approveDialogTitle,
      getBusinessRangeName,
      getSourceTableName,
      getStatusType,
      getCheckResultType,
      loadTaskDetail,
      loadTaskItems,
      handleFilterChange,
      handleSelectionChange,
      handleSelectAll,
      handleApproveItem,
      handleRejectItem,
      handleApproveSubmit,
      handleBatchApprove,
      handleBatchReject,
      handleGenerateItems,
      handleViewDetail,
      handleBack
    }
  }
}
</script>

<style scoped>
.check-task-detail-container {
  padding: 20px;
}

.page-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.page-header h2 {
  margin: 0;
  font-size: 18px;
  font-weight: bold;
}

.breadcrumb {
  color: #666;
  font-size: 14px;
}

.info-card {
  margin-bottom: 20px;
}

.progress-card {
  margin-bottom: 20px;
}

.filter-card {
  margin-bottom: 20px;
}

.items-card {
  margin-bottom: 20px;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  font-weight: bold;
  color: #333;
}

.header-actions {
  display: flex;
  gap: 10px;
}

.progress-container {
  display: flex;
  align-items: center;
  gap: 20px;
  padding: 20px;
}

.progress-text {
  font-weight: bold;
  color: #666;
}
</style>
