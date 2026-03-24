<template>
  <div class="exam-place-container">
    <el-card>
      <!-- Header Actions -->
      <template #header>
        <div class="card-header">
          <span>考点管理</span>
          <el-button type="primary" @click="handleCreate">新建考点</el-button>
        </div>
      </template>

      <!-- Search Form -->
      <el-form :model="searchForm" :inline="true" class="search-form">
        <el-form-item label="考点名称">
          <el-input v-model="searchForm.examPlaceName" placeholder="请输入考点名称" clearable />
        </el-form-item>
        <el-form-item label="状态">
          <el-select v-model="searchForm.status" placeholder="请选择状态" clearable>
            <el-option label="启用" value="ACTIVE" />
            <el-option label="禁用" value="INACTIVE" />
            <el-option label="已删除" value="DELETED" />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSearch">搜索</el-button>
          <el-button @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>

      <!-- Exam Place Table -->
      <el-table :data="tableData" border style="width: 100%" v-loading="loading">
        <el-table-column prop="examPlaceId" label="ID" width="80" />
        <el-table-column prop="examPlaceName" label="考点名称" min-width="180" />
        <el-table-column prop="examPlaceAddress" label="考点地址" min-width="200" show-overflow-tooltip />
        <el-table-column prop="linkMan" label="联系人" width="100" />
        <el-table-column prop="phone" label="联系电话" width="130" />
        <el-table-column prop="roomNum" label="房间数" width="80" align="center" />
        <el-table-column prop="examPersonNum" label="考试容量" width="90" align="center" />
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
            <el-button link type="primary" size="small" @click="handleEdit(row)">编辑</el-button>
            <el-button link type="warning" size="small" @click="handleToggleStatus(row)">
              {{ row.status === 'ACTIVE' ? '禁用' : '启用' }}
            </el-button>
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
        <el-form-item label="考点名称" prop="examPlaceName">
          <el-input v-model="formData.examPlaceName" placeholder="请输入考点名称" />
        </el-form-item>
        <el-form-item label="考点地址" prop="examPlaceAddress">
          <el-input v-model="formData.examPlaceAddress" placeholder="请输入考点地址" />
        </el-form-item>
        <el-form-item label="联系人" prop="linkMan">
          <el-input v-model="formData.linkMan" placeholder="请输入联系人姓名" />
        </el-form-item>
        <el-form-item label="联系电话" prop="phone">
          <el-input v-model="formData.phone" placeholder="请输入联系电话" />
        </el-form-item>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="房间数量" prop="roomNum">
              <el-input-number v-model="formData.roomNum" :min="1" :max="100" style="width: 100%" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="考试容量" prop="examPersonNum">
              <el-input-number v-model="formData.examPersonNum" :min="1" :max="10000" style="width: 100%" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="状态" prop="status">
          <el-select v-model="formData.status" placeholder="请选择状态">
            <el-option label="启用" value="ACTIVE" />
            <el-option label="禁用" value="INACTIVE" />
          </el-select>
        </el-form-item>
      </el-form>

      <template #footer>
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSubmit" :loading="submitLoading">确定</el-button>
      </template>
    </el-dialog>

    <!-- View Dialog -->
    <el-dialog v-model="viewVisible" title="考点详情" width="500px">
      <el-descriptions :column="1" border v-if="currentRow">
        <el-descriptions-item label="考点名称">{{ currentRow.examPlaceName }}</el-descriptions-item>
        <el-descriptions-item label="考点地址">{{ currentRow.examPlaceAddress }}</el-descriptions-item>
        <el-descriptions-item label="联系人">{{ currentRow.linkMan }}</el-descriptions-item>
        <el-descriptions-item label="联系电话">{{ currentRow.phone }}</el-descriptions-item>
        <el-descriptions-item label="房间数量">{{ currentRow.roomNum }}</el-descriptions-item>
        <el-descriptions-item label="考试容量">{{ currentRow.examPersonNum }}</el-descriptions-item>
        <el-descriptions-item label="状态">
          <el-tag :type="getStatusType(currentRow.status)">
            {{ getStatusText(currentRow.status) }}
          </el-tag>
        </el-descriptions-item>
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
  getExamPlaceList,
  createExamPlace,
  updateExamPlace,
  deleteExamPlace,
  updateExamPlaceStatus
} from '@/api/examPlace'

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
  examPlaceName: '',
  status: ''
})

