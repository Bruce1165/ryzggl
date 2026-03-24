<template>
  <div class="organization-container">
    <el-card>
      <!-- Header Actions -->
      <template #header>
        <div class="card-header">
          <span>机构管理</span>
          <el-space>
            <el-button type="success" @click="loadTree">刷新树</el-button>
            <el-button type="primary" @click="handleCreate">新建机构</el-button>
          </el-space>
        </div>
      </template>

      <!-- Layout with Tree and Table -->
      <el-row :gutter="20">
        <!-- Organization Tree -->
        <el-col :span="8">
          <el-card shadow="never">
            <template #header>
              <div class="card-header">
                <span>机构树</span>
              </div>
            </template>
            <el-tree
              ref="treeRef"
              :data="treeData"
              :props="treeProps"
              node-key="organId"
              :expand-on-click-node="true"
              :highlight-current="true"
              @node-click="handleNodeClick"
              class="organization-tree"
            >
              <template #default="{ node, data }">
                <span class="tree-node">
                  <el-tag v-if="data.organType" :type="getOrganTypeColor(data.organType)" size="small">
                    {{ data.organType }}
                  </el-tag>
                  <span class="node-name">{{ data.organName }}</span>
                  <span class="node-code">({{ data.organCoding }})</span>
                </span>
              </template>
            </el-tree>
          </el-card>
        </el-col>

        <!-- Organization Table -->
        <el-col :span="16">
          <el-card shadow="never">
            <template #header>
              <div class="card-header">
                <span>机构列表 - {{ currentSelection ? currentSelection.organName : '全部' }}</span>
                <el-button link type="primary" size="small" @click="handleViewTree">查看树</el-button>
              </div>
            </template>

            <!-- Search Form -->
            <el-form :model="searchForm" :inline="true" class="search-form">
              <el-form-item label="机构名称">
                <el-input v-model="searchForm.organName" placeholder="请输入机构名称" clearable />
              </el-form-item>
              <el-form-item label="机构编码">
                <el-input v-model="searchForm.organCoding" placeholder="请输入机构编码" clearable />
              </el-form-item>
              <el-form-item>
                <el-button type="primary" @click="handleSearch">搜索</el-button>
                <el-button @click="handleReset">重置</el-button>
              </el-form-item>
            </el-form>

            <!-- Organization Table -->
            <el-table :data="tableData" border style="width: 100%" v-loading="loading">
              <el-table-column prop="organId" label="ID" width="80" />
              <el-table-column prop="organCoding" label="机构编码" width="120" />
              <el-table-column prop="organName" label="机构名称" min-width="180" show-overflow-tooltip />
              <el-table-column prop="organType" label="机构类型" width="100">
                <template #default="{ row }">
                  <el-tag :type="getOrganTypeColor(row.organType)" size="small">
                    {{ row.organType }}
                  </el-tag>
                </template>
              </el-table-column>
              <el-table-column prop="organNature" label="机构性质" width="100" />
              <el-table-column prop="organTelphone" label="联系电话" width="130" />
              <el-table-column prop="organAddress" label="地址" min-width="150" show-overflow-tooltip />
              <el-table-column prop="isVisible" label="状态" width="80" align="center">
                <template #default="{ row }">
                  <el-tag :type="row.isVisible ? 'success' : 'info'" size="small">
                    {{ row.isVisible ? '可见' : '隐藏' }}
                  </el-tag>
                </template>
              </el-table-column>
              <el-table-column label="操作" width="180" fixed="right">
                <template #default="{ row }">
                  <el-button link type="primary" size="small" @click="handleView(row)">查看</el-button>
                  <el-button link type="primary" size="small" @click="handleEdit(row)">编辑</el-button>
                  <el-button link type="success" size="small" @click="handleShow(row)" v-if="!row.isVisible">显示</el-button>
                  <el-button link type="warning" size="small" @click="handleHide(row)" v-if="row.isVisible">隐藏</el-button>
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
        </el-col>
      </el-row>
    </el-card>

    <!-- Create/Edit Dialog -->
    <el-dialog
      v-model="dialogVisible"
      :title="dialogTitle"
      width="600px"
      @close="handleDialogClose"
    >
      <el-form :model="formData" :rules="formRules" ref="formRef" label-width="120px">
        <el-divider content-position="left">基本信息</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="机构编码" prop="organCoding">
              <el-input v-model="formData.organCoding" placeholder="请输入机构编码（4位父级/6位子级）" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="排序" prop="orderId">
              <el-input-number v-model="formData.orderId" :min="1" :max="999" style="width: 100%" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="机构类型" prop="organType">
              <el-input v-model="formData.organType" placeholder="请输入机构类型" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="机构性质" prop="organNature">
              <el-input v-model="formData.organNature" placeholder="请输入机构性质" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="机构名称" prop="organName">
          <el-input v-model="formData.organName" placeholder="请输入机构名称" />
        </el-form-item>
        <el-form-item label="机构描述">
          <el-input v-model="formData.organDescription" type="textarea" :rows="2" placeholder="请输入机构描述" />
        </el-form-item>
        <el-form-item label="业务属性">
          <el-input v-model="formData.businessProperties" placeholder="请输入业务属性" />
        </el-form-item>

        <el-divider content-position="left">联系信息</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="联系电话" prop="organTelphone">
              <el-input v-model="formData.organTelphone" placeholder="请输入联系电话" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="机构代码" prop="organCode">
              <el-input v-model="formData.organCode" placeholder="请输入机构代码" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="机构地址">
          <el-input v-model="formData.organAddress" placeholder="请输入机构地址" />
        </el-form-item>
        <el-form-item label="是否可见">
          <el-switch v-model="formData.isVisible" />
        </el-form-item>
      </el-form>

      <template #footer>
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSubmit" :loading="submitLoading">确定</el-button>
      </template>
    </el-dialog>

    <!-- View Dialog -->
    <el-dialog v-model="viewVisible" title="机构详情" width="500px">
      <el-descriptions :column="1" border v-if="currentRow">
        <el-descriptions-item label="机构ID">{{ currentRow.organId }}</el-descriptions-item>
        <el-descriptions-item label="机构编码">{{ currentRow.organCoding }}</el-descriptions-item>
        <el-descriptions-item label="机构类型">{{ currentRow.organType }}</el-descriptions-item>
        <el-descriptions-item label="机构性质">{{ currentRow.organNature }}</el-descriptions-item>
        <el-descriptions-item label="机构名称">{{ currentRow.organName }}</el-descriptions-item>
        <el-descriptions-item label="机构描述">{{ currentRow.organDescription || '-' }}</el-descriptions-item>
        <el-descriptions-item label="业务属性">{{ currentRow.businessProperties || '-' }}</el-descriptions-item>
        <el-descriptions-item label="联系电话">{{ currentRow.organTelphone || '-' }}</el-descriptions-item>
        <el-descriptions-item label="机构代码">{{ currentRow.organCode || '-' }}</el-descriptions-item>
        <el-descriptions-item label="机构地址">{{ currentRow.organAddress || '-' }}</el-descriptions-item>
        <el-descriptions-item label="排序">{{ currentRow.orderId }}</el-descriptions-item>
        <el-descriptions-item label="是否可见">
          <el-tag :type="currentRow.isVisible ? 'success' : 'info'">
            {{ currentRow.isVisible ? '可见' : '隐藏' }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="创建时间">{{ currentRow.createTime || '-' }}</el-descriptions-item>
        <el-descriptions-item label="修改时间">{{ currentRow.modifyTime || '-' }}</el-descriptions-item>
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
  getOrganizationTree,
  getAllOrganizations,
  createOrganization,
  updateOrganization,
  deleteOrganization,
  updateOrganizationVisibility
} from '@/api/organization'

