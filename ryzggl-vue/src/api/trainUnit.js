import request from '@/utils/request'

/**
 * Train Unit API
 * 培训单位API
 */
export default {
  /**
   * Get all training units
   */
  getAll() {
    return request({
      url: '/api/v1/train-units/all',
      method: 'get'
    })
  },

  /**
   * Get training unit by ID
   */
  getById(id) {
    return request({
      url: `/api/v1/train-units/${id}`,
      method: 'get'
    })
  },

  /**
   * Get active training units
   */
  getActive() {
    return request({
      url: '/api/v1/train-units/active',
      method: 'get'
    })
  },

  /**
   * Create training unit
   */
  create(data) {
    return request({
      url: '/api/v1/train-units',
      method: 'post',
      data
    })
  },

  /**
   * Update training unit
   */
  update(id, data) {
    return request({
      url: `/api/v1/train-units/${id}`,
      method: 'put',
      data
    })
  },

  /**
   * Delete training unit
   */
  delete(id) {
    return request({
      url: `/api/v1/train-units/${id}`,
      method: 'delete'
    })
  },

  /**
   * Activate training unit
   */
  activate(id) {
    return request({
      url: `/api/v1/train-units/${id}/activate`,
      method: 'put'
    })
  },

  /**
   * Deactivate training unit
   */
  deactivate(id) {
    return request({
      url: `/api/v1/train-units/${id}/deactivate`,
      method: 'put'
    })
  }
}
