<template>
  <div class="qualification-container">
    <el-card>
      <!-- Header Actions -->
      <template #header>
        <div class="card-header">
          <span>资格管理</span>
          <el-button type="primary" @click="handleCreate">新建资格</el-button>
        </div>
      </template>

      <!-- Search Form -->
      <el-form :model="searchForm" :inline="true" class="search-form">
        <el-form-item label="资格名称">
          <el-input v-model="searchForm.qualName" placeholder="请输入资格名称" clearable />
        </el-form-item>
        <el-form-item label="资格编码">
          <el-input v-model="searchForm.qualCode" placeholder="请输入资格编码" clearable />
        </el-form-item>
        <el-form-item label="资格类型">
          <el-select v-model="searchForm.qualType" placeholder="请选择类型" clearable>
            <el-option label="建造" value="建造" />
            <el-option label="造价" value="造价" />
            <el-option label="监理" value="监理" />
            <el-option label="其他" value="其他" />
          </el-select>
        </el-form-item>
        <el-form-item label="资格等级">
          <el-select v-model="searchForm.qualLevel" placeholder="请选择等级" clearable>
            <el-option label="初级" value="初级" />
            <el-option label="中级" value="中级" />
            <el-option label="高级" value="高级" />
            <el-option label="正高级" value="正高级" />
          </el-select>
        </el-form-item>
        <el-form-item label="资格类别">
          <el-input v-model="searchForm.qualCategory" placeholder="请输入类别" clearable />
        </el-form-item>
        <el-form-item label="状态">
          <el-select v-model="searchForm.isValid" placeholder="请选择状态" clearable>
            <el-option label="全部" value="" />
            <el-option label="有效" value="true" />
            <el-option label="无效" value="false" />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSearch">搜索</el-button>
          <el-button @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>

      <!-- Qualification Table -->
      <el-table :data="tableData" border style="width: 100%" v-loading="loading">
        <el-table-column prop="qualId" label="ID" width="80" />
        <el-table-column prop="qualCode" label="资格编码" width="120" />
        <el-table-column prop="qualName" label="资格名称" min-width="180" show-overflow-tooltip />
        <el-table-column prop="qualType" label="资格类型" width="100">
          <template #default="{ row }">
            <el-tag :type="getQualTypeColor(row.qualType)" size="small">
              {{ row.qualType }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="qualLevel" label="等级" width="80" />
        <el-table-column prop="qualCategory" label="类别" width="120" show-overflow-tooltip />
        <el-table-column prop="issuingAuthority" label="发证机构" min-width="150" show-overflow-tooltip />
        <el-table-column prop="issueDate" label="发证日期" width="110">
          <template #default="{ row }">
            {{ formatDate(row.issueDate) }}
          </template>
        </el-table-column>
        <el-table-column prop="validFromDate" label="有效期开始" width="110">
          <template #default="{ row }">
            {{ formatDate(row.validFromDate) }}
          </template>
        </el-table-column>
        <el-table-column prop="validToDate" label="有效期结束" width="110">
          <template #default="{ row }">
            {{ formatDate(row.validToDate) }}
          </template>
        </el-table-column>
        <el-table-column prop="isValid" label="状态" width="80" align="center">
          <template #default="{ row }">
            <el-tag :type="row.isValid ? 'success' : 'info'" size="small">
              {{ row.isValid ? '有效' : '无效' }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="200" fixed="right">
          <template #default="{ row }">
            <el-button link type="primary" size="small" @click="handleView(row)">查看</el-button>
            <el-button link type="primary" size="small" @click="handleEdit(row)">编辑</el-button>
            <el-button link type="success" size="small" @click="handleValid(row)" v-if="!row.isValid">设为有效</el-button>
            <el-button link type="warning" size="small" @click="handleInvalid(row)" v-if="row.isValid">设为无效</el-button>
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
      width="650px"
      @close="handleDialogClose"
    >
      <el-form :model="formData" :rules="formRules" ref="formRef" label-width="130px">
        <el-divider content-position="left">基本信息</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="资格编码" prop="qualCode">
              <el-input v-model="formData.qualCode" placeholder="请输入资格编码" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="资格名称" prop="qualName">
              <el-input v-model="formData.qualName" placeholder="请输入资格名称" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="资格类型" prop="qualType">
              <el-select v-model="formData.qualType" placeholder="请选择类型">
                <el-option label="建造" value="建造" />
                <el-option label="造价" value="造价" />
                <el-option label="监理" value="监理" />
                <el-option label="其他" value="其他" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="资格等级" prop="qualLevel">
              <el-select v-model="formData.qualLevel" placeholder="请选择等级">
                <el-option label="初级" value="初级" />
                <el-option label="中级" value="中级" />
                <el-option label="高级" value="高级" />
                <el-option label="正高级" value="正高级" />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="资格类别">
          <el-input v-model="formData.qualCategory" placeholder="请输入资格类别" />
        </el-form-item>
        <el-form-item label="发证机构">
          <el-input v-model="formData.issuingAuthority" placeholder="请输入发证机构" />
        </el-form-item>

        <el-divider content-position="left">有效期</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="发证日期" prop="issueDate">
              <el-date-picker
                v-model="formData.issueDate"
                type="date"
                placeholder="选择发证日期"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="有效期开始" prop="validFromDate">
              <el-date-picker
                v-model="formData.validFromDate"
                type="date"
                placeholder="选择开始日期"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="有效期结束" prop="validToDate">
              <el-date-picker
                v-model="formData.validToDate"
                type="date"
                placeholder="选择结束日期"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </el-form-item>
        </el-row>
        <el-form-item label="描述">
          <el-input v-model="formData.description" type="textarea" :rows="3" placeholder="请输入描述" />
        </el-form-item>
      </el-form>

      <template #footer>
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSubmit" :loading="submitLoading">确定</el-button>
      </template>
    </el-dialog>

    <!-- View Dialog -->
    <el-dialog v-model="viewVisible" title="资格详情" width="550px">
      <el-descriptions :column="2" border v-if="currentRow">
        <el-descriptions-item label="资格ID">{{ currentRow.qualId }}</el-descriptions-item>
        <el-descriptions-item label="资格编码">{{ currentRow.qualCode }}</el-descriptions-item>
        <el-descriptions-item label="资格名称">{{ currentRow.qualName }}</el-descriptions-item>
        <el-descriptions-item label="资格类型">
          <el-tag :type="getQualTypeColor(currentRow.qualType)" size="small">
            {{ currentRow.qualType }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="资格等级">{{ currentRow.qualLevel }}</el-descriptions-item>
        <el-descriptions-item label="类别" :span="2">{{ currentRow.qualCategory }}</el-descriptions-item>
        <el-descriptions-item label="发证机构">{{ currentRow.issuingAuthority }}</el-descriptions-item>
        <el-descriptions-item label="发证日期">{{ formatDate(currentRow.issueDate) }}</el-descriptions-item>
        <el-descriptions-item label="有效期开始">{{ formatDate(currentRow.validFromDate) }}</el-descriptions-item>
        <el-descriptions-item label="有效期结束">{{ formatDate(currentRow.validToDate) }}</el-descriptions-item>
        <el-descriptions-item label="状态">
          <el-tag :type="currentRow.isValid ? 'success' : 'info'" size="small">
            {{ currentRow.isValid ? '有效' : '无效' }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="描述" :span="2">{{ currentRow.description || '-' }}</el-descriptions-item>
        <el-descriptions-item label="创建时间">{{ currentRow.createTime }}</el-descriptions-item>
        <el-descriptions-item label="修改时间">{{ currentRow.modifyTime }}</el-descriptions-item>
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
  getAllQualifications,
  getQualificationsByType,
  getQualificationsByLevel,
  searchQualifications,
  createQualification,
  updateQualification,
  deleteQualification,
  updateQualificationValidity,
  batchUpdateQualificationValidity
} from '@/api/qualification'

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
  qualName: '',
  qualCode: '',
  qualType: '',
  qualLevel: '',
  qualCategory: '',
  isValid: ''
})

// Dialog
const dialogVisible = ref(false)
const dialogTitle = ref('新建资格')
const isEdit = ref(false)
const formRef = ref(null)
const submitLoading = ref(false)

// View dialog
const viewVisible = ref(false)
const currentRow = ref(null)

// Form data
const formData = reactive({
  qualId: null,
  qualCode: '',
  qualName: '',
  qualType: '',
  qualLevel: '',
  qualCategory: '',
  issuingAuthority: '',
  issueDate: '',
  validFromDate: '',
  validToDate: '',
  description: '',
  isValid: true
})

// Form rules
const formRules = {
  qualCode: [{ required: true, message: '请输入资格编码', trigger: 'blur' }],
  qualName: [{ required: true, message: '请输入资格名称', trigger: 'blur' }],
  qualType: [{ required: true, message: '请选择资格类型', trigger: 'change' }],
  qualLevel: [{ required: true, message: '请选择资格等级', trigger: 'change' }]
}

// Load data
const loadQualifications = async () => {
  loading.value = true
  try {
    const params = {
      current: pagination.current,
      size: pagination.size,
      ...searchForm
    }
    const response = await getAllQualifications()
    if (response.code === 200) {
      const allData = response.data || []
      // Filter client-side
      tableData.value = filterData(allData, searchForm)
      pagination.total = tableData.value.length
    } else {
      ElMessage.error(response.message || '加载数据失败')
    }
  } catch (error) {
    ElMessage.error('加载数据失败: ' + error.message)
  } finally {
    loading.value = false
  }
}

// Filter data
const filterData = (data, filters) => {
  return data.filter(item => {
    if (filters.qualName && !item.qualName.includes(filters.qualName)) return false
    if (filters.qualCode && !item.qualCode.includes(filters.qualCode)) return false
    if (filters.qualType && !item.qualType.includes(filters.qualType)) return false
    if (filters.qualLevel && !item.qualLevel.includes(filters.qualLevel)) return false
    if (filters.qualCategory && !item.qualCategory.includes(filters.qualCategory)) return false
    if (filters.isValid !== '' && String(item.isValid) !== filters.isValid) return false
    return true
  })
}

// Search
const handleSearch = () => {
  pagination.current = 1
  loadQualifications()
}

// Reset
const handleReset = () => {
  searchForm.qualName = ''
  searchForm.qualCode = ''
  searchForm.qualType = ''
  searchForm.qualLevel = ''
  searchForm.qualCategory = ''
  searchForm.isValid = ''
  pagination.current = 1
  loadQualifications()
}

// Create
const handleCreate = () => {
  isEdit.value = false
  dialogTitle.value = '新建资格'
  resetForm()
  dialogVisible.value = true
}

// Edit
const handleEdit = (row) => {
  isEdit.value = true
  dialogTitle.value = '编辑资格'
  Object.assign(formData, row)
  dialogVisible.value = true
}

// View
const handleView = (row) => {
  currentRow.value = row
  viewVisible.value = true
}

// Valid
const handleValid = (row) => {
  ElMessageBox.confirm(`确定要将 ${row.qualName} 设为有效吗？`, '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await updateQualificationValidity(row.qualId, true)
      ElMessage.success('已设为有效')
      loadQualifications()
    } catch (error) {
      ElMessage.error('操作失败: ' + error.message)
    }
  }).catch(() => {})
}

