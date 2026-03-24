/**
 * SECURE AUTHENTICATION MANAGEMENT
 *
 * Security enhancements:
 * - Token validation with expiration checking
 * - Secure encryption for sensitive data
 * - Token refresh mechanism
 * - CSRF token management
 * - Secure logout procedures
 *
 * NOTE: For production, tokens should be stored in httpOnly cookies.
 * This implementation provides encryption for localStorage as a fallback.
 */

import CryptoJS from 'crypto-js'

// Encryption key - In production, this should come from environment variable
// and be rotated regularly
const ENCRYPTION_KEY = import.meta.env.VITE_ENCRYPTION_KEY || 'your-secret-key-change-in-production'

// Token expiration buffer (5 minutes before actual expiration)
const TOKEN_EXPIRATION_BUFFER = 5 * 60 * 1000

/**
 * Encrypt data using AES
 * @param {string} data - Data to encrypt
 * @returns {string} Encrypted string
 */
function encrypt(data) {
  try {
    return CryptoJS.AES.encrypt(data, ENCRYPTION_KEY).toString()
  } catch (error) {
    console.error('Encryption failed:', error)
    throw new Error('Failed to encrypt data')
  }
}

/**
 * Decrypt data using AES
 * @param {string} encryptedData - Encrypted string
 * @returns {string} Decrypted data
 */
function decrypt(encryptedData) {
  try {
    const bytes = CryptoJS.AES.decrypt(encryptedData, ENCRYPTION_KEY)
    return bytes.toString(CryptoJS.enc.Utf8)
  } catch (error) {
    console.error('Decryption failed:', error)
    throw new Error('Failed to decrypt data')
  }
}

/**
 * Decode JWT token without verification (for client-side data extraction)
 * WARNING: Never trust this data without server verification
 * @param {string} token - JWT token
 * @returns {Object|null} Decoded payload
 */
function decodeJWT(token) {
  try {
    const base64Url = token.split('.')[1]
    const base64 = base64Url.replace(/-/g, '+').replace(/_/g, '/')
    const jsonPayload = decodeURIComponent(
      atob(base64)
        .split('')
        .map(c => '%' + ('00' + c.charCodeAt(0).toString(16)).slice(-2))
        .join('')
    )
    return JSON.parse(jsonPayload)
  } catch (error) {
    console.error('JWT decode failed:', error)
    return null
  }
}

/**
 * Check if token is expired or about to expire
 * @param {string} token - JWT token
 * @returns {boolean} Is expired or expiring soon
 */
function isTokenExpired(token) {
  const decoded = decodeJWT(token)
  if (!decoded || !decoded.exp) {
    return true // Invalid token, consider expired
  }

  const expirationTime = decoded.exp * 1000 // Convert to milliseconds
  const currentTime = Date.now()
  const expirationBuffer = expirationTime - TOKEN_EXPIRATION_BUFFER

  return currentTime >= expirationBuffer
}

/**
 * Get token expiration time
 * @param {string} token - JWT token
 * @returns {number|null} Expiration timestamp or null
 */
function getTokenExpiration(token) {
  const decoded = decodeJWT(token)
  return decoded?.exp ? decoded.exp * 1000 : null
}

/**
 * Store authentication data securely
 * @param {Object} authData - Authentication response from server
 * @param {string} authData.token - JWT token
 * @param {Object} authData.user - User information
 * @param {string} authData.refreshToken - Refresh token (optional)
 */
