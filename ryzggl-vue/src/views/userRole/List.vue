<template>
  <div class="user-role-list">
    <el-card class="filter-card">
      <el-form :inline="true" :model="filters">
        <el-form-item label="用户ID">
          <el-input v-model="filters.userId" placeholder="请输入用户ID" clearable />
        </el-form-item>
        <el-form-item label="角色ID">
          <el-input v-model="filters.roleId" placeholder="请输入角色ID" clearable />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSearch">查询</el-button>
          <el-button @click="handleReset">重置</el-button>
          <el-button type="primary" @click="showCreateDialog">新增</el-button>
        </el-form-item>
      </el-form>
    </el-card>

    <el-card>
      <el-table :data="tableData" border v-loading="loading" @selection-change="handleSelectionChange">
        <el-table-column type="selection" width="55" />
        <el-table-column type="index" label="序号" width="60" />
        <el-table-column prop="id" label="ID" width="80" />
        <el-table-column prop="userId" label="用户ID" width="150" />
        <el-table-column prop="roleId" label="角色ID" width="150" />
        <el-table-column label="操作" width="200" fixed="right">
          <template #default="{ row }">
            <el-button link type="primary" size="small" @click="handleView(row)">查看</el-button>
            <el-button link type="primary" size="small" @click="handleEdit(row)">编辑</el-button>
            <el-button link type="danger" size="small" @click="handleDelete(row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>

      <div class="action-bar" v-if="selectedRows.length > 0">
        <span>已选择 {{ selectedRows.length }} 项</span>
        <el-button type="danger" size="small" @click="handleBatchDelete">批量删除</el-button>
        <el-button type="primary" size="small" @click="handleClearSelection">清空选择</el-button>
      </div>
    </el-card>

    <!-- 新增/编辑对话框 -->
    <el-dialog
      v-model="dialogVisible"
      :title="isEdit ? '编辑用户角色' : '新增用户角色'"
      width="500px"
      @close="handleDialogClose"
    >
      <el-form :model="formData" :rules="formRules" ref="formRef" label-width="80px">
        <el-form-item label="用户ID" prop="userId">
          <el-input v-model="formData.userId" placeholder="请输入用户ID" />
        </el-form-item>
        <el-form-item label="角色ID" prop="roleId">
          <el-input v-model="formData.roleId" placeholder="请输入角色ID" />
        </el-form-item>
        <el-form-item v-if="!isEdit" label="检查角色">
          <el-button type="primary" @click="checkUserRole" :loading="checkLoading">
            检查用户是否已有此角色
          </el-button>
          <el-tag v-if="hasRoleCheck !== null" :type="hasRoleCheck ? 'success' : 'warning'" style="margin-left: 10px">
            {{ hasRoleCheck ? '已有此角色' : '无此角色' }}
          </el-tag>
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSave">确定</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import userRoleApi from '@/api/userRole'

const loading = ref(false)
const dialogVisible = ref(false)
const isEdit = ref(false)
const formRef = ref(null)
const checkLoading = ref(false)
const hasRoleCheck = ref(null)

const filters = reactive({
  userId: '',
  roleId: ''
})

const tableData = ref([])
const selectedRows = ref([])
const formData = reactive({
  id: null,
  userId: '',
  roleId: ''
})

const formRules = {
  userId: [{ required: true, message: '请输入用户ID', trigger: 'blur' }],
  roleId: [{ required: true, message: '请输入角色ID', trigger: 'blur' }]
}

const loadData = async () => {
  loading.value = true
  try {
    let result
    if (filters.userId) {
      result = await userRoleApi.getByUserId(filters.userId)
    } else if (filters.roleId) {
      result = await userRoleApi.getByRoleId(filters.roleId)
    } else {
      result = await userRoleApi.getAll()
    }
    if (result.code === 200) {
      tableData.value = result.data
    }
  } catch (error) {
    ElMessage.error('加载数据失败')
  } finally {
    loading.value = false
  }
}

const handleSearch = () => {
  loadData()
}

const handleReset = () => {
  filters.userId = ''
  filters.roleId = ''
  loadData()
}

const handleSelectionChange = (selection) => {
  selectedRows.value = selection
}

const handleClearSelection = () => {
  selectedRows.value = []
}

const showCreateDialog = () => {
  isEdit.value = false
  hasRoleCheck.value = null
  Object.assign(formData, {
    id: null,
    userId: '',
    roleId: ''
  })
  dialogVisible.value = true
}

const handleView = async (row) => {
  const result = await userRoleApi.getById(row.id)
  if (result.code === 200) {
    Object.assign(formData, result.data)
    isEdit.value = false
    dialogVisible.value = true
  }
}

const handleEdit = async (row) => {
  const result = await userRoleApi.getById(row.id)
  if (result.code === 200) {
    Object.assign(formData, result.data)
    isEdit.value = true
    dialogVisible.value = true
  }
}

const handleDelete = async (row) => {
  await ElMessageBox.confirm('确定要删除这个用户角色关联吗?', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  })
  const result = await userRoleApi.delete(row.id)
  if (result.code === 200) {
    ElMessage.success('删除成功')
    loadData()
  } else {
    ElMessage.error(result.message || '删除失败')
  }
}

const handleBatchDelete = async () => {
  await ElMessageBox.confirm(`确定要删除选中的 ${selectedRows.value.length} 个用户角色关联吗?`, '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  })

  const promises = selectedRows.value.map(row => userRoleApi.delete(row.id))
  const results = await Promise.allSettled(promises)

  const successCount = results.filter(r => r.status === 'fulfilled' && r.value.code === 200).length
  const failCount = results.length - successCount

  if (successCount > 0) {
    ElMessage.success(`成功删除 ${successCount} 条记录`)
  }
  if (failCount > 0) {
    ElMessage.warning(`有 ${failCount} 条记录删除失败`)
  }

  loadData()
  selectedRows.value = []
}

const checkUserRole = async () => {
  if (!formData.userId || !formData.roleId) {
    ElMessage.warning('请先填写用户ID和角色ID')
    return
  }

  checkLoading.value = true
  try {
    const result = await userRoleApi.hasRole(formData.userId, formData.roleId)
    if (result.code === 200) {
      hasRoleCheck.value = result.data
    }
  } catch (error) {
    ElMessage.error('检查失败')
  } finally {
    checkLoading.value = false
  }
}

const handleSave = async () => {
  await formRef.value.validate()
  const data = { ...formData }
  delete data.id

  const result = await userRoleApi.create(data)
  if (result.code === 200) {
    ElMessage.success('创建成功')
    dialogVisible.value = false
    loadData()
  } else {
    ElMessage.error(result.message || '操作失败')
  }
}

const handleDialogClose = () => {
  formRef.value?.resetFields()
  hasRoleCheck.value = null
}

onMounted(() => {
  loadData()
})
</script>

<style scoped>
.user-role-list {
  padding: 20px;
}
.filter-card {
  margin-bottom: 20px;
}
.action-bar {
  margin-top: 20px;
  padding: 10px;
  background-color: #f5f7fa;
  border-radius: 4px;
  display: flex;
  align-items: center;
  gap: 10px;
}
</style>
