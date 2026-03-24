<template>
  <div class="apply-list-container">
    <!-- App Header -->
    <AppHeader />

    <!-- Page Header -->
    <div class="page-header">
      <h2>应用列表</h2>
      <div class="breadcrumb">当前位置: 申请管理 &gt; 应用列表</div>
    </div>

    <!-- Search and Filter -->
    <el-form :inline="true" :model="queryParams" class="search-form">
      <el-form-item label="人员编号">
        <el-input v-model="queryParams.workerId" placeholder="输入人员编号" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="部门代码">
        <el-input v-model="queryParams.unitCode" placeholder="输入部门代码" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="申请状态">
        <el-select v-model="queryParams.status" placeholder="选择状态" clearable style="width: 200px">
          <el-option label="全部" value="" />
          <el-option label="未填写" value="未填写" />
          <el-option label="待确认" value="待确认" />
          <el-option label="未申报" value="未申报" />
          <el-option label="已申报" value="已申报" />
          <el-option label="已受理" value="已受理" />
          <el-option label="已驳回" value="已驳回" />
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="Search" @click="handleSearch">查询</el-button>
        <el-button type="success" icon="Plus" @click="handleCreate">首次申请</el-button>
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
      <el-table-column prop="applyId" label="申请编号" width="100" />
      <el-table-column prop="workerName" label="人员姓名" width="120" />
      <el-table-column prop="unitName" label="部门名称" width="150" />
      <el-table-column prop="applyStatus" label="申请状态" width="100">
        <template #default="{row}">
          <el-tag
            :type="getStatusType(row.applyStatus)"
            effect="dark"
            size="small"
          >
            {{ row.applyStatus }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="checkMan" label="审核人" width="100" />
      <el-table-column prop="checkDate" label="审核日期" width="120" />
      <el-table-column prop="checkAdvise" label="审核意见" width="200" show-overflow-tooltip />
      <el-table-column prop="createTime" label="创建时间" width="160" sortable />
      <el-table-column label="操作" width="180" fixed="right">
        <template #default="{row}">
          <el-button
            type="primary"
            size="small"
            link
            @click="handleView(row)"
          >
            查看
          </el-button>
          <el-button
            v-if="canApprove(row)"
            type="success"
            size="small"
            @click="handleApprove(row)"
          >
            审批
          </el-button>
        </template>
      </el-table-column>

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
  </div>
</template>

<script>
import { ref, reactive, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'
import { getApplyList, approveApply } from '@/api/apply'
import { hasRole } from '@/utils/auth'
import AppHeader from '@/components/AppHeader.vue'

export default {
  name: 'ApplyList',

  components: {
    AppHeader
  },

  setup() {
    const router = useRouter()
    const loading = ref(false)
    const tableData = ref([])
    const queryParams = reactive({
      workerId: '',
      unitCode: '',
      status: '',
      keyword: ''
    })
    const page = reactive({
      current: 1,
      size: 10,
      total: 0
    })

    // Load data on component mount
    onMounted(() => {
      loadTableData()
    })

    /**
     * Load application list from API
     */
    const loadTableData = async () => {
      loading.value = true
      try {
        const res = await getApplyList({
          current: page.current,
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
        loading.value = false
      } finally {
        loading.value = false
      }
    }

    /**
     * Handle search button click
     */
    const handleSearch = () => {
      page.current = 1
      loadTableData()
    }

    /**
     * Handle create button click
     */
    const handleCreate = () => {
      router.push('/apply/create')
    }

    /**
     * Handle view detail button click
     */
    const handleView = (row) => {
      router.push(`/apply/${row.applyId}/detail`)
    }

    /**
     * Check if current user can approve this application
     * Maps to: CheckTask.GetPendingApplications permission
     */
    const canApprove = (row) => {
      // Only allow approval for pending applications and users with appropriate roles
      if (row.applyStatus !== '待确认' && row.applyStatus !== '未申报') {
        return false
      }

      // Check if user has admin, manager, or reviewer role
      return hasRole(['admin', 'manager', 'reviewer'])
    }

    /**
     * Handle approve button click
     */
    const handleApprove = async (row) => {
      try {
        const res = await approveApply(row.applyId, {
          checkMan: localStorage.getItem('userName') || '',
          checkAdvise: '同意'
        })

        if (res.code === 0) {
          ElMessage.success('审批成功')
          loadTableData()
        } else {
          ElMessage.error(res.message || '审批失败')
        }
      } catch (error) {
        ElMessage.error('审批失败: ' + error.message)
      }
    }

    /**
     * Get status type for display
     * Maps .NET status values to Element Plus tag types
     */
    const getStatusType = (status) => {
      const statusMap = {
        '未填写': 'info',
        '待确认': 'warning',
        '已申报': 'primary',
        '已受理': 'success',
        '已驳回': 'danger'
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

    /**
     * Handle sort change
     */
    const handleSortChange = ({ prop, order }) => {
      // Implement sorting logic
      console.log('Sort:', prop, order)
    }

    return {
      router,
      loading,
      tableData,
      queryParams,
      page,
      handleSearch,
      handleCreate,
      handleView,
      canApprove,
      handleApprove,
      getStatusType,
      handleSizeChange,
      handleCurrentChange,
      handleSortChange
    }
  }
}
</script>

<style scoped>
.apply-list-container {
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
