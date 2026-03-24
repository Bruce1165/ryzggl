<template>
  <div class="user-list-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>用户管理</h2>
      <div class="breadcrumb">当前位置: 系统管理 &gt; 用户列表</div>
    </div>

    <!-- Search and Filter -->
    <el-form :inline="true" :model="queryParams" class="search-form">
      <el-form-item label="用户名">
        <el-input v-model="queryParams.username" placeholder="输入用户名" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="姓名">
        <el-input v-model="queryParams.realName" placeholder="输入姓名" clearable style="width: 150px" />
      </el-form-item>
      <el-form-item label="部门">
        <el-select v-model="queryParams.deptId" placeholder="选择部门" clearable style="width: 200px">
          <el-option label="全部" value="" />
          <el-option label="技术部" value="DEPT002" />
          <el-option label="财务部" value="DEPT003" />
          <el-option label="人力资源部" value="DEPT004" />
        </el-select>
      </el-form-item>
      <el-form-item label="角色">
        <el-select v-model="queryParams.roleId" placeholder="选择角色" clearable style="width: 150px">
          <el-option label="全部" value="" />
          <el-option label="管理员" value="admin" />
          <el-option label="操作员" value="operator" />
          <el-option label="查看员" value="viewer" />
        </el-select>
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
        <el-button type="success" icon="Plus" @click="handleCreate">新增用户</el-button>
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
        <el-table-column prop="userId" label="用户ID" width="80" />
        <el-table-column prop="username" label="用户名" width="120" />
        <el-table-column prop="realName" label="姓名" width="100" />
        <el-table-column prop="deptName" label="部门" width="150" />
        <el-table-column prop="roleName" label="角色" width="120">
          <template #default="{row}">
            <el-tag type="primary" size="small">{{ row.roleName }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="phone" label="手机号码" width="130" />
        <el-table-column prop="email" label="电子邮箱" width="180" />
        <el-table-column prop="status" label="状态" width="80">
          <template #default="{row}">
            <el-tag :type="row.status === '1' ? 'success' : 'danger'" size="small">
              {{ row.status === '1' ? '正常' : '停用' }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="lastLoginTime" label="最后登录" width="160" />
        <el-table-column prop="createTime" label="创建时间" width="160" />
        <el-table-column label="操作" width="280" fixed="right">
          <template #default="{row}">
            <el-button type="primary" size="small" link @click="handleView(row)">
              查看
            </el-button>
            <el-button type="primary" size="small" link @click="handleEdit(row)">
              编辑
            </el-button>
            <el-button type="warning" size="small" link @click="handleResetPassword(row)">
              重置密码
            </el-button>
            <el-button type="success" size="small" link @click="handleAssignRole(row)">
              分配角色
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
          :page-sizes="[10, 20, 50, 100]"
          layout="total, sizes, prev, pager, next"
          @size-change="handleSizeChange"
          @current-change="handleCurrentChange"
        />
      </div>
    </el-card>

    <!-- User Detail Dialog -->
    <el-dialog
      v-model="detailDialogVisible"
      :title="dialogMode === 'create' ? '新增用户' : '编辑用户'"
      width="600px"
    >
      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="100px"
      >
        <el-form-item label="用户名" prop="username">
          <el-input v-model="form.username" placeholder="输入用户名" :disabled="dialogMode !== 'create'" />
        </el-form-item>
        <el-form-item label="姓名" prop="realName">
          <el-input v-model="form.realName" placeholder="输入姓名" />
        </el-form-item>
        <el-form-item label="密码" prop="password" v-if="dialogMode === 'create'">
          <el-input v-model="form.password" type="password" placeholder="输入密码" show-password />
        </el-form-item>
        <el-form-item label="部门" prop="deptId">
          <el-select v-model="form.deptId" placeholder="选择部门" style="width: 100%">
            <el-option label="技术部" value="DEPT002" />
            <el-option label="财务部" value="DEPT003" />
            <el-option label="人力资源部" value="DEPT004" />
          </el-select>
        </el-form-item>
        <el-form-item label="角色" prop="roleId">
          <el-select v-model="form.roleId" placeholder="选择角色" style="width: 100%">
            <el-option label="管理员" value="admin" />
            <el-option label="操作员" value="operator" />
            <el-option label="查看员" value="viewer" />
          </el-select>
        </el-form-item>
        <el-form-item label="手机号码" prop="phone">
          <el-input v-model="form.phone" placeholder="输入手机号码" />
        </el-form-item>
        <el-form-item label="电子邮箱" prop="email">
          <el-input v-model="form.email" placeholder="输入电子邮箱" />
        </el-form-item>
        <el-form-item label="状态" prop="status">
          <el-radio-group v-model="form.status">
            <el-radio label="1">正常</el-radio>
            <el-radio label="0">停用</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="备注">
          <el-input
            v-model="form.remark"
            type="textarea"
            :rows="3"
            placeholder="输入备注信息"
          />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="detailDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSave">确定</el-button>
      </template>
    </el-dialog>

    <!-- Assign Role Dialog -->
    <el-dialog v-model="roleDialogVisible" title="分配角色" width="400px">
      <el-form label-width="100px">
        <el-form-item label="用户">
          <el-input :value="selectedUser?.realName" disabled />
        </el-form-item>
        <el-form-item label="角色">
          <el-select v-model="roleForm.roleId" placeholder="选择角色" style="width: 100%">
            <el-option label="管理员" value="admin" />
            <el-option label="操作员" value="operator" />
            <el-option label="查看员" value="viewer" />
          </el-select>
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="roleDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSaveRole">确定</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'

export default {
  name: 'UserList',

  setup() {
    const loading = ref(false)
    const tableData = ref([])
    const detailDialogVisible = ref(false)
    const roleDialogVisible = ref(false)
    const dialogMode = ref('create')
    const formRef = ref(null)
    const selectedUser = ref(null)

    const queryParams = reactive({
      username: '',
      realName: '',
      deptId: '',
      roleId: '',
      status: ''
    })

    const page = reactive({
      current: 1,
      size: 10,
      total: 0
    })

    const form = reactive({
      userId: '',
      username: '',
      realName: '',
      password: '',
      deptId: '',
      roleId: '',
      phone: '',
      email: '',
      status: '1',
      remark: ''
    })

    const roleForm = reactive({
      roleId: ''
    })

    const rules = {
      username: [
        { required: true, message: '请输入用户名', trigger: 'blur' },
        { min: 4, max: 20, message: '用户名长度在4-20个字符', trigger: 'blur' }
      ],
      realName: [{ required: true, message: '请输入姓名', trigger: 'blur' }],
      password: [
        { required: true, message: '请输入密码', trigger: 'blur' },
        { min: 6, message: '密码长度至少6位', trigger: 'blur' }
      ],
      deptId: [{ required: true, message: '请选择部门', trigger: 'change' }],
      roleId: [{ required: true, message: '请选择角色', trigger: 'change' }],
      phone: [{ pattern: /^1[3-9]\d{9}$/, message: '请输入正确的手机号', trigger: 'blur' }],
      email: [{ type: 'email', message: '请输入正确的邮箱地址', trigger: 'blur' }]
    }

    onMounted(() => {
      loadTableData()
    })

    /**
     * Load user data
     */
    const loadTableData = async () => {
      loading.value = true
      try {
        // TODO: Implement with actual API call
        tableData.value = [
          { userId: 1, username: 'admin', realName: '管理员', deptId: 'DEPT004', deptName: '人力资源部', roleId: 'admin', roleName: '管理员', phone: '13800138000', email: 'admin@example.com', status: '1', lastLoginTime: '2024-03-14 09:00:00', createTime: '2020-01-01 00:00:00' },
          { userId: 2, username: 'operator01', realName: '张三', deptId: 'DEPT002', deptName: '技术部', roleId: 'operator', roleName: '操作员', phone: '13800138001', email: 'zhangsan@example.com', status: '1', lastLoginTime: '2024-03-13 15:30:00', createTime: '2021-05-10 00:00:00' },
          { userId: 3, username: 'viewer01', realName: '李四', deptId: 'DEPT003', deptName: '财务部', roleId: 'viewer', roleName: '查看员', phone: '13800138002', email: 'lisi@example.com', status: '0', lastLoginTime: '2024-03-10 10:00:00', createTime: '2022-03-15 00:00:00' }
        ]
        page.total = 3
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
     * Handle create user
     */
    const handleCreate = () => {
      dialogMode.value = 'create'
      resetForm()
      detailDialogVisible.value = true
    }

    /**
     * Handle view user
     */
    const handleView = (row) => {
      dialogMode.value = 'view'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle edit user
     */
    const handleEdit = (row) => {
      dialogMode.value = 'edit'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle reset password
     */
    const handleResetPassword = (row) => {
      ElMessageBox.confirm(`确定要重置用户 ${row.realName} 的密码吗？`, '重置密码', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(() => {
        ElMessage.info('重置密码功能待实现')
        ElMessage.success('密码重置成功，新密码为: 123456')
      }).catch(() => {
        ElMessage.info('已取消重置')
      })
    }

    /**
     * Handle assign role
     */
    const handleAssignRole = (row) => {
      selectedUser.value = row
      roleForm.roleId = row.roleId
      roleDialogVisible.value = true
    }

    /**
     * Handle save role
     */
    const handleSaveRole = () => {
      ElMessage.info('分配角色功能待实现')
      roleDialogVisible.value = false
      loadTableData()
    }

    /**
     * Handle delete user
     */
    const handleDelete = (row) => {
      ElMessageBox.confirm(`确定要删除用户 ${row.realName} 吗？`, '删除确认', {
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
     * Handle save user
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
        userId: '',
        username: '',
        realName: '',
        password: '',
        deptId: '',
        roleId: '',
        phone: '',
        email: '',
        status: '1',
        remark: ''
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
      roleDialogVisible,
      dialogMode,
      form,
      roleForm,
      rules,
      selectedUser,
      formRef,
      loadTableData,
      handleSearch,
      handleCreate,
      handleView,
      handleEdit,
      handleResetPassword,
      handleAssignRole,
      handleSaveRole,
      handleDelete,
      handleSave,
      handleSizeChange,
      handleCurrentChange
    }
  }
}
</script>

<style scoped>
.user-list-container {
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
