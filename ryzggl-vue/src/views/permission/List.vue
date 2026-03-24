<template>
  <div class="permission-list-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>权限管理</h2>
      <div class="breadcrumb">当前位置: 系统管理 &gt; 权限列表</div>
    </div>

    <!-- Search and Filter -->
    <el-form :inline="true" :model="queryParams" class="search-form">
      <el-form-item label="权限名称">
        <el-input v-model="queryParams.permissionName" placeholder="输入权限名称" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="权限代码">
        <el-input v-model="queryParams.permissionCode" placeholder="输入权限代码" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="模块">
        <el-select v-model="queryParams.module" placeholder="选择模块" clearable style="width: 150px">
          <el-option label="全部" value="" />
          <el-option label="申请管理" value="apply" />
          <el-option label="证书管理" value="certificate" />
          <el-option label="人员管理" value="worker" />
          <el-option label="考试管理" value="exam" />
          <el-option label="系统管理" value="system" />
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="Search" @click="handleSearch">查询</el-button>
        <el-button type="success" icon="Plus" @click="handleCreate">新增权限</el-button>
      </el-form-item>
    </el-form>

    <!-- Data Table -->
    <el-card>
      <el-table
        v-loading="loading"
        :data="tableData"
        stripe
        border
      >
        <el-table-column prop="permissionId" label="权限ID" width="80" />
        <el-table-column prop="permissionCode" label="权限代码" width="180" />
        <el-table-column prop="permissionName" label="权限名称" width="200" show-overflow-tooltip />
        <el-table-column prop="module" label="所属模块" width="120">
          <template #default="{row}">
            <el-tag type="primary">{{ getModuleName(row.module) }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="permissionType" label="权限类型" width="120">
          <template #default="{row}">
            <el-tag :type="getTypeColor(row.permissionType)">{{ row.permissionType }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="url" label="路由路径" width="200" />
        <el-table-column prop="method" label="请求方法" width="100">
          <template #default="{row}">
            <el-tag :type="getMethodColor(row.method)">{{ row.method }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="description" label="描述" width="250" show-overflow-tooltip />
        <el-table-column prop="status" label="状态" width="80">
          <template #default="{row}">
            <el-tag :type="row.status === '1' ? 'success' : 'danger'" size="small">
              {{ row.status === '1' ? '启用' : '停用' }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="createTime" label="创建时间" width="160" />
        <el-table-column label="操作" width="180" fixed="right">
          <template #default="{row}">
            <el-button type="primary" size="small" link @click="handleView(row)">
              查看
            </el-button>
            <el-button type="primary" size="small" link @click="handleEdit(row)">
              编辑
            </el-button>
            <el-button type="warning" size="small" link @click="handleToggleStatus(row)">
              {{ row.status === '1' ? '停用' : '启用' }}
            </el-button>
            <el-button type="danger" size="small" link @click="handleDelete(row)">
              删除
            </el-button>
          </template>
        </el-table-column>
      </el-table>

      <!-- Pagination -->
      <div class="pagination-container">
        <el-pagination
          v-model:current-page="page.current"
          v-model:page-size="page.size"
          :total="page.total"
          :page-sizes="[10, 20, 50]"
          layout="total, sizes, prev, pager, next"
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
        />
      </div>
    </el-card>

    <!-- Permission Detail Dialog -->
    <el-dialog
      v-model="detailDialogVisible"
      :title="dialogMode === 'create' ? '新增权限' : '编辑权限'"
      width="700px"
    >
      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="120px"
      >
        <el-form-item label="权限代码" prop="permissionCode">
          <el-input v-model="form.permissionCode" placeholder="输入权限代码" :disabled="dialogMode !== 'create'" />
        </el-form-item>
        <el-form-item label="权限名称" prop="permissionName">
          <el-input v-model="form.permissionName" placeholder="输入权限名称" />
        </el-form-item>
        <el-form-item label="所属模块" prop="module">
          <el-select v-model="form.module" placeholder="选择所属模块" style="width: 100%">
            <el-option label="申请管理" value="apply" />
            <el-option label="证书管理" value="certificate" />
            <el-option label="人员管理" value="worker" />
            <el-option label="考试管理" value="exam" />
            <el-option label="系统管理" value="system" />
          </el-select>
        </el-form-item>
        <el-form-item label="权限类型" prop="permissionType">
          <el-select v-model="form.permissionType" placeholder="选择权限类型" style="width: 100%">
            <el-option label="菜单" value="menu" />
            <el-option label="按钮" value="button" />
            <el-option label="API" value="api" />
            <el-option label="数据" value="data" />
          </el-select>
        </el-form-item>
        <el-form-item label="路由路径" prop="url">
          <el-input v-model="form.url" placeholder="输入路由路径" />
        </el-form-item>
        <el-form-item label="请求方法" prop="method">
          <el-select v-model="form.method" placeholder="选择请求方法" style="width: 100%">
            <el-option label="GET" value="GET" />
            <el-option label="POST" value="POST" />
            <el-option label="PUT" value="PUT" />
            <el-option label="DELETE" value="DELETE" />
          </el-select>
        </el-form-item>
        <el-form-item label="描述" prop="description">
          <el-input
            v-model="form.description"
            type="textarea"
            :rows="3"
            placeholder="输入权限描述"
          />
        </el-form-item>
        <el-form-item label="排序">
          <el-input-number v-model="form.sortOrder" :min="0" :max="999" />
        </el-form-item>
        <el-form-item label="状态" prop="status">
          <el-radio-group v-model="form.status">
            <el-radio label="1">启用</el-radio>
            <el-radio label="0">停用</el-radio>
          </el-radio-group>
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="detailDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSave">确定</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'

export default {
  name: 'PermissionList',

  setup() {
    const loading = ref(false)
    const tableData = ref([])
    const detailDialogVisible = ref(false)
    const dialogMode = ref('create')
    const formRef = ref(null)

    const queryParams = reactive({
      permissionName: '',
      permissionCode: '',
      module: ''
    })

    const page = reactive({
      current: 1,
      size: 10,
      total: 0
    })

    const form = reactive({
      permissionId: '',
      permissionCode: '',
      permissionName: '',
      module: '',
      permissionType: '',
      url: '',
      method: 'GET',
      description: '',
      status: '1',
      sortOrder: 0
    })

    const rules = {
      permissionCode: [{ required: true, message: '请输入权限代码', trigger: 'blur' }],
      permissionName: [{ required: true, message: '请输入权限名称', trigger: 'blur' }],
      module: [{ required: true, message: '请选择所属模块', trigger: 'change' }],
      permissionType: [{ required: true, message: '请选择权限类型', trigger: 'change' }],
      url: [{ required: true, message: '请输入路由路径', trigger: 'blur' }],
      method: [{ required: true, message: '请选择请求方法', trigger: 'change' }],
      description: [{ required: true, message: '请输入权限描述', trigger: 'blur' }],
      status: [{ required: true, message: '请选择状态', trigger: 'change' }]
    }

    onMounted(() => {
      loadTableData()
    })

    /**
     * Load permission data
     */
    const loadTableData = async () => {
      loading.value = true
      try {
        // TODO: Implement with actual API call
        tableData.value = [
          { permissionId: 1, permissionCode: 'APPLY_LIST', permissionName: '申请列表', module: 'apply', permissionType: 'menu', url: '/apply/list', method: 'GET', description: '查看申请列表', status: '1', createTime: '2020-01-01 00:00:00' },
          { permissionId: 2, permissionCode: 'APPLY_CREATE', permissionName: '创建申请', module: 'apply', permissionType: 'button', url: '/apply/create', method: 'POST', description: '创建新申请', status: '1', createTime: '2020-01-01 00:00:00' },
          { permissionId: 3, permissionCode: 'CERT_LIST', permissionName: '证书列表', module: 'certificate', permissionType: 'menu', url: '/certificates', method: 'GET', description: '查看证书列表', status: '1', createTime: '2020-01-01 00:00:00' },
          { permissionId: 4, permissionCode: 'WORKER_LIST', permissionName: '人员列表', module: 'worker', permissionType: 'menu', url: '/worker/list', method: 'GET', description: '查看人员列表', status: '1', createTime: '2020-01-01 00:00:00' },
          { permissionId: 5, permissionCode: 'WORKER_CREATE', permissionName: '新增人员', module: 'worker', permissionType: 'button', url: '/worker/create', method: 'POST', description: '新增人员', status: '1', createTime: '2020-01-01 00:00:00' },
          { permissionId: 6, permissionCode: 'APPLY_APPROVE', permissionName: '审批申请', module: 'apply', permissionType: 'button', url: '/api/apply/*/approve', method: 'PUT', description: '审批申请', status: '1', createTime: '2020-01-01 00:00:00' },
          { permissionId: 7, permissionCode: 'CERT_PAUSE', permissionName: '暂停证书', module: 'certificate', permissionType: 'button', url: '/certificate/*/pause', method: 'PUT', description: '暂停证书', status: '1', createTime: '2020-01-01 00:00:00' }
        ]
        page.total = 7
        loading.value = false
      } catch (error) {
        loading.value = false
        ElMessage.error('加载数据失败: ' + error.message)
      }
    }

    /**
     * Get module name
     */
    const getModuleName = (module) => {
      const moduleMap = {
        'apply': '申请管理',
        'certificate': '证书管理',
        'worker': '人员管理',
        'exam': '考试管理',
        'system': '系统管理'
      }
      return moduleMap[module] || module
    }

    /**
     * Get type color
     */
    const getTypeColor = (type) => {
      const colorMap = {
        'menu': 'primary',
        'button': 'success',
        'api': 'warning',
        'data': 'info'
      }
      return colorMap[type] || 'info'
    }

    /**
     * Get method color
     */
    const getMethodColor = (method) => {
      const colorMap = {
        'GET': 'success',
        'POST': 'primary',
        'PUT': 'warning',
        'DELETE': 'danger'
      }
      return colorMap[method] || 'info'
    }

    /**
     * Handle search
     */
    const handleSearch = () => {
      page.current = 1
      loadTableData()
    }

    /**
     * Handle create permission
     */
    const handleCreate = () => {
      dialogMode.value = 'create'
      resetForm()
      detailDialogVisible.value = true
    }

    /**
     * Handle view permission
     */
    const handleView = (row) => {
      dialogMode.value = 'view'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle edit permission
     */
    const handleEdit = (row) => {
      dialogMode.value = 'edit'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle toggle status
     */
    const handleToggleStatus = (row) => {
      const newStatus = row.status === '1' ? '0' : '1'
      const action = newStatus === '1' ? '启用' : '停用'

      ElMessageBox.confirm(`确定要${action}权限 ${row.permissionName} 吗？`, `${action}权限`, {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        ElMessage.info(`${action}功能待实现`)
        loadTableData()
      }).catch(() => {
        ElMessage.info('已取消操作')
      })
    }

    /**
     * Handle delete permission
     */
    const handleDelete = (row) => {
      ElMessageBox.confirm(`确定要删除权限 ${row.permissionName} 吗？`, '删除确认', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        ElMessage.info('删除功能待实现')
        loadTableData()
      }).catch(() => {
        ElMessage.info('已取消删除')
      })
    }

    /**
     * Handle save permission
     */
    const handleSave = async () => {
      if (!formRef.value) return

      try {
        const valid = await formRef.value.validate()
        if (!valid) return

        // TODO: Implement with actual API call
        ElMessage.info('保存功能待实现')
        detailDialogVisible.value = false
        loadTableData()
      } catch (error) {
        ElMessage.error('保存失败: ' + error.message)
      }
    }

    /**
     * Reset form
     */
    const resetForm = () => {
      Object.assign(form, {
        permissionId: '',
        permissionCode: '',
        permissionName: '',
        module: '',
        permissionType: '',
        url: '',
        method: 'GET',
        description: '',
        status: '1',
        sortOrder: 0
      })
    }

    /**
     * Handle pagination size change
     */
    const handleSizeChange = (val) => {
      page.size = val
      page.current = 1
      loadTableData()
    }

    /**
     * Handle current page change
     */
    const handleCurrentChange = (val) => {
      page.current = val
      loadTableData()
    }

    return {
      loading,
      tableData,
      queryParams,
      page,
      detailDialogVisible,
      dialogMode,
      form,
      rules,
      formRef,
      loadTableData,
      handleSearch,
      getModuleName,
      getTypeColor,
      getMethodColor,
      handleCreate,
      handleView,
      handleEdit,
      handleToggleStatus,
      handleDelete,
      handleSave,
      handleSizeChange,
      handleCurrentChange
    }
  }
}
</script>

<style scoped>
.permission-list-container {
  padding: 20px;
}

.page-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.page-header h2 {
  margin: 0;
  font-size: 18px;
  font-weight: bold;
}

.breadcrumb {
  color: #666;
  font-size: 14px;
}

.search-form {
  background: #f5f7fa;
  padding: 20px;
  margin-bottom: 20px;
  border-radius: 4px;
}

.pagination-container {
  margin-top: 20px;
  display: flex;
  justify-content: center;
}
</style>
