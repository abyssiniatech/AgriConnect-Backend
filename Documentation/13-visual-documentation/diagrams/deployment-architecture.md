# AgriConnect Ethiopia — Deployment Architecture

## 1. Purpose

This document describes how AgriConnect Ethiopia is deployed across development, testing, and production environments.

The deployment architecture separates the frontend, backend API, database, and supporting services to improve security, maintainability, and scalability.

---

# 2. Deployment Overview

```text
                         INTERNET
                            │
                            │ HTTPS
                            ▼
                ┌──────────────────────┐
                │      USERS           │
                │                      │
                │ Farmers              │
                │ Buyers               │
                │ Experts              │
                │ Logistics Providers  │
                └──────────┬───────────┘
                           │
                           ▼
                ┌──────────────────────┐
                │  ANGULAR FRONTEND    │
                │                      │
                │ Production Web App   │
                └──────────┬───────────┘
                           │
                           │ HTTPS / REST
                           ▼
                ┌──────────────────────┐
                │  ASP.NET CORE API    │
                │                      │
                │ Authentication       │
                │ Authorization        │
                │ Business Logic       │
                │ REST Endpoints       │
                └──────────┬───────────┘
                           │
                           │ Secure DB Connection
                           ▼
                ┌──────────────────────┐
                │     POSTGRESQL       │
                │                      │
                │ Application Data     │
                └──────────────────────┘
```

---

# 3. Environment Architecture

AgriConnect Ethiopia should use separate environments where practical.

```text
┌───────────────────┐
│ Development       │
│                   │
│ Local Developer   │
│ Frontend          │
│ Backend           │
│ PostgreSQL        │
└─────────┬─────────┘
          │
          │ Tested Changes
          ▼
┌───────────────────┐
│ Testing / Staging │
│                   │
│ Frontend          │
│ Backend API       │
│ Test Database     │
└─────────┬─────────┘
          │
          │ Approved Release
          ▼
┌───────────────────┐
│ Production        │
│                   │
│ Frontend          │
│ Backend API       │
│ PostgreSQL        │
└───────────────────┘
```

---

# 4. Development Environment

The development environment is used for local implementation.

Typical components:

```text
Developer Computer
      │
      ├── Angular
      ├── ASP.NET Core
      ├── Entity Framework Core
      ├── PostgreSQL
      └── Git
```

Developers should not use production credentials in the development environment.

---

# 5. Testing Environment

The testing environment is used to validate application changes before production.

It should contain:

* Frontend build.
* Backend API.
* Test database.
* Test configuration.
* Test data.
* Automated testing where available.

Testing should not modify production data.

---

# 6. Production Environment

The production environment serves real users.

A production deployment should contain:

```text
┌─────────────────────────────┐
│ Production Frontend         │
│ Angular Application          │
└──────────────┬──────────────┘
               │
               │ HTTPS
               ▼
┌─────────────────────────────┐
│ Production Backend          │
│ ASP.NET Core API            │
└──────────────┬──────────────┘
               │
               │ Secure Connection
               ▼
┌─────────────────────────────┐
│ Production PostgreSQL       │
│ Database                    │
└─────────────────────────────┘
```

---

# 7. Frontend Deployment

The Angular application should be built for production before deployment.

Typical process:

```text
Source Code
     ↓
Install Dependencies
     ↓
Run Tests
     ↓
Production Build
     ↓
Generate Static Files
     ↓
Deploy Frontend
     ↓
Verify Application
```

Example command:

```bash
ng build
```

The exact production build command should follow the project's Angular configuration.

---

# 8. Backend Deployment

The ASP.NET Core backend should be built and published before deployment.

Typical process:

```text
Source Code
     ↓
Restore Dependencies
     ↓
Build
     ↓
Run Tests
     ↓
Publish
     ↓
Deploy API
     ↓
Run Health Checks
     ↓
Verify API
```

Example:

```bash
dotnet restore
dotnet build
dotnet test
dotnet publish -c Release
```

---

# 9. Database Deployment

Database changes should be handled through controlled Entity Framework Core migrations.

Typical workflow:

```text
Code Change
     ↓
Entity Model Change
     ↓
Create Migration
     ↓
Review Migration
     ↓
Test Migration
     ↓
Backup Production
     ↓
Apply Migration
     ↓
Verify Database
```

Production database changes should never be performed without appropriate backup and recovery planning.

---

# 10. Configuration Management

Configuration should be separated from application source code.

Examples include:

```text
Database Connection String
JWT Configuration
External API Configuration
Email Configuration
Storage Configuration
CORS Origins
Environment Settings
```

Sensitive configuration must be stored securely.

Do not commit secrets to Git.

---

# 11. Environment Variables

Production secrets should be supplied through secure environment configuration.

Example:

```text
DATABASE_CONNECTION_STRING
JWT_SECRET
JWT_ISSUER
JWT_AUDIENCE
EXTERNAL_API_KEY
```

Actual secret values must never be stored in this documentation.

---

# 12. HTTPS

Production communication should use HTTPS.

```text
User
  │
  │ HTTPS
  ▼
Frontend
  │
  │ HTTPS
  ▼
Backend API
```

HTTP should not be used for sensitive production communication.

---

# 13. Domain Architecture

A production deployment may use separate domains or subdomains.

Example:

