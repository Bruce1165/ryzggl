import request from './request'

/**
 * Migration Dashboard API
 * Provides project migration status and metrics
 */

/**
 * Get overall migration progress
 */
export function getOverallProgress() {
  return request({
    url: '/api/v1/migration-dashboard/progress',
    method: 'get'
  })
}

/**
 * Get entity migration status
 */
export function getEntityStatus() {
  return request({
    url: '/api/v1/migration-dashboard/entities',
    method: 'get'
  })
}

/**
 * Get known issues and blockers
 */
export function getIssues() {
  return request({
    url: '/api/v1/migration-dashboard/issues',
    method: 'get'
  })
}

/**
 * Get testing and quality metrics
 */
export function getQualityMetrics() {
  return request({
    url: '/api/v1/migration-dashboard/quality',
    method: 'get'
  })
}

/**
 * Get recent activities
 */
export function getRecentActivities() {
  return request({
    url: '/api/v1/migration-dashboard/activities',
    method: 'get'
  })
}

/**
 * Get migration phases status
 */
export function getPhases() {
  return request({
    url: '/api/v1/migration-dashboard/phases',
    method: 'get'
  })
}