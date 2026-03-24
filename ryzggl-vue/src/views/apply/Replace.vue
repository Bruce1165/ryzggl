<template>
  <div class="apply-replace-container">
    <el-card>
      <!-- Header Actions -->
      <template #header>
        <div class="card-header">
          <span>证书补办管理</span>
          <el-button type="primary" @click="handleCreate">新建补办申请</el-button>
        </div>
      </template>

      <!-- Search Form -->
      <el-form :model="searchForm" :inline="true" class="search-form">
        <el-form-item label="注册号">
          <el-input v-model="searchForm.registerNo" placeholder="请输入注册号" clearable />
        </el-form-item>
        <el-form-item label="补办类型">
          <el-select v-model="searchForm.replaceType" placeholder="请选择补办类型" clearable>
            <el-option label="证书遗失" value="LOST" />
            <el-option label="证书损坏" value="DAMAGED" />
            <el-option label="信息变更" value="INFO_CHANGE" />
            <el-option label="其他" value="OTHER" />
          </el-select>
        </el-form-item>
        <el-form-item label="状态">
          <el-select v-model="searchForm.status" placeholder="请选择状态" clearable>
            <el-option label="待审核" value="PENDING" />
            <el-option label="已通过" value="APPROVED" />
            <el-option label="已拒绝" value="REJECTED" />
            <el-option label="已处理" value="COMPLETED" />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSearch">搜索</el-button>
          <el-button @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>

      <!-- Apply Replace Table -->
      <el-table :data="tableData" border style="width: 100%" v-loading="loading">
        <el-table-column prop="applyId" label="申请ID" width="100" />
        <el-table-column prop="registerNo" label="注册号" width="140" />
        <el-table-column prop="registerCertificateNo" label="注册证书号" width="160" />
        <el-table-column prop="replaceType" label="补办类型" width="110">
          <template #default="{ row }">
            <el-tag :type="getReplaceTypeColor(row.replaceType)" size="small">
              {{ getReplaceTypeText(row.replaceType) }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="psnMobilePhone" label="手机号" width="120" />
        <el-table-column prop="psnEmail" label="邮箱" min-width="150" show-overflow-tooltip />
        <el-table-column prop="linkMan" label="联系人" width="100" />
        <el-table-column prop="disnableDate" label="失效日期" width="110">
          <template #default="{ row }">
            {{ formatDate(row.disnableDate) }}
          </template>
        </el-table-column>
        <el-table-column prop="status" label="状态" width="90">
          <template #default="{ row }">
            <el-tag :type="getStatusType(row.status)" size="small">
              {{ getStatusText(row.status) }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="createTime" label="创建时间" width="160">
          <template #default="{ row }">
            {{ formatDateTime(row.createTime) }}
          </template>
        </el-table-column>
        <el-table-column label="操作" width="180" fixed="right">
          <template #default="{ row }">
            <el-button link type="primary" size="small" @click="handleView(row)">查看</el-button>
            <el-button link type="primary" size="small" @click="handleEdit(row)" v-if="row.status === 'PENDING'">编辑</el-button>
            <el-button link type="success" size="small" @click="handleApprove(row)" v-if="row.status === 'PENDING'">通过</el-button>
            <el-button link type="danger" size="small" @click="handleReject(row)" v-if="row.status === 'PENDING'">拒绝</el-button>
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
      width="800px"
      @close="handleDialogClose"
    >
      <el-form :model="formData" :rules="formRules" ref="formRef" label-width="130px">
        <el-divider content-position="left">基本信息</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="注册号" prop="registerNo">
              <el-input v-model="formData.registerNo" placeholder="请输入注册号" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="注册证书号" prop="registerCertificateNo">
              <el-input v-model="formData.registerCertificateNo" placeholder="请输入注册证书号" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="失效日期" prop="disnableDate">
              <el-date-picker
                v-model="formData.disnableDate"
                type="date"
                placeholder="选择失效日期"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="有效码" prop="validCode">
              <el-input v-model="formData.validCode" placeholder="请输入有效码" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-divider content-position="left">个人信息</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="手机号" prop="psnMobilePhone">
              <el-input v-model="formData.psnMobilePhone" placeholder="请输入手机号" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="邮箱" prop="psnEmail">
              <el-input v-model="formData.psnEmail" placeholder="请输入邮箱" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="联系人" prop="linkMan">
              <el-input v-model="formData.linkMan" placeholder="请输入联系人" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-divider content-position="left">企业信息</el-divider>
        <el-form-item label="企业电话" prop="entTelephone">
          <el-input v-model="formData.entTelephone" placeholder="请输入企业电话" />
        </el-form-item>
        <el-form-item label="企业通讯地址" prop="entCorrespondence">
          <el-input v-model="formData.entCorrespondence" type="textarea" :rows="2" placeholder="请输入企业通讯地址" />
        </el-form-item>
        <el-form-item label="企业邮编" prop="entPostcode">
          <el-input v-model="formData.entPostcode" placeholder="请输入企业邮编" />
        </el-form-item>

        <el-divider content-position="left">注册专业信息</el-divider>
        <el-row :gutter="10">
          <el-col :span="6">
            <el-form-item label="注册专业1">
              <el-input v-model="formData.psnRegisteProfession1" placeholder="专业1" />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="有效期1">
              <el-input v-model="formData.psnCertificateValidity1" placeholder="有效期1" />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="注册专业2">
              <el-input v-model="formData.psnRegisteProfession2" placeholder="专业2" />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="有效期2">
              <el-input v-model="formData.psnCertificateValidity2" placeholder="有效期2" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="10">
          <el-col :span="6">
            <el-form-item label="注册专业3">
              <el-input v-model="formData.psnRegisteProfession3" placeholder="专业3" />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="有效期3">
              <el-input v-model="formData.psnCertificateValidity3" placeholder="有效期3" />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="注册专业4">
              <el-input v-model="formData.psnRegisteProfession4" placeholder="专业4" />
            </el-form-item>
          </el-col>
          <el-col :span="6">
            <el-form-item label="有效期4">
              <el-input v-model="formData.psnCertificateValidity4" placeholder="有效期4" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-divider content-position="left">补办信息</el-divider>
        <el-form-item label="补办类型" prop="replaceType">
          <el-radio-group v-model="formData.replaceType">
            <el-radio label="LOST">证书遗失</el-radio>
            <el-radio label="DAMAGED">证书损坏</el-radio>
            <el-radio label="INFO_CHANGE">信息变更</el-radio>
            <el-radio label="OTHER">其他</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="补办原因" prop="replaceReason">
          <el-input v-model="formData.replaceReason" type="textarea" :rows="4" placeholder="请详细描述补办原因" />
        </el-form-item>
      </el-form>

      <template #footer>
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSubmit" :loading="submitLoading">确定</el-button>
      </template>
    </el-dialog>

    <!-- View Dialog -->
    <el-dialog v-model="viewVisible" title="补办申请详情" width="700px">
      <el-descriptions :column="2" border v-if="currentRow">
        <el-descriptions-item label="申请ID">{{ currentRow.applyId }}</el-descriptions-item>
        <el-descriptions-item label="状态">
          <el-tag :type="getStatusType(currentRow.status)">
            {{ getStatusText(currentRow.status) }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="注册号">{{ currentRow.registerNo }}</el-descriptions-item>
        <el-descriptions-item label="注册证书号">{{ currentRow.registerCertificateNo }}</el-descriptions-item>
        <el-descriptions-item label="补办类型">
          <el-tag :type="getReplaceTypeColor(currentRow.replaceType)">
            {{ getReplaceTypeText(currentRow.replaceType) }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="失效日期">{{ formatDate(currentRow.disnableDate) }}</el-descriptions-item>
        <el-descriptions-item label="有效码">{{ currentRow.validCode }}</el-descriptions-item>
        <el-descriptions-item label="手机号">{{ currentRow.psnMobilePhone }}</el-descriptions-item>
        <el-descriptions-item label="邮箱">{{ currentRow.psnEmail }}</el-descriptions-item>
        <el-descriptions-item label="联系人">{{ currentRow.linkMan }}</el-descriptions-item>
        <el-descriptions-item label="企业电话">{{ currentRow.entTelephone }}</el-descriptions-item>
        <el-descriptions-item label="企业地址" :span="2">{{ currentRow.entCorrespondence }}</el-descriptions-item>
        <el-descriptions-item label="企业邮编">{{ currentRow.entPostcode }}</el-descriptions-item>
        <el-descriptions-item label="创建时间">{{ formatDateTime(currentRow.createTime) }}</el-descriptions-item>
        <el-descriptions-item label="更新时间">{{ formatDateTime(currentRow.updateTime) }}</el-descriptions-item>
        <el-descriptions-item label="注册专业1">{{ currentRow.psnRegisteProfession1 }}</el-descriptions-item>
        <el-descriptions-item label="有效期1">{{ currentRow.psnCertificateValidity1 }}</el-descriptions-item>
        <el-descriptions-item label="注册专业2">{{ currentRow.psnRegisteProfession2 }}</el-descriptions-item>
        <el-descriptions-item label="有效期2">{{ currentRow.psnCertificateValidity2 }}</el-descriptions-item>
        <el-descriptions-item label="注册专业3">{{ currentRow.psnRegisteProfession3 }}</el-descriptions-item>
        <el-descriptions-item label="有效期3">{{ currentRow.psnCertificateValidity3 }}</el-descriptions-item>
        <el-descriptions-item label="注册专业4">{{ currentRow.psnRegisteProfession4 }}</el-descriptions-item>
        <el-descriptions-item label="有效期4">{{ currentRow.psnCertificateValidity4 }}</el-descriptions-item>
        <el-descriptions-item label="补办原因" :span="2">{{ currentRow.replaceReason }}</el-descriptions-item>
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
  getApplyReplaceList,
  createApplyReplace,
  updateApplyReplace,
  deleteApplyReplace,
  updateApplyReplaceStatus
} from '@/api/applyReplace'

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
  registerNo: '',
  replaceType: '',
  status: ''
})

// Dialog
const dialogVisible = ref(false)
const dialogTitle = ref('新建补办申请')
const isEdit = ref(false)
const formRef = ref(null)
const submitLoading = ref(false)

// Form data
const formData = reactive({
  applyId: null,
  psnMobilePhone: '',
  psnEmail: '',
  registerNo: '',
  registerCertificateNo: '',
  disnableDate: '',
  validCode: '',
  linkMan: '',
  entTelephone: '',
  entCorrespondence: '',
  entPostcode: '',
  psnRegisteProfession1: '',
  psnCertificateValidity1: '',
  psnRegisteProfession2: '',
  psnCertificateValidity2: '',
  psnRegisteProfession3: '',
  psnCertificateValidity3: '',
  psnRegisteProfession4: '',
  psnCertificateValidity4: '',
  replaceReason: '',
  replaceType: '',
  status: 'PENDING'
})

// View dialog
const viewVisible = ref(false)
const currentRow = ref(null)

// Form rules
const formRules = {
  registerNo: [{ required: true, message: '请输入注册号', trigger: 'blur' }],
  registerCertificateNo: [{ required: true, message: '请输入注册证书号', trigger: 'blur' }],
  psnMobilePhone: [
    { required: true, message: '请输入手机号', trigger: 'blur' },
    { pattern: /^1[3-9]\d{9}$/, message: '请输入正确的手机号码', trigger: 'blur' }
  ],
  psnEmail: [
    { required: true, message: '请输入邮箱', trigger: 'blur' },
    { type: 'email', message: '请输入正确的邮箱地址', trigger: 'blur' }
  ],
  replaceType: [{ required: true, message: '请选择补办类型', trigger: 'change' }],
  replaceReason: [{ required: true, message: '请输入补办原因', trigger: 'blur' }]
}

// Load apply replaces
const loadApplyReplaces = async () => {
  loading.value = true
  try {
    const params = {
      current: pagination.current,
      size: pagination.size,
      ...searchForm
    }
    const response = await getApplyReplaceList(params)
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
  loadApplyReplaces()
}

// Reset
const handleReset = () => {
  searchForm.registerNo = ''
  searchForm.replaceType = ''
  searchForm.status = ''
  pagination.current = 1
  loadApplyReplaces()
}

// Create
const handleCreate = () => {
  isEdit.value = false
  dialogTitle.value = '新建补办申请'
  resetForm()
  dialogVisible.value = true
}

// Edit
const handleEdit = (row) => {
  isEdit.value = true
  dialogTitle.value = '编辑补办申请'
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
        await updateApplyReplace(formData.applyId, formData)
        ElMessage.success('更新成功')
      } else {
        await createApplyReplace(formData)
        ElMessage.success('创建成功')
      }
      dialogVisible.value = false
      loadApplyReplaces()
    } catch (error) {
      ElMessage.error('操作失败: ' + error.message)
    } finally {
      submitLoading.value = false
    }
  })
}

