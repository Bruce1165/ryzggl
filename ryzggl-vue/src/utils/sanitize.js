/**
 * INPUT SANITIZATION UTILITIES
 *
 * Provides XSS protection using DOMPurify
 * Sanitizes all user input before rendering
 *
 * OWASP XSS Prevention Cheat Sheet compliant
 */

import DOMPurify from 'dompurify'

// Configure DOMPurify with strict settings
const PURIFY_CONFIG = {
  ALLOWED_TAGS: [
    'p', 'br', 'b', 'i', 'strong', 'em', 'u', 'span',
    'a', 'ul', 'ol', 'li', 'h1', 'h2', 'h3', 'h4', 'h5', 'h6',
    'table', 'thead', 'tbody', 'tr', 'th', 'td'
  ],
  ALLOWED_ATTR: ['href', 'title', 'class', 'target', 'rel'],
  ALLOW_DATA_ATTR: false,
  FORBID_TAGS: ['script', 'object', 'embed', 'iframe', 'form', 'input', 'button'],
  FORBID_ATTR: ['onerror', 'onload', 'onclick', 'onmouseover', 'onfocus', 'onblur'],
  SANITIZE_DOM: true,
  ADD_ATTR: ['data-*'], // Allow custom data attributes
  SAFE_FOR_TEMPLATES: true,
  WHOLE_DOCUMENT: false
}

/**
 * Sanitize HTML string to prevent XSS attacks
 * @param {string} dirty - Potentially unsafe HTML
 * @param {Object} config - Custom DOMPurify config (optional)
 * @returns {string} Sanitized HTML
 */
export function sanitizeHTML(dirty, config = {}) {
  if (typeof dirty !== 'string') {
    return ''
  }

  try {
    return DOMPurify.sanitize(dirty, { ...PURIFY_CONFIG, ...config })
  } catch (error) {
    console.error('Sanitization failed:', error)
    return ''
  }
}

/**
 * Sanitize URL to prevent javascript: and data: protocol attacks
 * @param {string} url - Potentially unsafe URL
 * @returns {string} Sanitized URL or empty string if dangerous
 */
export function sanitizeURL(url) {
  if (typeof url !== 'string') {
    return ''
  }

  try {
    // Remove leading/trailing whitespace
    url = url.trim()

    // Block dangerous protocols
    const dangerousProtocols = ['javascript:', 'data:', 'vbscript:', 'file:']
    const lowerUrl = url.toLowerCase()

    for (const protocol of dangerousProtocols) {
      if (lowerUrl.startsWith(protocol)) {
        console.warn('Blocked dangerous URL protocol:', protocol)
        return ''
      }
    }

    // Check for onclick handlers
    if (/onclick|onerror|onload|onmouseover|onfocus|onblur/i.test(url)) {
      console.warn('Blocked URL with event handler')
      return ''
    }

    return url
  } catch (error) {
    console.error('URL sanitization failed:', error)
    return ''
  }
}

/**
 * Sanitize plain text (escape HTML entities)
 * @param {string} text - Plain text to escape
 * @returns {string} HTML-escaped text
 */
