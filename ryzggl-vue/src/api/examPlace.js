/**
 * Exam Place API Client
 * 考点管理相关接口
 *
 * Maps to ExamPlaceController
 * Backend endpoint: /api/v1/exam-places
 *
 * ExamPlace entity fields:
 * - examPlaceId (考点ID)
 * - examPlaceName (考点名称)
 * - examPlaceAddress (考点地址)
 * - linkMan (联系人)
 * - phone (联系电话)
 * - roomNum (房间数量)
 * - examPersonNum (考试人数/容量)
 * - status (状态)
 */

import request from './request'

/**
 * ExamPlace API endpoints
 */
const API = {
  // Base path
  BASE: '/api/v1/exam-places',
  // Get list
  LIST: '/api/v1/exam-places/list',
  // Get by ID
  GET_BY_ID: (id) => `/api/v1/exam-places/${id}`,
  // Create
  CREATE: '/api/v1/exam-places',
  // Update
  UPDATE: (id) => `/api/v1/exam-places/${id}`,
  // Delete
  DELETE: (id) => `/api/v1/exam-places/${id}`,
  // Update status
  UPDATE_STATUS: (id) => `/api/v1/exam-places/${id}/status`,
  // Get by name
  BY_NAME: (name) => `/api/v1/exam-places/name/${name}`,
  // Get active
  ACTIVE: '/api/v1/exam-places/active',
  // Get available
  AVAILABLE: '/api/v1/exam-places/available',
  // Search
  SEARCH: '/api/v1/exam-places/search',
  // Count
  COUNT: '/api/v1/exam-places/count',
}

/**
 * Get ExamPlace list with pagination
 * @param {Object} params - {current, size, status}
 */
export function getExamPlaceList(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}

/**
 * Get ExamPlace by ID
 * @param {Number} examPlaceId - ExamPlace ID
 */
export function getExamPlaceById(examPlaceId) {
  return request({
    url: API.GET_BY_ID(examPlaceId),
    method: 'get'
  })
}

/**
 * Get ExamPlace by Name
 * @param {String} examPlaceName - ExamPlace name
 */
export function getExamPlaceByName(examPlaceName) {
  return request({
    url: API.BY_NAME(examPlaceName),
    method: 'get'
  })
}

/**
 * Create ExamPlace
 * @param {Object} data - ExamPlace entity data
 */
export function createExamPlace(data) {
  return request({
    url: API.CREATE,
    method: 'post',
    data
  })
}

/**
 * Update ExamPlace
 * @param {Number} examPlaceId - ExamPlace ID
 * @param {Object} data - ExamPlace entity data
 */
export function updateExamPlace(examPlaceId, data) {
  return request({
    url: API.UPDATE(examPlaceId),
    method: 'put',
    data
  })
}

/**
 * Delete ExamPlace
 * @param {Number} examPlaceId - ExamPlace ID
 */
export function deleteExamPlace(examPlaceId) {
  return request({
    url: API.DELETE(examPlaceId),
    method: 'delete'
  })
}

/**
 * Search exam places
 * @param {Object} params - Search parameters (examPlaceName, status)
 */
export function searchExamPlaces(params) {
  return request({
    url: API.SEARCH,
    method: 'get',
    params
  })
}

/**
 * Get active exam places
 * @param {Object} params - Additional params (current, size)
 */
export function getActiveExamPlaces(params = {}) {
  return request({
    url: API.ACTIVE,
    method: 'get',
    params
  })
}

/**
 * Get available exam places with capacity
 * @param {Object} params - Parameters (requiredCapacity, status)
 */
export function getAvailableExamPlaces(params = {}) {
  return request({
    url: API.AVAILABLE,
    method: 'get',
    params
  })
}

/**
 * Update ExamPlace status
 * @param {Number} examPlaceId - ExamPlace ID
 * @param {String} status - New status
 */
export function updateExamPlaceStatus(examPlaceId, status) {
  return request({
    url: API.UPDATE_STATUS(examPlaceId),
    method: 'put',
    data: { status }
  })
}

/**
 * Count ExamPlace by status
 * @param {String} status - Status (optional)
 */
export function countExamPlace(status) {
  return request({
    url: API.COUNT,
    method: 'get',
    params: { status }
  })
}

// Export API constants for reference
export { API }
export default {
  getExamPlaceList,
  getExamPlaceById,
  getExamPlaceByName,
  createExamPlace,
  updateExamPlace,
  deleteExamPlace,
  searchExamPlaces,
  getActiveExamPlaces,
  getAvailableExamPlaces,
  updateExamPlaceStatus,
  countExamPlace,
  API
}
