<template>
  <div class="exam-signup-container">
    <el-card>
      <!-- Header Actions -->
      <template #header>
        <div class="card-header">
          <span>考试报名管理</span>
          <el-button type="primary" @click="handleCreate">新建报名</el-button>
        </div>
      </template>

      <!-- Search Form -->
      <el-form :model="searchForm" :inline="true" class="search-form">
        <el-form-item label="考试计划">
          <el-input v-model="searchForm.examPlanName" placeholder="请输入考试计划名称" clearable />
        </el-form-item>
        <el-form-item label="人员姓名">
          <el-input v-model="searchForm.workerName" placeholder="请输入人员姓名" clearable />
        </el-form-item>
        <el-form-item label="身份证号">
          <el-input v-model="searchForm.idCard" placeholder="请输入身份证号" clearable />
        </el-form-item>
        <el-form-item label="报名状态">
          <el-select v-model="searchForm.status" placeholder="请选择状态" clearable>
            <el-option label="待审核" value="PENDING" />
            <el-option label="已通过" value="APPROVED" />
            <el-option label="已拒绝" value="REJECTED" />
            <el-option label="待缴费" value="WAITING_PAYMENT" />
            <el-option label="已缴费" value="PAID" />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSearch">搜索</el-button>
          <el-button @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>

      <!-- Exam Sign Up Table -->
      <el-table :data="tableData" border style="width: 100%" v-loading="loading">
        <el-table-column prop="examSignUpId" label="报名ID" width="100" />
        <el-table-column prop="examPlanName" label="考试计划" min-width="180" show-overflow-tooltip />
        <el-table-column prop="workerId" label="人员编号" width="100" />
        <el-table-column prop="workerName" label="姓名" width="100" />
        <el-table-column prop="idCard" label="身份证号" width="160" />
        <el-table-column prop="phone" label="联系电话" width="120" />
        <el-table-column prop="qualificationLevel" label="资格等级" width="120" />
        <el-table-column prop="unitName" label="单位名称" min-width="150" show-overflow-tooltip />
        <el-table-column prop="signUpDate" label="报名时间" width="120">
          <template #default="{ row }">
            {{ formatDate(row.signUpDate) }}
          </template>
        </el-table-column>
        <el-table-column prop="status" label="状态" width="100">
          <template #default="{ row }">
            <el-tag :type="getStatusType(row.status)" size="small">
              {{ getStatusText(row.status) }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="paymentConfirmed" label="缴费确认" width="90" align="center">
          <template #default="{ row }">
            <el-tag :type="row.paymentConfirmed ? 'success' : 'warning'" size="small">
              {{ row.paymentConfirmed ? '已确认' : '待确认' }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="240" fixed="right">
          <template #default="{ row }">
            <el-button link type="primary" size="small" @click="handleView(row)">查看</el-button>
            <el-button link type="primary" size="small" @click="handleEdit(row)" v-if="row.status === 'PENDING'">编辑</el-button>
            <el-button link type="success" size="small" @click="handleApprove(row)" v-if="row.status === 'PENDING'">审核</el-button>
            <el-button link type="warning" size="small" @click="handleConfirmPayment(row)" v-if="row.status === 'APPROVED' && !row.paymentConfirmed">确认缴费</el-button>
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
      width="700px"
      @close="handleDialogClose"
    >
      <el-form :model="formData" :rules="formRules" ref="formRef" label-width="120px">
        <el-divider content-position="left">基本信息</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="考试计划" prop="examPlanId">
              <el-input v-model="formData.examPlanId" placeholder="请输入考试计划ID" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="人员编号" prop="workerId">
              <el-input v-model="formData.workerId" placeholder="请输入人员编号" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-divider content-position="left">人员信息</el-divider>
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
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="联系电话" prop="phone">
              <el-input v-model="formData.phone" placeholder="请输入联系电话" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="资格等级">
              <el-input v-model="formData.qualificationLevel" placeholder="请输入资格等级" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="单位名称">
          <el-input v-model="formData.unitName" placeholder="请输入单位名称" />
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
    <el-dialog v-model="viewVisible" title="报名详情" width="650px">
      <el-descriptions :column="2" border v-if="currentRow">
        <el-descriptions-item label="报名ID">{{ currentRow.examSignUpId }}</el-descriptions-item>
        <el-descriptions-item label="状态">
          <el-tag :type="getStatusType(currentRow.status)">
            {{ getStatusText(currentRow.status) }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="考试计划ID">{{ currentRow.examPlanId }}</el-descriptions-item>
        <el-descriptions-item label="人员编号">{{ currentRow.workerId }}</el-descriptions-item>
        <el-descriptions-item label="姓名">{{ currentRow.workerName }}</el-descriptions-item>
        <el-descriptions-item label="身份证号">{{ currentRow.idCard }}</el-descriptions-item>
        <el-descriptions-item label="联系电话">{{ currentRow.phone }}</el-descriptions-item>
        <el-descriptions-item label="资格等级">{{ currentRow.qualificationLevel }}</el-descriptions-item>
        <el-descriptions-item label="单位名称" :span="2">{{ currentRow.unitName }}</el-descriptions-item>
        <el-descriptions-item label="报名时间">{{ formatDate(currentRow.signUpDate) }}</el-descriptions-item>
        <el-descriptions-item label="缴费确认">
          <el-tag :type="currentRow.paymentConfirmed ? 'success' : 'warning'">
            {{ currentRow.paymentConfirmed ? '已确认' : '待确认' }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="审核人" v-if="currentRow.checkMan">{{ currentRow.checkMan }}</el-descriptions-item>
        <el-descriptions-item label="审核结果" v-if="currentRow.checkResult">{{ currentRow.checkResult }}</el-descriptions-item>
        <el-descriptions-item label="备注" :span="2">{{ currentRow.remark || '-' }}</el-descriptions-item>
      </el-descriptions>
      <template #footer>
        <el-button @click="viewVisible = false">关闭</el-button>
      </template>
    </el-dialog>

    <!-- Approve Dialog -->
    <el-dialog v-model="approveVisible" title="审核报名" width="500px">
      <el-form :model="approveForm" :rules="approveRules" ref="approveRef" label-width="100px">
        <el-form-item label="审核人" prop="checkMan">
          <el-input v-model="approveForm.checkMan" placeholder="请输入审核人姓名" />
        </el-form-item>
        <el-form-item label="审核结果" prop="checkResult">
          <el-radio-group v-model="approveForm.checkResult">
            <el-radio label="APPROVED">通过</el-radio>
            <el-radio label="REJECTED">拒绝</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="审核意见">
          <el-input v-model="approveForm.checkComment" type="textarea" :rows="3" placeholder="请输入审核意见" />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="approveVisible = false">取消</el-button>
        <el-button type="primary" @click="handleApproveSubmit" :loading="approveLoading">确定</el-button>
      </template>
    </el-dialog>

    <!-- Payment Confirm Dialog -->
    <el-dialog v-model="paymentVisible" title="确认缴费" width="500px">
      <el-form :model="paymentForm" :rules="paymentRules" ref="paymentRef" label-width="100px">
        <el-form-item label="确认人" prop="payConfirmMan">
          <el-input v-model="paymentForm.payConfirmMan" placeholder="请输入确认人姓名" />
        </el-form-item>
        <el-form-item label="确认结果" prop="payConfirmRult">
          <el-radio-group v-model="paymentForm.payConfirmRult">
            <el-radio label="CONFIRMED">已缴费</el-radio>
            <el-radio label="NOT_PAID">未缴费</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="备注">
          <el-input v-model="paymentForm.payConfirmComment" type="textarea" :rows="3" placeholder="请输入备注" />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="paymentVisible = false">取消</el-button>
        <el-button type="primary" @click="handlePaymentSubmit" :loading="paymentLoading">确定</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import {
  createSignUp,
  updateSignUp,
  deleteSignUp,
  getSignUpById,
  approveSignUp,
  confirmPayment,
  searchSignUps
} from '@/api/examSignUp'

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
  workerName: '',
  idCard: '',
  status: ''
})

// Dialog
const dialogVisible = ref(false)
const dialogTitle = ref('新建报名')
const isEdit = ref(false)
const formRef = ref(null)
const submitLoading = ref(false)

// Form data
const formData = reactive({
  examSignUpId: null,
  examPlanId: '',
  workerId: '',
  workerName: '',
  idCard: '',
  phone: '',
  qualificationLevel: '',
  unitName: '',
  remark: '',
  status: 'PENDING'
})

// View dialog
const viewVisible = ref(false)
const currentRow = ref(null)

// Approve dialog
const approveVisible = ref(false)
const approveRef = ref(null)
const approveLoading = ref(false)
const approveForm = reactive({
  checkMan: '',
  checkResult: '',
  checkComment: ''
})

// Payment dialog
const paymentVisible = ref(false)
const paymentRef = ref(null)
const paymentLoading = ref(false)
const paymentForm = reactive({
  payConfirmMan: '',
  payConfirmRult: '',
  payConfirmComment: ''
})

// Form rules
const formRules = {
  examPlanId: [{ required: true, message: '请输入考试计划ID', trigger: 'blur' }],
  workerId: [{ required: true, message: '请输入人员编号', trigger: 'blur' }],
  workerName: [{ required: true, message: '请输入姓名', trigger: 'blur' }],
  idCard: [{ required: true, message: '请输入身份证号', trigger: 'blur' }],
  phone: [{ required: true, message: '请输入联系电话', trigger: 'blur' }]
}

const approveRules = {
  checkMan: [{ required: true, message: '请输入审核人姓名', trigger: 'blur' }],
  checkResult: [{ required: true, message: '请选择审核结果', trigger: 'change' }]
}

const paymentRules = {
  payConfirmMan: [{ required: true, message: '请输入确认人姓名', trigger: 'blur' }],
  payConfirmRult: [{ required: true, message: '请选择确认结果', trigger: 'change' }]
}

// Load exam signups
const loadExamSignUps = async () => {
  loading.value = true
  try {
    const params = {
      current: pagination.current,
      size: pagination.size,
      ...searchForm
    }
    const response = await searchSignUps(params)
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
  loadExamSignUps()
}

// Reset
const handleReset = () => {
  searchForm.examPlanName = ''
  searchForm.workerName = ''
  searchForm.idCard = ''
  searchForm.status = ''
  pagination.current = 1
  loadExamSignUps()
}

// Create
const handleCreate = () => {
  isEdit.value = false
  dialogTitle.value = '新建报名'
  resetForm()
  dialogVisible.value = true
}

// Edit
const handleEdit = (row) => {
  isEdit.value = true
  dialogTitle.value = '编辑报名'
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
        await updateSignUp(formData.examSignUpId, formData)
        ElMessage.success('更新成功')
      } else {
        await createSignUp(formData)
        ElMessage.success('创建成功')
      }
      dialogVisible.value = false
      loadExamSignUps()
    } catch (error) {
      ElMessage.error('操作失败: ' + error.message)
    } finally {
      submitLoading.value = false
    }
  })
}

