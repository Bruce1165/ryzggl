import axios from 'axios'
import { ElMessage, ElLoading } from 'element-plus'

/**
 * Global loading instance
 */
let loadingInstance = null
let requestCount = 0

/**
 * Show loading
 */
function showLoading(config) {
  // Skip loading for specific requests
  if (config && config.skipLoading) {
    return
  }

  if (requestCount === 0) {
    loadingInstance = ElLoading.service({
      lock: true,
      text: '加载中...',
      background: 'rgba(0, 0, 0, 0.7)',
      spinner: 'el-icon-loading'
    })
  }
  requestCount++
}

/**
 * Hide loading
 */
function hideLoading() {
  requestCount--
  if (requestCount <= 0) {
    requestCount = 0
    if (loadingInstance) {
      loadingInstance.close()
      loadingInstance = null
    }
  }
}

/**
 * Create axios instance with base configuration
 */
const service = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || 'http://localhost:8080',
  timeout: 10000
})

/**
 * Request interceptor - Add JWT token to headers
 */
service.interceptors.request.use(
  config => {
    // Show loading for non-skipped requests
    showLoading(config)

    // Add JWT token from localStorage
    const token = localStorage.getItem('token')
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`
    }

    // Add unit code from localStorage
    const unitCode = localStorage.getItem('unitCode')
    if (unitCode) {
      config.headers['Unit-Code'] = unitCode
    }

    return config
  },
  error => {
    hideLoading()
    console.error('Request error:', error)
    return Promise.reject(error)
  }
)

/**
 * Response interceptor - Handle common error codes
 */
service.interceptors.response.use(
  response => {
    hideLoading()

    const res = response.data

    // Success - Return data directly
    if (res.code === 0) {
      return res.data
    }

    // Business error
    if (res.code === 1) {
      ElMessage.error(res.message || '操作失败')
      return Promise.reject(new Error(res.message))
    }

    // Unauthorized (2) - Token expired or invalid
    if (res.code === 2) {
      ElMessage.error('登录已过期，请重新登录')
      localStorage.removeItem('token')
      localStorage.removeItem('unitCode')
      localStorage.removeItem('userInfo')
      // Redirect to login
      window.location.href = '/login'
      return Promise.reject(new Error('Unauthorized'))
    }

    // Forbidden (3) - No permission
    if (res.code === 3) {
      ElMessage.error('没有权限访问此页面')
      return Promise.reject(new Error('Forbidden'))
    }

    // Show error message
    ElMessage.error(res.message || '操作失败')
    return Promise.reject(new Error(res.message))
  },
  error => {
    hideLoading()
    console.error('Response error:', error)
    // Handle network errors
    if (error.response) {
      // HTTP status errors
      const status = error.response.status
      if (status === 401) {
        ElMessage.error('登录已过期，请重新登录')
        localStorage.removeItem('token')
        window.location.href = '/login'
      } else if (status === 403) {
        ElMessage.error('没有权限访问此资源')
      } else if (status === 500) {
        ElMessage.error('服务器错误，请稍后重试')
      } else {
        ElMessage.error(`请求失败: ${status}`)
      }
    } else {
      // Network errors
      ElMessage.error('网络连接失败，请检查网络')
    }
    return Promise.reject(error)
  }
)

/**
 * Wrapper for API requests
 * @param {Object} config - Axios config
 * @param {Boolean} config.skipLoading - Skip loading indicator
 */
export default function request(config) {
  return service(config)
}
