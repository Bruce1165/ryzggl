<template>
  <div class="exam-plan-container">
    <el-card>
      <!-- Header Actions -->
      <template #header>
        <div class="card-header">
          <span>考试计划管理</span>
          <el-button type="primary" @click="handleCreate">新建考试计划</el-button>
        </div>
      </template>

      <!-- Search Form -->
      <el-form :model="searchForm" :inline="true" class="search-form">
        <el-form-item label="计划名称">
          <el-input v-model="searchForm.examPlanName" placeholder="请输入计划名称" clearable />
        </el-form-item>
        <el-form-item label="状态">
          <el-select v-model="searchForm.status" placeholder="请选择状态" clearable>
            <el-option label="草稿" value="DRAFT" />
            <el-option label="已发布" value="PUBLISHED" />
            <el-option label="已关闭" value="CLOSED" />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSearch">搜索</el-button>
          <el-button @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>

      <!-- Exam Plan Table -->
      <el-table :data="tableData" border style="width: 100%" v-loading="loading">
        <el-table-column prop="examPlanId" label="ID" width="80" />
        <el-table-column prop="examPlanName" label="计划名称" min-width="180" />
        <el-table-column prop="postTypeId" label="岗位类型" width="100" />
        <el-table-column prop="examDate" label="考试日期" width="120">
          <template #default="{ row }">
            {{ formatDate(row.examDate) }}
          </template>
        </el-table-column>
        <el-table-column prop="signUpStartDate" label="报名开始" width="120">
          <template #default="{ row }">
            {{ formatDate(row.signUpStartDate) }}
          </template>
        </el-table-column>
        <el-table-column prop="signUpEndDate" label="报名结束" width="120">
          <template #default="{ row }">
            {{ formatDate(row.signUpEndDate) }}
          </template>
        </el-table-column>
        <el-table-column prop="examCapacity" label="容量" width="80" />
        <el-table-column prop="maxSignUpCount" label="最大报名" width="90" />
        <el-table-column prop="examWay" label="考试方式" width="80">
          <template #default="{ row }">
            <el-tag :type="row.examWay === '机考' ? 'success' : 'warning'" size="small">
              {{ row.examWay }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="status" label="状态" width="90">
          <template #default="{ row }">
            <el-tag :type="getStatusType(row.status)" size="small">
              {{ getStatusText(row.status) }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="200" fixed="right">
          <template #default="{ row }">
            <el-button link type="primary" size="small" @click="handleView(row)">查看</el-button>
            <el-button link type="primary" size="small" @click="handleEdit(row)" v-if="row.status === 'DRAFT'">编辑</el-button>
            <el-button link type="success" size="small" @click="handlePublish(row)" v-if="row.status === 'DRAFT'">发布</el-button>
            <el-button link type="warning" size="small" @click="handleClose(row)" v-if="row.status === 'PUBLISHED'">关闭</el-button>
            <el-button link type="danger" size="small" @click="handleDelete(row)" v-if="row.status === 'DRAFT'">删除</el-button>
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
      width="800px"
      @close="handleDialogClose"
    >
      <el-form :model="formData" :rules="formRules" ref="formRef" label-width="140px">
        <el-divider content-position="left">基本信息</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="计划名称" prop="examPlanName">
              <el-input v-model="formData.examPlanName" placeholder="请输入计划名称" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="岗位类型" prop="postTypeId">
              <el-input v-model="formData.postTypeId" placeholder="请输入岗位类型ID" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="考试方式" prop="examWay">
              <el-select v-model="formData.examWay" placeholder="请选择考试方式">
                <el-option label="机考" value="机考" />
                <el-option label="网考" value="网考" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="技术等级" prop="planSkillLevel">
              <el-input v-model="formData.planSkillLevel" placeholder="请输入技术等级" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="说明">
          <el-input v-model="formData.description" type="textarea" :rows="3" placeholder="请输入说明" />
        </el-form-item>

        <el-divider content-position="left">考试时间</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="考试日期" prop="examDate">
              <el-date-picker
                v-model="formData.examDate"
                type="date"
                placeholder="选择考试日期"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="考试开始日期" prop="examStartDate">
              <el-date-picker
                v-model="formData.examStartDate"
                type="date"
                placeholder="选择开始日期"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="考试结束日期" prop="examEndDate">
          <el-date-picker
            v-model="formData.examEndDate"
            type="date"
            placeholder="选择结束日期"
            value-format="YYYY-MM-DD"
            style="width: 100%"
          />
        </el-form-item>

        <el-divider content-position="left">报名时间</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="报名开始日期" prop="signUpStartDate">
              <el-date-picker
                v-model="formData.signUpStartDate"
                type="date"
                placeholder="选择开始日期"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="报名结束日期" prop="signUpEndDate">
              <el-date-picker
                v-model="formData.signUpEndDate"
                type="date"
                placeholder="选择结束日期"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
        </el-row>

        <el-divider content-position="left">容量与限制</el-divider>
        <el-row :gutter="20">
          <el-col :span="8">
            <el-form-item label="考试容量" prop="examCapacity">
              <el-input-number v-model="formData.examCapacity" :min="1" :max="10000" style="width: 100%" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="最大报名人数" prop="maxSignUpCount">
              <el-input-number v-model="formData.maxSignUpCount" :min="1" :max="10000" style="width: 100%" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="人数限制" prop="personLimit">
              <el-input-number v-model="formData.personLimit" :min="1" :max="10000" style="width: 100%" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="考点ID" prop="examPlaceId">
          <el-input v-model="formData.examPlaceId" placeholder="请输入考点ID" />
        </el-form-item>

        <el-divider content-position="left">其他设置</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="审核开始时间" prop="startCheckDate">
              <el-date-picker
                v-model="formData.startCheckDate"
                type="date"
                placeholder="选择开始日期"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="审核截止时间" prop="latestCheckDate">
              <el-date-picker
                v-model="formData.latestCheckDate"
                type="date"
                placeholder="选择截止日期"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="缴费截止日期" prop="latestPayDate">
              <el-date-picker
                v-model="formData.latestPayDate"
                type="date"
                placeholder="选择截止日期"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="考试费用" prop="examFee">
              <el-input-number v-model="formData.examFee" :min="0" :precision="2" style="width: 100%" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="发证开始日期" prop="examCardSendStartDate">
              <el-date-picker
                v-model="formData.examCardSendStartDate"
                type="date"
                placeholder="选择开始日期"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="发证结束日期" prop="examCardSendEndDate">
              <el-date-picker
                v-model="formData.examCardSendEndDate"
                type="date"
                placeholder="选择结束日期"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="是否公开">
          <el-switch v-model="formData.ifPublish" />
        </el-form-item>
      </el-form>

      <template #footer>
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSubmit" :loading="submitLoading">确定</el-button>
      </template>
    </el-dialog>

    <!-- View Dialog -->
    <el-dialog v-model="viewVisible" title="考试计划详情" width="700px">
      <el-descriptions :column="2" border v-if="currentRow">
        <el-descriptions-item label="计划名称">{{ currentRow.examPlanName }}</el-descriptions-item>
        <el-descriptions-item label="岗位类型ID">{{ currentRow.postTypeId }}</el-descriptions-item>
        <el-descriptions-item label="考试方式">{{ currentRow.examWay }}</el-descriptions-item>
        <el-descriptions-item label="技术等级">{{ currentRow.planSkillLevel }}</el-descriptions-item>
        <el-descriptions-item label="考试日期">{{ formatDate(currentRow.examDate) }}</el-descriptions-item>
        <el-descriptions-item label="考试开始">{{ formatDate(currentRow.examStartDate) }}</el-descriptions-item>
        <el-descriptions-item label="考试结束">{{ formatDate(currentRow.examEndDate) }}</el-descriptions-item>
        <el-descriptions-item label="报名开始">{{ formatDate(currentRow.signUpStartDate) }}</el-descriptions-item>
        <el-descriptions-item label="报名结束">{{ formatDate(currentRow.signUpEndDate) }}</el-descriptions-item>
        <el-descriptions-item label="审核开始">{{ formatDate(currentRow.startCheckDate) }}</el-descriptions-item>
        <el-descriptions-item label="审核截止">{{ formatDate(currentRow.latestCheckDate) }}</el-descriptions-item>
        <el-descriptions-item label="缴费截止">{{ formatDate(currentRow.latestPayDate) }}</el-descriptions-item>
        <el-descriptions-item label="发证开始">{{ formatDate(currentRow.examCardSendStartDate) }}</el-descriptions-item>
        <el-descriptions-item label="发证结束">{{ formatDate(currentRow.examCardSendEndDate) }}</el-descriptions-item>
        <el-descriptions-item label="考试容量">{{ currentRow.examCapacity }}</el-descriptions-item>
        <el-descriptions-item label="最大报名">{{ currentRow.maxSignUpCount }}</el-descriptions-item>
        <el-descriptions-item label="人数限制">{{ currentRow.personLimit }}</el-descriptions-item>
        <el-descriptions-item label="考点ID">{{ currentRow.examPlaceId }}</el-descriptions-item>
        <el-descriptions-item label="考试费用">{{ currentRow.examFee }}</el-descriptions-item>
        <el-descriptions-item label="状态">
          <el-tag :type="getStatusType(currentRow.status)">
            {{ getStatusText(currentRow.status) }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="是否公开">
          <el-tag :type="currentRow.ifPublish ? 'success' : 'info'">
            {{ currentRow.ifPublish ? '公开' : '不公开' }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="说明" :span="2">{{ currentRow.description || '-' }}</el-descriptions-item>
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
  getExamPlanList,
  createExamPlan,
  updateExamPlan,
  deleteExamPlan,
  updateExamPlanStatus
} from '@/api/examPlan'

// Table data
const tableData = ref([])
const loading = ref(false)

// Pagination
const pagination = reactive({
  current: 1,
  size: 10,
  total: 0
})

// Search form
const searchForm = reactive({
  examPlanName: '',
  status: ''
})

// Dialog
const dialogVisible = ref(false)
const dialogTitle = ref('新建考试计划')
const isEdit = ref(false)
const formRef = ref(null)
const submitLoading = ref(false)

// Form data
const formData = reactive({
  examPlanId: null,
  examPlanName: '',
  postTypeId: '',
  examDate: '',
  signUpStartDate: '',
  signUpEndDate: '',
  examCardSendStartDate: '',
  examCardSendEndDate: '',
  examStartDate: '',
  examEndDate: '',
  examPlaceId: '',
  examCapacity: 100,
  maxSignUpCount: 100,
  description: '',
  status: 'DRAFT',
  examWay: '机考',
  personLimit: 100,
  planSkillLevel: '',
  ifPublish: true,
  examFee: 0,
  startCheckDate: '',
  latestCheckDate: '',
  latestPayDate: ''
})

// View dialog
const viewVisible = ref(false)
const currentRow = ref(null)

// Form rules
const formRules = {
  examPlanName: [{ required: true, message: '请输入计划名称', trigger: 'blur' }],
  postTypeId: [{ required: true, message: '请输入岗位类型ID', trigger: 'blur' }],
  examDate: [{ required: true, message: '请选择考试日期', trigger: 'change' }],
  signUpStartDate: [{ required: true, message: '请选择报名开始日期', trigger: 'change' }],
  signUpEndDate: [{ required: true, message: '请选择报名结束日期', trigger: 'change' }],
  examCapacity: [{ required: true, message: '请输入考试容量', trigger: 'blur' }],
  maxSignUpCount: [{ required: true, message: '请输入最大报名人数', trigger: 'blur' }]
}

// Load exam plans
const loadExamPlans = async () => {
  loading.value = true
  try {
    const params = {
      current: pagination.current,
      size: pagination.size,
      ...searchForm
    }
    const response = await getExamPlanList(params)
    if (response.code === 200) {
      tableData.value = response.data.records || []
      pagination.total = response.data.total || 0
    } else {
      ElMessage.error(response.message || '加载数据失败')
    }
  } catch (error) {
    ElMessage.error('加载数据失败: ' + error.message)
  } finally {
    loading.value = false
  }
}

// Search
const handleSearch = () => {
  pagination.current = 1
  loadExamPlans()
}

// Reset
const handleReset = () => {
  searchForm.examPlanName = ''
  searchForm.status = ''
  pagination.current = 1
  loadExamPlans()
}

// Create
const handleCreate = () => {
  isEdit.value = false
  dialogTitle.value = '新建考试计划'
  resetForm()
  dialogVisible.value = true
}

// Edit
const handleEdit = (row) => {
  isEdit.value = true
  dialogTitle.value = '编辑考试计划'
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
        await updateExamPlan(formData.examPlanId, formData)
        ElMessage.success('更新成功')
      } else {
        await createExamPlan(formData)
        ElMessage.success('创建成功')
      }
      dialogVisible.value = false
      loadExamPlans()
    } catch (error) {
      ElMessage.error('操作失败: ' + error.message)
    } finally {
      submitLoading.value = false
    }
  })
}

