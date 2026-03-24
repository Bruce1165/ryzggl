<template>
  <div class="exam-results-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>考试成绩</h2>
      <div class="breadcrumb">当前位置: 考试管理 &gt; 考试成绩</div>
    </div>

    <!-- Search and Filter -->
    <el-form :inline="true" :model="queryParams" class="search-form">
      <el-form-item label="考试">
        <el-select v-model="queryParams.examId" placeholder="选择考试" clearable style="width: 200px">
          <el-option
            v-for="exam in examList"
            :key="exam.examId"
            :label="exam.examName"
            :value="exam.examId"
          />
        </el-select>
      </el-form-item>
      <el-form-item label="人员编号">
        <el-input v-model="queryParams.workerId" placeholder="输入人员编号" clearable style="width: 150px" />
      </el-form-item>
      <el-form-item label="姓名">
        <el-input v-model="queryParams.workerName" placeholder="输入姓名" clearable style="width: 150px" />
      </el-form-item>
      <el-form-item label="成绩状态">
        <el-select v-model="queryParams.resultStatus" placeholder="选择状态" clearable style="width: 150px">
          <el-option label="全部" value="" />
          <el-option label="及格" value="及格" />
          <el-option label="不及格" value="不及格" />
          <el-option label="缺考" value="缺考" />
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="Search" @click="handleSearch">查询</el-button>
        <el-button type="success" icon="Download" @click="handleExport">导出</el-button>
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
        <el-table-column prop="examId" label="考试ID" width="80" />
        <el-table-column prop="examName" label="考试名称" width="200" show-overflow-tooltip />
        <el-table-column prop="workerId" label="人员编号" width="100" />
        <el-table-column prop="workerName" label="姓名" width="100" />
        <el-table-column prop="idCard" label="身份证号" width="180" />
        <el-table-column prop="score" label="考试分数" width="100" align="center">
          <template #default="{row}">
            <span :style="{ color: row.score >= row.passScore ? '#67c23a' : '#f56c6c', fontWeight: 'bold' }">
              {{ row.score }}
            </span>
          </template>
        </el-table-column>
        <el-table-column prop="passScore" label="及格分数" width="100" align="center" />
        <el-table-column prop="resultStatus" label="成绩状态" width="100">
          <template #default="{row}">
            <el-tag :type="getStatusType(row.resultStatus)" effect="dark" size="small">
              {{ row.resultStatus }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="examDate" label="考试日期" width="120" />
        <el-table-column prop="recordTime" label="记录时间" width="160" />
        <el-table-column label="操作" width="150" fixed="right">
          <template #default="{row}">
            <el-button type="primary" size="small" link @click="handleView(row)">
              查看
            </el-button>
            <el-button v-if="row.resultStatus === '缺考'" type="success" size="small" link @click="handleEditScore(row)">
              补录
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

    <!-- Score Detail Dialog -->
    <el-dialog v-model="detailDialogVisible" title="成绩详情" width="600px">
      <el-descriptions v-if="selectedResult" :column="2" border>
        <el-descriptions-item label="考试名称" :span="2">
          {{ selectedResult.examName }}
        </el-descriptions-item>
        <el-descriptions-item label="人员编号">
          {{ selectedResult.workerId }}
        </el-descriptions-item>
        <el-descriptions-item label="人员姓名">
          {{ selectedResult.workerName }}
        </el-descriptions-item>
        <el-descriptions-item label="身份证号" :span="2">
          {{ selectedResult.idCard }}
        </el-descriptions-item>
        <el-descriptions-item label="考试分数">
          <span :style="{ color: selectedResult.score >= selectedResult.passScore ? '#67c23a' : '#f56c6c', fontSize: '18px', fontWeight: 'bold' }">
            {{ selectedResult.score }}
          </span>
        </el-descriptions-item>
        <el-descriptions-item label="及格分数">
          {{ selectedResult.passScore }}
        </el-descriptions-item>
        <el-descriptions-item label="成绩状态" :span="2">
          <el-tag :type="getStatusType(selectedResult.resultStatus)" effect="dark">
            {{ selectedResult.resultStatus }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="考试日期" :span="2">
          {{ selectedResult.examDate }}
        </el-descriptions-item>
        <el-descriptions-item label="记录时间" :span="2">
          {{ selectedResult.recordTime }}
        </el-descriptions-item>
      </el-descriptions>
      <template #footer>
        <el-button @click="detailDialogVisible = false">关闭</el-button>
      </template>
    </el-dialog>

    <!-- Edit Score Dialog -->
    <el-dialog v-model="editScoreDialogVisible" title="补录成绩" width="500px">
      <el-form :model="editForm" label-width="100px">
        <el-form-item label="人员姓名">
          <el-input :value="selectedResult?.workerName" disabled />
        </el-form-item>
        <el-form-item label="考试分数" prop="score">
          <el-input-number v-model="editForm.score" :min="0" :max="100" :precision="1" />
        </el-form-item>
        <el-form-item label="备注">
          <el-input
            v-model="editForm.remark"
            type="textarea"
            :rows="3"
            placeholder="输入备注信息"
          />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="editScoreDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="confirmEditScore">确定</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script>
import { ref, reactive, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import { getExamResult } from '@/api/exam'

export default {
  name: 'ExamResults',

  setup() {
    const route = useRoute()
    const loading = ref(false)
    const tableData = ref([])
    const examList = ref([
      { examId: 1, examName: '2024年二级造价工程师考试' },
      { examId: 2, examName: '2024年一级造价工程师考试' }
    ])
    const detailDialogVisible = ref(false)
    const editScoreDialogVisible = ref(false)
    const selectedResult = ref(null)

    const queryParams = reactive({
      examId: '',
      workerId: '',
      workerName: '',
      resultStatus: ''
    })

    const page = reactive({
      current: 1,
      size: 10,
      total: 0
    })

    const editForm = reactive({
      score: 0,
      remark: ''
    })

    // Mock data
    onMounted(() => {
      // Set examId from query parameters if available
      if (route.query.examId) {
        queryParams.examId = route.query.examId
      }
      loadTableData()
    })

    /**
     * Load result data
     */
    const loadTableData = async () => {
      loading.value = true
      try {
        // TODO: Implement with actual API call to get exam results
        // This would typically call an endpoint like getExamResults
        const res = await getExamResult(queryParams.examId, queryParams.workerId)

        if (res.code === 0) {
          tableData.value = res.data.records || []
          page.total = res.data.total || 0
        } else {
          ElMessage.error(res.message || '加载数据失败')
        }
      } catch (error) {
        // Fallback to mock data for development
        tableData.value = [
          { examId: 1, examName: '2024年二级造价工程师考试', workerId: 'W001', workerName: '张三', idCard: '110101199001011234', score: 85, passScore: 60, resultStatus: '及格', examDate: '2024-03-10', recordTime: '2024-03-11 10:00:00' },
          { examId: 1, examName: '2024年二级造价工程师考试', workerId: 'W002', workerName: '李四', idCard: '110101199002022345', score: 55, passScore: 60, resultStatus: '不及格', examDate: '2024-03-10', recordTime: '2024-03-11 10:00:00' },
          { examId: 1, examName: '2024年二级造价工程师考试', workerId: 'W003', workerName: '王五', idCard: '110101199003033456', score: 0, passScore: 60, resultStatus: '缺考', examDate: '2024-03-10', recordTime: '2024-03-11 10:00:00' }
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
     * Handle export
     */
    const handleExport = () => {
      try {
        // TODO: Implement export functionality
        // This could export to Excel, PDF, or CSV format
        ElMessage.info('导出功能待实现，需要实现导出成绩的API接口')
      } catch (error) {
        ElMessage.error('导出失败: ' + error.message)
      }
    }

    /**
     * Handle view detail
     */
    const handleView = (row) => {
      selectedResult.value = row
      detailDialogVisible.value = true
    }

    /**
     * Handle edit score
     */
    const handleEditScore = (row) => {
      selectedResult.value = row
      editForm.score = row.score
      editForm.remark = ''
      editScoreDialogVisible.value = true
    }

    /**
     * Confirm edit score
     */
    const confirmEditScore = async () => {
      try {
        // Validate score
        if (editForm.score < 0 || editForm.score > selectedResult.value.totalScore) {
          ElMessage.error(`分数必须在0-${selectedResult.value.totalScore}之间`)
          return
        }

        // TODO: Implement with actual API call to update exam result
        // This would typically call an endpoint like updateExamResult
        ElMessage.info('补录成绩功能待实现，需要实现更新成绩的API接口')

        // Update local data for demo purposes
        selectedResult.value.score = editForm.score
        selectedResult.value.resultStatus = editForm.score >= selectedResult.value.passScore ? '及格' : '不及格'
        selectedResult.value.recordTime = new Date().toLocaleString()

        ElMessage.success('成绩更新成功')
        editScoreDialogVisible.value = false
        loadTableData()
      } catch (error) {
        ElMessage.error('更新失败: ' + error.message)
      }
    }

    /**
     * Handle delete
     */
    const handleDelete = (row) => {
      ElMessageBox.confirm(
        `确定要删除 ${row.workerName} 的成绩记录吗？此操作不可恢复。`,
        '删除确认',
        {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning',
          dangerouslyUseHTMLString: true
        }
      ).then(async () => {
        try {
          // TODO: Implement with actual API call to delete exam result
          ElMessage.info('删除功能待实现，需要实现删除成绩的API接口')
          loadTableData()
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
        '及格': 'success',
        '不及格': 'danger',
        '缺考': 'warning'
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
      examList,
      queryParams,
      page,
      detailDialogVisible,
      editScoreDialogVisible,
      selectedResult,
      editForm,
      loadTableData,
      handleSearch,
      handleExport,
      handleView,
      handleEditScore,
      confirmEditScore,
      handleDelete,
      getStatusType,
      handleSizeChange,
      handleCurrentChange
    }
  }
}
</script>

<style scoped>
.exam-results-container {
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
