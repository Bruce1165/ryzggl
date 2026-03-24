import request from './request'

/**
 * User API - 用户管理
 * User management API
 */

export const API = {
  LIST: '/api/v1/users',
  ACTIVE: '/api/v1/users/active',
  GET_BY_ID: (id) => `/api/v1/users/${id}`,
  GET_BY_USERNAME: (username) => `/api/v1/users/username/${username}`,
  CREATE: '/api/v1/users',
  UPDATE: (id) => `/api/v1/users/${id}`,
  DELETE: (id) => `/api/v1/users/${id}`,
  RESET_PASSWORD: '/api/v1/users/reset-password',
  CHANGE_STATUS: '/api/v1/users/change-status'
}

/**
 * Get user list with pagination
 * @param {Object} params - {current, size, username, realName, departmentId, roleId, status}
 */
export function getUserList(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}

/**
 * Get all active users
 */
export function getActiveUsers() {
  return request({
    url: API.ACTIVE,
    method: 'get'
  })
}

/**
 * Get user by ID
 * @param {Number} id - User ID
 */
export function getUserById(id) {
  return request({
    url: API.GET_BY_ID(id),
    method: 'get'
  })
}

/**
 * Get user by username
 * @param {String} username - Username
 */
export function getUserByUsername(username) {
  return request({
    url: API.GET_BY_USERNAME(username),
    method: 'get'
  })
}

/**
 * Create user
 * @param {Object} data - User entity
 */
export function createUser(data) {
  return request({
    url: API.CREATE,
    method: 'post',
    data
  })
}

/**
 * Update user
 * @param {Number} id - User ID
 * @param {Object} data - User entity
 */
export function updateUser(id, data) {
  return request({
    url: API.UPDATE(id),
    method: 'put',
    data
  })
}

/**
 * Delete user
 * @param {Number} id - User ID
 */
export function deleteUser(id) {
  return request({
    url: API.DELETE(id),
    method: 'delete'
  })
}

/**
 * Reset user password
 * @param {Object} data - {userId, newPassword}
 */
export function resetPassword(data) {
  return request({
    url: API.RESET_PASSWORD,
    method: 'put',
    data
  })
}

/**
 * Change user status
 * @param {Object} data - {userId, status}
 */
export function changeUserStatus(data) {
  return request({
    url: API.CHANGE_STATUS,
    method: 'put',
    data
  })
}
