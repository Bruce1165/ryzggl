/**
 * SECURITY LOGGER
 *
 * Comprehensive security event logging system
 * Logs authentication events, authorization failures, suspicious activities
 * Provides audit trail for security monitoring and incident response
 */

/**
 * Security event types
 */
export const SecurityEventType = {
  // Authentication events
  AUTH_SUCCESS: 'AUTH_SUCCESS',
  AUTH_FAILURE: 'AUTH_FAILURE',
  AUTH_DATA_STORED: 'AUTH_DATA_STORED',
  AUTH_CLEARED: 'AUTH_CLEARED',
  TOKEN_REFRESHED: 'TOKEN_REFRESHED',
  TOKEN_REFRESH_FAILED: 'TOKEN_REFRESH_FAILED',

  // Authorization events
  AUTHORIZATION_DENIED: 'AUTHORIZATION_DENIED',
  ROLE_CHECK_FAILED: 'ROLE_CHECK_FAILED',
  PERMISSION_CHECK_FAILED: 'PERMISSION_CHECK_FAILED',

  // Network events
  SECURE_REQUEST: 'SECURE_REQUEST',
  REQUEST_ERROR: 'REQUEST_ERROR',
  REQUEST_TIMEOUT: 'REQUEST_TIMEOUT',
  NETWORK_ERROR: 'NETWORK_ERROR',
  401_UNAUTHORIZED: '401_UNAUTHORIZED',
  403_FORBIDDEN: '403_FORBIDDEN',
  429_RATE_LIMITED: '429_RATE_LIMITED',

  // XSS protection events
  XSS_ATTEMPT: 'XSS_ATTEMPT',
  XSS_BLOCKED: 'XSS_BLOCKED',
  XSS_DETECTED: 'XSS_DETECTED',

  // CSRF protection events
  CSRF_TOKEN_MISSING: 'CSRF_TOKEN_MISSING',
  CSRF_TOKEN_INVALID: 'CSRF_TOKEN_INVALID',
  CSRF_CHECK_FAILED: 'CSRF_CHECK_FAILED',

  // Data protection events
  ENCRYPTION_FAILURE: 'ENCRYPTION_FAILURE',
  DECRYPTION_FAILURE: 'DECRYPTION_FAILURE',
  DATA_EXPOSURE: 'DATA_EXPOSURE',

  // Session events
  SESSION_EXPIRED: 'SESSION_EXPIRED',
  SESSION_INVALID: 'SESSION_INVALID',
  MULTIPLE_SESSIONS: 'MULTIPLE_SESSIONS',

  // Server errors
  500_SERVER_ERROR: '500_SERVER_ERROR',
  502_GATEWAY_ERROR: '502_GATEWAY_ERROR',
  503_SERVICE_UNAVAILABLE: '503_SERVICE_UNAVAILABLE',
  504_GATEWAY_TIMEOUT: '504_GATEWAY_TIMEOUT',

  // Suspicious activities
  SUSPICIOUS_ACTIVITY: 'SUSPICIOUS_ACTIVITY',
  BRUTE_FORCE_ATTEMPT: 'BRUTE_FORCE_ATTEMPT',
  UNUSUAL_PATTERN: 'UNUSUAL_PATTERN'
}

/**
 * Security severity levels
 */
export const SecuritySeverity = {
  INFO: 'INFO',
  LOW: 'LOW',
  MEDIUM: 'MEDIUM',
  HIGH: 'HIGH',
  CRITICAL: 'CRITICAL'
}

/**
 * Maximum number of events to store locally
 */
const MAX_LOCAL_EVENTS = 100

/**
 * Event retention period (7 days)
 */
const EVENT_RETENTION_MS = 7 * 24 * 60 * 60 * 1000

/**
 * Get severity for event type
 * @param {string} eventType - Event type
 * @returns {string} Severity level
 */
