import request from './request'

/**
 * Worker API - Worker/Personnel management
 * Maps to: .NET Worker pages
 */

// API endpoints (matching WorkerController.java)
export const API = {
  // Worker list
  LIST: '/api/worker/list',
  // Get worker by ID
  GET_BY_ID: (id) => `/api/worker/${id}`,
  // Create worker
  CREATE: '/api/worker',
  // Update worker
  UPDATE: (id) => `/api/worker/${id}`,
  // Delete worker
  DELETE: (id) => `/api/worker/${id}`,
  // Get workers by department
  GET_BY_DEPARTMENT: (deptId) => `/api/worker/department/${deptId}`,
  // Get workers by certificate status
  GET_BY_CERT_STATUS: (status) => `/api/worker/certificate/${status}`,
}

/**
 * Get worker list with pagination
 */
export function getWorkerList(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}

/**
 * Get worker by ID
 */
export function getWorkerById(id) {
  return request({
    url: API.GET_BY_ID(id),
    method: 'get'
  })
}

/**
 * Create new worker
 */
export function createWorker(data) {
  return request({
    url: API.CREATE,
    method: 'post',
    data
  })
}

/**
 * Update worker
 */
export function updateWorker(id, data) {
  return request({
    url: API.UPDATE(id),
    method: 'put',
    data
  })
}

/**
 * Delete worker
 */
export function deleteWorker(id) {
  return request({
    url: API.DELETE(id),
    method: 'delete'
  })
}

/**
 * Get workers by department
 */
export function getWorkersByDepartment(deptId) {
  return request({
    url: API.GET_BY_DEPARTMENT(deptId),
    method: 'get'
  })
}

/**
 * Get workers by certificate status
 */
export function getWorkersByCertStatus(status) {
  return request({
    url: API.GET_BY_CERT_STATUS(status),
    method: 'get'
  })
}
