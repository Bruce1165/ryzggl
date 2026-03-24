<template>
  <div class="apply-cancel-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>注销申请</h2>
      <div class="breadcrumb">当前位置: 申请管理 &gt; 注销申请</div>
    </div>

    <el-card class="form-card">
      <template #header>
        <div class="card-header">
          <span>证书注销申请</span>
        </div>
      </template>

      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="140px"
        class="cancel-form"
      >
        <!-- Certificate Info -->
        <el-divider content-position="left">证书信息</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="证书编号" prop="certificateNo">
              <el-input v-model="form.certificateNo" placeholder="输入证书编号" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="持证人" prop="workerId">
              <el-input v-model="form.workerId" placeholder="输入人员编号" />
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
            <el-form-item label="有效期结束">
              <el-input v-model="form.validDateEnd" placeholder="输入有效期结束日期" disabled />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="当前状态">
              <el-tag type="success">有效</el-tag>
            </el-form-item>
          </el-col>
        </el-row>

        <!-- Cancel Reason -->
        <el-divider content-position="left">注销信息</el-divider>
        <el-form-item label="注销原因" prop="cancelReason">
          <el-select v-model="form.cancelReason" placeholder="选择注销原因" style="width: 100%">
            <el-option label="人员离职" value="人员离职" />
            <el-option label="退休" value="退休" />
            <el-option label="证书过期" value="证书过期" />
            <el-option label="证书遗失" value="证书遗失" />
            <el-option label="其他原因" value="其他原因" />
          </el-select>
        </el-form-item>

        <el-form-item label="详细说明" prop="description">
          <el-input
            v-model="form.description"
            type="textarea"
            :rows="4"
            placeholder="请详细说明注销原因"
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
                支持上传身份证、证明材料等文件
              </div>
            </template>
          </el-upload>
        </el-form-item>

        <!-- Warning Message -->
        <el-alert
          title="注意：证书注销后不可恢复，请确认后再提交申请"
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
import { ref, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'

export default {
  name: 'ApplyCancel',

  setup() {
    const router = useRouter()
    const formRef = ref(null)
    const submitting = ref(false)

    const form = reactive({
      certificateNo: '',
      workerId: '',
      qualificationLevel: '',
      profession: '',
      validDateEnd: '',
      cancelReason: '',
      description: ''
    })

    const rules = {
      certificateNo: [{ required: true, message: '请输入证书编号', trigger: 'blur' }],
      workerId: [{ required: true, message: '请输入人员编号', trigger: 'blur' }],
      cancelReason: [{ required: true, message: '请选择注销原因', trigger: 'change' }],
      description: [{ required: true, message: '请输入详细说明', trigger: 'blur' }]
    }

    /**
     * Handle file change
     */
    const handleFileChange = (file) => {
      console.log('File selected:', file)
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

        // TODO: Implement with actual API call
        ElMessage.info('注销申请功能待实现')

        setTimeout(() => {
          ElMessage.success('注销申请提交成功')
          router.push('/apply/list')
          submitting.value = false
        }, 1000)
      } catch (error) {
        submitting.value = false
        ElMessage.error('提交失败: ' + error.message)
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
      handleFileChange,
      handleSubmit,
      handleCancel
    }
  }
}
</script>

<style scoped>
.apply-cancel-container {
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

.cancel-form {
  max-width: 900px;
}
</style>
