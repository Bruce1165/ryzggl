import request from './request'

/**
 * Apply API - Application workflows
 * Maps to: .NET WebForms zjs pages
 */

// API endpoints (matching ApplyController.java)
export const API = {
  // Application list
  LIST: '/api/apply/list',
  // Create application
  CREATE: '/api/apply',
  // Get application by ID
  GET_BY_ID: (id) => `/api/apply/${id}`,
  // Submit for review
  SUBMIT: (id) => `/api/apply/${id}/submit`,
  // Approve application
  APPROVE: (id) => `/api/apply/${id}/approve`,
  // Reject application
  REJECT: (id) => `/api/apply/${id}/reject`,
  // Get pending applications
  PENDING: '/api/apply/pending',
  // Get by status
  GET_BY_STATUS: (status) => `/api/apply/status/${status}`,
}

/**
 * Get application list with pagination
 * @param {Object} params - {current, workerId, unitCode, status, keyword}
 */
export function getApplyList(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}

/**
 * Create new application
 * @param {Object} data - Apply entity
 */
export function createApply(data) {
  return request({
    url: API.CREATE,
    method: 'post',
    data
  })
}

/**
 * Submit application for approval
 * @param {Number} id - Application ID
 */
export function submitApply(id) {
  return request({
    url: API.SUBMIT(id),
    method: 'put'
  })
}

/**
 * Approve application
 * @param {Number} id - Application ID
 * @param {String} checkMan - Checker name
 * @param {String} checkAdvise - Review advice
 */
export function approveApply(id, checkMan, checkAdvise) {
  return request({
    url: API.APPROVE(id),
    method: 'put',
    data: { checkMan, checkAdvise }
  })
}

/**
 * Reject application
 * @param {Number} id - Application ID
 * @param {String} checkMan - Checker name
 * @param {String} checkAdvise - Rejection reason
 */
export function rejectApply(id, checkMan, checkAdvise) {
  return request({
    url: API.REJECT(id),
    method: 'put',
    data: { checkMan, checkAdvise }
  })
}

/**
 * Get application by ID
 */
export function getApplyById(id) {
  return request({
    url: API.GET_BY_ID(id),
    method: 'get'
  })
}

/**
 * Get pending applications
 */
export function getPendingApplications() {
  return request({
    url: API.PENDING,
    method: 'get'
  })
}

/**
 * Get applications by status
 */
export function getApplicationsByStatus(status) {
  return request({
    url: API.GET_BY_STATUS(status),
    method: 'get'
  })
}
