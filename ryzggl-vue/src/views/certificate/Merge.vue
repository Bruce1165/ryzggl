<template>
  <div class="certificate-merge-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>证书合并</h2>
      <div class="breadcrumb">当前位置: 证书管理 &gt; 证书合并</div>
    </div>

    <el-card class="form-card">
      <template #header>
        <div class="card-header">
          <span>证书合并申请</span>
        </div>
      </template>

      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="140px"
        class="merge-form"
      >
        <!-- Source Certificates -->
        <el-divider content-position="left">合并证书信息</el-divider>
        <el-alert
          title="说明：将多个证书合并为一个新证书，原证书将被注销"
          type="info"
          :closable="false"
          show-icon
          style="margin-bottom: 20px;"
        />

        <el-form-item label="人员编号" prop="workerId">
          <el-input v-model="form.workerId" placeholder="输入人员编号" />
          <el-button type="primary" style="margin-left: 10px" @click="handleLoadCertificates" :loading="loadingCertificates">
            查询证书
          </el-button>
        </el-form-item>

        <el-form-item label="待合并证书" prop="sourceCertificates">
          <el-checkbox-group v-model="form.sourceCertificates" @change="handleCertificateChange">
            <el-checkbox
              v-for="cert in availableCertificates"
              :key="cert.certificateId"
              :label="cert.certificateId"
              :disabled="cert.status !== '有效'"
            >
              {{ cert.certificateNo }} - {{ cert.qualificationLevel }} - {{ cert.profession }}
              ({{ cert.validDateEnd }})
            </el-checkbox>
          </el-checkbox-group>
        </el-form-item>

        <el-form-item label="已选择证书">
          <el-tag
            v-for="certId in selectedCertificatesDetails"
            :key="certId.certificateId"
            type="info"
            closable
            @close="handleRemoveCertificate(certId.certificateId)"
            style="margin-right: 10px; margin-bottom: 10px;"
          >
            {{ certId.certificateNo }}
          </el-tag>
          <span v-if="selectedCertificatesDetails.length === 0" style="color: #999;">
            未选择证书
          </span>
        </el-form-item>

        <!-- Merge Options -->
        <el-divider content-position="left">合并选项</el-divider>
        <el-form-item label="合并类型" prop="mergeType">
          <el-radio-group v-model="form.mergeType">
            <el-radio label="same">同专业合并</el-radio>
            <el-radio label="cross">跨专业合并</el-radio>
          </el-radio-group>
        </el-form-item>

        <el-form-item label="新证书等级" prop="newLevel">
          <el-select v-model="form.newLevel" placeholder="选择新证书等级" style="width: 100%" :disabled="form.sourceCertificates.length === 0">
            <el-option label="一级" value="一级" />
            <el-option label="二级" value="二级" />
          </el-select>
        </el-form-item>

        <el-form-item label="新专业类别" prop="newProfession">
          <el-select v-model="form.newProfession" placeholder="选择新专业类别" style="width: 100%" :disabled="form.sourceCertificates.length === 0">
            <el-option label="土木建筑工程" value="土木建筑工程" />
            <el-option label="安装工程" value="安装工程" />
            <el-option label="市政工程" value="市政工程" />
            <el-option label="水利工程" value="水利工程" />
          </el-select>
        </el-form-item>

        <el-form-item label="新有效期计算" prop="validityCalc">
          <el-select v-model="form.validityCalc" placeholder="选择有效期计算方式" style="width: 100%">
            <el-option label="取最早有效期" value="earliest" />
            <el-option label="取最晚有效期" value="latest" />
            <el-option label="重新计算" value="recalculate" />
          </el-select>
        </el-form-item>

        <el-form-item label="预计新有效期">
          <el-input :value="calculateNewValidDate" disabled placeholder="自动计算" />
        </el-form-item>

        <el-form-item label="合并原因" prop="mergeReason">
          <el-input
            v-model="form.mergeReason"
            type="textarea"
            :rows="4"
            placeholder="请输入合并原因"
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
                支持上传身份证、原证书等材料
              </div>
            </template>
          </el-upload>
        </el-form-item>

        <!-- Warning Message -->
        <el-alert
          title="注意：证书合并后，原证书将被注销，不可恢复，请确认后再提交"
          type="warning"
          :closable="false"
          show-icon
        />

        <!-- Form Actions -->
        <el-form-item>
          <el-button type="primary" :loading="submitting" @click="handleSubmit">
            {{ submitting ? '提交中...' : '提交合并申请' }}
          </el-button>
          <el-button @click="handleCancel">取消</el-button>
        </el-form-item>
      </el-form>
    </el-card>
  </div>
</template>

