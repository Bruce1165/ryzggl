# RYZGGL 项目快速开始指南

**最后更新**: 2026年3月15日
**当前状态**: ✅ P0+P1任务全部完成

---

## 🚀 快速启动

### 启动后端

```bash
cd /Users/mac/Documents/RYZGGL-C/ryzggl-java
mvn clean package
java -jar target/ryzggl-java-1.0.0.jar
```

**后端地址**: http://localhost:8080
**API文档**: http://localhost:8080/doc.html

### 启动前端

```bash
cd /Users/mac/Documents/RYZGGL-C/ryzggl-vue
npm install
npm run dev
```

**前端地址**: http://localhost:5173

---

## 📊 当前进度

### ✅ 已完成 (8/8任务)

| 任务 | 状态 |
|------|------|
| 数据库连接验证 | ✅ 完成 |
| Axios请求拦截器 | ✅ 完成 |
| 前端路由配置 | ✅ 完成 |
| 认证系统(登录/登出) | ✅ 完成 |
| 申请管理-列表页 | ✅ 完成 |
| 申请管理-创建页 | ✅ 完成 |
| 证书管理-列表页 | ✅ 完成 |
| 通用组件封装 | ✅ 完成 |

### ⏳ 待完成

**P1任务** (4-8小时):
- [ ] 申请管理-变更/续期/注销页
- [ ] 证书管理-详情页
- [ ] 证书生命周期管理
- [ ] Dashboard数据对接
- [ ] 用户/角色管理

**P2任务** (6-16小时):
- [ ] 高优先级实体迁移 (8个实体)
- [ ] 考试模块前端
- [ ] 文件管理模块

---

## 🔧 数据库配置

### 当前配置

```yaml
spring:
  datasource:
    driver-class-name: com.microsoft.sqlserver.jdbc.SQLServerDriver
    url: jdbc:sqlserver://101.200.193.13:1433;databaseName=RYZG;encrypt=false;trustServerCertificate=true
    username: synergydatauser
    password: synergy_p@ssword
```

### 可用表

- ✅ Apply (申请)
- ✅ Certificate (证书)
- ✅ Worker (人员)
- ✅ User (用户)
- ❌ Exam (考试 - 待确认)

---

## 🎯 下一个任务建议

### 选项1: 继续P1任务

从以下任务中选择一个继续:

**1. 申请管理-变更/续期/注销页**
- 工时: 8-12小时
- 文件: `/src/views/apply/Change.vue`
- 功能: 变更、续期、注销、补办表单

**2. 证书管理-详情页**
- 工时: 4-6小时
- 文件: `/src/views/certificate/Detail.vue`
- 功能: 证书详情、操作历史

**3. Dashboard数据对接**
- 工时: 4-6小时
- 文件: `/src/views/Dashboard.vue`
- 功能: 统计API对接、图表组件

### 选项2: 开始P2任务

**高优先级实体迁移**
- 工时: 16-20小时
- 优先级: P2
- 功能: Department, Organization, Exam相关实体

### 选项3: 联调测试

**前后端集成测试**
- 启动后端服务
- 测试所有已完成功能
- 修复发现的问题
- 工时: 2-4小时

---

## 📁 通用组件使用指南

### DataTable 使用示例

```vue
<DataTable
  :data="tableData"
  :loading="loading"
  :pagination="page"
  @sort-change="handleSort"
>
  <el-table-column prop="name" label="名称" />
  <el-table-column prop="status" label="状态" />

  <template #actions="{ row }">
    <el-button size="small" @click="handleView(row)">查看</el-button>
  </template>
</DataTable>
```

### DataForm 使用示例

```vue
<DataForm
  v-model="formData"
  :rules="rules"
  label-width="120px"
  @submit="handleSubmit"
>
  <el-form-item label="名称" prop="name">
    <el-input v-model="formData.name" />
  </el-form-item>

  <template #actions>
    <el-button type="primary" @click="handleCustom">自定义</el-button>
  </template>
</DataForm>
```

### DataDialog 使用示例

```vue
<DataDialog
  v-model="dialogVisible"
  title="提示"
  width="50%"
  @confirm="handleConfirm"
  @cancel="handleCancel"
>
  <p>对话框内容</p>
</DataDialog>
```

---

## 🔐 认证工具使用

### 导入

```javascript
import { isAuthenticated, getUserInfo, hasRole, clearAuthData } from '@/utils/auth'
```

### 常用方法

```javascript
// 检查是否登录
if (!isAuthenticated()) {
  router.push('/login')
}

// 获取用户信息
const userInfo = getUserInfo()
console.log(userInfo.roles) // 用户角色数组

// 检查角色权限
if (hasRole(['admin', 'manager'])) {
  // 有管理权限
}

// 登出
clearAuthData()
router.push('/login')
```

---

## 🐛 常见问题

### Q1: 数据库连接失败
**A**: 检查 `/ryzggl-java/src/main/resources/application.yml` 中的密码是否为 `synergy_p@ssword`

### Q2: 前端无法访问后端
**A**:
1. 确保后端已启动 (`java -jar target/ryzggl-java-1.0.0.jar`)
2. 检查端口8080是否被占用
3. 查看 `/ryzggl-vue/src/api/request.js` 中的baseURL是否正确

### Q3: 登录后页面空白
**A**: 检查token是否正确存储，查看浏览器控制台是否有错误

### Q4: 路由跳转不工作
**A**: 确保路由守卫中的检查逻辑正确，使用 `isAuthenticated()` 工具函数

---

## 📚 相关文档

- **详细进度报告**: `/Users/mac/Desktop/RYZGGL开发进度报告_2026-03-15.md`
- **项目文档**: `/Users/mac/Documents/RYZGGL-C/ryzggl-java/README.md`
- **数据库参考**: `/Users/mac/Documents/RYZGGL-C/DATABASE_REFERENCE.md`

---

## ✅ 检查清单

启动开发前确认:

- [ ] 数据库连接正常
- [ ] 后端服务已启动
- [ ] 前端开发服务器已启动
- [ ] npm依赖已安装
- [ ] 当前分支正确

---

**祝开发顺利！** 🚀
