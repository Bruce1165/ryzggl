<template>
  <div class="certificate-list-container">
    <!-- App Header -->
    <AppHeader />

    <!-- Page Header -->
    <div class="page-header">
      <h2>证书管理</h2>
      <div class="breadcrumb">当前位置: 证书管理 &gt; 证书列表</div>
    </div>

    <!-- Search and Filter -->
    <el-form :inline="true" :model="queryParams" class="search-form">
      <el-form-item label="人员编号">
        <el-input v-model="queryParams.workerId" placeholder="输入人员编号" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="证书编号">
        <el-input v-model="queryParams.certificateNo" placeholder="输入证书编号" clearable style="width: 200px" />
      </el-form-item>
      <el-form-item label="证书状态">
        <el-select v-model="queryParams.status" placeholder="选择状态" clearable style="width: 200px">
          <el-option label="全部" value="" />
          <el-option label="有效" value="有效" />
          <el-option label="暂停" value="暂停" />
          <el-option label="锁定" value="锁定" />
          <el-option label="过期" value="过期" />
        </el-select>
      </el-form-item>
      <el-form-item>
        <el-button type="primary" icon="Search" @click="handleSearch">查询</el-button>
        <el-button type="success" icon="Plus" @click="handleCreate">新建证书</el-button>
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
      <el-table-column prop="certificateId" label="证书ID" width="80" />
      <el-table-column prop="certificateNo" label="证书编号" width="150" />
      <el-table-column prop="workerName" label="人员姓名" width="120" />
      <el-table-column prop="unitName" label="单位名称" width="150" />
      <el-table-column prop="qualificationLevel" label="资格等级" width="100">
        <template #default="{row}">
          <el-tag type="primary" size="small">{{ row.qualificationLevel }}</el-tag>
        </template>
      </el-table-column>
      <el-table-column prop="profession" label="专业类别" width="120" />
      <el-table-column prop="validDateStart" label="有效期开始" width="120" />
      <el-table-column prop="validDateEnd" label="有效期结束" width="120">
        <template #default="{row}">
          <span :class="{ 'text-danger': isExpired(row.validDateEnd) }">
            {{ row.validDateEnd }}
          </span>
        </template>
      </el-table-column>
      <el-table-column prop="status" label="证书状态" width="100">
        <template #default="{row}">
          <el-tag
            :type="getStatusType(row.status)"
            effect="dark"
            size="small"
          >
            {{ row.status }}
          </el-tag>
        </template>
      </el-table-column>
      <el-table-column label="操作" width="280" fixed="right">
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
            v-if="row.status === '有效'"
            type="success"
            size="small"
            link
            @click="handleContinue(row)"
          >
            延续
          </el-button>
          <el-button
            v-if="row.status === '有效'"
            type="warning"
            size="small"
            link
            @click="handlePause(row)"
          >
            暂停
          </el-button>
          <el-button
            v-if="row.status === '暂停'"
            type="success"
            size="small"
            link
            @click="handleResume(row)"
          >
            恢复
          </el-button>
          <el-button
            v-if="row.status === '锁定'"
            type="success"
            size="small"
            link
            @click="handleUnlock(row)"
          >
            解锁
          </el-button>
          <el-button
            v-if="row.status === '有效'"
            type="danger"
            size="small"
            link
            @click="handleLock(row)"
          >
            锁定
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
import { getCertificateList, continueCertificate, pauseCertificate, lockCertificate, unlockCertificate } from '@/api/certificate'
import AppHeader from '@/components/AppHeader.vue'

