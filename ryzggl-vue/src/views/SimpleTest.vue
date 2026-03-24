<template>
  <div style="padding: 20px; background: #f0f0f0; min-height: 100vh;">
    <h1 style="color: blue;">简单测试页面</h1>
    <p>如果你能看到这个蓝色标题，说明Vue正常工作！</p>
    <p>当前时间: {{ currentTime }}</p>
    <div style="margin-top: 20px; padding: 15px; background: white; border: 2px solid green;">
      <p><strong>测试结果:</strong></p>
      <p>✅ Vue组件加载成功</p>
      <p>✅ 样式渲染正常</p>
      <p>✅ 响应式数据工作: {{ currentTime }}</p>
    </div>
  </div>
</template>

<script>
export default {
  name: 'SimpleTest',
  data() {
    return {
      currentTime: new Date().toLocaleString('zh-CN')
    }
  },
  mounted() {
    console.log('简单测试组件已挂载！')
    setInterval(() => {
      this.currentTime = new Date().toLocaleString('zh-CN')
    }, 1000)
  }
}
</script>