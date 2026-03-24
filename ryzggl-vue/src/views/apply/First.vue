<template>
  <div class="apply-first-container">
    <!-- App Header -->
    <AppHeader />

    <!-- Page Header -->
    <div class="page-header">
      <h2>首次注册申请</h2>
      <div class="breadcrumb">当前位置: 申请管理 &gt; 首次注册申请</div>
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
        label-width="140px"
        class="apply-form"
      >
        <el-divider content-position="left">人员信息</el-divider>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="电话" prop="psnTelephone">
              <el-input v-model="form.psnTelephone" placeholder="输入电话" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="手机" prop="psnMobilePhone">
              <el-input v-model="form.psnMobilePhone" placeholder="输入手机号" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="邮箱" prop="psnEmail">
              <el-input v-model="form.psnEmail" placeholder="输入邮箱" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="民族" prop="nation">
              <el-select v-model="form.nation" placeholder="选择民族" style="width: 100%">
                <el-option label="汉族" value="汉族" />
                <el-option label="壮族" value="壮族" />
                <el-option label="回族" value="回族" />
                <el-option label="满族" value="满族" />
                <el-option label="维吾尔族" value="维吾尔族" />
                <el-option label="苗族" value="苗族" />
                <el-option label="彝族" value="彝族" />
                <el-option label="土家族" value="土家族" />
                <el-option label="藏族" value="藏族" />
                <el-option label="蒙古族" value="蒙古族" />
                <el-option label="侗族" value="侗族" />
                <el-option label="布依族" value="布依族" />
                <el-option label="瑶族" value="瑶族" />
                <el-option label="白族" value="白族" />
                <el-option label="朝鲜族" value="朝鲜族" />
                <el-option label="哈尼族" value="哈尼族" />
                <el-option label="黎族" value="黎族" />
                <el-option label="哈萨克族" value="哈萨克族" />
                <el-option label="其他" value="其他" />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="出生日期" prop="birthday">
              <el-date-picker
                v-model="form.birthday"
                type="date"
                placeholder="选择出生日期"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="毕业学校" prop="school">
              <el-input v-model="form.school" placeholder="输入毕业学校" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="专业" prop="major">
              <el-input v-model="form.major" placeholder="输入专业" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="毕业时间" prop="graduationTime">
              <el-date-picker
                v-model="form.graduationTime"
                type="date"
                placeholder="选择毕业时间"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="学历" prop="xueLi">
              <el-select v-model="form.xueLi" placeholder="选择学历" style="width: 100%">
                <el-option label="博士" value="博士" />
                <el-option label="硕士" value="硕士" />
                <el-option label="本科" value="本科" />
                <el-option label="专科" value="专科" />
                <el-option label="高中" value="高中" />
                <el-option label="中专" value="中专" />
                <el-option label="其他" value="其他" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="学位" prop="xueWei">
              <el-select v-model="form.xueWei" placeholder="选择学位" style="width: 100%">
                <el-option label="博士" value="博士" />
                <el-option label="硕士" value="硕士" />
                <el-option label="学士" value="学士" />
                <el-option label="无" value="无" />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-divider content-position="left">单位信息</el-divider>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="企业电话" prop="entTelephone">
              <el-input v-model="form.entTelephone" placeholder="输入企业电话" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="企业通讯地址" prop="entCorrespondence">
              <el-input v-model="form.entCorrespondence" placeholder="输入企业通讯地址" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="企业经济性质" prop="entEconomicNature">
              <el-select v-model="form.entEconomicNature" placeholder="选择企业经济性质" style="width: 100%">
                <el-option label="国有企业" value="国有企业" />
                <el-option label="集体企业" value="集体企业" />
                <el-option label="私营企业" value="私营企业" />
                <el-option label="港、澳、台投资企业" value="港、澳、台投资企业" />
                <el-option label="外商投资企业" value="外商投资企业" />
                <el-option label="其他" value="其他" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="企业地址" prop="endAddress">
              <el-input v-model="form.endAddress" placeholder="输入企业地址" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="邮编" prop="entPostcode">
              <el-input v-model="form.entPostcode" placeholder="输入邮编" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="企业类型" prop="entType">
              <el-select v-model="form.entType" placeholder="选择企业类型" style="width: 100%">
                <el-option label="施工企业" value="施工企业" />
                <el-option label="设计单位" value="设计单位" />
                <el-option label="监理单位" value="监理单位" />
                <el-option label="其他" value="其他" />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="企业类别" prop="entSort">
              <el-input v-model="form.entSort" placeholder="输入企业类别" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="企业资质等级" prop="entGrade">
              <el-select v-model="form.entGrade" placeholder="选择企业资质等级" style="width: 100%">
                <el-option label="特级" value="特级" />
                <el-option label="一级" value="一级" />
                <el-option label="二级" value="二级" />
                <el-option label="三级" value="三级" />
              </el-select>
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="企业资质证书号" prop="entQualificationCertificateNo">
              <el-input v-model="form.entQualificationCertificateNo" placeholder="输入企业资质证书号" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="专业" prop="zhuanYe">
              <el-input v-model="form.zhuanYe" placeholder="输入专业" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-divider content-position="left">申请信息</el-divider>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="申请类型" prop="getType">
              <el-select v-model="form.getType" placeholder="选择申请类型" style="width: 100%">
                <el-option label="初始注册" value="初始注册" />
                <el-option label="变更注册" value="变更注册" />
                <el-option label="延续注册" value="延续注册" />
                <el-option label="注销注册" value="注销注册" />
              </el-select>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="考试证书号" prop="psnExamCertCode">
              <el-input v-model="form.psnExamCertCode" placeholder="输入考试证书号" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="颁证日期" prop="conferDate">
              <el-date-picker
                v-model="form.conferDate"
                type="date"
                placeholder="选择颁证日期"
                value-format="YYYY-MM-DD"
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="申请注册专业" prop="applyRegisteProfession">
              <el-input v-model="form.applyRegisteProfession" placeholder="输入申请注册专业" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="必修" prop="biXiu">
              <el-input-number
                v-model="form.biXiu"
                :min="0"
                placeholder="输入必修数量"
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="选修" prop="xuanXiu">
              <el-input-number
                v-model="form.xuanXiu"
                :min="0"
                placeholder="输入选修数量"
                style="width: 100%"
              />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="考试信息" prop="examInfo">
              <el-input
                v-model="form.examInfo"
                type="textarea"
                :rows="3"
                placeholder="输入考试信息"
              />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="其他证书" prop="otherCert">
              <el-input v-model="form.otherCert" placeholder="输入其他证书" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-row :gutter="20">
          <el-col :span="12">
            <el-form-item label="是否同单位" prop="ifSameUnit">
              <el-switch v-model="form.ifSameUnit" />
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="主业" prop="mainJob">
              <el-input v-model="form.mainJob" placeholder="输入主业" />
            </el-form-item>
          </el-col>
        </el-row>

        <el-divider content-position="left">附件</el-divider>

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
import { createApplyFirst } from '@/api/applyFirst'
import AppHeader from '@/components/AppHeader.vue'

