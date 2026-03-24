/**
 * Exam Result API Client
 * 考试成绩相关接口
 */
import request from './request'

/**
 * Create exam result
 */
export function createResult(data) {
  return request({
    url: '/api/v1/exam-results',
    method: 'post',
    data
  })
}

/**
 * Update exam result
 */
export function updateResult(examResultId, data) {
  return request({
    url: `/api/v1/exam-results/${examResultId}`,
    method: 'put',
    data
  })
}

/**
 * Delete exam result
 */
export function deleteResult(examResultId) {
  return request({
    url: `/api/v1/exam-results/${examResultId}`,
    method: 'delete'
  })
}

/**
 * Delete exam results by exam plan ID
 */
export function deleteResultsByExamPlanId(examPlanId) {
  return request({
    url: `/api/v1/exam-results/by-exam-plan/${examPlanId}`,
    method: 'delete'
  })
}

/**
 * Get exam result by ID
 */
export function getResultById(examResultId) {
  return request({
    url: `/api/v1/exam-results/${examResultId}`,
    method: 'get'
  })
}

/**
 * Get exam result by exam card ID
 */
export function getResultByExamCardId(examCardId) {
  return request({
    url: `/api/v1/exam-results/by-exam-card/${examCardId}`,
    method: 'get'
  })
}

/**
 * Get exam results by exam plan ID
 */
export function getResultsByExamPlan(examPlanId) {
  return request({
    url: `/api/v1/exam-results/by-exam-plan/${examPlanId}`,
    method: 'get'
  })
}

/**
 * Get exam results by worker ID
 */
export function getResultsByWorker(workerId) {
  return request({
    url: `/api/v1/exam-results/by-worker/${workerId}`,
    method: 'get'
  })
}

/**
 * Pass an exam result
 */
export function passResult(examResultId) {
  return request({
    url: `/api/v1/exam-results/${examResultId}/pass`,
    method: 'post'
  })
}

/**
 * Fail an exam result
 */
export function failResult(examResultId) {
  return request({
    url: `/api/v1/exam-results/${examResultId}/fail`,
    method: 'post'
  })
}

/**
 * Publish exam results for an exam plan
 */
export function publishResults(examPlanId) {
  return request({
    url: `/api/v1/exam-results/publish/${examPlanId}`,
    method: 'post'
  })
}

/**
 * Get pass rate for exam plan
 */
export function getPassRate(examPlanId) {
  return request({
    url: `/api/v1/exam-results/pass-rate/${examPlanId}`,
    method: 'get'
  })
}

/**
 * Get pass statistics for exam plan
 */
export function getStatistics(examPlanId) {
  return request({
    url: `/api/v1/exam-results/statistics/${examPlanId}`,
    method: 'get'
  })
}

/**
 * Search exam results
 */
export function searchResults(params) {
  return request({
    url: '/api/v1/exam-results/search',
    method: 'get',
    params
  })
}
