<template>
  <div class="worker-detail-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>人员详情</h2>
      <div class="breadcrumb">当前位置: 人员管理 &gt; 人员详情</div>
    </div>

    <el-card v-loading="loading" class="detail-card">
      <template v-if="worker">
        <!-- Basic Information -->
        <div class="detail-section">
          <h3>基本信息</h3>
          <el-descriptions :column="2" border>
            <el-descriptions-item label="人员编号">{{ worker.workerId }}</el-descriptions-item>
            <el-descriptions-item label="姓名">{{ worker.workerName }}</el-descriptions-item>
            <el-descriptions-item label="性别">
              {{ worker.gender === '1' ? '男' : '女' }}
            </el-descriptions-item>
            <el-descriptions-item label="出生日期">{{ worker.birthday || '-' }}</el-descriptions-item>
            <el-descriptions-item label="身份证号">{{ worker.idCard }}</el-descriptions-item>
            <el-descriptions-item label="民族">{{ worker.nation || '汉族' }}</el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Contact Information -->
        <div class="detail-section">
          <h3>联系方式</h3>
          <el-descriptions :column="2" border>
            <el-descriptions-item label="联系电话">{{ worker.phone }}</el-descriptions-item>
            <el-descriptions-item label="手机号码">{{ worker.mobile || '-' }}</el-descriptions-item>
            <el-descriptions-item label="电子邮箱">{{ worker.email || '-' }}</el-descriptions-item>
            <el-descriptions-item label="通讯地址">{{ worker.address || '-' }}</el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Education Information -->
        <div class="detail-section">
          <h3>学历信息</h3>
          <el-descriptions :column="2" border>
            <el-descriptions-item label="学历">{{ worker.education || '-' }}</el-descriptions-item>
            <el-descriptions-item label="毕业院校">{{ worker.school || '-' }}</el-descriptions-item>
            <el-descriptions-item label="所学专业">{{ worker.major || '-' }}</el-descriptions-item>
            <el-descriptions-item label="毕业时间">{{ worker.graduationDate || '-' }}</el-descriptions-item>
            <el-descriptions-item label="学位">{{ worker.degree || '-' }}</el-descriptions-item>
            <el-descriptions-item label="政治面貌">{{ worker.politicalStatus || '群众' }}</el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Work Information -->
        <div class="detail-section">
          <h3>工作信息</h3>
          <el-descriptions :column="2" border>
            <el-descriptions-item label="单位代码">{{ worker.unitCode }}</el-descriptions-item>
            <el-descriptions-item label="单位名称">{{ worker.unitName }}</el-descriptions-item>
            <el-descriptions-item label="部门代码">{{ worker.deptCode || '-' }}</el-descriptions-item>
            <el-descriptions-item label="部门名称">{{ worker.deptName || '-' }}</el-descriptions-item>
            <el-descriptions-item label="参加工作时间">{{ worker.workStartDate || '-' }}</el-descriptions-item>
            <el-descriptions-item label="职称">{{ worker.title || '-' }}</el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Certificate Summary -->
        <div class="detail-section">
          <h3>证书信息</h3>
          <el-descriptions :column="3" border>
            <el-descriptions-item label="证书数量">
              <el-tag :type="worker.certificateCount > 0 ? 'success' : 'info'">
                {{ worker.certificateCount || 0 }} 个
              </el-tag>
            </el-descriptions-item>
            <el-descriptions-item label="有效证书">
              <el-tag type="success">
                {{ worker.validCertificateCount || 0 }} 个
              </el-tag>
            </el-descriptions-item>
            <el-descriptions-item label="过期证书">
              <el-tag type="danger">
                {{ worker.expiredCertificateCount || 0 }} 个
              </el-tag>
            </el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Additional Information -->
        <div class="detail-section">
          <h3>其他信息</h3>
          <el-descriptions :column="1" border>
            <el-descriptions-item label="备注">{{ worker.remark || '无' }}</el-descriptions-item>
            <el-descriptions-item label="创建时间">{{ worker.createTime }}</el-descriptions-item>
            <el-descriptions-item label="更新时间">{{ worker.updateTime || '-' }}</el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Actions -->
        <div class="action-buttons">
          <el-button @click="handleBack">返回</el-button>
          <el-button type="primary" @click="handleEdit">编辑</el-button>
          <el-button type="success" @click="handleApply">申请资格</el-button>
          <el-button type="info" @click="handleCertificates">查看证书</el-button>
          <el-button type="warning" @click="handleExam">考试记录</el-button>
          <el-button type="danger" @click="handleDelete">删除</el-button>
        </div>
      </template>

      <el-empty v-else description="暂无数据" />
    </el-card>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import { getWorkerById, deleteWorker } from '@/api/worker'

export default {
  name: 'WorkerDetail',

  setup() {
    const route = useRoute()
    const router = useRouter()
    const loading = ref(false)
    const worker = ref(null)

    /**
     * Load worker detail
     */
    const loadWorkerDetail = async () => {
      loading.value = true
      try {
        const workerId = route.params.id
        const res = await getWorkerById(workerId)

        if (res.code === 0) {
          worker.value = res.data
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
     * Handle back
     */
    const handleBack = () => {
      router.back()
    }

    /**
     * Handle edit
     */
    const handleEdit = () => {
      ElMessage.info('编辑功能待实现')
    }

    /**
     * Handle apply
     */
    const handleApply = () => {
      router.push({
        path: '/apply/create',
        query: { workerId: worker.value.workerId }
      })
    }

    /**
     * Handle view certificates
     */
    const handleCertificates = () => {
      router.push({
        path: '/certificates',
        query: { workerId: worker.value.workerId }
      })
    }

    /**
     * Handle exam records
     */
    const handleExam = () => {
      router.push({
        path: '/exam',
        query: { workerId: worker.value.workerId }
      })
    }

    /**
     * Handle delete
     */
    const handleDelete = () => {
      ElMessageBox.confirm(`确定要删除人员 ${worker.value.workerName} 吗？`, '删除确认', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(async () => {
        try {
          const res = await deleteWorker(worker.value.workerId)

          if (res.code === 0) {
            ElMessage.success('删除成功')
            router.push('/worker/list')
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

    onMounted(() => {
      loadWorkerDetail()
    })

    return {
      loading,
      worker,
      handleBack,
      handleEdit,
      handleApply,
      handleCertificates,
      handleExam,
      handleDelete
    }
  }
}
</script>

<style scoped>
.worker-detail-container {
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

.detail-section {
  margin-bottom: 30px;
}

.detail-section h3 {
  margin: 0 0 15px 0;
  font-size: 16px;
  font-weight: bold;
  color: #333;
  border-left: 4px solid #409eff;
  padding-left: 10px;
}

.action-buttons {
  margin-top: 30px;
  text-align: center;
}
</style>