// Delete
const handleDelete = (row) => {
  ElMessageBox.confirm('确定要删除该考试计划吗？', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await deleteExamPlan(row.examPlanId)
      ElMessage.success('删除成功')
      loadExamPlans()
    } catch (error) {
      ElMessage.error('删除失败: ' + error.message)
    }
  }).catch(() => {})
}

// Publish
const handlePublish = (row) => {
  ElMessageBox.confirm('确定要发布该考试计划吗？发布后不能修改。', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await updateExamPlanStatus(row.examPlanId, 'PUBLISHED')
      ElMessage.success('发布成功')
      loadExamPlans()
    } catch (error) {
      ElMessage.error('发布失败: ' + error.message)
    }
  }).catch(() => {})
}

// Close
const handleClose = (row) => {
  ElMessageBox.confirm('确定要关闭该考试计划吗？', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await updateExamPlanStatus(row.examPlanId, 'CLOSED')
      ElMessage.success('关闭成功')
      loadExamPlans()
    } catch (error) {
      ElMessage.error('关闭失败: ' + error.message)
    }
  }).catch(() => {})
}

// Pagination
const handleSizeChange = (val) => {
  pagination.size = val
  pagination.current = 1
  loadExamPlans()
}

const handleCurrentChange = (val) => {
  pagination.current = val
  loadExamPlans()
}

