# AgriConnect Ethiopia — Deployment Guide

## 1. Introduction

This document describes the deployment process for AgriConnect Ethiopia.

The deployment process prepares the application for a production environment and ensures that the frontend, backend, database, configuration, security, and monitoring components are correctly configured.

The main deployment components are:

```text
Frontend
   ↓
ASP.NET Core API
   ↓
PostgreSQL Database
   ↓
External Services
```

---

# 2. Deployment Environments

AgriConnect can operate in multiple environments:

| Environment | Purpose                      |
| ----------- | ---------------------------- |
| Development | Local feature development    |
| Testing     | Automated and manual testing |
| Staging     | Pre-production validation    |
| Production  | Live application             |

Configuration and credentials must be separated between environments.

---

# 3. Deployment Requirements

Before deploying, verify that the target environment has the required infrastructure.

Typical requirements include:

* Linux or Windows server.
* .NET runtime.
* PostgreSQL.
* Node.js/build environment where frontend builds are performed.
* Web server or reverse proxy.
* HTTPS certificate.
* Domain name.
* Environment variables.
* Database credentials.
* Backup strategy.

---

# 4. Pre-Deployment Preparation

Before deployment:

1. Confirm the release version.
2. Review the changes.
3. Run backend tests.
4. Run frontend tests.
5. Build the backend.
6. Build the frontend.
7. Review database migrations.
8. Verify environment variables.
9. Create a database backup.
10. Review the production checklist.

---

# 5. Source Code Preparation

Update the local repository:

```bash
git checkout main
git pull
```

Verify the working tree:

```bash
git status
```

The working tree should be clean before creating a release.

Review recent commits:

```bash
git log --oneline -10
```

---

# 6. Backend Build

Restore backend dependencies:

```bash
dotnet restore
```

Build the backend:

```bash
dotnet build --configuration Release
```

Run backend tests:

```bash
dotnet test --configuration Release
```

The build and tests should complete successfully before deployment.

---

# 7. Backend Publish

Publish the ASP.NET Core application:

```bash
dotnet publish --configuration Release --output ./publish
```

The generated publish directory contains the files required to run the backend.

The exact publish command may vary according to the solution structure.

---

# 8. Frontend Build

Navigate to the frontend project:

```bash
cd frontend
```

Install dependencies:

```bash
npm ci
```

Build the production application:

```bash
npm run build
```

Use the project's configured production build command if it differs.

The generated output directory should contain the static frontend files.

---

# 9. Database Preparation

Before applying production database changes:

1. Back up the database.
2. Review pending migrations.
3. Confirm the target database.
4. Confirm database credentials.
5. Verify migration compatibility.
6. Apply migrations according to the project's production procedure.

Check migrations:

```bash
dotnet ef migrations list
```

Do not experiment with migrations directly against production.

---

# 10. Database Migration

For controlled environments, apply the approved Entity Framework Core migrations.

Example:

```bash
dotnet ef database update
```

For production environments, follow the organization's approved migration and change-management process.

After migration, verify:

* Tables exist.
* Required indexes exist.
* Required constraints exist.
* Application can connect.
* Existing data remains intact.

---

# 11. Production Configuration

Production configuration must include the required settings for:

* Database.
* Authentication.
* Authorization.
* CORS.
* Logging.
* External APIs.
* File storage.
* Notifications.
* AI services where applicable.

Sensitive values must be supplied through secure environment configuration or a secret-management system.

---

# 12. Environment Variables

Production secrets should not be stored directly in source code.

Typical configuration may include:

```text
DATABASE_CONNECTION_STRING
JWT_SECRET
API_BASE_URL
CORS_ALLOWED_ORIGINS
AI_API_KEY
```

Only variables required by the actual implementation should be configured.

See:

```text
11-deployment/environment-variables.md
```

for the environment variable reference.

---

# 13. HTTPS Configuration

Production traffic should use HTTPS.

The deployment should provide:

```text
https://your-domain.example
```

rather than exposing the application through plain HTTP.

Configure a valid TLS certificate and redirect HTTP traffic to HTTPS where appropriate.

---

# 14. Reverse Proxy

A reverse proxy can sit in front of the ASP.NET Core application.

Typical architecture:

```text
Internet
   ↓
HTTPS
   ↓
Reverse Proxy
   ↓
ASP.NET Core API
   ↓
PostgreSQL
```

Common reverse-proxy responsibilities include:

* HTTPS termination.
* Request forwarding.
* Security headers.
* Compression.
* Request size limits.
* Access logging.

---

# 15. Frontend Deployment

The Angular production build generates static files.

These files can be served using an appropriate web server or hosting platform.

Typical deployment flow:

```text
Angular Source
      ↓
npm run build
      ↓
Production Build
      ↓
Web Server / Hosting Platform
      ↓
Users
```

Ensure that the frontend is configured to communicate with the production API URL.

---

# 16. Backend Deployment

Copy the published backend application to the target server.

Start the application using the approved hosting mechanism.

