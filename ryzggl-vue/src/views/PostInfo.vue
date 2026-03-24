<template>
  <div class="post-info-container">
    <el-card>
      <!-- Header Actions -->
      <template #header>
        <div class="card-header">
          <span>岗位信息管理</span>
          <el-button type="primary" @click="handleCreate">新增岗位</el-button>
        </div>
      </template>

      <!-- Search Form -->
      <el-form :model="searchForm" :inline="true" class="search-form">
        <el-form-item label="岗位类型">
          <el-select v-model="searchForm.postType" placeholder="请选择岗位类型" clearable>
            <el-option label="专业" value="1" />
            <el-option label="工种" value="2" />
            <el-option label="技能等级" value="3" />
          </el-select>
        </el-form-item>
        <el-form-item label="岗位名称">
          <el-input v-model="searchForm.keyword" placeholder="请输入岗位名称" clearable />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSearch">搜索</el-button>
          <el-button @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>

      <!-- Post Table -->
      <el-table :data="tableData" border style="width: 100%" v-loading="loading">
        <el-table-column prop="postId" label="岗位ID" width="80" />
        <el-table-column prop="postType" label="岗位类型" width="100">
          <template #default="{ row }">
            <el-tag :type="getPostTypeColor(row.postType)" size="small">
              {{ getPostTypeText(row.postType) }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="postName" label="岗位名称" width="150" />
        <el-table-column prop="upPostId" label="上级岗位ID" width="120" />
        <el-table-column prop="examFee" label="考试费用" width="100" />
        <el-table-column prop="currentNumber" label="当前流水号" width="120" />
        <el-table-column prop="codeYear" label="编码年度" width="100" />
        <el-table-column prop="codeFormat" label="编号格式" min-width="200" show-overflow-tooltip />
        <el-table-column label="操作" width="180" fixed="right">
          <template #default="{ row }">
            <el-button link type="primary" size="small" @click="handleEdit(row)">编辑</el-button>
            <el-button link type="success" size="small" @click="handleNextCertNo(row)">获取编号</el-button>
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
      :title="isEdit ? '编辑岗位' : '新增岗位'"
      width="600px"
      @close="handleDialogClose"
    >
      <el-form :model="formData" :rules="formRules" ref="formRef" label-width="120px">
        <el-form-item label="岗位ID" prop="postId" v-if="isEdit">
          <el-input v-model="formData.postId" disabled />
        </el-form-item>
        <el-form-item label="岗位类型" prop="postType">
          <el-select v-model="formData.postType" placeholder="请选择岗位类型">
            <el-option label="专业" value="1" />
            <el-option label="工种" value="2" />
            <el-option label="技能等级" value="3" />
          </el-select>
        </el-form-item>
        <el-form-item label="岗位名称" prop="postName">
          <el-input v-model="formData.postName" placeholder="请输入岗位名称" />
        </el-form-item>
        <el-form-item label="上级岗位ID">
          <el-input v-model="formData.upPostId" type="number" placeholder="请输入上级岗位ID" />
        </el-form-item>
        <el-form-item label="考试费用">
          <el-input-number v-model="formData.examFee" :min="0" :precision="2" />
        </el-form-item>
        <el-form-item label="当前流水号">
          <el-input-number v-model="formData.currentNumber" :min="0" />
        </el-form-item>
        <el-form-item label="编码年度">
          <el-input-number v-model="formData.codeYear" :min="2000" :max="2100" />
        </el-form-item>
        <el-form-item label="编号格式">
          <el-input v-model="formData.codeFormat" placeholder="例如：京|Y,2|N,7" />
        </el-form-item>
      </el-form>

      <template #footer>
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSubmit" :loading="submitLoading">确定</el-button>
      </template>
    </el-dialog>

    <!-- Certificate Number Dialog -->
    <el-dialog
      v-model="certNoDialogVisible"
      title="获取证书编号"
      width="500px"
    >
      <el-form :model="certNoForm" ref="certNoFormRef" label-width="120px">
        <el-form-item label="岗位名称">
          <el-input v-model="certNoForm.postName" disabled />
        </el-form-item>
        <el-form-item label="证书编号">
          <el-input v-model="certNoForm.certNo" readonly>
            <template #append>
              <el-button type="primary" @click="handleGenerateCertNo" :loading="generateLoading">生成</el-button>
            </template>
          </el-input>
        </el-form-item>
        <el-form-item label="更新流水号">
          <el-switch v-model="certNoForm.updateCurrent" />
        </el-form-item>
        <el-alert
          title="提示"
          type="info"
          :closable="false"
          style="margin-top: 10px"
        >
          编号格式规则：Y=年份, N=流水号, YN=年度流水, YPN=年度流水(父级), Level=技能等级, TrainCode=培训点编码
        </el-alert>
      </el-form>

      <template #footer>
        <el-button @click="certNoDialogVisible = false">关闭</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import {
  getPosts,
  createPost,
  updatePost,
  deletePost,
  getNextCertificateNo,
  updateNextCertificateNo
} from '@/api/postInfo'

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
  postType: null,
  keyword: ''
})