// Dialog close
const handleDialogClose = () => {
  formRef.value?.resetFields()
  resetForm()
}

// Reset form
const resetForm = () => {
  Object.assign(formData, {
    examPlanId: null,
    examPlanName: '',
    postTypeId: '',
    examDate: '',
    signUpStartDate: '',
    signUpEndDate: '',
    examCardSendStartDate: '',
    examCardSendEndDate: '',
    examStartDate: '',
    examEndDate: '',
    examPlaceId: '',
    examCapacity: 100,
    maxSignUpCount: 100,
    description: '',
    status: 'DRAFT',
    examWay: '机考',
    personLimit: 100,
    planSkillLevel: '',
    ifPublish: true,
    examFee: 0,
    startCheckDate: '',
    latestCheckDate: '',
    latestPayDate: ''
  })
}

// Format date
const formatDate = (date) => {
  if (!date) return '-'
  return date
}

// Get status type
const getStatusType = (status) => {
  const types = {
    'DRAFT': 'info',
    'PUBLISHED': 'success',
    'CLOSED': 'danger'
  }
  return types[status] || 'info'
}

// Get status text
const getStatusText = (status) => {
  const texts = {
    'DRAFT': '草稿',
    'PUBLISHED': '已发布',
    'CLOSED': '已关闭'
  }
  return texts[status] || status
}

// Load data on mount
onMounted(() => {
  loadExamPlans()
})
</script>

<style scoped>
.exam-plan-container {
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
