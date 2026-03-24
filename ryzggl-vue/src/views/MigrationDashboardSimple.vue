<template>
  <div class="migration-dashboard">
    <!-- Header -->
    <div class="dashboard-header">
      <h1>RYZGGL Migration Dashboard</h1>
      <p class="subtitle">人员资格管理系统 - .NET to Spring Boot + Vue.js Migration Tracker</p>
      <div class="last-updated">最后更新: {{ lastUpdated }}</div>
    </div>

    <!-- Overall Progress -->
    <div class="overall-progress-card">
      <h2>总体进度</h2>
      <div class="progress-bar-container">
        <div class="progress-bar" :style="{ width: overallProgress + '%' }"></div>
      </div>
      <div class="progress-text">{{ overallProgress }}%</div>
    </div>

    <!-- Metrics Row -->
    <div class="metrics-row">
      <div class="metric-card">
        <h3>实体迁移</h3>
        <div class="metric-value">{{ entityStats.completed }}/{{ entityStats.total }}</div>
        <div class="metric-percentage">{{ Math.round((entityStats.completed / entityStats.total) * 100) }}%</div>
      </div>
      <div class="metric-card">
        <h3>业务服务</h3>
        <div class="metric-value">{{ serviceStats.completed }}/{{ serviceStats.total }}</div>
        <div class="metric-percentage">{{ Math.round((serviceStats.completed / serviceStats.total) * 100) }}%</div>
      </div>
      <div class="metric-card">
        <h3>API控制器</h3>
        <div class="metric-value">{{ controllerStats.completed }}/{{ controllerStats.total }}</div>
        <div class="metric-percentage">{{ Math.round((controllerStats.completed / controllerStats.total) * 100) }}%</div>
      </div>
      <div class="metric-card">
        <h3>前端页面</h3>
        <div class="metric-value">{{ frontendStats.pages.completed }}/{{ frontendStats.pages.total }}</div>
        <div class="metric-percentage">{{ Math.round((frontendStats.pages.completed / frontendStats.pages.total) * 100) }}%</div>
      </div>
    </div>

    <!-- Issues -->
    <div class="issues-card">
      <h2>已知问题 & 阻塞项</h2>
      <div class="issues-list">
        <div v-for="(issue, index) in issues" :key="index" :class="['issue-item', { 'issue-blocker': issue.blocker }]">
          <h4>{{ issue.title }}</h4>
          <p>{{ issue.description }}</p>
          <div class="issue-meta">
            <span class="issue-severity">严重性: {{ issue.severity }}</span>
            <span v-if="issue.blocker" class="issue-blocker-tag">🔴 阻塞项</span>
            <span v-else class="issue-normal">⚪ 普通问题</span>
            <span>截止: {{ issue.dueDate }}</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'MigrationDashboardSimple',

  data() {
    return {
      lastUpdated: '2026-03-15 17:30',
      entityStats: {
        total: 150,
        completed: 10
      },
      serviceStats: {
        total: 50,
        completed: 8
      },
      controllerStats: {
        total: 40,
        completed: 6
      },
      frontendStats: {
        pages: {
          total: 35,
          completed: 15
        }
      },
      issues: [
        {
          title: '证书有效期计算逻辑迁移',
          description: '数据库函数GET_CertificateContinueValidEndDate需要完全迁移到Java服务层',
          severity: 'high',
          blocker: true,
          dueDate: '2024-03-20'
        },
        {
          title: '身份证校验功能实现',
          description: '数据库函数CheckIDCard需要迁移到Java工具类',
          severity: 'medium',
          blocker: false,
          dueDate: '2024-03-25'
        }
      ]
    }
  },

  computed: {
    overallProgress() {
      const total = this.entityStats.total + this.serviceStats.total + this.controllerStats.total + this.frontendStats.pages.total
      const completed = this.entityStats.completed + this.serviceStats.completed + this.controllerStats.completed + this.frontendStats.pages.completed
      return Math.round((completed / total) * 100)
    }
  },

  mounted() {
    console.log('简化版仪表板已挂载!')
    console.log('总体进度:', this.overallProgress + '%')
    console.log('问题数量:', this.issues.length)
  }
}
</script>

<style scoped>
.migration-dashboard {
  padding: 24px;
  background: #f5f7fa;
  min-height: 100vh;
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;
}

.dashboard-header {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  padding: 32px 24px;
  border-radius: 12px;
  margin-bottom: 24px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
}

.dashboard-header h1 {
  margin: 0 0 8px 0;
  font-size: 32px;
  font-weight: bold;
}

.subtitle {
  margin: 0 0 16px 0;
  font-size: 16px;
  opacity: 0.9;
}

.last-updated {
  font-size: 14px;
  opacity: 0.8;
}

.overall-progress-card {
  background: white;
  padding: 24px;
  border-radius: 12px;
  margin-bottom: 24px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.overall-progress-card h2 {
  margin: 0 0 16px 0;
  font-size: 20px;
  color: #303133;
}

.progress-bar-container {
  width: 100%;
  height: 30px;
  background: #f0f0f0;
  border-radius: 15px;
  overflow: hidden;
  position: relative;
}

.progress-bar {
  height: 100%;
  background: linear-gradient(90deg, #67c23a 0%, #85ce61 100%);
  transition: width 0.5s ease-out;
}

.progress-text {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  font-size: 16px;
  font-weight: bold;
  color: #303133;
  text-shadow: 0 1px 2px rgba(255,255,255,0.8);
}

.metrics-row {
  display: grid;
  grid-template-columns: repeat(4, 1fr);
  gap: 20px;
  margin-bottom: 24px;
}

.metric-card {
  background: white;
  padding: 20px;
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  transition: transform 0.2s;
}

.metric-card:hover {
  transform: translateY(-4px);
}

.metric-card h3 {
  margin: 0 0 12px 0;
  font-size: 14px;
  color: #666;
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

.metric-value {
  font-size: 36px;
  font-weight: bold;
  color: #409eff;
  margin-bottom: 8px;
}

.metric-percentage {
  font-size: 14px;
  color: #909399;
}

.issues-card {
  background: white;
  padding: 24px;
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.issues-card h2 {
  margin: 0 0 20px 0;
  font-size: 20px;
  color: #303133;
}

.issues-list {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.issue-item {
  padding: 16px;
  border-left: 4px solid #e6a23c;
  background: #fff7e6;
  border-radius: 4px;
}

.issue-item.issue-blocker {
  border-left-color: #f56c6c;
  background: #fef0f0;
}

.issue-item h4 {
  margin: 0 0 8px 0;
  font-size: 16px;
  color: #303133;
}

.issue-item p {
  margin: 0 0 12px 0;
  font-size: 14px;
  color: #606266;
  line-height: 1.6;
}

.issue-meta {
  display: flex;
  gap: 16px;
  font-size: 12px;
  flex-wrap: wrap;
}

.issue-severity {
  color: #e6a23c;
  font-weight: 500;
}

.issue-blocker-tag {
  color: #f56c6c;
  font-weight: bold;
}

.issue-normal {
  color: #909399;
}
</style>