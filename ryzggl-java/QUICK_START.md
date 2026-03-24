# RYZGGL Backend - Quick Start Guide

## Prerequisites

- Java 17 or higher
- Maven 3.6+
- SQL Server database connection
- Node.js 16+ (for frontend)

## Database Setup

The application connects to SQL Server database `RYZG`:
- Host: 101.200.193.13:1433
- Database: RYZG
- Username: synergydatauser
- Password: synergy_password

## Build & Run Backend

```bash
# Navigate to backend directory
cd ryzggl-java

# Clean and build
mvn clean package

# Run the application
java -jar target/ryzggl-java-1.0.0.jar

# Or run with Maven
mvn spring-boot:run
```

### Backend Endpoints

Once started, the backend will be available at:

- **API Base URL**: http://localhost:8080/api
- **Swagger Documentation**: http://localhost:8080/doc.html
- **Login Endpoint**: POST http://localhost:8080/api/auth/login

### API Response Format

All API responses follow this format:

```json
{
  "code": 0,          // 0=success, 1=business error, 2=unauthorized, 3=forbidden
  "message": "success",
  "data": { ... },      // Response payload (null for errors)
  "timestamp": 1234567890
}
```

## Run Frontend

```bash
# Navigate to frontend directory
cd ryzggl-vue

# Install dependencies (first time only)
npm install

# Run development server
npm run dev

# Build for production
npm run build
```

### Frontend URLs

- **Development**: http://localhost:5173
- **API Proxy**: Configured in .env files
  - Development: http://localhost:8080
  - Production: http://101.200.193.13:8080

## Authentication Flow

1. User submits login form → POST `/api/auth/login`
2. Backend validates credentials
3. Backend returns JWT token in response.data
4. Frontend stores token in localStorage
5. All subsequent requests include `Authorization: Bearer {token}` header
6. JwtAuthenticationFilter validates token on each request
7. Token expired (401) → Redirect to login

## Testing with Swagger

1. Start backend: `mvn spring-boot:run`
2. Open browser: http://localhost:8080/doc.html
3. Use Swagger UI to test all endpoints

### Test Login Example

```bash
curl -X POST http://localhost:8080/api/auth/login \
  -H "Content-Type: application/json" \
  -d '{"username":"admin","password":"password"}'
```

## Project Structure

```
ryzggl-java/
├── src/main/java/com/ryzggl/
│   ├── common/              # Shared components (Result, BaseEntity, JWT)
│   ├── entity/             # JPA entities (Apply, Certificate, Worker, Exam, User)
│   ├── repository/          # MyBatis-Plus mappers
│   ├── service/            # Business logic layer
│   ├── controller/         # REST API endpoints
│   └── config/             # Configuration (Security, MyBatis-Plus)
├── src/main/resources/
│   └── application.yml     # Database & JWT configuration
├── pom.xml               # Maven dependencies
└── target/               # Build output
```

## Available Endpoints

### Authentication
- `POST /api/auth/login` - User login
- `POST /api/auth/register` - User registration

### Application Management
- `GET /api/apply/list` - Query applications with pagination
- `GET /api/apply/{id}` - Get application by ID
- `POST /api/apply` - Create application
- `PUT /api/apply/{id}/submit` - Submit for review
- `PUT /api/apply/approve` - Approve application
- `PUT /api/apply/reject` - Reject application
- `DELETE /api/apply/{id}` - Delete application

### Certificate Management
- `GET /api/certificates/list` - Query certificates with pagination
- `GET /api/certificates/{id}` - Get certificate by ID
- `POST /api/certificates` - Create certificate
- `PUT /api/certificates` - Update certificate
- `PUT /api/certificates/{id}/pause` - Pause certificate
- `PUT /api/certificates/{id}/resume` - Resume certificate
- `PUT /api/certificates/{id}/revoke` - Revoke certificate
- `DELETE /api/certificates/{id}` - Delete certificate

### Worker Management
- `GET /api/worker/list` - Query workers with pagination
- `GET /api/worker/{id}` - Get worker by ID
- `GET /api/worker/idcard/{idCard}` - Get worker by ID card
- `POST /api/worker` - Create worker
- `PUT /api/worker` - Update worker
- `DELETE /api/worker/{id}` - Delete worker

### Examination Management
- `GET /api/exam/list` - Query exams with pagination
- `GET /api/exam/{id}` - Get exam by ID
- `GET /api/exam/available` - Get available exams for sign-up
- `POST /api/exam` - Create exam
- `PUT /api/exam` - Update exam
- `DELETE /api/exam/{id}` - Delete exam

## Troubleshooting

### Backend won't start
- Check Java version: `java -version` (must be 17+)
- Check port 8080 is not in use
- Verify database connection in application.yml
- Check SQL Server is accessible

### Frontend can't connect to backend
- Verify backend is running on port 8080
- Check .env.development has correct API_BASE_URL
- Check browser console for CORS errors
- Verify SecurityConfig allows CORS from frontend

### Login fails
- Check user exists in User table
- Verify password is BCrypt encrypted
- Check JWT secret in application.yml matches
- Review AuthService.login() logic

### 401 Unauthorized errors
- Token expired → Clear localStorage and re-login
- Invalid token format → Check Authorization header format
- User disabled → Check User.STATUS field

## Default Test Users

To test the system, you can insert test users directly into the database:

```sql
INSERT INTO User (USERNAME, PASSWORD, REALNAME, PHONE, STATUS, CREATE_TIME)
VALUES
('admin', '$2a$10$N.zmdr9k7uOCQb3dXGXu.eKv... (BCrypt hash)', '管理员', '13800138000', '正常', GETDATE()),
('test', '$2a$10$N.zmdr9k7uOCQb3dXGXu.eKv... (BCrypt hash)', '测试用户', '13900139000', '正常', GETDATE());
```

Or use the registration endpoint via Swagger UI.

## Development Tips

1. **Hot Reload**: Both Maven and Vite support hot reload
2. **Logging**: Check console for detailed logs
3. **Database**: Use SQL Server Management Studio to inspect data
4. **Debugging**: Set breakpoints in IntelliJ IDEA or VS Code
5. **API Testing**: Use Swagger UI at http://localhost:8080/doc.html

## Next Steps

- Add file upload/download endpoints (FileController)
- Implement data import/export functionality
- Add more entities (Department, Role, Enterprise, etc.)
- Implement role-based access control with @PreAuthorize
- Add unit and integration tests
- Configure production deployment

## Support

- Swagger UI: http://localhost:8080/doc.html
- Backend logs: Console output or log files
- Frontend logs: Browser DevTools Console
