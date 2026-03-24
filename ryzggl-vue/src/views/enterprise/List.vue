<template>
  <div class="enterprise-list-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>企业信息</h2>
      <div class="breadcrumb">当前位置: 组织管理 &gt; 企业列表</div>
    </div>

    <!-- Search and Filter -->
    <el-form :inline="true" :model="queryParams" class="search-form">
      <el-form-item label="企业代码">
        <el-input v-model="queryParams.unitCode" placeholder="输入企业代码" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="企业名称">
        <el-input v-model="queryParams.unitName" placeholder="输入企业名称" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="组织机构代码">
        <el-input v-model="queryParams.orgCode" placeholder="输入组织机构代码" clearable style="width: 200px" />
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
        <el-button type="success" icon="Plus" @click="handleCreate">新增企业</el-button>
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
        <el-table-column prop="unitId" label="企业ID" width="80" />
        <el-table-column prop="unitCode" label="企业代码" width="150" />
        <el-table-column prop="unitName" label="企业名称" width="250" show-overflow-tooltip />
        <el-table-column prop="orgCode" label="组织机构代码" width="150" />
        <el-table-column prop="legalPerson" label="法人代表" width="100" />
        <el-table-column prop="phone" label="联系电话" width="130" />
        <el-table-column prop="address" label="地址" width="250" show-overflow-tooltip />
        <el-table-column prop="workerCount" label="人员数量" width="100" align="center">
          <template #default="{row}">
            <el-tag type="primary">{{ row.workerCount || 0 }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="certCount" label="证书数量" width="100" align="center">
          <template #default="{row}">
            <el-tag type="success">{{ row.certCount || 0 }}</el-tag>
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
        <el-table-column label="操作" width="280" fixed="right">
          <template #default="{row}">
            <el-button type="primary" size="small" link @click="handleView(row)">
              查看
            </el-button>
            <el-button type="primary" size="small" link @click="handleEdit(row)">
              编辑
            </el-button>
            <el-button type="success" size="small" link @click="handleViewWorkers(row)">
              查看人员
            </el-button>
            <el-button type="success" size="small" link @click="handleViewCerts(row)">
              查看证书
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

    <!-- Enterprise Detail Dialog -->
    <el-dialog
      v-model="detailDialogVisible"
      :title="dialogMode === 'create' ? '新增企业' : '编辑企业'"
      width="800px"
    >
      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="120px"
      >
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="企业代码" prop="unitCode">
              <el-input v-model="form.unitCode" placeholder="输入企业代码" :disabled="dialogMode !== 'create'" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="企业名称" prop="unitName">
              <el-input v-model="form.unitName" placeholder="输入企业名称" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="组织机构代码" prop="orgCode">
              <el-input v-model="form.orgCode" placeholder="输入组织机构代码" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="统一社会信用代码" prop="creditCode">
              <el-input v-model="form.creditCode" placeholder="输入统一社会信用代码" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="法人代表" prop="legalPerson">
              <el-input v-model="form.legalPerson" placeholder="输入法人代表" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="注册资金" prop="registeredCapital">
              <el-input v-model="form.registeredCapital" placeholder="输入注册资金">
                <template #append>万元</template>
              </el-input>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="联系电话" prop="phone">
              <el-input v-model="form.phone" placeholder="输入联系电话" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="传真号码">
              <el-input v-model="form.fax" placeholder="输入传真号码" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-form-item label="电子邮箱" prop="email">
          <el-input v-model="form.email" placeholder="输入电子邮箱" />
        </el-form-item>

        <el-form-item label="注册地址" prop="address">
          <el-input v-model="form.address" placeholder="输入注册地址" />
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
  name: 'EnterpriseList',

  setup() {
    const loading = ref(false)
    const tableData = ref([])
    const detailDialogVisible = ref(false)
    const dialogMode = ref('create')
    const formRef = ref(null)

    const queryParams = reactive({
      unitCode: '',
      unitName: '',
      orgCode: '',
      status: ''
    })

    const page = reactive({
      current: 1,
      size: 10,
      total: 0
    })

    const form = reactive({
      unitId: '',
      unitCode: '',
      unitName: '',
      orgCode: '',
      creditCode: '',
      legalPerson: '',
      registeredCapital: '',
      phone: '',
      fax: '',
      email: '',
      address: '',
      businessScope: '',
      status: '1',
      remark: ''
    })

    const rules = {
      unitCode: [{ required: true, message: '请输入企业代码', trigger: 'blur' }],
      unitName: [{ required: true, message: '请输入企业名称', trigger: 'blur' }],
      orgCode: [{ required: true, message: '请输入组织机构代码', trigger: 'blur' }],
      creditCode: [{ required: true, message: '请输入统一社会信用代码', trigger: 'blur' }],
      legalPerson: [{ required: true, message: '请输入法人代表', trigger: 'blur' }],
      phone: [
        { required: true, message: '请输入联系电话', trigger: 'blur' },
        { pattern: /^1[3-9]\d{9}$/, message: '请输入正确的手机号', trigger: 'blur' }
      ],
      email: [{ type: 'email', message: '请输入正确的邮箱地址', trigger: 'blur' }],
      address: [{ required: true, message: '请输入注册地址', trigger: 'blur' }],
      businessScope: [{ required: true, message: '请输入经营范围', trigger: 'blur' }],
      status: [{ required: true, message: '请选择状态', trigger: 'change' }]
    }

    onMounted(() => {
      loadTableData()
    })

    /**
     * Load enterprise data
     */
    const loadTableData = async () => {
      loading.value = true
      try {
        // TODO: Implement with actual API call
        tableData.value = [
          { unitId: 1, unitCode: 'UNIT001', unitName: '建设集团有限公司', orgCode: '91110000123456789X', legalPerson: '张总', registeredCapital: '5000', phone: '010-12345678', fax: '010-12345679', address: '北京市东城区和平里街道1号', businessScope: '房屋建筑工程、市政公用工程', workerCount: 456, certCount: 328, status: '1', createTime: '2020-01-01 00:00:00' },
          { unitId: 2, unitCode: 'UNIT002', unitName: '工程监理有限公司', orgCode: '91110000234567890Y', legalPerson: '李总', registeredCapital: '3000', phone: '010-23456789', fax: '010-23456780', address: '北京市西城区金融街2号', businessScope: '工程监理、技术咨询', workerCount: 234, certCount: 187, status: '1', createTime: '2020-02-15 00:00:00' },
          { unitId: 3, unitCode: 'UNIT003', unitName: '装饰工程有限公司', orgCode: '91110000345678901Z', legalPerson: '王总', registeredCapital: '1000', phone: '010-34567890', fax: '010-34567891', address: '北京市朝阳区建国路3号', businessScope: '室内装饰装修、建筑材料销售', workerCount: 89, certCount: 56, status: '0', createTime: '2021-05-20 00:00:00' }
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
     * Handle create enterprise
     */
    const handleCreate = () => {
      dialogMode.value = 'create'
      resetForm()
      detailDialogVisible.value = true
    }

    /**
     * Handle view enterprise
     */
    const handleView = (row) => {
      dialogMode.value = 'view'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle edit enterprise
     */
    const handleEdit = (row) => {
      dialogMode.value = 'edit'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle view workers
     */
    const handleViewWorkers = (row) => {
      ElMessage.info('跳转到人员列表，企业: ' + row.unitName)
      // Navigate to worker list with unit filter
    }

    /**
     * Handle view certificates
     */
    const handleViewCerts = (row) => {
      ElMessage.info('跳转到证书列表，企业: ' + row.unitName)
      // Navigate to certificate list with unit filter
    }

    /**
     * Handle delete enterprise
     */
    const handleDelete = (row) => {
      ElMessageBox.confirm(`确定要删除企业 ${row.unitName} 吗？`, '删除确认', {
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
     * Handle save enterprise
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
        unitId: '',
        unitCode: '',
        unitName: '',
        orgCode: '',
        creditCode: '',
        legalPerson: '',
        registeredCapital: '',
        phone: '',
        fax: '',
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
      handleViewWorkers,
      handleViewCerts,
      handleDelete,
      handleSave,
      handleSizeChange,
      handleCurrentChange
    }
  }
}
</script>

<style scoped>
.enterprise-list-container {
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
