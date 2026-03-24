<template>
  <div class="apply-replace-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>补办申请</h2>
      <div class="breadcrumb">当前位置: 申请管理 &gt; 补办申请</div>
    </div>

    <el-card class="form-card">
      <template #header>
        <div class="card-header">
          <span>证书补办申请</span>
        </div>
      </template>

      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="140px"
        class="replace-form"
      >
        <!-- Certificate Info -->
        <el-divider content-position="left">原证书信息</el-divider>
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
            <el-form-item label="发证日期">
              <el-input v-model="form.issueDate" placeholder="输入发证日期" disabled />
            </el-form-item>
          </el-col>
        </el-row>

        <!-- Replace Reason -->
        <el-divider content-position="left">补办信息</el-divider>
        <el-form-item label="补办原因" prop="replaceReason">
          <el-select v-model="form.replaceReason" placeholder="选择补办原因" style="width: 100%">
            <el-option label="证书遗失" value="证书遗失" />
            <el-option label="证书损毁" value="证书损毁" />
            <el-option label="证书污损" value="证书污损" />
            <el-option label="信息错误" value="信息错误" />
            <el-option label="其他原因" value="其他原因" />
          </el-select>
        </el-form-item>

        <el-form-item label="遗失声明" v-if="form.replaceReason === '证书遗失'">
          <el-checkbox v-model="form.declaration">
            我已知晓，证书遗失补办需要提供遗失声明
          </el-checkbox>
        </el-form-item>

        <el-form-item label="补办说明" prop="description">
          <el-input
            v-model="form.description"
            type="textarea"
            :rows="4"
            placeholder="请详细说明补办原因"
          />
        </el-form-item>

        <el-form-item label="是否加急" prop="urgent">
          <el-radio-group v-model="form.urgent">
            <el-radio :label="false">普通</el-radio>
            <el-radio :label="true">加急</el-radio>
          </el-radio-group>
          <el-tag v-if="form.urgent" type="danger" style="margin-left: 20px">
            加急需要额外收费
          </el-tag>
        </el-form-item>

        <!-- Attachments -->
        <el-form-item label="附件上传" prop="attachments">
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
                支持上传身份证、遗失声明、损毁证明等材料
              </div>
            </template>
          </el-upload>
        </el-form-item>

        <!-- Fee Information -->
        <el-alert
          title="补办费用：50元（普通），100元（加急）"
          type="info"
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
  name: 'ApplyReplace',

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
      issueDate: '',
      replaceReason: '',
      declaration: false,
      description: '',
      urgent: false
    })

    const rules = {
      certificateNo: [{ required: true, message: '请输入证书编号', trigger: 'blur' }],
      workerId: [{ required: true, message: '请输入人员编号', trigger: 'blur' }],
      replaceReason: [{ required: true, message: '请选择补办原因', trigger: 'change' }],
      description: [{ required: true, message: '请输入补办说明', trigger: 'blur' }],
      declaration: [{
        validator: (rule, value, callback) => {
          if (form.replaceReason === '证书遗失' && !value) {
            callback(new Error('请确认遗失声明'))
          } else {
            callback()
          }
        },
        trigger: 'change'
      }]
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
        ElMessage.info('补办申请功能待实现')

        setTimeout(() => {
          ElMessage.success('补办申请提交成功')
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
.apply-replace-container {
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

.replace-form {
  max-width: 900px;
}
</style>
