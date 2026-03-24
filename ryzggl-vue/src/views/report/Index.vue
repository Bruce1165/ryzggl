<template>
  <div class="report-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>报表统计</h2>
      <div class="breadcrumb">当前位置: 报表统计</div>
    </div>

    <!-- Report Type Tabs -->
    <el-card>
      <el-tabs v-model="activeTab" @tab-click="handleTabClick">
        <!-- Apply Statistics -->
        <el-tab-pane label="申请统计" name="apply">
          <div class="report-section">
            <!-- Summary Cards -->
            <el-row :gutter="20" class="stats-row">
              <el-col :span="6">
                <div class="stat-card">
                  <div class="stat-value">{{ applyStats.total }}</div>
                  <div class="stat-label">总申请数</div>
                </div>
              </el-col>
              <el-col :span="6">
                <div class="stat-card">
                  <div class="stat-value">{{ applyStats.pending }}</div>
                  <div class="stat-label">待处理</div>
                </div>
              </el-col>
              <el-col :span="6">
                <div class="stat-card success">
                  <div class="stat-value">{{ applyStats.approved }}</div>
                  <div class="stat-label">已通过</div>
                </div>
              </el-col>
              <el-col :span="6">
                <div class="stat-card danger">
                  <div class="stat-value">{{ applyStats.rejected }}</div>
                  <div class="stat-label">已驳回</div>
                </div>
              </el-col>
            </el-row>

            <!-- Chart -->
            <div class="chart-container">
              <el-empty description="图表组件待实现" />
            </div>

            <!-- Filter and Table -->
            <div class="filter-section">
              <el-form :inline="true">
                <el-form-item label="申请类型">
                  <el-select v-model="applyFilter.type" placeholder="选择类型" clearable style="width: 150px">
                    <el-option label="首次申请" value="首次申请" />
                    <el-option label="变更申请" value="变更申请" />
                    <el-option label="延续申请" value="延续申请" />
                  </el-select>
                </el-form-item>
                <el-form-item label="申请日期">
                  <el-date-picker
                    v-model="applyFilter.dateRange"
                    type="daterange"
                    placeholder="选择日期范围"
                    style="width: 300px"
                  />
                </el-form-item>
                <el-form-item>
                  <el-button type="primary" icon="Search">查询</el-button>
                  <el-button type="success" icon="Download">导出</el-button>
                </el-form-item>
              </el-form>
            </div>

            <el-table :data="applyData" stripe border>
              <el-table-column prop="type" label="申请类型" width="120" />
              <el-table-column prop="month" label="月份" width="100" />
              <el-table-column prop="count" label="申请数量" width="100" align="center" />
              <el-table-column prop="approved" label="通过数" width="100" align="center" />
              <el-table-column prop="rejected" label="驳回数" width="100" align="center" />
              <el-table-column prop="approvalRate" label="通过率" width="100" align="center">
                <template #default="{row}">
                  <el-tag :type="row.approvalRate >= 80 ? 'success' : 'warning'">
                    {{ row.approvalRate }}%
                  </el-tag>
                </template>
              </el-table-column>
            </el-table>
          </div>
        </el-tab-pane>

        <!-- Certificate Statistics -->
        <el-tab-pane label="证书统计" name="certificate">
          <div class="report-section">
            <!-- Summary Cards -->
            <el-row :gutter="20" class="stats-row">
              <el-col :span="6">
                <div class="stat-card">
                  <div class="stat-value">{{ certStats.total }}</div>
                  <div class="stat-label">证书总数</div>
                </div>
              </el-col>
              <el-col :span="6">
                <div class="stat-card success">
                  <div class="stat-value">{{ certStats.valid }}</div>
                  <div class="stat-label">有效证书</div>
                </div>
              </el-col>
              <el-col :span="6">
                <div class="stat-card warning">
                  <div class="stat-value">{{ certStats.expiring }}</div>
                  <div class="stat-label">即将过期</div>
                </div>
              </el-col>
              <el-col :span="6">
                <div class="stat-card danger">
                  <div class="stat-value">{{ certStats.expired }}</div>
                  <div class="stat-label">已过期</div>
                </div>
              </el-col>
            </el-row>

            <!-- Chart -->
            <div class="chart-container">
              <el-empty description="图表组件待实现" />
            </div>

            <!-- Filter and Table -->
            <div class="filter-section">
              <el-form :inline="true">
                <el-form-item label="资格等级">
                  <el-select v-model="certFilter.level" placeholder="选择等级" clearable style="width: 150px">
                    <el-option label="一级" value="一级" />
                    <el-option label="二级" value="二级" />
                  </el-select>
                </el-form-item>
                <el-form-item label="专业类别">
                  <el-select v-model="certFilter.profession" placeholder="选择专业" clearable style="width: 150px">
                    <el-option label="土木建筑工程" value="土木建筑工程" />
                    <el-option label="安装工程" value="安装工程" />
                    <el-option label="市政工程" value="市政工程" />
                  </el-select>
                </el-form-item>
                <el-form-item>
                  <el-button type="primary" icon="Search">查询</el-button>
                  <el-button type="success" icon="Download">导出</el-button>
                </el-form-item>
              </el-form>
            </div>

            <el-table :data="certData" stripe border>
              <el-table-column prop="level" label="资格等级" width="100" />
              <el-table-column prop="profession" label="专业类别" width="120" />
              <el-table-column prop="total" label="证书总数" width="100" align="center" />
              <el-table-column prop="valid" label="有效数" width="100" align="center" />
              <el-table-column prop="expiring" label="即将过期" width="100" align="center" />
              <el-table-column prop="expired" label="已过期" width="100" align="center" />
              <el-table-column prop="validRate" label="有效比例" width="100" align="center">
                <template #default="{row}">
                  <el-tag type="success">{{ row.validRate }}%</el-tag>
                </template>
              </el-table-column>
            </el-table>
          </div>
        </el-tab-pane>

        <!-- Exam Statistics -->
        <el-tab-pane label="考试统计" name="exam">
          <div class="report-section">
            <!-- Summary Cards -->
            <el-row :gutter="20" class="stats-row">
              <el-col :span="6">
                <div class="stat-card">
                  <div class="stat-value">{{ examStats.total }}</div>
                  <div class="stat-label">考试总数</div>
                </div>
              </el-col>
              <el-col :span="6">
                <div class="stat-card success">
                  <div class="stat-value">{{ examStats.participants }}</div>
                  <div class="stat-label">参考人数</div>
                </div>
              </el-col>
              <el-col :span="6">
                <div class="stat-card primary">
                  <div class="stat-value">{{ examStats.passed }}</div>
                  <div class="stat-label">及格人数</div>
                </div>
              </el-col>
              <el-col :span="6">
                <div class="stat-card info">
                  <div class="stat-value">{{ examStats.passRate }}%</div>
                  <div class="stat-label">及格率</div>
                </div>
              </el-col>
            </el-row>

            <!-- Chart -->
            <div class="chart-container">
              <el-empty description="图表组件待实现" />
            </div>

            <!-- Filter and Table -->
            <div class="filter-section">
              <el-form :inline="true">
                <el-form-item label="考试类型">
                  <el-select v-model="examFilter.type" placeholder="选择类型" clearable style="width=" 150px">
                    <el-option label="资格认定" value="资格认定" />
                    <el-option label="继续教育" value="继续教育" />
                    <el-option label="考核评定" value="考核评定" />
                  </el-select>
                </el-form-item>
                <el-form-item label="考试日期">
                  <el-date-picker
                    v-model="examFilter.dateRange"
                    type="daterange"
                    placeholder="选择日期范围"
                    style="width: 300px"
                  />
                </el-form-item>
                <el-form-item>
                  <el-button type="primary" icon="Search">查询</el-button>
                  <el-button type="success" icon="Download">导出</el-button>
                </el-form-item>
              </el-form>
            </div>

            <el-table :data="examData" stripe border>
              <el-table-column prop="examName" label="考试名称" width="200" />
              <el-table-column prop="examDate" label="考试日期" width="120" />
              <el-table-column prop="participants" label="参考人数" width="100" align="center" />
              <el-table-column prop="passed" label="及格人数" width="100" align="center" />
              <el-table-column prop="failed" label="不及格" width="100" align="center" />
              <el-table-column prop="absent" label="缺考人数" width="100" align="center" />
              <el-table-column prop="passRate" label="及格率" width="100" align="center">
                <template #default="{row}">
                  <el-tag :type="row.passRate >= 60 ? 'success' : 'danger'">
                    {{ row.passRate }}%
                  </el-tag>
                </template>
              </el-table-column>
              <el-table-column prop="avgScore" label="平均分" width="100" align="center" />
            </el-table>
          </div>
        </el-tab-pane>

        <!-- Worker Statistics -->
        <el-tab-pane label="人员统计" name="worker">
          <div class="report-section">
            <!-- Summary Cards -->
            <el-row :gutter="20" class="stats-row">
              <el-col :span="6">
                <div class="stat-card">
                  <div class="stat-value">{{ workerStats.total }}</div>
                  <div class="stat-label">人员总数</div>
                </div>
              </el-col>
              <el-col :span="6">
                <div class="stat-card success">
                  <div class="stat-value">{{ workerStats.withCert }}</div>
                  <div class="stat-label">有证书</div>
                </div>
              </el-col>
              <el-col :span="6">
                <div class="stat-card warning">
                  <div class="stat-value">{{ workerStats.withoutCert }}</div>
                  <div class="stat-label">无证书</div>
                </div>
              </el-col>
              <el-col :span="6">
                <div class="stat-card info">
                  <div class="stat-value">{{ workerStats.certRate }}%</div>
                  <div class="stat-label">持证率</div>
                </div>
              </el-col>
            </el-row>

            <!-- Chart -->
            <div class="chart-container">
              <el-empty description="图表组件待实现" />
            </div>

            <!-- Filter and Table -->
            <div class="filter-section">
              <el-form :inline="true">
                <el-form-item label="部门">
                  <el-select v-model="workerFilter.deptId" placeholder="选择部门" clearable style="width: 200px">
                    <el-option label="技术部" value="DEPT002" />
                    <el-option label="财务部" value="DEPT003" />
                    <el-option label="人力资源部" value="DEPT004" />
                  </el-select>
                </el-form-item>
                <el-form-item label="学历">
                  <el-select v-model="workerFilter.education" placeholder="选择学历" clearable style="width: 150px">
                    <el-option label="博士" value="博士" />
                    <el-option label="硕士" value="硕士" />
                    <el-option label="本科" value="本科" />
                    <el-option label="大专" value="大专" />
                  </el-select>
                </el-form-item>
                <el-form-item>
                  <el-button type="primary" icon="Search">查询</el-button>
                  <el-button type="success" icon="Download">导出</el-button>
                </el-form-item>
              </el-form>
            </div>

            <el-table :data="workerData" stripe border>
              <el-table-column prop="deptName" label="部门" width="150" />
              <el-table-column prop="education" label="学历" width="100" />
              <el-table-column prop="total" label="总人数" width="100" align="center" />
              <el-table-column prop="withCert" label="有证书" width="100" align="center" />
              <el-table-column prop="withoutCert" label="无证书" width="100" align="center" />
              <el-table-column prop="certRate" label="持证率" width="100" align="center">
                <template #default="{row}">
                  <el-tag type="success">{{ row.certRate }}%</el-tag>
                </template>
              </el-table-column>
            </el-table>
          </div>
        </el-tab-pane>
      </el-tabs>
    </el-card>
  </div>