export default {
  name: 'ApplyFirst',

  components: {
    AppHeader
  },

  setup() {
    const router = useRouter()
    const formRef = ref(null)
    const submitting = ref(false)

    const form = reactive({
      psnTelephone: '',
      psnMobilePhone: '',
      psnEmail: '',
      nation: '',
      birthday: '',
      school: '',
      major: '',
      graduationTime: '',
      xueLi: '',
      xueWei: '',
      entTelephone: '',
      entCorrespondence: '',
      entEconomicNature: '',
      endAddress: '',
      entPostcode: '',
      entType: '',
      entSort: '',
      entGrade: '',
      entQualificationCertificateNo: '',
      zhuanYe: '',
      getType: '',
      psnExamCertCode: '',
      conferDate: '',
      applyRegisteProfession: '',
      biXiu: 0,
      xuanXiu: 0,
      examInfo: '',
      otherCert: '',
      ifSameUnit: false,
      mainJob: '',
      files: []
    })

    const rules = {
      psnTelephone: [{ required: true, message: '请输入电话', trigger: 'blur' }],
      psnMobilePhone: [
        { required: true, message: '请输入手机号', trigger: 'blur' },
        { pattern: /^1[3-9]\d{9}$/, message: '请输入正确的手机号', trigger: 'blur' }
      ],
      psnEmail: [
        { required: true, message: '请输入邮箱', trigger: 'blur' },
        { type: 'email', message: '请输入正确的邮箱', trigger: 'blur' }
      ],
      nation: [{ required: true, message: '请选择民族', trigger: 'change' }],
      birthday: [{ required: true, message: '请选择出生日期', trigger: 'change' }],
      school: [{ required: true, message: '请输入毕业学校', trigger: 'blur' }],
      major: [{ required: true, message: '请输入专业', trigger: 'blur' }],
      graduationTime: [{ required: true, message: '请选择毕业时间', trigger: 'change' }],
      xueLi: [{ required: true, message: '请选择学历', trigger: 'change' }],
      entTelephone: [{ required: true, message: '请输入企业电话', trigger: 'blur' }],
      getType: [{ required: true, message: '请选择申请类型', trigger: 'change' }]
    }

    /**
     * Handle file upload change
     */
    const handleFileChange = (file, fileList) => {
      if (file.size > 10 * 1024 * 1024) {
        ElMessage.error('文件大小不能超过10MB')
        return false
      }

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

        const formData = new FormData()
        Object.keys(form).forEach(key => {
          if (key !== 'files') {
            formData.append(key, form[key])
          }
        })
        form.files.forEach((file, index) => {
          formData.append(`files[${index}]`, file.file)
        })

        const res = await createApplyFirst(formData, {
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
.apply-first-container {
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
  max-width: 1000px;
}
</style>
