<template>
  <div class="operate-log-list">
    <el-card class="filter-card">
      <el-form :inline="true" :model="filters">
        <el-form-item label="操作人">
          <el-input v-model="filters.personName" placeholder="请输入操作人" clearable />
        </el-form-item>
        <el-form-item label="操作名称">
          <el-input v-model="filters.operateName" placeholder="请输入操作名称" clearable />
        </el-form-item>
        <el-form-item label="日期范围">
          <el-date-picker
            v-model="dateRange"
            type="daterange"
            range-separator="至"
            start-placeholder="开始日期"
            end-placeholder="结束日期"
            format="YYYY-MM-DD"
            value-format="YYYY-MM-DD"
            style="width: 280px"
          />
        </el-form-item>
        <el-form-item>
          <el-button type="primary" @click="handleSearch">查询</el-button>
          <el-button @click="handleReset">重置</el-button>
          <el-button type="primary" @click="showStatisticsDialog">统计</el-button>
        </el-form-item>
      </el-form>
    </el-card>

    <el-card>
      <el-table :data="tableData" border v-loading="loading">
        <el-table-column type="index" label="序号" width="60" />
        <el-table-column prop="logId" label="日志ID" width="100" />
        <el-table-column prop="logTime" label="操作时间" width="160" />
        <el-table-column prop="personName" label="操作人" width="120" />
        <el-table-column prop="personId" label="人员ID" width="120" />
        <el-table-column prop="operateName" label="操作名称" width="150" />
        <el-table-column prop="logDetail" label="操作详情" min-width="250" show-overflow-tooltip />
        <el-table-column label="操作" width="80" fixed="right">
          <template #default="{ row }">
            <el-button link type="primary" size="small" @click="handleView(row)">查看</el-button>
            <el-button link type="danger" size="small" @click="handleDelete(row)">删除</el-button>
          </template>
        </el-table-column>
      </el-table>

      <div class="pagination-container">
        <el-pagination
          v-model:current-page="pagination.page"
          v-model:page-size="pagination.size"
          :page-sizes="[10, 20, 50, 100]"
          :total="pagination.total"
          layout="total, sizes, prev, pager, next, jumper"
          @size-change="loadData"
          @current-change="loadData"
        />
      </div>
    </el-card>

    <!-- 查看详情对话框 -->
    <el-dialog
      v-model="detailDialogVisible"
      title="日志详情"
      width="600px"
    >
      <el-descriptions :column="1" border>
        <el-descriptions-item label="日志ID">{{ currentLog.logId }}</el-descriptions-item>
        <el-descriptions-item label="操作时间">{{ currentLog.logTime }}</el-descriptions-item>
        <el-descriptions-item label="操作人">{{ currentLog.personName }}</el-descriptions-item>
        <el-descriptions-item label="人员ID">{{ currentLog.personId }}</el-descriptions-item>
        <el-descriptions-item label="操作名称">{{ currentLog.operateName }}</el-descriptions-item>
        <el-descriptions-item label="操作详情">{{ currentLog.logDetail }}</el-descriptions-item>
      </el-descriptions>
      <template #footer>
        <el-button @click="detailDialogVisible = false">关闭</el-button>
      </template>
    </el-dialog>

    <!-- 统计对话框 -->
    <el-dialog
      v-model="statisticsDialogVisible"
      title="操作统计"
      width="800px"
    >
      <el-table :data="statisticsData" border v-loading="statisticsLoading">
        <el-table-column type="index" label="序号" width="60" />
        <el-table-column prop="operateName" label="操作名称" width="200" />
        <el-table-column prop="count" label="操作次数" width="150" align="center" />
        <el-table-column label="占比" width="150">
          <template #default="{ row }">
            <el-progress
              :percentage="((row.count / totalOperations) * 100).toFixed(2)"
              :color="getProgressColor(row.count / totalOperations)"
            />
          </template>
        </el-table-column>
      </el-table>
      <template #footer>
        <el-button @click="statisticsDialogVisible = false">关闭</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script setup>
import { ref, reactive, onMounted, computed } from 'vue'
import { ElMessage, ElMessageBox } from 'element-plus'
import operateLogApi from '@/api/operateLog'

const loading = ref(false)
const detailDialogVisible = ref(false)
const statisticsDialogVisible = ref(false)
const statisticsLoading = ref(false)
const dateRange = ref([])

const filters = reactive({
  personName: '',
  operateName: ''
})

const pagination = reactive({
  page: 1,
  size: 20,
  total: 0
})

const tableData = ref([])
const currentLog = ref({})
const statisticsData = ref([])

const totalOperations = computed(() => {
  return statisticsData.value.reduce((sum, item) => sum + item.count, 0)
})

const getProgressColor = (ratio) => {
  if (ratio > 0.5) return '#f56c6c'
  if (ratio > 0.3) return '#e6a23c'
  return '#67c23a'
}

const loadData = async () => {
  loading.value = true
  try {
    let result
    if (dateRange.value && dateRange.value.length === 2) {
      result = await operateLogApi.getByDateRange(dateRange.value[0], dateRange.value[1])
    } else if (filters.personName) {
      result = await operateLogApi.search(filters.personName)
    } else if (filters.operateName) {
      result = await operateLogApi.getByOperateName(filters.operateName)
    } else {
      result = await operateLogApi.getAll()
    }

    if (result.code === 200) {
      let data = result.data || []
      if (filters.personName || filters.operateName) {
        data = data.filter(item => {
          const matchPerson = !filters.personName || item.personName?.includes(filters.personName)
          const matchOperate = !filters.operateName || item.operateName?.includes(filters.operateName)
          return matchPerson && matchOperate
        })
      }
      pagination.total = data.length
      const start = (pagination.page - 1) * pagination.size
      const end = start + pagination.size
      tableData.value = data.slice(start, end)
    }
  } catch (error) {
    ElMessage.error('加载数据失败')
  } finally {
    loading.value = false
  }
}

const handleSearch = () => {
  pagination.page = 1
  loadData()
}

const handleReset = () => {
  filters.personName = ''
  filters.operateName = ''
  dateRange.value = []
  pagination.page = 1
  loadData()
}

const handleView = async (row) => {
  currentLog.value = { ...row }
  detailDialogVisible.value = true
}

const handleDelete = async (row) => {
  await ElMessageBox.confirm('确定要删除这条日志记录吗?', '提示', {
    confirmButtonText: '确定',
    cancelButtonText: '取消',
    type: 'warning'
  })
  const result = await operateLogApi.delete(row.logId)
  if (result.code === 200) {
    ElMessage.success('删除成功')
    loadData()
  } else {
    ElMessage.error(result.message || '删除失败')
  }
}

const showStatisticsDialog = async () => {
  statisticsDialogVisible.value = true
  statisticsLoading.value = true
  try {
    const result = await operateLogApi.getStatistics()
    if (result.code === 200) {
      statisticsData.value = result.data || []
    }
  } catch (error) {
    ElMessage.error('获取统计数据失败')
  } finally {
    statisticsLoading.value = false
  }
}

onMounted(() => {
  loadData()
})
</script>

<style scoped>
.operate-log-list {
  padding: 20px;
}
.filter-card {
  margin-bottom: 20px;
}
.pagination-container {
  margin-top: 20px;
  display: flex;
  justify-content: flex-end;
}
</style>