function getEventSeverity(eventType) {
  const criticalEvents = [
    SecurityEventType.AUTH_FAILURE,
    SecurityEventType.XSS_ATTEMPT,
    SecurityEventType.DATA_EXPOSURE,
    SecurityEventType.BRUTE_FORCE_ATTEMPT
  ]

  const highEvents = [
    SecurityEventType.AUTHORIZATION_DENIED,
    SecurityEventType.401_UNAUTHORIZED,
    SecurityEventType.403_FORBIDDEN,
    SecurityEventType.TOKEN_REFRESH_FAILED,
    SecurityEventType.SESSION_EXPIRED
  ]

  const mediumEvents = [
    SecurityEventType.REQUEST_ERROR,
    SecurityEventType.CSRF_CHECK_FAILED,
    SecurityEventType.SUSPICIOUS_ACTIVITY
  ]

  if (criticalEvents.includes(eventType)) {
    return SecuritySeverity.CRITICAL
  }

  if (highEvents.includes(eventType)) {
    return SecuritySeverity.HIGH
  }

  if (mediumEvents.includes(eventType)) {
    return SecuritySeverity.MEDIUM
  }

  return SecuritySeverity.LOW
}

/**
 * Sanitize sensitive data from event details
 * @param {Object} details - Event details
 * @returns {Object} Sanitized details
 */
function sanitizeDetails(details) {
  const sensitiveKeys = ['password', 'token', 'refreshToken', 'csrfToken', 'secret', 'apiKey']
  const sanitized = { ...details }

  for (const key of Object.keys(sanitized)) {
    if (sensitiveKeys.some(sk => key.toLowerCase().includes(sk))) {
      sanitized[key] = '[REDACTED]'
    }
  }

  return sanitized
}

/**
 * Log security event
 * @param {string} eventType - Type of security event
 * @param {Object} details - Event details
 * @param {string} severity - Severity level (optional, auto-detected)
 */
export function logSecurityEvent(eventType, details = {}, severity = null) {
  try {
    const event = {
      id: generateEventId(),
      type: eventType,
      severity: severity || getEventSeverity(eventType),
      timestamp: new Date().toISOString(),
      userAgent: navigator.userAgent,
      url: window.location.href,
      details: sanitizeDetails(details)
    }

    // Store locally
    storeEventLocally(event)

    // Send to server in production
    if (import.meta.env.PROD) {
      sendEventToServer(event)
    }

    // Log to console in development
    if (import.meta.env.DEV) {
      console.log(`[Security Event] ${eventType}`, event)
    }

    // Alert for critical events
    if (event.severity === SecuritySeverity.CRITICAL) {
      alertCriticalEvent(event)
    }

    return event
  } catch (error) {
    console.error('Failed to log security event:', error)
  }
}

/**
 * Generate unique event ID
 * @returns {string} Event ID
 */
function generateEventId() {
  return `sec_${Date.now()}_${Math.random().toString(36).substring(2, 9)}`
}

/**
 * Store event locally
 * @param {Object} event - Security event
 */
function storeEventLocally(event) {
  try {
    const events = getLocalEvents()

    // Add new event
    events.push(event)

    // Remove old events beyond retention period
    const cutoffTime = Date.now() - EVENT_RETENTION_MS
    const validEvents = events.filter(e => new Date(e.timestamp).getTime() > cutoffTime)

    // Limit total events
    const limitedEvents = validEvents.slice(-MAX_LOCAL_EVENTS)

    localStorage.setItem('security_events', JSON.stringify(limitedEvents))
  } catch (error) {
    console.error('Failed to store event locally:', error)
  }
}

/**
 * Get local security events
 * @returns {Array} Security events
 */
export function getLocalEvents() {
  try {
    const events = JSON.parse(localStorage.getItem('security_events') || '[]')
    return events
  } catch (error) {
    console.error('Failed to get local events:', error)
    return []
  }
}

