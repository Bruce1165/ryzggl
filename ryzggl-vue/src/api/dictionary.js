/**
 * Dictionary API Client
 * 数据字典相关接口
 *
 * Maps to DictionaryController
 * Backend endpoint: /api/v1/dictionaries
 *
 * Dictionary entity fields:
 * - dicId (字典ID)
 * - typeId (类型ID)
 * - typeName (类型名称)
 * - orderId (排序值)
 * - dicName (字典名称)
 * - dicDesc (描述)
 * - category (分类)
 */

import request from './request'

/**
 * Dictionary API endpoints
 */
const API = {
  // Base path
  BASE: '/api/v1/dictionaries',
  // Get by ID
  GET_BY_ID: (dicId) => `/api/v1/dictionaries/${dicId}`,
  // Get all
  ALL: '/api/v1/dictionaries/all',
  // Get by type ID
  GET_BY_TYPE: (typeId) => `/api/v1/dictionaries/type/${typeId}`,
  // Get dictionary name
  GET_NAME: '/api/v1/dictionaries/name',
  // Search by type and name
  SEARCH_BY_TYPE: '/api/v1/dictionaries/search-by-type',
  // Search
  SEARCH: '/api/v1/dictionaries/search',
  // Get dropdown data
  DROPDOWN: (typeId) => `/api/v1/dictionaries/dropdown/${typeId}`,
  // Create
  CREATE: '/api/v1/dictionaries/create',
  // Update
  UPDATE: (dicId) => `/api/v1/dictionaries/${dicId}`,
  // Delete
  DELETE: (dicId) => `/api/v1/dictionaries/${dicId}`,
  // Batch delete
  BATCH_DELETE: '/api/v1/dictionaries/batch'
}

/**
 * Get dictionary by ID
 * @param {String} dicId - Dictionary ID
 */
export function getDictionaryById(dicId) {
  return request({
    url: API.GET_BY_ID(dicId),
    method: 'get'
  })
}

/**
 * Get all dictionaries
 */
export function getAllDictionaries() {
  return request({
    url: API.ALL,
    method: 'get'
  })
}

/**
 * Get dictionaries by type ID
 * @param {Number} typeId - Type ID
 */
export function getDictionariesByTypeId(typeId) {
  return request({
    url: API.GET_BY_TYPE(typeId),
    method: 'get'
  })
}

/**
 * Get dictionary name by type ID and order ID
 * @param {Number} typeId - Type ID
 * @param {Number} orderId - Order ID
 */
export function getDicName(typeId, orderId) {
  return request({
    url: API.GET_NAME,
    method: 'get',
    params: { typeId, orderId }
  })
}

/**
 * Search dictionaries by type and name
 * @param {Object} params - {typeId, dicName}
 */
export function searchDictionariesByType(params) {
  return request({
    url: API.SEARCH_BY_TYPE,
    method: 'get',
    params
  })
}

/**
 * Search dictionaries globally
 * @param {String} keyword - Search keyword
 */
export function searchDictionaries(keyword) {
  return request({
    url: API.SEARCH,
    method: 'get',
    params: { keyword }
  })
}

/**
 * Get dropdown data for a type
 * @param {Number} typeId - Type ID
 */
export function getDropdownData(typeId) {
  return request({
    url: API.DROPDOWN(typeId),
    method: 'get'
  })
}

/**
 * Create dictionary
 * @param {Object} data - Dictionary entity data
 */
export function createDictionary(data) {
  return request({
    url: API.CREATE,
    method: 'post',
    data
  })
}

/**
 * Update dictionary
 * @param {String} dicId - Dictionary ID
 * @param {Object} data - Dictionary entity data
 */
export function updateDictionary(dicId, data) {
  return request({
    url: API.UPDATE(dicId),
    method: 'put',
    data
  })
}

/**
 * Delete dictionary
 * @param {String} dicId - Dictionary ID
 */
export function deleteDictionary(dicId) {
  return request({
    url: API.DELETE(dicId),
    method: 'delete'
  })
}

/**
 * Batch delete dictionaries
 * @param {Array<String>} dicIds - Array of dictionary IDs
 */
export function batchDeleteDictionaries(dicIds) {
  return request({
    url: API.BATCH_DELETE,
    method: 'delete',
    data: dicIds
  })
}

// Export API constants for reference
export { API }
export default {
  getDictionaryById,
  getAllDictionaries,
  getDictionariesByTypeId,
  getDicName,
  searchDictionariesByType,
  searchDictionaries,
  getDropdownData,
  createDictionary,
  updateDictionary,
  deleteDictionary,
  batchDeleteDictionaries,
  API
}
