<template>
  <div class="qualification-list-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>资格管理</h2>
      <div class="breadcrumb">当前位置: 系统管理 &gt; 资格列表</div>
    </div>

    <!-- Search and Filter -->
    <el-form :inline="true" :model="queryParams" class="search-form">
      <el-form-item label="资格名称">
        <el-input v-model="queryParams.qualificationName" placeholder="输入资格名称" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="资格等级">
        <el-select v-model="queryParams.level" placeholder="选择资格等级" clearable style="width: 150px">
          <el-option label="全部" value="" />
          <el-option label="一级" value="一级" />
          <el-option label="二级" value="二级" />
        </el-select>
      </el-form-item>
      <el-form-item label="专业类别">
        <el-select v-model="queryParams.profession" placeholder="选择专业类别" clearable style="width: 150px">
          <el-option label="全部" value="" />
          <el-option label="土木建筑工程" value="土木建筑工程" />
          <el-option label="安装工程" value="安装工程" />
          <el-option label="市政工程" value="市政工程" />
          <el-option label="水利工程" value="水利工程" />
        </el-select>
      </el-form-item>
      <el-form-item label="状态">
        <el-select v-model="queryParams.status" placeholder="选择状态" clearable style="width: 120px">
          <el-option label="全部" value="" />
          <el-option label="有效" value="1" />
          <el-option label="停用" value="0" />
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="Search" @click="handleSearch">查询</el-button>
        <el-button type="success" icon="Plus" @click="handleCreate">新增资格</el-button>
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
        <el-table-column prop="qualificationId" label="资格ID" width="80" />
        <el-table-column prop="qualificationCode" label="资格代码" width="120" />
        <el-table-column prop="qualificationName" label="资格名称" width="200" show-overflow-tooltip />
        <el-table-column prop="level" label="资格等级" width="100">
          <template #default="{row}">
            <el-tag type="primary">{{ row.level }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="profession" label="专业类别" width="120" />
        <el-table-column prop="certificateType" label="证书类型" width="120" />
        <el-table-column prop="validPeriod" label="有效期（年）" width="120" align="center" />
        <el-table-column prop="examRequired" label="是否需要考试" width="120" align="center">
          <template #default="{row}">
            <el-tag :type="row.examRequired ? 'success' : 'info'" size="small">
              {{ row.examRequired ? '是' : '否' }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="certificateCount" label="证书数量" width="100" align="center">
          <template #default="{row}">
            <el-tag type="success">{{ row.certificateCount || 0 }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="status" label="状态" width="80">
          <template #default="{row}">
            <el-tag :type="row.status === '1' ? 'success' : 'danger'" size="small">
              {{ row.status === '1' ? '有效' : '停用' }}
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
            <el-button type="success" size="small" link @click="handleViewRequirements(row)">
              考试要求
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

    <!-- Qualification Detail Dialog -->
    <el-dialog
      v-model="detailDialogVisible"
      :title="dialogMode === 'create' ? '新增资格' : '编辑资格'"
      width="700px"
    >
      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="120px"
      >
        <el-form-item label="资格代码" prop="qualificationCode">
          <el-input v-model="form.qualificationCode" placeholder="输入资格代码" :disabled="dialogMode !== 'create'" />
        </el-form-item>
        <el-form-item label="资格名称" prop="qualificationName">
          <el-input v-model="form.qualificationName" placeholder="输入资格名称" />
        </el-form-item>
        <el-form-item label="资格等级" prop="level">
          <el-select v-model="form.level" placeholder="选择资格等级" style="width: 100%">
            <el-option label="一级" value="一级" />
            <el-option label="二级" value="二级" />
          </el-select>
        </el-form-item>
        <el-form-item label="专业类别" prop="profession">
          <el-select v-model="form.profession" placeholder="选择专业类别" style="width: 100%">
            <el-option label="土木建筑工程" value="土木建筑工程" />
            <el-option label="安装工程" value="安装工程" />
            <el-option label="市政工程" value="市政工程" />
            <el-option label="水利工程" value="水利工程" />
          </el-select>
        </el-form-item>
        <el-form-item label="证书类型" prop="certificateType">
          <el-input v-model="form.certificateType" placeholder="输入证书类型" />
        </el-form-item>
        <el-form-item label="有效期" prop="validPeriod">
          <el-input-number v-model="form.validPeriod" :min="1" :max="10" style="width: 100%" />
          <span style="margin-left: 10px;">年</span>
        </el-form-item>
        <el-form-item label="需要考试" prop="examRequired">
          <el-radio-group v-model="form.examRequired">
            <el-radio :label="true">是</el-radio>
            <el-radio :label="false">否</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="考试科目" v-if="form.examRequired">
          <el-input
            v-model="form.examSubjects"
            type="textarea"
            :rows="3"
            placeholder="输入考试科目，多个科目用逗号分隔"
          />
        </el-form-item>
        <el-form-item label="说明" prop="description">
          <el-input
            v-model="form.description"
            type="textarea"
            :rows="4"
            placeholder="输入资格说明"
          />
        </el-form-item>
        <el-form-item label="状态" prop="status">
          <el-radio-group v-model="form.status">
            <el-radio label="1">有效</el-radio>
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

    <!-- Exam Requirements Dialog -->
    <el-dialog v-model="requirementsDialogVisible" title="考试要求" width="600px">
      <el-descriptions v-if="selectedQualification" :column="1" border>
        <el-descriptions-item label="资格名称">
          {{ selectedQualification.qualificationName }}
        </el-descriptions-item>
        <el-descriptions-item label="资格等级">
          {{ selectedQualification.level }}
        </el-descriptions-item>
        <el-descriptions-item label="专业类别">
          {{ selectedQualification.profession }}
        </el-descriptions-item>
        <el-descriptions-item label="证书类型">
          {{ selectedQualification.certificateType }}
        </el-descriptions-item>
        <el-descriptions-item label="有效期">
          {{ selectedQualification.validPeriod }} 年
        </el-descriptions-item>
        <el-descriptions-item label="是否需要考试">
          <el-tag :type="selectedQualification.examRequired ? 'success' : 'info'">
            {{ selectedQualification.examRequired ? '是' : '否' }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="考试科目" :span="1">
          {{ selectedQualification.examSubjects || '无' }}
        </el-descriptions-item>
      </el-descriptions>
      <template #footer>
        <el-button @click="requirementsDialogVisible = false">关闭</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'

export default {
  name: 'QualificationList',

  setup() {
    const loading = ref(false)
    const tableData = ref([])
    const detailDialogVisible = ref(false)
    const requirementsDialogVisible = ref(false)
    const dialogMode = ref('create')
    const formRef = ref(null)
    const selectedQualification = ref(null)

    const queryParams = reactive({
      qualificationName: '',
      level: '',
      profession: '',
      status: ''
    })

    const page = reactive({
      current: 1,
      size: 10,
      total: 0
    })

    const form = reactive({
      qualificationId: '',
      qualificationCode: '',
      qualificationName: '',
      level: '',
      profession: '',
      certificateType: '',
      validPeriod: 5,
      examRequired: true,
      examSubjects: '',
      description: '',
      status: '1',
      sortOrder: 0
    })

    const rules = {
      qualificationCode: [{ required: true, message: '请输入资格代码', trigger: 'blur' }],
      qualificationName: [{ required: true, message: '请输入资格名称', trigger: 'blur' }],
      level: [{ required: true, message: '请选择资格等级', trigger: 'change' }],
      profession: [{ required: true, message: '请选择专业类别', trigger: 'change' }],
      certificateType: [{ required: true, message: '请输入证书类型', trigger: 'blur' }],
      validPeriod: [{ required: true, message: '请输入有效期', trigger: 'blur' }],
      examRequired: [{ required: true, message: '请选择是否需要考试', trigger: 'change' }],
      examSubjects: [{
        validator: (rule, value, callback) => {
          if (form.examRequired && !value) {
            callback(new Error('请输入考试科目'))
          } else {
            callback()
          }
        },
        trigger: 'blur'
      }],
      description: [{ required: true, message: '请输入说明', trigger: 'blur' }],
      status: [{ required: true, message: '请选择状态', trigger: 'change' }]
    }

    onMounted(() => {
      loadTableData()
    })

    /**
     * Load qualification data
     */
    const loadTableData = async () => {
      loading.value = true
      try {
        // TODO: Implement with actual API call
        tableData.value = [
          { qualificationId: 1, qualificationCode: 'QF001', qualificationName: '二级造价工程师', level: '二级', profession: '土木建筑工程', certificateType: '专业资格证书', validPeriod: 5, examRequired: true, examSubjects: '建设工程造价管理,建设工程技术与计量', certificateCount: 328, status: '1', createTime: '2020-01-01 00:00:00' },
          { qualificationId: 2, qualificationCode: 'QF002', qualificationName: '二级造价工程师', level: '二级', profession: '安装工程', certificateType: '专业资格证书', validPeriod: 5, examRequired: true, examSubjects: '建设工程造价管理,安装工程技术', certificateCount: 156, status: '1', createTime: '2020-01-01 00:00:00' },
          { qualificationId: 3, qualificationCode: 'QF003', qualificationName: '一级造价工程师', level: '一级', profession: '土木建筑工程', certificateType: '专业资格证书', validPeriod: 5, examRequired: true, examSubjects: '建设工程造价案例分析,建设工程技术与计量', certificateCount: 89, status: '1', createTime: '2020-01-02 00:00:00' },
          { qualificationId: 4, qualificationCode: 'QF004', qualificationName: '二级建造师', level: '二级', profession: '市政工程', certificateType: '职业资格证书', validPeriod: 3, examRequired: true, examSubjects: '建设工程法规,专业工程管理', certificateCount: 245, status: '0', createTime: '2020-03-01 00:00:00' }
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
     * Handle create qualification
     */
    const handleCreate = () => {
      dialogMode.value = 'create'
      resetForm()
      detailDialogVisible.value = true
    }

    /**
     * Handle view qualification
     */
    const handleView = (row) => {
      dialogMode.value = 'view'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle edit qualification
     */
    const handleEdit = (row) => {
      dialogMode.value = 'edit'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle view exam requirements
     */
    const handleViewRequirements = (row) => {
      selectedQualification.value = row
      requirementsDialogVisible.value = true
    }

    /**
     * Handle delete qualification
     */
    const handleDelete = (row) => {
      if (row.certificateCount > 0) {
        ElMessage.warning('该资格下有证书，无法删除')
        return
      }

      ElMessageBox.confirm(`确定要删除资格 ${row.qualificationName} 吗？`, '删除确认', {
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
     * Handle save qualification
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
        qualificationId: '',
        qualificationCode: '',
        qualificationName: '',
        level: '',
        profession: '',
        certificateType: '',
        validPeriod: 5,
        examRequired: true,
        examSubjects: '',
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
      requirementsDialogVisible,
      dialogMode,
      form,
      rules,
      selectedQualification,
      formRef,
      loadTableData,
      handleSearch,
      handleCreate,
      handleView,
      handleEdit,
      handleViewRequirements,
      handleDelete,
      handleSave,
      handleSizeChange,
      handleCurrentChange
    }
  }
}
</script>

<style scoped>
.qualification-list-container {
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
