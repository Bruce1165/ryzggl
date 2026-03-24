import request from '@/utils/request'

/**
 * Operate Log API
 * 操作日志API
 */
export default {
  /**
   * Get all operation logs
   */
  getAll() {
    return request({
      url: '/api/v1/operate-logs/all',
      method: 'get'
    })
  },

  /**
   * Get log by ID
   */
  getById(logId) {
    return request({
      url: `/api/v1/operate-logs/${logId}`,
      method: 'get'
    })
  },

  /**
   * Get logs by person ID
   */
  getByPersonId(personId) {
    return request({
      url: '/api/v1/operate-logs/person/' + personId,
      method: 'get'
    })
  },

  /**
   * Get logs by operation name
   */
  getByOperateName(operateName) {
    return request({
      url: '/api/v1/operate-logs/operation/' + encodeURIComponent(operateName),
      method: 'get'
    })
  },

  /**
   * Get logs by date range
   */
  getByDateRange(beginDate, endDate) {
    return request({
      url: '/api/v1/operate-logs/date-range',
      method: 'get',
      params: { beginDate, endDate }
    })
  },

  /**
   * Get operation statistics
   */
  getStatistics() {
    return request({
      url: '/api/v1/operate-logs/statistics',
      method: 'get'
    })
  },

  /**
   * Search logs by keyword
   */
  search(keyword) {
    return request({
      url: '/api/v1/operate-logs/search',
      method: 'get',
      params: { keyword }
    })
  },

  /**
   * Create log
   */
  create(data) {
    return request({
      url: '/api/v1/operate-logs',
      method: 'post',
      data
    })
  },

  /**
   * Delete log
   */
  delete(logId) {
    return request({
      url: `/api/v1/operate-logs/${logId}`,
      method: 'delete'
    })
  }
}
