<template>
  <div class="certificate-lock-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>证书锁定</h2>
      <div class="breadcrumb">当前位置: 证书管理 &gt; 证书锁定</div>
    </div>

    <el-card class="form-card">
      <template #header>
        <div class="card-header">
          <span>证书锁定申请</span>
        </div>
      </template>

      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="140px"
        class="lock-form"
      >
        <!-- Current Certificate Info -->
        <el-divider content-position="left">当前证书信息</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="证书编号" prop="certificateNo">
              <el-input v-model="form.certificateNo" placeholder="输入证书编号" @blur="handleLoadCertificate" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="持证人" prop="workerId">
              <el-input v-model="form.workerId" placeholder="输入人员编号" @blur="handleLoadCertificate" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="资格等级">
              <el-input v-model="form.qualificationLevel" placeholder="输入资格等级" disabled />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="专业类别">
              <el-input v-model="form.profession" placeholder="输入专业类别" disabled />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="有效期开始">
              <el-input v-model="form.validDateStart" placeholder="有效期开始日期" disabled />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="有效期结束">
              <el-input v-model="form.validDateEnd" placeholder="有效期结束日期" disabled />
            </el-form-item>
          </el-col>
        </el-row>

        <!-- Lock Information -->
        <el-divider content-position="left">锁定信息</el-divider>
        <el-form-item label="锁定类型" prop="lockType">
          <el-radio-group v-model="form.lockType">
            <el-radio label="temporary">临时锁定</el-radio>
            <el-radio label="permanent">永久锁定</el-radio>
          </el-radio-group>
        </el-form-item>

        <el-form-item label="锁定期限" v-if="form.lockType === 'temporary'" prop="lockPeriod">
          <el-select v-model="form.lockPeriod" placeholder="选择锁定期限" style="width: 100%">
            <el-option label="1个月" :value="1" />
            <el-option label="3个月" :value="3" />
            <el-option label="6个月" :value="6" />
            <el-option label="12个月" :value="12" />
          </el-select>
          <span style="margin-left: 10px; color: #666;">
            锁定期满后自动解锁
          </span>
        </el-form-item>

        <el-form-item label="锁定原因" prop="lockReason">
          <el-select v-model="form.lockReason" placeholder="选择锁定原因" style="width: 100%">
            <el-option label="违规行为" value="违规行为" />
            <el-option label="投诉处理" value="投诉处理" />
            <el-option label="纪律处分" value="纪律处分" />
            <el-option label="其他原因" value="其他原因" />
          </el-select>
        </el-form-item>

        <el-form-item label="详细说明" prop="description">
          <el-input
            v-model="form.description"
            type="textarea"
            :rows="4"
            placeholder="请详细说明锁定原因"
          />
        </el-form-item>

        <!-- Evidence Upload -->
        <el-form-item label="证据材料">
          <el-upload
            class="upload-demo"
            action="#"
            :auto-upload="false"
            :on-change="handleEvidenceFileChange"
            multiple
          >
            <el-button type="primary">上传证据</el-button>
            <template #tip>
              <div class="el-upload__tip">
                支持上传投诉材料、违规证据等文件
              </div>
            </template>
          </el-upload>
        </el-form-item>

        <!-- Notification -->
        <el-form-item label="通知方式" prop="notification">
          <el-checkbox-group v-model="form.notification">
            <el-checkbox label="sms">短信通知</el-checkbox>
            <el-checkbox label="email">邮件通知</el-checkbox>
            <el-checkbox label="phone">电话通知</el-checkbox>
          </el-checkbox-group>
        </el-form-item>

        <!-- Warning Message -->
        <el-alert
          title="注意：证书锁定期间无法使用，临时锁定到期自动解锁，永久锁定需手动解锁"
          type="warning"
          :closable="false"
          show-icon
        />

        <!-- Form Actions -->
        <el-form-item>
          <el-button type="danger" :loading="submitting" @click="handleSubmit">
            {{ submitting ? '提交中...' : '确认锁定' }}
          </el-button>
          <el-button @click="handleCancel">取消</el-button>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>

