<template>
  <div class="agency-list-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>机构管理</h2>
      <div class="breadcrumb">当前位置: 组织管理 &gt; 机构列表</div>
    </div>

    <!-- Search and Filter -->
    <el-form :inline="true" :model="queryParams" class="search-form">
      <el-form-item label="机构代码">
        <el-input v-model="queryParams.agencyCode" placeholder="输入机构代码" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="机构名称">
        <el-input v-model="queryParams.agencyName" placeholder="输入机构名称" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="机构类型">
        <el-select v-model="queryParams.agencyType" placeholder="选择机构类型" clearable style="width: 150px">
          <el-option label="全部" value="" />
          <el-option label="事业单位" value="事业单位" />
          <el-option label="企业单位" value="企业单位" />
          <el-option label="社会组织" value="社会组织" />
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
        <el-button type="success" icon="Plus" @click="handleCreate">新增机构</el-button>
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
        <el-table-column prop="agencyId" label="机构ID" width="80" />
        <el-table-column prop="agencyCode" label="机构代码" width="150" />
        <el-table-column prop="agencyName" label="机构名称" width="200" show-overflow-tooltip />
        <el-table-column prop="agencyType" label="机构类型" width="120">
          <template #default="{row}">
            <el-tag type="primary">{{ row.agencyType }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="legalPerson" label="负责人" width="100" />
        <el-table-column prop="phone" label="联系电话" width="130" />
        <el-table-column prop="address" label="地址" width="250" show-overflow-tooltip />
        <el-table-column prop="enterpriseCount" label="企业数量" width="100" align="center">
          <template #default="{row}">
            <el-tag type="success">{{ row.enterpriseCount || 0 }}</el-tag>
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
            <el-button type="success" size="small" link @click="handleViewEnterprises(row)">
              查看企业
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

    <!-- Agency Detail Dialog -->
    <el-dialog
      v-model="detailDialogVisible"
      :title="dialogMode === 'create' ? '新增机构' : '编辑机构'"
      width="700px"
    >
      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="120px"
      >
        <el-form-item label="机构代码" prop="agencyCode">
          <el-input v-model="form.agencyCode" placeholder="输入机构代码" :disabled="dialogMode !== 'create'" />
        </el-form-item>
        <el-form-item label="机构名称" prop="agencyName">
          <el-input v-model="form.agencyName" placeholder="输入机构名称" />
        </el-form-item>
        <el-form-item label="机构类型" prop="agencyType">
          <el-select v-model="form.agencyType" placeholder="选择机构类型" style="width: 100%">
            <el-option label="事业单位" value="事业单位" />
            <el-option label="企业单位" value="企业单位" />
            <el-option label="社会组织" value="社会组织" />
          </el-select>
        </el-form-item>
        <el-form-item label="组织机构代码" prop="orgCode">
          <el-input v-model="form.orgCode" placeholder="输入组织机构代码" />
        </el-form-item>
        <el-form-item label="负责人" prop="legalPerson">
          <el-input v-model="form.legalPerson" placeholder="输入负责人" />
        </el-form-item>
        <el-form-item label="联系电话" prop="phone">
          <el-input v-model="form.phone" placeholder="输入联系电话" />
        </el-form-item>
        <el-form-item label="电子邮箱" prop="email">
          <el-input v-model="form.email" placeholder="输入电子邮箱" />
        </el-form-item>
        <el-form-item label="地址" prop="address">
          <el-input v-model="form.address" placeholder="输入地址" />
        </el-form-item>
        <el-form-item label="经营范围" prop="businessScope">
          <el-input
            v-model="form.businessScope"
            type="textarea"
            :rows="3"
            placeholder="输入经营范围"
          />
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
  name: 'AgencyList',

  setup() {
    const loading = ref(false)
    const tableData = ref([])
    const detailDialogVisible = ref(false)
    const dialogMode = ref('create')
    const formRef = ref(null)

    const queryParams = reactive({
      agencyCode: '',
      agencyName: '',
      agencyType: '',
      status: ''
    })

    const page = reactive({
      current: 1,
      size: 10,
      total: 0
    })

    const form = reactive({
      agencyId: '',
      agencyCode: '',
      agencyName: '',
      agencyType: '',
      orgCode: '',
      legalPerson: '',
      phone: '',
      email: '',
      address: '',
      businessScope: '',
      status: '1',
      remark: ''
    })

    const rules = {
      agencyCode: [{ required: true, message: '请输入机构代码', trigger: 'blur' }],
      agencyName: [{ required: true, message: '请输入机构名称', trigger: 'blur' }],
      agencyType: [{ required: true, message: '请选择机构类型', trigger: 'change' }],
      orgCode: [{ required: true, message: '请输入组织机构代码', trigger: 'blur' }],
      legalPerson: [{ required: true, message: '请输入负责人', trigger: 'blur' }],
      phone: [
        { required: true, message: '请输入联系电话', trigger: 'blur' },
        { pattern: /^1[3-9]\d{9}$/, message: '请输入正确的手机号', trigger: 'blur' }
      ],
      email: [{ type: 'email', message: '请输入正确的邮箱地址', trigger: 'blur' }],
      address: [{ required: true, message: '请输入地址', trigger: 'blur' }],
      businessScope: [{ required: true, message: '请输入经营范围', trigger: 'blur' }],
      status: [{ required: true, message: '请选择状态', trigger: 'change' }]
    }

    onMounted(() => {
      loadTableData()
    })

    /**
     * Load agency data
     */
    const loadTableData = async () => {
      loading.value = true
      try {
        // TODO: Implement with actual API call
        tableData.value = [
          { agencyId: 1, agencyCode: 'JG001', agencyName: '北京市建设行业执业资格注册中心', agencyType: '事业单位', legalPerson: '张主任', phone: '010-12345678', email: 'zhang@example.com', address: '北京市东城区和平里街道1号', businessScope: '建设行业执业资格注册管理', enterpriseCount: 156, status: '1', createTime: '2020-01-01 00:00:00' },
          { agencyId: 2, agencyCode: 'JG002', agencyName: '天津市建筑工程培训中心', agencyType: '事业单位', legalPerson: '李主任', phone: '022-23456789', email: 'li@example.com', address: '天津市河西区马场道2号', businessScope: '建筑工程培训', enterpriseCount: 89, status: '1', createTime: '2020-03-01 00:00:00' }
        ]
        page.total = 2
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
     * Handle create agency
     */
    const handleCreate = () => {
      dialogMode.value = 'create'
      resetForm()
      detailDialogVisible.value = true
    }

    /**
     * Handle view agency
     */
    const handleView = (row) => {
      dialogMode.value = 'view'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle edit agency
     */
    const handleEdit = (row) => {
      dialogMode.value = 'edit'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle view enterprises
     */
    const handleViewEnterprises = (row) => {
      ElMessage.info('跳转到企业列表，机构: ' + row.agencyName)
    }

    /**
     * Handle delete agency
     */
    const handleDelete = (row) => {
      if (row.enterpriseCount > 0) {
        ElMessage.warning('该机构下有企业，无法删除')
        return
      }

      ElMessageBox.confirm(`确定要删除机构 ${row.agencyName} 吗？`, '删除确认', {
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
     * Handle save agency
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
        agencyId: '',
        agencyCode: '',
        agencyName: '',
        agencyType: '',
        orgCode: '',
        legalPerson: '',
        phone: '',
        email: '',
        address: '',
        businessScope: '',
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
      formRef,
      loadTableData,
      handleSearch,
      handleCreate,
      handleView,
      handleEdit,
      handleViewEnterprises,
      handleDelete,
      handleSave,
      handleSizeChange,
      handleCurrentChange
    }
  }
}
</script>

<style scoped>
.agency-list-container {
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
