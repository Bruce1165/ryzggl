<template>
  <div class="check-task-list-container">
    <!-- App Header -->
    <AppHeader />

    <!-- Page Header -->
    <div class="page-header">
      <h2>业务申请单抽任务管理</h2>
      <div class="breadcrumb">当前位置: 任务管理 &gt; 业务申请单抽任务</div>
    </div>

    <!-- Filter Card -->
    <el-card class="filter-card">
      <el-form :inline="true" :model="filterForm" class="filter-form">
        <el-form-item label="年份">
          <el-select v-model="filterForm.year" placeholder="选择年份" @change="handleSearch">
            <el-option label="全部" value="" />
            <el-option
              v-for="year in years"
              :key="year"
              :label="year + '年'"
              :value="year"
            />
          </el-select>
        </el-form-item>

        <el-form-item label="月份">
          <el-select v-model="filterForm.month" placeholder="选择月份" @change="handleSearch">
            <el-option label="全部" value="" />
            <el-option label="1月" value="1" />
            <el-option label="2月" value="2" />
            <el-option label="3月" value="3" />
            <el-option label="4月" value="4" />
            <el-option label="5月" value="5" />
            <el-option label="6月" value="6" />
            <el-option label="7月" value="7" />
            <el-option label="8月" value="8" />
            <el-option label="9月" value="9" />
            <el-option label="10月" value="10" />
            <el-option label="11月" value="11" />
            <el-option label="12月" value="12" />
          </el-select>
        </el-form-item>

        <el-form-item label="状态">
          <el-select v-model="filterForm.status" placeholder="选择状态" @change="handleSearch">
            <el-option label="全部" value="" />
            <el-option label="进行中" value="进行中" />
            <el-option label="已完成" value="已完成" />
            <el-option label="已关闭" value="已关闭" />
          </el-select>
        </el-form-item>

        <el-form-item>
          <el-button type="primary" icon="Search" @click="handleSearch">查询</el-button>
          <el-button icon="Refresh" @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>
    </el-card>

    <!-- Task List Card -->
    <el-card class="list-card">
      <template #header>
        <div class="card-header">
          <span>任务列表</span>
          <el-button type="primary" icon="Plus" @click="handleCreate">新建任务</el-button>
        </div>
      </template>

      <el-table
        :data="taskList"
        v-loading="loading"
        stripe
        style="width: 100%"
      >
        <el-table-column prop="taskID" label="任务ID" width="100" />
        <el-table-column prop="busRangeCode" label="业务范围" width="150">
          <template #default="{ row }">
            {{ getBusinessRangeName(row.busRangeCode) }}
          </template>
        </el-table-column>
        <el-table-column prop="checkPer" label="抽查比例" width="100">
          <template #default="{ row }">
            {{ row.checkPer / 10 }}%
          </template>
        </el-table-column>
        <el-table-column prop="busStartDate" label="业务开始日期" width="120" />
        <el-table-column prop="busEndDate" label="业务结束日期" width="120" />
        <el-table-column prop="itemCount" label="抽查数" width="100" />
        <el-table-column prop="checkedCount" label="已审核数" width="100" />
        <el-table-column prop="taskStatus" label="状态" width="100">
          <template #default="{ row }">
            <el-tag :type="getStatusType(row.taskStatus)">
              {{ row.taskStatus }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="cjsj" label="创建时间" width="160" />
        <el-table-column label="操作" width="200" fixed="right">
          <template #default="{ row }">
            <el-button
              type="primary"
              size="small"
              link
              @click="handleView(row)"
            >
              查看
            </el-button>
            <el-button
              type="success"
              size="small"
              link
              @click="handleGenerateItems(row)"
              :disabled="row.taskStatus === '已完成' || row.taskStatus === '已关闭'"
            >
              生成抽查项
            </el-button>
            <el-button
              type="danger"
              size="small"
              link
              @click="handleDelete(row)"
            >
              删除
            </el-button>
          </template>
        </el-table-column>
      </el-table>

      <!-- Pagination -->
      <div class="pagination-container">
        <el-pagination
          v-model:current-page="pagination.current"
          v-model:page-size="pagination.size"
          :page-sizes="[10, 20, 50, 100]"
          :total="pagination.total"
          layout="total, sizes, prev, pager, next, jumper"
          @size-change="handleSizeChange"
          @current-change="handlePageChange"
        />
      </div>
    </el-card>

    <!-- Create Task Dialog -->
    <el-dialog
      v-model="createDialogVisible"
      title="新建业务申请单抽任务"
      width="800px"
      @close="handleDialogClose"
    >
      <el-form
        ref="createFormRef"
        :model="createForm"
        :rules="createRules"
        label-width="140px"
      >
        <el-form-item label="业务范围" prop="busRangeCode">
          <el-checkbox-group v-model="selectedBusinessTypes">
            <el-checkbox label="1">二建</el-checkbox>
            <el-checkbox label="2">二造</el-checkbox>
            <el-checkbox label="3">安管人员</el-checkbox>
            <el-checkbox label="4">特种作业</el-checkbox>
          </el-checkbox-group>
        </el-form-item>

        <el-form-item label="业务开始日期" prop="busStartDate">
          <el-date-picker
            v-model="createForm.busStartDate"
            type="date"
            placeholder="选择开始日期"
            value-format="YYYY-MM-DD"
            style="width: 100%"
          />
        </el-form-item>

        <el-form-item label="业务结束日期" prop="busEndDate">
          <el-date-picker
            v-model="createForm.busEndDate"
            type="date"
            placeholder="选择结束日期"
            value-format="YYYY-MM-DD"
            style="width: 100%"
          />
        </el-form-item>

        <el-form-item label="抽查比例(%)" prop="checkPer">
          <el-input-number
            v-model="createForm.checkPer"
            :min="1"
            :max="1000"
            :step="10"
            placeholder="输入抽查比例"
            style="width: 100%"
          />
          <div class="input-tip">
            例如：10表示1%，100表示10%
          </div>
        </el-form-item>

        <el-form-item label="任务名称" prop="taskName">
          <el-input v-model="createForm.taskName" placeholder="输入任务名称" />
        </el-form-item>

        <el-form-item label="备注">
          <el-input
            v-model="createForm.remark"
            type="textarea"
            :rows="3"
            placeholder="输入备注信息"
          />
        </el-form-item>
      </el-form>

      <template #footer>
        <el-button @click="createDialogVisible = false">取消</el-button>
        <el-button type="primary" :loading="submitting" @click="handleCreateSubmit">
          {{ submitting ? '提交中...' : '创建' }}
        </el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script>
import { ref, reactive, computed, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import {
  getCheckTaskList,
  createCheckTask,
  deleteCheckTask,
  generateCheckItems,
  getCheckedCount
} from '@/api/checkTask'
import AppHeader from '@/components/AppHeader.vue'

export default {
  name: 'CheckTaskList',

  components: {
    AppHeader
  },

  setup() {
    const router = useRouter()
    const createFormRef = ref(null)
    const createDialogVisible = ref(false)
    const loading = ref(false)
    const submitting = ref(false)

    // Generate year options (last 5 years + current year)
    const currentYear = new Date().getFullYear()
    const years = computed(() => {
      const result = []
      for (let i = currentYear; i >= currentYear - 5; i--) {
        result.push(i)
      }
      return result
    })

    // Filter form
    const filterForm = reactive({
      year: currentYear.toString(),
      month: '',
      status: ''
    })

    // Create task form
    const createForm = reactive({
      busRangeCode: '',
      busStartDate: '',
      busEndDate: '',
      checkPer: 10,
      taskName: '',
      remark: ''
    })

    const selectedBusinessTypes = ref([])

    // Pagination
    const pagination = reactive({
      current: 1,
      size: 20,
      total: 0
    })

    // Task list
    const taskList = ref([])

    // Form validation rules
    const createRules = {
      busRangeCode: [
        { required: true, message: '请选择业务范围', trigger: 'change' },
        {
          validator: (rule, value, callback) => {
            if (selectedBusinessTypes.value.length === 0) {
              callback(new Error('请至少选择一个业务类型'))
            } else {
              callback()
            }
          },
          trigger: 'change'
        }
      ],
      busStartDate: [{ required: true, message: '请选择业务开始日期', trigger: 'change' }],
      busEndDate: [{ required: true, message: '请选择业务结束日期', trigger: 'change' }],
      checkPer: [
        { required: true, message: '请输入抽查比例', trigger: 'blur' },
        { type: 'number', message: '抽查比例必须是数字', trigger: 'blur' }
      ],
      taskName: [{ required: true, message: '请输入任务名称', trigger: 'blur' }]
    }

    /**
     * Get business range name from code
     */
    const getBusinessRangeName = (code) => {
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
     * Load task list
     */
    const loadTasks = async () => {
      loading.value = true
      try {
        const params = {
          current: pagination.current,
          size: pagination.size
        }

        // Add filter conditions
        if (filterForm.year) {
          params.year = filterForm.year
        }
        if (filterForm.month) {
          params.month = filterForm.month
        }
        if (filterForm.status) {
          params.status = filterForm.status
        }

        const res = await getCheckTaskList(params)

        if (res.code === 0) {
          taskList.value = res.data || []

          // Load checked count for each task
          for (const task of taskList.value) {
            try {
              const countRes = await getCheckedCount(task.taskID)
              if (countRes.code === 0) {
                task.checkedCount = countRes.data
              }
            } catch (e) {
              console.error('Failed to get checked count:', e)
            }
          }

          pagination.total = res.total || 0
        } else {
          ElMessage.error(res.message || '加载任务列表失败')
        }
      } catch (error) {
        ElMessage.error('加载任务列表失败: ' + error.message)
      } finally {
        loading.value = false
      }
    }

    /**
     * Handle search
     */
    const handleSearch = () => {
      pagination.current = 1
      loadTasks()
    }

    /**
     * Handle reset
     */
    const handleReset = () => {
      filterForm.year = currentYear.toString()
      filterForm.month = ''
      filterForm.status = ''
      pagination.current = 1
      loadTasks()
    }

    /**
     * Handle view task detail
     */
    const handleView = (row) => {
      router.push({
        path: '/check-task/detail',
        query: { id: row.taskID }
      })
    }

    /**
     * Handle generate items
     */
    const handleGenerateItems = async (row) => {
      try {
        await ElMessageBox.confirm(
          `确定要为任务 ${row.taskName} 生成抽查项吗？`,
          '确认操作',
          {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning'
          }
        )

        const res = await generateCheckItems(row.taskID)

        if (res.code === 0) {
          ElMessage.success(`已生成 ${res.data} 个抽查项`)
          loadTasks()
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
     * Handle delete task
     */
    const handleDelete = async (row) => {
      try {
        await ElMessageBox.confirm(
          `确定要删除任务 ${row.taskName} 吗？删除后相关的抽查项也会被删除。`,
          '确认删除',
          {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning'
          }
        )

        const res = await deleteCheckTask(row.taskID)

        if (res.code === 0) {
          ElMessage.success('删除成功')
          loadTasks()
        } else {
          ElMessage.error(res.message || '删除失败')
        }
      } catch (error) {
        if (error !== 'cancel') {
          ElMessage.error('删除失败: ' + error.message)
        }
      }
    }

    /**
     * Handle create task
     */
    const handleCreateSubmit = async () => {
      if (!createFormRef.value) return

      try {
        const valid = await createFormRef.value.validate()
        if (!valid) return

        submitting.value = true

        // Combine selected business types
        createForm.busRangeCode = selectedBusinessTypes.value.join(',')

        const res = await createCheckTask(createForm)

        if (res.code === 0) {
          ElMessage.success('任务创建成功')
          createDialogVisible.value = false
          loadTasks()
        } else {
          ElMessage.error(res.message || '任务创建失败')
        }
      } catch (error) {
        ElMessage.error('任务创建失败: ' + error.message)
      } finally {
        submitting.value = false
      }
    }

    /**
     * Handle dialog close
     */
    const handleDialogClose = () => {
      createFormRef.value?.resetFields()
      selectedBusinessTypes.value = []
    }

    /**
     * Handle page change
     */
    const handlePageChange = (page) => {
      pagination.current = page
      loadTasks()
    }

    /**
     * Handle page size change
     */
    const handleSizeChange = (size) => {
      pagination.size = size
      pagination.current = 1
      loadTasks()
    }

    onMounted(() => {
      loadTasks()
    })

    return {
      createFormRef,
      createDialogVisible,
      loading,
      submitting,
      years,
      filterForm,
      createForm,
      selectedBusinessTypes,
      pagination,
      taskList,
      createRules,
      getBusinessRangeName,
      getStatusType,
      loadTasks,
      handleSearch,
      handleReset,
      handleView,
      handleGenerateItems,
      handleDelete,
      handleCreateSubmit,
      handleDialogClose,
      handlePageChange,
      handleSizeChange
    }
  }
}
</script>

<style scoped>
.check-task-list-container {
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

.filter-card {
  margin-bottom: 20px;
}

.filter-form {
  display: flex;
  align-items: center;
}

.list-card {
  margin-bottom: 20px;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  font-weight: bold;
  color: #333;
}

.pagination-container {
  margin-top: 20px;
  display: flex;
  justify-content: flex-end;
}

.input-tip {
  font-size: 12px;
  color: #999;
  margin-top: 5px;
}
</style>
