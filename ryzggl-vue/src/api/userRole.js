import request from '@/utils/request'

/**
 * User Role API
 * 用户角色API
 */
export default {
  /**
   * Get all user roles
   */
  getAll() {
    return request({
      url: '/api/v1/user-roles/all',
      method: 'get'
    })
  },

  /**
   * Get user role by ID
   */
  getById(id) {
    return request({
      url: `/api/v1/user-roles/${id}`,
      method: 'get'
    })
  },

  /**
   * Get user roles by user ID
   */
  getByUserId(userId) {
    return request({
      url: '/api/v1/user-roles/user/' + userId,
      method: 'get'
    })
  },

  /**
   * Get user roles by role ID
   */
  getByRoleId(roleId) {
    return request({
      url: '/api/v1/user-roles/role/' + roleId,
      method: 'get'
    })
  },

  /**
   * Get user role by user ID and role ID
   */
  getByUserIdAndRoleId(userId, roleId) {
    return request({
      url: '/api/v1/user-roles/user-role',
      method: 'get',
      params: { userId, roleId }
    })
  },

  /**
   * Check if user has role
   */
  hasRole(userId, roleId) {
    return request({
      url: '/api/v1/user-roles/has-role',
      method: 'get',
      params: { userId, roleId }
    })
  },

  /**
   * Create user role
   */
  create(data) {
    return request({
      url: '/api/v1/user-roles',
      method: 'post',
      data
    })
  },

  /**
   * Delete user role
   */
  delete(id) {
    return request({
      url: `/api/v1/user-roles/${id}`,
      method: 'delete'
    })
  },

  /**
   * Delete all roles for user
   */
  deleteByUserId(userId) {
    return request({
      url: '/api/v1/user-roles/user/' + userId,
      method: 'delete'
    })
  }
}
