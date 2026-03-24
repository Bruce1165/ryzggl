<template>
  <div class="train-unit-list">
    <el-card class="filter-card">
      <el-form :inline="true" :model="filters">
        <el-form-item label="单位名称">
          <el-input v-model="filters.unitName" placeholder="请输入单位名称" clearable />
        </el-form-item>
        <el-form-item label="使用状态">
          <el-select v-model="filters.useStatus" placeholder="请选择" clearable>
            <el-option label="启用" value="启用" />
            <el-option label="停用" value="停用" />
          </el-select>
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSearch">查询</el-button>
          <el-button @click="handleReset">重置</el-button>
          <el-button type="primary" @click="showCreateDialog">新增</el-button>
        </el-form-item>
      </el-form>
    </el-card>

    <el-card>
      <el-table :data="tableData" border v-loading="loading">
        <el-table-column type="index" label="序号" width="60" />
        <el-table-column prop="unitId" label="单位ID" width="100" />
        <el-table-column prop="unitCode" label="单位编码" width="120" />
        <el-table-column prop="unitName" label="单位名称" width="200" />
        <el-table-column prop="unitAddress" label="单位地址" min-width="200" show-overflow-tooltip />
        <el-table-column prop="contactPerson" label="联系人" width="120" />
        <el-table-column prop="contactPhone" label="联系电话" width="130" />
        <el-table-column label="使用状态" width="100">
          <template #default="{ row }">
            <el-tag :type="row.useStatus === '启用' ? 'success' : 'danger'">
              {{ row.useStatus }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column label="操作" width="200" fixed="right">
          <template #default="{ row }">
            <el-button link type="primary" size="small" @click="handleView(row)">查看</el-button>
            <el-button link type="primary" size="small" @click="handleEdit(row)">编辑</el-button>
            <el-button
              v-if="row.useStatus === '停用'"
              link
              type="success"
              size="small"
              @click="handleActivate(row)"
            >
              启用
            </el-button>
            <el-button
              v-else
              link
              type="warning"
              size="small"
              @click="handleDeactivate(row)"
            >
              停用
            </el-button>
            <el-button link type="danger" size="small" @click="handleDelete(row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
    </el-card>

    <!-- 新增/编辑对话框 -->
    <el-dialog
      v-model="dialogVisible"
      :title="isEdit ? '编辑培训单位' : '新增培训单位'"
      width="600px"
      @close="handleDialogClose"
    >
      <el-form :model="formData" :rules="formRules" ref="formRef" label-width="100px">
        <el-form-item label="单位编码" prop="unitCode">
          <el-input v-model="formData.unitCode" placeholder="请输入单位编码" />
        </el-form-item>
        <el-form-item label="单位名称" prop="unitName">
          <el-input v-model="formData.unitName" placeholder="请输入单位名称" />
        </el-form-item>
        <el-form-item label="单位地址" prop="unitAddress">
          <el-input v-model="formData.unitAddress" type="textarea" :rows="2" placeholder="请输入单位地址" />
        </el-form-item>
        <el-form-item label="联系人" prop="contactPerson">
          <el-input v-model="formData.contactPerson" placeholder="请输入联系人" />
        </el-form-item>
        <el-form-item label="联系电话" prop="contactPhone">
          <el-input v-model="formData.contactPhone" placeholder="请输入联系电话" />
        </el-form-item>
        <el-form-item label="使用状态" prop="useStatus">
          <el-select v-model="formData.useStatus" placeholder="请选择" style="width: 100%">
            <el-option label="启用" value="启用" />
            <el-option label="停用" value="停用" />
          </el-select>
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
import trainUnitApi from '@/api/trainUnit'

const loading = ref(false)
const dialogVisible = ref(false)
const isEdit = ref(false)
const formRef = ref(null)

const filters = reactive({
  unitName: '',
  useStatus: ''
})

const tableData = ref([])
const formData = reactive({
  unitId: null,
  unitCode: '',
  unitName: '',
  unitAddress: '',
  contactPerson: '',
  contactPhone: '',
  useStatus: '启用'
})

const formRules = {
  unitCode: [{ required: true, message: '请输入单位编码', trigger: 'blur' }],
  unitName: [{ required: true, message: '请输入单位名称', trigger: 'blur' }],
  contactPhone: [
    { required: true, message: '请输入联系电话', trigger: 'blur' },
    { pattern: /^1[3-9]\d{9}$/, message: '请输入正确的手机号码', trigger: 'blur' }
  ],
  useStatus: [{ required: true, message: '请选择使用状态', trigger: 'change' }]
}

const loadData = async () => {
  loading.value = true
  try {
    let result
    if (filters.useStatus === '启用') {
      result = await trainUnitApi.getActive()
    } else {
      result = await trainUnitApi.getAll()
    }
    if (result.code === 200) {
      let data = result.data
      if (filters.unitName) {
        data = data.filter(item => item.unitName.includes(filters.unitName))
      }
      tableData.value = data
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
  filters.unitName = ''
  filters.useStatus = ''
  loadData()
}

const showCreateDialog = () => {
  isEdit.value = false
  Object.assign(formData, {
    unitId: null,
    unitCode: '',
    unitName: '',
    unitAddress: '',
    contactPerson: '',
    contactPhone: '',
    useStatus: '启用'
  })
  dialogVisible.value = true
}

const handleView = async (row) => {
  const result = await trainUnitApi.getById(row.unitId)
  if (result.code === 200) {
    Object.assign(formData, result.data)
    isEdit.value = false
    dialogVisible.value = true
  }
}

const handleEdit = async (row) => {
  const result = await trainUnitApi.getById(row.unitId)
  if (result.code === 200) {
    Object.assign(formData, result.data)
    isEdit.value = true
    dialogVisible.value = true
  }
}

const handleDelete = async (row) => {
  await ElMessageBox.confirm('确定要删除这个培训单位吗?', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  })
  const result = await trainUnitApi.delete(row.unitId)
  if (result.code === 200) {
    ElMessage.success('删除成功')
    loadData()
  } else {
    ElMessage.error(result.message || '删除失败')
  }
}

const handleActivate = async (row) => {
  const result = await trainUnitApi.activate(row.unitId)
  if (result.code === 200) {
    ElMessage.success('启用成功')
    loadData()
  } else {
    ElMessage.error(result.message || '启用失败')
  }
}

const handleDeactivate = async (row) => {
  const result = await trainUnitApi.deactivate(row.unitId)
  if (result.code === 200) {
    ElMessage.success('停用成功')
    loadData()
  } else {
    ElMessage.error(result.message || '停用失败')
  }
}

const handleSave = async () => {
  await formRef.value.validate()
  const data = { ...formData }
  delete data.unitId

  let result
  if (isEdit.value) {
    result = await trainUnitApi.update(formData.unitId, data)
  } else {
    result = await trainUnitApi.create(data)
  }

  if (result.code === 200) {
    ElMessage.success(isEdit.value ? '更新成功' : '创建成功')
    dialogVisible.value = false
    loadData()
  } else {
    ElMessage.error(result.message || '操作失败')
  }
}

const handleDialogClose = () => {
  formRef.value?.resetFields()
}

onMounted(() => {
  loadData()
})
</script>

<style scoped>
.train-unit-list {
  padding: 20px;
}
.filter-card {
  margin-bottom: 20px;
}
</style>
