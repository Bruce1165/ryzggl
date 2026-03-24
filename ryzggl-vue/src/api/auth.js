import request from './request'

/**
 * User login
 * @param {Object} data - Login credentials
 */
export function login(data) {
  return request({
    url: '/api/auth/login',
    method: 'post',
    data
  })
}

/**
 * User register
 * @param {Object} data - Registration info
 */
export function register(data) {
  return request({
    url: '/api/auth/register',
    method: 'post',
    data
  })
}

/**
 * Logout
 */
export function logout() {
  return request({
    url: '/api/auth/logout',
    method: 'post'
  })
}

/**
 * Get current user info
 */
export function getUserInfo() {
  return request({
    url: '/api/auth/info',
    method: 'get'
  })
}
