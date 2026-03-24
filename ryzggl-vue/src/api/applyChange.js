import request from './request'

/**
 * ApplyChange API - 变更申请管理
 * Certificate change application management with from/to transfer fields
 */

export const API = {
  LIST: '/api/v1/apply-change',
  CREATE: '/api/v1/apply-change',
  GET_BY_ID: (id) => `/api/v1/apply-change/${id}`,
  UPDATE: (id) => `/api/v1/apply-change/${id}`,
  DELETE: (id) => `/api/v1/apply-change/${id}`,
}

/**
 * Get change application list
 * @param {Object} params - {current, workerId, psnRegisterNo, changeReason, ifOutside}
 */
export function getApplyChangeList(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}

/**
 * Create change application
 * @param {Object} data - ApplyChange entity
 */
export function createApplyChange(data) {
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
export function getApplyChangeById(id) {
  return request({
    url: API.GET_BY_ID(id),
    method: 'get'
  })
}

/**
 * Update change application
 * @param {String} id - Application ID
 * @param {Object} data - ApplyChange entity
 */
export function updateApplyChange(id, data) {
  return request({
    url: API.UPDATE(id),
    method: 'put',
    data
  })
}

/**
 * Delete change application
 * @param {String} id - Application ID
 */
export function deleteApplyChange(id) {
  return request({
    url: API.DELETE(id),
    method: 'delete'
  })
}

/**
 * Get applications by status
 * @param {String} status - Application status
 */
export function getApplyChangeByStatus(status) {
  return request({
    url: API.LIST,
    method: 'get',
    params: { status }
  })
}

/**
 * Search change applications
 * @param {Object} params - {workerName, psnRegisterNo, changeReason, fromEntCity, toEntCity}
 */
export function searchApplyChange(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}
