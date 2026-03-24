import request from '@/utils/request'

/**
 * Study Plan API
 * 学习计划API
 */
export default {
  /**
   * Get all study plans
   */
  getAll() {
    return request({
      url: '/api/v1/study-plans/all',
      method: 'get'
    })
  },

  /**
   * Get study plan by composite key
   */
  getById(workerCertificateCode, packageId) {
    return request({
      url: `/api/v1/study-plans/${workerCertificateCode}/${packageId}`,
      method: 'get'
    })
  },

  /**
   * Get study plans by worker certificate code
   */
  getByWorkerCertificateCode(workerCertificateCode) {
    return request({
      url: '/api/v1/study-plans/worker/' + workerCertificateCode,
      method: 'get'
    })
  },

  /**
   * Get study plans by package ID
   */
  getByPackageId(packageId) {
    return request({
      url: '/api/v1/study-plans/package/' + packageId,
      method: 'get'
    })
  },

  /**
   * Get study plans by status
   */
  getByStatus(status) {
    return request({
      url: '/api/v1/study-plans/status/' + encodeURIComponent(status),
      method: 'get'
    })
  },

  /**
   * Get study plans by add type
   */
  getByAddType(addType) {
    return request({
      url: '/api/v1/study-plans/add-type/' + encodeURIComponent(addType),
      method: 'get'
    })
  },

  /**
   * Create study plan
   */
  create(data) {
    return request({
      url: '/api/v1/study-plans',
      method: 'post',
      data
    })
  },

  /**
   * Update study plan
   */
  update(workerCertificateCode, packageId, data) {
    return request({
      url: `/api/v1/study-plans/${workerCertificateCode}/${packageId}`,
      method: 'put',
      data
    })
  },

  /**
   * Delete study plan
   */
  delete(workerCertificateCode, packageId) {
    return request({
      url: `/api/v1/study-plans/${workerCertificateCode}/${packageId}`,
      method: 'delete'
    })
  }
}
