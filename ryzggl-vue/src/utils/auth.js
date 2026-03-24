/**
 * Authentication management utilities
 */

/**
 * Store authentication data
 * @param {Object} authData - Authentication response from server
 */
export function setAuthData(authData) {
  if (authData.token) {
    localStorage.setItem('token', authData.token)
  }

  if (authData.user) {
    const userInfo = {
      id: authData.user.id,
      username: authData.user.username || authData.user.userName,
      realName: authData.user.realName || authData.user.userName,
      unitCode: authData.user.unitCode,
      unitName: authData.user.unitName,
      roles: authData.user.roles || [],
      permissions: authData.user.permissions || []
    }
    localStorage.setItem('userInfo', JSON.stringify(userInfo))

    if (userInfo.unitCode) {
      localStorage.setItem('unitCode', userInfo.unitCode)
    }
  }
}

/**
 * Get authentication token
 * @returns {string|null} JWT token
 */
export function getToken() {
  return localStorage.getItem('token')
}

/**
 * Get user info
 * @returns {Object|null} User information
 */
export function getUserInfo() {
  const userInfo = localStorage.getItem('userInfo')
  return userInfo ? JSON.parse(userInfo) : null
}

/**
 * Check if user has specific role
 * @param {string|Array} role - Role name or array of role names
 * @returns {boolean} Has role
 */
export function hasRole(role) {
  const userInfo = getUserInfo()
  if (!userInfo || !userInfo.roles) return false

  if (Array.isArray(role)) {
    return role.some(r => userInfo.roles.includes(r))
  }

  return userInfo.roles.includes(role)
}

/**
 * Check if user has specific permission
 * @param {string} permission - Permission name
 * @returns {boolean} Has permission
 */
export function hasPermission(permission) {
  const userInfo = getUserInfo()
  if (!userInfo || !userInfo.permissions) return false

  return userInfo.permissions.includes(permission)
}

/**
 * Clear authentication data (logout)
 */
export function clearAuthData() {
  localStorage.removeItem('token')
  localStorage.removeItem('userInfo')
  localStorage.removeItem('unitCode')
  localStorage.removeItem('userName')
}

/**
 * Check if user is authenticated
 * @returns {boolean} Is authenticated
 */
export function isAuthenticated() {
  return !!getToken()
}

/**
 * Get current username
 * @returns {string|null} Username
 */
export function getUsername() {
  return localStorage.getItem('userName')
}

/**
 * Get current unit code
 * @returns {string|null} Unit code
 */
export function getUnitCode() {
  return localStorage.getItem('unitCode')
}
