<template>
  <div class="worker-create-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>新增人员</h2>
      <div class="breadcrumb">当前位置: 人员管理 &gt; 新增人员</div>
    </div>

    <!-- Worker Form -->
    <el-card class="form-card">
      <template #header>
        <div class="card-header">
          <span>人员信息</span>
        </div>
      </template>

      <el-form
        ref="formRef"
        :model="form"
        :rules="rules"
        label-width="120px"
        class="worker-form"
      >
        <el-tabs v-model="activeTab">
          <!-- Basic Info -->
          <el-tab-pane label="基本信息" name="basic">
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="人员编号" prop="workerId">
                  <el-input v-model="form.workerId" placeholder="输入人员编号" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="姓名" prop="workerName">
                  <el-input v-model="form.workerName" placeholder="输入姓名" />
                </el-form-item>
              </el-col>
            </el-row>

            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="性别" prop="gender">
                  <el-radio-group v-model="form.gender">
                    <el-radio label="1">男</el-radio>
                    <el-radio label="2">女</el-radio>
                  </el-radio-group>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="出生日期" prop="birthday">
                  <el-date-picker
                    v-model="form.birthday"
                    type="date"
                    placeholder="选择出生日期"
                    style="width: 100%"
                    value-format="YYYY-MM-DD"
                  />
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
                <el-form-item label="民族" prop="nation">
                  <el-select v-model="form.nation" placeholder="选择民族" style="width: 100%">
                    <el-option label="汉族" value="汉族" />
                    <el-option label="回族" value="回族" />
                    <el-option label="满族" value="满族" />
                    <el-option label="蒙古族" value="蒙古族" />
                    <el-option label="藏族" value="藏族" />
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>
          </el-tab-pane>

          <!-- Contact Info -->
          <el-tab-pane label="联系方式" name="contact">
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="联系电话" prop="phone">
                  <el-input v-model="form.phone" placeholder="输入联系电话" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="手机号码" prop="mobile">
                  <el-input v-model="form.mobile" placeholder="输入手机号码" />
                </el-form-item>
              </el-col>
            </el-row>

            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="电子邮箱" prop="email">
                  <el-input v-model="form.email" placeholder="输入电子邮箱" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="通讯地址" prop="address">
                  <el-input v-model="form.address" placeholder="输入通讯地址" />
                </el-form-item>
              </el-col>
            </el-row>
          </el-tab-pane>

          <!-- Education Info -->
          <el-tab-pane label="学历信息" name="education">
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="学历" prop="education">
                  <el-select v-model="form.education" placeholder="选择学历" style="width: 100%">
                    <el-option label="博士" value="博士" />
                    <el-option label="硕士" value="硕士" />
                    <el-option label="本科" value="本科" />
                    <el-option label="大专" value="大专" />
                    <el-option label="中专" value="中专" />
                    <el-option label="高中" value="高中" />
                    <el-option label="初中" value="初中" />
                  </el-select>
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="学位" prop="degree">
                  <el-select v-model="form.degree" placeholder="选择学位" style="width: 100%">
                    <el-option label="博士" value="博士" />
                    <el-option label="硕士" value="硕士" />
                    <el-option label="学士" value="学士" />
                    <el-option label="无" value="无" />
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>

            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="毕业院校" prop="school">
                  <el-input v-model="form.school" placeholder="输入毕业院校" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="所学专业" prop="major">
                  <el-input v-model="form.major" placeholder="输入所学专业" />
                </el-form-item>
              </el-col>
            </el-row>

            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="毕业时间" prop="graduationDate">
                  <el-date-picker
                    v-model="form.graduationDate"
                    type="date"
                    placeholder="选择毕业时间"
                    style="width: 100%"
                    value-format="YYYY-MM-DD"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="政治面貌" prop="politicalStatus">
                  <el-select v-model="form.politicalStatus" placeholder="选择政治面貌" style="width: 100%">
                    <el-option label="中共党员" value="中共党员" />
                    <el-option label="共青团员" value="共青团员" />
                    <el-option label="民主党派" value="民主党派" />
                    <el-option label="群众" value="群众" />
                  </el-select>
                </el-form-item>
              </el-col>
            </el-row>
          </el-tab-pane>

          <!-- Work Info -->
          <el-tab-pane label="工作信息" name="work">
            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="单位代码" prop="unitCode">
                  <el-input v-model="form.unitCode" placeholder="输入单位代码" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="单位名称" prop="unitName">
                  <el-input v-model="form.unitName" placeholder="输入单位名称" />
                </el-form-item>
              </el-col>
            </el-row>

            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="部门代码" prop="deptCode">
                  <el-input v-model="form.deptCode" placeholder="输入部门代码" />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="部门名称" prop="deptName">
                  <el-input v-model="form.deptName" placeholder="输入部门名称" />
                </el-form-item>
              </el-col>
            </el-row>

            <el-row :gutter="20">
              <el-col :span="12">
                <el-form-item label="参加工作时间" prop="workStartDate">
                  <el-date-picker
                    v-model="form.workStartDate"
                    type="date"
                    placeholder="选择参加工作时间"
                    style="width: 100%"
                    value-format="YYYY-MM-DD"
                  />
                </el-form-item>
              </el-col>
              <el-col :span="12">
                <el-form-item label="职称" prop="title">
                  <el-input v-model="form.title" placeholder="输入职称" />
                </el-form-item>
              </el-col>
            </el-row>
          </el-tab-pane>
        </el-tabs>

        <el-form-item label="备注">
          <el-input
            v-model="form.remark"
            type="textarea"
            :rows="4"
            placeholder="输入备注信息"
          />
        </el-form-item>

        <!-- Form Actions -->
        <el-form-item>
          <el-button type="primary" :loading="submitting" @click="handleSubmit">
            {{ submitting ? '提交中...' : '提交' }}
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
import { createWorker } from '@/api/worker'

