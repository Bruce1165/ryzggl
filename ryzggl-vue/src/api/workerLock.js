/**
 * WorkerLock API Client
 * 人员锁定相关接口
 *
 * Maps to WorkerLockController
 * Backend endpoint: /api/v1/worker-locks
 *
 * WorkerLock entity fields:
 * - lockId (锁定记录ID)
 * - workerId (人员ID)
 * - lockType (锁定类型)
 * - lockTime (锁定时间)
 * - lockEndTime (锁定结束时间)
 * - lockPerson (锁定人)
 * - remark (备注)
 * - unlockTime (解锁时间)
 * - unlockPerson (解锁人)
 * - lockStatus (锁定状态：LOCKED/UNLOCKED)
 */

import request from './request'

/**
 * WorkerLock API endpoints
 */
const API = {
  // Base path
  BASE: '/api/v1/worker-locks',
  // Get by ID
  GET_BY_ID: (lockId) => `/api/v1/worker-locks/${lockId}`,
  // Get by worker ID
  GET_BY_WORKER: (workerId) => `/api/v1/worker-locks/worker/${workerId}`,
  // Get last lock for worker
  GET_LAST: (workerId) => `/api/v1/worker-locks/worker/${workerId}/last`,
  // Get active locks
  ACTIVE: '/api/v1/worker-locks/active',
  // List
  LIST: '/api/v1/worker-locks',
  // Search
  SEARCH: '/api/v1/worker-locks/search',
  // Lock
  LOCK: '/api/v1/worker-locks/lock',
  // Unlock
  UNLOCK: '/api/v1/worker-locks/unlock',
  // Check status
  CHECK_STATUS: (workerId) => `/api/v1/worker-locks/worker/${workerId}/status`,
  // Delete
  DELETE: (lockId) => `/api/v1/worker-locks/${lockId}`
}

/**
 * Get lock by ID
 * @param {Number} lockId - Worker Lock ID
 */
export function getLockById(lockId) {
  return request({
    url: API.GET_BY_ID(lockId),
    method: 'get'
  })
}

/**
 * Get locks by worker ID
 * @param {Number} workerId - Worker ID
 */
export function getLocksByWorkerId(workerId) {
  return request({
    url: API.GET_BY_WORKER(workerId),
    method: 'get'
  })
}

/**
 * Get last lock for a worker
 * @param {Number} workerId - Worker ID
 */
export function getLastLock(workerId) {
  return request({
    url: API.GET_LAST(workerId),
    method: 'get'
  })
}

/**
 * Get all active locks
 */
export function getActiveLocks() {
  return request({
    url: API.ACTIVE,
    method: 'get'
  })
}

/**
 * Get all locks with pagination
 * @param {Object} params - {current, size, keyword}
 */
export function getLocks(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}

/**
 * Search locks
 * @param {String} keyword - Search keyword
 */
export function searchLocks(keyword) {
  return request({
    url: API.SEARCH,
    method: 'get',
    params: { keyword }
  })
}

/**
 * Lock worker
 * @param {Object} data - {workerId, lockType, lockPerson, lockEndTime, remark}
 */
export function lockWorker(data) {
  return request({
    url: API.LOCK,
    method: 'post',
    data
  })
}

/**
 * Unlock worker
 * @param {Number} lockId - Lock ID
 * @param {String} unlockPerson - Unlock person
 */
export function unlockWorker(lockId, unlockPerson) {
  return request({
    url: API.UNLOCK,
    method: 'post',
    params: { lockId, unlockPerson }
  })
}

/**
 * Check if worker is locked
 * @param {Number} workerId - Worker ID
 */
export function checkWorkerLockStatus(workerId) {
  return request({
    url: API.CHECK_STATUS(workerId),
    method: 'get'
  })
}

/**
 * Delete lock record
 * @param {Number} lockId - Worker Lock ID
 */
export function deleteLock(lockId) {
  return request({
    url: API.DELETE(lockId),
    method: 'delete'
  })
}

// Export API constants for reference
export { API }
export default {
  getLockById,
  getLocksByWorkerId,
  getLastLock,
  getActiveLocks,
  getLocks,
  searchLocks,
  lockWorker,
  unlockWorker,
  checkWorkerLockStatus,
  deleteLock,
  API
}
