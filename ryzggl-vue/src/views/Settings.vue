<template>
  <div class="settings-container">
    <!-- Page Header -->
    <div class="page-header">
      <h2>系统设置</h2>
      <div class="breadcrumb">当前位置: 系统设置</div>
    </div>

    <el-row :gutter="20">
      <!-- Settings Menu -->
      <el-col :span="4">
        <el-card class="settings-menu">
          <el-menu
            v-model:default-active="activeMenu"
            class="settings-menu-list"
            @select="handleMenuSelect"
          >
            <el-menu-item index="profile">
              <el-icon><User /></el-icon>
              <span>个人信息</span>
            </el-menu-item>
            <el-menu-item index="password">
              <el-icon><Lock /></el-icon>
              <span>修改密码</span>
            </el-menu-item>
            <el-menu-item index="notifications">
              <el-icon><Bell /></el-icon>
              <span>通知设置</span>
            </el-menu-item>
            <el-menu-item index="system">
              <el-icon><Setting /></el-icon>
              <span>系统参数</span>
            </el-menu-item>
            <el-menu-item index="logs">
              <el-icon><Document /></el-icon>
              <span>操作日志</span>
            </el-menu-item>
          </el-menu>
        </el-card>
      </el-col>

      <!-- Settings Content -->
      <el-col :span="20">
        <el-card class="settings-content">
          <!-- Profile Settings -->
          <div v-if="activeMenu === 'profile'" class="settings-section">
            <h3>个人信息</h3>
            <el-form :model="profileForm" label-width="120px" class="settings-form">
              <el-form-item label="用户名">
                <el-input v-model="profileForm.username" disabled />
              </el-form-item>
              <el-form-item label="姓名">
                <el-input v-model="profileForm.realName" />
              </el-form-item>
              <el-form-item label="部门">
                <el-input v-model="profileForm.department" disabled />
              </el-form-item>
              <el-form-item label="手机号码">
                <el-input v-model="profileForm.phone" />
              </el-form-item>
              <el-form-item label="电子邮箱">
                <el-input v-model="profileForm.email" />
              </el-form-item>
              <el-form-item>
                <el-button type="primary" @click="handleSaveProfile">保存</el-button>
              </el-form-item>
            </el-form>
          </div>

          <!-- Password Settings -->
          <div v-if="activeMenu === 'password'" class="settings-section">
            <h3>修改密码</h3>
            <el-form
              ref="passwordFormRef"
              :model="passwordForm"
              :rules="passwordRules"
              label-width="120px"
              class="settings-form"
            >
              <el-form-item label="原密码" prop="oldPassword">
                <el-input
                  v-model="passwordForm.oldPassword"
                  type="password"
                  show-password
                  placeholder="输入原密码"
                />
              </el-form-item>
              <el-form-item label="新密码" prop="newPassword">
                <el-input
                  v-model="passwordForm.newPassword"
                  type="password"
                  show-password
                  placeholder="输入新密码"
                />
              </el-form-item>
              <el-form-item label="确认密码" prop="confirmPassword">
                <el-input
                  v-model="passwordForm.confirmPassword"
                  type="password"
                  show-password
                  placeholder="再次输入新密码"
                />
              </el-form-item>
              <el-form-item>
                <el-button type="primary" @click="handleChangePassword">修改密码</el-button>
              </el-form-item>
            </el-form>
          </div>

          <!-- Notification Settings -->
          <div v-if="activeMenu === 'notifications'" class="settings-section">
            <h3>通知设置</h3>
            <el-form :model="notificationForm" label-width="200px" class="settings-form">
              <el-form-item label="申请待处理通知">
                <el-switch v-model="notificationForm.applyPending" />
              </el-form-item>
              <el-form-item label="证书到期提醒">
                <el-switch v-model="notificationForm.certExpiry" />
              </el-form-item>
              <el-form-item label="考试报名通知">
                <el-switch v-model="notificationForm.examSignup" />
              </el-form-item>
              <el-form-item label="邮件通知">
                <el-switch v-model="notificationForm.email" />
              </el-form-item>
              <el-form-item label="短信通知">
                <el-switch v-model="notificationForm.sms" />
              </el-form-item>
              <el-form-item>
                <el-button type="primary" @click="handleSaveNotifications">保存</el-button>
              </el-form-item>
            </el-form>
          </div>

          <!-- System Settings -->
          <div v-if="activeMenu === 'system'" class="settings-section">
            <h3>系统参数</h3>
            <el-form :model="systemForm" label-width="200px" class="settings-form">
              <el-form-item label="证书有效期（年）">
                <el-input-number v-model="systemForm.certValidYears" :min="1" :max="10" />
              </el-form-item>
              <el-form-item label="延续提前提醒（天）">
                <el-input-number v-model="systemForm.continueReminderDays" :min="1" :max="90" />
              </el-form-item>
              <el-form-item label="每页显示条数">
                <el-select v-model="systemForm.pageSize" style="width: 200px">
                  <el-option label="10条" :value="10" />
                  <el-option label="20条" :value="20" />
                  <el-option label="50条" :value="50" />
                  <el-option label="100条" :value="100" />
                </el-select>
              </el-form-item>
              <el-form-item>
                <el-button type="primary" @click="handleSaveSystem">保存</el-button>
              </el-form-item>
            </el-form>
          </div>

          <!-- Operation Logs -->
          <div v-if="activeMenu === 'logs'" class="settings-section">
            <h3>操作日志</h3>
            <el-table :data="logData" stripe border>
              <el-table-column prop="logTime" label="操作时间" width="180" />
              <el-table-column prop="operator" label="操作人" width="100" />
              <el-table-column prop="operation" label="操作内容" width="200" />
              <el-table-column prop="ip" label="IP地址" width="150" />
              <el-table-column prop="result" label="结果" width="80">
                <template #default="{row}">
                  <el-tag :type="row.result === '成功' ? 'success' : 'danger'">
                    {{ row.result }}
                  </el-tag>
                </template>
              </el-table-column>
            </el-table>
            <div class="pagination-container">
              <el-pagination
                v-model:current-page="logPage.current"
                v-model:page-size="logPage.size"
                :total="logPage.total"
                layout="total, prev, pager, next"
              />
            </div>
          </div>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import { ref, reactive } from 'vue'