export function setAuthData(authData) {
  try {
    if (!authData.token) {
      throw new Error('Token is required')
    }

    // Validate token before storing
    const decoded = decodeJWT(authData.token)
    if (!decoded) {
      throw new Error('Invalid token format')
    }

    // Store token (encrypted)
    const encryptedToken = encrypt(authData.token)
    localStorage.setItem('auth_token', encryptedToken)

    // Store token expiration for quick checking
    const expiration = getTokenExpiration(authData.token)
    if (expiration) {
      localStorage.setItem('token_expiration', expiration.toString())
    }

    // Store refresh token if provided (encrypted)
    if (authData.refreshToken) {
      const encryptedRefreshToken = encrypt(authData.refreshToken)
      localStorage.setItem('refresh_token', encryptedRefreshToken)
    }

    // Store CSRF token if provided
    if (authData.csrfToken) {
      localStorage.setItem('csrf_token', authData.csrfToken)
    }

    // Store user info (encrypted)
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

      const encryptedUserInfo = encrypt(JSON.stringify(userInfo))
      localStorage.setItem('user_info', encryptedUserInfo)

      // Store non-sensitive data in sessionStorage (cleared on browser close)
      if (userInfo.unitCode) {
        sessionStorage.setItem('unit_code', userInfo.unitCode)
      }
    }

    // Log security event
    logSecurityEvent('AUTH_DATA_STORED', {
      userId: authData.user?.id,
      username: authData.user?.username
    })
  } catch (error) {
    console.error('Failed to store auth data:', error)
    throw new Error('Failed to store authentication data')
  }
}

/**
 * Get authentication token
 * @returns {string|null} JWT token
 */
export function getToken() {
  try {
    const encryptedToken = localStorage.getItem('auth_token')
    if (!encryptedToken) {
      return null
    }

    const token = decrypt(encryptedToken)

    // Check if token is expired
    if (isTokenExpired(token)) {
      console.warn('Token expired')
      return null
    }

    return token
  } catch (error) {
    console.error('Failed to get token:', error)
    return null
  }
}

/**
 * Get refresh token
 * @returns {string|null} Refresh token
 */
export function getRefreshToken() {
  try {
    const encryptedRefreshToken = localStorage.getItem('refresh_token')
    if (!encryptedRefreshToken) {
      return null
    }

    return decrypt(encryptedRefreshToken)
  } catch (error) {
    console.error('Failed to get refresh token:', error)
    return null
  }
}

/**
 * Get CSRF token
 * @returns {string|null} CSRF token
 */
export function getCSRFToken() {
  return localStorage.getItem('csrf_token')
}

/**
 * Get user info
 * @returns {Object|null} User information
 */
export function getUserInfo() {
  try {
    const encryptedUserInfo = localStorage.getItem('user_info')
    if (!encryptedUserInfo) {
      return null
    }

    const decrypted = decrypt(encryptedUserInfo)
    return JSON.parse(decrypted)
  } catch (error) {
    console.error('Failed to get user info:', error)
    return null
  }
}

/**
 * Check if user is authenticated
 * @returns {boolean} Is authenticated
 */
export function isAuthenticated() {
  const token = getToken()
  return !!token && !isTokenExpired(token)
}

/**
 * Check if token needs refresh
 * @returns {boolean} Needs refresh
 */
export function needsTokenRefresh() {
  const token = getToken()
  if (!token) {
    return false
  }

  const expiration = getTokenExpiration(token)
  if (!expiration) {
    return false
  }

  // Return true if token expires within buffer time
  return Date.now() >= (expiration - TOKEN_EXPIRATION_BUFFER)
}

/**
 * Refresh authentication token
 * @param {string} refreshToken - Refresh token
 * @returns {Promise<Object>} New authentication data
 */
export async function refreshToken(refreshToken) {
  try {
    const response = await fetch('/api/auth/refresh', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        'X-CSRF-Token': getCSRFToken() || ''
      },
      body: JSON.stringify({ refreshToken })
    })

    if (!response.ok) {
      throw new Error('Token refresh failed')
    }

    const data = await response.json()

    // Store new auth data
    setAuthData(data)

    // Log security event
    logSecurityEvent('TOKEN_REFRESHED', {
      userId: data.user?.id
    })

    return data
  } catch (error) {
    console.error('Token refresh failed:', error)
    logSecurityEvent('TOKEN_REFRESH_FAILED', { error: error.message })
    throw error
  }
}

