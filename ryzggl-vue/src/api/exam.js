import request from './request'

/**
 * Exam API - Examination management
 * Maps to: .NET Exam pages
 */

// API endpoints (matching ExamController.java)
export const API = {
  // Exam list
  LIST: '/api/exam/list',
  // Get exam by ID
  GET_BY_ID: (id) => `/api/exam/${id}`,
  // Create exam
  CREATE: '/api/exam',
  // Update exam
  UPDATE: (id) => `/api/exam/${id}`,
  // Delete exam
  DELETE: (id) => `/api/exam/${id}`,
  // Exam sign up
  SIGN_UP: (examId, workerId) => `/api/exam/${examId}/signup/${workerId}`,
  // Exam result
  RESULT: (examId, workerId) => `/api/exam/${examId}/result/${workerId}`,
  // Exam place list
  PLACE_LIST: '/api/exam/place/list',
  // Exam plan list
  PLAN_LIST: '/api/exam/plan/list',
}

/**
 * Get exam list with pagination
 */
export function getExamList(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}

/**
 * Get exam by ID
 */
export function getExamById(id) {
  return request({
    url: API.GET_BY_ID(id),
    method: 'get'
  })
}

/**
 * Create new exam
 */
export function createExam(data) {
  return request({
    url: API.CREATE,
    method: 'post',
    data
  })
}

/**
 * Update exam
 */
export function updateExam(id, data) {
  return request({
    url: API.UPDATE(id),
    method: 'put',
    data
  })
}

/**
 * Delete exam
 */
export function deleteExam(id) {
  return request({
    url: API.DELETE(id),
    method: 'delete'
  })
}

/**
 * Sign up for exam
 */
export function signUpExam(examId, workerId, data) {
  return request({
    url: API.SIGN_UP(examId, workerId),
    method: 'post',
    data
  })
}

/**
 * Get exam result
 */
export function getExamResult(examId, workerId) {
  return request({
    url: API.RESULT(examId, workerId),
    method: 'get'
  })
}

/**
 * Get exam place list
 */
export function getExamPlaceList(params) {
  return request({
    url: API.PLACE_LIST,
    method: 'get',
    params
  })
}

/**
 * Get exam plan list
 */
export function getExamPlanList(params) {
  return request({
    url: API.PLAN_LIST,
    method: 'get',
    params
  })
}
