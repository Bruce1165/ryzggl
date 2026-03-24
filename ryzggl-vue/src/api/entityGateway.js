/**
 * Dynamic API Gateway
 *
 * Generic API gateway implementing RESTful conventions for all entity types.
 * Provides a unified interface for CRUD operations while supporting custom endpoints.
 *
 * Implements the Repository pattern with dynamic endpoint resolution based on
 * entity configurations from the entity registry.
 */

import request from './request'

/**
 * API Gateway class for dynamic entity operations
 */
class EntityGateway {
  constructor() {
    this.cache = new Map()
  }

  /**
   * Generate API endpoint URL based on entity type and operation
   * @param {string} entityType - Entity type identifier
   * @param {string} operation - Operation type (list, get, create, update, delete)
   * @param {number|string} [id] - Entity ID for specific operations
   * @returns {string} Complete API endpoint URL
   * @private
   */
  _buildEndpoint(entityType, operation, id = null) {
    try {
      // Dynamically import entity registry to avoid circular dependencies
      const { getEntityConfig } = require('../utils/entityRegistry')
      const config = getEntityConfig(entityType)
      const basePath = config.apiPath

      switch (operation) {
        case 'list':
          return `${basePath}/list`
        case 'get':
          return `${basePath}/${id}`
        case 'create':
          return basePath
        case 'update':
          return `${basePath}/${id}`
        case 'delete':
          return `${basePath}/${id}`
        default:
          throw new Error(`Unknown operation: ${operation}`)
      }
    } catch (error) {
      throw new Error(`Failed to build endpoint for ${entityType} ${operation}: ${error.message}`)
    }
  }

  /**
   * Get entity list with pagination and filtering
   * @param {string} entityType - Entity type identifier
   * @param {Object} params - Query parameters (page, size, filters, search)
   * @returns {Promise<Object>} API response with data and pagination
   */
  async getList(entityType, params = {}) {
    try {
      const endpoint = this._buildEndpoint(entityType, 'list')

      // Normalize pagination parameters
      const queryParams = {
        page: params.page || 1,
        size: params.size || 10,
        ...params
      }

      const response = await request({
        url: endpoint,
        method: 'get',
        params: queryParams
      })

      // Normalize response structure
      return this._normalizeResponse(response)
    } catch (error) {
      this._handleError('getList', entityType, error)
      throw error
    }
  }

  /**
   * Get single entity by ID
   * @param {string} entityType - Entity type identifier
   * @param {number|string} id - Entity ID
   * @returns {Promise<Object>} Entity data
   */
  async getById(entityType, id) {
    try {
      const endpoint = this._buildEndpoint(entityType, 'get', id)

      const response = await request({
        url: endpoint,
        method: 'get'
      })

      return this._normalizeResponse(response)
    } catch (error) {
      this._handleError('getById', entityType, error)
      throw error
    }
  }

  /**
   * Create new entity
   * @param {string} entityType - Entity type identifier
   * @param {Object} data - Entity data
   * @returns {Promise<Object>} Created entity data
   */
  async create(entityType, data) {
    try {
      const endpoint = this._buildEndpoint(entityType, 'create')

      // Remove id field if present (server should generate it)
      const { id, ...createData } = data

      const response = await request({
        url: endpoint,
        method: 'post',
        data: createData
      })

      // Clear cache for this entity type
      this._clearCache(entityType)

      return this._normalizeResponse(response)
    } catch (error) {
      this._handleError('create', entityType, error)
      throw error
    }
  }

  /**
   * Update existing entity
   * @param {string} entityType - Entity type identifier
   * @param {number|string} id - Entity ID
   * @param {Object} data - Updated entity data
   * @returns {Promise<Object>} Updated entity data
   */
  async update(entityType, id, data) {
    try {
      const endpoint = this._buildEndpoint(entityType, 'update', id)

      // Ensure id is in the data
      const updateData = { ...data, id }

      const response = await request({
        url: endpoint,
        method: 'put',
        data: updateData
      })

      // Clear cache for this entity type
      this._clearCache(entityType)

      return this._normalizeResponse(response)
    } catch (error) {
      this._handleError('update', entityType, error)
      throw error
    }
  }

  /**
   * Delete entity
   * @param {string} entityType - Entity type identifier
   * @param {number|string} id - Entity ID
   * @returns {Promise<Object>} Deletion response
   */
  async delete(entityType, id) {
    try {
      const endpoint = this._buildEndpoint(entityType, 'delete', id)

      const response = await request({
        url: endpoint,
        method: 'delete'
      })

      // Clear cache for this entity type
      this._clearCache(entityType)

      return this._normalizeResponse(response)
    } catch (error) {
      this._handleError('delete', entityType, error)
      throw error
    }
  }

  /**
   * Call custom endpoint for entity
   * @param {string} entityType - Entity type identifier
   * @param {string} action - Custom action name (e.g., 'approve', 'reject', 'submit')
   * @param {number|string} id - Entity ID (optional for bulk operations)
   * @param {Object} data - Request data (optional)
   * @returns {Promise<Object>} API response
   */
  async customAction(entityType, action, id = null, data = {}) {
    try {
      const { getEntityConfig } = require('../utils/entityRegistry')
      const config = getEntityConfig(entityType)
      const basePath = config.apiPath

      let endpoint
      let method = 'post'

      // Build endpoint based on action convention
      if (id) {
        endpoint = `${basePath}/${id}/${action}`
      } else {
        endpoint = `${basePath}/${action}`
      }

      const response = await request({
        url: endpoint,
        method,
        data
      })

      // Clear cache for this entity type
      this._clearCache(entityType)

      return this._normalizeResponse(response)
    } catch (error) {
      this._handleError('customAction', entityType, error)
      throw error
    }
  }