</template>

<script>
import { ref, reactive } from 'vue'
import { ElMessage } from 'element-plus'

export default {
  name: 'ReportIndex',

  setup() {
    const activeTab = ref('apply')

    // Apply statistics
    const applyStats = reactive({
      total: 1256,
      pending: 45,
      approved: 1189,
      rejected: 22
    })

    const applyFilter = reactive({
      type: '',
      dateRange: []
    })

    const applyData = ref([
      { type: '首次申请', month: '2024-01', count: 120, approved: 115, rejected: 5, approvalRate: 96 },
      { type: '首次申请', month: '2024-02', count: 145, approved: 138, rejected: 7, approvalRate: 95 },
      { type: '首次申请', month: '2024-03', count: 98, approved: 92, rejected: 6, approvalRate: 94 },
      { type: '变更申请', month: '2024-01', count: 45, approved: 42, rejected: 3, approvalRate: 93 },
      { type: '变更申请', month: '2024-02', count: 52, approved: 50, rejected: 2, approvalRate: 96 },
      { type: '延续申请', month: '2024-01', count: 78, approved: 76, rejected: 2, approvalRate: 97 },
      { type: '延续申请', month: '2024-02', count: 85, approved: 82, rejected: 3, approvalRate: 96 }
    ])

    // Certificate statistics
    const certStats = reactive({
      total: 3280,
      valid: 3156,
      expiring: 124,
      expired: 0
    })

    const certFilter = reactive({
      level: '',
      profession: ''
    })

    const certData = ref([
      { level: '一级', profession: '土木建筑工程', total: 560, valid: 545, expiring: 15, expired: 0, validRate: 97 },
      { level: '一级', profession: '安装工程', total: 320, valid: 310, expiring: 10, expired: 0, validRate: 97 },
      { level: '二级', profession: '土木建筑工程', total: 1240, valid: 1200, expiring: 40, expired: 0, validRate: 97 },
      { level: '二级', profession: '安装工程', total: 680, valid: 655, expiring: 25, expired: 0, validRate: 96 },
      { level: '二级', profession: '市政工程', total: 480, valid: 446, expiring: 34, expired: 0, validRate: 93 }
    ])

    // Exam statistics
    const examStats = reactive({
      total: 12,
      participants: 2456,
      passed: 1898,
      passRate: 77
    })

    const examFilter = reactive({
      type: '',
      dateRange: []
    })

    const examData = ref([
      { examName: '2024年二级造价工程师考试', examDate: '2024-03-10', participants: 856, passed: 658, failed: 180, absent: 18, passRate: 77, avgScore: 75.5 },
      { examName: '2024年一级造价工程师考试', examDate: '2024-03-15', participants: 645, passed: 498, failed: 142, absent: 5, passRate: 77, avgScore: 76.2 },
      { examName: '2024年二级建造师考试', examDate: '2024-03-20', participants: 523, passed: 398, failed: 118, absent: 7, passRate: 76, avgScore: 74.8 },
      { examName: '2024年一级建造师考试', examDate: '2024-03-25', participants: 432, passed: 344, failed: 82, absent: 6, passRate: 80, avgScore: 78.1 }
    ])

    // Worker statistics
    const workerStats = reactive({
      total: 456,
      withCert: 328,
      withoutCert: 128,
      certRate: 72
    })

    const workerFilter = reactive({
      deptId: '',
      education: ''
    })

    const workerData = ref([
      { deptName: '技术部', education: '本科', total: 180, withCert: 145, withoutCert: 35, certRate: 81 },
      { deptName: '技术部', education: '大专', total: 95, withCert: 68, withoutCert: 27, certRate: 72 },
      { deptName: '财务部', education: '本科', total: 82, withCert: 65, withoutCert: 17, certRate: 79 },
      { deptName: '人力资源部', education: '本科', total: 45, withCert: 28, withoutCert: 17, certRate: 62 },
      { deptName: '人力资源部', education: '大专', total: 54, withCert: 22, withoutCert: 32, certRate: 41 }
    ])

    /**
     * Handle tab click
     */
    const handleTabClick = (tabName) => {
      console.log('Tab clicked:', tabName)
    }

    return {
      activeTab,
      applyStats,
      applyFilter,
      applyData,
      certStats,
      certFilter,
      certData,
      examStats,
      examFilter,
      examData,
      workerStats,
      workerFilter,
      workerData,
      handleTabClick
    }
  }
}
</script>