```text
https://app.example.com
        │
        │ Frontend
        │
        ▼

https://api.example.com
        │
        │ Backend API
        │
        ▼

PostgreSQL
```

The actual production domain names should be documented when they are finalized.

---

# 14. CORS Configuration

The backend should allow only trusted frontend origins.

Example:

```text
Allowed Origins:

https://app.example.com
```

Avoid allowing unrestricted origins in production:

```text
*
```

unless there is a documented and justified requirement.

---

# 15. Authentication Deployment

Authentication configuration must be environment-specific.

Production should use:

* Strong secrets.
* HTTPS.
* Secure token handling.
* Appropriate token expiration.
* Proper authorization policies.
* Secure password storage.

Development authentication credentials must not be reused in production.

---

# 16. Database Security

The production database should:

* Not be publicly exposed unnecessarily.
* Use strong credentials.
* Use encrypted connections where supported.
* Restrict network access.
* Use least-privilege database accounts.
* Have regular backups.
* Have a tested recovery process.

---

# 17. Backup Architecture

A basic backup strategy is:

```text
Production PostgreSQL
        │
        │ Scheduled Backup
        ▼
Backup Storage
        │
        │ Retention
        ▼
Recovery Point
```

Backups should be monitored and periodically tested.

A backup that has never been restored should not be assumed to be reliable.

---

# 18. Monitoring

Production should be monitored for:

* Application availability.
* API errors.
* Database availability.
* CPU usage.
* Memory usage.
* Disk usage.
* Response time.
* Authentication failures.
* Critical exceptions.

---

# 19. Health Checks

The backend should provide health information where appropriate.

Example conceptual endpoint:

```text
GET /health
```

Expected successful response:

```text
200 OK
```

Health checks can be used by deployment infrastructure and monitoring systems.

---

# 20. Logging

Production logs should provide enough information for troubleshooting without exposing sensitive information.

Logs may include:

* Request information.
* Error details.
* Application events.
* Authentication events.
* Database errors.
* Background process status.

Never log:

* Passwords.
* JWT secrets.
* API keys.
* Full authentication tokens.
* Sensitive personal information.

---

# 21. Deployment Pipeline

A future CI/CD pipeline may follow:

```text
Developer
    │
    ▼
Git Repository
    │
    ▼
Continuous Integration
    │
    ├── Build
    ├── Unit Tests
    ├── Integration Tests
    └── Security Checks
    │
    ▼
Staging
    │
    ├── Smoke Tests
    └── Acceptance Tests
    │
    ▼
Production Approval
    │
    ▼
Production Deployment
    │
    ├── Health Check
    └── Smoke Test
```

---

# 22. Deployment Checklist

Before production deployment:

* [ ] Code review completed.
* [ ] Build successful.
* [ ] Unit tests passed.
* [ ] Integration tests passed.
* [ ] API tests passed.
* [ ] Frontend tests passed.
* [ ] Security checks completed.
* [ ] Database migration reviewed.
* [ ] Database backup completed.
* [ ] Environment variables configured.
* [ ] HTTPS configured.
* [ ] CORS verified.
* [ ] Monitoring available.
* [ ] Rollback plan prepared.

---

# 23. Post-Deployment Checklist

After deployment:

* [ ] Frontend loads successfully.
* [ ] API is reachable.
* [ ] Health checks pass.
* [ ] Database connection works.
* [ ] Authentication works.
* [ ] Authorization works.
* [ ] Core user workflows work.
* [ ] Logs contain no critical errors.
* [ ] Monitoring is healthy.
* [ ] Users can access required functionality.

---

# 24. Rollback Architecture

If a deployment causes a critical failure:

```text
Production Release
       │
       │ Failure
       ▼
Incident Assessment
       │
       ▼
Rollback Decision
       │
       ▼
Previous Stable Version
       │
       ▼
Health Check
       │
       ▼
Service Restored
```

Database rollback must be handled separately when schema changes are involved.

---

# 25. Disaster Recovery

The recovery process should include:

1. Identify the failure.
2. Protect remaining data.
3. Determine the recovery point.
4. Restore infrastructure or application.
5. Restore database if necessary.
6. Verify application functionality.
7. Verify data integrity.
8. Resume service.
9. Document the incident.

---

# 26. Scalability

The architecture should allow future scaling.

Potential improvements include:

* Horizontal API scaling.
* Load balancing.
* Database optimization.
* Database connection pooling.
* Caching.
* Background processing.
* Object/file storage.
* CDN for frontend assets.
* Monitoring and alerting.

Scaling decisions should be based on actual application requirements and measured system behavior.

---

# 27. Security Boundaries

The production architecture should follow this principle:

```text
Internet
   │
   │ HTTPS
   ▼
Frontend
   │
   │ HTTPS
   ▼
API
   │
   │ Protected Database Connection
   ▼
Database
```

The database should not be directly accessible from the public internet unless there is a specific, secured requirement.

---

# 28. Related Documentation

Related documents:

```text
07-security/
08-testing/
10-developer-guide/
11-deployment/
12-project-management/
13-visual-documentation/diagrams/system-architecture.md
13-visual-documentation/diagrams/database-er-diagram.md
```

---

# 29. Conclusion

The AgriConnect Ethiopia deployment architecture separates application components and environments to improve security, reliability, maintainability, and scalability.

Production deployments should be performed through a controlled process involving testing, configuration management, database protection, health checks, monitoring, and rollback planning.