For example:

```bash
dotnet AgriConnect.Api.dll
```

The actual DLL name depends on the project.

For production, use a process manager or service mechanism rather than relying on an interactive terminal session.

---

# 17. Process Management

The backend should automatically restart after:

* Server reboot.
* Application failure.
* Deployment restart.

On Linux, a service manager such as `systemd` can be used.

The exact service configuration should match the hosting environment.

---

# 18. Database Deployment

Production PostgreSQL should be:

* Accessible only from authorized systems.
* Protected with strong credentials.
* Backed up regularly.
* Monitored.
* Properly sized.
* Configured according to organizational security requirements.

Do not expose PostgreSQL directly to the public internet unless there is a specific secured architecture requiring it.

---

# 19. CORS Configuration

Configure production CORS using the actual frontend domain.

Example:

```text
https://app.example.com
```

Do not use unrestricted CORS in production for authenticated applications.

Verify that only trusted frontend origins can access the API where appropriate.

---

# 20. Authentication Configuration

Before production deployment, verify:

* JWT signing secret is secure.
* Token expiration is appropriate.
* Issuer is correct.
* Audience is correct.
* HTTPS is enabled.
* Authentication endpoints work.
* Password handling is secure.
* Authorization policies are enabled.

Never use development authentication secrets in production.

---

# 21. API Documentation

API documentation may be useful during development and testing.

Production exposure should follow the project's security requirements.

If public API documentation is not required, restrict or disable development documentation endpoints in production.

---

# 22. Health Checks

The production application should provide appropriate health monitoring.

Where implemented, verify:

```text
Application health
Database connectivity
Critical dependencies
```

A health endpoint may look like:

```text
/api/health
```

Use the actual health endpoint implemented by the project.

---

# 23. Deployment Verification

After deployment, verify:

### Frontend

* [ ] Homepage loads.
* [ ] Navigation works.
* [ ] Assets load.
* [ ] API requests work.
* [ ] Authentication works.

### Backend

* [ ] API starts.
* [ ] Health check succeeds.
* [ ] Authentication works.
* [ ] Authorization works.
* [ ] Database connection works.
* [ ] Expected endpoints respond.

### Database

* [ ] Database is accessible.
* [ ] Migrations are applied.
* [ ] Required tables exist.
* [ ] Existing data is intact.

---

# 24. Smoke Testing

Perform basic smoke tests after deployment.

Test:

1. Open the frontend.
2. Register or authenticate a test user if applicable.
3. Log in.
4. View available agricultural information.
5. Test an API request.
6. Create a permitted resource.
7. Verify the resource.
8. Test authorization.
9. Log out.
10. Review application logs.

---

# 25. Rollback Strategy

Every production deployment should have a rollback plan.

Possible rollback actions include:

* Restore the previous frontend build.
* Restore the previous backend version.
* Revert configuration changes.
* Restore the database from backup when necessary.

Database rollbacks require special care because application versions and database schemas must remain compatible.

---

# 26. Deployment Failure

If deployment fails:

1. Stop further deployment changes.
2. Record the error.
3. Check application logs.
4. Check infrastructure logs.
5. Check database connectivity.
6. Check environment variables.
7. Verify the deployed version.
8. Determine whether rollback is required.
9. Document the resolution.

Do not make unrelated production changes while investigating the failure.

---

# 27. Monitoring

Production should monitor:

* Application availability.
* API response times.
* HTTP errors.
* Database availability.
* Server resources.
* Authentication failures.
* Application exceptions.
* External service failures.

Logs should be stored securely and retained according to organizational requirements.

---

# 28. Security After Deployment

After deployment:

* Verify HTTPS.
* Verify CORS.
* Verify authentication.
* Verify authorization.
* Check exposed ports.
* Remove development-only configuration.
* Protect API documentation where necessary.
* Verify database access restrictions.
* Verify secrets are not exposed.
* Review server logs.

---

# 29. Deployment Checklist

Before declaring a release successful:

* [ ] Source code reviewed.
* [ ] Backend builds successfully.
* [ ] Backend tests pass.
* [ ] Frontend builds successfully.
* [ ] Frontend tests pass.
* [ ] Database backup completed.
* [ ] Migrations reviewed.
* [ ] Production configuration verified.
* [ ] Secrets configured securely.
* [ ] HTTPS enabled.
* [ ] CORS configured.
* [ ] Authentication verified.
* [ ] Authorization verified.
* [ ] Backend deployed.
* [ ] Frontend deployed.
* [ ] Database updated.
* [ ] Smoke tests completed.
* [ ] Monitoring verified.
* [ ] Rollback plan available.

---

# 30. Conclusion

A successful AgriConnect deployment requires coordinated preparation of the frontend, backend, database, configuration, security, and monitoring systems.

Production deployments should be repeatable, documented, tested, and reversible wherever possible. Sensitive credentials must remain protected, database changes must be controlled, and every deployment should be verified through smoke testing and monitoring.