/**
 * Get events by type
 * @param {string} eventType - Event type
 * @returns {Array} Matching events
 */
export function getEventsByType(eventType) {
  const events = getLocalEvents()
  return events.filter(e => e.type === eventType)
}

/**
 * Get events by severity
 * @param {string} severity - Severity level
 * @returns {Array} Matching events
 */
export function getEventsBySeverity(severity) {
  const events = getLocalEvents()
  return events.filter(e => e.severity === severity)
}

/**
 * Get events within time range
 * @param {Date} start - Start time
 * @param {Date} end - End time
 * @returns {Array} Matching events
 */
export function getEventsByTimeRange(start, end) {
  const events = getLocalEvents()
  return events.filter(e => {
    const eventTime = new Date(e.timestamp).getTime()
    return eventTime >= start.getTime() && eventTime <= end.getTime()
  })
}

/**
 * Get events for specific user
 * @param {string} userId - User ID
 * @returns {Array} Matching events
 */
export function getEventsByUser(userId) {
  const events = getLocalEvents()
  return events.filter(e => e.details.userId === userId)
}

/**
 * Get recent events
 * @param {number} count - Number of events to return
 * @returns {Array} Recent events
 */
export function getRecentEvents(count = 10) {
  const events = getLocalEvents()
  return events.slice(-count).reverse()
}

/**
 * Get security statistics
 * @returns {Object} Security statistics
 */
export function getSecurityStats() {
  const events = getLocalEvents()

  const stats = {
    total: events.length,
    byType: {},
    bySeverity: {
      [SecuritySeverity.INFO]: 0,
      [SecuritySeverity.LOW]: 0,
      [SecuritySeverity.MEDIUM]: 0,
      [SecuritySeverity.HIGH]: 0,
      [SecuritySeverity.CRITICAL]: 0
    },
    recent24h: 0,
    recent7d: 0,
    authFailures: 0,
    xssAttempts: 0,
    suspiciousActivities: 0
  }

  const now = Date.now()
  const dayMs = 24 * 60 * 60 * 1000

  for (const event of events) {
    // Count by type
    stats.byType[event.type] = (stats.byType[event.type] || 0) + 1

    // Count by severity
    if (stats.bySeverity[event.severity] !== undefined) {
      stats.bySeverity[event.severity]++
    }

    // Count recent events
    const eventTime = new Date(event.timestamp).getTime()
    if (now - eventTime < dayMs) {
      stats.recent24h++
    }
    if (now - eventTime < 7 * dayMs) {
      stats.recent7d++
    }

    // Count specific event types
    if (event.type === SecurityEventType.AUTH_FAILURE) {
      stats.authFailures++
    }
    if (event.type === SecurityEventType.XSS_ATTEMPT) {
      stats.xssAttempts++
    }
    if (event.type === SecurityEventType.SUSPICIOUS_ACTIVITY ||
        event.type === SecurityEventType.BRUTE_FORCE_ATTEMPT ||
        event.type === SecurityEventType.UNUSUAL_PATTERN) {
      stats.suspiciousActivities++
    }
  }

  return stats
}

/**
 * Detect suspicious patterns
 * @returns {Array} Detected suspicious patterns
 */
export function detectSuspiciousPatterns() {
  const events = getLocalEvents()
  const patterns = []

  // Check for multiple auth failures from same IP/user
  const authFailures = events.filter(e => e.type === SecurityEventType.AUTH_FAILURE)
  const failureByUser = {}

  for (const event of authFailures) {
    const userId = event.details.userId || 'unknown'
    failureByUser[userId] = (failureByUser[userId] || 0) + 1

    if (failureByUser[userId] >= 5) {
      patterns.push({
        type: 'BRUTE_FORCE',
        severity: SecuritySeverity.HIGH,
        description: `Multiple authentication failures for user: ${userId}`,
        count: failureByUser[userId]
      })
    }
  }

  // Check for XSS attempts
  const xssAttempts = events.filter(e => e.type === SecurityEventType.XSS_ATTEMPT)
  if (xssAttempts.length > 0) {
    patterns.push({
      type: 'XSS_ATTACK',
      severity: SecuritySeverity.CRITICAL,
      description: 'XSS attack attempts detected',
      count: xssAttempts.length
    })
  }

  // Check for unusual request patterns
  const requests = events.filter(e => e.type === SecurityEventType.SECURE_REQUEST)
  if (requests.length > 100) {
    patterns.push({
      type: 'HIGH_VOLUME_REQUESTS',
      severity: SecuritySeverity.MEDIUM,
      description: 'Unusually high request volume',
      count: requests.length
    })
  }

  return patterns
}