export default {
  name: 'WorkerCreate',

  setup() {
    const router = useRouter()
    const formRef = ref(null)
    const activeTab = ref('basic')
    const submitting = ref(false)

    const form = reactive({
      workerId: '',
      workerName: '',
      gender: '1',
      birthday: '',
      idCard: '',
      nation: '汉族',
      phone: '',
      mobile: '',
      email: '',
      address: '',
      education: '',
      degree: '',
      school: '',
      major: '',
      graduationDate: '',
      politicalStatus: '群众',
      unitCode: '',
      unitName: '',
      deptCode: '',
      deptName: '',
      workStartDate: '',
      title: '',
      remark: ''
    })

    const rules = {
      workerId: [{ required: true, message: '请输入人员编号', trigger: 'blur' }],
      workerName: [{ required: true, message: '请输入姓名', trigger: 'blur' }],
      gender: [{ required: true, message: '请选择性别', trigger: 'change' }],
      idCard: [
        { required: true, message: '请输入身份证号', trigger: 'blur' },
        { pattern: /^[1-9]\d{5}(18|19|20)\d{2}(0[1-9]|1[0-2])(0[1-9]|[12]\d|3[01])\d{3}[\dXx]$/, message: '请输入正确的身份证号', trigger: 'blur' }
      ],
      phone: [
        { required: true, message: '请输入联系电话', trigger: 'blur' }
      ],
      unitCode: [{ required: true, message: '请输入单位代码', trigger: 'blur' }],
      unitName: [{ required: true, message: '请输入单位名称', trigger: 'blur' }]
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

        const res = await createWorker(form)

        if (res.code === 0) {
          ElMessage.success('人员创建成功')
          router.push('/worker/list')
        } else {
          ElMessage.error(res.message || '人员创建失败')
        }
      } catch (error) {
        ElMessage.error('人员创建失败: ' + error.message)
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
      activeTab,
      submitting,
      handleSubmit,
      handleCancel
    }
  }
}
</script>

<style scoped>
.worker-create-container {
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

.worker-form {
  max-width: 900px;
}
</style>
