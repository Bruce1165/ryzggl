/**
 * Apply Replace API Client
 * 证书补办申请相关接口
 *
 * Maps to ApplyReplaceController
 * Backend endpoint: /api/v1/apply-replace
 *
 * ApplyReplace entity fields:
 * - applyId (主键)
 * - psnMobilePhone (人员手机号)
 * - psnEmail (人员邮箱)
 * - registerNo (注册号)
 * - registerCertificateNo (注册证书号)
 * - disnableDate (失效日期)
 * - validCode (验证码)
 * - linkMan (联系人)
 * - entTelephone (企业电话)
 * - entCorrespondence (企业地址)
 * - entPostcode (企业邮编)
 * - psnRegisteProfession1-4 (注册专业1-4)
 * - psnCertificateValidity1-4 (证书有效期1-4)
 * - replaceReason (补办原因)
 * - replaceType (补办类型)
 */

import request from './request'

/**
 * ApplyReplace API endpoints
 */
const API = {
  // Base path
  BASE: '/api/v1/apply-replace',
  // Get list
  LIST: '/api/v1/apply-replace/list',
  // Get by ID
  GET_BY_ID: (id) => `/api/v1/apply-replace/${id}`,
  // Create
  CREATE: '/api/v1/apply-replace',
  // Update
  UPDATE: (id) => `/api/v1/apply-replace/${id}`,
  // Delete
  DELETE: (id) => `/api/v1/apply-replace/${id}`,
  // Update status
  UPDATE_STATUS: (id) => `/api/v1/apply-replace/${id}/status`,
  // Get by register no
  BY_REGISTER_NO: '/api/v1/apply-replace/by-register-no',
  // Get by status
  BY_STATUS: (status) => `/api/v1/apply-replace/status/${status}`,
  // Get by replace type
  BY_REPLACE_TYPE: (type) => `/api/v1/apply-replace/type/${type}`,
  // Search
  SEARCH: '/api/v1/apply-replace/search',
  // Count
  COUNT: '/api/v1/apply-replace/count',
}

/**
 * Get ApplyReplace list with pagination
 * @param {Object} params - {current, size, status}
 */
export function getApplyReplaceList(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}

/**
 * Get ApplyReplace by ID
 * @param {String} applyId - Apply ID
 */
export function getApplyReplaceById(applyId) {
  return request({
    url: API.GET_BY_ID(applyId),
    method: 'get'
  })
}

/**
 * Get ApplyReplace by Registration Number
 * @param {String} registerNo - Registration number
 */
export function getApplyReplaceByRegisterNo(registerNo) {
  return request({
    url: API.BY_REGISTER_NO,
    method: 'get',
    params: { registerNo }
  })
}

/**
 * Create ApplyReplace
 * @param {Object} data - ApplyReplace entity data
 */
export function createApplyReplace(data) {
  return request({
    url: API.CREATE,
    method: 'post',
    data
  })
}

/**
 * Update ApplyReplace
 * @param {String} applyId - Apply ID
 * @param {Object} data - ApplyReplace entity data
 */
export function updateApplyReplace(applyId, data) {
  return request({
    url: API.UPDATE(applyId),
    method: 'put',
    data
  })
}

/**
 * Delete ApplyReplace
 * @param {String} applyId - Apply ID
 */
export function deleteApplyReplace(applyId) {
  return request({
    url: API.DELETE(applyId),
    method: 'delete'
  })
}

/**
 * Search ApplyReplace by filters
 * @param {Object} params - Search parameters (registerNo, registerCertificateNo, replaceType, status)
 */
export function searchApplyReplace(params) {
  return request({
    url: API.SEARCH,
    method: 'get',
    params
  })
}

/**
 * Get ApplyReplace by status
 * @param {String} status - Status value
 * @param {Object} params - Additional params (current, size)
 */
export function getApplyReplaceByStatus(status, params = {}) {
  return request({
    url: API.BY_STATUS(status),
    method: 'get',
    params
  })
}

/**
 * Get ApplyReplace by replace type
 * @param {String} replaceType - Replace type
 * @param {Object} params - Additional params (current, size)
 */
export function getApplyReplaceByReplaceType(replaceType, params = {}) {
  return request({
    url: API.BY_REPLACE_TYPE(replaceType),
    method: 'get',
    params
  })
}

/**
 * Update ApplyReplace status
 * @param {String} applyId - Apply ID
 * @param {String} status - New status
 */
export function updateApplyReplaceStatus(applyId, status) {
  return request({
    url: API.UPDATE_STATUS(applyId),
    method: 'put',
    data: { status }
  })
}

/**
 * Count ApplyReplace by status
 * @param {String} status - Status (optional)
 */
export function countApplyReplace(status) {
  return request({
    url: API.COUNT,
    method: 'get',
    params: { status }
  })
}

// Export API constants for reference
export { API }
export default {
  getApplyReplaceList,
  getApplyReplaceById,
  getApplyReplaceByRegisterNo,
  createApplyReplace,
  updateApplyReplace,
  deleteApplyReplace,
  searchApplyReplace,
  getApplyReplaceByStatus,
  getApplyReplaceByReplaceType,
  updateApplyReplaceStatus,
  countApplyReplace,
  API
}
