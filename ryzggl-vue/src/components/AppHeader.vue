<template>
  <div class="app-header">
    <div class="header-left">
      <div class="logo">RYZGGL</div>
      <div class="title">人员资格管理系统</div>
    </div>

    <div class="header-right">
      <el-dropdown @command="handleCommand" trigger="click">
        <div class="user-info">
          <el-icon class="user-icon"><User /></el-icon>
          <span class="username">{{ userInfo?.realName || username }}</span>
          <el-icon class="dropdown-icon"><ArrowDown /></el-icon>
        </div>
        <template #dropdown>
          <el-dropdown-menu>
            <el-dropdown-item command="profile">
              <el-icon><User /></el-icon>
              个人信息
            </el-dropdown-item>
            <el-dropdown-item command="settings">
              <el-icon><Setting /></el-icon>
              系统设置
            </el-dropdown-item>
            <el-dropdown-item divided command="logout">
              <el-icon><SwitchButton /></el-icon>
              退出登录
            </el-dropdown-item>
          </el-dropdown-menu>
        </template>
      </el-dropdown>
    </div>
  </div>
</template>

<script>
import { ref, onMounted } from 'vue'
import { useRouter } from 'vue-router'
import { ElMessage, ElMessageBox } from 'element-plus'
import { User, ArrowDown, Setting, SwitchButton } from '@element-plus/icons-vue'
import { logout as logoutApi } from '@/api/auth'
import { getUserInfo, getUsername, clearAuthData } from '@/utils/auth'

export default {
  name: 'AppHeader',

  components: {
    User,
    ArrowDown,
    Setting,
    SwitchButton
  },

  setup() {
    const router = useRouter()
    const userInfo = ref(null)
    const username = ref('')

    // Load user info on mount
    onMounted(() => {
      userInfo.value = getUserInfo()
      username.value = getUsername() || '未知用户'
    })

    /**
     * Handle dropdown menu commands
     */
    const handleCommand = async (command) => {
      switch (command) {
        case 'profile':
          router.push('/profile')
          break
        case 'settings':
          router.push('/settings')
          break
        case 'logout':
          await handleLogout()
          break
      }
    }

    /**
     * Handle logout
     */
    const handleLogout = async () => {
      try {
        await ElMessageBox.confirm(
          '确定要退出登录吗?',
          '提示',
          {
            confirmButtonText: '确定',
            cancelButtonText: '取消',
            type: 'warning'
          }
        )

        // Call logout API
        await logoutApi()

        // Clear auth data
        clearAuthData()

        ElMessage.success('已退出登录')

        // Redirect to login
        router.push('/login')
      } catch (error) {
        if (error !== 'cancel') {
          console.error('Logout error:', error)
          ElMessage.error('退出登录失败')
        }
      }
    }

    return {
      userInfo,
      username,
      handleCommand
    }
  }
}
</script>

<style scoped>
.app-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 20px;
  height: 60px;
  background: #fff;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  position: sticky;
  top: 0;
  z-index: 1000;
}

.header-left {
  display: flex;
  align-items: center;
}

.logo {
  font-size: 24px;
  font-weight: bold;
  color: #409eff;
  margin-right: 15px;
}

.title {
  font-size: 16px;
  color: #333;
}

.header-right {
  display: flex;
  align-items: center;
}

.user-info {
  display: flex;
  align-items: center;
  cursor: pointer;
  padding: 8px 12px;
  border-radius: 4px;
  transition: background-color 0.2s;
}

.user-info:hover {
  background-color: #f5f7fa;
}

.user-icon {
  font-size: 20px;
  margin-right: 8px;
  color: #606266;
}

.username {
  font-size: 14px;
  color: #303133;
  margin-right: 4px;
}

.dropdown-icon {
  font-size: 12px;
  color: #909399;
}
</style>
