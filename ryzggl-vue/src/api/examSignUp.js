/**
 * Exam Sign Up API Client
 * 考试报名相关接口
 */
import request from './request'

/**
 * Create exam sign up
 */
export function createSignUp(data) {
  return request({
    url: '/api/v1/exam-signups',
    method: 'post',
    data
  })
}

/**
 * Update exam sign up
 */
export function updateSignUp(examSignUpId, data) {
  return request({
    url: `/api/v1/exam-signups/${examSignUpId}`,
    method: 'put',
    data
  })
}

/**
 * Delete exam sign up
 */
export function deleteSignUp(examSignUpId) {
  return request({
    url: `/api/v1/exam-signups/${examSignUpId}`,
    method: 'delete'
  })
}

/**
 * Get exam sign up by ID
 */
export function getSignUpById(examSignUpId) {
  return request({
    url: `/api/v1/exam-signups/${examSignUpId}`,
    method: 'get'
  })
}

/**
 * Get exam signups by exam plan ID
 */
export function getSignUpsByExamPlan(examPlanId) {
  return request({
    url: `/api/v1/exam-signups/by-exam-plan/${examPlanId}`,
    method: 'get'
  })
}

/**
 * Get exam signups by worker ID
 */
export function getSignUpsByWorker(workerId) {
  return request({
    url: `/api/v1/exam-signups/by-worker/${workerId}`,
    method: 'get'
  })
}

/**
 * Approve exam sign up
 */
export function approveSignUp(examSignUpId, checkMan, checkResult) {
  return request({
    url: `/api/v1/exam-signups/${examSignUpId}/approve`,
    method: 'post',
    params: { checkMan, checkResult }
  })
}

/**
 * Confirm payment for exam sign up
 */
export function confirmPayment(examSignUpId, payConfirmMan, payConfirmRult) {
  return request({
    url: `/api/v1/exam-signups/${examSignUpId}/confirm-payment`,
    method: 'post',
    params: { payConfirmMan, payConfirmRult }
  })
}

/**
 * Get pending check exam signups
 */
export function getPendingChecks() {
  return request({
    url: '/api/v1/exam-signups/pending-check',
    method: 'get'
  })
}

/**
 * Search exam signups
 */
export function searchSignUps(params) {
  return request({
    url: '/api/v1/exam-signups/search',
    method: 'get',
    params
  })
}

/**
 * Count exam signups by exam plan ID
 */
export function countByExamPlanId(examPlanId) {
  return request({
    url: `/api/v1/exam-signups/count/${examPlanId}`,
    method: 'get'
  })
}
