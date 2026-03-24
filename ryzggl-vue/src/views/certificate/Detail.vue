<template>
  <div class="certificate-detail-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>证书详情</h2>
      <div class="breadcrumb">当前位置: 证书管理 &gt; 证书详情</div>
    </div>

    <el-card v-loading="loading" class="detail-card">
      <template v-if="certificate">
        <!-- Certificate Information -->
        <div class="detail-section">
          <h3>证书信息</h3>
          <el-descriptions :column="2" border>
            <el-descriptions-item label="证书ID">{{ certificate.certificateId }}</el-descriptions-item>
            <el-descriptions-item label="证书编号">{{ certificate.certificateNo }}</el-descriptions-item>
            <el-descriptions-item label="证书状态">
              <el-tag :type="getStatusType(certificate.status)" effect="dark">
                {{ certificate.status }}
              </el-tag>
            </el-descriptions-item>
            <el-descriptions-item label="发证日期">{{ certificate.issueDate || '-' }}</el-descriptions-item>
            <el-descriptions-item label="有效期开始">{{ certificate.validDateStart }}</el-descriptions-item>
            <el-descriptions-item label="有效期结束">
              <span :class="{ 'text-danger': isExpired(certificate.validDateEnd) }">
                {{ certificate.validDateEnd }}
              </span>
            </el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Worker Information -->
        <div class="detail-section">
          <h3>人员信息</h3>
          <el-descriptions :column="2" border>
            <el-descriptions-item label="人员ID">{{ certificate.workerId }}</el-descriptions-item>
            <el-descriptions-item label="人员姓名">{{ certificate.workerName }}</el-descriptions-item>
            <el-descriptions-item label="身份证号">{{ certificate.idCard || '-' }}</el-descriptions-item>
            <el-descriptions-item label="联系电话">{{ certificate.phone || '-' }}</el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Unit Information -->
        <div class="detail-section">
          <h3>单位信息</h3>
          <el-descriptions :column="2" border>
            <el-descriptions-item label="单位代码">{{ certificate.unitCode }}</el-descriptions-item>
            <el-descriptions-item label="单位名称">{{ certificate.unitName }}</el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Qualification Information -->
        <div class="detail-section">
          <h3>资格信息</h3>
          <el-descriptions :column="2" border>
            <el-descriptions-item label="资格等级">{{ certificate.qualificationLevel }}</el-descriptions-item>
            <el-descriptions-item label="专业类别">{{ certificate.profession }}</el-descriptions-item>
            <el-descriptions-item label="职称级别">{{ certificate.titleLevel || '-' }}</el-descriptions-item>
            <el-descriptions-item label="注册类别">{{ certificate.registerType || '-' }}</el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Additional Information -->
        <div class="detail-section">
          <h3>其他信息</h3>
          <el-descriptions :column="1" border>
            <el-descriptions-item label="备注">{{ certificate.remark || '无' }}</el-descriptions-item>
            <el-descriptions-item label="创建时间">{{ certificate.createTime }}</el-descriptions-item>
            <el-descriptions-item label="更新时间">{{ certificate.updateTime || '-' }}</el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Actions -->
        <div class="action-buttons">
          <el-button @click="handleBack">返回</el-button>
          <el-button @click="handleEdit">编辑</el-button>
          <el-button @click="handlePrint">打印证书</el-button>
          <el-button
            v-if="certificate.status === '有效'"
            type="success"
            @click="handleContinue"
          >
            延续
          </el-button>
          <el-button
            v-if="certificate.status === '有效'"
            type="warning"
            @click="handlePause"
          >
            暂停
          </el-button>
          <el-button
            v-if="certificate.status === '暂停'"
            type="success"
            @click="handleResume"
          >
            恢复
          </el-button>
          <el-button
            v-if="certificate.status === '锁定'"
            type="success"
            @click="handleUnlock"
          >
            解锁
          </el-button>
          <el-button
            v-if="certificate.status === '有效'"
            type="danger"
            @click="handleLock"
          >
            锁定
          </el-button>
          <el-button type="danger" @click="handleDelete">删除</el-button>
        </div>
      </template>

      <el-empty v-else description="暂无数据" />
    </el-card>

    <!-- Continue Dialog -->
    <el-dialog v-model="continueDialogVisible" title="延续证书" width="500px">
      <el-form :model="continueForm" label-width="120px">
        <el-form-item label="延续年限">
          <el-input-number v-model="continueForm.years" :min="1" :max="5" />
        </el-form-item>
        <el-form-item label="新有效期结束">
          <el-date-picker
            v-model="continueForm.newEndDate"
            type="date"
            placeholder="选择新有效期结束日期"
            style="width: 100%"
            value-format="YYYY-MM-DD"
          />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="continueDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="confirmContinue">确认延续</el-button>
      </template>
    </el-dialog>

    <!-- Pause/Resume/Lock Dialog -->
    <el-dialog
      v-model="operationDialogVisible"
      :title="operationDialogTitle"
      width="500px"
    >
      <el-form :model="operationForm" label-width="100px">
        <el-form-item label="原因说明">
          <el-input
            v-model="operationForm.reason"
            type="textarea"
            :rows="4"
            placeholder="请输入原因"
          />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="operationDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="confirmOperation">确认</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script>
import { ref, reactive, onMounted, computed } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import { getCertificateById, deleteCertificate, continueCertificate, pauseCertificate, lockCertificate, unlockCertificate } from '@/api/certificate'
import dayjs from 'dayjs'