  /**
   * Batch create entities
   * @param {string} entityType - Entity type identifier
   * @param {Array<Object>} items - Array of entity data
   * @returns {Promise<Object>} Batch creation response
   */
  async batchCreate(entityType, items) {
    try {
      const { getEntityConfig } = require('../utils/entityRegistry')
      const config = getEntityConfig(entityType)
      const basePath = config.apiPath

      const response = await request({
        url: `${basePath}/batch`,
        method: 'post',
        data: { items }
      })

      // Clear cache for this entity type
      this._clearCache(entityType)

      return this._normalizeResponse(response)
    } catch (error) {
      this._handleError('batchCreate', entityType, error)
      throw error
    }
  }

  /**
   * Batch delete entities
   * @param {string} entityType - Entity type identifier
   * @param {Array<number|string>} ids - Array of entity IDs
   * @returns {Promise<Object>} Batch deletion response
   */
  async batchDelete(entityType, ids) {
    try {
      const { getEntityConfig } = require('../utils/entityRegistry')
      const config = getEntityConfig(entityType)
      const basePath = config.apiPath

      const response = await request({
        url: `${basePath}/batch`,
        method: 'delete',
        data: { ids }
      })

      // Clear cache for this entity type
      this._clearCache(entityType)

      return this._normalizeResponse(response)
    } catch (error) {
      this._handleError('batchDelete', entityType, error)
      throw error
    }
  }

  /**
   * Get entity statistics/count
   * @param {string} entityType - Entity type identifier
   * @param {Object} filters - Optional filters for counting
   * @returns {Promise<Object>} Statistics data
   */
  async getStatistics(entityType, filters = {}) {
    try {
      const { getEntityConfig } = require('../utils/entityRegistry')
      const config = getEntityConfig(entityType)
      const basePath = config.apiPath

      const response = await request({
        url: `${basePath}/statistics`,
        method: 'get',
        params: filters
      })

      return this._normalizeResponse(response)
    } catch (error) {
      // Statistics endpoint may not exist for all entities, return default
      if (error.response?.status === 404) {
        return {
          success: true,
          data: {
            total: 0,
            byStatus: {},
            byCategory: {}
          }
        }
      }
      this._handleError('getStatistics', entityType, error)
      throw error
    }
  }

  /**
   * Normalize API response structure
   * @param {Object} response - Raw API response
   * @returns {Object} Normalized response with consistent structure
   * @private
   */
  _normalizeResponse(response) {
    // Handle different response formats from backend
    if (response.code === 200 || response.success === true) {
      return {
        success: true,
        data: response.data,
        message: response.message || '操作成功',
        pagination: response.pagination || {
          total: response.data?.total || 0,
          page: response.data?.current || 1,
          size: response.data?.size || 10
        }
      }
    } else {
      return {
        success: false,
        data: null,
        message: response.message || '操作失败',
        error: response.error
      }
    }
  }

  /**
   * Handle API errors consistently
   * @param {string} operation - Operation that failed
   * @param {string} entityType - Entity type
   * @param {Error} error - Original error
   * @private
   */
  _handleError(operation, entityType, error) {
    const errorMessage = `${operation} failed for ${entityType}: ${error.message}`

    // Log error for debugging
    console.error('[EntityGateway]', errorMessage, error)

    // Enrich error with context
    error.entityType = entityType
    error.operation = operation
    error.timestamp = new Date().toISOString()

    return error
  }

  /**
   * Clear cache for entity type
   * @param {string} entityType - Entity type identifier
   * @private
   */
  _clearCache(entityType) {
    const cacheKey = `list:${entityType}`
    this.cache.delete(cacheKey)
  }

  /**
   * Get cached data (basic caching for list operations)
   * @param {string} entityType - Entity type identifier
   * @param {Object} params - Query parameters
   * @returns {Object|null} Cached data or null
   * @private
   */
  _getFromCache(entityType, params) {
    const cacheKey = `list:${entityType}:${JSON.stringify(params)}`
    const cached = this.cache.get(cacheKey)
    if (cached && Date.now() - cached.timestamp < 30000) { // 30 second cache
      return cached.data
    }
    return null
  }

  /**
   * Set data in cache
   * @param {string} entityType - Entity type identifier
   * @param {Object} params - Query parameters
   * @param {Object} data - Data to cache
   * @private
   */
  _setCache(entityType, params, data) {
    const cacheKey = `list:${entityType}:${JSON.stringify(params)}`
    this.cache.set(cacheKey, {
      data,
      timestamp: Date.now()
    })
  }
}

// Create singleton instance
const gateway = new EntityGateway()

/**
 * Convenience functions for common operations
 */
export const entityGateway = {
  /**
   * Get entity list
   */
  list: (entityType, params) => gateway.getList(entityType, params),

  /**
   * Get entity by ID
   */
  get: (entityType, id) => gateway.getById(entityType, id),

  /**
   * Create entity
   */
  create: (entityType, data) => gateway.create(entityType, data),

  /**
   * Update entity
   */
  update: (entityType, id, data) => gateway.update(entityType, id, data),

  /**
   * Delete entity
   */
  delete: (entityType, id) => gateway.delete(entityType, id),

  /**
   * Perform custom action
   */
  action: (entityType, action, id, data) => gateway.customAction(entityType, action, id, data),

  /**
   * Batch create
   */
  batchCreate: (entityType, items) => gateway.batchCreate(entityType, items),

  /**
   * Batch delete
   */
  batchDelete: (entityType, ids) => gateway.batchDelete(entityType, ids),

  /**
   * Get statistics
   */
  statistics: (entityType, filters) => gateway.getStatistics(entityType, filters)
}

export default entityGateway