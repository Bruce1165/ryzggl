<template>
  <div class="learn-record-list">
    <el-card class="filter-card">
      <el-form :inline="true" :model="filters">
        <el-form-item label="人员证书编号">
          <el-input v-model="filters.workerCode" placeholder="请输入人员证书编号" clearable />
        </el-form-item>
        <el-form-item label="学习包ID">
          <el-input v-model="filters.packageId" placeholder="请输入学习包ID" clearable />
        </el-form-item>
        <el-form-item label="完成状态">
          <el-select v-model="filters.isCompleted" placeholder="请选择" clearable>
            <el-option label="已完成" :value="true" />
            <el-option label="未完成" :value="false" />
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
        <el-table-column prop="id" label="ID" width="80" />
        <el-table-column prop="workerCertificateCode" label="人员证书编号" width="180" />
        <el-table-column prop="packageId" label="学习包ID" width="120" />
        <el-table-column prop="startTime" label="开始时间" width="160" />
        <el-table-column prop="endTime" label="结束时间" width="160" />
        <el-table-column prop="totalHours" label="总学时" width="100" />
        <el-table-column prop="completedHours" label="完成学时" width="100" />
        <el-table-column label="完成状态" width="100">
          <template #default="{ row }">
            <el-tag :type="row.isCompleted ? 'success' : 'warning'">
              {{ row.isCompleted ? '已完成' : '未完成' }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="studyContent" label="学习内容" min-width="200" show-overflow-tooltip />
        <el-table-column label="操作" width="150" fixed="right">
          <template #default="{ row }">
            <el-button link type="primary" size="small" @click="handleView(row)">查看</el-button>
            <el-button link type="primary" size="small" @click="handleEdit(row)">编辑</el-button>
            <el-button link type="danger" size="small" @click="handleDelete(row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>
    </el-card>

    <!-- 新增/编辑对话框 -->
    <el-dialog
      v-model="dialogVisible"
      :title="isEdit ? '编辑学习记录' : '新增学习记录'"
      width="600px"
      @close="handleDialogClose"
    >
      <el-form :model="formData" :rules="formRules" ref="formRef" label-width="120px">
        <el-form-item label="人员证书编号" prop="workerCertificateCode">
          <el-input v-model="formData.workerCertificateCode" placeholder="请输入人员证书编号" />
        </el-form-item>
        <el-form-item label="学习包ID" prop="packageId">
          <el-input v-model="formData.packageId" placeholder="请输入学习包ID" />
        </el-form-item>
        <el-form-item label="开始时间" prop="startTime">
          <el-date-picker
            v-model="formData.startTime"
            type="datetime"
            placeholder="选择开始时间"
            style="width: 100%"
            format="YYYY-MM-DD HH:mm:ss"
            value-format="YYYY-MM-DD HH:mm:ss"
          />
        </el-form-item>
        <el-form-item label="结束时间" prop="endTime">
          <el-date-picker
            v-model="formData.endTime"
            type="datetime"
            placeholder="选择结束时间"
            style="width: 100%"
            format="YYYY-MM-DD HH:mm:ss"
            value-format="YYYY-MM-DD HH:mm:ss"
          />
        </el-form-item>
        <el-form-item label="总学时" prop="totalHours">
          <el-input-number v-model="formData.totalHours" :min="0" :precision="1" placeholder="总学时" />
        </el-form-item>
        <el-form-item label="完成学时" prop="completedHours">
          <el-input-number v-model="formData.completedHours" :min="0" :precision="1" placeholder="完成学时" />
        </el-form-item>
        <el-form-item label="完成状态" prop="isCompleted">
          <el-switch v-model="formData.isCompleted" />
        </el-form-item>
        <el-form-item label="学习内容" prop="studyContent">
          <el-input
            v-model="formData.studyContent"
            type="textarea"
            :rows="4"
            placeholder="请输入学习内容"
          />
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
import learnRecordApi from '@/api/learnRecord'

const loading = ref(false)
const dialogVisible = ref(false)
const isEdit = ref(false)
const formRef = ref(null)

const filters = reactive({
  workerCode: '',
  packageId: '',
  isCompleted: null
})

const tableData = ref([])
const formData = reactive({
  id: null,
  workerCertificateCode: '',
  packageId: '',
  startTime: '',
  endTime: '',
  totalHours: 0,
  completedHours: 0,
  isCompleted: false,
  studyContent: ''
})

const formRules = {
  workerCertificateCode: [{ required: true, message: '请输入人员证书编号', trigger: 'blur' }],
  packageId: [{ required: true, message: '请输入学习包ID', trigger: 'blur' }],
  startTime: [{ required: true, message: '请选择开始时间', trigger: 'change' }],
  endTime: [{ required: true, message: '请选择结束时间', trigger: 'change' }],
  totalHours: [{ required: true, message: '请输入总学时', trigger: 'blur' }]
}

const loadData = async () => {
  loading.value = true
  try {
    let result
    if (filters.workerCode) {
      result = await learnRecordApi.getByWorkerCode(filters.workerCode)
    } else if (filters.packageId) {
      result = await learnRecordApi.getByPackageId(filters.packageId)
    } else if (filters.isCompleted !== null) {
      result = await learnRecordApi.getByStatus(filters.isCompleted)
    } else {
      result = await learnRecordApi.getAll()
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
  filters.workerCode = ''
  filters.packageId = ''
  filters.isCompleted = null
  loadData()
}

const showCreateDialog = () => {
  isEdit.value = false
  Object.assign(formData, {
    id: null,
    workerCertificateCode: '',
    packageId: '',
    startTime: '',
    endTime: '',
    totalHours: 0,
    completedHours: 0,
    isCompleted: false,
    studyContent: ''
  })
  dialogVisible.value = true
}

const handleView = async (row) => {
  const result = await learnRecordApi.getById(row.id)
  if (result.code === 200) {
    Object.assign(formData, result.data)
    isEdit.value = false
    dialogVisible.value = true
  }
}

const handleEdit = async (row) => {
  const result = await learnRecordApi.getById(row.id)
  if (result.code === 200) {
    Object.assign(formData, result.data)
    isEdit.value = true
    dialogVisible.value = true
  }
}

const handleDelete = async (row) => {
  await ElMessageBox.confirm('确定要删除这条学习记录吗?', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  })
  const result = await learnRecordApi.delete(row.id)
  if (result.code === 200) {
    ElMessage.success('删除成功')
    loadData()
  } else {
    ElMessage.error(result.message || '删除失败')
  }
}

const handleSave = async () => {
  await formRef.value.validate()
  const data = { ...formData }
  delete data.id

  let result
  if (isEdit.value) {
    result = await learnRecordApi.update(formData.id, data)
  } else {
    result = await learnRecordApi.create(data)
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
.learn-record-list {
  padding: 20px;
}
.filter-card {
  margin-bottom: 20px;
}
</style>
