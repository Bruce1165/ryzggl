<template>
  <div class="certificate-continue-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>证书延续</h2>
      <div class="breadcrumb">当前位置: 证书管理 &gt; 证书延续</div>
    </div>

    <el-card class="form-card">
      <template #header>
        <div class="card-header">
          <span>证书延续申请</span>
        </div>
      </template>

      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="140px"
        class="continue-form"
      >
        <!-- Current Certificate Info -->
        <el-divider content-position="left">当前证书信息</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="证书编号">
              <el-input v-model="form.certificateNo" placeholder="输入证书编号" @blur="handleLoadCertificate" :loading="loadingCertificate" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="持证人">
              <el-input v-model="form.workerName" placeholder="输入持证人姓名" @blur="handleLoadCertificate" :loading="loadingCertificate" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="有效期结束">
              <el-input v-model="form.validDateEnd" placeholder="当前有效期结束日期" disabled />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="资格等级">
              <el-input v-model="form.qualificationLevel" placeholder="资格等级" disabled />
            </el-form-item>
          </el-col>
        </el-row>

        <!-- Continue Information -->
        <el-divider content-position="left">延续信息</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="延续年限" prop="continueYears">
              <el-select v-model="form.continueYears" placeholder="选择延续年限" style="width: 100%">
                <el-option label="1年" :value="1" />
                <el-option label="2年" :value="2" />
                <el-option label="3年" :value="3" />
                <el-option label="5年" :value="5" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="新有效期结束" prop="newValidDateEnd">
              <el-date-picker
                v-model="form.newValidDateEnd"
                type="date"
                placeholder="选择新有效期结束日期"
                style="width: 100%"
                value-format="YYYY-MM-DD"
              />
            </el-form-item>
          </el-col>
        </el-row>

        <el-form-item label="延续原因" prop="continueReason">
          <el-input
            v-model="form.continueReason"
            type="textarea"
            :rows="4"
            placeholder="输入延续原因"
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
                支持上传身份证、学历证明、继续教育证明等材料
              </div>
            </template>
          </el-upload>
        </el-form-item>

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
import { ref, reactive, onMounted, computed } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { ElMessage } from 'element-plus'
import { getCertificateById, continueCertificate } from '@/api/certificate'
import dayjs from 'dayjs'

export default {
  name: 'CertificateContinue',

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
      validDateStart: '',
      validDateEnd: '',
      qualificationLevel: '',
      profession: '',
      continueYears: 1,
      newValidDateEnd: '',
      continueReason: '',
      files: []
    })

    const rules = {
      continueYears: [{ required: true, message: '请选择延续年限', trigger: 'change' }],
      newValidDateEnd: [{ required: true, message: '请选择新有效期结束日期', trigger: 'change' }],
      continueReason: [{ required: true, message: '请输入延续原因', trigger: 'blur' }]
    }

    /**
     * Calculate new valid date
     */
    const calculatedNewValidDate = computed(() => {
      if (form.validDateEnd && form.continueYears) {
        return dayjs(form.validDateEnd).add(form.continueYears, 'year').format('YYYY-MM-DD')
      }
      return ''
    })

    /**
     * Load certificate information
     */
    const handleLoadCertificate = async () => {
      if (!form.certificateNo && !form.workerName) {
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
          form.validDateStart = cert.validDateStart
          form.validDateEnd = cert.validDateEnd
          form.qualificationLevel = cert.qualificationLevel
          form.profession = cert.profession
          form.newValidDateEnd = calculatedNewValidDate.value

          if (cert.status !== '有效') {
            ElMessage.warning(`当前证书状态为：${cert.status}，只能延续有效的证书`)
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

        // Prepare continue data
        const continueData = {
          certificateId: form.certificateId,
          years: form.continueYears,
          newValidDateEnd: form.newValidDateEnd || calculatedNewValidDate.value,
          reason: form.continueReason
        }

        const res = await continueCertificate(form.certificateId, continueData)

        if (res.code === 0) {
          ElMessage.success('延续申请提交成功')
          router.push('/certificates')
        } else {
          ElMessage.error(res.message || '延续申请失败')
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
      calculatedNewValidDate,
      handleLoadCertificate,
      handleFileChange,
      handleSubmit,
      handleCancel
    }
  }
}
</script>

<style scoped>
.certificate-continue-container {
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

.continue-form {
  max-width: 900px;
}
</style>