// Invalid
const handleInvalid = (row) => {
  ElMessageBox.confirm(`确定要将 ${row.qualName} 设为无效吗？`, '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await updateQualificationValidity(row.qualId, false)
      ElMessage.success('已设为无效')
      loadQualifications()
    } catch (error) {
      ElMessage.error('操作失败: ' + error.message)
    }
  }).catch(() => {})
}

// Delete
const handleDelete = (row) => {
  ElMessageBox.confirm(`确定要删除 ${row.qualName} 吗？此操作不可恢复。`, '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await deleteQualification(row.qualId)
      ElMessage.success('删除成功')
      loadQualifications()
    } catch (error) {
      ElMessage.error('删除失败: ' + error.message)
    }
  }).catch(() => {})
}

// Submit
const handleSubmit = async () => {
  if (!formRef.value) return

  await formRef.value.validate(async (valid) => {
    if (!valid) return

    submitLoading.value = true
    try {
      if (isEdit.value) {
        await updateQualification(formData.qualId, formData)
        ElMessage.success('更新成功')
      } else {
        await createQualification(formData)
        ElMessage.success('创建成功')
      }
      dialogVisible.value = false
      loadQualifications()
    } catch (error) {
      ElMessage.error('操作失败: ' + error.message)
    } finally {
      submitLoading.value = false
    }
  })
}

// Pagination
const handleSizeChange = (val) => {
  pagination.size = val
  pagination.current = 1
  loadQualifications()
}

const handleCurrentChange = (val) => {
  pagination.current = val
  loadQualifications()
}

// Dialog close
const handleDialogClose = () => {
  formRef.value?.resetFields()
  resetForm()
}

// Reset form
const resetForm = () => {
  Object.assign(formData, {
    qualId: null,
    qualCode: '',
    qualName: '',
    qualType: '',
    qualLevel: '',
    qualCategory: '',
    issuingAuthority: '',
    issueDate: '',
    validFromDate: '',
    validToDate: '',
    description: '',
    isValid: true
  })
}

// Format date
const formatDate = (date) => {
  if (!date) return '-'
  return date
}

// Get qualification type color
const getQualTypeColor = (type) => {
  const colors = {
    '建造': 'primary',
    '造价': 'success',
    '监理': 'warning',
    '其他': 'info'
  }
  return colors[type] || 'info'
}

// Load data on mount
onMounted(() => {
  loadQualifications()
})
</script>

<style scoped>
.qualification-container {
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
