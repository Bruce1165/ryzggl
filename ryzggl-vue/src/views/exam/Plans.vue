<template>
  <div class="exam-plans-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>考试计划</h2>
      <div class="breadcrumb">当前位置: 考试管理 &gt; 考试计划</div>
    </div>

    <!-- Search and Filter -->
    <el-form :inline="true" :model="queryParams" class="search-form">
      <el-form-item label="计划名称">
        <el-input v-model="queryParams.planName" placeholder="输入计划名称" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="考试类型">
        <el-select v-model="queryParams.examType" placeholder="选择考试类型" clearable style="width: 150px">
          <el-option label="全部" value="" />
          <el-option label="资格认定" value="资格认定" />
          <el-option label="继续教育" value="继续教育" />
          <el-option label="考核评定" value="考核评定" />
        </el-select>
      </el-form-item>
      <el-form-item label="年份">
        <el-date-picker
          v-model="queryParams.year"
          type="year"
          placeholder="选择年份"
          style="width: 150px"
          value-format="YYYY"
        />
      </el-form-item>
      <el-form-item label="状态">
        <el-select v-model="queryParams.status" placeholder="选择状态" clearable style="width: 120px">
          <el-option label="全部" value="" />
          <el-option label="草稿" value="草稿" />
          <el-option label="已发布" value="已发布" />
          <el-option label="进行中" value="进行中" />
          <el-option label="已结束" value="已结束" />
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="Search" @click="handleSearch">查询</el-button>
        <el-button type="success" icon="Plus" @click="handleCreate">新增计划</el-button>
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
        <el-table-column prop="planId" label="计划ID" width="80" />
        <el-table-column prop="planName" label="计划名称" width="200" show-overflow-tooltip />
        <el-table-column prop="examType" label="考试类型" width="120">
          <template #default="{row}">
            <el-tag type="primary" size="small">{{ row.examType }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="year" label="年份" width="100" />
        <el-table-column prop="startDate" label="开始日期" width="120" />
        <el-table-column prop="endDate" label="结束日期" width="120" />
        <el-table-column prop="examCount" label="考试场次" width="100" align="center">
          <template #default="{row}">
            <el-tag type="info">{{ row.examCount }}</el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="expectedParticipants" label="预计人数" width="100" align="center" />
        <el-table-column prop="actualParticipants" label="实际人数" width="100" align="center" />
        <el-table-column prop="status" label="状态" width="100">
          <template #default="{row}">
            <el-tag :type="getStatusType(row.status)" effect="dark" size="small">
              {{ row.status }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="createTime" label="创建时间" width="160" />
        <el-table-column label="操作" width="280" fixed="right">
          <template #default="{row}">
            <el-button type="primary" size="small" link @click="handleView(row)">
              查看
            </el-button>
            <el-button v-if="row.status === '草稿'" type="primary" size="small" link @click="handleEdit(row)">
              编辑
            </el-button>
            <el-button v-if="row.status === '草稿'" type="success" size="small" link @click="handlePublish(row)">
              发布
            </el-button>
            <el-button type="info" size="small" link @click="handleViewExams(row)">
              考试安排
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

    <!-- Plan Detail Dialog -->
    <el-dialog v-model="detailDialogVisible" :title="dialogMode === 'create' ? '新增考试计划' : '编辑考试计划'" width="800px">
      <el-form ref="formRef" :model="form" :rules="rules" label-width="120px">
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="计划名称" prop="planName">
              <el-input v-model="form.planName" placeholder="输入计划名称" />
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
            <el-form-item label="年份" prop="year">
              <el-date-picker
                v-model="form.year"
                type="year"
                placeholder="选择年份"
                style="width: 100%"
                value-format="YYYY"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="计划周期" prop="period">
              <el-select v-model="form.period" placeholder="选择计划周期" style="width: 100%">
                <el-option label="上半年" value="上半年" />
                <el-option label="下半年" value="下半年" />
                <el-option label="全年" value="全年" />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="开始日期" prop="startDate">
              <el-date-picker
                v-model="form.startDate"
                type="date"
                placeholder="选择开始日期"
                style="width: 100%"
                value-format="YYYY-MM-DD"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="结束日期" prop="endDate">
              <el-date-picker
                v-model="form.endDate"
                type="date"
                placeholder="选择结束日期"
                style="width: 100%"
                value-format="YYYY-MM-DD"
              />
            </el-form-item>
          </el-col>
        </el-row>

        <el-form-item label="计划说明" prop="description">
          <el-input
            v-model="form.description"
            type="textarea"
            :rows="4"
            placeholder="输入计划说明"
          />
        </el-form-item>

        <el-divider>考试安排</el-divider>

        <el-table :data="examSchedule" border style="margin-bottom: 20px;">
          <el-table-column prop="examName" label="考试名称" />
          <el-table-column prop="examDate" label="考试日期" width="120" />
          <el-table-column prop="examTime" label="考试时间" width="120" />
          <el-table-column label="操作" width="100">
            <template #default="{ $index }">
              <el-button type="danger" size="small" link @click="handleRemoveExam($index)">删除</el-button>
            </template>
          </el-table-column>
        </el-table>

        <el-button type="dashed" style="width: 100%;" @click="handleAddExam">
          <el-icon><Plus /></el-icon> 添加考试场次
        </el-button>
      </el-form>
      <template #footer>
        <el-button @click="detailDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="handleSave">确定</el-button>
      </template>
    </el-dialog>

    <!-- Exam Arrangement Dialog -->
    <el-dialog v-model="examsDialogVisible" title="考试安排" width="900px">
      <el-table :data="examArrangements" stripe border>
        <el-table-column prop="examName" label="考试名称" width="200" />
        <el-table-column prop="examDate" label="考试日期" width="120" />
        <el-table-column prop="examTime" label="考试时间" width="120" />
        <el-table-column prop="examPlace" label="考试地点" width="150" />
        <el-table-column prop="signupCount" label="报名人数" width="100" align="center" />
        <el-table-column prop="maxParticipants" label="最大人数" width="100" align="center" />
      </el-table>
      <template #footer>
        <el-button @click="examsDialogVisible = false">关闭</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script>
import { ref, reactive, onMounted } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import { Plus } from '@element-plus/icons-vue'
import { getExamPlanList } from '@/api/exam'

export default {
  name: 'ExamPlans',

  components: {
    Plus
  },

  setup() {
    const loading = ref(false)
    const tableData = ref([])
    const detailDialogVisible = ref(false)
    const examsDialogVisible = ref(false)
    const dialogMode = ref('create')
    const formRef = ref(null)
    const selectedPlan = ref(null)
    const examArrangements = ref([])

    const queryParams = reactive({
      planName: '',
      examType: '',
      year: '',
      status: ''
    })

    const page = reactive({
      current: 1,
      size: 10,
      total: 0
    })

    const form = reactive({
      planId: '',
      planName: '',
      examType: '',
      year: '',
      period: '',
      startDate: '',
      endDate: '',
      description: ''
    })

    const rules = {
      planName: [{ required: true, message: '请输入计划名称', trigger: 'blur' }],
      examType: [{ required: true, message: '请选择考试类型', trigger: 'change' }],
      year: [{ required: true, message: '请选择年份', trigger: 'change' }],
      period: [{ required: true, message: '请选择计划周期', trigger: 'change' }],
      startDate: [{ required: true, message: '请选择开始日期', trigger: 'change' }],
      endDate: [{ required: true, message: '请选择结束日期', trigger: 'change' }],
      description: [{ required: true, message: '请输入计划说明', trigger: 'blur' }]
    }

    const examSchedule = ref([])

    onMounted(() => {
      loadTableData()
    })

    /**
     * Load exam plan data
     */
    const loadTableData = async () => {
      loading.value = true
      try {
        // TODO: Implement with actual API call to get exam plans
        const res = await getExamPlanList({
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
        // Fallback to mock data for development
        tableData.value = [
          { planId: 1, planName: '2024年上半年资格认定计划', examType: '资格认定', year: '2024', period: '上半年', startDate: '2024-01-01', endDate: '2024-06-30', examCount: 2, expectedParticipants: 500, actualParticipants: 456, status: '已结束', createTime: '2024-01-01 00:00:00' },
          { planId: 2, planName: '2024年下半年继续教育计划', examType: '继续教育', year: '2024', period: '下半年', startDate: '2024-07-01', endDate: '2024-12-31', examCount: 3, expectedParticipants: 600, actualParticipants: 0, status: '已发布', createTime: '2024-06-15 00:00:00' },
          { planId: 3, planName: '2025年度考核评定计划', examType: '考核评定', year: '2025', period: '全年', startDate: '2025-01-01', endDate: '2025-12-31', examCount: 4, expectedParticipants: 800, actualParticipants: 0, status: '草稿', createTime: '2024-12-01 00:00:00' }
        ]
        page.total = 3
        ElMessage.warning('使用模拟数据，请实现真实的API接口')
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
     * Handle create plan
     */
    const handleCreate = () => {
      dialogMode.value = 'create'
      resetForm()
      detailDialogVisible.value = true
    }

    /**
     * Handle view plan
     */
    const handleView = (row) => {
      dialogMode.value = 'view'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle edit plan
     */
    const handleEdit = (row) => {
      dialogMode.value = 'edit'
      Object.assign(form, row)
      detailDialogVisible.value = true
    }

    /**
     * Handle publish plan
     */
    const handlePublish = (row) => {
      ElMessageBox.confirm(
        `确定要发布计划 ${row.planName} 吗？发布后将开放报名，此操作不可撤销。`,
        '发布计划',
        {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning',
          dangerouslyUseHTMLString: true
        }
      ).then(async () => {
        try {
          // TODO: Implement with actual API call to publish exam plan
          ElMessage.info('发布功能待实现，需要实现发布考试计划的API接口')
          loadTableData()
        } catch (error) {
          ElMessage.error('发布失败: ' + error.message)
        }
      }).catch(() => {
        ElMessage.info('已取消发布')
      })
    }

    /**
     * Handle view exams
     */
    const handleViewExams = (row) => {
      selectedPlan.value = row
      // TODO: Implement with actual API call to get exam arrangements for this plan
      examArrangements.value = [
        { examName: '二级造价工程师考试', examDate: '2024-03-10', examTime: '09:00-11:30', examPlace: '北京市第一中学', signupCount: 856, maxParticipants: 1000 },
        { examName: '二级造价工程师补考', examDate: '2024-04-15', examTime: '14:00-16:30', examPlace: '北京市第二职业中学', signupCount: 234, maxParticipants: 500 }
      ]
      examsDialogVisible.value = true
    }

    /**
     * Handle delete plan
     */
    const handleDelete = (row) => {
      if (row.status !== '草稿') {
        ElMessage.warning('只有草稿状态的计划可以删除')
        return
      }

      ElMessageBox.confirm(
        `确定要删除计划 ${row.planName} 吗？此操作不可恢复。`,
        '删除确认',
        {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning',
          dangerouslyUseHTMLString: true
        }
      ).then(async () => {
        try {
          // TODO: Implement with actual API call to delete exam plan
          ElMessage.info('删除功能待实现，需要实现删除考试计划的API接口')
          loadTableData()
        } catch (error) {
          ElMessage.error('删除失败: ' + error.message)
        }
      }).catch(() => {
        ElMessage.info('已取消删除')
      })
    }

    /**
     * Handle save plan
     */
    const handleSave = async () => {
      if (!formRef.value) return

      try {
        const valid = await formRef.value.validate()
        if (!valid) return

        // TODO: Implement with actual API call to create/update exam plan
        // This would typically call endpoints like createExamPlan or updateExamPlan
        ElMessage.info('保存功能待实现，需要实现考试计划管理的API接口')

        ElMessage.success(dialogMode.value === 'create' ? '创建成功' : '更新成功')
        detailDialogVisible.value = false
        loadTableData()
      } catch (error) {
        ElMessage.error('保存失败: ' + error.message)
      }
    }

    /**
     * Handle add exam
     */
    const handleAddExam = () => {
      examSchedule.value.push({
        examName: '',
        examDate: '',
        examTime: ''
      })
    }

    /**
     * Handle remove exam
     */
    const handleRemoveExam = (index) => {
      examSchedule.value.splice(index, 1)
    }

    /**
     * Reset form
     */
    const resetForm = () => {
      Object.assign(form, {
        planId: '',
        planName: '',
        examType: '',
        year: '',
        period: '',
        startDate: '',
        endDate: '',
        description: ''
      })
      examSchedule.value = []
    }

    /**
     * Get status type
     */
    const getStatusType = (status) => {
      const statusMap = {
        '草稿': 'info',
        '已发布': 'success',
        '进行中': 'warning',
        '已结束': 'primary'
      }
      return statusMap[status] || 'info'
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
      examsDialogVisible,
      dialogMode,
      form,
      rules,
      examSchedule,
      examArrangements,
      formRef,
      loadTableData,
      handleSearch,
      handleCreate,
      handleView,
      handleEdit,
      handlePublish,
      handleViewExams,
      handleDelete,
      handleSave,
      handleAddExam,
      handleRemoveExam,
      getStatusType,
      handleSizeChange,
      handleCurrentChange
    }
  }
}
</script>

<style scoped>
.exam-plans-container {
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
