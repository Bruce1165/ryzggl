import request from '@/utils/request'

/**
 * Learn Record API
 * 培训学习记录API
 */
export default {
  /**
   * Get all learning records
   */
  getAll() {
    return request({
      url: '/api/v1/learn-records/all',
      method: 'get'
    })
  },

  /**
   * Get learning record by ID
   */
  getById(id) {
    return request({
      url: `/api/v1/learn-records/${id}`,
      method: 'get'
    })
  },

  /**
   * Get learning records by worker code
   */
  getByWorkerCode(workerCode) {
    return request({
      url: '/api/v1/learn-records/worker/' + workerCode,
      method: 'get'
    })
  },

  /**
   * Get learning records by study package ID
   */
  getByPackageId(packageId) {
    return request({
      url: '/api/v1/learn-records/package/' + packageId,
      method: 'get'
    })
  },

  /**
   * Get learning records by completion status
   */
  getByStatus(isCompleted) {
    return request({
      url: '/api/v1/learn-records/status/' + isCompleted,
      method: 'get'
    })
  },

  /**
   * Create learning record
   */
  create(data) {
    return request({
      url: '/api/v1/learn-records',
      method: 'post',
      data
    })
  },

  /**
   * Update learning record
   */
  update(id, data) {
    return request({
      url: `/api/v1/learn-records/${id}`,
      method: 'put',
      data
    })
  },

  /**
   * Delete learning record
   */
  delete(id) {
    return request({
      url: `/api/v1/learn-records/${id}`,
      method: 'delete'
    })
  }
}
