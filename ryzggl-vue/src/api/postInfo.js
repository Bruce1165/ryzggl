/**
 * PostInfo API Client
 * 岗位信息相关接口
 *
 * Maps to PostInfoController
 * Backend endpoint: /api/v1/posts
 *
 * PostInfo entity fields:
 * - postId (岗位ID)
 * - postType (岗位类型: 1=专业, 2=工种, 3=技能等级)
 * - postName (岗位名称)
 * - upPostId (上级岗位ID)
 * - examFee (考试费用)
 * - currentNumber (当前流水号)
 * - codeYear (编码年度)
 * - codeFormat (证书编号格式)
 */

import request from './request'

/**
 * PostInfo API endpoints
 */
const API = {
  // Base path
  BASE: '/api/v1/posts',
  // Get by ID
  GET_BY_ID: (postId) => `/api/v1/posts/${postId}`,
  // Get by type
  GET_BY_TYPE: (postType) => `/api/v1/posts/type/${postType}`,
  // Get by parent ID
  GET_BY_PARENT: (upPostId) => `/api/v1/posts/parent/${upPostId}`,
  // Get skill levels
  SKILL_LEVELS: '/api/v1/posts/skill-levels',
  // List
  LIST: '/api/v1/posts',
  // Search
  SEARCH: '/api/v1/posts/search',
  // Create
  CREATE: '/api/v1/posts/create',
  // Update
  UPDATE: (postId) => `/api/v1/posts/${postId}`,
  // Delete
  DELETE: (postId) => `/api/v1/posts/${postId}`,
  // Get next certificate number
  NEXT_CERT_NO: (postId) => `/api/v1/posts/${postId}/next-cert-no`,
  // Get and update next certificate number
  UPDATE_NEXT_CERT_NO: (postId) => `/api/v1/posts/${postId}/next-cert-no`,
  // Get skill level code
  SKILL_LEVEL_CODE: '/api/v1/posts/skill-level-code'
}

/**
 * Get post by ID
 * @param {Number} postId - Post ID
 */
export function getPostById(postId) {
  return request({
    url: API.GET_BY_ID(postId),
    method: 'get'
  })
}

/**
 * Get posts by type
 * @param {String} postType - Post type (1=专业, 2=工种, 3=技能等级)
 */
export function getPostsByType(postType) {
  return request({
    url: API.GET_BY_TYPE(postType),
    method: 'get'
  })
}

/**
 * Get child posts by parent ID
 * @param {Number} upPostId - Parent post ID
 */
export function getPostsByParent(upPostId) {
  return request({
    url: API.GET_BY_PARENT(upPostId),
    method: 'get'
  })
}

/**
 * Get distinct skill levels
 */
export function getSkillLevels() {
  return request({
    url: API.SKILL_LEVELS,
    method: 'get'
  })
}

/**
 * Get all posts with pagination
 * @param {Object} params - {current, size, postType, keyword}
 */
export function getPosts(params) {
  return request({
    url: API.LIST,
    method: 'get',
    params
  })
}

/**
 * Search posts
 * @param {String} keyword - Search keyword
 */
export function searchPosts(keyword) {
  return request({
    url: API.SEARCH,
    method: 'get',
    params: { keyword }
  })
}

/**
 * Create post
 * @param {Object} data - PostInfo entity data
 */
export function createPost(data) {
  return request({
    url: API.CREATE,
    method: 'post',
    data
  })
}

/**
 * Update post
 * @param {Number} postId - Post ID
 * @param {Object} data - PostInfo entity data
 */
export function updatePost(postId, data) {
  return request({
    url: API.UPDATE(postId),
    method: 'put',
    data
  })
}

/**
 * Delete post
 * @param {Number} postId - Post ID
 */
export function deletePost(postId) {
  return request({
    url: API.DELETE(postId),
    method: 'delete'
  })
}

/**
 * Get next certificate number (without updating)
 * @param {Number} postId - Post ID
 */
export function getNextCertificateNo(postId) {
  return request({
    url: API.NEXT_CERT_NO(postId),
    method: 'get'
  })
}

/**
 * Get and update next certificate number
 * @param {Number} postId - Post ID
 */
export function updateNextCertificateNo(postId) {
  return request({
    url: API.UPDATE_NEXT_CERT_NO(postId),
    method: 'post'
  })
}

/**
 * Get skill level code
 * @param {String} skillLevelName - Skill level name
 */
export function getSkillLevelCode(skillLevelName) {
  return request({
    url: API.SKILL_LEVEL_CODE,
    method: 'get',
    params: { skillLevelName }
  })
}

// Export API constants for reference
export { API }
export default {
  getPostById,
  getPostsByType,
  getPostsByParent,
  getSkillLevels,
  getPosts,
  searchPosts,
  createPost,
  updatePost,
  deletePost,
  getNextCertificateNo,
  updateNextCertificateNo,
  getSkillLevelCode,
  API
}
