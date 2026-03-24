<template>
  <div class="exam-result-container">
    <el-card>
      <!-- Header Actions -->
      <template #header>
        <div class="card-header">
          <span>考试成绩管理</span>
          <div>
            <el-button type="success" @click="handlePublishAll" :disabled="!selectedExamPlanId">批量发布</el-button>
            <el-button type="primary" @click="handleCreate">录入成绩</el-button>
          </div>
        </div>
      </template>

      <!-- Search Form -->
      <el-form :model="searchForm" :inline="true" class="search-form">
        <el-form-item label="考试计划">
          <el-select v-model="searchForm.examPlanId" placeholder="请选择考试计划" clearable @change="handleExamPlanChange">
            <el-option
              v-for="plan in examPlanList"
              :key="plan.examPlanId"
              :label="plan.examPlanName"
              :value="plan.examPlanId"
            />
          </el-select>
        </el-form-item>
        <el-form-item label="人员姓名">
          <el-input v-model="searchForm.workerName" placeholder="请输入人员姓名" clearable />
        </el-form-item>
        <el-form-item label="身份证号">
          <el-input v-model="searchForm.idCard" placeholder="请输入身份证号" clearable />
        </el-form-item>
        <el-form-item label="成绩状态">
          <el-select v-model="searchForm.resultStatus" placeholder="请选择状态" clearable>
            <el-option label="及格" value="PASS" />
            <el-option label="不及格" value="FAIL" />
            <el-option label="缺考" value="ABSENT" />
            <el-option label="待录入" value="PENDING" />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSearch">搜索</el-button>
          <el-button @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>

      <!-- Pass Rate Statistics -->
      <el-alert
        v-if="passRateStats"
        :title="`及格率统计: 总数 ${passRateStats.total}, 及格 ${passRateStats.pass}, 不及格 ${passRateStats.fail}, 及格率 ${passRateStats.rate}%`"
        type="info"
        :closable="false"
        show-icon
        style="margin-bottom: 20px"
      />

      <!-- Exam Result Table -->
      <el-table :data="tableData" border style="width: 100%" v-loading="loading">
        <el-table-column prop="examResultId" label="成绩ID" width="100" />
        <el-table-column prop="examPlanName" label="考试计划" min-width="180" show-overflow-tooltip />
        <el-table-column prop="workerId" label="人员编号" width="100" />
        <el-table-column prop="workerName" label="姓名" width="100" />
        <el-table-column prop="idCard" label="身份证号" width="180" />
        <el-table-column prop="score" label="分数" width="100" align="center">
          <template #default="{ row }">
            <span :style="{ color: getScoreColor(row), fontWeight: 'bold', fontSize: '16px' }">
              {{ row.score || '-' }}
            </span>
          </template>
        </el-table-column>
        <el-table-column prop="passScore" label="及格线" width="90" align="center" />
        <el-table-column prop="resultStatus" label="状态" width="90">
          <template #default="{ row }">
            <el-tag :type="getStatusType(row.resultStatus)" size="small">
              {{ getStatusText(row.resultStatus) }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="examDate" label="考试日期" width="110">
          <template #default="{ row }">
            {{ formatDate(row.examDate) }}
          </template>
        </el-table-column>
        <el-table-column prop="recordTime" label="录入时间" width="160" />
        <el-table-column prop="published" label="是否发布" width="90" align="center">
          <template #default="{ row }">
            <el-tag :type="row.published ? 'success' : 'info'" size="small">
              {{ row.published ? '已发布' : '未发布' }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="200" fixed="right">
          <template #default="{ row }">
            <el-button link type="primary" size="small" @click="handleView(row)">查看</el-button>
            <el-button link type="primary" size="small" @click="handleEdit(row)" v-if="!row.published">编辑</el-button>
            <el-button link type="success" size="small" @click="handlePass(row)" v-if="row.resultStatus !== 'PASS' && row.score !== null">及格</el-button>
            <el-button link type="danger" size="small" @click="handleFail(row)" v-if="row.resultStatus !== 'FAIL' && row.score !== null">不及格</el-button>
            <el-button link type="danger" size="small" @click="handleDelete(row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>

      <!-- Pagination -->
      <el-pagination
        v-model:current-page="pagination.current"
        v-model:page-size="pagination.size"
        :page-sizes="[10, 20, 50, 100]"
        :total="pagination.total"
        layout="total, sizes, prev, pager, next, jumper"
        @size-change="handleSizeChange"
        @current-change="handleCurrentChange"
        style="margin-top: 20px; justify-content: flex-end"
      />
    </el-card>

    <!-- Create/Edit Dialog -->
    <el-dialog
      v-model="dialogVisible"
      :title="dialogTitle"
      width="600px"
      @close="handleDialogClose"
    >
      <el-form :model="formData" :rules="formRules" ref="formRef" label-width="120px">
        <el-divider content-position="left">基本信息</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="考试计划" prop="examPlanId">
              <el-select v-model="formData.examPlanId" placeholder="请选择考试计划">
                <el-option
                  v-for="plan in examPlanList"
                  :key="plan.examPlanId"
                  :label="plan.examPlanName"
                  :value="plan.examPlanId"
                />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="人员编号" prop="workerId">
              <el-input v-model="formData.workerId" placeholder="请输入人员编号" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="姓名" prop="workerName">
              <el-input v-model="formData.workerName" placeholder="请输入姓名" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="身份证号" prop="idCard">
              <el-input v-model="formData.idCard" placeholder="请输入身份证号" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-divider content-position="left">成绩信息</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="考试分数" prop="score">
              <el-input-number v-model="formData.score" :min="0" :max="100" :precision="1" style="width: 100%" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="及格分数" prop="passScore">
              <el-input-number v-model="formData.passScore" :min="0" :max="100" style="width: 100%" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="考试日期" prop="examDate">
          <el-date-picker
            v-model="formData.examDate"
            type="date"
            placeholder="选择考试日期"
            value-format="YYYY-MM-DD"
            style="width: 100%"
          />
        </el-form-item>
        <el-form-item label="成绩状态" prop="resultStatus">
          <el-radio-group v-model="formData.resultStatus">
            <el-radio label="PASS">及格</el-radio>
            <el-radio label="FAIL">不及格</el-radio>
            <el-radio label="ABSENT">缺考</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="备注">
          <el-input v-model="formData.remark" type="textarea" :rows="3" placeholder="请输入备注" />
        </el-form-item>
      </el-form>

      <template #footer>
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSubmit" :loading="submitLoading">确定</el-button>
      </template>
    </el-dialog>

    <!-- View Dialog -->
    <el-dialog v-model="viewVisible" title="成绩详情" width="600px">
      <el-descriptions :column="2" border v-if="currentRow">
        <el-descriptions-item label="成绩ID">{{ currentRow.examResultId }}</el-descriptions-item>
        <el-descriptions-item label="考试计划">{{ currentRow.examPlanName }}</el-descriptions-item>
        <el-descriptions-item label="人员编号">{{ currentRow.workerId }}</el-descriptions-item>
        <el-descriptions-item label="姓名">{{ currentRow.workerName }}</el-descriptions-item>
        <el-descriptions-item label="身份证号" :span="2">{{ currentRow.idCard }}</el-descriptions-item>
        <el-descriptions-item label="考试分数">
          <span :style="{ color: getScoreColor(currentRow), fontSize: '18px', fontWeight: 'bold' }">
            {{ currentRow.score || '-' }}
          </span>
        </el-descriptions-item>
        <el-descriptions-item label="及格分数">{{ currentRow.passScore }}</el-descriptions-item>
        <el-descriptions-item label="成绩状态" :span="2">
          <el-tag :type="getStatusType(currentRow.resultStatus)">
            {{ getStatusText(currentRow.resultStatus) }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="考试日期">{{ formatDate(currentRow.examDate) }}</el-descriptions-item>
        <el-descriptions-item label="录入时间">{{ currentRow.recordTime }}</el-descriptions-item>
        <el-descriptions-item label="是否发布" :span="2">
          <el-tag :type="currentRow.published ? 'success' : 'info'">
            {{ currentRow.published ? '已发布' : '未发布' }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="备注" :span="2">{{ currentRow.remark || '-' }}</el-descriptions-item>
      </el-descriptions>
      <template #footer>
        <el-button @click="viewVisible = false">关闭</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import {
  createResult,
  updateResult,
  deleteResult,
  passResult,
  failResult,
  publishResults,
  getPassRate,
  getStatistics,
  searchResults,
  getResultsByExamPlan
} from '@/api/examResult'
import { getExamPlanList } from '@/api/examPlan'

// Table data
const tableData = ref([])
const loading = ref(false)
const examPlanList = ref([])

// Pagination
const pagination = reactive({
  current: 1,
  size: 10,
  total: 0
})

// Search form
const searchForm = reactive({
  examPlanId: '',
  workerName: '',
  idCard: '',
  resultStatus: ''
})

// Pass rate statistics
const passRateStats = ref(null)

// Dialog
const dialogVisible = ref(false)
const dialogTitle = ref('录入成绩')
const isEdit = ref(false)
const formRef = ref(null)
const submitLoading = ref(false)

// Form data
const formData = reactive({
  examResultId: null,
  examPlanId: '',
  workerId: '',
  workerName: '',
  idCard: '',
  score: 0,
  passScore: 60,
  examDate: '',
  resultStatus: 'PASS',
  remark: '',
  published: false
})

// View dialog
const viewVisible = ref(false)
const currentRow = ref(null)

// Selected exam plan for batch operations
const selectedExamPlanId = ref('')

// Form rules
const formRules = {
  examPlanId: [{ required: true, message: '请选择考试计划', trigger: 'change' }],
  workerId: [{ required: true, message: '请输入人员编号', trigger: 'blur' }],
  workerName: [{ required: true, message: '请输入姓名', trigger: 'blur' }],
  idCard: [{ required: true, message: '请输入身份证号', trigger: 'blur' }],
  score: [{ required: true, message: '请输入考试分数', trigger: 'blur' }],
  passScore: [{ required: true, message: '请输入及格分数', trigger: 'blur' }]
}

// Load exam plans
const loadExamPlans = async () => {
  try {
    const response = await getExamPlanList({ current: 1, size: 100 })
    if (response.code === 200) {
      examPlanList.value = response.data.records || []
    }
  } catch (error) {
    console.error('加载考试计划失败:', error)
  }
}

// Load exam results
const loadExamResults = async () => {
  loading.value = true
  try {
    const params = {
      current: pagination.current,
      size: pagination.size,
      ...searchForm
    }
    const response = await searchResults(params)
    if (response.code === 200) {
      tableData.value = response.data.records || []
      pagination.total = response.data.total || 0

      // Load statistics if exam plan is selected
      if (searchForm.examPlanId) {
        loadStatistics(searchForm.examPlanId)
      } else {
        passRateStats.value = null
      }
    } else {
      ElMessage.error(response.message || '加载数据失败')
    }
  } catch (error) {
    ElMessage.error('加载数据失败: ' + error.message)
  } finally {
    loading.value = false
  }
}

// Load statistics
const loadStatistics = async (examPlanId) => {
  try {
    const [passRateRes, statsRes] = await Promise.all([
      getPassRate(examPlanId),
      getStatistics(examPlanId)
    ])

    if (passRateRes.code === 200 && statsRes.code === 200) {
      const total = tableData.value.length
      const pass = statsRes.data.passCount || 0
      const fail = statsRes.data.failCount || 0
      const rate = total > 0 ? ((pass / total) * 100).toFixed(1) : 0

      passRateStats.value = {
        total,
        pass,
        fail,
        rate
      }
    }
  } catch (error) {
    console.error('加载统计数据失败:', error)
  }
}

// Search
const handleSearch = () => {
  pagination.current = 1
  loadExamResults()
}

// Reset
const handleReset = () => {
  searchForm.examPlanId = ''
  searchForm.workerName = ''
  searchForm.idCard = ''
  searchForm.resultStatus = ''
  pagination.current = 1
  loadExamResults()
}

// Exam plan change
const handleExamPlanChange = (val) => {
  selectedExamPlanId.value = val
  if (val) {
    loadStatistics(val)
  } else {
    passRateStats.value = null
  }
  loadExamResults()
}

// Create
const handleCreate = () => {
  isEdit.value = false
  dialogTitle.value = '录入成绩'
  resetForm()
  dialogVisible.value = true
}

// Edit
const handleEdit = (row) => {
  isEdit.value = true
  dialogTitle.value = '编辑成绩'
  Object.assign(formData, row)
  dialogVisible.value = true
}

// View
const handleView = (row) => {
  currentRow.value = row
  viewVisible.value = true
}

// Submit
const handleSubmit = async () => {
  if (!formRef.value) return

  await formRef.value.validate(async (valid) => {
    if (!valid) return

    submitLoading.value = true
    try {
      if (isEdit.value) {
        await updateResult(formData.examResultId, formData)
        ElMessage.success('更新成功')
      } else {
        await createResult(formData)
        ElMessage.success('录入成功')
      }
      dialogVisible.value = false
      loadExamResults()
    } catch (error) {
      ElMessage.error('操作失败: ' + error.message)
    } finally {
      submitLoading.value = false
    }
  })
}

// Pass
const handlePass = (row) => {
  ElMessageBox.confirm(`确定要将 ${row.workerName} 标记为及格吗？`, '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await passResult(row.examResultId)
      ElMessage.success('已标记为及格')
      loadExamResults()
    } catch (error) {
      ElMessage.error('操作失败: ' + error.message)
    }
  }).catch(() => {})
}

// Fail
const handleFail = (row) => {
  ElMessageBox.confirm(`确定要将 ${row.workerName} 标记为不及格吗？`, '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await failResult(row.examResultId)
      ElMessage.success('已标记为不及格')
      loadExamResults()
    } catch (error) {
      ElMessage.error('操作失败: ' + error.message)
    }
  }).catch(() => {})
}

