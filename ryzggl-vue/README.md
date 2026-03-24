# RYZGGL Frontend - Vue.js 3

Frontend application for RYZGGL (人员资格管理系统) - Personnel Qualification Management System.

## Overview

This is a Vue.js 3 frontend that connects to the Spring Boot backend for managing professional certifications, examinations, and worker information.

## Technology Stack

- **Framework**: Vue.js 3 with Composition API
- **Build Tool**: Vite
- **UI Library**: Element Plus
- **HTTP Client**: Axios
- **State Management**: Reactive API (Vue 3 built-in)
- **Routing**: Vue Router 4
- **Language**: JavaScript (ES6+)

## Project Structure

```
ryzggl-vue/
├── src/
│   ├── api/                 # API client modules
│   │   ├── request.js      # Axios instance with interceptors
│   │   ├── auth.js         # Authentication APIs
│   │   ├── apply.js       # Application APIs
│   │   ├── certificate.js  # Certificate APIs
│   │   ├── worker.js      # Worker APIs
│   │   ├── exam.js        # Examination APIs
│   │   └── file.js        # File management APIs
│   ├── views/                # Page components (35 pages)
│   │   ├── Login.vue       # Login page
│   │   ├── Dashboard.vue   # Dashboard
│   │   ├── apply/         # Application pages
│   │   ├── certificate/    # Certificate pages
│   │   ├── worker/        # Worker pages
│   │   ├── exam/          # Examination pages
│   │   ├── department/     # Department management
│   │   ├── user/          # User management
│   │   ├── role/          # Role management
│   │   ├── agency/        # Agency management
│   │   ├── permission/     # Permission management
│   │   ├── enterprise/     # Enterprise management
│   │   ├── qualification/  # Qualification types
│   │   ├── report/        # Reports and statistics
│   │   ├── file/          # File management
│   │   └── import-export/  # Data import/export
│   ├── router/
│   │   └── index.js       # Vue Router configuration
│   ├── App.vue             # Root component
│   └── main.js             # Application entry point
├── .env.development       # Development environment variables
├── .env.production        # Production environment variables
├── index.html             # HTML template
├── package.json           # Dependencies
└── vite.config.js        # Vite configuration
```

## Getting Started

### Prerequisites

- Node.js 16 or higher
- npm or yarn
- Backend API running (Spring Boot on port 8080)

### Installation

```bash
# Install dependencies
npm install

# Or with yarn
yarn install
```

### Development

```bash
# Start development server
npm run dev

# Frontend will be available at http://localhost:5173
```

### Production Build

```bash
# Build for production
npm run build

# Output in dist/ directory
# Serve with any static file server
```

## API Integration

The frontend connects to the backend via:

1. **API Base URL**: Configured in `.env` files
   - Development: `http://localhost:8080`
   - Production: `http://101.200.193.13:8080`

2. **Authentication**: JWT token stored in `localStorage`
   - Token included in `Authorization: Bearer {token}` header
   - Auto-redirect to login on 401 errors

3. **Response Format**: All backend responses follow Result<T> wrapper
   ```json
   {
     "code": 0,
     "message": "success",
     "data": { ... },
     "timestamp": 1234567890
   }
   ```

## Pages (35 Total)

### Authentication (2 pages)
- **Login** - User authentication
- **Dashboard** - Statistics and quick actions

### Application Management (7 pages)
- **List** - Application list with search and filters
- **Create** - New application form
- **Detail** - Application details with approve/reject
- **Change** - Certificate change application
- **Renew** - Certificate renewal application
- **Cancel** - Certificate cancellation application
- **Replace** - Certificate replacement (lost/damaged)

### Certificate Management (6 pages)
- **List** - Certificate list with status management
- **Detail** - Certificate detail view
- **Continue** - Certificate renewal form
- **Change** - Certificate change operations
- **Pause** - Certificate pause with resume calculation
- **Lock** - Certificate lock (temporary/permanent)
- **Merge** - Certificate merge with multi-select

### Worker Management (3 pages)
- **List** - Worker list with search
- **Create** - New worker form
- **Detail** - Worker detail view

### Examination Management (6 pages)
- **List** - Exam list
- **Detail** - Exam details
- **SignUp** - Exam registration form
- **Results** - Score management
- **Places** - Exam venue management
- **Plans** - Exam plan scheduling
- **Participants** - Exam participant management

### Organization Management (6 pages)
- **Department** - Department hierarchy with tree
- **User** - User management with role assignment
- **Role** - Role management with permission tree
- **Agency** - Agency/institution management
- **Enterprise** - Enterprise CRUD
- **Qualification** - Qualification type management

### System Management (5 pages)
- **Settings** - System settings (profile, password, notifications)
- **Report** - Statistics dashboard
- **Permission** - Permission management
- **File** - File upload/download manager
- **Import-Export** - Data import/export

## Key Features

### Authentication
- JWT-based stateless authentication
- Auto token refresh
- Route guards for protected pages
- Session management

### UI Components
- Responsive design with Element Plus
- Form validation
- File upload with drag-and-drop
- Data tables with pagination
- Dialog modals
- Date pickers
- Tree views for hierarchies
- Rich text editing
- Image previews

### Data Management
- CRUD operations for all entities
- Bulk operations (batch delete)
- Search and filtering
- Sorting and pagination
- Export to Excel
- Import from Excel

### Error Handling
- Global error interceptors
- User-friendly error messages
- Auto-logout on token expiry
- Network error detection

## Configuration

### Environment Variables

`.env.development`:
```
VITE_API_BASE_URL=http://localhost:8080
```

`.env.production`:
```
VITE_API_BASE_URL=http://101.200.193.13:8080
```

### Router Configuration

- Lazy loading for all routes
- Authentication guards
- Route meta for permissions
- Dynamic titles

### API Client

- Axios instance with interceptors
- Request: Add JWT token
- Response: Handle error codes
- Timeout: 10 seconds

## Deployment

### Development

```bash
npm run dev
```

### Production

```bash
# Build
npm run build

# Serve dist/ with Nginx or similar
# Configure Nginx to proxy API requests to backend
```

### Docker (Optional)

```dockerfile
# Dockerfile example
FROM node:18-alpine
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run build
EXPOSE 80
CMD ["npm", "run", "preview"]
```

## Browser Support

- Chrome (latest)
- Firefox (latest)
- Safari (latest)
- Edge (latest)

## Troubleshooting

### Frontend won't start
- Check Node.js version: `node --version`
- Clear node_modules: `rm -rf node_modules && npm install`
- Check port 5173 is available

### API connection errors
- Verify backend is running on port 8080
- Check CORS configuration in backend
- Verify .env API_BASE_URL
- Check browser console for errors

### Login fails
- Check network connection
- Verify backend login endpoint works (test with Swagger)
- Clear localStorage and retry
- Check JWT token storage

### Pages not loading
- Check router configuration
- Verify component imports
- Check for syntax errors
- Review browser console

## Development Guidelines

### Code Style
- Use Vue 3 Composition API
- Use `<script setup>` for new components
- Follow existing patterns
- Use Element Plus components
- Validate all forms

### API Calls
- Use API modules from `@/api/`
- Handle loading states
- Show error messages
- Update UI after success
- Use try-catch-finally pattern

### State Management
- Use reactive() for component state
- Use ref() for primitives
- Avoid global state when possible
- Use props/emits for parent-child

## License

Copyright © 2024 RYZGGL. All rights reserved.

## Support

- Backend: See `../ryzggl-java/QUICK_START.md`
- Issue Tracker: Internal project management
- Documentation: Project wiki
