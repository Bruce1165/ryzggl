import request from './request'

/**
 * Upload file
 * @param {FormData} formData - File data
 */
export function uploadFile(formData) {
  return request({
    url: '/api/file/upload',
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data'
    }
  })
}

/**
 * Get file list
 * @param {Object} params - Query parameters
 */
export function getFileList(params) {
  return request({
    url: '/api/file/list',
    method: 'get',
    params
  })
}

/**
 * Get file detail
 * @param {String} fileId - File ID
 */
export function getFileDetail(fileId) {
  return request({
    url: `/api/file/${fileId}`,
    method: 'get'
  })
}

/**
 * Delete file
 * @param {String} fileId - File ID
 */
export function deleteFile(fileId) {
  return request({
    url: `/api/file/${fileId}`,
    method: 'delete'
  })
}

/**
 * Batch delete files
 * @param {Array} fileIds - File IDs array
 */
export function batchDeleteFiles(fileIds) {
  return request({
    url: '/api/file/batch',
    method: 'delete',
    data: { fileIds }
  })
}

/**
 * Download file
 * @param {String} fileId - File ID
 */
export function downloadFile(fileId) {
  return request({
    url: `/api/file/${fileId}/download`,
    method: 'get',
    responseType: 'blob'
  })
}

/**
 * Get file preview URL
 * @param {String} fileId - File ID
 */
export function getFilePreviewUrl(fileId) {
  return `${import.meta.env.VITE_API_BASE_URL}/api/file/${fileId}/preview?token=${localStorage.getItem('token')}`
}
