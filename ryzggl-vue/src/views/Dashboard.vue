<template>
  <div class="dashboard-container">
    <!-- App Header -->
    <AppHeader />

    <!-- Page Header -->
    <div class="page-header">
      <h2>工作台</h2>
      <div class="breadcrumb">当前位置: 工作台</div>
    </div>

    <!-- Statistics Cards -->
    <el-row :gutter="20" class="stats-row">
      <el-col :span="6">
        <el-card class="stats-card">
          <div class="stats-content">
            <div class="stats-icon" style="background: #409eff">
              <el-icon><Document /></el-icon>
            </div>
            <div class="stats-info">
              <div class="stats-label">待处理申请</div>
              <div class="stats-value">{{ stats.pendingApply }}</div>
            </div>
          </div>
        </el-card>
      </el-col>
      <el-col :span="6">
        <el-card class="stats-card">
          <div class="stats-content">
            <div class="stats-icon" style="background: #67c23a">
              <el-icon><Medal /></el-icon>
            </div>
            <div class="stats-info">
              <div class="stats-label">有效证书</div>
              <div class="stats-value">{{ stats.validCertificate }}</div>
            </div>
          </div>
        </el-card>
      </el-col>
      <el-col :span="6">
        <el-card class="stats-card">
          <div class="stats-content">
            <div class="stats-icon" style="background: #e6a23c">
              <el-icon><User /></el-icon>
            </div>
            <div class="stats-info">
              <div class="stats-label">在册人员</div>
              <div class="stats-value">{{ stats.totalWorker }}</div>
            </div>
          </div>
        </el-card>
      </el-col>
      <el-col :span="6">
        <el-card class="stats-card">
          <div class="stats-content">
            <div class="stats-icon" style="background: #f56c6c">
              <el-icon><Reading /></el-icon>
            </div>
            <div class="stats-info">
              <div class="stats-label">即将过期</div>
              <div class="stats-value">{{ stats.expiringSoon }}</div>
            </div>
          </div>
        </el-card>
      </el-col>
    </el-row>

    <!-- Quick Actions -->
    <el-row :gutter="20" class="actions-row">
      <el-col :span="24">
        <el-card>
          <template #header>
            <div class="card-header">
              <span>快捷操作</span>
            </div>
          </template>
          <div class="quick-actions">
            <el-button type="primary" icon="DocumentAdd" @click="handleQuickAction('apply')">
              新建申请
            </el-button>
            <el-button type="success" icon="Medal" @click="handleQuickAction('certificate')">
              证书管理
            </el-button>
            <el-button type="warning" icon="UserFilled" @click="handleQuickAction('worker')">
              人员管理
            </el-button>
            <el-button type="info" icon="Reading" @click="handleQuickAction('exam')">
              考试管理
            </el-button>
            <el-button type="danger" icon="Upload" @click="handleQuickAction('import')">
              数据导入
            </el-button>
            <el-button type="default" icon="Download" @click="handleQuickAction('export')">
              数据导出
            </el-button>
          </div>
          <div style="margin-top: 20px">
            <el-divider>实体管理快捷入口</el-divider>
            <div class="entity-actions">
              <el-button type="primary" plain @click="handleQuickAction('applyContinue')">
                延续申请管理
              </el-button>
              <el-button type="success" plain @click="handleQuickAction('applyRenew')">
                续期申请管理
              </el-button>
              <el-button type="warning" plain @click="handleQuickAction('certificateChange')">
                证书变更管理
              </el-button>
              <el-button type="info" plain @click="handleQuickAction('certificateContinue')">
                证书延续管理
              </el-button>
            </div>
          </div>
        </el-card>
      </el-col>
    </el-row>

    <!-- Recent Activities & Notices -->
    <el-row :gutter="20" class="info-row">
      <el-col :span="12">
        <el-card>
          <template #header>
            <div class="card-header">
              <span>最近申请</span>
              <el-link type="primary" @click="handleQuickAction('apply')">查看全部</el-link>
            </div>
          </template>
          <el-table :data="recentApplies" stripe size="small">
            <el-table-column prop="applyId" label="申请编号" width="80" />
            <el-table-column prop="workerName" label="申请人" width="80" />
            <el-table-column prop="applyType" label="申请类型" width="100" />
            <el-table-column prop="applyStatus" label="状态" width="80">
              <template #default="{row}">
                <el-tag :type="getStatusType(row.applyStatus)" size="small">
                  {{ row.applyStatus }}
                </el-tag>
              </template>
            </el-table-column>
            <el-table-column prop="createTime" label="创建时间" />
          </el-table>
        </el-card>
      </el-col>
      <el-col :span="12">
        <el-card>
          <template #header>
            <div class="card-header">
              <span>即将到期证书</span>
              <el-link type="primary" @click="handleQuickAction('certificate')">查看全部</el-link>
            </div>
          </template>
          <el-table :data="expiringCertificates" stripe size="small">
            <el-table-column prop="certificateNo" label="证书编号" width="120" />
            <el-table-column prop="workerName" label="持证人" width="80" />
            <el-table-column prop="validDateEnd" label="有效期至" width="100" />
            <el-table-column label="剩余天数" width="80">
              <template #default="{row}">
                <el-tag :type="getDaysType(row.daysRemaining)" size="small">
                  {{ row.daysRemaining }} 天
                </el-tag>
              </template>
            </el-table-column>
          </el-table>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import { ref, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { Document, Medal, User, Reading, DocumentAdd, UserFilled, Upload, Download } from '@element-plus/icons-vue'
import AppHeader from '@/components/AppHeader.vue'

export default {
  name: 'Dashboard',

  components: {
    AppHeader,
    Document,
    Medal,
    User,
    Reading,
    DocumentAdd,
    UserFilled,
    Upload,
    Download
  },

  setup() {
    const router = useRouter()

    const stats = reactive({
      pendingApply: 12,
      validCertificate: 328,
      totalWorker: 456,
      expiringSoon: 8
    })

    const recentApplies = ref([
      { applyId: 'A001', workerName: '张三', applyType: '首次注册', applyStatus: '待确认', createTime: '2024-03-14 09:30' },
      { applyId: 'A002', workerName: '李四', applyType: '延续注册', applyStatus: '已受理', createTime: '2024-03-13 15:20' },
      { applyId: 'A003', workerName: '王五', applyType: '变更注册', applyStatus: '已申报', createTime: '2024-03-13 11:45' },
      { applyId: 'A004', workerName: '赵六', applyType: '首次注册', applyStatus: '未填写', createTime: '2024-03-12 14:00' }
    ])

    const expiringCertificates = ref([
      { certificateNo: 'ZS2024001', workerName: '张三', validDateEnd: '2024-03-20', daysRemaining: 6 },
      { certificateNo: 'ZS2024002', workerName: '李四', validDateEnd: '2024-03-25', daysRemaining: 11 },
      { certificateNo: 'ZS2024003', workerName: '王五', validDateEnd: '2024-03-30', daysRemaining: 16 },
      { certificateNo: 'ZS2024004', workerName: '赵六', validDateEnd: '2024-04-05', daysRemaining: 22 }
    ])

    /**
     * Handle quick action
     */
    const handleQuickAction = (action) => {
      const routes = {
        apply: '/apply/list',
        certificate: '/certificates',
        worker: '/worker/list',
        exam: '/exam/list',
        import: '/import',
        export: '/export',
        applyContinue: '/apply-continue',
        applyRenew: '/apply-renew',
        certificateChange: '/certificate-change',
        certificateContinue: '/certificate-continue'
      }
      if (routes[action]) {
        router.push(routes[action])
      }
    }

    /**
     * Get status type
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
     * Get days type
     */
    const getDaysType = (days) => {
      if (days <= 7) return 'danger'
      if (days <= 30) return 'warning'
      return 'success'
    }

    return {
      stats,
      recentApplies,
      expiringCertificates,
      handleQuickAction,
      getStatusType,
      getDaysType
    }
  }
}
</script>

<style scoped>
.dashboard-container {
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

.stats-row {
  margin-bottom: 20px;
}

.stats-card {
  cursor: pointer;
  transition: transform 0.2s;
}

.stats-card:hover {
  transform: translateY(-4px);
}

.stats-content {
  display: flex;
  align-items: center;
}

.stats-icon {
  width: 60px;
  height: 60px;
  border-radius: 8px;
  display: flex;
  align-items: center;
  justify-content: center;
  margin-right: 15px;
}

.stats-icon .el-icon {
  font-size: 30px;
  color: white;
}

.stats-info {
  flex: 1;
}

.stats-label {
  font-size: 14px;
  color: #666;
  margin-bottom: 8px;
}

.stats-value {
  font-size: 28px;
  font-weight: bold;
  color: #333;
}

.actions-row {
  margin-bottom: 20px;
}

.card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.quick-actions {
  display: flex;
  flex-wrap: wrap;
  gap: 15px;
}

.quick-actions .el-button {
  flex: 0 0 auto;
}

.entity-actions {
  display: flex;
  gap: 15px;
  flex-wrap: wrap;
}

.info-row {
  margin-bottom: 20px;
}
</style>
