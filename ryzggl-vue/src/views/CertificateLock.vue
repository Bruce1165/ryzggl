<template>
  <div class="certificate-lock-container">
    <el-card>
      <!-- Header Actions -->
      <template #header>
        <div class="card-header">
          <span>证书锁定管理</span>
          <el-button type="primary" @click="handleCreateLock">锁定证书</el-button>
        </div>
      </template>

      <!-- Search Form -->
      <el-form :model="searchForm" :inline="true" class="search-form">
        <el-form-item label="证书ID">
          <el-input v-model="searchForm.certificateId" placeholder="请输入证书ID" clearable />
        </el-form-item>
        <el-form-item label="锁定人">
          <el-input v-model="searchForm.lockPerson" placeholder="请输入锁定人" clearable />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSearch">搜索</el-button>
          <el-button @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>

      <!-- Lock Table -->
      <el-table :data="tableData" border style="width: 100%" v-loading="loading">
        <el-table-column prop="lockId" label="ID" width="80" />
        <el-table-column prop="certificateId" label="证书ID" width="100" />
        <el-table-column prop="lockType" label="锁定类型" width="100">
          <template #default="{ row }">
            <el-tag :type="getLockTypeColor(row.lockType)" size="small">
              {{ getLockTypeText(row.lockType) }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="lockPerson" label="锁定人" width="120" />
        <el-table-column prop="lockTime" label="锁定时间" width="160">
          <template #default="{ row }">
            {{ formatDateTime(row.lockTime) }}
          </template>
        </el-table-column>
        <el-table-column prop="remark" label="备注" min-width="150" show-overflow-tooltip />
        <el-table-column prop="lockStatus" label="状态" width="90" align="center">
          <template #default="{ row }">
            <el-tag :type="getLockStatusColor(row.lockStatus)" size="small">
              {{ getLockStatusText(row.lockStatus) }}
            </el-tag>
          </template>
        <el-table-column prop="unlockTime" label="解锁时间" width="160">
          <template #default="{ row }">
            {{ formatDateTime(row.unlockTime) || '-' }}
          </template>
        </el-table-column>
        <el-table-column prop="unlockPerson" label="解锁人" width="120" />
        <el-table-column label="操作" width="180" fixed="right">
          <template #default="{ row }">
            <el-button link type="primary" size="small" @click="handleView(row)">查看</el-button>
            <el-button link type="success" size="small" @click="handleUnlock(row)" v-if="row.lockStatus === 'LOCKED'">解锁</el-button>
            <el-button link type="danger" size="small" @click="handleDelete(row)" v-if="!row.unlockTime">删除</el-button>
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

    <!-- Lock Dialog -->
    <el-dialog
      v-model="dialogVisible"
      title="锁定证书"
      width="500px"
      @close="handleDialogClose"
    >
      <el-form :model="formData" :rules="formRules" ref="formRef" label-width="120px">
        <el-form-item label="证书ID" prop="certificateId">
          <el-input v-model="formData.certificateId" placeholder="请输入证书ID" type="number" disabled />
        </el-form-item>
        <el-form-item label="锁定类型" prop="lockType">
          <el-select v-model="formData.lockType" placeholder="请选择锁定类型">
            <el-option label="业务办理" value="BUSINESS" />
            <el-option label="行政处罚" value="ADMIN" />
            <el-option label="司法冻结" value="LEGAL" />
            <el-option label="其他" value="OTHER" />
          </el-select>
        </el-form-item>
        <el-form-item label="锁定人" prop="lockPerson">
          <el-input v-model="formData.lockPerson" placeholder="请输入锁定人" />
        </el-form-item>
        <el-form-item label="备注">
          <el-input v-model="formData.remark" type="textarea" :rows="3" placeholder="请输入备注" />
        </el-form-item>
      </el-form>

      <template #footer>
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSubmit" :loading="submitLoading">确定锁定</el-button>
      </template>
    </el-dialog>

    <!-- View Dialog -->
    <el-dialog v-model="viewVisible" title="锁定详情" width="500px">
      <el-descriptions :column="1" border v-if="currentRow">
        <el-descriptions-item label="锁定记录ID">{{ currentRow.lockId }}</el-descriptions-item>
        <el-descriptions-item label="证书ID">{{ currentRow.certificateId }}</el-descriptions-item>
        <el-descriptions-item label="锁定类型">
          <el-tag :type="getLockTypeColor(currentRow.lockType)" size="small">
            {{ getLockTypeText(currentRow.lockType) }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="锁定人">{{ currentRow.lockPerson }}</el-descriptions-item>
        <el-descriptions-item label="锁定时间">{{ formatDateTime(currentRow.lockTime) }}</el-descriptions-item>
        <el-descriptions-item label="备注">{{ currentRow.remark || '-' }}</el-descriptions-item>
        <el-descriptions-item label="状态">
          <el-tag :type="getLockStatusColor(currentRow.lockStatus)">
            {{ getLockStatusText(currentRow.lockStatus) }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="解锁时间">{{ formatDateTime(currentRow.unlockTime) || '-' }}</el-descriptions-item>
        <el-descriptions-item label="解锁人">{{ currentRow.unlockPerson || '-' }}</el-descriptions-item>
        <el-descriptions-item label="创建时间">{{ formatDateTime(currentRow.createTime) }}</el-descriptions-item>
        <el-descriptions-item label="修改时间">{{ formatDateTime(currentRow.modifyTime) }}</el-descriptions-item>
      </el-descriptions>
      <template #footer>
        <el-button @click="viewVisible = false">关闭</el-button>
      </template>
    </el-dialog>

    <!-- Unlock Dialog -->
    <el-dialog v-model="unlockDialogVisible" title="解锁证书" width="400px">
      <el-form :model="unlockForm" :rules="unlockRules" ref="unlockRef" label-width="120px">
        <el-form-item label="解锁人">
          <el-input v-model="unlockForm.unlockPerson" placeholder="请输入解锁人姓名" />
        </el-form-item>
      </el-form>

      <template #footer>
        <el-button @click="unlockDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleUnlockSubmit" :loading="unlockLoading">确定解锁</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import {
  getLockById,
  getLocksByCertificateId,
  getActiveLocks,
  searchLocks,
  lockCertificate,
  unlockCertificate,
  getLastLock,
  getLockStatus,
  deleteLock
} from '@/api/certificateLock'

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
  certificateId: null,
  lockPerson: ''
})

// Dialog
const dialogVisible = ref(false)
const formRef = ref(null)
const submitLoading = ref(false)

// Form data
const formData = reactive({
  certificateId: null,
  lockType: 'BUSINESS',
  lockPerson: '',
  remark: ''
})

// View dialog
const viewVisible = ref(false)
const currentRow = ref(null)

// Unlock dialog
const unlockDialogVisible = ref(false)
const unlockRef = ref(null)
const unlockLoading = ref(false)

// Unlock form
const unlockForm = reactive({
  unlockPerson: ''
})

// Form rules
const formRules = {
  certificateId: [{ required: true, message: '请输入证书ID', trigger: 'blur' }],
  lockType: [{ required: true, message: '请选择锁定类型', trigger: 'change' }],
  lockPerson: [{ required: true, message: '请输入锁定人', trigger: 'blur' }]
}

const unlockRules = {
  unlockPerson: [{ required: true, message: '请输入解锁人姓名', trigger: 'blur' }]
}

// Load locks
const loadLocks = async () => {
  loading.value = true
  try {
    const params = {
      current: pagination.current,
      size: pagination.size
    }
    if (searchForm.certificateId) {
      params.certificateId = searchForm.certificateId
    }
    if (searchForm.lockPerson) {
      params.lockPerson = searchForm.lockPerson
    }
    const response = await getActiveLocks()
    if (response.code === 200) {
      tableData.value = response.data || []
      pagination.total = response.data?.length || 0
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
  loadLocks()
}

// Reset
const handleReset = () => {
  searchForm.certificateId = null
  searchForm.lockPerson = ''
  pagination.current = 1
  loadLocks()
}

// Create lock
const handleCreateLock = () => {
  formData.certificateId = null
  dialogVisible.value = true
}

// View
const handleView = (row) => {
  currentRow.value = row
  viewVisible.value = true
}

// Unlock
const handleUnlock = (row) => {
  unlockForm.unlockPerson = ''
  unlockDialogVisible.value = true
  currentRow.value = row
}

// Submit unlock
const handleUnlockSubmit = async () => {
  if (!unlockRef.value) return

  await unlockRef.value.validate(async (valid) => {
    if (!valid) return

    unlockLoading.value = true
    try {
      await unlockCertificate(row.certificateId, unlockForm.unlockPerson)
      ElMessage.success('解锁成功')
      unlockDialogVisible.value = false
      loadLocks()
    } catch (error) {
      ElMessage.error('解锁失败: ' + error.message)
    } finally {
      unlockLoading.value = false
    }
  })
}

// Submit lock
const handleSubmit = async () => {
  if (!formRef.value) return

  await formRef.value.validate(async (valid) => {
    if (!valid) return

    submitLoading.value = true
    try {
      await lockCertificate(
        formData.certificateId,
        formData.lockType,
        formData.lockPerson,
        formData.remark
      )
      ElMessage.success('锁定成功')
      dialogVisible.value = false
      loadLocks()
    } catch (error) {
      ElMessage.error('锁定失败: ' + error.message)
    } finally {
      submitLoading.value = false
    }
  })
}

// Delete
const handleDelete = (row) => {
  ElMessageBox.confirm('确定要删除该锁定记录吗？此操作不可恢复。', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await deleteLock(row.lockId)
      ElMessage.success('删除成功')
      loadLocks()
    } catch (error) {
      ElMessage.error('删除失败: ' + error.message)
    }
  }).catch(() => {})
}

// Pagination
const handleSizeChange = (val) => {
  pagination.size = val
  pagination.current = 1
  loadLocks()
}

const handleCurrentChange = (val) => {
  pagination.current = val
  loadLocks()
}

// Dialog close
const handleDialogClose = () => {
  formRef.value?.resetFields()
  resetForm()
}

// Reset form
const resetForm = () => {
  Object.assign(formData, {
    certificateId: null,
    lockType: 'BUSINESS',
    lockPerson: '',
    remark: ''
  })
}

// Format datetime
const formatDateTime = (datetime) => {
  if (!datetime) return '-'
  return new Date(datetime).toLocaleString('zh-CN', { timeZone: 'Asia/Shanghai' })
}

// Get lock type color
const getLockTypeColor = (type) => {
  const colors = {
    'BUSINESS': 'primary',
    'ADMIN': 'danger',
    'LEGAL': 'warning',
    'OTHER': 'info'
  }
  return colors[type] || 'info'
}

// Get lock type text
const getLockTypeText = (type) => {
  const texts = {
    'BUSINESS': '业务办理',
    'ADMIN': '行政处罚',
    'LEGAL': '司法冻结',
    'OTHER': '其他'
  }
  return texts[type] || type
}

// Get lock status color
const getLockStatusColor = (status) => {
  const colors = {
    'LOCKED': 'success',
    'UNLOCKED': 'info'
  }
  return colors[status] || 'info'
}

// Get lock status text
const getLockStatusText = (status) => {
  const texts = {
    'LOCKED': '已锁定',
    'UNLOCKED': '已解锁'
  }
  return texts[status] || status
}

// Load data on mount
onMounted(() => {
  loadLocks()
})
</script>

<style scoped>
.certificate-lock-container {
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
