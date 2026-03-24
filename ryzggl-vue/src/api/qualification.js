/**
 * Qualification API Client
 * 资格证书管理相关接口
 *
 * Maps to QualificationController
 * Backend endpoint: /api/v1/qualifications
 *
 * Qualification entity fields:
 * - qualId (资格ID)
 * - qualCode (资格编码)
 * - qualName (资格名称)
 * - qualType (资格类型)
 * - qualLevel (资格等级)
 * - qualCategory (资格类别)
 * - zGZSBH (陕西省编码)
 * - xm (河南省编码)
 * - zJHM (福建省编码)
 * - description (描述)
 * - isValid (是否有效)
 * - issueDate (发证日期)
 * - validFromDate (有效期开始)
 * - validToDate (有效期结束)
 * - issuingAuthority (发证机构)
 * - createTime (创建时间)
 * - modifyTime (修改时间)
 */

import request from './request'

/**
 * Qualification API endpoints
 */
const API = {
  // Base path
  BASE: '/api/v1/qualifications',
  // Get by ID
  GET_BY_ID: (id) => `/api/v1/qualifications/${id}`,
  // Get by code
  GET_BY_CODE: (code) => `/api/v1/qualifications/code/${code}`,
  // Get all
  ALL: '/api/v1/qualifications/all',
  // Get by type
  BY_TYPE: (type) => `/api/v1/qualifications/type/${type}`,
  // Get by level
  BY_LEVEL: (level) => `/api/v1/qualifications/level/${level}`,
  // Get by category
  BY_CATEGORY: (category) => `/api/v1/qualifications/category/${category}`,
  // Get by province
  BY_PROVINCE: (provinceCode) => `/api/v1/qualifications/province/${provinceCode}`,
  // Search
  SEARCH: '/api/v1/qualifications/search',
  // Create
  CREATE: '/api/v1/qualifications',
  // Update
  UPDATE: (id) => `/api/v1/qualifications/${id}`,
  // Delete
  DELETE: (id) => `/api/v1/qualifications/${id}`,
  // Update validity
  UPDATE_VALIDITY: (id) => `/api/v1/qualifications/${id}/validity`,
  // Batch update validity
  BATCH_UPDATE_VALIDITY: '/api/v1/qualifications/validity/batch'
}

/**
 * Get qualification by ID
 * @param {Number} qualId - Qualification ID
 */
export function getQualificationById(qualId) {
  return request({
    url: API.GET_BY_ID(qualId),
    method: 'get'
  })
}

/**
 * Get qualification by code
 * @param {String} qualCode - Qualification code
 */
export function getQualificationByCode(qualCode) {
  return request({
    url: API.GET_BY_CODE(qualCode),
    method: 'get'
  })
}

/**
 * Get all qualifications
 */
export function getAllQualifications() {
  return request({
    url: API.ALL,
    method: 'get'
  })
}

/**
 * Get qualifications by type
 * @param {String} type - Qualification type
 */
export function getQualificationsByType(type) {
  return request({
    url: API.BY_TYPE(type),
    method: 'get'
  })
}

/**
 * Get qualifications by level
 * @param {String} level - Qualification level
 */
export function getQualificationsByLevel(level) {
  return request({
    url: API.BY_LEVEL(level),
    method: 'get'
  })
}

/**
 * Get qualifications by category
 * @param {String} category - Qualification category
 */
export function getQualificationsByCategory(category) {
  return request({
    url: API.BY_CATEGORY(category),
    method: 'get'
  })
}

/**
 * Get qualifications by province
 * @param {String} provinceCode - Province code
 */
export function getQualificationsByProvince(provinceCode) {
  return request({
    url: API.BY_PROVINCE(provinceCode),
    method: 'get'
  })
}

/**
 * Search qualifications
 * @param {Object} params - {keyword}
 */
export function searchQualifications(params) {
  return request({
    url: API.SEARCH,
    method: 'get',
    params
  })
}

/**
 * Create qualification
 * @param {Object} data - Qualification entity data
 */
export function createQualification(data) {
  return request({
    url: API.CREATE,
    method: 'post',
    data
  })
}

/**
 * Update qualification
 * @param {Number} qualId - Qualification ID
 * @param {Object} data - Qualification entity data
 */
export function updateQualification(qualId, data) {
  return request({
    url: API.UPDATE(qualId),
    method: 'put',
    data
  })
}

/**
 * Delete qualification
 * @param {Number} qualId - Qualification ID
 */
export function deleteQualification(qualId) {
  return request({
    url: API.DELETE(qualId),
    method: 'delete'
  })
}

/**
 * Update validity
 * @param {Number} qualId - Qualification ID
 * @param {Boolean} isValid - Validity flag
 */
export function updateQualificationValidity(qualId, isValid) {
  return request({
    url: API.UPDATE_VALIDITY(qualId),
    method: 'put',
    params: { isValid }
  })
}

/**
 * Batch update validity
 * @param {Array} ids - Qualification IDs
 * @param {Boolean} isValid - Validity flag
 */
export function batchUpdateQualificationValidity(ids, isValid) {
  return request({
    url: API.BATCH_UPDATE_VALIDITY,
    method: 'put',
    data: { ids },
    params: { isValid }
  })
}

// Export API constants for reference
export { API }
export default {
  getQualificationById,
  getQualificationByCode,
  getAllQualifications,
  getQualificationsByType,
  getQualificationsByLevel,
  getQualificationsByCategory,
  getQualificationsByProvince,
  searchQualifications,
  createQualification,
  updateQualification,
  deleteQualification,
  updateQualificationValidity,
  batchUpdateQualificationValidity,
  API
}
