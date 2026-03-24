/**
 * Organization API Client
 * 机构管理相关接口
 *
 * Maps to OrganizationController
 * Backend endpoint: /api/v1/organizations
 *
 * Organization entity fields:
 * - organId (机构ID)
 * - orderId (排序ID)
 * - organCoding (机构编码：4位为父级，6位为子级)
 * - organType (机构类型)
 * - organNature (机构性质)
 * - organName (机构名称)
 * - organDescription (机构描述)
 * - businessProperties (业务属性)
 * - organTelphone (机构电话)
 * - organAddress (机构地址)
 * - organCode (机构编码)
 * - regionId (区ID)
 * - isVisible (是否可见)
 * - createTime (创建时间)
 * - modifyTime (修改时间)
 */

import request from './request'

/**
 * Organization API endpoints
 */
const API = {
  // Base path
  BASE: '/api/v1/organizations',
  // Get by ID
  GET_BY_ID: (id) => `/api/v1/organizations/${id}`,
  // Get by code
  GET_BY_CODE: (code) => `/api/v1/organizations/code/${code}`,
  // Get tree
  TREE: '/api/v1/organizations/tree',
  // Get list (paginated)
  LIST: '/api/v1/organizations/list',
  // Get all (for tree view)
  ALL: '/api/v1/organizations/all',
  // Get by type
  BY_TYPE: (type) => `/api/v1/organizations/type/${type}`,
  // Get by region
  BY_REGION: (regionId) => `/api/v1/organizations/region/${regionId}`,
  // Search
  SEARCH: '/api/v1/organizations/search',
  // Create
  CREATE: '/api/v1/organizations',
  // Update
  UPDATE: (id) => `/api/v1/organizations/${id}`,
  // Delete
  DELETE: (id) => `/api/v1/organizations/${id}`,
  // Update visibility
  UPDATE_VISIBILITY: (id) => `/api/v1/organizations/${id}/visibility`
}

/**
 * Get organization by ID
 * @param {Number} organId - Organization ID
 */
export function getOrganizationById(organId) {
  return request({
    url: API.GET_BY_ID(organId),
    method: 'get'
  })
}

/**
 * Get organization by code
 * @param {String} organCoding - Organization code
 */
export function getOrganizationByCode(organCoding) {
  return request({
    url: API.GET_BY_CODE(organCoding),
    method: 'get'
  })
}

/**
 * Get organization tree
 */
export function getOrganizationTree() {
  return request({
    url: API.TREE,
    method: 'get'
  })
}

/**
 * Get organization list with pagination
 * @param {Object} params - {current, size}
 */
export function getOrganizationList(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}

/**
 * Get all organizations (for tree view)
 */
export function getAllOrganizations() {
  return request({
    url: API.ALL,
    method: 'get'
  })
}

/**
 * Get organizations by type
 * @param {String} type - Organization type
 */
export function getOrganizationsByType(type) {
  return request({
    url: API.BY_TYPE(type),
    method: 'get'
  })
}

/**
 * Get organizations by region
 * @param {String} regionId - Region ID
 */
export function getOrganizationsByRegion(regionId) {
  return request({
    url: API.BY_REGION(regionId),
    method: 'get'
  })
}

/**
 * Search organizations
 * @param {Object} params - {keyword}
 */
export function searchOrganizations(params) {
  return request({
    url: API.SEARCH,
    method: 'get',
    params
  })
}

/**
 * Create organization
 * @param {Object} data - Organization entity data
 */
export function createOrganization(data) {
  return request({
    url: API.CREATE,
    method: 'post',
    data
  })
}

/**
 * Update organization
 * @param {Number} organId - Organization ID
 * @param {Object} data - Organization entity data
 */
export function updateOrganization(organId, data) {
  return request({
    url: API.UPDATE(organId),
    method: 'put',
    data
  })
}

/**
 * Delete organization
 * @param {Number} organId - Organization ID
 */
export function deleteOrganization(organId) {
  return request({
    url: API.DELETE(organId),
    method: 'delete'
  })
}

/**
 * Update organization visibility
 * @param {Number} organId - Organization ID
 * @param {Boolean} isVisible - Visibility flag
 */
export function updateOrganizationVisibility(organId, isVisible) {
  return request({
    url: API.UPDATE_VISIBILITY(organId),
    method: 'put',
    params: { isVisible }
  })
}

// Export API constants for reference
export { API }
export default {
  getOrganizationById,
  getOrganizationByCode,
  getOrganizationTree,
  getOrganizationList,
  getAllOrganizations,
  getOrganizationsByType,
  getOrganizationsByRegion,
  searchOrganizations,
  createOrganization,
  updateOrganization,
  deleteOrganization,
  updateOrganizationVisibility,
  API
}
