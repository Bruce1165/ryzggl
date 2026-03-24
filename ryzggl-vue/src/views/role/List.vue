<template>
  <div class="role-list-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>角色管理</h2>
      <div class="breadcrumb">当前位置: 系统管理 &gt; 角色列表</div>
    </div>

    <!-- Search and Filter -->
    <el-form :inline="true" :model="queryParams" class="search-form">
      <el-form-item label="角色名称">
        <el-input v-model="queryParams.roleName" placeholder="输入角色名称" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="角色编码">
        <el-input v-model="queryParams.roleCode" placeholder="输入角色编码" clearable style="width=" 200px" />
      </el-form-item>
      <el-form-item label="状态">
        <el-select v-model="queryParams.status" placeholder="选择状态" clearable style="width: 120px">
          <el-option label="全部" value="" />
          <el-option label="正常" value="1" />
          <el-option label="停用" value="0" />
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="Search" @click="handleSearch">查询</el-button>
        <el-button type="success" icon="Plus" @click="handleCreate">新增角色</el-button>
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
        <el-table-column prop="roleId" label="角色ID" width="80" />
        <el-table-column prop="roleName" label="角色名称" width="150" />
        <el-table-column prop="roleCode" label="角色编码" width="150" />
        <el-table-column prop="description" label="描述" width="250" show-overflow-tooltip />
        <el-table-column prop="userCount" label="用户数量" width="100" align="center">
          <template #default="{row}">
            <el-tag type="primary">{{ row.userCount || 0 }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="permissionCount" label="权限数量" width="100" align="center">
          <template #default="{row}">
            <el-tag type="success">{{ row.permissionCount || 0 }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="status" label="状态" width="80">
          <template #default="{row}">
            <el-tag :type="row.status === '1' ? 'success' : 'danger'" size="small">
              {{ row.status === '1' ? '正常' : '停用' }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="createTime" label="创建时间" width="160" />
        <el-table-column label="操作" width="220" fixed="right">
          <template #default="{row}">
            <el-button type="primary" size="small" link @click="handleView(row)">
              查看
            </el-button>
            <el-button type="primary" size="small" link @click="handleEdit(row)">
              编辑
            </el-button>
            <el-button type="success" size="small" link @click="handleAssignPermission(row)">
              分配权限
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

    <!-- Role Detail Dialog -->
    <el-dialog
      v-model="detailDialogVisible"
      :title="dialogMode === 'create' ? '新增角色' : '编辑角色'"
      width="600px"
    >
      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="100px"
      >
        <el-form-item label="角色名称" prop="roleName">
          <el-input v-model="form.roleName" placeholder="输入角色名称" />
        </el-form-item>
        <el-form-item label="角色编码" prop="roleCode">
          <el-input v-model="form.roleCode" placeholder="输入角色编码" :disabled="dialogMode !== 'create'" />
        </el-form-item>
        <el-form-item label="描述" prop="description">
          <el-input
            v-model="form.description"
            type="textarea"
            :rows="4"
            placeholder="输入角色描述"
          />
        </el-form-item>
        <el-form-item label="状态" prop="status">
          <el-radio-group v-model="form.status">
            <el-radio label="1">正常</el-radio>
            <el-radio label="0">停用</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="排序">
          <el-input-number v-model="form.sortOrder" :min="0" :max="999" />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="detailDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSave">确定</el-button>
      </template>
    </el-dialog>

    <!-- Assign Permission Dialog -->
    <el-dialog v-model="permissionDialogVisible" title="分配权限" width="700px">
      <el-form label-width="100px">
        <el-form-item label="角色">
          <el-input :value="selectedRole?.roleName" disabled />
        </el-form-item>
        <el-form-item label="权限">
          <el-tree
            ref="permissionTreeRef"
            :data="permissionTree"
            :props="{ label: 'name', children: 'children' }"
            node-key="id"
            show-checkbox
            default-expand-all
          />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="permissionDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSavePermission">确定</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'

export default {
  name: 'RoleList',

  setup() {
    const loading = ref(false)
    const tableData = ref([])
    const detailDialogVisible = ref(false)
    const permissionDialogVisible = ref(false)
    const dialogMode = ref('create')
    const formRef = ref(null)
    const permissionTreeRef = ref(null)
    const selectedRole = ref(null)

    const queryParams = reactive({
      roleName: '',
      roleCode: '',
      status: ''
    })

    const page = reactive({
      current: 1,
      size: 10,
      total: 0
    })

    const form = reactive({
      roleId: '',
      roleName: '',
      roleCode: '',
      description: '',
      status: '1',
      sortOrder: 0
    })

    const rules = {
      roleName: [{ required: true, message: '请输入角色名称', trigger: 'blur' }],
      roleCode: [{ required: true, message: '请输入角色编码', trigger: 'blur' }],
      description: [{ required: true, message: '请输入角色描述', trigger: 'blur' }],
      status: [{ required: true, message: '请选择状态', trigger: 'change' }]
    }

    const permissionTree = ref([
      {
        id: 1,
        name: '申请管理',
        children: [
          { id: 11, name: '申请列表' },
          { id: 12, name: '首次申请' },
          { id: 13, name: '变更申请' },
          { id: 14, name: '延续申请' },
          { id: 15, name: '审批申请' }
        ]
      },
      {
        id: 2,
        name: '证书管理',
        children: [
          { id: 21, name: '证书列表' },
          { id: 22, name: '证书查询' },
          { id: 23, name: '证书打印' },
          { id: 24, name: '证书变更' },
          { id: 25, name: '证书延续' }
        ]
      },
      {
        id: 3,
        name: '人员管理',
        children: [
          { id: 31, name: '人员列表' },
          { id: 32, name: '新增人员' },
          { id: 33, name: '编辑人员' },
          { id: 34, name: '删除人员' }
        ]
      },
      {
        id: 4,
        name: '考试管理',
        children: [
          { id: 41, name: '考试列表' },
          { id: 42, name: '考试报名' },
          { id: 43, name: '考试成绩' },
          { id: 44, name: '考点管理' }
        ]
      },
      {
        id: 5,
        name: '系统管理',
        children: [
          { id: 51, name: '用户管理' },
          { id: 52, name: '角色管理' },
          { id: 53, name: '权限管理' },
          { id: 54, name: '部门管理' },
          { id: 55, name: '操作日志' }
        ]
      }
    ])

    onMounted(() => {
      loadTableData()
    })

    /**
     * Load role data
     */
    const loadTableData = async () => {
      loading.value = true
      try {
        // TODO: Implement with actual API call
        tableData.value = [
          { roleId: 1, roleName: '管理员', roleCode: 'admin', description: '系统管理员，拥有所有权限', userCount: 2, permissionCount: 25, status: '1', createTime: '2020-01-01 00:00:00' },
          { roleId: 2, roleName: '操作员', roleCode: 'operator', description: '业务操作人员，可以处理申请和证书', userCount: 8, permissionCount: 18, status: '1', createTime: '2020-01-02 00:00:00' },
          { roleId: 3, roleName: '查看员', roleCode: 'viewer', description: '只读权限，可以查看信息', userCount: 15, permissionCount: 10, status: '1', createTime: '2020-01-03 00:00:00' },
          { roleId: 4, roleName: '部门管理员', roleCode: 'dept_admin', description: '部门管理员，管理本部门人员', userCount: 5, permissionCount: 15, status: '0', createTime: '2021-05-10 00:00:00' }
        ]
        page.total = 4
        loading.value = false
      } catch (error) {
        loading.value = false
        ElMessage.error('加载数据失败: ' + error.message)
      }
    }

    /**
     * Handle search
     */
    const handleSearch = () => {
      page.current = 1
      loadTableData()
    }

    /**
     * Handle create role
     */
    const handleCreate = () => {
      dialogMode.value = 'create'
      resetForm()
      detailDialogVisible.value = true
    }

    /**
     * Handle view role
     */
    const handleView = (row) => {
      dialogMode.value = 'view'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle edit role
     */
    const handleEdit = (row) => {
      dialogMode.value = 'edit'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle assign permission
     */
    const handleAssignPermission = (row) => {
      selectedRole.value = row
      // TODO: Load role permissions
      permissionDialogVisible.value = true
    }

    /**
     * Handle save permission
     */
    const handleSavePermission = () => {
      const checkedKeys = permissionTreeRef.value.getCheckedKeys()
      console.log('Checked permissions:', checkedKeys)
      ElMessage.info('分配权限功能待实现')
      permissionDialogVisible.value = false
    }

    /**
     * Handle delete role
     */
    const handleDelete = (row) => {
      if (row.userCount > 0) {
        ElMessage.warning('该角色下有用户，无法删除')
        return
      }

      ElMessageBox.confirm(`确定要删除角色 ${row.roleName} 吗？`, '删除确认', {
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
     * Handle save role
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
        roleId: '',
        roleName: '',
        roleCode: '',
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
      permissionDialogVisible,
      dialogMode,
      form,
      rules,
      permissionTree,
      selectedRole,
      formRef,
      permissionTreeRef,
      loadTableData,
      handleSearch,
      handleCreate,
      handleView,
      handleEdit,
      handleAssignPermission,
      handleSavePermission,
      handleDelete,
      handleSave,
      handleSizeChange,
      handleCurrentChange
    }
  }
}
</script>

<style scoped>
.role-list-container {
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
