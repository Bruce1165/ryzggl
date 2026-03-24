<template>
  <div class="worker-list-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>人员管理</h2>
      <div class="breadcrumb">当前位置: 人员管理 &gt; 人员列表</div>
    </div>

    <!-- Search and Filter -->
    <el-form :inline="true" :model="queryParams" class="search-form">
      <el-form-item label="人员编号">
        <el-input v-model="queryParams.workerId" placeholder="输入人员编号" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="姓名">
        <el-input v-model="queryParams.workerName" placeholder="输入姓名" clearable style="width: 150px" />
      </el-form-item>
      <el-form-item label="身份证号">
        <el-input v-model="queryParams.idCard" placeholder="输入身份证号" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="部门代码">
        <el-input v-model="queryParams.unitCode" placeholder="输入部门代码" clearable style="width: 150px" />
      </el-form-item>
      <el-form-item label="证书状态">
        <el-select v-model="queryParams.certStatus" placeholder="选择状态" clearable style="width: 150px">
          <el-option label="全部" value="" />
          <el-option label="有证书" value="有证书" />
          <el-option label="无证书" value="无证书" />
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="Search" @click="handleSearch">查询</el-button>
        <el-button type="success" icon="Plus" @click="handleCreate">新增人员</el-button>
        <el-button type="info" icon="Download" @click="handleExport">导出</el-button>
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
      <el-table-column prop="workerId" label="人员编号" width="100" />
      <el-table-column prop="workerName" label="姓名" width="100" />
      <el-table-column prop="idCard" label="身份证号" width="180" />
      <el-table-column prop="phone" label="联系电话" width="120" />
      <el-table-column prop="gender" label="性别" width="60">
        <template #default="{row}">
          {{ row.gender === '1' ? '男' : '女' }}
        </template>
      </el-table-column>
      <el-table-column prop="unitCode" label="部门代码" width="100" />
      <el-table-column prop="unitName" label="单位名称" width="150" show-overflow-tooltip />
      <el-table-column prop="education" label="学历" width="100" />
      <el-table-column prop="certificateCount" label="证书数量" width="80" align="center">
        <template #default="{row}">
          <el-tag :type="row.certificateCount > 0 ? 'success' : 'info'" size="small">
            {{ row.certificateCount || 0 }}
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
          <el-button type="success" size="small" link @click="handleCertificates(row)">
            证书
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
  </div>
</template>

<script>
import { ref, reactive, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import { getWorkerList, deleteWorker } from '@/api/worker'

export default {
  name: 'WorkerList',

  setup() {
    const router = useRouter()
    const loading = ref(false)
    const tableData = ref([])
    const queryParams = reactive({
      workerId: '',
      workerName: '',
      idCard: '',
      unitCode: '',
      certStatus: ''
    })
    const page = reactive({
      current: 1,
      size: 10,
      total: 0
    })

    onMounted(() => {
      loadTableData()
    })

    /**
     * Load worker list
     */
    const loadTableData = async () => {
      loading.value = true
      try {
        const res = await getWorkerList({
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
     * Handle create worker
     */
    const handleCreate = () => {
      ElMessage.info('新增人员功能待实现')
    }

    /**
     * Handle view detail
     */
    const handleView = (row) => {
      router.push(`/worker/${row.workerId}/detail`)
    }

    /**
     * Handle edit worker
     */
    const handleEdit = (row) => {
      ElMessage.info('编辑功能待实现')
    }

    /**
     * Handle view certificates
     */
    const handleCertificates = (row) => {
      router.push({
        path: '/certificates',
        query: { workerId: row.workerId }
      })
    }

    /**
     * Handle delete worker
     */
    const handleDelete = (row) => {
      ElMessageBox.confirm(`确定要删除人员 ${row.workerName} 吗？`, '删除确认', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(async () => {
        try {
          const res = await deleteWorker(row.workerId)

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
     * Handle export
     */
    const handleExport = () => {
      ElMessage.info('导出功能待实现')
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
      handleSearch,
      handleCreate,
      handleView,
      handleEdit,
      handleCertificates,
      handleDelete,
      handleExport,
      handleSortChange,
      handleSizeChange,
      handleCurrentChange
    }
  }
}
</script>

<style scoped>
.worker-list-container {
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