// Delete
const handleDelete = (row) => {
  ElMessageBox.confirm('确定要删除该补办申请吗？', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await deleteApplyReplace(row.applyId)
      ElMessage.success('删除成功')
      loadApplyReplaces()
    } catch (error) {
      ElMessage.error('删除失败: ' + error.message)
    }
  }).catch(() => {})
}

// Approve
const handleApprove = (row) => {
  ElMessageBox.confirm('确定要通过该补办申请吗？', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await updateApplyReplaceStatus(row.applyId, 'APPROVED')
      ElMessage.success('已通过')
      loadApplyReplaces()
    } catch (error) {
      ElMessage.error('操作失败: ' + error.message)
    }
  }).catch(() => {})
}

// Reject
const handleReject = (row) => {
  ElMessageBox.confirm('确定要拒绝该补办申请吗？', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await updateApplyReplaceStatus(row.applyId, 'REJECTED')
      ElMessage.success('已拒绝')
      loadApplyReplaces()
    } catch (error) {
      ElMessage.error('操作失败: ' + error.message)
    }
  }).catch(() => {})
}

// Pagination
const handleSizeChange = (val) => {
  pagination.size = val
  pagination.current = 1
  loadApplyReplaces()
}

const handleCurrentChange = (val) => {
  pagination.current = val
  loadApplyReplaces()
}

