import request from './request'

/**
 * Certificate API - Certificate management
 * Maps to: .NET WebForms CertifManage pages
 */

// API endpoints (matching CertificateController.java)
export const API = {
  // Certificate list
  LIST: '/api/certificate/list',
  // Get certificate by ID
  GET_BY_ID: (id) => `/api/certificate/${id}`,
  // Create certificate
  CREATE: '/api/certificate',
  // Update certificate
  UPDATE: (id) => `/api/certificate/${id}`,
  // Delete certificate
  DELETE: (id) => `/api/certificate/${id}`,
  // Continue certificate
  CONTINUE: (id) => `/api/certificate/${id}/continue`,
  // Pause certificate
  PAUSE: (id) => `/api/certificate/${id}/pause`,
  // Lock certificate
  LOCK: (id) => `/api/certificate/${id}/lock`,
  // Unlock certificate
  UNLOCK: (id) => `/api/certificate/${id}/unlock`,
  // Merge certificates
  MERGE: '/api/certificate/merge',
  // Change certificate
  CHANGE: (id) => `/api/certificate/${id}/change`,
}

/**
 * Get certificate list with pagination
 * @param {Object} params - {current, workerId, certificateNo, status}
 */
export function getCertificateList(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}

/**
 * Get certificate by ID
 */
export function getCertificateById(id) {
  return request({
    url: API.GET_BY_ID(id),
    method: 'get'
  })
}

/**
 * Create new certificate
 */
export function createCertificate(data) {
  return request({
    url: API.CREATE,
    method: 'post',
    data
  })
}

/**
 * Update certificate
 */
export function updateCertificate(id, data) {
  return request({
    url: API.UPDATE(id),
    method: 'put',
    data
  })
}

/**
 * Delete certificate
 */
export function deleteCertificate(id) {
  return request({
    url: API.DELETE(id),
    method: 'delete'
  })
}

/**
 * Continue certificate
 */
export function continueCertificate(id, data) {
  return request({
    url: API.CONTINUE(id),
    method: 'put',
    data
  })
}

/**
 * Pause certificate
 */
export function pauseCertificate(id, reason) {
  return request({
    url: API.PAUSE(id),
    method: 'put',
    data: { reason }
  })
}

/**
 * Lock certificate
 */
export function lockCertificate(id, reason) {
  return request({
    url: API.LOCK(id),
    method: 'put',
    data: { reason }
  })
}

/**
 * Unlock certificate
 */
export function unlockCertificate(id) {
  return request({
    url: API.UNLOCK(id),
    method: 'put'
  })
}

/**
 * Merge certificates
 */
export function mergeCertificates(data) {
  return request({
    url: API.MERGE,
    method: 'post',
    data
  })
}

/**
 * Change certificate
 */
export function changeCertificate(id, data) {
  return request({
    url: API.CHANGE(id),
    method: 'put',
    data
  })
}
