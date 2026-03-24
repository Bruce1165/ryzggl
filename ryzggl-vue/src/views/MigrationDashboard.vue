<template>
  <div class="migration-dashboard">
    <!-- Main Container -->
    <div class="dashboard-container">
      <!-- Header Section -->
      <div class="dashboard-header">
        <div class="header-content">
          <div class="header-text">
            <h1 class="main-title">RYZGGL 迁移追踪仪表板</h1>
            <p class="subtitle">人员资格管理系统 - .NET 到 Spring Boot + Vue.js</p>
          </div>
          <div class="header-stats">
            <div class="header-stat">
              <div class="stat-label">总体进度</div>
              <div class="stat-value">{{ overallProgress }}%</div>
            </div>
            <div class="header-stat">
              <div class="stat-label">更新时间</div>
              <div class="stat-time">{{ lastUpdated }}</div>
            </div>
          </div>
        </div>
      </div>

      <!-- Overall Progress Section -->
      <div class="progress-section">
        <div class="progress-header">
          <h2 class="section-title">📊 迁移进度概览</h2>
        </div>
        <div class="progress-bar-wrapper">
          <div class="progress-bar-bg">
            <div class="progress-bar-fill" :style="{ width: overallProgress + '%' }"></div>
          </div>
          <div class="progress-percentage">{{ overallProgress }}%</div>
        </div>
        <div class="progress-phase">
          <div class="phase-info">
            <span class="phase-label">当前阶段:</span>
            <span class="phase-name">Phase 2 - 核心业务逻辑</span>
            <span class="phase-status">进行中</span>
          </div>
        </div>
      </div>

      <!-- Metrics Grid -->
      <div class="metrics-section">
        <h2 class="section-title">📈 关键指标</h2>
        <div class="metrics-grid">
          <!-- Entity Migration -->
          <div class="metric-card">
            <div class="metric-icon" style="background: #e3f2fd;">
              <span class="icon">🗄️</span>
            </div>
            <div class="metric-content">
              <div class="metric-title">实体迁移</div>
              <div class="metric-number">{{ entityStats.completed }}<span class="metric-total">/{{ entityStats.total }}</span></div>
              <div class="metric-percent" :style="{ color: getPercentColor((entityStats.completed / entityStats.total) * 100) }">
                {{ Math.round((entityStats.completed / entityStats.total) * 100) }}%
              </div>
            </div>
          </div>

          <!-- Services -->
          <div class="metric-card">
            <div class="metric-icon" style="background: #f3e5f5;">
              <span class="icon">⚙️</span>
            </div>
            <div class="metric-content">
              <div class="metric-title">业务服务</div>
              <div class="metric-number">{{ serviceStats.completed }}<span class="metric-total">/{{ serviceStats.total }}</span></div>
              <div class="metric-percent" :style="{ color: getPercentColor((serviceStats.completed / serviceStats.total) * 100) }">
                {{ Math.round((serviceStats.completed / serviceStats.total) * 100) }}%
              </div>
            </div>
          </div>

          <!-- Controllers -->
          <div class="metric-card">
            <div class="metric-icon" style="background: #e8f5e8;">
              <span class="icon">🔌</span>
            </div>
            <div class="metric-content">
              <div class="metric-title">API控制器</div>
              <div class="metric-number">{{ controllerStats.completed }}<span class="metric-total">/{{ controllerStats.total }}</span></div>
              <div class="metric-percent" :style="{ color: getPercentColor((controllerStats.completed / controllerStats.total) * 100) }">
                {{ Math.round((controllerStats.completed / controllerStats.total) * 100) }}%
              </div>
            </div>
          </div>

          <!-- Frontend Pages -->
          <div class="metric-card">
            <div class="metric-icon" style="background: #fff3e0;">
              <span class="icon">🖥️</span>
            </div>
            <div class="metric-content">
              <div class="metric-title">前端页面</div>
              <div class="metric-number">{{ frontendStats.pages.completed }}<span class="metric-total">/{{ frontendStats.pages.total }}</span></div>
              <div class="metric-percent" :style="{ color: getPercentColor((frontendStats.pages.completed / frontendStats.pages.total) * 100) }">
                {{ Math.round((frontendStats.pages.completed / frontendStats.pages.total) * 100) }}%
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Issues Section -->
      <div class="issues-section">
        <h2 class="section-title">⚠️ 已知问题 & 阻塞项</h2>
        <div class="issues-grid">
          <div v-for="(issue, index) in issues" :key="index" :class="['issue-card', { 'issue-blocker': issue.blocker }]">
            <div class="issue-header">
              <div class="issue-priority" :class="issue.priority">
                <span v-if="issue.priority === 'high'">🔴 高优先级</span>
                <span v-else-if="issue.priority === 'medium'">🟡 中优先级</span>
                <span v-else>🟢 低优先级</span>
              </div>
              <div v-if="issue.blocker" class="issue-blocker-badge">阻塞项</div>
            </div>
            <h4 class="issue-title">{{ issue.title }}</h4>
            <p class="issue-description">{{ issue.description }}</p>
            <div class="issue-footer">
              <div class="issue-due-date">📅 截止: {{ issue.dueDate }}</div>
              <div class="issue-status" :class="issue.status">
                {{ issue.status === 'pending' ? '待处理' : issue.status === 'in-progress' ? '处理中' : '已完成' }}
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- Recent Activities -->
      <div class="activities-section">
        <h2 class="section-title">📋 近期活动</h2>
        <div class="activities-list">
          <div v-for="(activity, index) in recentActivities" :key="index" class="activity-item">
            <div class="activity-time">{{ activity.time }}</div>
            <div class="activity-content">
              <div class="activity-title">{{ activity.title }}</div>
              <div class="activity-detail">{{ activity.detail }}</div>
            </div>
          </div>
        </div>
      </div>

      <!-- Footer -->
      <div class="dashboard-footer">
        <div class="footer-content">
          <div class="footer-links">
            <a href="/simple-test">简单测试</a>
            <a href="/debug.html">调试页面</a>
          </div>
          <div class="footer-info">
            <span>RYZGGL 迁移追踪系统 v1.0</span>
            <span>© 2024</span>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'MigrationDashboard',

  data() {
    return {
      lastUpdated: '2026-03-16 10:10',
      entityStats: {
        total: 150,
        completed: 14
      },
      serviceStats: {
        total: 50,
        completed: 12
      },
      controllerStats: {
        total: 40,
        completed: 10
      },
      frontendStats: {
        pages: {
          total: 35,
          completed: 15
        }
      },
      issues: [
        {
          title: '新实体业务逻辑实现',
          description: 'ApplyContinue、ApplyRenew、CertificateChange、CertificateContinue等4个新实体已创建，需要实现Repository、Service和Controller业务逻辑',
          priority: 'high',
          severity: 'medium',
          blocker: false,
          dueDate: '2024-03-20',
          status: 'completed'
        },
        {
          title: '证书有效期计算逻辑迁移',
          description: '数据库函数GET_CertificateContinueValidEndDate需要完全迁移到Java服务层，涉及年龄、地区、职级等复杂业务规则',
          priority: 'high',
          severity: 'high',
          blocker: true,
          dueDate: '2024-03-25',
          status: 'pending'
        },
        {
          title: '多步骤审批工作流',
          description: 'ApplyCheckTask表的多步骤审批逻辑需要重新设计和实现',
          priority: 'high',
          severity: 'high',
          blocker: true,
          dueDate: '2024-04-05',
          status: 'pending'
        },
        {
          title: '身份证校验功能实现',
          description: '数据库函数CheckIDCard需要迁移到Java工具类，支持15位和18位身份证校验',
          priority: 'medium',
          severity: 'medium',
          blocker: false,
          dueDate: '2024-03-30',
          status: 'pending'
        }
      ],
      recentActivities: [
        {
          time: '2026-03-16 10:10',
          title: '新实体业务逻辑完成',
          detail: '完成4个新实体（ApplyContinue、ApplyRenew、CertificateChange、CertificateContinue）的Repository、Service和Controller实现'
        },
        {
          time: '2026-03-15 18:00',
          title: '实体迁移进度更新',
          detail: '完成4个进行中的实体类创建（ApplyContinue、ApplyRenew、CertificateChange、CertificateContinue）'
        },
        {
          time: '2024-03-15 17:45',
          title: '仪表板数据更新',
          detail: '实体迁移进度更新至14/150 (9%)，总体进度提升至15%'
        },
        {
          time: '2024-03-15 17:30',
          title: '仪表板UI优化完成',
          detail: '重新设计仪表板布局和视觉样式，提升用户体验'
        },
        {
          time: '2024-03-15 16:45',
          title: '后端API修复完成',
          detail: 'MyBatis-Plus版本兼容性问题已解决'
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

  methods: {
    getPercentColor(percentage) {
      if (percentage >= 80) return '#67c23a'
      if (percentage >= 50) return '#409eff'
      if (percentage >= 25) return '#e6a23c'
      return '#f56c6c'
    }
  }
}
</script>

<style scoped>
.migration-dashboard {
  min-height: 100vh;
  background: #f5f7fa;
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', 'PingFang SC', 'Hiragino Sans GB', 'Microsoft YaHei', sans-serif;
}

.dashboard-container {
  max-width: 1400px;
  margin: 0 auto;
  padding: 24px;
}

/* Header Section */
.dashboard-header {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  padding: 32px 40px;
  border-radius: 16px;
  margin-bottom: 32px;
  box-shadow: 0 10px 30px rgba(102, 126, 234, 0.3);
  color: white;
}

.header-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
  gap: 24px;
}

.header-text h1 {
  margin: 0 0 8px 0;
  font-size: 28px;
  font-weight: 700;
}

.subtitle {
  margin: 0;
  font-size: 16px;
  opacity: 0.9;
}

.header-stats {
  display: flex;
  gap: 32px;
}

.header-stat {
  text-align: center;
}

.stat-label {
  font-size: 12px;
  text-transform: uppercase;
  letter-spacing: 1px;
  margin-bottom: 4px;
  opacity: 0.8;
}

.stat-value {
  font-size: 32px;
  font-weight: 700;
}

.stat-time {
  font-size: 14px;
  font-weight: 500;
}

/* Progress Section */
.progress-section {
  background: white;
  padding: 32px;
  border-radius: 16px;
  margin-bottom: 32px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
}

.progress-header {
  margin-bottom: 24px;
}

.section-title {
  margin: 0;
  font-size: 20px;
  font-weight: 600;
  color: #303133;
}

.progress-bar-wrapper {
  position: relative;
  height: 48px;
  background: #f5f7fa;
  border-radius: 24px;
  overflow: hidden;
  margin-bottom: 20px;
}

.progress-bar-bg {
  width: 100%;
  height: 100%;
  background: #f5f7fa;
}

.progress-bar-fill {
  height: 100%;
  background: linear-gradient(90deg, #667eea 0%, #764ba2 100%);
  transition: width 0.8s ease-out;
}

.progress-percentage {
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  font-size: 20px;
  font-weight: 700;
  color: #303133;
  text-shadow: 0 1px 3px rgba(255, 255, 255, 0.8);
}

.progress-phase {
  display: flex;
  justify-content: center;
}

.phase-info {
  display: flex;
  gap: 16px;
  font-size: 16px;
  align-items: center;
}

.phase-label {
  color: #909399;
  font-weight: 500;
}

.phase-name {
  font-weight: 600;
  color: #303133;
}

.phase-status {
  padding: 4px 16px;
  background: #67c23a;
  color: white;
  border-radius: 12px;
  font-size: 14px;
  font-weight: 500;
}

/* Metrics Section */
.metrics-section {
  margin-bottom: 32px;
}

.metrics-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
  gap: 24px;
}

.metric-card {
  background: white;
  border-radius: 16px;
  padding: 28px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
  transition: transform 0.3s ease, box-shadow 0.3s ease;
  display: flex;
  gap: 20px;
  align-items: center;
}

.metric-card:hover {
  transform: translateY(-8px);
  box-shadow: 0 12px 24px rgba(0, 0, 0, 0.1);
}

.metric-icon {
  width: 80px;
  height: 80px;
  border-radius: 16px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 36px;
  flex-shrink: 0;
}

.metric-content {
  flex: 1;
}

.metric-title {
  font-size: 16px;
  font-weight: 600;
  color: #606266;
  margin-bottom: 12px;
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

.metric-number {
  font-size: 32px;
  font-weight: 700;
  color: #303133;
  margin-bottom: 8px;
}

.metric-total {
  font-size: 20px;
  font-weight: 400;
  color: #909399;
}

.metric-percent {
  font-size: 18px;
  font-weight: 600;
}

/* Issues Section */
.issues-section {
  margin-bottom: 32px;
}

.issues-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(400px, 1fr));
  gap: 20px;
}

.issue-card {
  background: white;
  border-radius: 12px;
  padding: 24px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
  border-left: 4px solid #e6a23c;
  transition: transform 0.3s ease;
}

.issue-card:hover {
  transform: translateX(8px);
}

.issue-card.issue-blocker {
  border-left-color: #f56c6c;
  background: linear-gradient(135deg, #fff 0%, #fef0f0 100%);
}

.issue-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 12px;
  flex-wrap: wrap;
  gap: 12px;
}

.issue-priority {
  padding: 6px 16px;
  border-radius: 8px;
  font-size: 12px;
  font-weight: 600;
  text-transform: uppercase;
}

.issue-priority.high {
  background: #fee2e2;
  color: #f56c6c;
}

.issue-priority.medium {
  background: #fef9e7;
  color: #e6a23c;
}

.issue-priority.low {
  background: #f0f9ff;
  color: #409eff;
}

.issue-blocker-badge {
  padding: 6px 16px;
  background: #f56c6c;
  color: white;
  border-radius: 8px;
  font-size: 12px;
  font-weight: 600;
  text-transform: uppercase;
}

.issue-title {
  font-size: 16px;
  font-weight: 600;
  color: #303133;
  margin: 0 0 12px 0;
}

.issue-description {
  color: #606266;
  font-size: 14px;
  line-height: 1.6;
  margin: 0 0 16px 0;
}

.issue-footer {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding-top: 12px;
  border-top: 1px solid #ebeef5;
}

.issue-due-date {
  font-size: 13px;
  color: #909399;
}

.issue-status {
  padding: 6px 16px;
  border-radius: 8px;
  font-size: 12px;
  font-weight: 500;
}

.issue-status.pending {
  background: #f0f9ff;
  color: #409eff;
}

.issue-status.in-progress {
  background: #f0f9ff;
  color: #409eff;
}

.issue-status.completed {
  background: #f0f9ff;
  color: #67c23a;
}

/* Activities Section */
.activities-section {
  margin-bottom: 32px;
}

.activities-list {
  background: white;
  border-radius: 16px;
  padding: 24px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
}

.activity-item {
  display: flex;
  gap: 16px;
  padding: 16px 0;
  border-bottom: 1px solid #f5f7fa;
}

.activity-item:last-child {
  border-bottom: none;
}

.activity-time {
  min-width: 140px;
  font-size: 13px;
  color: #909399;
  font-weight: 500;
}

.activity-content {
  flex: 1;
}

.activity-title {
  font-size: 15px;
  font-weight: 600;
  color: #303133;
  margin-bottom: 4px;
}

.activity-detail {
  font-size: 14px;
  color: #606266;
  line-height: 1.5;
}

/* Footer */
.dashboard-footer {
  background: white;
  padding: 24px 32px;
  border-radius: 16px;
  box-shadow: 0 4px 6px rgba(0, 0, 0, 0.05);
}

.footer-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
  flex-wrap: wrap;
  gap: 16px;
}

.footer-links {
  display: flex;
  gap: 24px;
}

.footer-links a {
  color: #409eff;
  text-decoration: none;
  font-weight: 500;
  transition: color 0.2s;
}

.footer-links a:hover {
  color: #66b1ff;
}

.footer-info {
  display: flex;
  gap: 24px;
  font-size: 14px;
  color: #909399;
}

/* Responsive Design */
@media (max-width: 1200px) {
  .metrics-grid {
    grid-template-columns: repeat(2, 1fr);
  }
}

@media (max-width: 768px) {
  .dashboard-container {
    padding: 16px;
  }

  .header-content {
    flex-direction: column;
    align-items: flex-start;
  }

  .header-stats {
    width: 100%;
    justify-content: space-around;
  }

  .metrics-grid {
    grid-template-columns: 1fr;
  }

  .issues-grid {
    grid-template-columns: 1fr;
  }

  .metric-card {
    flex-direction: column;
    text-align: center;
  }

  .metric-icon {
    width: 60px;
    height: 60px;
    font-size: 28px;
  }

  .footer-content {
    flex-direction: column;
    align-items: center;
    text-align: center;
  }
}
</style>