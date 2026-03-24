/**
 * ExamPlan API Client
 * 考试计划管理相关接口
 *
 * Maps to ExamPlanController
 * Backend endpoint: /api/v1/exam-plans
 *
 * ExamPlan entity fields:
 * - examPlanId (考试计划ID)
 * - examPlanName (考试计划名称)
 * - postTypeId (岗位类型ID)
 * - examDate (考试日期)
 * - signUpStartDate (报名开始日期)
 * - signUpEndDate (报名结束日期)
 * - examPlaceId (考点ID)
 * - examCapacity (考试容量)
 * - maxSignUpCount (最大报名人数)
 * - description (说明)
 * - status (状态：草稿、已发布、已关闭)
 * - examWay (考试方式：机考、网考)
 * - personLimit (报名人数限制)
 * - planSkillLevel (技术等级)
 * - ifPublish (是否公开)
 * - examFee (考试费用)
 * - startCheckDate (审核开始时间)
 * - latestCheckDate (审核截止时间)
 * - examCardSendStartDate (发证开始日期)
 * - examCardSendEndDate (发证结束日期)
 * - latestPayDate (缴费截止日期)
 * - createPersonId (创建人)
 * - createTime (创建时间)
 * - modifyPersonId (修改人)
 * - modifyTime (修改时间)
 */

import request from './request'

/**
 * ExamPlan API endpoints
 */
const API = {
  // Base path
  BASE: '/api/v1/exam-plans',
  // Get list
  LIST: '/api/v1/exam-plans/list',
  // Get by ID
  GET_BY_ID: (id) => `/api/v1/exam-plans/${id}`,
  // Create
  CREATE: '/api/v1/exam-plans',
  // Update
  UPDATE: (id) => `/api/v1/exam-plans/${id}`,
  // Delete
  DELETE: (id) => `/api/v1/exam-plans/${id}`,
  // Get by name
  GET_BY_NAME: (name) => `/api/v1/exam-plans/name/${name}`,
  // Search
  SEARCH: '/api/v1/exam-plans/search',
  // Get published
  PUBLISHED: '/api/v1/exam-plans/published',
  // Get active
  ACTIVE: '/api/v1/exam-plans/active',
  // Update status
  UPDATE_STATUS: (id) => `/api/v1/exam-plans/${id}/status`,
  // Count
  COUNT: '/api/v1/exam-plans/count'
}

/**
 * Get ExamPlan list with pagination
 * @param {Object} params - {current, size, status}
 */
export function getExamPlanList(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}

/**
 * Get ExamPlan by ID
 * @param {Number} examPlanId - Exam Plan ID
 */
export function getExamPlanById(examPlanId) {
  return request({
    url: API.GET_BY_ID(examPlanId),
    method: 'get'
  })
}

/**
 * Get ExamPlan by name
 * @param {String} examPlanName - Exam Plan name
 */
export function getExamPlanByName(examPlanName) {
  return request({
    url: API.GET_BY_NAME(examPlanName),
    method: 'get'
  })
}

/**
 * Create ExamPlan
 * @param {Object} data - ExamPlan entity data
 */
export function createExamPlan(data) {
  return request({
    url: API.CREATE,
    method: 'post',
    data
  })
}

/**
 * Update ExamPlan
 * @param {Number} examPlanId - Exam Plan ID
 * @param {Object} data - ExamPlan entity data
 */
export function updateExamPlan(examPlanId, data) {
  return request({
    url: API.UPDATE(examPlanId),
    method: 'put',
    data
  })
}

/**
 * Delete ExamPlan
 * @param {Number} examPlanId - Exam Plan ID
 */
export function deleteExamPlan(examPlanId) {
  return request({
    url: API.DELETE(examPlanId),
    method: 'delete'
  })
}

/**
 * Search ExamPlans
 * @param {Object} params - Search parameters (examPlanName, status)
 */
export function searchExamPlans(params) {
  return request({
    url: API.SEARCH,
    method: 'get',
    params
  })
}

/**
 * Get published exam plans
 * @param {Object} params - Additional params (current, size)
 */
export function getPublishedExamPlans(params = {}) {
  return request({
    url: API.PUBLISHED,
    method: 'get',
    params
  })
}

/**
 * Get active exam plans (available for sign-up)
 * @param {Object} params - Additional params (current, size)
 */
export function getActiveExamPlans(params = {}) {
  return request({
    url: API.ACTIVE,
    method: 'get',
    params
  })
}

/**
 * Update ExamPlan status
 * @param {Number} examPlanId - Exam Plan ID
 * @param {String} status - New status (DRAFT, PUBLISHED, CLOSED)
 */
export function updateExamPlanStatus(examPlanId, status) {
  return request({
    url: API.UPDATE_STATUS(examPlanId),
    method: 'put',
    data: { status }
  })
}

/**
 * Count ExamPlan by status
 * @param {String} status - Exam Plan status (optional, default: all)
 */
export function countExamPlan(status) {
  return request({
    url: API.COUNT,
    method: 'get',
    params: { status }
  })
}

// Export API constants for reference
export { API }
export default {
  getExamPlanList,
  getExamPlanById,
  getExamPlanByName,
  createExamPlan,
  updateExamPlan,
  deleteExamPlan,
  searchExamPlans,
  getPublishedExamPlans,
  getActiveExamPlans,
  updateExamPlanStatus,
  countExamPlan,
  API
}
