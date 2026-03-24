<template>
  <div class="study-plan-list">
    <el-card class="filter-card">
      <el-form :inline="true" :model="filters">
        <el-form-item label="人员证书编号">
          <el-input v-model="filters.workerCertificateCode" placeholder="请输入人员证书编号" clearable />
        </el-form-item>
        <el-form-item label="学习包ID">
          <el-input v-model="filters.packageId" placeholder="请输入学习包ID" clearable />
        </el-form-item>
        <el-form-item label="达标状态">
          <el-select v-model="filters.status" placeholder="请选择" clearable>
            <el-option label="未达标" value="未达标" />
            <el-option label="已达标" value="已达标" />
          </el-select>
        </el-form-item>
        <el-form-item label="添加类型">
          <el-select v-model="filters.addType" placeholder="请选择" clearable>
            <el-option label="个人添加" value="个人添加" />
            <el-option label="系统指派" value="系统指派" />
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
        <el-table-column prop="workerCertificateCode" label="人员证书编号" width="180" />
        <el-table-column prop="packageId" label="学习包ID" width="120" />
        <el-table-column prop="packageName" label="学习包名称" width="180" />
        <el-table-column prop="targetHours" label="目标学时" width="100" />
        <el-table-column prop="completedHours" label="完成学时" width="100" />
        <el-table-column prop="startTime" label="开始时间" width="160" />
        <el-table-column prop="endTime" label="结束时间" width="160" />
        <el-table-column label="达标状态" width="100">
          <template #default="{ row }">
            <el-tag :type="row.status === '已达标' ? 'success' : 'warning'">
              {{ row.status }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="addType" label="添加类型" width="100">
          <template #default="{ row }">
            <el-tag :type="row.addType === '系统指派' ? 'primary' : 'info'">
              {{ row.addType }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="remark" label="备注" min-width="150" show-overflow-tooltip />
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
      :title="isEdit ? '编辑学习计划' : '新增学习计划'"
      width="600px"
      @close="handleDialogClose"
    >
      <el-form :model="formData" :rules="formRules" ref="formRef" label-width="120px">
        <el-form-item label="人员证书编号" prop="workerCertificateCode">
          <el-input v-model="formData.workerCertificateCode" placeholder="请输入人员证书编号" :disabled="isEdit" />
        </el-form-item>
        <el-form-item label="学习包ID" prop="packageId">
          <el-input v-model="formData.packageId" placeholder="请输入学习包ID" :disabled="isEdit" />
        </el-form-item>
        <el-form-item label="学习包名称" prop="packageName">
          <el-input v-model="formData.packageName" placeholder="请输入学习包名称" />
        </el-form-item>
        <el-form-item label="目标学时" prop="targetHours">
          <el-input-number v-model="formData.targetHours" :min="0" :precision="1" placeholder="目标学时" />
        </el-form-item>
        <el-form-item label="完成学时" prop="completedHours">
          <el-input-number v-model="formData.completedHours" :min="0" :precision="1" placeholder="完成学时" />
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
        <el-form-item label="达标状态" prop="status">
          <el-select v-model="formData.status" placeholder="请选择" style="width: 100%">
            <el-option label="未达标" value="未达标" />
            <el-option label="已达标" value="已达标" />
          </el-select>
        </el-form-item>
        <el-form-item label="添加类型" prop="addType">
          <el-select v-model="formData.addType" placeholder="请选择" style="width: 100%">
            <el-option label="个人添加" value="个人添加" />
            <el-option label="系统指派" value="系统指派" />
          </el-select>
        </el-form-item>
        <el-form-item label="备注" prop="remark">
          <el-input v-model="formData.remark" type="textarea" :rows="3" placeholder="请输入备注" />
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
import studyPlanApi from '@/api/studyPlan'

const loading = ref(false)
const dialogVisible = ref(false)
const isEdit = ref(false)
const formRef = ref(null)

const filters = reactive({
  workerCertificateCode: '',
  packageId: '',
  status: '',
  addType: ''
})

const tableData = ref([])
const formData = reactive({
  workerCertificateCode: '',
  packageId: '',
  packageName: '',
  targetHours: 0,
  completedHours: 0,
  startTime: '',
  endTime: '',
  status: '未达标',
  addType: '个人添加',
  remark: ''
})

const formRules = {
  workerCertificateCode: [{ required: true, message: '请输入人员证书编号', trigger: 'blur' }],
  packageId: [{ required: true, message: '请输入学习包ID', trigger: 'blur' }],
  packageName: [{ required: true, message: '请输入学习包名称', trigger: 'blur' }],
  targetHours: [{ required: true, message: '请输入目标学时', trigger: 'blur' }],
  status: [{ required: true, message: '请选择达标状态', trigger: 'change' }],
  addType: [{ required: true, message: '请选择添加类型', trigger: 'change' }]
}

const loadData = async () => {
  loading.value = true
  try {
    let result
    if (filters.workerCertificateCode) {
      result = await studyPlanApi.getByWorkerCertificateCode(filters.workerCertificateCode)
    } else if (filters.packageId) {
      result = await studyPlanApi.getByPackageId(filters.packageId)
    } else if (filters.status) {
      result = await studyPlanApi.getByStatus(filters.status)
    } else if (filters.addType) {
      result = await studyPlanApi.getByAddType(filters.addType)
    } else {
      result = await studyPlanApi.getAll()
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
  filters.workerCertificateCode = ''
  filters.packageId = ''
  filters.status = ''
  filters.addType = ''
  loadData()
}

const showCreateDialog = () => {
  isEdit.value = false
  Object.assign(formData, {
    workerCertificateCode: '',
    packageId: '',
    packageName: '',
    targetHours: 0,
    completedHours: 0,
    startTime: '',
    endTime: '',
    status: '未达标',
    addType: '个人添加',
    remark: ''
  })
  dialogVisible.value = true
}

const handleView = async (row) => {
  const result = await studyPlanApi.getById(row.workerCertificateCode, row.packageId)
  if (result.code === 200) {
    Object.assign(formData, result.data)
    isEdit.value = false
    dialogVisible.value = true
  }
}

const handleEdit = async (row) => {
  const result = await studyPlanApi.getById(row.workerCertificateCode, row.packageId)
  if (result.code === 200) {
    Object.assign(formData, result.data)
    isEdit.value = true
    dialogVisible.value = true
  }
}

const handleDelete = async (row) => {
  await ElMessageBox.confirm('确定要删除这条学习计划吗?', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  })
  const result = await studyPlanApi.delete(row.workerCertificateCode, row.packageId)
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

  let result
  if (isEdit.value) {
    result = await studyPlanApi.update(formData.workerCertificateCode, formData.packageId, data)
  } else {
    result = await studyPlanApi.create(data)
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
.study-plan-list {
  padding: 20px;
}
.filter-card {
  margin-bottom: 20px;
}
</style>
