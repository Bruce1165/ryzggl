<template>
  <div class="dictionary-container">
    <el-card>
      <!-- Header Actions -->
      <template #header>
        <div class="card-header">
          <span>数据字典管理</span>
          <el-button type="primary" @click="handleCreate">新增字典</el-button>
        </div>
      </template>

      <!-- Search Form -->
      <el-form :model="searchForm" :inline="true" class="search-form">
        <el-form-item label="类型ID">
          <el-input v-model="searchForm.typeId" placeholder="请输入类型ID" clearable type="number" />
        </el-form-item>
        <el-form-item label="字典名称">
          <el-input v-model="searchForm.dicName" placeholder="请输入字典名称" clearable />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSearch">搜索</el-button>
          <el-button @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>

      <!-- Dictionary Table -->
      <el-table :data="tableData" border style="width: 100%" v-loading="loading" @selection-change="handleSelectionChange">
        <el-table-column type="selection" width="55" />
        <el-table-column prop="dicId" label="字典ID" width="120" />
        <el-table-column prop="typeId" label="类型ID" width="80" />
        <el-table-column prop="typeName" label="类型名称" width="120" />
        <el-table-column prop="dicName" label="字典名称" width="150" />
        <el-table-column prop="orderId" label="排序" width="80" />
        <el-table-column prop="dicDesc" label="描述" min-width="200" show-overflow-tooltip />
        <el-table-column prop="category" label="分类" width="80" />
        <el-table-column label="操作" width="180" fixed="right">
          <template #default="{ row }">
            <el-button link type="primary" size="small" @click="handleEdit(row)">编辑</el-button>
            <el-button link type="danger" size="small" @click="handleDelete(row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>

      <!-- Batch Actions -->
      <div v-if="selectedRows.length > 0" class="batch-actions">
        <el-alert :title="`已选择 ${selectedRows.length} 项`" type="info" show-icon :closable="false" />
        <el-button type="danger" @click="handleBatchDelete">批量删除</el-button>
      </div>

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
      :title="isEdit ? '编辑字典' : '新增字典'"
      width="600px"
      @close="handleDialogClose"
    >
      <el-form :model="formData" :rules="formRules" ref="formRef" label-width="120px">
        <el-form-item label="字典ID" prop="dicId" v-if="isEdit">
          <el-input v-model="formData.dicId" disabled />
        </el-form-item>
        <el-form-item label="类型ID" prop="typeId">
          <el-input v-model="formData.typeId" placeholder="请输入类型ID" type="number" />
        </el-form-item>
        <el-form-item label="类型名称" prop="typeName">
          <el-input v-model="formData.typeName" placeholder="请输入类型名称" />
        </el-form-item>
        <el-form-item label="字典名称" prop="dicName">
          <el-input v-model="formData.dicName" placeholder="请输入字典名称" />
        </el-form-item>
        <el-form-item label="排序值" prop="orderId">
          <el-input-number v-model="formData.orderId" :min="0" />
        </el-form-item>
        <el-form-item label="描述">
          <el-input v-model="formData.dicDesc" type="textarea" :rows="3" placeholder="请输入描述" />
        </el-form-item>
        <el-form-item label="分类">
          <el-input v-model="formData.category" type="number" placeholder="请输入分类" />
        </el-form-item>
      </el-form>

      <template #footer>
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSubmit" :loading="submitLoading">确定</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import {
  getAllDictionaries,
  searchDictionariesByType,
  createDictionary,
  updateDictionary,
  deleteDictionary,
  batchDeleteDictionaries
} from '@/api/dictionary'

// Table data
const tableData = ref([])
const loading = ref(false)
const selectedRows = ref([])

// Pagination
const pagination = reactive({
  current: 1,
  size: 10,
  total: 0
})

// Search form
const searchForm = reactive({
  typeId: null,
  dicName: ''
})

// Dialog
const dialogVisible = ref(false)
const isEdit = ref(false)
const formRef = ref(null)
const submitLoading = ref(false)