/**
 * Clear all authentication data (secure logout)
 */
export function clearAuthData() {
  // Log security event before clearing
  const userInfo = getUserInfo()
  logSecurityEvent('AUTH_CLEARED', {
    userId: userInfo?.id,
    username: userInfo?.username
  })

  // Clear all auth-related localStorage items
  const authItems = [
    'auth_token',
    'refresh_token',
    'user_info',
    'token_expiration',
    'csrf_token',
    'unitCode',
    'userName',
    'userInfo'
  ]

  authItems.forEach(item => {
    try {
      localStorage.removeItem(item)
    } catch (error) {
      console.error(`Failed to clear ${item}:`, error)
    }
  })

  // Clear sessionStorage
  sessionStorage.clear()

  // Clear any cached data that might contain sensitive info
  if ('caches' in window) {
    caches.keys().then(cacheNames => {
      cacheNames.forEach(cacheName => {
        caches.delete(cacheName)
      })
    })
  }
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
 * Get current username
 * @returns {string|null} Username
 */
export function getUsername() {
  const userInfo = getUserInfo()
  return userInfo?.username || null
}

/**
 * Get current unit code
 * @returns {string|null} Unit code
 */
export function getUnitCode() {
  // Prefer sessionStorage (cleared on close)
  const sessionUnitCode = sessionStorage.getItem('unit_code')
  if (sessionUnitCode) {
    return sessionUnitCode
  }

  // Fallback to userInfo
  const userInfo = getUserInfo()
  return userInfo?.unitCode || null
}

/**
 * Get token expiration time
 * @returns {number|null} Expiration timestamp
 */
export function getTokenExpirationTime() {
  const expirationStr = localStorage.getItem('token_expiration')
  if (!expirationStr) {
    return null
  }

  const expiration = parseInt(expirationStr, 10)
  return isNaN(expiration) ? null : expiration
}

/**
 * Validate token integrity
 * @param {string} token - Token to validate
 * @returns {boolean} Is valid
 */
export function validateToken(token) {
  if (!token) {
    return false
  }

  const decoded = decodeJWT(token)
  if (!decoded) {
    return false
  }

  // Check required fields
  if (!decoded.sub && !decoded.userId) {
    return false
  }

  if (!decoded.exp) {
    return false
  }

  // Check expiration
  return !isTokenExpired(token)
}

/**
 * Log security event
 * @param {string} eventType - Type of security event
 * @param {Object} details - Event details
 */
function logSecurityEvent(eventType, details = {}) {
  try {
    const event = {
      type: eventType,
      timestamp: new Date().toISOString(),
      userAgent: navigator.userAgent,
      url: window.location.href,
      ...details
    }

    // Store in localStorage for debugging (max 50 events)
    const securityEvents = JSON.parse(localStorage.getItem('security_events') || '[]')
    securityEvents.push(event)

    // Keep only last 50 events
    if (securityEvents.length > 50) {
      securityEvents.shift()
    }

    localStorage.setItem('security_events', JSON.stringify(securityEvents))

    // In production, send to server
    if (import.meta.env.PROD) {
      sendSecurityLogToServer(event)
    }
  } catch (error) {
    console.error('Failed to log security event:', error)
  }
}

/**
 * Send security log to server
 * @param {Object} event - Security event
 */
function sendSecurityLogToServer(event) {
  try {
    fetch('/api/security/log', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(event),
      keepalive: true // Ensure request completes even if page unloads
    }).catch(error => {
      console.error('Failed to send security log:', error)
    })
  } catch (error) {
    console.error('Failed to send security log:', error)
  }
}

/**
 * Get security events for debugging
 * @returns {Array} Security events
 */
export function getSecurityEvents() {
  try {
    return JSON.parse(localStorage.getItem('security_events') || '[]')
  } catch (error) {
    console.error('Failed to get security events:', error)
    return []
  }
}

/**
 * Clear security events
 */
export function clearSecurityEvents() {
  localStorage.removeItem('security_events')
}
