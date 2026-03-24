<template>
  <div class="test-dashboard">
    <h1>测试仪表板</h1>
    <p>如果你能看到这个页面，说明Vue路由是工作的！</p>
    <div class="test-stats">
      <div class="stat-card">
        <h2>总体进度: {{ overallProgress }}%</h2>
      </div>
      <div class="stat-card">
        <h2>实体迁移: 10/150</h2>
      </div>
      <div class="stat-card">
        <h2>服务实现: 8/50</h2>
      </div>
    </div>
    <div class="test-message">
      <p>🎉 仪表板基础加载成功！</p>
      <p>现在让我们尝试加载完整的组件...</p>
    </div>
  </div>
</template>

<script>
import { ref, computed, onMounted } from 'vue'

export default {
  name: 'MigrationDashboardTest',

  setup() {
    const overallProgress = ref(14)

    onMounted(() => {
      console.log('测试仪表板已挂载!')
      console.log('总体进度:', overallProgress.value)
    })

    return {
      overallProgress
    }
  }
}
</script>

<style scoped>
.test-dashboard {
  padding: 24px;
  background: #f5f7fa;
  min-height: 100vh;
}

.test-dashboard h1 {
  color: #409eff;
  font-size: 32px;
  margin-bottom: 16px;
}

.test-dashboard p {
  font-size: 16px;
  color: #303133;
}

.test-stats {
  display: grid;
  grid-template-columns: repeat(3, 1fr);
  gap: 20px;
  margin: 24px 0;
}

.stat-card {
  background: white;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.stat-card h2 {
  color: #67c23a;
  font-size: 24px;
  margin: 0;
}

.test-message {
  background: #fff7e6;
  padding: 20px;
  border-radius: 8px;
  border-left: 4px solid #faad14;
}

.test-message p {
  margin: 8px 0;
}
</style>