<template>
  <div class="exam-detail-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>考试详情</h2>
      <div class="breadcrumb">当前位置: 考试管理 &gt; 考试详情</div>
    </div>

    <el-card v-loading="loading" class="detail-card">
      <template v-if="exam">
        <!-- Exam Information -->
        <div class="detail-section">
          <h3>考试信息</h3>
          <el-descriptions :column="2" border>
            <el-descriptions-item label="考试ID">{{ exam.examId }}</el-descriptions-item>
            <el-descriptions-item label="考试名称">{{ exam.examName }}</el-descriptions-item>
            <el-descriptions-item label="考试类型">
              <el-tag type="primary">{{ exam.examType }}</el-tag>
            </el-descriptions-item>
            <el-descriptions-item label="考试状态">
              <el-tag :type="getStatusType(exam.examStatus)" effect="dark">
                {{ exam.examStatus }}
              </el-tag>
            </el-descriptions-item>
            <el-descriptions-item label="考试日期">{{ exam.examDate }}</el-descriptions-item>
            <el-descriptions-item label="考试时间">{{ exam.examTime || '-' }}</el-descriptions-item>
            <el-descriptions-item label="考试地点">{{ exam.examPlace || '-' }}</el-descriptions-item>
            <el-descriptions-item label="考试时长">{{ exam.duration || '-' }} 分钟</el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Participant Information -->
        <div class="detail-section">
          <h3>参与信息</h3>
          <el-descriptions :column="3" border>
            <el-descriptions-item label="最大人数">{{ exam.maxParticipants }}</el-descriptions-item>
            <el-descriptions-item label="报名人数">
              <el-tag type="primary">{{ exam.signupCount || 0 }}</el-tag>
            </el-descriptions-item>
            <el-descriptions-item label="参考人数">
              <el-tag type="success">{{ exam.participantCount || 0 }}</el-tag>
            </el-descriptions-item>
            <el-descriptions-item label="通过人数">
              <el-tag type="success">{{ exam.passCount || 0 }}</el-tag>
            </el-descriptions-item>
            <el-descriptions-item label="及格率">
              <el-tag type="warning">{{ exam.passRate || 0 }}%</el-tag>
            </el-descriptions-item>
            <el-descriptions-item label="平均分">
              <el-tag type="info">{{ exam.averageScore || 0 }}</el-tag>
            </el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Score Requirements -->
        <div class="detail-section">
          <h3>分数要求</h3>
          <el-descriptions :column="3" border>
            <el-descriptions-item label="总分">{{ exam.totalScore }}</el-descriptions-item>
            <el-descriptions-item label="及格分数">{{ exam.passScore }}</el-descriptions-item>
            <el-descriptions-item label="及格率要求">{{ exam.minPassRate || 0 }}%</el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Additional Information -->
        <div class="detail-section">
          <h3>其他信息</h3>
          <el-descriptions :column="1" border>
            <el-descriptions-item label="考试说明">{{ exam.description || '无' }}</el-descriptions-item>
            <el-descriptions-item label="备注">{{ exam.remark || '无' }}</el-descriptions-item>
            <el-descriptions-item label="创建时间">{{ exam.createTime }}</el-descriptions-item>
            <el-descriptions-item label="更新时间">{{ exam.updateTime || '-' }}</el-descriptions-item>
          </el-descriptions>
        </div>

        <!-- Actions -->
        <div class="action-buttons">
          <el-button @click="handleBack">返回</el-button>
          <el-button v-if="exam?.examStatus === '草稿' || exam?.examStatus === '未开始'" type="primary" @click="handleEdit">
            编辑
          </el-button>
          <el-button v-if="exam?.examStatus === '报名中'" type="success" @click="handleSignUp">
            报名
          </el-button>
          <el-button type="info" @click="handleParticipants">
            参与人员
          </el-button>
          <el-button type="warning" @click="handleResults">
            成绩管理
          </el-button>
          <el-button v-if="exam?.signupCount === 0" type="danger" @click="handleDelete">
            删除
          </el-button>
        </div>
      </template>

      <el-empty v-else description="暂无数据" />
    </el-card>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue'
import { useRoute, useRouter } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import { getExamById, deleteExam } from '@/api/exam'

export default {
  name: 'ExamDetail',

  setup() {
    const route = useRoute()
    const router = useRouter()
    const loading = ref(false)
    const exam = ref(null)

    /**
     * Load exam detail
     */
    const loadExamDetail = async () => {
      loading.value = true
      try {
        const examId = route.params.id
        const res = await getExamById(examId)

        if (res.code === 0) {
          exam.value = res.data
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
     * Handle back
     */
    const handleBack = () => {
      router.back()
    }

    /**
     * Handle edit
     */
    const handleEdit = () => {
      ElMessage.info('编辑功能待实现')
      // TODO: Navigate to edit page or open edit dialog
      // router.push(`/exam/${exam.value.examId}/edit`)
    }

    /**
     * Handle sign up
     */
    const handleSignUp = () => {
      router.push({
        path: '/exam/sign-up',
        query: { examId: exam.value.examId }
      })
    }

    /**
     * Handle view participants
     */
    const handleParticipants = () => {
      router.push({
        path: '/exam/participants',
        query: { examId: exam.value.examId }
      })
    }

    /**
     * Handle view results
     */
    const handleResults = () => {
      router.push({
        path: '/exam/results',
        query: { examId: exam.value.examId }
      })
    }

    /**
     * Handle delete
     */
    const handleDelete = () => {
      ElMessageBox.confirm(
        `确定要删除考试 ${exam.value.examName} 吗？此操作不可恢复。`,
        '删除确认',
        {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning',
          dangerouslyUseHTMLString: true
        }
      ).then(async () => {
        try {
          const res = await deleteExam(exam.value.examId)

          if (res.code === 0) {
            ElMessage.success('删除成功')
            router.push('/exam/list')
          } else {
            ElMessage.error(res.message || '删除失败')
          }
        } catch (error) {
          ElMessage.error('删除失败: ' + error.message)
        }
      }).catch(() => {
        ElMessage.info('已取消删除')
      })
    }

    /**
     * Get status type
     */
    const getStatusType = (status) => {
      const statusMap = {
        '未开始': 'info',
        '报名中': 'primary',
        '考试中': 'warning',
        '已结束': 'success'
      }
      return statusMap[status] || 'info'
    }

    onMounted(() => {
      loadExamDetail()
    })

    return {
      loading,
      exam,
      handleBack,
      handleEdit,
      handleSignUp,
      handleParticipants,
      handleResults,
      handleDelete,
      getStatusType
    }
  }
}
</script>

<style scoped>
.exam-detail-container {
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

.detail-section {
  margin-bottom: 30px;
}

.detail-section h3 {
  margin: 0 0 15px 0;
  font-size: 16px;
  font-weight: bold;
  color: #333;
  border-left: 4px solid #409eff;
  padding-left: 10px;
}

.action-buttons {
  margin-top: 30px;
  text-align: center;
}
</style>