// Delete
const handleDelete = (row) => {
  ElMessageBox.confirm('确定要删除该报名记录吗？', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await deleteSignUp(row.examSignUpId)
      ElMessage.success('删除成功')
      loadExamSignUps()
    } catch (error) {
      ElMessage.error('删除失败: ' + error.message)
    }
  }).catch(() => {})
}

// Approve
const handleApprove = (row) => {
  currentRow.value = row
  approveForm.checkMan = ''
  approveForm.checkResult = ''
  approveForm.checkComment = ''
  approveVisible.value = true
}

// Approve submit
const handleApproveSubmit = async () => {
  if (!approveRef.value) return

  await approveRef.value.validate(async (valid) => {
    if (!valid) return

    approveLoading.value = true
    try {
      await approveSignUp(currentRow.value.examSignUpId, approveForm.checkMan, approveForm.checkResult)
      ElMessage.success('审核完成')
      approveVisible.value = false
      loadExamSignUps()
    } catch (error) {
      ElMessage.error('审核失败: ' + error.message)
    } finally {
      approveLoading.value = false
    }
  })
}

// Confirm payment
const handleConfirmPayment = (row) => {
  currentRow.value = row
  paymentForm.payConfirmMan = ''
  paymentForm.payConfirmRult = ''
  paymentForm.payConfirmComment = ''
  paymentVisible.value = true
}

