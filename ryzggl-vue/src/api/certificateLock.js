/**
 * CertificateLock API Client
 * 证书锁定管理相关接口
 *
 * Maps to CertificateLockController
 * Backend endpoint: /api/v1/certificate-locks
 *
 * CertificateLock entity fields:
 * - lockId (锁定记录ID)
 * - certificateId (证书ID)
 * - lockType (锁定类型)
 * - lockTime (锁定时间)
 * - lockEndTime (锁定结束时间)
 * - lockPerson (锁定人)
 * - remark (备注)
 * - unlockTime (解锁时间)
 * - unlockPerson (解锁人)
 * - lockStatus (锁定状态：LOCKED/UNLOCKED)
 * - createPersonId (创建人)
 * - createTime (创建时间)
 * - modifyPersonId (修改人)
 * - modifyTime (修改时间)
 */

import request from './request'

/**
 * CertificateLock API endpoints
 */
const API = {
  // Base path
  BASE: '/api/v1/certificate-locks',
  // Get by ID
  GET_BY_ID: (id) => `/api/v1/certificate-locks/${id}`,
  // Get by certificate
  GET_BY_CERTIFICATE: (certificateId) => `/api/v1/certificate-locks/certificate/${certificateId}`,
  // Get all active
  ACTIVE: '/api/v1/certificate-locks/active',
  // Search
  SEARCH: '/api/v1/certificate-locks/search',
  // Lock
  LOCK: '/api/v1/certificate-locks/lock',
  // Unlock
  UNLOCK: '/api/v1/certificate-locks/unlock',
  // Get last lock
  LAST_LOCK: (certificateId) => `/api/v1/certificate-locks/certificate/${certificateId}/last`,
  // Check status
  CHECK_STATUS: (certificateId) => `/api/v1/certificate-locks/certificate/${certificateId}/status`,
  // Delete
  DELETE: (id) => `/api/v1/certificate-locks/${id}`
}

/**
 * Get lock by ID
 * @param {Number} id - Certificate Lock ID
 */
export function getLockById(id) {
  return request({
    url: API.GET_BY_ID(id),
    method: 'get'
  })
}

/**
 * Get locks by certificate ID
 * @param {Number} certificateId - Certificate ID
 */
export function getLocksByCertificateId(certificateId) {
  return request({
    url: API.GET_BY_CERTIFICATE(certificateId),
    method: 'get'
  })
}

/**
 * Get all active locks
 */
export function getActiveLocks() {
  return request({
    url: API.ACTIVE,
    method: 'get'
  })
}

/**
 * Search locks
 * @param {Object} params - {keyword}
 */
export function searchLocks(params) {
  return request({
    url: API.SEARCH,
    method: 'get',
    params
  })
}

/**
 * Lock certificate
 * @param {Object} data - CertificateLock entity data
 */
export function lockCertificate(data) {
  return request({
    url: API.LOCK,
    method: 'post',
    data
  })
}

/**
 * Unlock certificate
 * @param {Number} certificateId - Certificate ID
 * @param {String} unlockPerson - Unlock person
 */
export function unlockCertificate(certificateId, unlockPerson) {
  return request({
    url: API.UNLOCK(certificateId),
    method: 'post',
    params: { unlockPerson }
  })
}

/**
 * Get last lock for a certificate
 * @param {Number} certificateId - Certificate ID
 */
export function getLastLock(certificateId) {
  return request({
    url: API.LAST_LOCK(certificateId),
    method: 'get'
  })
}

/**
 * Check if certificate is locked
 * @param {Number} certificateId - Certificate ID
 */
export function getLockStatus(certificateId) {
  return request({
    url: API.CHECK_STATUS(certificateId),
    method: 'get'
  })
}

/**
 * Delete lock record
 * @param {Number} id - Certificate Lock ID
 */
export function deleteLock(id) {
  return request({
    url: API.DELETE(id),
    method: 'delete'
  })
}

// Export API constants for reference
export { API }
export default {
  getLockById,
  getLocksByCertificateId,
  getActiveLocks,
  searchLocks,
  lockCertificate,
  unlockCertificate,
  getLastLock,
  getLockStatus,
  deleteLock,
  API
}
