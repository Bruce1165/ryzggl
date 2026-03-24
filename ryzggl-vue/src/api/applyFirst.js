import request from './request'

/**
 * ApplyFirst API - 首次注册申请管理
 * Initial registration application management
 */

export const API = {
  LIST: '/api/v1/apply-first',
  CREATE: '/api/v1/apply-first',
  GET_BY_ID: (id) => `/api/v1/apply-first/${id}`,
  UPDATE: (id) => `/api/v1/apply-first/${id}`,
  DELETE: (id) => `/api/v1/apply-first/${id}`,
}

/**
 * Get initial registration application list
 * @param {Object} params - {current, workerId, school, major, status}
 */
export function getApplyFirstList(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}

/**
 * Create initial registration application
 * @param {Object} data - ApplyFirst entity
 */
export function createApplyFirst(data) {
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
export function getApplyFirstById(id) {
  return request({
    url: API.GET_BY_ID(id),
    method: 'get'
  })
}

/**
 * Update initial registration application
 * @param {String} id - Application ID
 * @param {Object} data - ApplyFirst entity
 */
export function updateApplyFirst(id, data) {
  return request({
    url: API.UPDATE(id),
    method: 'put',
    data
  })
}

/**
 * Delete initial registration application
 * @param {String} id - Application ID
 */
export function deleteApplyFirst(id) {
  return request({
    url: API.DELETE(id),
    method: 'delete'
  })
}

/**
 * Get applications by status
 * @param {String} status - Application status
 */
export function getApplyFirstByStatus(status) {
  return request({
    url: API.LIST,
    method: 'get',
    params: { status }
  })
}

/**
 * Search initial registration applications
 * @param {Object} params - {workerName, idCard, school, major, nation}
 */
export function searchApplyFirst(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}