// Dialog
const dialogVisible = ref(false)
const dialogTitle = ref('新建考点')
const isEdit = ref(false)
const formRef = ref(null)
const submitLoading = ref(false)

// Form data
const formData = reactive({
  examPlaceId: null,
  examPlaceName: '',
  examPlaceAddress: '',
  linkMan: '',
  phone: '',
  roomNum: 10,
  examPersonNum: 100,
  status: 'ACTIVE'
})

// View dialog
const viewVisible = ref(false)
const currentRow = ref(null)

// Form rules
const formRules = {
  examPlaceName: [{ required: true, message: '请输入考点名称', trigger: 'blur' }],
  examPlaceAddress: [{ required: true, message: '请输入考点地址', trigger: 'blur' }],
  phone: [
    { required: true, message: '请输入联系电话', trigger: 'blur' },
    { pattern: /^1[3-9]\d{9}$/, message: '请输入正确的手机号码', trigger: 'blur' }
  ],
  roomNum: [{ required: true, message: '请输入房间数量', trigger: 'blur' }],
  examPersonNum: [{ required: true, message: '请输入考试容量', trigger: 'blur' }]
}

// Load exam places
const loadExamPlaces = async () => {
  loading.value = true
  try {
    const params = {
      current: pagination.current,
      size: pagination.size,
      ...searchForm
    }
    const response = await getExamPlaceList(params)
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
  loadExamPlaces()
}

// Reset
const handleReset = () => {
  searchForm.examPlaceName = ''
  searchForm.status = ''
  pagination.current = 1
  loadExamPlaces()
}

// Create
const handleCreate = () => {
  isEdit.value = false
  dialogTitle.value = '新建考点'
  resetForm()
  dialogVisible.value = true
}

// Edit
const handleEdit = (row) => {
  isEdit.value = true
  dialogTitle.value = '编辑考点'
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
        await updateExamPlace(formData.examPlaceId, formData)
        ElMessage.success('更新成功')
      } else {
        await createExamPlace(formData)
        ElMessage.success('创建成功')
      }
      dialogVisible.value = false
      loadExamPlaces()
    } catch (error) {
      ElMessage.error('操作失败: ' + error.message)
    } finally {
      submitLoading.value = false
    }
  })
}

// Delete
const handleDelete = (row) => {
  ElMessageBox.confirm('确定要删除该考点吗？', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await deleteExamPlace(row.examPlaceId)
      ElMessage.success('删除成功')
      loadExamPlaces()
    } catch (error) {
      ElMessage.error('删除失败: ' + error.message)
    }
  }).catch(() => {})
}

// Toggle status
const handleToggleStatus = (row) => {
  const newStatus = row.status === 'ACTIVE' ? 'INACTIVE' : 'ACTIVE'
  const actionText = newStatus === 'ACTIVE' ? '启用' : '禁用'

  ElMessageBox.confirm(`确定要${actionText}该考点吗？`, '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await updateExamPlaceStatus(row.examPlaceId, newStatus)
      ElMessage.success(`${actionText}成功`)
      loadExamPlaces()
    } catch (error) {
      ElMessage.error(`${actionText}失败: ` + error.message)
    }
  }).catch(() => {})
}

// Pagination
const handleSizeChange = (val) => {
  pagination.size = val
  pagination.current = 1
  loadExamPlaces()
}

const handleCurrentChange = (val) => {
  pagination.current = val
  loadExamPlaces()
}

// Dialog close
const handleDialogClose = () => {
  formRef.value?.resetFields()
  resetForm()
}

// Reset form
const resetForm = () => {
  Object.assign(formData, {
    examPlaceId: null,
    examPlaceName: '',
    examPlaceAddress: '',
    linkMan: '',
    phone: '',
    roomNum: 10,
    examPersonNum: 100,
    status: 'ACTIVE'
  })
}

// Get status type
const getStatusType = (status) => {
  const types = {
    'ACTIVE': 'success',
    'INACTIVE': 'warning',
    'DELETED': 'danger'
  }
  return types[status] || 'info'
}

// Get status text
const getStatusText = (status) => {
  const texts = {
    'ACTIVE': '启用',
    'INACTIVE': '禁用',
    'DELETED': '已删除'
  }
  return texts[status] || status
}

// Load data on mount
onMounted(() => {
  loadExamPlaces()
})
</script>

<style scoped>
.exam-place-container {
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