// Delete
const handleDelete = (row) => {
  ElMessageBox.confirm(`确定要删除 ${row.workerName} 的成绩记录吗？`, '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await deleteResult(row.examResultId)
      ElMessage.success('删除成功')
      loadExamResults()
    } catch (error) {
      ElMessage.error('删除失败: ' + error.message)
    }
  }).catch(() => {})
}

// Publish all
const handlePublishAll = () => {
  ElMessageBox.confirm(`确定要发布 ${searchForm.examPlanId} 的所有成绩吗？`, '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await publishResults(searchForm.examPlanId)
      ElMessage.success('批量发布成功')
      loadExamResults()
    } catch (error) {
      ElMessage.error('发布失败: ' + error.message)
    }
  }).catch(() => {})
}

// Pagination
const handleSizeChange = (val) => {
  pagination.size = val
  pagination.current = 1
  loadExamResults()
}

const handleCurrentChange = (val) => {
  pagination.current = val
  loadExamResults()
}

// Dialog close
const handleDialogClose = () => {
  formRef.value?.resetFields()
  resetForm()
}

// Reset form
const resetForm = () => {
  Object.assign(formData, {
    examResultId: null,
    examPlanId: '',
    workerId: '',
    workerName: '',
    idCard: '',
    score: 0,
    passScore: 60,
    examDate: '',
    resultStatus: 'PASS',
    remark: '',
    published: false
  })
}

// Format date
const formatDate = (date) => {
  if (!date) return '-'
  return date
}

// Get score color
const getScoreColor = (row) => {
  if (row.resultStatus === 'PASS') return '#67c23a' // green
  if (row.resultStatus === 'FAIL') return '#f56c6c' // red
  if (row.resultStatus === 'ABSENT') return '#909399' // gray
  if (row.score === null || row.score === undefined) return '#909399' // gray
  return row.score >= row.passScore ? '#67c23a' : '#f56c6c' // green/red based on pass
}

// Get status type
const getStatusType = (status) => {
  const types = {
    'PASS': 'success',
    'FAIL': 'danger',
    'ABSENT': 'warning',
    'PENDING': 'info'
  }
  return types[status] || 'info'
}

// Get status text
const getStatusText = (status) => {
  const texts = {
    'PASS': '及格',
    'FAIL': '不及格',
    'ABSENT': '缺考',
    'PENDING': '待录入'
  }
  return texts[status] || status
}

// Load data on mount
onMounted(() => {
  loadExamPlans()
  loadExamResults()
})
</script>

<style scoped>
.exam-result-container {
  padding: 20px;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.search-form {
  margin-bottom: 20px;
}

:deep(.el-pagination) {
  display: flex;
}
</style>