// Dialog close
const handleDialogClose = () => {
  formRef.value?.resetFields()
  resetForm()
}

// Reset form
const resetForm = () => {
  Object.assign(formData, {
    applyId: null,
    psnMobilePhone: '',
    psnEmail: '',
    registerNo: '',
    registerCertificateNo: '',
    disnableDate: '',
    validCode: '',
    linkMan: '',
    entTelephone: '',
    entCorrespondence: '',
    entPostcode: '',
    psnRegisteProfession1: '',
    psnCertificateValidity1: '',
    psnRegisteProfession2: '',
    psnCertificateValidity2: '',
    psnRegisteProfession3: '',
    psnCertificateValidity3: '',
    psnRegisteProfession4: '',
    psnCertificateValidity4: '',
    replaceReason: '',
    replaceType: '',
    status: 'PENDING'
  })
}

// Format date
const formatDate = (date) => {
  if (!date) return '-'
  return date
}

// Format datetime
const formatDateTime = (datetime) => {
  if (!datetime) return '-'
  return datetime
}

// Get status type
const getStatusType = (status) => {
  const types = {
    'PENDING': 'warning',
    'APPROVED': 'success',
    'REJECTED': 'danger',
    'COMPLETED': 'info'
  }
  return types[status] || 'info'
}

// Get status text
const getStatusText = (status) => {
  const texts = {
    'PENDING': '待审核',
    'APPROVED': '已通过',
    'REJECTED': '已拒绝',
    'COMPLETED': '已处理'
  }
  return texts[status] || status
}

// Get replace type color
const getReplaceTypeColor = (type) => {
  const colors = {
    'LOST': 'danger',
    'DAMAGED': 'warning',
    'INFO_CHANGE': 'primary',
    'OTHER': 'info'
  }
  return colors[type] || 'info'
}

// Get replace type text
const getReplaceTypeText = (type) => {
  const texts = {
    'LOST': '证书遗失',
    'DAMAGED': '证书损坏',
    'INFO_CHANGE': '信息变更',
    'OTHER': '其他'
  }
  return texts[type] || type
}

// Load data on mount
onMounted(() => {
  loadApplyReplaces()
})
</script>

<style scoped>
.apply-replace-container {
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