export function escapeHTML(text) {
  if (typeof text !== 'string') {
    return ''
  }

  const map = {
    '&': '&amp;',
    '<': '&lt;',
    '>': '&gt;',
    '"': '&quot;',
    "'": '&#x27;',
    '/': '&#x2F;'
  }

  return text.replace(/[&<>"'/]/g, char => map[char])
}

/**
 * Sanitize user input object deeply
 * @param {Object|Array} data - Data to sanitize
 * @param {Array} skipFields - Field names to skip sanitization
 * @returns {Object|Array} Sanitized data
 */
export function sanitizeObject(data, skipFields = []) {
  if (data === null || data === undefined) {
    return data
  }

  if (Array.isArray(data)) {
    return data.map(item => sanitizeObject(item, skipFields))
  }

  if (typeof data === 'object') {
    const sanitized = {}
    for (const [key, value] of Object.entries(data)) {
      if (skipFields.includes(key)) {
        sanitized[key] = value
      } else if (typeof value === 'string') {
        sanitized[key] = escapeHTML(value)
      } else if (typeof value === 'object' || Array.isArray(value)) {
        sanitized[key] = sanitizeObject(value, skipFields)
      } else {
        sanitized[key] = value
      }
    }
    return sanitized
  }

  if (typeof data === 'string') {
    return escapeHTML(data)
  }

  return data
}

/**
 * Validate email format
 * @param {string} email - Email to validate
 * @returns {boolean} Is valid email
 */
export function isValidEmail(email) {
  if (typeof email !== 'string') {
    return false
  }

  const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/
  return emailRegex.test(email.trim())
}

/**
 * Validate username format
 * @param {string} username - Username to validate
 * @returns {boolean} Is valid username
 */
export function isValidUsername(username) {
  if (typeof username !== 'string') {
    return false
  }

  // Alphanumeric with underscore, dash, and dot
  const usernameRegex = /^[a-zA-Z0-9_.-]{3,30}$/
  return usernameRegex.test(username.trim())
}

/**
 * Validate phone number format (Chinese phone numbers)
 * @param {string} phone - Phone number to validate
 * @returns {boolean} Is valid phone number
 */
export function isValidPhone(phone) {
  if (typeof phone !== 'string') {
    return false
  }

  // Chinese mobile phone format
  const phoneRegex = /^1[3-9]\d{9}$/
  return phoneRegex.test(phone.trim())
}

/**
 * Validate ID card format (Chinese ID card)
 * @param {string} idCard - ID card to validate
 * @returns {boolean} Is valid ID card
 */
export function isValidIDCard(idCard) {
  if (typeof idCard !== 'string') {
    return false
  }

  // Chinese ID card format (18 digits)
  const idCardRegex = /^\d{17}[\dXx]$/
  return idCardRegex.test(idCard.trim())
}

/**
 * Sanitize file name
 * @param {string} fileName - File name to sanitize
 * @returns {string} Sanitized file name
 */
export function sanitizeFileName(fileName) {
  if (typeof fileName !== 'string') {
    return 'unnamed'
  }

  // Remove dangerous characters
  let sanitized = fileName
    .replace(/[<>:"/\\|?*\x00-\x1f]/g, '') // Remove invalid characters
    .replace(/[\s.]+/g, '_') // Replace spaces and dots with underscores
    .replace(/^_+|_+$/g, '') // Remove leading/trailing underscores
    .substring(0, 255) // Limit length

  return sanitized || 'unnamed'
}

/**
 * Validate and sanitize search query
 * @param {string} query - Search query
 * @returns {string} Sanitized query
 */
export function sanitizeSearchQuery(query) {
  if (typeof query !== 'string') {
    return ''
  }

  // Remove SQL injection attempts
  let sanitized = query
    .replace(/['";\\]/g, '') // Remove SQL injection characters
    .replace(/--/g, '') // Remove SQL comment markers
    .replace(/\/\*|\*\//g, '') // Remove SQL block comments
    .trim()

  return sanitized
}

/**
 * Sanitize URL parameters
 * @param {Object} params - URL parameters
 * @returns {Object} Sanitized parameters
 */
export function sanitizeURLParams(params) {
  if (typeof params !== 'object' || params === null) {
    return {}
  }

  const sanitized = {}
  for (const [key, value] of Object.entries(params)) {
    if (typeof value === 'string') {
      sanitized[key] = sanitizeURL(value)
    } else if (typeof value === 'number' || typeof value === 'boolean') {
      sanitized[key] = value
    }
    // Arrays and objects are not typically used in URL params
  }

  return sanitized
}

/**
 * Create a safe v-html directive wrapper
 * This should be used instead of v-html in Vue templates
 * @param {string} content - Content to render
 * @returns {string} Sanitized HTML
 */
export function safeHTML(content) {
  return sanitizeHTML(content)
}

/**
 * Validate string length
 * @param {string} str - String to validate
 * @param {number} min - Minimum length
 * @param {number} max - Maximum length
 * @returns {boolean} Is valid length
 */
export function isValidLength(str, min = 0, max = 1000) {
  if (typeof str !== 'string') {
    return false
  }

  return str.length >= min && str.length <= max
}

/**
 * Sanitize and validate form data
 * @param {Object} formData - Form data to sanitize
 * @param {Object} schema - Validation schema
 * @returns {Object} { valid: boolean, data: Object, errors: Array }
 */
export function sanitizeAndValidateForm(formData, schema) {
  const errors = []
  const sanitized = {}

  for (const [field, rules] of Object.entries(schema)) {
    const value = formData[field]

    // Skip if field not in data and not required
    if (value === undefined || value === null) {
      if (rules.required) {
        errors.push(`${field} is required`)
      }
      continue
    }

    // Type validation
    if (rules.type === 'email' && !isValidEmail(value)) {
      errors.push(`${field} must be a valid email`)
      continue
    }

    if (rules.type === 'phone' && !isValidPhone(value)) {
      errors.push(`${field} must be a valid phone number`)
      continue
    }

    if (rules.type === 'idcard' && !isValidIDCard(value)) {
      errors.push(`${field} must be a valid ID card`)
      continue
    }

    if (rules.type === 'username' && !isValidUsername(value)) {
      errors.push(`${field} must be a valid username`)
      continue
    }

    // Length validation
    if (rules.minLength && !isValidLength(value, rules.minLength, rules.maxLength || 1000)) {
      errors.push(`${field} must be at least ${rules.minLength} characters`)
      continue
    }

    if (rules.maxLength && !isValidLength(value, 0, rules.maxLength)) {
      errors.push(`${field} must be at most ${rules.maxLength} characters`)
      continue
    }

    // Sanitize
    if (typeof value === 'string') {
      sanitized[field] = rules.allowHTML ? sanitizeHTML(value) : escapeHTML(value)
    } else {
      sanitized[field] = value
    }
  }

  return {
    valid: errors.length === 0,
    data: sanitized,
    errors
  }
}

/**
 * Detect potential XSS attack patterns
 * @param {string} input - Input to check
 * @returns {boolean} Contains suspicious patterns
 */
export function detectXSS(input) {
  if (typeof input !== 'string') {
    return false
  }

  const xssPatterns = [
    /<script/i,
    /javascript:/i,
    /on\w+\s*=/i,
    /<iframe/i,
    /<embed/i,
    /<object/i,
    /eval\s*\(/i,
    /fromCharCode/i,
    /&#x/i,
    /&#\d+;/i
  ]

  return xssPatterns.some(pattern => pattern.test(input))
}

/**
 * Log XSS attempt for security monitoring
 * @param {string} input - Suspicious input
 * @param {string} context - Context where input was detected
 */
export function logXSSThreat(input, context = 'unknown') {
  try {
    const threat = {
      type: 'XSS_ATTEMPT',
      timestamp: new Date().toISOString(),
      input: input.substring(0, 500), // Limit length
      context,
      userAgent: navigator.userAgent,
      url: window.location.href
    }

    // Store in localStorage
    const threats = JSON.parse(localStorage.getItem('xss_threats') || '[]')
    threats.push(threat)

    // Keep only last 100 threats
    if (threats.length > 100) {
      threats.shift()
    }

    localStorage.setItem('xss_threats', JSON.stringify(threats))

    // In production, send to server
    if (import.meta.env.PROD) {
      fetch('/api/security/xss-report', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(threat),
        keepalive: true
      }).catch(console.error)
    }
  } catch (error) {
    console.error('Failed to log XSS threat:', error)
  }
}
