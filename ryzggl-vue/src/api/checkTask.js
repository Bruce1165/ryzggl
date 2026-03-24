import request from './request'

/**
 * Check Task API - 业务申请单抽任务管理
 * Multi-step approval workflow for checking applications
 * Maps to: ApplyCheckTaskController.java and ApplyCheckTaskItemController.java
 */

// API endpoints (matching ApplyCheckTaskController.java)
export const API = {
  // Task management
  LIST: '/api/v1/check-tasks',
  CREATE: '/api/v1/check-tasks',
  GET_BY_ID: (id) => `/api/v1/check-tasks/${id}`,
  UPDATE: (id) => `/api/v1/check-tasks/${id}`,
  DELETE: (id) => `/api/v1/check-tasks/${id}`,

  // Task item management
  GET_ITEMS: (taskId) => `/api/v1/check-tasks/${taskId}/items`,
  APPROVE_ITEM: (itemId) => `/api/v1/check-task-items/${itemId}/approve`,
  REJECT_ITEM: (itemId) => `/api/v1/check-task-items/${itemId}/reject`,
  BATCH_APPROVE: '/api/v1/check-tasks/batch-approve',

  // Task workflow
  GENERATE_ITEMS: (id) => `/api/v1/check-tasks/${id}/generate-items`,
  GET_PROGRESS: (id) => `/api/v1/check-tasks/${id}/progress`,
  GET_CHECKED_COUNT: (id) => `/api/v1/check-tasks/${id}/checked-count`,
}

/**
 * Get check task list with pagination
 * @param {Object} params - {current, year, month, status, keyword}
 */
export function getCheckTaskList(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}

/**
 * Create new check task
 * @param {Object} data - ApplyCheckTask entity
 */
export function createCheckTask(data) {
  return request({
    url: API.CREATE,
    method: 'post',
    data
  })
}

/**
 * Get check task by ID with items loaded
 * @param {Number} id - Task ID
 */
export function getCheckTaskById(id) {
  return request({
    url: API.GET_BY_ID(id),
    method: 'get'
  })
}

/**
 * Update check task
 * @param {Number} id - Task ID
 * @param {Object} data - ApplyCheckTask entity
 */
export function updateCheckTask(id, data) {
  return request({
    url: API.UPDATE(id),
    method: 'put',
    data
  })
}

/**
 * Delete check task (cascade deletes items)
 * @param {Number} id - Task ID
 */
export function deleteCheckTask(id) {
  return request({
    url: API.DELETE(id),
    method: 'delete'
  })
}

/**
 * Generate check items (sample applications)
 * @param {Number} id - Task ID
 */
export function generateCheckItems(id) {
  return request({
    url: API.GENERATE_ITEMS(id),
    method: 'post'
  })
}

/**
 * Get task progress statistics
 * @param {Number} id - Task ID
 */
export function getTaskProgress(id) {
  return request({
    url: API.GET_PROGRESS(id),
    method: 'get'
  })
}

/**
 * Get checked count for task
 * @param {Number} id - Task ID
 */
export function getCheckedCount(id) {
  return request({
    url: API.GET_CHECKED_COUNT(id),
    method: 'get'
  })
}

/**
 * Get task items by task ID
 * @param {Number} taskId - Task ID
 * @param {Object} params - {status, checked}
 */
export function getTaskItems(taskId, params = {}) {
  return request({
    url: API.GET_ITEMS(taskId),
    method: 'get',
    params
  })
}

/**
 * Approve single task item
 * @param {Number} itemId - Task Item ID
 * @param {String} checkMan - Approver name
 * @param {String} checkResult - Check result (通过/不通过)
 * @param {String} checkResultDesc - Check result description
 */
export function approveTaskItem(itemId, checkMan, checkResult, checkResultDesc) {
  return request({
    url: API.APPROVE_ITEM(itemId),
    method: 'post',
    data: { checkMan, checkResult, checkResultDesc }
  })
}

/**
 * Reject single task item
 * @param {Number} itemId - Task Item ID
 * @param {String} checkMan - Approver name
 * @param {String} checkResultDesc - Rejection reason
 */
export function rejectTaskItem(itemId, checkMan, checkResultDesc) {
  return request({
    url: API.REJECT_ITEM(itemId),
    method: 'post',
    data: {
      checkMan,
      checkResult: '不通过',
      checkResultDesc
    }
  })
}

/**
 * Batch approve task items
 * @param {String} checkMan - Approver name
 * @param {String} checkResult - Check result (通过/不通过)
 * @param {String} checkResultDesc - Check result description
 * @param {Number} taskItemId - Task Item ID (optional, for single item)
 * @param {Number} taskId - Task ID (optional, for all items in task)
 */
export function batchApproveItems(checkMan, checkResult, checkResultDesc, taskItemId = null, taskId = null) {
  return request({
    url: API.BATCH_APPROVE,
    method: 'post',
    data: {
      checkMan,
      checkResult,
      checkResultDesc,
      taskItemId,
      taskId
    }
  })
}
