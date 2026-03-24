<template>
  <div class="apply-create-container">
    <!-- App Header -->
    <AppHeader />

    <!-- Page Header -->
    <div class="page-header">
      <h2>首次申请</h2>
      <div class="breadcrumb">当前位置: 申请管理 &gt; 首次申请</div>
    </div>

    <!-- Application Form -->
    <el-card class="form-card">
      <template #header>
        <div class="card-header">
          <span>申请信息</span>
        </div>
      </template>

      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="120px"
        class="apply-form"
      >
        <el-row :gutter="20">
          <!-- Worker Information -->
          <el-col :span="12">
            <el-form-item label="人员编号" prop="workerId">
              <el-input v-model="form.workerId" placeholder="输入人员编号" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="人员姓名" prop="workerName">
              <el-input v-model="form.workerName" placeholder="输入人员姓名" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="身份证号" prop="idCard">
              <el-input v-model="form.idCard" placeholder="输入身份证号" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="联系电话" prop="phone">
              <el-input v-model="form.phone" placeholder="输入联系电话" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-divider content-position="left">单位信息</el-divider>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="部门代码" prop="unitCode">
              <el-input v-model="form.unitCode" placeholder="输入部门代码" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="部门名称" prop="unitName">
              <el-input v-model="form.unitName" placeholder="输入部门名称" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-divider content-position="left">申请信息</el-divider>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="申请类型" prop="applyType">
              <el-select v-model="form.applyType" placeholder="选择申请类型" style="width: 100%">
                <el-option label="首次注册" value="首次注册" />
                <el-option label="延续注册" value="延续注册" />
                <el-option label="变更注册" value="变更注册" />
                <el-option label="注销注册" value="注销注册" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="资格等级" prop="qualificationLevel">
              <el-select v-model="form.qualificationLevel" placeholder="选择资格等级" style="width: 100%">
                <el-option label="一级" value="一级" />
                <el-option label="二级" value="二级" />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="专业类别" prop="profession">
              <el-select v-model="form.profession" placeholder="选择专业类别" style="width: 100%">
                <el-option label="土木建筑工程" value="土木建筑工程" />
                <el-option label="安装工程" value="安装工程" />
                <el-option label="市政工程" value="市政工程" />
                <el-option label="水利工程" value="水利工程" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="申请日期" prop="applyDate">
              <el-date-picker
                v-model="form.applyDate"
                type="date"
                placeholder="选择申请日期"
                style="width: 100%"
                value-format="YYYY-MM-DD"
              />
            </el-form-item>
          </el-col>
        </el-row>

        <el-form-item label="备注" prop="remark">
          <el-input
            v-model="form.remark"
            type="textarea"
            :rows="4"
            placeholder="输入备注信息"
          />
        </el-form-item>

        <el-form-item label="附件">
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
import { createApply } from '@/api/apply'
import AppHeader from '@/components/AppHeader.vue'

export default {
  name: 'ApplyCreate',

  components: {
    AppHeader
  },

  setup() {
    const router = useRouter()
    const formRef = ref(null)
    const submitting = ref(false)

    const form = reactive({
      workerId: '',
      workerName: '',
      idCard: '',
      phone: '',
      unitCode: '',
      unitName: '',
      applyType: '首次注册',
      qualificationLevel: '',
      profession: '',
      applyDate: '',
      remark: '',
      files: []
    })

    const rules = {
      workerId: [{ required: true, message: '请输入人员编号', trigger: 'blur' }],
      workerName: [{ required: true, message: '请输入人员姓名', trigger: 'blur' }],
      idCard: [
        { required: true, message: '请输入身份证号', trigger: 'blur' },
        { pattern: /^[1-9]\d{5}(18|19|20)\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{3}[\dXx]$/, message: '请输入正确的身份证号', trigger: 'blur' }
      ],
      phone: [
        { required: true, message: '请输入联系电话', trigger: 'blur' },
        { pattern: /^1[3-9]\d{9}$/, message: '请输入正确的手机号', trigger: 'blur' }
      ],
      unitCode: [{ required: true, message: '请输入部门代码', trigger: 'blur' }],
      unitName: [{ required: true, message: '请输入部门名称', trigger: 'blur' }],
      applyType: [{ required: true, message: '请选择申请类型', trigger: 'change' }],
      qualificationLevel: [{ required: true, message: '请选择资格等级', trigger: 'change' }],
      profession: [{ required: true, message: '请选择专业类别', trigger: 'change' }],
      applyDate: [{ required: true, message: '请选择申请日期', trigger: 'change' }]
    }

    /**
     * Handle file upload change
     */
    const handleFileChange = (file, fileList) => {
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

      form.files = fileList.map(f => ({
        name: f.name,
        size: f.size,
        type: f.raw.type,
        file: f.raw
      }))

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

        // Create FormData for file upload
        const formData = new FormData()

        // Add form fields
        Object.keys(form).forEach(key => {
          if (key !== 'files') {
            formData.append(key, form[key])
          }
        })

        // Add files
        form.files.forEach((file, index) => {
          formData.append(`files[${index}]`, file.file)
        })

        const res = await createApply(formData, {
          headers: {
            'Content-Type': 'multipart/form-data'
          }
        })

        if (res.code === 0) {
          ElMessage.success('申请提交成功')
          router.push('/apply/list')
        } else {
          ElMessage.error(res.message || '申请提交失败')
        }
      } catch (error) {
        ElMessage.error('申请提交失败: ' + error.message)
      } finally {
        submitting.value = false
      }
    }

    /**
     * Handle cancel button click
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
.apply-create-container {
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

.apply-form {
  max-width: 900px;
}
</style>
