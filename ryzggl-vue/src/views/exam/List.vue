<template>
  <div class="exam-list-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>考试管理</h2>
      <div class="breadcrumb">当前位置: 考试管理 &gt; 考试列表</div>
    </div>

    <!-- Search and Filter -->
    <el-form :inline="true" :model="queryParams" class="search-form">
      <el-form-item label="考试名称">
        <el-input v-model="queryParams.examName" placeholder="输入考试名称" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="考试类型">
        <el-select v-model="queryParams.examType" placeholder="选择考试类型" clearable style="width: 150px">
          <el-option label="全部" value="" />
          <el-option label="资格认定" value="资格认定" />
          <el-option label="继续教育" value="继续教育" />
          <el-option label="考核评定" value="考核评定" />
        </el-select>
      </el-form-item>
      <el-form-item label="考试状态">
        <el-select v-model="queryParams.examStatus" placeholder="选择考试状态" clearable style="width: 150px">
          <el-option label="全部" value="" />
          <el-option label="未开始" value="未开始" />
          <el-option label="报名中" value="报名中" />
          <el-option label="考试中" value="考试中" />
          <el-option label="已结束" value="已结束" />
        </el-select>
      </el-form-item>
      <el-form-item label="考试日期">
        <el-date-picker
          v-model="queryParams.examDate"
          type="date"
          placeholder="选择考试日期"
          style="width: 180px"
          value-format="YYYY-MM-DD"
        />
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="Search" @click="handleSearch">查询</el-button>
        <el-button icon="Refresh" @click="handleReset">重置</el-button>
        <el-button type="success" icon="Plus" @click="handleCreate">新增考试</el-button>
      </el-form-item>
    </el-form>

    <!-- Data Table -->
    <el-table
      v-loading="loading"
      :data="tableData"
      stripe
      border
      @sort-change="handleSortChange"
    >
      <el-table-column type="selection" width="55" />
      <el-table-column prop="examId" label="考试ID" width="80" />
      <el-table-column prop="examName" label="考试名称" width="200" show-overflow-tooltip />
      <el-table-column prop="examType" label="考试类型" width="100">
        <template #default="{row}">
          <el-tag type="primary" size="small">{{ row.examType }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="examDate" label="考试日期" width="120" />
      <el-table-column prop="examTime" label="考试时间" width="120" />
      <el-table-column prop="examPlace" label="考试地点" width="150" show-overflow-tooltip />
      <el-table-column prop="examStatus" label="考试状态" width="100">
        <template #default="{row}">
          <el-tag :type="getStatusType(row.examStatus)" effect="dark" size="small">
            {{ row.examStatus }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="signupCount" label="报名人数" width="90" align="center">
        <template #default="{row}">
          <el-tag :type="getSignupStatusType(row.signupCount, row.maxParticipants)" size="small">
            {{ row.signupCount }}/{{ row.maxParticipants }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="createTime" label="创建时间" width="160" />
      <el-table-column label="操作" width="280" fixed="right">
        <template #default="{row}">
          <el-button type="primary" size="small" link @click="handleView(row)">
            查看
          </el-button>
          <el-button v-if="row.examStatus === '报名中'" type="success" size="small" link @click="handleSignUp(row)">
            报名
          </el-button>
          <el-button type="info" size="small" link @click="handleResults(row)">
            成绩
          </el-button>
          <el-button type="warning" size="small" link @click="handleEdit(row)">
            编辑
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

    <!-- Create/Edit Dialog -->
    <el-dialog v-model="dialogVisible" :title="dialogMode === 'create' ? '新增考试' : '编辑考试'" width="800px">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="120px">
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="考试名称" prop="examName">
              <el-input v-model="form.examName" placeholder="输入考试名称" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="考试类型" prop="examType">
              <el-select v-model="form.examType" placeholder="选择考试类型" style="width: 100%">
                <el-option label="资格认定" value="资格认定" />
                <el-option label="继续教育" value="继续教育" />
                <el-option label="考核评定" value="考核评定" />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="考试日期" prop="examDate">
              <el-date-picker
                v-model="form.examDate"
                type="date"
                placeholder="选择考试日期"
                style="width: 100%"
                value-format="YYYY-MM-DD"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="考试时间" prop="examTime">
              <el-input v-model="form.examTime" placeholder="例如: 09:00-11:30" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-form-item label="考试地点" prop="examPlace">
          <el-input v-model="form.examPlace" placeholder="输入考试地点" />
        </el-form-item>

        <el-row :gutter="20">
          <el-col :span="8">
            <el-form-item label="总分" prop="totalScore">
              <el-input-number v-model="form.totalScore" :min="0" :max="1000" style="width: 100%" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="及格分数" prop="passScore">
              <el-input-number v-model="form.passScore" :min="0" :max="form.totalScore || 100" style="width: 100%" />
            </el-form-item>
          </el-col>
          <el-col :span="8">
            <el-form-item label="考试时长(分)" prop="duration">
              <el-input-number v-model="form.duration" :min="1" :max="480" style="width: 100%" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="最大人数" prop="maxParticipants">
              <el-input-number v-model="form.maxParticipants" :min="1" :max="10000" style="width: 100%" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="及格率要求(%)" prop="minPassRate">
              <el-input-number v-model="form.minPassRate" :min="0" :max="100" style="width: 100%" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-form-item label="考试说明" prop="description">
          <el-input
            v-model="form.description"
            type="textarea"
            :rows="3"
            placeholder="输入考试说明"
          />
        </el-form-item>

        <el-form-item label="备注">
          <el-input
            v-model="form.remark"
            type="textarea"
            :rows="2"
            placeholder="输入备注信息"
          />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="dialogVisible = false">取消</el-button>
        <el-button type="primary" :loading="submitting" @click="handleSave">确定</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script>
import { ref, reactive, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import { getExamList, deleteExam, createExam, updateExam } from '@/api/exam'

export default {
  name: 'ExamList',

  setup() {
    const router = useRouter()
    const loading = ref(false)
    const tableData = ref([])
    const dialogVisible = ref(false)
    const dialogMode = ref('create')
    const formRef = ref(null)
    const submitting = ref(false)

    const queryParams = reactive({
      examName: '',
      examType: '',
      examStatus: '',
      examDate: ''
    })

    const page = reactive({
      current: 1,
      size: 10,
      total: 0
    })

    const form = reactive({
      examId: '',
      examName: '',
      examType: '',
      examDate: '',
      examTime: '',
      examPlace: '',
      totalScore: 100,
      passScore: 60,
      duration: 120,
      maxParticipants: 100,
      minPassRate: 60,
      description: '',
      remark: ''
    })

    const rules = {
      examName: [{ required: true, message: '请输入考试名称', trigger: 'blur' }],
      examType: [{ required: true, message: '请选择考试类型', trigger: 'change' }],
      examDate: [{ required: true, message: '请选择考试日期', trigger: 'change' }],
      examTime: [{ required: true, message: '请输入考试时间', trigger: 'blur' }],
      examPlace: [{ required: true, message: '请输入考试地点', trigger: 'blur' }],
      totalScore: [{ required: true, message: '请输入总分', trigger: 'blur' }],
      passScore: [{ required: true, message: '请输入及格分数', trigger: 'blur' }],
      duration: [{ required: true, message: '请输入考试时长', trigger: 'blur' }],
      maxParticipants: [{ required: true, message: '请输入最大人数', trigger: 'blur' }],
      description: [{ required: true, message: '请输入考试说明', trigger: 'blur' }]
    }

    onMounted(() => {
      loadTableData()
    })

    /**
     * Load exam list
     */
    const loadTableData = async () => {
      loading.value = true
      try {
        const res = await getExamList({
          current: page.current,
          size: page.size,
          ...queryParams
        })

        if (res.code === 0) {
          tableData.value = res.data.records || []
          page.total = res.data.total || 0
        } else {
          ElMessage.error(res.message || '加载数据失败')
        }
      } catch (error) {
        ElMessage.error('加载数据失败: ' + error.message)
      } finally {
        loading.value = false
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
     * Handle reset
     */
    const handleReset = () => {
      Object.assign(queryParams, {
        examName: '',
        examType: '',
        examStatus: '',
        examDate: ''
      })
      page.current = 1
      loadTableData()
    }

    /**
     * Handle create exam
     */
    const handleCreate = () => {
      dialogMode.value = 'create'
      resetForm()
      dialogVisible.value = true
    }

    /**
     * Handle edit exam
     */
    const handleEdit = (row) => {
      dialogMode.value = 'edit'
      Object.assign(form, row)
      dialogVisible.value = true
    }

    /**
     * Handle save exam
     */
    const handleSave = async () => {
      if (!formRef.value) return

      try {
        const valid = await formRef.value.validate()
        if (!valid) return

        submitting.value = true

        const apiCall = dialogMode.value === 'create' ? createExam : updateExam
        const params = dialogMode.value === 'create' ? form : { ...form, examId: form.examId }

        const res = await apiCall(params)

        if (res.code === 0) {
          ElMessage.success(dialogMode.value === 'create' ? '创建成功' : '更新成功')
          dialogVisible.value = false
          loadTableData()
        } else {
          ElMessage.error(res.message || '操作失败')
        }
      } catch (error) {
        ElMessage.error('操作失败: ' + error.message)
      } finally {
        submitting.value = false
      }
    }

    /**
     * Handle view exam
     */
    const handleView = (row) => {
      router.push(`/exam/${row.examId}/detail`)
    }

    /**
     * Handle sign up
     */
    const handleSignUp = (row) => {
      router.push({
        path: '/exam/sign-up',
        query: { examId: row.examId }
      })
    }

    /**
     * Handle view results
     */
    const handleResults = (row) => {
      router.push({
        path: '/exam/results',
        query: { examId: row.examId }
      })
    }

    /**
     * Handle delete exam
     */
    const handleDelete = (row) => {
      if (row.signupCount > 0) {
        ElMessage.warning('该考试已有报名人员，无法删除')
        return
      }

      ElMessageBox.confirm(`确定要删除考试 ${row.examName} 吗？`, '删除确认', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(async () => {
        try {
          const res = await deleteExam(row.examId)

          if (res.code === 0) {
            ElMessage.success('删除成功')
            loadTableData()
          } else {
            ElMessage.error(res.message || '删除失败')
          }
        } catch (error) {
          ElMessage.error('删除失败: ' + error.message)
        }
      }).catch(() => {
        ElMessage.info('已取消删除')
      })
    }

    /**
     * Get status type
     */
    const getStatusType = (status) => {
      const statusMap = {
        '未开始': 'info',
        '报名中': 'primary',
        '考试中': 'warning',
        '已结束': 'success'
      }
      return statusMap[status] || 'info'
    }

    /**
     * Get signup status type
     */
    const getSignupStatusType = (current, max) => {
      const ratio = current / max
      if (ratio >= 1) return 'danger'
      if (ratio >= 0.8) return 'warning'
      if (ratio >= 0.5) return 'primary'
      return 'success'
    }

    /**
     * Reset form
     */
    const resetForm = () => {
      Object.assign(form, {
        examId: '',
        examName: '',
        examType: '',
        examDate: '',
        examTime: '',
        examPlace: '',
        totalScore: 100,
        passScore: 60,
        duration: 120,
        maxParticipants: 100,
        minPassRate: 60,
        description: '',
        remark: ''
      })
    }

    /**
     * Handle sort change
     */
    const handleSortChange = ({ prop, order }) => {
      console.log('Sort:', prop, order)
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
      dialogVisible,
      dialogMode,
      form,
      rules,
      formRef,
      submitting,
      handleSearch,
      handleReset,
      handleCreate,
      handleEdit,
      handleSave,
      handleView,
      handleSignUp,
      handleResults,
      handleDelete,
      getStatusType,
      getSignupStatusType,
      handleSortChange,
      handleSizeChange,
      handleCurrentChange
    }
  }
}
</script>

<style scoped>
.exam-list-container {
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
