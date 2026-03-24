<template>
  <div class="certificate-pause-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>证书暂停</h2>
      <div class="breadcrumb">当前位置: 证书管理 &gt; 证书暂停</div>
    </div>

    <el-card class="form-card">
      <template #header>
        <div class="card-header">
          <span>证书暂停申请</span>
        </div>
      </template>

      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="140px"
        class="pause-form"
      >
        <!-- Current Certificate Info -->
        <el-divider content-position="left">当前证书信息</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="证书编号" prop="certificateNo">
              <el-input v-model="form.certificateNo" placeholder="输入证书编号" @blur="handleLoadCertificate" :loading="loadingCertificate" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="持证人" prop="workerId">
              <el-input v-model="form.workerId" placeholder="输入人员编号" @blur="handleLoadCertificate" :loading="loadingCertificate" />
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
              <el-input v-model="form.validDateStart" placeholder="输入有效期开始日期" disabled />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="有效期结束">
              <el-input v-model="form.validDateEnd" placeholder="输入有效期结束日期" disabled />
            </el-form-item>
          </el-col>
        </el-row>

        <!-- Pause Information -->
        <el-divider content-position="left">暂停信息</el-divider>
        <el-form-item label="暂停开始日期" prop="pauseStartDate">
          <el-date-picker
            v-model="form.pauseStartDate"
            type="date"
            placeholder="选择暂停开始日期"
            style="width: 100%"
            value-format="YYYY-MM-DD"
          />
        </el-form-item>

        <el-form-item label="暂停期限" prop="pausePeriod">
          <el-select v-model="form.pausePeriod" placeholder="选择暂停期限" style="width: 100%">
            <el-option label="1个月" :value="1" />
            <el-option label="3个月" :value="3" />
            <el-option label="6个月" :value="6" />
            <el-option label="12个月" :value="12" />
            <el-option label="24个月" :value="24" />
          </el-select>
          <span style="margin-left: 10px; color: #666;">
            暂停期限最长不超过2年
          </span>
        </el-form-item>

        <el-form-item label="预计恢复日期">
          <el-input :value="calculateResumeDate" disabled placeholder="自动计算" />
        </el-form-item>

        <el-form-item label="暂停原因" prop="pauseReason">
          <el-select v-model="form.pauseReason" placeholder="选择暂停原因" style="width: 100%">
            <el-option label="个人原因" value="个人原因" />
            <el-option label="疾病原因" value="疾病原因" />
            <el-option label="出国学习" value="出国学习" />
            <el-option label="待业" value="待业" />
            <el-option label="其他原因" value="其他原因" />
          </el-select>
        </el-form-item>

        <el-form-item label="详细说明" prop="description">
          <el-input
            v-model="form.description"
            type="textarea"
            :rows="4"
            placeholder="请详细说明暂停原因"
          />
        </el-form-item>

        <!-- Attachments -->
        <el-form-item label="附件上传">
          <el-upload
            class="upload-demo"
            action="#"
            :auto-upload="false"
            :on-change="handleFileChange"
            multiple
          >
            <el-button type="primary">上传附件</el-button>
            <template #tip>
              <div class="el-upload__tip">
                支持上传医疗证明、出国证明等相关材料
              </div>
            </template>
          </el-upload>
        </el-form-item>

        <!-- Warning Message -->
        <el-alert
          title="注意：证书暂停期间不得从事相关执业活动，暂停期结束后需办理恢复手续"
          type="warning"
          :closable="false"
          show-icon
        />

        <!-- Form Actions -->
        <el-form-item>
          <el-button type="primary" :loading="submitting" @click="handleSubmit">
            {{ submitting ? '提交中...' : '提交申请' }}
          </el-button>
          <el-button @click="handleCancel">取消</el-button>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>

<script>
import { ref, reactive, computed } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { ElMessage } from 'element-plus'
import { getCertificateById, pauseCertificate } from '@/api/certificate'
import dayjs from 'dayjs'

