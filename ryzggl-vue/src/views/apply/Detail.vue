<template>
  <div class="apply-detail-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>申请详情</h2>
      <div class="breadcrumb">当前位置: 二级造价工程师注册 &gt; 申请详情</div>
    </div>

    <el-card v-loading="loading" class="detail-card">
      <template v-if="apply">
        <!-- Basic Information -->
        <div class="detail-section">
          <h3>基本信息</h3>
          <el-descriptions :column="2" border>
            <el-descriptions-item label="申请编号">{{ apply.applyId }}</el-descriptions-item>
            <el-descriptions-item label="申请状态">
              <el-tag :type="getStatusType(apply.applyStatus)" effect="dark">
                {{ apply.applyStatus }}
              </el-tag>
            </el-descriptions-item>
            <el-descriptions-item label="申请类型">{{ apply.applyType || '-' }}</el-descriptions-item>
            <el-descriptions-item label="申请日期">{{ apply.applyDate || '-' }}</el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Worker Information -->
        <div class="detail-section">
          <h3>人员信息</h3>
          <el-descriptions :column="2" border>
            <el-descriptions-item label="人员编号">{{ apply.workerId }}</el-descriptions-item>
            <el-descriptions-item label="人员姓名">{{ apply.workerName }}</el-descriptions-item>
            <el-descriptions-item label="身份证号">{{ apply.idCard || '-' }}</el-descriptions-item>
            <el-descriptions-item label="联系电话">{{ apply.phone || '-' }}</el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Unit Information -->
        <div class="detail-section">
          <h3>单位信息</h3>
          <el-descriptions :column="2" border>
            <el-descriptions-item label="部门代码">{{ apply.unitCode }}</el-descriptions-item>
            <el-descriptions-item label="部门名称">{{ apply.unitName }}</el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Qualification Information -->
        <div class="detail-section">
          <h3>资格信息</h3>
          <el-descriptions :column="2" border>
            <el-descriptions-item label="资格等级">{{ apply.qualificationLevel || '-' }}</el-descriptions-item>
            <el-descriptions-item label="专业类别">{{ apply.profession || '-' }}</el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Review Information -->
        <div v-if="apply.applyStatus !== '未填写'" class="detail-section">
          <h3>审核信息</h3>
          <el-descriptions :column="2" border>
            <el-descriptions-item label="审核人">{{ apply.checkMan || '-' }}</el-descriptions-item>
            <el-descriptions-item label="审核日期">{{ apply.checkDate || '-' }}</el-descriptions-item>
            <el-descriptions-item label="审核意见" :span="2">
              {{ apply.checkAdvise || '-' }}
            </el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Additional Information -->
        <div class="detail-section">
          <h3>其他信息</h3>
          <el-descriptions :column="1" border>
            <el-descriptions-item label="备注">{{ apply.remark || '无' }}</el-descriptions-item>
            <el-descriptions-item label="创建时间">{{ apply.createTime }}</el-descriptions-item>
            <el-descriptions-item label="更新时间">{{ apply.updateTime || '-' }}</el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Actions -->
        <div class="action-buttons">
          <el-button @click="handleBack">返回</el-button>
          <el-button v-if="apply.applyStatus === '待确认'" type="success" @click="handleApprove">
            审批通过
          </el-button>
          <el-button v-if="apply.applyStatus === '待确认'" type="danger" @click="handleReject">
            驳回
          </el-button>
          <el-button v-if="apply.applyStatus === '未填写'" type="primary" @click="handleSubmit">
            提交申请
          </el-button>
        </div>
      </template>

      <el-empty v-else description="暂无数据" />
    </el-card>

    <!-- Reject Dialog -->
    <el-dialog v-model="rejectDialogVisible" title="驳回申请" width="500px">
      <el-form :model="rejectForm" label-width="80px">
        <el-form-item label="驳回原因">
          <el-input
            v-model="rejectForm.reason"
            type="textarea"
            :rows="4"
            placeholder="请输入驳回原因"
          />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="rejectDialogVisible = false">取消</el-button>
        <el-button type="primary" @click="confirmReject">确认驳回</el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script>
import { ref, reactive, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import { getApplyList, approveApply, rejectApply, submitApply } from '@/api/apply'

export default {
  name: 'ApplyDetail',

  setup() {
    const route = useRoute()
    const router = useRouter()
    const loading = ref(false)
    const apply = ref(null)
    const rejectDialogVisible = ref(false)

    const rejectForm = reactive({
      reason: ''
    })

    /**
     * Load application detail
     */
    const loadApplyDetail = async () => {
      loading.value = true
      try {
        const applyId = route.params.id
        // Since we don't have a detail API, we'll get all applications and filter
        const res = await getApplyList({ current: 1 })

        if (res.code === 0) {
          const found = res.data.records?.find(item => item.applyId == applyId)
          if (found) {
            apply.value = found
          } else {
            ElMessage.error('申请不存在')
          }
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
     * Handle approve button
     */
    const handleApprove = async () => {
      try {
        await ElMessageBox.confirm(
          '确定要通过此申请吗？',
          '审批确认',
          {
            confirmButtonText: '确定通过',
            cancelButtonText: '取消',
            type: 'info'
          }
        )

        const res = await approveApply(
          apply.value.applyId,
          localStorage.getItem('userName') || '管理员',
          '审批通过'
        )

        if (res.code === 0) {
          ElMessage.success('审批成功')
          loadApplyDetail()
        } else {
          ElMessage.error(res.message || '审批失败')
        }
      } catch (error) {
        if (error !== 'cancel') {
          ElMessage.error('审批失败: ' + error.message)
        }
      }
    }

    /**
     * Handle reject button
     */
    const handleReject = () => {
      rejectForm.reason = ''
      rejectDialogVisible.value = true
    }

    /**
     * Confirm reject
     */
    const confirmReject = async () => {
      if (!rejectForm.reason.trim()) {
        ElMessage.warning('请输入驳回原因')
        return
      }

      try {
        const res = await rejectApply(
          apply.value.applyId,
          localStorage.getItem('userName') || '管理员',
          rejectForm.reason
        )

        if (res.code === 0) {
          ElMessage.success('驳回成功')
          rejectDialogVisible.value = false
          loadApplyDetail()
        } else {
          ElMessage.error(res.message || '驳回失败')
        }
      } catch (error) {
        ElMessage.error('驳回失败: ' + error.message)
      }
    }

    /**
     * Handle submit button
     */
    const handleSubmit = async () => {
      try {
        await ElMessageBox.confirm(
          '确定要提交此申请吗？提交后将进入审批流程。',
          '提交确认',
          {
            confirmButtonText: '确定提交',
            cancelButtonText: '取消',
            type: 'info'
          }
        )

        const res = await submitApply(apply.value.applyId)

        if (res.code === 0) {
          ElMessage.success('提交成功')
          loadApplyDetail()
        } else {
          ElMessage.error(res.message || '提交失败')
        }
      } catch (error) {
        if (error !== 'cancel') {
          ElMessage.error('提交失败: ' + error.message)
        }
      }
    }

    /**
     * Get status type for display
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

    onMounted(() => {
      loadApplyDetail()
    })

    return {
      loading,
      apply,
      rejectDialogVisible,
      rejectForm,
      handleBack,
      handleApprove,
      handleReject,
      confirmReject,
      handleSubmit,
      getStatusType
    }
  }
}
</script>

<style scoped>
.apply-detail-container {
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