export default {
  name: 'CertificateList',

  components: {
    AppHeader
  },

  setup() {
    const router = useRouter()
    const loading = ref(false)
    const tableData = ref([])
    const queryParams = reactive({
      workerId: '',
      certificateNo: '',
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
     * Load certificate list from API
     */
    const loadTableData = async () => {
      loading.value = true
      try {
        const res = await getCertificateList({
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
      // Navigate to create page
      ElMessage.info('新建证书功能待实现')
    }

    /**
     * Handle view detail button click
     */
    const handleView = (row) => {
      router.push(`/certificates/${row.certificateId}/detail`)
    }

    /**
     * Handle continue button click
     */
    const handleContinue = (row) => {
      router.push(`/certificates/${row.certificateId}/continue`)
    }

    /**
     * Handle pause button click
     */
    const handlePause = (row) => {
      ElMessageBox.prompt('请输入暂停原因', '暂停证书', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        inputPattern: /\S+/,
        inputErrorMessage: '暂停原因不能为空'
      }).then(async ({ value }) => {
        try {
          const res = await pauseCertificate(row.certificateId, value)
          if (res.code === 0) {
            ElMessage.success('证书暂停成功')
            loadTableData()
          } else {
            ElMessage.error(res.message || '暂停失败')
          }
        } catch (error) {
          ElMessage.error('暂停失败: ' + error.message)
        }
      }).catch(() => {
        ElMessage.info('已取消暂停')
      })
    }

    /**
     * Handle resume button click
     */
    const handleResume = (row) => {
      ElMessageBox.prompt('请输入恢复原因', '恢复证书', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        inputPattern: /\S+/,
        inputErrorMessage: '恢复原因不能为空'
      }).then(async ({ value }) => {
        try {
          // For resume, we use unlock API with a reason
          const res = await unlockCertificate(row.certificateId)
          if (res.code === 0) {
            ElMessage.success('证书恢复成功')
            loadTableData()
          } else {
            ElMessage.error(res.message || '恢复失败')
          }
        } catch (error) {
          ElMessage.error('恢复失败: ' + error.message)
        }
      }).catch(() => {
        ElMessage.info('已取消恢复')
      })
    }

    /**
     * Handle lock button click
     */
    const handleLock = (row) => {
      ElMessageBox.prompt('请输入锁定原因', '锁定证书', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        inputPattern: /\S+/,
        inputErrorMessage: '锁定原因不能为空'
      }).then(async ({ value }) => {
        try {
          const res = await lockCertificate(row.certificateId, value)
          if (res.code === 0) {
            ElMessage.success('证书锁定成功')
            loadTableData()
          } else {
            ElMessage.error(res.message || '锁定失败')
          }
        } catch (error) {
          ElMessage.error('锁定失败: ' + error.message)
        }
      }).catch(() => {
        ElMessage.info('已取消锁定')
      })
    }

    /**
     * Handle unlock button click
     */
    const handleUnlock = (row) => {
      ElMessageBox.confirm(
        '确定要解锁此证书吗？',
        '解锁确认',
        {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'info'
        }
      ).then(async () => {
        try {
          const res = await unlockCertificate(row.certificateId)
          if (res.code === 0) {
            ElMessage.success('证书解锁成功')
            loadTableData()
          } else {
            ElMessage.error(res.message || '解锁失败')
          }
        } catch (error) {
          ElMessage.error('解锁失败: ' + error.message)
        }
      }).catch(() => {
        ElMessage.info('已取消解锁')
      })
    }

    /**
     * Check if certificate is expired
     */
    const isExpired = (validDateEnd) => {
      if (!validDateEnd) return false
      const endDate = new Date(validDateEnd)
      return endDate < new Date()
    }

    /**
     * Get status type for display
     */
    const getStatusType = (status) => {
      const statusMap = {
        '有效': 'success',
        '暂停': 'warning',
        '锁定': 'danger',
        '过期': 'info'
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
      console.log('Sort:', prop, order)
    }

    return {
      loading,
      tableData,
      queryParams,
      page,
      handleSearch,
      handleCreate,
      handleView,
      handleContinue,
      handlePause,
      handleResume,
      handleLock,
      handleUnlock,
      isExpired,
      getStatusType,
      handleSizeChange,
      handleCurrentChange,
      handleSortChange
    }
  }
}
</script>

<style scoped>
.certificate-list-container {
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

.text-danger {
  color: #f56c6c;
  font-weight: bold;
}
</style>
