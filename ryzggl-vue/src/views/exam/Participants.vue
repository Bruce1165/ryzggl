<template>
  <div class="exam-participants-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>参考人员</h2>
      <div class="breadcrumb">当前位置: 考试管理 &gt; 参考人员</div>
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
      <el-form-item label="参考状态">
        <el-select v-model="queryParams.status" placeholder="选择状态" clearable style="width: 150px">
          <el-option label="全部" value="" />
          <el-option label="已报名" value="已报名" />
          <el-option label="已参考" value="已参考" />
          <el-option label="缺考" value="缺考" />
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="Search" @click="handleSearch">查询</el-button>
        <el-button type="success" icon="Download" @click="handleExport">导出</el-button>
      </el-form-item>
    </el-form>

    <!-- Summary Cards -->
    <el-row :gutter="20" class="stats-row">
      <el-col :span="6">
        <div class="stat-card">
          <div class="stat-value">{{ stats.total }}</div>
          <div class="stat-label">总报名人数</div>
        </div>
      </el-col>
      <el-col :span="6">
        <div class="stat-card success">
          <div class="stat-value">{{ stats.present }}</div>
          <div class="stat-label">参考人数</div>
        </div>
      </el-col>
      <el-col :span="6">
        <div class="stat-card warning">
          <div class="stat-value">{{ stats.absent }}</div>
          <div class="stat-label">缺考人数</div>
        </div>
      </el-col>
      <el-col :span="6">
        <div class="stat-card primary">
          <div class="stat-value">{{ stats.attendanceRate }}%</div>
          <div class="stat-label">参考率</div>
        </div>
      </el-col>
    </el-row>

    <!-- Data Table -->
    <el-card>
      <el-table
        v-loading="loading"
        :data="tableData"
        stripe
        border
      >
        <el-table-column prop="participantId" label="报名ID" width="80" />
        <el-table-column prop="examName" label="考试名称" width="200" show-overflow-tooltip />
        <el-table-column prop="workerId" label="人员编号" width="100" />
        <el-table-column prop="workerName" label="姓名" width="100" />
        <el-table-column prop="idCard" label="身份证号" width="180" />
        <el-table-column prop="phone" label="联系电话" width="130" />
        <el-table-column prop="examPlace" label="考点" width="150" />
        <el-table-column prop="room" label="考场号" width="100" />
        <el-table-column prop="seat" label="座位号" width="80" />
        <el-table-column prop="status" label="参考状态" width="100">
          <template #default="{row}">
            <el-tag :type="getStatusType(row.status)" effect="dark" size="small">
              {{ row.status }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="signUpTime" label="报名时间" width="160" />
        <el-table-column label="操作" width="180" fixed="right">
          <template #default="{row}">
            <el-button type="primary" size="small" link @click="handleView(row)">
              查看
            </el-button>
            <el-button v-if="row.status === '已报名'" type="warning" size="small" link @click="handleMarkAbsent(row)">
              标记缺考
            </el-button>
            <el-button type="danger" size="small" link @click="handleDelete(row)">
              取消报名
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

    <!-- Participant Detail Dialog -->
    <el-dialog v-model="detailDialogVisible" title="参考人员详情" width="600px">
      <el-descriptions v-if="selectedParticipant" :column="2" border>
        <el-descriptions-item label="报名ID">
          {{ selectedParticipant.participantId }}
        </el-descriptions-item>
        <el-descriptions-item label="考试名称">
          {{ selectedParticipant.examName }}
        </el-descriptions-item>
        <el-descriptions-item label="人员编号">
          {{ selectedParticipant.workerId }}
        </el-descriptions-item>
        <el-descriptions-item label="人员姓名">
          {{ selectedParticipant.workerName }}
        </el-descriptions-item>
        <el-descriptions-item label="身份证号" :span="2">
          {{ selectedParticipant.idCard }}
        </el-descriptions-item>
        <el-descriptions-item label="联系电话">
          {{ selectedParticipant.phone }}
        </el-descriptions-item>
        <el-descriptions-item label="参考状态">
          <el-tag :type="getStatusType(selectedParticipant.status)">
            {{ selectedParticipant.status }}
          </el-tag>
        </el-descriptions-item>
        <el-descriptions-item label="考点" :span="2">
          {{ selectedParticipant.examPlace }}
        </el-descriptions-item>
        <el-descriptions-item label="考场号">
          {{ selectedParticipant.room }}
        </el-descriptions-item>
        <el-descriptions-item label="座位号">
          {{ selectedParticipant.seat }}
        </el-descriptions-item>
        <el-descriptions-item label="报名时间" :span="2">
          {{ selectedParticipant.signUpTime }}
        </el-descriptions-item>
      </el-descriptions>
      <template #footer>
        <el-button @click="detailDialogVisible = false">关闭</el-button>
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
  name: 'ExamParticipants',

  setup() {
    const route = useRoute()
    const loading = ref(false)
    const tableData = ref([])
    const examList = ref([
      { examId: 1, examName: '2024年二级造价工程师考试' },
      { examId: 2, examName: '2024年一级造价工程师考试' }
    ])
    const detailDialogVisible = ref(false)
    const selectedParticipant = ref(null)

    const queryParams = reactive({
      examId: '',
      workerId: '',
      workerName: '',
      status: ''
    })

    const stats = reactive({
      total: 0,
      present: 0,
      absent: 0,
      attendanceRate: 0
    })

    const page = reactive({
      current: 1,
      size: 10,
      total: 0
    })

    onMounted(() => {
      // Set examId from query parameters if available
      if (route.query.examId) {
        queryParams.examId = route.query.examId
      }
      loadTableData()
    })

    /**
     * Load participant data
     */
    const loadTableData = async () => {
      loading.value = true
      try {
        // TODO: Implement with actual API call to get exam participants
        // This would typically call an endpoint like getExamParticipants
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
          { participantId: 1, examId: 1, examName: '2024年二级造价工程师考试', workerId: 'W001', workerName: '张三', idCard: '110101199001011234', phone: '13800138000', examPlace: '北京市第一中学', room: '第一考场', seat: 'A01', status: '已参考', signUpTime: '2024-02-15 10:00:00' },
          { participantId: 2, examId: 1, examName: '2024年二级造价工程师考试', workerId: 'W002', workerName: '李四', idCard: '110101199002022345', phone: '13800138001', examPlace: '北京市第一中学', room: '第一考场', seat: 'A02', status: '已参考', signUpTime: '2024-02-15 10:30:00' },
          { participantId: 3, examId: 1, examName: '2024年二级造价工程师考试', workerId: 'W003', workerName: '王五', idCard: '110101199003033456', phone: '13800138002', examPlace: '北京市第一中学', room: '第一考场', seat: 'A03', status: '缺考', signUpTime: '2024-02-15 11:00:00' }
        ]

        page.total = tableData.value.length

        // Calculate stats
        stats.total = tableData.value.length
        stats.present = tableData.value.filter(item => item.status === '已参考').length
        stats.absent = tableData.value.filter(item => item.status === '缺考').length
        stats.attendanceRate = stats.total > 0 ? Math.round(stats.present / stats.total * 100) : 0

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
        // TODO: Implement export functionality for participant list
        // This could export to Excel, PDF, or CSV format
        ElMessage.info('导出功能待实现，需要实现导出参考人员列表的API接口')
      } catch (error) {
        ElMessage.error('导出失败: ' + error.message)
      }
    }

    /**
     * Handle view detail
     */
    const handleView = (row) => {
      selectedParticipant.value = row
      detailDialogVisible.value = true
    }

    /**
     * Handle mark absent
     */
    const handleMarkAbsent = (row) => {
      ElMessageBox.confirm(
        `确定要标记 ${row.workerName} 缺考吗？此操作不可恢复。`,
        '标记缺考',
        {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning',
          dangerouslyUseHTMLString: true
        }
      ).then(async () => {
        try {
          // TODO: Implement with actual API call to mark participant as absent
          ElMessage.info('标记缺考功能待实现，需要实现标记缺考的API接口')
          loadTableData()
        } catch (error) {
          ElMessage.error('标记失败: ' + error.message)
        }
      }).catch(() => {
        ElMessage.info('已取消标记')
      })
    }

    /**
     * Handle delete
     */
    const handleDelete = (row) => {
      ElMessageBox.confirm(
        `确定要取消 ${row.workerName} 的报名吗？此操作不可恢复。`,
        '取消报名',
        {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning',
          dangerouslyUseHTMLString: true
        }
      ).then(async () => {
        try {
          // TODO: Implement with actual API call to cancel participant registration
          ElMessage.info('取消报名功能待实现，需要实现取消报名的API接口')
          loadTableData()
        } catch (error) {
          ElMessage.error('取消失败: ' + error.message)
        }
      }).catch(() => {
        ElMessage.info('已取消操作')
      })
    }

    /**
     * Get status type
     */
    const getStatusType = (status) => {
      const statusMap = {
        '已报名': 'primary',
        '已参考': 'success',
        '缺考': 'danger'
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
      stats,
      page,
      detailDialogVisible,
      selectedParticipant,
      loadTableData,
      handleSearch,
      handleExport,
      handleView,
      handleMarkAbsent,
      handleDelete,
      getStatusType,
      handleSizeChange,
      handleCurrentChange
    }
  }
}
</script>

<style scoped>
.exam-participants-container {
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

.stats-row {
  margin-bottom: 20px;
}

.stat-card {
  padding: 20px;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  border-radius: 8px;
  text-align: center;
  color: white;
}

.stat-card.success {
  background: linear-gradient(135deg, #67c23a 0%, #85ce61 100%);
}

.stat-card.warning {
  background: linear-gradient(135deg, #e6a23c 0%, #ebb563 100%);
}

.stat-card.primary {
  background: linear-gradient(135deg, #409eff 0%, #66b1ff 100%);
}

.stat-value {
  font-size: 28px;
  font-weight: bold;
  margin-bottom: 10px;
}

.stat-label {
  font-size: 14px;
  opacity: 0.9;
}

.pagination-container {
  margin-top: 20px;
  display: flex;
  justify-content: center;
}
</style>