// Payment submit
const handlePaymentSubmit = async () => {
  if (!paymentRef.value) return

  await paymentRef.value.validate(async (valid) => {
    if (!valid) return

    paymentLoading.value = true
    try {
      await confirmPayment(currentRow.value.examSignUpId, paymentForm.payConfirmMan, paymentForm.payConfirmRult)
      ElMessage.success('缴费确认完成')
      paymentVisible.value = false
      loadExamSignUps()
    } catch (error) {
      ElMessage.error('确认失败: ' + error.message)
    } finally {
      paymentLoading.value = false
    }
  })
}

// Pagination
const handleSizeChange = (val) => {
  pagination.size = val
  pagination.current = 1
  loadExamSignUps()
}

const handleCurrentChange = (val) => {
  pagination.current = val
  loadExamSignUps()
}

// Dialog close
const handleDialogClose = () => {
  formRef.value?.resetFields()
  resetForm()
}

// Reset form
const resetForm = () => {
  Object.assign(formData, {
    examSignUpId: null,
    examPlanId: '',
    workerId: '',
    workerName: '',
    idCard: '',
    phone: '',
    qualificationLevel: '',
    unitName: '',
    remark: '',
    status: 'PENDING'
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
    'PENDING': 'warning',
    'APPROVED': 'success',
    'REJECTED': 'danger',
    'WAITING_PAYMENT': 'warning',
    'PAID': 'success'
  }
  return types[status] || 'info'
}

// Get status text
const getStatusText = (status) => {
  const texts = {
    'PENDING': '待审核',
    'APPROVED': '已通过',
    'REJECTED': '已拒绝',
    'WAITING_PAYMENT': '待缴费',
    'PAID': '已缴费'
  }
  return texts[status] || status
}

// Load data on mount
onMounted(() => {
  loadExamSignUps()
})
</script>

<style scoped>
.exam-signup-container {
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
