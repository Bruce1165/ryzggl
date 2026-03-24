<template>
  <div class="exam-signup-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>考试报名</h2>
      <div class="breadcrumb">当前位置: 考试管理 &gt; 考试报名</div>
    </div>

    <el-row :gutter="20">
      <!-- Exam List -->
      <el-col :span="24">
        <el-card>
          <template #header>
            <div class="card-header">
              <span>可报名考试</span>
              <el-button type="primary" icon="Refresh" @click="loadExamList">刷新</el-button>
            </div>
          </template>

          <el-table
            v-loading="loading"
            :data="examList"
            stripe
            border
          >
            <el-table-column prop="examId" label="考试ID" width="80" />
            <el-table-column prop="examName" label="考试名称" width="200" show-overflow-tooltip />
            <el-table-column prop="examType" label="考试类型" width="100">
              <template #default="{row}">
                <el-tag type="primary" size="small">{{ row.examType }}</el-tag>
              </template>
            </el-table-column>
            <el-table-column prop="examDate" label="考试日期" width="120" />
            <el-table-column prop="examTime" label="考试时间" width="120" />
            <el-table-column prop="examPlace" label="考试地点" width="150" show-overflow-tooltip />
            <el-table-column prop="maxParticipants" label="最大人数" width="90" align="center" />
            <el-table-column prop="signupCount" label="已报名" width="90" align="center">
              <template #default="{row}">
                <el-tag :type="getSignupType(row.signupCount, row.maxParticipants)">
                  {{ row.signupCount }}/{{ row.maxParticipants }}
                </el-tag>
              </template>
            </el-table-column>
            <el-table-column prop="registrationDeadline" label="报名截止" width="120" />
            <el-table-column label="操作" width="120" fixed="right">
              <template #default="{row}">
                <el-button
                  type="primary"
                  size="small"
                  :disabled="!canSignup(row)"
                  @click="handleSignup(row)"
                >
                  {{ canSignup(row) ? '报名' : '已满员' }}
                </el-button>
              </template>
            </el-table-column>
          </el-table>

          <!-- Pagination -->
          <div class="pagination-container">
            <el-pagination
              v-model:current-page="page.current"
              v-model:page-size="page.size"
              :total="page.total"
              :page-sizes="[10, 20, 50]"
              layout="total, sizes, prev, pager, next"
              @size-change="loadExamList"
              @current-change="loadExamList"
            />
          </div>
        </el-card>
      </el-col>
    </el-row>

    <!-- Signup Dialog -->
    <el-dialog v-model="signupDialogVisible" title="考试报名" width="600px">
      <el-form
        ref="signupFormRef"
        :model="signupForm"
        :rules="signupRules"
        label-width="120px"
      >
        <el-form-item label="考试名称">
          <el-input :value="selectedExam?.examName" disabled />
        </el-form-item>
        <el-form-item label="考试日期">
          <el-input :value="selectedExam?.examDate" disabled />
        </el-form-item>
        <el-form-item label="考试地点">
          <el-input :value="selectedExam?.examPlace" disabled />
        </el-form-item>
        <el-form-item label="人员编号" prop="workerId">
          <el-input v-model="signupForm.workerId" placeholder="输入人员编号">
            <template #append>
              <el-button :icon="Search" @click="handleSearchWorker">查询</el-button>
            </template>
          </el-input>
        </el-form-item>
        <el-form-item label="人员姓名">
          <el-input v-model="signupForm.workerName" placeholder="自动填充" disabled />
        </el-form-item>
        <el-form-item label="身份证号">
          <el-input v-model="signupForm.idCard" placeholder="自动填充" disabled />
        </el-form-item>
        <el-form-item label="联系电话">
          <el-input v-model="signupForm.phone" placeholder="自动填充" disabled />
        </el-form-item>
        <el-form-item label="单位名称">
          <el-input v-model="signupForm.unitName" placeholder="自动填充" disabled />
        </el-form-item>
        <el-form-item label="资格等级">
          <el-input v-model="signupForm.qualificationLevel" placeholder="自动填充" disabled />
        </el-form-item>
        <el-form-item label="备注">
          <el-input
            v-model="signupForm.remark"
            type="textarea"
            :rows="3"
            placeholder="输入备注信息"
          />
        </el-form-item>

        <!-- Eligibility Check -->
        <el-form-item v-if="eligibilityResult" label="资格验证">
          <el-alert
            :type="eligibilityResult.eligible ? 'success' : 'error'"
            :title="eligibilityResult.eligible ? '符合报名条件' : '不符合报名条件'"
            :description="eligibilityResult.message"
            :closable="false"
            show-icon
          />
        </el-form-item>
      </el-form>
      <template #footer>
        <el-button @click="signupDialogVisible = false">取消</el-button>
        <el-button
          type="primary"
          :loading="submitting"
          :disabled="!eligibilityResult?.eligible"
          @click="confirmSignup"
        >
          确认报名
        </el-button>
      </template>
    </el-dialog>
  </div>
</template>

<script>
import { ref, reactive, onMounted } from 'vue'
import { useRoute } from 'vue-router'
import { ElMessage } from 'element-plus'
import { Search } from '@element-plus/icons-vue'
import { getExamList, signUpExam } from '@/api/exam'