export default {
  name: 'CertificatePause',

  setup() {
    const router = useRouter()
    const route = useRoute()
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
      pauseStartDate: dayjs().format('YYYY-MM-DD'),
      pausePeriod: 6,
      pauseReason: '',
      description: '',
      files: []
    })

    const rules = {
      certificateNo: [{ required: true, message: '请输入证书编号', trigger: 'blur' }],
      workerId: [{ required: true, message: '请输入人员编号', trigger: 'blur' }],
      pauseStartDate: [{ required: true, message: '请选择暂停开始日期', trigger: 'change' }],
      pausePeriod: [{ required: true, message: '请选择暂停期限', trigger: 'change' }],
      pauseReason: [{ required: true, message: '请选择暂停原因', trigger: 'change' }],
      description: [{ required: true, message: '请输入详细说明', trigger: 'blur' }]
    }

    /**
     * Calculate resume date
     */
    const calculateResumeDate = computed(() => {
      if (form.pauseStartDate && form.pausePeriod) {
        return dayjs(form.pauseStartDate).add(form.pausePeriod, 'month').format('YYYY-MM-DD')
      }
      return ''
    })

    /**
     * Load certificate information
     */
    const handleLoadCertificate = async () => {
      if (!form.certificateNo && !form.workerId) {
        return
      }

      loadingCertificate.value = true
      try {
        const res = await getCertificateById(route.query.certificateId || form.certificateNo)

        if (res.code === 0) {
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
            ElMessage.warning(`当前证书状态为：${cert.status}，只能暂停有效的证书`)
          }
        } else {
          ElMessage.error(res.message || '加载证书信息失败')
        }
      } catch (error) {
        ElMessage.error('加载证书信息失败: ' + error.message)
      } finally {
        loadingCertificate.value = false
      }
    }

    /**
     * Handle file change
     */
    const handleFileChange = (file) => {
      // Validate file size (max 10MB)
      if (file.size > 10 * 1024 * 1024) {
        ElMessage.error('文件大小不能超过10MB')
        return false
      }

      // Validate file type
      const allowedTypes = [
        'application/pdf',
        'application/msword',
        'application/vnd.openxmlformats-officedocument.wordprocessingml.document',
        'image/jpeg',
        'image/png'
      ]

      if (!allowedTypes.includes(file.raw.type)) {
        ElMessage.error('只支持上传PDF、Word、JPG、PNG格式的文件')
        return false
      }

      form.files = [...form.files, {
        name: file.name,
        size: file.size,
        type: file.raw.type,
        file: file.raw
      }]

      return true
    }

    /**
     * Handle form submit
     */
    const handleSubmit = async () => {
      if (!formRef.value) return

      try {
        const valid = await formRef.value.validate()
        if (!valid) return

        submitting.value = true

        // Prepare pause data
        const pauseData = {
          certificateId: form.certificateId,
          pauseStartDate: form.pauseStartDate,
          pausePeriod: form.pausePeriod,
          pauseReason: form.pauseReason,
          description: form.description,
          resumeDate: calculateResumeDate.value
        }

        const res = await pauseCertificate(form.certificateId, `${form.pauseReason}: ${form.description}`)

        if (res.code === 0) {
          ElMessage.success('暂停申请提交成功')
          router.push('/certificates')
        } else {
          ElMessage.error(res.message || '暂停申请失败')
        }
      } catch (error) {
        ElMessage.error('提交失败: ' + error.message)
      } finally {
        submitting.value = false
      }
    }

    /**
     * Handle cancel
     */
    const handleCancel = () => {
      router.back()
    }

    // Load certificate data if certificate ID is provided in route
    onMounted(() => {
      if (route.query.certificateId) {
        handleLoadCertificate()
      }
    })

    return {
      formRef,
      form,
      rules,
      submitting,
      loadingCertificate,
      calculateResumeDate,
      handleLoadCertificate,
      handleFileChange,
      handleSubmit,
      handleCancel
    }
  }
}
</script>

<style scoped>
.certificate-pause-container {
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

.pause-form {
  max-width: 900px;
}
</style>
