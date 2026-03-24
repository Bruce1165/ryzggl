<template>
  <div class="department-list-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>部门管理</h2>
      <div class="breadcrumb">当前位置: 组织管理 &gt; 部门列表</div>
    </div>

    <!-- Search and Filter -->
    <el-form :inline="true" :model="queryParams" class="search-form">
      <el-form-item label="部门代码">
        <el-input v-model="queryParams.deptCode" placeholder="输入部门代码" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="部门名称">
        <el-input v-model="queryParams.deptName" placeholder="输入部门名称" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="所属单位">
        <el-input v-model="queryParams.unitCode" placeholder="输入单位代码" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="Search" @click="handleSearch">查询</el-button>
        <el-button type="success" icon="Plus" @click="handleCreate">新增部门</el-button>
      </el-form-item>
    </el-form>

    <!-- Data Table -->
    <el-card>
      <el-table
        v-loading="loading"
        :data="tableData"
        stripe
        border
        :tree-props="{ children: 'children', hasChildren: 'hasChildren' }"
        row-key="deptId"
        default-expand-all
      >
        <el-table-column prop="deptCode" label="部门代码" width="150" />
        <el-table-column prop="deptName" label="部门名称" width="200" />
        <el-table-column prop="unitCode" label="单位代码" width="150" />
        <el-table-column prop="unitName" label="单位名称" width="200" show-overflow-tooltip />
        <el-table-column prop="manager" label="负责人" width="100" />
        <el-table-column prop="phone" label="联系电话" width="150" />
        <el-table-column prop="workerCount" label="人员数量" width="100" align="center">
          <template #default="{row}">
            <el-tag type="primary">{{ row.workerCount || 0 }}</el-tag>
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
            <el-button type="success" size="small" link @click="handleAddChild(row)">
              添加子部门
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

    <!-- Department Detail Dialog -->
    <el-dialog
      v-model="detailDialogVisible"
      :title="dialogMode === 'create' ? '新增部门' : '编辑部门'"
      width="600px"
    >
      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="120px"
      >
        <el-form-item label="上级部门">
          <el-cascader
            v-model="form.parentId"
            :options="deptTree"
            :props="{ checkStrictly: true, emitPath: false }"
            placeholder="选择上级部门"
            clearable
            style="width: 100%"
          />
        </el-form-item>
        <el-form-item label="部门代码" prop="deptCode">
          <el-input v-model="form.deptCode" placeholder="输入部门代码" />
        </el-form-item>
        <el-form-item label="部门名称" prop="deptName">
          <el-input v-model="form.deptName" placeholder="输入部门名称" />
        </el-form-item>
        <el-form-item label="单位代码" prop="unitCode">
          <el-input v-model="form.unitCode" placeholder="输入单位代码" />
        </el-form-item>
        <el-form-item label="单位名称" prop="unitName">
          <el-input v-model="form.unitName" placeholder="输入单位名称" />
        </el-form-item>
        <el-form-item label="负责人" prop="manager">
          <el-input v-model="form.manager" placeholder="输入负责人" />
        </el-form-item>
        <el-form-item label="联系电话" prop="phone">
          <el-input v-model="form.phone" placeholder="输入联系电话" />
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
  </div>
</template>

<script>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'

export default {
  name: 'DepartmentList',

  setup() {
    const loading = ref(false)
    const tableData = ref([])
    const detailDialogVisible = ref(false)
    const dialogMode = ref('create')
    const formRef = ref(null)

    const queryParams = reactive({
      deptCode: '',
      deptName: '',
      unitCode: ''
    })

    const page = reactive({
      current: 1,
      size: 10,
      total: 0
    })

    const form = reactive({
      parentId: '',
      deptCode: '',
      deptName: '',
      unitCode: '',
      unitName: '',
      manager: '',
      phone: '',
      status: '1',
      remark: ''
    })

    const rules = {
      deptCode: [{ required: true, message: '请输入部门代码', trigger: 'blur' }],
      deptName: [{ required: true, message: '请输入部门名称', trigger: 'blur' }],
      unitCode: [{ required: true, message: '请输入单位代码', trigger: 'blur' }],
      unitName: [{ required: true, message: '请输入单位名称', trigger: 'blur' }],
      phone: [{ pattern: /^1[3-9]\d{9}$/, message: '请输入正确的手机号', trigger: 'blur' }]
    }

    const deptTree = ref([
      { value: '1', label: '总部', children: [
        { value: '2', label: '技术部' },
        { value: '3', label: '财务部' },
        { value: '4', label: '人力资源部' }
      ]}
    ])

    onMounted(() => {
      loadTableData()
    })

    /**
     * Load department data
     */
    const loadTableData = async () => {
      loading.value = true
      try {
        // TODO: Implement with actual API call
        tableData.value = [
          { deptId: 1, deptCode: 'DEPT001', deptName: '总部', unitCode: 'UNIT001', unitName: '建设集团有限公司', manager: '张总', phone: '13800138000', workerCount: 156, status: '1', createTime: '2020-01-01 00:00:00', children: [
            { deptId: 2, deptCode: 'DEPT002', deptName: '技术部', unitCode: 'UNIT001', unitName: '建设集团有限公司', manager: '李经理', phone: '13800138001', workerCount: 45, status: '1', createTime: '2020-01-01 00:00:00' },
            { deptId: 3, deptCode: 'DEPT003', deptName: '财务部', unitCode: 'UNIT001', unitName: '建设集团有限公司', manager: '王经理', phone: '13800138002', workerCount: 32, status: '1', createTime: '2020-01-01 00:00:00' },
            { deptId: 4, deptCode: 'DEPT004', deptName: '人力资源部', unitCode: 'UNIT001', unitName: '建设集团有限公司', manager: '赵经理', phone: '13800138003', workerCount: 28, status: '1', createTime: '2020-01-01 00:00:00' }
          ]}
        ]
        page.total = 1
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
     * Handle create department
     */
    const handleCreate = () => {
      dialogMode.value = 'create'
      resetForm()
      detailDialogVisible.value = true
    }

    /**
     * Handle view department
     */
    const handleView = (row) => {
      dialogMode.value = 'view'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle edit department
     */
    const handleEdit = (row) => {
      dialogMode.value = 'edit'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle add child department
     */
    const handleAddChild = (row) => {
      dialogMode.value = 'create'
      resetForm()
      form.parentId = row.deptId.toString()
      detailDialogVisible.value = true
    }

    /**
     * Handle delete department
     */
    const handleDelete = (row) => {
      ElMessageBox.confirm(`确定要删除部门 ${row.deptName} 吗？`, '删除确认', {
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
     * Handle save
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
        parentId: '',
        deptCode: '',
        deptName: '',
        unitCode: '',
        unitName: '',
        manager: '',
        phone: '',
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
      dialogMode,
      form,
      rules,
      deptTree,
      formRef,
      loadTableData,
      handleSearch,
      handleCreate,
      handleView,
      handleEdit,
      handleAddChild,
      handleDelete,
      handleSave,
      handleSizeChange,
      handleCurrentChange
    }
  }
}
</script>

<style scoped>
.department-list-container {
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
