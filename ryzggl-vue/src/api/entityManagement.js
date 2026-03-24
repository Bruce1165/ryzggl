import request from './request'

// ApplyContinue API
export function getApplyContinueList(params) {
  return request({
    url: '/api/v1/apply-continue/list',
    method: 'get',
    params
  })
}

export function getApplyContinueById(id) {
  return request({
    url: `/api/v1/apply-continue/${id}`,
    method: 'get'
  })
}

export function createApplyContinue(data) {
  return request({
    url: '/api/v1/apply-continue',
    method: 'post',
    data
  })
}

export function updateApplyContinue(id, data) {
  return request({
    url: `/api/v1/apply-continue/${id}`,
    method: 'put',
    data
  })
}

export function deleteApplyContinue(id) {
  return request({
    url: `/api/v1/apply-continue/${id}`,
    method: 'delete'
  })
}

// ApplyRenew API
export function getApplyRenewList(params) {
  return request({
    url: '/api/v1/apply/renew/list',
    method: 'get',
    params
  })
}

export function getApplyRenewById(id) {
  return request({
    url: `/api/v1/apply/renew/${id}`,
    method: 'get'
  })
}

export function createApplyRenew(data) {
  return request({
    url: '/api/v1/apply/renew',
    method: 'post',
    data
  })
}

export function updateApplyRenew(id, data) {
  return request({
    url: `/api/v1/apply/renew/${id}`,
    method: 'put',
    data
  })
}

export function deleteApplyRenew(id) {
  return request({
    url: `/api/v1/apply/renew/${id}`,
    method: 'delete'
  })
}

// CertificateChange API
export function getCertificateChangeList(params) {
  return request({
    url: '/api/v1/certificate/change/list',
    method: 'get',
    params
  })
}

export function getCertificateChangeById(id) {
  return request({
    url: `/api/v1/certificate/change/${id}`,
    method: 'get'
  })
}

export function createCertificateChange(data) {
  return request({
    url: '/api/v1/certificate/change',
    method: 'post',
    data
  })
}

export function updateCertificateChange(id, data) {
  return request({
    url: `/api/v1/certificate/change/${id}`,
    method: 'put',
    data
  })
}

export function deleteCertificateChange(id) {
  return request({
    url: `/api/v1/certificate/change/${id}`,
    method: 'delete'
  })
}

export function getCertificateChangeStatistics(params) {
  return request({
    url: '/api/v1/certificate/change/statistics',
    method: 'get',
    params
  })
}

// CertificateContinue API
export function getCertificateContinueList(params) {
  return request({
    url: '/api/v1/certificate/continue/list',
    method: 'get',
    params
  })
}

export function getCertificateContinueById(id) {
  return request({
    url: `/api/v1/certificate/continue/${id}`,
    method: 'get'
  })
}

export function createCertificateContinue(data) {
  return request({
    url: '/api/v1/certificate/continue',
    method: 'post',
    data
  })
}

export function updateCertificateContinue(id, data) {
  return request({
    url: `/api/v1/certificate/continue/${id}`,
    method: 'put',
    data
  })
}

export function deleteCertificateContinue(id) {
  return request({
    url: `/api/v1/certificate/continue/${id}`,
    method: 'delete'
  })
}

export function getCertificateContinueStatistics(params) {
  return request({
    url: '/api/v1/certificate/continue/statistics',
    method: 'get',
    params
  })
}