// Dialog
const dialogVisible = ref(false)
const isEdit = ref(false)
const formRef = ref(null)
const submitLoading = ref(false)

// Form data
const formData = reactive({
  postId: null,
  postType: '1',
  postName: '',
  upPostId: null,
  examFee: 0,
  currentNumber: 0,
  codeYear: new Date().getFullYear(),
  codeFormat: ''
})

// Certificate number dialog
const certNoDialogVisible = ref(false)
const certNoFormRef = ref(null)
const generateLoading = ref(false)
const currentPost = ref(null)
const certNoForm = reactive({
  postName: '',
  certNo: '',
  updateCurrent: true
})

// Form rules
const formRules = {
  postType: [{ required: true, message: '请选择岗位类型', trigger: 'change' }],
  postName: [{ required: true, message: '请输入岗位名称', trigger: 'blur' }]
}

// Load posts
const loadPosts = async () => {
  loading.value = true
  try {
    const params = {
      current: pagination.current,
      size: pagination.size
    }
    if (searchForm.postType) {
      params.postType = searchForm.postType
    }
    if (searchForm.keyword) {
      params.keyword = searchForm.keyword
    }
    const response = await getPosts(params)
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
  loadPosts()
}

// Reset
const handleReset = () => {
  searchForm.postType = null
  searchForm.keyword = ''
  pagination.current = 1
  loadPosts()
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
    postId: row.postId,
    postType: row.postType,
    postName: row.postName,
    upPostId: row.upPostId,
    examFee: row.examFee,
    currentNumber: row.currentNumber,
    codeYear: row.codeYear,
    codeFormat: row.codeFormat
  })
  dialogVisible.value = true
}

// Delete
const handleDelete = (row) => {
  ElMessageBox.confirm('确定要删除该岗位吗？此操作不可恢复。', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await deletePost(row.postId)
      ElMessage.success('删除成功')
      loadPosts()
    } catch (error) {
      ElMessage.error('删除失败: ' + error.message)
    }
  }).catch(() => {})
}

// Next certificate number
const handleNextCertNo = (row) => {
  currentPost.value = row
  certNoForm.postName = row.postName
  certNoForm.certNo = ''
  certNoDialogVisible.value = true
}

// Generate certificate number
const handleGenerateCertNo = async () => {
  if (!currentPost.value) return

  generateLoading.value = true
  try {
    let response
    if (certNoForm.updateCurrent) {
      response = await updateNextCertificateNo(currentPost.value.postId)
    } else {
      response = await getNextCertificateNo(currentPost.value.postId)
    }

    if (response.code === 200) {
      certNoForm.certNo = response.data.certNo
      ElMessage.success('证书编号生成成功')
    } else {
      ElMessage.error(response.message || '生成失败')
    }
  } catch (error) {
    ElMessage.error('生成失败: ' + error.message)
  } finally {
    generateLoading.value = false
  }
}

// Submit
const handleSubmit = async () => {
  if (!formRef.value) return

  await formRef.value.validate(async (valid) => {
    if (!valid) return

    submitLoading.value = true
    try {
      if (isEdit.value) {
        await updatePost(formData.postId, formData)
        ElMessage.success('更新成功')
      } else {
        await createPost(formData)
        ElMessage.success('创建成功')
      }
      dialogVisible.value = false
      loadPosts()
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
  loadPosts()
}

const handleCurrentChange = (val) => {
  pagination.current = val
  loadPosts()
}

// Dialog close
const handleDialogClose = () => {
  formRef.value?.resetFields()
  resetForm()
}

// Reset form
const resetForm = () => {
  Object.assign(formData, {
    postId: null,
    postType: '1',
    postName: '',
    upPostId: null,
    examFee: 0,
    currentNumber: 0,
    codeYear: new Date().getFullYear(),
    codeFormat: ''
  })
}

// Get post type color
const getPostTypeColor = (type) => {
  const colors = {
    '1': 'primary',
    '2': 'success',
    '3': 'warning'
  }
  return colors[type] || 'info'
}

// Get post type text
const getPostTypeText = (type) => {
  const texts = {
    '1': '专业',
    '2': '工种',
    '3': '技能等级'
  }
  return texts[type] || type
}

// Load data on mount
onMounted(() => {
  loadPosts()
})
</script>

<style scoped>
.post-info-container {
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
