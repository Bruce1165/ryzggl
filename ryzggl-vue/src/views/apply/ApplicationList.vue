<template>
  <div class="apply-list-container">
    <!-- App Header -->
    <AppHeader />

    <!-- Page Header -->
    <div class="page-header">
      <h2>申请管理</h2>
      <div class="breadcrumb">当前位置: 申请管理 &gt; 申请列表</div>
    </div>

    <!-- Filter Card -->
    <el-card class="filter-card">
      <el-form :inline="true" :model="filterForm" class="filter-form">
        <el-form-item label="申请人">
          <el-input v-model="filterForm.applicantName" placeholder="输入申请人姓名" clearable />
        </el-form-item>

        <el-form-item label="申请类型">
          <el-select v-model="filterForm.applyType" placeholder="选择申请类型" clearable>
            <el-option label="全部" value="" />
            <el-option label="首次注册" value="首次注册" />
            <el-option label="延续注册" value="延续注册" />
            <el-option label="变更注册" value="变更注册" />
            <el-option label="注销注册" value="注销注册" />
          </el-select>
        </el-form-item>

        <el-form-item label="申请状态">
          <el-select v-model="filterForm.applyStatus" placeholder="选择状态" clearable>
            <el-option label="全部" value="" />
            <el-option label="未填写" value="未填写" />
            <el-option label="待确认" value="待确认" />
            <el-option label="已受理" value="已受理" />
            <el-option label="已申报" value="已申报" />
            <el-option label="已驳回" value="已驳回" />
          </el-select>
        </el-form-item>

        <el-form-item>
          <el-button type="primary" icon="Search" @click="handleSearch">查询</el-button>
          <el-button icon="Refresh" @click="handleReset">重置</el-button>
        </el-form-item>
      </el-form>
    </el-card>

    <!-- Application List Card -->
    <el-card class="list-card">
      <template #header>
        <div class="card-header">
          <span>申请列表</span>
        </div>
      </template>

      <el-table
        :data="applyList"
        v-loading="loading"
        stripe
        style="width: 100%"
      >
        <el-table-column prop="applyNo" label="申请编号" width="120" />
        <el-table-column prop="applicantName" label="申请人" width="100" />
        <el-table-column prop="idCard" label="身份证号" width="150" />
        <el-table-column prop="unitName" label="企业名称" width="150" />
        <el-table-column prop="qualificationName" label="资格名称" width="120" />
        <el-table-column prop="level" label="等级" width="80" />
        <el-table-column prop="profession" label="专业类别" width="100" />
        <el-table-column prop="applyType" label="申请类型" width="100" />
        <el-table-column prop="applyStatus" label="申请状态" width="100">
          <template #default="{ row }">
            <el-tag :type="getStatusType(row.applyStatus)">
              {{ row.applyStatus }}
            </el-tag>
          </template>
        </el-table-column>
        <el-table-column prop="createTime" label="申请时间" width="160" />
        <el-table-column label="操作" width="200" fixed="right">
          <template #default="{ row }">
            <el-button
              type="primary"
              size="small"
              link
              @click="handleView(row)"
            >
              查看
            </el-button>
          </template>
        </el-table-column>
      </el-table>

      <!-- Pagination -->
      <div class="pagination-container">
        <el-pagination
          v-model:current-page="pagination.current"
          v-model:page-size="pagination.size"
          :page-sizes="[10, 20, 50, 100]"
          :total="pagination.total"
          layout="total, sizes, prev, pager, next, jumper"
          @size-change="handleSizeChange"
          @current-change="handlePageChange"
        />
      </div>
    </el-card>
  </div>
</template>

<script>
import { ref, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'
import { getApplyList } from '@/api/apply'
import AppHeader from '@/components/AppHeader.vue'

export default {
  name: 'ApplyList',

  components: {
    AppHeader
  },

  setup() {
    const router = useRouter()
    const loading = ref(false)

    const filterForm = reactive({
      applicantName: '',
      applyType: '',
      applyStatus: ''
    })

    const pagination = reactive({
      current: 1,
      size: 20,
      total: 0
    })

    const applyList = ref([])

    const getStatusType = (status) => {
      const typeMap = {
        '未填写': 'info',
        '待确认': 'warning',
        '已受理': 'primary',
        '已申报': 'success',
        '已驳回': 'danger'
      }
      return typeMap[status] || 'info'
    }

    const loadApplications = async () => {
      loading.value = true
      try {
        const params = {
          current: pagination.current,
          size: pagination.size
        }

        if (filterForm.applicantName) {
          params.applicantName = filterForm.applicantName
        }
        if (filterForm.applyType) {
          params.applyType = filterForm.applyType
        }
        if (filterForm.applyStatus) {
          params.applyStatus = filterForm.applyStatus
        }

        const res = await getApplyList(params)

        if (res.code === 0) {
          applyList.value = res.data || []
          pagination.total = res.total || 0
        } else {
          ElMessage.error(res.message || '加载申请列表失败')
        }
      } catch (error) {
        ElMessage.error('加载申请列表失败: ' + error.message)
      } finally {
        loading.value = false
      }
    }

    const handleSearch = () => {
      pagination.current = 1
      loadApplications()
    }

    const handleReset = () => {
      filterForm.applicantName = ''
      filterForm.applyType = ''
      filterForm.applyStatus = ''
      pagination.current = 1
      loadApplications()
    }

    const handleView = (row) => {
      router.push({
        path: '/apply/detail',
        query: { id: row.applyNo }
      })
    }

    const handlePageChange = (page) => {
      pagination.current = page
      loadApplications()
    }

    const handleSizeChange = (size) => {
      pagination.size = size
      pagination.current = 1
      loadApplications()
    }

    return {
      loading,
      filterForm,
      pagination,
      applyList,
      getStatusType,
      loadApplications,
      handleSearch,
      handleReset,
      handleView,
      handlePageChange,
      handleSizeChange
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

.filter-card {
  margin-bottom: 20px;
}

.filter-form {
  display: flex;
  align-items: center;
  flex-wrap: wrap;
}

.list-card {
  margin-bottom: 20px;
}

.card-header {
  font-weight: bold;
  color: #333;
}

.pagination-container {
  margin-top: 20px;
  display: flex;
  justify-content: flex-end;
}
</style>