// Tree data
const treeData = ref([])
const treeRef = ref(null)
const currentSelection = ref(null)

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
  organName: '',
  organCoding: ''
})

// Dialog
const dialogVisible = ref(false)
const dialogTitle = ref('新建机构')
const isEdit = ref(false)
const formRef = ref(null)
const submitLoading = ref(false)

// View dialog
const viewVisible = ref(false)
const currentRow = ref(null)

// Form data
const formData = reactive({
  organId: null,
  orderId: 1,
  organCoding: '',
  organType: '',
  organNature: '',
  organName: '',
  organDescription: '',
  businessProperties: '',
  organTelphone: '',
  organAddress: '',
  organCode: '',
  regionId: '',
  isVisible: true
})

// Tree props
const treeProps = {
  children: 'children',
  label: 'organName'
}

// Form rules
const formRules = {
  organCoding: [
    { required: true, message: '请输入机构编码', trigger: 'blur' },
    { pattern: /^[A-Za-z0-9]{4,6}$/, message: '机构编码必须是4位（父级）或6位（子级）', trigger: 'blur' }
  ],
  organName: [{ required: true, message: '请输入机构名称', trigger: 'blur' }],
  orderId: [{ required: true, message: '请输入排序', trigger: 'blur' }],
  organType: [{ required: true, message: '请输入机构类型', trigger: 'blur' }],
  organTelphone: [
    { required: true, message: '请输入联系电话', trigger: 'blur' },
    { pattern: /^1[3-9]\d{9}$/, message: '请输入正确的手机号码', trigger: 'blur' }
  ]
}

