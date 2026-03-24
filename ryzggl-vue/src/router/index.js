import { createRouter, createWebHistory } from 'vue-router'
import { ElMessage } from 'element-plus'
import { isAuthenticated, hasRole } from '@/utils/auth'

/**
 * Vue Router configuration
 * Maps to .NET page structure (zjs/, CertifManage/, etc.)
 */
const routes = [
  {
    path: '/simple-test',
    name: 'SimpleTest',
    component: () => import('../views/SimpleTest.vue'),
    meta: { requiresAuth: false, title: '简单测试' }
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('../views/Login.vue'),
    meta: { requiresAuth: false }
  },
  {
    path: '/',
    name: 'Dashboard',
    component: () => import('../views/Dashboard.vue'),
    meta: { requiresAuth: true, title: '工作台' }
  },
  {
    path: '/migration-dashboard',
    name: 'MigrationDashboard',
    component: () => import('../views/MigrationDashboard.vue'),
    meta: { requiresAuth: false, title: '迁移进度追踪' }
  },
  {
    path: '/apply/list',
    name: 'ApplyList',
    component: () => import('../views/apply/List.vue'),
    meta: { requiresAuth: true, title: '应用列表' }
  },
  {
    path: '/apply/create',
    name: 'ApplyCreate',
    component: () => import('../views/apply/Create.vue'),
    meta: { requiresAuth: true, title: '首次申请' }
  },
  {
    path: '/apply/:id/detail',
    name: 'ApplyDetail',
    component: () => import('../views/apply/Detail.vue'),
    meta: { requiresAuth: true, title: '申请详情' }
  },
  {
    path: '/certificates',
    name: 'CertificateList',
    component: () => import('../views/certificate/List.vue'),
    meta: { requiresAuth: true, title: '证书管理' }
  },
  {
    path: '/certificates/:id/detail',
    name: 'CertificateDetail',
    component: () => import('../views/certificate/Detail.vue'),
    meta: { requiresAuth: true, title: '证书详情' }
  },
  {
    path: '/worker/list',
    name: 'WorkerList',
    component: () => import('../views/worker/List.vue'),
    meta: { requiresAuth: true, title: '人员列表' }
  },
  {
    path: '/worker/create',
    name: 'WorkerCreate',
    component: () => import('../views/worker/Create.vue'),
    meta: { requiresAuth: true, title: '新增人员' }
  },
  {
    path: '/worker/:id/detail',
    name: 'WorkerDetail',
    component: () => import('../views/worker/Detail.vue'),
    meta: { requiresAuth: true, title: '人员详情' }
  },
  {
    path: '/exam/list',
    name: 'ExamList',
    component: () => import('../views/exam/List.vue'),
    meta: { requiresAuth: true, title: '考试列表' }
  },
  {
    path: '/exam/:id/detail',
    name: 'ExamDetail',
    component: () => import('../views/exam/Detail.vue'),
    meta: { requiresAuth: true, title: '考试详情' }
  },
  {
    path: '/certificate/continue',
    name: 'CertificateContinue',
    component: () => import('../views/certificate/Continue.vue'),
    meta: { requiresAuth: true, title: '证书延续' }
  },
  {
    path: '/settings',
    name: 'Settings',
    component: () => import('../views/Settings.vue'),
    meta: { requiresAuth: true, title: '系统设置', roles: ['admin'] }
  },
  {
    path: '/apply/change',
    name: 'ApplyChange',
    component: () => import('../views/apply/Change.vue'),
    meta: { requiresAuth: true, title: '变更申请' }
  },
  {
    path: '/apply/renew',
    name: 'ApplyRenew',
    component: () => import('../views/apply/Renew.vue'),
    meta: { requiresAuth: true, title: '延续申请' }
  },
  {
    path: '/apply/first',
    name: 'ApplyFirst',
    component: () => import('../views/apply/First.vue'),
    meta: { requiresAuth: true, title: '首次注册申请' }
  },
  {
    path: '/apply/cancel',
    name: 'ApplyCancel',
    component: () => import('../views/apply/Cancel.vue'),
    meta: { requiresAuth: true, title: '注销申请' }
  },
  {
    path: '/apply/change',
    name: 'ApplyChange',
    component: () => import('../views/apply/Change.vue'),
    meta: { requiresAuth: true, title: '变更申请' }
  },
  {
    path: '/worker/list',
    name: 'WorkerList',
    component: () => import('../views/worker/List.vue'),
    meta: { requiresAuth: true, title: '人员列表' }
  },
  {
    path: '/certificate/change',
    name: 'CertificateChange',
    component: () => import('../views/certificate/Change.vue'),
    meta: { requiresAuth: true, title: '证书变更' }
  },
  {
    path: '/exam/signup',
    name: 'ExamSignUp',
    component: () => import('../views/exam/SignUp.vue'),
    meta: { requiresAuth: true, title: '考试报名' }
  },
  {
    path: '/exam/results',
    name: 'ExamResults',
    component: () => import('../views/exam/Results.vue'),
    meta: { requiresAuth: true, title: '考试成绩' }
  },
  {
    path: '/department/list',
    name: 'DepartmentList',
    component: () => import('../views/department/List.vue'),
    meta: { requiresAuth: true, title: '部门列表' }
  },
  {
    path: '/department/detail',
    name: 'DepartmentDetail',
    component: () => import('../views/department/Detail.vue'),
    meta: { requiresAuth: true, title: '部门详情' }
  },
  {
    path: '/department/edit',
    name: 'DepartmentEdit',
    component: () => import('../views/department/Edit.vue'),
    meta: { requiresAuth: true, title: '编辑部门' }
  },
  {
    path: '/user/list',
    name: 'UserList',
    component: () => import('../views/user/List.vue'),
    meta: { requiresAuth: true, title: '用户列表', roles: ['admin', 'manager'] }
  },
  {
    path: '/role/list',
    name: 'RoleList',
    component: () => import('../views/role/List.vue'),
    meta: { requiresAuth: true, title: '角色列表', roles: ['admin'] }
  },
  {
    path: '/exam/places',
    name: 'ExamPlaces',
    component: () => import('../views/exam/ExamPlace.vue'),
    meta: { requiresAuth: true, title: '考点列表' }
  },
  {
    path: '/report',
    name: 'ReportIndex',
    component: () => import('../views/report/Index.vue'),
    meta: { requiresAuth: true, title: '报表统计', roles: ['admin', 'manager'] }
  },
  {
    path: '/apply/cancel',
    name: 'ApplyCancel',
    component: () => import('../views/apply/Cancel.vue'),
    meta: { requiresAuth: true, title: '注销申请' }
  },
  {
    path: '/apply/replace',
    name: 'ApplyReplace',
    component: () => import('../views/apply/Replace.vue'),
    meta: { requiresAuth: true, title: '补办申请' }
  },
  {
    path: '/certificate/pause',
    name: 'CertificatePause',
    component: () => import('../views/certificate/Pause.vue'),
    meta: { requiresAuth: true, title: '证书暂停' }
  },
  {
    path: '/certificate/lock',
    name: 'CertificateLock',
    component: () => import('../views/certificate/Lock.vue'),
    meta: { requiresAuth: true, title: '证书锁定' }
  },
  {
    path: '/certificate/merge',
    name: 'CertificateMerge',
    component: () => import('../views/certificate/Merge.vue'),
    meta: { requiresAuth: true, title: '证书合并' }
  },
  {
    path: '/exam/participants',
    name: 'ExamParticipants',
    component: () => import('../views/exam/Participants.vue'),
    meta: { requiresAuth: true, title: '参考人员' }
  },
  {
    path: '/exam/plans',
    name: 'ExamPlans',
    component: () => import('../views/exam/ExamPlan.vue'),
    meta: { requiresAuth: true, title: '考试计划', roles: ['admin', 'manager', 'examiner'] }
  },
  {
    path: '/organization/list',
    name: 'OrganizationList',
    component: () => import('../views/Organization.vue'),
    meta: { requiresAuth: true, title: '机构管理' }
  },
  {
    path: '/enterprise/list',
    name: 'EnterpriseList',
    component: () => import('../views/enterprise/List.vue'),
    meta: { requiresAuth: true, title: '企业列表' }
  },
  {
    path: '/qualification/list',
    name: 'QualificationList',
    component: () => import('../views/qualification/List.vue'),
    meta: { requiresAuth: true, title: '资格列表' }
  },
  {
    path: '/file/list',
    name: 'FileList',
    component: () => import('../views/file/List.vue'),
    meta: { requiresAuth: true, title: '文件管理' }
  },
  {
    path: '/qualification/list',
    name: 'QualificationList',
    component: () => import('../views/Qualification.vue'),
    meta: { requiresAuth: true, title: '资格管理' }
  },
  {
    path: '/import-export',
    name: 'ImportExport',
    component: () => import('../views/import-export/Index.vue'),
    meta: { requiresAuth: true, title: '数据导入导出', roles: ['admin', 'manager'] }
  },
  {
    path: '/entity-management/:type',
    name: 'EntityManagement',
    component: () => import('../views/EntityManagement.vue'),
    meta: { requiresAuth: true, title: '实体管理' }
  },
  {
    path: '/apply-continue',
    name: 'ApplyContinueManagement',
    component: () => import('../views/EntityManagement.vue'),
    meta: { requiresAuth: true, title: '延续申请管理' },
    redirect: '/entity-management/applyContinue'
  },
  {
    path: '/apply-renew',
    name: 'ApplyRenewManagement',
    component: () => import('../views/EntityManagement.vue'),
    meta: { requiresAuth: true, title: '续期申请管理' },
    redirect: '/entity-management/applyRenew'
  },
  {
    path: '/certificate-change',
    name: 'CertificateChangeManagement',
    component: () => import('../views/EntityManagement.vue'),
    meta: { requiresAuth: true, title: '证书变更管理' },
    redirect: '/entity-management/certificateChange'
  },
  {
    path: '/certificate-continue',
    name: 'CertificateContinueManagement',
    component: () => import('../views/EntityManagement.vue'),
    meta: { requiresAuth: true, title: '证书延续管理' },
    redirect: '/entity-management/certificateContinue'
  },
  {
    path: '/check-task/list',
    name: 'CheckTaskList',
    component: () => import('../views/checkTask/List.vue'),
    meta: { requiresAuth: true, title: '业务申请单抽任务管理' }
  },
  {
    path: '/check-task/detail',
    name: 'CheckTaskDetail',
    component: () => import('../views/checkTask/Detail.vue'),
    meta: { requiresAuth: true, title: '业务申请单抽任务详情' }
  },
  {
    path: '/certificate-lock/list',
    name: 'CertificateLockList',
    component: () => import('../views/CertificateLock.vue'),
    meta: { requiresAuth: true, title: '证书锁定管理' }
  },
  {
    path: '/dictionary/list',
    name: 'DictionaryList',
    component: () => import('../views/Dictionary.vue'),
    meta: { requiresAuth: true, title: '数据字典管理' }
  },
  {
    path: '/post-info/list',
    name: 'PostInfoList',
    component: () => import('../views/PostInfo.vue'),
    meta: { requiresAuth: true, title: '岗位信息管理' }
  },
  {
    // 404 redirect
    path: '/:pathMatch(.*)*',
    name: 'NotFound',
    redirect: '/'
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

/**
 * Navigation guard - Check authentication and authorization
 */
router.beforeEach((to, from, next) => {
  const requiresAuth = to.matched.some(record => record.meta.requiresAuth)

  // Set page title
  if (to.meta.title) {
    document.title = `${to.meta.title} - RYZGGL人员资格管理系统`
  }

  // Check authentication
  if (requiresAuth && !isAuthenticated()) {
    // Redirect to login if no token and auth required
    ElMessage.warning('请先登录')
    next('/login')
    return
  }

  // Check if already on login page and has token
  if (to.path === '/login' && isAuthenticated()) {
    next('/')
    return
  }

  // Check role permissions (if roles are defined in meta)
  if (to.meta.roles && Array.isArray(to.meta.roles)) {
    if (!hasRole(to.meta.roles)) {
      ElMessage.error('您没有权限访问此页面')
      next('/')
      return
    }
  }

  next()
})

export default router