<script>
import { ref, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import { lockCertificate, getCertificateById } from '@/api/certificate'

export default {
  name: 'CertificateLock',

  setup() {
    const router = useRouter()
    const formRef = ref(null)
    const submitting = ref(false)
    const loadingCertificate = ref(false)

    const form = reactive({
      certificateId: '',
      certificateNo: '',
      workerId: '',
      workerName: '',
      qualificationLevel: '',
      profession: '',
      validDateStart: '',
      validDateEnd: '',
      lockType: 'temporary',
      lockPeriod: 6,
      lockReason: '',
      description: '',
      notification: ['sms']
    })

    const rules = {
      certificateNo: [{ required: true, message: '请输入证书编号', trigger: 'blur' }],
      workerId: [{ required: true, message: '请输入人员编号', trigger: 'blur' }],
      lockType: [{ required: true, message: '请选择锁定类型', trigger: 'change' }],
      lockPeriod: [{ required: true, message: '请选择锁定期限', trigger: 'change' }],
      lockReason: [{ required: true, message: '请选择锁定原因', trigger: 'change' }],
      description: [{ required: true, message: '请输入详细说明', trigger: 'blur' }],
      notification: [{ type: 'array', required: true, message: '请选择通知方式', trigger: 'change' }]
    }

    /**
     * Handle evidence file change
     */
    const handleEvidenceFileChange = (file) => {
      console.log('Evidence file selected:', file)
    }

    /**
     * Load certificate information
     */
    const handleLoadCertificate = async () => {
      if (!form.certificateNo && !form.workerId) {
        return
      }

      loadingCertificate.value = true
      try {
        // Try to load certificate by number or worker ID
        let res
        if (form.certificateNo) {
          // Assuming there's an API to get certificate by number
          // For now, we'll use the list API and filter
          ElMessage.info('请输入完整信息以加载证书')
          loadingCertificate.value = false
          return
        }

        if (res && res.code === 0) {
          const cert = res.data
          form.certificateId = cert.certificateId
          form.certificateNo = cert.certificateNo
          form.workerId = cert.workerId
          form.workerName = cert.workerName
          form.qualificationLevel = cert.qualificationLevel
          form.profession = cert.profession
          form.validDateStart = cert.validDateStart
          form.validDateEnd = cert.validDateEnd

          if (cert.status !== '有效') {
            ElMessage.warning(`当前证书状态为：${cert.status}，只能锁定有效的证书`)
          }
        }
      } catch (error) {
        ElMessage.error('加载证书信息失败: ' + error.message)
      } finally {
        loadingCertificate.value = false
      }
    }

    /**
     * Handle form submit
     */
    const handleSubmit = async () => {
      if (!formRef.value) return

      try {
        const valid = await formRef.value.validate()
        if (!valid) return

        // Show confirmation dialog
        await ElMessageBox.confirm(
          `确定要${form.lockType === 'temporary' ? '临时' : '永久'}锁定证书 ${form.certificateNo} 吗？${form.lockType === 'temporary' ? '临时锁定到期自动解锁，' : ''}此操作需谨慎。`,
          '锁定确认',
          {
            confirmButtonText: '确定锁定',
            cancelButtonText: '取消',
            type: 'warning'
          }
        )

        submitting.value = true

        // Prepare lock data
        const lockData = {
          certificateId: form.certificateId,
          lockType: form.lockType,
          lockPeriod: form.lockType === 'temporary' ? form.lockPeriod : null,
          lockReason: form.lockReason,
          description: form.description,
          notification: form.notification
        }

        const res = await lockCertificate(form.certificateId, lockData)

        if (res.code === 0) {
          ElMessage.success('证书锁定成功')
          router.push('/certificates')
        } else {
          ElMessage.error(res.message || '证书锁定失败')
        }
      } catch (error) {
        if (error !== 'cancel') {
          submitting.value = false
          ElMessage.error('提交失败: ' + error.message)
        }
      }
    }

    /**
     * Handle cancel
     */
    const handleCancel = () => {
      router.back()
    }

    return {
      formRef,
      form,
      rules,
      submitting,
      loadingCertificate,
      handleEvidenceFileChange,
      handleLoadCertificate,
      handleSubmit,
      handleCancel
    }
  }
}
</script>

<style scoped>
.certificate-lock-container {
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

.card-header {
  font-weight: bold;
  color: #333;
}

.lock-form {
  max-width: 900px;
}
</style>
