<template>
  <div class="apply-change-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>变更申请</h2>
      <div class="breadcrumb">当前位置: 申请管理 &gt; 变更申请</div>
    </div>

    <el-card class="form-card">
      <template #header>
        <div class="card-header">
          <span>变更申请</span>
        </div>
      </template>

      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="140px"
        class="change-form"
      >
        <!-- Original Certificate Info -->
        <el-divider content-position="left">原证书信息</el-divider>
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
            <el-form-item label="原资格等级">
              <el-input v-model="form.oldLevel" placeholder="输入原资格等级" disabled />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="原专业类别">
              <el-input v-model="form.oldProfession" placeholder="输入原专业类别" disabled />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="原单位代码">
              <el-input v-model="form.oldUnitCode" placeholder="输入原单位代码" disabled />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="原单位名称">
              <el-input v-model="form.oldUnitName" placeholder="输入原单位名称" disabled />
            </el-form-item>
          </el-col>
        </el-row>

        <!-- Change Type -->
        <el-divider content-position="left">变更信息</el-divider>
        <el-form-item label="变更类型" prop="changeType">
          <el-radio-group v-model="form.changeType" @change="handleTypeChange">
            <el-radio label="unit">单位变更</el-radio>
            <el-radio label="profession">专业变更</el-radio>
            <el-radio label="level">等级变更</el-radio>
            <el-radio label="other">其他变更</el-radio>
          </el-radio-group>
        </el-form-item>

        <!-- Unit Change -->
        <div v-if="form.changeType === 'unit'" class="change-section">
          <el-row :gutter="20">
            <el-col :span="12">
              <el-form-item label="新单位代码" prop="newUnitCode">
                <el-input v-model="form.newUnitCode" placeholder="输入新单位代码" />
              </el-form-item>
            </el-col>
            <el-col :span="12">
              <el-form-item label="新单位名称" prop="newUnitName">
                <el-input v-model="form.newUnitName" placeholder="输入新单位名称" />
              </el-form-item>
            </el-col>
          </el-row>
        </div>

        <!-- Profession Change -->
        <div v-if="form.changeType === 'profession'" class="change-section">
          <el-form-item label="新专业类别" prop="newProfession">
            <el-select v-model="form.newProfession" placeholder="选择新专业类别" style="width: 100%">
              <el-option label="土木建筑工程" value="土木建筑工程" />
              <el-option label="安装工程" value="安装工程" />
              <el-option label="市政工程" value="市政工程" />
              <el-option label="水利工程" value="水利工程" />
            </el-select>
          </el-form-item>
        </div>

        <!-- Level Change -->
        <div v-if="form.changeType === 'level'" class="change-section">
          <el-form-item label="新资格等级" prop="newLevel">
            <el-select v-model="form.newLevel" placeholder="选择新资格等级" style="width: 100%">
              <el-option label="一级" value="一级" />
              <el-option label="二级" value="二级" />
            </el-select>
          </el-form-item>
        </div>

        <!-- Other Change -->
        <div v-if="form.changeType === 'other'" class="change-section">
          <el-form-item label="变更内容" prop="changeContent">
            <el-input
              v-model="form.changeContent"
              type="textarea"
              :rows="4"
              placeholder="请详细说明变更内容"
            />
          </el-form-item>
        </div>

        <el-form-item label="变更原因" prop="changeReason">
          <el-input
            v-model="form.changeReason"
            type="textarea"
            :rows="4"
            placeholder="输入变更原因"
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
                支持上传变更证明材料、原证书复印件等
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
import { ref, reactive, onMounted } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { ElMessage } from 'element-plus'
import { getCertificateList } from '@/api/certificate'
import { createApply } from '@/api/apply'

export default {
  name: 'ApplyChange',

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
      oldLevel: '',
      oldProfession: '',
      oldUnitCode: '',
      oldUnitName: '',
      changeType: 'unit',
      newUnitCode: '',
      newUnitName: '',
      newProfession: '',
      newLevel: '',
      changeContent: '',
      changeReason: '',
      files: []
    })

    const rules = {
      certificateNo: [{ required: true, message: '请输入证书编号', trigger: 'blur' }],
      workerId: [{ required: true, message: '请输入人员编号', trigger: 'blur' }],
      changeType: [{ required: true, message: '请选择变更类型', trigger: 'change' }],
      newUnitCode: [{ required: true, message: '请输入新单位代码', trigger: 'blur' }],
      newUnitName: [{ required: true, message: '请输入新单位名称', trigger: 'blur' }],
      newProfession: [{ required: true, message: '请选择新专业类别', trigger: 'change' }],
      newLevel: [{ required: true, message: '请选择新资格等级', trigger: 'change' }],
      changeContent: [{ required: true, message: '请输入变更内容', trigger: 'blur' }],
      changeReason: [{ required: true, message: '请输入变更原因', trigger: 'blur' }]
    }

    /**
     * Handle load certificate information
     */
    const handleLoadCertificate = async () => {
      if (!form.certificateNo && !form.workerId) {
        return
      }

      loadingCertificate.value = true
      try {
        const res = await getCertificateList({
          current: 1,
          size: 1,
          certificateNo: form.certificateNo,
          workerId: form.workerId
        })

        if (res.code === 0 && res.data.records.length > 0) {
          const cert = res.data.records[0]
          form.certificateId = cert.certificateId
          form.certificateNo = cert.certificateNo
          form.workerId = cert.workerId
          form.oldLevel = cert.qualificationLevel
          form.oldProfession = cert.profession
          form.oldUnitCode = cert.unitCode
          form.oldUnitName = cert.unitName
          ElMessage.success('证书信息加载成功')
        } else {
          ElMessage.warning('未找到相关证书信息')
        }
      } catch (error) {
        ElMessage.error('加载证书信息失败: ' + error.message)
      } finally {
        loadingCertificate.value = false
      }
    }

    /**
     * Handle change type change
     */
    const handleTypeChange = (value) => {
      // Reset change-specific fields
      form.newUnitCode = ''
      form.newUnitName = ''
      form.newProfession = ''
      form.newLevel = ''
      form.changeContent = ''
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

        // Prepare application data
        const applyData = {
          applyType: '变更注册',
          workerId: form.workerId,
          certificateId: form.certificateId,
          changeType: form.changeType,
          newUnitCode: form.newUnitCode,
          newUnitName: form.newUnitName,
          newProfession: form.newProfession,
          newLevel: form.newLevel,
          changeContent: form.changeContent,
          changeReason: form.changeReason,
          applyDate: new Date().toISOString().split('T')[0]
        }

        const res = await createApply(applyData)

        if (res.code === 0) {
          ElMessage.success('变更申请提交成功')
          router.push('/apply/list')
        } else {
          ElMessage.error(res.message || '申请提交失败')
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
        form.certificateNo = route.query.certificateNo || ''
        handleLoadCertificate()
      }
    })

    return {
      formRef,
      form,
      rules,
      submitting,
      loadingCertificate,
      handleLoadCertificate,
      handleTypeChange,
      handleFileChange,
      handleSubmit,
      handleCancel
    }
  }
}
</script>

<style scoped>
.apply-change-container {
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

.change-form {
  max-width: 900px;
}

.change-section {
  padding: 10px 0;
  background: #f5f7fa;
  border-radius: 4px;
  margin-bottom: 20px;
}
</style>