export default {
  name: 'CertificateDetail',

  setup() {
    const route = useRoute()
    const router = useRouter()
    const loading = ref(false)
    const certificate = ref(null)
    const continueDialogVisible = ref(false)
    const operationDialogVisible = ref(false)
    const operationType = ref('')

    const continueForm = reactive({
      years: 1,
      newEndDate: ''
    })

    const operationForm = reactive({
      reason: ''
    })

    const operationDialogTitle = computed(() => {
      const titles = {
        pause: '暂停证书',
        resume: '恢复证书',
        lock: '锁定证书'
      }
      return titles[operationType.value] || '操作'
    })

    /**
     * Load certificate detail
     */
    const loadCertificateDetail = async () => {
      loading.value = true
      try {
        const certificateId = route.params.id
        const res = await getCertificateById(certificateId)

        if (res.code === 0) {
          certificate.value = res.data
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
     * Handle back button
     */
    const handleBack = () => {
      router.back()
    }

    /**
     * Handle edit button
     */
    const handleEdit = () => {
      ElMessage.info('编辑功能待实现')
    }

    /**
     * Handle print button
     */
    const handlePrint = () => {
      ElMessage.info('打印证书功能待实现')
    }

    /**
     * Handle continue button
     */
    const handleContinue = () => {
      const currentDate = new Date(certificate.value.validDateEnd)
      const newDate = new Date(currentDate.setFullYear(currentDate.getFullYear() + 1))
      continueForm.newEndDate = dayjs(newDate).format('YYYY-MM-DD')
      continueForm.years = 1
      continueDialogVisible.value = true
    }

    /**
     * Confirm continue
     */
    const confirmContinue = async () => {
      try {
        const res = await continueCertificate(certificate.value.certificateId, {
          years: continueForm.years,
          newValidDateEnd: continueForm.newEndDate,
          reason: '证书延续'
        })

        if (res.code === 0) {
          ElMessage.success('证书延续成功')
          continueDialogVisible.value = false
          loadCertificateDetail()
        } else {
          ElMessage.error(res.message || '延续失败')
        }
      } catch (error) {
        ElMessage.error('延续失败: ' + error.message)
      }
    }

    /**
     * Handle pause button
     */
    const handlePause = () => {
      operationType.value = 'pause'
      operationForm.reason = ''
      operationDialogVisible.value = true
    }

    /**
     * Handle resume button
     */
    const handleResume = () => {
      operationType.value = 'resume'
      operationForm.reason = ''
      operationDialogVisible.value = true
    }

    /**
     * Handle lock button
     */
    const handleLock = () => {
      operationType.value = 'lock'
      operationForm.reason = ''
      operationDialogVisible.value = true
    }

    /**
     * Handle unlock button
     */
    const handleUnlock = async () => {
      try {
        await ElMessageBox.confirm(
          '确定要解锁此证书吗？',
          '解锁确认',
          {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'info'
          }
        )

        const res = await unlockCertificate(certificate.value.certificateId)

        if (res.code === 0) {
          ElMessage.success('证书解锁成功')
          loadCertificateDetail()
        } else {
          ElMessage.error(res.message || '解锁失败')
        }
      } catch (error) {
        if (error !== 'cancel') {
          ElMessage.error('解锁失败: ' + error.message)
        }
      }
    }

    /**
     * Confirm operation
     */
    const confirmOperation = async () => {
      if (!operationForm.reason.trim()) {
        ElMessage.warning('请输入原因')
        return
      }

      try {
        let res
        if (operationType.value === 'pause') {
          res = await pauseCertificate(certificate.value.certificateId, operationForm.reason)
        } else if (operationType.value === 'resume') {
          res = await unlockCertificate(certificate.value.certificateId)
        } else if (operationType.value === 'lock') {
          res = await lockCertificate(certificate.value.certificateId, {
            lockType: 'temporary',
            lockPeriod: 6,
            lockReason: operationForm.reason,
            description: operationForm.reason,
            notification: ['sms']
          })
        }

        if (res && res.code === 0) {
          ElMessage.success(`${operationType.value === 'lock' ? '锁定' : operationType.value === 'pause' ? '暂停' : '恢复'}成功`)
          operationDialogVisible.value = false
          loadCertificateDetail()
        } else {
          ElMessage.error(res?.message || '操作失败')
        }
      } catch (error) {
        ElMessage.error('操作失败: ' + error.message)
      }
    }

    /**
     * Handle delete button
     */
    const handleDelete = () => {
      ElMessageBox.confirm('确定要删除此证书吗？删除后不可恢复。', '删除确认', {
        confirmButtonText: '确定',
        cancelButtonText: '取消',
        type: 'warning'
      }).then(async () => {
        try {
          const res = await deleteCertificate(certificate.value.certificateId)

          if (res.code === 0) {
            ElMessage.success('删除成功')
            router.push('/certificates')
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

    onMounted(() => {
      loadCertificateDetail()
    })

    return {
      loading,
      certificate,
      continueDialogVisible,
      operationDialogVisible,
      operationDialogTitle,
      continueForm,
      operationForm,
      handleBack,
      handleEdit,
      handlePrint,
      handleContinue,
      confirmContinue,
      handlePause,
      handleResume,
      handleLock,
      handleUnlock,
      confirmOperation,
      handleDelete,
      isExpired,
      getStatusType
    }
  }
}
</script>

<style scoped>
.certificate-detail-container {
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

.text-danger {
  color: #f56c6c;
  font-weight: bold;
}
</style>