/**
 * Send event to server
 * @param {Object} event - Security event
 */
function sendEventToServer(event) {
  try {
    // Use sendBeacon for reliable delivery even if page unloads
    const data = JSON.stringify(event)

    if (navigator.sendBeacon) {
      navigator.sendBeacon('/api/security/log', new Blob([data], { type: 'application/json' }))
    } else {
      // Fallback to fetch
      fetch('/api/security/log', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: data,
        keepalive: true
      }).catch(error => {
        console.error('Failed to send security log:', error)
      })
    }
  } catch (error) {
    console.error('Failed to send event to server:', error)
  }
}

/**
 * Alert critical events
 * @param {Object} event - Critical security event
 */
function alertCriticalEvent(event) {
  try {
    // Store critical event for review
    const criticalEvents = JSON.parse(localStorage.getItem('critical_security_events') || '[]')
    criticalEvents.push(event)
    localStorage.setItem('critical_security_events', JSON.stringify(criticalEvents.slice(-20)))

    // In production, you might want to:
    // - Send to security monitoring service (e.g., Datadog, Sentry)
    // - Send email to security team
    // - Create ticket in issue tracking system
    // - Trigger security webhook

    console.warn('Critical security event detected:', event)
  } catch (error) {
    console.error('Failed to alert critical event:', error)
  }
}

/**
 * Clear local security events
 */
export function clearSecurityEvents() {
  localStorage.removeItem('security_events')
  localStorage.removeItem('critical_security_events')
}

/**
 * Export security events
 * @returns {string} JSON string of events
 */
export function exportSecurityEvents() {
  try {
    const events = getLocalEvents()
    const stats = getSecurityStats()
    const patterns = detectSuspiciousPatterns()

    const report = {
      exportedAt: new Date().toISOString(),
      statistics: stats,
      suspiciousPatterns: patterns,
      events: events
    }

    return JSON.stringify(report, null, 2)
  } catch (error) {
    console.error('Failed to export events:', error)
    return '{}'
  }
}

/**
 * Initialize security logger
 * Runs on application startup
 */
export function initializeSecurityLogger() {
  try {
    // Clean up old events
    const events = getLocalEvents()
    const cutoffTime = Date.now() - EVENT_RETENTION_MS
    const validEvents = events.filter(e => new Date(e.timestamp).getTime() > cutoffTime)

    if (validEvents.length !== events.length) {
      localStorage.setItem('security_events', JSON.stringify(validEvents))
    }

    // Log initialization
    logSecurityEvent('SECURITY_LOGGER_INITIALIZED', {
      retainedEvents: validEvents.length
    })

    // Set up periodic cleanup (every hour)
    setInterval(() => {
      const currentEvents = getLocalEvents()
      const currentCutoff = Date.now() - EVENT_RETENTION_MS
      const cleanedEvents = currentEvents.filter(e => new Date(e.timestamp).getTime() > currentCutoff)

      if (cleanedEvents.length !== currentEvents.length) {
        localStorage.setItem('security_events', JSON.stringify(cleanedEvents))
      }
    }, 60 * 60 * 1000)

  } catch (error) {
    console.error('Failed to initialize security logger:', error)
  }
}