<script>
import { ref, reactive, computed } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'
import { getCertificateList, mergeCertificates } from '@/api/certificate'

export default {
  name: 'CertificateMerge',

  setup() {
    const router = useRouter()
    const formRef = ref(null)
    const submitting = ref(false)
    const loadingCertificates = ref(false)

    const form = reactive({
      workerId: '',
      sourceCertificates: [],
      mergeType: 'same',
      newLevel: '',
      newProfession: '',
      validityCalc: 'latest',
      mergeReason: '',
      files: []
    })

    const availableCertificates = ref([])

    const rules = {
      workerId: [{ required: true, message: '请输入人员编号', trigger: 'blur' }],
      sourceCertificates: [
        { type: 'array', min: 2, message: '至少选择2个证书', trigger: 'change' }
      ],
      mergeType: [{ required: true, message: '请选择合并类型', trigger: 'change' }],
      newLevel: [{ required: true, message: '请选择新证书等级', trigger: 'change' }],
      newProfession: [{ required: true, message: '请选择新专业类别', trigger: 'change' }],
      validityCalc: [{ required: true, message: '请选择有效期计算方式', trigger: 'change' }],
      mergeReason: [{ required: true, message: '请输入合并原因', trigger: 'blur' }]
    }

    /**
     * Get selected certificates details
     */
    const selectedCertificatesDetails = computed(() => {
      return availableCertificates.value.filter(cert =>
        form.sourceCertificates.includes(cert.certificateId)
      )
    })

    /**
     * Calculate new valid date
     */
    const calculateNewValidDate = computed(() => {
      if (selectedCertificatesDetails.value.length === 0) return ''

      const dates = selectedCertificatesDetails.value
        .filter(cert => cert.validDateEnd)
        .map(cert => new Date(cert.validDateEnd))

      if (dates.length === 0) return ''

      let resultDate
      if (form.validityCalc === 'earliest') {
        resultDate = new Date(Math.min(...dates))
      } else if (form.validityCalc === 'latest') {
        resultDate = new Date(Math.max(...dates))
      } else {
        // Recalculate from latest date
        resultDate = new Date(Math.max(...dates))
        // Add some logic here for recalculation
      }

      return resultDate.toISOString().split('T')[0]
    })

    /**
     * Handle load certificates
     */
    const handleLoadCertificates = async () => {
      if (!form.workerId) {
        ElMessage.warning('请先输入人员编号')
        return
      }

      loadingCertificates.value = true
      try {
        const res = await getCertificateList({
          current: 1,
          size: 100,
          workerId: form.workerId,
          status: '有效'
        })

        if (res.code === 0) {
          availableCertificates.value = res.data.records || []
          ElMessage.success(`加载到 ${availableCertificates.value.length} 个有效证书`)

          if (availableCertificates.value.length < 2) {
            ElMessage.warning('需要至少2个有效证书才能进行合并')
          }
        } else {
          ElMessage.error(res.message || '加载证书失败')
        }
      } catch (error) {
        ElMessage.error('加载证书失败: ' + error.message)
      } finally {
        loadingCertificates.value = false
      }
    }

    /**
     * Handle certificate change
     */
    const handleCertificateChange = (value) => {
      console.log('Selected certificates:', value)
    }

    /**
     * Handle remove certificate
     */
    const handleRemoveCertificate = (certificateId) => {
      const index = form.sourceCertificates.indexOf(certificateId)
      if (index > -1) {
        form.sourceCertificates.splice(index, 1)
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

        if (form.sourceCertificates.length < 2) {
          ElMessage.warning('至少选择2个证书进行合并')
          return
        }

        submitting.value = true

        // Prepare merge data
        const mergeData = {
          workerId: form.workerId,
          sourceCertificateIds: form.sourceCertificates,
          mergeType: form.mergeType,
          newLevel: form.newLevel,
          newProfession: form.newProfession,
          validityCalc: form.validityCalc,
          mergeReason: form.mergeReason,
          calculatedNewValidDate: calculateNewValidDate.value
        }

        const res = await mergeCertificates(mergeData)

        if (res.code === 0) {
          ElMessage.success('合并申请提交成功')
          router.push('/certificates')
        } else {
          ElMessage.error(res.message || '合并申请失败')
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

    return {
      formRef,
      form,
      rules,
      availableCertificates,
      selectedCertificatesDetails,
      calculateNewValidDate,
      submitting,
      loadingCertificates,
      handleLoadCertificates,
      handleCertificateChange,
      handleRemoveCertificate,
      handleFileChange,
      handleSubmit,
      handleCancel
    }
  }
}
</script>

<style scoped>
.certificate-merge-container {
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

.merge-form {
  max-width: 900px;
}
</style>