// Load tree
const loadTree = async () => {
  try {
    const response = await getOrganizationTree()
    if (response.code === 200) {
      treeData.value = response.data || []
    } else {
      ElMessage.error(response.message || '加载机构树失败')
    }
  } catch (error) {
    ElMessage.error('加载机构树失败: ' + error.message)
  }
}

// Load table data
const loadTableData = async () => {
  loading.value = true
  try {
    const response = await getAllOrganizations()
    if (response.code === 200) {
      tableData.value = response.data || []
      pagination.total = response.data.length || 0
    } else {
      ElMessage.error(response.message || '加载数据失败')
    }
  } catch (error) {
    ElMessage.error('加载数据失败: ' + error.message)
  } finally {
    loading.value = false
  }
}

// Node click
const handleNodeClick = (data) => {
  currentSelection.value = data
  loadTableData()
}

// Search
const handleSearch = () => {
  loadTableData()
}

// Reset
const handleReset = () => {
  searchForm.organName = ''
  searchForm.organCoding = ''
  currentSelection.value = null
  loadTableData()
}

// Create
const handleCreate = () => {
  isEdit.value = false
  dialogTitle.value = '新建机构'
  resetForm()
  dialogVisible.value = true
}

// Edit
const handleEdit = (row) => {
  isEdit.value = true
  dialogTitle.value = '编辑机构'
  Object.assign(formData, row)
  dialogVisible.value = true
}

// View
const handleView = (row) => {
  currentRow.value = row
  viewVisible.value = true
}

// Show
const handleShow = (row) => {
  ElMessageBox.confirm('确定要显示该机构吗？', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await updateOrganizationVisibility(row.organId, true)
      ElMessage.success('已显示')
      loadTableData()
    } catch (error) {
      ElMessage.error('操作失败: ' + error.message)
    }
  }).catch(() => {})
}

// Hide
const handleHide = (row) => {
  ElMessageBox.confirm('确定要隐藏该机构吗？', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await updateOrganizationVisibility(row.organId, false)
      ElMessage.success('已隐藏')
      loadTableData()
    } catch (error) {
      ElMessage.error('操作失败: ' + error.message)
    }
  }).catch(() => {})
}

// Delete
const handleDelete = (row) => {
  ElMessageBox.confirm('确定要删除该机构吗？此操作不可恢复。', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  }).then(async () => {
    try {
      await deleteOrganization(row.organId)
      ElMessage.success('删除成功')
      loadTableData()
      loadTree()
    } catch (error) {
      ElMessage.error('删除失败: ' + error.message)
    }
  }).catch(() => {})
}

// View tree
const handleViewTree = () => {
  currentSelection.value = null
  loadTableData()
}

// Submit
const handleSubmit = async () => {
  if (!formRef.value) return

  await formRef.value.validate(async (valid) => {
    if (!valid) return

    submitLoading.value = true
    try {
      if (isEdit.value) {
        await updateOrganization(formData.organId, formData)
        ElMessage.success('更新成功')
      } else {
        await createOrganization(formData)
        ElMessage.success('创建成功')
      }
      dialogVisible.value = false
      loadTableData()
      loadTree()
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
  loadTableData()
}

const handleCurrentChange = (val) => {
  pagination.current = val
  loadTableData()
}

// Dialog close
const handleDialogClose = () => {
  formRef.value?.resetFields()
  resetForm()
}

// Reset form
const resetForm = () => {
  Object.assign(formData, {
    organId: null,
    orderId: 1,
    organCoding: '',
    organType: '',
    organNature: '',
    organName: '',
    organDescription: '',
    businessProperties: '',
    organTelphone: '',
    organAddress: '',
    organCode: '',
    regionId: '',
    isVisible: true
  })
}

// Get organ type color
const getOrganTypeColor = (type) => {
  const colors = {
    'government': 'primary',
    'enterprise': 'success',
    'institution': 'warning',
    'other': 'info'
  }
  return colors[type] || 'info'
}

// Load data on mount
onMounted(() => {
  loadTree()
  loadTableData()
})
</script>

<style scoped>
.organization-container {
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

.organization-tree {
  margin-top: 20px;
}

.tree-node {
  display: flex;
  align-items: center;
  padding: 4px 0;
}

.node-name {
  margin-right: 8px;
  font-weight: 500;
}

.node-code {
  color: #909399;
  font-size: 12px;
}

:deep(.el-pagination) {
  display: flex;
}
</style>
