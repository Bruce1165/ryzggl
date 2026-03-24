<template>
  <div class="apply-renew-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>延续申请</h2>
      <div class="breadcrumb">当前位置: 申请管理 &gt; 延续申请</div>
    </div>

    <el-card class="form-card">
      <template #header>
        <div class="card-header">
          <span>延续申请</span>
        </div>
      </template>

      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="140px"
        class="renew-form"
      >
        <!-- Current Certificate Info -->
        <el-divider content-position="left">当前证书信息</el-divider>
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
            <el-form-item label="当前有效期结束">
              <el-input v-model="form.currentEndDate" placeholder="输入当前有效期结束日期" disabled />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="资格等级">
              <el-input v-model="form.qualificationLevel" placeholder="输入资格等级" disabled />
            </el-form-item>
          </el-col>
        </el-row>

        <!-- Renew Information -->
        <el-divider content-position="left">延续信息</el-divider>
        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="延续年限" prop="renewYears">
              <el-select v-model="form.renewYears" placeholder="选择延续年限" style="width: 100%">
                <el-option label="1年" :value="1" />
                <el-option label="2年" :value="2" />
                <el-option label="3年" :value="3" />
                <el-option label="5年" :value="5" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="新有效期结束" prop="newEndDate">
              <el-date-picker
                v-model="form.newEndDate"
                type="date"
                placeholder="选择新有效期结束日期"
                style="width: 100%"
                value-format="YYYY-MM-DD"
              />
            </el-form-item>
          </el-col>
        </el-row>

        <el-form-item label="继续教育情况" prop="educationStatus">
          <el-radio-group v-model="form.educationStatus">
            <el-radio label="completed">已完成</el-radio>
            <el-radio label="inProgress">进行中</el-radio>
            <el-radio label="notStarted">未开始</el-radio>
          </el-radio-group>
        </el-form-item>

        <el-form-item label="继续教育证明">
          <el-upload
            class="upload-demo"
            action="#"
            :auto-upload="false"
            :on-change="handleEducationFileChange"
          >
            <el-button type="primary">上传继续教育证明</el-button>
          </el-upload>
        </el-form-item>

        <el-form-item label="延续原因" prop="renewReason">
          <el-input
            v-model="form.renewReason"
            type="textarea"
            :rows="4"
            placeholder="输入延续原因"
          />
        </el-form-item>

        <!-- Attachments -->
        <el-form-item label="其他附件">
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
                支持上传身份证、学历证明等材料
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
import { ref, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage } from 'element-plus'

export default {
  name: 'ApplyRenew',

  setup() {
    const router = useRouter()
    const formRef = ref(null)
    const submitting = ref(false)

    const form = reactive({
      certificateNo: '',
      workerId: '',
      currentEndDate: '',
      qualificationLevel: '',
      renewYears: 3,
      newEndDate: '',
      educationStatus: 'completed',
      renewReason: ''
    })

    const rules = {
      certificateNo: [{ required: true, message: '请输入证书编号', trigger: 'blur' }],
      workerId: [{ required: true, message: '请输入人员编号', trigger: 'blur' }],
      renewYears: [{ required: true, message: '请选择延续年限', trigger: 'change' }],
      newEndDate: [{ required: true, message: '请选择新有效期结束日期', trigger: 'change' }],
      educationStatus: [{ required: true, message: '请选择继续教育情况', trigger: 'change' }],
      renewReason: [{ required: true, message: '请输入延续原因', trigger: 'blur' }]
    }

    /**
     * Handle file change
     */
    const handleFileChange = (file) => {
      console.log('File selected:', file)
    }

    /**
     * Handle education file change
     */
    const handleEducationFileChange = (file) => {
      console.log('Education file selected:', file)
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
        ElMessage.info('延续申请功能待实现')

        setTimeout(() => {
          ElMessage.success('延续申请提交成功')
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
      handleEducationFileChange,
      handleSubmit,
      handleCancel
    }
  }
}
</script>

<style scoped>
.apply-renew-container {
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

.renew-form {
  max-width: 900px;
}
</style>