<style scoped>
.report-container {
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

.report-section {
  padding: 20px 0;
}

.stats-row {
  margin-bottom: 30px;
}

.stat-card {
  padding: 20px;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  border-radius: 8px;
  text-align: center;
  color: white;
}

.stat-card.success {
  background: linear-gradient(135deg, #67c23a 0%, #85ce61 100%);
}

.stat-card.danger {
  background: linear-gradient(135deg, #f56c6c 0%, #f78989 100%);
}

.stat-card.warning {
  background: linear-gradient(135deg, #e6a23c 0%, #ebb563 100%);
}

.stat-card.info {
  background: linear-gradient(135deg, #909399 0%, #a6a9ad 100%);
}

.stat-card.primary {
  background: linear-gradient(135deg, #409eff 0%, #66b1ff 100%);
}

.stat-value {
  font-size: 32px;
  font-weight: bold;
  margin-bottom: 10px;
}

.stat-label {
  font-size: 14px;
  opacity: 0.9;
}

.chart-container {
  height: 300px;
  margin-bottom: 30px;
  display: flex;
  align-items: center;
  justify-content: center;
  background: #f5f7fa;
  border-radius: 8px;
}

.filter-section {
  margin-bottom: 20px;
  background: #f5f7fa;
  padding: 20px;
  border-radius: 4px;
}
</style>