// Form data
const formData = reactive({
  dicId: '',
  typeId: null,
  typeName: '',
  dicName: '',
  orderId: 0,
  dicDesc: '',
  category: null
})

// Form rules
const formRules = {
  typeId: [{ required: true, message: '请输入类型ID', trigger: 'blur' }],
  typeName: [{ required: true, message: '请输入类型名称', trigger: 'blur' }],
  dicName: [{ required: true, message: '请输入字典名称', trigger: 'blur' }]
}

// Load dictionaries
const loadDictionaries = async () => {
  loading.value = true
  try {
    const params = {}
    if (searchForm.typeId) {
      params.typeId = searchForm.typeId
    }
    if (searchForm.dicName) {
      params.dicName = searchForm.dicName
    }

    let response
    if (searchForm.typeId || searchForm.dicName) {
      response = await searchDictionariesByType(params)
    } else {
      response = await getAllDictionaries()
    }

    if (response.code === 200) {
      const allData = response.data || []
      // Client-side pagination
      const start = (pagination.current - 1) * pagination.size
      const end = start + pagination.size
      tableData.value = allData.slice(start, end)
      pagination.total = allData.length
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
  loadDictionaries()
}

// Reset
const handleReset = () => {
  searchForm.typeId = null
  searchForm.dicName = ''
  pagination.current = 1
  loadDictionaries()
}

// Create
const handleCreate = () => {
  isEdit.value = false
  resetForm()
  dialogVisible.value = true
}

// Edit
const handleEdit = (row) => {
  isEdit.value = true
  Object.assign(formData, {
    dicId: row.dicId,
    typeId: row.typeId,
    typeName: row.typeName,
    dicName: row.dicName,
    orderId: row.orderId,
    dicDesc: row.dicDesc,
    category: row.category
  })
  dialogVisible.value = true
}

// Delete
const handleDelete = (row) => {
  ElMessageBox.confirm('确定要删除该字典吗？此操作不可恢复。', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await deleteDictionary(row.dicId)
      ElMessage.success('删除成功')
      loadDictionaries()
    } catch (error) {
      ElMessage.error('删除失败: ' + error.message)
    }
  }).catch(() => {})
}

// Selection change
const handleSelectionChange = (selection) => {
  selectedRows.value = selection
}

// Batch delete
const handleBatchDelete = () => {
  ElMessageBox.confirm(`确定要删除选中的 ${selectedRows.value.length} 项吗？此操作不可恢复。`, '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      const dicIds = selectedRows.value.map(row => row.dicId)
      await batchDeleteDictionaries(dicIds)
      ElMessage.success('批量删除成功')
      selectedRows.value = []
      loadDictionaries()
    } catch (error) {
      ElMessage.error('批量删除失败: ' + error.message)
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
        await updateDictionary(formData.dicId, formData)
        ElMessage.success('更新成功')
      } else {
        await createDictionary(formData)
        ElMessage.success('创建成功')
      }
      dialogVisible.value = false
      loadDictionaries()
    } catch (error) {
      ElMessage.error((isEdit.value ? '更新' : '创建') + '失败: ' + error.message)
    } finally {
      submitLoading.value = false
    }
  })
}

// Pagination
const handleSizeChange = (val) => {
  pagination.size = val
  pagination.current = 1
  loadDictionaries()
}

const handleCurrentChange = (val) => {
  pagination.current = val
  loadDictionaries()
}

// Dialog close
const handleDialogClose = () => {
  formRef.value?.resetFields()
  resetForm()
}

// Reset form
const resetForm = () => {
  Object.assign(formData, {
    dicId: '',
    typeId: null,
    typeName: '',
    dicName: '',
    orderId: 0,
    dicDesc: '',
    category: null
  })
}

// Load data on mount
onMounted(() => {
  loadDictionaries()
})
</script>

<style scoped>
.dictionary-container {
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

.batch-actions {
  margin-top: 15px;
  display: flex;
  align-items: center;
  gap: 15px;
}

:deep(.el-pagination) {
  display: flex;
}
</style>