import { ElMessage } from 'element-plus'
import { User, Lock, Bell, Setting, Document } from '@element-plus/icons-vue'

export default {
  name: 'Settings',

  components: {
    User,
    Lock,
    Bell,
    Setting,
    Document
  },

  setup() {
    const activeMenu = ref('profile')
    const passwordFormRef = ref(null)

    const profileForm = reactive({
      username: 'admin',
      realName: '管理员',
      department: '系统管理部',
      phone: '',
      email: ''
    })

    const passwordForm = reactive({
      oldPassword: '',
      newPassword: '',
      confirmPassword: ''
    })

    const passwordRules = {
      oldPassword: [{ required: true, message: '请输入原密码', trigger: 'blur' }],
      newPassword: [
        { required: true, message: '请输入新密码', trigger: 'blur' },
        { min: 6, message: '密码长度至少6位', trigger: 'blur' }
      ],
      confirmPassword: [
        { required: true, message: '请确认新密码', trigger: 'blur' },
        {
          validator: (rule, value, callback) => {
            if (value !== passwordForm.newPassword) {
              callback(new Error('两次输入的密码不一致'))
            } else {
              callback()
            }
          },
          trigger: 'blur'
        }
      ]
    }

    const notificationForm = reactive({
      applyPending: true,
      certExpiry: true,
      examSignup: true,
      email: true,
      sms: false
    })

    const systemForm = reactive({
      certValidYears: 5,
      continueReminderDays: 30,
      pageSize: 20
    })

    const logData = ref([
      { logTime: '2024-03-14 10:30:00', operator: 'admin', operation: '登录系统', ip: '192.168.1.100', result: '成功' },
      { logTime: '2024-03-14 10:15:00', operator: 'admin', operation: '修改密码', ip: '192.168.1.100', result: '成功' },
      { logTime: '2024-03-13 18:00:00', operator: 'admin', operation: '审批申请', ip: '192.168.1.100', result: '成功' },
      { logTime: '2024-03-13 17:30:00', operator: 'admin', operation: '导出数据', ip: '192.168.1.100', result: '成功' }
    ])

    const logPage = reactive({
      current: 1,
      size: 10,
      total: 4
    })

    /**
     * Handle menu select
     */
    const handleMenuSelect = (index) => {
      activeMenu.value = index
    }

    /**
     * Handle save profile
     */
    const handleSaveProfile = () => {
      ElMessage.success('个人信息保存成功')
    }

    /**
     * Handle change password
     */
    const handleChangePassword = async () => {
      if (!passwordFormRef.value) return

      try {
        const valid = await passwordFormRef.value.validate()
        if (!valid) return

        ElMessage.success('密码修改成功')
        passwordForm.oldPassword = ''
        passwordForm.newPassword = ''
        passwordForm.confirmPassword = ''
      } catch (error) {
        ElMessage.error('密码修改失败')
      }
    }

    /**
     * Handle save notifications
     */
    const handleSaveNotifications = () => {
      ElMessage.success('通知设置保存成功')
    }

    /**
     * Handle save system settings
     */
    const handleSaveSystem = () => {
      ElMessage.success('系统参数保存成功')
    }

    return {
      activeMenu,
      passwordFormRef,
      profileForm,
      passwordForm,
      passwordRules,
      notificationForm,
      systemForm,
      logData,
      logPage,
      handleMenuSelect,
      handleSaveProfile,
      handleChangePassword,
      handleSaveNotifications,
      handleSaveSystem
    }
  }
}
</script>

<style scoped>
.settings-container {
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

.settings-menu {
  height: 100%;
}

.settings-menu-list {
  border: none;
}

.settings-content {
  min-height: 600px;
}

.settings-section h3 {
  margin: 0 0 20px 0;
  font-size: 16px;
  font-weight: bold;
  color: #333;
  border-left: 4px solid #409eff;
  padding-left: 10px;
}

.settings-form {
  max-width: 500px;
}

.pagination-container {
  margin-top: 20px;
  display: flex;
  justify-content: center;
}
</style>
