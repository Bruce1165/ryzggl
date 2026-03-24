/**
 * Vite Security Headers Plugin
 *
 * Adds security headers to all responses during development
 * For production, these should be configured on your web server (nginx, Apache, etc.)
 */

export function securityHeadersPlugin() {
  return {
    name: 'vite-security-headers',

    configureServer(server) {
      server.middlewares.use((req, res, next) => {
        // Content Security Policy (CSP)
        // Strict CSP policy to prevent XSS attacks
        res.setHeader(
          'Content-Security-Policy',
          [
            "default-src 'self'",
            "script-src 'self' 'unsafe-inline' 'unsafe-eval'",
            "style-src 'self' 'unsafe-inline'",
            "img-src 'self' data: https:",
            "font-src 'self' data:",
            "connect-src 'self' http://localhost:8080 https:",
            "media-src 'self'",
            "object-src 'none'",
            "frame-src 'none'",
            "base-uri 'self'",
            "form-action 'self'",
            "frame-ancestors 'none'",
            "block-all-mixed-content",
            "upgrade-insecure-requests"
          ].join('; ')
        )

        // X-XSS-Protection
        // Enables browser's built-in XSS filter (for legacy browsers)
        res.setHeader('X-XSS-Protection', '1; mode=block')

        // X-Frame-Options
        // Prevents clickjacking attacks
        res.setHeader('X-Frame-Options', 'DENY')

        // X-Content-Type-Options
        // Prevents MIME type sniffing
        res.setHeader('X-Content-Type-Options', 'nosniff')

        // Strict-Transport-Security (HSTS)
        // Forces HTTPS connections (only effective when served over HTTPS)
        // res.setHeader('Strict-Transport-Security', 'max-age=31536000; includeSubDomains; preload')

        // Referrer-Policy
        // Controls referrer information sent
        res.setHeader('Referrer-Policy', 'strict-origin-when-cross-origin')

        // Permissions-Policy
        // Controls which browser features can be used
        res.setHeader(
          'Permissions-Policy',
          [
            'camera=()',
            'microphone=()',
            'geolocation=()',
            'payment=()',
            'usb=()',
            'bluetooth=()',
            'magnetometer=()',
            'gyroscope=()',
            'accelerometer=()'
          ].join(', ')
        )

        // X-Permitted-Cross-Domain-Policies
        // Restricts cross-domain policy files
        res.setHeader('X-Permitted-Cross-Domain-Policies', 'none')

        // Cache-Control for sensitive resources
        // Prevents caching of sensitive data
        if (req.url.includes('/api/') || req.url.includes('/auth/')) {
          res.setHeader('Cache-Control', 'no-store, no-cache, must-revalidate, private')
          res.setHeader('Pragma', 'no-cache')
          res.setHeader('Expires', '0')
        }

        // Cross-Origin Opener Policy
        // Controls cross-origin opener behavior
        res.setHeader('Cross-Origin-Opener-Policy', 'same-origin')

        // Cross-Origin Resource Policy
        // Controls cross-origin resource access
        res.setHeader('Cross-Origin-Resource-Policy', 'same-origin')

        // Cross-Origin Embedder Policy
        // Controls cross-origin embedding
        res.setHeader('Cross-Origin-Embedder-Policy', 'require-corp')

        next()
      })
    }
  }
}
