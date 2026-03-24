import request from './request'

/**
 * ApplyCancel API - 注销申请管理
 * Certificate cancellation application management
 */

export const API = {
  LIST: '/api/v1/apply-cancel',
  CREATE: '/api/v1/apply-cancel',
  GET_BY_ID: (id) => `/api/v1/apply-cancel/${id}`,
  UPDATE: (id) => `/api/v1/apply-cancel/${id}`,
  DELETE: (id) => `/api/v1/apply-cancel/${id}`,
}

/**
 * Get cancellation application list
 * @param {Object} params - {current, workerId, psnRegisterNo, status}
 */
export function getApplyCancelList(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}

/**
 * Create cancellation application
 * @param {Object} data - ApplyCancel entity
 */
export function createApplyCancel(data) {
  return request({
    url: API.CREATE,
    method: 'post',
    data
  })
}

/**
 * Get application by ID
 * @param {String} id - Application ID
 */
export function getApplyCancelById(id) {
  return request({
    url: API.GET_BY_ID(id),
    method: 'get'
  })
}

/**
 * Update cancellation application
 * @param {String} id - Application ID
 * @param {Object} data - ApplyCancel entity
 */
export function updateApplyCancel(id, data) {
  return request({
    url: API.UPDATE(id),
    method: 'put',
    data
  })
}

/**
 * Delete cancellation application
 * @param {String} id - Application ID
 */
export function deleteApplyCancel(id) {
  return request({
    url: API.DELETE(id),
    method: 'delete'
  })
}

/**
 * Get applications by status
 * @param {String} status - Application status
 */
export function getApplyCancelByStatus(status) {
  return request({
    url: API.LIST,
    method: 'get',
    params: { status }
  })
}

/**
 * Search cancellation applications
 * @param {Object} params - {workerName, psnRegisterNo, cancelReason, linkMan}
 */
export function searchApplyCancel(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}