export default {
  name: 'ExamSignUp',

  components: {
    Search
  },

  setup() {
    const route = useRoute()
    const loading = ref(false)
    const examList = ref([])
    const signupDialogVisible = ref(false)
    const selectedExam = ref(null)
    const submitting = ref(false)
    const signupFormRef = ref(null)
    const eligibilityResult = ref(null)

    const page = reactive({
      current: 1,
      size: 10,
      total: 0
    })

    const signupForm = reactive({
      examId: '',
      workerId: '',
      workerName: '',
      idCard: '',
      phone: '',
      unitName: '',
      qualificationLevel: '',
      remark: ''
    })

    const signupRules = {
      workerId: [{ required: true, message: '请输入人员编号', trigger: 'blur' }]
    }

    onMounted(() => {
      loadExamList()
      // Check if examId is passed in query parameters
      if (route.query.examId) {
        handleExamFromQuery(route.query.examId)
      }
    })

    /**
     * Load exam list
     */
    const loadExamList = async () => {
      loading.value = true
      try {
        const res = await getExamList({
          current: page.current,
          size: page.size,
          examStatus: '报名中'
        })

        if (res.code === 0) {
          examList.value = res.data.records || []
          page.total = res.data.total || 0
        } else {
          ElMessage.error(res.message || '加载数据失败')
        }
      } catch (error) {
        ElMessage.error('加载数据失败: ' + error.message)
      } finally {
        loading.value = false
      }
    }

    /**
     * Handle exam from query parameters
     */
    const handleExamFromQuery = (examId) => {
      const exam = examList.value.find(e => e.examId == examId)
      if (exam) {
        handleSignup(exam)
      } else {
        ElMessage.warning('未找到指定的考试，请选择其他考试')
      }
    }

    /**
     * Search worker information
     */
    const handleSearchWorker = async () => {
      if (!signupForm.workerId) {
        ElMessage.warning('请输入人员编号')
        return
      }

      try {
        // TODO: Implement actual API call to get worker information
        // Mock data for now
        const mockWorker = {
          workerId: signupForm.workerId,
          workerName: '张三',
          idCard: '110101199001011234',
          phone: '13800138000',
          unitName: '北京市某某建筑公司',
          qualificationLevel: '二级造价工程师'
        }

        Object.assign(signupForm, mockWorker)

        // Check eligibility
        checkEligibility(selectedExam.value, mockWorker)
      } catch (error) {
        ElMessage.error('查询人员信息失败: ' + error.message)
      }
    }

    /**
     * Check eligibility for exam registration
     */
    const checkEligibility = (exam, worker) => {
      eligibilityResult.value = {
        eligible: true,
        message: '该人员符合报名条件'
      }

      // TODO: Implement actual eligibility check logic
      // Check if worker has required qualifications
      // Check if worker has already signed up for this exam
      // Check if worker's certificate is valid
      // etc.
    }

    /**
     * Check if can signup
     */
    const canSignup = (exam) => {
      return exam.signupCount < exam.maxParticipants
    }

    /**
     * Get signup type
     */
    const getSignupType = (current, max) => {
      const ratio = current / max
      if (ratio >= 0.9) return 'danger'
      if (ratio >= 0.7) return 'warning'
      return 'success'
    }

    /**
     * Handle signup
     */
    const handleSignup = (exam) => {
      selectedExam.value = exam
      signupForm.examId = exam.examId
      signupForm.workerId = ''
      signupForm.workerName = ''
      signupForm.idCard = ''
      signupForm.phone = ''
      signupForm.unitName = ''
      signupForm.qualificationLevel = ''
      signupForm.remark = ''
      eligibilityResult.value = null
      signupDialogVisible.value = true
    }

    /**
     * Confirm signup
     */
    const confirmSignup = async () => {
      if (!signupFormRef.value) return

      try {
        const valid = await signupFormRef.value.validate()
        if (!valid) return

        if (!eligibilityResult.value?.eligible) {
          ElMessage.error('该人员不符合报名条件，无法报名')
          return
        }

        submitting.value = true

        const res = await signUpExam(signupForm.examId, signupForm.workerId, {
          workerName: signupForm.workerName,
          idCard: signupForm.idCard,
          phone: signupForm.phone,
          unitName: signupForm.unitName,
          qualificationLevel: signupForm.qualificationLevel,
          remark: signupForm.remark
        })

        if (res.code === 0) {
          ElMessage.success('报名成功')
          signupDialogVisible.value = false
          loadExamList()
        } else {
          ElMessage.error(res.message || '报名失败')
        }
      } catch (error) {
        ElMessage.error('报名失败: ' + error.message)
      } finally {
        submitting.value = false
      }
    }

    return {
      loading,
      examList,
      page,
      signupDialogVisible,
      selectedExam,
      signupForm,
      signupRules,
      submitting,
      eligibilityResult,
      loadExamList,
      canSignup,
      getSignupType,
      handleSignup,
      handleSearchWorker,
      confirmSignup
    }
  }
}
</script>

<style scoped>
.exam-signup-container {
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
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.pagination-container {
  margin-top: 20px;
  display: flex;
  justify-content: center;
}
</style>
