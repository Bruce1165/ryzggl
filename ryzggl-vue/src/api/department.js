import request from './request'

/**
 * Department API - 部门管理
 * Department management
 */

export const API = {
  LIST: '/api/v1/departments',
  CREATE: '/api/v1/departments',
  GET_BY_ID: (id) => `/api/v1/departments/${id}`,
  UPDATE: (id) => `/api/v1/departments/${id}`,
  DELETE: (id) => `/api/v1/departments/${id}`
}

/**
 * Get department list with pagination
 * @param {Object} params - {current, size, departmentName}
 */
export function getDepartmentList(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}

/**
 * Get department by ID
 * @param {String} id - Department ID
 */
export function getDepartmentById(id) {
  return request({
    url: API.GET_BY_ID(id),
    method: 'get'
  })
}

/**
 * Create department
 * @param {Object} data - Department entity
 */
export function createDepartment(data) {
  return request({
    url: API.CREATE,
    method: 'post',
    data
  })
}

/**
 * Update department
 * @param {String} id - Department ID
 * @param {Object} data - Department entity
 */
export function updateDepartment(id, data) {
  return request({
    url: API.UPDATE(id),
    method: 'put',
    data
  })
}

/**
 * Delete department
 * @param {String} id - Department ID
 */
export function deleteDepartment(id) {
  return request({
    url: API.DELETE(id),
    method: 'delete'
  })
}
